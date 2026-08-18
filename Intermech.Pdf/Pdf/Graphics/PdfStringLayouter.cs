// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.PdfStringLayouter
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;


namespace Syncfusion.Pdf.Graphics
{
    public class PdfStringLayouter
    {
      private PdfFont m_font;
      private PdfStringFormat m_format;
      private float m_pageHeight;
      private StringTokenizer m_reader;
      private RectangleF m_rect;
      private SizeF m_size;
      private string m_text;

      private void AddToLineResult(
        PdfStringLayoutResult lineResult,
        List<LineInfo> lines,
        string line,
        float lineWidth,
        LineType breakType)
      {
        if (lineResult == null)
          throw new ArgumentNullException(nameof (lineResult));
        if (lines == null)
          throw new ArgumentNullException(nameof (lines));
        if (line == null)
          throw new ArgumentNullException(nameof (line));
        lines.Add(new LineInfo()
        {
          Text = line,
          Width = lineWidth,
          LineType = breakType
        });
        SizeF actualSize = lineResult.ActualSize;
        actualSize.Height += this.GetLineHeight();
        actualSize.Width = Math.Max(actualSize.Width, lineWidth);
        lineResult.m_actualSize = actualSize;
      }

      private void Clear()
      {
        this.m_font = (PdfFont) null;
        this.m_format = (PdfStringFormat) null;
        this.m_reader.Close();
        this.m_reader = (StringTokenizer) null;
        this.m_text = (string) null;
      }

      private bool CopyToResult(
        PdfStringLayoutResult result,
        PdfStringLayoutResult lineResult,
        List<LineInfo> lines,
        out int numInserted)
      {
        if (result == null)
          throw new ArgumentNullException(nameof (result));
        if (lineResult == null)
          throw new ArgumentNullException(nameof (lineResult));
        if (lines == null)
          throw new ArgumentNullException(nameof (lines));
        bool result1 = true;
        bool flag = this.m_format != null && !this.m_format.LineLimit;
        SizeF actualSize1 = result.ActualSize;
        float num1 = actualSize1.Height;
        float num2 = this.m_size.Height;
        if ((double) this.m_pageHeight > 0.0 && (double) num2 + (double) this.m_rect.Y > (double) this.m_pageHeight)
        {
          float val1 = this.m_rect.Y - this.m_pageHeight;
          num2 = Math.Max(val1, -val1);
        }
        numInserted = 0;
        if (lineResult.Lines != null)
        {
          int index = 0;
          for (int length = lineResult.Lines.Length; index < length; ++index)
          {
            float num3 = num1 + lineResult.LineHeight;
            if ((((double) num3 <= (double) num2 ? 1 : ((double) num2 <= 0.0 ? 1 : 0)) | (flag ? 1 : 0)) != 0)
            {
              LineInfo line = lineResult.Lines[index];
              numInserted += line.Text.Length;
              LineInfo lineInfo = this.TrimLine(line, lines.Count == 0);
              lines.Add(lineInfo);
              SizeF actualSize2 = result.ActualSize;
              actualSize2.Width = Math.Max(actualSize2.Width, lineInfo.Width);
              result.m_actualSize = actualSize2;
              if ((((double) num3 < (double) num2 ? 0 : ((double) num2 > 0.0 ? 1 : 0)) & (flag ? 1 : 0)) != 0)
              {
                if (this.m_format == null || !this.m_format.NoClip)
                {
                  float num4 = num3 - num2;
                  float num5 = lineResult.LineHeight - num4;
                  num1 += num5;
                }
                else
                  num1 = num3;
                result1 = false;
                break;
              }
              num1 = num3;
            }
            else
            {
              result1 = false;
              break;
            }
          }
        }
        double num6 = (double) num1;
        actualSize1 = result.ActualSize;
        double height = (double) actualSize1.Height;
        if (num6 != height)
        {
          SizeF actualSize3 = result.ActualSize with
          {
            Height = num1
          };
          result.m_actualSize = actualSize3;
        }
        return result1;
      }

      private PdfStringLayoutResult DoLayout()
      {
        PdfStringLayoutResult result = new PdfStringLayoutResult();
        PdfStringLayoutResult stringLayoutResult = new PdfStringLayoutResult();
        List<LineInfo> lines = new List<LineInfo>();
        string line = this.m_reader.PeekLine();
        float lineIndent = this.GetLineIndent(true);
        while (line != null)
        {
          PdfStringLayoutResult lineResult = this.LayoutLine(line, lineIndent);
          if (!lineResult.Empty)
          {
            int numInserted = 0;
            if (!this.CopyToResult(result, lineResult, lines, out numInserted))
            {
              this.m_reader.Read(numInserted);
              break;
            }
          }
          if (lineResult.Remainder == null || lineResult.Remainder.Length <= 0)
          {
            this.m_reader.ReadLine();
            line = this.m_reader.PeekLine();
            lineIndent = this.GetLineIndent(false);
          }
          else
            break;
        }
        this.FinalizeResult(result, lines);
        return result;
      }

      private void FinalizeResult(PdfStringLayoutResult result, List<LineInfo> lines)
      {
        if (result == null)
          throw new ArgumentNullException(nameof (result));
        result.m_lines = lines != null ? lines.ToArray() : throw new ArgumentNullException(nameof (lines));
        result.m_lineHeight = this.GetLineHeight();
        if (!this.m_reader.EOF)
          result.m_remainder = this.m_reader.ReadToEnd();
        lines.Clear();
      }

      private float GetLineHeight()
      {
        float lineHeight = this.m_font.Height;
        if (this.m_format != null && (double) this.m_format.LineSpacing != 0.0)
          lineHeight = this.m_format.LineSpacing;
        return lineHeight;
      }

      private float GetLineIndent(bool firstLine)
      {
        float lineIndent = 0.0f;
        if (this.m_format != null)
        {
          float val2 = firstLine ? this.m_format.FirstLineIndent : this.m_format.ParagraphIndent;
          lineIndent = (double) this.m_size.Width > 0.0 ? Math.Min(this.m_size.Width, val2) : val2;
        }
        return lineIndent;
      }

      private float GetLineWidth(string line) => this.m_font.GetLineWidth(line, this.m_format);

      private PdfWordWrapType GetWrapType()
      {
        return this.m_format == null ? PdfWordWrapType.Word : this.m_format.WordWrap;
      }

      private void Initialize(string text, PdfFont font, PdfStringFormat format, SizeF size)
      {
        this.Initialize(text, font, format, new RectangleF(PointF.Empty, size), 0.0f);
      }

      private void Initialize(
        string text,
        PdfFont font,
        PdfStringFormat format,
        RectangleF rect,
        float pageHeight)
      {
        if (text == null)
          throw new ArgumentNullException(nameof (text));
        if (font == null)
          throw new ArgumentNullException(nameof (font));
        this.m_text = text;
        this.m_font = font;
        this.m_format = format;
        this.m_size = rect.Size;
        this.m_rect = rect;
        this.m_pageHeight = pageHeight;
        this.m_reader = new StringTokenizer(text);
      }

      public PdfStringLayoutResult Layout(
        string text,
        PdfFont font,
        PdfStringFormat format,
        SizeF size)
      {
        this.Initialize(text, font, format, size);
        PdfStringLayoutResult stringLayoutResult = this.DoLayout();
        this.Clear();
        return stringLayoutResult;
      }

      internal PdfStringLayoutResult Layout(
        string text,
        PdfFont font,
        PdfStringFormat format,
        RectangleF rect,
        float pageHeight)
      {
        this.Initialize(text, font, format, rect, pageHeight);
        PdfStringLayoutResult stringLayoutResult = this.DoLayout();
        this.Clear();
        return stringLayoutResult;
      }

      private PdfStringLayoutResult LayoutLine(string line, float lineIndent)
      {
        line = line != null ? line.Replace("\t", "    ") : throw new ArgumentNullException(nameof (line));
        PdfStringLayoutResult lineResult = new PdfStringLayoutResult();
        lineResult.m_lineHeight = this.GetLineHeight();
        List<LineInfo> lines = new List<LineInfo>();
        float width = this.m_size.Width;
        float lineWidth1 = this.GetLineWidth(line) + lineIndent;
        LineType lineType = LineType.FirstParagraphLine;
        bool flag = true;
        if ((double) width <= 0.0 || Math.Round((double) lineWidth1, 2) <= Math.Round((double) width, 2))
        {
          this.AddToLineResult(lineResult, lines, line, lineWidth1, LineType.NewLineBreak | lineType);
        }
        else
        {
          StringBuilder stringBuilder1 = new StringBuilder();
          StringBuilder stringBuilder2 = new StringBuilder();
          float lineWidth2 = lineIndent;
          float num1 = lineIndent;
          StringTokenizer stringTokenizer = new StringTokenizer(line);
          string str = stringTokenizer.PeekWord();
          if (str.Length != stringTokenizer.Length && str == " ")
          {
            ++stringTokenizer.Position;
            str = stringTokenizer.PeekWord();
          }
          while (str != null)
          {
            stringBuilder2.Append(str);
            float num2 = this.GetLineWidth(stringBuilder2.ToString()) + num1;
            if ((double) num2 > (double) width)
            {
              if (this.GetWrapType() != PdfWordWrapType.None)
              {
                if (stringBuilder2.Length == str.Length)
                {
                  if (this.GetWrapType() == PdfWordWrapType.WordOnly || stringBuilder2.Length == 1)
                  {
                    lineResult.m_remainder = line.Substring(stringTokenizer.Position);
                    break;
                  }
                  flag = false;
                  stringBuilder2.Length = 0;
                  str = stringTokenizer.Peek().ToString();
                }
                else if (this.GetWrapType() != PdfWordWrapType.Character || !flag)
                {
                  string line1 = stringBuilder1.ToString();
                  if (line1 != " ")
                    this.AddToLineResult(lineResult, lines, line1, lineWidth2, LineType.LayoutBreak | lineType);
                  stringBuilder2.Length = 0;
                  stringBuilder1.Length = 0;
                  lineWidth2 = 0.0f;
                  num1 = 0.0f;
                  lineType = LineType.None;
                  str = flag ? str : stringTokenizer.PeekWord();
                  flag = true;
                }
                else
                {
                  flag = false;
                  stringBuilder2.Length = 0;
                  stringBuilder2.Append(stringBuilder1.ToString());
                  str = stringTokenizer.Peek().ToString();
                }
              }
              else
                break;
            }
            else
            {
              stringBuilder1.Append(str);
              lineWidth2 = num2;
              if (flag)
              {
                stringTokenizer.ReadWord();
                str = stringTokenizer.PeekWord();
              }
              else
              {
                int num3 = (int) stringTokenizer.Read();
                str = stringTokenizer.Peek().ToString();
              }
            }
          }
          if (stringBuilder1.Length > 0)
          {
            string line2 = stringBuilder1.ToString();
            this.AddToLineResult(lineResult, lines, line2, lineWidth2, LineType.LastParagraphLine | LineType.NewLineBreak);
          }
          stringTokenizer.Close();
        }
        lineResult.m_lines = lines.ToArray();
        lines.Clear();
        return lineResult;
      }

      private LineInfo TrimLine(LineInfo info, bool firstLine)
      {
        string str = info.Text;
        float num1 = info.Width;
        int num2 = (info.LineType & LineType.FirstParagraphLine) == LineType.None ? 1 : 0;
        bool flag = this.m_format == null || !this.m_format.RightToLeft;
        char[] spaces = StringTokenizer.Spaces;
        if (num2 != 0)
          str = flag ? str.TrimStart(spaces) : str.TrimEnd(spaces);
        if (this.m_format == null || !this.m_format.MeasureTrailingSpaces)
          str = (info.LineType & LineType.FirstParagraphLine) <= LineType.None || !StringTokenizer.IsWhitespace(str) ? (flag ? str.TrimEnd(spaces) : str.TrimStart(spaces)) : new string(' ', 1);
        if (str.Length != info.Text.Length)
        {
          num1 = this.GetLineWidth(str);
          if ((info.LineType & LineType.FirstParagraphLine) > LineType.None)
            num1 += this.GetLineIndent(firstLine);
        }
        info.Text = str;
        info.Width = num1;
        return info;
      }
    }
}
