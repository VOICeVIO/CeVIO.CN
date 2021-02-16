# CeVIO.CN
[![Build status](https://ci.appveyor.com/api/projects/status/imr3qtv8rm5d5509/branch/master?svg=true)](https://ci.appveyor.com/project/UlyssesWu/cevio-cn/build/artifacts)

本项目将提供CeVIO的非官方简体中文汉化。

[查看当前对应版本](https://github.com/VOICeVIO/CeVIO.CN/blob/master/CeVIO.SFE.Signer/Properties/Resources.resx#L139)

“对应版本”是当前自动编译（CI）所对应的版本。旧版请查看release页面或CI编译历史。若要协助支持新版，请提交修改版本号至最新版本的PR。

## 使用方式

下载[稳定版本](https://github.com/VOICeVIO/CeVIO.CN/releases)或[自动编译版本](https://ci.appveyor.com/project/UlyssesWu/cevio-cn/build/artifacts)，将`ja-JP`（或`zh-CN`）文件夹置于CeVIO根目录即可。

若打开软件后为英语，请到选项窗口将语言改为Japanese或Chinese（若有）。

如果release页面中的版本较旧，请到[CI页面](https://ci.appveyor.com/project/UlyssesWu/cevio-cn/history)按照版本号寻找对应的自动编译版本下载使用。

如果你的CeVIO版本与汉化的版本不一致，你可以使用汉化中自带的“汉化版本适配工具”(`CeVIO.CN.VersionFix`)来强制CeVIO使用当前的汉化版本（仅限CeVIO AI，且更新版本后需要重新运行工具）。

## 许可协议

所有源代码以[CC BY-NC-ND 4.0](https://creativecommons.org/licenses/by-nc-nd/4.0/)协议提供。

翻译文本及编译结果（DLL）文件以[CC BY-NC-SA 4.0](https://github.com/VOICeVIO/CeVIO.CN/blob/master/CeVIO.SFE.Signer/CeVIO.CN.LICENSE.txt)协议提供。（使用前必读）

## 贡献方式

你可以通过[issues](https://github.com/VOICeVIO/CeVIO.CN/issues)对翻译提出建议。

本项目推荐使用[ResX Resource Manager](https://github.com/tom-englert/ResXResourceManager)（以下简称ResX）进行翻译。
请先在ResX的“配置”一栏中将“中立资源语言”设为“中文（简体，中国）”（zh-CN）。

下载[ResX Resource Manager](https://clickonce-tom-englert.azurewebsites.net/ResXResourceManager/ResXManager.application)独立版（clickonce）

打开ResX，点击左上角【Directory:】按钮——Browse，选择项目目录，随后进行翻译。

翻译完毕后，提交Pull Request，Review通过后将会直接合并到主分支并自动编译新版本。

（若不会使用git）也可通过“导出所选”功能在issue中以附件提交你的翻译。

## 来源

本汉化项目的部分中文文本继承自Project CeVIO SFE（已取得授权）。

---

by **VOICeVIO**

Contact: wdwxy12345@gmail.com
