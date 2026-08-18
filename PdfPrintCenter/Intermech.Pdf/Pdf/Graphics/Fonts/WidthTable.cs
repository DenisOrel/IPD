// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.Fonts.WidthTable
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System;

#nullable disable
namespace Syncfusion.Pdf.Graphics.Fonts;

internal abstract class WidthTable : ICloneable
{
  public abstract WidthTable Clone();

  object ICloneable.Clone() => (object) this.Clone();

  internal abstract PdfArray ToArray();

  public abstract int this[int index] { get; }
}
