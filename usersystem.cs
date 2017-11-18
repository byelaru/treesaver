using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using SharpOS.Cryptography;
using static System.Console;

namespace SharpOS
{
    class usersystem
    {
        public static string user;
        public static List<string> passwords = new List<string>();
        public static List<string> users = new List<string>();

        public static void comChangeUser(string com)
        {
            string[] args = com.Split(' ');
            if (args.Count() < 4)
            {
                WriteLine("Используйте user change {имя учетной записи} {пароль}");
                return;
            }
            bool exst = false;
            foreach (string temp in users)
            {
                if (args[2] == temp) { exst = true; }
            }
            if (!exst) { WriteLine("Учетная запись не существует."); return; }
            int userid = users.IndexOf(args[3]);
            Write("Введите пароль(введите \"-1\", если не хотите переключаться): ");
            string passw = ReadLine();
            while (passw != passwords[userid] && passw != "-1")
            {
                Write("Попробуйте снова: ");
                passw = ReadLine();
            }
            if (passw != "-1")
            {
                user = args[3];
                WriteLine("Вы успешно вошли в учетную запись " + user);
            }


        }

        public static void comCreateUser(string com)
        {
            string[] args = com.Split(' ');
            if (args.Count() > 3)
            {
                foreach (string temp in users)
                {
                    if (temp == args[2]) { WriteLine("Учетная запись с таким же именем уже существует."); return; }
                }

                Stream pass = GenerateStream.FromString(args[3]);
                MD5 ToMD5 = MD5.Create();
                byte[] md5pass = ToMD5.ComputeHash(pass);
                string newpass = "";
                foreach (byte temp in md5pass)
                {
                    newpass += temp;
                }
                string app = "\r\n" + args[2] + "\r\n" + newpass;
                File.AppendAllText("data\\users.csos", app);
                users.Add(args[2]);
                passwords.Add(args[3]);
                WriteLine("Учетная запись успешно создана.");
            }
            else WriteLine("Используйте user create {имя учетной записи} {пароль}");
        }

        public static void comSetUserName(string com)
        {
            string[] args = com.Split(' ');
            if (args.Count() < 5)
            {
                WriteLine("Используйте user setname {имя учетной записи} {новое имя учетной записи} {пароль от этой учетной записи}");
                return;
            }
            if (!userExist(args[2]))
            {
                WriteLine("Учетная запись не существует.");
                return;
            }
            if (args[3] == "")
            {
                WriteLine("Используйте user setname {имя учетной записи} {новое имя учетной записи} {пароль от этой учетной записи}");
                return;
            }
            if (passwords[getUserId(args[2])] != md5.getHash(args[4]))
            {
                WriteLine("Неверный пароль от учетной записи.");
                return;
            }
            users[getUserId(args[2])] = args[3];
            FileStream usersStream = new FileStream("data\\users.csos", FileMode.Open, FileAccess.Write);
            StreamWriter usersWriter = new StreamWriter(usersStream);
            int i = 0;
            foreach (string temp in users)
            {
                usersWriter.WriteLine(temp);
                usersWriter.WriteLine(passwords[i]);
                i++;
            }
            usersWriter.Close(); usersStream.Close();
            WriteLine("Имя учетной записи изменено на" + args[3] + ".");

        }

        public static bool userExist(string username)
        {
            foreach (string temp in users)
            {
                if (temp == username) { return true; }
            }
            return false;
        }

        public static int getUserId(string username)
        {
            int i = 0;
            foreach (string temp in users)
            {
                if (temp == username) return i;
                i++;
            }
            return -1;
        }

        public static void comDeleteUser(string com)
        {
            string[] args = com.Split(' ');
            bool exst = false;
            foreach (string temp in users)
            {
                if (temp == args[2]) { exst = true; }
            }
            if (!exst) { WriteLine("Учетной записи с таким именем не существует."); return; }
            if (args.Count() > 2)
            {
                if (users.Count > 1 && args[2] != user)
                {
                    string[] tms = File.ReadAllLines("data\\users.csos");
                    bool dlt = false;
                    for (int i = 0; i < tms.Count(); i++)
                    {
                        if (dlt)
                        {
                            tms[i] = "";
                            dlt = false;
                        }
                        if (tms[i] == args[2])
                        {
                            tms[i] = "";
                            dlt = true;
                        }
                    }
                    FileStream usersStream = new FileStream("data\\users.csos", FileMode.Create);
                    StreamWriter usersWriter = new StreamWriter(usersStream);
                    foreach (string temp in tms)
                    {
                        if (temp != "")
                        {
                            usersWriter.WriteLine(temp);
                        }
                    }
                    WriteLine("Учетная запись успешно удалена.");
                    users.Remove(args[2]);
                    usersWriter.Close(); usersStream.Close();
                }
                else if (args.Count() < 3) WriteLine("Используйте user delete {имя учетной записи}");
                else if (args[2] == user) WriteLine("Нельзя удалить свою же учетную запись. Войдите с другой.");
                else WriteLine("Осталась только одна учетная запись. Создайте новую, что-бы удалить эту.");

            }
        }
    }
}
