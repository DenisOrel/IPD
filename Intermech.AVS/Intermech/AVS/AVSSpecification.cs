// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.AVSSpecification
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.AVS.Output;
using Intermech.DataFormats;
using Intermech.Document.Client;
using Intermech.Document.DBCore;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Document;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using Intermech.PropertyEditors.AttrProcessor;
using Intermech.Tools.LaunchActions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS;

public class AVSSpecification : AVSDocument
{
  private IAttributesLockService attLockService;

  /// <summary>Конструктор</summary>
  /// <param name="avsWindow">Окно редактора</param>
  /// <param name="objectType">Тип объекта</param>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <param name="createUndo">Создавать данные для восстановления</param>
  /// <param name="readOnly">Только для чтения</param>
  public AVSSpecification(
    AVSWindow avsWindow,
    int objectType,
    long objectId,
    bool readOnly,
    bool? createUndo)
    : base(avsWindow, objectType, objectId, readOnly, createUndo)
  {
  }

  /// <summary>Конструктор для генерации СП без документа в базе</summary>
  /// <param name="objectType">Тип объекта</param>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <param name="documentForm">Форма конструкторского документа. Если форма единичная, то исполнения игнорируются</param>
  /// <param name="useFiltration">Использовать правила подбора и фильтрацию состава</param>
  /// <param name="configureCompositionRoot">Корень конфигурации состава</param>
  /// <param name="filtrationOwnerID">Владелец настроек фильтрации</param>
  public AVSSpecification(
    int objectType,
    long objectId,
    AVSDocumentForm documentForm,
    RelationPair configureCompositionRoot,
    string filtrationOwnerID,
    bool readOnly)
    : base(objectType, objectId, documentForm, configureCompositionRoot, filtrationOwnerID, readOnly)
  {
  }

  /// <summary>Конструктор для фабрики классов</summary>
  public AVSSpecification()
  {
  }

  protected override bool IsAllowableObjectType(int objectType)
  {
    return AVSDocument.GetDefaultSectionForType(objectType) != null;
  }

  /// <summary>Получить список допустимых разделов для спецификации</summary>
  /// <returns></returns>
  public override List<SpecificationSectionInfo> GetAllowableDocumentSections()
  {
    if (!SpecificationSectionInfo.Cached)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        SpecificationSectionInfo.CacheSpecSections(sessionKeeper.Session);
    }
    List<SpecificationSectionInfo> documentSections = SpecificationSectionInfo.GetAllowableSpecSections(this.AVSDocumentTemplateID);
    if (documentSections == null || documentSections.Count == 0)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        documentSections = SpecificationSectionInfo.CacheSpecSections(sessionKeeper.Session, this.AVSDocumentTemplateID, new AVSDocumentType?(this.avsDocumentType));
    }
    if (documentSections == null)
      documentSections = new List<SpecificationSectionInfo>();
    return documentSections;
  }

  /// <summary>Получить список допустимых разделов для спецификации</summary>
  /// <param name="templateId">Идентификатор шаблона</param>
  /// <param name="docType">Тип конструкторского документа</param>
  /// <returns></returns>
  public static List<SpecificationSectionInfo> GetAllowableDocumentSections(
    long templateId,
    AVSDocumentType? docType)
  {
    if (!SpecificationSectionInfo.Cached)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        SpecificationSectionInfo.CacheSpecSections(sessionKeeper.Session);
    }
    List<SpecificationSectionInfo> documentSections = SpecificationSectionInfo.GetAllowableSpecSections(templateId);
    if (documentSections == null)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        documentSections = SpecificationSectionInfo.CacheSpecSections(sessionKeeper.Session, templateId, docType);
    }
    if (documentSections == null)
      documentSections = new List<SpecificationSectionInfo>();
    return documentSections;
  }

  public static List<AvsRowAttributeInfo> GetVirtualAttributeListForSP()
  {
    List<AvsRowAttributeInfo> attributeListForDocument = AVSDocument.GetVirtualAttributeListForDocument();
    attributeListForDocument.AddRange((IEnumerable<AvsRowAttributeInfo>) AvsIDCache.VirtualAttributes);
    return attributeListForDocument;
  }

  public override List<AvsRowAttributeInfo> GetVirtualAttributeList()
  {
    return AVSSpecification.GetVirtualAttributeListForSP();
  }

  /// <summary>Получить список типов связей по которым загружаются данные для записей</summary>
  /// <returns></returns>
  internal override List<int> GetRelationTypesUsedInDocument()
  {
    return AVSSpecification.GetDefaultRelationTypesUsedInSpecification();
  }

  internal static List<int> GetDefaultRelationTypesUsedInSpecification()
  {
    return new List<int>()
    {
      AvsIDCache.Relation_Document,
      AvsIDCache.Relation_Project,
      AvsIDCache.Relation_Zagotovka,
      AvsIDCache.Relation_Podbor,
      AvsIDCache.Relation_AddComplect
    };
  }

  /// <summary>Проверить наличие базовых таблиц в документе и выбросить исключение, если их нет</summary>
  protected override void CheckMainDocumentTablesAndThrowException()
  {
    if (this.avsDocTable == null && this.avsFormB_Table == null)
      throw new Exception($"Нарушена структура документа! В спецификации \"{this.DocumentCaption}\" не найдена таблица \"Таблица Спецификация\".");
  }

  /// <summary>Загрузить все связи полученные от родительских изделий или исполнений</summary>
  /// <param name="loadContext">Контекст загрузки данных. Если null, то создаётся контекст с параметрами по умолчанию</param>
  /// <param name="rowDicts">Словари строк документа</param>
  internal override void LoadAllProductsRelations(
    AVSDocumentContext loadContext,
    RowDictionariesForLoadDocument rowDicts)
  {
    for (int index = 0; index < this.productsInfo.Count; ++index)
      this.LoadProductData(this.productsInfo[index], loadContext, rowDicts);
  }

  /// <summary>Объект хранящий настройки графы "Примечание"</summary>
  internal override long NoteFieldSettingsObjectID => AVSDocument.ObjID_CommonSpecificationTemplate;

  /// <summary>Выводить символ «*» рядом с Позиционным обозначением основного компонента</summary>
  internal override bool InsertStarAfterPositionDesignation
  {
    get => AvsConfig.Podbor.InsertStarAfterPositionDesignationInSP;
  }

  protected override void CheckPartWithoutZagotovka(
    AVSRow partRow,
    List<SpecRowCheckMessage> rowMessages)
  {
    long partMaterialObjectID;
    if (partRow == null || partRow.IsZagotovka() || !partRow.HasPartAsMaterial(out partMaterialObjectID) || Intermech.Consts.IsUndefinedObjectId(partMaterialObjectID) || this.CheckExistingDraftsForPart(partRow, partMaterialObjectID).Item1)
      return;
    rowMessages.Add(new SpecRowCheckMessage(AVSCheckType.PartWithoutDraft, (string) null));
  }

  protected override bool CheckDraftCountValue(
    AVSRow draftRow,
    List<SpecRowCheckMessage> rowMessages = null)
  {
    bool flag = true;
    if (draftRow == null || !draftRow.IsZagotovka())
      return flag;
    AVSRow partForDraft = draftRow.GetPartForDraft();
    if (partForDraft == null || partForDraft.Relations == null)
      return flag;
    if (partForDraft.Relations.Count != draftRow.Relations.Count)
    {
      rowMessages?.Add(new SpecRowCheckMessage(AVSCheckType.DraftCountDoesntMatch, (string) null));
      flag = false;
    }
    else
    {
      for (int index = 0; index < partForDraft.Relations.Count; ++index)
      {
        int productIndex = this.GetProductIndex(partForDraft.Relations[index].ProjectId);
        MeasuredValue count1 = partForDraft.GetCount(-1, productIndex);
        MeasuredValue count2 = draftRow.GetCount(-1, productIndex);
        if ((count1 != null || count2 != null) && (count1 == null || count2 == null || MeasureHelper.Compare(count1, count2) != CompareResult.Equal))
        {
          rowMessages?.Add(new SpecRowCheckMessage(AVSCheckType.DraftCountDoesntMatch, (string) null, productIndex, this.Field_Count));
          flag = false;
          break;
        }
      }
    }
    return flag;
  }

  internal override bool CanMergeRelationsInSummRows(AvsRowData relation1, AvsRowData relation2)
  {
    CellOutputMapping noteCellMapping = relation1.AvsRow?.NoteCellMapping ?? relation2.AvsRow?.NoteCellMapping;
    if (!this.CanSummThisRelations(relation1, relation2, noteCellMapping) || relation1.ProductID != relation2.ProductID || relation1.RelationType == AvsIDCache.Relation_Podbor || relation2.RelationType == AvsIDCache.Relation_Podbor || relation1.GetFieldBoolValue(this.Attr_Podbor, false) || relation2.GetFieldBoolValue(this.Attr_Podbor, false) || relation1.GetFieldStringValue(this.Attr_FunctionalGroupPosDesignation, false) != relation2.GetFieldStringValue(this.Attr_FunctionalGroupPosDesignation, false))
      return false;
    string fieldStringValue1 = relation1.GetFieldStringValue(this.Field_PosDesignation, false);
    string fieldStringValue2 = relation2.GetFieldStringValue(this.Field_PosDesignation, false);
    return AVSDocument.IsMergeRelationsWithoutPosDesignation(fieldStringValue1, fieldStringValue2) || AVSDocument.IsContinuousSequencePosDesignation(fieldStringValue1, fieldStringValue2);
  }

  internal override bool CanSummThisRelations(
    AvsRowData rowData1,
    AvsRowData rowData2,
    CellOutputMapping noteCellMapping)
  {
    return (AvsConfig.Podbor.SummarizePartsForPodbor || !rowData1.GetFieldBoolValue(this.Attr_Podbor, false) && !rowData2.GetFieldBoolValue(this.Attr_Podbor, false)) && (AvsConfig.Podbor.SummarizePartsForPodbor || rowData1.RelationType != AvsIDCache.Relation_Podbor) && !(rowData1.GetFieldStringValue(this.Field_Position, false) != rowData2.GetFieldStringValue(this.Field_Position, false)) && !(rowData1.GetFieldStringValue(this.Field_Zone, false) != rowData2.GetFieldStringValue(this.Field_Zone, false)) && (rowData1.AvsRow == null || rowData2.AvsRow == null || this.IsRowsWithEqualFieldNameAdditionalNotes(rowData1.AvsRow, rowData2.AvsRow)) && base.CanSummThisRelations(rowData1, rowData2, noteCellMapping);
  }

  private bool IsRowsWithEqualFieldNameAdditionalNotes(AVSRow avsRow1, AVSRow avsRow2)
  {
    if (avsRow1.IsFormB != avsRow2.IsFormB)
      return false;
    if (avsRow1.IsFormB)
    {
      for (int productIndex = 0; productIndex < this.productsInfo.Count; productIndex += this.RowProductCount)
      {
        if (avsRow1.GetAdditionalNameNote(productIndex) != avsRow2.GetAdditionalNameNote(productIndex))
          return false;
      }
    }
    else if (avsRow1.GetAdditionalNameNote(-1) != avsRow2.GetAdditionalNameNote(-1))
      return false;
    return true;
  }

  protected override bool AllowIncludeRelationInDocument(RelationAttributeValuesCache relation)
  {
    return base.AllowIncludeRelationInDocument(relation) && relation.SortIndex != long.MaxValue;
  }

  /// <summary>Проверить режим редактирования Спецификации</summary>
  /// <returns>Возвращает сообщение для вопроса пользователю "...открыть только для чтения?"</returns>
  private string CheckSpecificationModifyMode()
  {
    if (this.DocumentID > 0L)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(this.DocumentID);
        switch (dbObject.ObjectModifyMode)
        {
          case ObjectModifyModes.Checkout:
            if (dbObject.CheckoutBy == sessionKeeper.Session.UserID)
            {
              this.DocumentID = -this.DocumentID;
              break;
            }
            if (dbObject.CheckoutBy != 0L)
              return $"Спецификацию \"{dbObject.Caption}\" нельзя изменять, т.к. она взята на изменение другим пользователем.{Environment.NewLine}Открыть спецификацию только для чтения?";
            break;
          case ObjectModifyModes.CreateVersion:
            return $"Чтобы изменять спецификацию \"{dbObject.Caption}\" нужно выпустить версию объекта.{Environment.NewLine}Открыть спецификацию только для чтения?";
          case ObjectModifyModes.CantModify:
            return $"Нельзя изменять спецификацию \"{dbObject.Caption}\" на текущем шаге жизненного цикла.{Environment.NewLine}Открыть спецификацию только для чтения?";
        }
      }
    }
    return "";
  }

  /// <summary>Проверить режим редактирования изделия</summary>
  /// <returns>Возвращает сообщение для вопроса пользователю "...открыть только для чтения?"</returns>
  private string CheckProductModifyMode(ProductInfo product)
  {
    if (product.Id > 0L)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (product.CheckoutBy == sessionKeeper.Session.UserID)
        {
          product.Id = -product.Id;
          return "";
        }
        IDBObject dbObject = sessionKeeper.Session.GetObject(product.Id);
        switch (dbObject.ObjectModifyMode)
        {
          case ObjectModifyModes.Checkout:
            if (dbObject.CheckoutBy != 0L)
              return $"Изделие \"{dbObject.Caption}\" нельзя изменять, т.к. оно взято на изменение другим пользователем.\r\nОткрыть спецификацию только для чтения?";
            break;
          case ObjectModifyModes.CreateVersion:
            return $"Чтобы изменять изделие \"{dbObject.Caption}\", нужно выпустить версию объекта.\r\nОткрыть спецификацию только для чтения?";
          case ObjectModifyModes.CantModify:
            return $"Нельзя изменять объект \"{dbObject.Caption}\" на текущем шаге жизненного цикла.{Environment.NewLine}Открыть спецификацию только для чтения?";
        }
      }
    }
    return "";
  }

  /// <summary>Сгенерировать запись о заготовке</summary>
  /// <param name="contextNode">Контекст</param>
  /// <param name="useCurrentMaterial">Создавать запись о заготовке на базе текущего материала изделия</param>
  /// <param name="newMaterialData">Объект с информацией о новом материале заготовки. Игнорируется, если useCurrentMaterial = false</param>
  public void GenerateRowForZagotovka(
    DocumentTreeNode contextNode,
    AVSRow sourceRow,
    bool useCurrentMaterial = false,
    object newMaterialData = null)
  {
    long oldMaterialId;
    QuickObjectInfo? oldMaterialInfo;
    string errorMessage;
    (bool flag, List<ProductInfo> collection, AVSRow destinationRow) = sourceRow != null && sourceRow.HasObject ? this.CheckCanCreateDraftForPart(sourceRow, useCurrentMaterial, out oldMaterialId, out oldMaterialInfo, out errorMessage) : throw new ArgumentException("Ошибка добавления заготовки.", nameof (sourceRow));
    if (!flag)
    {
      if (!useCurrentMaterial)
        throw new Exception("Ошибка добавления заготовки. " + errorMessage);
    }
    else
    {
      if (useCurrentMaterial)
        newMaterialData = (object) oldMaterialInfo;
      if (newMaterialData == null)
        return;
      AVSDocumentContext contextChapters = this.GetContextChapters(contextNode);
      if (collection != null && collection.Count > 0)
      {
        contextChapters.Products.Clear();
        contextChapters.Products.AddRange((IEnumerable<ProductInfo>) collection);
      }
      else if (contextNode == null && sourceRow.Relations != null)
        contextChapters.Products.AddRange(sourceRow.Relations.Select<RelationAttributeValuesCache, ProductInfo>((Func<RelationAttributeValuesCache, ProductInfo>) (r => r.projInfo)));
      contextChapters.DefaultRelationType = AvsIDCache.Relation_Zagotovka;
      this.SuspendDocumentAndGridUpdates();
      try
      {
        if (!useCurrentMaterial)
          this.SetNewMaterialForPart(sourceRow.ObjectId, oldMaterialId, newMaterialData);
        this.CreateZagotovkaRow(contextChapters, sourceRow, destinationRow, newMaterialData, sourceRow.ObjectId);
      }
      finally
      {
        this.ResumeDocumentAndGridUpdates(0, true, true, true, true);
      }
    }
  }

  /// <summary>
  /// Проверяет, можно ли менять значение атрибута Material детали (при необходимости берет объект на редактирование)
  /// </summary>
  /// <param name="partRow">Строка объекта детали в документе</param>
  /// <param name="reason">Причина невозможности изменения атрибута</param>
  /// <returns>true - значение можно изменять, в противном случае - false</returns>
  public bool VerifyMaterialEditableForPartRow(AVSRow partRow, out string reason)
  {
    reason = "";
    IDBObject dbObj = (IDBObject) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      dbObj = sessionKeeper.Session.GetObject(partRow.ObjectId);
    string reasonMessage;
    (bool flag1, bool flag2) = DocumentEditorLaunchHandler.AdvancedEditModeCheckForObject(LaunchType.Edit, dbObj, out reasonMessage);
    if (!flag1)
    {
      reason = reasonMessage;
      return false;
    }
    bool flag3 = false;
    using (new SessionKeeper())
    {
      if (flag2)
      {
        dbObj = dbObj.CheckOut();
        flag3 = true;
      }
      AttributeProcessor attributeProcessor = partRow.GetObjectAttributeProcessor(true, true, dbObj.ObjectID);
      if (attributeProcessor != null)
      {
        AttributeValues attributeValues = (AttributeValues) null;
        if (attributeProcessor.Loaded)
          attributeValues = attributeProcessor.FindAttributeValues(AvsIDCache.Attr_Material);
        if (attributeValues != null)
        {
          if (!attributeValues.ReadOnly)
            return true;
          if (flag3)
            dbObj.CancelChanges();
          reason = "Значение атрибута 'Материал' изделия не может быть изменено.";
          return false;
        }
      }
    }
    reason = "Не удалось получить информацию об атрибуте 'Материал' изделия.";
    return false;
  }

  internal (bool, List<ProductInfo>, AVSRow) CheckCanCreateDraftForPart(
    AVSRow sourceRow,
    bool useCurrentMaterial,
    out long oldMaterialId,
    out QuickObjectInfo? oldMaterialInfo,
    out string errorMessage)
  {
    oldMaterialId = -1L;
    oldMaterialInfo = new QuickObjectInfo?();
    errorMessage = "";
    AVSRow avsRow1 = (AVSRow) null;
    List<ProductInfo> productInfoList1 = (List<ProductInfo>) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject objectActual = sessionKeeper.Session.GetObjectActual(sourceRow.ObjectId, true);
      IDBAttribute attributeById = objectActual.GetAttributeByID(AvsIDCache.Attr_Material);
      long id = objectActual.ID;
      string reason;
      if (!useCurrentMaterial && !this.VerifyMaterialEditableForPartRow(sourceRow, out reason))
      {
        errorMessage = reason;
        return (false, (List<ProductInfo>) null, (AVSRow) null);
      }
      if (!useCurrentMaterial)
      {
        this.attLockService = this.attLockService ?? ServiceUtils.GetService<IAttributesLockService>((object) ServicesManager.ServiceContainer, true);
        ICollection<int> lockedAttributes = this.attLockService?.GetLockedAttributes(AttributableElements.Object, objectActual.ObjectID, objectActual.ObjectType);
        if (lockedAttributes != null && lockedAttributes.Contains(AvsIDCache.Attr_Material))
        {
          errorMessage = "Заготовка не может быть добавлена для изделия с CAD-моделью.";
          return (false, (List<ProductInfo>) null, (AVSRow) null);
        }
      }
      if (attributeById != null && attributeById.Value != null && attributeById.Value != DBNull.Value)
        oldMaterialId = Convert.ToInt64(attributeById.Value);
      if (oldMaterialId != -1L)
        oldMaterialInfo = new QuickObjectInfo?(sessionKeeper.Session.GetObjectInfo(oldMaterialId));
      if (useCurrentMaterial && !oldMaterialInfo.HasValue)
      {
        errorMessage = "Не определен материал для заготовки.";
        return (false, (List<ProductInfo>) null, (AVSRow) null);
      }
      if (oldMaterialInfo.HasValue)
      {
        (bool flag, List<ProductInfo> productInfoList2, AVSRow avsRow2) = this.CheckExistingDraftsForPart(sourceRow, oldMaterialId, !useCurrentMaterial);
        if (flag)
        {
          string caption = sessionKeeper.Session.GetObjectInfo(oldMaterialId).Caption;
          errorMessage = $"Для выбранного изделия уже существует заготовка \"{caption}\"";
          return (false, (List<ProductInfo>) null, avsRow2);
        }
        productInfoList1 = productInfoList2;
        if (avsRow2 != null && this.IsFormB)
        {
          avsRow2.Remove();
          avsRow2 = (AVSRow) null;
        }
        avsRow1 = avsRow2;
      }
    }
    return (true, productInfoList1 ?? new List<ProductInfo>(), avsRow1);
  }

  private void CreateZagotovkaRow(
    AVSDocumentContext context,
    AVSRow sourceRow,
    AVSRow destinationRow,
    object materialData,
    long sourceObjectId)
  {
    long num = -1;
    if (destinationRow != null && !destinationRow.IsFreeSortIndex)
      num = destinationRow.SortIndex;
    List<AVSRow> avsRowList = this.AddAvsRowParts(new object[1]
    {
      materialData
    }, AvsIDCache.Relation_Zagotovka, context, false, true, (IList<long>) new long[1]
    {
      num
    });
    if (avsRowList == null || avsRowList.Count <= 0)
      return;
    this.SetCountValueForBlank(sourceRow, avsRowList[0]);
    avsRowList[0].SetFieldValue(this.Field_Position, -1, -1, (object) "-", true, true, true, this.IsGridViewMode, false, false);
    avsRowList[0].SetFieldValue(new AvsRowAttributeInfo(true, AvsIDCache.Attr_ArticleID), -1, -1, (object) Math.Abs(sourceObjectId), true, true, true, false, true, true);
    avsRowList[0].SetLinkFromDraftToPart(sourceRow);
  }

  private void SetCountValueForBlank(AVSRow partRow, AVSRow zagotovkaRow)
  {
    if (partRow.Relations == null)
      return;
    for (int index = 0; index < partRow.Relations.Count; ++index)
    {
      int productIndex = this.GetProductIndex(partRow.Relations[index].ProjectId);
      zagotovkaRow.SetCount(productIndex, partRow.GetFieldValue(this.Field_Count, index, productIndex, true, false), true);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sourceRow">строка с деталью</param>
  /// <param name="materialId">id материала</param>
  /// <param name="removeIfExists">удалить строку заготовки со связями (если есть) и сделать вид, что ее не было</param>
  /// <returns>кортеж: (есть ли все связи-заготовки + список исполнений, для которых нет связей-заготовок)</returns>
  private (bool, List<ProductInfo>, AVSRow) CheckExistingDraftsForPart(
    AVSRow sourceRow,
    long materialId,
    bool removeIfExists = false)
  {
    bool flag1 = false;
    List<ProductInfo> source = new List<ProductInfo>();
    AVSRow avsRow = (AVSRow) null;
    List<AVSRow> list = this.GetAllRows(false, false).Where<AVSRow>((Func<AVSRow, bool>) (r => r.IsZagotovka())).ToList<AVSRow>();
    IOrderedEnumerable<Guid> first = sourceRow.Relations.Select<RelationAttributeValuesCache, Guid>((Func<RelationAttributeValuesCache, Guid>) (r => r.projInfo.Guid)).Distinct<Guid>().OrderBy<Guid, Guid>((Func<Guid, Guid>) (i => i));
    for (int index = 0; index < list.Count; ++index)
    {
      long fieldInt64Value = list[index].GetFieldInt64Value(new AvsRowAttributeInfo(true, AvsIDCache.Attr_ArticleID), 0, (List<RelationAttributeValuesCache>) null, false);
      if (fieldInt64Value == -1L || fieldInt64Value == Math.Abs(sourceRow.ObjectId))
      {
        bool flag2 = Math.Abs(materialId) != Math.Abs(list[index].ObjectId);
        IOrderedEnumerable<Guid> second = list[index].Relations.Select<RelationAttributeValuesCache, Guid>((Func<RelationAttributeValuesCache, Guid>) (r => r.projInfo.Guid)).Distinct<Guid>().OrderBy<Guid, Guid>((Func<Guid, Guid>) (i => i));
        if (first.SequenceEqual<Guid>((IEnumerable<Guid>) second))
        {
          if (removeIfExists | flag2)
          {
            source.AddRange(sourceRow.Relations.Select<RelationAttributeValuesCache, ProductInfo>((Func<RelationAttributeValuesCache, ProductInfo>) (r => r.projInfo)));
            list[index].Remove();
            flag1 = false;
            avsRow = (AVSRow) null;
            break;
          }
          flag1 = true;
          avsRow = list[index];
          avsRow.SetLinkFromDraftToPart(sourceRow);
          break;
        }
        if (!this.IsFormA)
        {
          IEnumerable<Guid> missingGuids = first.Except<Guid>((IEnumerable<Guid>) second);
          if (this.IsFormB || this.IsFormV && source.Any<ProductInfo>((Func<ProductInfo, bool>) (mi => mi.IsVariableData)))
          {
            if (missingGuids.Any<Guid>())
            {
              source.Clear();
              source.AddRange(sourceRow.Relations.Select<RelationAttributeValuesCache, ProductInfo>((Func<RelationAttributeValuesCache, ProductInfo>) (r => r.projInfo)));
              if (removeIfExists)
              {
                list[index].Remove();
                avsRow = (AVSRow) null;
                break;
              }
              avsRow = list[index];
              break;
            }
            flag1 = true;
            avsRow = list[index];
            break;
          }
          source.AddRange(sourceRow.Relations.Select<RelationAttributeValuesCache, ProductInfo>((Func<RelationAttributeValuesCache, ProductInfo>) (r => r.projInfo)).Where<ProductInfo>((Func<ProductInfo, bool>) (pi => missingGuids.Any<Guid>((Func<Guid, bool>) (m => m == pi.Guid)))));
          break;
        }
      }
    }
    if (!flag1 && this.IsFormA)
      source.AddRange(sourceRow.Relations.Select<RelationAttributeValuesCache, ProductInfo>((Func<RelationAttributeValuesCache, ProductInfo>) (sr => sr.projInfo)));
    return (flag1, source, avsRow);
  }

  /// <summary>Назначить новое значение атрибута Материал в изделии</summary>
  /// <param name="partObjectId">Идентификатор объекта изделия</param>
  /// <param name="oldMaterialId">Идентификатор объекта материала</param>
  /// <param name="newMaterialData">Объект с данными о новом материале</param>
  private void SetNewMaterialForPart(long partObjectId, long oldMaterialId, object newMaterialData)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      long objectF_ID;
      AttributeValues newAttrValues = new AttributeValues(AvsIDCache.Attr_Material, (object) AVSDocument.GetObjectIDNavigatorData(sessionKeeper.Session, newMaterialData, out objectF_ID, out int _));
      long id = sessionKeeper.Session.GetObjectInfo(Math.Abs(oldMaterialId)).ID;
      if (!Intermech.Consts.IsUndefinedObjectId(oldMaterialId) && id == objectF_ID)
        return;
      IDBObject objectActual = sessionKeeper.Session.GetObjectActual(partObjectId, true);
      objectActual.SetAttributesValues(new AttributeValues[1]
      {
        newAttrValues
      });
      object initValue = (object) null;
      if (oldMaterialId != -1L)
        initValue = (object) oldMaterialId;
      if (AVSPlugin.NotificationService == null)
        return;
      AVSPlugin.NotificationService.FireEvent((object) this, (NotificationEventArgs) new DBObjectsExtendedEventArgs(objectActual.ObjectID, objectActual.ObjectType, new AttributeValues(AvsIDCache.Attr_Material, initValue), newAttrValues));
    }
  }

  /// <summary>Выбор строк из БД для добавления в спецификацию</summary>
  /// <param name="sections">Список разделов допустимые изделия которых можно выбирать</param>
  /// <param name="multiSelect">Использовать множественный выбор</param>
  /// <returns></returns>
  public ArrayList SelectDBObjectsForSections(
    IList<SpecificationSectionInfo> sections,
    bool multiSelect)
  {
    List<int> intList = new List<int>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      SpecificationSectionInfo.CacheSpecSections(sessionKeeper.Session);
    for (int index1 = 0; index1 < sections.Count; ++index1)
    {
      List<int> availableTypes = this.GetAvailableTypes(sections[index1]);
      for (int index2 = 0; index2 < availableTypes.Count; ++index2)
      {
        if (!MetaDataHelper.IsObjectTypeChildOf(availableTypes[index2], AvsIDCache.ObjType_Document) && !intList.Contains(availableTypes[index2]))
          intList.Add(availableTypes[index2]);
      }
    }
    DescriptorCollection descriptors = new DescriptorCollection();
    for (int index = 0; index < intList.Count; ++index)
      descriptors.Add((IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(intList[index]));
    object[] c = SelectionWindow.Select("Выберите объект", descriptors.Count != 1 ? (IDescriptor) new Intermech.Navigator.CustomNode.Descriptor("Допустимые типы объектов", descriptors) : descriptors[0], typeof (IDBTypedObjectID), (SelectionOptions) (8589938944L /*0x0200001100*/ | (!multiSelect ? 16777216L /*0x01000000*/ : 0L)));
    return c != null ? new ArrayList((ICollection) c) : new ArrayList();
  }

  /// <summary>Получить допустимые типы из разделов спецификации</summary>
  /// <param name="sectionInfo">Список разделов</param>
  /// <returns></returns>
  private List<int> GetAvailableTypes(SpecificationSectionInfo sectionInfo)
  {
    List<int> partTypes = new List<int>();
    if (!SpecificationSectionInfo.Cached)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        SpecificationSectionInfo.CacheSpecSections(sessionKeeper.Session);
    }
    if (sectionInfo != null)
    {
      AVSDocument.GetPartTypes(sectionInfo, partTypes);
    }
    else
    {
      List<SpecificationSectionInfo> documentSections = this.GetAllowableDocumentSections();
      for (int index = 0; index < documentSections.Count; ++index)
        AVSDocument.GetPartTypes(documentSections[index], partTypes);
    }
    return partTypes;
  }

  /// <summary> Создать заготовки для изделий с материалами-изделиями </summary>
  internal void RecreateDraftForParts()
  {
    if (this.ReadOnly)
      return;
    foreach (AVSRow allRow in this.GetAllRows(true, false))
    {
      if (allRow.HasPartAsMaterial(out long _) && !allRow.IsZagotovka())
        this.GenerateRowForZagotovka((DocumentTreeNode) allRow.DocNode, allRow, true);
    }
  }

  internal void UpdateCountsForDraftForParts()
  {
    if (this.ReadOnly)
      return;
    List<AVSRow> list = this.GetAllRows(true, false).Where<AVSRow>((Func<AVSRow, bool>) (r => r.IsZagotovka())).ToList<AVSRow>();
    if (list.Count == 0 || AvsConfig.General.UpdateCountValueForZagotovka == PerformActionModeEnum.Never)
      return;
    DialogResult dialogResult = AvsConfig.General.UpdateCountValueForZagotovka == PerformActionModeEnum.Auto ? DialogResult.Yes : DialogResult.None;
    foreach (AVSRow avsRow in list)
    {
      AVSRow partForDraft = avsRow.GetPartForDraft();
      if (!this.CheckDraftCountValue(avsRow, (List<SpecRowCheckMessage>) null))
      {
        if (dialogResult == DialogResult.None)
          dialogResult = MessageBox.Show("Обновить значения поля 'Количество' в записях заготовок?", "Обновление записей заготовок", MessageBoxButtons.YesNo);
        if (dialogResult == DialogResult.Yes)
        {
          if (partForDraft != null)
            this.SetCountValueForBlank(partForDraft, avsRow);
        }
        else
          this.AvsRowEventMessageViewer.AddEvent(avsRow, new AvsRowEventMessage(AVSEventType.SkipUpdateRowField, "Значение 'Количество' заготовки не соответствует детали")
          {
            AttrInfo = this.Field_Count
          });
      }
    }
  }

  /// <summary>Взять документ и исполнения на изменение</summary>
  /// <param name="session">Сессия</param>
  /// <param name="cancel">Отменить операцию</param>
  /// <returns>Вернет false, если есть объекты, которые взяты другим пользователем или нельзя взять</returns>
  protected override bool CheckOutObjects(out bool cancel)
  {
    cancel = false;
    List<long> objectIDs = new List<long>();
    List<long> newObjectIDs = new List<long>();
    string text1 = this.CheckSpecificationModifyMode();
    if (text1 != "")
    {
      if (MessageBox.Show(text1, "Внимание!", MessageBoxButtons.YesNo) != DialogResult.Yes)
        cancel = true;
      return false;
    }
    foreach (ProductInfo product in this.productsInfo)
    {
      string text2 = this.CheckProductModifyMode(product);
      if (text2 != "")
      {
        if (MessageBox.Show(text2, "Внимание!", MessageBoxButtons.YesNo) != DialogResult.Yes)
          cancel = true;
        return false;
      }
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index = 0; index < this.productsInfo.Count; ++index)
      {
        if (this.productsInfo[index].Id > 0L)
        {
          IDBObject dbObject1 = sessionKeeper.Session.GetObject(this.productsInfo[index].Id);
          if (dbObject1.ObjectModifyMode == ObjectModifyModes.Checkout)
          {
            objectIDs.Add(this.productsInfo[index].Id);
            IDBObject dbObject2 = dbObject1.CheckOut();
            this.productsInfo[index].Id = dbObject2.ObjectID;
            newObjectIDs.Add(this.productsInfo[index].Id);
          }
        }
      }
      if (this.DocumentID > 0L)
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(this.DocumentID);
        if (dbObject.ObjectModifyMode == ObjectModifyModes.Checkout)
        {
          objectIDs.Add(dbObject.ObjectID);
          this.DocumentID = dbObject.CheckOut().ObjectID;
          newObjectIDs.Add(this.DocumentID);
        }
      }
    }
    if (newObjectIDs.Count > 0)
      ((INotificationService) ServicesManager.GetService(typeof (INotificationService)))?.FireEvent((object) this, (NotificationEventArgs) new DBObjectsCheckOutEventArgs("ObjectsCheckedOut", (IList<long>) objectIDs, (IList<long>) newObjectIDs));
    return true;
  }

  internal override SpecificationSection FindSectionForNewRowInCommonData(
    LoadDataParams loadParams,
    long sectionId,
    RelationAttributeValuesCache relation)
  {
    return this.FindOrCreateSection(this.FindOwnerForSectionInCommonData(loadParams, relation), sectionId);
  }

  private Chapter FindOwnerForAdditionalChapter(Chapter productChapter)
  {
    if (this.AvsDocumentForm == AVSDocumentForm.Single || this.IsFormB)
      return this.commonDataChapter;
    return this.AdditionalChaptersInDataChapter ? productChapter : (Chapter) null;
  }

  private AdditionalChapter FindOrCreateAdditionalChapterForRelationFromDbRecord(
    RelationAttributeValuesCache relation,
    Chapter productChapter)
  {
    if (relation == null)
      return (AdditionalChapter) null;
    long valueInt64 = relation.GetValueInt64(this.Attr_AdditionalChapter, false);
    if (Intermech.Consts.IsUndefinedObjectId(valueInt64))
      return (AdditionalChapter) null;
    AdditionalChapterSettings additionalChapterSettings = this.AVSCommonPropertiesSchema.FindAdditionalChapterSettings(valueInt64);
    if (additionalChapterSettings == null)
      return (AdditionalChapter) null;
    Chapter additionalChapter = this.FindOwnerForAdditionalChapter(productChapter);
    if (additionalChapter == null)
    {
      if (!(this.GetChapter(additionalChapterSettings.ChapterGuid) is AdditionalChapter relationFromDbRecord2))
        this.AddRootChapter((Chapter) (relationFromDbRecord2 = new AdditionalChapter((AVSDocument) this, additionalChapterSettings, this.AdditionalChaptersInDataChapter)), true);
    }
    else if (!(additionalChapter.GetChapter(additionalChapterSettings.ChapterGuid) is AdditionalChapter relationFromDbRecord2))
      additionalChapter.AddChapter((Chapter) (relationFromDbRecord2 = new AdditionalChapter((AVSDocument) this, additionalChapterSettings, this.AdditionalChaptersInDataChapter)), true, true, this.IsGridViewMode, (TableData) null);
    return relationFromDbRecord2;
  }

  private Chapter FindOwnerForSectionInCommonData(
    LoadDataParams loadParams,
    RelationAttributeValuesCache relation)
  {
    Chapter sectionInCommonData = this.GetSectionOwnerFromContextInCommonData(relation, loadParams.Context);
    if (relation != null && (sectionInCommonData == null || relation.GetValueInt64(this.Attr_AdditionalChapter, false).IsDefinedId()))
    {
      AdditionalChapter relationFromDbRecord = this.FindOrCreateAdditionalChapterForRelationFromDbRecord(relation, this.commonDataChapter);
      sectionInCommonData = relationFromDbRecord == null ? this.commonDataChapter : (!this.AdditionalChaptersInDataChapter ? relationFromDbRecord.InnerCommonDataChapter : (Chapter) relationFromDbRecord);
    }
    return sectionInCommonData;
  }

  private Chapter GetSectionOwnerFromContextInCommonData(
    RelationAttributeValuesCache relation,
    AVSDocumentContext context)
  {
    Chapter contextInCommonData = (Chapter) null;
    if (context.Chapter != null)
    {
      if (context.Chapter.IsSectionOwner)
        contextInCommonData = context.Chapter;
      else if (this.AvsDocumentForm == AVSDocumentForm.V && context.Chapter.IsFormB)
      {
        if (context.Chapter.IsAdditionalChapter && context.AdditionalChapter != null)
          contextInCommonData = (Chapter) context.AdditionalChapter.InnerVariableData_FormV;
        if (contextInCommonData == null)
          contextInCommonData = (Chapter) this.variableDataChapter_FormV;
      }
      else if (context.Chapter.IsAdditionalChapter && context.AdditionalChapter != null)
        contextInCommonData = context.AdditionalChapter.InnerCommonDataChapter;
    }
    else if (this.AvsDocumentForm == AVSDocumentForm.V && context.Product != null && context.Product.IsVariableData)
      contextInCommonData = (Chapter) this.variableDataChapter_FormV;
    if (contextInCommonData == null && (relation == null || relation.GetValueInt64(this.Attr_AdditionalChapter, false).IsUndefinedId()))
      contextInCommonData = this.commonDataChapter;
    return contextInCommonData;
  }

  internal override SpecificationSection FindSectionForNewRowInNextProduct(
    LoadDataParams loadParams,
    long sectionId,
    RelationAttributeValuesCache relation)
  {
    return this.FindOrCreateSection(this.FindOwnerForSectionInNextProduct(loadParams, relation), sectionId);
  }

  private Chapter FindOwnerForSectionInNextProduct(
    LoadDataParams loadParams,
    RelationAttributeValuesCache relation)
  {
    Chapter sectionInNextProduct = this.GetNewRowSectionOwnerInProductFromContext(loadParams.Context);
    if (sectionInNextProduct == null)
    {
      Chapter defaultProductChapter = this.GetNewRowDefaultProductChapter(loadParams);
      AdditionalChapter relationFromDbRecord = this.FindOrCreateAdditionalChapterForRelationFromDbRecord(relation, defaultProductChapter);
      sectionInNextProduct = relationFromDbRecord == null ? defaultProductChapter : this.GetSectionOwnerForProductInAdditionalChapter(loadParams, relationFromDbRecord);
    }
    return sectionInNextProduct;
  }

  private Chapter GetSectionOwnerForProductInAdditionalChapter(
    LoadDataParams loadParams,
    AdditionalChapter additionalChapter)
  {
    return this.AdditionalChaptersInDataChapter ? (Chapter) additionalChapter : (this.AvsDocumentForm != AVSDocumentForm.V ? (this.AvsDocumentForm != AVSDocumentForm.A ? additionalChapter.InnerCommonDataChapter : additionalChapter.InnerVariableData_FormA.GetChapter(loadParams.Context.Product.Id)) : (Chapter) additionalChapter.InnerVariableData_FormV);
  }

  private Chapter GetNewRowSectionOwnerInProductFromContext(AVSDocumentContext context)
  {
    if (context.Chapter == null)
      return (Chapter) null;
    Chapter productFromContext = (Chapter) null;
    if (this.AvsDocumentForm == AVSDocumentForm.V)
    {
      if (context.Chapter.IsFormB && context.Chapter.IsSectionOwner)
        productFromContext = context.Chapter;
      if (productFromContext == null && context.AdditionalChapter != null)
        productFromContext = (Chapter) context.AdditionalChapter.InnerVariableData_FormV;
    }
    else if (this.AvsDocumentForm == AVSDocumentForm.A)
    {
      if (context.Chapter.IsSectionOwner && context.Chapter.Product.Id == context.Product.Id)
        productFromContext = context.Chapter;
      if (productFromContext == null && context.AdditionalChapter != null)
      {
        VariableDataChapterFormA variableDataFormA = context.AdditionalChapter.InnerVariableData_FormA;
        if (variableDataFormA != null)
          productFromContext = variableDataFormA.GetChapter(context.Product.Id);
      }
    }
    else
    {
      if (context.Chapter.IsSectionOwner)
        productFromContext = context.Chapter;
      if (productFromContext == null && context.AdditionalChapter != null)
        productFromContext = context.AdditionalChapter.InnerCommonDataChapter;
    }
    return productFromContext;
  }

  protected override void UpdateDraftForParts()
  {
    if (this._processingUpdateDraftForParts)
      return;
    this._processingUpdateDraftForParts = true;
    try
    {
      if (AvsConfig.General.UpdateCountValueForZagotovka != PerformActionModeEnum.Never)
        this.UpdateCountsForDraftForParts();
      if (!AvsConfig.General.AutoCreateBlank)
        return;
      this.RecreateDraftForParts();
    }
    finally
    {
      this._processingUpdateDraftForParts = false;
    }
  }

  internal override void LoadSourceProductByRelations()
  {
  }

  /// <summary>Проверить документ на ошибки</summary>
  /// <param name="checkType">Тип проверки</param>
  /// <param name="avsRows">Список записей для проверки. Если null, то проверяются все записи документа</param>
  /// <param name="errorRows">Списки записей с соответствующими ошибками</param>
  /// <returns>Возвращает true, если ошибок не найдено</returns>
  public override void CheckErrorsInRows(
    AVSCheckType checkType,
    AVSCheckMode checkMode,
    ICollection<AVSRow> avsRows,
    Dictionary<AVSRow, List<SpecRowCheckMessage>> errorRows)
  {
    base.CheckErrorsInRows(checkType, checkMode, avsRows, errorRows);
    if (checkType == AVSCheckType.None)
      return;
    if (avsRows == null)
      avsRows = (ICollection<AVSRow>) this.GetAllRows(false, true);
    if (avsRows.Count == 0)
      return;
    if (this.DocumentControl != null)
    {
      CancelEventArgs cancelArgs = new CancelEventArgs();
      this.DocumentControl.EditorValidating(cancelArgs);
      if (cancelArgs.Cancel)
        return;
    }
    if ((checkType & AVSCheckType.CheckDuplicatePositionDesignation) != AVSCheckType.None)
      this.CheckPositionDesignationErrors(errorRows);
    if ((checkType & AVSCheckType.DuplicatePosition) != AVSCheckType.None)
      this.CheckPositionsErrors(errorRows);
    bool flag1 = (checkType & AVSCheckType.MassCalc) != 0;
    if (flag1)
      this.LoadNewAttributes(this.CreateAttributeListForMassaCalc(), true);
    long num1 = -1;
    if ((checkType & (AVSCheckType.EmptyPosition | AVSCheckType.MassCalc)) != AVSCheckType.None && SpecificationSectionInfo.SectionDictionaryByGuid[(object) new Guid("cad0025d-306c-11d8-b4e9-00304f19f545")] is SpecificationSectionInfo specificationSectionInfo)
      num1 = specificationSectionInfo.SectionID;
    bool flag2 = this.IsFormB && (checkType & AVSCheckType.EmptyCountAllProdFormB) != 0;
    bool flag3 = (checkType & AVSCheckType.MissingOutputMappingForNote) != 0;
    AvsRowAttributeInfo rowAttributeInfo1 = new AvsRowAttributeInfo(false, AvsIDCache.Attr_Weight);
    AvsRowAttributeInfo rowAttributeInfo2 = new AvsRowAttributeInfo(false, AvsIDCache.Attr_UnitWeight);
    AvsRowAttributeInfo rowAttributeInfo3 = new AvsRowAttributeInfo(false, AvsIDCache.Attr_Size);
    List<SpecRowCheckMessage> specRowCheckMessageList = new List<SpecRowCheckMessage>();
    foreach (AVSRow avsRow in avsRows.Where<AVSRow>((Func<AVSRow, bool>) (r => !r.IsHiddenRow)))
    {
      bool flag4 = (checkType & (AVSCheckType.EmptyCount | AVSCheckType.MassCalc)) != 0;
      bool flag5 = !errorRows.TryGetValue(avsRow, out specRowCheckMessageList);
      if (flag5)
        specRowCheckMessageList = new List<SpecRowCheckMessage>();
      if (flag1 && !avsRow.IsDocRelation)
      {
        bool flag6 = true;
        mValue = (MeasuredValue) null;
        MeasureDescriptor measureDescriptor = (MeasureDescriptor) null;
        bool flag7 = false;
        double unitMass = 0.0;
        bool flag8 = false;
        double num2 = 0.0;
        AvsRowAttributeInfo attrInfo1 = new AvsRowAttributeInfo(false, AvsIDCache.Attr_Weight);
        AvsRowAttributeInfo rowAttributeInfo4 = new AvsRowAttributeInfo(false, AvsIDCache.Attr_UnitWeight);
        AvsRowAttributeInfo attrInfo2 = new AvsRowAttributeInfo(false, AvsIDCache.Attr_Size);
        for (int index = 0; index < this.productsInfo.Count; ++index)
        {
          int relationIndexForProduct = avsRow.GetRelationIndexForProduct(this.productsInfo[index].Id);
          if (relationIndexForProduct != -1)
          {
            bool flag9 = false;
            if (flag6 || avsRow.IsFormB)
            {
              flag6 = false;
              object fieldValue = avsRow.GetFieldValue(this.Field_Count, relationIndexForProduct, index, false, false);
              mValue = (MeasuredValue) null;
              switch (fieldValue)
              {
                case null:
                case DBNull _:
                  continue;
                case MeasuredValue mValue:
label_27:
                  if (mValue != null)
                  {
                    double num3 = mValue.Value;
                  }
                  if (mValue != null)
                  {
                    measureDescriptor = MeasureHelper.FindDescriptor(mValue);
                    break;
                  }
                  break;
                default:
                  mValue = AVSRow.ConvertCountToMeasuredValue(fieldValue);
                  goto label_27;
              }
            }
            if (mValue != null && mValue.MeasureID != AVSRow.DefaultCountID && measureDescriptor != null)
            {
              if (measureDescriptor.PhysicalQuantityGuid == SystemGUIDs.objectMassGuid)
              {
                double num4 = MeasureHelper.ConvertToBaseMeasure(mValue).Value;
                flag9 = true;
              }
              else if (measureDescriptor.PhysicalQuantityGuid == SystemGUIDs.objectLengthGuid || measureDescriptor.PhysicalQuantityGuid == SystemGUIDs.objectSquareGuid || measureDescriptor.PhysicalQuantityGuid == SystemGUIDs.objectVolumeGuid)
              {
                if (!flag7)
                {
                  flag7 = true;
                  if (!this.GetUnitMass(avsRow, out unitMass, specRowCheckMessageList))
                  {
                    unitMass = 0.0;
                    continue;
                  }
                }
                else if (unitMass == 0.0)
                  continue;
                flag9 = true;
                double num5 = MeasureHelper.ConvertToBaseMeasure(mValue).Value;
              }
              else if (measureDescriptor.PhysicalQuantityGuid == SystemGUIDs.objectQuantityGuid)
              {
                double num6 = MeasureHelper.ConvertToBaseMeasure(mValue).Value;
              }
            }
            if (!flag9)
            {
              if (!flag8)
              {
                flag8 = true;
                num2 = 0.0;
                object fieldValue = avsRow.GetFieldValue(attrInfo1, 0, -1, true, false);
                if (fieldValue == null)
                {
                  string fieldStringValue = avsRow.GetFieldStringValue(attrInfo2, 0, -1, (List<RelationAttributeValuesCache>) null, false);
                  if (fieldStringValue == null || fieldStringValue == "")
                  {
                    specRowCheckMessageList.Add(new SpecRowCheckMessage(AVSCheckType.MassCalc, "Невозможно рассчитать массу, так как нет значения у атрибута \"Масса\" и не заданы размеры для расчёта массы изделия"));
                  }
                  else
                  {
                    double result = 0.0;
                    if (!this.GetSizeKoef(fieldStringValue, out result))
                      specRowCheckMessageList.Add(new SpecRowCheckMessage(AVSCheckType.MassCalc, "Невозможно рассчитать массу, так как неправильно задано значение атрибута \"Размеры\""));
                    if (!flag7)
                    {
                      flag7 = true;
                      if (!this.GetUnitMass(avsRow, out unitMass, specRowCheckMessageList))
                      {
                        unitMass = 0.0;
                        continue;
                      }
                    }
                    else if (unitMass == 0.0)
                      continue;
                    num2 = result * unitMass;
                  }
                }
                else
                {
                  if (!(fieldValue is MeasuredValue measuredValue))
                    measuredValue = MeasureHelper.ConvertToMeasuredValue(fieldValue.ToString(), "", false);
                  if (measuredValue == null)
                    specRowCheckMessageList.Add(new SpecRowCheckMessage(AVSCheckType.MassCalc, "Невозможно рассчитать массу, так как некорректно задано значение атрибута \"Масса\""));
                  else
                    num2 = MeasureHelper.ConvertToBaseMeasure(measuredValue).Value;
                }
              }
            }
          }
        }
      }
      if (avsRow.Relations == null || avsRow.Relations.Count == 0)
      {
        if ((checkType & (AVSCheckType.ObjectWithoutRelation | AVSCheckType.MassCalc)) != AVSCheckType.None && (avsRow.RelType != AvsIDCache.Relation_AddComplect || checkMode == AVSCheckMode.ChangeForm))
          specRowCheckMessageList.Add(new SpecRowCheckMessage(AVSCheckType.ObjectWithoutRelation, (string) null));
      }
      else if (!avsRow.IsDocRelation)
      {
        bool flag10 = (checkType & (AVSCheckType.EmptyPosition | AVSCheckType.MassCalc | AVSCheckType.NotNumberPosition)) != AVSCheckType.None && avsRow.SectionID != num1;
        for (int relationIndex = 0; relationIndex < avsRow.Relations.Count && flag4 | flag10; ++relationIndex)
        {
          if (flag4 && avsRow.GetFieldValue(this.Field_Count, relationIndex, -1, true, false) == null && (avsRow.RelType != AvsIDCache.Relation_AddComplect || checkMode == AVSCheckMode.ChangeForm))
          {
            specRowCheckMessageList.Add(new SpecRowCheckMessage(AVSCheckType.EmptyCount, (string) null));
            flag4 = false;
          }
          if (flag10)
          {
            string fieldStringValue = avsRow.GetFieldStringValue(this.Field_Position, relationIndex, -1, (List<RelationAttributeValuesCache>) null, false);
            if (checkType.HasFlag((Enum) AVSCheckType.EmptyPosition) && string.IsNullOrEmpty(fieldStringValue) && avsRow.RelType != AvsIDCache.Relation_Podbor)
            {
              specRowCheckMessageList.Add(new SpecRowCheckMessage(AVSCheckType.EmptyPosition, (string) null));
              flag10 = false;
            }
            if (checkType.HasFlag((Enum) AVSCheckType.NotNumberPosition))
            {
              int result = 0;
              if (!string.IsNullOrEmpty(fieldStringValue) && !int.TryParse(fieldStringValue, out result) && avsRow.RelType != AvsIDCache.Relation_Zagotovka)
              {
                specRowCheckMessageList.Add(new SpecRowCheckMessage(AVSCheckType.NotNumberPosition, (string) null));
                flag10 = false;
              }
            }
          }
        }
      }
      if (flag2 && !avsRow.IsNoteRow && !avsRow.IsDocObject)
      {
        if (!this.HasCountForAnyProduct(avsRow) && (avsRow.RelType != AvsIDCache.Relation_AddComplect || checkMode == AVSCheckMode.ChangeForm))
          specRowCheckMessageList.Add(new SpecRowCheckMessage(AVSCheckType.EmptyCountAllProdFormB, "Объект, у которого количество отсутствует во всех исполнениях, будет отсутствовать в дереве состава."));
        if (!this.AllProductMeasuresMatchNote(avsRow))
          specRowCheckMessageList.Add(new SpecRowCheckMessage(AVSCheckType.EmptyCountAllProdFormB, "Единица измерения количества для исполнения отличается от единицы измерения в примечании."));
      }
      if ((checkType & AVSCheckType.PartWithoutDraft) != AVSCheckType.None && !avsRow.IsZagotovka())
        this.CheckPartWithoutZagotovka(avsRow, specRowCheckMessageList);
      if ((checkType & AVSCheckType.DraftCountDoesntMatch) != AVSCheckType.None && avsRow.IsZagotovka())
        this.CheckDraftCountValue(avsRow, specRowCheckMessageList);
      if (flag3)
      {
        if (avsRow.GetCellAttributeMapping(AVSRow.DocAttr_Note) == null && specRowCheckMessageList != null)
        {
          // ISSUE: explicit non-virtual call
          __nonvirtual (specRowCheckMessageList.Add(new SpecRowCheckMessage(AVSCheckType.MissingOutputMappingForNote, (string) null)));
        }
        flag3 = false;
      }
      if (flag5 && specRowCheckMessageList.Count > 0)
        errorRows.Add(avsRow, specRowCheckMessageList);
    }
  }

  /// <summary> Идентификатор объекта "Шаблон титульного листа СП" </summary>
  public static long ObjID_StdTemplateSpecificationTitlePage
  {
    [DebuggerStepThrough] get
    {
      if (AvsIDCache.ObjID_StdTemplateSpecificationTitlePage_ != -1L)
        return AvsIDCache.ObjID_StdTemplateSpecificationTitlePage_;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        return AvsIDCache.GetStdTemplateSpecificationTitlePageId(sessionKeeper.Session);
    }
  }
}
