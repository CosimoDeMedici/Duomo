using System;
using System.Collections.Generic;
using System.Linq;


namespace Duomo.Common.Lib.MOVE
{
    public class BasicOperationPipelineRunner : IOperationNodeRunner
    {
        #region IOperationNodeRunner Members

        public IRun Run(IOperationNode operationNode, IModelHolder modelHolder)
        {
            IOperationPipeline pipeline = operationNode as IOperationPipeline;
            if (null == pipeline)
            {
                throw new WrongTypeException(typeof(IOperationPipeline), operationNode);
            }

            IRun retValue = new Run(operationNode, modelHolder, RunStatusEnumeration.Waiting);

            IModelHolder pipelineModelHolder = pipeline.GetModelHolderFromParentModelHolder(modelHolder);

            BasicOperationRunner basicOperationRunner = new BasicOperationRunner();

            retValue.RunStatus = RunStatusEnumeration.Starting;

            for (int iOperationNode = 0; iOperationNode < pipeline.Count; iOperationNode++)
            {
                IOperationNode curNode = pipeline[iOperationNode];

                IRun curRun;
                if (curNode is IOperation)
                {
                    curRun = basicOperationRunner.Run(curNode, pipelineModelHolder);
                }
                else
                {
                    // Assume if the node is not an operation, it's an operation pipeline. Recurse.
                    curRun = this.Run(curNode, pipelineModelHolder);
                }

                retValue.SubRuns.Add(curRun);

                // Allow stopping on errors.
                if (curRun.RunStatus == RunStatusEnumeration.Errored)
                {
                    if (curNode.OnErrorBlock)
                    {
                        break;
                    }
                }
            }

            pipeline.SetModelHolderInParentModelHolder(pipelineModelHolder, modelHolder);

            if (HasErroredSubNodeOperations(retValue))
            {
                retValue.RunStatus = RunStatusEnumeration.Errored;
            }
            else
            {
                retValue.RunStatus = RunStatusEnumeration.Finished;
            }

            return retValue;
        }

        #endregion


        private bool HasErroredSubNodeOperations(IRun pipelineRun)
        {
            int count = pipelineRun.SubRuns.Where(x => x.RunStatus == RunStatusEnumeration.Errored).Count();

            return 0 != count;
        }
    }
}
