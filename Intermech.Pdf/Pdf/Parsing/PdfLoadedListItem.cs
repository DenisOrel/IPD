// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Parsing.PdfLoadedListItem
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.IO;
using Syncfusion.Pdf.Primitives;
using System;


namespace Syncfusion.Pdf.Parsing
{
    public class PdfLoadedListItem
    {
      private PdfCrossTable m_crossTable;
      private PdfLoadedChoiceField m_field;
      private string m_text;
      private string m_value;

      public PdfLoadedListItem(string text, string value)
      {
        this.m_text = text != null ? text : throw new ArgumentNullException(nameof (text));
        this.m_value = value;
      }

      internal PdfLoadedListItem(
        string text,
        string value,
        PdfLoadedChoiceField field,
        PdfCrossTable cTable)
        : this(text, value)
      {
        this.m_field = field;
        this.m_crossTable = cTable;
      }

      private void SetText(string value)
      {
        if (value == null)
          throw new ArgumentNullException("text");
        if (!(this.m_text != value))
          return;
        PdfDictionary dictionary = this.m_field.Dictionary;
        if (!dictionary.ContainsKey("Opt"))
          return;
        PdfArray primitive = this.m_crossTable.GetObject(dictionary["Opt"]) as PdfArray;
        PdfArray element = new PdfArray();
        element.Add((IPdfPrimitive) new PdfString(this.m_value));
        element.Add((IPdfPrimitive) new PdfString(value));
        int index = 0;
        for (int count = primitive.Count; index < count; ++index)
        {
          if ((this.m_crossTable.GetObject((this.m_crossTable.GetObject(primitive[index]) as PdfArray)[1]) as PdfString).Value == this.m_text)
          {
            this.m_text = value;
            primitive.RemoveAt(index);
            primitive.Insert(index, (IPdfPrimitive) element);
          }
        }
        dictionary.SetProperty("Opt", (IPdfPrimitive) primitive);
        this.m_field.Changed = true;
      }

      private void SetValue(string value)
      {
        if (value == null)
          throw new ArgumentNullException(nameof (value));
        if (!(this.m_value != value))
          return;
        PdfDictionary dictionary = this.m_field.Dictionary;
        if (!dictionary.ContainsKey("Opt"))
          return;
        PdfArray primitive = this.m_crossTable.GetObject(dictionary["Opt"]) as PdfArray;
        PdfArray element = new PdfArray();
        element.Add((IPdfPrimitive) new PdfString(value));
        element.Add((IPdfPrimitive) new PdfString(this.m_text));
        int index = 0;
        for (int count = primitive.Count; index < count; ++index)
        {
          if ((this.m_crossTable.GetObject((this.m_crossTable.GetObject(primitive[index]) as PdfArray)[1]) as PdfString).Value == this.m_value)
          {
            this.m_value = value;
            primitive.RemoveAt(index);
            primitive.Insert(index, (IPdfPrimitive) element);
          }
        }
        dictionary.SetProperty("Opt", (IPdfPrimitive) primitive);
        this.m_field.Changed = true;
      }

      public string Text
      {
        get => this.m_text;
        set => this.SetText(value);
      }

      public string Value
      {
        get => this.m_value ?? this.Text;
        set => this.SetValue(value);
      }
    }
}
