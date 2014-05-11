using System;

using Duomo.Common.Lib.MOVE;


namespace Duomo.Common.Lib.Testing.BasicPipeline
{
    public class TestModelA : ModelBase
    {
        public IModelHolder Parent { get; set; }
        public string ValueString { get; set; }
        public double ValueDouble { get; set; }
        public TestModelB TestModelB { get; set; }
    }
}
