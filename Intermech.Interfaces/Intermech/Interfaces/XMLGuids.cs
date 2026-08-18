
// Type: Intermech.Interfaces.XMLGuids
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>
    /// Класс хранит константы с названиями и Guid-ами узлов настроек в XML документе
    /// </summary>
    public static class XMLGuids
    {
      /// <summary>Стандартный заголовок без указания кодировки</summary>
      public const string xmlHeader = "<?xml version='1.0' ?>";
      /// <summary>Стандартный заголовок с кодировкой UTF8</summary>
      public const string xmlHeaderUTF8 = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
      /// <summary>
      /// Шаблон пустого документа с корректным корневым узлом "IPS.UserSettings"
      /// </summary>
      public const string xmlEmptyDoc = "<?xml version=\"1.0\" encoding=\"utf-8\"?><IPS.UserSettings />";
      /// <summary>
      /// Шаблон пустого документа с корректным корневым узлом "IPS"
      /// </summary>
      public const string xmlEmptyDocRoot = "<?xml version=\"1.0\" encoding=\"utf-8\"?><IPS />";
      /// <summary>Корневой узел - "IPS.UserSettings"</summary>
      public const string xmlRootNode = "IPS.UserSettings";
      /// <summary>Корневой узел - "IPS"</summary>
      public const string xmlRoot = "IPS";
      /// <summary>Узел с настройками панелей управления - "Toolbars"</summary>
      public const string xmlToolbarsNode = "Toolbars";
      /// <summary>Узел с настройками панели управления - "Toolbar"</summary>
      public const string xmlToolbarNode = "Toolbar";
      /// <summary>Узел с цветовыми схемами пользователя</summary>
      public const string xmlUserColorsScheme = "UserColorsScheme";
      /// <summary>Цветовая схема</summary>
      public const string xmlColorScheme = "ColorScheme";
      /// <summary>имя цветовой схемы</summary>
      public const string xmlColorSchemeName = "ColorSchemeName";
      /// <summary>guid цветовой схемы</summary>
      public const string xmlColorSchemeGuid = "ColorSchemeGuid";
      /// <summary>для каких стилей используется градиент</summary>
      public const string xmlGradientUsing = "GradientUsing";
      /// <summary>узел с опиcанием настроек цветовой схемы</summary>
      public const string xmlUIColorsScheme = "UIColorsScheme";
      /// <summary>элемент цветовой схемы</summary>
      public const string xmlSchemeElement = "SchemeElement";
      /// <summary>id элемента</summary>
      public const string xmlElementId = "ElementId";
      /// <summary>цвет фона</summary>
      public const string xmlElementBackground = "Background";
      /// <summary>цвет текста</summary>
      public const string xmlElementForeground = "Foreground";
      /// <summary>начальный цвет фона</summary>
      public const string xmlElementBkStartColor = "BkStartColor";
      /// <summary>конечный цвет фона</summary>
      public const string xmlElementBkEndColor = "BkEndColor";
      /// <summary>тип градиента</summary>
      public const string xmlElementGradientMode = "GradientMode";
      /// <summary>узел с серверной информацией</summary>
      public const string xmlServerInformation = "ServerInformation";
      /// <summary>узел с клиентской информацией</summary>
      public const string xmlClientInformation = "ClientInformation";
      /// <summary>узел с информацией, полученной из формы запроса</summary>
      public const string xmlRequestInformation = "RequestInformation";
      /// <summary>
      ///  узел с описанием ошибки, полученной при попытке обратиться к серверу приложений
      /// </summary>
      public const string xmlServerNotAvailable = "ServerNotAvailable";
      /// <summary>узел с описанием темы запроса</summary>
      public const string xmlRequestTopic = "Topic";
      /// <summary>имя юзера, отправившего запрос</summary>
      public const string xmlUserName = "UserName";
      /// <summary>роль юзера, отправившего запрос</summary>
      public const string xmlUserRole = "UserRole";
      /// <summary>
      /// узел с описание ошибки, которая возникла на сервере при попытке к нему обратиться
      /// </summary>
      public const string xmlServerException = "ServerException";
      /// <summary>узел с описанием организации</summary>
      public const string xmlOrganization = "Organization";
      /// <summary>узел с обратной связью</summary>
      public const string xmlMailTo = "MailTo";
      /// <summary>узел с текстом запроса</summary>
      public const string xmlRequest = "Request";
      /// <summary>
      /// 
      /// </summary>
      public const string xmlLogOversize = "xmlLogOversize";
      /// <summary>версия IIS</summary>
      public const string xmlIISVersion = "IISVersion";
      /// <summary>узел с описанием установленных framework'ов</summary>
      public const string xmlFramework = "Framework";
      /// <summary>версия framework</summary>
      public const string xmlFrameworkVersion = "FrameworkVersion";
      /// <summary>версия и разрядность Windows</summary>
      public const string xmlWindowsVersion = "WindowsVersion";
      /// <summary>инфмормация из окна вывод сервера приложений</summary>
      public const string xmlServerOutput = "ServerOutput";
      /// <summary>инфмормация из окна вывод клиента</summary>
      public const string xmlClientOutput = "ClientOutput";
      /// <summary>категория сообщения в окне вывода сервера приложений</summary>
      public const string xmlOutputCategory = "OutputCategory";
      /// <summary>сообщение в окне вывода сервера приложений</summary>
      public const string xmlOutputMessage = "OutputMessage";
      /// <summary>узел с конфигами всех установленных ips-клиентов</summary>
      public const string xmlIPSHomeClient = "IPSHomeClient";
      /// <summary>атрибут  - запущен данный клиент или нет</summary>
      public const string xmlAttrCurrentClient = "current";
      /// <summary>узел с конфигом инсталлятора клиента</summary>
      public const string xmlClientSetupConfig = "config";
      /// <summary>
      ///  атрибут в конфиге инсталлятора клиента - имя параметра
      /// </summary>
      public const string xmlAttrSetupConfigKey = "key";
      /// <summary>
      ///  атрибут в конфиге инсталлятора клиента - значение параметра
      /// </summary>
      public const string xmlAttrSetupConfigVal = "val";
      /// <summary>значение параметра</summary>
      public const string xmlValue = "value";
      /// <summary>Узел с настройками контекстных меню - "ContextMenus"</summary>
      public const string xmlContextMenusNode = "ContextMenus";
      /// <summary>Узел с настройками контекстного меню - "ContextMenu"</summary>
      public const string xmlContextMenuNode = "ContextMenu";
      /// <summary>
      /// Узел с настройками закладок "Навигатора" - "NavigatorViews"
      /// </summary>
      public const string xmlNavigatorViewsNode = "NavigatorViews";
      /// <summary>
      /// Узел с настройками закладки "Навигатора" - "NavigatorView"
      /// </summary>
      public const string xmlNavigatorViewNode = "NavigatorView";
      /// <summary>
      /// Узел для окна с информацией об исключениях - "Exception"
      /// </summary>
      public const string xmlException = "Exception";
      /// <summary>
      /// Узел для окна с информацией об исключениях - "ExceptionText"
      /// </summary>
      public const string xmlExceptionText = "ExceptionText";
      /// <summary>
      /// Узел для окна с информацией об исключениях - "ExceptionStack"
      /// </summary>
      public const string xmlExceptionStack = "ExceptionStack";
      /// <summary>
      /// Узел для окна с информацией об исключениях - "ExceptionSource"
      /// </summary>
      public const string xmlExceptionSource = "ExceptionSource";
      /// <summary>
      /// Узел для окна с информацией об исключениях - "Plugins"
      /// </summary>
      public const string xmlPlugins = "Plugins";
      /// <summary>Узел для окна с информацией об исключениях - "Plugin"</summary>
      public const string xmlPlugin = "Plugin";
      /// <summary>Узел для окна с информацией об исключениях - "Build"</summary>
      public const string xmlBuild = "Build";
      /// <summary>
      /// Узел для окна с информацией об исключениях - "BuildDate"
      /// </summary>
      public const string xmlBuildDate = "BuildDate";
      /// <summary>
      /// Узел для окна с информацией об исключениях - "BuildTime"
      /// </summary>
      public const string xmlBuildTime = "BuildTime";
      /// <summary>
      /// Узел для окна с информацией об исключениях - "BuildGuid"
      /// </summary>
      public const string xmlBuildGuid = "BuildGuid";
      /// <summary>Атрибут для разрешения узла - "enabled"</summary>
      public const string xmlattrEnabled = "enabled";
      /// <summary>Атрибут для активного узла - "active"</summary>
      public const string xmlattrActive = "active";
      /// <summary>Атрибут для guid узла - "guid"</summary>
      public const string xmlattrGuid = "guid";
      /// <summary>Атрибут для команды - "command"</summary>
      public const string xmlattrCommand = "command";
      /// <summary>Атрибут для названия - "name"</summary>
      public const string xmlattrName = "name";
      /// <summary>Атрибут для номера группы - "groupID"</summary>
      public const string xmlattrGroupID = "groupID";
      /// <summary>Атрибут для номера в группе - "orderID"</summary>
      public const string xmlattrOrderID = "orderID";
      /// <summary>
      /// Атрибут для окна с информацией об исключениях - "Version"
      /// </summary>
      public const string xmlattrVersion = "version";
      /// <summary>
      /// Атрибут для окна с информацией об исключениях - "location"
      /// </summary>
      public const string xmlattrLocation = "location";
    }
}
