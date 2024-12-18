
using System.Text;

namespace BuildTool
{
    public class Program
    {
        public static int Main(string[] args)
        {
#if DEBUG
            StringBuilder argsStringBuilder = new StringBuilder();
            foreach (string arg in args)
            {
                argsStringBuilder.Append(arg);
                argsStringBuilder.Append(" ");
            }

            Console.WriteLine($"Running Build Tool with: {argsStringBuilder.ToString()}");
#endif
            return 0;
        }
    }
}
