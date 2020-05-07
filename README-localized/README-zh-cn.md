---
page_type: sample 
products:
- office-excel
- ms-graph
languages:
- csharp
- windows-uwp
extensions:
  contentType: samples
  technologies:
  - Microsoft Graph
  services:
  - Excel
  createdDate: 3/23/2016 12:31:30 PM
---
# Excel REST API 资源管理器

本示例是一个[通用 Windows 平台](http://dev.windows.com)应用程序，演示如何通过来自本机应用程序的 Office Graph，
使用 [Excel REST API](https://github.com/OfficeDev/microsoft-graph-docs/tree/beta) 访问存储在 OneDrive for Business 和 SharePoint Online 中的 Excel 工作簿。
请注意，Excel REST API 在 beta 版本中发布，且本示例仅显示如何使用 Excel REST API 来创建一个更小的请求子集。查看文档以了解可通过 API 执行的其他操作。

## 先决条件 ##

此示例要求如下：  

  * Visual Studio 2015 （含 Update 1）
  * Windows 10（[已启用开发模式](https://msdn.microsoft.com/library/windows/apps/xaml/dn706236.aspx)）
  * Office 365 商业版帐户。你可以注册 [Office 365 开发人员订阅](https://msdn.microsoft.com/en-us/office/office365/howto/setup-development-environment)，其中包含你开始构建 Office 365 应用所需的资源。

## 入门 ##

完成以下步骤运行该示例：

  * 打开 src 文件夹中的 ExcelRESTExplorer 解决方案文件。
  * 按下 F5 创建并开始调试
  * 使用 Office 365 商业版帐户登录。
  * 单击“上传文件”将示例文件上载到已登录用户的 OneDrive 或单击“获取项元数据”以检索用户 OneDrive 中现有文件的 id 
  
## 提供反馈

您的反馈对我们意义重大。  

查看示例代码并在此存储库中直接[提交问题](https://github.com/OfficeDev/Microsoft-Graph-UWP-Excel-REST-API-Explorer/issues)，告诉我们发现的任何疑问和问题。在任何打开话题中提供存储库步骤、控制台输出、错误消息。

## 版权信息

版权所有 (c) 2016 Microsoft。保留所有权利。
  
此项目已采用 [Microsoft 开放源代码行为准则](https://opensource.microsoft.com/codeofconduct/)。有关详细信息，请参阅[行为准则常见问题解答](https://opensource.microsoft.com/codeofconduct/faq/)。如有其他任何问题或意见，也可联系 [opencode@microsoft.com](mailto:opencode@microsoft.com)。
