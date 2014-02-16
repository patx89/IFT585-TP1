using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TP1
{
	public class RStation : Station
	{
		private string _destinationFile;

		public RStation (Network network, string destinationFile) : base (network)
		{
			this._destinationFile = destinationFile;
		}

		public override void start ()
		{
			this.receivePackets ();
		}

		protected override void sendPackets ()
		{

		}

		protected override void sendPackets (Frame packet)
		{
			
		}

		protected override void receivePackets ()
		{
			Byte[] receivedPacket;
			FileStream myFile;
			BinaryWriter fileWriter;

			if (network.destinationCanReceive) {
				receivedPacket = network.destinationFromSource;
				//Console.WriteLine ("I have receive the packet with ID {0}", receivedPacket.ID);
				Console.WriteLine ("I have receive a packet with ID {0}", receivedPacket [0]);
				if (validatePacket (receivedPacket)) {
					if (network.destinationCanSend) {
						network.destinationToSource = receivedPacket;
						// Besoin de regarder l'ordre des packets!
						Console.WriteLine ("The received packet is valid!");
						try {
							myFile = new FileStream (_destinationFile, FileMode.OpenOrCreate);
							fileWriter = new BinaryWriter (myFile);
							//fileWriter.Write (receivedPacket.data);

							// Unknown error, commenting for now
							//fileWriter.Write (receivePackets);
							fileWriter.Flush ();
							fileWriter.Close ();
						} catch (Exception ex) {
							Console.WriteLine ("Error ! {0}", ex.Message);
						}
					}
				} else {
					Console.WriteLine ("The received packet is invalid for X, Y reason !");
					//Frame ack = new ACK (false, receivedPacket.ID);
					//sendPackets (ack);
				}
			}
		}

		private bool validatePacket (byte[] frame)
		{
			return true;
		}
	}
}
