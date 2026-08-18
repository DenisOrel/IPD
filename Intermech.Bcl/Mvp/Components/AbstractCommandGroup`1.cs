
// Type: Intermech.Mvp.Components.AbstractCommandGroup`1
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Collections;
using System;
using System.Collections.Generic;


namespace Intermech.Mvp.Components
{
    public abstract class AbstractCommandGroup<TCommand>
    {
      protected readonly ICollection<TCommand> commands;
      protected readonly TCommand firstCommand;

      public AbstractCommandGroup(ICollection<TCommand> commands)
      {
        if (commands == null)
          throw new ArgumentNullException(nameof (commands));
        this.commands = commands.Count != 0 ? commands : throw new ArgumentException("Требуется, чтобы коллекция содержала хотя бы один элемент", nameof (commands));
        this.firstCommand = CollectionUtils.GetFirstItem((IEnumerable<TCommand>) commands);
        this.InitializeSubCommands();
      }

      public AbstractCommandGroup(params TCommand[] commands)
      {
        if (commands == null)
          throw new ArgumentNullException(nameof (commands));
        this.commands = commands.Length != 0 ? (ICollection<TCommand>) commands : throw new ArgumentException("Требуется, чтобы коллекция содержала хотя бы один элемент", nameof (commands));
        this.firstCommand = commands[0];
        this.InitializeSubCommands();
      }

      protected abstract void InitializeSubCommands();
    }
}
