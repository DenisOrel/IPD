// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfLoadedAnnotation
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.IO;
using Syncfusion.Pdf.Primitives;
using System;
using System.IO;
using System.Text;


namespace Syncfusion.Pdf.Interactive
{
    public abstract class PdfLoadedAnnotation : PdfAnnotation
    {
      private bool m_Changed;
      private PdfCrossTable m_crossTable;
      private int m_defaultIndex;
      private string m_fileName;
      private PdfLoadedPage m_loadedpage;
      public int ObjectID;

      internal event PdfLoadedAnnotation.BeforeNameChangesEventHandler BeforeNameChanges;

      internal PdfLoadedAnnotation(PdfDictionary dictionary, PdfCrossTable crossTable)
      {
        if (dictionary == null)
          throw new ArgumentNullException(nameof (dictionary));
        if (crossTable == null)
          throw new ArgumentNullException(nameof (crossTable));
        this.Dictionary = dictionary;
        this.m_crossTable = crossTable;
      }

      internal override void ApplyText(string text) => this.SetText(text);

      internal virtual void BeginSave()
      {
      }

      internal void ExportText(Stream stream, ref int objectid)
      {
        bool flag = false;
        pdfArray = (PdfArray) null;
        if (this.Dictionary.ContainsKey("Kids") && this.CrossTable.GetObject(this.Dictionary["Kids"]) is PdfArray pdfArray)
        {
          for (int index = 0; index < pdfArray.Count; ++index)
            flag = flag || pdfArray[index] is PdfLoadedAnnotation;
        }
        PdfString pdfString = PdfLoadedAnnotation.GetValue(this.Dictionary, this.CrossTable, "Contents", true) as PdfString;
        string text1 = "";
        if (pdfString != null)
          text1 = pdfString.Value;
        if (!(!PdfLoadedAnnotation.validateString(text1) | flag))
          return;
        if (flag)
        {
          for (int index = 0; index < pdfArray.Count; ++index)
          {
            if (pdfArray[index] is PdfLoadedAnnotation loadedAnnotation)
              loadedAnnotation.ExportText(stream, ref objectid);
          }
          this.ObjectID = objectid;
          ++objectid;
          StringBuilder stringBuilder = new StringBuilder();
          byte[] bytes1 = Encoding.GetEncoding("windows-1252").GetBytes(new PdfString(text1)
          {
            Encode = PdfString.ForceEncoding.ASCII
          }.Value);
          stringBuilder.AppendFormat("{0} 0 obj<</T <{1}> /Kids [", (object) this.ObjectID, (object) PdfString.BytesToHex(bytes1));
          for (int index = 0; index < pdfArray.Count; ++index)
          {
            if (pdfArray[index] is PdfLoadedAnnotation loadedAnnotation && loadedAnnotation.ObjectID != 0)
              stringBuilder.AppendFormat("{0} 0 R ", (object) loadedAnnotation.ObjectID);
          }
          stringBuilder.Append("]>>endobj\n");
          byte[] bytes2 = Encoding.GetEncoding("windows-1252").GetBytes(new PdfString(stringBuilder.ToString())
          {
            Encode = PdfString.ForceEncoding.ASCII
          }.Value);
          stream.Write(bytes2, 0, bytes2.Length);
        }
        else
        {
          this.ObjectID = objectid;
          ++objectid;
          string str;
          if (this.GetType().Name == "PdfLoadedCheckBoxField" || this.GetType().Name == "PdfLoadedRadioButtonListField")
            str = "/" + text1;
          else
            str = $"<{PdfString.BytesToHex(Encoding.GetEncoding("windows-1252").GetBytes(new PdfString(text1)
            {
              Encode = PdfString.ForceEncoding.ASCII
            }.Value))}>";
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.AppendFormat("{0} 0 obj<</T <{1}> /Contents {2} >>endobj\n", (object) this.ObjectID, (object) PdfString.BytesToHex(Encoding.GetEncoding("windows-1252").GetBytes(new PdfString(this.Text)
          {
            Encode = PdfString.ForceEncoding.ASCII
          }.Value)), (object) str);
          byte[] bytes = Encoding.GetEncoding("windows-1252").GetBytes(new PdfString(stringBuilder.ToString())
          {
            Encode = PdfString.ForceEncoding.ASCII
          }.Value);
          stream.Write(bytes, 0, bytes.Length);
        }
      }

      internal static IPdfPrimitive GetValue(
        PdfDictionary dictionary,
        PdfCrossTable crossTable,
        string value,
        bool inheritable)
      {
        IPdfPrimitive pdfPrimitive = (IPdfPrimitive) null;
        if (dictionary.ContainsKey(value))
          return crossTable.GetObject(dictionary[value]);
        if (inheritable)
          pdfPrimitive = PdfLoadedAnnotation.SearchInParents(dictionary, crossTable, value);
        return pdfPrimitive;
      }

      internal PdfDictionary GetWidgetAnnotation(PdfDictionary dictionary, PdfCrossTable crossTable)
      {
        PdfDictionary widgetAnnotation = (PdfDictionary) null;
        if (dictionary.ContainsKey("Kids"))
        {
          PdfArray pdfArray = crossTable.GetObject(dictionary["Kids"]) as PdfArray;
          PdfReference reference = crossTable.GetReference(pdfArray[this.m_defaultIndex]);
          widgetAnnotation = crossTable.GetObject((IPdfPrimitive) reference) as PdfDictionary;
        }
        if (dictionary.ContainsKey("Subtype") && (this.CrossTable.GetObject(dictionary["Subtype"]) as PdfName).Value == "Widget")
          widgetAnnotation = dictionary;
        return widgetAnnotation;
      }

      internal static IPdfPrimitive SearchInParents(
        PdfDictionary dictionary,
        PdfCrossTable crossTable,
        string value)
      {
        IPdfPrimitive pdfPrimitive = (IPdfPrimitive) null;
        PdfDictionary pdfDictionary = dictionary;
        while (pdfPrimitive == null && pdfDictionary != null)
        {
          if (pdfDictionary.ContainsKey(value))
            pdfPrimitive = crossTable.GetObject(pdfDictionary[value]);
          else
            pdfDictionary = !pdfDictionary.ContainsKey("Parent") ? (PdfDictionary) null : crossTable.GetObject(pdfDictionary["Parent"]) as PdfDictionary;
        }
        return pdfPrimitive;
      }

      public void SetText(string text)
      {
        if (text == null)
          throw new ArgumentNullException(nameof (text));
        if (text == string.Empty)
          throw new ArgumentException("The text can't be empty");
        if (!(this.Text != text))
          return;
        PdfString pdfString = new PdfString(text);
        this.Dictionary.SetString("T", text);
        this.Changed = true;
      }

      internal static bool validateString(string text1) => text1 == null || text1.Length == 0;

      internal bool Changed
      {
        get => this.m_Changed;
        set => this.m_Changed = value;
      }

      internal PdfCrossTable CrossTable
      {
        get => this.m_crossTable;
        set
        {
          if (value == null)
            throw new ArgumentNullException(nameof (CrossTable));
          if (this.m_crossTable == value)
            return;
          this.m_crossTable = value;
        }
      }

      public PdfLoadedPage Page
      {
        get => this.m_loadedpage;
        set => this.m_loadedpage = value;
      }

      internal delegate void BeforeNameChangesEventHandler(string name);
    }
}
