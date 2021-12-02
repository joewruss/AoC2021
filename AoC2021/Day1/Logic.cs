using System.Threading.Tasks;

namespace AoC2021.Day1
{
    public class Logic : ILogic
    {        
        public async Task<string> RunPart1(string fileName)
        {
            var depths = await Tools.InputParser.GetInputAsEnumerable<int>(fileName, (t) => { return int.Parse(t) ; });
            
            // start at -1 as the very first cycle through will make it go to zero
            int depthIncreases = -1;
            int formerDepth = 0;

            foreach(var currentDepth in depths)
            {
                if (currentDepth > formerDepth) depthIncreases++;
                formerDepth = currentDepth;                
            }

            return depthIncreases.ToString();
        }

        public async Task<string> RunPart2(string fileName)
        {
            var depths = await Tools.InputParser.GetInputAsDictionary<int>(fileName, (t) => { return int.Parse(t); });

            // start at -1 as the very first cycle through will make it go to zero
            int depthIncreases = -1;
            int formerDepthTriad = 0;
            for (int position = 2; position < depths.Keys.Count; position++)
            {   
                //take the sum of the depth from depth plus the two before it and compare 
                //to the former cycle
                if (depths[position - 2] + depths[position - 1] + depths[position] > formerDepthTriad) depthIncreases++;
                formerDepthTriad = depths[position - 2] + depths[position - 1] + depths[position];
            }

            return depthIncreases.ToString();
        }
    }
}