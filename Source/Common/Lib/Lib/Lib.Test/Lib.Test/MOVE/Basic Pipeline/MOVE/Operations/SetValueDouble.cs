using System;

using Duomo.Common.Lib.MOVE;


namespace Duomo.Common.Lib.Testing.BasicPipeline
{
    public class SetValueDouble : OperationBase<TestModelA>
    {
        public override void Execute(IModelHolder<TestModelA> modelHolder)
        {
            modelHolder.Instance.ValueDouble = 1;
        }
    }
}
