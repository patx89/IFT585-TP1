using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading;
using System.Configuration;

namespace TP1
{
	public class Network : INotifyPropertyChanged
	{
		//Sender part
		public Byte[] sourceToDestination = new byte[int.Parse (ConfigurationManager.AppSettings ["BufferSize"])];
		public Byte[] destinationFromSource = new byte[int.Parse (ConfigurationManager.AppSettings ["BufferSize"])];
		//Receiver part
		public Byte[] sourceFromDestination = new byte[int.Parse (ConfigurationManager.AppSettings ["BufferSize"])];
		public Byte[] destinationToSource = new byte[int.Parse (ConfigurationManager.AppSettings ["BufferSize"])];
		private bool sourceCanSend = true;
        private bool sourceCanReceive = false;
        private bool destinationCanSend = true;
        private bool destinationCanReceive = false;


        public bool SourceCanSend
        {
            get { return sourceCanSend; }

            set {
                if (this.sourceCanSend != value)
                {
                    this.sourceCanSend = value;
                    OnPropertyChanged("SourceCanSend");
                }            
            }
        }

        public bool SourceCanReceive
        {
            get { return sourceCanReceive; }

            set
            {
                if (this.sourceCanReceive != value)
                {
                    this.sourceCanReceive = value;
                    OnPropertyChanged("SourceCanReceive");
                }
            }
        }

        public bool DestinationCanSend
        {
            get { return destinationCanSend; }

            set
            {
                if (this.destinationCanSend != value)
                {
                    this.destinationCanSend = value;
                    OnPropertyChanged("DestinationCanSend");
                }
            }
        }

        public bool DestinationCanReceive
        {
            get { return destinationCanReceive; }

            set
            {
                if (this.destinationCanReceive != value)
                {
                    this.destinationCanReceive = value;
                    OnPropertyChanged("DestinationCanReceive");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


		public void setSourcePacket (Byte[] frame)
		{
			if (this.SourceCanSend) {
				sourceToDestination = frame;
				this.SourceCanSend = false;
			}
		}

		public void setDestinationPacket (Byte[] frame)
		{
			if (this.DestinationCanSend) {
				destinationToSource = frame;
				this.DestinationCanSend = false;
			}
		}
		// From source
		/*
		public Byte[] SourceToDestination { 
			private get { return sourceToDestination; }
			set {
				sourceToDestination = value;
				SourceCanSend = false;
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
				DestinationCanSend = false;
			}
		}

		public Byte[] SourceFromDestination {
			get { 
				SourceHasReceived = false;
				return sourceFromDestination;
			}
			private set { sourceFromDestination = value; }
		}
		*/
		public void manageRequests ()
		{
			Thread workerOne = new Thread (() => this.SourceToDestination ());
			Thread workerTwo = new Thread (() => this.DestinationToSource ());

            workerOne.Start();
            workerTwo.Start();
		}

		private void SourceToDestination ()
		{
			Random integer = new Random ();
			while (true) {
				if (!sourceCanSend && !destinationCanReceive) {
					Thread.Sleep (integer.Next (0, 10) * 1000);
					Console.WriteLine ("Network has transfered the data - SourceToDestination");
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
					Console.WriteLine ("Network has transfered the data - DestinationToSource");
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
