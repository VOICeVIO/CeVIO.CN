using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using CeVIO.CN.Installer.Properties;

namespace CeVIO.CN.Installer
{
    enum InstallState
    {
        FailedToDetect,
        NotInstalled_ResourceDll,
        NotInstalled_LoaderDll,
        NotInstalled_Config,
        Installed
    }

    class Program
    {
        private const string ConfigFileName = "CeVIO AI.exe.config";
        public static string AppRoamingFolderPath
        {
            get
            {
                string path = "CeVIO AI (x64)";
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CeVIO", path);
            }
        }

        public static string ApplicationFolderPath
        {
            get
            {
                string packageName = "CeVIO AI (x64)";
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CeVIO", packageName);
            }
        }


        static void Main(string[] args)
        {
            Console.WriteLine("CeVIO.CN 安装器V2");
            Console.WriteLine("by Ulysses, wdwxy12345@gmail.com");
            Console.WriteLine();
            Console.WriteLine("本工具用于安装CeVIO AI汉化补丁（8.1.16.0以上版本）。");
            Console.WriteLine();
            var currentDir = Environment.CurrentDirectory;

            if (args.Length > 0 && Directory.Exists(args[0]))
            {
                currentDir = args[0];
            }

            //currentDir = @"D:\Program Files\CeVIO\CeVIO AI";

            Console.WriteLine($"[*] 当前路径: {currentDir}");
            var configPath = Path.Combine(currentDir, ConfigFileName);
            if (!File.Exists(configPath))
            {
                Console.WriteLine("[!] 未找到CeVIO AI。请将本程序放到CeVIO AI所在的目录中。");
                Console.ReadLine();
                return;
            }

            var state = GetCurrentState(configPath, out var version);
            string stateText = "";
            bool installed = false;
            if (state == InstallState.Installed)
            {
                stateText = $"已安装汉化v{version}";
                installed = true;
            }

            if (state == InstallState.NotInstalled_ResourceDll)
            {
                Console.WriteLine("[!] 未检测到zh-CN文件夹，汉化将无法正常工作。");
                stateText = "汉化补丁缺失，只能卸载汉化";
                installed = true;
            }

            if (state == InstallState.NotInstalled_Config)
            {
                stateText = $"可安装汉化v{version}";
            }

            if (state == InstallState.NotInstalled_LoaderDll)
            {
                Console.WriteLine("[!] 汉化补丁DLL缺失，请重新下载补丁。");
                stateText = "汉化补丁缺失，只能卸载汉化";
                installed = true;
            }

            Console.WriteLine($"[*] 当前状态：{stateText}");

            Console.WriteLine();
            if (installed)
            {
                Console.WriteLine("选择模式:\r\n 1.卸载汉化\r\n 2.访问汉化下载地址\r\n 3.设置CeVIO使用简体中文\r\n 直接Enter: 退出");
            }
            else
            {
                Console.WriteLine("选择模式:\r\n 1.安装汉化\r\n 2.访问汉化下载地址\r\n 3.设置CeVIO使用简体中文\r\n 直接Enter: 等同于1");
            }

            var input = Console.ReadLine()?.Trim();
            if (input == "1")
            {
                if (installed)
                {
                    V2Uninstall(configPath);
                }
                else
                {
                    V2Install(configPath);
                    V2InstallXml(configPath);
                }
            }
            else if (input == "2")
            {
                Process.Start("https://github.com/VOICeVIO/CeVIO.CN/releases");
            }
            else if (input == "3")
            {
                V2InstallXml(configPath);
            }
            else if (input == "")
            {
                if (!installed)
                {
                    V2Install(configPath);
                }
            }
            else
            {
                Console.WriteLine("输入不正确。");
            }

            Console.WriteLine("按任意键退出...");
            Console.ReadLine();
        }

        private static InstallState GetCurrentState(string configPath, out string version)
        {
            version = "";
            var mainResourcePath = Path.Combine(Path.GetDirectoryName(configPath), "zh-CN", "CeVIO AI.resources.dll");
            if (File.Exists(mainResourcePath))
            {
                var asmName = AssemblyName.GetAssemblyName(mainResourcePath);
                version = asmName.Version.ToString();
            }
            else
            {
                return InstallState.NotInstalled_ResourceDll;
            }

            var loaderDllPath = Path.Combine(Path.GetDirectoryName(configPath), "ModSatellite.Logger.dll");
            if (!File.Exists(loaderDllPath))
            {
                return InstallState.NotInstalled_LoaderDll;
            }

            if (!File.Exists(configPath))
            {
                return InstallState.NotInstalled_Config;
            }

            using var ms = new MemoryStream(File.ReadAllBytes(configPath));
            var xDoc = XDocument.Load(ms);
            var loggingConfiguration = xDoc.Descendants().FirstOrDefault(x => x.Name.LocalName == "loggingConfiguration");
            if (loggingConfiguration != null)
            {
                var listeners = loggingConfiguration.Element("listeners");
                var categorySources = loggingConfiguration.Element("categorySources");
                if (listeners != null && categorySources != null)
                {
                    var check1 = listeners.Elements("add").Any(x => x.Attribute("name") is { Value: "CeVIO.CN" });
                    var check2 = categorySources.Descendants().Any(x => x.Attribute("name") is { Value: "CeVIO.CN" });
                    if (check1 && check2)
                    {
                        return InstallState.Installed;
                    }
                }
            }

            return InstallState.NotInstalled_Config;
        }
        
        private static void V2Uninstall(string configPath)
        {
            if (!File.Exists(configPath))
            {
                return;
            }

            using var ms = new MemoryStream(File.ReadAllBytes(configPath));
            var xDoc = XDocument.Load(ms);
            var loggingConfiguration = xDoc.Descendants().FirstOrDefault(x => x.Name.LocalName == "loggingConfiguration");
            if (loggingConfiguration != null)
            {
                var listeners = loggingConfiguration.Element("listeners");
                var categorySources = loggingConfiguration.Element("categorySources");
                if (listeners != null && categorySources != null)
                {
                    var check1 = listeners.Elements("add").FirstOrDefault(x => x.Attribute("name") is { Value: "CeVIO.CN" });
                    var check2 = categorySources.Descendants().FirstOrDefault(x => x.Attribute("name") is { Value: "CeVIO.CN" });
                    check1?.Remove();
                    check2?.Remove();
                }
            }

            xDoc.Save(File.Open(configPath, FileMode.Create));
            Console.WriteLine("已删除汉化配置。");
        }

        private static void V2InstallXml(string configPath)
        {
            var runtimeSettings = Path.Combine(ApplicationFolderPath, "RuntimeSettings.xml");
            if (File.Exists(runtimeSettings))
            {
                using var ms = new MemoryStream(File.ReadAllBytes(runtimeSettings));
                var xDoc = XDocument.Load(ms);
                var displayLanguage = xDoc.Descendants("DisplayLanguage").FirstOrDefault();
                if (displayLanguage != null)
                {
                    displayLanguage.Value = "zh-CN";
                    xDoc.Save(File.Open(runtimeSettings, FileMode.Create));
                    Console.WriteLine("已设置CeVIO AI使用简体中文作为界面语言。");
                }
                else
                {
                    Console.WriteLine("RuntimeSettings格式不正确。");
                }
            }
            else
            {
                Console.WriteLine("未找到RuntimeSettings，CeVIO AI可能没有正确安装。");
            }

            //Apply vocal source setting
            var vocalSettingPath = Path.Combine(AppRoamingFolderPath,
                BitConverter.ToString(new Guid("05A60A94-101E-43AB-93F3-0A7A17BD8629").ToByteArray()).Replace("-", string.Empty)
                    .ToLower());
            
            try
            {
                if (File.Exists(vocalSettingPath))
                {
                    File.SetAttributes(vocalSettingPath, FileAttributes.Normal);
                    File.Copy(vocalSettingPath, Path.GetFileName(vocalSettingPath) + ".bak", true);
                }

                File.WriteAllText(vocalSettingPath, Resources.VocalSourceSettings2);
                Console.WriteLine("已汉化角色名XML。");
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine("未能汉化角色名XML，权限不足。");
                Console.WriteLine(e);
            }
        }

        private static void V2Install(string configPath)
        {
            using var ms = new MemoryStream(File.ReadAllBytes(configPath));
            var xDoc = XDocument.Load(ms);
            var loggingConfiguration = xDoc.Descendants().FirstOrDefault(x => x.Name.LocalName == "loggingConfiguration");
            if (loggingConfiguration != null)
            {
                var listeners = loggingConfiguration.Element("listeners");
                var categorySources = loggingConfiguration.Element("categorySources");
                if (listeners != null && categorySources != null)
                {
                    var modListener = new XElement("add", new XAttribute("name", "CeVIO.CN"),
                        new XAttribute("type",
                            "ModSatellite.Logger.ModLoader, ModSatellite.Logger, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"),
                        new XAttribute("listenerDataType",
                            "ModSatellite.Logger.ModTraceListenerData, ModSatellite.Logger, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"),
                        new XAttribute("filter", "All"));
                    listeners.Add(modListener);

                    foreach (var add in categorySources.Elements("add"))
                    {
                        if (add.Attribute("name") is { Value: "General" })
                        {
                            var general = add.Element("listeners");
                            if (general == null)
                            {
                                general = new XElement("listeners");
                                add.Add(general);
                            }
                            general.Add(new XElement("add", new XAttribute("name", "CeVIO.CN")));
                        }
                    }
                }
            }

            xDoc.Save(File.Open(configPath, FileMode.Create));
            Console.WriteLine("已配置界面文本汉化。");
        }

        private static void Uninstall(string configPath)
        {
            using var ms = new MemoryStream(File.ReadAllBytes(configPath));
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
            using var ms = new MemoryStream(File.ReadAllBytes(configPath));
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
                assemblyBinding?.Remove();
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
