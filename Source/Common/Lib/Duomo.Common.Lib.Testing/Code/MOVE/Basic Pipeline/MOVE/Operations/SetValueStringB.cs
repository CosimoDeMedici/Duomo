using System;

using Duomo.Common.Lib.MOVE;


namespace Duomo.Common.Lib.Testing.BasicPipeline
{
    public class SetValueStringB : OperationBase<TestModelB>
    {
        public override void Execute(IModelHolder<TestModelB> modelHolder)
        {
            modelHolder.Instance.ValueStringB = "BB";
        }
    }
}
