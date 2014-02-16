using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TP1
{
    public class DataFrame : Frame
    {
        public DataFrame(int id)
            : base(id)
        {

        }

        public DateTime Date { get; set; }

        public int Timeout { get; set; }

        public int Lng { get; set; }

        public MainClass.Protocols Protocol { get; set; }

        //Chksum will be generated

        public Byte[] Data { get; set; }

        public static DataFrame FromBytes(Byte[] input)
        {
            var bytesList = new List<Byte>(input);

            var frame = new DataFrame( Convert.ToInt16(bytesList[0]));
            bytesList.RemoveAt(0);

            //frame.Date = Convert.ToInt16(bytesList[0]);
            //bytesList.RemoveAt(0);

            frame.Timeout = Convert.ToInt16(bytesList[0]);
            bytesList.RemoveAt(0);

            frame.Lng = Convert.ToInt16(bytesList[0]);
            bytesList.RemoveAt(0);

            frame.Protocol = (MainClass.Protocols)Convert.ToInt16(bytesList[0]);
            bytesList.RemoveAt(0);

            frame.Data = bytesList.ToArray();
            bytesList.RemoveAt(0);

            Console.WriteLine(frame.ID);


            return frame;
        }

        public Byte[] ToBytes()
        {

            
            var bytesList = new List<Byte>
                            {
                                Convert.ToByte(ID),
                                //Convert.ToByte(Date.ToBinary()),
                                Convert.ToByte(Timeout),
                                Convert.ToByte(Lng),
                                Convert.ToByte(Protocol)
                            };


            var bytesTab = new Byte[bytesList.Count + Data.Length];
            for (int i = 0; i < bytesList.Count; i++)
                bytesTab[i] = bytesList[i];

            for (int i = bytesList.Count + 1; i < Data.Length; i++)
            {
                bytesTab[i] = Data[bytesTab.Length - i];
            }
           
            //Vizualisation
            var vizu = "";
            foreach (var byteCell in bytesTab)
                vizu += Convert.ToString(byteCell, 2).PadLeft(8, '0') + "\n";
            Console.WriteLine("Frame: \n" + vizu);

            return bytesTab;
        }
    }
}
