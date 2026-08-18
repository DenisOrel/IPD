// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.PdfTransformationMatrix
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;


namespace Syncfusion.Pdf.Graphics
{
    public class PdfTransformationMatrix : ICloneable
    {
      private const double DegRadFactor = 0.017453292519943295;
      private Matrix m_matrix;
      private const double RadDegFactor = 57.295779513082323;

      public PdfTransformationMatrix() => this.m_matrix = new Matrix(1f, 0.0f, 0.0f, 1f, 0.0f, 0.0f);

      internal PdfTransformationMatrix(bool value)
      {
        this.m_matrix = new Matrix(1f, 0.0f, 0.0f, -1f, 0.0f, 0.0f);
      }

      internal PdfTransformationMatrix Clone()
      {
        PdfTransformationMatrix transformationMatrix = this.MemberwiseClone() as PdfTransformationMatrix;
        transformationMatrix.m_matrix = this.m_matrix.Clone();
        return transformationMatrix;
      }

      public static double DegressToRadians(float degreesX) => Math.PI / 180.0 * (double) degreesX;

      protected internal void Multiply(PdfTransformationMatrix matrix)
      {
        this.m_matrix.Multiply(matrix.Matrix);
      }

      public static double RadiansToDegress(float radians) => 180.0 / Math.PI * (double) radians;

      public void Rotate(float angle) => this.m_matrix.Rotate(angle);

      public void RotateAt(float angle, PointF point) => this.m_matrix.RotateAt(angle, point);

      public void Scale(SizeF scales) => this.Scale(scales.Width, scales.Height);

      public void Scale(float scaleX, float scaleY) => this.m_matrix.Scale(scaleX, scaleY);

      public void Shear(float shearX, float shearY) => this.m_matrix.Shear(shearX, shearY);

      public void Skew(SizeF angles) => this.Skew(angles.Width, angles.Height);

      public void Skew(float angleX, float angleY)
      {
        this.m_matrix.Multiply(new Matrix(1f, (float) Math.Tan(PdfTransformationMatrix.DegressToRadians(angleX)), (float) Math.Tan(PdfTransformationMatrix.DegressToRadians(angleY)), 1f, 0.0f, 0.0f));
      }

      object ICloneable.Clone() => (object) this.Clone();

      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        char ch = ' ';
        int index = 0;
        for (int length = this.m_matrix.Elements.Length; index < length; ++index)
        {
          stringBuilder.Append(PdfNumber.FloatToString(this.m_matrix.Elements[index]));
          stringBuilder.Append(ch);
        }
        return stringBuilder.ToString();
      }

      public void Translate(SizeF offsets) => this.Translate(offsets.Width, offsets.Height);

      public void Translate(float offsetX, float offsetY) => this.m_matrix.Translate(offsetX, offsetY);

      public Matrix Matrix
      {
        get => this.m_matrix;
        set
        {
          if (this.m_matrix == value)
            return;
          this.m_matrix = value;
        }
      }

      public float OffsetX => this.m_matrix.OffsetX;

      public float OffsetY => this.m_matrix.OffsetY;
    }
}
