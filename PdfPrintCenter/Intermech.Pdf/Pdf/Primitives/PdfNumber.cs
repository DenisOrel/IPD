// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Primitives.PdfNumber
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.IO;
using System;
using System.Globalization;

#nullable disable
namespace Syncfusion.Pdf.Primitives;

internal class PdfNumber : IPdfPrimitive
{
  private float m_floatValue;
  private int m_index;
  private int m_intValue;
  private bool m_isInteger;
  private bool m_isSaving;
  private int m_position;
  private ObjectStatus m_status;

  internal PdfNumber(double value)
  {
    this.m_position = -1;
    this.FloatValue = (float) value;
  }

  internal PdfNumber(int value)
  {
    this.m_position = -1;
    this.IntValue = value;
  }

  internal PdfNumber(long value)
  {
    this.m_position = -1;
    this.IntValue = (int) value;
  }

  internal PdfNumber(float value)
  {
    this.m_position = -1;
    this.FloatValue = value;
  }

  public IPdfPrimitive Clone(PdfCrossTable crossTable)
  {
    return this.IsInteger ? (IPdfPrimitive) new PdfNumber(this.IntValue) : (IPdfPrimitive) new PdfNumber(this.FloatValue);
  }

  public static string FloatToString(float number)
  {
    return number.ToString("######################.00######", (IFormatProvider) CultureInfo.InvariantCulture);
  }

  public static float Max(float x, float y, float z)
  {
    float val2 = Math.Max(x, y);
    return Math.Max(z, val2);
  }

  public static float Min(float x, float y, float z)
  {
    float val2 = Math.Min(x, y);
    return Math.Min(z, val2);
  }

  public void Save(IPdfWriter writer)
  {
    if (this.IsInteger)
      writer.Write(this.IntValue.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    else
      writer.Write(PdfNumber.FloatToString(this.FloatValue));
  }

  public IPdfPrimitive ClonedObject => (IPdfPrimitive) null;

  public float FloatValue
  {
    get => this.m_floatValue;
    set
    {
      this.m_isInteger = false;
      this.m_floatValue = value;
      this.m_intValue = (int) value;
    }
  }

  public int IntValue
  {
    get => this.m_intValue;
    set
    {
      this.m_isInteger = true;
      this.m_intValue = value;
      this.m_floatValue = (float) value;
    }
  }

  public bool IsInteger
  {
    get => this.m_isInteger;
    set => this.m_isInteger = value;
  }

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
