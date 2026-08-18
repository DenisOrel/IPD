// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Chapter
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using DevExpress.IM.XtraTreeList;
using DevExpress.IM.XtraTreeList.Nodes;
using Infralution.Controls.VirtualTree;
using Intermech.AVS.GridColumns.VirtualTreeList;
using Intermech.AVS.HelperClasses;
using Intermech.Document.Client;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Document;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

#nullable disable
namespace Intermech.AVS;

/// <summary>Обобщённый подраздел конструкторского документа</summary>
public class Chapter : 
  IEnumerable<AVSRow>,
  IEnumerable,
  IComparable,
  IVirtualTreeItem,
  ICustomTypeDescriptor
{
  /// <summary>Имя ячейки для текста примечания в строке примечания</summary>
  public static string NoteRowTextCellName = "Текст примечания";
  /// <summary>Имя атрибута узла документа для сохранения типа узла в спецификации</summary>
  public static string DocNodeType_AttributeName = "AVSNodeType";
  /// <summary>Имя атрибута узла документа для сохранения обозначения</summary>
  public static string Designation_AttributeName = "Designation";
  /// <summary>Имя атрибута узла документа для сохранения заголовка со знаками подстановки</summary>
  public static string CaptionFormat_AttributeName = "CaptionFormat";
  /// <summary>Имя атрибута узла документа для сохранения Guid части</summary>
  public static string ChapterGuid_AttributeName = nameof (ChapterGuid);
  /// <summary>Имя атрибута узла документа для хранения экспортного заголовка</summary>
  public static string ExportCaption_AttributeName = "AVSExportCaption";
  /// <summary>Значение атрибута документа AVSNodeType для раздела спецификации</summary>
  public static string Section_TypeName = "Section";
  /// <summary>Значение атрибута документа AVSNodeType для группы записей</summary>
  public static string Group_TypeName = "Group";
  /// <summary>Значение атрибута документа AVSNodeType для доп комплектов</summary>
  public static string AdditionalComplectGroup_TypeName = "AdditionalComplectGroup";
  /// <summary>Значение атрибута документа AVSNodeType для общих данных и блока исполнений формы Б</summary>
  public static string CommonData_TypeName = "CommonData";
  /// <summary>Значение атрибута документа AVSNodeType для переменных данных</summary>
  public static string VariableData_TypeName = "VariableData";
  /// <summary>Значение атрибута документа AVSNodeType для дополнительных частей типа "Устанавливается по МЭ"</summary>
  public static string AdditionalChapter_TypeName = "AdditionalChapter";
  /// <summary>Значение атрибута документа AVSNodeType для переменных данных исполнения</summary>
  public static string ProductVariableData_TypeName = "ProductVariableData";
  /// <summary>Значение атрибута документа AVSNodeType для содержания переменных данных формы В</summary>
  public static string ProductPageLinks_TypeName = "ProductPageLinks";
  /// <summary>Значение атрибута документа AVSNodeType для записи спецификации</summary>
  public static string AVSRow_TypeName = "DocRow";
  /// <summary>Значение атрибута документа AVSNodeType для строки примечания</summary>
  public static string SpecNote_TypeName = "NoteRow";
  /// <summary>Значение атрибута документа AVSNodeType для записи листа регистрации изменений</summary>
  public static string LRIRow_TypeName = "LRIRow";
  /// <summary>Идентификатор заголовка исполнения в документе</summary>
  public static string ProductCaptionRowID = "Заголовок исполнения";
  /// <summary>Имя атрибута узла документа для сохранения информации о ProductInfo</summary>
  public static string ProductVariableData_AttributeName = "ProductVariable_Data";
  private TableData docNodeExp;
  /// <summary>Родитель в табличном виде</summary>
  private IVirtualTreeItem parentTreeItem;
  protected AVSDocument avsDocument;
  protected List<Chapter> chapters = new List<Chapter>();
  private TableData docNode;
  protected List<TableData> docNodes = new List<TableData>();
  private List<TableData> docNodesExp = new List<TableData>();
  private List<DocNodesBlock> docNodesBlocks;
  protected string nodeLevel;
  protected TreeListNode listNode;
  /// <summary>Глобальный идентификатор версии объекта подраздела</summary>
  public Guid ChapterGuid = Guid.Empty;
  /// <summary>Идентификатор версии объекта подраздела</summary>
  public long ChapterID = -1;
  /// <summary>Тип объекта подраздела</summary>
  public int ChapterType = -1;
  /// <summary>Заголовок подраздела</summary>
  internal string caption = "";
  /// <summary>Заголовок подраздела экспортной части документа</summary>
  internal string captionExp;
  /// <summary>Индекс сортировки</summary>
  private long sortIndex;
  protected Chapter parent;
  protected ProductInfo product;
  protected Dictionary<long, Chapter> chaptersIdDictionary = new Dictionary<long, Chapter>();
  protected Dictionary<Guid, Chapter> chaptersGuidDictionary = new Dictionary<Guid, Chapter>();
  protected bool isSectionOwner;

  /// <summary>Конструктор</summary>
  /// <param name="avsDocument">Спецификация владелец подраздела</param>
  /// <param name="chapterGuid">Глобальный идентификатор версии объекта подраздела</param>
  /// <param name="chapterID">Идентификатор версии объекта подраздела</param>
  /// <param name="chapterType">Тип объекта подраздела</param>
  /// <param name="caption">Заголовок подраздела</param>
  /// <param name="sortIndex">Индекс сортировки</param>
  /// <param name="isSectionOwner">Является владелец разделов спецификации</param>
  public Chapter(
    AVSDocument avsDocument,
    Guid chapterGuid,
    long chapterID,
    int chapterType,
    string caption,
    long sortIndex,
    bool isSectionOwner)
  {
    this.avsDocument = avsDocument;
    this.ChapterGuid = chapterGuid;
    this.ChapterID = chapterID;
    this.ChapterType = chapterType;
    this.caption = caption;
    this.sortIndex = sortIndex;
    this.isSectionOwner = isSectionOwner;
  }

  /// <summary>Конструктор</summary>
  /// <param name="avsDocument">Спецификация владелец подраздела</param>
  /// <param name="isSectionOwner">Является владельцем разделов спецификации</param>
  public Chapter(AVSDocument avsDocument, bool isSectionOwner)
  {
    this.avsDocument = avsDocument;
    this.isSectionOwner = isSectionOwner;
  }

  /// <summary>Конструктор</summary>
  protected Chapter()
  {
  }

  /// <summary>Узел документа представляющий этот раздел</summary>
  public TableData DocNode
  {
    [DebuggerStepThrough] get => this.docNode;
    set
    {
      if (this.docNode == value)
        return;
      if (value == null)
        this.DocNodes = new List<TableData>();
      else
        this.DocNodes = new List<TableData>() { value };
    }
  }

  /// <summary>Есть ли узлы в этой части</summary>
  internal bool HasDocNodes => this.docNodes != null && this.docNodes.Count > 0;

  /// <summary>Узлы документа представляющие этот раздел</summary>
  public virtual List<TableData> DocNodes
  {
    [DebuggerStepThrough] get => this.docNodes;
    set
    {
      if (this.docNodes == value)
        return;
      if (this.HasDocNodes)
      {
        for (int index = 0; index < this.docNodes.Count; ++index)
          this.docNodes[index].Tag = (object) null;
      }
      this.docNodes = value;
      if (this.HasDocNodes)
      {
        for (int index = 0; index < this.docNodes.Count; ++index)
          this.ConnectDocNode(this.docNodes[index], false);
        this.docNode = this.docNodes[0];
      }
      else
        this.docNode = (TableData) null;
    }
  }

  /// <summary>Есть ли узлы экспортной части СП в этой части</summary>
  internal bool HasDocNodesExp => this.docNodesExp != null && this.docNodesExp.Count > 0;

  /// <summary>Узел документа представляющий этот раздел в экспортной СП</summary>
  public virtual TableData DocNodeExp
  {
    [DebuggerStepThrough] get => this.docNodeExp;
    set
    {
      if (this.docNodeExp == value)
        return;
      if (this.docNodeExp != null)
        this.docNodeExp.Tag = (object) null;
      this.docNodeExp = value;
      if (this.docNodeExp == null)
        return;
      this.ConnectDocNode(this.docNodeExp, true);
    }
  }

  /// <summary>Узел документа представляющий этот раздел в экспортной СП</summary>
  public virtual List<TableData> DocNodesExp
  {
    [DebuggerStepThrough] get => this.docNodesExp;
    set
    {
      if (this.docNodesExp == value)
        return;
      if (this.docNodesExp != null)
      {
        for (int index = 0; index < this.docNodesExp.Count; ++index)
          this.docNodesExp[index].Tag = (object) null;
      }
      this.docNodesExp = value;
      if (this.docNodesExp == null)
        return;
      for (int index = 0; index < this.docNodesExp.Count; ++index)
        this.ConnectDocNode(this.docNodesExp[index], true);
    }
  }

  /// <summary>Есть ли блоки узлов документов для экспортного варианта</summary>
  internal bool HasDocNodesBlocks => this.docNodesBlocks != null && this.docNodesBlocks.Count > 0;

  /// <summary>Узлы документа представляющий этот раздел в нормальной и экспортной частях</summary>
  internal virtual List<DocNodesBlock> DocNodesBlocks
  {
    [DebuggerStepThrough] get => this.docNodesBlocks;
    set
    {
      if (this.docNodesBlocks == value)
        return;
      this.docNodesBlocks = value;
    }
  }

  AttributeCollection ICustomTypeDescriptor.GetAttributes()
  {
    return TypeDescriptor.GetAttributes((object) this, true);
  }

  string ICustomTypeDescriptor.GetClassName() => TypeDescriptor.GetClassName((object) this, true);

  string ICustomTypeDescriptor.GetComponentName()
  {
    return TypeDescriptor.GetComponentName((object) this, true);
  }

  TypeConverter ICustomTypeDescriptor.GetConverter()
  {
    return TypeDescriptor.GetConverter((object) this, true);
  }

  EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
  {
    return TypeDescriptor.GetDefaultEvent((object) this, true);
  }

  PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
  {
    return TypeDescriptor.GetDefaultProperty((object) this, true);
  }

  object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
  {
    return TypeDescriptor.GetEditor((object) this, editorBaseType, true);
  }

  EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
  {
    return TypeDescriptor.GetEvents((object) this, true);
  }

  EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
  {
    return TypeDescriptor.GetEvents((object) this, attributes, true);
  }

  PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
  {
    AttributeCollection attributes1 = ((ICustomTypeDescriptor) this).GetAttributes();
    if (attributes1 == null || attributes1.Count <= 0)
      return this.GetProperties(new Attribute[0]);
    Attribute[] attributes2 = new Attribute[attributes1.Count];
    attributes1.CopyTo((Array) attributes2, 0);
    return this.GetProperties(attributes2);
  }

  PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
  {
    return this.GetProperties(attributes);
  }

  /// <summary>Получить дескрипторы для свойств</summary>
  /// <param name="attributes">Атрибуты свойств</param>
  /// <returns>Коллекция дескрипторов свойств</returns>
  protected virtual PropertyDescriptorCollection GetProperties(Attribute[] attributes)
  {
    PropertyDescriptorCollection properties1 = TypeDescriptor.GetProperties((object) this, attributes, true);
    HybridDictionary hybridDictionary = new HybridDictionary(200);
    foreach (PropertyDescriptor PropDesc in properties1)
    {
      if (!(PropDesc is CustomPropertyDescriptor propertyDescriptor))
      {
        propertyDescriptor = new CustomPropertyDescriptor(PropDesc);
        if (this.avsDocument.ReadOnly)
          propertyDescriptor.SetIsReadOnly(true);
      }
      hybridDictionary.Add((object) propertyDescriptor.Name, (object) propertyDescriptor);
    }
    PropertyDescriptorCollection properties2 = new PropertyDescriptorCollection((PropertyDescriptor[]) null);
    foreach (DictionaryEntry dictionaryEntry in hybridDictionary)
      properties2.Add((PropertyDescriptor) dictionaryEntry.Value);
    return properties2;
  }

  object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd) => (object) this;

  /// <summary>Для внутреннего использования. Добавить в список узлов документа ещё один узел</summary>
  /// <param name="newDocNode">Новый узел документа</param>
  /// <param name="isExportTable">Узел экспортного документа</param>
  /// <returns>Индекс добавленного узла</returns>
  internal int AddDocNode(TableData newDocNode, bool isExportTable = false)
  {
    if (newDocNode == null)
      return -1;
    List<TableData> tableDataList = isExportTable ? this.docNodesExp : this.docNodes;
    if (tableDataList == null)
    {
      tableDataList = new List<TableData>();
      if (isExportTable)
        this.docNodesExp = tableDataList;
      else
        this.docNodes = tableDataList;
    }
    else
    {
      int num = tableDataList.IndexOf(newDocNode);
      if (num != -1)
        return num;
    }
    tableDataList.Add(newDocNode);
    int num1 = tableDataList.Count - 1;
    this.ConnectDocNode(newDocNode, isExportTable);
    if (!isExportTable)
      this.docNode = this.docNodes[0];
    this.OnDocNodeAdded(newDocNode);
    return num1;
  }

  /// <summary>Для внутреннего использования. Добавить в список узлов документа ещё один узел</summary>
  /// <param name="newDocNode">Новый узел документа</param>
  /// <param name="newDocNodeExp">Узел экспортного документа</param>
  /// <returns>Индекс добавленного узла</returns>
  internal int AddDocNodeBlock(TableData newDocNode, TableData newDocNodeExp = null)
  {
    if (newDocNode == null && newDocNodeExp == null)
      return -1;
    if (this.docNodesBlocks == null)
      this.docNodesBlocks = new List<DocNodesBlock>();
    this.docNodesBlocks.Add(new DocNodesBlock(newDocNode, newDocNodeExp));
    return this.docNodesBlocks.Count - 1;
  }

  /// <summary>Связать новый узел с разделом</summary>
  /// <param name="docNode">Новый узел документа</param>
  /// <param name="isExportTable">Узел экспортного документа</param>
  protected virtual void ConnectDocNode(TableData docNode, bool isExportTable)
  {
    if (docNode == null)
      throw new ArgumentNullException(nameof (docNode));
    if (!this.UseParentDocNode)
    {
      docNode.Tag = (object) this;
      if (this.nodeLevel != null)
        docNode.SetAttributeValue(Chapter.DocNodeType_AttributeName, this.nodeLevel, false, false, false);
      else
        docNode.RemoveAttribute(Chapter.DocNodeType_AttributeName, false, false);
      if (this.FromNewPage.HasValue)
        docNode.SetFromNewPage(this.FromNewPage.Value, false, false);
    }
    if (!isExportTable)
      return;
    docNode.SetAttributeValue(Chapter.ExportCaption_AttributeName, this.captionExp, false);
  }

  /// <summary>Связать новый узел с разделом</summary>
  /// <param name="docNode">Новый узел документа</param>
  /// <param name="isExportTable">Узел экспортного документа</param>
  protected virtual void DisconnectDocNode(TableData docNode, bool isExportTable)
  {
    docNode.Tag = (object) null;
  }

  /// <summary>Обработчик события при добавлении узла документа</summary>
  /// <param name="newDocNode">Новый узел документа</param>
  protected virtual void OnDocNodeAdded(TableData newDocNode)
  {
  }

  /// <summary>Сохранить информацию о исполнении в документе</summary>
  internal void SaveProductInfoToDocNode()
  {
    if (this.Product == null || this.DocNode == null)
      return;
    string attributeValue = this.Product.Serialize();
    if (!(attributeValue != ""))
      return;
    this.DocNode.SetAttributeValue(Chapter.ProductVariableData_AttributeName, attributeValue);
  }

  /// <summary>Узел TreeList представляющий этот раздел</summary>
  public TreeListNode ListNode
  {
    [DebuggerStepThrough] get => this.listNode;
    set
    {
      if (this.listNode == value)
        return;
      if (this.listNode != null)
        this.listNode.Tag = (object) null;
      this.listNode = value;
      if (this.listNode == null)
        return;
      this.listNode.Tag = (object) this;
    }
  }

  /// <summary>Спецификация, в которой находится раздел</summary>
  public AVSDocument AVSDocument
  {
    [DebuggerStepThrough] get => this.avsDocument;
    set => this.avsDocument = value;
  }

  /// <summary>Родитель</summary>
  public virtual Chapter Parent
  {
    [DebuggerStepThrough] get => this.parent;
    set => this.parent = value;
  }

  /// <summary>Исполнение которое соответствует разделу</summary>
  public virtual ProductInfo Product
  {
    [DebuggerStepThrough] get
    {
      if (this.product != null)
        return this.product;
      for (Chapter parent = this.parent; parent != null; parent = parent.Parent)
      {
        if (parent.product != null)
          return parent.product;
      }
      return (ProductInfo) null;
    }
    set => this.product = value;
  }

  /// <summary>Получить часть верхнего уровня к которой принадлежит подраздел. Если это общая часть, то возвращает её (CommonDataChapter или VariableDataChapter)</summary>
  public Chapter GetRootChapter()
  {
    return this.parent == null || this.IsAdditionalChapter ? this : this.parent.GetRootChapter();
  }

  /// <summary>Использовать родительский узел документа</summary>
  public virtual bool UseParentDocNode
  {
    [DebuggerStepThrough] get => false;
  }

  /// <summary>Заголовок</summary>
  public virtual string Caption
  {
    [DebuggerStepThrough] get => this.caption ?? "";
    set
    {
      if (!(this.caption != value))
        return;
      this.caption = value;
      if (this.docNodesExp != null)
      {
        for (int index = 0; index < this.docNodesExp.Count; ++index)
          this.docNodesExp[index].SetName(this.Caption, false, false);
      }
      for (int index = 0; index < this.docNodes.Count; ++index)
        this.docNodes[index].SetName(this.Caption, true, true);
    }
  }

  /// <summary>Заголовок в экспортной СП</summary>
  public virtual string CaptionExp
  {
    [DebuggerStepThrough] get => this.captionExp;
    set
    {
      if (!(this.captionExp != value))
        return;
      this.captionExp = value;
      if (this.docNodesExp == null)
        return;
      for (int index = 0; index < this.docNodesExp.Count; ++index)
        this.docNodesExp[index].SetAttributeValue(Chapter.ExportCaption_AttributeName, this.captionExp, false);
    }
  }

  /// <summary>Уровень раздела в структуре спецификации</summary>
  public string NodeLevel
  {
    [DebuggerStepThrough] get => this.nodeLevel;
    set
    {
      if (!(this.nodeLevel != value))
        return;
      this.nodeLevel = value;
      for (int index = 0; index < this.docNodes.Count; ++index)
      {
        if (this.nodeLevel != null)
          this.docNodes[index].SetAttributeValue(Chapter.DocNodeType_AttributeName, this.nodeLevel, false, false, false);
        else
          this.docNodes[index].RemoveAttribute(Chapter.DocNodeType_AttributeName, false, false);
      }
    }
  }

  /// <summary>Подразделы</summary>
  public List<Chapter> Chapters
  {
    [DebuggerStepThrough] get => this.chapters;
  }

  /// <summary>Индекс сортировки разделов по их типам</summary>
  public virtual long ChapterSortIndex
  {
    [DebuggerStepThrough] get => 10;
  }

  /// <summary>Индекс сортировки однотипных разделов</summary>
  public virtual long SortIndex
  {
    get => this.sortIndex;
    set => this.sortIndex = value;
  }

  /// <summary>Поля отображаемые в бумажном виде документа</summary>
  [Browsable(false)]
  public virtual List<AvsRowAttributeInfo> DocRowFields
  {
    [DebuggerStepThrough] get
    {
      if (this.parent != null)
        return this.parent.DocRowFields;
      return this.avsDocument != null ? this.avsDocument.docRowFields : new List<AvsRowAttributeInfo>();
    }
  }

  /// <summary>Поля отображаемые в бумажном виде экспортного документа</summary>
  [Browsable(false)]
  public virtual List<AvsRowAttributeInfo> DocRowFields_Exp
  {
    [DebuggerStepThrough] get
    {
      if (this.parent != null)
        return this.parent.DocRowFields_Exp;
      return this.avsDocument != null ? this.avsDocument.docRowFields_Exp : new List<AvsRowAttributeInfo>();
    }
  }

  /// <summary>Создать узел документа для этого раздела</summary>
  /// <remarks>При необходимости создаёт новую страницу для раздела</remarks>
  /// <param name="templateNode">Шаблон</param>
  /// <returns>Узел документа</returns>
  public virtual TableData CreateDocNode(TableData templateNode)
  {
    if (this.avsDocument == null)
      return (TableData) null;
    TableData ownerNode = (TableData) null;
    if (this.IsCommonDataChapter && this.IsFormB && this.parent == null)
    {
      if (this.avsDocument.productsPage2Template != null)
      {
        PageData pageData = (PageData) this.avsDocument.productsPage2Template.CloneFromTemplate(true, true);
        if (templateNode == null)
          templateNode = this.avsDocument.avsDocTableFormBMore10_Template;
        if (templateNode != null)
          ownerNode = pageData.FindFirstNodeFromTemplate_Recursive((DocumentTreeNode) templateNode) as TableData;
      }
    }
    else
    {
      if (templateNode == null)
        throw new ArgumentNullException(nameof (templateNode));
      if (templateNode.IsTopLevelTable)
      {
        ownerNode = templateNode.Page.CloneFromTemplate(true, true).FindFirstNodeFromTemplate_Recursive((DocumentTreeNode) templateNode) as TableData;
      }
      else
      {
        ownerNode = (TableData) templateNode.CloneFromTemplate(true, true);
        ownerNode.Id = this.Caption;
        ownerNode.SetName(this.Caption, false, false);
        if (this.ChapterGuid != Guid.Empty || this.ChapterID != -1L)
          ownerNode.Reference = (ReferenceBase) new ReferenceToDBObject((DocumentTreeNode) ownerNode, RefToDBObjectType.rtSelectedObject, (DBObjectInfoBase) new DBObjectInfo(this.ChapterGuid, this.ChapterID, this.ChapterType, this.Caption), true);
        ownerNode.UpdateNodeLinks(true, false, false, false);
      }
    }
    return ownerNode;
  }

  /// <summary>Найти ячейку для текста примечания</summary>
  /// <param name="noteDocRow">Строка примечания документа</param>
  /// <returns>Индекс ячейки в строке</returns>
  public static int FindNoteRowTextCell(TableData noteDocRow)
  {
    textData4 = (TextData) null;
    int index1 = -1;
    if (noteDocRow != null)
    {
      int count = noteDocRow.Nodes.Count;
      index1 = 4;
      if (count > index1 && noteDocRow.Nodes[index1] is TextData textData4 && textData4.Name != Chapter.NoteRowTextCellName)
        textData4 = (TextData) null;
      if (textData4 == null && count > 3)
      {
        index1 = 3;
        if (noteDocRow.Nodes[index1] is TextData textData4 && textData4.Name != Chapter.NoteRowTextCellName)
          textData4 = (TextData) null;
      }
      if (textData4 == null)
      {
        for (int index2 = 0; textData4 == null && index2 < 3 && index2 < count; ++index2)
        {
          index1 = index2;
          if (noteDocRow.Nodes[index2] is TextData textData4 && textData4.Name != Chapter.NoteRowTextCellName)
            textData4 = (TextData) null;
        }
        for (int index3 = 5; textData4 == null && index3 < count; ++index3)
        {
          index1 = index3;
          if (noteDocRow.Nodes[index3] is TextData textData4 && textData4.Name != Chapter.NoteRowTextCellName)
            textData4 = (TextData) null;
        }
      }
    }
    return textData4 != null ? index1 : -1;
  }

  /// <summary>Создать узел табличного вида для записи примечания</summary>
  /// <param name="treeList">Дерево табличного вида</param>
  /// <param name="noteDocRow">Строка примечания в документе</param>
  /// <param name="index">Индекс узла в дереве</param>
  /// <returns>Узел дерева</returns>
  public virtual TreeListNode CreateNoteTreeListNode(
    TreeList treeList,
    TableData noteDocRow,
    int index)
  {
    if (this.avsDocument == null || !this.avsDocument.IsGeneratedDoc && this.avsDocument.ReadOnly && this.DocNode == null)
      return (TreeListNode) null;
    if (!this.avsDocument.IsGridViewMode)
      return (TreeListNode) null;
    object[] nodeData = new object[treeList.Columns.Count];
    for (int index1 = 0; index1 < nodeData.Length; ++index1)
      nodeData[index1] = !(treeList.Columns[index1].Name != "AVS.Status") ? (object) StatusIcons.None : (object) "-";
    int index2 = -1;
    if (this.avsDocument.AVSWindow != null)
      index2 = this.avsDocument.AVSWindow.GetNameColumnIndex();
    if (index2 > -1 && index2 < nodeData.Length)
    {
      int noteRowTextCell = Chapter.FindNoteRowTextCell(noteDocRow);
      if (noteRowTextCell != -1 && noteDocRow.Nodes[noteRowTextCell] is TextData node)
        nodeData[index2] = (object) node.Text;
    }
    TreeListNode node1 = treeList.AppendNode((object) nodeData, this.ListNode);
    node1.Tag = noteDocRow.Tag;
    treeList.SetNodeIndex(node1, index);
    return node1;
  }

  /// <summary>Игнорировать параметр сreateForEmptyChapters или нет</summary>
  protected virtual bool IgnoreCreateForEmptyChapters => false;

  /// <summary>Раздел пуст</summary>
  public virtual bool IsEmpty
  {
    [DebuggerStepThrough] get
    {
      for (int index = 0; index < this.chapters.Count; ++index)
      {
        if (!this.chapters[index].UseParentDocNode || !this.chapters[index].IsEmpty)
          return false;
      }
      return true;
    }
  }

  /// <summary>Найти записи по идентификатору версии изделия</summary>
  /// <param name="partId">Идентификатор версии изделия</param>
  /// <param name="productInfo">Информация об исполнении или блоке данных</param>
  /// <param name="sectionId">Идентификатор раздела</param>
  /// <param name="chapterGuid">Идентификатор части. Guid.Empty если в общей части</param>
  /// <returns>Список записей спецификации</returns>
  public virtual List<AVSRow> FindAvsRowsByPartId(
    long partId,
    ProductInfo productInfo,
    long sectionId,
    Guid? chapterGuid)
  {
    return this.avsDocument.FindAvsRowsByPartId(partId, this, productInfo, sectionId, chapterGuid);
  }

  /// <summary>Найти записи по идентификатору версии изделия</summary>
  /// <param name="partGuid">Глобальный идентификатор версии изделия</param>
  /// <param name="productInfo">Информация об исполнении или блоке данных</param>
  /// <param name="sectionId">Идентификатор раздела</param>
  /// <param name="chapterGuid">Идентификатор части</param>
  /// <returns>Список записей спецификации</returns>
  public virtual List<AVSRow> FindSpecRowsByPartGuid(
    Guid partGuid,
    ProductInfo productInfo,
    long sectionId,
    Guid? chapterGuid)
  {
    return this.avsDocument.FindSpecRowsByPartGuid(partGuid, this, productInfo, sectionId, chapterGuid);
  }

  /// <summary>Получить список всех разделов входящих в данный</summary>
  /// <returns>Список разделов</returns>
  public List<Chapter> GetAllChapters()
  {
    List<Chapter> allChapters = new List<Chapter>();
    if (this.Chapters != null)
    {
      foreach (Chapter chapter in this.Chapters)
      {
        allChapters.Add(chapter);
        allChapters.AddRange((IEnumerable<Chapter>) chapter.GetAllChapters());
      }
    }
    return allChapters;
  }

  /// <summary>Получить все записи спецификации из этого раздела</summary>
  /// <param name="withRelationsOnly">Получить только записи со связями</param>
  /// <param name="withObjectsOnly">Получить только записи с объектами. Если onlyRelations и onlyObjects имеют значение false,
  /// то получают только информационные записи примечания</param>
  /// <param name="rowList">Список записей</param>
  public virtual void GetAllRowsList(
    bool withRelationsOnly,
    bool withObjectsOnly,
    List<AVSRow> rowList)
  {
    for (int index = 0; index < this.chapters.Count; ++index)
      this.chapters[index].GetAllRowsList(withRelationsOnly, withObjectsOnly, rowList);
  }

  /// <summary>Получить энумератор по всем записям спецификации из этого раздела</summary>
  /// <param name="withRelationsOnly">Получить только записи со связями</param>
  /// <param name="withObjectsOnly">Получить только записи с объектами. Если onlyRelations и onlyObjects имеют значение false,
  /// то получают только информационные записи примечания</param>
  public virtual IEnumerable<AVSRow> GetRows(bool withRelationsOnly = false, bool withObjectsOnly = false)
  {
    foreach (Chapter chapter in this.chapters)
    {
      foreach (AVSRow row in chapter.GetRows(withRelationsOnly, withObjectsOnly))
        yield return row;
    }
  }

  internal IEnumerable<Chapter> GetChaptersEnumerator()
  {
    foreach (Chapter chapter1 in this.chapters)
    {
      yield return chapter1;
      foreach (Chapter chapter2 in chapter1.GetChaptersEnumerator())
        yield return chapter2;
    }
  }

  /// <summary>Индексировать записи документа</summary>
  /// <param name="startIndex">Начальный индекс</param>
  /// <param name="endIndex">Последний индекс диапазона</param>
  /// <param name="onlyNew">Не менять уже установленные индексы</param>
  /// <param name="session">Сессия</param>
  public virtual void IndexSpecificationRows(
    long startIndex,
    out long endIndex,
    bool onlyNew,
    IUserSession session)
  {
    endIndex = startIndex + 10000000L;
    for (int index = 0; index < this.Chapters.Count; ++index)
    {
      this.Chapters[index].IndexSpecificationRows(startIndex, out endIndex, onlyNew, session);
      startIndex = endIndex + 100L;
    }
  }

  public virtual List<IVirtualTreeItem> GetTreeChildren()
  {
    List<IVirtualTreeItem> treeChildren = new List<IVirtualTreeItem>();
    bool flag = this.chapters.Count == 1 && this.chapters[0].UseParentDocNode;
    for (int index = 0; index < this.chapters.Count; ++index)
    {
      if (!flag)
        treeChildren.Add((IVirtualTreeItem) this.chapters[index]);
      else
        treeChildren.AddRange((IEnumerable<IVirtualTreeItem>) this.chapters[index].GetTreeChildren());
    }
    return treeChildren;
  }

  public bool CanTreeShow()
  {
    return (this.avsDocument.IsGeneratedDoc || !this.avsDocument.ReadOnly || this.DocNode != null) && this.avsDocument.IsGridViewMode;
  }

  /// <summary>Родитель в табличном виде</summary>
  public virtual IVirtualTreeItem ParentItem
  {
    get
    {
      if (this.parentTreeItem != null)
        return this.parentTreeItem;
      return this.UseParentDocNode ? this.Parent.ParentItem : (IVirtualTreeItem) this.Parent;
    }
    set => this.parentTreeItem = value;
  }

  public virtual void GetRowData(RowData data)
  {
    if (this.UseParentDocNode)
    {
      this.Parent.GetRowData(data);
    }
    else
    {
      Intermech.AVS.GridColumns.VirtualTreeList.VirtualTreeList virtualTree = this.AVSDocument.AVSWindow.virtualTree;
      if (virtualTree.Columns.Count <= 0)
        return;
      CellData data1 = new CellData(virtualTree.Columns[0]);
      this.GetCellData(virtualTree.Columns[0] as AVSColumn, data1);
      int num = Convert.ToString(data1.Value).Split('\n').Length;
      if (num == 0)
        num = 1;
      data.Height = virtualTree.RowHeight * num;
    }
  }

  public virtual void GetCellData(AVSColumn column, CellData data)
  {
    if (this.UseParentDocNode)
      this.Parent.GetCellData(column, data);
    else
      data.Value = (object) this.Caption;
  }

  bool IVirtualTreeItem.HeaderRow => true;

  /// <summary>Обновить узлы для страничного и табличного видов</summary>
  /// <param name="skipLinesSchema">Настройки пропусков строк</param>
  /// <param name="reCreateDocNode">Пересоздавать узлы документа</param>
  /// <param name="reCreateListNode">Пересоздавать узлы табличного вида</param>
  /// <param name="updateCountB">Обновить количество для групповой СП формы Б</param>
  /// <param name="createForEmptyChapters">Создавать узлы для пустых разделов</param>
  /// <param name="updateTemplate">Обновить шаблоны узлов документа</param>
  /// <param name="updateMode">Режим обновления записей с пустым количеством</param>
  public virtual void UpdateViewNodes(
    SkipLinesSchema skipLinesSchema,
    bool reCreateDocNode,
    bool reCreateListNode,
    bool updateCountB,
    bool createForEmptyChapters,
    bool updateTemplate,
    EmptyRowUpdateMode updateMode)
  {
    if (this.avsDocument == null)
      return;
    if (reCreateDocNode && (!this.IsCommonDataChapter || this.avsDocument.AvsDocumentForm != AVSDocumentForm.Single))
    {
      this.DocNodes = new List<TableData>();
      this.DocNodesExp = new List<TableData>();
    }
    bool isGridViewMode = this.avsDocument.IsGridViewMode;
    List<DataNodesEnumerator> dataNodesEnumeratorList = new List<DataNodesEnumerator>();
    DataNodesEnumerator curChapterPosition = (DataNodesEnumerator) null;
    int curPositionIndex1 = 0;
    List<TableData> tableDataList1 = new List<TableData>();
    int productIndex = -1;
    TableData tableData = !this.UseParentDocNode ? this.GetDocNodeTemplate() : this.parent.GetDocNodeTemplate();
    Dictionary<int, DataNodesEnumerator> productChapterDocNodes = new Dictionary<int, DataNodesEnumerator>();
    if (!this.avsDocument.IsExportSP)
    {
      if (this.HasDocNodes)
      {
        for (int index = 0; index < this.docNodes.Count; ++index)
        {
          dataNodesEnumeratorList.Add(curChapterPosition = new DataNodesEnumerator(this.docNodes[index]));
          productIndex = Chapter.GetFirstProductIndexForDocChapter((DocumentTreeNode) this.docNodes[index]);
          if (!productChapterDocNodes.ContainsKey(productIndex))
            productChapterDocNodes.Add(productIndex, curChapterPosition);
        }
      }
    }
    else
    {
      if (this.HasDocNodes)
      {
        for (int index = 0; index < this.docNodes.Count; ++index)
          dataNodesEnumeratorList.Add(new DataNodesEnumerator(this.docNodes[index]));
      }
      if (this.HasDocNodesExp)
      {
        for (int index = 0; index < this.DocNodesExp.Count; ++index)
          tableDataList1.Add(this.DocNodesExp[index]);
      }
    }
    bool newDocNode = false;
    bool newDocNodeExists = false;
    bool flag1 = false;
    bool skipSectionNode = this.chapters.Count == 1 && this.chapters[0].UseParentDocNode;
    int firstProductForDoc = -1;
    if ((!this.IsFormB ? 0 : (this.IsCommonDataChapter ? 1 : 0)) != 0)
    {
      productIndex = 0;
      if (this.avsDocument.productsInfo.Count == 0)
        productIndex = -1;
      for (; productIndex < this.avsDocument.productsInfo.Count; productIndex += this.avsDocument.RowProductCount)
      {
        if (dataNodesEnumeratorList.FirstOrDefault<DataNodesEnumerator>((Func<DataNodesEnumerator, bool>) (ch => Chapter.GetFirstProductIndexForDocChapter((DocumentTreeNode) ch.MainTable) == productIndex)) == null)
          this.CreateNewChapterDocNode(skipSectionNode, (DataNodesEnumerator) null, (TableData) null, productIndex, 0, tableData, productChapterDocNodes, dataNodesEnumeratorList, ref newDocNode, ref newDocNodeExists, ref curPositionIndex1);
      }
    }
    if (isGridViewMode)
      this.avsDocument.AVSWindow.virtualTree.RefreshRows((IVirtualTreeItem) this);
    int curPositionIndex2 = 0;
    productIndex = 0;
    for (int index1 = 0; index1 < this.chapters.Count; ++index1)
    {
      if (!this.AVSDocument.ReadOnly || this.AVSDocument.IsGeneratedDoc || this.chapters[index1] is ProductVariableDataChapter)
      {
        if (skipSectionNode && this.DocNode != null)
          this.chapters[index1].DocNode = this.DocNode;
        this.chapters[index1].UpdateViewNodes(skipLinesSchema, reCreateDocNode, reCreateListNode, updateCountB, createForEmptyChapters, updateTemplate, updateMode);
        if (!this.IsAdditionalChapter || !(this.chapters[index1] is VariableDataChapterFormV))
        {
          int num1 = this.chapters[index1].HasDocNodes ? 1 : 0;
          int num2 = this.chapters[index1].HasDocNodesExp ? 1 : 0;
          if (this.chapters[index1].HasDocNodes)
          {
            flag1 = false;
            for (int index2 = 0; index2 < this.chapters[index1].DocNodes.Count; ++index2)
            {
              productIndex = Chapter.GetFirstProductIndexForDocChapter((DocumentTreeNode) this.chapters[index1].DocNodes[index2]);
              if (firstProductForDoc == -1 || firstProductForDoc > productIndex)
                firstProductForDoc = productIndex;
            }
            for (int index3 = 0; index3 < this.chapters[index1].DocNodes.Count; ++index3)
            {
              newDocNode = this.chapters[index1].DocNodes[index3].Parent == null;
              if (!this.avsDocument.IsExportSP)
              {
                productIndex = Chapter.GetFirstProductIndexForDocChapter((DocumentTreeNode) this.chapters[index1].DocNodes[index3]);
                productChapterDocNodes.TryGetValue(productIndex, out curChapterPosition);
                if (curChapterPosition != null)
                {
                  if (dataNodesEnumeratorList.Count == 1)
                  {
                    curPositionIndex2 = 0;
                  }
                  else
                  {
                    for (int index4 = 0; index4 < dataNodesEnumeratorList.Count; ++index4)
                    {
                      if (dataNodesEnumeratorList[index4] == curChapterPosition)
                      {
                        curPositionIndex2 = index4;
                        break;
                      }
                    }
                  }
                }
              }
              if (curChapterPosition == null)
                curChapterPosition = this.CreateNewChapterDocNode(skipSectionNode, curChapterPosition, this.chapters[index1].DocNodes[index3], productIndex, firstProductForDoc, tableData, productChapterDocNodes, dataNodesEnumeratorList, ref newDocNode, ref newDocNodeExists, ref curPositionIndex2);
              if (!skipSectionNode)
              {
                curChapterPosition.MoveNext();
                if (newDocNode && curChapterPosition.PrevCellPage != null && curChapterPosition.CurrentCellPage != null && curChapterPosition.PrevCellPage.Index != curChapterPosition.CurrentCellPage.Index && curChapterPosition.CurrentCellPage.IsNextToAdditionalPage)
                  curChapterPosition.AppendAfterPreviousPos((RectangleElement) this.chapters[index1].DocNodes[index3]);
                else
                  curChapterPosition.InsertAtCurrentPos((RectangleElement) this.chapters[index1].DocNodes[index3]);
              }
              flag1 |= newDocNode;
            }
          }
          if (flag1 && this.chapters[index1].HasDocNodes)
          {
            for (int index5 = 0; index5 < this.chapters[index1].DocNodes.Count; ++index5)
              this.chapters[index1].DocNodes[index5].UpdateNodeLinks(true, false, false, false);
          }
        }
      }
    }
    for (int index = 0; index < tableDataList1.Count; ++index)
      tableDataList1[index] = tableDataList1[index].FindFirstTable();
    if (!this.IsExportSP && !skipSectionNode)
    {
      for (int index6 = dataNodesEnumeratorList.Count - 1; index6 >= 0; --index6)
      {
        productIndex = Chapter.GetFirstProductIndexForDocChapter((DocumentTreeNode) dataNodesEnumeratorList[index6].MainTable);
        bool flag2 = productIndex % this.avsDocument.RowProductCount != 0;
        bool flag3 = false;
        if (productChapterDocNodes.TryGetValue(productIndex, out curChapterPosition))
        {
          flag3 = curChapterPosition != dataNodesEnumeratorList[index6];
          int num = flag3 ? 1 : 0;
        }
        if (flag3 | flag2 || updateMode == EmptyRowUpdateMode.Delete && dataNodesEnumeratorList[index6].MainTable.NodesCount - dataNodesEnumeratorList[index6].MainTable.HeadersCount == 0 && (this.IsFormB && this.IsCommonDataChapter || this is VariableDataChapterFormV) || productIndex >= this.avsDocument.productsInfo.Count)
        {
          PageData pageData = dataNodesEnumeratorList[index6].MainTable.Page;
          if (dataNodesEnumeratorList[index6].MainTable.ParentCell != null | flag2 || productIndex >= this.avsDocument.productsInfo.Count)
          {
            dataNodesEnumeratorList[index6].MainTable.UniteTable();
            if (flag3 | flag2 || updateMode == EmptyRowUpdateMode.Delete && dataNodesEnumeratorList[index6].MainTable.NodesCount - dataNodesEnumeratorList[index6].MainTable.HeadersCount == 0 && (this.IsFormB && this.IsCommonDataChapter || this is VariableDataChapterFormV) || productIndex >= this.avsDocument.productsInfo.Count)
            {
              dataNodesEnumeratorList[index6].MainTable.Remove(false, false);
              dataNodesEnumeratorList.RemoveAt(index6);
              if (pageData != null && (this.IsFormB && this.IsCommonDataChapter || this is VariableDataChapterFormV))
              {
                List<PageData> pageDataList = new List<PageData>();
                for (; pageData != null; pageData = pageData.NextPage)
                  pageDataList.Add(pageData);
                for (int index7 = pageDataList.Count - 1; index7 >= 0; --index7)
                  pageDataList[index7].Remove(false, false);
              }
            }
          }
        }
      }
      newDocNodeExists = true;
    }
    for (int index = 0; index * this.avsDocument.RowProductCount < this.avsDocument.productsInfo.Count && (index <= 0 || this.IsFormB); ++index)
    {
      productChapterDocNodes.TryGetValue(index * this.avsDocument.RowProductCount, out curChapterPosition);
      if (curChapterPosition == null)
      {
        newDocNodeExists = true;
        if (index == 0 && this.IsCommonDataChapter && (this.IsFormB || this.avsDocument.AvsDocumentForm == AVSDocumentForm.Single))
        {
          curChapterPosition = new DataNodesEnumerator(this.avsDocument.avsDocTable);
        }
        else
        {
          if (!this.IsCommonDataChapter && !createForEmptyChapters && this.IsEmpty && !this.IgnoreCreateForEmptyChapters && this.DocNode == null)
          {
            newDocNodeExists = false;
            break;
          }
          TableData docNode = this.CreateDocNode(tableData);
          if (docNode != null)
            curChapterPosition = new DataNodesEnumerator(docNode);
        }
        if (this.IsCommonDataChapter && this.IsFormB && this.parent == null)
          curChapterPosition.MainTable.FromNewPage = true;
        if (index > 0)
        {
          curChapterPosition.MainTable.SetAttributeValue(AVSRow.DocAttr_ProductIndex, (index * this.avsDocument.RowProductCount).ToString(), false, false, false);
          if (this.IsCommonDataChapter)
            curChapterPosition.MainTable.SetName($"Исполнения {index * this.avsDocument.RowProductCount}...{(index + 1) * this.avsDocument.RowProductCount - 1}", false, false);
        }
        else
          curChapterPosition.MainTable.RemoveAttribute(AVSRow.DocAttr_ProductIndex, false, false);
        dataNodesEnumeratorList.Insert(index, curChapterPosition);
      }
    }
    if (!(newDocNodeExists | skipSectionNode))
      return;
    List<TableData> tableDataList2 = new List<TableData>();
    for (int index = 0; index < dataNodesEnumeratorList.Count; ++index)
      tableDataList2.Add(dataNodesEnumeratorList[index].MainTable);
    this.DocNodes = tableDataList2;
  }

  private DataNodesEnumerator CreateNewChapterDocNode(
    bool skipSectionNode,
    DataNodesEnumerator curChapterPosition,
    TableData subChapterDocNode,
    int productIndex,
    int firstProductForDoc,
    TableData docNodeTemplate,
    Dictionary<int, DataNodesEnumerator> productChapterDocNodes,
    List<DataNodesEnumerator> subChaptersDocPositions,
    ref bool newDocNode,
    ref bool newDocNodeExists,
    ref int curPositionIndex)
  {
    if (skipSectionNode)
      curChapterPosition = new DataNodesEnumerator(subChapterDocNode);
    else if (!this.avsDocument.IsExportSP && productIndex == firstProductForDoc && this.parent == null)
    {
      if (this.IsCommonDataChapter && this.IsFormB)
        curChapterPosition = new DataNodesEnumerator(this.avsDocument.avsDocTable);
      else if (this is VariableDataChapterFormV)
      {
        if (this.avsDocument.avsFormB_Table == null)
          this.avsDocument.avsFormB_Table = this.CreateDocNode(this.avsDocument.avsDocTableFormBForV_Template);
        curChapterPosition = new DataNodesEnumerator(this.avsDocument.avsFormB_Table);
      }
    }
    if (curChapterPosition == null)
    {
      curChapterPosition = new DataNodesEnumerator(this.CreateDocNode(docNodeTemplate));
      newDocNode = true;
      newDocNodeExists = true;
    }
    if (!this.avsDocument.IsExportSP)
    {
      if (productIndex != 0 && this.IsFormB)
        curChapterPosition.MainTable.SetAttributeValue(AVSRow.DocAttr_ProductIndex, productIndex.ToString(), false, false, false);
      else
        curChapterPosition.MainTable.RemoveAttribute(AVSRow.DocAttr_ProductIndex, false, false);
      if (!productChapterDocNodes.ContainsKey(productIndex))
        productChapterDocNodes.Add(productIndex, curChapterPosition);
      else
        productChapterDocNodes[productIndex] = curChapterPosition;
      curPositionIndex = 0;
      while (curPositionIndex < subChaptersDocPositions.Count && Chapter.GetFirstProductIndexForDocChapter((DocumentTreeNode) subChaptersDocPositions[curPositionIndex].MainTable) <= productIndex)
        ++curPositionIndex;
    }
    if (this.IsCommonDataChapter)
      curChapterPosition.MainTable.SetFromNewPage(true, false, false);
    if (newDocNode || !subChaptersDocPositions.Contains(curChapterPosition))
      subChaptersDocPositions.Insert(curPositionIndex, curChapterPosition);
    return curChapterPosition;
  }

  /// <summary>Вставить страницы созданные для раздела в документ</summary>
  /// <param name="prevPage">Предыдущая страница</param>
  /// <returns>Возвращает последнюю страницу</returns>
  public virtual PageData InsertPagesInDocument(PageData prevPage) => prevPage;

  /// <summary>Получить первый индекс исполнения в разделе документа</summary>
  /// <param name="node">Узел документа</param>
  /// <returns></returns>
  public static int GetFirstProductIndexForDocChapter(DocumentTreeNode node)
  {
    int result = 0;
    DocumentTreeNode documentTreeNode = AVSDocument.FindParentChapterDocNode(node, false) ?? node;
    if (documentTreeNode != null)
    {
      string attributeValue = documentTreeNode.GetAttributeValue(AVSRow.DocAttr_ProductIndex, true);
      if (attributeValue == "" || !int.TryParse(attributeValue, out result))
        result = 0;
    }
    return result;
  }

  /// <summary>Начало раздела в начале страницы</summary>
  /// <returns></returns>
  internal static bool IsStartOfPage(TableData docNode)
  {
    if (docNode == null)
      throw new ArgumentNullException(nameof (docNode));
    if (docNode.PrevCell != null)
      return false;
    for (; docNode != null; docNode = docNode.ParentCell)
    {
      if (docNode.ParentCell != null && docNode.Index != 0)
        return false;
    }
    return true;
  }

  /// <summary>Обновить пропуски строк в документе согласно настройкам</summary>
  /// <param name="skipLinesSchema">Настройки пропусков строк</param>
  /// <param name="str">О назначении структуры спрашивать у Пилипёнка</param>
  public virtual void UpdateSkipLines(SkipLinesSchema skipLinesSchema, SkipLinesStruct str)
  {
    foreach (TableData docNode in this.docNodes)
    {
      TableData tableData = this.GetChapterCaptionRow(docNode) ?? docNode;
      tableData.SetSkipCellsAfter(str.SkipAfter, true, false, false);
      tableData.SetSkipCellsBefore(str.SkipBefore, true, false, false);
    }
  }

  /// <summary>Получить пропуски строк</summary>
  /// <param name="skipLinesSchema">Настройки пропусков строк</param>
  /// <param name="structs">Список пропусков строк</param>
  /// <returns></returns>
  public virtual SkipLinesStruct GetSkipLines(
    SkipLinesSchema skipLinesSchema,
    List<SkipLinesStruct> structs)
  {
    SkipLinesStruct skipLines = new SkipLinesStruct(this);
    if (!this.IsCommonDataChapter)
    {
      switch (this)
      {
        case ProductVariableDataChapter _:
        case VariableDataChapterFormA _:
        case SpecificationSection _:
        case AdditionalChapter _:
label_4:
          structs.Add(skipLines);
          break;
        default:
          if (this.DocNode != null && (this.DocNode.NodesCount > 0 ? (DocumentTreeNode) (this.DocNode.Nodes[0] as TableData) : (DocumentTreeNode) this.DocNode).Template is TableData template)
          {
            skipLines.SkipBefore = template.SkipCellsBefore;
            skipLines.SkipAfter = template.SkipCellsAfter;
            goto label_4;
          }
          goto label_4;
      }
    }
    for (int index = 0; index < this.Chapters.Count; ++index)
    {
      if (this.Chapters[index] != null)
        this.Chapters[index].GetSkipLines(skipLinesSchema, structs);
    }
    return skipLines;
  }

  /// <summary>Сортировать</summary>
  public virtual void Sort()
  {
    for (int index = 0; index < this.chapters.Count; ++index)
      this.chapters[index].Sort();
    this.chapters.Sort();
  }

  /// <summary>Обновить кэш настроек сортировки</summary>
  /// <param name="sortSchema">Настройки сортировки</param>
  public virtual void UpdateSortSchema(SortSchema sortSchema)
  {
    for (int index = 0; index < this.chapters.Count; ++index)
      this.chapters[index].UpdateSortSchema(sortSchema);
  }

  /// <summary>Пронумеровать позиции записей</summary>
  /// <param name="numerationHelper">Вспомогательный класс для нумерации позиций</param>
  public virtual void RenumberPositions(NumerationHelper numerationHelper)
  {
    for (int index = 0; index < this.chapters.Count; ++index)
      this.chapters[index].RenumberPositions(numerationHelper);
  }

  /// <summary>Обновить заголовки в документе и табличном виде</summary>
  public virtual void UpdateChapterCaption()
  {
    string caption = this.Caption;
    if (!this.UseParentDocNode)
    {
      foreach (DocumentTreeNode documentTreeNode in this.docNodes.Where<TableData>((Func<TableData, bool>) (t => !t.IsTopLevelTable)))
        documentTreeNode.SetName(caption, false, false);
    }
    if (this.listNode == null || this.listNode.TreeList == null || !this.avsDocument.IsGridViewMode)
      return;
    int nameColumnIndex = this.avsDocument.AVSWindow.GetNameColumnIndex();
    if (nameColumnIndex == -1 || this.listNode[(object) nameColumnIndex] == null || !(this.listNode[(object) nameColumnIndex].ToString() != caption))
      return;
    this.listNode[(object) nameColumnIndex] = (object) caption;
  }

  /// <summary>Получить строку заголовка раздела</summary>
  /// <returns></returns>
  public virtual TableData GetChapterCaptionRow()
  {
    return this.DocNode != null ? this.GetChapterCaptionRow(this.DocNode) : (TableData) null;
  }

  /// <summary>Получить строку заголовка раздела</summary>
  /// <param name="chapterTable">Таблица документа в которой нужно найти заголовок</param>
  /// <returns></returns>
  public virtual TableData GetChapterCaptionRow(TableData chapterTable)
  {
    return chapterTable != null && chapterTable.NodesCount > 0 && chapterTable.Nodes[0] is TableData node && node.TableCellType == CellType.Header ? node : (TableData) null;
  }

  /// <summary>Скрыть строку заголовка раздела</summary>
  /// <param name="chapterTable">Таблица документа в которой нужно скрыть заголовок</param>
  public virtual void HideChapterHeaderRow(TableData chapterTable)
  {
    this.GetChapterCaptionRow(chapterTable)?.SetVisible(false, false, true, false, false);
  }

  /// <summary>Получить шаблон для раздела документа.
  /// Не использовать если раздел ещё не добавлен в состав документа!</summary>
  /// <returns></returns>
  internal virtual TableData GetSectionTemplate()
  {
    if (this.avsDocument == null)
      return (TableData) null;
    return this.avsDocument.AvsDocumentForm == AVSDocumentForm.V && this.IsFormB ? this.avsDocument.sectionFormBTemplate : this.avsDocument.sectionTemplate;
  }

  /// <summary>Получить шаблон для раздела экспортного документа.
  /// Не использовать если раздел ещё не добавлен в состав документа!</summary>
  /// <returns></returns>
  internal virtual TableData GetSectionExpTemplate()
  {
    return this.avsDocument == null ? (TableData) null : this.avsDocument.sectionExpTemplate;
  }

  /// <summary>Получить шаблон для раздела документа.
  /// Не использовать если раздел ещё не добавлен в состав документа!</summary>
  /// <returns></returns>
  internal virtual TableData GetChapterTemplate()
  {
    if (this.avsDocument == null)
      return (TableData) null;
    return this.avsDocument.AvsDocumentForm == AVSDocumentForm.V && this.IsFormB ? this.avsDocument.chapterWithoutHeaderFormBTemplate : this.avsDocument.chapterWithoutHeaderTemplate;
  }

  /// <summary>Получить шаблон для раздела экспортного документа.
  /// Не использовать если раздел ещё не добавлен в состав документа!</summary>
  /// <returns></returns>
  internal virtual TableData GetChapterExpTemplate()
  {
    return this.avsDocument == null ? (TableData) null : this.avsDocument.chapterWithoutHeaderExpTemplate;
  }

  /// <summary>Получить шаблон узла документа для этого подраздела</summary>
  public virtual TableData GetDocNodeTemplate()
  {
    if (this.avsDocument == null)
      return (TableData) null;
    if (!this.IsCommonDataChapter)
      return this.GetSectionTemplate();
    if (this.parent != null)
      return this.GetChapterTemplate();
    return this.IsFormB ? this.avsDocument.avsDocTableFormBMore10_Template : this.avsDocument.commonChapterTemplate;
  }

  /// <summary>Получить шаблон узла экспортной СП для этого подраздела</summary>
  public virtual TableData GetDocNodeExpTemplate()
  {
    if (this.avsDocument == null)
      return (TableData) null;
    if (!this.IsCommonDataChapter)
      return this.GetSectionExpTemplate();
    return this.parent == null ? this.avsDocument.commonChapterExpTemplate : this.GetChapterExpTemplate();
  }

  /// <summary>Раздел для общих данных в групповом документе</summary>
  public bool IsCommonDataChapter
  {
    [DebuggerStepThrough] get => this.nodeLevel == Chapter.CommonData_TypeName;
  }

  /// <summary>Раздел для переменных данных в групповом документе</summary>
  public bool IsVariableDataChapter
  {
    [DebuggerStepThrough] get => this.nodeLevel == Chapter.VariableData_TypeName;
  }

  /// <summary>Раздел для переменных данных исполнения в групповом документе</summary>
  public bool IsProductVariableDataChapter
  {
    [DebuggerStepThrough] get => this.nodeLevel == Chapter.ProductVariableData_TypeName;
  }

  /// <summary>Раздел для дополнительных частей типа "Устанавливается по МЭ"</summary>
  public bool IsAdditionalChapter
  {
    [DebuggerStepThrough] get => this.nodeLevel == Chapter.AdditionalChapter_TypeName;
  }

  /// <summary>Структура таблицы формы Б</summary>
  [Browsable(false)]
  public virtual bool IsFormB
  {
    [DebuggerStepThrough] get
    {
      if (this.Parent != null)
        return this.Parent.IsFormB;
      return this.avsDocument != null && this.avsDocument.IsFormB;
    }
  }

  /// <summary>Является владельцем разделов спецификации</summary>
  public bool IsSectionOwner
  {
    [DebuggerStepThrough] get => this.isSectionOwner;
  }

  /// <summary>Экспортная спецификация</summary>
  [Browsable(false)]
  public virtual bool IsExportSP
  {
    [DebuggerStepThrough] get => this.avsDocument != null && this.avsDocument.IsExportSP;
  }

  /// <summary>Проверить допустимость главной таблицы для нормальной части СП</summary>
  /// <param name="table">Подраздел нормальной части СП</param>
  /// <param name="hasNormalDocNode">Эта часть содержит узлы нормальной части СП</param>
  /// <param name="hasExportDocNode">Эта часть содержит узлы экспортной части СП</param>
  /// <returns></returns>
  internal bool TopTableIsSuitable_Normal(
    TableData table,
    bool hasNormalDocNode,
    bool hasExportDocNode)
  {
    table = table != null ? table.TopLevelTable : throw new ArgumentNullException(nameof (table));
    if (hasNormalDocNode & hasExportDocNode)
      return table.Template == this.avsDocument.avsDocTableTemplate || table.Template == this.avsDocument.avsDocTableMixP1_Template;
    if (!hasNormalDocNode)
      return false;
    return table.Template == this.avsDocument.avsDocTableSingleT1_Template || table.Template == this.avsDocument.avsDocTableSingleP2_Template;
  }

  /// <summary>Проверить допустимость главной таблицы для экспортной части СП</summary>
  /// <param name="table">Подраздел экспортной части СП</param>
  /// <param name="hasNormalDocNode">Эта часть содержит узлы нормальной части СП</param>
  /// <param name="hasExportDocNode">Эта часть содержит узлы экспортной части СП</param>
  /// <returns></returns>
  internal bool TopTableIsSuitable_Export(
    TableData table,
    bool hasNormalDocNode,
    bool hasExportDocNode)
  {
    table = table != null ? table.TopLevelTable : throw new ArgumentNullException(nameof (table));
    if (hasNormalDocNode & hasExportDocNode)
      return table.Template == this.avsDocument.avsDocTableTemplate || table.Template == this.avsDocument.avsDocTableMixP1_Template;
    if (!hasExportDocNode)
      return false;
    return table.Template == this.avsDocument.avsDocTableExpSingle_Template || table.Template == this.avsDocument.avsDocTableExpSingleP2_Template;
  }

  /// <summary>Преобразовать в строку</summary>
  public override string ToString() => $"{base.ToString()}: \"{this.Caption}\"";

  /// <summary>Подучить подраздел по идентификатору</summary>
  /// <param name="chapterId">Идентификатор раздела</param>
  /// <returns></returns>
  public virtual Chapter GetChapter(long chapterId)
  {
    Chapter chapter = (Chapter) null;
    if (chapterId != -1L)
      this.chaptersIdDictionary.TryGetValue(chapterId, out chapter);
    else
      chapter = this.chapters.FirstOrDefault<Chapter>((Func<Chapter, bool>) (ch => ch.ChapterID == chapterId));
    if (chapter == null)
      this.chaptersGuidDictionary.TryGetValue(AVSDocument.SectionUnassignedGuid, out chapter);
    return chapter;
  }

  /// <summary>Подучить подраздел по глобальному идентификатору</summary>
  /// <param name="chapterId">Глобальному идентификатор раздела</param>
  /// <returns></returns>
  public virtual Chapter GetChapter(Guid chapterGuid)
  {
    Chapter chapter = (Chapter) null;
    this.chaptersGuidDictionary.TryGetValue(chapterGuid, out chapter);
    return chapter;
  }

  /// <summary>Добавить подраздел</summary>
  /// <param name="chapter">Подраздел</param>
  /// <param name="sort">Вставить согласно сортировке</param>
  /// <param name="createDocNode">Создать узел документа</param>
  /// <param name="createListNode">Создать узел дерева табличного вида</param>
  /// <param name="docChapterTemplate">Шаблон раздела документа</param>
  /// <returns>Индекс вставленного подраздела</returns>
  public virtual int AddChapter(
    Chapter chapter,
    bool sort,
    bool createDocNode,
    bool createListNode,
    TableData docChapterTemplate)
  {
    int chapterIndex = !sort ? this.chapters.Count : AVSDocument.FindIndexInSortedList((object) chapter, (IList) this.chapters, true, 0, (IComparer) this.avsDocument);
    this.InsertChapter(chapter, chapterIndex, createDocNode, createListNode, docChapterTemplate);
    return chapterIndex;
  }

  /// <summary>Вставить подраздел</summary>
  /// <param name="chapter">Подраздел</param>
  /// <param name="chapterIndex">Индекс подраздела</param>
  /// <param name="createDocNode">Создать узел документа</param>
  /// <param name="createListNode">Создать узел дерева табличного вида</param>
  /// <param name="docChapterTemplate">Шаблон раздела документа</param>
  public virtual void InsertChapter(
    Chapter chapter,
    int chapterIndex,
    bool createDocNode,
    bool createListNode,
    TableData docChapterTemplate)
  {
    if (chapter.ChapterID != -1L)
      this.chaptersIdDictionary.Add(chapter.ChapterID, chapter);
    if (chapter.ChapterGuid != Guid.Empty)
      this.chaptersGuidDictionary.Add(chapter.ChapterGuid, chapter);
    this.chapters.Insert(chapterIndex, chapter);
    chapter.Parent = this;
    chapter.AVSDocument = this.avsDocument;
    if (createDocNode)
    {
      if (docChapterTemplate == null)
        docChapterTemplate = this.GetSectionTemplate();
      for (int index1 = 0; index1 < this.DocNodes.Count; ++index1)
      {
        TableData tableData1 = (TableData) null;
        int indexForDocChapter1 = Chapter.GetFirstProductIndexForDocChapter((DocumentTreeNode) this.DocNodes[index1]);
        for (int index2 = 0; index2 < chapter.DocNodes.Count; ++index2)
        {
          if (Chapter.GetFirstProductIndexForDocChapter((DocumentTreeNode) chapter.DocNodes[index2]) == indexForDocChapter1)
          {
            tableData1 = chapter.DocNodes[index2];
            break;
          }
        }
        if (tableData1 == null)
        {
          tableData1 = chapter.CreateDocNode(docChapterTemplate);
          if (this.IsFormB)
          {
            int indexForDocChapter2 = Chapter.GetFirstProductIndexForDocChapter((DocumentTreeNode) this.docNodes[index1]);
            if (indexForDocChapter2 != 0)
              tableData1.SetAttributeValue(AVSRow.DocAttr_ProductIndex, indexForDocChapter2.ToString(), false, false, false);
            else
              tableData1.RemoveAttribute(AVSRow.DocAttr_ProductIndex, false, false);
          }
          chapter.AddDocNode(tableData1);
        }
        TableData dataOwner = this.DocNodes[index1];
        int index3 = 0;
        if (chapterIndex > 0)
        {
          TableData tableData2 = (TableData) null;
          for (int index4 = chapterIndex - 1; tableData2 == null && index4 >= 0; --index4)
          {
            for (int index5 = 0; index5 < this.chapters[index4].DocNodes.Count; ++index5)
            {
              if (Chapter.GetFirstProductIndexForDocChapter((DocumentTreeNode) this.chapters[index4].DocNodes[index5]) == indexForDocChapter1)
              {
                tableData2 = this.chapters[index4].DocNodes[index5];
                break;
              }
            }
          }
          if (tableData2 != null)
          {
            dataOwner = tableData2.ParentCell;
            if (dataOwner != null)
              index3 = dataOwner.FindNextDataPositionInFlow(tableData2.Index, out dataOwner);
          }
        }
        if (index3 == 0 && dataOwner == this.DocNodes[index1])
          index3 = this.DocNodes[index1].FindDataPositionInFlow(0, out dataOwner);
        if (index3 == -1)
          index3 = 0;
        if (dataOwner == null)
        {
          dataOwner = this.DocNodes[index1];
          index3 = 0;
        }
        dataOwner.InsertChildNode(index3, (DocumentTreeNode) tableData1, false, true, false, false, false);
        if (chapter is SpecificationSection)
        {
          if (this.IsFormB)
          {
            string attributeValue = dataOwner.GetAttributeValue(AVSRow.DocAttr_ProductIndex, true);
            if (attributeValue != "")
              tableData1.SetAttributeValue(AVSRow.DocAttr_ProductIndex, attributeValue, false, false, false);
            else
              tableData1.RemoveAttribute(AVSRow.DocAttr_ProductIndex, false, false);
          }
          else
            tableData1.RemoveAttribute(AVSRow.DocAttr_ProductIndex, false, false);
        }
      }
    }
    if (createListNode && this.avsDocument.IsGridViewMode)
      this.avsDocument.AVSWindow.virtualTree.RefreshRows((IVirtualTreeItem) chapter.Parent);
    if (!createDocNode)
      return;
    this.UpdateChapterCaption();
  }

  /// <summary>Удалить подраздел</summary>
  /// <param name="chapter">Подраздел</param>
  /// <param name="removeRelations">Удалить связи, принадлежащие записям, из базы</param>
  /// <param name="removeDocObjectWithoutRelations">Удалять документы без связей</param>
  /// <param name="removeDocNode">Удалить узлы документа</param>
  /// <param name="removeGridNode">Удалить узлы табличного вида</param>
  public virtual List<KeyValuePair<long, RelInfo>> RemoveChapter(
    Chapter chapter,
    bool removeRelations,
    bool removeDocObjectWithoutRelations,
    bool removeDocNode,
    bool removeGridNode)
  {
    List<KeyValuePair<long, RelInfo>> keyValuePairList = new List<KeyValuePair<long, RelInfo>>();
    List<AVSRow> rowList = new List<AVSRow>();
    chapter.GetAllRowsList(false, false, rowList);
    for (int index = 0; index < rowList.Count; ++index)
    {
      List<KeyValuePair<long, RelInfo>> collection = rowList[index].Section.RemoveRow(rowList[index], true, removeRelations, false, false, removeDocObjectWithoutRelations);
      keyValuePairList.AddRange((IEnumerable<KeyValuePair<long, RelInfo>>) collection);
    }
    int index1 = this.chapters.IndexOf(chapter);
    if (index1 != -1)
    {
      this.chapters.RemoveAt(index1);
      chapter.Parent = (Chapter) null;
      this.chaptersIdDictionary.Remove(chapter.ChapterID);
      this.chaptersGuidDictionary.Remove(chapter.ChapterGuid);
      if (removeDocNode)
      {
        for (int index2 = 0; index2 < chapter.DocNodes.Count; ++index2)
        {
          TableData parentCell = chapter.DocNodes[index2].ParentCell;
          chapter.DocNodes[index2].UniteTable();
          chapter.DocNodes[index2].Remove(true, true);
          if (parentCell != null && parentCell.Nodes.Count == 0 && !parentCell.IsTopLevelTable)
          {
            RectangleF properBounds = parentCell.ProperBounds with
            {
              Height = !parentCell.IsFixedSizeRows ? parentCell.MinHeight : parentCell.DefaultRowSize
            };
            parentCell.AssignProperBounds(properBounds, false, true, true);
          }
        }
        for (int index3 = 0; chapter.DocNodesExp != null && index3 < chapter.DocNodesExp.Count; ++index3)
        {
          TableData parentCell = chapter.DocNodesExp[index3].ParentCell;
          chapter.DocNodesExp[index3].UniteTable();
          chapter.DocNodesExp[index3].Remove(true, true);
          if (parentCell != null && parentCell.Nodes.Count == 0 && !parentCell.IsTopLevelTable)
          {
            RectangleF properBounds = parentCell.ProperBounds with
            {
              Height = !parentCell.IsFixedSizeRows ? parentCell.MinHeight : parentCell.DefaultRowSize
            };
            parentCell.AssignProperBounds(properBounds, false, true, true);
          }
        }
      }
      if (removeGridNode && chapter.ListNode != null && chapter.ListNode.TreeList != null)
        chapter.ListNode.TreeList.DeleteNode(chapter.ListNode);
    }
    this.UpdateChapterCaption();
    return keyValuePairList;
  }

  /// <summary>Удалить пустые разделы</summary>
  /// <param name="keepWithDocNode">Сохранять разделы для которых есть узлы документов</param>
  public virtual void RemoveEmptySections(bool keepWithDocNode)
  {
    for (int index = this.chapters.Count - 1; index >= 0; --index)
    {
      if (this.chapters[index] is SpecificationSection chapter)
      {
        if (!keepWithDocNode)
        {
          if (chapter.IsEmpty || chapter.DocNode == null)
            this.RemoveChapter((Chapter) chapter, false, false, true, this.avsDocument.IsGridViewMode);
          else
            chapter.RemoveEmptySectionDocNodes(chapter.DocNodes, EmptyRowUpdateMode.Delete);
        }
      }
      else
        this.chapters[index].RemoveEmptySections(keepWithDocNode);
    }
  }

  /// <summary>Найти в списке дочерних частей (разделов, исполнений), часть с некоторым наименованием</summary>
  public Chapter FindChildChapterByCaption(string attrValueStr)
  {
    attrValueStr = attrValueStr.Trim();
    foreach (Chapter chapter in this.chapters)
    {
      if (chapter.Caption.Trim().Equals(attrValueStr))
        return chapter;
    }
    return (Chapter) null;
  }

  /// <summary>Найти в списке дочерних частей (разделов, исполнений), часть с некоторым идентификатором</summary>
  public Chapter FindChildChapterByID(long id)
  {
    foreach (Chapter chapter in this.chapters)
    {
      if (chapter.ChapterID == id)
        return chapter;
    }
    return (Chapter) null;
  }

  /// <summary>Номер исполнения</summary>
  [Browsable(false)]
  internal int ProductNumber
  {
    get
    {
      int result = 0;
      if (this.product != null && !int.TryParse(this.product.GetNumber(this.AVSDocument.DocumentDesignation, true), out result))
        result = 0;
      return result;
    }
  }

  /// <summary>Начинать ли запись с новой страницы </summary>
  [DefaultValue(false)]
  [Description("Начинать ли раздел с новой страницы")]
  [DisplayName("C новой страницы")]
  [Category("Страницы")]
  [TypeConverter(typeof (CustomBooleanNullableConverter))]
  public virtual bool? FromNewPage
  {
    [DebuggerStepThrough] get
    {
      return this.DocNode != null ? new bool?(this.DocNode.FromNewPage) : new bool?();
    }
    set
    {
      for (int index = 0; index < this.docNodes.Count; ++index)
      {
        if (value.HasValue)
        {
          this.docNodes[index].SetFromNewPage(value.Value, false, false);
        }
        else
        {
          this.docNodes[index].overrideFlags |= OverrideFlags.FromNewPage;
          this.docNodes[index].ApplyTemplateProperties(false, false);
        }
      }
      this.avsDocument.Document.UpdateLayout(true);
    }
  }

  /// <summary>Игнорировать пропуски в начале страницы</summary>
  [DefaultValue(true)]
  [Description("Игнорировать пропуски в начале страницы")]
  [DisplayName("Игнорировать пропуски строк перед записью в начале страницы")]
  [Category("Пропуск строк")]
  [TypeConverter(typeof (CustomBooleanNullableConverter))]
  public virtual bool? NonSkipBeforeAtStartPage
  {
    [DebuggerStepThrough] get
    {
      if (this.DocNodes.Count > 0)
      {
        TableData tableData = this.GetChapterCaptionRow(this.docNodes[0]) ?? this.docNodes[0];
        if (tableData != null && (tableData.IsOverridden3(OverrideFlags3.NonSkipBeforeAtStartPage) || tableData.Template != null && tableData.Template.IsOverridden3(OverrideFlags3.NonSkipBeforeAtStartPage)))
          return new bool?(tableData.NonSkipBeforeAtStartPage);
      }
      return new bool?();
    }
    set
    {
      foreach (TableData docNode in this.docNodes)
      {
        TableData tableData = this.GetChapterCaptionRow(docNode) ?? docNode;
        if (value.HasValue)
        {
          bool beforeAtStartPage = tableData.NonSkipBeforeAtStartPage;
          if (tableData.NonSkipBeforeAtStartPage != beforeAtStartPage)
          {
            tableData.SetNonSkipBeforeAtStartPage(value.Value, false, false, false);
          }
          else
          {
            tableData.overrideFlags3 |= OverrideFlags3.NonSkipBeforeAtStartPage;
            tableData.SetNonSkipBeforeAtStartPage(value.Value, false, false, false);
            tableData.SetNeedUpdateLayoutFlag(true, true, false, false);
          }
        }
        else
        {
          tableData.overrideFlags3 &= ~OverrideFlags3.NonSkipBeforeAtStartPage;
          tableData.SetNeedUpdateLayoutFlag(true, true, false, false);
        }
      }
      if (this.docNodes.Count <= 0)
        return;
      this.avsDocument.Document.UpdateLayout(false);
      this.avsDocument.UpdateProductHeadersOnPages(true, true);
    }
  }

  /// <summary>Получить IEnumerator для сквозного цикла по записям спецификации всех внутренних подразделов</summary>
  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

  /// <summary>Получить IEnumerator для сквозного цикла по записям спецификации всех внутренних подразделов</summary>
  public virtual IEnumerator<AVSRow> GetEnumerator()
  {
    return (IEnumerator<AVSRow>) new ChapterRowEnumerator(this);
  }

  /// <summary>Сравнить заданный подраздел с этим подразделом. Для сортировки</summary>
  /// <param name="obj">Подраздел</param>
  /// <returns>Возвращает значение меньше нуля, если этот экземпляр меньше, чем аргумент.
  /// Возвращает значение равное нулю, если этот экземпляр равен аргументу.
  /// Возвращает значение больше нуля, если этот экземпляр больше, чем аргумент.</returns>
  public int CompareTo(object obj)
  {
    if (obj == null)
      throw new ArgumentNullException(nameof (obj));
    if (!(obj is Chapter chapter))
      return 1;
    int num1 = this.ChapterSortIndex.CompareTo(chapter.ChapterSortIndex);
    if (num1 != 0)
      return num1;
    int num2 = this.SortIndex.CompareTo(chapter.SortIndex);
    return num2 != 0 ? num2 : string.Compare(this.Caption, chapter.Caption);
  }
}
