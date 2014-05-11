using System;


namespace Duomo.Common.Lib
{
    public interface ILabeled
    {
        string Name { get; set; }
        string Description { get; set; }
    }
}
