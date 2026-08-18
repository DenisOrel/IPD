// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Parsing.PdfLoadedListBoxField
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Interactive;
using Syncfusion.Pdf.IO;
using Syncfusion.Pdf.Primitives;
using System.Drawing;


namespace Syncfusion.Pdf.Parsing
{
    public class PdfLoadedListBoxField : PdfLoadedChoiceField
    {
      private PdfLoadedListFieldItemCollection m_items;

      internal PdfLoadedListBoxField(PdfDictionary dictionary, PdfCrossTable crossTable)
        : base(dictionary, crossTable)
      {
        PdfArray kids = this.Kids;
        this.m_items = new PdfLoadedListFieldItemCollection();
        if (kids == null)
          return;
        for (int index = 0; index < kids.Count; ++index)
        {
          PdfDictionary dictionary1 = crossTable.GetObject(kids[index]) as PdfDictionary;
          this.m_items.Add(new PdfLoadedListFieldItem((PdfLoadedStyledField) this, index, dictionary1));
        }
      }

      private void ApplyAppearance(PdfDictionary widget, PdfLoadedFieldItem item)
      {
        if (widget != null && widget.ContainsKey("AP"))
        {
          if (!(this.CrossTable.GetObject(widget["AP"]) is PdfDictionary primitive) || !primitive.ContainsKey("N"))
            return;
          PdfTemplate wrapper = new PdfTemplate((item == null ? this.Bounds : item.Bounds).Size);
          this.DrawListBox(wrapper.Graphics, item);
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
        PdfLoadedListBoxField loadedListBoxField = new PdfLoadedListBoxField(dictionary, crossTable);
        loadedListBoxField.Page = (PdfPageBase) page;
        loadedListBoxField.SetName(this.GetFieldName());
        loadedListBoxField.Widget.Dictionary = this.Widget.Dictionary.Clone(crossTable) as PdfDictionary;
        return (PdfField) loadedListBoxField;
      }

      private PdfListFieldItemCollection ConvertToListItems(PdfLoadedListItemCollection items)
      {
        PdfListFieldItemCollection listItems = new PdfListFieldItemCollection();
        foreach (PdfLoadedListItem pdfLoadedListItem in (PdfCollection) items)
          listItems.Add(new PdfListFieldItem(pdfLoadedListItem.Text, pdfLoadedListItem.Value));
        return listItems;
      }

      internal override PdfLoadedFieldItem CreateLoadedItem(PdfDictionary dictionary)
      {
        base.CreateLoadedItem(dictionary);
        PdfLoadedListFieldItem loadedItem = new PdfLoadedListFieldItem((PdfLoadedStyledField) this, this.m_items.Count, dictionary);
        this.m_items.Add(loadedItem);
        if (this.Kids == null)
          this.Dictionary["Kids"] = (IPdfPrimitive) new PdfArray();
        this.Kids.Add((IPdfPrimitive) new PdfReferenceHolder((IPdfPrimitive) dictionary));
        return (PdfLoadedFieldItem) loadedItem;
      }

      internal override void Draw()
      {
        base.Draw();
        PdfTemplate template1 = new PdfTemplate(this.Bounds.Size);
        PdfArray kids = this.Kids;
        if (kids != null && kids.Count > 1)
        {
          for (int index = 0; index < kids.Count; ++index)
          {
            PdfLoadedFieldItem pdfLoadedFieldItem = (PdfLoadedFieldItem) this.Items[index];
            PdfTemplate template2 = new PdfTemplate(pdfLoadedFieldItem.Size);
            this.DrawListBox(template2.Graphics, pdfLoadedFieldItem);
            pdfLoadedFieldItem.Page.Graphics.DrawPdfTemplate(template2, this.Bounds.Location);
          }
        }
        else
        {
          this.DrawListBox(template1.Graphics, (PdfLoadedFieldItem) null);
          this.Page.Graphics.DrawPdfTemplate(template1, this.Bounds.Location);
        }
      }

      private void DrawListBox(PdfGraphics graphics, PdfLoadedFieldItem item)
      {
        PdfLoadedStyledField.GraphicsProperties graphicsProperties;
        this.GetGraphicsProperties(out graphicsProperties, item);
        graphicsProperties.Rect.Location = PointF.Empty;
        PaintParams paintParams = new PaintParams(graphicsProperties.Rect, graphicsProperties.BackBrush, graphicsProperties.ForeBrush, graphicsProperties.Pen, graphicsProperties.Style, graphicsProperties.BorderWidth, graphicsProperties.ShadowBrush, graphicsProperties.RotationAngle);
        PdfListFieldItemCollection listItems = this.ConvertToListItems(this.Values);
        FieldPainter.DrawListBox(graphics, paintParams, listItems, this.SelectedIndex, graphicsProperties.Font, graphicsProperties.StringFormat);
      }

      internal override float GetFontHeight(PdfFontFamily family)
      {
        PdfLoadedListItemCollection values = this.Values;
        float fontHeight = 0.0f;
        if (values.Count <= 0)
          return fontHeight;
        PdfFont pdfFont = (PdfFont) new PdfStandardFont(family, 12f);
        float num1 = pdfFont.MeasureString(values[0].Text).Width;
        int index = 1;
        for (int count = values.Count; index < count; ++index)
        {
          float width = pdfFont.MeasureString(values[index].Text).Width;
          num1 = (double) num1 > (double) width ? num1 : width;
        }
        float num2 = (float) (12.0 * ((double) this.Bounds.Size.Width - (double) (4 * this.BorderWidth))) / num1;
        return (double) num2 <= 12.0 ? num2 : 12f;
      }

      public PdfLoadedListFieldItemCollection Items => this.m_items;

      public bool MultiSelect
      {
        get => (FieldFlags.MultiSelect & this.Flags) != 0;
        set
        {
          if (value)
            this.Flags |= FieldFlags.MultiSelect;
          else
            this.Flags &= ~FieldFlags.MultiSelect;
        }
      }
    }
}
