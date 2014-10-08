using System;
using System.Collections.Generic;
using System.IO;

using Duomo.Common.Lib;
using Duomo.Common.Lib.IO.Serialization;


namespace Duomo.Common.Gunther.Lib
{
    public class XmlFileScheduledJobSpecificationsListSource : IScheduledJobSpecificationsListSource
    {
        #region IScheduledJobSpecificationsListSource Members

        public List<IScheduledJobSpecification> ScheduledJobs { get; protected set; }

        #endregion


        public XmlFileScheduledJobSpecificationsListSource()
        {
            this.Setup();

            string xmlFileRootedPath = Path.Combine(Utilities.ExecutableFolderRootedPath, @"Files", @"ScheduledJobs.xml");

            this.AddScheduledJobsFromFile(xmlFileRootedPath);
        }

        public XmlFileScheduledJobSpecificationsListSource(string xmlFileRootedPath)
        {
            this.Setup();

            this.AddScheduledJobsFromFile(xmlFileRootedPath);
        }

        private void Setup()
        {
            this.ScheduledJobs = new List<IScheduledJobSpecification>();
        }

        private void AddScheduledJobsFromFile(string xmlFileRootedPath)
        {
            ScheduledJobSpecificationList list = XmlSerializer<ScheduledJobSpecificationList>.DeserializeFromRootedPath(xmlFileRootedPath);
            foreach (ScheduledJobSpecification scheduledJobSpec in list.ScheduledJob)
            {
                this.ScheduledJobs.Add(scheduledJobSpec);
            }
        }
    }
}
