using System;
using System.Linq;

namespace Equinox.Patcher
{
    internal class Program
    {
        public const string Version = "1.0";

        private static void Main(string[] args)
        {
            Console.WriteLine("This patcher was created by www.equinoxgamers.com");
            Console.WriteLine("Version v" + Version);
            //Console.WriteLine("Copyright © EquinoxGamers 2016");
            Console.WriteLine("Patching...");

            ILPatcher patcher = new ILPatcher();
            patcher.FirstPass();
            Console.WriteLine("The patch was applied successfully!");
            Console.ReadLine();
        }
    }
}
