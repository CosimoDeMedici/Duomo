using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using Duomo.Common.Gunther.Lib;
using Duomo.Common.Gunther.Forecaster.Lib;


namespace Duomo.Common.Gunther.Forecaster
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

            //SendTestEmail.Test();

            GuntherForecasterModel model = new GuntherForecasterModel();
            model.StartDate = DateTime.Today;
            model.EndDate = model.StartDate.AddDays(2);

            model.DateOperationsProvider = new BasicDateOperationProvider();
            model.ScheduledJobSpecificationsListSource = new DummyScheduledJobListSource();
            model.ForecastEmitter = new EmailScheduleForecastEmitter();

            model.CreateScheduledTimeSpecifications();
            model.EmitScheduleForecast();
        }
    }
}
