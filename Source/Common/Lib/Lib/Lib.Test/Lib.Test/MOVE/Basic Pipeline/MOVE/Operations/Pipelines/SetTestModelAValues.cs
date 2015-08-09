using System;

using Duomo.Common.Lib.MOVE;


namespace Duomo.Common.Lib.Testing.BasicPipeline
{
    public class SetTestModelAValues : OperationPipelineBase<TestModelA, IModel>
    {
        protected override void RegisterOperations()
        {
            this
                .Register(new SetValueString())
                .Register(new SetValueDouble())
                .Register(new SetTestModelBValues())
                ;
        }

        public override TestModelA GetTypedModelFromParentModelHolder(IModelHolder<IModel> parentModelHolder)
        {
            TestModelA retValue = new TestModelA();
            retValue.Parent = parentModelHolder;

            return retValue;
        }

        public override void SetTypedModelHolderInParentModelHolder(IModelHolder<TestModelA> modelHolder, IModelHolder<IModel> parentModelHolder)
        {
            parentModelHolder.Instance = modelHolder.Instance;
        }
    }
}
