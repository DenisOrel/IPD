
// Type: Intermech.Mvp.Components.MultiCommandGroup
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Mvp.Components
{
    public sealed class MultiCommandGroup : AbstractCommandGroup<IMultiCommand>, IMultiCommand
    {
      private EventHandler<MultiCommandEventArgs> clickHandler;

      public MultiCommandGroup(ICollection<IMultiCommand> commands)
        : base(commands)
      {
      }

      public MultiCommandGroup(params IMultiCommand[] commands)
        : base(commands)
      {
      }

      protected override void InitializeSubCommands()
      {
        foreach (IMultiCommand command in (IEnumerable<IMultiCommand>) this.commands)
          command.Click += new EventHandler<MultiCommandEventArgs>(this.SubCommandClickHandler);
      }

      void IMultiCommand.ClearItems()
      {
        foreach (IMultiCommand command in (IEnumerable<IMultiCommand>) this.commands)
          command.ClearItems();
      }

      void IMultiCommand.SetItems(ICollection<MultiCommandItem> subItems)
      {
        if (subItems == null)
          throw new ArgumentNullException(nameof (subItems));
        foreach (IMultiCommand command in (IEnumerable<IMultiCommand>) this.commands)
          command.SetItems(subItems);
      }

      bool IMultiCommand.Enabled
      {
        [DebuggerStepThrough] get => this.firstCommand.Enabled;
        [DebuggerStepThrough] set
        {
          if (this.firstCommand.Enabled == value)
            return;
          foreach (IMultiCommand command in (IEnumerable<IMultiCommand>) this.commands)
            command.Enabled = value;
        }
      }

      event EventHandler<MultiCommandEventArgs> IMultiCommand.Click
      {
        add => this.clickHandler += value;
        remove => this.clickHandler -= value;
      }

      private void SubCommandClickHandler(object sender, MultiCommandEventArgs e)
      {
        if (this.clickHandler == null)
          return;
        this.clickHandler((object) this, e);
      }
    }
}
