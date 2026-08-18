// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Primitives.PdfNull
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.IO;


namespace Syncfusion.Pdf.Primitives
{
    internal class PdfNull : IPdfPrimitive
    {
      private int m_index;
      private bool m_isSaving;
      private int m_position = -1;
      private ObjectStatus m_status;

      public IPdfPrimitive Clone(PdfCrossTable crossTable) => (IPdfPrimitive) new PdfNull();

      public void Save(IPdfWriter writer) => writer.Write("null");

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
    }
}
