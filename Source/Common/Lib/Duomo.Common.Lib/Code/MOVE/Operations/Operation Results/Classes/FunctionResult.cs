using System;


namespace Duomo.Common.Lib
{
    [Serializable]
    public class FunctionResult<TReturnType, TOperationResult>
        where TOperationResult : IOperationResult
    {
        public TReturnType ReturnValue { get; set; }
        public TOperationResult OperationResult { get; set; }


        public FunctionResult()
        {
        }

        public FunctionResult(TReturnType returnValue, TOperationResult operationResult)
        {
            this.ReturnValue = returnValue;
            this.OperationResult = operationResult;
        }
    }
}
