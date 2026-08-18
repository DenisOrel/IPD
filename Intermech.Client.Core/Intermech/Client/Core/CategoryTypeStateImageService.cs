
// Type: Intermech.Client.Core.CategoryTypeStateImageService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary>
/// Реализует сервис для хранения изображений элементов навигации, привязанных к
/// категориям, типам и состояниям элементов.
/// </summary>
public sealed class CategoryTypeStateImageService : ICategoryTypeStateImageService, IDisposable
{
  private Control syncRoot;
  private ICategoryTypeIconService iconService;
  private Dictionary<CategoryTypeStateImageService.ImageKey, int> cache;
  private Brush gapBrush;
  private Rectangle gapRectangle;

  /// <summary>
  /// Создает сервис, позволяя указать контрол, который будет использоваться для
  /// синхронизации доступа к сервису из разных потоков.
  /// </summary>
  /// <param name="syncRoot">Контрол для синхронизации</param>
  public CategoryTypeStateImageService(Control syncRoot)
  {
    if (syncRoot == null)
      throw new ArgumentNullException(nameof (syncRoot), LocalizationHolder.rm.GetString("Client.Core_1053"));
    this.iconService = (ICategoryTypeIconService) ServicesManager.GetService(typeof (ICategoryTypeIconService));
    if (this.iconService == null)
      throw new InvalidOperationException(LocalizationHolder.rm.GetString("Client.Core_1095"));
    this.syncRoot = syncRoot;
    this.gapBrush = (Brush) new SolidBrush(Color.Transparent);
    this.gapRectangle = new Rectangle(0, 0, 7, 16 /*0x10*/);
    this.cache = new Dictionary<CategoryTypeStateImageService.ImageKey, int>();
  }

  /// <summary>
  /// Возвращает индекс зарегистрированного изображения или -1, если изображение не найдено.
  /// </summary>
  /// <param name="categoryId">Требуемая категория</param>
  /// <param name="typeId">Требуемый тип</param>
  /// <param name="data">Дополнительные данные</param>
  /// <param name="state">Состояние элемента навигации</param>
  /// <returns>Индекс изображение в ImageList</returns>
  public int IndexOf(int categoryId, int typeId, object data, object state)
  {
    try
    {
      if (this.syncRoot.InvokeRequired)
        return (int) this.syncRoot.Invoke((Delegate) new CategoryTypeStateImageService.AsyncIndexOf(this.IndexOf), (object) categoryId, (object) typeId, data, state);
      CategoryTypeStateImageService.ImageKey key = new CategoryTypeStateImageService.ImageKey(categoryId, typeId, data, state);
      if (!this.cache.ContainsKey(key))
      {
        int num = this.iconService.IndexOf(categoryId, typeId, data);
        if (num >= 0)
          this.cache.Add(key, num);
      }
      return this.cache.ContainsKey(key) ? this.cache[key] : -1;
    }
    catch
    {
      return -1;
    }
  }

  /// <summary>
  /// Событие, которое возникает, если изображение для запрошенного элемента навигации
  /// еще не загружено.
  /// </summary>
  public event FindStateImageEventHandler FindStateImage;

  public void Dispose()
  {
    if (this.gapBrush == null)
      return;
    this.gapBrush.Dispose();
    this.gapBrush = (Brush) null;
  }

  private Image GetStateImage(CategoryTypeStateImageService.ImageKey key, Image image)
  {
    foreach (FindStateImageEventHandler invocation in this.FindStateImage.GetInvocationList())
    {
      Image stateImage = invocation(key.CategoryId, key.TypeId, key.Data, key.State);
      if (stateImage != null)
      {
        if (stateImage.Width != 7 || stateImage.Height != 16 /*0x10*/)
          throw new InvalidOperationException(LocalizationHolder.rm.GetString("Client.Core_1054"));
        return stateImage;
      }
    }
    return (Image) null;
  }

  private Image GetImage(CategoryTypeStateImageService.ImageKey key, Image image)
  {
    Bitmap image1 = new Bitmap(23, 16 /*0x10*/);
    using (Graphics graphics = Graphics.FromImage((Image) image1))
    {
      graphics.DrawImageUnscaled(image, 7, 0);
      Image stateImage = this.FindStateImage != null ? this.GetStateImage(key, image) : (Image) null;
      if (stateImage != null)
        graphics.DrawImageUnscaled(stateImage, 0, 0);
      else
        graphics.FillRectangle(this.gapBrush, this.gapRectangle);
    }
    return (Image) image1;
  }

  private delegate int AsyncIndexOf(int categoryId, int typeId, object data, object state);

  private class ImageKey
  {
    private int categoryId;
    private int typeId;
    private object data;
    private object state;

    public ImageKey(int categoryId, int typeId, object data, object state)
    {
      this.categoryId = categoryId;
      this.typeId = typeId;
      this.data = data;
      this.state = state;
    }

    public int CategoryId => this.categoryId;

    public int TypeId => this.typeId;

    public object Data => this.data;

    public object State => this.state;

    public override bool Equals(object obj)
    {
      if (!(obj is CategoryTypeStateImageService.ImageKey imageKey))
        return base.Equals(obj);
      return this.categoryId == imageKey.categoryId && this.typeId == imageKey.typeId && object.Equals(this.data, imageKey.data) && object.Equals(this.state, imageKey.state);
    }

    public override int GetHashCode()
    {
      int hashCode = this.categoryId.GetHashCode() << 24 ^ this.typeId.GetHashCode() << 16 /*0x10*/;
      if (this.data != null)
        hashCode ^= this.data.GetHashCode() << 8;
      if (this.state != null)
        hashCode ^= this.state.GetHashCode();
      return hashCode;
    }
  }
}
