
// Type: Intermech.Runtime.ComInterop.LocalServer.ComServerCommandLine
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml


namespace Intermech.Runtime.ComInterop.LocalServer
{
    internal sealed class ComServerCommandLine
    {
      public ComServerCommandLine()
      {
        this.Command = ComServerInitializationCommand.None;
        this.RunMode = ComServerRunMode.Normal;
      }

      public ComServerInitializationCommand Command { get; set; }

      public ComServerRunMode RunMode { get; set; }
    }
}
