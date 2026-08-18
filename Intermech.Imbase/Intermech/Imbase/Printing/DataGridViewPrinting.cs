// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Printing.DataGridViewPrinting
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Printing;

public class DataGridViewPrinting
{
  private DataGridView _dgv;
  private PrintDocument _printDoc;
  private RectangleF _footerBounds = (RectangleF) Rectangle.Empty;
  private float _currY;
  private static int _pageNumber = 1;
  private float _pagePrintableAreaWidth;
  private float _pagePrintableAreaHeight;
  private string _titleText = string.Empty;
  private static int _currRow = 0;
  private Margins _margins;
  private Dictionary<int, DataGridViewColumn> _colDisplayedIndexDict = new Dictionary<int, DataGridViewColumn>();
  private Dictionary<int, int> _startNumsRowOnPage = new Dictionary<int, int>();
  private List<int[]> _colPoints = new List<int[]>();
  private List<float> _colPointsWidth = new List<float>();
  private int _colPoint;
  private string _strTable = string.Empty;
  private string _strContinuation = string.Empty;

  public DataGridViewPrinting(DataGridView dgv, PrintDocument printDoc, string titleText)
  {
    this._dgv = dgv;
    this._printDoc = printDoc;
    this._titleText = titleText;
    this._margins = this._printDoc.DefaultPageSettings.Margins;
    DataGridViewPrinting._pageNumber = 1;
    this._strTable = LocalizationHolder.rm.GetString("Imbase_Table");
    this._strContinuation = LocalizationHolder.rm.GetString("Imbase_ContinuationTable");
  }

  private void Calculate()
  {
    this._startNumsRowOnPage.Clear();
    this._startNumsRowOnPage.Add(1, 0);
    this._colDisplayedIndexDict.Clear();
    this._colPoints.Clear();
    this._colPointsWidth.Clear();
    int num1 = 0;
    this.CalculateArea();
    foreach (DataGridViewColumn column in (BaseCollection) this._dgv.Columns)
    {
      if (column.Visible)
      {
        this._colDisplayedIndexDict.Add(column.DisplayIndex, column);
        num1 += column.Width;
      }
    }
    this._colDisplayedIndexDict = this.SortDictionary(this._colDisplayedIndexDict);
    int num2 = 0;
    int num3 = 0;
    if ((double) num1 > (double) this._pagePrintableAreaWidth)
    {
      num1 = 0;
      foreach (KeyValuePair<int, DataGridViewColumn> keyValuePair in this._colDisplayedIndexDict)
      {
        if ((double) keyValuePair.Value.Width > (double) this._pagePrintableAreaWidth)
        {
          if (num1 > 0)
          {
            this._colPoints.Add(new int[2]{ num2, num3 });
            this._colPointsWidth.Add((float) num1);
            num2 = num3;
          }
          this._colPoints.Add(new int[2]{ num2, ++num3 });
          this._colPointsWidth.Add(this._pagePrintableAreaWidth);
          num1 = 0;
          num2 = num3;
        }
        else if ((double) (num1 + keyValuePair.Value.Width) > (double) this._pagePrintableAreaWidth)
        {
          this._colPoints.Add(new int[2]{ num2, num3 });
          this._colPointsWidth.Add((float) num1);
          num1 = keyValuePair.Value.Width;
          num2 = num3++;
        }
        else
        {
          num1 += keyValuePair.Value.Width;
          ++num3;
        }
      }
    }
    this._colPoint = 0;
    if (num1 == 0)
      return;
    this._colPoints.Add(new int[2]
    {
      num2,
      this._colDisplayedIndexDict.Count
    });
    this._colPointsWidth.Add((float) num1);
  }

  private void CalculateArea()
  {
    if (this._printDoc == null)
      return;
    float num;
    float width;
    int height;
    if (!this._printDoc.DefaultPageSettings.Landscape)
    {
      RectangleF printableArea = this._printDoc.DefaultPageSettings.PrintableArea;
      num = printableArea.Height;
      printableArea = this._printDoc.DefaultPageSettings.PrintableArea;
      width = printableArea.Width;
      height = this._margins.Bottom;
    }
    else
    {
      RectangleF printableArea = this._printDoc.DefaultPageSettings.PrintableArea;
      num = printableArea.Width;
      printableArea = this._printDoc.DefaultPageSettings.PrintableArea;
      width = printableArea.Height;
      height = this._margins.Left;
    }
    this._pagePrintableAreaHeight = num - (float) this._margins.Top - (float) this._margins.Bottom;
    this._pagePrintableAreaWidth = width - (float) this._margins.Left - (float) this._margins.Right;
    this._footerBounds = new RectangleF(0.0f, num - (float) height, width, (float) height);
  }

  private void DrawHeader(Graphics g)
  {
    this._currY = (float) this._margins.Top;
    RectangleF empty = RectangleF.Empty;
    using (StringFormat format = new StringFormat())
    {
      string s = $"- {DataGridViewPrinting._pageNumber} -";
      format.Alignment = StringAlignment.Center;
      format.LineAlignment = StringAlignment.Center;
      using (Font font = new Font("Tahoma", 10f, FontStyle.Regular, GraphicsUnit.Point))
      {
        using (SolidBrush solidBrush = new SolidBrush(Color.Black))
          g.DrawString(s, font, (Brush) solidBrush, this._footerBounds, format);
      }
      format.Alignment = StringAlignment.Near;
      using (Font font1 = new Font("Tahoma", 16f, FontStyle.Bold, GraphicsUnit.Point))
      {
        SizeF sizeF1 = g.MeasureString(this._titleText, font1);
        int int32 = Convert.ToInt32(Math.Ceiling((double) sizeF1.Width / (double) this._pagePrintableAreaWidth));
        using (SolidBrush solidBrush = new SolidBrush(Color.Black))
        {
          string str = $"{this._strTable} - {this._titleText}";
          RectangleF layoutRectangle1 = new RectangleF((float) this._margins.Left, this._currY, this._pagePrintableAreaWidth, sizeF1.Height * (float) int32);
          g.DrawString(str, font1, (Brush) solidBrush, layoutRectangle1, format);
          this._currY += (float) ((double) sizeF1.Height * (double) int32 + 5.0);
          if (DataGridViewPrinting._currRow > 0)
          {
            using (Font font2 = new Font("Tahoma", 10f, FontStyle.Regular, GraphicsUnit.Point))
            {
              SizeF sizeF2 = g.MeasureString(str, font2);
              RectangleF layoutRectangle2 = new RectangleF((float) this._margins.Left, this._currY, this._pagePrintableAreaWidth, sizeF2.Height);
              g.DrawString(this._strContinuation, font2, (Brush) solidBrush, layoutRectangle2, format);
              this._currY += sizeF2.Height + 10f;
            }
          }
        }
      }
      float left = (float) this._margins.Left;
      Color backColor = this._dgv.ColumnHeadersDefaultCellStyle.BackColor;
      if (backColor.IsEmpty)
        backColor = this._dgv.DefaultCellStyle.BackColor;
      RectangleF rect = new RectangleF(left, this._currY, this._colPointsWidth[this._colPoint], (float) this._dgv.ColumnHeadersHeight);
      using (SolidBrush solidBrush = new SolidBrush(backColor))
        g.FillRectangle((Brush) solidBrush, rect);
      Color foreColor = this._dgv.ColumnHeadersDefaultCellStyle.ForeColor;
      if (foreColor.IsEmpty)
        foreColor = this._dgv.DefaultCellStyle.ForeColor;
      using (SolidBrush solidBrush = new SolidBrush(foreColor))
      {
        using (Pen pen = new Pen(this._dgv.GridColor, 1f))
        {
          Font font = this._dgv.ColumnHeadersDefaultCellStyle.Font ?? this._dgv.DefaultCellStyle.Font;
          format.Trimming = StringTrimming.Word;
          List<int> intList = new List<int>((IEnumerable<int>) this._colDisplayedIndexDict.Keys);
          for (int index = this._colPoints[this._colPoint][0]; index < this._colPoints[this._colPoint][1]; ++index)
          {
            float width = (double) this._colDisplayedIndexDict[intList[index]].Width > (double) this._pagePrintableAreaWidth ? this._pagePrintableAreaWidth : (float) this._colDisplayedIndexDict[intList[index]].Width;
            DataGridViewContentAlignment alignment = this._dgv.ColumnHeadersDefaultCellStyle.Alignment;
            if (alignment.ToString().Contains("Right"))
            {
              format.Alignment = StringAlignment.Far;
            }
            else
            {
              alignment = this._dgv.ColumnHeadersDefaultCellStyle.Alignment;
              format.Alignment = !alignment.ToString().Contains("Center") ? StringAlignment.Near : StringAlignment.Center;
            }
            format.LineAlignment = StringAlignment.Center;
            RectangleF layoutRectangle = new RectangleF(left, this._currY, width, (float) this._dgv.ColumnHeadersHeight);
            g.DrawString(this._colDisplayedIndexDict[intList[index]].HeaderText, font, (Brush) solidBrush, layoutRectangle, format);
            if (this._dgv.RowHeadersBorderStyle != DataGridViewHeaderBorderStyle.None)
              g.DrawRectangle(pen, left, this._currY, width, (float) this._dgv.ColumnHeadersHeight);
            left += width;
          }
        }
      }
    }
    this._currY += (float) this._dgv.ColumnHeadersHeight;
  }

  private void DrawRows(Graphics g)
  {
    using (Pen pen = new Pen(this._dgv.GridColor, 1f))
    {
      using (StringFormat format = new StringFormat())
      {
        format.Trimming = StringTrimming.Word;
        format.FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.LineLimit;
        for (; DataGridViewPrinting._currRow < this._dgv.Rows.Count; ++DataGridViewPrinting._currRow)
        {
          if (this._dgv.Rows[DataGridViewPrinting._currRow].Visible)
          {
            if ((double) this._currY + (double) this._dgv.Rows[DataGridViewPrinting._currRow].Height > (double) this._pagePrintableAreaHeight)
              return;
            Font font = this._dgv.Rows[DataGridViewPrinting._currRow].DefaultCellStyle.Font ?? this._dgv.DefaultCellStyle.Font;
            Color backColor = this._dgv.Rows[DataGridViewPrinting._currRow].DefaultCellStyle.BackColor;
            if (backColor.IsEmpty)
              backColor = this._dgv.DefaultCellStyle.BackColor;
            int left = this._margins.Left;
            RectangleF rect = new RectangleF((float) left, this._currY, this._colPointsWidth[this._colPoint], (float) this._dgv.Rows[DataGridViewPrinting._currRow].Height);
            if (DataGridViewPrinting._currRow % 2 == 0)
            {
              using (SolidBrush solidBrush = new SolidBrush(backColor))
                g.FillRectangle((Brush) solidBrush, rect);
            }
            else
            {
              using (SolidBrush solidBrush = new SolidBrush(this._dgv.AlternatingRowsDefaultCellStyle.BackColor))
                g.FillRectangle((Brush) solidBrush, rect);
            }
            Color foreColor = this._dgv.Rows[DataGridViewPrinting._currRow].DefaultCellStyle.ForeColor;
            if (foreColor.IsEmpty)
              foreColor = this._dgv.DefaultCellStyle.ForeColor;
            using (SolidBrush solidBrush = new SolidBrush(foreColor))
            {
              List<int> intList = new List<int>((IEnumerable<int>) this._colDisplayedIndexDict.Keys);
              for (int index = this._colPoints[this._colPoint][0]; index < this._colPoints[this._colPoint][1]; ++index)
              {
                DataGridViewColumn dataGridViewColumn = this._colDisplayedIndexDict[intList[index]];
                if (dataGridViewColumn.DefaultCellStyle.Alignment.ToString().Contains("Right"))
                {
                  format.Alignment = StringAlignment.Far;
                }
                else
                {
                  DataGridViewContentAlignment alignment = dataGridViewColumn.DefaultCellStyle.Alignment;
                  format.Alignment = !alignment.ToString().Contains("Center") ? StringAlignment.Near : StringAlignment.Center;
                }
                float width = (double) dataGridViewColumn.Width > (double) this._pagePrintableAreaWidth ? this._pagePrintableAreaWidth : (float) dataGridViewColumn.Width;
                RectangleF layoutRectangle = new RectangleF((float) left, this._currY, width, (float) this._dgv.Rows[DataGridViewPrinting._currRow].Height);
                g.DrawString(this._dgv.Rows[DataGridViewPrinting._currRow].Cells[dataGridViewColumn.Index].EditedFormattedValue.ToString(), font, (Brush) solidBrush, layoutRectangle, format);
                if (this._dgv.CellBorderStyle != DataGridViewCellBorderStyle.None)
                  g.DrawRectangle(pen, (float) left, this._currY, width, (float) this._dgv.Rows[DataGridViewPrinting._currRow].Height);
                left += dataGridViewColumn.Width;
              }
            }
            this._currY += (float) this._dgv.Rows[DataGridViewPrinting._currRow].Height;
          }
        }
      }
    }
    ++this._colPoint;
    DataGridViewPrinting._currRow = 0;
  }

  private bool IsContinuedPrinting()
  {
    return this._printDoc.PrintController.IsPreview || this._printDoc.PrinterSettings.PrintRange == PrintRange.AllPages ? this._colPoint < this._colPoints.Count : DataGridViewPrinting._pageNumber <= this._printDoc.PrinterSettings.ToPage;
  }

  private Dictionary<int, DataGridViewColumn> SortDictionary(
    Dictionary<int, DataGridViewColumn> dict)
  {
    List<int> intList = new List<int>((IEnumerable<int>) dict.Keys);
    intList.Sort();
    Dictionary<int, DataGridViewColumn> dictionary = new Dictionary<int, DataGridViewColumn>(dict.Count);
    foreach (int key in intList)
      dictionary.Add(key, dict[key]);
    return dictionary;
  }

  public void BeginPrint()
  {
    if (this._printDoc.PrintController.IsPreview)
    {
      this.Calculate();
    }
    else
    {
      if (this._printDoc.PrinterSettings.PrintRange == PrintRange.AllPages)
        return;
      DataGridViewPrinting._pageNumber = this._printDoc.PrinterSettings.FromPage;
      if (!this._startNumsRowOnPage.ContainsKey(DataGridViewPrinting._pageNumber))
        return;
      DataGridViewPrinting._currRow = this._startNumsRowOnPage[DataGridViewPrinting._pageNumber];
    }
  }

  public void EndPrint()
  {
    DataGridViewPrinting._currRow = this._colPoint = 0;
    DataGridViewPrinting._pageNumber = 1;
  }

  public bool DrawDataGridView(Graphics g)
  {
    try
    {
      this.DrawHeader(g);
      this.DrawRows(g);
      ++DataGridViewPrinting._pageNumber;
      if (this._printDoc.PrintController.IsPreview && this._colPoint < this._colPoints.Count)
        this._startNumsRowOnPage.Add(DataGridViewPrinting._pageNumber, DataGridViewPrinting._currRow);
      return this.IsContinuedPrinting();
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show(ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
      return false;
    }
  }
}
