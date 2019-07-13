using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32.TaskScheduler;
using System.IO;
using System.Windows.Forms;
using System.Security.Principal;
using System.Diagnostics;


namespace Builder_Companion
{
    public static class TaskServicer
    {
        private const string taskDef = "BUILDER_COMPANION";

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
