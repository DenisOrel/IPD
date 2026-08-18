
// Type: Intermech.Runtime.ComInterop.LocalServer.ComXmlPluginInfo
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System.Collections.Generic;


namespace Intermech.Runtime.ComInterop.LocalServer
{
    internal sealed class ComXmlPluginInfo : ComPluginInfo
    {
      public ComXmlPluginInfo(
        string assemblyPath,
        ICollection<string> typeLibPathList,
        string hostName)
        : base(assemblyPath, typeLibPathList)
      {
        this.HostName = hostName;
      }

      public string HostName { get; private set; }
    }
}
