using System;

using Duomo.Common.Lib.MOVE;


namespace Duomo.Common.Lib.Testing.BasicPipeline
{
    public class SetTestModelBValues : OperationPipelineBase<TestModelB, TestModelA>
    {
        protected override void RegisterOperations()
        {
            this
                .Register(new SetValueStringB());
        }

        public override TestModelB GetTypedModelFromParentModelHolder(IModelHolder<TestModelA> parentModelHolder)
        {
            TestModelB retValue = new TestModelB();
            retValue.Parent = parentModelHolder;

            return retValue;
        }

        public override void SetTypedModelHolderInParentModelHolder(IModelHolder<TestModelB> modelHolder, IModelHolder<TestModelA> parentModelHolder)
        {
            parentModelHolder.Instance.TestModelB = modelHolder.Instance;
        }
    }
}
