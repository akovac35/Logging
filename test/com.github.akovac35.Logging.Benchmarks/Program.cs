// License:
// Apache License Version 2.0, January 2004

// Authors:
//   Aleksander Kovač

using BenchmarkDotNet.Running;

namespace com.github.akovac35.Logging.Benchmarks
{
    public class Program
    {
        public static void Main(string[] args) => BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
    }
}
