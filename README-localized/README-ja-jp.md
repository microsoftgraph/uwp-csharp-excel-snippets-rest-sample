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
# Excel REST API エクスプローラー

このサンプルは [Universal Windows Platform](http://dev.windows.com) アプリケーションです。
これは、[Excel REST API](https://github.com/OfficeDev/microsoft-graph-docs/tree/beta) を使用して、ネイティブ アプリケーションから Office Graph を使用し、OneDrive for Business や SharePoint Online に保存されている Excel ブックにアクセスする方法を示します。
Excel REST API はベータ版のリリースであり、サンプルが示しているのは Excel REST API を使用して行える要求の一部であることにご注意ください。この API で行える他のことについては、ドキュメントを参照してください。

## 前提条件 ##

このサンプルを実行するには次のものが必要です。  

  * 更新プログラム 1 が適用された Visual Studio 2015
  * Windows 10 ([開発モードが有効](https://msdn.microsoft.com/library/windows/apps/xaml/dn706236.aspx))
  * ビジネス向けの Office 365 アカウント。Office 365 アプリのビルドを開始するために必要なリソースを含む [Office 365 Developer サブスクリプション](https://msdn.microsoft.com/en-us/office/office365/howto/setup-development-environment)にサインアップできます。

## はじめに ##

次の手順に従ってサンプルを実行します。

  * src フォルダー内の ExcelRESTExplorer.sln ソリューション ファイルを開きます。
  * F5 キーを押してデバッグをビルドし、開始する
  * Office 365 Business アカウントでサインインします。
  * [ファイルのアップロード] をクリックしてサンプル ファイルをサインインしたユーザーの OneDrive にアップロードするか、または [アイテム メタデータを取得する] をクリックしてユーザーの OneDrive 上の既存のファイルの ID を取得する 
  
## フィードバックをお寄せください

お客様からのフィードバックを重視しています。  

サンプル コードを確認していだだき、質問や問題があれば、直接このリポジトリに[問題を送信](https://github.com/OfficeDev/Microsoft-Graph-UWP-Excel-REST-API-Explorer/issues)してお知らせください。発生したエラーの再現手順、コンソール出力、およびエラー メッセージをご提供ください。

## 著作権

Copyright (c) 2016 Microsoft.All rights reserved.
  
このプロジェクトでは、[Microsoft オープン ソース倫理規定](https://opensource.microsoft.com/codeofconduct/) が採用されています。詳細については、「[Code of Conduct の FAQ (倫理規定の FAQ)](https://opensource.microsoft.com/codeofconduct/faq/)」を参照してください。また、その他の質問やコメントがあれば、[opencode@microsoft.com](mailto:opencode@microsoft.com) までお問い合わせください。
