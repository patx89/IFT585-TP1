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

			if (network.sourceCanSend) {
				Console.WriteLine ("Sending the packet");
				//network.setSourcePacket (buffer.getFrame ().ToBytes ());
				network.setSourcePacket (buffer.GetNextElement ());
			} else
				Console.WriteLine ("Unable to send any packets for X,Y reason");
		}

		protected override void sendPackets (Frame test)
		{

		}

		protected override void receivePackets ()
		{
			Frame ack;
			Byte[] receivedPacket;
			if (network.sourceCanReceive) {
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
				while (fileReader.PeekChar () != -1) {
					try {
						//readData = fileReader.ReadBytes (this.PACKETSIZE);
						//DataFrame data = new DataFrame ();
						//hasInsertedPacket = buffer.addPacket (data.ToBytes());
						// TO REMOVE - ONLY FOR TESTING
						hasInsertedPacket = true;
						byte[] fakeData = new byte[5];
						fakeData = this.createFakeData ();
						buffer.Push (fakeData);
						// TO REMOVE - ONLY FOR TESTING
						if (hasInsertedPacket)
							Console.WriteLine ("Set the following packet in the CircularBuffer: {0}", fakeData);
						else
							Console.WriteLine ("Unable to set the following packet in the CircularBuffer: {0}, the buffer might be full?", fakeData);
					} catch (Exception e) {
						Console.WriteLine ("Error ! : {0}", e.Message);
					}
				}
				Console.WriteLine ("Reading file - Stopped (There is nothing more to read in the file)");
			}
		}

		private byte[] createFakeData ()
		{
			byte[] fake = new byte[5];
			fake [0] = Convert.ToByte (GetNextPacketID);
			fake [1] = Convert.ToByte ('b');
			fake [2] = Convert.ToByte ('c');
			fake [3] = Convert.ToByte ('d');
			fake [4] = Convert.ToByte ('e');
			return fake;
		}
	}
}
