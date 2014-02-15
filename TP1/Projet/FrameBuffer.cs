using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TP1
{
    public class FrameBuffer : CircularBuffer<Frame>
    {

        public FrameBuffer(int length)
            : base(length)
        {

        }

        /// <summary>
        /// Remove a Frame from its ID.
        /// </summary>
        /// <param name="frameID">The id of the Frame we want to remove.</param>
        /// <returns></returns>
        public Frame RemoveFromFrameID(int frameID)
        {
            int pos = 0;

            do
            {
                if (this[pos] != null &&
                    this[pos].ID == frameID)
                {
                    return RemoveAt(pos);
                }

                pos++;
            } while (pos < Length);

            return null;
        }

    }
}
