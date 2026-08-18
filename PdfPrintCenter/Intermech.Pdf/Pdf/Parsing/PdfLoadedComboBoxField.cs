// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Parsing.PdfLoadedComboBoxField
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Interactive;
using Syncfusion.Pdf.IO;
using Syncfusion.Pdf.Primitives;
using System.Collections.Generic;
using System.Drawing;

#nullable disable
namespace Syncfusion.Pdf.Parsing;

public class PdfLoadedComboBoxField : PdfLoadedChoiceField
{
  private PdfLoadedComboBoxItemCollection m_items;

  internal PdfLoadedComboBoxField(PdfDictionary dictionary, PdfCrossTable crossTable)
    : base(dictionary, crossTable)
  {
    PdfArray kids = this.Kids;
    this.m_items = new PdfLoadedComboBoxItemCollection();
    if (kids == null)
      return;
    for (int index = 0; index < kids.Count; ++index)
    {
      PdfDictionary dictionary1 = crossTable.GetObject(kids[index]) as PdfDictionary;
      this.m_items.Add(new PdfLoadedComboBoxItem((PdfLoadedStyledField) this, index, dictionary1));
    }
  }

  private void ApplyAppearance(PdfDictionary widget, PdfLoadedFieldItem item)
  {
    if (widget != null && widget.ContainsKey("AP"))
    {
      if (!(this.CrossTable.GetObject(widget["AP"]) is PdfDictionary primitive) || !primitive.ContainsKey("N"))
        return;
      if (item != null)
      {
        RectangleF bounds1 = item.Bounds;
      }
      else
      {
        RectangleF bounds2 = this.Bounds;
      }
      PdfTemplate wrapper = new PdfTemplate(this.Bounds.Size);
      this.DrawComboBox(wrapper.Graphics, item);
      primitive.Remove("N");
      primitive.SetProperty("N", (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) wrapper));
      widget.SetProperty("AP", (IPdfPrimitive) primitive);
    }
    else
    {
      if (!this.Form.SetAppearanceDictionary)
        return;
      this.Form.NeedAppearances = true;
    }
  }

  internal override void BeginSave()
  {
    base.BeginSave();
    PdfArray kids = this.Kids;
    if (kids != null)
    {
      for (int index = 0; index < kids.Count; ++index)
        this.ApplyAppearance(this.CrossTable.GetObject(kids[index]) as PdfDictionary, (PdfLoadedFieldItem) this.Items[index]);
    }
    else
      this.ApplyAppearance(this.GetWidgetAnnotation(this.Dictionary, this.CrossTable), (PdfLoadedFieldItem) null);
  }

  internal new PdfField Clone(PdfDictionary dictionary, PdfPage page)
  {
    PdfCrossTable crossTable = page.Section.ParentDocument.CrossTable;
    PdfLoadedComboBoxField loadedComboBoxField = new PdfLoadedComboBoxField(dictionary, crossTable);
    loadedComboBoxField.Page = (PdfPageBase) page;
    loadedComboBoxField.SetName(this.GetFieldName());
    loadedComboBoxField.Widget.Dictionary = this.Widget.Dictionary.Clone(crossTable) as PdfDictionary;
    return (PdfField) loadedComboBoxField;
  }

  internal override PdfLoadedFieldItem CreateLoadedItem(PdfDictionary dictionary)
  {
    base.CreateLoadedItem(dictionary);
    PdfLoadedComboBoxItem loadedItem = new PdfLoadedComboBoxItem((PdfLoadedStyledField) this, this.m_items.Count, dictionary);
    this.m_items.Add(loadedItem);
    if (this.Kids == null)
      this.Dictionary["Kids"] = (IPdfPrimitive) new PdfArray();
    this.Kids.Add((IPdfPrimitive) new PdfReferenceHolder((IPdfPrimitive) dictionary));
    return (PdfLoadedFieldItem) loadedItem;
  }

  internal override void Draw()
  {
    base.Draw();
    RectangleF bounds1 = this.Bounds with
    {
      Location = PointF.Empty
    };
    string s = string.Empty;
    if (this.SelectedIndex != -1)
      s = this.SelectedItem[0].Text;
    else if (this.Dictionary.ContainsKey("DV"))
      s = (this.Dictionary["DV"] as PdfString).Value;
    PdfTemplate template1 = new PdfTemplate(bounds1.Size);
    PdfArray kids = this.Kids;
    if (kids != null && kids.Count > 1)
    {
      for (int index = 0; index < kids.Count; ++index)
      {
        PdfLoadedFieldItem pdfLoadedFieldItem = (PdfLoadedFieldItem) this.Items[index];
        PdfTemplate template2 = new PdfTemplate(pdfLoadedFieldItem.Size);
        RectangleF bounds2 = pdfLoadedFieldItem.Bounds with
        {
          Location = PointF.Empty
        };
        this.DrawComboBox(template2.Graphics, pdfLoadedFieldItem);
        template2.Graphics.DrawString(s, pdfLoadedFieldItem.Font, pdfLoadedFieldItem.ForeBrush, bounds2, pdfLoadedFieldItem.StringFormat);
        pdfLoadedFieldItem.Page.Graphics.DrawPdfTemplate(template2, pdfLoadedFieldItem.Bounds.Location);
      }
    }
    else
    {
      this.DrawComboBox(template1.Graphics, (PdfLoadedFieldItem) null);
      template1.Graphics.DrawString(s, this.Font, this.ForeBrush, bounds1, this.StringFormat);
      this.Page.Graphics.DrawPdfTemplate(template1, this.Bounds.Location);
    }
  }

  private void DrawComboBox(PdfGraphics graphics, PdfLoadedFieldItem item)
  {
    PdfLoadedStyledField.GraphicsProperties graphicsProperties;
    this.GetGraphicsProperties(out graphicsProperties, item);
    graphicsProperties.Rect.Location = PointF.Empty;
    PaintParams paintParams = new PaintParams(graphicsProperties.Rect, graphicsProperties.BackBrush, graphicsProperties.ForeBrush, graphicsProperties.Pen, graphicsProperties.Style, graphicsProperties.BorderWidth, graphicsProperties.ShadowBrush, graphicsProperties.RotationAngle);
    FieldPainter.DrawComboBox(graphics, paintParams);
  }

  internal override float GetFontHeight(PdfFontFamily family)
  {
    List<float> floatList = new List<float>();
    foreach (PdfLoadedListItem pdfLoadedListItem in (PdfCollection) this.SelectedItem)
    {
      PdfFont pdfFont = (PdfFont) new PdfStandardFont(family, 12f);
      floatList.Add(pdfFont.MeasureString(pdfLoadedListItem.Text).Width);
    }
    floatList.Sort();
    return (float) (12.0 * ((double) this.Bounds.Size.Width - (double) (4 * this.BorderWidth))) / floatList[floatList.Count - 1];
  }

  public bool Editable
  {
    get => (FieldFlags.Edit & this.Flags) != 0;
    set
    {
      if (value)
        this.Flags |= FieldFlags.Edit;
      else
        this.Flags -= FieldFlags.Edit;
    }
  }

  public PdfLoadedComboBoxItemCollection Items => this.m_items;

  public int SelectedIndex
  {
    get => this.GetSelectedIndex()[0];
    set => this.SetSelectedIndex(new int[1]{ value });
  }

  public string SelectedValue
  {
    get => this.GetSelectedValue()[0];
    set => this.SetSelectedValue(new string[1]{ value });
  }
}
