
// Type: Intermech.Tools.Data.Sync.AppToDBAttributeSyncTask
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Collections;
using Intermech.Data;
using Intermech.Interfaces;
using Intermech.Interfaces.Data;
using Intermech.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Tools.Data.Sync;

/// <summary>
/// Реализует перенос атрибутов из файла документа в объект IPS.
/// </summary>
public class AppToDBAttributeSyncTask : AppToDBAttributeSyncTaskBase
{
  private AttributableElements entityAttributableType;
  private long entityId;

  /// <summary>Создает объект.</summary>
  public AppToDBAttributeSyncTask()
  {
    this.entityAttributableType = AttributableElements.Object;
    this.entityId = 0L;
  }

  /// <summary>
  /// Возвращает или задает тип элемента IPS, атрибуты которого синхронизируются.
  /// </summary>
  public AttributableElements EntityAttributableType
  {
    get => this.entityAttributableType;
    set => this.entityAttributableType = value;
  }

  /// <summary>
  /// Возвращает или задает идентификатор элемента IPS, атрибуты которого синхронизируются.
  /// </summary>
  public long EntityId
  {
    get => this.entityId;
    set => this.entityId = value;
  }

  /// <summary>
  /// Указывает атрибуты, прочитанные из файла документа. Они будут являться передающей стороной в процессе синхронизации атрибутов.
  /// </summary>
  /// <param name="table">Таблица с атрибутами</param>
  /// <param name="isOpenMetadata">Признак открытого формата метаданных у файла документа</param>
  /// <exception cref="T:System.ArgumentNullException">Не указана таблица с атрибутами</exception>
  public void SetApplicationAttributes(ValueBag table, bool isOpenMetadata)
  {
    if (table == null)
      throw new ArgumentNullException(nameof (table));
    this.SetSource(table, (IAttributeSyncHelper) new AppAttributeSyncHelper(isOpenMetadata));
  }

  /// <summary>
  /// Указывает атрибуты, прочитанные из объекта документа в базе IPS. Они будут являться принимающей стороной в процессе синхронизации атрибутов.
  /// </summary>
  /// <param name="table">Таблица с атрибутами</param>
  /// <param name="attributableTypeRef">Вспомогательный объект для получения метаданных атрибутов документа в базе IPS</param>
  /// <exception cref="T:System.ArgumentNullException">Не указана таблица с атрибутами</exception>
  public void SetDatabaseAttributes(ValueBag table, IDBAttributableTypeRef attributableTypeRef)
  {
    if (table == null)
      throw new ArgumentNullException(nameof (table));
    this.SetTarget(table, (IAttributeSyncHelper) new DBAttributeSyncHelper(attributableTypeRef));
  }

  /// <summary>
  /// Выбирает направление и способ переноса значения для указанного атрибута.
  /// </summary>
  /// <param name="detectData">Сведения об атрибуте и результаты работы метода</param>
  protected override void DoDetectAttributeAction(DetectAttributeSyncActionArgs detectData)
  {
    this.DetectDatabaseFlags(detectData.Attribute);
    if (detectData.Action != null)
      return;
    if (detectData.Attribute.Flags[AppToDBAttributeSyncTaskBase.IsSystemFlag])
      this.DetectSystemAttributeAction(detectData);
    else if (detectData.Attribute.Flags[AppToDBAttributeSyncTaskBase.IsComputableFlag])
      detectData.Direction = SyncDirection.Backward;
    else if (detectData.Attribute.Flags[AppToDBAttributeSyncTaskBase.IsObjectLinkFlag])
    {
      detectData.Direction = SyncDirection.Backward;
      detectData.Action = (AttributeSyncAction) AppToDBAttributeSyncTaskBase.defaultObjectLinkAction;
    }
    else
      base.DoDetectAttributeAction(detectData);
  }

  private void DetectSystemAttributeAction(DetectAttributeSyncActionArgs detectData)
  {
    detectData.Direction = SyncDirection.Backward;
    if (detectData.Attribute.Key == (StringKey) AppToDBAttributeSyncTaskBase.InternalCaches.IDCache.ObjectType.Text)
      detectData.Action = (AttributeSyncAction) AppToDBAttributeSyncTaskBase.defaultObjectTypeAction;
    else if (detectData.Attribute.Key == (StringKey) AppToDBAttributeSyncTaskBase.InternalCaches.IDCache.OwnerId.Text || detectData.Attribute.Key == (StringKey) AppToDBAttributeSyncTaskBase.InternalCaches.IDCache.CheckoutById.Text || detectData.Attribute.Key == (StringKey) AppToDBAttributeSyncTaskBase.InternalCaches.IDCache.ProjectId.Text)
    {
      detectData.Action = (AttributeSyncAction) AppToDBAttributeSyncTaskBase.defaultObjectLinkAction;
    }
    else
    {
      if (!detectData.Attribute.Flags[AppToDBAttributeSyncTaskBase.IsObjectLinkFlag])
        return;
      detectData.Action = (AttributeSyncAction) AppToDBAttributeSyncTaskBase.defaultObjectLinkAction;
    }
  }

  /// <summary>
  /// Вызывается непосредственно перед переносом атрибутов в обратном направлении. Метод вызывается только в том случае, если есть атрибуты, требующие переноса.
  /// </summary>
  /// <param name="taskData">Данные систем, участвующих в переносе атрибутов. Принимающая и передающая сторона переставлены местами</param>
  /// <param name="attributes">Список атрибутов, которые будут перенесены</param>
  protected override void OnBeforeBackwardRun(
    AttributeSyncTaskData taskData,
    IEnumerable<AttributeSyncUnit> attributes)
  {
    base.OnBeforeBackwardRun(taskData, attributes);
    if (!taskData.SourceTable.HasChanges)
      return;
    this.RecalculateComputableAttributes(taskData, attributes);
  }

  /// <summary>
  /// Обновляет значения вычисляемых атрибутов на принимающей стороне перед их переносом на передающую сторону.
  /// </summary>
  /// <param name="taskData">Параметры задачи переноса атрибутов в обратном направлении. Принимающая и передающая сторона переставлены местами</param>
  /// <param name="attributes">Список атрибутов, которые будут перенесены</param>
  private void RecalculateComputableAttributes(
    AttributeSyncTaskData taskData,
    IEnumerable<AttributeSyncUnit> attributes)
  {
    if (this.entityAttributableType != AttributableElements.Object || this.entityId == 0L || !(taskData.SourceSyncHelper is DBAttributeSyncHelper sourceSyncHelper))
      return;
    LinkedList<AttributeSyncUnit> allAsLinkedList = CollectionUtils.FindAllAsLinkedList<AttributeSyncUnit>(attributes, (Predicate<AttributeSyncUnit>) (attribute => attribute.Flags[AppToDBAttributeSyncTaskBase.IsComputableFlag]));
    if (allAsLinkedList.Count == 0)
      return;
    List<ValueRecord> computableAttributesInput = this.GetComputableAttributesInput(taskData.SourceTable.GetChangedItems(), (IEnumerable<AttributeSyncUnit>) allAsLinkedList, sourceSyncHelper);
    if (computableAttributesInput.Count == 0)
      return;
    if (UIReport.Enabled)
    {
      UIReportItem uiReportItem = new UIReportItem();
      uiReportItem.TraceLevel = TraceLevel.Verbose;
      uiReportItem.Text = "Выполняется пересчет значений вычисляемых атрибутов:";
      uiReportItem.Data = (object[]) CollectionUtils.ConvertAsArray<AttributeSyncUnit, string>((ICollection<AttributeSyncUnit>) allAsLinkedList, (Converter<AttributeSyncUnit, string>) (attr => (string) attr.Key));
      UIReport.Indent();
      UIReport.ReportItem(uiReportItem);
      UIReport.Unindent();
    }
    AttributeValues[] attributeValues = DBAttributeHelper.ToAttributeValues((IList<ValueRecord>) computableAttributesInput);
    List<ValueRecord> valueRecordList;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      List<AttributeValues> rawValues = new List<AttributeValues>((IEnumerable<AttributeValues>) sessionKeeper.Session.GetObject(this.entityId).GetCalculatedValues(attributeValues, GetAttributeValuesModes.IncludeObligatoryAttributes));
      rawValues.RemoveAll((Predicate<AttributeValues>) (item => item.ComputeMode == ComputeValueModes.NotComputableValue));
      valueRecordList = DBAttributeHelper.ReadEntityValues(sourceSyncHelper.AttributableType, (ICollection<AttributeValues>) rawValues);
    }
    foreach (ValueRecord newItem in valueRecordList)
      AppToDBAttributeSyncTask.AddOrReplaceItem(taskData.SourceTable, newItem);
  }

  private List<ValueRecord> GetComputableAttributesInput(
    List<ValueRecord> items,
    IEnumerable<AttributeSyncUnit> computableAttributes,
    DBAttributeSyncHelper dbSyncHelper)
  {
    List<ValueRecord> computableAttributesInput = new List<ValueRecord>(items.Count);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttribute4TypeCollection attributableType = dbSyncHelper.AttributableType.GetAttributableType(sessionKeeper.Session);
      foreach (AttributeSyncUnit computableAttribute in computableAttributes)
      {
        IDBAttributeType4 attributeByName = attributableType.GetAttributeByName((string) computableAttribute.Key);
        if (attributeByName != null)
        {
          foreach (int formulaAttribute in attributeByName.GetRelatedFormulaAttributes())
          {
            int formulaAttrId = formulaAttribute;
            ValueRecord valueRecord = CollectionUtils.TryExtract<ValueRecord>((IList<ValueRecord>) items, (Predicate<ValueRecord>) (item => item.Key == AppToDBAttributeSyncTask.AttributeIdToKey(formulaAttrId)));
            if (valueRecord != null)
            {
              computableAttributesInput.Add(valueRecord);
              if (items.Count == 0)
                break;
            }
          }
          if (items.Count == 0)
            break;
        }
      }
    }
    return computableAttributesInput;
  }

  private static StringKey AttributeIdToKey(int attributeId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return new StringKey(sessionKeeper.Session.GetAttributeType(attributeId).Name);
  }

  private static void AddOrReplaceItem(ValueBag bag, ValueRecord newItem)
  {
    bag.Remove(newItem.Key);
    bag.Add(newItem);
    bag.AcceptChanges(newItem.Key);
  }
}
