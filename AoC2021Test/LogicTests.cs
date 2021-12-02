using AoC2021;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace AoC2021Test
{
    public class LogicTests
    {
        [Theory]
        [InlineData(1, 1, "7")]
        [InlineData(1, 2, "5")]
        [InlineData(2, 1, "not implemented")]
        [InlineData(2, 2, "not implemented")]
        [InlineData(3, 1, "not implemented")]
        [InlineData(3, 2, "not implemented")]
        [InlineData(4, 1, "not implemented")]
        [InlineData(4, 2, "not implemented")]
        [InlineData(5, 1, "not implemented")]
        [InlineData(5, 2, "not implemented")]
        [InlineData(6, 1, "not implemented")]
        [InlineData(6, 2, "not implemented")]
        [InlineData(7, 1, "not implemented")]
        [InlineData(7, 2, "not implemented")]
        [InlineData(8, 1, "not implemented")]
        [InlineData(8, 2, "not implemented")]
        [InlineData(9, 1, "not implemented")]
        [InlineData(9, 2, "not implemented")]
        [InlineData(10, 1, "not implemented")]
        [InlineData(10, 2, "not implemented")]
        [InlineData(11, 1, "not implemented")]
        [InlineData(11, 2, "not implemented")]
        [InlineData(12, 1, "not implemented")]
        [InlineData(12, 2, "not implemented")]
        [InlineData(13, 1, "not implemented")]
        [InlineData(13, 2, "not implemented")]
        [InlineData(14, 1, "not implemented")]
        [InlineData(14, 2, "not implemented")]
        [InlineData(15, 1, "not implemented")]
        [InlineData(15, 2, "not implemented")]
        [InlineData(16, 1, "not implemented")]
        [InlineData(16, 2, "not implemented")]
        [InlineData(17, 1, "not implemented")]
        [InlineData(17, 2, "not implemented")]
        [InlineData(18, 1, "not implemented")]
        [InlineData(18, 2, "not implemented")]
        [InlineData(19, 1, "not implemented")]
        [InlineData(19, 2, "not implemented")]
        [InlineData(20, 1, "not implemented")]
        [InlineData(20, 2, "not implemented")]
        [InlineData(21, 1, "not implemented")]
        [InlineData(21, 2, "not implemented")]
        [InlineData(22, 1, "not implemented")]
        [InlineData(22, 2, "not implemented")]
        [InlineData(23, 1, "not implemented")]
        [InlineData(23, 2, "not implemented")]
        [InlineData(24, 1, "not implemented")]
        [InlineData(24, 2, "not implemented")]
        [InlineData(25, 1, "not implemented")]
        [InlineData(25, 2, "not implemented")]

        public async void DayPartResultIsAsExpected(int day, int part, string expectedResult)
        {
            
            var concreteType = Assembly.GetAssembly(typeof(ILogic)).GetTypes()
                .Where(t => typeof(ILogic).IsAssignableFrom(t))
                .SingleOrDefault(t => t.Namespace.Equals($"AoC2021.Day{day}"));
            Assert.NotNull(concreteType);

            var method = concreteType.GetMethod($"RunPart{part}");
            Assert.NotNull(method);

            var sut = Activator.CreateInstance(concreteType) as ILogic;
            var t = method.Invoke(sut, new object[] { @$"C:\Users\jwr\source\repos\AoC2021\AoC2021\Day{day}\TestInput.txt" }) as Task<string>;
            Assert.Equal(expectedResult, await t);            
        }
    }
}