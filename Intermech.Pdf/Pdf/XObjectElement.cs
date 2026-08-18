// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.XObjectElement
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.IO;
using Syncfusion.Pdf.Primitives;
using System.Collections.Generic;
using System.Drawing.Drawing2D;


namespace Syncfusion.Pdf
{
    internal class XObjectElement
    {
      private PdfMatrix m_imageInfo;
      private string m_objectName;
      private string m_objectType;
      private PdfDictionary m_xObjectDictionary;

      public XObjectElement(PdfDictionary xobjectDictionary, string name)
      {
        this.m_xObjectDictionary = xobjectDictionary;
        this.m_objectName = name;
        this.GetObjectType();
      }

      public XObjectElement(PdfDictionary xobjectDictionary, string name, PdfMatrix tm)
      {
        this.m_xObjectDictionary = xobjectDictionary;
        this.m_objectName = name;
        this.ImageInfo = tm;
        this.GetObjectType();
      }

      private void GetObjectType()
      {
        if (!this.m_xObjectDictionary.ContainsKey("Subtype"))
          return;
        this.m_objectType = (this.m_xObjectDictionary["Subtype"] as PdfName).Value;
      }

      public PdfRecordCollection Render(PdfPageResources resources, Stack<GraphicsState> graphicsStates)
      {
        if (!(this.ObjectType == "Form"))
          return (PdfRecordCollection) null;
        PdfStream objectDictionary = this.m_xObjectDictionary as PdfStream;
        objectDictionary.Decompress();
        return new ContentParser(objectDictionary.InternalStream.ToArray()).ReadContent();
      }

      internal PdfMatrix ImageInfo
      {
        get => this.m_imageInfo;
        set => this.m_imageInfo = value;
      }

      internal string ObjectName
      {
        get => this.m_objectName;
        set => this.m_objectName = value;
      }

      internal string ObjectType
      {
        get => this.m_objectType;
        set => this.m_objectType = value;
      }

      internal PdfDictionary XObjectDictionary
      {
        get => this.m_xObjectDictionary;
        set => this.m_xObjectDictionary = value;
      }
    }
}
