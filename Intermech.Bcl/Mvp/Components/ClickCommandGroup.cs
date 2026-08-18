
// Type: Intermech.Mvp.Components.ClickCommandGroup
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Mvp.Components
{
    public class ClickCommandGroup : AbstractCommandGroup<IClickCommand>, IClickCommand
    {
      private EventHandler clickHandler;

      public ClickCommandGroup(ICollection<IClickCommand> commands)
        : base(commands)
      {
      }

      public ClickCommandGroup(params IClickCommand[] commands)
        : base(commands)
      {
      }

      protected override void InitializeSubCommands()
      {
        foreach (IClickCommand command in (IEnumerable<IClickCommand>) this.commands)
          command.Click += new EventHandler(this.SubCommandClickHandler);
      }

      bool IClickCommand.Enabled
      {
        [DebuggerStepThrough] get => this.firstCommand.Enabled;
        [DebuggerStepThrough] set
        {
          if (this.firstCommand.Enabled == value)
            return;
          foreach (IClickCommand command in (IEnumerable<IClickCommand>) this.commands)
            command.Enabled = value;
        }
      }

      void IClickCommand.PerformClick() => this.firstCommand.PerformClick();

      event EventHandler IClickCommand.Click
      {
        add => this.clickHandler += value;
        remove => this.clickHandler -= value;
      }

      private void SubCommandClickHandler(object sender, EventArgs e)
      {
        if (this.clickHandler == null)
          return;
        this.clickHandler((object) this, e);
      }
    }
}
