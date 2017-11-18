using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Security.Cryptography;
using Microsoft.CSharp;
using SharpOS;
using SharpOS.escript;
using static System.Console;
using static SharpOS.usersystem;
using static SharpOS.filesystem;

namespace SharpOS
{
    class Program
    {
        public static void comHelp(string com)
        {
            if (com == "help")
            {
                WriteLine("|-------------------------------------|-------------------------------|");
                WriteLine("| help - помощь                       | quit - выход                  |");
                WriteLine("| cd - сменить директорию             | obs(erve) - узнать директории |");
                WriteLine("| user - действия с учетными записями | dir - действия с директориями |");
                WriteLine("| file - действия с файлами           |                               |");
                WriteLine("|-------------------------------------|-------------------------------|");
            }
            else if (com == "help user")
            {
                WriteLine("|-------------------|-----------------------|");
                WriteLine("| create - создать  | delete - удалить      |");
                WriteLine("| change - сменить  | rename - сменить имя  |");
                WriteLine("| get - узнать имя  |                       |");
                WriteLine("|-------------------|-----------------------|");
            }
            else if (com == "help dir")
            {
                WriteLine("|-----------------------|-----------------------|");
                WriteLine("| create - создать      | delete - удалить      |");
                WriteLine("| rename - сменить имя  |                       |");
                WriteLine("|-----------------------|-----------------------|");
            }
        }

        public static void commandNotFound(string com)
        {
            string[] args = com.Split(' ');
            WriteLine(args[0] + ": команда не существует.");
        }

        public static void runDevelopmentScript()
        {
            eScriptFile app = new eScriptFile("files\\scr.cse");
            WriteLine(app.GetVariableList());
        }

        static void Main(string[] args)
        {
            FileStream usersStream = new FileStream("data\\users.csos", FileMode.Open);
            StreamReader usersReader = new StreamReader(usersStream);
            while (!usersReader.EndOfStream)
            {
                users.Add(usersReader.ReadLine());
                passwords.Add(usersReader.ReadLine());
            }
            usersStream.Close(); usersReader.Close();
            int chosen = 1;
            string com;
            int userid = 1;
            int i = 1;
            ConsoleKey key = ConsoleKey.U;
            while (key != ConsoleKey.Enter)
            {
                
                Clear();
                WriteLine("Выберите пользователя:");

                foreach (string temp in users)
                {
                    if (i == chosen)
                        WriteLine("> " + temp);
                    else WriteLine("  " + temp);
                    i++;
                }
                i = 1;
                
                user = users[chosen-1];
                userid = chosen-1;

                key = ReadKey().Key;

                if (key == ConsoleKey.DownArrow && chosen + 1 <= users.Count) chosen++;
                if (key == ConsoleKey.UpArrow && chosen - 1 >= 1) chosen--;

            }
            Clear();
             WriteLine("                     |====================================|");
             WriteLine("                     |          #   #   ####   ####       |");
             WriteLine("                     |        ########  #  #  #           |");
             WriteLine("                     |         #   #    #  #   ###        |");
             WriteLine("                     |       ########   #  #      #       |");
             WriteLine("                     |        #   #     ####  ####        |");
             WriteLine("                     |====================================|");
            Write("Введите пароль: ");
            ForegroundColor = ConsoleColor.Black;
            com = ReadLine();
            Console.ResetColor();
            string inpass = SharpOS.Cryptography.md5.getHash(com);
            if (inpass != passwords[userid])
            {
                return;
            }
            while(com != "quit")
            {
                Write(path + "> ");
                com = ReadLine();
                com = com.ToLower();
                if (com.IndexOf("user create") == 0) comCreateUser(com);
                else if (com.IndexOf("help") == 0) comHelp(com);
                else if (com.IndexOf("user delete") == 0) comDeleteUser(com);
                else if (com.IndexOf("user change") == 0) comChangeUser(com);
                else if (com.IndexOf("user get") == 0) WriteLine("Вы используете учетную запись " + user);
                else if (com.IndexOf("user setname") == 0) comSetUserName(com);
                else if (com.IndexOf("user") == 0) comHelp("help user");
                else if (com.IndexOf("dir create") == 0) comCreateDirectory(com);
                else if (com.IndexOf("dir delete") == 0) comDeleteDirectory(com);
                else if (com.IndexOf("dir rename") == 0) comRenameDirectory(com);
                else if (com.IndexOf("file create") == 0) comCreateFile(com);
                else if (com.IndexOf("file delete") == 0) comDeleteFile(com);
                else if (com.IndexOf("file rename") == 0) comRenameFile(com);
                else if (com.IndexOf("rundscript") == 0) runDevelopmentScript();
                else if (com.IndexOf("exe") == 0 || com.IndexOf("execute") == 0) System.Threading.Thread.Sleep(0);
                else if (com.IndexOf("file") == 0) comHelp("help file");
                else if (com.IndexOf("dir") == 0) comHelp("help dir");
                else if (com.IndexOf("quit") == 0) System.Threading.Thread.Sleep(0);
                else if (com.IndexOf("obs") == 0 || com.IndexOf("observe") == 0) comObserve();
                else if (com.IndexOf("cd") == 0) comSetDirectory(com);
                else commandNotFound(com);
            }
        }
    }
}
