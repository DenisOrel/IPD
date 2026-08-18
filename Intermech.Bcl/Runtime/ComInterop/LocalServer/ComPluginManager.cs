
// Type: Intermech.Runtime.ComInterop.LocalServer.ComPluginManager
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;


namespace Intermech.Runtime.ComInterop.LocalServer
{
    /// <summary>Базовый класс для менеджера плагинов COM-сервера.</summary>
    public abstract class ComPluginManager
    {
      /// <summary>Находит плагины для COM-сервера.</summary>
      /// <param name="comServer">COM-сервер</param>
      /// <param name="errorList">Список ошибок, произошедших при поиске плагинов</param>
      /// <returns>Коллекция описателей плагинов</returns>
      /// <exception cref="T:System.ArgumentNullException">Параметры <paramref name="comServer" />, <paramref name="errorList" /> не должны быть равны null</exception>
      public ICollection<ComPluginInfo> FindPlugins(ComServer comServer, IErrorList errorList)
      {
        if (comServer == null)
          throw new ArgumentNullException(nameof (comServer));
        return errorList != null ? this.DoFindPlugins(comServer, errorList) : throw new ArgumentNullException(nameof (errorList));
      }

      /// <summary>Находит плагины для COM-сервера.</summary>
      /// <param name="comServer">COM-сервер</param>
      /// <param name="errorList">Список ошибок, произошедших при поиске плагинов</param>
      /// <returns>Коллекция описателей плагинов</returns>
      protected abstract ICollection<ComPluginInfo> DoFindPlugins(
        ComServer comServer,
        IErrorList errorList);
    }
}
