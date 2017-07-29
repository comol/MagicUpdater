using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace MagicUpdaterDebugConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			var service = new MagicUpdater.Service1();
			ServiceBase[] servicesToRun = new ServiceBase[] { service };

			if (Environment.UserInteractive)
			{
				Console.CancelKeyPress += (x, y) => service.Stop();

				service.Start();
				Console.WriteLine("Служба запущена");

				Console.ReadKey();
				service.Stop();
				Console.WriteLine("Служба остановлена");
			}
			else
			{
				ServiceBase.Run(servicesToRun);
			}
		}
	}
}
