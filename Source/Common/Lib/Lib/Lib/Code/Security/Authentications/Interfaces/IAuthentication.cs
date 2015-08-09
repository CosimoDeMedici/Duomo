using System;


namespace Duomo.Common.Lib
{
    public interface IAuthentication
    {
        string UserName { get; set; }
        string Password { get; set; }
    }
}
