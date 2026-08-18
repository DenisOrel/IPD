// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Parsing.PdfLoadedButtonField
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
    public class PdfLoadedButtonField : PdfLoadedStyledField
    {
      private PdfLoadedButtonItemCollection m_items;

      internal PdfLoadedButtonField(PdfDictionary dictionary, PdfCrossTable crossTable)
        : base(dictionary, crossTable)
      {
        PdfArray kids = this.Kids;
        this.m_items = new PdfLoadedButtonItemCollection();
        if (kids == null)
          return;
        for (int index = 0; index < kids.Count; ++index)
        {
          PdfDictionary dictionary1 = crossTable.GetObject(kids[index]) as PdfDictionary;
          this.m_items.Add(new PdfLoadedButtonItem((PdfLoadedStyledField) this, index, dictionary1));
        }
      }

      public void AddPrintAction()
      {
        PdfDictionary primitive = new PdfDictionary();
        primitive.SetProperty("N", (IPdfPrimitive) new PdfName("Print"));
        primitive.SetProperty("S", (IPdfPrimitive) new PdfName("Named"));
        if (this.Dictionary["Kids"] is PdfArray pdfArray)
          ((pdfArray[0] as PdfReferenceHolder).Object as PdfDictionary).SetProperty("A", (IPdfPrimitive) primitive);
        else
          this.Dictionary.SetProperty("A", (IPdfPrimitive) primitive);
      }

      private void ApplyAppearance(PdfDictionary widget, PdfLoadedFieldItem item)
      {
        if (widget != null && widget.ContainsKey("AP"))
        {
          if (!(this.CrossTable.GetObject(widget["AP"]) is PdfDictionary primitive) || !primitive.ContainsKey("N"))
            return;
          RectangleF rectangleF = item == null ? this.Bounds : item.Bounds;
          PdfTemplate wrapper1 = new PdfTemplate(rectangleF.Size);
          PdfTemplate wrapper2 = new PdfTemplate(rectangleF.Size);
          this.DrawButton(wrapper1.Graphics, item);
          this.DrawButton(wrapper2.Graphics, item);
          primitive.SetProperty("N", (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) wrapper1));
          primitive.SetProperty("D", (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) wrapper2));
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
        PdfLoadedButtonField loadedButtonField = new PdfLoadedButtonField(dictionary, crossTable);
        loadedButtonField.Page = (PdfPageBase) page;
        loadedButtonField.SetName(this.GetFieldName());
        loadedButtonField.Widget.Dictionary = this.Widget.Dictionary.Clone(crossTable) as PdfDictionary;
        return (PdfField) loadedButtonField;
      }

      internal override PdfLoadedFieldItem CreateLoadedItem(PdfDictionary dictionary)
      {
        base.CreateLoadedItem(dictionary);
        PdfLoadedButtonItem loadedItem = new PdfLoadedButtonItem((PdfLoadedStyledField) this, this.m_items.Count, dictionary);
        this.m_items.Add(loadedItem);
        if (this.Kids == null)
          this.Dictionary["Kids"] = (IPdfPrimitive) new PdfArray();
        this.Kids.Add((IPdfPrimitive) new PdfReferenceHolder((IPdfPrimitive) dictionary));
        return (PdfLoadedFieldItem) loadedItem;
      }

      internal override void Draw()
      {
        base.Draw();
        PdfArray kids = this.Kids;
        if (kids != null && kids.Count > 1)
        {
          for (int index = 0; index < kids.Count; ++index)
          {
            PdfLoadedFieldItem pdfLoadedFieldItem = (PdfLoadedFieldItem) this.Items[index];
            this.DrawButton(pdfLoadedFieldItem.Page.Graphics, pdfLoadedFieldItem);
          }
        }
        else
          this.DrawButton(this.Page.Graphics, (PdfLoadedFieldItem) null);
      }

      private void DrawButton(PdfGraphics graphics, PdfLoadedFieldItem item)
      {
        PdfLoadedStyledField.GraphicsProperties graphicsProperties;
        this.GetGraphicsProperties(out graphicsProperties, item);
        if (!this.Flatten)
          graphicsProperties.Rect.Location = new PointF(0.0f, 0.0f);
        PaintParams paintParams = new PaintParams(graphicsProperties.Rect, graphicsProperties.BackBrush, graphicsProperties.ForeBrush, graphicsProperties.Pen, graphicsProperties.Style, graphicsProperties.BorderWidth, graphicsProperties.ShadowBrush, graphicsProperties.RotationAngle);
        if (this.Flatten)
          graphicsProperties.StringFormat.Alignment = PdfTextAlignment.Center;
        FieldPainter.DrawButton(graphics, paintParams, this.Text, graphicsProperties.Font, graphicsProperties.StringFormat);
      }

      internal override float GetFontHeight(PdfFontFamily family)
      {
        float num = (float) (12.0 * ((double) this.Bounds.Size.Width - (double) (4 * this.BorderWidth))) / new PdfStandardFont(family, 12f).MeasureString(this.Text).Width;
        return (double) num <= 12.0 ? num : 12f;
      }

      private string GetText()
      {
        PdfDictionary pdfDictionary1 = this.GetWidgetAnnotation(this.Dictionary, this.CrossTable) ?? this.Dictionary;
        string text = (string) null;
        if (pdfDictionary1.ContainsKey("MK"))
        {
          PdfDictionary pdfDictionary2 = this.CrossTable.GetObject(pdfDictionary1["MK"]) as PdfDictionary;
          if (pdfDictionary2.ContainsKey("CA"))
            text = (this.CrossTable.GetObject(pdfDictionary2["CA"]) as PdfString).Value;
        }
        if (text != null)
          return text;
        if (!(this.CrossTable.GetObject(this.Dictionary["V"]) is PdfString pdfString))
          pdfString = PdfLoadedField.GetValue(this.Dictionary, this.CrossTable, "V", true) as PdfString;
        return pdfString != null ? pdfString.Value : "";
      }

      private void SetText(string value)
      {
        string str = value;
        PdfDictionary pdfDictionary1 = this.GetWidgetAnnotation(this.Dictionary, this.CrossTable) ?? this.Dictionary;
        if (pdfDictionary1.ContainsKey("MK"))
        {
          PdfDictionary pdfDictionary2 = this.CrossTable.GetObject(pdfDictionary1["MK"]) as PdfDictionary;
          pdfDictionary2.SetString("CA", str);
          pdfDictionary1.SetProperty("MK", (IPdfPrimitive) new PdfReferenceHolder((IPdfPrimitive) pdfDictionary2));
        }
        else
        {
          PdfDictionary pdfDictionary3 = new PdfDictionary();
          pdfDictionary3.SetString("CA", str);
          pdfDictionary1.SetProperty("MK", (IPdfPrimitive) new PdfReferenceHolder((IPdfPrimitive) pdfDictionary3));
        }
        this.Changed = true;
      }

      public PdfLoadedButtonItemCollection Items => this.m_items;

      public string Text
      {
        get => this.GetText();
        set
        {
          if ((FieldFlags.ReadOnly & this.Flags) != FieldFlags.Default)
            return;
          this.Form.SetAppearanceDictionary = true;
          this.SetText(value);
        }
      }
    }
}
