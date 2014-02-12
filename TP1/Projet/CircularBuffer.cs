using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TP1
{
    public class CircularBuffer
    {

        public event FreedSlotEventHandler FreedSlot;

        public delegate void FreedSlotEventHandler(object sender, EventArgs e);


        public List<Frame> _frames
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

    }

    class ChangedEventHandler
    {
    }
}
