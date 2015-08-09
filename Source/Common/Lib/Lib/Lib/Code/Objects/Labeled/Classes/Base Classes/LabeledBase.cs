using System;


namespace Duomo.Common.Lib
{
    public class LabeledBase : ILabeled
    {
        #region ILabeled Members

        public string Name { get; set; }
        public string Description { get; set; }

        #endregion
    }
}
