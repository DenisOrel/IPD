
// Type: Intermech.Mvp.Winforms.ToolStripClickCommand
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Mvp.Components;
using System;
using System.Windows.Forms;


namespace Intermech.Mvp.Winforms
{
    public class ToolStripClickCommand : IClickCommand
    {
      private readonly ToolStripItem item;

      public ToolStripClickCommand(ToolStripItem item)
      {
        this.item = item != null ? item : throw new ArgumentNullException(nameof (item));
      }

      bool IClickCommand.Enabled
      {
        get => this.item.Enabled;
        set => this.item.Enabled = value;
      }

      void IClickCommand.PerformClick() => this.item.PerformClick();

      event EventHandler IClickCommand.Click
      {
        add => this.item.Click += value;
        remove => this.item.Click -= value;
      }
    }
}
