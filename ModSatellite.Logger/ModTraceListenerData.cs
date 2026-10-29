using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ObjectBuilder;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;

namespace ModSatellite.Logger
{
    [Assembler(typeof(ModLogAssembler))]
    class ModTraceListenerData : TraceListenerData
    {
        private static bool _loaded = false;
        private static Harmony _harmony;
        private static Assembly SongAssembly = null;

        static ModTraceListenerData()
        {
            if (_loaded)
            {
                return;
            }

            if (Check())
            {
                try
                {
                    Inject();
                }
                catch (Exception e)
                {
                }
            }
        }

        static bool Check()
        {
            var zhCnPath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "zh-CN");
            if (!Directory.Exists(zhCnPath))
            {
                zhCnPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            }

            var p = Path.Combine(zhCnPath, "CeVIO.SongEditorControl.resources.dll");
            if (!File.Exists(p))
            {
                return false;
            }

            return true;
        }
        
        static void Inject()
        {
            if (_harmony == null)
            {
                _harmony = new Harmony("VOICeVIO");
            }

            var ApplicationSettingCircuitType = Dynamitey.DynamicObjects.LateType.FindType("CeVIO.Editor.Adapters.ApplicationSettingCircuit", Assembly.GetEntryAssembly());
            var get_DisplayLanguages = ApplicationSettingCircuitType.GetMethod("get_DisplayLanguages", BindingFlags.Instance | BindingFlags.Public);
            var newMethod = _harmony.Patch(get_DisplayLanguages, postfix: new HarmonyMethod(typeof(Handler).GetMethod("_get_DisplayLanguages", BindingFlags.Static | BindingFlags.NonPublic)));

            System.Windows.Application.Current.Dispatcher.Thread.CurrentUICulture = CultureInfo.GetCultureInfo("zh-CN");

            //Set recognizableCultures
            var UtilityType = Dynamitey.DynamicObjects.LateType.FindType("CeVIO.Editor.Utilities.Utility", Assembly.GetEntryAssembly());
            if (UtilityType != null)
            {
                var cultures = new[]
                {
                    CultureInfo.GetCultureInfo("zh-CN"),
                    CultureInfo.GetCultureInfo("ja-JP"),
                    CultureInfo.GetCultureInfo("en-US")
                };

                var field = UtilityType.GetField("recognizableCultures", BindingFlags.Static | BindingFlags.NonPublic);
                if (field != null)
                {
                    var val = field.GetValue(null);
                    field.SetValue(null, cultures);
                    val = field.GetValue(null);
                }
                //UtilityType.recognizableCultures = cultures;
            }

            var RuntimeAssemblyType = Dynamitey.DynamicObjects.LateType.FindType("System.Reflection.RuntimeAssembly", Assembly.GetAssembly(typeof(Assembly)));
            var methods = RuntimeAssemblyType.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic);
            var InternalGetSatelliteAssemblyMethod = methods.First(m => m.Name == "InternalGetSatelliteAssembly" && m.GetParameters().Length == 5);
            var newMethod2 = _harmony.Patch(InternalGetSatelliteAssemblyMethod, prefix: new HarmonyMethod(typeof(Handler).GetMethod("_InternalGetSatelliteAssembly", BindingFlags.Static | BindingFlags.NonPublic)));
            
            var ProductLibraryType = Dynamitey.DynamicObjects.LateType.FindType("CeVIO.Editor.MissionAssistant.ProductLibrary", Assembly.GetEntryAssembly());
            if (ProductLibraryType != null)
            {
                var GetAdditionalVocalSourceSettingsMaster = ProductLibraryType.GetMethod("GetAdditionalVocalSourceSettingsMaster", BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                var newMethod4 = _harmony.Patch(GetAdditionalVocalSourceSettingsMaster, prefix: new HarmonyMethod(typeof(Handler).GetMethod("_GetAdditionalVocalSourceSettingsMaster", BindingFlags.Static | BindingFlags.NonPublic)));
            }
            
            _loaded = true;
        }
    }
}
