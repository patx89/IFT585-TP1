using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading;

namespace TP1
{
	public class Network
	{
		//Sender part
		public Byte[] sourceToDestination;
		public Byte[] destinationFromSource;
		//Receiver part
		public Byte[] sourceFromDestination;
		public Byte[] destinationToSource;
		public bool sourceCanSend = true;
		public bool sourceCanReceive = false;
		public bool destinationCanSend = true;
		public bool destinationCanReceive = false;

		public void setSourcePacket (Byte[] frame)
		{
			if (this.sourceCanSend) {
				sourceToDestination = frame;
				this.sourceCanSend = false;
			}
		}

		public void setDestinationPacket (Byte[] frame)
		{
			if (this.destinationCanSend) {
				destinationToSource = frame;
				this.destinationCanSend = false;
			}
		}
		// From source
		/*
		public Byte[] SourceToDestination { 
			private get { return sourceToDestination; }
			set {
				sourceToDestination = value;
				sourceCanSend = false;
			}
		}

		public Byte[] DestinationFromSource {
			get { 
				destinationHasReceived = false;
				return destinationFromSource;
			}
			private set { destinationFromSource = value; }
		}
		// From destination
		public Byte[] DestinationToSource { 
			private get { return destinationToSource; }
			set {
				destinationToSource = value;
				destinationCanSend = false;
			}
		}

		public Byte[] SourceFromDestination {
			get { 
				sourceHasReceived = false;
				return sourceFromDestination;
			}
			private set { sourceFromDestination = value; }
		}
		*/
		public void manageRequests ()
		{
			Thread workerOne = new Thread (() => this.SourceToDestination ());
			Thread workerTwo = new Thread (() => this.DestinationToSource ());
		}

		private void SourceToDestination ()
		{
			Random integer = new Random ();
			while (true) {
				if (!sourceCanSend && !destinationCanReceive) {
					Thread.Sleep (integer.Next (0, 10) * 1000);
					/* 
					 * testing timeOut of the packet, do not set if the packet is expired
					 * ? Do we have to remove it from the buffer of the source then?
					if (sourceToDestination.dateSend - currentTime > Timeout)
						return;
					*/
					destinationFromSource = sourceToDestination;
					sourceCanSend = destinationCanReceive = true;
				}
			}
		}

		private void DestinationToSource ()
		{
			Random integer = new Random ();
			while (true) {
				if (!destinationCanSend && !sourceCanReceive) {
					Thread.Sleep (integer.Next (0, 10) * 1000);
					/* 
					 * testing timeOut of the packet, do not set if the packet is expired
					 * ? Do we have to remove it from the buffer of the source then?
					if (sourceToDestination.dateSend - currentTime > Timeout)
						return;
					*/
					sourceFromDestination = destinationToSource;
					destinationCanSend = sourceCanReceive = true;
				}
			}
		}
	}
}
