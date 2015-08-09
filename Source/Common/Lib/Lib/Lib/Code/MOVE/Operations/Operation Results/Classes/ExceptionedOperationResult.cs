using System;
using System.Collections.Generic;


namespace Duomo.Common.Lib
{
    [Serializable]
    public class ExceptionedOperationResult : OperationResult
    {
        public Exception Exception { get; set; }


        public ExceptionedOperationResult()
            : base()
        {
        }

        public ExceptionedOperationResult(Exception exception)
            : base(exception.Message)
        {
            this.Exception = exception;
        }

        public ExceptionedOperationResult(bool success, string message, Exception exception)
            : base(success, message)
        {
            this.Exception = exception;
        }

        public override List<string> Write()
        {
            List<string> retValue = base.Write();

            if (null != this.Exception)
            {
                string exceptionMessage = this.Exception.Message;

                string[] lineSeparators = new string[] { Environment.NewLine };
                string[] lines = exceptionMessage.Split(lineSeparators, StringSplitOptions.None);

                retValue.AddRange(lines);
            }

            return retValue;
        }
    }
}
