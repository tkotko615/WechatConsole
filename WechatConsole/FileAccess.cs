using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WechatConsole
{
    class FileAccess
    {
        string FilePath { get; set; }

        public FileAccess(string filepath)
        {
            this.FilePath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @filepath;
        }

        public void Write(string str)
        {
            if (!File.Exists(FilePath))
            {
                FileStream fs = File.Create(FilePath);
                fs.Close();
            }
            StreamWriter wr = new StreamWriter(FilePath, true, System.Text.Encoding.UTF8);
            wr.WriteLine(str);
            wr.Close();
        }

        public string Read()
        {
            var str = "";
            StreamReader sr = new StreamReader(FilePath);
            while (!sr.EndOfStream)
            {
                str += sr.ReadLine();
            }
            sr.Close();
            return str;
        }
    }
}
