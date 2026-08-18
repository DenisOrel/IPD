// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Parsing.PdfLoadedStateField
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Interactive;
using Syncfusion.Pdf.IO;
using Syncfusion.Pdf.Primitives;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

#nullable disable
namespace Syncfusion.Pdf.Parsing;

public abstract class PdfLoadedStateField : PdfLoadedStyledField
{
  private bool m_bUnchecking;
  private PdfLoadedStateItemCollection m_items;

  internal PdfLoadedStateField(
    PdfDictionary dictionary,
    PdfCrossTable crossTable,
    PdfLoadedStateItemCollection items)
    : base(dictionary, crossTable)
  {
    if (crossTable == null)
      throw new ArgumentNullException(nameof (crossTable));
    if (dictionary == null)
      throw new ArgumentNullException(nameof (dictionary));
    if (items == null)
      throw new ArgumentNullException(nameof (items));
    PdfArray kids = this.Kids;
    this.m_items = items;
    if (kids != null)
    {
      for (int index = 0; index < kids.Count; ++index)
      {
        PdfDictionary itemDictionary = crossTable.GetObject(kids[index]) as PdfDictionary;
        this.m_items.Add(this.GetItem(index, itemDictionary));
      }
    }
    else
    {
      PdfLoadedStateItem pdfLoadedStateItem = this.GetItem(0, dictionary);
      if (pdfLoadedStateItem is PdfLoadedRadioButtonItem)
      {
        if (!((pdfLoadedStateItem as PdfLoadedRadioButtonItem).Value != ""))
          return;
        this.m_items.Add(pdfLoadedStateItem);
      }
      else
        this.m_items.Add(pdfLoadedStateItem);
    }
  }

  internal void ApplyAppearance(PdfDictionary widget, PdfLoadedCheckBoxItem item)
  {
    if (widget != null && widget.ContainsKey("AP"))
    {
      if (this.CrossTable.GetObject(widget["AP"]) is PdfDictionary primitive && primitive.ContainsKey("N"))
      {
        string empty = string.Empty;
        string key = item == null ? PdfLoadedStateField.GetItemValue(this.Dictionary, this.CrossTable) : PdfLoadedStateField.GetItemValue(item.Dictionary, item.CrossTable);
        RectangleF rectangleF = item == null ? this.Bounds : item.Bounds;
        if (!(PdfCrossTable.Dereference(primitive["N"]) is PdfDictionary))
        {
          PdfDictionary pdfDictionary = new PdfDictionary();
          PdfTemplate wrapper1 = new PdfTemplate(rectangleF.Size);
          PdfTemplate wrapper2 = new PdfTemplate(rectangleF.Size);
          this.DrawStateItem(wrapper1.Graphics, PdfCheckFieldState.Checked, (PdfLoadedStateItem) item);
          this.DrawStateItem(wrapper2.Graphics, PdfCheckFieldState.Unchecked, (PdfLoadedStateItem) item);
          pdfDictionary.SetProperty("Off", (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) wrapper2));
          pdfDictionary.SetProperty(key, (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) wrapper1));
          primitive["N"] = (IPdfPrimitive) new PdfReferenceHolder((IPdfPrimitive) pdfDictionary);
        }
        if (!(PdfCrossTable.Dereference(primitive["D"]) is PdfDictionary pdfDictionary1))
        {
          PdfTemplate wrapper3 = new PdfTemplate(rectangleF.Size);
          PdfTemplate wrapper4 = new PdfTemplate(rectangleF.Size);
          this.DrawStateItem(wrapper3.Graphics, PdfCheckFieldState.PressedChecked, (PdfLoadedStateItem) item);
          this.DrawStateItem(wrapper4.Graphics, PdfCheckFieldState.PressedUnchecked, (PdfLoadedStateItem) item);
          if (pdfDictionary1 != null)
          {
            pdfDictionary1.SetProperty("Off", (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) wrapper4));
            pdfDictionary1.SetProperty(key, (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) wrapper3));
            primitive["D"] = (IPdfPrimitive) new PdfReferenceHolder((IPdfPrimitive) pdfDictionary1);
          }
        }
      }
      widget.SetProperty("AP", (IPdfPrimitive) primitive);
    }
    else
    {
      if (!this.Form.SetAppearanceDictionary)
        return;
      this.Form.NeedAppearances = true;
    }
  }

  internal void DrawStateItem(
    PdfGraphics graphics,
    PdfCheckFieldState state,
    PdfLoadedStateItem item)
  {
    PdfLoadedStyledField.GraphicsProperties graphicsProperties;
    this.GetGraphicsProperties(out graphicsProperties, (PdfLoadedFieldItem) item);
    if (!this.Flatten)
      graphicsProperties.Rect.Location = PointF.Empty;
    PaintParams paintParams = new PaintParams(graphicsProperties.Rect, graphicsProperties.BackBrush, graphicsProperties.ForeBrush, graphicsProperties.Pen, graphicsProperties.Style, graphicsProperties.BorderWidth, graphicsProperties.ShadowBrush, graphicsProperties.RotationAngle);
    graphics.StreamWriter.SetTextRenderingMode(TextRenderingMode.Fill);
    PdfTemplate stateTemplate = this.GetStateTemplate(state, item);
    if (stateTemplate == null)
      return;
    RectangleF rectangleF = item == null ? this.Bounds : item.Bounds;
    graphics.DrawPdfTemplate(stateTemplate, rectangleF.Location);
  }

  internal abstract PdfLoadedStateItem GetItem(int index, PdfDictionary itemDictionary);

  internal static string GetItemValue(PdfDictionary dictionary, PdfCrossTable crossTable)
  {
    string empty = string.Empty;
    if (dictionary.ContainsKey("AS"))
    {
      PdfName pdfName = crossTable.GetObject(dictionary["AS"]) as PdfName;
      if (pdfName != (PdfName) null && pdfName.Value != "Off")
        empty = pdfName.Value;
    }
    if (empty == string.Empty && dictionary.ContainsKey("AP"))
    {
      PdfDictionary pdfDictionary1 = crossTable.GetObject(dictionary["AP"]) as PdfDictionary;
      if (!pdfDictionary1.ContainsKey("N"))
        return empty;
      PdfReference reference = crossTable.GetReference(pdfDictionary1["N"]);
      PdfDictionary pdfDictionary2 = crossTable.GetObject((IPdfPrimitive) reference) as PdfDictionary;
      List<object> objectList = new List<object>();
      foreach (PdfName key in (IEnumerable) pdfDictionary2.Keys)
        objectList.Add((object) key);
      int index = 0;
      for (int count = objectList.Count; index < count; ++index)
      {
        PdfName pdfName = objectList[index] as PdfName;
        if (pdfName.Value != "Off")
          return pdfName.Value;
      }
    }
    return empty;
  }

  private PdfTemplate GetStateTemplate(PdfCheckFieldState state, PdfLoadedStateItem item)
  {
    PdfDictionary dictionary = item != null ? item.Dictionary : this.Dictionary;
    string key = state == PdfCheckFieldState.Checked ? PdfLoadedStateField.GetItemValue(dictionary, this.CrossTable) : "Off";
    PdfTemplate stateTemplate = (PdfTemplate) null;
    if (dictionary.ContainsKey("AP") && PdfCrossTable.Dereference((PdfCrossTable.Dereference((PdfCrossTable.Dereference(dictionary["AP"]) as PdfDictionary)["N"]) as PdfDictionary)[key]) is PdfStream template)
      stateTemplate = new PdfTemplate(template);
    return stateTemplate;
  }

  protected void SetCheckedStatus(bool value)
  {
    if (value)
    {
      string itemValue = PdfLoadedStateField.GetItemValue(this.Dictionary, this.CrossTable);
      this.Dictionary.SetName("V", itemValue);
      this.Dictionary.SetProperty("AS", (IPdfPrimitive) new PdfName(itemValue));
    }
    else
    {
      this.Dictionary.Remove("V");
      this.Dictionary.SetProperty("AS", (IPdfPrimitive) new PdfName("Off"));
    }
    this.Changed = true;
  }

  internal void UncheckOthers(PdfLoadedStateItem child, string value, bool check)
  {
    if (this.m_bUnchecking)
      return;
    this.m_bUnchecking = true;
    int index = 0;
    for (int count = this.Items.Count; index < count; ++index)
    {
      PdfLoadedStateItem pdfLoadedStateItem = this.Items[index];
      if (pdfLoadedStateItem != child)
      {
        bool flag = PdfLoadedStateField.GetItemValue(pdfLoadedStateItem.Dictionary, this.CrossTable) == value;
        pdfLoadedStateItem.Checked = flag & check;
      }
    }
    this.m_bUnchecking = false;
  }

  public PdfLoadedStateItemCollection Items => this.m_items;
}
