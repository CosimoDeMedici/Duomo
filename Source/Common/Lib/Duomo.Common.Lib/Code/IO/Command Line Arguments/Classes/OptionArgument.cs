using System;


namespace Duomo.Common.Lib.IO
{
    public class OptionArgument : CommandLineArgumentBase, IOptionArgument
    {
        #region IOptionArgument Members

        public string Flag { get; protected set; }

        #endregion


        public OptionArgument(
            string name,
            string description,
            Type dataType,
            string flag,
            Action<ICommandLineArgument> setPropertyFromString)
            : base(name, description, dataType, setPropertyFromString)
        {
            Setup(flag);
        }

        public OptionArgument(
            string name,
            string description,
            Type dataType,
            string flag,
            string defaultValue,
            Action<ICommandLineArgument> setPropertyFromString)
            : base(name, description, dataType, defaultValue, setPropertyFromString)
        {
            Setup(flag);
        }

        private void Setup(string flag)
        {
            this.Flag = flag;
        }
    }
}
