using System; 

namespace TianYu.Core.UnitTestProject1.IOCTest
{
    public interface ITestService
    {
        void Show();
    }

    public class TestService : ITestService
    {
        public void Show()
        {
            Console.WriteLine("TestService.Show()");
        }
    }
}
