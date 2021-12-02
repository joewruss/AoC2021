using System.Threading.Tasks;

namespace AoC2021
{
    public interface ILogic
    {
        Task<string> RunPart1(string fileName);

        Task<string> RunPart2(string fileName);
    }
}