﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace CeVIO.CN.Installer.Properties {
    using System;
    
    
    /// <summary>
    ///   一个强类型的资源类，用于查找本地化的字符串等。
    /// </summary>
    // 此类是由 StronglyTypedResourceBuilder
    // 类通过类似于 ResGen 或 Visual Studio 的工具自动生成的。
    // 若要添加或移除成员，请编辑 .ResX 文件，然后重新运行 ResGen
    // (以 /str 作为命令选项)，或重新生成 VS 项目。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   返回此类使用的缓存的 ResourceManager 实例。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("CeVIO.CN.Installer.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   重写当前线程的 CurrentUICulture 属性，对
        ///   使用此强类型资源类的所有资源查找执行重写。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   查找类似 &lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;
        ///&lt;VocalSources&gt;
        ///  &lt;!--
        ///    ▼备忘录▼ - Localized by CeVIO.CN - https://github.com/VOICeVIO/CeVIO.CN
        ///    Language 是音源自身所表现的语言。
        ///    Locale 是对应运行环境的显示语言。
        ///    → 日语以外的语言最好只设置语言代码（例：英语：&quot;en&quot;）
        ///      → 中文区分简体和繁体，因此用简体的 &quot;zh-Hans&quot; 和繁体的 &quot;zh-Hant&quot; 来代替通用的 &quot;zh&quot; （译注：实际操作中使用 &quot;zh-CN&quot; 来表示简体）
        ///
        ///    另外，英语是永远必须设置的。就用英语来做默认值吧。
        ///  --&gt;
        ///
        ///  &lt;!--
        ///    ■ ささらトーク
        ///  --&gt;
        ///  &lt;VocalSource Id=&quot;A&quot; ProductCode=&quot;01b54a3e-cb58-4418-a435-2bac87450350&quot;&gt;
        ///    &lt;DisplayName&gt;
        ///      &lt;Notation Locale=&quot;en&quot;&gt;Sat [字符串的其余部分被截断]&quot;; 的本地化字符串。
        /// </summary>
        internal static string VocalSourceSettings2 {
            get {
                return ResourceManager.GetString("VocalSourceSettings2", resourceCulture);
            }
        }
    }
}
