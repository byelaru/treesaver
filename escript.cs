using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SharpOS.escript;
using static System.Console;

namespace SharpOS
{
    namespace escript
    {
        class eScriptFile
        {
            private static string pth;

            public List<string> varnames;
            public List<string> varvalue;

            public string[] scripttext;

            public eScriptFile(string path)
            {
                pth = path;
                scripttext = File.ReadAllLines(pth);
            }

            public int FindBlock(string blockname)
            {
                int blockPosition = -1;
                int tempBlockPosition = -1;
                foreach (string temp in scripttext)
                {
                    tempBlockPosition++;
                    if (temp.IndexOf(blockname + ':') == 0) blockPosition = tempBlockPosition;
                }
                return blockPosition;
            }

            public List<string> GetVariableList()
            {
                int s = this.FindBlock("variables");
                if (s == -1)
                {
                    WriteLine("В файле нет блока \"variables\"!");
                    return null;
                }
                for(int i = s+2; scripttext[i] != "}"; i++)
                {
                    for(int z = 0; scripttext[i][z] != '('; z++)
                    {
                        if (scripttext[i][s] != ' ')
                        {
                            varnames[i] = varnames[i] + scripttext[i][z];
                        }
                    }
                }
                return this.varnames;
            }
        }
    }
}
