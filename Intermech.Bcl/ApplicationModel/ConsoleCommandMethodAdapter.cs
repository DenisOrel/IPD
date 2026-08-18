using System;
using System.Collections.Generic;


namespace Intermech.ApplicationModel
{
    internal sealed class ConsoleCommandMethodAdapter : AbstractConsoleCommand
    {
      private readonly ConsoleCommandMethod methodHandler;

      public ConsoleCommandMethodAdapter(ConsoleCommandMethod methodHandler)
      {
        this.methodHandler = methodHandler != null ? methodHandler : throw new ArgumentNullException(nameof (methodHandler));
      }

      protected sealed override void DoInvoke(IConsoleService consoleService, List<string> commandArgs)
      {
        this.methodHandler(consoleService, commandArgs);
      }
    }
}
