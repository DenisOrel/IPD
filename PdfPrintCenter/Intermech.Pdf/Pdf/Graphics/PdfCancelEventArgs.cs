// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.PdfCancelEventArgs
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;

#nullable disable
namespace Syncfusion.Pdf.Graphics;

public class PdfCancelEventArgs : EventArgs
{
  private bool m_cancel;

  public bool Cancel
  {
    get => this.m_cancel;
    set => this.m_cancel = value;
  }
}
