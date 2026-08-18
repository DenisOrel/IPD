// Decompiled with JetBrains decompiler
// Type: Syncfusion.PdfViewer.Base.GlyphWriter
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;


namespace Syncfusion.PdfViewer.Base
{
    internal class GlyphWriter
    {
      private bool allowAll;
      private string[] charForGlyphIndex;
      private static string charSet = "0123456789.ee -";
      private PointF CurrentLocation;
      private int currentOp;
      internal Dictionary<string, byte[]> glyphs = new Dictionary<string, byte[]>();
      private MemoryStream glyphStream;
      private double h;
      private int hintCount;
      public bool is1C;
      private PdfUnitConvertor m_convertor = new PdfUnitConvertor();
      private List<GraphicsPath> m_tempSubPaths = new List<GraphicsPath>();
      private int max = 100;
      private int operandReached;
      private double[] operands = new double[100];
      private GraphicsPath Path;
      private float[] pt;
      private int ptCount;
      internal Dictionary<int, string> UnicodeCharMapTable;
      private double x;
      private double xs = -1.0;
      private double y;
      private double ys = -1.0;

      public GlyphWriter(Dictionary<string, byte[]> glyphCharSet, bool isC1)
      {
        this.glyphs = glyphCharSet;
        this.is1C = isC1;
      }

      private void AddBezierCurve(string[] curve)
      {
        PointF pt1 = new PointF(this.CurrentLocation.X, this.CurrentLocation.Y);
        PointF pt2 = new PointF(float.Parse(curve[0]), float.Parse(curve[1]));
        PointF pt3 = new PointF(float.Parse(curve[2]), float.Parse(curve[3]));
        PointF pt4 = new PointF(float.Parse(curve[4]), float.Parse(curve[5]));
        this.Path.AddBezier(pt1, pt2, pt3, pt4);
        this.CurrentLocation = pt4;
      }

      private void AddBezierCurve2(string[] curve)
      {
        PointF pt1 = new PointF(this.CurrentLocation.X, this.CurrentLocation.Y);
        PointF pt2 = new PointF(this.CurrentLocation.X, this.CurrentLocation.Y);
        PointF pt3 = new PointF(float.Parse(curve[0]), float.Parse(curve[1]));
        PointF pt4 = new PointF(float.Parse(curve[2]), float.Parse(curve[3]));
        this.Path.AddBezier(pt1, pt2, pt3, pt4);
        this.CurrentLocation = pt4;
      }

      private void AddBezierCurve3(string[] curve)
      {
        PointF pt1 = new PointF(this.CurrentLocation.X, this.CurrentLocation.Y);
        PointF pt2 = new PointF(float.Parse(curve[0]), float.Parse(curve[1]));
        PointF pt3 = new PointF(float.Parse(curve[2]), float.Parse(curve[3]));
        PointF pt4 = new PointF(float.Parse(curve[2]), float.Parse(curve[3]));
        this.Path.AddBezier(pt1, pt2, pt3, pt4);
        this.CurrentLocation = pt4;
      }

      private void AddLine(string[] line)
      {
        PointF pt2 = new PointF();
        pt2.X = float.Parse(line[0]);
        pt2.Y = float.Parse(line[1]);
        this.Path.AddLine(this.CurrentLocation, pt2);
        this.CurrentLocation = pt2;
      }

      private void BeginPath(string[] point)
      {
        try
        {
          if (this.Path != null && this.Path.PointCount > 0)
          {
            this.m_tempSubPaths.Add(this.Path);
            this.Path = new GraphicsPath();
          }
          else
            this.Path = new GraphicsPath();
        }
        catch
        {
          this.Path = new GraphicsPath();
        }
        this.CurrentLocation = new PointF(float.Parse(point[0]), float.Parse(point[1]));
      }

      private void ClosePath()
      {
        if (this.xs != -1.0)
          this.AddLine(new string[2]
          {
            this.xs.ToString(),
            this.ys.ToString()
          });
        this.xs = -1.0;
      }

      private void Div()
      {
        double num = this.operands[this.operandReached - 2] / this.operands[this.operandReached - 1];
        if (this.operandReached > 0)
          --this.operandReached;
        this.operands[this.operandReached - 1] = num;
      }

      private void EndChar(int rawInt)
      {
        float num1 = (float) (this.x + this.operands[this.currentOp]);
        float num2 = (float) (this.y + this.operands[this.currentOp + 1]);
        string str1 = this.getunicodechar((int) this.operands[this.currentOp + 2]);
        string str2 = this.getunicodechar((int) this.operands[this.currentOp + 3]);
        this.x = 0.0;
        this.y = 0.0;
        this.glyphParser(str1.ToString(), rawInt, 500f, (Dictionary<int, string>) null);
        this.ClosePath();
        this.x = (double) num1;
        this.y = (double) num2;
        this.glyphParser(str2.ToString(), rawInt, 500f, (Dictionary<int, string>) null);
      }

      private int EndChar(int rawInt, int dicEnd)
      {
        if (this.operandReached == 5)
        {
          --this.operandReached;
          ++this.currentOp;
        }
        if (this.operandReached == 4)
        {
          this.EndChar(rawInt);
          return dicEnd;
        }
        this.ClosePath();
        return dicEnd;
      }

      private GraphicsPath FillPath(string mode)
      {
        if (this.Path != null)
        {
          if (this.m_tempSubPaths.Count > 0)
          {
            foreach (GraphicsPath tempSubPath in this.m_tempSubPaths)
            {
              try
              {
                if (this.Path == null)
                  this.Path = new GraphicsPath();
                if (tempSubPath != null)
                {
                  if (tempSubPath.PointCount > 0)
                  {
                    tempSubPath.CloseAllFigures();
                    this.Path.AddPath(tempSubPath, true);
                  }
                }
              }
              catch
              {
                if (tempSubPath != null)
                {
                  if (tempSubPath.PointCount > 0)
                  {
                    this.Path = new GraphicsPath();
                    this.Path.AddPath(tempSubPath, true);
                  }
                }
              }
            }
            this.m_tempSubPaths.Clear();
          }
          this.Path.FillMode = !(mode == "Alternate") ? FillMode.Alternate : FillMode.Winding;
        }
        return this.Path;
      }

      private void Flex()
      {
        for (int index1 = 0; index1 < 12; index1 += 6)
        {
          for (int index2 = 0; index2 < 6; index2 += 2)
          {
            this.x += this.operands[index1 + index2];
            this.y += this.operands[index1 + index2 + 1];
            this.pt[index2] = (float) this.x;
            this.pt[index2 + 1] = (float) this.y;
          }
          this.AddBezierCurve(new string[6]
          {
            this.pt[0].ToString(),
            this.pt[1].ToString(),
            this.pt[2].ToString(),
            this.pt[3].ToString(),
            this.pt[4].ToString(),
            this.pt[5].ToString()
          });
        }
      }

      private void Flex1()
      {
        double num1 = 0.0;
        double num2 = 0.0;
        double x = this.x;
        double y = this.y;
        for (int index = 0; index < 10; index += 2)
        {
          num1 += this.operands[index];
          num2 += this.operands[index + 1];
        }
        bool flag = Math.Abs(num1) > Math.Abs(num2);
        for (int index = 0; index < 6; index += 2)
        {
          this.x += this.operands[index];
          this.y += this.operands[index + 1];
          this.pt[index] = (float) this.x;
          this.pt[index + 1] = (float) this.y;
        }
        this.AddBezierCurve(new string[6]
        {
          this.pt[0].ToString(),
          this.pt[1].ToString(),
          this.pt[2].ToString(),
          this.pt[3].ToString(),
          this.pt[4].ToString(),
          this.pt[5].ToString()
        });
        for (int index = 0; index < 4; index += 2)
        {
          this.x += this.operands[index + 6];
          this.y += this.operands[index + 7];
          this.pt[index] = (float) this.x;
          this.pt[index + 1] = (float) this.y;
        }
        if (flag)
        {
          this.x += this.operands[10];
          this.y = y;
        }
        else
        {
          this.x = x;
          this.y += this.operands[10];
        }
        this.pt[4] = (float) this.x;
        this.pt[5] = (float) this.y;
        this.AddBezierCurve(new string[6]
        {
          this.pt[0].ToString(),
          this.pt[1].ToString(),
          this.pt[2].ToString(),
          this.pt[3].ToString(),
          this.pt[4].ToString(),
          this.pt[5].ToString()
        });
      }

      public string getunicodechar(int charval) => this.UnicodeCharMapTable[charval];

      internal object glyphParser(
        string glyphChar,
        int rawInt,
        float currentWidth,
        Dictionary<int, string> unicodeCharMapTable)
      {
        if (unicodeCharMapTable != null)
          this.UnicodeCharMapTable = unicodeCharMapTable;
        if (glyphChar == null && glyphChar == null)
          glyphChar = ".notdef";
        this.BeginPath(new string[2]{ "0", "0" });
        byte[] numArray1 = this.glyphs[glyphChar];
        if (numArray1 != null)
        {
          int[] numArray2 = new int[numArray1.Length];
          for (int index = 0; index < numArray2.Length; ++index)
            numArray2[index] = numArray1[index] <= (byte) 128 /*0x80*/ ? (int) numArray1[index] : (int) numArray1[index] - 256 /*0x0100*/;
          bool isFirst = true;
          int index1 = 0;
          int num1 = -1;
          int index2 = 0;
          int num2 = 0;
          int key = 0;
          int dicEnd = numArray1.Length;
          int routine = 0;
          this.currentOp = 0;
          this.hintCount = 0;
          double num3 = 999999.0;
          double num4 = 0.0;
          double num5 = 1000.0;
          bool isFlex = false;
          this.pt = new float[6];
          this.h = 100000.0;
          if (this.is1C)
          {
            this.operands = new double[this.max];
            this.operandReached = 0;
            this.allowAll = true;
          }
          while (index2 < dicEnd)
          {
            int num6 = (int) numArray1[index2];
            if (num6 > 31 /*0x1F*/ || num6 == 28)
            {
              num2 = index2;
              index2 = this.ParseOperand(numArray1, index2, this.operands, this.operandReached);
              routine = (int) this.operands[this.operandReached];
              ++this.operandReached;
            }
            else
            {
              ++num1;
              int lastKey = key;
              key = num6;
              ++index2;
              this.currentOp = 0;
              switch (key)
              {
                case 12:
                  key = (int) numArray1[index2];
                  ++index2;
                  if (key == 7)
                  {
                    num5 = this.SideBearingWidth();
                    this.operandReached = 0;
                    break;
                  }
                  if (this.allowAll)
                  {
                    switch (key)
                    {
                      case 0:
                        this.operandReached = 0;
                        break;
                      case 6:
                        this.StandardEncodingAccent(rawInt, this.currentOp);
                        this.operandReached = 0;
                        break;
                      case 12:
                        this.Div();
                        break;
                      case 16 /*0x10*/:
                        isFlex = this.ProcessFlex(lastKey, isFlex, routine);
                        this.operandReached = 0;
                        break;
                      case 17:
                        this.Pop();
                        break;
                      case 33:
                        this.SetCurrentPoint();
                        this.operandReached = 0;
                        break;
                      case 34:
                        this.HybridFlex();
                        this.operandReached = 0;
                        break;
                      case 35:
                        this.Flex();
                        this.operandReached = 0;
                        break;
                      case 36:
                        this.HybridFlex1();
                        this.operandReached = 0;
                        break;
                      case 37:
                        this.Flex1();
                        this.operandReached = 0;
                        break;
                      default:
                        this.operandReached = 0;
                        break;
                    }
                  }
                  else
                    break;
                  break;
                case 13:
                  this.Hsbw();
                  this.operandReached = 0;
                  break;
                default:
                  if (this.allowAll && key != 0)
                  {
                    if (key == 1 | key == 3 | key == 18 | key == 23)
                    {
                      this.hintCount += this.operandReached / 2;
                      this.operandReached = 0;
                      break;
                    }
                    switch (key)
                    {
                      case 4:
                        if (isFlex)
                        {
                          this.y += this.operands[this.currentOp];
                          this.pt[index1] = (float) this.x;
                          int index3 = index1 + 1;
                          this.pt[index3] = (float) this.y;
                          index1 = index3 + 1;
                        }
                        else
                          this.VerticalMoveTo(isFirst);
                        this.operandReached = 0;
                        break;
                      case 5:
                        this.RelativeLineTo();
                        this.operandReached = 0;
                        break;
                      default:
                        if (key == 6 | key == 7)
                        {
                          this.HorizontalVerticalLineTo(key);
                          this.operandReached = 0;
                          break;
                        }
                        switch (key)
                        {
                          case 8:
                            this.RelativeRCurveTo();
                            this.operandReached = 0;
                            break;
                          case 9:
                            this.ClosePath();
                            this.operandReached = 0;
                            break;
                          case 10:
                          case 29:
                            if (!this.is1C && key == 10 && routine >= 0 && routine <= 2 && lastKey != 11 && this.operandReached > 5)
                            {
                              isFlex = this.ProcessFlex(lastKey, isFlex, routine);
                              this.operandReached = 0;
                              break;
                            }
                            int num7 = 0;
                            int num8 = 0;
                            if (key == 10)
                              routine += num7;
                            else
                              routine += num8;
                            byte[] sourceArray = key != 10 ? this.glyphs["global" + (object) routine] : this.glyphs["subrs" + (object) routine];
                            if (sourceArray != null)
                            {
                              int length1 = sourceArray.Length;
                              int length2 = numArray1.Length;
                              int length3 = length1 + length2 - 2;
                              dicEnd = dicEnd + length1 - 2;
                              byte[] destinationArray = new byte[length3];
                              Array.Copy((Array) numArray1, 0, (Array) destinationArray, 0, num2);
                              Array.Copy((Array) sourceArray, 0, (Array) destinationArray, num2, length1);
                              Array.Copy((Array) numArray1, index2, (Array) destinationArray, num2 + length1, length2 - index2);
                              numArray1 = destinationArray;
                              index2 = num2;
                              if (this.operandReached > 0)
                              {
                                --this.operandReached;
                                break;
                              }
                              break;
                            }
                            break;
                          case 11:
                            break;
                          case 14:
                            this.EndChar(rawInt, dicEnd);
                            this.operandReached = 0;
                            index2 = dicEnd + 1;
                            break;
                          case 16 /*0x10*/:
                            this.operandReached = 0;
                            break;
                          default:
                            if (key == 19 | key == 20)
                            {
                              index2 = this.Mask(index2, lastKey);
                              this.operandReached = 0;
                              break;
                            }
                            switch (key)
                            {
                              case 21:
                                if (isFlex)
                                {
                                  this.y += this.operands[this.currentOp + 1];
                                  this.x += this.operands[this.currentOp];
                                  this.pt[index1] = (float) this.x;
                                  int index4 = index1 + 1;
                                  this.pt[index4] = (float) this.y;
                                  index1 = index4 + 1;
                                }
                                else
                                  this.RelativeMoveTo(isFirst);
                                this.operandReached = 0;
                                break;
                              case 22:
                                if (isFlex)
                                {
                                  this.x += this.operands[this.currentOp];
                                  this.pt[index1] = (float) this.x;
                                  int index5 = index1 + 1;
                                  this.pt[index5] = (float) this.y;
                                  index1 = index5 + 1;
                                }
                                else
                                  this.HorizontalMoveTo(isFirst);
                                this.operandReached = 0;
                                break;
                              case 24:
                                this.RelativeCurveLine();
                                this.operandReached = 0;
                                break;
                              case 25:
                                this.RelativeLineCurve();
                                this.operandReached = 0;
                                break;
                              default:
                                if (key == 26 | key == 27)
                                {
                                  this.VVHHCurveTo(key);
                                  this.operandReached = 0;
                                  break;
                                }
                                if (key == 30 | key == 31 /*0x1F*/)
                                {
                                  this.VHHVCurveTo(key);
                                  this.operandReached = 0;
                                  break;
                                }
                                break;
                            }
                            break;
                        }
                        break;
                    }
                  }
                  else
                    break;
                  break;
              }
              if (num3 > this.y)
                num3 = this.y;
              if (num4 < this.y)
                num4 = this.y;
              if (key != 19 && key != 29 && key != 10)
                isFirst = false;
            }
          }
          if (num5 > this.h)
            num3 = num5 - this.h;
          double num9;
          if (num4 < num5)
          {
            num9 = 0.0;
          }
          else
          {
            float num10 = (float) (num4 - (num5 - num3));
            if (((double) num10 >= 0.0 ? 0.0 : (num5 - num4 > (double) num10 ? num3 - (double) num10 : (double) num10)) < 0.0)
              num9 = 0.0;
          }
        }
        return (object) this.FillPath("abcd");
      }

      private void HorizontalMoveTo(bool isFirst)
      {
        if (isFirst && this.operandReached == 2)
          ++this.currentOp;
        this.x += this.operands[this.currentOp];
        this.BeginPath(new string[2]
        {
          this.x.ToString(),
          this.y.ToString()
        });
        this.xs = this.x;
        this.ys = this.y;
      }

      private void HorizontalVerticalLineTo(int key)
      {
        bool flag = key == 6;
        int index = 0;
        while (index < this.operandReached)
        {
          if (flag)
            this.x += this.operands[index];
          else
            this.y += this.operands[index];
          this.AddLine(new string[2]
          {
            this.x.ToString(),
            this.y.ToString()
          });
          ++index;
          flag = !flag;
        }
      }

      private void Hsbw()
      {
        this.x += this.operands[0];
        this.BeginPath(new string[2]{ this.x.ToString(), "0" });
        this.allowAll = true;
      }

      private void HybridFlex()
      {
        this.x += this.operands[0];
        this.pt[0] = (float) this.x;
        this.pt[1] = (float) this.y;
        this.x += this.operands[1];
        this.y += this.operands[2];
        this.pt[2] = (float) this.x;
        this.pt[3] = (float) this.y;
        this.x += this.operands[3];
        this.pt[4] = (float) this.x;
        this.pt[5] = (float) this.y;
        this.AddBezierCurve(new string[6]
        {
          this.pt[0].ToString(),
          this.pt[1].ToString(),
          this.pt[2].ToString(),
          this.pt[3].ToString(),
          this.pt[4].ToString(),
          this.pt[5].ToString()
        });
        this.x += this.operands[4];
        this.pt[0] = (float) this.x;
        this.pt[1] = (float) this.y;
        this.x += this.operands[5];
        this.pt[2] = (float) this.x;
        this.pt[3] = (float) this.y;
        this.x += this.operands[6];
        this.pt[4] = (float) this.x;
        this.pt[5] = (float) this.y;
        this.AddBezierCurve(new string[6]
        {
          this.pt[0].ToString(),
          this.pt[1].ToString(),
          this.pt[2].ToString(),
          this.pt[3].ToString(),
          this.pt[4].ToString(),
          this.pt[5].ToString()
        });
      }

      private void HybridFlex1()
      {
        this.x += this.operands[0];
        this.y += this.operands[1];
        this.pt[0] = (float) this.x;
        this.pt[1] = (float) this.y;
        this.x += this.operands[2];
        this.y += this.operands[3];
        this.pt[2] = (float) this.x;
        this.pt[3] = (float) this.y;
        this.x += this.operands[4];
        this.pt[4] = (float) this.x;
        this.pt[5] = (float) this.y;
        this.AddBezierCurve(new string[6]
        {
          this.pt[0].ToString(),
          this.pt[1].ToString(),
          this.pt[2].ToString(),
          this.pt[3].ToString(),
          this.pt[4].ToString(),
          this.pt[5].ToString()
        });
        this.x += this.operands[5];
        this.pt[0] = (float) this.x;
        this.pt[1] = (float) this.y;
        this.x += this.operands[6];
        this.y += this.operands[7];
        this.pt[2] = (float) this.x;
        this.pt[3] = (float) this.y;
        this.x += this.operands[8];
        this.pt[4] = (float) this.x;
        this.pt[5] = (float) this.y;
        this.AddBezierCurve(new string[6]
        {
          this.pt[0].ToString(),
          this.pt[1].ToString(),
          this.pt[2].ToString(),
          this.pt[3].ToString(),
          this.pt[4].ToString(),
          this.pt[5].ToString()
        });
      }

      private int Mask(int p, int lastKey)
      {
        this.hintCount += this.operandReached / 2;
        for (int hintCount = this.hintCount; hintCount > 0; hintCount -= 8)
          ++p;
        return p;
      }

      public int ParseOperand(byte[] glyphChars, int pos, double[] values, int valuePointer)
      {
        double num1 = 0.0;
        int glyphChar1 = (int) glyphChars[pos];
        if (!(glyphChar1 < 28 | glyphChar1 == 31 /*0x1F*/))
        {
          switch (glyphChar1)
          {
            case 28:
              num1 = (double) (((int) glyphChars[pos + 1] << 8) + (int) glyphChars[pos + 2]);
              pos += 3;
              break;
            case 29:
              num1 = (double) (((int) glyphChars[pos + 1] << 24) + ((int) glyphChars[pos + 2] << 16 /*0x10*/) + ((int) glyphChars[pos + 3] << 8) + (int) glyphChars[pos + 4]);
              pos += 5;
              break;
            case 30:
              char[] chArray = new char[65];
              ++pos;
              int length = 0;
              while (length < 64 /*0x40*/)
              {
                int glyphChar2 = (int) glyphChars[pos++];
                int index = glyphChar2 >> 4;
                int num2 = glyphChar2;
                if (index != 15)
                {
                  chArray[length++] = GlyphWriter.charSet[index];
                  if (length != 64 /*0x40*/)
                  {
                    if (index == 12)
                      chArray[length++] = '-';
                    if (length != 64 /*0x40*/ && num2 != 15)
                    {
                      chArray[length++] = GlyphWriter.charSet[index];
                      if (length != 64 /*0x40*/)
                      {
                        if (num2 == 12)
                          chArray[length++] = '-';
                      }
                      else
                        break;
                    }
                    else
                      break;
                  }
                  else
                    break;
                }
                else
                  break;
              }
              num1 = double.Parse(new string(chArray, 0, length));
              break;
            case (int) byte.MaxValue:
              if (this.is1C)
              {
                int num3 = ((int) glyphChars[pos + 1] << 8) + (int) glyphChars[pos + 2];
                if (num3 > 32768 /*0x8000*/)
                  num3 = 65536 /*0x010000*/ - num3;
                num1 = (double) num3 + (double) (((int) glyphChars[pos + 3] << 8) + (int) glyphChars[pos + 4]) / 65536.0;
                if (glyphChars[pos + 1] < (byte) 0)
                  num1 = -num1;
              }
              else
                num1 = (double) (((int) glyphChars[pos + 1] << 24) + ((int) glyphChars[pos + 2] << 16 /*0x10*/) + ((int) glyphChars[pos + 3] << 8) + (int) glyphChars[pos + 4]);
              pos += 5;
              break;
            default:
              if (glyphChar1 < 247)
              {
                num1 = (double) (glyphChar1 - 139);
                ++pos;
                break;
              }
              if (glyphChar1 < 251)
              {
                num1 = (double) ((glyphChar1 - 247 << 8) + (int) glyphChars[pos + 1] + 108);
                pos += 2;
                break;
              }
              num1 = (double) (-(glyphChar1 - 251 << 8) - (int) glyphChars[pos + 1] - 108);
              pos += 2;
              break;
          }
        }
        values[valuePointer] = num1;
        return pos;
      }

      private void Pop()
      {
        if (this.operandReached <= 0)
          return;
        --this.operandReached;
      }

      private bool ProcessFlex(int lastKey, bool isFlex, int routine)
      {
        if (isFlex && this.ptCount == 14 && routine == 0)
        {
          isFlex = false;
          for (int index = 0; index < 12; index += 6)
            this.AddBezierCurve(new string[6]
            {
              this.pt[index].ToString(),
              this.pt[index + 1].ToString(),
              this.pt[index + 2].ToString(),
              this.pt[index + 3].ToString(),
              this.pt[index + 4].ToString(),
              this.pt[index + 5].ToString()
            });
          return isFlex;
        }
        if (!isFlex && routine >= 0 && routine <= 2)
        {
          isFlex = true;
          this.ptCount = 0;
          this.pt = new float[16 /*0x10*/];
        }
        return isFlex;
      }

      private void RelativeCurveLine()
      {
        for (int index = (this.operandReached - 2) / 6; index > 0; --index)
        {
          float[] numArray = new float[6];
          this.x += this.operands[this.currentOp];
          this.y += this.operands[this.currentOp + 1];
          numArray[0] = (float) this.x;
          numArray[1] = (float) this.y;
          this.x += this.operands[this.currentOp + 2];
          this.y += this.operands[this.currentOp + 3];
          numArray[2] = (float) this.x;
          numArray[3] = (float) this.y;
          this.x += this.operands[this.currentOp + 4];
          this.y += this.operands[this.currentOp + 5];
          numArray[4] = (float) this.x;
          numArray[5] = (float) this.y;
          this.AddBezierCurve(new string[6]
          {
            numArray[0].ToString(),
            numArray[1].ToString(),
            numArray[2].ToString(),
            numArray[3].ToString(),
            numArray[4].ToString(),
            numArray[5].ToString()
          });
          this.currentOp += 6;
        }
        this.x += this.operands[this.currentOp];
        this.y += this.operands[this.currentOp + 1];
        this.AddLine(new string[2]
        {
          this.x.ToString(),
          this.y.ToString()
        });
        this.currentOp += 2;
      }

      private void RelativeLineCurve()
      {
        for (int index = (this.operandReached - 6) / 2; index > 0; --index)
        {
          this.x += this.operands[this.currentOp];
          this.y += this.operands[this.currentOp + 1];
          this.AddLine(new string[2]
          {
            this.x.ToString(),
            this.y.ToString()
          });
          this.currentOp += 2;
        }
        float[] numArray = new float[6];
        this.x += this.operands[this.currentOp];
        this.y += this.operands[this.currentOp + 1];
        numArray[0] = (float) this.x;
        numArray[1] = (float) this.y;
        this.x += this.operands[this.currentOp + 2];
        this.y += this.operands[this.currentOp + 3];
        numArray[2] = (float) this.x;
        numArray[3] = (float) this.y;
        this.x += this.operands[this.currentOp + 4];
        this.y += this.operands[this.currentOp + 5];
        numArray[4] = (float) this.x;
        numArray[5] = (float) this.y;
        this.AddBezierCurve(new string[6]
        {
          numArray[0].ToString(),
          numArray[1].ToString(),
          numArray[2].ToString(),
          numArray[3].ToString(),
          numArray[4].ToString(),
          numArray[5].ToString()
        });
        this.currentOp += 6;
      }

      private void RelativeLineTo()
      {
        for (int index = this.operandReached / 2; index > 0; --index)
        {
          this.x += this.operands[this.currentOp];
          this.y += this.operands[this.currentOp + 1];
          this.AddLine(new string[2]
          {
            this.x.ToString(),
            this.y.ToString()
          });
          this.currentOp += 2;
        }
      }

      private void RelativeMoveTo(bool isFirst)
      {
        if (isFirst && this.operandReached == 3)
          ++this.currentOp;
        this.y += this.operands[this.currentOp + 1];
        this.x += this.operands[this.currentOp];
        this.BeginPath(new string[2]
        {
          this.x.ToString(),
          this.y.ToString()
        });
        this.xs = this.x;
        this.ys = this.y;
      }

      private void RelativeRCurveTo()
      {
        for (int index = this.operandReached / 6; index > 0; --index)
        {
          float[] numArray = new float[6];
          this.x += this.operands[this.currentOp];
          this.y += this.operands[this.currentOp + 1];
          numArray[0] = (float) this.x;
          numArray[1] = (float) this.y;
          this.x += this.operands[this.currentOp + 2];
          this.y += this.operands[this.currentOp + 3];
          numArray[2] = (float) this.x;
          numArray[3] = (float) this.y;
          this.x += this.operands[this.currentOp + 4];
          this.y += this.operands[this.currentOp + 5];
          numArray[4] = (float) this.x;
          numArray[5] = (float) this.y;
          this.AddBezierCurve(new string[6]
          {
            numArray[0].ToString(),
            numArray[1].ToString(),
            numArray[2].ToString(),
            numArray[3].ToString(),
            numArray[4].ToString(),
            numArray[5].ToString()
          });
          this.currentOp += 6;
        }
      }

      private void SetCurrentPoint()
      {
        this.x = this.operands[0];
        this.y = this.operands[1];
        this.BeginPath(new string[2]
        {
          this.x.ToString(),
          this.y.ToString()
        });
      }

      private double SideBearingWidth()
      {
        this.y = this.operands[this.operandReached - 2];
        this.x = this.operands[this.operandReached - 1];
        this.xs = this.x;
        this.ys = this.y;
        this.allowAll = true;
        double y = this.y;
        this.h = this.operands[this.operandReached - 3];
        return y;
      }

      private void StandardEncodingAccent(int rawInt, int currentOp)
      {
      }

      private void VerticalMoveTo(bool isFirst)
      {
        if (isFirst && this.operandReached == 2)
          ++this.currentOp;
        this.y += this.operands[this.currentOp];
        this.BeginPath(new string[2]
        {
          this.x.ToString(),
          this.y.ToString()
        });
        this.xs = this.x;
        this.ys = this.y;
      }

      private void VHHVCurveTo(int key)
      {
        bool flag = key == 31 /*0x1F*/;
        while (this.operandReached >= 4)
        {
          this.operandReached -= 4;
          if (flag)
            this.x += this.operands[this.currentOp];
          else
            this.y += this.operands[this.currentOp];
          this.pt[0] = (float) this.x;
          this.pt[1] = (float) this.y;
          this.x += this.operands[this.currentOp + 1];
          this.y += this.operands[this.currentOp + 2];
          this.pt[2] = (float) this.x;
          this.pt[3] = (float) this.y;
          if (flag)
          {
            this.y += this.operands[this.currentOp + 3];
            if (this.operandReached == 1)
              this.x += this.operands[this.currentOp + 4];
          }
          else
          {
            this.x += this.operands[this.currentOp + 3];
            if (this.operandReached == 1)
              this.y += this.operands[this.currentOp + 4];
          }
          this.pt[4] = (float) this.x;
          this.pt[5] = (float) this.y;
          this.AddBezierCurve(new string[6]
          {
            this.pt[0].ToString(),
            this.pt[1].ToString(),
            this.pt[2].ToString(),
            this.pt[3].ToString(),
            this.pt[4].ToString(),
            this.pt[5].ToString()
          });
          this.currentOp += 4;
          flag = !flag;
        }
      }

      private void VVHHCurveTo(int key)
      {
        bool flag = key == 26;
        if ((this.operandReached & 1) == 1)
        {
          if (flag)
            this.x += this.operands[0];
          else
            this.y += this.operands[0];
          ++this.currentOp;
        }
        while (this.currentOp < this.operandReached)
        {
          if (flag)
            this.y += this.operands[this.currentOp];
          else
            this.x += this.operands[this.currentOp];
          this.pt[0] = (float) this.x;
          this.pt[1] = (float) this.y;
          this.x += this.operands[this.currentOp + 1];
          this.y += this.operands[this.currentOp + 2];
          this.pt[2] = (float) this.x;
          this.pt[3] = (float) this.y;
          if (flag)
            this.y += this.operands[this.currentOp + 3];
          else
            this.x += this.operands[this.currentOp + 3];
          this.pt[4] = (float) this.x;
          this.pt[5] = (float) this.y;
          this.currentOp += 4;
          this.AddBezierCurve(new string[6]
          {
            this.pt[0].ToString(),
            this.pt[1].ToString(),
            this.pt[2].ToString(),
            this.pt[3].ToString(),
            this.pt[4].ToString(),
            this.pt[5].ToString()
          });
        }
      }
    }
}
