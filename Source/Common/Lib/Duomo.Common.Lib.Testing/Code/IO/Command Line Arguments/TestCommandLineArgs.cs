using System;
using System.Collections.Generic;

using Duomo.Common.Lib.IO;


namespace Duomo.Common.Lib.Testing.IO
{
    public class TestCommandLineArgs : CommandLineArgumentsStructureBase
    {
        #region Static

        public static string[] EXAMPLE_ARGS
        {
            get
            {
                string[] retValue = new string[]
                {
                    "localhost",
                    "Send this!"
                };

                return retValue;
            }
        }

        #endregion

        public string Server { get; set; }
        public string Word { get; set; }
        public int Port { get; set; }


        public TestCommandLineArgs(string programName)
            : base(programName)
        {
        }

        public TestCommandLineArgs(string programName, string[] arguments)
            : base(programName, arguments)
        {
        }

        protected override IList<ICommandLineArgument> GetArgumentDefinitions()
        {
            List<ICommandLineArgument> retValue = new List<ICommandLineArgument>();

            retValue.Add(new DataArgument("Server", "Name or IP Address of server.", typeof(string), false, 1, x => this.Server = x.Value));
            retValue.Add(new DataArgument("Word", "Word to send.", typeof(string), false, 2, x => this.Word = x.Value));
            retValue.Add(new DataArgument("Port", "Server port.", typeof(int), true, 3, "7", x => this.Port = Convert.ToInt32(x.Value)));

            return retValue;
        }
    }
}
