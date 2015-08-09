using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Duomo.Common.Lib
{
    [Serializable]
    public class NestedOperationResult : OperationResult
    {
        #region Static

        public static int SubOperationCountBySuccessStatus(NestedOperationResult nestedOperationResult, bool successStatus)
        {
            int retValue =
                (from element in nestedOperationResult.SubOperationResults
                 where element.Success == successStatus
                 select element).Count();

            return retValue;
        }

        #endregion


        public List<IOperationResult> SubOperationResults { get; protected set; }


        public NestedOperationResult()
            : base()
        {
            this.Setup();
        }

        public NestedOperationResult(string messageIfFailure, IOperationResult subOperationResult)
            : base(messageIfFailure)
        {
            this.Setup();

            this.SubOperationResults.Add(subOperationResult);
        }

        public NestedOperationResult(string messageIfFailure)
            : base(messageIfFailure)
        {
        }

        private void Setup()
        {
            this.SubOperationResults = new List<IOperationResult>();
        }

        public int SubOperationCountBySuccessStatusInstance(bool successStatus)
        {
            int retValue = NestedOperationResult.SubOperationCountBySuccessStatus(this, successStatus);
            return retValue;
        }

        public void SetAsSuccessIfSubOperationsSuccessful()
        {
            int numSuccessfulSubOps = NestedOperationResult.SubOperationCountBySuccessStatus(this, true);
            if (this.SubOperationResults.Count == numSuccessfulSubOps)
            {
                this.SetSuccess();
            }
        }

        public override List<string> Write()
        {
            List<string> retValue = this.WriteRecursive(0);
            return retValue;
        }

        protected List<string> WriteRecursive(int indentation)
        {
            List<string> lines = base.Write();

            foreach (IOperationResult result in this.SubOperationResults)
            {
                List<string> subLines;
                if (result is NestedOperationResult)
                {
                    subLines = ((NestedOperationResult)result).WriteRecursive(indentation + 1);
                }
                else
                {
                    subLines = result.Write();
                }

                lines.AddRange(subLines);
            }

            StringBuilder builder = new StringBuilder();
            for (int iIndent = 0; iIndent < indentation; iIndent++)
            {
                builder.Append("\t");
            }
            string indentStr = builder.ToString();

            List<string> retValue = new List<string>();
            foreach (string line in lines)
            {
                retValue.Add(indentStr + line);
            }

            return retValue;
        }
    }
}
