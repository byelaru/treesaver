using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static System.Console;

namespace SharpOS
{
    class filesystem
    {
        public static string path = "files";

        public static void comObserve()
        {
            List<string> dirs = new List<string>(Directory.EnumerateDirectories(path));
            WriteLine();
            WriteLine("--- ");
            WriteLine(path + ":");
            foreach (string temp in dirs)
            {
                WriteLine("*DIR* {0}", temp.Substring(temp.LastIndexOf("\\") + 1));
            }
            WriteLine();
            string[] files = Directory.GetFiles(path);
            foreach (string temp in files)
            {
                WriteLine(temp.Substring(temp.LastIndexOf('\\') + 1));
            }
            WriteLine("---");
            WriteLine();
        }

        public static void comSetDirectory(string com)
        {
            string[] args = com.Split(' ');
            string need;
            if (args.Count() < 2)
            {
                WriteLine("Используйте cd {директория} или '..'");
                return;
            }
            if (args[1] == "..")
            {
                path = path.Remove(path.LastIndexOf('\\'));
                return;
            }
            if (args[1].IndexOf('\\') == args[1].Length)
            {
                need = args[1].Remove(args[1].Length);
            }
            else
            {
                need = args[1];
            }
            if (!Directory.Exists(path + "\\" + need))
            {
                WriteLine("Директории " + path + '\\' + need + " не существует.");
                return;
            }
            path += '\\' + need;
        }

        public static void comCreateDirectory(string com)
        {
            string[] args = com.Split(' ');
            if (args.Count() < 3)
            {
                WriteLine("Используте dir create {название директории}");
                return;
            }
            if (args[2].Contains('\\') || args[2].Contains('/') || args[2].Contains(':') || args[2].Contains('"') || args[2].Contains('<') || args[2].Contains('*') || args[2].Contains('>') || args[2].Contains('|'))
            {
                WriteLine("Не используйте симоволы '\\', '/', ':', '*', '\"', '<', '>', '|'.");
                return;
            }
            if (Directory.Exists(path + "\\" + args[2]))
            {
                WriteLine("Директория с таким названием уже существует.");
                return;
            }
            Directory.CreateDirectory(path + '\\' + args[2]);
            WriteLine("Директория создана.");
        }

        public static void comRenameDirectory(string com)
        {
            string[] args = com.Split(' ');
            if (args.Count() < 4)
            {
                WriteLine("Используте dir rename {название директории} {новое название}");
                return;
            }
            if (args[3].Contains('\\') || args[3].Contains('/') || args[3].Contains(':') || args[3].Contains('"') || args[3].Contains('<') || args[3].Contains('*') || args[3].Contains('>') || args[3].Contains('|'))
            {
                WriteLine("Не используйте симоволы '\\', '/', ':', '*', '\"', '<', '>', '|'.");
                return;
            }
            if (Directory.Exists(path + "\\" + args[3]))
            {
                WriteLine("Директория с таким названием уже существует.");
                return;
            }
            if (args[2] == args[3])
            {
                WriteLine("Нужно указывать разные имена.");
                return;
            }
            Directory.Move(path + '\\' + args[2], path + '\\' + args[3]);
            WriteLine("Директория переименована.");
        }

        public static void comDeleteDirectory(string com)
        {
            string[] args = com.Split(' ');
            if (args.Count() < 3)
            {
                WriteLine("Используте dir delete {название директории}");
                return;
            }
            if (!Directory.Exists(path + "\\" + args[2]))
            {
                WriteLine("Директории с таким названием не существует.");
                return;
            }
            Directory.Delete(path + '\\' + args[2], true);
            WriteLine("Директория удалена.");
        }

        public static void comCreateFile(string com)
        {
            string[] args = com.Split(' ');
            if (args.Count() < 3)
            {
                WriteLine("Используйте file create {название файла}");
                return;
            }
            if (args[2].Contains('\\') || args[2].Contains('/') || args[2].Contains(':') || args[2].Contains('"') || args[2].Contains('<') || args[2].Contains('*') || args[2].Contains('>') || args[2].Contains('|'))
            {
                WriteLine("Не используйте симоволы '\\', '/', ':', '*', '\"', '<', '>', '|'.");
                return;
            }
            if (File.Exists(path + "\\" + args[2]))
            {
                WriteLine("Файл с таким названием уже существует.");
                return;
            }
            File.Create(path + '\\' + args[2]);
            WriteLine("Файл создан.");
        }

        public static void comRenameFile(string com)
        {
            string[] args = com.Split(' ');
            if (args.Count() < 4)
            {
                WriteLine("Используйте file rename {название файла} {новое название}");
                return;
            }
            if (args[3].Contains('\\') || args[2].Contains('/') || args[2].Contains(':') || args[2].Contains('"') || args[2].Contains('<') || args[2].Contains('*') || args[2].Contains('>') || args[2].Contains('|'))
            {
                WriteLine("Не используйте симоволы '\\', '/', ':', '*', '\"', '<', '>', '|'.");
                return;
            }
            if (File.Exists(path + "\\" + args[3]))
            {
                WriteLine("Файл с таким названием уже существует.");
                return;
            }
            if (args[2] == args[3])
            {
                WriteLine("Нужно указывать разные названия.");
                return;
            }
            File.Move(path + "\\" + args[2], path + "\\" + args[3]);
            WriteLine("Файл переименован.");
        }

        public static void comDeleteFile(string com)
        {
            string[] args = com.Split(' ');
            if (args.Count() < 2)
            {
                WriteLine("Используйте file delete {название файла}");
                return;
            }
            if (!File.Exists(path + "\\" + args[2]))
            {
                WriteLine("Файл с таким названием не существует.");
                return;
            }
            File.Delete(path + "\\" + args[2]);
            WriteLine("Файл удален.");
        }
    }
}
