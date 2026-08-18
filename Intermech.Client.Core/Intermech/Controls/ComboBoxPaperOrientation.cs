
// Type: Intermech.Controls.ComboBoxPaperOrientation
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Diagnostics;
using Intermech.Localization;
using Intermech.Print;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;


namespace Intermech.Controls;

public class ComboBoxPaperOrientation : TypedComboBox<PaperOrientation>
{
  public ComboBoxPaperOrientation()
  {
    this.BeginUpdate();
    try
    {
      this.Items.AddRange((IEnumerable<PaperOrientation>) Enum.GetValues(typeof (PaperOrientation)));
    }
    finally
    {
      this.EndUpdate();
    }
    if (this.Items.Count <= 0)
      return;
    this.SelectedIndex = 0;
  }

  protected override bool GetItemCaption(PaperOrientation orientation, [CanBeNull] out string caption)
  {
    if (orientation != PaperOrientation.Portrait)
    {
      if (orientation != PaperOrientation.Landscape)
        throw new Exception("Unknown PaperOrientation");
      caption = LocalizationHolder.rm.GetString("Landscape");
    }
    else
      caption = LocalizationHolder.rm.GetString("Portrait");
    return true;
  }

  [Bindable(false)]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public PaperOrientation SelectedOrientation
  {
    get => this.SelectedItem;
    set
    {
      if (this.SelectedItem == value)
        return;
      this.SelectedItem = value;
    }
  }

  protected override bool GetItemImage(PaperOrientation orientation, out Image image)
  {
    image = orientation == PaperOrientation.Portrait ? (Image) Intermech.Client.Core.Properties.Resources.PortraitCombo : (Image) Intermech.Client.Core.Properties.Resources.LandscapeCombo;
    return true;
  }
}
