//This source code is licensed under CC BY-NC-ND 4.0
//Author: Ulysses (wdwxy12345@gmail.com) from VOICeVIO

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using dnlib.DotNet;

namespace CeVIO.SFE.ResMerge
{
    static class Program
    {
        static List<string> SkipList = new List<string>{ "KnownWords", "SecretConfiguration", "Personality" };

        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: .exe <dll path> <resx path>");
                return;
            }

            try
            {
                var dic = ParseResDll(args[0]);
                AddResource(dic, args[1], args[1]);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }
        }
        
        public static Dictionary<string, object> ParseResDll(string path)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            var asm = ModuleDefMD.Load(path);
            var res = asm.Resources[0] as EmbeddedResource;
            ResourceReader rr = new ResourceReader(res.CreateReader().AsStream());
            var iter = rr.GetEnumerator();
            while (iter.MoveNext())
            {
                dic.Add(iter.Key.ToString(), iter.Value);
            }

            return dic;
        }
        
        public static void AddResource(Dictionary<string, object> res, string inputPath, string outputPath)
        {
            var dic = new Dictionary<string, ResXDataNode>();
            using (var reader = new ResXResourceReader(inputPath){UseResXDataNodes = true})
            {
                
                dic = reader.Cast<DictionaryEntry>().ToDictionary(e => e.Key.ToString(), e => e.Value as ResXDataNode);
                foreach (var kv in res)
                {
                    if (!dic.ContainsKey(kv.Key) && !SkipList.Contains(kv.Key))
                    {
                        dic.Add(kv.Key, new ResXDataNode(kv.Key, kv.Value){Comment = kv.Value.ToString()});
                    }
                }
            }

            using (var writer = new ResXResourceWriter(outputPath))
            {
                foreach (var kv in dic)
                {
                    writer.AddResource(kv.Key, kv.Value);
                }

                writer.Generate();
            }
        }
    }
}