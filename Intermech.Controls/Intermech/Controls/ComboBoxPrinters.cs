
// Type: Intermech.Controls.ComboBoxPrinters
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using Intermech.Printing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;


namespace Intermech.Controls;

/// <summary>Комбобокс для выбора принтера</summary>
public class ComboBoxPrinters : TypedComboBox<Printer>
{
  public ComboBoxPrinters()
  {
    if (this.DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
      return;
    Printers.Refresh(this.IsHandleCreated ? this.Handle : IntPtr.Zero);
    this.Items.AddRange((IEnumerable<Printer>) Printers.List.OrderBy<Printer, string>((Func<Printer, string>) (printer => printer.Name)).ToArray<Printer>());
  }

  protected override bool GetItemIcon(Printer item, out Icon icon)
  {
    icon = this.ItemHeight >= 32 /*0x20*/ ? item.Icon : item.SmallIcon;
    return true;
  }

  [Bindable(true)]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Printer SelectedPrinter
  {
    get => this.SelectedItem;
    set => this.SelectedItem = value;
  }

  protected override bool GetItemRemarks(Printer item, out string remarks)
  {
    remarks = "Готово";
    return true;
  }
}
