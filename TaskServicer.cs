/*
 * TaskServicer.cs
 * 
 * @Author Magnus Hjorth
 * 
 * File Description: This class holds static methods needed to create and destroy windows tasks,
 * allowing the program to be automatically started with admin rights on startup.
 */

using Microsoft.Win32.TaskScheduler;
using System.IO;
using System.Windows.Forms;

namespace Builder_Companion
{
    public static class TaskServicer
    {
        private const string taskDef = "BUILDER_COMPANION";

        /// <summary>
        /// Creates a Windows Task which will start up this application with admin rights at every startup.
        /// </summary>
        public static void CreateTaskService()
        {
            TaskService ts = new TaskService();
            TaskDefinition td = ts.NewTask();
            td.Principal.RunLevel = TaskRunLevel.Highest;
            td.Triggers.AddNew(TaskTriggerType.Logon);
            //td.Triggers.AddNew(TaskTriggerType.Once);   
            string program_path = Path.Combine(Application.ExecutablePath);                                                                               

            td.Actions.Add(new ExecAction(program_path, null));
            ts.RootFolder.RegisterTaskDefinition(taskDef, td);
          
        }

        /// <summary>
        /// Deletes the Windows Task for this application if any.
        /// </summary>
        public static void DeleteTaskService()
        {
            EnumAllTasks();
        }

        private static void EnumAllTasks()
        {
            EnumFolderTasks(TaskService.Instance.RootFolder);
        }

        private static void EnumFolderTasks(TaskFolder fld)
        {
            foreach (Task task in fld.Tasks)
                DeleteTask(task);
            foreach (TaskFolder sfld in fld.SubFolders)
                EnumFolderTasks(sfld);
        }

        private static void DeleteTask(Task t)
        {
            // Only delete if our task
            if (t.Name.Equals(taskDef))
            {
                TaskService ts = t.TaskService;
                ts.RootFolder.DeleteTask(t.Name);
            }
        }
    }
}
