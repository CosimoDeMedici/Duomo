using System;
using System.Collections.Generic;


namespace Duomo.Common.Lib
{
    public class OperationResult : IOperationResult
    {
        #region Static

        public static readonly OperationResult Successful;


        static OperationResult()
        {
            OperationResult.Successful = new OperationResult(true);
        }

        #endregion

        #region IOperationResult Members

        public bool Success { get; set; }
        public string Message { get; set; }


        public virtual List<string> Write()
        {
            string successString;
            if (this.Success)
            {
                successString = "SUCCESS";
            }
            else
            {
                successString = "FAILURE";
            }

            string line;
            if (String.Empty == this.Message)
            {
                line = successString;
            }
            else
            {
                line = String.Format("{0} - {1}", successString, this.Message);
            }

            List<string> retValue = new List<string>();
            retValue.Add(line);

            return retValue;
        }

        #endregion


        public OperationResult()
        {
            this.Success = true;
            this.Message = String.Empty;
        }

        public OperationResult(string messageIfFailure)
        {
            this.AssumeFailure(messageIfFailure);
        }

        public OperationResult(bool success)
        {
            this.Success = success;
            this.Message = String.Empty;
        }

        public OperationResult(bool success, string message)
        {
            this.Success = success;
            this.Message = message;
        }

        public void AssumeFailure(string messageIfFailure)
        {
            this.Success = false;
            this.Message = messageIfFailure;
        }

        public void SetSuccess()
        {
            this.Success = true;
            this.Message = String.Empty;
        }
    }
}
