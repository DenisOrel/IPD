// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfRadioButtonListField
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System;

#nullable disable
namespace Syncfusion.Pdf.Interactive;

public class PdfRadioButtonListField : PdfField
{
  private PdfRadioButtonItemCollection m_items;
  private int m_selectedIndex;

  public PdfRadioButtonListField(PdfPageBase page, string name)
    : base(page, name)
  {
    this.m_selectedIndex = -1;
    this.Flags |= FieldFlags.Radio;
    this.Dictionary.SetProperty("FT", (IPdfPrimitive) new PdfName("Btn"));
  }

  internal override void Draw()
  {
    int index = 0;
    for (int count = this.Items.Count; index < count; ++index)
      this.Items[index].Draw();
  }

  public PdfRadioButtonItemCollection Items
  {
    get
    {
      if (this.m_items == null)
      {
        this.m_items = new PdfRadioButtonItemCollection(this);
        this.Dictionary.SetProperty("Kids", (IPdfWrapper) this.m_items);
      }
      return this.m_items;
    }
  }

  public int SelectedIndex
  {
    get => this.m_selectedIndex;
    set
    {
      if (value < 0 || value >= this.Items.Count)
        throw new ArgumentOutOfRangeException(nameof (SelectedIndex));
      if (this.m_selectedIndex == value)
        return;
      this.m_selectedIndex = value;
      PdfRadioButtonListItem radioButtonListItem = this.m_items[this.m_selectedIndex];
      this.Dictionary.SetName("V", radioButtonListItem.Value);
      this.Dictionary.SetName("DV", radioButtonListItem.Value);
    }
  }

  public PdfRadioButtonListItem SelectedItem
  {
    get
    {
      PdfRadioButtonListItem selectedItem = (PdfRadioButtonListItem) null;
      if (this.m_selectedIndex != -1)
        selectedItem = this.m_items[this.m_selectedIndex];
      return selectedItem;
    }
  }

  public string SelectedValue
  {
    get => this.m_items[this.m_selectedIndex].Value;
    set
    {
      if (value == null)
        throw new ArgumentNullException(nameof (SelectedValue));
      PdfRadioButtonListItem radioButtonListItem = this.m_items[this.m_selectedIndex];
      if (!(radioButtonListItem.Value != value))
        return;
      radioButtonListItem.Value = value;
    }
  }
}
