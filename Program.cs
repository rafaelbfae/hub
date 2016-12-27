using System;

using Microsoft.AspNetCore.Hosting;

namespace CrmHub
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Servidor Iniciado");

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}