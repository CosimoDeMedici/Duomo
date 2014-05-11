using System;

using Duomo.Common.Lib.MOVE;


namespace Duomo.Common.Lib.Testing.BasicPipeline
{
    public static class BasicPipelineExamples
    {
        public static void SubMain()
        {
            //RunSingleOperationManually();
            //RunSingleOperationUsingBasicOperationRunner();
            RunPipelineOfOperationsUsingBasicOperationPipelineRunner();
        }

        private static void RunSingleOperationManually()
        {
            // Run an operation manually.
            TestModelA testModelA = new TestModelA();
            ModelHolder<TestModelA> modelHolder = new ModelHolder<TestModelA>(testModelA);

            SetValueString setValueStringOperation = new SetValueString();

            setValueStringOperation.Execute(modelHolder);
        }

        private static void RunSingleOperationUsingBasicOperationRunner()
        {
            // Run an operation using the BasicOperationRunner.
            TestModelA testModelA = new TestModelA();
            ModelHolder<TestModelA> modelHolder = new ModelHolder<TestModelA>(testModelA);

            SetValueString setValueStringOperation = new SetValueString();

            BasicOperationRunner basicRunner = new BasicOperationRunner();

            IRun runResult = basicRunner.Run(setValueStringOperation, modelHolder);
        }

        private static void RunPipelineOfOperationsUsingBasicOperationPipelineRunner()
        {
            // Run a pipeline of operations by using the pipeline operation runner.
            ModelHolder<IModel> modelHolder = new ModelHolder<IModel>();
            modelHolder.Instance = null; // This should be the Environment model.

            SetTestModelAValues pipeline = new SetTestModelAValues();

            BasicOperationPipelineRunner pipelineRunner = new BasicOperationPipelineRunner();
            IRun runResult = pipelineRunner.Run(pipeline, modelHolder);
        }
    }
}
