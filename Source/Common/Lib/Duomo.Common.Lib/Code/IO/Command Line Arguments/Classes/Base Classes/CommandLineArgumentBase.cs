using System;

using Duomo.Common.Lib;


namespace Duomo.Common.Lib.IO
{
    public abstract class CommandLineArgumentBase : LabeledString, ICommandLineArgument
    {
        #region ICommandLineArgument Members

        public Type DataType { get; protected set; }
        public Action<ICommandLineArgument> SetPropertyFromString { get; protected set; }

        #endregion

        public string DefaultValue { get; protected set; }


        public CommandLineArgumentBase(
            string name,
            string description,
            Type dataType,
            Action<ICommandLineArgument> setPropertyFromString)
            : base()
        {
            Setup(name, description, dataType, String.Empty, setPropertyFromString);
        }

        public CommandLineArgumentBase(
            string name,
            string description,
            Type dataType,
            string defaultValue,
            Action<ICommandLineArgument> setPropertyFromString)
            : base()
        {
            Setup(name, description, dataType, defaultValue, setPropertyFromString);
        }

        private void Setup(
            string name,
            string description,
            Type dataType,
            string defaultValue,
            Action<ICommandLineArgument> setPropertyFromString)
        {
            this.Name = name;
            this.Description = description;
            this.DataType = dataType;
            this.DefaultValue = defaultValue;
            this.Value = this.DefaultValue;
            this.SetPropertyFromString = setPropertyFromString;
        }
    }
}
