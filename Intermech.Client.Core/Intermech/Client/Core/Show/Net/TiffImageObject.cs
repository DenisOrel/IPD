
// Type: Intermech.Client.Core.Show.Net.TiffImageObject
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using BitMiracle.LibTiff.Classic;
using Intermech.Client.Core.Visualizers;
using Intermech.Localization;
using Intermech.Map;
using System;
using System.Diagnostics;
using System.Drawing;


namespace Intermech.Client.Core.Show.Net;

/// <summary>объект с рисунком (в мм)</summary>
public class TiffImageObject : MapObject, IMapRelative, IPager
{
  private TiffImageFrame[] _array;
  private Tiff _tiff;
  private bool _imageDispose;
  private int _indexCurrent;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private object _currentPage;

  public event EventHandler Refit;

  public event EventHandler Refresh;

  /// <summary>Событие перехода на другую страницу</summary>
  public event EventHandler PageChanged;

  public override void Dispose()
  {
    this.Clear();
    if (this._imageDispose && this._tiff != null)
      this._tiff.Dispose();
    this._tiff = (Tiff) null;
    base.Dispose();
  }

  ~TiffImageObject() => this.Clear();

  /// <summary> освободить все ссылки	 </summary>
  private void Clear()
  {
    this._currentPage = (object) null;
    if (this._array != null)
    {
      foreach (TiffImageFrame tiffImageFrame in this._array)
      {
        if (tiffImageFrame is IDisposable disposable)
          disposable.Dispose();
      }
    }
    this._array = (TiffImageFrame[]) null;
  }

  /// <summary>создание объекта с рисунком (в мм) </summary>
  /// <param name="image">рисунок</param>
  /// <param name="imageDispose"></param>
  public TiffImageObject(Tiff tiff, bool imageDispose)
  {
    this._tiff = tiff != null ? tiff : throw new ArgumentNullException();
    this._imageDispose = imageDispose;
    this.Selectable = false;
    this._currentPage = (object) null;
    int length = Math.Max(1, (int) tiff.NumberOfDirectories());
    this._array = new TiffImageFrame[length];
    for (int frameIndex = 0; frameIndex < length; ++frameIndex)
    {
      string name = string.Format(LocalizationHolder.rm.GetString("Client.Core_1241"), (object) (frameIndex + 1));
      this._array[frameIndex] = new TiffImageFrame(name, $"{frameIndex}", this._tiff, frameIndex);
    }
    this.Current = (object) this._array[0];
  }

  public override void Paint(Graphics g, MapView view)
  {
    lock (this)
    {
      try
      {
        RectangleF bounds = base.Bounds;
        if (!(this._currentPage is TiffImageFrame))
          return;
        TiffImageFrame currentPage = this._currentPage as TiffImageFrame;
        g.DrawImage(currentPage.Image, 0.0f, 0.0f, bounds.Width, bounds.Height);
      }
      catch (Exception ex)
      {
        MapObject.Trace("Paint: " + ex.ToString());
        throw ex;
      }
    }
  }

  private void OnRefit()
  {
    if (this.Refit == null)
      return;
    this.Refit((object) this, EventArgs.Empty);
  }

  private void OnRefresh()
  {
    if (this.Refresh == null)
      return;
    this.Refresh((object) this, EventArgs.Empty);
  }

  /// <summary>Текущая страница</summary>
  public object Current
  {
    get => this._currentPage;
    set
    {
      this._currentPage = value;
      if (this._currentPage is TiffImageFrame currentPage)
      {
        currentPage.SelectActiveFrame();
        base.Bounds = currentPage.Bounds;
      }
      if (this.PageChanged != null)
        this.PageChanged((object) this, new EventArgs());
      if (this.Document == null)
        return;
      this.OnRefit();
    }
  }

  /// <summary> Переход на первую страницу</summary>
  public void First()
  {
    object[] pages = this.Pages;
    if (pages == null)
      this.Current = (object) null;
    else if (pages.Length == 0)
      this.Current = (object) null;
    else
      this.Current = pages[this._indexCurrent = 0];
  }

  /// <summary> Переход на следующую страницу</summary>
  public void Next()
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

  /// <summary>Переход на предыдущую страницу</summary>
  public void Prev()
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

  /// <summary>Переход на последнюю страницу</summary>
  public void Last()
  {
    object[] pages = this.Pages;
    if (pages == null)
      this.Current = (object) null;
    else if (pages.Length == 0)
      this.Current = (object) null;
    else
      this.Current = (object) this._array[this._indexCurrent = this._array.Length - 1];
  }

  /// <summary> Список страниц </summary>
  public object[] Pages => (object[]) this._array;

  /// <summary>найти элемент в документе </summary>
  /// <param name="id">ID элемента в документе</param>
  /// <returns>элемента в документе если есть,иначе null</returns>
  private TiffImageFrame FindId(string id)
  {
    foreach (TiffImageFrame page in this.Pages)
    {
      if (id == page.NameId)
        return page;
    }
    return (TiffImageFrame) null;
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
    this.FindId(id);
    return PointF.Empty;
  }

  /// <summary> проверить сущетвование элемента в документе</summary>
  /// <param name="id">ID элемента в документе</param>
  /// <returns>true, если элемент существует</returns>
  public bool CheckElementId(string id) => id == null || this.FindId(id) != null;

  /// <summary> видим ли графику к указанному элементу </summary>
  /// <param name="id">ID элемента в документе</param>
  /// <returns>true, если элемент видим</returns>
  public bool GetVisible(string id)
  {
    return this.Current != null && id == (this.Current as TiffImageFrame).NameId;
  }

  /// <summary>получить ID текущей страницы в документе</summary>
  /// <returns>ID текущей страницы в документе</returns>
  public string GetCurrentPageId()
  {
    return this.Current == null ? (string) null : (this.Current as TiffImageFrame).NameId;
  }

  /// <summary>
  /// Получение страницы в документе для указанного элемента
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  public object GetPage(string id) => (object) this.FindId(id);

  /// <summary>
  /// Получение ID страницы в документе для указанного элемента
  /// </summary>
  /// <param name="id">ID элемента в документе</param>
  /// <returns></returns>
  public object GetPageId(string id)
  {
    return !(this.GetPage(id) is TiffImageFrame page) ? (object) string.Empty : (object) page.NameId;
  }

  public override RectangleF Bounds
  {
    get => base.Bounds;
    set => base.Bounds = value;
  }
}
