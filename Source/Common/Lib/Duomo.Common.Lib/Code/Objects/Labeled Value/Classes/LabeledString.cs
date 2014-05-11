using System;


namespace Duomo.Common.Lib
{
    public class LabeledString : LabeledValueBase<string>
    {
        public LabeledString()
            : base()
        {
        }

        public LabeledString(string value)
            : base(value)
        {
        }
    }
}
