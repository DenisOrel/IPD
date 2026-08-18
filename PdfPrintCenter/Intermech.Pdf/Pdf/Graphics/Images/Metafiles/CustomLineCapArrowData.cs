// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.Images.Metafiles.CustomLineCapArrowData
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

#nullable disable
namespace Syncfusion.Pdf.Graphics.Images.Metafiles;

internal struct CustomLineCapArrowData
{
  internal float width;
  internal float height;
  internal float middleInset;
  internal int fillState;
  internal int lineStartCap;
  internal int lineEndCap;
  internal int lineJoin;
  internal float lineMitterLimit;
  internal float widthScale;

  internal void Reset()
  {
    this.width = 0.0f;
    this.height = 0.0f;
    this.middleInset = 0.0f;
    this.fillState = 0;
    this.fillState = 0;
    this.lineEndCap = 0;
    this.lineEndCap = 0;
    this.lineJoin = 0;
    this.lineMitterLimit = 0.0f;
    this.widthScale = 0.0f;
  }
}
