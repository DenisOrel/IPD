
// Type: Intermech.Mvp.Winforms.ToolStripDropDownMultiCommand
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Mvp.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;


namespace Intermech.Mvp.Winforms
{
    public class ToolStripDropDownMultiCommand : IMultiCommand
    {
      private EventHandler<MultiCommandEventArgs> clickHandler;
      private readonly ToolStripDropDownItem control;

      public ToolStripDropDownMultiCommand(ToolStripDropDownItem control)
      {
        this.control = control != null ? control : throw new ArgumentNullException(nameof (control));
        this.control.DropDownItemClicked += new ToolStripItemClickedEventHandler(this.ControlClickHandler);
      }

      void IMultiCommand.ClearItems() => this.control.DropDownItems.Clear();

      void IMultiCommand.SetItems(ICollection<MultiCommandItem> subItems)
      {
        if (subItems == null)
          throw new ArgumentNullException(nameof (subItems));
        this.control.DropDownItems.Clear();
        foreach (MultiCommandItem subItem in (IEnumerable<MultiCommandItem>) subItems)
          this.control.DropDownItems.Add(subItem.Text).Tag = (object) subItem;
      }

      bool IMultiCommand.Enabled
      {
        [DebuggerStepThrough] get => this.control.Enabled;
        [DebuggerStepThrough] set => this.control.Enabled = value;
      }

      event EventHandler<MultiCommandEventArgs> IMultiCommand.Click
      {
        add => this.clickHandler += value;
        remove => this.clickHandler -= value;
      }

      private void ControlClickHandler(object sender, ToolStripItemClickedEventArgs e)
      {
        if (this.clickHandler == null)
          return;
        this.clickHandler((object) this, new MultiCommandEventArgs((MultiCommandItem) e.ClickedItem.Tag));
      }
    }
}
