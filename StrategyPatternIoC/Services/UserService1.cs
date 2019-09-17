using System.Diagnostics;
using StrategyPatternIoC.Interfaces;

namespace StrategyPatternIoC.Services
{
    public class UserService1 : IUserService
    {
        public void TryMethod()
        {
            Debug.WriteLine("UserService1");
        }
    }
}