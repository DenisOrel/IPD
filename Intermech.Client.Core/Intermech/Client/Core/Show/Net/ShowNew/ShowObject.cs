
// Type: Intermech.Client.Core.Show.Net.ShowNew.ShowObject
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Show.Net.DwgLayer;
using Intermech.Client.Core.Show.Net.ShowDll;
using Intermech.Client.Core.Show.Net.ShowNew.Block;
using Intermech.Client.Core.Show.Net.ShowNew.ExternFile;
using Intermech.Client.Core.Show.Net.ShowNew.Layout;
using Intermech.Client.Core.Show.Net.ShowNew.Shape;
using Intermech.Client.Core.Show.Net.Stylus;
using Intermech.Client.Core.Visualizers;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Show;
using Intermech.Map;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Interactive;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;


namespace Intermech.Client.Core.Show.Net.ShowNew;

/// <summary>графика DWG</summary>
public class ShowObject : MapObject, IMapRelative, IPager, IShowDwgWork, IShowDwg
{
  private readonly float _defaultWeight = (float) ShowSetting.Settings[(object) "DefaultWeight"];
  private RectangleD _clipBox = RectangleD.Empty;
  private IBlackWidthService _blackWidthService = (IBlackWidthService) ServicesManager.GetService(typeof (IBlackWidthService));
  /// <summary>старая проверка нужно ли сменить цвета</summary>
  private bool _isChanged;
  private int _indexCurrent;
  private object _currentPage;

  public void SetClip(RectangleD drawBox) => this._clipBox = drawBox;

  /// <summary>цвет подложки</summary>
  public Color PaperColor
  {
    get
    {
      MapDocument document = this.Document;
      Color paperColor = document != null ? document.PaperColor : Color.Empty;
      if (paperColor == Color.Empty)
      {
        MapView view = this.View;
        paperColor = view != null ? view.BackColor : Color.White;
      }
      return paperColor;
    }
  }

  /// <summary>нужно ли приводить все цвета к чёрному</summary>
  private bool IsBlack
  {
    get
    {
      IBlackWidthService blackWidthService = this._blackWidthService;
      return blackWidthService != null && blackWidthService.AllColorToBlack;
    }
  }

  private void _ColorChanged(object sender, EventArgs e) => this._isChanged = true;

  /// <summary>проверить нужно ли сменить цвета</summary>
  public bool CheckColorToBlack()
  {
    if (!this._isChanged)
      return false;
    this.ChangedColorToBlack();
    return true;
  }

  /// <summary>проверка приведения цветов к чёрному</summary>
  public void ChangedColorToBlack()
  {
    if (this.Styluses == null)
      return;
    bool isBlack = this.IsBlack;
    float num = 0.0f;
    foreach (KeyValuePair<DwgColor, IStylus> styluse in this.Styluses)
    {
      DwgColor key = styluse.Key;
      if (!key.IsEmpty)
      {
        IStylus stylus = styluse.Value;
        if (this._blackWidthService != null)
        {
          ColorWidth colorWidth = this._blackWidthService[(byte) stylus.ColorDwg.AcadIndex];
          colorWidth.Used = true;
          num = colorWidth.Width;
        }
        if (isBlack)
        {
          stylus.Weight = (double) num;
          stylus.ColorPen = Color.Black;
        }
        else
        {
          stylus.Weight = 0.0;
          stylus.ColorPen = key.GdiColor;
        }
      }
    }
    this._isChanged = false;
  }

  /// <summary>прорисовка графики из текущего блока или пространства</summary>
  /// <param name="graphics">графика</param>
  /// <param name="view">окно</param>
  public override void Paint(System.Drawing.Graphics graphics, MapView view)
  {
    lock (this)
    {
      GraphicsState gstate = graphics.Save();
      try
      {
        if (this._clipBox != RectangleD.Empty)
          graphics.SetClip(RectangleD.ToRectangleF(this._clipBox));
        switch (this.Current)
        {
          case BlockObject blockObject:
            blockObject.PaintCurrentUnit(graphics);
            break;
          case LayoutObject layoutObject:
            layoutObject.PaintCurrentUnit(graphics);
            break;
        }
      }
      catch (OverflowException ex)
      {
      }
      finally
      {
        graphics.Restore(gstate);
      }
    }
  }

  public event EventHandler Refit;

  public event EventHandler Refresh;

  /// <summary>Событие перехода на другую страницу</summary>
  public event EventHandler PageChanged;

  private void OnRefit()
  {
    EventHandler refit = this.Refit;
    if (refit == null)
      return;
    refit((object) this, EventArgs.Empty);
  }

  private void OnRefresh()
  {
    EventHandler refresh = this.Refresh;
    if (refresh == null)
      return;
    refresh((object) this, EventArgs.Empty);
  }

  /// <summary>Текущая страница</summary>
  public object Current
  {
    get => this._currentPage;
    set
    {
      lock (this)
      {
        switch (this._currentPage = value)
        {
          case IBlock block:
            this.Bounds = RectangleD.ToRectangleF(block.BoundsAll);
            break;
          case ILayout layout:
            this.Bounds = RectangleD.ToRectangleF(layout.BoundsAll);
            break;
        }
        EventHandler pageChanged = this.PageChanged;
        if (pageChanged != null)
          pageChanged((object) this, new EventArgs());
        if (this.Document == null)
          return;
        this.OnRefit();
      }
    }
  }

  /// <summary>Переход на первую страницу</summary>
  public void First()
  {
    lock (this)
    {
      object[] pages = this.Pages;
      if (pages == null)
        this.Current = (object) null;
      else if (pages.Length == 0)
        this.Current = (object) null;
      else
        this.Current = pages[this._indexCurrent = 0];
    }
  }

  /// <summary> Переход на следующую страниц</summary>
  public void Next()
  {
    lock (this)
    {
      object[] pages = this.Pages;
      if (pages == null)
        this.Current = (object) null;
      else if (pages.Length == 0)
      {
        this.Current = (object) null;
      }
      else
      {
        if (this._indexCurrent < pages.Length - 1)
          ++this._indexCurrent;
        this.Current = pages[this._indexCurrent];
      }
    }
  }

  /// <summary> Переход на предыдущую страниц</summary>
  public void Prev()
  {
    lock (this)
    {
      object[] pages = this.Pages;
      if (pages == null)
        this.Current = (object) null;
      else if (pages.Length == 0)
      {
        this.Current = (object) null;
      }
      else
      {
        if (this._indexCurrent > 0)
          --this._indexCurrent;
        this.Current = pages[this._indexCurrent];
      }
    }
  }

  /// <summary> Переход на последнюю страниц</summary>
  public void Last()
  {
    lock (this)
    {
      object[] pages = this.Pages;
      if (pages == null)
        this.Current = (object) null;
      else if (pages.Length == 0)
        this.Current = (object) null;
      else
        this.Current = pages[this._indexCurrent = pages.Length - 1];
    }
  }

  /// <summary> Список страниц </summary>
  public object[] Pages => (object[]) this.Layouts.Array;

  /// <summary>найти элемент в документе </summary>
  /// <param name="id">ID элемента в документе</param>
  /// <returns>элемента в документе если есть,иначе null</returns>
  private object FindId(string id)
  {
    lock (this)
    {
      foreach (object layout in (IEnumerable) this.Layouts)
      {
        if (id == layout.ToString())
          return layout;
      }
      foreach (object block in (IEnumerable) this.Blocks)
      {
        if (id == block.ToString())
          return block;
      }
      return (object) null;
    }
  }

  /// <summary>по точке в документе найти ID элемента состовляющего документ</summary>
  /// <param name="point">по точке в документе </param>
  /// <returns>ID элемента в документе на который указывает точка</returns>
  public string GetId(PointF point) => this.GetCurrentPageId();

  /// <summary>получить базовую точку элемента </summary>
  /// <param name="id">ID элемента в документе</param>
  /// <returns>базовая точка</returns>
  public PointF GetBasePoint(string id)
  {
    lock (this)
      return this.FindId(id) == null ? PointF.Empty : new PointF(0.0f, 0.0f);
  }

  /// <summary> проверить сущетвование элемента в документе</summary>
  /// <param name="id">ID элемента в документе</param>
  /// <returns>true, если элемент существует</returns>
  public bool CheckElementId(string id) => this.FindId(id) != null;

  /// <summary> видим ли графику к указанному элементу </summary>
  /// <param name="id">ID элемента в документе</param>
  /// <returns>true, если элемент видим</returns>
  public bool GetVisible(string id)
  {
    lock (this)
      return this.Current != null && this.Current.ToString() == id;
  }

  /// <summary>получить ID текущей страницы в документе</summary>
  /// <returns>ID текущей страницы в документе</returns>
  public string GetCurrentPageId() => this.Current?.ToString();

  /// <summary>
  /// Получение страницы в документе для указанного элемента
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  public object GetPage(string id) => this.FindId(id);

  /// <summary>
  /// Получение ID страницы в документе для указанного элемента
  /// </summary>
  /// <param name="id">ID элемента в документе</param>
  /// <returns></returns>
  public object GetPageId(string id)
  {
    return (object) this.GetPage(id)?.ToString() ?? (object) string.Empty;
  }

  public override RectangleF Bounds
  {
    get => base.Bounds;
    set => base.Bounds = value;
  }

  public override void Dispose()
  {
    this.Clear();
    base.Dispose();
  }

  ~ShowObject() => this.Clear();

  /// <summary> освободить все ссылки	 </summary>
  internal void Clear()
  {
    lock (this)
    {
      if (this._blackWidthService != null)
      {
        this._blackWidthService.Changed -= new EventHandler(this._ColorChanged);
        this._blackWidthService = (IBlackWidthService) null;
      }
      this._currentPage = (object) null;
      this.Layouts = (ILayoutTable) null;
      this.Blocks = (IBlockTable) null;
      this.Images = (ImageTable) null;
      this.Styluses = (StylusTable) null;
      this.Layers = (ILayerTable) null;
      try
      {
        this.Files?.CloseBase();
      }
      catch
      {
      }
      this.Files = (FileTable) null;
    }
  }

  /// <summary>список внешних файлов</summary>
  internal FileTable Files { get; private set; }

  /// <summary>список типов линий(по цвету примитива) </summary>
  internal StylusTable Styluses { get; private set; }

  /// <summary>список отображаемых рисунков</summary>
  internal ImageTable Images { get; private set; }

  /// <summary>Открытие Dwg-файла и установить связи с DLL</summary>
  private void SubOpen()
  {
    lock (this)
    {
      int num = (int) this.Files.OpenBase(this._defaultWeight);
    }
  }

  /// <summary>разорвать связи с DLL </summary>
  private void SubClose() => this.Files.CloseBase();

  /// <summary>читать данные DWG и работать с графическими данными</summary>
  /// <param name="fileName">имя  файла</param>
  /// <param name="buffer">данные файла</param>
  /// <param name="externFind">делегат для подстановки файла</param>
  public ShowObject(string fileName, byte[] buffer, ExternFileFunction externFind)
  {
    lock (this)
    {
      if (this._blackWidthService != null)
        this._blackWidthService.Changed += new EventHandler(this._ColorChanged);
      this.Files = new FileTable(new FileData(fileName, buffer), externFind);
      this.SubOpen();
      this.Styluses = new StylusTable();
      this.Layers = (ILayerTable) new DwgLayerTable(Intermech.Client.Core.Show.Net.ShowDll.ShowDll.GetLayerNames());
      foreach (ILayer layer in (IEnumerable) this.Layers)
        layer.Visible = Intermech.Client.Core.Show.Net.ShowDll.ShowDll.GetLayerVisible(layer.Index);
      this.Layers[0].Visible = true;
      this.Layouts = (ILayoutTable) new LayoutTable(Intermech.Client.Core.Show.Net.ShowDll.ShowDll.GetLayoutNames(), Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Layout, (IShowDwgWork) this);
      this.Blocks = (IBlockTable) new BlockTable(Intermech.Client.Core.Show.Net.ShowDll.ShowDll.GetBlockNames(), (IShowDwgWork) this);
      this.Images = new ImageTable(Intermech.Client.Core.Show.Net.ShowDll.ShowDll.GetImageOnly());
      this.Current = this.Pages[this._indexCurrent = this.Layouts[this.Layouts.InFile]];
      this.ChangedColorToBlack();
    }
  }

  private void TestPDF()
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      this.CreatePDF((Stream) memoryStream);
      using (FileStream destination = new FileStream("D:\\Output.pdf", FileMode.Create, FileAccess.Write))
        memoryStream.CopyTo((Stream) destination);
    }
  }

  private void CreateCurrentPage(PdfDocument pdfDocument)
  {
    RectangleF bounds = this.Bounds;
    SizeF size1 = bounds.Size;
    if ((double) Math.Abs(size1.Width) < 1.0 / 1000.0 || (double) Math.Abs(size1.Height) < 1.0 / 1000.0)
      return;
    SizeF sizeF = new SizeF(size1.Width, size1.Height);
    System.Drawing.Size size2 = new System.Drawing.Size(ShowObject.UnitsConverter.MmToPixels(size1.Width, ShowObject.UnitsConverter.HorizontalResolution), ShowObject.UnitsConverter.MmToPixels(size1.Height, ShowObject.UnitsConverter.VerticalResolution));
    PdfSection pdfSection = pdfDocument.Sections.Add();
    pdfSection.PageSettings.Margins = new PdfMargins();
    pdfSection.PageSettings.Orientation = (double) sizeF.Width > (double) sizeF.Height ? PdfPageOrientation.Landscape : PdfPageOrientation.Portrait;
    pdfSection.PageSettings.Size = sizeF;
    PdfPage page = pdfSection.Pages.Add();
    PdfBookmark pdfBookmark = pdfDocument.Bookmarks.Add(this.Current.ToString());
    pdfBookmark.Destination = new PdfDestination((PdfPageBase) page);
    pdfBookmark.TextStyle = PdfTextStyle.Bold;
    pdfBookmark.Color = (PdfColor) Color.Red;
    PdfGraphics graphics = page.Graphics;
    graphics.TranslateTransform(-bounds.X, -bounds.Y);
    RectangleD clipBox = RectangleD.Empty;
    if (this.Current is BlockObject current1)
      clipBox = current1.Bounds;
    if (this.Current is LayoutObject current2)
      clipBox = current2.Bounds;
    MatrixD matrixD = new MatrixD();
    this.PaintPdf(graphics, matrixD, clipBox);
  }

  /// <summary>прорисовка графики из текущего блока или пространства в PDF</summary>
  /// <param name="graphics">Graphics для рисования PDF</param>
  /// <param name="matrixD">матрица преобразования для графики</param>
  /// <param name="clipBox">Границы для рисования</param>
  public void PaintPdf(PdfGraphics graphics, MatrixD matrixD, RectangleD clipBox)
  {
    lock (this)
    {
      PdfGraphicsState state = graphics.Save();
      try
      {
        if (clipBox != RectangleD.Empty)
          graphics.SetClip(RectangleD.ToRectangleF(clipBox));
        switch (this.Current)
        {
          case BlockObject blockObject:
            blockObject.PaintCurrentUnit(graphics, matrixD, clipBox);
            break;
          case LayoutObject layoutObject:
            layoutObject.PaintCurrentUnit(graphics, matrixD, clipBox);
            break;
        }
      }
      catch (OverflowException ex)
      {
      }
      finally
      {
        graphics.Restore(state);
      }
    }
  }

  public void CreatePDF(Stream stream)
  {
    PdfDocument pdfDocument = new PdfDocument()
    {
      PageSettings = {
        Orientation = PdfPageOrientation.Landscape
      }
    };
    ShowObject showObject = this;
    object current = showObject.Current;
    try
    {
      foreach (object layout in (IEnumerable) showObject.Layouts)
      {
        this.Current = layout;
        this.CreateCurrentPage(pdfDocument);
      }
    }
    finally
    {
      showObject.Current = current;
    }
    pdfDocument.Save(stream);
    stream.Position = 0L;
    pdfDocument.Close(true);
  }

  /// <summary>список блоков </summary>
  public IBlockTable Blocks { get; private set; }

  /// <summary>список компоновок </summary>
  public ILayoutTable Layouts { get; private set; }

  /// <summary>список слоёв </summary>
  public ILayerTable Layers { get; private set; }

  /// <summary>прочитать графику из компоновки или блока</summary>
  /// <param name="blok">компоновка или блок</param>
  /// <returns>объект работы со списком графики</returns>
  ShapeList IShowDwgWork.SubReadDataShowBlock(IDllIndex objIndex)
  {
    lock (this)
    {
      ShapeList shapeList = new ShapeList();
      this.SubOpen();
      switch (objIndex)
      {
        case ILayout _:
          Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Layout = objIndex.Index;
          Intermech.Client.Core.Show.Net.ShowDll.ShowDll.SetZoomAll_Dwg(0);
          break;
        case IBlock _:
          Intermech.Client.Core.Show.Net.ShowDll.ShowDll.SetZoomAll_Dwg(objIndex.Index);
          break;
      }
      foreach (IDllIndex layer in (IEnumerable) this.Layers)
        Intermech.Client.Core.Show.Net.ShowDll.ShowDll.SetLayerVisible(layer.Index, true);
      try
      {
        if (Intermech.Client.Core.Show.Net.ShowDll.ShowDll.VersionNetShowDLL == 0)
        {
          Rectangle windowDraw = new Rectangle(0, 0, (int) short.MaxValue, (int) short.MaxValue);
          Intermech.Client.Core.Show.Net.ShowDll.ShowDll.SetBounds(windowDraw);
          double num1 = Intermech.Client.Core.Show.Net.ShowDll.ShowDll.StartDrawDwg();
          if (num1 > 1.0)
            windowDraw.Height = (int) ((double) windowDraw.Height / num1);
          if (num1 < 1.0)
            windowDraw.Width = (int) ((double) windowDraw.Width * num1);
          Intermech.Client.Core.Show.Net.ShowDll.ShowDll.SetBounds(windowDraw);
          RectangleF rectangleF = RectangleF.Empty;
          RectangleD box = RectangleD.Empty;
          foreach (ILayer layer in (IEnumerable) this.Layers)
          {
            RectangleF bounds = (RectangleF) Intermech.Client.Core.Show.Net.ShowDll.ShowDll.GetBounds(layer.Index);
            if (!(bounds == RectangleF.Empty))
            {
              rectangleF = RectangleF.Union(rectangleF == RectangleF.Empty ? bounds : rectangleF, bounds);
              RectangleD dwgBounds = Intermech.Client.Core.Show.Net.ShowDll.ShowDll.GetDwgBounds(layer.Index);
              box = RectangleD.Union(box == RectangleD.Empty ? dwgBounds : box, dwgBounds);
            }
          }
          Intermech.Client.Core.Show.Net.ShowDll.ShowDll.SetDwgBounds(box);
          double num2 = box.Height / (double) rectangleF.Height;
          MatrixD matr = new MatrixD();
          matr.Scale(num2, num2);
          matr.Translate(box.X, box.Y, MatrixD.MatrixOrder.Append);
          shapeList.ReadShort(this.Layers, this.Styluses, this.Images, matr, num2, (IShowDwgWork) this);
          this.ChangedColorToBlack();
          return shapeList;
        }
        Intermech.Client.Core.Show.Net.ShowDll.ShowDll.StartDrawDwgDouble();
        shapeList.Read(this.Layers, this.Styluses, this.Images, (IShowDwgWork) this);
        this.ChangedColorToBlack();
      }
      finally
      {
        foreach (ILayer layer in (IEnumerable) this.Layers)
          Intermech.Client.Core.Show.Net.ShowDll.ShowDll.SetLayerVisible(layer.Index, layer.Visible);
      }
      return shapeList;
    }
  }

  /// <summary>прочитать штамп</summary>
  /// <param name="layout">компоновка со штампом</param>
  /// <param name="fileCfgName">имя файла конфигурации штампа</param>
  /// <param name="cfgData">данные файла конфигурации штампа</param>
  /// <returns>список прочитанных данных из штампа; null -нет штампа</returns>
  IStampField[] IShowDwgWork.SubReadScanStamp(ILayout layout, string fileCfgName, byte[] cfgData)
  {
    lock (this)
    {
      this.SubOpen();
      if (!Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Open_Scan_FilesData(fileCfgName, cfgData))
        return (IStampField[]) null;
      Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Layout = layout.Index;
      foreach (ILayer layer in (IEnumerable) this.Layers)
        Intermech.Client.Core.Show.Net.ShowDll.ShowDll.SetLayerVisible(layer.Index, layer.Visible);
      List<KeyValuePair<string, string>> keyValuePairList = Intermech.Client.Core.Show.Net.ShowDll.ShowDll.ScanLayout();
      if (keyValuePairList.Count == 0)
        return (IStampField[]) null;
      IStampField[] stampFieldArray1 = new IStampField[keyValuePairList.Count];
      for (int index1 = 0; index1 < stampFieldArray1.Length; ++index1)
      {
        IStampField[] stampFieldArray2 = stampFieldArray1;
        int index2 = index1;
        KeyValuePair<string, string> keyValuePair = keyValuePairList[index1];
        string key = keyValuePair.Key;
        keyValuePair = keyValuePairList[index1];
        string str = keyValuePair.Value;
        StampObject stampObject = new StampObject(key, str);
        stampFieldArray2[index2] = (IStampField) stampObject;
      }
      return stampFieldArray1;
    }
  }

  private static class UnitsConverter
  {
    internal static readonly float HorizontalResolution = 96f;
    internal static readonly float VerticalResolution = 96f;

    /// <summary>Преобразовать миллиметры в пиксели</summary>
    /// <param name="mm">Миллиметры</param>
    /// <param name="dpi">Точек на дюйм</param>
    public static int MmToPixels(float mm, float dpi) => Convert.ToInt32(mm / 25.4f * dpi);

    /// <summary>Миллиметры в пункты (1/72 дюйма)</summary>
    /// <param name="mm">Миллиметры</param>
    /// <returns>Пункты</returns>
    public static float MmToPointsF(float mm) => mm * 2.83464575f;
  }
}
