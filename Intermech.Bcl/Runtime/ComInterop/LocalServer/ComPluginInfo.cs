
// Type: Intermech.Runtime.ComInterop.LocalServer.ComPluginInfo
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;


namespace Intermech.Runtime.ComInterop.LocalServer
{
    /// <summary>Описатель плагина для COM-сервера.</summary>
    public class ComPluginInfo
    {
      /// <summary>Создает объект.</summary>
      /// <param name="assemblyPath">Путь к файлу сборки плагина с реализациями COM-классов</param>
      /// <param name="typeLibPathList">Коллекция путей к файлам библиотек типов, поставляемых вместе со сборкой плагина</param>
      /// <exception cref="T:System.ArgumentNullException">Параметры <paramref name="assemblyPath" />, <paramref name="typeLibPathList" /> не должны быть равны null</exception>
      public ComPluginInfo(string assemblyPath, ICollection<string> typeLibPathList)
      {
        if (assemblyPath == null)
          throw new ArgumentNullException(nameof (assemblyPath));
        if (typeLibPathList == null)
          throw new ArgumentNullException(nameof (typeLibPathList));
        this.AssemblyPath = assemblyPath;
        this.TypeLibPathList = typeLibPathList;
      }

      /// <summary>
      /// Возвращает путь к файлу сборки плагина с реализациями COM-классов.
      /// </summary>
      public string AssemblyPath { get; private set; }

      /// <summary>
      /// Возвращает коллекцию путей к файлам библиотек типов, поставляемых вместе со сборкой плагина.
      /// При регистрации COM-классов будут зарегистрированы и эти библиотеки типов.
      /// </summary>
      public ICollection<string> TypeLibPathList { get; private set; }
    }
}
