# CeVIO.CN
[![Build status](https://ci.appveyor.com/api/projects/status/imr3qtv8rm5d5509/branch/master?svg=true)](https://ci.appveyor.com/project/UlyssesWu/cevio-cn/build/artifacts)

本项目将提供CeVIO的非官方中文汉化。

部分文本仍在汉化中，欢迎贡献。

## 使用方式

下载[稳定版本](https://github.com/VOICeVIO/CeVIO.CN/releases)或[测试版本](https://ci.appveyor.com/project/UlyssesWu/cevio-cn/build/artifacts)，将`zh-CN`文件夹置于CeVIO根目录即可。

## 许可协议

所有源代码以[CC BY-NC-ND 4.0](https://creativecommons.org/licenses/by-nc-nd/4.0/)协议提供。

翻译文本及编译结果（DLL）文件以[CC BY-NC-SA 4.0](https://github.com/VOICeVIO/CeVIO.CN/blob/master/CeVIO.SFE.Signer/CeVIO.CN.LICENSE.txt)协议提供。

## 贡献方式

本项目推荐使用[ResX Resource Manager](https://github.com/tom-englert/ResXResourceManager)（以下简称ResX）进行翻译。
请先在ResX的“配置”一栏中将“中立资源语言”设为“中文（简体，中国）”（zh-CN）。

### 入门版

你可以通过[issues](https://github.com/VOICeVIO/CeVIO.CN/issues)对翻译提出建议。

### 简易版

环境需求：[ResX Resource Manager](https://clickonce-tom-englert.azurewebsites.net/ResXResourceManager/ResXManager.application)独立版（clickonce）。

打开ResX，点击左上角【Directory:】按钮——Browse，选择项目中的某个*.resources目录（包含.csproj的目录）。随后进行翻译。

翻译完毕后，提交Pull Request，Review通过后将会直接合并到主分支并自动编译新版本。

（若不会使用git）或通过“导出所选”功能在issue中以附件提交你的翻译。

### 硬核版

环境需求：VS2017；[ResX Resource Manager](https://marketplace.visualstudio.com/items?itemName=TomEnglert.ResXManager)插件。

使用VS打开项目，打开ResX插件，对资源文件中的文本进行翻译。（无需进行编译）

修改完毕后，提交Pull Request，Review通过后将会直接合并到主分支并自动编译新版本。

## 来源

本汉化项目的部分中文文本继承自Project CeVIO SFE（已取得授权）。

---

by **VOICeVIO**

Contact: wdwxy12345@gmail.com
