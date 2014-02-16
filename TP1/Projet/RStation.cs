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
		private int lastReceivedID = 0;

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
			DataFrame frame;
			FileStream myFile;
			BinaryWriter fileWriter;

			if (network.destinationCanReceive) {
				receivedPacket = network.destinationFromSource;
				frame = DataFrame.FromBytes (receivedPacket);
				Console.WriteLine ("I have receive the packet with ID {0}", frame.ID);
				if (validatePacket (receivedPacket)) {
					if (network.destinationCanSend) {
						Console.WriteLine ("The received packet is valid!");
						network.destinationToSource = receivedPacket;
						// Besoin de regarder l'ordre des packets!
						if (frame.ID - 1 == lastReceivedID) {
							Console.WriteLine ("The packet is in the correct order !");
							try {
								myFile = new FileStream (_destinationFile, FileMode.OpenOrCreate);
								fileWriter = new BinaryWriter (myFile);
								fileWriter.Write (frame.Data);
								fileWriter.Flush ();
								fileWriter.Close ();
							} catch (Exception ex) {
								Console.WriteLine ("Error ! {0}", ex.Message);
							}
						} else {
							Console.WriteLine ("The packet was not sent in the correct order !");
							// Put in ValidatePacket?!?!
							// DO NACK !
						}
					}
				} else {
					Console.WriteLine ("The received packet is invalid for X, Y reason !");
					//ACK ack = new ACK (frame.ID);
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
