// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Creator.TechObjectCreatorRiderCustomService
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Protection;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Creator;

/// <summary>
/// Реализация базового создателя технологических объектов
/// </summary>
/// <remarks>
/// </remarks>
/// &gt;
internal abstract class TechObjectCreatorRiderCustomService : 
  IObjectCreatorRiderParamCustomService,
  IObjectCreatorRiderCustomService,
  IObjectCreatorCustomService
{
  /// <summary>Параметры вызова создания объекта</summary>
  protected TechObjectCreatorArgs _creatorArgs;
  /// <summary>Доп. параметры для создания объекта</summary>
  protected IObjectCreatorParams _creatorExtraParams;

  /// <summary>Сохраним параметры</summary>
  /// <param name="createParams"></param>
  public void SetParams(IObjectCreatorParams createParams)
  {
    this._creatorExtraParams = createParams;
  }

  /// <summary>
  /// Отображение собственного диалога создания объекта, если этого требует AcceptDialog
  /// </summary>
  /// <param name="objectTypeId"></param>
  /// <param name="templateObjectId"></param>
  /// <param name="relationTypeIDs"></param>
  /// <param name="relatedObjectIDs"></param>
  /// <param name="startDate"></param>
  /// <param name="isVersion"></param>
  /// <returns></returns>
  public virtual long CreateObjectDialog(
    int objectTypeId,
    long templateObjectId,
    int[] relationTypeIDs,
    long[] relatedObjectIDs,
    DateTime startDate,
    bool isVersion)
  {
    return 0;
  }

  /// <summary>
  /// Вызывать собственный диалог ?
  /// Если здесь вернуть true, то вызовется диалог создания объектов реализованный в функции CreateObjectDialog подписчика
  /// на конкретный тип объектов, если же вернуть false, то вызовется стандартный диалог создания объекта
  /// с изменениями, реализованными подписчиком (см. функции интерфейса)
  /// </summary>
  /// <param name="objectTypeId">Идентификатор типа создаваемого объекта</param>
  /// <param name="templateObjectId">Идентификатор объекта-прототипа</param>
  /// <param name="relationTypeIDs">массив идентификаторов связей которые необходимо создавать</param>
  /// <param name="relatedObjectIDs">массив идентификаторов объектов с которыми надо связать созданный объект</param>
  /// <param name="startDate">время с которого начинают действовать связи (если они были созданы)</param>
  /// <param name="isVersion">признак, нужно ли создавать версию объекта</param>
  /// <returns></returns>
  public virtual bool AcceptDialog(
    int objectTypeId,
    long templateObjectId,
    int[] relationTypeIDs,
    long[] relatedObjectIDs,
    DateTime startDate,
    bool isVersion)
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = TechCardProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = TechCardProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(TechCardProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("TechCard.Client_252"), (object) num));
    this._creatorArgs = new TechObjectCreatorArgs(objectTypeId, templateObjectId, relationTypeIDs, relatedObjectIDs, startDate, isVersion);
    return false;
  }

  /// <summary>
  /// Метод вызывается сразу-же после создания новой заготовки ДО отображения диалога создания
  /// </summary>
  /// <param name="newObjectId"></param>
  /// <returns></returns>
  public abstract bool AfterCreate(long newObjectId);

  /// <summary>
  /// 
  /// </summary>
  public virtual IDictionary<ObjectCreatePages, bool> VisiblePages
  {
    get
    {
      return (IDictionary<ObjectCreatePages, bool>) new Dictionary<ObjectCreatePages, bool>()
      {
        {
          ObjectCreatePages.FileAttributes,
          true
        },
        {
          ObjectCreatePages.Properties,
          true
        },
        {
          ObjectCreatePages.Classifier,
          true
        },
        {
          ObjectCreatePages.Template,
          true
        }
      };
    }
  }

  /// <summary>Метод вызывается по нажатию на кнопку готово</summary>
  /// <param name="session"></param>
  /// <param name="newObjectId"></param>
  /// <param name="nea"></param>
  /// <returns></returns>
  public virtual bool OnCommitAction(
    IUserSession session,
    long newObjectId,
    List<NotificationEventArgs> nea)
  {
    return true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  /// <param name="newObject"></param>
  /// <returns></returns>
  public virtual bool OnBeforeCommitAction(IUserSession session, IDBObject newObject) => true;

  /// <summary>Метод вызывается по нажатию на кнопку отмена</summary>
  /// <param name="session"></param>
  /// <param name="newObjectId"></param>
  /// <param name="nea"></param>
  /// <returns></returns>
  public virtual bool OnCancelAction(
    IUserSession session,
    long newObjectId,
    List<NotificationEventArgs> nea)
  {
    return true;
  }

  /// <summary>
  /// Добавление в мастер своих страниц (унаследованных от ObjectCreatorControl)
  /// </summary>
  /// <param name="createdObject"></param>
  /// <param name="propPageIndex"></param>
  /// <returns></returns>
  public virtual Dictionary<UserControl, int> AddPages(object createdObject, int propPageIndex)
  {
    return (Dictionary<UserControl, int>) null;
  }
}
