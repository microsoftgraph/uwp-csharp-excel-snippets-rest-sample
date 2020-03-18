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
# Explorador de la API de REST de Excel

Este ejemplo es una aplicación de la [Plataforma universal de Windows](http://dev.windows.com)
que muestra cómo usar la [API de REST de Excel](https://github.com/OfficeDev/microsoft-graph-docs/tree/beta) para obtener acceso a libros de Excel guardados en OneDrive para la Empresa y SharePoint Online mediante Office Graph desde una aplicación nativa. Tenga en cuenta que la API de REST de Excel está publicada en versión beta y el ejemplo solo muestra cómo crear un subconjunto más pequeño de las solicitudes que se pueden realizar con la API de REST de Excel.
Vea la documentación para conocer otras acciones que puede realizar con la API.

## Requisitos previos ##

Este ejemplo necesita lo siguiente:  

  * Visual Studio 2015 con Actualización 1
  * Windows 10 ([modo de desarrollo habilitado](https://msdn.microsoft.com/library/windows/apps/xaml/dn706236.aspx))
  * Una cuenta de Office 365 para empresas. Puede registrarse para obtener [una suscripción a Office 365 Developer](https://msdn.microsoft.com/en-us/office/office365/howto/setup-development-environment) que incluye los recursos que necesita para comenzar a crear aplicaciones de Office 365.

## Introducción ##

Complete estos pasos para ejecutar el ejemplo:

  * Abra el archivo de solución ExcelRESTExplorer.sln en la carpeta src.
  * Presione F5 para Crear e Iniciar la depuración
  * Inicie sesión con una cuenta de Office 365 Empresa.
  * Haga clic en Cargar archivo para cargar un archivo de ejemplo en el OneDrive del usuario que ha iniciado sesión o en Obtener metadatos del elemento para recuperar el Id. de un archivo existente en el OneDrive del usuario. 
  
## Envíenos sus comentarios

Su opinión es importante para nosotros.  

Revise el código de ejemplo y háganos saber todas las preguntas y las dificultades que encuentre [enviando un problema](https://github.com/OfficeDev/Microsoft-Graph-UWP-Excel-REST-API-Explorer/issues) directamente en este repositorio. Incluya los pasos de reproducción, las salidas de la consola y los mensajes de error en cualquier problema que abra.

## Derechos de autor

Copyright (c) 2016 Microsoft. Todos los derechos reservados.
  
Este proyecto ha adoptado el [Código de conducta de código abierto de Microsoft](https://opensource.microsoft.com/codeofconduct/). Para obtener más información, vea [Preguntas frecuentes sobre el código de conducta](https://opensource.microsoft.com/codeofconduct/faq/) o póngase en contacto con [opencode@microsoft.com](mailto:opencode@microsoft.com) si tiene otras preguntas o comentarios.
