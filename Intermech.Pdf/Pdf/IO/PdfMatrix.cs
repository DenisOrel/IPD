// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.IO.PdfMatrix
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;


namespace Syncfusion.Pdf.IO
{
    internal class PdfMatrix
    {
      private PdfReader m_contentStream;
      private float m_height;
      private string m_key;
      private float m_leftMargin;
      private string m_marginToken;
      private SizeF m_pageSize;
      private string m_prevRectToken;
      private string m_rectToken;
      internal RectangleF m_scaledBounds;
      private float[] m_scaleMatrix;
      internal List<string> m_token;
      private float m_topMargin;
      private float[] m_translationMatrix;
      private float m_width;
      private float m_x;
      private float m_y;

      public PdfMatrix()
      {
        this.m_token = new List<string>();
        this.m_marginToken = string.Empty;
        this.m_rectToken = string.Empty;
        this.m_prevRectToken = string.Empty;
        this.m_scaledBounds = new RectangleF();
      }

      public PdfMatrix(PdfReader ContentStream, string key, SizeF pageSize)
      {
        this.m_token = new List<string>();
        this.m_marginToken = string.Empty;
        this.m_rectToken = string.Empty;
        this.m_prevRectToken = string.Empty;
        this.m_scaledBounds = new RectangleF();
        this.m_contentStream = ContentStream;
        this.m_key = key;
        this.m_pageSize = pageSize;
        this.ConvertToArray(this.MatrixCalculation());
        if (this.m_token.Count != 2)
          return;
        if (this.m_translationMatrix != null)
        {
          this.SetTranslationMatrix();
        }
        else
        {
          if ((double) this.m_x != 0.0 || (double) this.m_y != 0.0 || (double) this.m_width != 0.0 || (double) this.m_height != 0.0)
            return;
          this.SetScaleMatrix(pageSize);
        }
      }

      private void ConvertToArray(List<string> matrixString)
      {
        if (this.m_token.Count <= 0)
          return;
        string str1 = this.m_token[0].ToString();
        if (this.m_marginToken.Length > 0)
        {
          string[] strArray = this.m_marginToken.Split(new string[1]
          {
            "cm"
          }, StringSplitOptions.None)[0].Split(new char[1]
          {
            ' '
          }, StringSplitOptions.RemoveEmptyEntries);
          this.m_leftMargin = float.Parse(strArray[4].ToString(), NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture);
          this.m_topMargin = float.Parse(strArray[5].ToString(), NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture);
          this.m_leftMargin = Math.Abs(this.m_leftMargin);
          this.m_topMargin = Math.Abs(this.m_topMargin);
        }
        if (this.m_token.Count == 2)
        {
          foreach (string str2 in matrixString)
          {
            string[] strArray = str2.Split(new string[1]{ "cm" }, StringSplitOptions.None)[0].Split(new char[1]
            {
              ' '
            }, StringSplitOptions.RemoveEmptyEntries);
            if (strArray[0] == "q")
            {
              for (int index = 0; index < strArray.Length - 1; ++index)
                strArray[index] = strArray[index + 1];
            }
            switch (strArray[0])
            {
              case "1.00":
                this.m_translationMatrix = new float[strArray.Length];
                for (int index = 0; index < strArray.Length; ++index)
                  this.m_translationMatrix[index] = float.Parse(strArray[index], NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture);
                continue;
              case "1":
                this.m_translationMatrix = new float[strArray.Length];
                for (int index = 0; index < strArray.Length; ++index)
                  this.m_translationMatrix[index] = float.Parse(strArray[index], NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture);
                continue;
              default:
                if (this.m_scaleMatrix == null)
                {
                  this.m_scaleMatrix = new float[strArray.Length];
                  for (int index = 0; index < strArray.Length; ++index)
                    this.m_scaleMatrix[index] = float.Parse(strArray[index], NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture);
                  continue;
                }
                if ((double) this.m_scaleMatrix[0] != 1.0)
                {
                  float result1;
                  float.TryParse(strArray[0], NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result1);
                  float result2;
                  float.TryParse(strArray[1], NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result2);
                  float result3;
                  float.TryParse(strArray[2], NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result3);
                  float result4;
                  float.TryParse(strArray[3], NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result4);
                  float result5;
                  float.TryParse(strArray[4], NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result5);
                  float result6;
                  float.TryParse(strArray[5], NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result6);
                  if ((double) result1 == 0.0 && (double) result4 == 0.0)
                  {
                    if ((double) this.m_scaleMatrix[0] > 0.0 && (double) this.m_scaleMatrix[3] > 0.0)
                    {
                      this.m_x = (result3 + result5) * this.m_scaleMatrix[0];
                      this.m_y = (result6 + result4) * this.m_scaleMatrix[0];
                      this.m_height = -result3 * this.m_scaleMatrix[0];
                      this.m_width = result2 * this.m_scaleMatrix[0];
                      continue;
                    }
                    continue;
                  }
                  for (int index = 0; index < strArray.Length; ++index)
                    this.m_scaleMatrix[index] *= float.Parse(strArray[index], NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture);
                  continue;
                }
                for (int index = 0; index < strArray.Length; ++index)
                  this.m_scaleMatrix[index] = float.Parse(strArray[index], NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture);
                continue;
            }
          }
        }
        else
        {
          if (this.m_token.Count != 1)
            return;
          string[] strArray = str1.Split(new string[1]{ "cm" }, StringSplitOptions.None)[0].Split(new char[1]
          {
            ' '
          }, StringSplitOptions.RemoveEmptyEntries);
          if (strArray.Length == 0)
            return;
          if (strArray[0] == "q")
          {
            for (int index = 0; index < strArray.Length - 1; ++index)
              strArray[index] = strArray[index + 1];
          }
          float result7;
          float.TryParse(strArray[4].ToString(), NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result7);
          float result8;
          float.TryParse(strArray[5].ToString(), NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result8);
          float result9;
          float.TryParse(strArray[0].ToString(), NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result9);
          float result10;
          float.TryParse(strArray[3].ToString(), NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result10);
          if ((double) result8 > 0.0)
            result8 = this.m_pageSize.Height - result8;
          RectangleF rectangleF = new RectangleF(new PointF(Math.Abs(result7), Math.Abs(result8)), new SizeF(Math.Abs(result9), Math.Abs(result10)));
          if ((double) rectangleF.Y >= (double) rectangleF.Height)
            rectangleF.Y -= rectangleF.Height;
          rectangleF.X += this.m_leftMargin;
          rectangleF.Y += this.m_topMargin;
          if (this.m_rectToken.Length > 0)
            this.m_rectToken.Split(new string[1]{ "cm" }, StringSplitOptions.None)[0].Split(new char[1]
            {
              ' '
            }, StringSplitOptions.RemoveEmptyEntries);
          this.m_x = rectangleF.X;
          this.m_y = rectangleF.Y;
          this.m_height = rectangleF.Height;
          this.m_width = rectangleF.Width;
        }
      }

      private List<string> MatrixCalculation()
      {
        this.m_contentStream.Position = 0L;
        string str1 = this.m_contentStream.ReadLine();
        string str2 = string.Empty;
        bool flag1 = true;
        bool flag2 = false;
        bool flag3 = true;
        while (str1 != string.Empty || this.m_contentStream.Stream.Position < this.m_contentStream.Stream.Length)
        {
          if (str1.Contains(" re"))
          {
            if (this.m_rectToken != string.Empty)
              this.m_prevRectToken = this.m_rectToken;
            this.m_rectToken = str1;
          }
          if (str1.Contains("q") && !flag3)
            flag2 = true;
          if (str1.Contains("Q"))
            this.m_token.Clear();
          if (flag2 && str1.Contains(" cm"))
          {
            if (this.m_token.Count == 0)
            {
              string[] strArray1 = str1.Split(new string[2]
              {
                " ",
                "q"
              }, StringSplitOptions.RemoveEmptyEntries);
              if (strArray1.Length >= 6)
              {
                float result1;
                float.TryParse(strArray1[0], NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result1);
                float.TryParse(strArray1[1], NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out float _);
                float.TryParse(strArray1[2], NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out float _);
                float result2;
                float.TryParse(strArray1[3], NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result2);
                float result3;
                float.TryParse(strArray1[4], NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result3);
                float result4;
                float.TryParse(strArray1[5], NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result4);
                if (((double) result1 != 1.0 || (double) result2 != 1.0) && str2 != null)
                {
                  string[] strArray2 = str2.Split(new string[2]
                  {
                    " ",
                    "q"
                  }, StringSplitOptions.RemoveEmptyEntries);
                  if (strArray2.Length >= 6)
                  {
                    float result5;
                    float.TryParse(strArray2[0], NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result5);
                    float.TryParse(strArray2[1], NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out float _);
                    float.TryParse(strArray2[2], NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out float _);
                    float result6;
                    float.TryParse(strArray2[3], NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result6);
                    float.TryParse(strArray2[4], NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out float _);
                    float.TryParse(strArray2[5], NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out float _);
                    if ((double) result5 != 1.0 || (double) result6 != 1.0)
                    {
                      float width = result5 * result1;
                      float height = result6 * result2;
                      this.m_scaledBounds = new RectangleF(result3, result4, width, height);
                    }
                  }
                }
              }
              this.m_token.Add(str1);
            }
            else
              this.m_token.Add(str1);
          }
          flag3 = false;
          if (str1.Contains(" cm"))
          {
            float result;
            float.TryParse(str1.Split(new string[2]{ " ", "q" }, StringSplitOptions.RemoveEmptyEntries)[0], NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result);
            if ((double) result != 1.0)
            {
              if (!flag2)
                this.m_token.Add(str1);
            }
            else if (this.m_token.Count == 0)
              str2 = str1;
          }
          if (!flag1)
            this.m_marginToken = str1;
          flag1 = !str1.Contains("Translate co-ordinate system");
          str1 = this.m_contentStream.ReadLine();
          if (str1 == string.Empty && this.m_token.Count == 0)
            str1 = this.m_contentStream.ReadLine();
          if (str1.Contains(this.m_key) || str1.Equals(""))
            break;
        }
        if (this.m_token.Count == 0 && str2.Length > 0)
          this.m_token.Add(str2);
        return this.m_token;
      }

      private void SetScaleMatrix(SizeF pageSize)
      {
        float num = this.m_scaleMatrix[0];
        this.m_x = this.m_scaleMatrix[4];
        this.m_y = (double) pageSize.Width <= (double) pageSize.Height ? ((double) num + (double) float.Parse(this.m_scaleMatrix[5].ToString()) > (double) pageSize.Height ? num + float.Parse(this.m_scaleMatrix[5].ToString()) - pageSize.Height : float.Parse(this.m_scaleMatrix[5].ToString())) : float.Parse(this.m_scaleMatrix[5].ToString());
        this.m_width = this.m_scaleMatrix[0];
        if ((double) this.m_scaleMatrix[1] != 0.0 || (double) this.m_scaleMatrix[3] != 0.0)
          this.m_width = (float) Math.Sqrt(Math.Pow((double) this.m_scaleMatrix[0], 2.0) + Math.Pow((double) this.m_scaleMatrix[1], 2.0));
        this.m_height = this.m_scaleMatrix[4];
        if ((double) this.m_scaleMatrix[1] != 0.0 || (double) this.m_scaleMatrix[3] != 0.0)
          this.m_height = (float) Math.Sqrt(Math.Pow((double) this.m_scaleMatrix[3], 2.0) + Math.Pow((double) this.m_scaleMatrix[4], 2.0));
        this.m_x += this.m_leftMargin;
        this.m_y += this.m_topMargin;
      }

      private void SetTranslationMatrix()
      {
        this.m_x = float.Parse(this.m_translationMatrix[4].ToString());
        this.m_y = float.Parse(this.m_translationMatrix[5].ToString());
        if (this.m_scaleMatrix != null)
        {
          this.m_width = this.m_scaleMatrix[0];
          if ((double) this.m_scaleMatrix[1] != 0.0 || (double) this.m_scaleMatrix[3] != 0.0)
            this.m_width = (float) Math.Sqrt(Math.Pow((double) float.Parse(this.m_scaleMatrix[0].ToString()), 2.0) + Math.Pow((double) float.Parse(this.m_scaleMatrix[1].ToString()), 2.0));
          this.m_height = this.m_scaleMatrix[4];
          if ((double) this.m_scaleMatrix[1] != 0.0 || (double) this.m_scaleMatrix[3] != 0.0)
            this.m_height = (float) Math.Sqrt(Math.Pow((double) this.m_scaleMatrix[3], 2.0) + Math.Pow((double) this.m_scaleMatrix[4], 2.0));
        }
        else
        {
          this.m_width = -1f;
          this.m_height = -1f;
        }
        this.m_x += this.m_leftMargin;
        this.m_y += this.m_topMargin;
        if ((double) this.m_y < (double) this.m_height)
          return;
        this.m_y -= this.m_height;
      }

      public float GetHeight => this.m_height;

      public float GetScaleX => this.m_x;

      public float GetScaleY => this.m_y;

      public float GetWidth => this.m_width;

      public float LeftMargin => this.m_leftMargin;

      public float TopMargin => this.m_topMargin;
    }
}
