
// Type: Intermech.Runtime.ComInterop.LocalServer.SimpleComPluginManager
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;
using System.Reflection;


namespace Intermech.Runtime.ComInterop.LocalServer
{
    /// <summary>
    /// Менеджер плагинов COM-сервера для простых приложений, в которых все COM-классы реализованы в одной сборке.
    /// </summary>
    public class SimpleComPluginManager : ComPluginManager
    {
      private Assembly comPluginAssembly;
      private string[] typeLibraryPaths;

      /// <summary>Создает объект.</summary>
      /// <param name="comPluginAssembly">Сборка плагина COM-сервера с реализацией COM-классов</param>
      /// <param name="typeLibraryPaths">Массив путей к библиотекам типов приложения. Может быть пуст</param>
      /// <exception cref="T:System.ArgumentNullException">Параметры <paramref name="comPluginAssembly" />, <paramref name="typeLibraryPaths" /> не должен быть равен null</exception>
      public SimpleComPluginManager(Assembly comPluginAssembly, params string[] typeLibraryPaths)
      {
        if (comPluginAssembly == (Assembly) null)
          throw new ArgumentNullException(nameof (comPluginAssembly));
        if (typeLibraryPaths == null)
          throw new ArgumentNullException(nameof (typeLibraryPaths));
        this.comPluginAssembly = comPluginAssembly;
        this.typeLibraryPaths = typeLibraryPaths;
      }

      /// <summary>Находит плагины для COM-сервера.</summary>
      /// <param name="comServer">COM-сервер</param>
      /// <param name="errorList">Список ошибок, произошедших при поиске плагинов</param>
      /// <returns>Коллекция описателей плагинов</returns>
      protected override ICollection<ComPluginInfo> DoFindPlugins(
        ComServer comServer,
        IErrorList errorList)
      {
        return (ICollection<ComPluginInfo>) new List<ComPluginInfo>()
        {
          new ComPluginInfo(this.comPluginAssembly.Location, (ICollection<string>) this.typeLibraryPaths)
        };
      }
    }
}
