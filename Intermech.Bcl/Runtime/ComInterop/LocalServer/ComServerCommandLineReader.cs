
// Type: Intermech.Runtime.ComInterop.LocalServer.ComServerCommandLineReader
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace Intermech.Runtime.ComInterop.LocalServer
{
    internal sealed class ComServerCommandLineReader
    {
      private Regex registerCommand;
      private Regex unregisterCommand;
      private Regex embedCommand;

      public ComServerCommandLineReader()
      {
        this.registerCommand = new Regex("[/-](?:Regserver|Register)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
        this.unregisterCommand = new Regex("[/-](?:Unregserver|Unregister)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
        this.embedCommand = new Regex("[/-]Embedding", RegexOptions.IgnoreCase | RegexOptions.Singleline);
      }

      public ComServerCommandLine Read(ICollection<string> commandLine)
      {
        ComServerCommandLine serverCommandLine = new ComServerCommandLine();
        if (commandLine != null && commandLine.Count > 0)
        {
          foreach (string input in (IEnumerable<string>) commandLine)
          {
            if (this.registerCommand.IsMatch(input) && serverCommandLine.Command == ComServerInitializationCommand.None)
              serverCommandLine.Command = ComServerInitializationCommand.Register;
            if (this.unregisterCommand.IsMatch(input) && serverCommandLine.Command == ComServerInitializationCommand.None)
              serverCommandLine.Command = ComServerInitializationCommand.Unregister;
            if (this.embedCommand.IsMatch(input))
              serverCommandLine.RunMode = ComServerRunMode.Embedding;
          }
        }
        return serverCommandLine;
      }
    }
}
