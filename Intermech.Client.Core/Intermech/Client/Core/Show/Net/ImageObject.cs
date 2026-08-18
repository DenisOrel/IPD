
// Type: Intermech.Client.Core.Show.Net.ImageObject
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Visualizers;
using Intermech.Localization;
using Intermech.Map;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;


namespace Intermech.Client.Core.Show.Net;

/// <summary>объект с рисунком (в мм)</summary>
public class ImageObject : MapObject, IMapRelative, IPager
{
  private ImageFrame[] _array;
  private Image _image;
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
    if (this._imageDispose && this._image != null)
      this._image.Dispose();
    this._image = (Image) null;
    base.Dispose();
  }

  ~ImageObject() => this.Clear();

  /// <summary> освободить все ссылки	 </summary>
  private void Clear()
  {
    this._currentPage = (object) null;
    if (this._array == null)
      return;
    for (int index = 0; index < this._array.Length; ++index)
    {
      using (this._array[index])
        this._array[index] = (ImageFrame) null;
    }
    this._array = (ImageFrame[]) null;
  }

  /// <summary>создание объекта с рисунком (в мм) </summary>
  /// <param name="image">рисунок</param>
  /// <param name="imageDispose"></param>
  public ImageObject(Image image, bool imageDispose)
  {
    this._image = image != null ? image : throw new ArgumentNullException();
    this._imageDispose = imageDispose;
    this.Selectable = false;
    this._currentPage = (object) null;
    string format = LocalizationHolder.rm.GetString("Client.Core_1241") ?? "Страница {0}";
    Guid[] frameDimensionsList = image.FrameDimensionsList;
    if (frameDimensionsList != null)
    {
      FrameDimension[] frameDimensionArray = new FrameDimension[frameDimensionsList.Length];
      int length = 0;
      for (int index = 0; index < frameDimensionsList.Length; ++index)
      {
        frameDimensionArray[index] = new FrameDimension(frameDimensionsList[index]);
        length += image.GetFrameCount(frameDimensionArray[index]);
      }
      this._array = new ImageFrame[length];
      if (length > 1)
      {
        int index1 = 0;
        int index2 = 0;
        for (; index1 < frameDimensionsList.Length; ++index1)
        {
          FrameDimension frameDimension = frameDimensionArray[index1];
          int frameIndex = 0;
          for (int frameCount = image.GetFrameCount(frameDimension); frameIndex < frameCount; ++frameIndex)
          {
            string name = string.Format(format, (object) (index2 + 1));
            this._array[index2] = new ImageFrame(name, $"{frameDimensionsList[index1]}_{frameIndex}", this._image, frameDimension, frameIndex);
            ++index2;
          }
        }
      }
      if (length == 1)
      {
        FrameDimension frameDimension = frameDimensionArray[0];
        this._array[0] = new ImageFrame(string.Format(format, (object) 1), $"{frameDimensionsList[0]}_{0}", this._image, (FrameDimension) null, 0);
      }
    }
    if (this._array == null || this._array != null && this._array.Length == 0)
    {
      this._array = new ImageFrame[1];
      this._array[0] = new ImageFrame(string.Format(format, (object) 1), $"{Guid.Empty}_{0}", this._image, (FrameDimension) null, 0);
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
        if (!(this._currentPage is ImageFrame))
          return;
        ImageFrame currentPage = this._currentPage as ImageFrame;
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
      if (this._currentPage is ImageFrame currentPage)
        base.Bounds = currentPage.Bounds;
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
  private ImageFrame FindId(string id)
  {
    foreach (ImageFrame page in this.Pages)
    {
      if (id == page.NameId)
        return page;
    }
    return (ImageFrame) null;
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
    return this.Current != null && id == (this.Current as ImageFrame).NameId;
  }

  /// <summary>получить ID текущей страницы в документе</summary>
  /// <returns>ID текущей страницы в документе</returns>
  public string GetCurrentPageId()
  {
    return this.Current == null ? (string) null : (this.Current as ImageFrame).NameId;
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
    return !(this.GetPage(id) is ImageFrame page) ? (object) string.Empty : (object) page.NameId;
  }

  public override RectangleF Bounds
  {
    get => base.Bounds;
    set => base.Bounds = value;
  }
}
