using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using static System.Console;

namespace lesson
{
    class Program
    {
        /* 
        Имеется список имен. Создайте метод, который
        будет выводить на экран эти имена через запятую.
        Перегрузите этот метод так, чтобы можно было
        изменять разделитель – вместо запятых между
        именами любой символ, переданный параметром. 
        */

        public static void WriteAllList(List<string> templist)
        {
            foreach(string temp in templist)
            {
                Write(temp + ", ");
            }
        }

        public static void WriteAllList(List<string> templist, char symb)
        {
            foreach(string temp in templist)
            {
                Write(temp + symb + " ");
            }
        }
          
        static void Main(string[] args)
        {
            List<string> names = new List<string>();
            names.Add("First");
            names.Add("Second");
            names.Add("Third");
            WriteAllList(names);
            WriteAllList(names,'_');
            ReadKey();
        }
    }
}
