using System;
using System.Collections.Generic;

namespace SumaKontrolna
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            byte m;
            byte b = 0;
            byte a = 0;
            char[] zmienne = new char[] {'d', 'c', 'x'};
            foreach (var v in zmienne)
            {
                for (int j = 1; j < 8; j++)
                {
                    m = (byte) (1 << (j - 1));

                    b = (byte) ((v & m) >> (j - 1));
                    a ^= b;
                }
            }
            
            Console.WriteLine(a);
        }
    }
}