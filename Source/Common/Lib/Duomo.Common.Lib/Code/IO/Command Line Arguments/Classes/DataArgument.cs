using System;


namespace Duomo.Common.Lib.IO
{
    public class DataArgument : CommandLineArgumentBase, IDataArgument
    {
        #region IDataArgument Members

        public bool Optional { get; protected set; }
        public int Ordinal { get; protected set; }

        #endregion


        public DataArgument(
            string name,
            string description,
            Type dataType,
            bool optional,
            int ordinal,
            Action<ICommandLineArgument> setPropertyFromString)
            : base(name, description, dataType, setPropertyFromString)
        {
            Setup(optional, ordinal);
        }

        public DataArgument(
            string name,
            string description,
            Type dataType,
            bool optional,
            int ordinal,
            string defaultValue,
            Action<ICommandLineArgument> setPropertyFromString)
            : base(name, description, dataType, defaultValue, setPropertyFromString)
        {
            Setup(optional, ordinal);
        }

        private void Setup(bool optional, int ordinal)
        {
            this.Optional = optional;
            this.Ordinal = ordinal;
        }
    }
}
