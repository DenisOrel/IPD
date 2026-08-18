// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Primitives.IPdfPrimitive
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.IO;

#nullable disable
namespace Syncfusion.Pdf.Primitives;

internal interface IPdfPrimitive
{
  IPdfPrimitive Clone(PdfCrossTable crossTable);

  void Save(IPdfWriter writer);

  IPdfPrimitive ClonedObject { get; }

  bool IsSaving { get; set; }

  int ObjectCollectionIndex { get; set; }

  int Position { get; set; }

  ObjectStatus Status { get; set; }
}
