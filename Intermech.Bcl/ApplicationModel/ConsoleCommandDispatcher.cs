using Intermech.Text;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;


namespace Intermech.ApplicationModel
{
    /// <summary>
    /// Реализует ввод с клавиатуры и выполнение команд, не блокируя надолго основной поток приложения.
    /// Такой режим работы необходим для корректной выгрузки дополнительных AppDomain, создаваемых движком скриптов.
    /// </summary>
    public sealed class ConsoleCommandDispatcher
    {
      private readonly IConsoleService consoleService;
      private readonly IConsoleCommandRegistry commandRegistry;
      private readonly IConsoleCommandRegistry internalRegistry;
      private ConsoleCommandInfo quitCommand;
      private ConsoleCommandInfo helpCommand;

      public ConsoleCommandDispatcher(
        IConsoleService consoleService,
        IConsoleCommandRegistry commandRegistry)
      {
        if (consoleService == null)
          throw new ArgumentNullException(nameof (consoleService));
        if (commandRegistry == null)
          throw new ArgumentNullException(nameof (commandRegistry));
        this.consoleService = consoleService;
        this.commandRegistry = commandRegistry;
        this.internalRegistry = (IConsoleCommandRegistry) new ConsoleCommandRegistry();
        this.quitCommand = new ConsoleCommandInfo("quit", "q", "terminate application", new ConsoleCommandMethod(this.QuitCommandHandler));
        this.internalRegistry.Add(this.quitCommand);
        this.helpCommand = new ConsoleCommandInfo("help", "?", "show help", new ConsoleCommandMethod(this.HelpCommandHandler));
        this.internalRegistry.Add(this.helpCommand);
      }

      public void Run()
      {
        while (true)
        {
          Tuple<string, List<string>> tuple;
          do
          {
            string commandText;
            do
            {
              this.consoleService.WriteLine("Press 'q' or 'quit' to close application.");
              this.consoleService.Write(">");
              string str;
              for (str = this.consoleService.TryReadLine(); str == null; str = this.consoleService.TryReadLine())
                Thread.CurrentThread.Join(100);
              commandText = str.Trim();
            }
            while (string.IsNullOrEmpty(commandText));
            tuple = this.TrySplitCommandNameAndArgs(commandText);
          }
          while (tuple == null);
          string commandName = tuple.Item1;
          List<string> commandArgs = tuple.Item2;
          ConsoleCommandInfo command = this.FindCommand(commandName);
          if (command == null)
            this.consoleService.WriteLine("Unknown command. Press ? or help for available commands.");
          else if (command != this.quitCommand)
          {
            try
            {
              command.Handler.Invoke(this.consoleService, commandArgs);
            }
            catch (Exception ex)
            {
              this.consoleService.WriteLine(ex.Message, ConsoleColor.Red);
              this.consoleService.WriteLine(ex.StackTrace, ConsoleColor.Red);
            }
          }
          else
            break;
        }
      }

      private Tuple<string, List<string>> TrySplitCommandNameAndArgs(string commandText)
      {
        string[] collection = commandText.Split(TextServices.WordsSplitPatterns, StringSplitOptions.RemoveEmptyEntries);
        if (collection.Length == 0)
          return (Tuple<string, List<string>>) null;
        string str = collection[0];
        List<string> stringList = new List<string>((IEnumerable<string>) collection);
        stringList.RemoveAt(0);
        return Tuple.Create(str, stringList);
      }

      private ConsoleCommandInfo FindCommand(string commandName)
      {
        return this.internalRegistry.FindByName(commandName) ?? this.commandRegistry.FindByName(commandName) ?? (ConsoleCommandInfo) null;
      }

      private void QuitCommandHandler(IConsoleService service, List<string> commandArgs)
      {
      }

      private void HelpCommandHandler(IConsoleService service, List<string> commandArgs)
      {
        List<ConsoleCommandInfo> consoleCommandInfoList = new List<ConsoleCommandInfo>(64 /*0x40*/);
        consoleCommandInfoList.AddRange((IEnumerable<ConsoleCommandInfo>) this.internalRegistry.GetAll());
        consoleCommandInfoList.AddRange((IEnumerable<ConsoleCommandInfo>) this.commandRegistry.GetAll());
        consoleCommandInfoList.Sort((Comparison<ConsoleCommandInfo>) ((x, y) => string.Compare(x.Name, y.Name, true)));
        StringBuilder stringBuilder = new StringBuilder(512 /*0x0200*/);
        foreach (ConsoleCommandInfo consoleCommandInfo in consoleCommandInfoList)
        {
          stringBuilder.Append("  ");
          stringBuilder.Append(consoleCommandInfo.Name);
          if (!string.IsNullOrEmpty(consoleCommandInfo.ShortName))
          {
            stringBuilder.Append(' ');
            stringBuilder.AppendFormat("({0})", (object) consoleCommandInfo.ShortName);
          }
          if (!string.IsNullOrEmpty(consoleCommandInfo.Help))
          {
            stringBuilder.Append(" - ");
            stringBuilder.Append(consoleCommandInfo.Help);
          }
          this.consoleService.WriteLine(stringBuilder.ToString());
          stringBuilder.Clear();
        }
      }
    }
}
