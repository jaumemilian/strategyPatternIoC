using System.Diagnostics;
using StrategyPatternIoC.Interfaces;

namespace StrategyPatternIoC.Services
{
    public class UserService2 : IUserService
    {
        public void TryMethod()
        {
            Debug.WriteLine("UserService2");
        }
    }
}