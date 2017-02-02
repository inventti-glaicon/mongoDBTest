using System;
using Nancy.Hosting.Self;

namespace LanchoneteMongoDB
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "http://localhost:666";
            using (var host = new NancyHost(new Uri(url)))
            {
                host.Start();
                Console.WriteLine("Running on " + url);
                Console.ReadLine();
            }
        }
    }
}
