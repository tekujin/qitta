using System;
using System.Diagnostics;
using System.IO;

namespace wifi
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Process ps = new Process();
                StreamReader sr;

                ps.StartInfo.FileName = "cmd";
                ps.StartInfo.Arguments = "/A /C netsh wlan show profile|find \":\"";
                ps.StartInfo.RedirectStandardOutput = true;
                ps.StartInfo.UseShellExecute = false;
                ps.StartInfo.CreateNoWindow = true;
                ps.Start();
                ps.WaitForExit();
                sr = ps.StandardOutput;

                while (sr.Peek() > -1)
                {
                    string line = sr.ReadLine().Trim();
                    showProfile(line.Split(':')[1].Trim());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void showProfile(string profile)
        {
            Process ps = new Process();
            StreamReader sr;

            ps.StartInfo.FileName = "cmd";
            ps.StartInfo.Arguments = "/A /C netsh wlan show profile \""+ profile+ "\" key=clear|find \"コンテンツ\"";
            ps.StartInfo.RedirectStandardOutput = true;
            ps.StartInfo.UseShellExecute = false;
            ps.StartInfo.CreateNoWindow = true;
            ps.Start();
            ps.WaitForExit();
            sr = ps.StandardOutput;

            while (sr.Peek() > -1)
            {
                string line = sr.ReadLine().Trim();
                Console.WriteLine("ssid="+profile + "\npwd ="+line.Split(':')[1].Trim()+"\n");
            }
        }
    }
}