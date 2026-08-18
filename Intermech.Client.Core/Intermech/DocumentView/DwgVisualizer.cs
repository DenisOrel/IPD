
// Type: Intermech.DocumentView.DwgVisualizer
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Show.Net;
using Intermech.Client.Core.Show.Net.ShowDll;
using Intermech.Client.Core.Show.Net.ShowNew;
using Intermech.Client.Core.Visualizers;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Map;
using System;
using System.Drawing;
using System.IO;


namespace Intermech.DocumentView;

/// <summary>Визуализатор документа Dwg</summary>
public class DwgVisualizer : IVisualizer
{
  /// <summary>Создает объект для визуализации из представленных данных</summary>
  /// <param name="objectId">Идентификатор объекта</param>
  /// <param name="valueIndex">Индекс файла в файловом атрибуте с множеством значений</param>
  /// <param name="fileName">Имя файла</param>
  /// <returns>объект для визуализации из представленных данных ипи null</returns>
  public MapObject GetViewObject(long objectId, int valueIndex, string fileName, byte[] data)
  {
    try
    {
      ShowObject viewObject = new ShowObject(fileName, data, (ExternFileFunction) null);
      viewObject.Selectable = false;
      return (MapObject) viewObject;
    }
    catch (Exception ex)
    {
      Exception exception = ex;
      MapText viewObject = new MapText();
      if (exception.InnerException != null)
        exception = exception.InnerException;
      viewObject.Text = exception.Message;
      viewObject.TextColor = Color.Red;
      return (MapObject) viewObject;
    }
  }

  internal static void Initialize(IServiceProvider serviceProvider)
  {
    if (serviceProvider.GetService(typeof (IVisualizerService)) is IVisualizerService service)
    {
      DwgVisualizer dwgVisualizer = new DwgVisualizer();
      service.AddVisualizer("dwg", (IVisualizer) dwgVisualizer);
      service.AddVisualizer("dxf", (IVisualizer) dwgVisualizer);
      service.AddVisualizer("sld", (IVisualizer) dwgVisualizer);
      service.AddVisualizer("slb", (IVisualizer) dwgVisualizer);
    }
    DwgVisualizer.ImageVisualizer.Initialize(serviceProvider);
    DwgVisualizer.LibraryImageVisualizer.Initialize(serviceProvider);
  }

  /// <summary>Визуализатор документа Image</summary>
  internal class ImageVisualizer : IVisualizer
  {
    internal static void Initialize(IServiceProvider serviceProvider)
    {
      if (!(serviceProvider.GetService(typeof (IVisualizerService)) is IVisualizerService service))
        return;
      DwgVisualizer.ImageVisualizer imageVisualizer = new DwgVisualizer.ImageVisualizer();
      service.AddVisualizer("bmp", (IVisualizer) imageVisualizer);
      service.AddVisualizer("jpg", (IVisualizer) imageVisualizer);
      service.AddVisualizer("jpeg", (IVisualizer) imageVisualizer);
      service.AddVisualizer("png", (IVisualizer) imageVisualizer);
      service.AddVisualizer("wmf", (IVisualizer) imageVisualizer);
      service.AddVisualizer("emf", (IVisualizer) imageVisualizer);
      service.AddVisualizer("gif", (IVisualizer) imageVisualizer);
      service.AddVisualizer("ico", (IVisualizer) imageVisualizer);
      service.AddVisualizer("tif", (IVisualizer) imageVisualizer);
      service.AddVisualizer("tiff", (IVisualizer) imageVisualizer);
    }

    /// <summary>Создает объект для визуализации из представленных данных</summary>
    /// <param name="objectId">Идентификатор объекта</param>
    /// <param name="valueIndex">Индекс файла в файловом атрибуте с множеством значений</param>
    /// <param name="fileName">Имя файла</param>
    /// <returns>объект для визуализации из представленных данных ипи null</returns>
    public MapObject GetViewObject(long objectId, int valueIndex, string fileName, byte[] data)
    {
      string lower = Path.GetExtension(fileName)?.ToLower();
      Image image1;
      try
      {
        if (data == null || data.Length == 0)
        {
          if (lower == ".ico")
          {
            using (Icon icon = new Icon(fileName))
              image1 = (Image) icon.ToBitmap();
          }
          else
            image1 = Image.FromFile(fileName);
        }
        else
        {
          using (MemoryStream memoryStream = new MemoryStream(data))
          {
            if (lower == ".ico")
            {
              using (Icon icon = new Icon((Stream) memoryStream))
                image1 = (Image) icon.ToBitmap();
            }
            else
            {
              using (Image image2 = Image.FromStream((Stream) memoryStream))
                image1 = (Image) image2.Clone();
            }
          }
        }
      }
      catch (Exception ex)
      {
        MapText viewObject = new MapText();
        viewObject.Multiline = true;
        viewObject.Text = $"Невозможно отобразить файл: '{fileName}'{Environment.NewLine}Ошибка: '{ex.Message}'";
        viewObject.TextColor = Color.Red;
        viewObject.Selectable = false;
        return (MapObject) viewObject;
      }
      ImageObject viewObject1 = new ImageObject(image1, true);
      viewObject1.Selectable = false;
      return (MapObject) viewObject1;
    }
  }

  /// <summary>Визуализатор библиотечных изображений</summary>
  internal class LibraryImageVisualizer : IVisualizer
  {
    internal static void Initialize(IServiceProvider serviceProvider)
    {
      if (!(serviceProvider.GetService(typeof (IVisualizerService)) is IVisualizerService service))
        return;
      DwgVisualizer.LibraryImageVisualizer libraryImageVisualizer = new DwgVisualizer.LibraryImageVisualizer();
      service.AddVisualizer(ExtensionsConsts.LibraryImageExtension, (IVisualizer) libraryImageVisualizer);
    }

    public MapObject GetViewObject(long objectId, int valueIndex, string fileName, byte[] data)
    {
      Image image1 = (Image) null;
      object obj = (object) null;
      IPicturesCache service = ServiceUtils.GetService<IPicturesCache>((object) ServicesManager.ServiceContainer, true);
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(objectId);
        if (dbObject.ObjectType == Intermech.Client.Core.Thumbnail.Consts.ImageLibraryItemTypeID)
        {
          obj = service.GetPicture(objectId);
        }
        else
        {
          IDBAttribute attributeById = dbObject.GetAttributeByID(Intermech.Client.Core.Thumbnail.Consts.ImageAttTypeID);
          if (attributeById != null)
          {
            if (attributeById.AsInteger >= 0L)
              obj = service.GetPicture(attributeById.AsInteger);
          }
        }
      }
      bool imageDispose = false;
      switch (obj)
      {
        case Icon icon:
          image1 = (Image) icon.ToBitmap();
          imageDispose = true;
          break;
        case IThumbImageProvider thumbImageProvider:
          image1 = thumbImageProvider.Image;
          break;
        case Image image2:
          image1 = image2;
          break;
      }
      return image1 == null ? (MapObject) null : (MapObject) new ImageObject(image1, imageDispose);
    }
  }
}
