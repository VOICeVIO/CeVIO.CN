using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Xml.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ModSatellite
{
    class Handler
    {
        private static bool Inited = false;


        private static async Task Init()
        {
            if (Inited)
            {
                return;
            }
            
            Inited = true;
        }

        //[HarmonyPostfix]
        /// <summary>
        /// get_DisplayLanguages
        /// </summary>
        /// <param name="__result"></param>
        private static void _get_DisplayLanguages(ref IEnumerable __result)
        {
            __result = new[]
            {
                new {Title = "简体中文", Value = "zh-CN"},
                new {Title = "日本語", Value = "ja-JP"},
                new { Title = "English", Value = "en-US" },
            };
        }

        //[HarmonyPrefix]
        /// <summary>
        /// InternalGetSatelliteAssembly
        /// </summary>
        /// <param name="__result"></param>
        /// <param name="name"></param>
        /// <param name="culture"></param>
        /// <param name="version"></param>
        /// <param name="throwOnFileNotFound"></param>
        /// <param name="stackMark"></param>
        private static void _InternalGetSatelliteAssembly(ref object __result, string name, CultureInfo culture, ref Version version, bool throwOnFileNotFound, object stackMark)
        {
            if (Equals(culture, CultureInfo.GetCultureInfo("zh-CN")))
            {
                var mainPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var cnPath = Path.Combine(mainPath, "zh-CN", name + ".dll");
                if (File.Exists(cnPath))
                {
                    try
                    {
                        var asmName = AssemblyName.GetAssemblyName(cnPath);
                        version = asmName.Version;
                    }
                    catch (Exception)
                    {
                        return;
                    }

                }
            }
        }
        
        //[HarmonyPrefix]
        /// <summary>
        /// GetAdditionalVocalSourceSettingsMaster
        /// </summary>
        /// <param name="__result"></param>
        /// <returns></returns>
        private static bool _GetAdditionalVocalSourceSettingsMaster(ref XElement __result)
        {
            try
            {
                HttpClient h = new HttpClient();
                h.Timeout = TimeSpan.FromSeconds(10);
                var t = h.GetStringAsync("https://raw.githubusercontent.com/VOICeVIO/CeVIO.CN/master/CeVIO.CN.Installer/Resources/VocalSourceSettings2.xml");
                t.Wait();
                var result = t.Result;

                //WebClient client = new WebClient() { Encoding = Encoding.UTF8 };
                //var result = client.DownloadString("https://raw.githubusercontent.com/VOICeVIO/CeVIO.CN/master/CeVIO.CN.Installer/Resources/VocalSourceSettings2.xml");
                __result = XElement.Parse(result);
            }
            catch (Exception)
            {
                __result = null;
            }

            return false;
        }
    }
}
