using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml.Schema;


namespace XsdImportHandlingWrapper
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

            string initialXsdFileRootedPath = @"C:\Code\DEV\Duomo\Source\Common\Experiments\Silvas\Silvas.Lib\Xml\Schemas\ScheduledJobSpecificationTypes.xsd";

            HashSet<string> importedXsdFileRootedPathsHashSet = new HashSet<string>();
            Program.AddImportFilePathsForFilePath(initialXsdFileRootedPath, importedXsdFileRootedPathsHashSet);

            // Add in the initial path.
            importedXsdFileRootedPathsHashSet.Add(initialXsdFileRootedPath);

            List<string> allXsdFileRootedPaths = new List<string>(importedXsdFileRootedPathsHashSet);

            string outputFolderRootedPath = @"C:\temp";
            string inputArguments = Program.CreateXsdToolCommand(allXsdFileRootedPaths, outputFolderRootedPath);
            
            string xsdToolExecutableRootedPath = @"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\Bin\NETFX 4.0 Tools\xsd.exe";
            Program.RunXsdTool(xsdToolExecutableRootedPath, inputArguments, outputFolderRootedPath);
        }

        private static void RunXsdTool(string xsdToolRootedPath, string inputArguments, string outputFolderRootedPath)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(xsdToolRootedPath, inputArguments);
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;

            Process xsdToolProcess = new Process();
            xsdToolProcess.StartInfo = startInfo;

            xsdToolProcess.Start();

            string logFileRootedPath = Program.GetLogFileRootedPath(outputFolderRootedPath);
            using (StreamWriter writer = new StreamWriter(logFileRootedPath))
            {
                while (!xsdToolProcess.StandardOutput.EndOfStream)
                {
                    string line = xsdToolProcess.StandardOutput.ReadLine();
                    writer.WriteLine(line);
                }
            }
        }

        private static string CreateXsdToolCommand(List<string> allXsdFileRootedPaths, string outputFolderRootedPath)
        {
            StringBuilder builder = new StringBuilder();

            foreach (string xsdFileRootedPath in allXsdFileRootedPaths)
            {
                builder.Append(String.Format("\"{0}\" ", xsdFileRootedPath));
            }

            builder.Append(String.Format("/c /out:{0}", outputFolderRootedPath));

            string retValue = builder.ToString();
            return retValue;
        }

        private static string GetLogFileRootedPath(string outputFolderRootedPath)
        {
            string retValue = Path.Combine(outputFolderRootedPath, "xsd.output.txt");
            return retValue;
        }

        private static void AddImportFilePathsForFilePath(string xsdFileRootedPath, HashSet<string> importedXsdFileRootedPathsHashSet)
        {
            FileInfo initialXsdFileInfo = new FileInfo(xsdFileRootedPath);
            string initialDirectory = initialXsdFileInfo.DirectoryName;

            XmlSchema schema;
            using (StreamReader reader = new StreamReader(xsdFileRootedPath))
            {
                schema = XmlSchema.Read(reader, Program.ValidationEventHandler);
            }

            foreach (XmlSchemaImport import in schema.Includes)
            {
                string importedXsdFilePath = import.SchemaLocation;
                string importedXsdFileRootedPath = importedXsdFilePath;
                if (!Path.IsPathRooted(importedXsdFileRootedPath))
                {
                    string importedXsdFileUnresolvedRootedPath = Path.Combine(initialDirectory, importedXsdFilePath);
                    importedXsdFileRootedPath = Path.GetFullPath(importedXsdFileUnresolvedRootedPath);
                }

                // First add any sub-imports.
                Program.AddImportFilePathsForFilePath(importedXsdFileRootedPath, importedXsdFileRootedPathsHashSet);

                // Then add the path, in not already present.
                if (!importedXsdFileRootedPathsHashSet.Contains(importedXsdFileRootedPath))
                {
                    importedXsdFileRootedPathsHashSet.Add(importedXsdFileRootedPath);
                }
            }
        }

        private static void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            // Who cares? Do nothing.
        }
    }
}
