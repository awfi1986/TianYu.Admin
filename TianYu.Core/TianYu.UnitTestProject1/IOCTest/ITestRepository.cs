using System; 

namespace TianYu.Core.UnitTestProject1.IOCTest
{
    public interface ITestRepository
    {
        void Show();
    }
    public class TestRepository : ITestRepository
    {
        public void Show()
        {
            Console.WriteLine("TestRepository.Show()");
        }
    }
}
