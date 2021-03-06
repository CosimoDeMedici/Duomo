﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Duomo.Common.Lib.Dates;
using Duomo.Common.Lib.IO;
using Duomo.Common.Gunther.Lib;


namespace Duomo.Common.Gunther
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            GuntherCommandLineArgumentsStructure inputArgs = new GuntherCommandLineArgumentsStructure(args);

            // Should be done via configuration of the GuntherModelImodel.
            GuntherModel model = new GuntherModel();
            model.DateOperationsProvider = new BasicDateOperationProvider();
            model.ScheduledJobsListSource = new XmlFileScheduledJobSpecificationsListSource(); //new DummyScheduledJobSpecificationsListSource();
            model.JobRepository = new BasicJobRepository();

            Application.Run(new GuntherMainView(model, true));
        }
    }
}
