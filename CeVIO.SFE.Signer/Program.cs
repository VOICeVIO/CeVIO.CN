using System;
using System.IO;
using dnlib.DotNet;

namespace CeVIO.SFE.Signer
{
    class Program
    {
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

            var dir = Directory.CreateDirectory("zh-CN");

            if (File.Exists("CeVIO.CN.LICENSE.txt"))
            {
                File.Copy("CeVIO.CN.LICENSE.txt", "zh-CN\\CeVIO.CN.LICENSE.txt", true);
            }

            foreach (var file in Directory.EnumerateFiles(dir.Parent.FullName, "*.resources.dll"))
            {
                Console.WriteLine($"Signing {file} ...");
                Sign(file, args[0]);
            }
            Console.WriteLine("All Done!");
        }

        /// <summary>
        /// Faker!
        /// </summary>
        static void Sign(string path, string key)
        {
            var dll = AssemblyDef.Load(path);
            dll.HasPublicKey = true;
            dll.PublicKey = new PublicKey(key);
            dll.Write($"zh-CN\\{dll.Name}.dll");
        }
    }
}
