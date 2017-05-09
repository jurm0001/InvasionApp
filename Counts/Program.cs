using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

namespace Counts
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, double> dic = new Dictionary<string, double>();
            TextReader rdr = new
            StreamReader(@"C:\Users\Jurm\Desktop\MouseScripts\Type.dat");
            string online = "";
            while ((online = rdr.ReadLine()) != null)
            {
                if (!dic.ContainsKey(online.Trim()))
                    dic.Add(online, 1);
                else
                    dic[online] = dic[online] + 1;
            }

            double total = dic.Sum(x => x.Value);

            double en = (dic["Energy"] / total) * 100;
            double oil = (dic["Oil"] / total) * 100;
            double food = (dic["Food"] / total) * 100;
            double st = (dic["Steel"] / total) * 100;


            Console.WriteLine("Energy: " + en.ToString("0.00") + "%");
            Console.WriteLine("Oil: " + oil.ToString("0.00") + "%");
            Console.WriteLine("Food: " + food.ToString("0.00") + "%");
            Console.WriteLine("Steel: " + st.ToString("0.00") + "%");
            
            rdr.Close();

            Console.ReadKey();
        }
    }
}
