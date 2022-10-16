//内容参照サイト：https://qiita.com/neomi/items/3076f97af4eb5685bbe0
using System;
using System.IO;
using System.Security.Cryptography;

namespace filelist
{
    class Program
    {
        static readonly HashAlgorithm hashProvider = new SHA1CryptoServiceProvider();

        /// <summary>
        /// Returns the hash string for the file.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string ComputeFileHash(string filePath)
        {
            var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            var bs = hashProvider.ComputeHash(fs);
            return BitConverter.ToString(bs).ToLower().Replace("-", "");
        }

        static void Main(string[] args)
        {
            string path1 = @".\";
            try
            {
                var filename = Directory.EnumerateFiles(path1, "*.*", System.IO.SearchOption.AllDirectories);

                StreamWriter sw = new StreamWriter(@".\file" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt");

                foreach (string f in filename)
                {
                    if (args.Length == 1 && args[0].ToString().ToUpper() == "-H")
                    {
                        Console.WriteLine(ComputeFileHash(f) + " " + f.Substring(2,f.Length - 2));
                        sw.WriteLine(ComputeFileHash(f) + " " + f.Substring(2, f.Length - 2));
                    }
                    else
                    {
                        Console.WriteLine(f.Substring(2, f.Length - 2));
                        sw.WriteLine(f.Substring(2, f.Length - 2));
                    }
                }

                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
