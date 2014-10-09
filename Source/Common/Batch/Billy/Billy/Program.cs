using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using IWshRuntimeLibrary;
using SharpSvn;

using Duomo.Common.Lib;
using Duomo.Common.Lib.IO.Serialization;


namespace Billy
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

            string localProdDirectoryRootedPath = @"C:\Code\PROD";
            string repositorySourceDirectoryUri = @"https://github.com/CosimoDeMedici/Duomo/trunk/Source"; // NOTE, http:// requires final '/' whereas svn:// does not.
            int repositorySourceDirectoryUriLength = repositorySourceDirectoryUri.Length;

            // TODO, will need to ensure no duplicate repository solution URIs.
            Dictionary<string, int> repositorySolutionUrisAndRevisions = new Dictionary<string, int>();
            repositorySolutionUrisAndRevisions.Add(@"https://github.com/CosimoDeMedici/Duomo/trunk/Source/Common/Applications/Gunther/Duomo.Common.Gunther.sln", 17);

            // Ensure that all repository solution URIs are in the source directory.
            Dictionary<string, int> repositorySolutionRelativeUrisAndRevisions = new Dictionary<string, int>();
            foreach (string curUri in repositorySolutionUrisAndRevisions.Keys)
            {
                if (curUri.Substring(0, repositorySourceDirectoryUriLength) == repositorySourceDirectoryUri)
                {
                    string solutionRelativeUri = curUri.Substring(repositorySourceDirectoryUriLength);
                    repositorySolutionRelativeUrisAndRevisions.Add(solutionRelativeUri, repositorySolutionUrisAndRevisions[curUri]);
                }
                else
                {
                    throw new ApplicationException(String.Format("Repository solution URI '{0}' was not within the repositoryl source directory URI '{1}'.", curUri, repositorySourceDirectoryUri));
                }
            }

            // Checkout (the checkout command will update if the directory already exists) the Lib directory. This assumes there is a 'Lib' directory as a sibling to the main 'Source' directory.
            string libFolderName = @"Lib";
            string localLibDirectoryRootedPath = Path.Combine(localProdDirectoryRootedPath, libFolderName);

            Uri uri = new Uri(repositorySourceDirectoryUri);
            string repositorySourceParentDirectoryUri = uri.GetParentUriString();
            uri = new Uri(repositorySourceParentDirectoryUri);
            string repositoryLibDirectoryUri = uri.AppendSegment(libFolderName);

            using (SvnClient client = new SvnClient())
            {
                SvnUriTarget target = new SvnUriTarget(repositoryLibDirectoryUri);
                client.CheckOut(new SvnUriTarget(repositoryLibDirectoryUri), localLibDirectoryRootedPath);
            }

            // For each solution, check out the solution's directory to the proper local path.
            WshShell wshShell = new WshShellClass(); // For creating a short-cut to the solution.
            foreach (string curRelativeUri in repositorySolutionRelativeUrisAndRevisions.Keys)
            {
                // Check out the solution.
                string curRepoRootedUri = repositorySourceDirectoryUri + curRelativeUri;
                Uri curRepoUri = new Uri(curRepoRootedUri);

                string solutionFileName = curRepoUri.GetLeaf();
                string solutionName = solutionFileName.Substring(0, solutionFileName.Length - 4);

                string localFolderName = Path.Combine(localProdDirectoryRootedPath, solutionName);
                if (!Directory.Exists(localFolderName))
                {
                    Directory.CreateDirectory(localFolderName);
                }

                string repoSolutionFolder = curRepoUri.GetParentUriString();
                string localSolutionFileRootedPath = localFolderName + curRelativeUri;
                FileInfo fInfo = new FileInfo(localSolutionFileRootedPath);
                string localSolutionDirectoryRootedPath = fInfo.DirectoryName;

                using (SvnClient client = new SvnClient())
                {
                    client.CheckOut(new SvnUriTarget(repoSolutionFolder), localSolutionDirectoryRootedPath);
                }

                // Create a short-cut to the solution file and place it in the local folder for the solution.
                string shortCutFileName = String.Format("{0} - Shortcut.lnk", solutionFileName);
                string shortCutFileRootedPath = Path.Combine(localFolderName, shortCutFileName);
                if (!System.IO.File.Exists(shortCutFileRootedPath))
                {
                    IWshShortcut shortCut = (IWshShortcut)wshShell.CreateShortcut(shortCutFileRootedPath);
                    shortCut.TargetPath = localSolutionFileRootedPath;
                    shortCut.Save();
                }

                // Now get the list of relative project paths by parsing the solution file.
                List<string> solutionFileLines = TextFileSerializer.DeserializeFromRootedPath(localSolutionFileRootedPath);

                List<string> projectLines = new List<string>();
                string projectSignifier = @"Project";
                foreach (string line in solutionFileLines)
                {
                    if (line.Length >= projectSignifier.Length && projectSignifier == line.Substring(0, projectSignifier.Length))
                    {
                        projectLines.Add(line);
                    }
                }

                List<string> relativeFilePaths = new List<string>();
                string[] separators = new string[] { "=", "," };
                foreach (string line in projectLines)
                {
                    string[] tokens = line.Split(separators, StringSplitOptions.None);
                    string relativePath = tokens[2].Trim().Trim('"');

                    if (@".." == relativePath.Substring(0, 2)) // Otherwise it's a subdirectory of the main solution directory, which has already been checked out.
                    {
                        relativeFilePaths.Add(relativePath);
                    }
                }

                // Now check out the folders containing each of the relative projects to their appropriate relative local paths.
                foreach (string relativeFilePath in relativeFilePaths)
                {
                    string relativeOtherProjectPath = Path.Combine(localSolutionDirectoryRootedPath, relativeFilePath);
                    string localOtherProjectRootedFilePath = Path.GetFullPath(relativeOtherProjectPath);
                    FileInfo localOtherProjectFileInfo = new FileInfo(localOtherProjectRootedFilePath);
                    string localOtherProjectDirectoryRootedPath = localOtherProjectFileInfo.DirectoryName;

                    if (!Directory.Exists(localOtherProjectDirectoryRootedPath))
                    {
                        Directory.CreateDirectory(localOtherProjectDirectoryRootedPath);
                    }

                    Uri repoOtherProjectUri = new Uri(repoSolutionFolder + relativeFilePath);
                    string repoOtherProjectFolderUri = repoOtherProjectUri.GetParentUriString();

                    using (SvnClient client = new SvnClient())
                    {
                        client.CheckOut(new SvnUriTarget(repoOtherProjectFolderUri), localOtherProjectDirectoryRootedPath);
                    }
                }
            }
        }
    }
}
