// Decompiled with JetBrains decompiler
// Type: Intermech.Pdm.Server.DBArticleCollection
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Pdm;
using Intermech.Kernel;
using Intermech.Search.MSOfficeAddins;
using System;

#nullable disable
namespace Intermech.Pdm.Server;

internal sealed class DBArticleCollection(UserSession uSession, int objectType) : DBObjectCollection(uSession, objectType)
{
  protected override string CheckPrototypeCaption(
    IDBObject prototype,
    IDBObjectType objType,
    string new_caption)
  {
    if (objType.CaptionAttribute > 0)
    {
      IDBAttribute byId = prototype.Attributes.FindByID(objType.CaptionAttribute);
      if (byId != null && (byId.AttributeType.Options & AttributeOptions.DontCopyPrototypeAttributeValueForArticle) == AttributeOptions.DontCopyPrototypeAttributeValueForArticle)
        new_caption = string.Empty;
    }
    return new_caption;
  }

  protected override IDBObject CreateObject(
    long id,
    int objectType,
    IDBObject prototype,
    Guid versionGuid)
  {
    IDBObject dbObject = base.CreateObject(id, objectType, prototype, versionGuid);
    CreateGroupInstanceType groupInstanceType = GroupInstanceHelper.ProcessingEnable((IUserSession) this.UserSession, dbObject);
    if (this.UserSession.GetCustomService(typeof (IGroupInstanceService)) is IGroupInstanceService customService && groupInstanceType == CreateGroupInstanceType.ArticleVersion)
      customService.ArticleVersionCreated((IUserSession) this.UserSession, dbObject, prototype);
    else if (groupInstanceType == CreateGroupInstanceType.None)
      this.RefreshNewObjectAttributeValuesFromPrototype((IDBAttributable) prototype, (IDBAttributable) dbObject);
    return dbObject;
  }

  private void RefreshNewObjectAttributeValuesFromPrototype(
    IDBAttributable prototype,
    IDBAttributable newObject)
  {
    if (prototype == null || newObject == null)
      return;
    IDBAttributeCollection attributes1 = prototype.Attributes;
    DBAttributeCollection attributes2 = newObject.Attributes as DBAttributeCollection;
    (newObject as DBAttributable).SetAttributesState(Consts.AssignValuesMode, attributes1);
    attributes2._AssignMode = Consts.CreateMode;
    try
    {
      this.UserSession.StartTransaction();
      try
      {
        for (int AttrIndex = 0; AttrIndex < attributes1.Count; ++AttrIndex)
        {
          IDBAttribute sourceAttribute = attributes1[AttrIndex];
          if (sourceAttribute.AttributeID >= 0 && (sourceAttribute.AttributeType.Options & AttributeOptions.DontCopyPrototypeValue) == AttributeOptions.DontCopyPrototypeValue && (sourceAttribute.AttributeType.Options & AttributeOptions.DontCopyPrototypeAttributeValueForArticle) == AttributeOptions.None)
          {
            if (DBAttributeType.CanSkipInit(sourceAttribute.AttributeType.AttributeType))
            {
              if (sourceAttribute.TemporaryAttribute)
                attributes2.AddTemporaryAttribute(sourceAttribute.AttributeID, false, sourceAttribute.Values);
              else if (newObject.GetAttributeType(sourceAttribute.AttributeID).Computed == ComputeValueModes.NotComputableValue)
                attributes2.AddAttribute(sourceAttribute.AttributeID, false, attributes2.ValidatingOn, sourceAttribute.Values);
              else
                attributes2.AddAttribute(sourceAttribute.AttributeID, false, attributes2.ValidatingOn);
            }
            else
            {
              DBAttribute dbAttribute = !sourceAttribute.TemporaryAttribute ? attributes2.AddAttribute(sourceAttribute.AttributeID, false, attributes2.ValidatingOn) as DBAttribute : attributes2.AddTemporaryAttribute(sourceAttribute.AttributeID, false) as DBAttribute;
              if (dbAttribute.AttributeType.Computed == ComputeValueModes.NotComputableValue)
              {
                dbAttribute.ValidatingOn = attributes2.ValidatingOn;
                dbAttribute.Assign(sourceAttribute);
              }
            }
          }
        }
        for (int AttrIndex = attributes2.Count - 1; AttrIndex >= 0; --AttrIndex)
        {
          if (attributes2[AttrIndex].AttributeID >= 0 && attributes2[AttrIndex].AttributeID != AvsIDCache.Attr_ObjectPrototype)
          {
            IDBAttribute byId = attributes1.FindByID(attributes2[AttrIndex].AttributeID);
            if ((byId == null ? 1 : ((byId.AttributeType.Options & AttributeOptions.DontCopyPrototypeAttributeValueForArticle) == AttributeOptions.DontCopyPrototypeAttributeValueForArticle ? 1 : 0)) != 0)
            {
              (attributes2[AttrIndex] as DBAttribute).ValidatingOn = attributes2.ValidatingOn;
              attributes2[AttrIndex].Delete((long) Consts.PurgeMode);
            }
          }
        }
        (newObject as DBAttributable).CommitComputedValues();
        this.UserSession.Commit();
      }
      catch
      {
        this.UserSession.Rollback();
        throw;
      }
    }
    finally
    {
      attributes2._AssignMode = 0;
      (newObject as DBAttributable).ClearAttributesState(Consts.AssignValuesMode);
    }
  }

  protected override void CheckRelationAttributes(IDBRelation newrel, IDBRelation oldrel)
  {
    this.RefreshNewObjectAttributeValuesFromPrototype((IDBAttributable) oldrel, (IDBAttributable) newrel);
  }

  protected override IDBRelation CreateObject_CopyVersionRelations(
    IDBObject newObject,
    IDBObject prototype,
    DBRelationCollection rels,
    NewRelationProperties props)
  {
    int? relationTypeId = rels?.RelationTypeID;
    int referenceRelationTypeId = MSOfficeAddinsConstants.ObjectsAddedByReferenceRelationTypeID;
    if (relationTypeId.GetValueOrDefault() == referenceRelationTypeId & relationTypeId.HasValue)
    {
      if (newObject == null || prototype == null)
        return (IDBRelation) null;
      if (newObject.ID == prototype.ID)
        return (IDBRelation) null;
    }
    return base.CreateObject_CopyVersionRelations(newObject, prototype, rels, props);
  }
}
