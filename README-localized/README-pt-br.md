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
# Explorador de API REST do Excel

Este exemplo é um aplicativo da [Plataforma Universal do Windows](http://dev.windows.com)
que demonstra como usar a [API REST do Excel](https://github.com/OfficeDev/microsoft-graph-docs/tree/beta) para acessar as pastas de trabalho do Excel armazenadas no OneDrive for Business e no SharePoint Online usando o Office Graph de um aplicativo nativo. A API REST do Excel foi lançada em versão beta e o exemplo mostra apenas como fazer um subconjunto menor das solicitações que podem ser feitas usando a API REST do Excel.
Confira a documentação para ver o que mais é possível fazer com a API.

## Pré-requisitos ##

Esse exemplo requer o seguinte:  

  * Visual Studio de 2015 com Atualização 1
  * Windows 10 ([habilitado para o modo de desenvolvimento](https://msdn.microsoft.com/library/windows/apps/xaml/dn706236.aspx))
  * Uma conta do Office 365 para empresas. Inscreva-se para uma [Assinatura de Desenvolvedor do Office 365](https://msdn.microsoft.com/en-us/office/office365/howto/setup-development-environment), que inclui os recursos necessários para começar a criação de aplicativos do Office 365.

## Introdução ##

Siga estas etapas para executar o aplicativo de exemplo:

  * Abra o arquivo de solução ExcelRESTExplorer. sln na pasta src.
  * Pressione F5 para criar e iniciar a depuração
  * Entre com uma conta do Office 365 Business.
  * Clique em Carregar arquivo para carregar um arquivo de exemplo para o OneDrive do usuário conectado ou clique em Obter metadados do item para recuperar a ID de um arquivo existente no OneDrive do usuário 
  
## Envie-nos os seus comentários

Seus comentários são importantes para nós.  

Confira o código de amostra e fale conosco caso tenha alguma dúvida ou problema para encontrá-los [enviando um problema](https://github.com/OfficeDev/Microsoft-Graph-UWP-Excel-REST-API-Explorer/issues) diretamente nesse repositório. Forneça etapas de reprodução, saída do console e mensagens de erro em qualquer edição que você abrir.

## Direitos autorais

Copyright © 2016 Microsoft. Todos os direitos reservados.
  
Este projeto adotou o [Código de Conduta de Código Aberto da Microsoft](https://opensource.microsoft.com/codeofconduct/).  Para saber mais, confira as [Perguntas frequentes sobre o Código de Conduta](https://opensource.microsoft.com/codeofconduct/faq/) ou entre em contato pelo [opencode@microsoft.com](mailto:opencode@microsoft.com) se tiver outras dúvidas ou comentários.
