// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.DbOperations
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.ControlFlow;
using Intermech.Data;
using Intermech.Data.SectionEntities;
using Intermech.Interfaces;
using Intermech.Interfaces.Data;
using Intermech.Interfaces.Data.Actions;
using Intermech.Localization;
using Intermech.Tools.Data;
using Intermech.Tools.DataExchange;
using Intermech.UI;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators;

public sealed class DbOperations
{
  private static readonly string integrationErrorsEmptyXmlString = new DBObjectErrorsBuilder().ToXmlString();
  private CheckoutOperations checkoutOperations;

  public DbOperations(CheckoutOperations checkoutOperations)
  {
    this.checkoutOperations = checkoutOperations != null ? checkoutOperations : throw new ArgumentNullException(nameof (checkoutOperations));
  }

  public void CreateBlankObject(
    CaptureChangesDriverContext ctx,
    SectionEntity objectEntity,
    IAction action = null)
  {
    if (ctx == null)
      throw new ArgumentNullException(nameof (ctx));
    int objectType = objectEntity != null ? ObjectSection.GetObjectType(objectEntity) : throw new ArgumentNullException(nameof (objectEntity));
    DBObjectEntityRef objRef = new DBObjectEntityRef(objectEntity);
    if (action == null)
      action = (IAction) new CreateBlankObjectAction(objectType, (IUpdateableDBObjectRef) objRef);
    action = (IAction) new UIReportActionDecorator(action);
    action.Perform();
    ctx.ServerCleanupActions.Add((IAction) new UIReportActionDecorator((IAction) new DeleteObjectAction((IDBObjectRef) objRef, false), LocalizationHolder.rm.GetString("Tools.Components_508")));
  }

  public static ValueRecord FindIdentityAttribute(
    SectionEntity objectItem,
    IEnumerable<StringKey> identityAttrs,
    bool allowEmptyValue)
  {
    if (objectItem == null)
      throw new ArgumentNullException(nameof (objectItem));
    if (identityAttrs == null)
      throw new ArgumentNullException(nameof (identityAttrs));
    return DbOperations.FindIdentityAttribute(objectItem.Sections.Get<AttributesSection>().WorkingSet, identityAttrs, allowEmptyValue);
  }

  public static ValueRecord FindIdentityAttribute(
    ValueBag attributeSet,
    IEnumerable<StringKey> identityAttrs,
    bool allowEmptyValue)
  {
    if (attributeSet == null)
      throw new ArgumentNullException(nameof (attributeSet));
    if (identityAttrs == null)
      throw new ArgumentNullException(nameof (identityAttrs));
    foreach (StringKey identityAttr in identityAttrs)
    {
      ValueRecord identityAttribute = attributeSet.Find(identityAttr);
      if (identityAttribute != null && identityAttribute.DataType == typeof (string))
      {
        string str = identityAttribute.Read<string>(string.Empty);
        if (allowEmptyValue || !string.IsNullOrEmpty(str))
          return identityAttribute;
      }
    }
    return (ValueRecord) null;
  }

  public static string ReadObjectTypeName(SectionEntity workItem, string attrName)
  {
    if (workItem == null)
      throw new ArgumentNullException();
    if (string.IsNullOrEmpty(attrName))
      throw new ArgumentException();
    ValueRecord valueRecord = workItem.Sections.Get<AttributesSection>().WorkingSet.Find((StringKey) attrName);
    return (valueRecord == null ? 0 : (valueRecord.DataType == typeof (string) ? 1 : 0)) == 0 ? string.Empty : valueRecord.Read<string>(string.Empty);
  }

  public static int ReadObjectTypeAttribute(SectionEntity workItem, string attrName)
  {
    string anObjectTypeName = DbOperations.ReadObjectTypeName(workItem, attrName);
    if (string.IsNullOrEmpty(anObjectTypeName))
      return -1;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObjectType objectType = sessionKeeper.Session.GetObjectType(anObjectTypeName, false);
      return objectType != null ? objectType.ObjectType : -1;
    }
  }

  /// <summary>
  /// Возвращает значения атрибутов по умолчанию для указанной сущности. Этот метод используется для получения атрибутов объекта/связи, которые еще не
  /// созданы в базе PDM-системы, но будут там созданы.
  /// </summary>
  /// <param name="attributableType">Описатель для атрибутов сущности</param>
  /// <returns>Контейнер со значениями атрибутов</returns>
  /// <exception cref="T:System.ArgumentNullException">Аргумент метода не может быть null</exception>
  public ValueBag ReadBlankAttributes(IDBAttributableTypeRef attributableType)
  {
    return DbOperationsHelper.ReadBlankAttributes(attributableType);
  }

  /// <summary>
  /// Читает значения указанного объекта из базы данных PDM-системы.
  /// </summary>
  /// <param name="objRef">Ссылка на идентификатор объекта</param>
  /// <param name="attributableType">Описатель для атрибутов объекта</param>
  /// <returns>Контейнер со значениями атрибутов</returns>
  /// <exception cref="T:System.ArgumentNullException">Аргумент метода не может быть null</exception>
  public ValueBag ReadObjectAttributes(IDBObjectRef objRef, IDBAttributableTypeRef attributableType)
  {
    return DbOperationsHelper.ReadObjectAttributes(objRef, attributableType);
  }

  /// <summary>
  /// Читает значения атрибутов указанного объекта из базы данных PDM-системы.
  /// </summary>
  /// <param name="objectItem">Объект PDM-системы</param>
  /// <param name="attributableType">Описатель для атрибутов объекта PDM-системы</param>
  /// <returns>Контейнер со значениями атрибутов</returns>
  /// <exception cref="T:System.ArgumentNullException">Аргумент метода не может быть null</exception>
  public ValueBag ReadObjectAttributes(
    SectionEntity objectItem,
    IDBAttributableTypeRef attributableType)
  {
    if (objectItem == null)
      throw new ArgumentNullException(nameof (objectItem));
    if (attributableType == null)
      throw new ArgumentNullException(nameof (attributableType));
    ObjectSection objectSection = objectItem.Sections.Get<ObjectSection>();
    return objectSection.ObjectId == 0L ? this.ReadBlankAttributes(attributableType) : this.ReadObjectAttributes((IDBObjectRef) new DirectDBObjectRef(objectSection.ObjectId), attributableType);
  }

  /// <summary>
  /// Читает значения атрибутов указанного объекта из базы данных PDM-системы и сохраняет их в секции AttributesSection.
  /// </summary>
  /// <param name="objectItem">Объект PDM-системы</param>
  /// <param name="attributableType">Описатель для атрибутов объекта PDM-системы</param>
  /// <exception cref="T:System.ArgumentNullException">Аргумент метода не может быть null</exception>
  public void FetchObjectAttributes(
    SectionEntity objectItem,
    IDBAttributableTypeRef attributableType)
  {
    if (objectItem == null)
      throw new ArgumentNullException(nameof (objectItem));
    objectItem.Sections.Get<AttributesSection>().DatabaseSet = attributableType != null ? this.ReadObjectAttributes(objectItem, attributableType) : throw new ArgumentNullException(nameof (attributableType));
  }

  /// <summary>
  /// Читает значения указанной связи из базы данных PDM-системы.
  /// </summary>
  /// <param name="objRef">Ссылка на идентификатор связи</param>
  /// <param name="attributableType">Описатель для атрибутов связи</param>
  /// <returns>Контейнер со значениями атрибутов</returns>
  /// <exception cref="T:System.ArgumentNullException">Аргумент метода не может быть null</exception>
  public ValueBag ReadRelationAttributes(
    IDBRelationRef relationRef,
    IDBAttributableTypeRef attributableType)
  {
    return DbOperationsHelper.ReadRelationAttributes(relationRef, attributableType);
  }

  /// <summary>
  /// Читает значения атрибутов указанной связи из базы данных PDM-системы.
  /// </summary>
  /// <param name="relationItem">Связь между объектами PDM-системы</param>
  /// <param name="attributableType">Описатель для атрибутов связи</param>
  /// <returns>Контейнер со значениями атрибутов</returns>
  /// <exception cref="T:System.ArgumentNullException">Аргумент метода не может быть null</exception>
  public ValueBag ReadRelationAttributes(
    SectionEntity relationItem,
    IDBAttributableTypeRef attributableType)
  {
    if (relationItem == null)
      throw new ArgumentNullException(nameof (relationItem));
    if (attributableType == null)
      throw new ArgumentNullException(nameof (attributableType));
    RelationSection relationSection = relationItem.Sections.Get<RelationSection>();
    if (relationSection.NewRelation)
      return this.ReadBlankAttributes(attributableType);
    if (relationSection.ProjectItem == null)
      throw new InvalidOperationException(LocalizationHolder.rm.GetString("Tools.Components_445"));
    if (relationSection.RelationGuid == Guid.Empty)
      throw new InvalidOperationException(LocalizationHolder.rm.GetString("Tools.Components_446"));
    return this.ReadRelationAttributes((IDBRelationRef) new ProjectGuidDBRelationRef((IDBObjectRef) new DBObjectEntityRef(relationSection.ProjectItem), relationSection.RelationGuid), attributableType);
  }

  /// <summary>
  /// Читает значения атрибутов указанной связи из базы данных PDM-системы и сохраняет их в секции AttributesSection.
  /// </summary>
  /// <param name="relationItem">Связь между объектами PDM-системы</param>
  /// <param name="attributableType">Описатель для атрибутов связи</param>
  /// <returns>Контейнер со значениями атрибутов</returns>
  /// <exception cref="T:System.ArgumentNullException">Аргумент метода не может быть null</exception>
  public void FetchRelationAttributes(
    SectionEntity relationItem,
    IDBAttributableTypeRef attributableType)
  {
    if (relationItem == null)
      throw new ArgumentNullException(nameof (relationItem));
    relationItem.Sections.Get<AttributesSection>().DatabaseSet = attributableType != null ? this.ReadRelationAttributes(relationItem, attributableType) : throw new ArgumentNullException(nameof (attributableType));
  }

  public void EmitObjectAttributesServerActions(SectionEntity objectItem)
  {
    AttributesSection attributesSection = objectItem != null ? objectItem.Sections.Get<AttributesSection>((AttributesSection) null) : throw new ArgumentNullException(nameof (objectItem));
    if (attributesSection == null || !attributesSection.DatabaseSet.HasChanges)
      return;
    int objectType = ObjectSection.GetObjectType(objectItem);
    ObjectActionsSection objectActionsSection1 = objectItem.Sections.Get<ObjectActionsSection>();
    List<Tuple<StringKey, ValueRecordState>> changes = attributesSection.DatabaseSet.GetChanges();
    List<ValueRecord> items = new List<ValueRecord>(changes.Count);
    List<string> attributeKeys = new List<string>(changes.Count);
    foreach (Tuple<StringKey, ValueRecordState> tuple in changes)
    {
      if (tuple.Item2 == ValueRecordState.Added || tuple.Item2 == ValueRecordState.Modified)
      {
        if (this.checkoutOperations.RequireCheckoutOnObjectAttribute(objectType, tuple.Item1))
        {
          ObjectActionsSection objectActionsSection2 = objectActionsSection1;
          objectActionsSection2.RequireCheckout = ((objectActionsSection2.RequireCheckout ? 1 : 0) | 1) != 0;
        }
        ValueRecord valueRecord = attributesSection.DatabaseSet.Find(tuple.Item1);
        items.Add(valueRecord);
      }
      else if (tuple.Item2 == ValueRecordState.Removed)
      {
        if (this.checkoutOperations.RequireCheckoutOnObjectAttribute(objectType, tuple.Item1))
        {
          ObjectActionsSection objectActionsSection3 = objectActionsSection1;
          objectActionsSection3.RequireCheckout = ((objectActionsSection3.RequireCheckout ? 1 : 0) | 1) != 0;
        }
        attributeKeys.Add((string) tuple.Item1);
      }
    }
    if (items.Count != 0)
      objectActionsSection1.ObjectActions.ServerActions.Add((IAction) new WriteObjectAttributesAction((IDBObjectRef) new DBObjectEntityRef(objectItem), DBAttributeHelper.ToAttributeValues((IList<ValueRecord>) items)));
    if (attributeKeys.Count == 0)
      return;
    objectActionsSection1.ObjectActions.ServerActions.Add((IAction) new DeleteObjectAttributesAction((IDBObjectRef) new DBObjectEntityRef(objectItem), (IList<string>) attributeKeys));
  }

  public bool CanHaveIntegrationStatus(SectionEntity objectEntity)
  {
    if (objectEntity == null)
      throw new ArgumentNullException(nameof (objectEntity));
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return new DirectObjectAttributesRef(ObjectSection.GetObjectType(objectEntity)).GetAttributableType(sessionKeeper.Session).GetAttributeByID(IDCache.Default.IntegrationStatus.Id) != null;
  }

  public void UpdatePartialStructureStatus(SectionEntity objectEntity, bool partialStructureStatus)
  {
    if (objectEntity == null)
      throw new ArgumentNullException(nameof (objectEntity));
    ValueBag databaseSet = objectEntity.Sections.Get<AttributesSection>().DatabaseSet;
    string str = databaseSet.Read<string>((StringKey) IDCache.Default.IntegrationStatus.Text, string.Empty);
    DBObjectIntegrationStatus integrationStatus = new DBObjectIntegrationStatus(str);
    integrationStatus.PartialObjectStructure = partialStructureStatus;
    if (!(integrationStatus.Value != str))
      return;
    databaseSet.Update((StringKey) IDCache.Default.IntegrationStatus.Text, (object) integrationStatus.Value);
  }

  public void RemoveIntegrationStatusIfEmpty(SectionEntity objectEntity)
  {
    if (objectEntity == null)
      throw new ArgumentNullException(nameof (objectEntity));
    ValueRecord valueRecord = objectEntity.Sections.Get<AttributesSection>().DatabaseSet.Find((StringKey) IDCache.Default.IntegrationStatus.Text);
    if (valueRecord == null || !new DBObjectIntegrationStatus(valueRecord.Read<string>(string.Empty)).IsEmpty)
      return;
    valueRecord.Remove();
  }

  public bool CanHaveIntegrationErrors(SectionEntity objectEntity)
  {
    if (objectEntity == null)
      throw new ArgumentNullException(nameof (objectEntity));
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return new DirectObjectAttributesRef(ObjectSection.GetObjectType(objectEntity)).GetAttributableType(sessionKeeper.Session).GetAttributeByID(IDCache.Default.IntegrationErrors.Id) != null;
  }

  public ValueRecord GetIntegrationErrors(SectionEntity objectEntity)
  {
    AttributesSection attributesSection = objectEntity != null ? objectEntity.Sections.Get<AttributesSection>() : throw new ArgumentNullException(nameof (objectEntity));
    if (!attributesSection.DatabaseSet.Exists((StringKey) IDCache.Default.IntegrationErrors.Text))
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBAttribute attributeById = sessionKeeper.Session.GetObject(ObjectSection.GetObjectId(objectEntity), true).GetAttributeByID(IDCache.Default.IntegrationErrors.Id);
        if (attributeById != null)
        {
          attributesSection.DatabaseSet.Add(new ValueRecord((StringKey) IDCache.Default.IntegrationErrors.Text, (object) (string) attributeById.Value));
          attributesSection.DatabaseSet.AcceptChanges((StringKey) IDCache.Default.IntegrationErrors.Text);
        }
        else
          attributesSection.DatabaseSet.Add(new ValueRecord((StringKey) IDCache.Default.IntegrationErrors.Text, (object) DbOperations.integrationErrorsEmptyXmlString));
      }
    }
    return attributesSection.DatabaseSet.Find((StringKey) IDCache.Default.IntegrationErrors.Text);
  }

  public DBObjectErrorsBuilder GetIntegrationErrorsBuilder(SectionEntity objectEntity)
  {
    string xmlString = (string) this.GetIntegrationErrors(objectEntity).Value;
    return !string.IsNullOrEmpty(xmlString) ? new DBObjectErrorsBuilder(xmlString) : new DBObjectErrorsBuilder();
  }

  public void UpdateIntegrationErrors(
    SectionEntity objectEntity,
    DBObjectErrorsBuilder errorsBuilder)
  {
    if (objectEntity == null)
      throw new ArgumentNullException(nameof (objectEntity));
    if (errorsBuilder == null)
      throw new ArgumentNullException(nameof (errorsBuilder));
    ValueRecord integrationErrors = this.GetIntegrationErrors(objectEntity);
    string str = (string) integrationErrors.Value;
    string xmlString = errorsBuilder.ToXmlString();
    if (!(xmlString != str))
      return;
    integrationErrors.Value = (object) xmlString;
  }

  public void RemoveIntegrationErrorsIfEmpty(SectionEntity objectEntity)
  {
    if (objectEntity == null)
      throw new ArgumentNullException(nameof (objectEntity));
    ValueRecord valueRecord = objectEntity.Sections.Get<AttributesSection>().DatabaseSet.Find((StringKey) IDCache.Default.IntegrationErrors.Text);
    if (valueRecord == null || !object.Equals(valueRecord.Value, (object) DbOperations.integrationErrorsEmptyXmlString))
      return;
    valueRecord.Remove();
  }

  public void EmitUIActions(CaptureChangesDriverContext ctx, SectionEntity objectItem)
  {
    if (ctx == null)
      throw new ArgumentNullException(nameof (ctx));
    ObjectSection objectSection = objectItem != null ? objectItem.Sections.Get<ObjectSection>() : throw new ArgumentNullException(nameof (objectItem));
    ActionQueuePair objectActions = objectItem.Sections.Get<ObjectActionsSection>().ObjectActions;
    if (objectSection.ExistenceStatus == ObjectExistenceStatus.NewObject)
      objectActions.ClientActions.Add((IAction) new FireObjectCreatedAction((IDBObjectRef) new DBObjectEntityRef(objectItem), ctx.UINotifications));
    else if (objectSection.ExistenceStatus == ObjectExistenceStatus.ConvertedObject)
    {
      objectActions.ClientActions.Add((IAction) new FireObjectModifiedAction((IDBObjectRef) new DBObjectEntityRef(objectItem), ctx.UINotifications));
    }
    else
    {
      if (objectActions.ServerActions.Count <= 0)
        return;
      objectActions.ClientActions.Add((IAction) new FireObjectModifiedAction((IDBObjectRef) new DBObjectEntityRef(objectItem), ctx.UINotifications));
    }
  }
}
