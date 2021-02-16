using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

namespace CeVIO.CN.VersionFix
{
    class Program
    {
        private const string Locale = "ja-JP";
        private const string ConfigFileName = "CeVIO AI.exe.config";

        static void Main(string[] args)
        {
            Console.WriteLine("CeVIO.CN 汉化版本适配工具");
            Console.WriteLine("by Ulysses, wdwxy12345@gmail.com");
            Console.WriteLine();
            Console.WriteLine("本工具用于配置任意版本的CeVIO AI加载固定版本的CeVIO汉化。");
            Console.WriteLine();
            var locale = Locale;
            var currentDir = Environment.CurrentDirectory;

            if (args.Length > 0 && Directory.Exists(args[0]))
            {
                currentDir = args[0];
            }

            if (args.Length > 1)
            {
                locale = args[1].Trim();
            }

            Console.WriteLine($"当前路径: {currentDir}");
            var dirInfo = new DirectoryInfo(currentDir);
            var configPath = Path.Combine(Path.GetDirectoryName(currentDir), ConfigFileName);
            if (!string.Equals(dirInfo.Name, locale, StringComparison.InvariantCultureIgnoreCase))
            {
                if (dirInfo.GetDirectories(locale).Length > 0)
                {
                    configPath = Path.Combine(currentDir, ConfigFileName);
                    currentDir = Path.Combine(currentDir, locale);
                }
                else
                {
                    Console.WriteLine($"路径不正确，需要放置到{locale}目录下运行。");
                    Console.ReadLine();
                    return;
                }
            }
            else
            {
                if (!File.Exists(configPath))
                {
                    Console.WriteLine("未能从上级目录找到CeVIO AI。");
                    Console.ReadLine();
                    return;
                }
            }

            Console.WriteLine("选择模式:\r\n 1.配置或更新汉化版本\r\n 2.取消配置\r\n 直接Enter: 等同于1");
            var input = Console.ReadLine()?.Trim();
            if (input == "1" || input == "")
            {
                Install(currentDir, configPath);
            }
            else if (input == "2")
            {
                Uninstall(configPath);
            }
            else
            {
                Console.WriteLine("输入不正确。");
            }

            Console.WriteLine("按任意键退出...");
            Console.ReadLine();
        }

        private static void Uninstall(string configPath)
        {
            var ms = new MemoryStream(File.ReadAllBytes(configPath));
            var xDoc = XDocument.Load(ms);
            var assemblyBinding = xDoc.Descendants().FirstOrDefault(x => x.Name.LocalName == "assemblyBinding");
            if (assemblyBinding == null)
            {
                Console.WriteLine("无需进行任何操作。");
                return;
            }

            assemblyBinding.Remove();
            Console.WriteLine("已删除汉化配置。");

            xDoc.Save(File.Open(configPath, FileMode.Create));
        }

        private static void Install(string dllPath, string configPath)
        {
            var ms = new MemoryStream(File.ReadAllBytes(configPath));
            var xDoc = XDocument.Load(ms);
            var xElement = CollectVersions(dllPath);
            var runtime = xDoc.Descendants().FirstOrDefault(x => x.Name.LocalName == "runtime");
            if (runtime == null)
            {
                var config = xDoc.Descendants().FirstOrDefault(x => x.Name.LocalName == "configuration");
                if (config == null)
                {
                    Console.WriteLine("不支持此版本。");
                    return;
                }

                runtime = new XElement(XName.Get("runtime", config.Name.NamespaceName));
                config.Add(runtime);
            }
            else
            {
                var assemblyBinding = runtime.Descendants().FirstOrDefault(x => x.Name.LocalName == "assemblyBinding");
                assemblyBinding?.Remove();;
            }

            runtime.Add(xElement);

            xDoc.Save(File.Open(configPath, FileMode.Create));
        }

        static XElement CollectVersions(string path, string oldVersion = "0.0.0.0-9.99.99.99")
        {
            XElement assemblyBinding = new XElement("assemblyBinding");

            foreach (var file in Directory.EnumerateFiles(path, "*.resources.dll", SearchOption.TopDirectoryOnly))
            {
                var asm = AssemblyName.GetAssemblyName(file);
                var asmName = asm.Name;
                var asmCulture = asm.CultureName;
                var asmPublicKeyToken = asm.GetPublicKeyToken().PrintInHex();
                var asmVer = asm.Version.ToString();

                if (asmName == "CeVIO AI.resources" || asmName == "CeVIO Creative Studio.resources")
                {
                    Console.WriteLine($"当前汉化版本：{asmVer}");
                }
                
                var bindingRedirect = new XElement("bindingRedirect", new XAttribute("oldVersion", oldVersion),
                    new XAttribute("newVersion", asmVer));
                var assemblyIdentity = new XElement("assemblyIdentity", new XAttribute("name", asmName),
                    new XAttribute("publicKeyToken", asmPublicKeyToken), new XAttribute("culture", asmCulture));
                var dependentAssembly = new XElement("dependentAssembly", assemblyIdentity, bindingRedirect);
                assemblyBinding.Add(dependentAssembly);
            }

            XNamespace ns = XNamespace.Get("urn:schemas-microsoft-com:asm.v1");
            assemblyBinding.EnsureNamespaceExists(ns);
            return assemblyBinding;
        }
    }

    static class Helper
    {
        public static XElement EnsureNamespaceExists(this XElement xElement, XNamespace xNamespace)
        {
            string nodeName = xElement.Name.LocalName;

            if (xElement.Attribute("xmlns") == null)
            {
                foreach (XElement tmpElement in xElement.DescendantsAndSelf())
                {
                    tmpElement.Name = xNamespace + tmpElement.Name.LocalName;
                }
                xElement = new XElement(xNamespace + nodeName, xElement.FirstNode);
            }

            return xElement;
        }

        /// <summary>
        /// Print byte array in Hex
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        internal static string PrintInHex(this byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var b in bytes)
            {
                var s = Convert.ToString(b, 16);
                if (s.Length == 1)
                {
                    s = "0" + s;
                }

                sb.Append(s);
            }

            return sb.ToString();
        }
    }
}
