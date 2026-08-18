// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Primitives.PdfReference
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.IO;
using System;
using System.Globalization;


namespace Syncfusion.Pdf.Primitives
{
    internal class PdfReference : IPdfPrimitive
    {
      public readonly int GenNum;
      private int m_index;
      private bool m_isSaving;
      private int m_position;
      private ObjectStatus m_status;
      public readonly long ObjNum;

      public PdfReference(long objNum, int genNum)
      {
        this.m_position = -1;
        this.ObjNum = objNum;
        this.GenNum = genNum;
      }

      public PdfReference(string objNum, string genNum)
      {
        this.m_position = -1;
        double result1;
        if (!double.TryParse(objNum, NumberStyles.Integer, (IFormatProvider) CultureInfo.InvariantCulture, out result1))
          throw new ArgumentException("Invalid format (must be an integer)", nameof (objNum));
        double result2;
        if (!double.TryParse(genNum, NumberStyles.Integer, (IFormatProvider) CultureInfo.InvariantCulture, out result2))
          throw new ArgumentException("Invalid format (must be an integer)", nameof (genNum));
        this.ObjNum = (long) (int) result1;
        this.GenNum = (int) result2;
      }

      public override bool Equals(object obj)
      {
        PdfReference pdfReference = obj as PdfReference;
        return !(pdfReference == (PdfReference) null) && pdfReference.ObjNum == this.ObjNum && pdfReference.GenNum == this.GenNum;
      }

      public override int GetHashCode() => (int) (this.ObjNum + (long) this.GenNum << 24);

      public static bool operator ==(PdfReference ref1, PdfReference ref2)
      {
        object obj1 = (object) ref1;
        object obj2 = (object) ref2;
        if (obj1 == null || obj2 == null)
          return obj1 == obj2;
        return ref1.ObjNum == ref2.ObjNum && ref1.GenNum == ref2.GenNum;
      }

      public static bool operator !=(PdfReference ref1, PdfReference ref2) => !(ref1 == ref2);

      public void Save(IPdfWriter writer) => writer.Write(this.ToString());

      IPdfPrimitive IPdfPrimitive.Clone(PdfCrossTable crossTable) => (IPdfPrimitive) null;

      public override string ToString() => $"{this.ObjNum} {this.GenNum} R";

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
