
// Type: Intermech.Runtime.ComInterop.LocalServer.ComXmlFilesPluginManager
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System.Collections.Generic;


namespace Intermech.Runtime.ComInterop.LocalServer
{
    /// <summary>
    /// Менеджер плагинов COM-сервера для приложений с плагинами, в которых плагины самого приложения, содержащие COM-классы, описываются с помощью .com.xml-файлов.
    /// </summary>
    public sealed class ComXmlFilesPluginManager : ComPluginManager
    {
      /// <summary>Находит плагины для COM-сервера.</summary>
      /// <param name="comServer">COM-сервер</param>
      /// <param name="errorList">Список ошибок, произошедших при поиске плагинов</param>
      /// <returns>Коллекция описателей плагинов</returns>
      protected override ICollection<ComPluginInfo> DoFindPlugins(
        ComServer comServer,
        IErrorList errorList)
      {
        return new ComXmlFilesSearchService(comServer.HostApplication.ExecutablePath).FindPlugins(errorList);
      }
    }
}
