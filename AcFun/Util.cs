using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcFun
{
    class Util
    {
        public enum PrintType { INFO, WARNING, ERROR }
        static public void Print(string t, PrintType type = PrintType.INFO, ConsoleColor c = ConsoleColor.Gray)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("[");

            if (type == PrintType.INFO)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("信息");
            }
            else if (type == PrintType.WARNING)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("警告");
            }
            else if (type == PrintType.ERROR)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("错误");
            }

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("] ");

            Console.ForegroundColor = c;
            Console.Write(t + "\n");

            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
