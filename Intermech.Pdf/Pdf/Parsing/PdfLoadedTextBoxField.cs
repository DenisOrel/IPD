// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Parsing.PdfLoadedTextBoxField
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Interactive;
using Syncfusion.Pdf.IO;
using Syncfusion.Pdf.Native;
using Syncfusion.Pdf.Primitives;
using System;
using System.Drawing;


namespace Syncfusion.Pdf.Parsing
{
    public class PdfLoadedTextBoxField : PdfLoadedStyledField
    {
      private PdfColor m_foreColor;
      private PdfLoadedTextBoxItemCollection m_items;
      private const string m_passwordValue = "*";

      internal PdfLoadedTextBoxField(PdfDictionary dictionary, PdfCrossTable crossTable)
        : base(dictionary, crossTable)
      {
        this.m_foreColor = new PdfColor((byte) 0, (byte) 0, (byte) 0);
        PdfArray kids = this.Kids;
        this.m_items = new PdfLoadedTextBoxItemCollection();
        if (kids == null)
          return;
        for (int index = 0; index < kids.Count; ++index)
        {
          PdfDictionary dictionary1 = crossTable.GetObject(kids[index]) as PdfDictionary;
          this.m_items.Add(new PdfLoadedTexBoxItem((PdfLoadedStyledField) this, index, dictionary1));
        }
      }

      private void ApplyAppearance(PdfDictionary widget, PdfLoadedFieldItem item)
      {
        bool needAppearances = this.Form.NeedAppearances;
        if (!this.Form.SetAppearanceDictionary)
          return;
        if (widget != null && !needAppearances)
        {
          PdfDictionary pdfDictionary = this.CrossTable.GetObject(widget["AP"]) as PdfDictionary;
          PdfDictionary primitive = new PdfDictionary();
          PdfTemplate wrapper = new PdfTemplate((item == null ? this.Bounds : item.Bounds).Size, false);
          wrapper.Graphics.StreamWriter.BeginMarkupSequence("Tx");
          wrapper.Graphics.InitializeCoordinates();
          this.DrawTextBox(wrapper.Graphics, item);
          wrapper.Graphics.StreamWriter.EndMarkupSequence();
          primitive.SetProperty("N", (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) wrapper));
          widget.SetProperty("AP", (IPdfPrimitive) primitive);
        }
        else
          this.Form.NeedAppearances = true;
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
        PdfLoadedTextBoxField loadedTextBoxField = new PdfLoadedTextBoxField(dictionary, crossTable);
        loadedTextBoxField.Page = (PdfPageBase) page;
        loadedTextBoxField.SetName(this.GetFieldName());
        loadedTextBoxField.Widget.Dictionary = this.Widget.Dictionary.Clone(crossTable) as PdfDictionary;
        return (PdfField) loadedTextBoxField;
      }

      internal override PdfLoadedFieldItem CreateLoadedItem(PdfDictionary dictionary)
      {
        base.CreateLoadedItem(dictionary);
        PdfLoadedTexBoxItem loadedItem = new PdfLoadedTexBoxItem((PdfLoadedStyledField) this, this.m_items.Count, dictionary);
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
        if (kids != null)
        {
          for (int index1 = 0; index1 < kids.Count; ++index1)
          {
            PdfLoadedFieldItem pdfLoadedFieldItem = (PdfLoadedFieldItem) this.Items[index1];
            if (this.Page is PdfLoadedPage)
              this.DrawTextBox(pdfLoadedFieldItem.Page.Graphics, pdfLoadedFieldItem);
            else if ((((kids[index1] as PdfReferenceHolder).Object as PdfDictionary)["P"] as PdfReferenceHolder).Reference == (PdfReference) null)
            {
              this.DrawTextBox(((this.Page as PdfPage).Section.ParentDocument.EnableMemoryOptimization ? pdfLoadedFieldItem.Page : this.Page).Graphics, pdfLoadedFieldItem);
            }
            else
            {
              PdfPageBase page = this.Form.m_pageMap[(((kids[index1] as PdfReferenceHolder).Object as PdfDictionary)["P"] as PdfReferenceHolder).Object as PdfDictionary];
              PdfArray pdfArray = page.Dictionary["Annots"] as PdfArray;
              int count = pdfArray.Count;
              for (int index2 = 0; index2 < count - 1; ++index2)
              {
                if ((object) (pdfArray[index2] as PdfReferenceHolder) != null)
                {
                  PdfDictionary pdfDictionary = (pdfArray[index2] as PdfReferenceHolder).Object as PdfDictionary;
                  if (pdfDictionary.ContainsKey("Parent") && (object) (pdfDictionary["Parent"] as PdfReferenceHolder) != null && (((pdfDictionary["Parent"] as PdfReferenceHolder).Object as PdfDictionary)["T"] as PdfString).Value == this.Name)
                    pdfArray.RemoveAt(index2);
                }
              }
              this.DrawTextBox(page.Graphics, pdfLoadedFieldItem);
            }
          }
        }
        else
          this.DrawTextBox(this.Page.Graphics, (PdfLoadedFieldItem) null);
      }

      private void DrawTextBox(PdfGraphics graphics, PdfLoadedFieldItem item)
      {
        PdfLoadedStyledField.GraphicsProperties graphicsProperties;
        this.GetGraphicsProperties(out graphicsProperties, item);
        if (!this.Flatten)
          graphicsProperties.Rect.Location = new PointF(0.0f, 0.0f);
        string str = this.Text;
        if (this.Password)
        {
          str = string.Empty;
          for (int index = 0; index < this.Text.Length; ++index)
            str += "*";
        }
        ushort[] numArray = new ushort[str.Length];
        KernelApi.GetStringTypeExW(2048U /*0x0800*/, StringInfoType.CT_TYPE2, str, str.Length, numArray);
        graphicsProperties.StringFormat.RightToLeft = this.IsRTLText(numArray);
        graphicsProperties.StringFormat.LineLimit = false;
        if (!this.Multiline)
        {
          graphicsProperties.StringFormat.LineAlignment = PdfVerticalAlignment.Middle;
          graphicsProperties.StringFormat.WordWrap = PdfWordWrapType.None;
        }
        if (!this.Multiline && this.Flatten)
        {
          graphicsProperties.StringFormat.WordWrap = PdfWordWrapType.Character;
          if ((double) this.Font.Height < (double) graphicsProperties.Rect.Height)
            graphicsProperties.StringFormat.LineLimit = true;
        }
        PaintParams paintParams = new PaintParams(graphicsProperties.Rect, graphicsProperties.BackBrush, graphicsProperties.ForeBrush, graphicsProperties.Pen, graphicsProperties.Style, graphicsProperties.BorderWidth, graphicsProperties.ShadowBrush, graphicsProperties.RotationAngle);
        if (graphicsProperties.Font.Name.Equals("TimesLTStd-Roman") && graphicsProperties.Font is PdfStandardFont font)
          graphicsProperties.Font = (PdfFont) new PdfStandardFont(font.FontFamily, graphicsProperties.Font.Size, graphicsProperties.Font.Style);
        if (!this.Dictionary.ContainsKey("Rect") && !this.Dictionary.ContainsKey("Kids"))
          return;
        FieldPainter.DrawTextBox(graphics, paintParams, str, graphicsProperties.Font, graphicsProperties.StringFormat, this.Multiline, this.Scrollable);
      }

      internal override float GetFontHeight(PdfFontFamily family)
      {
        PdfStandardFont pdfStandardFont = new PdfStandardFont(family, 12f);
        if (this.Multiline)
          return 12.5f;
        SizeF sizeF = pdfStandardFont.MeasureString(this.Text);
        float width = sizeF.Width;
        sizeF = this.Bounds.Size;
        float num = (float) (8.0 * ((double) sizeF.Width - (double) (4 * this.BorderWidth))) / width;
        return (double) num <= 8.0 ? num : 8f;
      }

      private PdfHighlightMode GetHighlightModeFromString(PdfName hightlightMode)
      {
        switch (hightlightMode.Value)
        {
          case "P":
            return PdfHighlightMode.Push;
          case "N":
            return PdfHighlightMode.NoHighlighting;
          case "O":
            return PdfHighlightMode.Outline;
          default:
            return PdfHighlightMode.Invert;
        }
      }

      private string HighlightModeToString(PdfHighlightMode m_highlightingMode)
      {
        switch (m_highlightingMode)
        {
          case PdfHighlightMode.NoHighlighting:
            return "N";
          case PdfHighlightMode.Outline:
            return "O";
          case PdfHighlightMode.Push:
            return "P";
          default:
            return "I";
        }
      }

      private bool IsRTLText(ushort[] characterCodes)
      {
        int index = 0;
        for (int length = characterCodes.Length; index < length; ++index)
        {
          if (characterCodes[index] == (ushort) 2 || characterCodes[index] == (ushort) 6)
            return true;
        }
        return false;
      }

      private void SetBackColor(PdfColor value)
      {
        PdfDictionary widgetAnnotation = this.GetWidgetAnnotation(this.Dictionary, this.CrossTable);
        if (widgetAnnotation == null)
          return;
        if (widgetAnnotation.ContainsKey("MK"))
        {
          (this.CrossTable.GetObject(widgetAnnotation["MK"]) as PdfDictionary)["BG"] = (IPdfPrimitive) value.ToArray();
        }
        else
        {
          PdfDictionary pdfDictionary = new PdfDictionary();
          PdfArray array = value.ToArray();
          pdfDictionary["BG"] = (IPdfPrimitive) array;
          widgetAnnotation["MK"] = (IPdfPrimitive) pdfDictionary;
        }
        this.Form.SetAppearanceDictionary = true;
      }

      public PdfColor BackColor
      {
        get => this.GetBackColor();
        set => this.SetBackColor(value);
      }

      public string DefaultValue
      {
        get
        {
          string defaultValue = (string) null;
          if (PdfLoadedField.GetValue(this.Dictionary, this.CrossTable, "DV", true) is PdfString pdfString)
            defaultValue = pdfString.Value;
          return defaultValue;
        }
        set
        {
          if (value == null)
            throw new ArgumentNullException(nameof (DefaultValue));
          this.Dictionary.SetString("DV", value);
          this.Changed = true;
        }
      }

      public PdfColor ForeColor
      {
        get
        {
          PdfDictionary widgetAnnotation = this.GetWidgetAnnotation(this.Dictionary, this.CrossTable);
          if (widgetAnnotation != null && widgetAnnotation.ContainsKey("DA"))
            this.m_foreColor = this.GetForeColour((this.CrossTable.GetObject(widgetAnnotation["DA"]) as PdfString).Value);
          return this.m_foreColor;
        }
        set
        {
          PdfDictionary widgetAnnotation = this.GetWidgetAnnotation(this.Dictionary, this.CrossTable);
          float height = 0.0f;
          string str = (string) null;
          if (widgetAnnotation != null && widgetAnnotation.ContainsKey("DA"))
          {
            PdfDictionary pdfDictionary = this.CrossTable.GetObject(this.Form.Resources["Font"]) as PdfDictionary;
            str = this.FontName((widgetAnnotation["DA"] as PdfString).Value, out height);
            string key = str;
            PdfReferenceHolder pdfReferenceHolder = pdfDictionary[key] as PdfReferenceHolder;
            if (pdfReferenceHolder != (PdfReferenceHolder) null)
            {
              IPdfPrimitive pdfPrimitive = pdfReferenceHolder.Object;
            }
          }
          else if (widgetAnnotation != null && this.Dictionary.ContainsKey("DA"))
          {
            PdfDictionary pdfDictionary = this.CrossTable.GetObject(this.Form.Resources["Font"]) as PdfDictionary;
            str = this.FontName((this.Dictionary["DA"] as PdfString).Value, out height);
            string key = str;
            IPdfPrimitive pdfPrimitive = (pdfDictionary[key] as PdfReferenceHolder).Object;
          }
          if (str != null)
            widgetAnnotation["DA"] = (IPdfPrimitive) new PdfString(new PdfDefaultAppearance()
            {
              FontName = str,
              FontSize = height,
              ForeColor = value
            }.ToString());
          else
            widgetAnnotation["DA"] = (IPdfPrimitive) new PdfString(new PdfDefaultAppearance()
            {
              FontName = this.Font.Name,
              FontSize = this.Font.Size,
              ForeColor = value
            }.ToString());
          this.Form.SetAppearanceDictionary = true;
        }
      }

      public PdfHighlightMode HighlightMode
      {
        get
        {
          PdfDictionary widgetAnnotation = this.GetWidgetAnnotation(this.Dictionary, this.CrossTable);
          PdfHighlightMode highlightMode = PdfHighlightMode.NoHighlighting;
          if (widgetAnnotation.ContainsKey("H"))
            highlightMode = this.GetHighlightModeFromString(this.CrossTable.GetObject(widgetAnnotation["H"]) as PdfName);
          return highlightMode;
        }
        set
        {
          this.GetWidgetAnnotation(this.Dictionary, this.CrossTable)["H"] = (IPdfPrimitive) new PdfName(this.HighlightModeToString(value));
        }
      }

      public bool InsertSpaces
      {
        get => (FieldFlags.Comb & this.Flags) != 0;
        set
        {
          if (value)
            this.Flags |= FieldFlags.Comb;
          else
            this.Flags &= ~FieldFlags.Comb;
        }
      }

      public PdfLoadedTextBoxItemCollection Items => this.m_items;

      public int MaxLength
      {
        get
        {
          int maxLength = 0;
          if (PdfLoadedField.GetValue(this.Dictionary, this.CrossTable, "MaxLen", true) is PdfNumber pdfNumber)
            maxLength = pdfNumber.IntValue;
          return maxLength;
        }
        set
        {
          this.Dictionary.SetNumber("MaxLen", value);
          this.Changed = true;
        }
      }

      public bool Multiline
      {
        get => (FieldFlags.Multiline & this.Flags) != 0;
        set
        {
          if (value)
            this.Flags |= FieldFlags.Multiline;
          else
            this.Flags &= ~FieldFlags.Multiline;
        }
      }

      public bool Password
      {
        get => (FieldFlags.Password & this.Flags) != 0;
        set
        {
          if (value)
            this.Flags |= FieldFlags.Password;
          else
            this.Flags &= ~FieldFlags.Password;
        }
      }

      public bool Scrollable
      {
        get => (FieldFlags.DoNotScroll & this.Flags) == FieldFlags.Default;
        set
        {
          if (value)
            this.Flags &= ~FieldFlags.DoNotScroll;
          else
            this.Flags |= FieldFlags.DoNotScroll;
        }
      }

      public bool SpellCheck
      {
        get => (FieldFlags.DoNotSpellCheck & this.Flags) == FieldFlags.Default;
        set
        {
          if (value)
            this.Flags &= ~FieldFlags.DoNotSpellCheck;
          else
            this.Flags |= FieldFlags.DoNotSpellCheck;
        }
      }

      public string Text
      {
        get
        {
          return PdfLoadedField.GetValue(this.Dictionary, this.CrossTable, "V", true) is PdfString pdfString ? pdfString.Value : string.Empty;
        }
        set
        {
          if ((FieldFlags.ReadOnly & this.Flags) == FieldFlags.Default)
          {
            if (value == null)
              throw new ArgumentNullException("text");
            this.Dictionary.SetProperty("V", (IPdfPrimitive) new PdfString(value));
            this.CrossTable.GetObject(this.GetWidgetAnnotation(this.Dictionary, this.CrossTable)["MK"]);
            this.Changed = true;
            this.Form.SetAppearanceDictionary = true;
          }
          else
            this.Changed = false;
        }
      }

      public PdfTextAlignment TextAlignment
      {
        get
        {
          PdfDictionary widgetAnnotation = this.GetWidgetAnnotation(this.Dictionary, this.CrossTable);
          PdfTextAlignment textAlignment = PdfTextAlignment.Left;
          if (widgetAnnotation.ContainsKey("Q"))
            textAlignment = (PdfTextAlignment) Enum.ToObject(typeof (PdfTextAlignment), (widgetAnnotation["Q"] as PdfNumber).IntValue);
          return textAlignment;
        }
        set
        {
          this.GetWidgetAnnotation(this.Dictionary, this.CrossTable).SetProperty("Q", (IPdfPrimitive) new PdfNumber((int) value));
          this.Form.SetAppearanceDictionary = true;
        }
      }
    }
}
