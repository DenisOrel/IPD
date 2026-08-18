// Decompiled with JetBrains decompiler
// Type: Intermech.Techcard.Client.FormDesigner.External.CAD.Classes.ExternalCADActionHandler
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using ImSSP;
using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.Expert;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.TechCard;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.PropertyEditors;
using Intermech.TechCard.Client.FormDesigner.CAD.Classes;
using Intermech.Tools.Integrators.CADInterface;
using Interop.Cadmech;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Techcard.Client.FormDesigner.External.CAD.Classes;

/// <summary>
/// Implementation of IFormDesignerActionHandler for CAD action
/// </summary>
internal class ExternalCADActionHandler : IFormDesignerActionHandler
{
  /// <summary>
  /// All child object types for type "Электронные модели деталей"
  /// </summary>
  private List<int> _modelObjectTypes;

  /// <summary>Initialize object data</summary>
  private void InitData() => this._modelObjectTypes = this.GetModelChildTypes();

  /// <summary>Getting child types for models</summary>
  /// <returns></returns>
  private List<int> GetModelChildTypes()
  {
    List<int> modelChildTypes = new List<int>();
    IMSObjectType objectType = MetaDataHelper.GetObjectType(ExternalCADConsts.ExternalCADModelTypeGuid);
    if (objectType == null)
      return modelChildTypes;
    List<int> objectTypeChildrenId = MetaDataHelper.GetObjectTypeChildrenID(objectType.ObjectTypeID);
    if (!objectTypeChildrenId.Contains(objectType.ObjectTypeID))
      objectTypeChildrenId.Add(objectType.ObjectTypeID);
    objectTypeChildrenId.Sort();
    return objectTypeChildrenId;
  }

  /// <summary>Get article object id for object</summary>
  /// <param name="dbAttrObject"></param>
  /// <returns></returns>
  private long GetArticleObjectId(IDBAttributable dbAttrObject)
  {
    if (dbAttrObject == null)
      return 0;
    List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive((IEnumerable<int>) TechCardConsts.ObjectTypes.ArticleObjectTypes);
    return dbAttrObject is IDBObject dbObject ? (childrenIdRecursive.Contains(dbObject.ObjectType) ? dbObject.ObjectID : this.GetArticleObjectID(new ObjInfoItem(dbObject.ObjectID, dbObject.ObjectType))) : (dbAttrObject is IDBRelation dbRelation ? this.GetArticleObjectID(new ObjInfoItem(dbRelation.PartObjectID)) : 0L);
  }

  /// <summary>Get article object id for object</summary>
  /// <param name="objItem"></param>
  /// <returns></returns>
  private long GetArticleObjectID(ObjInfoItem objItem)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (ObjInfoItem objInfoItem in TechCardObjUtils.Article.GetArticles4Object(objItem, sessionKeeper.Session))
      {
        if (!((TypedInfoItem) objInfoItem == (TypedInfoItem) null) && objInfoItem.ObjectID != 0L)
          return objInfoItem.ObjectID;
      }
    }
    return 0;
  }

  /// <summary>Get model object id for article</summary>
  /// <param name="objectId"></param>
  /// <returns></returns>
  private long GetModelForArticle(long objectId)
  {
    if (objectId == 0L)
      return 0;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      Guid relationTypeGUID = new Guid("cad00154-306c-11d8-b4e9-00304f19f545");
      IDBRelationType relationType = sessionKeeper.Session.GetRelationType(relationTypeGUID, false);
      if (relationType == null)
        return 0;
      ConditionStructure[] conditions = new ConditionStructure[1]
      {
        new ConditionStructure(-7, RelationalOperators.In, (object) this._modelObjectTypes.ToArray(), (object) null, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Auto, ColumnContents.Text)
      };
      DataTable childSostavData = DataHelper.GetChildSostavData(new ObjInfoItem(objectId), sessionKeeper.Session, (IEnumerable<int>) new int[1]
      {
        relationType.RelationType
      }, false, (IEnumerable<ConditionStructure>) conditions);
      long modelForArticle = 0;
      if (childSostavData == null || childSostavData.Rows.Count == 0)
        return modelForArticle;
      int columnIndex1 = childSostavData.Columns.IndexOf("F_OBJECT_ID");
      int columnIndex2 = childSostavData.Columns.IndexOf("F_OBJECT_TYPE");
      foreach (DataRow row in (InternalDataCollectionBase) childSostavData.Rows)
      {
        if (this._modelObjectTypes.BinarySearch(Convert.ToInt32(row[columnIndex2])) >= 0)
        {
          long int64 = Convert.ToInt64(row[columnIndex1]);
          switch (int64)
          {
            case -1:
            case 0:
              continue;
            default:
              modelForArticle = int64;
              goto label_16;
          }
        }
      }
label_16:
      return modelForArticle;
    }
  }

  /// <summary>Getting model ID by objectID</summary>
  /// <param name="dbAttrObject"></param>
  /// <returns></returns>
  private long GetModelObjectId(IDBAttributable dbAttrObject)
  {
    long articleObjectId = this.GetArticleObjectId(dbAttrObject);
    return articleObjectId == 0L ? 0L : this.GetModelForArticle(articleObjectId);
  }

  /// <summary>Check button's state</summary>
  /// <param name="button"></param>
  /// <param name="form"></param>
  /// <returns></returns>
  public bool ButtonEnabled(object button, object form)
  {
    if (ServiceUtils.GetService<ICadmech3DServices>((object) ApplicationServices.Container, false) == null)
      return false;
    DesForm desForm = form as DesForm;
    AttrButton attrButton = button as AttrButton;
    if (desForm == null || attrButton == null || !(attrButton.FormDesignerActionParams is ExternalCADActionParams designerActionParams))
      return false;
    if (attrButton.Tag is bool && Convert.ToBoolean(attrButton.Tag).Equals(true))
      return attrButton.Enabled;
    if (designerActionParams.Method == ExternalCADMethod.undefined)
      return false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttributable dbAttrObject = (IDBAttributable) null;
      IElementInfo info = desForm.Info;
      switch (info.ElementKind)
      {
        case AttributableElements.Object:
          dbAttrObject = (IDBAttributable) sessionKeeper.Session.GetObject(info.ElementIdentifier);
          break;
        case AttributableElements.Relation:
          dbAttrObject = (IDBAttributable) sessionKeeper.Session.GetRelation(info.ElementIdentifier);
          break;
      }
      return this.GetModelObjectId(dbAttrObject) != 0L;
    }
  }

  /// <summary>Implementation of button's press events</summary>
  /// <param name="button"></param>
  /// <param name="form"></param>
  public void ButtonPressed(object button, object form)
  {
    ICadmech3DServices service = ServiceUtils.GetService<ICadmech3DServices>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    DesForm desForm = form as DesForm;
    AttrButton attrButton = button as AttrButton;
    if (desForm == null || attrButton == null)
      return;
    ExternalCADActionParams exaParams = attrButton.FormDesignerActionParams as ExternalCADActionParams;
    IElementInfo info = desForm.Info;
    long modelObjectId;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttributable dbAttrObject = (IDBAttributable) null;
      switch (info.ElementKind)
      {
        case AttributableElements.Object:
          dbAttrObject = (IDBAttributable) sessionKeeper.Session.GetObject(info.ElementIdentifier);
          break;
        case AttributableElements.Relation:
          dbAttrObject = (IDBAttributable) sessionKeeper.Session.GetRelation(info.ElementIdentifier);
          break;
      }
      modelObjectId = this.GetModelObjectId(dbAttrObject);
      if (modelObjectId == 0L)
      {
        string str = LocalizationHolder.rm.GetString(sc_19315.ssp_techcard_19316());
        switch (info.ElementKind)
        {
          case AttributableElements.Object:
            str = string.Format(str, (object) ((IDBObject) dbAttrObject).ObjectID);
            break;
          case AttributableElements.Relation:
            str = string.Format(str, (object) ((IDBRelation) dbAttrObject).PartID);
            break;
        }
        int num = (int) MessageBox.Show(str, LocalizationHolder.rm.GetString(sc_19315.ssp_techcard_19317()), MessageBoxButtons.OK, MessageBoxIcon.Hand);
        return;
      }
    }
    string data;
    try
    {
      data = service.UseAttInterface<string>(modelObjectId, (System.Func<IAttInterface, string>) (attInterface =>
      {
        if (exaParams == null)
          return string.Empty;
        switch (exaParams.Method)
        {
          case ExternalCADMethod.CommonAttributes:
            return attInterface.GetCommonAttributes();
          case ExternalCADMethod.FaceAttributes:
            return attInterface.GetFaceAttributes();
          default:
            return string.Empty;
        }
      }));
      if (string.IsNullOrEmpty(data))
        return;
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
      return;
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      Dictionary<int, object> computedValues = ExternalCADAttrParser.Parse(info, data);
      if (computedValues == null || computedValues.Count == 0)
        return;
      long elementIdentifier = info.ElementIdentifier;
      List<AttributeValues> valuesFromControls = desForm.GetAttributeValuesFromControls(elementIdentifier);
      List<AttributeValues> additionalValues = desForm.GetAdditionalValues(elementIdentifier);
      valuesFromControls.AddRange((IEnumerable<AttributeValues>) additionalValues);
      List<AttributeValues> newObjectValues = (List<AttributeValues>) null;
      List<AttributeValues> newRelationValues = (List<AttributeValues>) null;
      if (info.ElementKind == AttributableElements.Object)
        newObjectValues = this.CreateAttributeValueList(sessionKeeper.Session, valuesFromControls, computedValues);
      else if (info.ElementKind == AttributableElements.Relation)
        newRelationValues = this.CreateAttributeValueList(sessionKeeper.Session, valuesFromControls, computedValues);
      desForm.AttributeChanging((IEnumerable<AttributeValues>) newObjectValues, (IEnumerable<AttributeValues>) newRelationValues);
    }
  }

  /// <summary>Constructor</summary>
  public ExternalCADActionHandler() => this.InitData();

  /// <summary>Сформировать список измененных атрибутов.</summary>
  /// <param name="session">Сессия пользователя</param>
  /// <param name="attributeValues">Список атрибутов объекта/связи</param>
  /// <param name="computedValues">Словарь измененных значений для указанного списка атрибутов</param>
  /// <returns>Список измененных атрибутов</returns>
  private List<AttributeValues> CreateAttributeValueList(
    IUserSession session,
    List<AttributeValues> attributeValues,
    Dictionary<int, object> computedValues)
  {
    List<AttributeValues> attributeValueList = (List<AttributeValues>) null;
    if (attributeValues.Count > 0)
    {
      string str = LocalizationHolder.rm.GetString("TechCard.Client_539");
      attributeValueList = new List<AttributeValues>(attributeValues.Count);
      foreach (KeyValuePair<int, object> computedValue in computedValues)
      {
        int attrId = computedValue.Key;
        object obj = computedValue.Value;
        AttributeValues attributeValues1 = attributeValues.FirstOrDefault<AttributeValues>((System.Func<AttributeValues, bool>) (x => x.AttributeID == attrId));
        if (attributeValues1 == null)
        {
          IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attrId);
          attributeValues1 = new AttributeValues(attrId)
          {
            AttributeType = attributeType.FieldType
          };
        }
        object[] objArray1 = new object[1]{ obj };
        object[] objArray2;
        if (attributeValues1.AttributeType == FieldTypes.ftObjectLink || attributeValues1.AttributeType == FieldTypes.ftObjectLinkByID)
        {
          long num = obj is long ? Convert.ToInt64(obj) : 0L;
          if (attributeValues1.AttributeType == FieldTypes.ftObjectLinkByID && num != 0L)
            num = session.GetObjectBaseVersionByID(num, true).ObjectID;
          QuickObjectInfo objectInfo = session.GetObjectInfo(num);
          objArray2 = new object[1]
          {
            objectInfo.Empty || string.IsNullOrEmpty(objectInfo.Caption) ? (object) $"{str} №{num.ToString()}" : (object) objectInfo.Caption
          };
        }
        else
          objArray2 = objArray1;
        attributeValues1.Descriptions = objArray2;
        attributeValues1.Values = objArray1;
        attributeValueList.Add(attributeValues1);
      }
    }
    return attributeValueList;
  }
}
