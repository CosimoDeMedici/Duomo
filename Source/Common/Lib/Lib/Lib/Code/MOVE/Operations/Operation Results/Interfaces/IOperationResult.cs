using System;
using System.Collections.Generic;


namespace Duomo.Common.Lib
{
    /// <summary>
    /// Helpasd
    /// </summary>
    public interface IOperationResult
    {
        bool Success { get; set; }
        string Message { get; set; }


        List<string> Write();
    }
}
