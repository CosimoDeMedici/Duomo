using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Duomo.Common.Lib;
using Duomo.Common.Gunther.Lib;


namespace Duomo.Common.Gunther
{
    public partial class GuntherMainView : Form
    {
        private GuntherModel Model { get; set; }
        private Dictionary<ScheduledTimeSpecification, DataGridViewRow> ScheduledTimeSpecificationsToDataGridViewRows { get; set; }
        private Timer Timer { get; set; }


        public GuntherMainView(GuntherModel model, bool autoStart)
        {
            InitializeComponent();

            this.Setup();

            this.Model = model;

            this.InitializeComponents();

            this.BindViewToModel();

            if (autoStart)
            {
                model.Start();
            }
        }

        private void Setup()
        {
            this.ScheduledTimeSpecificationsToDataGridViewRows = new Dictionary<ScheduledTimeSpecification, DataGridViewRow>();
        }

        private void InitializeComponents()
        {
            this.InitializeJobListDataGridView();
            this.InitializeTimer();
        }

        private void InitializeTimer()
        {
            this.Timer = new Timer();
            this.Timer.Interval = 1000; // Tick every second.
            this.Timer.Tick += new EventHandler(Timer_Tick);
            this.Timer.Start();
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            this.SetCurrentTime();
        }

        private void SetCurrentTime()
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate { this.SetCurrentTime(); });
            }
            else
            {
                this.CurrentTimeLabel.Text = this.FormatDateTime(DateTime.Now);
            }
        }

        private void BindViewToModel()
        {
            this.Model.ScheduledTimeSpecificationAdded += new ScheduledTimeSpecificationListChanged(Model_ScheduledTimeSpecificationAdded);
            this.Model.ScheduledTimeSpecificationRemoved += new ScheduledTimeSpecificationListChanged(Model_ScheduledTimeSpecificationRemoved);

            foreach (ScheduledTimeSpecification scheduledTimeSpec in this.Model.ScheduledTimes)
            {
                int index = this.JobsListDataGridView.Rows.Add();
                DataGridViewRow row = this.JobsListDataGridView.Rows[index];

                row.Cells["ScheduledTime"].Value = this.FormatDateTime(scheduledTimeSpec.ScheduledTime);
                row.Cells["JobDescription"].Value = scheduledTimeSpec.ScheduledJob.JobSpecification;

                this.ScheduledTimeSpecificationsToDataGridViewRows.Add(scheduledTimeSpec, row);
                row.Tag = scheduledTimeSpec;
            }
        }

        void Model_ScheduledTimeSpecificationAdded(object sender, ScheduledTimeSpecificationListChangedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate { this.Model_ScheduledTimeSpecificationAdded(sender, e); });
            }
            else
            {
                ScheduledTimeSpecification scheduledTimeSpec = e.ScheduledTimeSpecification;

                int index = this.JobsListDataGridView.Rows.Add();
                DataGridViewRow row = this.JobsListDataGridView.Rows[index];

                row.Cells["ScheduledTime"].Value = this.FormatDateTime(scheduledTimeSpec.ScheduledTime);
                row.Cells["JobDescription"].Value = scheduledTimeSpec.ScheduledJob.JobSpecification;

                this.ScheduledTimeSpecificationsToDataGridViewRows.Add(scheduledTimeSpec, row);
                row.Tag = scheduledTimeSpec;
            }
        }

        void Model_ScheduledTimeSpecificationRemoved(object sender, ScheduledTimeSpecificationListChangedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate { this.Model_ScheduledTimeSpecificationRemoved(sender, e); });
            }
            else
            {
                ScheduledTimeSpecification scheduledTimeSpec = e.ScheduledTimeSpecification;

                DataGridViewRow row = this.ScheduledTimeSpecificationsToDataGridViewRows[scheduledTimeSpec];
                this.ScheduledTimeSpecificationsToDataGridViewRows.Remove(scheduledTimeSpec);

                this.JobsListDataGridView.Rows.Remove(row);
            }
        }

        private void InitializeJobListDataGridView()
        {
            this.JobsListDataGridView.AllowUserToAddRows = false;
            this.JobsListDataGridView.AllowUserToDeleteRows = false;
            this.JobsListDataGridView.AllowUserToOrderColumns = false;
            this.JobsListDataGridView.AutoGenerateColumns = false;

            DataGridViewColumn scheduledTimeColumn = new DataGridViewColumn();
            DataGridViewCell dateTimeCell = new DataGridViewTextBoxCell();
            scheduledTimeColumn.CellTemplate = dateTimeCell;
            scheduledTimeColumn.Name = "ScheduledTime";
            scheduledTimeColumn.HeaderText = "Scheduled Time";
            scheduledTimeColumn.Width = 150;
            scheduledTimeColumn.ReadOnly = true;
            
            this.JobsListDataGridView.Columns.Add(scheduledTimeColumn);

            DataGridViewColumn jobDescriptionColumn = new DataGridViewColumn();
            DataGridViewCell jobDescriptionCell = new DataGridViewTextBoxCell();
            jobDescriptionColumn.CellTemplate = jobDescriptionCell;
            jobDescriptionColumn.Name = "JobDescription";
            jobDescriptionColumn.HeaderText = "Job Description";
            jobDescriptionColumn.Width = 800;
            jobDescriptionColumn.ReadOnly = true;

            this.JobsListDataGridView.Columns.Add(jobDescriptionColumn);
        }

        private void RunJobNowButton_Click(object sender, EventArgs e)
        {
            // Get the current job.
            ScheduledTimeSpecification scheduleTimeSpec = (ScheduledTimeSpecification)this.JobsListDataGridView.CurrentRow.Tag;

            // Add the current job the job repository. TODO, change this to run immediately.
            this.Model.JobRepository.Add(scheduleTimeSpec.ScheduledJob.JobSpecification, scheduleTimeSpec.ScheduledTime);
        }

        private string FormatDateTime(DateTime dateTime)
        {
            string retValue = String.Format("{0:yyyy-MM-dd HH:mm:ss}", dateTime);
            return retValue;
        }

        private void LoadFileMainMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Xml Files|*.xml";
            ofd.Multiselect = false;
            ofd.InitialDirectory = Utilities.ExecutableFolderRootedPath;

            if (DialogResult.OK == ofd.ShowDialog())
            {
                string xmlFileRootedPath = ofd.FileName;
                this.Model.ScheduledJobsListSource = new XmlFileScheduledJobSpecificationsListSource(xmlFileRootedPath);
                this.Model.AddScheduledJobs();
            }
        }
    }
}
