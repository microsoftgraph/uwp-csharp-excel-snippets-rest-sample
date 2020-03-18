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
# Обозреватель REST API для Excel

Это приложение для [универсальной платформы Windows](http://dev.windows.com)
иллюстрирует использование [REST API Excel](https://github.com/OfficeDev/microsoft-graph-docs/tree/beta) доступа к книгам Excel, хранящимся в OneDrive для бизнеса и SharePoint Online, с помощью Office Graph из собственного приложения. Обратите внимание, что REST API Excel выпущен в бета-версии, а в примере показаны лишь некоторые из запросов, которые можно отправлять с помощью REST API Excel.
Сведения о других возможностях этого API вы найдете в документации.

## Необходимые компоненты ##

Для этого примера требуются приведенные ниже компоненты.  

  * Visual Studio 2015 с обновлением 1
  * Windows 10 ([с включенным режимом разработки](https://msdn.microsoft.com/library/windows/apps/xaml/dn706236.aspx)).
  * Учетная запись Office 365 для бизнеса. Вы можете [подписаться на план Office 365 для разработчиков](https://msdn.microsoft.com/en-us/office/office365/howto/setup-development-environment), который включает ресурсы, необходимые для создания приложений Office 365.

## Начало работы ##

Чтобы запустить пример кода:

  * Откройте файл решения ExcelRESTExplorer.sln. в папке src.
  * Чтобы создать и начать отладку, нажмите F5.
  * Войдите с помощью учетной записи Office 365 для бизнеса.
  * Нажмите кнопку «Добавить файл», чтобы добавить образец файла в OneDrive пользователя, выполнившего вход, или команду «Получить метаданные элемента» для получения идентификатор существующего файла в OneDrive пользователя. 
  
## Оставьте свой отзыв

Ваш отзыв важен для нас.  

Ознакомьтесь с образцом кода и сообщите нам о любых возникших вопросах и проблемах, с которыми вы столкнулись, [отправив сообщение](https://github.com/OfficeDev/Microsoft-Graph-UWP-Excel-REST-API-Explorer/issues) в этом репозитории. Укажите выполненные действия, выходное сообщение консоли и сообщения об ошибках при любой проблеме.

## Авторские права

(c) Корпорация Майкрософт (Microsoft Corporation), 2016. Все права защищены.
  
Этот проект соответствует [Правилам поведения разработчиков открытого кода Майкрософт](https://opensource.microsoft.com/codeofconduct/). Дополнительные сведения см. в разделе [часто задаваемых вопросов о правилах поведения](https://opensource.microsoft.com/codeofconduct/faq/). Если у вас возникли вопросы или замечания, напишите нам по адресу [opencode@microsoft.com](mailto:opencode@microsoft.com).
