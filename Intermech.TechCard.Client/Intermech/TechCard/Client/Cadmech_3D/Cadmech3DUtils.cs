// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Cadmech_3D.Cadmech3DUtils
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.CADInterface.Proxies.Cadmech;
using Intermech.Imbase;
using Intermech.Imbase.Common;
using Intermech.Interfaces;
using Intermech.Interfaces.AutoSelection;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.TechCard;
using Intermech.Interfaces.TechCard.TechRelation;
using Intermech.TechCard.Client.Common;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.TechCard.Client.Cadmech_3D;

/// <summary>
/// 
/// </summary>
internal class Cadmech3DUtils
{
  /// <summary>
  /// 
  /// </summary>
  private readonly Dictionary<int, List<long>> _objType2ImbaseCatalogsCache = new Dictionary<int, List<long>>();

  /// <summary>
  /// 
  /// </summary>
  private void InitializeData()
  {
  }

  /// <summary>
  /// Получение списка каталогов, по которым можно создавать объекты указанного типа
  /// </summary>
  /// <param name="session"></param>
  /// <param name="objTypeId"></param>
  /// <returns></returns>
  private List<long> GetImbaseCatalogs4ObjectType(IUserSession session, int objTypeId)
  {
    List<long> catalogs4ObjectType;
    if (this._objType2ImbaseCatalogsCache.TryGetValue(objTypeId, out catalogs4ObjectType))
      return catalogs4ObjectType;
    List<long> catalogIdForObjType = ImbaseUtils.GetCatalogIDForObjType(new int[1]
    {
      objTypeId
    }, session);
    this._objType2ImbaseCatalogsCache[objTypeId] = catalogIdForObjType;
    return catalogIdForObjType;
  }

  /// <summary>Создание объекта по каталогу</summary>
  /// <param name="objTypeId">Тип создаваемого объекта</param>
  /// <param name="attrCondList">Условия поиска папки / записи каталога</param>
  /// <param name="showDialogIfNotFound"></param>
  /// <returns></returns>
  private long CreateObjectByImbase(
    int objTypeId,
    List<Tuple<int, object>> attrCondList,
    bool showDialogIfNotFound)
  {
    long objectByImbase = 0;
    if (objTypeId == -1)
      return objectByImbase;
    long baseId = 0;
    List<long> catalogs4ObjectType;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      catalogs4ObjectType = this.GetImbaseCatalogs4ObjectType(sessionKeeper.Session, objTypeId);
      if (catalogs4ObjectType.Count == 0)
        return objectByImbase;
      foreach (long catalogID in catalogs4ObjectType)
      {
        List<long> longList = ImbaseHelper.SearchImFolderData(sessionKeeper.Session, catalogID, attrCondList);
        if (longList != null && longList.Count != 0)
        {
          baseId = longList[0];
          break;
        }
      }
    }
    if (baseId == 0L & showDialogIfNotFound)
    {
      IImbaseSelector service = ServiceUtils.GetService<IImbaseSelector>((object) ApplicationServices.Container, false);
      if (service != null)
        baseId = service.SelectFromCatalog(Intermech.Localization.LocalizationHolder.rm.GetString("TechCard.Client_194"), "", (object) catalogs4ObjectType[0], true, false, (int[]) null, objTypeId);
    }
    if (baseId == 0L)
      return objectByImbase;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return ServiceUtils.GetService<IImbaseServer>((object) sessionKeeper.Session, true).CreateObject(sessionKeeper.Session.SessionGUID, 0L, baseId, 0L, false, objTypeId);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="imСadFaceAttr"></param>
  /// <returns></returns>
  private ObjInfoItem CreateObject_FaceAttr(IMTextFaceAttributeProxy imСadFaceAttr)
  {
    if (imСadFaceAttr == null)
      throw new ArgumentNullException(nameof (imСadFaceAttr));
    if (imСadFaceAttr.Properties != null && Array.IndexOf<string>(imСadFaceAttr.Properties, "FCN_TEMPLATE") != -1)
      return (ObjInfoItem) null;
    long objectByImbase = this.CreateObjectByImbase(TechCardConsts.ObjectTypes.SurfaceParamID, new List<Tuple<int, object>>(1)
    {
      new Tuple<int, object>(MetaDataHelper.GetAttributeID((object) TechCardConsts.AttributeTypes.CadmechAttrTypeAttrGuid), (object) (int) imСadFaceAttr.AttrType)
    }, true);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectByImbase, false) ?? sessionKeeper.Session.GetObjectCollection(TechCardConsts.ObjectTypes.SurfaceParamID).Create();
      List<AttributeValues> attributeValuesList = new List<AttributeValues>();
      if (imСadFaceAttr.Properties != null)
      {
        foreach (string property in imСadFaceAttr.Properties)
        {
          IIMCadAttrTypeParamSettings typeParamSettings = (IIMCadAttrTypeParamSettings) null;
          if (typeParamSettings != null && !(typeParamSettings.IpsAttrType == Guid.Empty))
          {
            int attributeId = MetaDataHelper.GetAttributeID((object) typeParamSettings.IpsAttrType);
            if (attributeId != 0)
              attributeValuesList.Add(new AttributeValues(attributeId, imСadFaceAttr.GetProperty(property)));
          }
        }
      }
      if (attributeValuesList.Count != 0)
        dbObject.SetAttributesValues(attributeValuesList.ToArray());
      return new ObjInfoItem(dbObject);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="imСadFace"></param>
  /// <returns></returns>
  private ObjInfoItem CreateObject_Face(IMTextFaceProxy imСadFace)
  {
    if (imСadFace == null)
      throw new ArgumentNullException(nameof (imСadFace));
    ObjInfoItem objInfoItem;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObjectCollection(TechCardConsts.ObjectTypes.SurfaceSlaveID).Create();
      dbObject.Caption = imСadFace.Description;
      objInfoItem = new ObjInfoItem(dbObject);
    }
    IMTextFaceAttributeProxy[] refAttrs = imСadFace.GetRefAttrs();
    if (refAttrs != null)
      this.AddObjects_FaceAttrs(refAttrs, objInfoItem);
    return objInfoItem;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="imFaceAttr"></param>
  private ObjInfoItem CreateObject_TemplateFaceAttr(IMTextFaceAttributeProxy imFaceAttr)
  {
    if (imFaceAttr == null)
      return (ObjInfoItem) null;
    ObjInfoItem objInfoItem;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      string str = Convert.ToString(imFaceAttr.GetProperty("FCN_TEMPLATE"));
      IDBObject dbObject = sessionKeeper.Session.GetObjectCollection(TechCardConsts.ObjectTypes.SurfaceMasterID).Create();
      dbObject.Caption = str;
      objInfoItem = new ObjInfoItem(dbObject);
    }
    IMTextFaceProxy[] faces = imFaceAttr.Faces;
    if (faces != null)
      this.AddObjects_Faces(faces, objInfoItem);
    return objInfoItem;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  /// <param name="createdObjInfoList"></param>
  private void CommitObjects(IUserSession session, List<ObjInfoItem> createdObjInfoList)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    if (createdObjInfoList == null)
      throw new ArgumentNullException(nameof (createdObjInfoList));
    if (createdObjInfoList.Count == 0)
      return;
    foreach (ObjInfoItem createdObjInfo in createdObjInfoList)
    {
      IDBObject dbObject = session.GetObject(createdObjInfo.ObjectID, false);
      if (dbObject != null && dbObject.IsCreationMode)
      {
        dbObject.CommitCreation(true);
        createdObjInfo.ObjectID = dbObject.ObjectID;
      }
    }
  }

  /// <summary>Создание связей с объектоами</summary>
  /// <param name="projObjInfo"></param>
  /// <param name="createdObjInfoList"></param>
  private List<RelObjInfoItem> CreateObjectLinks(
    ObjInfoItem projObjInfo,
    List<ObjInfoItem> createdObjInfoList)
  {
    List<RelObjInfoItem> objectLinks = new List<RelObjInfoItem>();
    if (ObjInfoItem.IsEmpty((ITypedInfoItem) projObjInfo) || createdObjInfoList == null || createdObjInfoList.Count == 0)
      return objectLinks;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      TechcardClientUtils.StartCreateRelations(projObjInfo, sessionKeeper.Session);
      try
      {
        foreach (ObjInfoItem createdObjInfo in createdObjInfoList)
        {
          List<IDBRelation> relations = TechcardClientUtils.CreateRelations(sessionKeeper.Session, createdObjInfo.ObjectID, new int[1]
          {
            TechCardConsts.RelTypes.TechRelationID
          }, new long[1]{ projObjInfo.ObjectID }, DateTime.Now, TechCreateRelMode.tcrmEnterIn);
          if (relations != null)
          {
            foreach (IDBRelation dbRel in relations)
              objectLinks.Add(new RelObjInfoItem(new RelInfoItem(dbRel), projObjInfo, createdObjInfo));
            IAutoSelectionService service = ServiceUtils.GetService<IAutoSelectionService>((object) ApplicationServices.Container, false);
            if (service != null)
            {
              long projectRelationId = 0;
              if (relations.Count != 0)
                projectRelationId = relations[0].RelationID;
              service.ExecuteSelection(new AutoSelectionParams(createdObjInfo.ObjectID, projectRelationId, AutoSelectionMode.AutoObject));
            }
          }
        }
      }
      finally
      {
        TechcardClientUtils.StopCreateRelations(sessionKeeper.Session);
      }
    }
    return objectLinks;
  }

  /// <summary>Добавление типовых элементов</summary>
  /// <param name="imСadFaceAttrs"></param>
  /// <param name="objInfoItem"></param>
  private List<RelObjInfoItem> AddObjects_TemplateFaceAttr(
    IMTextFaceAttributeProxy[] imСadFaceAttrs,
    ObjInfoItem objInfoItem)
  {
    List<RelObjInfoItem> relObjInfoItemList = new List<RelObjInfoItem>();
    if (imСadFaceAttrs == null)
      throw new ArgumentNullException(nameof (imСadFaceAttrs));
    if ((TypedInfoItem) objInfoItem == (TypedInfoItem) null)
      throw new ArgumentNullException(nameof (objInfoItem));
    if (imСadFaceAttrs.Length == 0 || ObjInfoItem.IsEmpty((ITypedInfoItem) objInfoItem))
      return relObjInfoItemList;
    List<ObjInfoItem> createdObjInfoList = new List<ObjInfoItem>();
    foreach (IMTextFaceAttributeProxy imСadFaceAttr in imСadFaceAttrs)
    {
      ObjInfoItem templateFaceAttr = this.CreateObject_TemplateFaceAttr(imСadFaceAttr);
      if (!ObjInfoItem.IsEmpty((ITypedInfoItem) templateFaceAttr))
        createdObjInfoList.Add(templateFaceAttr);
    }
    relObjInfoItemList.AddRange((IEnumerable<RelObjInfoItem>) this.CreateObjectLinks(objInfoItem, createdObjInfoList));
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.CommitObjects(sessionKeeper.Session, createdObjInfoList);
    return relObjInfoItemList;
  }

  /// <summary>Добавление поверхностей к указанному объекту</summary>
  /// <param name="imСadFaces"></param>
  /// <param name="objInfoItem"></param>
  private List<RelObjInfoItem> AddObjects_Faces(
    IMTextFaceProxy[] imСadFaces,
    ObjInfoItem objInfoItem)
  {
    List<RelObjInfoItem> relObjInfoItemList = new List<RelObjInfoItem>();
    if (imСadFaces == null)
      throw new ArgumentNullException(nameof (imСadFaces));
    if ((TypedInfoItem) objInfoItem == (TypedInfoItem) null)
      throw new ArgumentNullException(nameof (objInfoItem));
    if (imСadFaces.Length == 0 || ObjInfoItem.IsEmpty((ITypedInfoItem) objInfoItem))
      return relObjInfoItemList;
    List<ObjInfoItem> createdObjInfoList = new List<ObjInfoItem>();
    foreach (IMTextFaceProxy imСadFace in imСadFaces)
    {
      ObjInfoItem objectFace = this.CreateObject_Face(imСadFace);
      if (!ObjInfoItem.IsEmpty((ITypedInfoItem) objectFace))
        createdObjInfoList.Add(objectFace);
    }
    relObjInfoItemList.AddRange((IEnumerable<RelObjInfoItem>) this.CreateObjectLinks(objInfoItem, createdObjInfoList));
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.CommitObjects(sessionKeeper.Session, createdObjInfoList);
    return relObjInfoItemList;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="imСadFaceAttrs"></param>
  /// <param name="objInfoItem"></param>
  private List<RelObjInfoItem> AddObjects_FaceAttrs(
    IMTextFaceAttributeProxy[] imСadFaceAttrs,
    ObjInfoItem objInfoItem)
  {
    List<RelObjInfoItem> relObjInfoItemList = new List<RelObjInfoItem>();
    if (imСadFaceAttrs == null)
      throw new ArgumentNullException(nameof (imСadFaceAttrs));
    if ((TypedInfoItem) objInfoItem == (TypedInfoItem) null)
      throw new ArgumentNullException(nameof (objInfoItem));
    if (imСadFaceAttrs.Length == 0 || ObjInfoItem.IsEmpty((ITypedInfoItem) objInfoItem))
      return relObjInfoItemList;
    List<ObjInfoItem> createdObjInfoList = new List<ObjInfoItem>();
    foreach (IMTextFaceAttributeProxy imСadFaceAttr in imСadFaceAttrs)
    {
      ObjInfoItem objectFaceAttr = this.CreateObject_FaceAttr(imСadFaceAttr);
      if (!ObjInfoItem.IsEmpty((ITypedInfoItem) objectFaceAttr))
        createdObjInfoList.Add(objectFaceAttr);
    }
    relObjInfoItemList.AddRange((IEnumerable<RelObjInfoItem>) this.CreateObjectLinks(objInfoItem, createdObjInfoList));
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.CommitObjects(sessionKeeper.Session, createdObjInfoList);
    return relObjInfoItemList;
  }

  /// <summary>Конструктор</summary>
  private Cadmech3DUtils(IServiceProvider provider)
  {
    if (provider == null)
      throw new ArgumentNullException(nameof (provider));
    this.InitializeData();
  }

  /// <summary>Добавление типовых элементов</summary>
  /// <param name="imСadFaceAttrs"></param>
  /// <param name="objInfoItem"></param>
  /// <param name="provider"></param>
  public static List<RelObjInfoItem> AddObjects_TemplateFaceAttr(
    IMTextFaceAttributeProxy[] imСadFaceAttrs,
    ObjInfoItem objInfoItem,
    IServiceProvider provider)
  {
    return new Cadmech3DUtils(provider).AddObjects_TemplateFaceAttr(imСadFaceAttrs, objInfoItem);
  }

  /// <summary>Добавление поверхностей к указанному объекту</summary>
  /// <param name="imСadFaces"></param>
  /// <param name="objInfoItem"></param>
  /// <param name="provider"></param>
  public static List<RelObjInfoItem> AddObjects_Faces(
    IMTextFaceProxy[] imСadFaces,
    ObjInfoItem objInfoItem,
    IServiceProvider provider)
  {
    return new Cadmech3DUtils(provider).AddObjects_Faces(imСadFaces, objInfoItem);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="imСadFaceAttrs"></param>
  /// <param name="objInfoItem"></param>
  /// <param name="provider"></param>
  public static List<RelObjInfoItem> AddObjects_FaceAttrs(
    IMTextFaceAttributeProxy[] imСadFaceAttrs,
    ObjInfoItem objInfoItem,
    IServiceProvider provider)
  {
    return new Cadmech3DUtils(provider).AddObjects_FaceAttrs(imСadFaceAttrs, objInfoItem);
  }
}
