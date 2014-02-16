using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.ComponentModel;

namespace TP1
{
	public class EStation : Station
	{
		private string _sourceFile;
		private int packetID;

		protected int GetNextPacketID {
			get { return ++packetID; }
		}

		public EStation (Network network, string sourceFile) : base (network)
		{
			this._sourceFile = sourceFile;
		}

		public override void start ()
		{
			this.readFile ();
		}

		protected override void sendPackets ()
		{

			if (network.SourceCanSend) {
				Console.WriteLine ("Sending the packet");
				//network.setSourcePacket (buffer.getFrame ().ToBytes ());
				network.setSourcePacket (buffer.GetNextElement ().ToBytes ());
			} else
				Console.WriteLine ("Unable to send the packet since the Receiver is unable to compute");
				
		}

		protected override void sendPackets (Frame test)
		{

		}

		protected override void receivePackets ()
		{
			Frame ack;
			Byte[] receivedPacket;
			if (network.SourceCanReceive) {
				receivedPacket = network.sourceFromDestination;
				Console.WriteLine ("I have received a packet!");
				/*
				ack = getFromNetwork();
				if (ack.isAccepted == true)
					CircularBuffer.removePacket (ack.ID);
				else
					CircularBuffer.mustResend (ack.ID);
				*/
			}
		}

		private void readFile ()
		{
			byte[] readData;
			bool hasInsertedPacket;
			FileStream myFile = new FileStream (_sourceFile, FileMode.Open);

			if (myFile.CanRead) {
				BinaryReader fileReader = new BinaryReader (myFile);
			
				Console.WriteLine ("Reading file - Started");
				if (!buffer.IsFull) {
					while (fileReader.PeekChar () != -1) {
						try {
							readData = fileReader.ReadBytes (PACKETSIZE);
							DataFrame frame = new DataFrame (this.GetNextPacketID);
							frame.Data = readData;
							frame.Lng = PACKETSIZE;
							frame.Protocol = Program.Protocols.Protocole1;
							frame.Timeout = Convert.ToByte (69);
					
							hasInsertedPacket = buffer.Push (frame);
							if (hasInsertedPacket)
								Console.WriteLine ("Set the following packet in the CircularBuffer: {0}", frame.ID);
							else
								Console.WriteLine ("Unable to set the following packet in the CircularBuffer: {0}, the buffer might be full?", frame.ID);
							this.sendPackets ();
						} catch (Exception e) {
							Console.WriteLine ("Error ! penis : {0}", e.Message);
						}
					}
					Console.WriteLine ("Reading file - Stopped (There is nothing more to read in the file)");
				} else {
					// JP - SUBSCRIBE TO EVENT "NOT FULL ANYMORE" or w/e
				}
			}
		}
	}
}
