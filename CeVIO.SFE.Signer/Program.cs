//This source code is licensed under CC BY-NC-ND 4.0
//Author: Ulysses (wdwxy12345@gmail.com) from VOICeVIO

using System;
using System.IO;
using CeVIO.SFE.Signer.Properties;
using dnlib.DotNet;
using dnlib.DotNet.Writer;
using dnlib.PE;

namespace CeVIO.SFE.Signer
{
    class Program
    {
        private static string[] _locales = new[] {"zh-CN", "ja-JP"};
        private static string _outputDir = "output";

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

            DirectoryInfo dir = Directory.CreateDirectory(_outputDir);

            foreach (var locale in _locales)
            {
                Directory.CreateDirectory(Path.Combine(_outputDir, locale));

                if (File.Exists("CeVIO.CN.LICENSE.txt"))
                {
                    File.Copy("CeVIO.CN.LICENSE.txt", $"{_outputDir}\\{locale}\\CeVIO.CN.LICENSE.txt", true);
                }

                foreach (var file in Directory.EnumerateFiles(locale, "*.resources.dll"))
                {
                    Version v = null;
                    try
                    {
                        string ver = Resources.ResourceManager.GetString(Path.GetFileName(file));
                        if (!string.IsNullOrEmpty(ver))
                        {
                            v = Version.Parse(ver);
                        }
                    }
                    catch (Exception)
                    {
                        // ignored
                    }

                    Console.WriteLine($"Signing {file} ...");
                    Sign(file, args[0], v, Path.Combine("output", locale));
                }
            }
            
            Console.WriteLine("All Done!");
        }

        /// <summary>
        /// Faker!
        /// </summary>
        static void Sign(string path, string key, Version v, string newPath = null)
        {
            var dll = AssemblyDef.Load(path);
            //If you want the dll to be platform: x64 instead of AnyCPU (x64 first), use this option. It shouldn't be used here.
            //var option = new ModuleWriterOptions(dll.ManifestModule) {PEHeadersOptions = {Machine = Machine.AMD64}};

            dll.HasPublicKey = true;
            dll.PublicKey = new PublicKey(key);

            if (v != null)
            {
                dll.Version = v;
            }

            if (newPath == null)
            {
                dll.Write(path);
                //dll.Write(path, option);
            }
            else
            {
                dll.Write(Path.Combine(newPath, $"{dll.Name}.dll"));
                //dll.Write(Path.Combine(newPath, $"{dll.Name}.dll"), option);
            }

        }
    }
}
