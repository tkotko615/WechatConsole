using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WechatConsole
{
    class Program
    {
        static string wechatAPI { get; set; }

        static void Main(string[] args)
        {
            ConfigSet();
            //wechatAPI = "http://exweb.acesconn.com/wechatapi/api/values";
            SentMsg().Wait();
        }

        //檢查筆數不對
        static async Task SentMsg()
        {
            string sLog = "";
            try
            {
                using (HttpClient client = new HttpClient())
                using (HttpResponseMessage response = await client.GetAsync(wechatAPI))
                using (HttpContent content = response.Content)
                {
                    //取得API的內容
                    string result = await content.ReadAsStringAsync();
                    sLog = "API傳回值: "+result;
                }
            }
            catch (Exception e)
            {
                sLog= "console執行錯誤: " + e.Message;
            }

            var file = new FileAccess("log_" + DateTime.Now.ToString("yyyy-MM") + ".txt");
            file.Write(DateTime.Now + "=>" + sLog);
        }

        //讀取設定檔
        public static void ConfigSet()
        {
            //讀取INI
            string iniPath = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\Setting.ini";
            if (System.IO.File.Exists(iniPath))
            {
                wechatAPI = ReadIniValues("apiUrl", "sentMsgUrl");
            }
        }
        //讀寫INI文件API函數
        public static string ReadIniValues(string section, string key)
        {
            string iniPath = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\Setting.ini";
            StringBuilder sb = new StringBuilder(22000);
            int i = GetPrivateProfileString(section, key, "Not Found", sb, 22000, iniPath);
            return sb.ToString();
        }
        [System.Runtime.InteropServices.DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
    }

}
