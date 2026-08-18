
// Type: Intermech.Runtime.ComInterop.LocalServer.RegisterComPluginContext
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;


namespace Intermech.Runtime.ComInterop.LocalServer
{
    internal sealed class RegisterComPluginContext
    {
      public RegisterComPluginContext(
        ComServer comServer,
        string assemblyPath,
        ICollection<Guid> typeLibIdList,
        IErrorList errorList)
      {
        if (comServer == null)
          throw new ArgumentOutOfRangeException(nameof (comServer));
        if (assemblyPath == null)
          throw new ArgumentNullException(nameof (assemblyPath));
        if (typeLibIdList == null)
          throw new ArgumentNullException(nameof (typeLibIdList));
        if (errorList == null)
          throw new ArgumentNullException(nameof (errorList));
        this.ComServer = comServer;
        this.AssemblyPath = assemblyPath;
        this.TypeLibIdList = typeLibIdList;
        this.ErrorList = errorList;
      }

      public ComServer ComServer { get; private set; }

      /// <summary>
      /// Возвращает путь к файлу сборки плагина с реализациями COM-классов.
      /// </summary>
      public string AssemblyPath { get; private set; }

      public ICollection<Guid> TypeLibIdList { get; private set; }

      public IErrorList ErrorList { get; private set; }
    }
}
