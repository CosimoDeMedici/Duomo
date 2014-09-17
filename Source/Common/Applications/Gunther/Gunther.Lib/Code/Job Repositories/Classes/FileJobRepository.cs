using System;
using System.IO;

using Duomo.Common.Lib.IO.Serialization;


namespace Duomo.Common.Gunther.Lib
{
    public class FileJobRepository : IJobRepository
    {
        #region IJobRepository Members

        public void Add(IJobSpecification jobSpecification, DateTime scheduledTime)
        {
            throw new NotImplementedException();
        }

        #endregion


        public string RootFolderRootedPath { get; protected set; }
        public string MaximumJobNumberFileRootedPath { get; protected set; }
        public string JobsTODOFolderRootedPath { get; protected set; }
        public string JobsFolderRootedPath { get; protected set; }
        private int NextJobIDNumber { get; set; }


        public FileJobRepository(string rootFolderRootedPath)
        {
            this.RootFolderRootedPath = rootFolderRootedPath;

            this.MaximumJobNumberFileRootedPath = Path.Combine(this.RootFolderRootedPath, "MaximumJobNumber.txt");
            this.JobsTODOFolderRootedPath = Path.Combine(this.RootFolderRootedPath, "JobsTODO");
            this.JobsFolderRootedPath = Path.Combine(this.RootFolderRootedPath, "Jobs");

            TextFile
        }
    }
}
