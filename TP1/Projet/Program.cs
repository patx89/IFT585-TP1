using System;
using System.Configuration;

namespace TP1
{
	public class Program
	{
		public enum Protocols
		{
		}

		public static void Main (string[] args)
		{
			Console.WriteLine ("Hello World!");

			/*Network network = new Network ();
			Station sender = new EStation (network, ConfigurationManager.AppSettings ["SourceFilePath"]);
			Station receiver = new RStation (network, ConfigurationManager.AppSettings ["DestinationFilePath"]);

			Console.WriteLine ("Starting network!");
			network.manageRequests ();

			Console.WriteLine ("Starting the sender!");
			sender.start ();

			Console.WriteLine ("Starting the receiver!");
			receiver.start ();*/

		}
	}
}
