# CeVIO.CN
[![Build status](https://ci.appveyor.com/api/projects/status/imr3qtv8rm5d5509/branch/master?svg=true)](https://ci.appveyor.com/project/UlyssesWu/cevio-cn/build/artifacts)

本项目将提供CeVIO的非官方简体中文汉化。

[查看当前对应版本](https://github.com/VOICeVIO/CeVIO.CN/blob/master/CeVIO.SFE.Signer/Properties/Resources.resx#L139)

“对应版本”是当前自动编译（CI）所对应的版本。旧版请查看release页面或CI编译历史。若要协助支持新版，请提交修改版本号至最新版本的PR。

## 使用方式

目前，CeVIO.CN分为3个部分：zh-CN汉化DLL、ja-JP汉化DLL、安装器，并有2种安装方式，可按需选用其中1种。

### 安装方式1：跨版本兼容（推荐）
跨版本兼容补丁安装后，升级主程序通常无需重新安装补丁，因此推荐使用该版本。

1. 从[发布页面](https://github.com/VOICeVIO/CeVIO.CN/releases)获取【安装器】（Installer），从[发布页面](https://github.com/VOICeVIO/CeVIO.CN/releases)或[CI页面](https://ci.appveyor.com/project/UlyssesWu/cevio-cn/build/artifacts)获取最新的【zh-CN汉化DLL】。
2. 将所有文件及文件夹复制到CeVIO AI的安装目录。
3. 运行CeVIO.CN.Installer.exe，按提示安装补丁。

若打开软件后为英语或日语，请到选项窗口将语言改为简体中文。

如果主程序升级后，多出了部分界面文本（将显示为英文），则可以到补丁发布地址下载最新的【zh-CN汉化DLL】，并替换所有文件到CeVIO AI的安装目录。

如果主程序升级后，补丁完全失效（所有界面文本显示为英文），则需要获取最新的【安装器】和【zh-CN汉化DLL】，重新安装。

### 安装方式2：替换日语
替换日语版补丁不能跨版本兼容，升级主程序后必须下载对应版本的补丁。若版本不对应，汉化无法加载。

从[CI页面](https://ci.appveyor.com/project/UlyssesWu/cevio-cn/build/artifacts)下载对应版本的【ja-JP汉化DLL】。

将`ja-JP`文件夹复制到CeVIO根目录替换现有文件即可。

若打开软件后为英语，请到选项窗口将语言改为Japanese。

如果release页面中的版本较旧，请到[CI页面](https://ci.appveyor.com/project/UlyssesWu/cevio-cn/history)按照版本号寻找对应的自动编译版本下载使用。

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

## 致谢

感谢 [uselessbug](https://github.com/uselessbug) 协助测试并反馈BUG。

感谢 [夜轮_NachtgeistW](https://github.com/NachtgeistW) 对CeVIO相关文档进行中文翻译：[CeVIO AI用户指南（非官方译文）](https://cevio-user-guide-unofficial.github.io/CeVIO-AI/)

## 挂人

在感谢CeVIO开发团队的同时，我们仍需对CeVIO开发团队中出现的不和谐音表示抗议。我们深知个人行为并不能代表团队的态度，因此我们不会上纲上线去抵制CeVIO。

但是，我们仍希望大家看清这位简介为“开发者（CeVIO AI / CS）”的 **川出阳一** 先生的真面目。


<a href="https://twitter.com/kawade_yoichi/status/1368777076539465728"><img src="https://raw.githubusercontent.com/VOICeVIO/CeVIO.CN/master/img/kawade_yoichi.png" height="650"  alt="Kawade Yoichi"/></a>

---

by **VOICeVIO**

Contact: wdwxy12345@gmail.com
