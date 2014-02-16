using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TP1
{
    public class Frame
    {
        private int _id;

        public int ID
        {
            get
            {
                return _id;
            }
        }


        public Frame(int id)
        {
            _id = id;
        }
    }
}
