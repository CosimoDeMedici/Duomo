using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;


namespace Duomo.Common.Lib.IO
{
    public abstract class CommandLineArgumentsStructureBase : ICommandLineArgumentsStructure
    {
        #region ICommandLineArgumentsStructure Members

        public string ProgramName { get; protected set; }
        public string Usage
        {
            get
            {
                return GetUsage();
            }
        }
        public IList<ICommandLineArgument> Arguments
        {
            get
            {
                return GetArgumentDefinitions();
            }
        }

        
        public void Parse(IList<string> arguments)
        {
            IList<ICommandLineArgument> argumentDefinitions = GetArgumentDefinitions();
            IList<IDataArgument> dataArguments = GetDataArgumentDefinitionsInOrder(argumentDefinitions);
            IList<IOptionArgument> optionArguments = GetOptionArgumentDefinitionsInOrder(argumentDefinitions);

            // First read in the values from the command line input.
            int numDataArgumentsRequired = GetNumberOfRequiredDataArguments(dataArguments);
            int numDataArgumentsFound = ParseDataArguments(arguments, dataArguments);
            if (numDataArgumentsRequired != numDataArgumentsFound)
            {
                // TODO, change to a ArgumentExceptionDuomo, after ArgumentExceptionDuomo is created.
                throw new ArgumentException(FormatUsageForError(String.Format("Required number of command line arguments not found. Required: {0}, found {1}.", numDataArgumentsRequired, numDataArgumentsFound)));
            }

            ParseOptionArguments(arguments, optionArguments);

            // Now loop through all input arugments, setting their values.
            foreach (ICommandLineArgument argumentDefinition in argumentDefinitions)
            {
                try
                {
                    argumentDefinition.SetPropertyFromString(argumentDefinition); // Ok to not reference object since this makes it easier when creating the anonymous function for setting the value.
                }
                catch (Exception ex)
                {
                    throw new ArgumentException(FormatUsageForError(String.Format("Unable to convert input '{0}' to value for argument.", argumentDefinition.Value)), ex); 
                }
            }
        }

        public void Parse(string[] arguments)
        {
            Parse(new List<string>(arguments));
        }

        #endregion


        public const char OPTION_FLAG_INDICATOR = '-';
        private static readonly string zOptionFlagCharString = OPTION_FLAG_INDICATOR.ToString();


        public CommandLineArgumentsStructureBase(string programName)
        {
            Setup(programName);
        }

        public CommandLineArgumentsStructureBase(string programName, string[] arguments)
        {
            Setup(programName);

            Parse(arguments);
        }

        private void Setup(string programName)
        {
            ProgramName = programName;
        }

        protected abstract IList<ICommandLineArgument> GetArgumentDefinitions();

        private string GetUsage()
        {
            IList<ICommandLineArgument> argumentDefinitions = GetArgumentDefinitions();

            StringBuilder builder = new StringBuilder();
            builder.AppendLine(String.Format("Usage:\n\n{0}", ProgramName));

            IList<IDataArgument> dataArguments = GetDataArgumentDefinitionsInOrder(argumentDefinitions);
            foreach (IDataArgument dataArgument in dataArguments)
            {
                string argumentDescriptor = String.Format(
                    "<{0}({1}), {2}>",
                    dataArgument.Name,
                    dataArgument.DataType.Name,
                    dataArgument.Description);

                if (dataArgument.Optional)
                {
                    argumentDescriptor = String.Format("[{0}]", argumentDescriptor);
                }

                builder.AppendLine(String.Format("\t{0}", argumentDescriptor));
            }

            builder.AppendLine("Options:");
            IList<IOptionArgument> optionArguments = GetOptionArgumentDefinitionsInOrder(argumentDefinitions);
            if (0 == optionArguments.Count)
            {
                builder.AppendLine("\tNONE");
            }

            foreach (IOptionArgument optionArgument in optionArguments)
            {
                builder.AppendLine(String.Format(
                    "\t{0}{1} <{2}({3}), {4}>",
                    OPTION_FLAG_INDICATOR,
                    optionArgument.Flag,
                    optionArgument.Name,
                    optionArgument.DataType.Name,
                    optionArgument.Description));
            }

            return builder.ToString();
        }

        private string FormatUsageForError(string errorMessage)
        {
            return String.Format("{0}\n\n{1}", errorMessage, Usage);
        }

        private int ParseDataArguments(IList<string> arguments, IList<IDataArgument> dataArguments)
        {
            int numDataArgumentsFound = 0;
            int iDataArgument = 0;
            foreach (string argument in arguments)
            {
                if (zOptionFlagCharString == argument.Substring(0, 0))
                {
                    // Option arguments have been reached.
                    break;
                }

                // Data argument.
                IDataArgument curDataArgument = dataArguments[iDataArgument]; // Note, data arguments are in ordinal order.
                curDataArgument.Value = argument;

                iDataArgument++;
                numDataArgumentsFound++;
            }

            return numDataArgumentsFound;
        }

        private void ParseOptionArguments(IList<string> arguments, IList<IOptionArgument> optionArguments)
        {
            int indexOfFirstOptionFlag = GetIndexOfFirstOptionFlag(arguments);
            if (-1 == indexOfFirstOptionFlag)
            {
                return; // No option arguments.
            }

            List<string> optionArgumentPairs = GetOptionArgumentPairs(arguments, indexOfFirstOptionFlag);

            // Expecting an even number of strings since option arguments consist of option flag-option value pairs.
            if (0 != optionArgumentPairs.Count % 2)
            {
                throw new ArgumentException(FormatUsageForError(String.Format("Upaired option flag-option value detected. Check input command.")));
            }

            IOptionArgument curOptionArgument = null;
            bool skip = false;
            foreach (string optionArgumentFlagOrValue in optionArgumentPairs)
            {
                if (skip)
                {
                    // The 'skip' flag is used to skip the value of an option argument, and instead, set it's value.
                    skip = false;

                    Debug.Assert(null != curOptionArgument, "The curOptionArgument should have been set on the prior iteration. Check code.");

                    curOptionArgument.Value = optionArgumentFlagOrValue;

                    continue;
                }

                // Option argument.
                if (zOptionFlagCharString != optionArgumentFlagOrValue.Substring(0, 1))
                {
                    throw new ArgumentException(FormatUsageForError(String.Format("Misplaced option value detected. Should have been the option flag of an option flag-option value pair. Instead found: {0}.", optionArgumentFlagOrValue)));
                }

                string optionFlag = optionArgumentFlagOrValue.Substring(1);
                curOptionArgument = GetOptionArgumentByFlag(optionFlag, optionArguments);
                if (null == curOptionArgument)
                {
                    throw new ArgumentException(FormatUsageForError(String.Format("Unrecognized option detected: {0}. Check usage.", optionFlag)));
                }

                skip = true;
            }

            // Then final option flag was missing it's value. This should not happen.
            Debug.Assert(!skip, "Option flag was missing it's value. This should not have happened. Check code.");
        }

        private IList<IDataArgument> GetDataArgumentDefinitionsInOrder(IList<ICommandLineArgument> argumentDefinitions)
        {
            IList<IDataArgument> retValue = argumentDefinitions
                .OfType<IDataArgument>()
                .OrderBy(x => x.Ordinal)
                .ToList<IDataArgument>();

            int numDataArguments = retValue.Count;
            int numDistinctOrdinals = retValue.Select(x => x.Ordinal).Distinct<int>().Count();
            Debug.Assert(
                numDistinctOrdinals == numDataArguments,
                "Duplicate data argument ordinals found. Check code and ensure argument definitions are correct.");

            return retValue;
        }

        private IList<IOptionArgument> GetOptionArgumentDefinitionsInOrder(IList<ICommandLineArgument> argumentDefinitions)
        {
            IList<IOptionArgument> retValue = argumentDefinitions
                .OfType<IOptionArgument>()
                .OrderBy(x => x.Flag)
                .ToList<IOptionArgument>();

            return retValue;
        }

        private int GetNumberOfRequiredDataArguments(IList<IDataArgument> dataArguments)
        {
            int retValue = dataArguments.Where(x => x.Optional == false).Count();

            return retValue;
        }

        private int GetIndexOfFirstOptionFlag(IList<string> arguments)
        {
            List<string> temp = new List<string>(arguments);
            int retValue = temp.FindIndex(x => x.Substring(0, 1) == zOptionFlagCharString);

            return retValue;
        }

        private List<string> GetOptionArgumentPairs(IList<string> arguments, int indexOfFirstOptionFlag)
        {
            List<string> retValue = arguments.Skip(indexOfFirstOptionFlag).Take(arguments.Count - indexOfFirstOptionFlag).ToList<string>();

            return retValue;
        }

        private IOptionArgument GetOptionArgumentByFlag(string flag, IList<IOptionArgument> optionArguments)
        {
            IOptionArgument retValue = optionArguments.Where(x => x.Flag == flag).FirstOrDefault();

            return retValue;
        }
    }
}
