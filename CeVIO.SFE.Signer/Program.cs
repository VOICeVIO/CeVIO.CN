using System;
using System.IO;
using dnlib.DotNet;

namespace CeVIO.SFE.Signer
{
    class Program
    {
        private static string zhCN = "zh-CN";
        private static string jaJP = "ja-JP";
        static void Main(string[] args)
        {
            Console.WriteLine("CeVIO.CN Signer");
            Console.WriteLine("by Ulysses from VOICeVIO");
            Console.WriteLine();

            if (args.Length <= 0)
            {
                Console.WriteLine("No Key.");
                return;
            }

            DirectoryInfo dir = null;
            string locale = zhCN;
            if (Directory.Exists(jaJP))
            {
                if (File.Exists("CeVIO.CN.LICENSE.txt"))
                {
                    File.Copy("CeVIO.CN.LICENSE.txt", $"{jaJP}\\CeVIO.CN.LICENSE.txt", true);
                }
                dir = new DirectoryInfo(jaJP);
                locale = jaJP;
            }
            else
            {
                dir = Directory.CreateDirectory(zhCN);
                locale = zhCN;
                if (File.Exists("CeVIO.CN.LICENSE.txt"))
                {
                    File.Copy("CeVIO.CN.LICENSE.txt", $"{zhCN}\\CeVIO.CN.LICENSE.txt", true);
                }
            }

            if (dir?.Parent == null)
            {
                Console.WriteLine("Directory not found.");
                return;
            }

            foreach (var file in Directory.EnumerateFiles(dir.Parent.FullName, "*.resources.dll"))
            {
                Console.WriteLine($"Signing {file} ...");
                Sign(file, args[0], locale);
            }
            Console.WriteLine("All Done!");
        }

        /// <summary>
        /// Faker!
        /// </summary>
        static void Sign(string path, string key, string locale)
        {
            var dll = AssemblyDef.Load(path);
            dll.HasPublicKey = true;
            dll.PublicKey = new PublicKey(key);
            dll.Write($"{locale}\\{dll.Name}.dll");
        }
    }
}
