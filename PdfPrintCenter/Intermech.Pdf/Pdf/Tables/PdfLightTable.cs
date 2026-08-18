// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Tables.PdfLightTable
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using System;
using System.Data;
using System.Drawing;

#nullable disable
namespace Syncfusion.Pdf.Tables;

public class PdfLightTable : PdfLayoutElement
{
  private bool m_allowRowBreakAcrossPages = true;
  private PdfColumnCollection m_columns;
  private string m_dataMember;
  private object m_dataSource;
  private PdfLightTableDataSourceType m_dataSourceType;
  private PdfDataSource m_dsParser;
  private PdfLightTableStyle m_properties;
  private PdfRowCollection m_rows;

  public event BeginCellLayoutEventHandler BeginCellLayout;

  public event BeginRowLayoutEventHandler BeginRowLayout;

  public event EndCellLayoutEventHandler EndCellLayout;

  public event EndRowLayoutEventHandler EndRowLayout;

  public event QueryColumnCountEventHandler QueryColumnCount;

  public event QueryNextRowEventHandler QueryNextRow;

  public event QueryRowCountEventHandler QueryRowCount;

  private PdfColumnCollection CreateColumns()
  {
    PdfColumnCollection columns = new PdfColumnCollection();
    int num = this.m_dsParser != null ? this.m_dsParser.ColumnCount : this.OnGetColumnNumber();
    for (int index = 0; index < num; ++index)
    {
      PdfColumn column = new PdfColumn(10f);
      columns.Add(column);
    }
    return columns;
  }

  private PdfDataSource CreateDataSourceConsumer(object value)
  {
    Array array = value as Array;
    DataSet dataSet = value as DataSet;
    DataColumn column = value as DataColumn;
    DataTable table = value as DataTable;
    DataView view = value as DataView;
    PdfDataSource dataSourceConsumer = (PdfDataSource) null;
    if (array != null)
      return new PdfDataSource(array);
    if (column != null)
      return new PdfDataSource(column);
    if (table != null)
      return new PdfDataSource(table);
    if (view != null)
      return new PdfDataSource(view);
    if (dataSet != null)
      dataSourceConsumer = new PdfDataSource(dataSet, this.m_dataMember);
    return dataSourceConsumer;
  }

  private PdfRowCollection CreateRows()
  {
    PdfRowCollection rows = new PdfRowCollection();
    int num = this.m_dsParser != null ? this.m_dsParser.RowCount : this.OnGetRowNumber();
    for (int index = 0; index < num; ++index)
    {
      PdfRow row = new PdfRow();
      rows.Add(row);
    }
    return rows;
  }

  public void Draw(PdfGraphics graphics, RectangleF bounds)
  {
    if (this.m_dataSourceType == PdfLightTableDataSourceType.TableDirect)
      this.DataSource = this.FillData();
    new LightTableLayouter(this).Layout(graphics, bounds);
  }

  public PdfLightTableLayoutResult Draw(PdfPage page, PointF location)
  {
    if (this.m_dataSourceType == PdfLightTableDataSourceType.TableDirect)
      this.DataSource = this.FillData();
    return (PdfLightTableLayoutResult) base.Draw(page, location);
  }

  public PdfLightTableLayoutResult Draw(PdfPage page, RectangleF bounds)
  {
    if (this.m_dataSourceType == PdfLightTableDataSourceType.TableDirect)
      this.DataSource = this.FillData();
    return (PdfLightTableLayoutResult) base.Draw(page, bounds);
  }

  public void Draw(PdfGraphics graphics, PointF location, float width)
  {
    this.Draw(graphics, location.X, location.Y, width);
  }

  public override void Draw(PdfGraphics graphics, float x, float y)
  {
    SizeF clientSize = graphics.ClientSize;
    clientSize.Width -= x;
    clientSize.Height -= y;
    this.Draw(graphics, x, y, clientSize.Width);
  }

  public PdfLightTableLayoutResult Draw(
    PdfPage page,
    PointF location,
    PdfLightTableLayoutFormat format)
  {
    if (this.m_dataSourceType == PdfLightTableDataSourceType.TableDirect)
      this.DataSource = this.FillData();
    return (PdfLightTableLayoutResult) this.Draw(page, location, (PdfLayoutFormat) format);
  }

  public PdfLightTableLayoutResult Draw(
    PdfPage page,
    RectangleF bounds,
    PdfLightTableLayoutFormat format)
  {
    if (this.m_dataSourceType == PdfLightTableDataSourceType.TableDirect)
      this.DataSource = this.FillData();
    return (PdfLightTableLayoutResult) this.Draw(page, bounds, (PdfLayoutFormat) format);
  }

  public PdfLightTableLayoutResult Draw(PdfPage page, float x, float y)
  {
    if (this.m_dataSourceType == PdfLightTableDataSourceType.TableDirect)
      this.DataSource = this.FillData();
    return (PdfLightTableLayoutResult) base.Draw(page, x, y);
  }

  public void Draw(PdfGraphics graphics, float x, float y, float width)
  {
    RectangleF bounds = new RectangleF(x, y, width, 0.0f);
    this.Draw(graphics, bounds);
  }

  public PdfLightTableLayoutResult Draw(
    PdfPage page,
    float x,
    float y,
    PdfLightTableLayoutFormat format)
  {
    if (this.m_dataSourceType == PdfLightTableDataSourceType.TableDirect)
      this.DataSource = this.FillData();
    return (PdfLightTableLayoutResult) this.Draw(page, x, y, (PdfLayoutFormat) format);
  }

  public PdfLightTableLayoutResult Draw(PdfPage page, float x, float y, float width)
  {
    return this.Draw(page, x, y, width, (PdfLightTableLayoutFormat) null);
  }

  public PdfLightTableLayoutResult Draw(
    PdfPage page,
    float x,
    float y,
    float width,
    PdfLightTableLayoutFormat format)
  {
    if (this.m_dataSourceType == PdfLightTableDataSourceType.TableDirect)
      this.DataSource = this.FillData();
    RectangleF layoutRectangle = new RectangleF(x, y, width, 0.0f);
    return (PdfLightTableLayoutResult) this.Draw(page, layoutRectangle, (PdfLayoutFormat) format);
  }

  protected override void DrawInternal(PdfGraphics graphics)
  {
    new LightTableLayouter(this).Layout(graphics, PointF.Empty);
  }

  private object FillData()
  {
    try
    {
      DataTable dataTable = new DataTable();
      for (int index = 0; index < this.Columns.Count; ++index)
      {
        if (this.Columns[index].ColumnName != null)
          dataTable.Columns.Add(this.Columns[index].ColumnName);
        else
          dataTable.Columns.Add(string.Empty);
        this.Columns[index].Width = this.Columns[index].Width;
        this.Columns[index].StringFormat = this.Columns[index].StringFormat;
      }
      foreach (PdfRow row in (PdfCollection) this.Rows)
      {
        if (row.Values != null)
          dataTable.Rows.Add(row.Values);
      }
      return (object) dataTable;
    }
    catch (Exception ex)
    {
      throw new PdfException("Please check whether the number of rows matches the column count.", ex);
    }
  }

  internal string[] GetColumnCaptions()
  {
    PdfColumnCollection columns = this.Columns;
    string[] columnCaptions = this.m_dsParser != null ? this.m_dsParser.ColumnCaptions : (string[]) null;
    for (int index = 0; index < this.m_dsParser.ColumnCount; ++index)
    {
      if (columns[index].ColumnName != null)
      {
        if (columnCaptions == null)
          columnCaptions = new string[this.m_dsParser.ColumnCount];
        columnCaptions[index] = columns[index].ColumnName;
      }
    }
    return columnCaptions;
  }

  internal string[] GetNextRow(ref int index)
  {
    return this.m_dsParser == null ? this.OnGetNextRow(index) : this.m_dsParser.GetRow(ref index);
  }

  protected override PdfLayoutResult Layout(PdfLayoutParams param)
  {
    if ((double) param.Bounds.Width < 0.0)
      throw new ArgumentOutOfRangeException("Width");
    return new LightTableLayouter(this).Layout(param);
  }

  internal void OnBeginCellLayout(BeginCellLayoutEventArgs args)
  {
    if (!this.RaiseBeginCellLayout)
      return;
    this.BeginCellLayout((object) this, args);
  }

  internal void OnBeginRowLayout(BeginRowLayoutEventArgs args)
  {
    if (!this.RaiseBeginRowLayout)
      return;
    this.BeginRowLayout((object) this, args);
  }

  internal void OnEndCellLayout(EndCellLayoutEventArgs args)
  {
    if (!this.RaiseEndCellLayout)
      return;
    this.EndCellLayout((object) this, args);
  }

  internal void OnEndRowLayout(EndRowLayoutEventArgs args)
  {
    if (!this.RaiseEndRowLayout)
      return;
    this.EndRowLayout((object) this, args);
  }

  private int OnGetColumnNumber()
  {
    int num = 0;
    if (this.QueryColumnCount != null)
    {
      QueryColumnCountEventArgs args = new QueryColumnCountEventArgs();
      this.QueryColumnCount((object) this, args);
      num = args.ColumnCount;
    }
    return num >= 0 ? num : throw new PdfLightTableException("There is no columns.");
  }

  private string[] OnGetNextRow(int rowIndex)
  {
    string[] nextRow = (string[]) null;
    if (this.QueryNextRow != null)
    {
      QueryNextRowEventArgs args = new QueryNextRowEventArgs(this.Columns.Count, rowIndex);
      this.QueryNextRow((object) this, args);
      nextRow = args.RowData;
    }
    return nextRow;
  }

  private int OnGetRowNumber()
  {
    int num = 0;
    if (this.QueryColumnCount != null)
    {
      QueryRowCountEventArgs args = new QueryRowCountEventArgs();
      this.QueryRowCount((object) this, args);
      num = args.RowCount;
    }
    return num >= 0 ? num : throw new PdfLightTableException("There is no Rows.");
  }

  public bool AllowRowBreakAcrossPages
  {
    get => this.m_allowRowBreakAcrossPages;
    set => this.m_allowRowBreakAcrossPages = value;
  }

  public PdfColumnCollection Columns
  {
    get
    {
      if (this.m_columns == null)
        this.m_columns = this.CreateColumns();
      return this.m_columns;
    }
  }

  public string DataMember
  {
    get => this.m_dataMember;
    set
    {
      if (!(this.m_dataSource is DataSet))
        return;
      this.m_dataMember = value;
      this.m_dsParser = this.CreateDataSourceConsumer(this.m_dataSource);
    }
  }

  public object DataSource
  {
    get => this.m_dataSource;
    set
    {
      this.m_dataSource = value != null ? value : throw new ArgumentNullException(nameof (DataSource));
      this.m_dsParser = this.CreateDataSourceConsumer(value);
      if (this.m_dataSource == null)
        this.m_dataMember = (string) null;
      if (this.DataSourceType == PdfLightTableDataSourceType.TableDirect)
        return;
      this.m_columns = (PdfColumnCollection) null;
    }
  }

  public PdfLightTableDataSourceType DataSourceType
  {
    get => this.m_dataSourceType;
    set => this.m_dataSourceType = value;
  }

  public bool IgnoreSorting
  {
    get
    {
      bool ignoreSorting = true;
      if (this.m_dsParser != null)
        ignoreSorting = this.m_dsParser.UseSorting;
      return ignoreSorting;
    }
    set
    {
      if (this.m_dsParser == null)
        return;
      this.m_dsParser.UseSorting = !value;
    }
  }

  internal bool RaiseBeginCellLayout => this.BeginCellLayout != null;

  internal bool RaiseBeginRowLayout => this.BeginRowLayout != null;

  internal bool RaiseEndCellLayout => this.EndCellLayout != null;

  internal bool RaiseEndRowLayout => this.EndRowLayout != null;

  public PdfRowCollection Rows
  {
    get
    {
      if (this.m_rows == null)
        this.m_rows = this.CreateRows();
      return this.m_rows;
    }
  }

  public PdfLightTableStyle Style
  {
    get
    {
      if (this.m_properties == null)
        this.m_properties = new PdfLightTableStyle();
      return this.m_properties;
    }
    set
    {
      this.m_properties = value != null ? value : throw new ArgumentNullException("Properties");
    }
  }
}
