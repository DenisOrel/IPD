// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfListField
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System;

#nullable disable
namespace Syncfusion.Pdf.Interactive;

public abstract class PdfListField : PdfAppearanceField
{
  private PdfListFieldItemCollection m_items;
  private int m_selectedIndex;

  internal PdfListField() => this.m_selectedIndex = -1;

  public PdfListField(PdfPageBase page, string name)
    : base(page, name)
  {
    this.m_selectedIndex = -1;
  }

  internal override void Draw() => base.Draw();

  protected override void Initialize()
  {
    base.Initialize();
    this.Dictionary.SetProperty("FT", (IPdfPrimitive) new PdfName("Ch"));
  }

  public PdfListFieldItemCollection Items
  {
    get
    {
      if (this.m_items == null)
      {
        this.m_items = new PdfListFieldItemCollection();
        this.Dictionary.SetProperty("Opt", (IPdfWrapper) this.m_items);
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
      this.Dictionary.SetProperty("I", (IPdfPrimitive) new PdfArray(new int[1]
      {
        this.m_selectedIndex
      }));
    }
  }

  public PdfListFieldItem SelectedItem => this.m_items[this.m_selectedIndex];

  public string SelectedValue
  {
    get => this.m_items[this.m_selectedIndex].Value;
    set
    {
      if (value == null)
        throw new ArgumentNullException(nameof (SelectedValue));
      PdfListFieldItem pdfListFieldItem = this.m_items[this.m_selectedIndex];
      if (!(pdfListFieldItem.Value != value))
        return;
      pdfListFieldItem.Value = value;
    }
  }
}
