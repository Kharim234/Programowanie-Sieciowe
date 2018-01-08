using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRC
{
    class Program
    {
        static void Main(string[] args)
        {
            short result =0, final_res=0, CRC_div, data_byte = 0x52;
            short[] data = new short[] { 0x23, 0x25, 0x13, 0x5 };
            // Console.WriteLine(data_byte);
            CRC_div = 0xA; //0b1010
            //mask = 0b1111;
            int CRC_div_len = 4;
            result = 0;
            //data_byte = (short) (data_byte << (CRC_div_len -1));
            foreach (var item in data)
            {

 
                result ^= item;
                for (int i = 0; i < 8 + CRC_div_len - 1; i++)
                {
                    
                    if ((result & 1) > 0)
                    {
                        result = (short)(result ^ CRC_div);
                    }
                    result >>= 1;
                    
                }
            }
            Console.WriteLine(result);
            //Console.ReadLine();
        }
    }
}
