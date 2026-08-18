
// Type: Intermech.Client.Core.Thumbnail.BitmapCreator
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;


namespace Intermech.Client.Core.Thumbnail;

/// <summary>Summary description for StandardImageCreator.</summary>
public class BitmapCreator : 
  IThumbImageCreator,
  IObjectCreatorRiderCustomService,
  IObjectCreatorCustomService
{
  private IDictionary<ObjectCreatePages, bool> _createPages;
  private string _fileName = string.Empty;

  /// <summary>Зарегистрировать класс.</summary>
  /// <param name="service"></param>
  public static void Attach(IObjectCreatorService service)
  {
    service.RegisterCreatorCustomService(Consts.ImageLibraryItemTypeID, typeof (BitmapCreator));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="stream">содержимое рисунка</param>
  /// <param name="ext">расширение</param>
  /// <returns></returns>
  public object CreateFromStream(Stream stream, string ext)
  {
    stream.Position = 0L;
    string lower = ext.ToLower();
    // ISSUE: reference to a compiler-generated method
    switch (\u003CPrivateImplementationDetails\u003E.ComputeStringHash(lower))
    {
      case 65876325:
        if (lower == "wmf")
          goto label_17;
        goto default;
      case 1581869418:
        if (lower == "tiff")
          break;
        goto default;
      case 1714084033:
        if (lower == "gif")
          break;
        goto default;
      case 1748353692:
        if (lower == "png")
          break;
        goto default;
      case 1824651960:
        if (lower == "bmp")
          break;
        goto default;
      case 2055912771:
        if (lower == "emf")
          goto label_17;
        goto default;
      case 2095122494:
        if (lower == "ico")
        {
          try
          {
            return (object) new Icon(stream);
          }
          catch (ArgumentException ex)
          {
            stream.Position = 0L;
            return (object) new Bitmap(stream);
          }
        }
        else
          goto default;
      case 3202323235:
        if (lower == "jpeg")
          break;
        goto default;
      case 3305831240:
        if (lower == "tif")
          break;
        goto default;
      case 3670499120:
        if (lower == "jpg")
          break;
        goto default;
      case 3689754767:
        if (lower == "exif")
          break;
        goto default;
      default:
        return (object) null;
    }
    try
    {
      return (object) new Bitmap(stream);
    }
    catch (Exception ex)
    {
      stream.Position = 0L;
      return (object) new AcadSlide(stream);
    }
label_17:
    return (object) new Metafile(stream);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="ObjectTypeID"></param>
  /// <param name="TemplateObjectID"></param>
  /// <param name="RelationTypeIDs"></param>
  /// <param name="RelatedObjectIDs"></param>
  /// <param name="StartDate"></param>
  /// <param name="isVersion"></param>
  /// <returns></returns>
  public long CreateObjectDialog(
    int ObjectTypeID,
    long TemplateObjectID,
    int[] RelationTypeIDs,
    long[] RelatedObjectIDs,
    DateTime StartDate,
    bool isVersion)
  {
    return 0;
  }

  public bool OnBeforeCommitAction(IUserSession session, IDBObject newObject) => true;

  /// <summary>
  /// Вызывать собственный диалог ?
  /// Если здесь вернуть true, то вызовется диалог создания объектов реализованный в функции CreateObjectDialog подписчика
  /// на конкретный тип объектов, если же вернуть false, то вызоветься стандартный диалог создания объекта
  /// с изменениями, реализованными подписчиком (см. функции интерфейса.
  /// </summary>
  /// <param name="ObjectTypeID">Идентификатор типа создаваемого объекта</param>
  /// <param name="TemplateObjectID">Идентификатор объекта-прототипа</param>
  /// <param name="RelationTypeIDs">Массив идентификаторов связей которые необходимо создавать</param>
  /// <param name="RelatedObjectIDs">Массив идентификаторов объектов с которыми надо связать созданный объект</param>
  /// <param name="StartDate">Время с которого начинают действовать связи (если они были созданы)</param>
  /// <param name="isVersion">Признак, нужно ли создавать версию объекта</param>
  /// <returns>Результат</returns>
  public bool AcceptDialog(
    int ObjectTypeID,
    long TemplateObjectID,
    int[] RelationTypeIDs,
    long[] RelatedObjectIDs,
    DateTime StartDate,
    bool isVersion)
  {
    if (!(ServicesManager.GetService(typeof (IPicturesCache)) is IPicturesCache service))
      return true;
    OpenFileDialog openFileDialog = new OpenFileDialog();
    openFileDialog.RestoreDirectory = true;
    openFileDialog.Filter = service.Filter;
    if (openFileDialog.ShowDialog() != DialogResult.OK)
      return true;
    this._fileName = openFileDialog.FileName;
    return false;
  }

  /// <summary>
  /// Метод вызывается сразу-же после создания новой заготовки ДО отображения диалога создания.
  /// </summary>
  /// <param name="newObjectID">ID заготовки</param>
  public bool AfterCreate(long newObjectID)
  {
    if (newObjectID == -1L || newObjectID == 0L)
      return false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(newObjectID, false);
      if (objectActualCopy == null)
        return false;
      objectActualCopy.Caption = Path.GetFileNameWithoutExtension(this._fileName);
    }
    return true;
  }

  /// <summary>
  /// Возвращает коллекцию страниц (наследованные от ObjectCreatorControl), которые будут присутствовать в мастере создания объекта,
  /// значение в коллекции обозначает отображать ли эту страницу в мастере.
  /// </summary>
  public IDictionary<ObjectCreatePages, bool> VisiblePages
  {
    get
    {
      if (this._createPages == null)
      {
        this._createPages = (IDictionary<ObjectCreatePages, bool>) new Dictionary<ObjectCreatePages, bool>();
        this._createPages.Add(ObjectCreatePages.Properties, true);
        this._createPages.Add(ObjectCreatePages.Template, true);
      }
      return this._createPages;
    }
  }

  /// <summary>
  /// Метод вызывается по нажатию на кнопку готово.
  /// Внутри не выводить никаких форм !!!!! Этот метод вызывается внутри транзакции !!!
  /// </summary>
  /// <param name="session"></param>
  /// <param name="newObjectID"></param>
  /// <param name="nea">Сюда ложить евенты</param>
  /// <returns></returns>
  public bool OnCommitAction(
    IUserSession session,
    long newObjectID,
    List<NotificationEventArgs> nea)
  {
    if (ServicesManager.GetService(typeof (IPicturesCache)) is IPicturesCache service)
      service.UpdateItem(Consts.ImageLibraryItemTypeID, newObjectID, this._fileName);
    return true;
  }

  /// <summary>
  /// Метод вызывается по нажатию на кнопку отмена.
  /// Внутри не выводить никаких форм !!!!! Этот метод вызывается внутри транзакции !!!
  /// </summary>
  /// <param name="session"></param>
  /// <param name="newObjectID"></param>
  /// <param name="nea"></param>
  /// <returns></returns>
  public bool OnCancelAction(
    IUserSession session,
    long newObjectID,
    List<NotificationEventArgs> nea)
  {
    return true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="CreatedObject"></param>
  /// <param name="propPageIndex"></param>
  /// <returns></returns>
  public Dictionary<UserControl, int> AddPages(object CreatedObject, int propPageIndex)
  {
    return (Dictionary<UserControl, int>) null;
  }
}
