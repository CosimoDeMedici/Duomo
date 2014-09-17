using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using Duomo.Common.Lib.IO.Serialization;
using Duomo.Common.Gunther.Lib;


namespace Gunther.JobRepository
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());

            JobRun jobRun = new JobRun();
            jobRun.ID = 1;

            SystemProcessCall sysProcCall = new SystemProcessCall();
            sysProcCall.Name = "Hello World!";
            sysProcCall.Description = "Basic hello world.";
            sysProcCall.Value = @"C:\temp\temp.exe";
            jobRun.Item = sysProcCall;

            jobRun.StartDateTime = DateTime.MaxValue;
            jobRun.EndDateTime = DateTime.MinValue;

            string rootedPath = @"C:\temp\TestJobRunFile";
            XmlSerializer<JobRun>.StaticSerializeToRootedPath(jobRun, rootedPath);
        }
    }
}
