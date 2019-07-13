using System;
using WUApiLib;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;

// Exit Codes:
//   0 = scripting failure
//   1 = error obtaining or installing updates
//   2 = installation successful, no further updates to install
//   3 = reboot needed; rerun script after reboot

namespace Builder_Companion
{    
    //Consider embedding in partial form class
    public partial class Form1: Form
    {
        private bool _updateSessionComplete = false;
        private BackgroundWorker EnableServicesWorker;

        public UpdateSession updateSession;
        #region<------- Search Section ------->

        public IUpdateSearcher iUpdateSearcher;

        public ISearchJob iSearchJob;

        public UpdateCollection NewUpdatesCollection;

        public ISearchResult NewUpdatesSearchResult;

        #endregion <------- Search Section ------->

        #region <------- Downloader Section ------->

        public IUpdateDownloader iUpdateDownloader;

        public IDownloadJob iDownloadJob;

        public IDownloadResult iDownloadResult;

        #endregion <------- Downloader Section ------->

        #region <------- Installer Section ------->

        public IUpdateInstaller iUpdateInstaller;

        public IInstallationJob iInstallationJob;

        public IInstallationResult iInstallationResult;
        #endregion <------- Installer Section ------->

        public delegate void UpdStatus();
        public UpdStatus updStatus;

        private int count = 0;

        public int Count
        {
            get { return count; }
            set { count = value; }
        }

        public void DoUpdates()
        {
            updStatus = new UpdStatus(WUPDone);
            EnableServicesWorker = new BackgroundWorker();
            EnableServicesWorker.DoWork += EnableServicesWorker_DoWork;
            EnableServicesWorker.RunWorkerCompleted += EnableServicesWorker_RunWorkerCompleted;

            this.WUP.Text = "Enabling Update Services...";

            // Lets check Windows is up to that task...
            EnableServicesWorker.RunWorkerAsync();
        }

        #region <------- Service Methods ------->
        private void EnableServicesWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // Get Services Collection...
            ServiceController[] serviceController;
            serviceController = ServiceController.GetServices();

            // Loop through and check for a particular Service...
            foreach (ServiceController scTemp in serviceController)
            {
                switch (scTemp.DisplayName)
                {
                    case "Windows Update":
                        RestartService(scTemp.DisplayName, 5000);
                        break;
                    case "Automatic Updates":
                        RestartService(scTemp.DisplayName, 5000);
                        break;
                    default:
                        break;
                }
            }

            // Check for iAutomaticUpdates.ServiceEnabled...
            IAutomaticUpdates iAutomaticUpdates = new AutomaticUpdates();
            if (!iAutomaticUpdates.ServiceEnabled)
            {
                iAutomaticUpdates.EnableService();
            }
        }

        private void EnableServicesWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            StartSearch();
        }

        public static void RestartService(string serviceName, int timeoutMilliseconds)
        {
            ServiceController serviceController = new ServiceController(serviceName);
            try
            {
                int millisec1 = Environment.TickCount;
                TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);

                serviceController.Stop();
                serviceController.WaitForStatus(ServiceControllerStatus.Stopped, timeout);

                // count the rest of the timeout
                int millisec2 = Environment.TickCount;
                timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds - (millisec2 - millisec1));

                serviceController.Start();
                serviceController.WaitForStatus(ServiceControllerStatus.Running, timeout);
            }
            catch
            {
                // ...
            }
        }

        #endregion <------- Service Methods ------->

        /* WUp Entry point
         */
        private void StartSearch()
        {
            updateSession = new UpdateSession();
            iUpdateSearcher = updateSession.CreateUpdateSearcher();
            //iUpdateSearcher.Online = true;   //Only search online
            iSearchJob = iUpdateSearcher.BeginSearch("IsInstalled=0 AND IsPresent=0", new tSearcher_onCompleted(this), new tSearcher_state(this));
        }

        private void SearchComplete(Form1 mainForm)
        {
            Form1 formRef = mainForm;

            NewUpdatesCollection = new UpdateCollection();
            NewUpdatesSearchResult = iUpdateSearcher.EndSearch(iSearchJob);
            Count = NewUpdatesSearchResult.Updates.Count;

            // Accept Eula code for each update
            for (int i = 0; i < NewUpdatesSearchResult.Updates.Count; i++)
            {
                IUpdate iUpdate = NewUpdatesSearchResult.Updates[i];

                if (iUpdate.EulaAccepted == false)
                {
                    iUpdate.AcceptEula();
                }

                NewUpdatesCollection.Add(iUpdate);      
            }

            if (NewUpdatesSearchResult.Updates.Count > 0)
            {
                UpdatesDownload();                
            } else
            {
                formRef.Invoke(formRef.updStatus);
            }
        }

        private void UpdatesDownload()
        {
            updateSession = new UpdateSession();
            iUpdateDownloader = updateSession.CreateUpdateDownloader();

            iUpdateDownloader.Updates = NewUpdatesCollection;
            iUpdateDownloader.Priority = DownloadPriority.dpHigh;
            iDownloadJob = iUpdateDownloader.BeginDownload(new tDownload_onProgressChanged(this), new tDownload_onCompleted(this), new tDownload_state(this));
        }

        public void DownloadComplete()
        {
            iDownloadResult = iUpdateDownloader.EndDownload(iDownloadJob);

            switch (iDownloadResult.ResultCode)
            {
                case OperationResultCode.orcSucceeded:
                    // Complete
                    Installation();
                    break;
                case OperationResultCode.orcSucceededWithErrors:
                    // Need reboot
                    WUP.Text = "Reboot system";
                    WUP.ForeColor = Color.GreenYellow;
                    break;
                default:
                    WUP.Text = "Updates download error";
                    WUP.ForeColor = Color.YellowGreen;
                    break;
            }
        }

        public void Installation()
        {
            iUpdateInstaller = updateSession.CreateUpdateInstaller() as IUpdateInstaller;
            iUpdateInstaller.Updates = NewUpdatesCollection;

            iInstallationJob = iUpdateInstaller.BeginInstall(new tInstall_onProgressChanged(this), new tInstall_onCompleted(this), new tInstall_state(this));
        }

        public void InstallationComplete()
        {            
            iInstallationResult = iUpdateInstaller.EndInstall(iInstallationJob);
            switch (iInstallationResult.ResultCode)
            {
                case OperationResultCode.orcSucceeded:
                    // Complete                   
                    WUP.Text = "Updates installation complete...";
                    WUP.ForeColor = Color.Green;                   
                    break;
                case OperationResultCode.orcSucceededWithErrors:
                    // Need reboot
                    WUP.Text = "Reboot system";
                    WUP.ForeColor = Color.GreenYellow;
                    break;
                default:
                    WUP.Text = "There was a problem installing updates";
                    WUP.ForeColor = Color.YellowGreen;
                    break;
            }
            _updateSessionComplete = true;
            if (Properties.Settings.Default.TestComplete)
            {
                Restart();
            }
        }

        #region <------- Notification Methods ------->        

        public void SetWUP(string status)
        {
            WUP.Text = status;
        }                                 
        #endregion <------- Notification Methods ------->


        public class tSearcher_onCompleted : ISearchCompletedCallback
        {
            private Form1 _form;

            public tSearcher_onCompleted(Form1 form)
            {
                _form = form;
            }

            public void Invoke(ISearchJob sJob, ISearchCompletedCallbackArgs e)
            {
                _form.SearchComplete(_form);
            }
        }

        public class tSearcher_state
        {
            private Form1 _form;

            public tSearcher_state(Form1 form)
            {
                this._form = form;
                _form.SetWUP("Searching for updates...");
            }
        }

        public class tDownload_onProgressChanged : IDownloadProgressChangedCallback
        {
            private Form1 _form;

            public tDownload_onProgressChanged(Form1 form)
            {
                this._form = form;
            }

            public void Invoke(IDownloadJob dj, IDownloadProgressChangedCallbackArgs e)
            {
                decimal bDone = ((e.Progress.TotalBytesDownloaded / 1024) / 1024);
                decimal bLeft = ((e.Progress.TotalBytesToDownload / 1024) / 1024);
                bDone = decimal.Round(bDone, 2);
                bLeft = decimal.Round(bLeft, 2);

                _form.SetWUP("Downloading update: "
                     + e.Progress.CurrentUpdateIndex
                     + "/"
                     + dj.Updates.Count
                     + " - "
                     + bDone + "Mb"
                     + " / "
                     + bLeft + "Mb");
            }
        }

        public class tDownload_onCompleted : IDownloadCompletedCallback
        {
            private Form1 _form;

            public tDownload_onCompleted(Form1 form)
            {
                this._form = form;
            }

            public void Invoke(IDownloadJob dj, IDownloadCompletedCallbackArgs e)
            {
                _form.DownloadComplete();
            }
        }

        public class tDownload_state
        {
            private Form1 _form;

            public tDownload_state(Form1 form)
            {
                this._form = form;
                _form.SetWUP("Updates download started...");
            }
        }

        public class tInstall_onProgressChanged : IInstallationProgressChangedCallback
        {
            private Form1 _form;

            public tInstall_onProgressChanged(Form1 form)
            {
                this._form = form;
            }

            public void Invoke(IInstallationJob iJob, IInstallationProgressChangedCallbackArgs e)
            {                
                _form.SetWUP("Installing update: "
                    + e.Progress.CurrentUpdateIndex
                    + " / "
                    + iJob.Updates.Count
                    + " - "
                    + e.Progress.CurrentUpdatePercentComplete + "% complete");                    
            }
        }

        public class tInstall_onCompleted : IInstallationCompletedCallback
        {
            private Form1 _form;

            public tInstall_onCompleted(Form1 form)
            {
                this._form = form;
            }

            public void Invoke (IInstallationJob iIJob, IInstallationCompletedCallbackArgs e)
            {
                _form.InstallationComplete();
            }
        }

        public class tInstall_state
        {
            private Form1 _form;

            public tInstall_state(Form1 form)
            {
                this._form  = form;
                _form.SetWUP("Updates installation started...");
            }
        }
    }
}
