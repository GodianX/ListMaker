using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ListMaker
{
    class Program
    {
        static void Main(string[] args)
        {
            ListRand list = new ListRand();
            list.AddFromFile("1.txt");
            list.Serialize(new FileStream("s.txt",FileMode.Create));
            list.Deserialize(new FileStream("s.txt",FileMode.Open));
            list.PrintList();
            Console.ReadKey();
        }
    }
}
