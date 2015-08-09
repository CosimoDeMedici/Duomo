using System;

using Duomo.Common.Lib.MOVE;


namespace Duomo.Common.Lib.Testing.BasicPipeline
{
    public class TestModelB : ModelBase
    {
        public IModelHolder<TestModelA> Parent { get; set; }
        public string ValueStringB { get; set; }
    }
}
