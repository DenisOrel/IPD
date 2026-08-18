// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PdfAutomaticFieldInfo
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Drawing;


namespace Syncfusion.Pdf
{
    internal class PdfAutomaticFieldInfo
    {
      private PdfAutomaticField m_field;
      private PointF m_location;
      private float m_scalingX;
      private float m_scalingY;

      public PdfAutomaticFieldInfo(PdfAutomaticFieldInfo fieldInfo)
      {
        this.m_location = PointF.Empty;
        this.m_scalingX = 1f;
        this.m_scalingY = 1f;
        this.m_field = fieldInfo != null ? fieldInfo.Field : throw new ArgumentNullException(nameof (fieldInfo));
        this.m_location = fieldInfo.Location;
        this.m_scalingX = fieldInfo.ScalingX;
        this.m_scalingY = fieldInfo.ScalingY;
      }

      public PdfAutomaticFieldInfo(PdfAutomaticField field, PointF location)
      {
        this.m_location = PointF.Empty;
        this.m_scalingX = 1f;
        this.m_scalingY = 1f;
        this.m_field = field != null ? field : throw new ArgumentNullException(nameof (field));
        this.m_location = location;
      }

      public PdfAutomaticFieldInfo(
        PdfAutomaticField field,
        PointF location,
        float scalingX,
        float scalingY)
      {
        this.m_location = PointF.Empty;
        this.m_scalingX = 1f;
        this.m_scalingY = 1f;
        this.m_field = field != null ? field : throw new ArgumentNullException(nameof (field));
        this.m_location = location;
        this.m_scalingX = scalingX;
        this.m_scalingY = scalingY;
      }

      public PdfAutomaticField Field
      {
        get => this.m_field;
        set => this.m_field = value != null ? value : throw new ArgumentNullException(nameof (Field));
      }

      public PointF Location
      {
        get => this.m_location;
        set => this.m_location = value;
      }

      public float ScalingX
      {
        get => this.m_scalingX;
        set => this.m_scalingX = value;
      }

      public float ScalingY
      {
        get => this.m_scalingY;
        set => this.m_scalingY = value;
      }
    }
}
