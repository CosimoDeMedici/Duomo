using System;


namespace Duomo.Common.Lib.MOVE
{
    public class BasicOperationRunner : IOperationNodeRunner
    {
        #region IOperationNodeRunner Members

        public IRun Run(IOperationNode operationNode, IModelHolder modelHolder)
        {
            IOperation operation = operationNode as IOperation;
            if (null == operation)
            {
                throw new DuomoWrongTypeException(typeof(IOperation), operationNode);
            }

            IRun retValue = new Run(operationNode, modelHolder, RunStatusEnumeration.Waiting);

            try
            {
                retValue.RunStatus = RunStatusEnumeration.Running;

                operation.Execute(modelHolder);

                retValue.RunStatus = RunStatusEnumeration.Finished;
            }
            catch (Exception ex)
            {
                retValue.RunStatus = RunStatusEnumeration.Errored;

                throw ex;
            }

            return retValue;
        }

        #endregion
    }
}
