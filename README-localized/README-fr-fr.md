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
# Explorateur de l’API REST Excel

Cet exemple est une application de [plateforme Windows universelle](http://dev.windows.com)
qui démontre comment utiliser l’[API REST Excel](https://github.com/OfficeDev/microsoft-graph-docs/tree/beta) pour accéder aux classeurs Excel stockés dans OneDrive Entreprise et SharePoint Online à l’aide d’Office Graph à partir d’une application native.
Veuillez noter que l’API REST Excel est publiée dans la version bêta et que l’exemple décrit uniquement comment créer un sous-ensemble de requêtes plus petit qui peuvent être effectuées avec l’API REST Excel. Consultez la documentation pour connaître les autres tâches que vous pouvez effectuer avec l’API.

## Conditions préalables ##

Cet exemple nécessite les éléments suivants :  

  * Visual Studio 2015 avec la mise à jour 1
  * Windows 10 ([avec mode de développement](https://msdn.microsoft.com/library/windows/apps/xaml/dn706236.aspx))
  * Un compte professionnel Office 365. Vous pouvez vous inscrire à [Office 365 Developer](https://msdn.microsoft.com/en-us/office/office365/howto/setup-development-environment) pour accéder aux ressources dont vous avez besoin pour commencer à créer des applications Office 365.

## Prise en main ##

Pour exécuter l’exemple, procédez comme suit :

  * Ouvrez le fichier solution ExcelRESTExplorer.sln dans le dossier src.
  * Appuyer sur F5 pour générer et démarrer le débogage
  * Connectez-vous à l’aide d’un compte Office 365 Business.
  * Cliquez sur Télécharger un fichier pour télécharger un exemple de fichier dans l’espace OneDrive de l’utilisateur connecté, ou cliquez sur obtenir les métadonnées d’élément pour récupérer l’ID d’un fichier existant sur le OneDrive de l’utilisateur. 
  
## Donnez-nous votre avis.

Votre avis compte beaucoup pour nous.  

Consultez les codes d’exemple et signalez-nous toute question ou tout problème vous trouvez en [soumettant une question](https://github.com/OfficeDev/Microsoft-Graph-UWP-Excel-REST-API-Explorer/issues) directement dans ce référentiel. Fournissez des étapes de reproduction, de sortie de la console et des messages d’erreur dans tout problème que vous ouvrez.

## Copyright

Copyright (c) 2016 Microsoft. Tous droits réservés.
  
Ce projet a adopté le [code de conduite Open Source de Microsoft](https://opensource.microsoft.com/codeofconduct/). Pour en savoir plus, reportez-vous à la [FAQ relative au code de conduite](https://opensource.microsoft.com/codeofconduct/faq/) ou contactez [opencode@microsoft.com](mailto:opencode@microsoft.com) pour toute question ou tout commentaire.
