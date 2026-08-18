// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.ComboBoxPrintPageSettings
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Controls;
using Intermech.Diagnostics;
using Intermech.Project.Print;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

#nullable disable
namespace Intermech.Project.Controls;

public class ComboBoxPrintPageSettings : TypedComboBox<PrintPageSettings>
{
  public ComboBoxPrintPageSettings()
  {
    this.BeginUpdate();
    try
    {
      this.Items.AddRange((IEnumerable<PrintPageSettings>) Enum.GetValues(typeof (PrintPageSettings)));
    }
    finally
    {
      this.EndUpdate();
    }
    if (this.Items.Count <= 0)
      return;
    this.SelectedIndex = 0;
  }

  protected override bool GetItemCaption(PrintPageSettings printPages, [NotNull] out string caption)
  {
    switch (printPages)
    {
      case PrintPageSettings.AllProjectDates:
        caption = "Печать всего проекта";
        break;
      case PrintPageSettings.SelectedDates:
        caption = "Печать определённых дат";
        break;
      case PrintPageSettings.SelectedPages:
        caption = "Печать определённых страниц";
        break;
      default:
        throw new Exception("Unknown PaperOrientation");
    }
    return true;
  }

  protected override bool GetItemRemarks(PrintPageSettings printPages, [NotNull] out string remarks)
  {
    switch (printPages)
    {
      case PrintPageSettings.AllProjectDates:
        remarks = "Печать проекта от начала до конца";
        break;
      case PrintPageSettings.SelectedDates:
        remarks = "Печать только шкалы времени между выбранными датами";
        break;
      case PrintPageSettings.SelectedPages:
        remarks = "Печать только заданных страниц";
        break;
      default:
        throw new Exception("Unknown PaperOrientation");
    }
    return true;
  }

  [Bindable(false)]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public PrintPageSettings SelectedSettings
  {
    get => this.SelectedItem;
    set
    {
      if (this.SelectedItem == value)
        return;
      this.SelectedItem = value;
    }
  }

  protected override bool GetItemImage(PrintPageSettings settings, out Image image)
  {
    switch (settings)
    {
      case PrintPageSettings.AllProjectDates:
        image = (Image) Intermech.Project.Controls.img.Images.PrintEntireProject;
        return true;
      case PrintPageSettings.SelectedDates:
        image = (Image) Intermech.Project.Controls.img.Images.PrintSelectedDates;
        return true;
      case PrintPageSettings.SelectedPages:
        image = (Image) Intermech.Project.Controls.img.Images.PrintSelectedPages;
        return true;
      default:
        image = (Image) null;
        return false;
    }
  }
}
