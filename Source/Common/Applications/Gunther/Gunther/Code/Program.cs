using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Duomo.Common.Gunther.Lib;


namespace Gunther
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

            // Should be done via configuration of the GuntherModelImodel.
            GuntherModel model = new GuntherModel();
            model.DateOperationsProvider = new BasicDateOperationProvider();
            model.JobRepository = new BasicJobRepository();

            // Should also be done via configuration, but can also be done programmatically.
            DummyScheduledJobListSource jobsSource = new DummyScheduledJobListSource();
            List<IScheduledJobSpecification> scheduledJobs = jobsSource.ScheduledJobs;
            model.AddScheduledJobs(scheduledJobs);

            model.Start();

            // Need something for the main thread to do! Perhaps use this thread as the scheduler and use some of the Async wait methods?
            Application.Run(new Form1());
        }
    }
}
