using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace TP1
{
	public abstract class Station
	{
		protected Network network;
		//		protected CircularBuffer<Frame> buffer = new FrameBuffer (int.Parse (ConfigurationManager.AppSettings ["BufferSize"]));
		protected CircularBuffer<byte[]> buffer = new CircularBuffer<byte[]> (int.Parse (ConfigurationManager.AppSettings ["BufferSize"]));
		// number of bytes for each packets
		protected const int PACKETSIZE = 15;
		// number of packet in the buffer
		public Station (Network network)
		{
			this.network = network;
		}

		protected abstract void receivePackets ();

		protected abstract void sendPackets ();

		protected abstract void sendPackets (Frame myFrame);

		public abstract void start ();
	}
}
