using System;
using System.Collections.Generic;


namespace Duomo.Common.Lib.MOVE
{
    public class Run : IRun
    {
        #region IRun Members

        public IOperationNode OperaionNode { get; set; }
        public IModelHolder ModelHolder { get; set; }
        public RunStatusEnumeration RunStatus { get; set; }
        public IList<IRun> SubRuns { get; protected set; }

        #endregion


        public Run()
        {
            Setup();
        }

        public Run(IOperationNode operationNode, IModelHolder modelHolder, RunStatusEnumeration runStatus)
        {
            OperaionNode = operationNode;
            ModelHolder = modelHolder;
            RunStatus = runStatus;

            Setup();
        }

        private void Setup()
        {
            SubRuns = new List<IRun>();
        }
    }
}
