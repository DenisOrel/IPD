// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Primitives.PdfBoolean
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.IO;


namespace Syncfusion.Pdf.Primitives
{
    internal class PdfBoolean : IPdfPrimitive
    {
      private int m_index;
      private bool m_isSaving;
      private int m_position;
      private ObjectStatus m_status;
      private bool m_value;

      internal PdfBoolean() => this.m_position = -1;

      internal PdfBoolean(bool value)
      {
        this.m_position = -1;
        this.m_value = value;
      }

      private string BoolToStr(bool value) => !value ? "false" : "true";

      public IPdfPrimitive Clone(PdfCrossTable crossTable)
      {
        return (IPdfPrimitive) new PdfBoolean(this.m_value);
      }

      public void Save(IPdfWriter writer) => writer.Write(this.BoolToStr(this.m_value));

      public IPdfPrimitive ClonedObject => (IPdfPrimitive) null;

      public bool IsSaving
      {
        get => this.m_isSaving;
        set => this.m_isSaving = value;
      }

      public int ObjectCollectionIndex
      {
        get => this.m_index;
        set => this.m_index = value;
      }

      public int Position
      {
        get => this.m_position;
        set => this.m_position = value;
      }

      public ObjectStatus Status
      {
        get => this.m_status;
        set => this.m_status = value;
      }

      public bool Value
      {
        get => this.m_value;
        set => this.m_value = value;
      }
    }
}
