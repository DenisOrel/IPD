// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.DocumentTreeNode
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using Intermech.Diagnostics;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Базовый класс узла дерева документа</summary>
[TypeConverter(typeof (DocumentTreeNodeConverter))]
[Serializable]
public abstract class DocumentTreeNode : 
  IDisposable,
  ICustomTypeDescriptor,
  ICloneable,
  IDeserializationCallback,
  ISerializable,
  IWriteReadXml,
  IUnknownXmlElement
{
  /// <summary>Текущая версия формата файла XML</summary>
  public static readonly int FileVersion = 44;
  /// <summary>Делегат проверки возможности вызова внешнего редактора для элемента документа</summary>
  [NonSerialized]
  public CanCallDocNodeEditorDelegate CanCallExternalEditor;
  /// <summary>Делегат вызова внешнего редактора для элемента документа</summary>
  [NonSerialized]
  public CallDocNodeEditorDelegate CallExternalEditor;
  private int index = -1;
  [NonSerialized]
  private BeforeAddChildNode_EventHandler beforeAddChildNode;
  [NonSerialized]
  private ChildNodeAdded_EventHandler childNodeAdded;
  [NonSerialized]
  private ChildNodeAdded_EventHandler treeNodeAdded;
  [NonSerialized]
  private BeforeRemoveChildNode_EventHandler beforeRemoveChildNode;
  [NonSerialized]
  private ChildNodeRemoved_EventHandler childNodeRemoved;
  [NonSerialized]
  private ChildNodeRemoved_EventHandler treeNodeRemoved;
  [NonSerialized]
  private NodeRemoved_EventHandler branchRemoved;
  [NonSerialized]
  private NodeRemoved_EventHandler nodeRemoved;
  [NonSerialized]
  private ParentChanged_EventHandler parentChanged;
  [NonSerialized]
  private NameChanged_EventHandler nameChanged;
  [NonSerialized]
  private Changed_EventHandler changed;
  [NonSerialized]
  private ChildNodesPositionExchanged_EventHandler childNodesPositionExchanged;
  [NonSerialized]
  private ChildNodePositionChanged_EventHandler childNodePositionСhanged;
  [NonSerialized]
  private StructureChanging_EventHandler beginStructureChanging;
  [NonSerialized]
  private StructureChanging_EventHandler endStructureChanging;
  /// <summary>Кэш для Regex</summary>
  internal static Regex TypeNameFromXmlAttr_RegEx = (Regex) null;
  /// <summary>Кэш для Regex</summary>
  internal static Regex BaseTypeNameFromXmlAttr_RegEx = (Regex) null;
  /// <summary>Кэш для Regex</summary>
  internal static Regex ParseAttr_RegEx = (Regex) null;
  protected static Dictionary<string, ReadFieldFromXmlDelegate> ReadFieldsDict;
  internal static StringDictionary VirtualAttributeNameDict_Loc_Inv;
  internal static StringDictionary VirtualAttributeNameDict_Inv_Loc;
  private static bool debugCheckParentCircle = true;
  /// <summary>Словарь атрибутов свойств, которые нужно перекрыть</summary>
  public static IDictionary OverridePropertyAttributes = (IDictionary) null;
  /// <summary>Словарь имен типов</summary>
  private static IDictionary typeNameDictionary;
  /// <summary>Словарь псевдонимов для типов</summary>
  private static Dictionary<Type, string> typeAliasDictionary;
  /// <summary>Словарь конструкторов типов</summary>
  private static IDictionary typeConstructorDictionary;
  /// <summary>Имя атрибута Name</summary>
  public static readonly string AttributeName_Name = LocalizationHolder.rm.GetString("Interfaces.Document_19");
  /// <summary>Имя атрибута Text</summary>
  public static readonly string AttributeName_Text = LocalizationHolder.rm.GetString("Interfaces.Document_150");
  /// <summary>Имя атрибута PageNumber</summary>
  public static readonly string AttributeName_DocPageNumber = LocalizationHolder.rm.GetString("Interfaces.Document_72");
  /// <summary>Имя атрибута PageNumber</summary>
  public static readonly string AttributeName_ComplectPageNumber = LocalizationHolder.rm.GetString("Interfaces.Document_161");
  /// <summary>Имя атрибута PageNumberMore</summary>
  public static readonly string AttributeName_PageNumberMore1 = LocalizationHolder.rm.GetString("Interfaces.Document_73");
  /// <summary>Имя атрибута HierarchicalPageNumber</summary>
  public static readonly string AttributeName_HierarchicalPageNumber = LocalizationHolder.rm.GetString("Interfaces.Document_721");
  /// <summary>Имя атрибута DocPageCount</summary>
  public static readonly string AttributeName_DocPageCount = LocalizationHolder.rm.GetString("Interfaces.Document_60");
  /// <summary>Имя атрибута LastDocPageNumber. Используется вместо реального количества страниц, когда номер первой страницы не 1</summary>
  public static readonly string AttributeName_LastDocPageNumber = LocalizationHolder.rm.GetString("Interfaces.Document_199");
  /// <summary>Имя атрибута ComplectPageCount</summary>
  public static readonly string AttributeName_ComplectPageCount = LocalizationHolder.rm.GetString("Interfaces.Document_160");
  /// <summary>Имя атрибута Designation</summary>
  public static readonly string AttributeName_Designation = LocalizationHolder.rm.GetString("Interfaces.Document_155");
  /// <summary>Имя атрибута  контрольная сумма</summary>
  public static readonly string AttributeName_CheckSum = LocalizationHolder.rm.GetString("Interfaces.Document_197");
  public static readonly string AttributeName_PrintUser = LocalizationHolder.rm.GetString("Interfaces.Document_200");
  public static readonly string AttributeName_PrintDate = LocalizationHolder.rm.GetString("Interfaces.Document_201");
  public static readonly string VirtualAttributeName_FileName = "Имя файла";
  public static readonly string VirtualAttributeName_FileSize = "Размер файла";
  public static readonly string VirtualAttributeName_FileModifyDate = "Дата модификации файла";
  /// <summary>Имя атрибута составное обозначение</summary>
  public static readonly string AttributeName_ComplexDesignation = "ComplexDesignation";
  /// <summary>
  /// В документе есть атрибуты требующие вычисления контрольной суммы
  /// </summary>
  public static readonly string AttributeName_DocumentHasCheckSum = nameof (AttributeName_DocumentHasCheckSum);
  /// <summary>Имя атрибута DocName</summary>
  public static readonly string AttributeName_DocName = LocalizationHolder.rm.GetString("Interfaces.Document_156");
  /// <summary>Имя атрибута FileName</summary>
  public static readonly string AttributeName_FileName = "FileName";
  /// <summary>Имя специального атрибута NBreakTxt</summary>
  public static readonly string AttributeName_NBreakTxt = "NBreakTxt";
  public static readonly string AttributeName_VersionId = LocalizationHolder.rm.GetString("Interfaces.Document_185");
  /// <summary>Текст заголовка группы, по которому группируются записи</summary>
  public const string AttributeName_GroupHeader = "GroupHeaderText";
  public const string AttributeName_GroupRowCountForDynamicHeader = "GroupHeaderRowCount";
  public const string AttributeName_GroupHeaderTemplate = "GroupHeaderTemplate";
  public const string AttributeName_IsGroupHeader = "GroupHeader";
  /// <summary>Текущее значение текста для вывода в записи, которая может быть сгруппирована</summary>
  public const string AttributeName_GroupHeaderCellText = "GroupHeaderCellText";
  /// <summary>Значение текста для вывода в несгруппированной записи</summary>
  public const string AttributeName_GroupHeaderCellOriginalText = "GroupHeaderCellOriginalText";
  /// <summary>Значение текста для вывода в сгруппированной записи</summary>
  public const string AttributeName_GroupHeaderCellTextForGroup = "GroupHeaderCellTextForGroup";
  /// <summary>
  /// Атрибут для хранения идентификатора элемента из файла BLN.
  /// Сохраняется при конвертации.
  /// </summary>
  public const string AttributeName_BlankID = "BLN.ID";
  /// <summary>
  /// Атрибут для хранения имени элемента из файла BLN.
  /// Сохраняется при конвертации.
  /// </summary>
  public const string AttributeName_BlankName = "BLN.NAME";
  /// <summary>
  /// Атрибут для хранения флага CanBeFirst страницы из файла BLN.
  /// Сохраняется при конвертации.
  /// </summary>
  public const string AttributeName_BlankPageCanBeFirst = "BLN.CanBeFirst";
  /// <summary>
  /// Атрибут для хранения типа элемента из файла BLN.
  /// Сохраняется при конвертации.
  /// </summary>
  public const string AttributeName_BlankType = "BLN.TYPE";
  private static ColorConverter colorConverter = (ColorConverter) null;
  /// <summary> Словарь "эталонных" значений - один для всех узлов документа.
  /// Реальные значения атрибутов хранятся в нестатической коллекции additionalAttributes
  /// и читаются/пишутся/клонируются/сериализуются тоже оттуда (не из propertyBindings).
  ///  </summary>
  protected static readonly Dictionary<string, AddAttrValue> propertyBindings = new Dictionary<string, AddAttrValue>();
  /// <summary>Имя узла</summary>
  protected string name;
  /// <summary>Идентификатор узла</summary>
  protected string id;
  /// <summary>Коллекция атрибутов узла</summary>
  private AdditionalAttributeCollection additionalAttributes;
  /// <summary>Ссылка на шаблон узла</summary>
  internal ReferenceToTemplate referenceToTemplate;
  /// <summary>Виртуальный узел</summary>
  protected bool isVirtualNode;
  /// <summary>Клонировать узел по шаблону вместе с родителем</summary>
  protected bool cloneByTemplateWithParent = true;
  /// <summary>Узел был клонирован по шаблону вместе с родителем</summary>
  protected bool clonedByTemplateWithParent;
  private List<StringKeyValue> unknownXmlAttributes;
  private string unknownXmlElements;
  /// <summary>Коллекция дочерних узлов</summary>
  [ChildLink]
  protected DocumentTreeNodeCollection nodes;
  /// <summary>Флаги для внутреннего пользования, вместо нескольких булевских полей</summary>
  [NonSerialized]
  protected byte flags;
  /// <summary>Свойства были изменены</summary>
  protected const byte Flag_PropertiesChanged = 1;
  /// <summary>Структура дерева была изменена</summary>
  protected const byte Flag_TreeStructureChanged = 2;
  public const byte Flag_AllowAutoWidthEditor = 8;
  protected const byte Flag_ReplaceAVSMaterial = 32 /*0x20*/;
  protected const byte Flag_NonSkipBeforeAtStartPage = 64 /*0x40*/;
  /// <summary>В текстовом элементе есть формулы</summary>
  public const byte Flag_ElementHasFormulas = 128 /*0x80*/;
  [NonSerialized]
  protected bool needUpdateLayoutFlag;
  [NonSerialized]
  private PropertyDescriptorCollection globalizedProps;
  /// <summary>Количество начатых изменений</summary>
  [NonSerialized]
  private int changingCount;
  /// <summary>Сервис уникальных идентификаторов</summary>
  [NonSerialized]
  internal IUniqueIdService idService;
  /// <summary>Родительский узел</summary>
  [NonSerialized]
  protected DocumentTreeNode parent;
  /// <summary>Количество начатых изменений структуры</summary>
  [NonSerialized]
  private int changingStructureCount;
  /// <summary>Коллекция ссылок на этот узел</summary>
  [NonSerialized]
  internal List<ReferenceToNode> connectionList;
  /// <summary>Счетчик блокировок применения шаблона</summary>
  [NonSerialized]
  private int suspendApplyThisTemplateCount;
  /// <summary>Счетчик блокировок обновления представления данных</summary>
  [NonSerialized]
  internal int suspendUpdateLayoutCount;
  /// <summary>Обработчик события AttributeValueChanging</summary>
  [NonSerialized]
  private AttributeValueChanging_EventHandler attributeValueChanging_EventHandler;
  /// <summary>Обработчик события AttributeValueChanged</summary>
  [NonSerialized]
  private AttributeValueChanged_EventHandler attributeValueChanged_EventHandler;
  /// <summary>Обработчик события AttributeRemoving</summary>
  [NonSerialized]
  private AttributeRemoving_EventHandler attributeValueRemoving_EventHandler;
  /// <summary>Обработчик события AttributeRemoved</summary>
  [NonSerialized]
  private AttributeRemoved_EventHandler attributeValueRemoved_EventHandler;
  /// <summary>Обработчик события GetVirtualAttributeNames для плагинов</summary>
  [NonSerialized]
  private GetPluginVirtualAttributeNames_EventHandler getPluginVirtualAttributeNames;
  /// <summary>Обработчик события GetVirtualAttributeNames для плагинов</summary>
  [NonSerialized]
  private GetPluginVirtualAttributeValue_EventHandler getPluginVirtualAttributeValue;
  public OverrideFlags overrideFlags;
  public OverrideFlags2 overrideFlags2;
  public OverrideFlags3 overrideFlags3;

  /// <summary>Инициализировать поля объекта</summary>
  protected virtual void InitFields()
  {
  }

  /// <summary>Конструктор</summary>
  protected DocumentTreeNode() => this.InitFields();

  /// <summary>Конструктор</summary>
  /// <param name="initFields">Вызывать метод инициализации полей InitFields()</param>
  protected DocumentTreeNode(bool initFields)
  {
    if (!initFields)
      return;
    this.InitFields();
  }

  static DocumentTreeNode()
  {
    DocumentTreeNode.InitReadFieldDict();
    DocumentTreeNode.VirtualAttributeNameDict_Inv_Loc = new StringDictionary();
    DocumentTreeNode.VirtualAttributeNameDict_Loc_Inv = new StringDictionary();
    DocumentTreeNode.VirtualAttributeNameDict_Inv_Loc.Add(nameof (Name), DocumentTreeNode.AttributeName_Name);
    DocumentTreeNode.VirtualAttributeNameDict_Loc_Inv.Add(DocumentTreeNode.AttributeName_Name, nameof (Name));
    if (DocumentTreeNode.AttributeName_Name != LocalizationHolder.rm.GetString("Interfaces.Document_169"))
      DocumentTreeNode.VirtualAttributeNameDict_Loc_Inv.Add(LocalizationHolder.rm.GetString("Interfaces.Document_169"), nameof (Name));
    DocumentTreeNode.VirtualAttributeNameDict_Inv_Loc.Add("Text", DocumentTreeNode.AttributeName_Text);
    DocumentTreeNode.VirtualAttributeNameDict_Loc_Inv.Add(DocumentTreeNode.AttributeName_Text, "Text");
    if (DocumentTreeNode.AttributeName_Text != LocalizationHolder.rm.GetString("Interfaces.Document_170"))
      DocumentTreeNode.VirtualAttributeNameDict_Loc_Inv.Add(LocalizationHolder.rm.GetString("Interfaces.Document_170"), "Text");
    DocumentTreeNode.VirtualAttributeNameDict_Inv_Loc.Add("PageNumber", DocumentTreeNode.AttributeName_DocPageNumber);
    DocumentTreeNode.VirtualAttributeNameDict_Loc_Inv.Add(DocumentTreeNode.AttributeName_DocPageNumber, "PageNumber");
    if (DocumentTreeNode.AttributeName_DocPageNumber != LocalizationHolder.rm.GetString("Interfaces.Document_171"))
      DocumentTreeNode.VirtualAttributeNameDict_Loc_Inv.Add(LocalizationHolder.rm.GetString("Interfaces.Document_171"), "PageNumber");
    DocumentTreeNode.VirtualAttributeNameDict_Inv_Loc.Add("ComplectPageNumber", DocumentTreeNode.AttributeName_ComplectPageNumber);
    DocumentTreeNode.VirtualAttributeNameDict_Loc_Inv.Add(DocumentTreeNode.AttributeName_ComplectPageNumber, "ComplectPageNumber");
    DocumentTreeNode.VirtualAttributeNameDict_Inv_Loc.Add("PageNumberMore1", DocumentTreeNode.AttributeName_PageNumberMore1);
    DocumentTreeNode.VirtualAttributeNameDict_Loc_Inv.Add(DocumentTreeNode.AttributeName_PageNumberMore1, "PageNumberMore1");
    if (DocumentTreeNode.AttributeName_PageNumberMore1 != LocalizationHolder.rm.GetString("Interfaces.Document_172"))
      DocumentTreeNode.VirtualAttributeNameDict_Loc_Inv.Add(LocalizationHolder.rm.GetString("Interfaces.Document_172"), "PageNumberMore1");
    DocumentTreeNode.VirtualAttributeNameDict_Inv_Loc.Add("PageCount", DocumentTreeNode.AttributeName_DocPageCount);
    DocumentTreeNode.VirtualAttributeNameDict_Loc_Inv.Add(DocumentTreeNode.AttributeName_DocPageCount, "PageCount");
    if (DocumentTreeNode.AttributeName_DocPageCount != LocalizationHolder.rm.GetString("Interfaces.Document_173"))
      DocumentTreeNode.VirtualAttributeNameDict_Loc_Inv.Add(LocalizationHolder.rm.GetString("Interfaces.Document_173"), "PageCount");
    DocumentTreeNode.VirtualAttributeNameDict_Inv_Loc.Add("LastDocPageNumber", DocumentTreeNode.AttributeName_LastDocPageNumber);
    DocumentTreeNode.VirtualAttributeNameDict_Loc_Inv.Add(DocumentTreeNode.AttributeName_LastDocPageNumber, "LastDocPageNumber");
    DocumentTreeNode.VirtualAttributeNameDict_Inv_Loc.Add("ComplectPageCount", DocumentTreeNode.AttributeName_ComplectPageCount);
    DocumentTreeNode.VirtualAttributeNameDict_Loc_Inv.Add(DocumentTreeNode.AttributeName_ComplectPageCount, "ComplectPageCount");
    DocumentTreeNode.VirtualAttributeNameDict_Inv_Loc.Add("CheckSum", DocumentTreeNode.AttributeName_CheckSum);
    DocumentTreeNode.VirtualAttributeNameDict_Loc_Inv.Add(DocumentTreeNode.AttributeName_CheckSum, "CheckSum");
    DocumentTreeNode.VirtualAttributeNameDict_Inv_Loc.Add("PrintUser", DocumentTreeNode.AttributeName_PrintUser);
    DocumentTreeNode.VirtualAttributeNameDict_Loc_Inv.Add(DocumentTreeNode.AttributeName_PrintUser, "PrintUser");
    DocumentTreeNode.VirtualAttributeNameDict_Inv_Loc.Add("PrintDate", DocumentTreeNode.AttributeName_PrintDate);
    DocumentTreeNode.VirtualAttributeNameDict_Loc_Inv.Add(DocumentTreeNode.AttributeName_PrintDate, "PrintDate");
    DocumentTreeNode.VirtualAttributeNameDict_Inv_Loc.Add("Designation", DocumentTreeNode.AttributeName_Designation);
    DocumentTreeNode.VirtualAttributeNameDict_Loc_Inv.Add(DocumentTreeNode.AttributeName_Designation, "Designation");
    DocumentTreeNode.VirtualAttributeNameDict_Inv_Loc.Add("Title", DocumentTreeNode.AttributeName_DocName);
    DocumentTreeNode.VirtualAttributeNameDict_Loc_Inv.Add(DocumentTreeNode.AttributeName_DocName, "Title");
  }

  /// <summary>Создать пустой экземпляр класса с инициализацией только самых неоходимых полей</summary>
  /// <param name="element">Ссылка на новый экземпляр класса, элемент создается
  /// если на входе element равен null, иначе, считается, что он был создан
  /// в перекрытом методе наследника</param>
  public virtual void CreateEmptyElement(ref DocumentTreeNode element)
  {
    if (element != null)
      return;
    element = (DocumentTreeNode) Activator.CreateInstance(this.GetType(), true);
  }

  /// <summary>Инициализация словарей TypeConstructorDictionary и TypeNameDictionary</summary>
  public static void InitTypeNameDictionary()
  {
    bool flag1 = false;
    if (DocumentTreeNode.typeConstructorDictionary == null)
    {
      DocumentTreeNode.typeConstructorDictionary = (IDictionary) new HybridDictionary(10);
      flag1 = true;
    }
    bool flag2 = false;
    if (DocumentTreeNode.typeNameDictionary == null)
    {
      DocumentTreeNode.typeNameDictionary = (IDictionary) new HybridDictionary(10);
      flag2 = true;
    }
    if (DocumentTreeNode.typeAliasDictionary == null)
      DocumentTreeNode.typeAliasDictionary = new Dictionary<Type, string>(10);
    if (flag1)
    {
      DocumentTreeNode.typeConstructorDictionary[(object) DocumentsComplect.TypeNameForConstructorDictionary] = (object) new EmptyConstructorDelegate(DocumentsComplect.EmptyConstructor);
      DocumentTreeNode.typeConstructorDictionary[(object) typeof (ImDocumentData).Name] = (object) new EmptyConstructorDelegate(ImDocumentData.EmptyConstructor);
      DocumentTreeNode.typeConstructorDictionary[(object) ImDocumentData.TypeNameForConstructorDictionary] = (object) new EmptyConstructorDelegate(ImDocumentData.EmptyConstructor);
      DocumentTreeNode.typeConstructorDictionary[(object) typeof (PageData).Name] = (object) new EmptyConstructorDelegate(PageData.EmptyConstructor);
      DocumentTreeNode.typeConstructorDictionary[(object) PageData.TypeNameForConstructorDictionary] = (object) new EmptyConstructorDelegate(PageData.EmptyConstructor);
      DocumentTreeNode.typeConstructorDictionary[(object) typeof (TableData).Name] = (object) new EmptyConstructorDelegate(TableData.EmptyConstructor);
      DocumentTreeNode.typeConstructorDictionary[(object) TableData.TypeNameForConstructorDictionary] = (object) new EmptyConstructorDelegate(TableData.EmptyConstructor);
      DocumentTreeNode.typeConstructorDictionary[(object) typeof (TextData).Name] = (object) new EmptyConstructorDelegate(TextData.EmptyConstructor);
    }
    Type type1 = typeof (UnknownReferenceToObject);
    if (flag2 || !DocumentTreeNode.typeNameDictionary.Contains((object) type1.Name))
      DocumentTreeNode.typeNameDictionary[(object) type1.Name] = (object) type1;
    if (flag1 || !DocumentTreeNode.typeConstructorDictionary.Contains((object) type1.Name))
      DocumentTreeNode.typeConstructorDictionary[(object) type1.Name] = (object) new EmptyConstructorDelegate(UnknownReferenceToObject.EmptyConstructor);
    Type type2 = typeof (UnknownReferenceToTextSource);
    if (flag2 || !DocumentTreeNode.typeNameDictionary.Contains((object) type2.Name))
      DocumentTreeNode.typeNameDictionary[(object) type2.Name] = (object) type2;
    if (flag1 || !DocumentTreeNode.typeConstructorDictionary.Contains((object) type2.Name))
      DocumentTreeNode.typeConstructorDictionary[(object) type2.Name] = (object) new EmptyConstructorDelegate(UnknownReferenceToTextSource.EmptyConstructor);
    Type type3 = typeof (ReferenceToDBObjectBase);
    if (flag2 || !DocumentTreeNode.typeNameDictionary.Contains((object) type3.Name))
      DocumentTreeNode.typeNameDictionary[(object) type3.Name] = (object) type3;
    if (flag2 || !DocumentTreeNode.typeNameDictionary.Contains((object) ReferenceToDBObjectBase.XmlTypeName))
      DocumentTreeNode.typeNameDictionary[(object) ReferenceToDBObjectBase.XmlTypeName] = (object) type3;
    if (flag1 || !DocumentTreeNode.typeConstructorDictionary.Contains((object) type3.Name))
      DocumentTreeNode.typeConstructorDictionary[(object) type3.Name] = (object) new EmptyConstructorDelegate(ReferenceToDBObjectBase.EmptyConstructor);
    if (flag1 || !DocumentTreeNode.typeConstructorDictionary.Contains((object) ReferenceToDBObjectBase.XmlTypeName))
      DocumentTreeNode.typeConstructorDictionary[(object) ReferenceToDBObjectBase.XmlTypeName] = (object) new EmptyConstructorDelegate(ReferenceToDBObjectBase.EmptyConstructor);
    Type type4 = typeof (ReferenceToDBObjectAttributeBase);
    if (flag2 || !DocumentTreeNode.typeNameDictionary.Contains((object) type4.Name))
      DocumentTreeNode.typeNameDictionary[(object) type4.Name] = (object) type4;
    if (flag2 || !DocumentTreeNode.typeNameDictionary.Contains((object) ReferenceToDBObjectAttributeBase.XmlTypeName))
      DocumentTreeNode.typeNameDictionary[(object) ReferenceToDBObjectAttributeBase.XmlTypeName] = (object) type4;
    if (flag1 || !DocumentTreeNode.typeConstructorDictionary.Contains((object) type4.Name))
      DocumentTreeNode.typeConstructorDictionary[(object) type4.Name] = (object) new EmptyConstructorDelegate(ReferenceToDBObjectAttributeBase.EmptyConstructor);
    if (flag1 || !DocumentTreeNode.typeConstructorDictionary.Contains((object) ReferenceToDBObjectAttributeBase.XmlTypeName))
      DocumentTreeNode.typeConstructorDictionary[(object) ReferenceToDBObjectAttributeBase.XmlTypeName] = (object) new EmptyConstructorDelegate(ReferenceToDBObjectAttributeBase.EmptyConstructor);
    Type type5 = typeof (ReferenceToSignBase);
    if (flag2 || !DocumentTreeNode.typeNameDictionary.Contains((object) type5.Name))
      DocumentTreeNode.typeNameDictionary[(object) type5.Name] = (object) type5;
    if (flag2 || !DocumentTreeNode.typeNameDictionary.Contains((object) ReferenceToSignBase.XmlTypeName))
      DocumentTreeNode.typeNameDictionary[(object) ReferenceToSignBase.XmlTypeName] = (object) type5;
    if (flag1 || !DocumentTreeNode.typeConstructorDictionary.Contains((object) type5.Name))
      DocumentTreeNode.typeConstructorDictionary[(object) type5.Name] = (object) new EmptyConstructorDelegate(ReferenceToSignBase.EmptyConstructor);
    if (flag1 || !DocumentTreeNode.typeConstructorDictionary.Contains((object) ReferenceToSignBase.XmlTypeName))
      DocumentTreeNode.typeConstructorDictionary[(object) ReferenceToSignBase.XmlTypeName] = (object) new EmptyConstructorDelegate(ReferenceToSignBase.EmptyConstructor);
    Type type6 = typeof (ReferenceToGraphicsBase);
    if (flag2 || !DocumentTreeNode.typeNameDictionary.Contains((object) type6.Name))
      DocumentTreeNode.typeNameDictionary[(object) type6.Name] = (object) type6;
    if (flag2 || !DocumentTreeNode.typeNameDictionary.Contains((object) ReferenceToGraphicsBase.XmlTypeName))
      DocumentTreeNode.typeNameDictionary[(object) ReferenceToGraphicsBase.XmlTypeName] = (object) type6;
    if (flag1 || !DocumentTreeNode.typeConstructorDictionary.Contains((object) type6.Name))
      DocumentTreeNode.typeConstructorDictionary[(object) type6.Name] = (object) new EmptyConstructorDelegate(ReferenceToGraphicsBase.EmptyConstructor);
    if (!flag1 && DocumentTreeNode.typeConstructorDictionary.Contains((object) ReferenceToGraphicsBase.XmlTypeName))
      return;
    DocumentTreeNode.typeConstructorDictionary[(object) ReferenceToGraphicsBase.XmlTypeName] = (object) new EmptyConstructorDelegate(ReferenceToGraphicsBase.EmptyConstructor);
  }

  /// <summary>Словарь имен типов</summary>
  public static IDictionary TypeNameDictionary
  {
    [DebuggerStepThrough] get
    {
      if (DocumentTreeNode.typeNameDictionary == null)
        DocumentTreeNode.InitTypeNameDictionary();
      return DocumentTreeNode.typeNameDictionary;
    }
    set => DocumentTreeNode.typeNameDictionary = value;
  }

  /// <summary>Словарь имен типов</summary>
  public static Dictionary<Type, string> TypeAliasDictionary
  {
    [DebuggerStepThrough] get
    {
      if (DocumentTreeNode.typeAliasDictionary == null)
        DocumentTreeNode.InitTypeNameDictionary();
      return DocumentTreeNode.typeAliasDictionary;
    }
    set => DocumentTreeNode.typeAliasDictionary = value;
  }

  /// <summary>Словарь конструкторов типов</summary>
  public static IDictionary TypeConstructorDictionary
  {
    [DebuggerStepThrough] get
    {
      if (DocumentTreeNode.typeConstructorDictionary == null)
        DocumentTreeNode.InitTypeNameDictionary();
      return DocumentTreeNode.typeConstructorDictionary;
    }
    set => DocumentTreeNode.typeConstructorDictionary = value;
  }

  /// <summary>Возможен вызов дополнительного редактора для элемента</summary>
  [Browsable(false)]
  public virtual bool CanCallEditor
  {
    [DebuggerStepThrough] get
    {
      if (this.CanCallExternalEditor != null)
        return this.CanCallExternalEditor(this);
      if (this.OwnerDocument != null && this.OwnerDocument.ExternalEditor != null)
      {
        if (this.OwnerDocument.ExternalEditor.CanCallEditor(new DocumentTreeNode[1]
        {
          this
        }))
          return true;
      }
      return this.CallExternalEditor != null;
    }
  }

  /// <summary>Вызвать дополнительный редактор для элемента</summary>
  public virtual void CallEditor()
  {
    if (this.CallExternalEditor != null)
    {
      this.CallExternalEditor(this);
    }
    else
    {
      if (this.OwnerDocument == null || this.OwnerDocument.ExternalEditor == null)
        return;
      this.OwnerDocument.ExternalEditor.CallEditor(new DocumentTreeNode[1]
      {
        this
      });
    }
  }

  /// <summary>Отображать узел в дереве</summary>
  [Browsable(false)]
  public virtual bool ShowInTreeView
  {
    [DebuggerStepThrough] get => true;
  }

  /// <summary>Отображать узел в дереве</summary>
  [Browsable(false)]
  public virtual Color HighlightColor { get; set; } = Color.Empty;

  /// <summary>Команда пользователя "Удалить". В общем случае не совпадает с Remove()</summary>
  /// <param name="update">Обновлять внешний вид и разбивку по страницам</param>
  public virtual void UserCommand_Delete(bool update) => this.Remove(update, update);

  /// <summary>Документ владелец</summary>
  [Browsable(false)]
  public virtual ImDocumentData OwnerDocument
  {
    [DebuggerStepThrough] get
    {
      DocumentTreeNode documentTreeNode = this;
      ImDocumentData ownerDocument = (ImDocumentData) null;
      for (; documentTreeNode != null; documentTreeNode = documentTreeNode.Parent)
      {
        if (documentTreeNode is ImDocumentData)
        {
          ownerDocument = documentTreeNode as ImDocumentData;
          break;
        }
      }
      return ownerDocument;
    }
  }

  /// <summary>Для внутреннего использования. Применяется в операциях с выделением,
  /// когда одновременно выбраны и родители и дочерние элементы, а операции нужно выполнить только с родителями.</summary>
  /// <param name="treeNodes">Исходный массив</param>
  /// <returns>Отфильтрованный массив</returns>
  public static DocumentTreeNode[] GetNodesWithoutChilds(DocumentTreeNode[] treeNodes)
  {
    return DocumentTreeNode.GetNodesWithoutChilds(treeNodes, true);
  }

  /// <summary>Для внутреннего использования. Применяется в операциях с выделением,
  /// когда одновременно выбраны и родители и дочерние элементы, а операции нужно выполнить только с родителями.</summary>
  /// <param name="treeNodes">Исходный массив</param>
  /// <param name="includeVirtual">Учитывать виртуальные узлы</param>
  /// <returns>Отфильтрованный массив</returns>
  public static DocumentTreeNode[] GetNodesWithoutChilds(
    DocumentTreeNode[] treeNodes,
    bool includeVirtual)
  {
    if (treeNodes == null)
      return (DocumentTreeNode[]) null;
    DocumentTreeNode[] documentTreeNodeArray = new DocumentTreeNode[treeNodes.Length];
    treeNodes.CopyTo((Array) documentTreeNodeArray, 0);
    for (int index1 = 0; index1 < documentTreeNodeArray.Length; ++index1)
    {
      if (documentTreeNodeArray[index1] != null)
      {
        for (int index2 = 0; index2 < documentTreeNodeArray.Length; ++index2)
        {
          if (index1 != index2 && documentTreeNodeArray[index2] != null && documentTreeNodeArray[index2].IsChildForNode(documentTreeNodeArray[index1], includeVirtual))
            documentTreeNodeArray[index2] = (DocumentTreeNode) null;
        }
      }
    }
    int length = 0;
    for (int index = 0; index < documentTreeNodeArray.Length; ++index)
    {
      if (documentTreeNodeArray[index] != null)
        ++length;
    }
    DocumentTreeNode[] nodesWithoutChilds = new DocumentTreeNode[length];
    int index3 = 0;
    for (int index4 = 0; index4 < documentTreeNodeArray.Length; ++index4)
    {
      if (documentTreeNodeArray[index4] != null)
      {
        nodesWithoutChilds[index3] = documentTreeNodeArray[index4];
        ++index3;
      }
    }
    return nodesWithoutChilds;
  }

  /// <summary>Для внутреннего использования. Применяется в операциях с выделением,
  /// когда одновременно выбраны и родители и дочерние элементы, а операции нужно выполнить только с родителями.</summary>
  /// <param name="treeNodes">Исходная коллекция</param>
  /// <param name="includeVirtual">Учитывать виртуальные узлы</param>
  /// <returns>Отфильтрованная коллекция</returns>
  public static List<DocumentTreeNode> GetNodesWithoutChilds(
    IList<DocumentTreeNode> treeNodes,
    bool includeVirtual)
  {
    if (treeNodes == null)
      return (List<DocumentTreeNode>) null;
    List<DocumentTreeNode> documentTreeNodeList = new List<DocumentTreeNode>((IEnumerable<DocumentTreeNode>) treeNodes);
    for (int index1 = 0; index1 < documentTreeNodeList.Count; ++index1)
    {
      if (documentTreeNodeList[index1] != null)
      {
        for (int index2 = 0; index2 < documentTreeNodeList.Count; ++index2)
        {
          if (index1 != index2 && documentTreeNodeList[index2] != null && documentTreeNodeList[index2].IsChildForNode(documentTreeNodeList[index1], includeVirtual))
            documentTreeNodeList[index2] = (DocumentTreeNode) null;
        }
      }
    }
    int capacity = 0;
    for (int index = 0; index < documentTreeNodeList.Count; ++index)
    {
      if (documentTreeNodeList[index] != null)
        ++capacity;
    }
    List<DocumentTreeNode> nodesWithoutChilds = new List<DocumentTreeNode>(capacity);
    for (int index = 0; index < documentTreeNodeList.Count; ++index)
    {
      if (documentTreeNodeList[index] != null)
        nodesWithoutChilds.Add(documentTreeNodeList[index]);
    }
    return nodesWithoutChilds;
  }

  /// <summary>Получить все дочерние элементы</summary>
  /// <param name="node"></param>
  /// <returns></returns>
  public static List<DocumentTreeNode> GetChildNodes(DocumentTreeNode node)
  {
    List<DocumentTreeNode> childNodes = new List<DocumentTreeNode>();
    if (node.Nodes != null && node.NodesCount > 0)
    {
      childNodes.AddRange((IEnumerable<DocumentTreeNode>) node.Nodes);
      for (int index = 0; index < node.NodesCount; ++index)
        childNodes.AddRange((IEnumerable<DocumentTreeNode>) DocumentTreeNode.GetChildNodes(node.Nodes[index]));
    }
    return childNodes;
  }

  /// <summary>Нахождение элементов виртуальной ячейки</summary>
  /// <returns>список реальных ячеек</returns>
  public List<DocumentTreeNode> GetNodesFromVirtualNode()
  {
    List<DocumentTreeNode> nodesFromVirtualNode = new List<DocumentTreeNode>();
    if (this.IsVirtualNode)
    {
      for (int index = 0; index < this.NodesCount; ++index)
        nodesFromVirtualNode.AddRange((IEnumerable<DocumentTreeNode>) this.GetNodesFromVirtualNode(this.Nodes[index]));
    }
    else
      nodesFromVirtualNode.Add(this);
    return nodesFromVirtualNode;
  }

  /// <summary>Нахождение элементов виртуальной ячейки</summary>
  /// <param name="node">Узел в котором нужно искать</param>
  /// <returns></returns>
  private List<DocumentTreeNode> GetNodesFromVirtualNode(DocumentTreeNode node)
  {
    List<DocumentTreeNode> nodesFromVirtualNode = new List<DocumentTreeNode>();
    if (node.IsVirtualNode)
    {
      for (int index = 0; index < node.NodesCount; ++index)
        nodesFromVirtualNode.AddRange((IEnumerable<DocumentTreeNode>) this.GetNodesFromVirtualNode(node.Nodes[index]));
    }
    else
      nodesFromVirtualNode.Add(node);
    return nodesFromVirtualNode;
  }

  /// <summary>Поиск узла в дереве по идентификатору</summary>
  /// <param name="nodeId">Идентификатор узла</param>
  /// <returns>Возвращает найденный узел, или null, если узел не найден.</returns>
  public virtual DocumentTreeNode FindNode(string nodeId)
  {
    if (string.IsNullOrEmpty(nodeId))
      throw new ArgumentNullException(nameof (nodeId));
    if (nodeId == this.id)
      return this;
    if (this.idService != null)
      return (DocumentTreeNode) this.idService[(object) nodeId];
    for (DocumentTreeNode parent = this.parent; parent != null; parent = parent.parent)
    {
      if (nodeId == parent.Id)
        return parent;
    }
    if (this.nodes != null)
    {
      for (int index = 0; index < this.nodes.Count; ++index)
      {
        DocumentTreeNode node = this.nodes[index].FindNode(nodeId);
        if (node != null)
          return node;
      }
    }
    return (DocumentTreeNode) null;
  }

  /// <summary>Поиск первого узла в дереве по имени</summary>
  /// <param name="nodeName">Имя узла</param>
  /// <returns>Возвращает найденный узел, или null, если узел не найден.</returns>
  public virtual DocumentTreeNode FindFirstNodeByName(string nodeName)
  {
    if (nodeName == "")
      nodeName = (string) null;
    if (nodeName == this.Name)
      return this;
    if (this.nodes != null)
    {
      for (int index = 0; index < this.nodes.Count; ++index)
      {
        DocumentTreeNode firstNodeByName = this.nodes[index].FindFirstNodeByName(nodeName);
        if (firstNodeByName != null)
          return firstNodeByName;
      }
    }
    return (DocumentTreeNode) null;
  }

  public virtual DocumentTreeNode FindParentNodeByNameOrId(string nodeName)
  {
    if (nodeName == "")
      nodeName = (string) null;
    if (nodeName == this.Name)
      return this;
    DocumentTreeNode parent = this.Parent;
    while (parent != null && !(parent.Name == nodeName) && !(parent.id == nodeName))
      parent = parent.Parent;
    return parent;
  }

  /// <summary>Поиск первого дочернего узла по имени</summary>
  /// <param name="nodeName">Имя узла</param>
  /// <returns>Возвращает найденный узел, или null, если узел не найден.</returns>
  public DocumentTreeNode FindFirstChildNodeByName(string nodeName)
  {
    if (this.nodes == null)
      return (DocumentTreeNode) null;
    for (int index = 0; index < this.nodes.Count; ++index)
    {
      if (this.nodes[index].Name == nodeName)
        return this.nodes[index];
    }
    return (DocumentTreeNode) null;
  }

  /// <summary>Поиск узла в дереве по идентификатору и типу</summary>
  /// <param name="nodeId">Идентификатор узла</param>
  /// <param name="type">Тип узла</param>
  /// <returns>Возвращает найденный узел, или null, если узел не найден.</returns>
  public virtual DocumentTreeNode FindNode(string nodeId, Type type)
  {
    if (nodeId == "")
      nodeId = (string) null;
    if (nodeId == this.id && type.IsInstanceOfType((object) this))
      return this;
    if (this.nodes != null)
    {
      for (int index = 0; index < this.nodes.Count; ++index)
      {
        DocumentTreeNode node = this.nodes[index].FindNode(nodeId, type);
        if (node != null)
          return node;
      }
    }
    return (DocumentTreeNode) null;
  }

  /// <summary>Поиск узлов в дереве по типу</summary>
  /// <param name="type">Тип узла</param>
  /// <param name="foundNodes">Найденные узлы</param>
  public virtual void FindNodes(Type type, List<DocumentTreeNode> foundNodes)
  {
    if (type.IsInstanceOfType((object) this))
      foundNodes.Add(this);
    if (this.nodes == null)
      return;
    for (int index = 0; index < this.nodes.Count; ++index)
      this.nodes[index].FindNodes(type, foundNodes);
  }

  /// <summary>Поиск узла в дереве по условию</summary>
  /// <param name="condition">Условие поиска</param>
  /// <param name="conditionValue">Значение используемое в условии поиска</param>
  /// <returns>Возвращает первый узел удовлетворяющий заданному условию</returns>
  public virtual DocumentTreeNode FindNode(FindCondition condition, object conditionValue)
  {
    if (condition == null)
      throw new ArgumentNullException(nameof (condition));
    if (condition(this, conditionValue))
      return this;
    if (this.nodes != null)
    {
      for (int index = 0; index < this.nodes.Count; ++index)
      {
        DocumentTreeNode node = this.nodes[index].FindNode(condition, conditionValue);
        if (node != null)
          return node;
      }
    }
    return (DocumentTreeNode) null;
  }

  /// <summary>Поиск узлов в дереве по условию</summary>
  /// <param name="condition">Условие поиска</param>
  /// <param name="conditionValue">Значение используемое в условии поиска</param>
  /// <param name="foundNodes">Узлы удовлетворяющие заданному условию</param>
  public virtual void FindNodes(
    FindCondition condition,
    object conditionValue,
    List<DocumentTreeNode> foundNodes)
  {
    if (condition == null)
      throw new ArgumentNullException(nameof (condition));
    if (condition(this, conditionValue))
      foundNodes.Add(this);
    if (this.nodes == null)
      return;
    for (int index = 0; index < this.nodes.Count; ++index)
      this.nodes[index].FindNodes(condition, conditionValue, foundNodes);
  }

  /// <summary>Поиск узлов в дереве по значению атрибута</summary>
  /// <param name="attributeName">Имя атрибута</param>
  /// <param name="attributeValue">Значение атрибута</param>
  /// <param name="foundNodes">Найденные узлы</param>
  public virtual void FindNodes(
    string attributeName,
    string attributeValue,
    List<DocumentTreeNode> foundNodes)
  {
    if (attributeName == null || attributeName == "")
      throw new ArgumentNullException(nameof (attributeName));
    if (this.GetAttributeValue(attributeName, false) == attributeValue)
      foundNodes.Add(this);
    if (this.nodes == null)
      return;
    for (int index = 0; index < this.nodes.Count; ++index)
      this.nodes[index].FindNodes(attributeName, attributeValue, foundNodes);
  }

  /// <summary>Поиск всех узлов созданных по заданному шаблону</summary>
  /// <param name="nodeTemplate">Шаблон</param>
  /// <param name="foundNodes">Найденные узлы</param>
  public virtual void FindNodesFromTemplate(
    DocumentTreeNode nodeTemplate,
    List<DocumentTreeNode> foundNodes)
  {
    if (nodeTemplate == this.Template)
      foundNodes.Add(this);
    if (this.nodes == null)
      return;
    for (int index = 0; index < this.nodes.Count; ++index)
      this.nodes[index].FindNodesFromTemplate(nodeTemplate, foundNodes);
  }

  /// <summary>Найти первый узел созданный по шаблону</summary>
  /// <remarks>В отличии от FindNodesFromTemplate находит первый узел и прекращает поиск.
  /// Использутеся когда не должно быть более одного шаблона</remarks>
  /// <param name="nodeTemplate">Шаблон</param>
  /// <returns>Возвращает найденный узел, или null, если узел не найден.</returns>
  public virtual DocumentTreeNode FindFirstNodeFromTemplate(string nodeTemplateId)
  {
    if (nodeTemplateId == null || nodeTemplateId == "")
      throw new ArgumentNullException(nameof (nodeTemplateId));
    if (nodeTemplateId == this.TemplateId)
      return this;
    DocumentTreeNode nodeFromTemplate = (DocumentTreeNode) null;
    if (this.TemplateRoot != null)
    {
      DocumentTreeNode node = this.TemplateRoot.FindNode(nodeTemplateId);
      if (node != null)
        nodeFromTemplate = this.FindFirstNodeFromTemplate(node);
    }
    else
      nodeFromTemplate = this.FindFirstNodeFromTemplate_Recursive(nodeTemplateId);
    return nodeFromTemplate;
  }

  /// <summary>Найти первый узел созданный по шаблону. Искать только внутри данной ветки дерева</summary>
  /// <remarks>В отличии от FindNodesFromTemplate находит первый узел и прекращает поиск.
  /// Использутеся когда не должно быть более одного шаблона.
  /// Обходит рекурсивно всю данную ветку дерева</remarks>
  /// <param name="nodeTemplate">Шаблон</param>
  /// <returns>Возвращает найденный узел, или null, если узел не найден.</returns>
  public DocumentTreeNode FindFirstNodeFromTemplate_Recursive(string nodeTemplateId)
  {
    return this.FindFirstNodeFromTemplate_Recursive(nodeTemplateId, false);
  }

  /// <summary>Найти первый узел созданный по шаблону. Искать только внутри данной ветки дерева</summary>
  /// <remarks>В отличии от FindNodesFromTemplate находит первый узел и прекращает поиск.
  /// Использутеся когда не должно быть более одного шаблона.
  /// Обходит рекурсивно всю данную ветку дерева</remarks>
  /// <param name="nodeTemplate">Шаблон</param>
  /// <param name="notThis">Искать только в дочерних узлах - этот узел не учитывать</param>
  /// <returns>Возвращает найденный узел, или null, если узел не найден.</returns>
  public DocumentTreeNode FindFirstNodeFromTemplate_Recursive(string nodeTemplateId, bool notThis)
  {
    if (nodeTemplateId == null || nodeTemplateId == "")
      throw new ArgumentNullException(nameof (nodeTemplateId));
    if (!notThis && nodeTemplateId == this.TemplateId)
      return this;
    DocumentTreeNode templateRecursive = (DocumentTreeNode) null;
    if (this.nodes != null)
    {
      for (int index = 0; index < this.nodes.Count; ++index)
      {
        templateRecursive = this.nodes[index].FindFirstNodeFromTemplate_Recursive(nodeTemplateId, false);
        if (templateRecursive != null)
          break;
      }
    }
    return templateRecursive;
  }

  /// <summary>Найти первый узел созданный по шаблону</summary>
  /// <remarks>В отличии от FindNodesFromTemplate находит первый узел и прекращает поиск.
  /// Использутеся когда не должно быть более одного шаблона</remarks>
  /// <param name="nodeTemplate">Шаблон</param>
  /// <returns>Возвращает найденный узел, или null, если узел не найден.</returns>
  public virtual DocumentTreeNode FindFirstNodeFromTemplate(DocumentTreeNode nodeTemplate)
  {
    if (nodeTemplate == null)
      throw new ArgumentNullException(nameof (nodeTemplate));
    if (nodeTemplate == this.Template)
      return this;
    if (nodeTemplate.connectionList != null && nodeTemplate.connectionList.Count > 0)
    {
      for (int index = 0; index < nodeTemplate.connectionList.Count; ++index)
      {
        if (nodeTemplate.connectionList[index] is ReferenceToTemplate && nodeTemplate.connectionList[index].OwnerNode != null)
          return nodeTemplate.connectionList[index].OwnerNode;
      }
    }
    return this.FindFirstNodeFromTemplate_Recursive(nodeTemplate);
  }

  /// <summary>Найти первый узел созданный по шаблону</summary>
  /// <remarks>В отличии от FindNodesFromTemplate находит первый узел и прекращает поиск.
  /// Используется когда не должно быть более одного шаблона.
  /// Обходит рекурсивно всю данную ветку дерева</remarks>
  /// <param name="nodeTemplate">Шаблон</param>
  /// <returns>Возвращает найденный узел, или null, если узел не найден.</returns>
  public DocumentTreeNode FindFirstNodeFromTemplate_Recursive(DocumentTreeNode nodeTemplate)
  {
    if (nodeTemplate == null)
      throw new ArgumentNullException(nameof (nodeTemplate));
    if (nodeTemplate == this.Template)
      return this;
    DocumentTreeNode templateRecursive = (DocumentTreeNode) null;
    if (this.nodes != null)
    {
      for (int index = 0; index < this.nodes.Count; ++index)
      {
        templateRecursive = this.nodes[index].FindFirstNodeFromTemplate_Recursive(nodeTemplate);
        if (templateRecursive != null)
          break;
      }
    }
    return templateRecursive;
  }

  /// <summary>Найти ближайший к данному узел, созданный по шаблону</summary>
  /// <param name="nodeTemplate">Шаблон узла</param>
  /// <param name="onePageFlow">Искать только внутри одного потока страниц</param>
  /// <param name="onlyPrev">Искать только предыдущие элементы</param>
  /// <returns></returns>
  public virtual DocumentTreeNode FindNearestNodeFromTemplate(
    DocumentTreeNode nodeTemplate,
    bool onePageFlow = false,
    bool onlyPrev = false)
  {
    DocumentTreeNode nodeFromTemplate = this.SelectNearestNodeFromClonesByTemplate(nodeTemplate);
    if (nodeFromTemplate != null)
      return nodeFromTemplate;
    if (nodeTemplate != null && nodeTemplate.connectionList != null && nodeTemplate.connectionList.Count > 0)
    {
      List<DocumentTreeNode> documentTreeNodeList = new List<DocumentTreeNode>(nodeTemplate.connectionList.Count + 1);
      if (nodeTemplate != this.Template)
        documentTreeNodeList.Add(this);
      PageElementNode pageElementNode = this as PageElementNode;
      PageData pageData1 = (PageData) null;
      if (onePageFlow && pageElementNode != null && pageElementNode.Page != null)
        pageData1 = pageElementNode.Page.FindFirstPage();
      for (int index = 0; index < nodeTemplate.connectionList.Count; ++index)
      {
        if (nodeTemplate.connectionList[index] is ReferenceToTemplate && nodeTemplate.connectionList[index].OwnerNode != null && nodeTemplate.connectionList[index].OwnerNode.OwnerDocument != null && nodeTemplate.connectionList[index].OwnerNode.OwnerDocument == this.OwnerDocument)
        {
          PageData pageData2 = (PageData) null;
          if (onePageFlow && pageData1 != null && nodeTemplate.connectionList[index].OwnerNode is PageElementNode ownerNode && ownerNode.Page != null)
            pageData2 = ownerNode.Page.FindFirstPage();
          if (!onePageFlow || pageData2 == pageData1)
            documentTreeNodeList.Add(nodeTemplate.connectionList[index].OwnerNode);
        }
      }
      documentTreeNodeList.Sort((IComparer<DocumentTreeNode>) new DocNodeComparer());
      int num = documentTreeNodeList.IndexOf(this);
      if (num > 0)
        return documentTreeNodeList[num - 1];
      if (!onlyPrev && num < documentTreeNodeList.Count - 1)
        return documentTreeNodeList[num + 1];
    }
    return (DocumentTreeNode) null;
  }

  /// <summary>Найти ближайший к данному узел, созданный по шаблону</summary>
  /// <param name="nodeTemplate">Шаблон узла</param>
  /// <returns></returns>
  public virtual DocumentTreeNode SelectNearestNodeFromClonesByTemplate(
    DocumentTreeNode nodeTemplate)
  {
    if (nodeTemplate == null)
      return (DocumentTreeNode) null;
    if (nodeTemplate.connectionList == null || nodeTemplate.connectionList.Count == 0)
      return (DocumentTreeNode) null;
    DocumentTreeNode documentTreeNode = (DocumentTreeNode) null;
    DocNodeComparer docNodeComparer = new DocNodeComparer();
    ImDocumentData ownerDocument = this.OwnerDocument;
    for (int index = 0; index < nodeTemplate.connectionList.Count; ++index)
    {
      if (nodeTemplate.connectionList[index] is ReferenceToTemplate && nodeTemplate.connectionList[index].OwnerDocument == ownerDocument)
      {
        if (docNodeComparer.Compare(this, nodeTemplate.connectionList[index].OwnerNode) < 0)
        {
          if (documentTreeNode == null)
          {
            DocumentTreeNode ownerNode = nodeTemplate.connectionList[index].OwnerNode;
            break;
          }
          break;
        }
        documentTreeNode = nodeTemplate.connectionList[index].OwnerNode;
      }
    }
    return documentTreeNode;
  }

  /// <summary>Виртуальный узел. Не входит в состав дерева документа. Не имеет идентификатора.
  /// Не сохраняется в xml файле. Не владеет узлами. Только для внутреннего использования!</summary>
  [Browsable(false)]
  public virtual bool IsVirtualNode
  {
    [DebuggerStepThrough] get => this.isVirtualNode;
  }

  /// <summary>Узел являющийся переносимыми данными в таблице.
  /// Только для внутреннего использования!</summary>
  [Browsable(false)]
  internal virtual bool IsDataNode
  {
    [DebuggerStepThrough] get => false;
  }

  /// <summary>Назначить значение свойству IsVirtualNode</summary>
  /// <param name="value">Значение свойства</param>
  protected virtual void SetIsVirtualNode(bool value) => this.isVirtualNode = value;

  /// <summary>Запрет на изменение пользователем структуры узла</summary>
  [Browsable(false)]
  public virtual bool ReadOnlyStructure
  {
    [DebuggerStepThrough] get => false;
  }

  /// <summary>Этот узел является дочерним для заданного</summary>
  /// <param name="node">Узел</param>
  /// <param name="includeVirtual">Включая виртуальные узлы</param>
  /// <returns>true, если этот узел является дочерним для заданного</returns>
  public virtual bool IsChildForNode(DocumentTreeNode parentNode, bool includeVirtual)
  {
    bool flag = false;
    if (parentNode.IsVirtualNode)
    {
      flag = includeVirtual && parentNode.Nodes.Contains(this);
    }
    else
    {
      for (DocumentTreeNode parent = this.Parent; parent != null; parent = parent.Parent)
      {
        if (parent == parentNode)
        {
          flag = true;
          break;
        }
      }
    }
    return flag;
  }

  /// <summary>Этот узел является родительским для заданного</summary>
  /// <param name="node">Узел</param>
  /// <param name="includeVirtual">Включая виртуальные узлы</param>
  /// <returns>true, если этот узел является родительским для заданного</returns>
  public bool IsParentForNode(DocumentTreeNode node, bool includeVirtual)
  {
    return node.IsChildForNode(this, includeVirtual);
  }

  /// <summary>
  /// Переместить элемент документа на позицию выше с учётом логической структуры данных, разбитых по страницам
  /// </summary>
  /// <param name="updateLayoutAndUI">Обновить разбивку данных по страницам и внешний вид документа в интерфейсе пользователя.
  /// Если False, после всех манипуляций с документом необходимо вызывать UpdateLayout</param>
  public virtual void MoveDataElementUp(bool updateLayoutAndUI)
  {
    if (this.Parent == null)
      return;
    int index = this.Index - 1;
    if (index < 0)
      return;
    this.Parent.InsertChildNode(index, this, false, true, false, false);
    if (!updateLayoutAndUI)
      return;
    this.UpdateLayout(true);
  }

  /// <summary>
  /// Переместить элемент документа на позицию ниже с учётом логической структуры данных, разбитых по страницам
  /// </summary>
  /// <param name="updateLayoutAndUI">Обновить разбивку данных по страницам и внешний вид документа в интерфейсе пользователя.
  /// Если False, после всех манипуляций с документом необходимо вызывать UpdateLayout</param>
  public virtual void MoveDataElementDown(bool updateLayoutAndUI)
  {
    if (this.Parent == null)
      return;
    int index = this.Index + 1;
    if (index >= this.Parent.NodesCount)
      return;
    this.Parent.InsertChildNode(index, this, false, true, false, false);
    if (!updateLayoutAndUI)
      return;
    this.UpdateLayout(true);
  }

  /// <summary>
  /// Переместить элемент документа на позицию выше с учётом логической структуры данных, разбитых по страницам
  /// </summary>
  /// <param name="updateLayoutAndUI">Обновить разбивку данных по страницам и внешний вид документа в интерфейсе пользователя.
  /// Если False, после всех манипуляций с документом необходимо вызывать UpdateLayout</param>
  public virtual void MoveDataElementToBegin(bool updateLayoutAndUI)
  {
    if (this.Parent == null || this.Index == 0)
      return;
    this.Parent.InsertChildNode(0, this, false, true, false, false);
    if (!updateLayoutAndUI)
      return;
    this.UpdateLayout(true);
  }

  /// <summary>
  /// Переместить элемент документа на позицию ниже с учётом логической структуры данных, разбитых по страницам
  /// </summary>
  /// <param name="updateLayoutAndUI">Обновить разбивку данных по страницам и внешний вид документа в интерфейсе пользователя.
  /// Если False, после всех манипуляций с документом необходимо вызывать UpdateLayout</param>
  public virtual void MoveDataElementToEnd(bool updateLayoutAndUI)
  {
    if (this.Parent == null || this.Index == this.Parent.NodesCount - 1)
      return;
    this.Parent.InsertChildNode(this.Parent.NodesCount - 1, this, false, true, false, false);
    if (!updateLayoutAndUI)
      return;
    this.UpdateLayout(true);
  }

  /// <summary>Узел является первым у родительского элемента</summary>
  [Browsable(false)]
  public virtual bool IsFirstCellInParentDataFlow => this.Parent == null || this.Index == 0;

  /// <summary>Узел является последним у родительского элемента</summary>
  [Browsable(false)]
  public virtual bool IsLastCellInParentDataFlow
  {
    get => this.Parent == null || this.Index == this.Parent.NodesCount - 1;
  }

  /// <summary>Этот узел можно удалить. Влияет только на интерфейс пользователя.</summary>
  public virtual bool CanRemove()
  {
    return !this.ClonedByTemplateWithParent && this.Parent != null && !this.Parent.ReadOnlyStructure;
  }

  /// <summary>Проверить можно ли добавить заданный элемент в этот элемент</summary>
  /// <param name="child">Вставляемый элемент</param>
  /// <returns>Возвращает true, если заданный элемент можно добавить в этот элемент</returns>
  public virtual bool CanAddChildElement(DocumentTreeNode child) => true;

  /// <summary>Проверить можно ли добавить элемент заданного типа в этот элемент</summary>
  /// <param name="type">Тип вставляемого элемента</param>
  /// <returns>Возвращает true, если элемент заданного типа можно добавить в этот элемент</returns>
  public virtual bool CanAddChildElement(Type type)
  {
    return typeof (DocumentTreeNode).IsAssignableFrom(type);
  }

  /// <summary>Назначает значение свойству Parent. При этом удаляет у старого parent и добавляет в новый</summary>
  /// <param name="value">Новое значение Parent</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public virtual void SetParent(DocumentTreeNode value, bool updateUI, bool updateLayout)
  {
    if (this.parent == value)
      return;
    if (this.isVirtualNode)
      this.AssignParent(value, updateUI, updateLayout, false);
    else if (value != null)
      value.AddChildNode(this, false, true, updateUI, updateLayout);
    else
      this.parent.RemoveChildNode(this, updateUI, updateLayout);
  }

  /// <summary>Присвоить значение свойству Parent</summary>
  /// <param name="value">Новое значение Parent</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public virtual void AssignParent(
    DocumentTreeNode value,
    bool updateUI,
    bool updateLayout,
    bool isLoading)
  {
    if (this.parent == value)
      return;
    if (isLoading || this.isVirtualNode)
    {
      this.parent = value;
    }
    else
    {
      if (this.parent != null)
        this.SetChangingCount(this.changingCount - this.parent.changingCount);
      this.parent = value;
      if (this.parent != null)
        this.SetChangingCount(this.changingCount + this.parent.changingCount);
      this.OnParentChanged(new ParentChanged_EventArgs());
    }
  }

  /// <summary>Предок узла документа.
  /// Должен быть перекрыт для связи UIControl и IdService родителя и узла</summary>
  [Browsable(false)]
  public DocumentTreeNode Parent
  {
    [DebuggerStepThrough] get => this.parent;
  }

  /// <summary>Проверить соответствие иерархии родителей по списку идентификаторов</summary>
  /// <param name="parents"></param>
  /// <returns></returns>
  public bool CheckParentList(params string[] parents)
  {
    if (parents == null || parents.Length == 0)
      return true;
    DocumentTreeNode parent1 = this.parent;
    foreach (string parent2 in parents)
    {
      if (parent1 == null || parent1.Id != parent2)
        return false;
      parent1 = parent1.Parent;
    }
    return true;
  }

  /// <summary>Корень дерева документа в котором находится этот узел.
  /// <remarks>Документ который владеет этим узлом. Если узел не пренадлежит документу, то null</remarks>
  /// </summary>
  public virtual ImDocumentData GetDocTreeRoot()
  {
    ImDocumentData docTreeRoot = this as ImDocumentData;
    for (DocumentTreeNode documentTreeNode = this; documentTreeNode.Parent != null && docTreeRoot == null; docTreeRoot = documentTreeNode as ImDocumentData)
      documentTreeNode = documentTreeNode.Parent;
    return docTreeRoot;
  }

  /// <summary>Получить корень дерева в котором находится этот узел
  /// <remarks>Корнем считается первый узел без родителя вверх по иерархии</remarks>
  /// </summary>
  public virtual DocumentTreeNode GetTreeRoot()
  {
    DocumentTreeNode treeRoot = this;
    while (treeRoot.Parent != null)
      treeRoot = treeRoot.Parent;
    return treeRoot;
  }

  /// <summary>Добавить узел в коллекцию дочерних узлов</summary>
  /// <param name="child">Добавляемый узел</param>
  /// <param name="insertByShift">Узел перемещается в пределах таблицы</param>
  /// <param name="uniteTable">Объединить распределенные ячейки перед вставкой</param>
  /// <param name="updateUI">Обновить элементы управления</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public int AddChildNode(
    DocumentTreeNode child,
    bool insertByShift,
    bool uniteTable,
    bool updateUI,
    bool updateLayout)
  {
    if (child == null)
      throw new ArgumentNullException(nameof (child));
    if (child == this)
      throw new ArgumentException(LocalizationHolder.rm.GetString("Interfaces.Document_158"), nameof (child));
    if (this.nodes == null)
      throw new Exception(LocalizationHolder.rm.GetString("Interfaces.Document_7"));
    lock (this.nodes)
    {
      int count = this.nodes.Count;
      if (this.InsertChildNode(count, child, insertByShift, uniteTable, updateUI, updateLayout))
        return count;
    }
    return -1;
  }

  /// <summary>Добавить узел в коллекцию дочерних узлов</summary>
  /// <param name="child">Добавляемый узел</param>
  /// <param name="updateUI">Обновить элементы управления</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public int AddChildNode(DocumentTreeNode child, bool updateUI, bool updateLayout)
  {
    return this.AddChildNode(child, false, !this.IsVirtualNode, updateUI, updateLayout);
  }

  /// <summary>Добавить узлы в коллекцию дочерних узлов</summary>
  /// <param name="nodes">Узлы</param>
  /// <param name="insertByShift">Узел перемещается в пределах таблицы</param>
  /// <param name="uniteTable">Объединить распределенные ячейки перед вставкой</param>
  /// <param name="updateUI">Обновить элементы управления</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public void AddChildNodes(
    DocumentTreeNode[] nodes,
    bool insertByShift,
    bool uniteTable,
    bool updateUI,
    bool updateLayout)
  {
    lock (nodes)
    {
      for (int index = 0; index < nodes.Length; ++index)
        this.AddChildNode(nodes[index], insertByShift, uniteTable, updateUI, updateLayout);
    }
  }

  /// <summary>Добавить узлы в коллекцию дочерних узлов</summary>
  /// <param name="nodes">Узлы</param>
  /// <param name="updateUI">Обновить элементы управления</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public void AddChildNodes(DocumentTreeNode[] nodes, bool updateUI, bool updateLayout)
  {
    this.AddChildNodes(nodes, false, true, updateUI, updateLayout);
  }

  /// <summary>Добавить узлы в коллекцию дочерних узлов</summary>
  /// <param name="nodes">Узлы</param>
  /// <param name="insertByShift">Узел перемещается в пределах таблицы</param>
  /// <param name="uniteTable">Объединить распределенные ячейки перед вставкой</param>
  /// <param name="updateUI">Обновить элементы управления</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public void AddChildNodes(
    List<DocumentTreeNode> nodes,
    bool insertByShift,
    bool uniteTable,
    bool updateUI,
    bool updateLayout)
  {
    lock (nodes)
    {
      for (int index = 0; index < nodes.Count; ++index)
        this.AddChildNode(nodes[index], insertByShift, uniteTable, updateUI, updateLayout);
    }
  }

  /// <summary>Добавить узлы в коллекцию дочерних узлов</summary>
  /// <param name="nodes">Узлы</param>
  /// <param name="updateUI">Обновить элементы управления</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public void AddChildNodes(List<DocumentTreeNode> nodes, bool updateUI, bool updateLayout)
  {
    this.AddChildNodes(nodes, false, true, updateUI, updateLayout);
  }

  /// <summary>Метод вызываемый до добавления дочернего узла</summary>
  /// <param name="child">Дочерний узел</param>
  protected virtual void PreProcessAddChildNode(DocumentTreeNode child)
  {
  }

  /// <summary>Зарезервировать идентификатор для клона по шаблону</summary>
  /// <param name="idServiceOwner">Владелец сервиса идентификаторов</param>
  protected virtual void ReserveIdForTemplateClone(DocumentTreeNode idServiceOwner)
  {
    List<DocumentTreeNode> templateClones = idServiceOwner.GetTemplateClones();
    if (string.IsNullOrEmpty(this.id) || this.IdService.ContainsId((object) this.id) && this.IdService[(object) this.id] != this)
      this.Id = this.IdService.GenerateUniqueId().ToString();
    bool flag = false;
    while (!flag)
    {
      flag = true;
      for (int index = 0; index < templateClones.Count; ++index)
      {
        if (templateClones[index].IdService.ContainsId((object) this.id))
        {
          flag = false;
          this.Id = this.IdService.GenerateUniqueId().ToString();
          break;
        }
      }
    }
    if (this.nodes == null)
      return;
    lock (this.nodes)
    {
      for (int index = 0; index < this.nodes.Count; ++index)
      {
        if (this.nodes[index].ShouldReserveIdForTemplateClone(out idServiceOwner))
          this.nodes[index].ReserveIdForTemplateClone(idServiceOwner);
      }
    }
  }

  /// <summary>Метод вызывается после добавления дочернего элемента, но до вызова события ChildNodeAdded</summary>
  /// <param name="child">Дочерний элемент</param>
  /// <param name="insertByShift">Узел перемещается в пределах таблицы</param>
  /// <param name="updateUI">Обновить элементы управления</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  protected virtual void PostProcessAddChildNode(
    DocumentTreeNode child,
    bool insertByShift,
    bool updateUI,
    bool updateLayout)
  {
    if (this.isVirtualNode || !this.IsTemplate || !child.CloneByTemplateWithParent)
      return;
    if (this.SuspendedApplyThisTemplateFlag)
    {
      this.TreeStructureChangedFlag = true;
    }
    else
    {
      List<DocumentTreeNode> templateClones = this.GetTemplateClones();
      int num = 0;
      int index1 = 0;
      for (int index2 = child.Index; index1 < index2; ++index1)
      {
        if (this.nodes[index1].CloneByTemplateWithParent)
          ++num;
      }
      DocumentTreeNode idServeceOwner = (DocumentTreeNode) null;
      if (child.ShouldReserveIdForTemplateClone(out idServeceOwner))
        child.ReserveIdForTemplateClone(idServeceOwner);
      int index3 = 0;
      for (int count = templateClones.Count; index3 < count; ++index3)
      {
        DocumentTreeNode child1 = child.CloneFromTemplate(true, true);
        child1.AssignClonedByTemplateWithParent(true);
        int index4 = num < templateClones[index3].NodesCount ? num : templateClones[index3].NodesCount;
        templateClones[index3].InsertChildNode(index4, child1, false, true, updateUI, updateLayout);
      }
    }
  }

  /// <summary>Вставить в заданную позицию дочерний узел</summary>
  /// <param name="index">Позиция в которую будет вставлен узел</param>
  /// <param name="child">Узел</param>
  /// <param name="insertByShift">Узел перемещается в пределах таблицы</param>
  /// <param name="uniteTable">Объединить распределенные ячейки перед вставкой</param>
  /// <param name="updateUI">Обновить элементы управления</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  /// <param name="isNew">Узел новый и не требуется это проверять</param>
  /// <returns>true, если вставка не была отменена</returns>
  public virtual bool InsertChildNode(
    int index,
    DocumentTreeNode child,
    bool insertByShift,
    bool uniteTable,
    bool updateUI,
    bool updateLayout,
    bool isNew = false)
  {
    if (child == null)
      throw new ArgumentNullException(nameof (child));
    if (child == this)
      throw new ArgumentException(LocalizationHolder.rm.GetString("Interfaces.Document_174"), nameof (child));
    lock (this.nodes)
    {
      if (!this.isVirtualNode && child.isVirtualNode)
        return false;
      if (this.nodes == null)
        throw new Exception(LocalizationHolder.rm.GetString("Interfaces.Document_9"));
      if (DocumentTreeNode.debugCheckParentCircle && !this.isVirtualNode)
      {
        DocumentTreeNode documentTreeNode = this;
        while (documentTreeNode != null)
        {
          documentTreeNode = documentTreeNode.parent;
          if (documentTreeNode == child)
            throw new ArgumentException(LocalizationHolder.rm.GetString("Interfaces.Document_184"), nameof (child));
        }
      }
      if (index < 0 || index > this.nodes.Count)
        throw new ArgumentOutOfRangeException(nameof (index), string.Format(LocalizationHolder.rm.GetString("Interfaces.Document_190"), (object) index, (object) this.NodesCount, (object) this.Id, (object) child.Id));
      int num = -1;
      if (!isNew)
        num = this.nodes.IndexOf(child);
      if (num == -1)
      {
        BeforeAddChildNode_EventArgs e = new BeforeAddChildNode_EventArgs(child);
        this.OnBeforeAddChildNode(e);
        if (e.Cancel)
          return false;
        this.BeginChangingStructure();
        this.PreProcessAddChildNode(child);
        if (child.Parent != null && !this.isVirtualNode)
          child.Parent.RemoveChildNode(child, insertByShift, false, false);
        this.nodes.InsertInternal(index, child);
        if (!this.isVirtualNode)
        {
          child.IdService = this.IdService;
          child.AssignParent(this, false, false, false);
          if (this.TemplateRoot != null)
            child.UpdateTemplateLinks(false, !insertByShift, false, false);
        }
        this.PostProcessAddChildNode(child, insertByShift, false, false);
        this.OnChildNodeAdded(new ChildNode_EventArgs(this, child, index, insertByShift, updateUI, updateLayout));
        this.SetNeedUpdateLayoutFlag(true, true, false, false);
        if (updateLayout)
          this.UpdateLayout(updateUI);
        this.EndChangingStructure(updateUI, updateUI, false, updateLayout);
      }
      else if (index != num)
      {
        if (index <= this.nodes.Count)
        {
          this.nodes.RemoveAtInternal(num);
          if (index > this.nodes.Count)
            index = this.nodes.Count;
          this.nodes.InsertInternal(index, child);
          this.SetNeedUpdateLayoutFlag(true, true, false, false);
          this.OnChildNodePositionChanged(new ChildNodePositionChanged_EventArgs(child, num, index, updateUI));
          if (updateLayout)
            this.UpdateLayout(updateUI);
        }
        if (!this.isVirtualNode)
          child.AssignParent(this, updateUI, updateLayout, false);
      }
    }
    return true;
  }

  /// <summary>Метод вызывается после того как два дочерних элемента поменяются местами</summary>
  /// <param name="index1">Индекс одного элемента</param>
  /// <param name="index2">Индекс второго элемента</param>
  /// <param name="updateUI">Обновить элементы управления</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  protected virtual void PostExchangeChildsMethod(
    int index1,
    int index2,
    bool updateUI,
    bool updateLayout)
  {
  }

  /// <summary>Удалить дочерний узел</summary>
  /// <param name="node">Удаляемый узел</param>
  /// <param name="updateUI">Обновить элементы управления</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public void RemoveChildNode(DocumentTreeNode node, bool updateUI, bool updateLayout)
  {
    this.RemoveChildNode(node, false, updateUI, updateLayout);
  }

  /// <summary>Удалить дочерний узел</summary>
  /// <param name="node">Удаляемый узел</param>
  /// <param name="updateUI">Обновить элементы управления</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public void RemoveChildNode(
    DocumentTreeNode node,
    bool removeByShift,
    bool updateUI,
    bool updateLayout)
  {
    if (this.nodes == null)
      return;
    lock (this.nodes)
    {
      int index = this.nodes.IndexOf(node);
      if (index == -1)
        return;
      this.RemoveChildNodeAt(index, removeByShift, updateUI, updateLayout);
    }
  }

  /// <summary>Удалить элемент с заданным индексом</summary>
  /// <param name="index">Индекс удаляемого элемента</param>
  /// <param name="updateUI">Обновить элементы управления</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public virtual bool RemoveChildNodeAt(int index, bool updateUI, bool updateLayout)
  {
    return this.RemoveChildNodeAt(index, false, updateUI, updateLayout);
  }

  /// <summary>Удалить элемент с заданным индексом</summary>
  /// <param name="index">Индекс удаляемого элемента</param>
  /// <param name="updateUI">Обновить элементы управления</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public virtual bool RemoveChildNodeAt(
    int index,
    bool removeByShift,
    bool updateUI,
    bool updateLayout)
  {
    if (this.nodes == null)
      return false;
    lock (this.nodes)
    {
      DocumentTreeNode node = this.nodes[index];
      BeforeRemoveChildNode_EventArgs e = new BeforeRemoveChildNode_EventArgs(node, removeByShift);
      this.OnBeforeRemoveChildNode(e);
      if (e.Cancel)
        return false;
      this.BeginChangingStructure();
      if (!this.isVirtualNode)
        node.AssignParent((DocumentTreeNode) null, false, false, false);
      this.nodes.RemoveAtInternal(index);
      if (!this.isVirtualNode)
      {
        node.OnRemoved(new Removed_EventArgs(node, this, removeByShift));
        if (node.referenceToTemplate != null)
          node.referenceToTemplate.DisconnectLink();
        if (this.IsTemplate && node.CloneByTemplateWithParent)
        {
          if (this.SuspendedApplyThisTemplateFlag)
          {
            this.TreeStructureChangedFlag = true;
          }
          else
          {
            List<DocumentTreeNode> templateClones = this.GetTemplateClones();
            List<DocumentTreeNode> documentTreeNodeList = new List<DocumentTreeNode>();
            int index1 = 0;
            for (int count1 = templateClones.Count; index1 < count1; ++index1)
            {
              DocumentTreeNode documentTreeNode = templateClones[index1];
              documentTreeNodeList.Clear();
              DocumentTreeNode nodeTemplate = node;
              List<DocumentTreeNode> foundNodes = documentTreeNodeList;
              documentTreeNode.FindNodesFromTemplate(nodeTemplate, foundNodes);
              int index2 = 0;
              for (int count2 = documentTreeNodeList.Count; index2 < count2; ++index2)
              {
                if (documentTreeNodeList[index2].ClonedByTemplateWithParent)
                  documentTreeNodeList[index2].Remove(updateUI, updateLayout);
              }
            }
          }
        }
        if (node.connectionList != null)
        {
          for (int index3 = node.connectionList.Count - 1; index3 >= 0; --index3)
          {
            DocumentTreeNode documentTreeNode = (DocumentTreeNode) null;
            if (!(node.connectionList[index3] is ReferenceToNodeAttributeBase connection) || connection.ReferenceBaseType == BaseReferenceNodeType.ntSelectedNode)
            {
              if (node.connectionList[index3] is ReferenceToNodeAttributeBase)
                documentTreeNode = node.connectionList[index3].OwnerNode;
              node.connectionList[index3].DisconnectLink();
              documentTreeNode?.UpdateNodeAttributeLinks(false, updateUI, updateLayout);
            }
          }
        }
        node.UpdateNodeAttributeLinks(true, updateUI, updateLayout);
      }
      this.OnChildNodeRemoved(new ChildNode_EventArgs(this, node, index, removeByShift, updateUI, updateLayout));
      this.EndChangingStructure(updateUI, updateUI, false, updateLayout);
    }
    return true;
  }

  /// <summary>Переместить дочерний узел на другую позицию в пределах коллекции</summary>
  /// <param name="index">Индекс перемещаемого узла</param>
  /// <param name="newIndex">Новый индекс узла</param>
  /// <param name="updateUI">Обновить элементы управления</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  protected virtual void MoveChildNode(int index, int newIndex, bool updateUI, bool updateLayout)
  {
    if (this.nodes == null)
      return;
    lock (this.nodes)
    {
      DocumentTreeNode node = this.nodes[index];
      this.nodes.RemoveAtInternal(index);
      this.nodes.InsertInternal(newIndex, node);
    }
  }

  /// <summary>Удалить элемент из списка элемента родителей</summary>
  /// <param name="updateUI">Обновить элементы управления</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public void Remove(bool updateUI, bool updateLayout)
  {
    if (this.parent == null || this.isVirtualNode)
      return;
    this.parent.RemoveChildNode(this, false, updateUI, updateLayout);
  }

  /// <summary>Удалить элемент из списка элемента родителей</summary>
  /// <param name="updateUI">Обновить элементы управления</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public void Remove(bool removeByShift, bool updateUI, bool updateLayout)
  {
    if (this.parent == null || this.isVirtualNode)
      return;
    this.parent.RemoveChildNode(this, removeByShift, updateUI, updateLayout);
  }

  /// <summary>Очистить узел</summary>
  /// <param name="updateUI">Обновить элементы управления</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public virtual void Clear(bool updateUI, bool updateLayout)
  {
    if (this.nodes == null)
      return;
    lock (this.nodes)
    {
      for (int index = this.nodes.Count - 1; index >= 0; --index)
        this.nodes[index].Remove(updateUI, updateLayout);
    }
  }

  /// <summary>Количество узлов в дереве</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_96")]
  [CustomDescription("Attribute.Interfaces.Document_97")]
  [Category("Debug")]
  public int NodesCount
  {
    [DebuggerStepThrough] get => this.nodes == null ? 0 : this.nodes.Count;
  }

  /// <summary>Подузлы элемента. Если узел не должен иметь дочерних узлов, то значение null</summary>
  [Browsable(false)]
  public virtual DocumentTreeNodeCollection Nodes
  {
    [DebuggerStepThrough] get => this.nodes;
    set => this.nodes = value;
  }

  /// <summary>Индекс узла в родительском узле</summary>
  [Browsable(false)]
  public int Index
  {
    [DebuggerStepThrough] get
    {
      if (this.parent != null && !this.isVirtualNode)
      {
        DocumentTreeNodeCollection nodes = this.parent.Nodes;
        if (nodes != null)
        {
          if (this.index < 0 || this.index >= nodes.Count || nodes[this.index] != this)
            this.index = this.parent.Nodes.IndexOf(this);
          return this.index;
        }
      }
      return -1;
    }
  }

  /// <summary>Перечисление всех дочерних узлов рекурсивно</summary>
  /// <returns></returns>
  [Browsable(false)]
  public virtual IEnumerable<DocumentTreeNode> NodesRecursive
  {
    get
    {
      if (this.nodes != null)
      {
        foreach (DocumentTreeNode node in this.nodes)
        {
          yield return node;
          foreach (DocumentTreeNode documentTreeNode in node.NodesRecursive)
            yield return documentTreeNode;
        }
      }
    }
  }

  /// <summary>
  /// Перечисление всех дочерних узлов рекурсивно, с условием сбора подузлов
  /// Узлы не подходящие по условию игнорируются и внутрь них рекурсия тоже не заходит
  /// </summary>
  /// <returns></returns>
  public virtual IEnumerable<DocumentTreeNode> NodesRecursiveByCondition(
    Func<DocumentTreeNode, bool> predicate)
  {
    if (this.nodes != null)
    {
      foreach (DocumentTreeNode node in this.nodes)
      {
        if (predicate(node))
        {
          yield return node;
          foreach (DocumentTreeNode documentTreeNode in node.NodesRecursiveByCondition(predicate))
            yield return documentTreeNode;
        }
      }
    }
  }

  /// <summary>
  /// Перечисление всех дочерних узлов заданного типа рекурсивно, с условием сбора подузлов
  /// </summary>
  /// <returns></returns>
  public virtual IEnumerable<T> ChildNodesByCondition<T>(Func<T, bool> predicate)
  {
    if (this.nodes != null)
    {
      foreach (DocumentTreeNode node in this.nodes)
      {
        DocumentTreeNode documentTreeNode;
        if ((documentTreeNode = node) is T)
        {
          T obj = (T) documentTreeNode;
          if (predicate(obj))
            yield return obj;
        }
        foreach (T obj in node.ChildNodesByCondition<T>(predicate))
          yield return obj;
      }
    }
  }

  /// <summary>Сравнить положение узлов в дереве</summary>
  /// <param name="nodeX">Узел X</param>
  /// <param name="nodeY">Узел Y</param>
  /// <returns>Результат сравнения.
  /// -1 означает x меньше y
  /// 0 означает x равно y, либо узлы находятся в разных деревьях
  /// 1 означает x больше y
  /// </returns>
  public int CompareTreePositions(DocumentTreeNode nodeX, DocumentTreeNode nodeY)
  {
    if (nodeX == null)
      throw new ArgumentNullException(nameof (nodeX));
    if (nodeY == null)
      throw new ArgumentNullException(nameof (nodeY));
    if (nodeX.Parent == null || nodeY.Parent == null)
      return 0;
    Tuple<DocumentTreeNode, DocumentTreeNode> lowestCommonAncestor = this.FindChildsForLowestCommonAncestor(nodeX, nodeY);
    return lowestCommonAncestor.Item1 == null || lowestCommonAncestor.Item2 == null ? 0 : nodeX.Index.CompareTo(nodeY.Index);
  }

  /// <summary>Найти ближайшего общего предка двух узлов дерева</summary>
  /// <returns></returns>
  public Tuple<DocumentTreeNode, DocumentTreeNode> FindChildsForLowestCommonAncestor(
    DocumentTreeNode nodeX,
    DocumentTreeNode nodeY)
  {
    if (nodeX == null)
      throw new ArgumentNullException(nameof (nodeX));
    if (nodeY == null)
      throw new ArgumentNullException(nameof (nodeY));
    if (nodeX.Parent == nodeY.Parent)
      return new Tuple<DocumentTreeNode, DocumentTreeNode>(nodeX, nodeY);
    List<DocumentTreeNode> documentTreeNodeList1 = new List<DocumentTreeNode>();
    while (nodeX.Parent != null)
    {
      nodeX = nodeX.Parent;
      documentTreeNodeList1.Add(nodeX);
    }
    List<DocumentTreeNode> documentTreeNodeList2 = new List<DocumentTreeNode>();
    while (nodeY.Parent != null)
    {
      nodeY = nodeY.Parent;
      documentTreeNodeList2.Add(nodeY);
    }
    if (documentTreeNodeList1[documentTreeNodeList1.Count - 1] != documentTreeNodeList2[documentTreeNodeList2.Count - 1])
      return new Tuple<DocumentTreeNode, DocumentTreeNode>((DocumentTreeNode) null, (DocumentTreeNode) null);
    DocumentTreeNode documentTreeNode1 = (DocumentTreeNode) null;
    DocumentTreeNode documentTreeNode2 = (DocumentTreeNode) null;
    int num = documentTreeNodeList1.Count < documentTreeNodeList2.Count ? documentTreeNodeList1.Count : documentTreeNodeList2.Count;
    for (int index = 1; index < num; ++index)
    {
      if (documentTreeNodeList1[documentTreeNodeList1.Count - 1 - index] != documentTreeNodeList2[documentTreeNodeList2.Count - 1 - index])
      {
        documentTreeNode1 = documentTreeNodeList1[documentTreeNodeList1.Count - 1 - index];
        documentTreeNode2 = documentTreeNodeList2[documentTreeNodeList2.Count - 1 - index];
        break;
      }
    }
    return new Tuple<DocumentTreeNode, DocumentTreeNode>(documentTreeNode1, documentTreeNode2);
  }

  /// <summary>Наименование типа</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_98")]
  [CustomDescription("Attribute.Interfaces.Document_99")]
  [CustomCategory("Attribute.Interfaces.Document_100")]
  [ReadOnly(true)]
  public virtual string NodeTypeCaption
  {
    [DebuggerStepThrough] get => this.GetType().Name;
    set
    {
    }
  }

  /// <summary>Класс элемента</summary>
  [Browsable(false)]
  public string NodeClass
  {
    [DebuggerStepThrough] get => this.GetType().Name;
  }

  /// <summary>Получить подпись элемента по умолчанию</summary>
  public virtual string GetDefautCaption()
  {
    string str = this.GetName();
    if (DocumentTreeNode.IsEmptyString(str))
      str = this.NodeTypeCaption;
    return $"{str} ({this.Id})";
  }

  /// <summary>Получить подпись элемента для сообщений</summary>
  public virtual string GetCaptionForMessage()
  {
    string name = this.GetName();
    return DocumentTreeNode.IsEmptyString(name) ? $"{this.NodeTypeCaption} ({this.Id})" : name;
  }

  /// <summary>Пропустить через фильтр.
  /// Параметр exclude имеет больший приоритет, чем include</summary>
  /// <param name="exclude">Типы элементов, которые должны быть исключены.
  /// Допустимо значение null, или пустой массив</param>
  /// <param name="include">Типы элементов, которые должны быть включены.
  /// Если значение null или массив пустой то включаются все элементы</param>
  /// <returns>true, если проходит через фильтр</returns>
  public virtual bool FilterCheck(Type[] exclude, Type[] include)
  {
    Type type = this.GetType();
    bool flag1 = false;
    if (exclude != null)
    {
      for (int index = 0; index < exclude.Length; ++index)
      {
        if (exclude[index].IsAssignableFrom(type))
          flag1 = true;
      }
    }
    bool flag2 = false;
    if (include != null && include.Length != 0)
    {
      for (int index = 0; index < include.Length; ++index)
      {
        if (include[index].IsAssignableFrom(type))
          flag2 = true;
      }
    }
    else
      flag2 = true;
    return !flag1 & flag2;
  }

  /// <summary>Производятся изменения</summary>
  [Browsable(false)]
  public bool IsChanging
  {
    [DebuggerStepThrough] get => this.changingCount > 0;
  }

  /// <summary>Начать изменение</summary>
  public virtual void BeginChanges(bool recursive)
  {
    ++this.changingCount;
    if (!recursive || this.nodes == null)
      return;
    for (int index = 0; index < this.nodes.Count; ++index)
      this.nodes[index].BeginChanges(true);
  }

  /// <summary>Завершить изменение</summary>
  public virtual void EndChanges(bool recursive)
  {
    if (this.changingCount > 0)
      --this.changingCount;
    else
      this.changingCount = 0;
    if (!recursive || this.nodes == null)
      return;
    for (int index = 0; index < this.nodes.Count; ++index)
      this.nodes[index].EndChanges(true);
  }

  /// <summary>Установить счетчик незаконченных изменений</summary>
  protected void SetChangingCount(int count)
  {
    this.changingCount = count;
    if (this.nodes == null)
      return;
    for (int index = 0; index < this.nodes.Count; ++index)
      this.nodes[index].SetChangingCount(count);
  }

  /// <summary>Начать изменение структуры дерева,
  /// служит для блокирования некоторых действий
  /// пока изменения дерева не закончены (EndChangingStructure)</summary>
  public virtual void BeginChangingStructure()
  {
    ++this.changingStructureCount;
    if (this.changingStructureCount != 1)
      return;
    this.OnBeginStructureChanging(new StructureChanging_EventArgs(this));
  }

  /// <summary>Структура таблицы изменяется</summary>
  [Browsable(false)]
  public bool IsChangingStructure
  {
    [DebuggerStepThrough] get => this.changingStructureCount > 0;
  }

  /// <summary>Выполнить предварительные действия перед окончанием изменения структуры</summary>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="refreshUI">Обновить изображение в интерфейсе пользователя</param>
  /// <param name="updateTemplateLinks">Обновить ссылки на шаблоны</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  protected virtual void PreProcessEndChangingStructure(
    bool updateUI,
    bool refreshUI,
    bool updateTemplateLinks,
    bool updateLayout)
  {
    if (!updateTemplateLinks || this.isVirtualNode)
      return;
    this.UpdateTemplateLinks(true, true, updateUI, updateLayout);
  }

  /// <summary>Закончить изменения структуры таблицы</summary>
  public virtual void EndChangingStructure(
    bool updateUI,
    bool refreshUI,
    bool updateTemplateLinks,
    bool updateLayout)
  {
    if (this.changingStructureCount > 0)
    {
      --this.changingStructureCount;
      if (this.changingStructureCount != 0)
        return;
      this.PreProcessEndChangingStructure(updateUI, refreshUI, updateTemplateLinks, updateLayout);
      this.OnEndStructureChanging(new StructureChanging_EventArgs(this));
    }
    else
    {
      if (this.changingStructureCount != 0)
        return;
      this.PreProcessEndChangingStructure(updateUI, refreshUI, false, updateLayout);
    }
  }

  /// <summary>Список ссылок, указывающих на этот узел. Используется для обратной связи.</summary>
  [Browsable(false)]
  public List<ReferenceToNode> ConnectionList
  {
    [DebuggerStepThrough] get => this.connectionList;
  }

  /// <summary>Добавить связь указывающую на этот узел</summary>
  /// <param name="link">Связь</param>
  public virtual void RemoveConnection(ReferenceToNode link)
  {
    if (link == null)
      throw new ArgumentNullException(nameof (link));
    if (this.connectionList == null)
      return;
    lock (this.connectionList)
      this.connectionList.Remove(link);
  }

  /// <summary>Добавить связь указывающую на этот узел</summary>
  /// <param name="link">Связь</param>
  public virtual void AddConnection(ReferenceToNode link)
  {
    if (this.connectionList == null)
      this.connectionList = new List<ReferenceToNode>();
    lock (this.connectionList)
    {
      int index1 = this.connectionList.Count;
      if (link is ReferenceToTemplate)
      {
        DocNodeComparer docNodeComparer = new DocNodeComparer();
        ImDocumentData ownerDocument = this.OwnerDocument;
        for (int index2 = this.connectionList.Count - 1; index2 >= 0; --index2)
        {
          if (this.connectionList[index2] is ReferenceToTemplate && this.connectionList[index2].OwnerDocument == ownerDocument && docNodeComparer.Compare(link.OwnerNode, this.connectionList[index2].OwnerNode) > 0)
            index1 = index2 + 1;
        }
      }
      this.connectionList.Insert(index1, link);
    }
  }

  /// <summary>Установить значение флагов OverrideFlags</summary>
  /// <param name="flags">Проверяемые флаги</param>
  /// <returns>Возвращает true, если все флаги установлены в 1</returns>
  public bool IsOverridden(OverrideFlags flags) => (this.overrideFlags & flags) == flags;

  /// <summary>Установить битовые флаги в поле overrideFlags</summary>
  /// <param name="flags">Флаги, которые нужно установить</param>
  public void SetOverrideFlags(OverrideFlags flags) => this.overrideFlags |= flags;

  /// <summary>Сбросить битовые флаги в поле overrideFlags</summary>
  /// <param name="flags">Флаги, которые нужно сбросить</param>
  public void ResetOverrideFlags(OverrideFlags flags) => this.overrideFlags &= ~flags;

  /// <summary>Установить значение флагов OverrideFlags2</summary>
  /// <param name="flag">Проверяемые флаги</param>
  /// <returns>Возвращает true, если все флаги установлены в 1</returns>
  public bool IsOverridden2(OverrideFlags2 flag) => (this.overrideFlags2 & flag) == flag;

  /// <summary>Установить битовые флаги в поле overrideFlags2</summary>
  /// <param name="flags">Флаги, которые нужно установить</param>
  public void SetOverrideFlags2(OverrideFlags2 flags) => this.overrideFlags2 |= flags;

  /// <summary>Сбросить битовые флаги в поле overrideFlags2</summary>
  /// <param name="flags">Флаги, которые нужно сбросить</param>
  public void ResetOverrideFlags2(OverrideFlags2 flags) => this.overrideFlags2 &= ~flags;

  /// <summary>Установить значение флагов OverrideFlags3</summary>
  /// <param name="flag">Проверяемые флаги</param>
  /// <returns>Возвращает true, если все флаги установлены в 1</returns>
  public bool IsOverridden3(OverrideFlags3 flag) => (this.overrideFlags3 & flag) == flag;

  /// <summary>Установить битовые флаги в поле overrideFlags3</summary>
  /// <param name="flags">Флаги, которые нужно установить</param>
  public void SetOverrideFlags3(OverrideFlags3 flags) => this.overrideFlags3 |= flags;

  /// <summary>Сбросить битовые флаги в поле overrideFlags3</summary>
  /// <param name="flags">Флаги, которые нужно сбросить</param>
  public void ResetOverrideFlags3(OverrideFlags3 flags) => this.overrideFlags3 &= ~flags;

  /// <summary>Проверить значение флагов в поле flag</summary>
  /// <param name="flag">Проверяемые флаги, заданные константами Flag_...</param>
  /// <returns>Возвращает true, если все флаги установлены в 1</returns>
  public bool CheckFlags(byte flag) => ((int) this.flags & (int) flag) == (int) flag;

  /// <summary>Только для внутреннего пользования. Установить значение битовых флагов в поле flag</summary>
  /// <param name="flags">Биты которые нужно установить</param>
  /// <param name="value">Значение</param>
  public void SetFlags(byte flags, bool value)
  {
    if (value)
      this.flags |= flags;
    else
      this.flags &= ~flags;
  }

  public override string ToString() => this.GetDefautCaption();

  /// <summary>Вставить следующий элемент потока в цепочку</summary>
  /// <param name="newNextFlow">Новый следующий родительский элемент потока</param>
  public virtual void InsertNextFlowChaineElement(IParentFlow newNextFlow)
  {
  }

  /// <summary>Обновить ссылки на узлы</summary>
  /// <param name="recursive">Для всех дочерних элементов</param>
  /// <param name="saveUndo">Сохранять данные для Undo</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public virtual void UpdateNodeLinks(
    bool recursive,
    bool saveUndo,
    bool updateUI,
    bool updateLayout)
  {
    if (!(this.nodes != null & recursive))
      return;
    for (int index = 0; index < this.nodes.Count; ++index)
      this.nodes[index].UpdateNodeLinks(recursive, saveUndo, updateUI, updateLayout);
  }

  /// <summary>Обновить ссылки на узлы обновляемые при печати</summary>
  /// <param name="recursive">Для всех дочерних элементов</param>
  /// <param name="saveUndo">Сохранять данные для Undo</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public virtual void UpdatePrintLinks(
    bool recursive,
    bool saveUndo,
    bool updateUI,
    bool updateLayout)
  {
    if (!(this.nodes != null & recursive))
      return;
    for (int index = 0; index < this.nodes.Count; ++index)
      this.nodes[index].UpdatePrintLinks(recursive, saveUndo, updateUI, updateLayout);
  }

  /// <summary>Обновить ссылки на атрибуты</summary>
  /// <param name="recursive">Для всех дочерних элементов</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public virtual void UpdateNodeAttributeLinks(bool recursive, bool updateUI, bool updateLayout)
  {
    if (!(this.nodes != null & recursive))
      return;
    for (int index = 0; index < this.nodes.Count; ++index)
      this.nodes[index].UpdateNodeAttributeLinks(recursive, updateUI, updateLayout);
  }

  /// <summary>Обновить разбивку по страницам.
  /// Вызывает UpdateLayout для вышестоящих узлов или Distribute для себя.
  /// Вызов UpdateLayout для дочерних узлов недопустим!</summary>
  /// <param name="updateUI">Обновлять пользовательский интерфейс</param>
  public virtual void UpdateLayout(bool updateUI) => this.ResetNeedUpdateLayoutFlag(false);

  /// <summary>Вызывает разбивку по страницам</summary>
  /// <param name="context">Контекст разбивки</param>
  /// <param name="updateUI">Обновлять пользовательский интерфейс</param>
  public virtual void Distribute(DistributeContext context, bool updateUI)
  {
    this.ResetNeedUpdateLayoutFlag(false);
  }

  /// <summary>Обновление представлений данных временно заблокировано</summary>
  [Category("Debug")]
  public virtual bool SuspendedUpdateLayoutFlag
  {
    [DebuggerStepThrough] get => this.suspendUpdateLayoutCount > 0;
    set
    {
      if (value == this.SuspendedUpdateLayoutFlag)
        return;
      if (value)
        ++this.suspendUpdateLayoutCount;
      else
        this.suspendUpdateLayoutCount = 0;
    }
  }

  /// <summary>Установить значение suspendUpdateLayoutCount</summary>
  internal void SetSuspendUpdateLayoutCount(int count)
  {
    this.suspendUpdateLayoutCount = count;
    if (this.nodes == null)
      return;
    for (int index = 0; index < this.nodes.Count; ++index)
      this.nodes[index].SetSuspendUpdateLayoutCount(count);
  }

  /// <summary>Приостановить автоматическое обновление представлений данных</summary>
  public virtual void SuspendUpdateLayout()
  {
    ++this.suspendUpdateLayoutCount;
    if (this.nodes == null)
      return;
    for (int index = 0; index < this.nodes.Count; ++index)
      this.nodes[index].SuspendUpdateLayout();
  }

  /// <summary>Возобновить автоматическое обновление представлений данных</summary>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public virtual void ResumeUpdateLayout(bool updateUI, bool updateLayout)
  {
    if (this.suspendUpdateLayoutCount > 0)
      --this.suspendUpdateLayoutCount;
    else
      this.suspendUpdateLayoutCount = 0;
    if (this.nodes != null)
    {
      for (int index = 0; index < this.nodes.Count; ++index)
        this.nodes[index].ResumeUpdateLayout(false, false);
    }
    if (!updateLayout || this.SuspendedUpdateLayoutFlag)
      return;
    this.UpdateLayout(updateUI);
  }

  /// <summary>Установить значение NeedUpdateLayoutFlag без автоматического обновления</summary>
  /// <param name="value">Новое значение</param>
  public void AssignNeedUpdateLayoutFlag(bool value) => this.needUpdateLayoutFlag = value;

  /// <summary>Требуется обновить отображение данных</summary>
  [Category("Debug")]
  public virtual bool NeedUpdateLayoutFlag
  {
    [DebuggerStepThrough] get => this.needUpdateLayoutFlag;
  }

  /// <summary>Установить флаг NeedUpdateLayoutFlag</summary>
  /// <param name="value">Значение флага</param>
  /// <param name="setInPrevCell">Установить флаг и для предыдущих ячеек</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  /// <param name="recursive">Установить флаг и для всех дочерних узлов</param>
  public void SetNeedUpdateLayoutFlag(
    bool value,
    bool setInPrevCell,
    bool updateUI,
    bool updateLayout,
    bool recursive)
  {
    this.SetNeedUpdateLayoutFlag(value, setInPrevCell, false, false);
    if (recursive && this.nodes != null)
    {
      for (int index = 0; index < this.nodes.Count; ++index)
        this.nodes[index].SetNeedUpdateLayoutFlag(value, setInPrevCell, false, false, true);
    }
    if (!updateLayout || !this.needUpdateLayoutFlag || this.SuspendedUpdateLayoutFlag)
      return;
    this.UpdateLayout(updateUI);
  }

  /// <summary>Установить флаг NeedUpdateLayoutFlag</summary>
  /// <param name="value">Значение флага</param>
  /// <param name="setInPrevCell">Установить флаг и для предыдущих ячеек</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public virtual void SetNeedUpdateLayoutFlag(
    bool value,
    bool setInPrevCell,
    bool updateUI,
    bool updateLayout)
  {
    if (!(updateLayout & value) && this.needUpdateLayoutFlag == value)
      return;
    this.AssignNeedUpdateLayoutFlag(value);
    if (!updateLayout || !this.needUpdateLayoutFlag || this.SuspendedUpdateLayoutFlag)
      return;
    this.UpdateLayout(updateUI);
  }

  /// <summary>Сбросить флаг NeedUpdateLayoutFlag</summary>
  /// <param name="recursive">Рекурсивно</param>
  public void ResetNeedUpdateLayoutFlag(bool recursive)
  {
    if (this.needUpdateLayoutFlag & recursive && this.nodes != null)
    {
      for (int index = 0; index < this.nodes.Count; ++index)
        this.nodes[index].ResetNeedUpdateLayoutFlag(true);
    }
    this.AssignNeedUpdateLayoutFlag(false);
  }

  /// <summary>Обновить формулы в текстовых полях</summary>
  protected virtual void UpdateFormulasInTextBox()
  {
    if (this.nodes == null)
      return;
    for (int index = 0; index < this.nodes.Count; ++index)
      this.nodes[index].UpdateFormulasInTextBox();
  }

  /// <summary>Элемент принадлежит библиотеке формул</summary>
  [Browsable(false)]
  public virtual bool IsFormulaLib => false;

  /// <summary>Получить список дополнительных атрибутов</summary>
  /// <returns></returns>
  public AdditionalAttributeCollection GetAdditionalAttributes() => this.additionalAttributes;

  /// <summary>Назначить список дополнительных атрибутов</summary>
  /// <returns></returns>
  public void SetAdditionalAttributes(AdditionalAttributeCollection value)
  {
    if (this.additionalAttributes == value)
      return;
    if (this.additionalAttributes != null)
      this.additionalAttributes.Owner = (DocumentTreeNode) null;
    this.additionalAttributes = value;
    if (this.additionalAttributes == null)
      return;
    this.additionalAttributes.Owner = this;
  }

  /// <summary>Для внутреннего использования. Добавить дополнительные атрибуты. Не производит обновлений</summary>
  /// <param name="newAttributes">Словарь с атрибутами</param>
  /// <returns></returns>
  public void AddAdditionalAttributes(IDictionary newAttributes)
  {
    foreach (DictionaryEntry newAttribute in newAttributes)
      this.SetAttributeValue((string) newAttribute.Key, Convert.ToString(newAttribute.Value), false, false, false);
  }

  /// <summary>Список дополнительных атрибутов</summary>
  [RefreshProperties(RefreshProperties.All)]
  [CustomDisplayName("Attribute.Interfaces.Document_101")]
  [CustomDescription("Attribute.Interfaces.Document_102")]
  [CustomCategory("Attribute.Interfaces.Document_103")]
  internal AdditionalAttributeCollection AdditionalAttributes
  {
    [DebuggerStepThrough] get => this.additionalAttributes;
    set => this.SetAdditionalAttributes(value);
  }

  /// <summary>Содержит ли объект атрибут с указанным именем</summary>
  /// <param name="attributeName">Имя атрибута</param>
  /// <returns>Возвращает true, если объект содержит атрибут с указанным именем</returns>
  public bool ContainsAttribute(string attributeName)
  {
    if (attributeName == null)
      throw new ArgumentNullException(nameof (attributeName));
    return this.additionalAttributes != null && this.additionalAttributes.ContainsAttribute(attributeName) || this.ContainsVirtualAttribute(attributeName);
  }

  /// <summary>Получить значение атрибута</summary>
  /// <param name="attributeName">Имя атрибута</param>
  /// <param name="notNull">Вернуть пустую строку вместо значения null</param>
  /// <param name="callChain">Цепочка вызовов для защиты от циклических связей. Если null, то работает без проверок</param>
  /// <returns>Возвращает значение атрибута. Если атрибута нет, вернет null.</returns>
  public string GetAttributeValue(
    string attributeName,
    bool notNull,
    List<DocumentTreeNode> callChain = null)
  {
    if (attributeName == null)
      throw new ArgumentNullException(nameof (attributeName));
    GetVirtualAttributeResult virtualAttributeValue = this.GetVirtualAttributeValue(attributeName, notNull, callChain);
    if (virtualAttributeValue.Found)
      return virtualAttributeValue.Value;
    if (this.additionalAttributes != null)
      return this.additionalAttributes.GetAttributeStringValue(attributeName, notNull);
    return notNull ? "" : (string) null;
  }

  /// <summary>Установить значение атрибута. Если атрибута не было, то он создается</summary>
  /// <param name="attributeName">Имя атрибута</param>
  /// <param name="attributeValue">Значение атрибута</param>
  /// <param name="saveUndo">Сохранять данные для Undo</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  /// <returns>Возвращает true, если удалось установить значение</returns>
  public bool SetAttributeValue(
    string attributeName,
    string attributeValue,
    bool saveUndo = true,
    bool updateUI = true,
    bool updateLayout = true)
  {
    return this.SetAttributeValue(attributeName, attributeValue, saveUndo, updateUI, updateLayout, (List<DocumentTreeNode>) null);
  }

  /// <summary>Установить значение атрибута. Если атрибута не было, то он создается</summary>
  /// <param name="attributeName">Имя атрибута</param>
  /// <param name="attributeValue">Значение атрибута</param>
  /// <param name="saveUndo">Сохранять данные для Undo</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  /// <returns>Возвращает true, если удалось установить значение</returns>
  internal bool SetAttributeValue(
    string attributeName,
    string attributeValue,
    bool saveUndo,
    bool updateUI,
    bool updateLayout,
    List<DocumentTreeNode> callChain)
  {
    if (attributeName == null)
      throw new ArgumentNullException(nameof (attributeName));
    if (saveUndo && this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.BeginCreateMultyUndo("");
    bool flag = true;
    try
    {
      if (attributeValue == null)
        attributeValue = "";
      string attributeValue1 = this.GetAttributeValue(attributeName, true);
      if (!(attributeValue1 != attributeValue))
      {
        if (this.ContainsAttribute(attributeName))
          goto label_23;
      }
      AttributeValueChanging_EventArgs e = new AttributeValueChanging_EventArgs(attributeName, (object) attributeValue1, (object) attributeValue);
      this.OnAttributeValueChanging(e);
      if (!e.Cancel)
      {
        SetVirtualAttributeResult virtualAttributeResult = this.SetVirtualAttributeValue(attributeName, attributeValue, updateUI, updateLayout, callChain);
        if (!virtualAttributeResult.Cancel)
        {
          if (!virtualAttributeResult.Found)
          {
            if (this.AdditionalAttributes == null)
              this.AdditionalAttributes = new AdditionalAttributeCollection(this);
            if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
              this.OwnerDocument.UndoManager.CreateUndo(this.AdditionalAttributes.ContainsAttribute(attributeName) ? (IUndoAction) new UndoAttributeChanged(this.OwnerDocument.UndoManager, this, attributeName, attributeValue1, attributeValue) : (IUndoAction) new UndoAttributeAdd(this.OwnerDocument.UndoManager, this, attributeName, attributeValue), false);
            this.AdditionalAttributes.SetAttributeStringValue(attributeName, attributeValue);
          }
          this.OnAttributeValueChanged(new AttributeValueChanged_EventArgs(attributeName, (object) attributeValue1, (object) attributeValue, updateUI, updateLayout));
          this.SetPropertiesChangedFlag(true, true, false, updateUI, updateLayout);
          this.OnChanged(new Changed_EventArgs());
        }
        else
          flag = false;
      }
      else
        flag = false;
    }
    finally
    {
      if (saveUndo && this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.EndCreateMultyUndo();
    }
label_23:
    return flag;
  }

  /// <summary>
  /// Удалить атрибут без вызова обработчиков событий и установки флага Modified. Только для внутреннего использования.
  /// </summary>
  /// <param name="attributeName">Имя атрибута</param>
  public void RemoveAttributeWithoutEvents(string attributeName)
  {
    if (this.additionalAttributes == null || !this.additionalAttributes.ContainsAttribute(attributeName))
      return;
    this.additionalAttributes.RemoveAttribute(attributeName);
  }

  /// <summary>Удалить атрибут</summary>
  /// <param name="attributeName">Имя атрибута</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  /// <returns>Результат удаления</returns>
  public bool RemoveAttribute(string attributeName, bool updateUI, bool updateLayout)
  {
    if (attributeName == null)
      throw new ArgumentNullException(nameof (attributeName));
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.BeginCreateMultyUndo("");
    bool flag = true;
    try
    {
      if (this.ContainsVirtualAttribute(attributeName))
        flag = false;
      else if (this.additionalAttributes != null)
      {
        if (this.additionalAttributes.ContainsAttribute(attributeName))
        {
          AttributeRemoving_EventArgs e = new AttributeRemoving_EventArgs(attributeName);
          this.OnAttributeRemoving(e);
          if (!e.Cancel)
          {
            if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
              this.OwnerDocument.UndoManager.CreateUndo((IUndoAction) new UndoAttributeRemove(this.OwnerDocument.UndoManager, this, attributeName, this.additionalAttributes.GetAttributeStringValue(attributeName, false)), false);
            this.additionalAttributes.RemoveAttribute(attributeName);
            this.OnAttributeRemoved(new AttributeRemoved_EventArgs(attributeName, updateUI, updateLayout));
            this.SetPropertiesChangedFlag(true, true, false, updateUI, updateLayout);
            this.OnChanged(new Changed_EventArgs());
          }
          else
            flag = false;
        }
      }
    }
    finally
    {
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.EndCreateMultyUndo();
    }
    return flag;
  }

  /// <summary>Получить список всех имен атрибутов</summary>
  /// <param name="includeVirtual">Включая виртуальные атрибуты</param>
  /// <returns>Список всех имен атрибутов</returns>
  public StringCollection GetAttributeNames(bool includeVirtual)
  {
    StringCollection attributeNames = new StringCollection();
    if (includeVirtual)
      this.GetVirtualAttributeNames(attributeNames);
    if (this.additionalAttributes != null)
    {
      foreach (string key in (IEnumerable) this.additionalAttributes.Keys)
        attributeNames.Add(key);
    }
    return attributeNames;
  }

  /// <summary>Получить все атрибуты</summary>
  /// <param name="attributes">Словарь куда будут помещены атрибуты</param>
  /// <param name="includeVirtual">Включая виртуальные атрибуты</param>
  public void GetAttributes(IDictionary attributes, bool includeVirtual)
  {
    if (attributes == null)
      throw new ArgumentNullException(nameof (attributes));
    if (includeVirtual)
      this.GetVirtualAttributes(attributes);
    if (this.additionalAttributes == null)
      return;
    foreach (DictionaryEntry attribute in this.additionalAttributes.Attributes)
      attributes.Add(attribute.Key, attribute.Value);
  }

  /// <summary>Содержит ли объект виртуальный атрибут с указанным именем</summary>
  /// <param name="attributeName">Имя виртуального атрибута</param>
  /// <returns>Возвращает true, если объект содержит виртуальный атрибут
  /// с указанным именем</returns>
  internal virtual bool ContainsVirtualAttribute(string attributeName)
  {
    if (attributeName == null)
      throw new ArgumentNullException(nameof (attributeName));
    return attributeName == DocumentTreeNode.AttributeName_Name;
  }

  /// <summary>Получить значение виртуального атрибута</summary>
  /// <param name="attributeName">Имя атрибута</param>
  /// <param name="notNull">Вернуть пустую строку вместо значения null</param>
  /// <param name="callChain">Цепочка вызовов для защиты от циклических связей. Если null, то работает без проверок</param>
  /// <returns>Результат выполнения</returns>
  protected virtual GetVirtualAttributeResult GetVirtualAttributeValue(
    string attributeName,
    bool notNull,
    List<DocumentTreeNode> callChain = null)
  {
    if (attributeName == null)
      throw new ArgumentNullException(nameof (attributeName));
    GetVirtualAttributeResult virtualAttributeValue = new GetVirtualAttributeResult(false, (string) null);
    if (attributeName == DocumentTreeNode.AttributeName_Name)
    {
      virtualAttributeValue.Value = this.Name;
      virtualAttributeValue.Found = true;
    }
    if (this.getPluginVirtualAttributeValue != null)
    {
      GetVirtualAttributeResult virtualAttributeResult = this.getPluginVirtualAttributeValue((object) this, attributeName, notNull);
      if (virtualAttributeResult.Found)
        virtualAttributeValue = virtualAttributeResult;
    }
    if (virtualAttributeValue.Found & notNull && virtualAttributeValue.Value == null)
      virtualAttributeValue.Value = "";
    return virtualAttributeValue;
  }

  /// <summary>Событие получения значения виртуального атрибута. Вызывается в GetVirtualAttributeValue. Используется для плагинов</summary>
  public event GetPluginVirtualAttributeValue_EventHandler GetPluginVirtualAttributeValue
  {
    add => this.getPluginVirtualAttributeValue += value;
    remove => this.getPluginVirtualAttributeValue -= value;
  }

  /// <summary>Установить значение виртуального атрибута</summary>
  /// <param name="attributeName">Имя атрибута</param>
  /// <param name="attributeValue">Значение атрибута</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  /// <param name="callChain">Цепочка вызовов, для защиты от зацикливания</param>
  /// <returns>Результат выполнения</returns>
  protected virtual SetVirtualAttributeResult SetVirtualAttributeValue(
    string attributeName,
    string attributeValue,
    bool updateUI,
    bool updateLayout,
    List<DocumentTreeNode> callChain)
  {
    if (attributeName == null)
      throw new ArgumentNullException(nameof (attributeName));
    SetVirtualAttributeResult virtualAttributeResult = new SetVirtualAttributeResult(false, false);
    if (attributeName == DocumentTreeNode.AttributeName_Name)
    {
      virtualAttributeResult.Found = true;
      this.AssignName(attributeValue);
    }
    return virtualAttributeResult;
  }

  /// <summary>Получить список всех имен атрибутов</summary>
  /// <param name="attributeNames">Список в который добавляются имена атрибутов</param>
  /// <param name="forSaveOnly">Добавлять в список только те атрибуты, которые должны сохраниться в XML или копироваться при копировании через буфер</param>
  protected virtual void GetVirtualAttributeNames(StringCollection attributeNames, bool forSaveOnly = false)
  {
    if (attributeNames == null)
      throw new ArgumentNullException(nameof (attributeNames));
    if (!(this is ImDocumentData))
      attributeNames.Add(DocumentTreeNode.AttributeName_Name);
    if (this.getPluginVirtualAttributeNames == null)
      return;
    this.getPluginVirtualAttributeNames((object) this, attributeNames, forSaveOnly);
  }

  /// <summary>Событие Получение имён виртуальных атрибутов. Вызывается в GetVirtualAttributeNames</summary>
  public event GetPluginVirtualAttributeNames_EventHandler GetPluginVirtualAttributeNames
  {
    add => this.getPluginVirtualAttributeNames += value;
    remove => this.getPluginVirtualAttributeNames -= value;
  }

  /// <summary>Получить все виртуальные атрибуты</summary>
  /// <param name="attributes">Словарь куда будут помещены виртуальные атрибуты</param>
  /// <param name="forSaveOnly">Добавлять в только те атрибуты, которые должны сохраниться в XML или копироваться при копировании через буфер</param>
  protected void GetVirtualAttributes(IDictionary attributes, bool forSaveOnly = false)
  {
    if (attributes == null)
      throw new ArgumentNullException(nameof (attributes));
    StringCollection attributeNames = new StringCollection();
    this.GetVirtualAttributeNames(attributeNames);
    foreach (string str in attributeNames)
      attributes.Add((object) str, (object) this.GetVirtualAttributeValue(str, false));
  }

  /// <summary>Событие Изменение значения атрибута. Возникает до изменения</summary>
  public event AttributeValueChanging_EventHandler AttributeValueChanging
  {
    add => this.attributeValueChanging_EventHandler += value;
    remove => this.attributeValueChanging_EventHandler -= value;
  }

  /// <summary>Событие Изменилось значение атрибута</summary>
  public event AttributeValueChanged_EventHandler AttributeValueChanged
  {
    add => this.attributeValueChanged_EventHandler += value;
    remove => this.attributeValueChanged_EventHandler -= value;
  }

  /// <summary>Событие Удаляется атрибут</summary>
  public event AttributeRemoving_EventHandler AttributeRemoving
  {
    add => this.attributeValueRemoving_EventHandler += value;
    remove => this.attributeValueRemoving_EventHandler -= value;
  }

  /// <summary>Событие Удален атрибут</summary>
  public event AttributeRemoved_EventHandler AttributeRemoved
  {
    add => this.attributeValueRemoved_EventHandler += value;
    remove => this.attributeValueRemoved_EventHandler -= value;
  }

  /// <summary>Вызывает событие AttributeValueChanging</summary>
  /// <param name="e">Аргументы события</param>
  protected virtual void OnAttributeValueChanging(AttributeValueChanging_EventArgs e)
  {
    if (this.attributeValueChanging_EventHandler == null)
      return;
    this.attributeValueChanging_EventHandler((object) this, e);
  }

  /// <summary>Вызывает событие AttributeValueChanged</summary>
  /// <param name="e">Аргументы события</param>
  protected virtual void OnAttributeValueChanged(AttributeValueChanged_EventArgs e)
  {
    if (this.connectionList != null)
    {
      for (int index = 0; index < this.connectionList.Count; ++index)
      {
        if (this.connectionList[index] is ReferenceToNodeAttributeBase connection && connection.AttributeName == e.AttributeName)
          connection.OnTextChanged(Convert.ToString(e.OldValue), Convert.ToString(e.NewValue), false, e.UpdateUI, e.UpdateLayout);
      }
    }
    if (this.attributeValueChanged_EventHandler == null)
      return;
    this.attributeValueChanged_EventHandler((object) this, e);
  }

  /// <summary>Вызывает событие AttributeRemoving</summary>
  /// <param name="e">Аргументы события</param>
  protected virtual void OnAttributeRemoving(AttributeRemoving_EventArgs e)
  {
    if (this.attributeValueRemoving_EventHandler == null)
      return;
    this.attributeValueRemoving_EventHandler((object) this, e);
  }

  /// <summary>Вызывает событие AttributeRemoved</summary>
  /// <param name="e">Аргументы события</param>
  protected virtual void OnAttributeRemoved(AttributeRemoved_EventArgs e)
  {
    if (this.connectionList != null)
    {
      for (int index = 0; index < this.connectionList.Count; ++index)
      {
        if (this.connectionList[index] is ReferenceToNodeAttributeBase connection && connection.AttributeName == e.AttributeName)
          connection.OwnerNode.SetNeedUpdateLayoutFlag(true, true, e.UpdateUI, e.UpdateLayout);
      }
    }
    if (this.attributeValueRemoved_EventHandler == null)
      return;
    this.attributeValueRemoved_EventHandler((object) this, e);
  }

  /// <summary>Событие Перед добавлением дочернего узла</summary>
  public event BeforeAddChildNode_EventHandler BeforeAddChildNode
  {
    add => this.beforeAddChildNode += value;
    remove => this.beforeAddChildNode -= value;
  }

  /// <summary>Вызывает событие BeforeAddChildNode</summary>
  /// <param name="e">Аргумент события</param>
  protected virtual void OnBeforeAddChildNode(BeforeAddChildNode_EventArgs e)
  {
    if (this.beforeAddChildNode == null)
      return;
    this.beforeAddChildNode((object) this, e);
  }

  /// <summary>Событие Добавлен дочерний узел</summary>
  public event ChildNodeAdded_EventHandler ChildNodeAdded
  {
    add => this.childNodeAdded += value;
    remove => this.childNodeAdded -= value;
  }

  /// <summary>Событие Добавлен узел дерева (вызывается из документа)</summary>
  public event ChildNodeAdded_EventHandler TreeNodeAdded
  {
    add => this.treeNodeAdded += value;
    remove => this.treeNodeAdded -= value;
  }

  /// <summary>Вызывает событие ChildNodeAdded</summary>
  /// <param name="e">Аргумент события</param>
  protected virtual void OnChildNodeAdded(ChildNode_EventArgs e)
  {
    if (!this.IsVirtualNode && !e.ByShift && this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo(e.Parent, e.Child);
    this.OnChildNodeAddedCore(e);
    this.GetDocTreeRoot()?.OnTreeNodeAdded(e);
    this.OnChanged(new Changed_EventArgs());
  }

  /// <summary>Базовая часть обработки события ChildNodeAdded
  /// Вспомогательный метод, чтобы можно было переопределять обработку не меняя последовательности вызова событий</summary>
  /// <param name="e">Аргумент события</param>
  protected virtual void OnChildNodeAddedCore(ChildNode_EventArgs e)
  {
    ChildNodeAdded_EventHandler childNodeAdded = this.childNodeAdded;
    if (childNodeAdded == null)
      return;
    childNodeAdded((object) this, e);
  }

  /// <summary>Вызывает событие TreeNodeAdded</summary>
  /// <param name="e">Аргумент события</param>
  protected virtual void OnTreeNodeAdded(ChildNode_EventArgs e)
  {
    if (this.treeNodeAdded == null)
      return;
    this.treeNodeAdded((object) this, e);
  }

  /// <summary>Событие Перед удалением дочернего узла</summary>
  public event BeforeRemoveChildNode_EventHandler BeforeRemoveChildNode
  {
    add => this.beforeRemoveChildNode += value;
    remove => this.beforeRemoveChildNode -= value;
  }

  /// <summary>Вызывает событие BeforeRemoveChildNode</summary>
  /// <param name="e">Аргумент события</param>
  protected virtual void OnBeforeRemoveChildNode(BeforeRemoveChildNode_EventArgs e)
  {
    if (this.beforeRemoveChildNode == null)
      return;
    this.beforeRemoveChildNode((object) this, e);
  }

  /// <summary>Событие Удален дочерний узел</summary>
  public event ChildNodeRemoved_EventHandler ChildNodeRemoved
  {
    add => this.childNodeRemoved += value;
    remove => this.childNodeRemoved -= value;
  }

  /// <summary>Событие Удален узел дерева(Вызывается из документа)</summary>
  public event ChildNodeRemoved_EventHandler TreeNodeRemoved
  {
    add => this.treeNodeRemoved += value;
    remove => this.treeNodeRemoved -= value;
  }

  /// <summary>Вызывает событие ChildNodeRemoved</summary>
  /// <param name="e">Аргумент события</param>
  public virtual void OnChildNodeRemoved(ChildNode_EventArgs e)
  {
    if (!e.ByShift && this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo(e.Parent, e.Child, e.Index);
    this.OnChildNodeRemovedCore(e);
    this.GetDocTreeRoot()?.OnTreeNodeRemoved(e);
    this.OnChanged(new Changed_EventArgs());
  }

  /// <summary>Базовая часть обработки события ChildNodeRemoved
  /// Вспомогательный метод, чтобы можно было переопределять обработку не меняя последовательности вызова событий</summary>
  /// <param name="e">Аргумент события</param>
  protected virtual void OnChildNodeRemovedCore(ChildNode_EventArgs e)
  {
    ChildNodeRemoved_EventHandler childNodeRemoved = this.childNodeRemoved;
    if (childNodeRemoved == null)
      return;
    childNodeRemoved((object) this, e);
  }

  /// <summary>Вызывает событие OnTreeNodeRemoved</summary>
  /// <param name="e">Аргумент события</param>
  public virtual void OnTreeNodeRemoved(ChildNode_EventArgs e)
  {
    if (this.treeNodeRemoved == null)
      return;
    this.treeNodeRemoved((object) this, e);
  }

  /// <summary>Вызывается при удалении ветки, в которой находится этот узел</summary>
  public event NodeRemoved_EventHandler BranchRemoved
  {
    add => this.branchRemoved += value;
    remove => this.branchRemoved -= value;
  }

  /// <summary>Метод вызывается при удалении ветки, в которой находится этот узел</summary>
  protected virtual void OnBranchRemoved(Removed_EventArgs e)
  {
    if (this.nodes != null)
    {
      for (int index = 0; index < this.nodes.Count; ++index)
        this.nodes[index].OnBranchRemoved(e);
    }
    if (!e.RemovedByShift && this.ReferenceToTemplate != null)
      this.ReferenceToTemplate.AssignNodeLink((DocumentTreeNode) null);
    if (this.branchRemoved == null)
      return;
    this.branchRemoved((object) this, e);
  }

  /// <summary>Происходит когда узел удален</summary>
  public event NodeRemoved_EventHandler NodeRemoved
  {
    add => this.nodeRemoved += value;
    remove => this.nodeRemoved -= value;
  }

  /// <summary>Генерирует событие Removed</summary>
  protected virtual void OnRemoved(Removed_EventArgs e)
  {
    this.OnBranchRemoved(e);
    if (this.nodeRemoved == null)
      return;
    this.nodeRemoved((object) this, e);
  }

  /// <summary>Происходит когда изменен родительский узел (Parent)</summary>
  public event ParentChanged_EventHandler ParentChanged
  {
    add => this.parentChanged += value;
    remove => this.parentChanged -= value;
  }

  /// <summary>Генерирует событие ParentChanged</summary>
  protected virtual void OnParentChanged(ParentChanged_EventArgs e)
  {
    if (this.parentChanged == null)
      return;
    this.parentChanged((object) this, e);
  }

  /// <summary>Происходит когда изменено имя (Name) узла</summary>
  public event NameChanged_EventHandler NameChanged
  {
    add => this.nameChanged += value;
    remove => this.nameChanged -= value;
  }

  /// <summary>Генерирует событие NameChanged</summary>
  public virtual void OnNameChanged(NameChanged_EventArgs e)
  {
    if (this.nameChanged != null)
      this.nameChanged((object) this, e);
    this.SetPropertiesChangedFlag(true, true, false, true, true);
    this.OnChanged(new Changed_EventArgs());
  }

  /// <summary>Происходит когда произошли изменения</summary>
  public event Changed_EventHandler Changed
  {
    add => this.changed += value;
    remove => this.changed -= value;
  }

  /// <summary>Генерирует событие Changed</summary>
  public virtual void OnChanged(Changed_EventArgs e)
  {
    if (this.IsChanging || this.IsVirtualNode || this.changed == null)
      return;
    this.changed((object) this, e);
  }

  /// <summary>Генерируется когда два узла поменялись местами в пределах одного родителя</summary>
  public event ChildNodesPositionExchanged_EventHandler ChildNodesPositionExchanged
  {
    add => this.childNodesPositionExchanged += value;
    remove => this.childNodesPositionExchanged -= value;
  }

  /// <summary>Генерирует событие ChildNodesPositionExchanged</summary>
  public virtual void OnChildNodesPositionExchanged(ChildNodesPositionExchanged_EventArgs e)
  {
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo(this, e.Index1, e.Index2, true);
    if (this.IsChanging || this.childNodesPositionExchanged == null)
      return;
    this.childNodesPositionExchanged((object) this, e);
  }

  /// <summary>Генерируется когда дочерний узел меняет позицию</summary>
  public event ChildNodePositionChanged_EventHandler ChildNodePositionChanged
  {
    add => this.childNodePositionСhanged += value;
    remove => this.childNodePositionСhanged -= value;
  }

  /// <summary>Генерирует событие ChildNodePositionChanged</summary>
  public virtual void OnChildNodePositionChanged(ChildNodePositionChanged_EventArgs e)
  {
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo(this, e.OldIndex, e.NewIndex, false);
    if (this.IsChanging)
      return;
    ChildNodePositionChanged_EventHandler nodePositionСhanged = this.childNodePositionСhanged;
    if (nodePositionСhanged != null)
      nodePositionСhanged((object) this, e);
    if (e.UpdateUI)
      this.SynchronizeNodePositionWithUI(e.Node, e.OldIndex, e.NewIndex);
    this.OnChanged(new Changed_EventArgs());
  }

  /// <summary>
  /// Синхронизировать изменение позиции дочернего элемента дерева структуры с визуальным деревом документа
  /// </summary>
  /// <param name="oldIndex">Прежний индекс позиции дочернего элемента</param>
  /// <param name="newIndex">Новый индекс позиции дочернего элемента</param>
  public virtual void SynchronizeNodePositionWithUI(
    DocumentTreeNode node,
    int oldIndex,
    int newIndex)
  {
  }

  /// <summary>Происходит когда произошли изменения</summary>
  public event StructureChanging_EventHandler BeginStructureChangingEvent
  {
    add => this.beginStructureChanging += value;
    remove => this.beginStructureChanging -= value;
  }

  /// <summary>Генерирует событие Changed</summary>
  public virtual void OnBeginStructureChanging(StructureChanging_EventArgs e)
  {
    if (this.IsChanging || this.beginStructureChanging == null)
      return;
    this.beginStructureChanging((object) this, e);
  }

  /// <summary>Происходит когда произошли изменения</summary>
  public event StructureChanging_EventHandler EndStructureChangingEvent
  {
    add => this.endStructureChanging += value;
    remove => this.endStructureChanging -= value;
  }

  /// <summary>Генерирует событие Changed</summary>
  public virtual void OnEndStructureChanging(StructureChanging_EventArgs e)
  {
    if (this.IsChanging || this.endStructureChanging == null)
      return;
    this.endStructureChanging((object) this, e);
  }

  /// <summary>Можно ли использовать заданный узел как шаблон</summary>
  /// <param name="node">Узел</param>
  /// <returns></returns>
  public virtual bool CanUseNodeAsTemplate(DocumentTreeNode node) => node != null;

  /// <summary>Обновить ссылки на шаблоны</summary>
  /// <param name="applyTemplate">Применить шаблоны</param>
  /// <param name="recursive">Выполнить для всех подузлов</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public virtual void UpdateTemplateLinks(
    bool applyTemplate,
    bool recursive,
    bool updateUI,
    bool updateLayout)
  {
    if (this.referenceToTemplate != null)
      this.referenceToTemplate.UpdateLink(updateUI, updateLayout);
    if (applyTemplate)
    {
      DocumentTreeNode template = this.Template;
      if (template != null)
        this.ApplyTemplateTreeStructure(template, true, false, updateUI, updateLayout);
      if (this.nodes != null & recursive)
      {
        for (int index = 0; index < this.nodes.Count; ++index)
          this.nodes[index].UpdateTemplateLinks(applyTemplate, recursive, updateUI, updateLayout);
      }
      if (template == null)
        return;
      this.ApplyTemplateProperties(updateUI, updateLayout);
    }
    else
    {
      if (!recursive || this.nodes == null)
        return;
      for (int index = 0; index < this.nodes.Count; ++index)
        this.nodes[index].UpdateTemplateLinks(applyTemplate, recursive, updateUI, updateLayout);
    }
  }

  /// <summary>Свойства узла были изменены. Используется для обновления сделанных
  /// по шаблону узлов</summary>
  [Browsable(false)]
  public virtual bool PropertiesChangedFlag
  {
    [DebuggerStepThrough] get => this.CheckFlags((byte) 1);
    set
    {
      if (this.PropertiesChangedFlag == value)
        return;
      this.SetFlags((byte) 1, value);
      if (!value || this.SuspendedApplyThisTemplateFlag)
        return;
      this.ApplyThisTemplateChanges(false, true, true);
    }
  }

  /// <summary>Назначить значение свойству PropertiesChangedFlag без автоматических обновлений</summary>
  /// <param name="value">Значение свойства</param>
  /// <param name="applyTemplate">Применить по шаблону</param>
  /// <param name="recursive">Выполнить для всех подузлов</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public void SetPropertiesChangedFlag(
    bool value,
    bool applyTemplate,
    bool recursive,
    bool updateUI,
    bool updateLayout)
  {
    if (this.PropertiesChangedFlag == value)
      return;
    this.SetFlags((byte) 1, value);
    if (applyTemplate & value && !this.SuspendedApplyThisTemplateFlag)
      this.ApplyThisTemplateChanges(false, updateUI, updateLayout);
    if (!recursive || this.nodes == null)
      return;
    for (int index = 0; index < this.nodes.Count; ++index)
      this.nodes[index].SetPropertiesChangedFlag(value, recursive, false, updateUI, updateLayout);
  }

  /// <summary>Структура дерева была изменена. Используется для обновления
  /// структуры сделанных по шаблону узлов</summary>
  [Browsable(false)]
  protected virtual bool TreeStructureChangedFlag
  {
    [DebuggerStepThrough] get => ((uint) this.flags & 2U) > 0U;
    set
    {
      if (this.TreeStructureChangedFlag == value)
        return;
      this.SetFlags((byte) 2, value);
      if (!value || this.SuspendedApplyThisTemplateFlag)
        return;
      this.ApplyThisTemplateChanges(false, true, true);
    }
  }

  /// <summary>Установить значение свойства TreeStructureChangedFlag без автоматических обновлений</summary>
  /// <param name="value">Значение</param>
  /// <param name="recursive">Выполнить для всех подузлов</param>
  public void AssignTreeStructureChangedFlag(bool value, bool recursive)
  {
    this.SetFlags((byte) 2, value);
    if (!recursive || this.nodes == null)
      return;
    for (int index = 0; index < this.nodes.Count; ++index)
      this.nodes[index].AssignTreeStructureChangedFlag(value, recursive);
  }

  /// <summary>Применение шаблона заблокировано</summary>
  [Category("Debug")]
  protected bool SuspendedApplyThisTemplateFlag
  {
    [DebuggerStepThrough] get => this.suspendApplyThisTemplateCount > 0;
  }

  /// <summary>Установить значение поля suspendApplyThisTemplateCount</summary>
  /// <param name="count">Значение</param>
  protected void SetSuspendApplyThisTemplateCount(int count)
  {
    this.suspendApplyThisTemplateCount = count;
    if (this.nodes == null)
      return;
    for (int index = 0; index < this.nodes.Count; ++index)
      this.nodes[index].SetSuspendApplyThisTemplateCount(count);
  }

  /// <summary>Заблокировать применение шаблона</summary>
  protected virtual void SuspendApplyThisTemplate()
  {
    ++this.suspendApplyThisTemplateCount;
    if (this.nodes == null)
      return;
    for (int index = 0; index < this.nodes.Count; ++index)
      this.nodes[index].SuspendApplyThisTemplate();
  }

  /// <summary>Разблокировать применеине шаблона</summary>
  /// <param name="apply">Применить шаблон</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public void ResumeApplyThisTemplate(bool apply, bool updateUI, bool updateLayout)
  {
    if (this.suspendApplyThisTemplateCount > 0)
      --this.suspendApplyThisTemplateCount;
    else
      this.suspendApplyThisTemplateCount = 0;
    if (this.nodes != null)
    {
      for (int index = 0; index < this.nodes.Count; ++index)
        this.nodes[index].ResumeApplyThisTemplate(false, false, false);
    }
    if (!apply || this.suspendApplyThisTemplateCount != 0)
      return;
    this.ApplyThisTemplateChanges(true, updateUI, updateLayout);
  }

  /// <summary>Корень дерева в котором должен находиться шаблон этого узла</summary>
  [Browsable(false)]
  public abstract DocumentTreeNode TemplateRoot { get; }

  /// <summary>Узел является шаблоном</summary>
  [ReadOnly(true)]
  [CustomDisplayName("Attribute.Interfaces.Document_104")]
  [CustomDescription("Attribute.Interfaces.Document_105")]
  [CustomCategory("Attribute.Interfaces.Document_106")]
  [Browsable(false)]
  public abstract bool IsTemplate { get; }

  /// <summary>Вся цепочка по иерархии была сделана по шаблону с родителями
  /// (см. ClonedByTemplateWithParent)</summary>
  /// <returns>Вся цепочка по иерархии была сделана по шаблону</returns>
  public bool PathIsCompletelyClonedByTemplate()
  {
    DocumentTreeNode documentTreeNode = this;
    bool flag;
    for (flag = !documentTreeNode.IsIdServiceOwner; documentTreeNode != null & flag && !documentTreeNode.IsIdServiceOwner; documentTreeNode = documentTreeNode.Parent)
      flag = documentTreeNode.ClonedByTemplateWithParent;
    return flag;
  }

  /// <summary>Нужно зарезервировать идентификатор для последующего клонирования по шаблону</summary>
  /// <param name="idServeceOwner">Владелец сервиса уникальных идентификаторов</param>
  /// <returns>Нужно зарезервировать идентификатор</returns>
  public virtual bool ShouldReserveIdForTemplateClone(out DocumentTreeNode idServeceOwner)
  {
    idServeceOwner = (DocumentTreeNode) null;
    if (!this.IsTemplate || this.idService == null)
      return false;
    DocumentTreeNode documentTreeNode = this;
    bool flag = !documentTreeNode.IsIdServiceOwner;
    for (; documentTreeNode != null; documentTreeNode = documentTreeNode.Parent)
    {
      if (documentTreeNode.IsIdServiceOwner)
      {
        idServeceOwner = documentTreeNode;
        break;
      }
      flag = flag && documentTreeNode.CloneByTemplateWithParent;
    }
    return flag && idServeceOwner != null;
  }

  /// <summary>При создании копии родителя на основе шаблона,
  /// этот элемент тоже должен копироваться</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_107")]
  [CustomDescription("Attribute.Interfaces.Document_108")]
  [CustomCategory("Attribute.Interfaces.Document_109")]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public virtual bool CloneByTemplateWithParent
  {
    [DebuggerStepThrough] get => this.cloneByTemplateWithParent;
    set
    {
      if (this.cloneByTemplateWithParent == value)
        return;
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.CreateUndo((object) this, nameof (CloneByTemplateWithParent), (object) this.CloneByTemplateWithParent, (object) value);
      this.AssignCloneByTemplateWithParent(value);
      if (this.IsTemplate && this.Parent != null)
      {
        if (this.cloneByTemplateWithParent)
        {
          DocumentTreeNode idServeceOwner = (DocumentTreeNode) null;
          if (this.ShouldReserveIdForTemplateClone(out idServeceOwner))
            this.ReserveIdForTemplateClone(idServeceOwner);
          List<DocumentTreeNode> templateClones = this.Parent.GetTemplateClones();
          for (int index = 0; index < templateClones.Count; ++index)
          {
            DocumentTreeNode child = this.CloneFromTemplate(true, true);
            child.AssignClonedByTemplateWithParent(false);
            templateClones[index].AddChildNode(child, false, true, true, true);
          }
        }
        else
        {
          List<DocumentTreeNode> templateClones = this.Parent.GetTemplateClones();
          List<DocumentTreeNode> foundNodes = new List<DocumentTreeNode>();
          for (int index1 = 0; index1 < templateClones.Count; ++index1)
          {
            foundNodes.Clear();
            templateClones[index1].FindNodesFromTemplate(this, foundNodes);
            for (int index2 = 0; index2 < foundNodes.Count; ++index2)
            {
              if (foundNodes[index2].ClonedByTemplateWithParent)
                foundNodes[index2].Remove(true, true);
            }
          }
        }
      }
      this.OnChanged(new Changed_EventArgs());
    }
  }

  /// <summary>Назначить значение свойству CloneByTemplateWithParent,
  /// без выполнения автоматических действий</summary>
  /// <param name="value">Значение</param>
  public virtual void AssignCloneByTemplateWithParent(bool value)
  {
    this.cloneByTemplateWithParent = value;
  }

  /// <summary>Узел был клонирован по шаблону вместе с родителем (см. CloneByTemplateWithParent)</summary>
  [Category("Debug")]
  public bool ClonedByTemplateWithParent
  {
    [DebuggerStepThrough] get => this.clonedByTemplateWithParent;
  }

  /// <summary>Назначить значение свойства ClonedByTemplateWithParent</summary>
  /// <param name="value">Значение</param>
  public virtual void AssignClonedByTemplateWithParent(bool value)
  {
    this.clonedByTemplateWithParent = value;
  }

  /// <summary>Найти узлы использующие этот узел как шаблон</summary>
  /// <returns>Список найденных узлов</returns>
  public List<DocumentTreeNode> GetTemplateClones()
  {
    List<DocumentTreeNode> templateClones = new List<DocumentTreeNode>();
    if (this.connectionList != null)
    {
      for (int index = 0; index < this.connectionList.Count; ++index)
      {
        if (this.connectionList[index] is ReferenceToTemplate)
          templateClones.Add(this.connectionList[index].OwnerNode);
      }
    }
    return templateClones;
  }

  /// <summary>Применить к элементам дерева их шаблоны</summary>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public virtual void ApplyTreeTemplates(bool updateUI, bool updateLayout)
  {
    bool flag = !updateLayout || this.SuspendedUpdateLayoutFlag;
    if (!flag)
      this.SuspendUpdateLayout();
    try
    {
      List<DocumentTreeNode> documentTreeNodeList = this.ApplyTemplateTreeStructure(false, true, false, false);
      if (this.nodes != null)
      {
        for (int index = 0; index < this.nodes.Count; ++index)
        {
          if (documentTreeNodeList == null || !documentTreeNodeList.Contains(this.nodes[index]))
            this.nodes[index].ApplyTreeTemplates(false, false);
        }
      }
      this.ApplyTemplateProperties(updateUI, false);
    }
    finally
    {
      if (!flag)
        this.ResumeUpdateLayout(true, true);
    }
  }

  /// <summary>Применить к элементу свойства шаблона</summary>
  /// <param name="template">Шаблон</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  /// <param name="isLoading">Вызов в процессе загрузки</param>
  public virtual void ApplyTemplateProperties(
    DocumentTreeNode template,
    bool updateUI,
    bool updateLayout,
    bool isLoading)
  {
    if (template == null)
      return;
    this.overrideFlags2 |= template.overrideFlags2 & ~(OverrideFlags2.NextPageTemplateId | OverrideFlags2.LastPageTemplateId | OverrideFlags2.Name | OverrideFlags2.Reference);
  }

  /// <summary>Применить к элементу свойства шаблона</summary>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public void ApplyTemplateProperties(bool updateUI, bool updateLayout)
  {
    this.ApplyTemplateProperties(this.Template, updateUI, updateLayout, false);
  }

  /// <summary>Применить к элементу структуру его шаблона</summary>
  /// <param name="updateTemplateLinks">Обновлять ссылки на шаблоны</param>
  /// <param name="returnNewNodes">Вернуть новые узлы появившиеся в результате применения шаблона</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  /// <returns>Новые элементы дерева</returns>
  public virtual List<DocumentTreeNode> ApplyTemplateTreeStructure(
    bool updateTemplateLinks,
    bool returnNewNodes,
    bool updateUI,
    bool updateLayout)
  {
    return this.ApplyTemplateTreeStructure(this.Template, updateTemplateLinks, returnNewNodes, updateUI, updateLayout);
  }

  /// <summary>Применить структуру шаблона к узлам сделанным по шаблону</summary>
  /// <param name="template">Шаблон</param>
  /// <param name="updateTemplateLinks">Обновить ссылки на шаблон</param>
  /// <param name="returnNewNodes">Вернуть созданные в результате применения узлы</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  /// <returns>Созданные в результате применения узлы</returns>
  protected virtual List<DocumentTreeNode> ApplyTemplateTreeStructure(
    DocumentTreeNode template,
    bool updateTemplateLinks,
    bool returnNewNodes,
    bool updateUI,
    bool updateLayout)
  {
    List<DocumentTreeNode> documentTreeNodeList = (List<DocumentTreeNode>) null;
    int num = 0;
    if (template != null && template.nodes != null)
    {
      if (this.nodes == null)
        this.Nodes = new DocumentTreeNodeCollection(this);
      int index1 = 0;
      for (int count = template.nodes.Count; index1 < count; ++index1)
      {
        if (updateTemplateLinks && index1 < this.nodes.Count)
          this.nodes[index1].UpdateTemplateLinks(false, false, updateUI, updateLayout);
        if (template.nodes[index1].CloneByTemplateWithParent)
        {
          bool flag = true;
          for (int index2 = index1; index2 < this.nodes.Count; ++index2)
          {
            if (this.nodes[index2].Template != template.Nodes[index1] && this.nodes[index2].TemplateId == template.Nodes[index1].Id)
              this.nodes[index2].UpdateTemplateLinks(false, false, updateUI, updateLayout);
            if (this.nodes[index2].Template == template.Nodes[index1])
            {
              if (!this.nodes[index2].ClonedByTemplateWithParent)
                this.nodes[index2].AssignClonedByTemplateWithParent(true);
              if (index2 != num)
                this.nodes.Exchange(index2, num);
              flag = false;
              break;
            }
          }
          if (flag)
          {
            for (int index3 = 0; index3 < index1 && index3 < this.nodes.Count; ++index3)
            {
              if (this.nodes[index3].Template != template.Nodes[index1] && this.nodes[index3].TemplateId == template.Nodes[index1].Id)
                this.nodes[index3].UpdateTemplateLinks(false, false, updateUI, updateLayout);
              if (this.nodes[index3].Template == template.Nodes[index1])
              {
                if (!this.nodes[index3].ClonedByTemplateWithParent)
                  this.nodes[index3].AssignClonedByTemplateWithParent(true);
                if (index3 != num)
                  this.nodes.Exchange(index3, num);
                flag = false;
                break;
              }
            }
          }
          if (flag)
          {
            DocumentTreeNode child = template.nodes[index1].CloneFromTemplate(true, true);
            child.AssignClonedByTemplateWithParent(true);
            if (returnNewNodes)
            {
              if (documentTreeNodeList == null)
                documentTreeNodeList = new List<DocumentTreeNode>();
              documentTreeNodeList.Add(child);
            }
            this.InsertChildNode(num, child, false, true, updateUI, updateLayout);
          }
          ++num;
        }
      }
    }
    if (this.nodes != null)
    {
      for (int index = this.nodes.Count - 1; index >= num; --index)
      {
        if (this.nodes[index].ClonedByTemplateWithParent)
          this.nodes[index].Remove(true, true);
      }
    }
    return documentTreeNodeList;
  }

  /// <summary>Применить только изменения в дереве шаблона</summary>
  /// <param name="recursive">Вызывать для дочерних элементов</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public virtual void ApplyThisTemplateChanges(bool recursive, bool updateUI, bool updateLayout)
  {
    if (!this.IsTemplate)
      return;
    if ((this.TreeStructureChangedFlag || this.PropertiesChangedFlag) && this.connectionList != null)
    {
      for (int index = 0; index < this.connectionList.Count; ++index)
      {
        if (this.connectionList[index] is ReferenceToTemplate)
        {
          if (this.TreeStructureChangedFlag)
            this.connectionList[index].OwnerNode.ApplyTemplateTreeStructure(false, false, updateUI, updateLayout);
          if (this.PropertiesChangedFlag)
            this.connectionList[index].OwnerNode.ApplyTemplateProperties(updateUI, updateLayout);
        }
      }
    }
    this.TreeStructureChangedFlag = false;
    this.SetPropertiesChangedFlag(false, false, false, false, false);
    if (recursive && this.nodes != null)
    {
      for (int index = 0; index < this.nodes.Count; ++index)
        this.nodes[index].ApplyThisTemplateChanges(recursive, updateUI, updateLayout);
    }
    this.TreeStructureChangedFlag = false;
    this.SetPropertiesChangedFlag(false, false, false, false, false);
  }

  /// <summary>Шаблон этого узла</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_110")]
  [CustomDescription("Attribute.Interfaces.Document_111")]
  [CustomCategory("Attribute.Interfaces.Document_112")]
  [RefreshProperties(RefreshProperties.All)]
  [TypeConverter(typeof (TemplateLinkConverter))]
  public virtual DocumentTreeNode Template
  {
    [DebuggerStepThrough] get
    {
      return this.referenceToTemplate != null ? this.referenceToTemplate.NodeLink : (DocumentTreeNode) null;
    }
    set
    {
      if (this.Template == value)
        return;
      this.AssignTemplate(value, true, true, true);
      if (this.Template == null)
        return;
      this.ApplyTreeTemplates(true, true);
    }
  }

  /// <summary>Идентификатор шаблона. null, если шаблон не назначен</summary>
  public string TemplateId
  {
    get => this.referenceToTemplate != null ? this.referenceToTemplate.NodeId : (string) null;
  }

  /// <summary>Ссылка на шаблон</summary>
  protected ReferenceToTemplate ReferenceToTemplate
  {
    [DebuggerStepThrough] get => this.referenceToTemplate;
  }

  /// <summary>Применить новое значение Template</summary>
  /// <param name="value">Новое значение Template</param>
  /// <param name="applyTemplate">Применить шаблон</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public virtual void AssignTemplate(
    DocumentTreeNode value,
    bool applyTemplate,
    bool updateUI,
    bool updateLayout)
  {
    if (this.Template == value)
      return;
    this.AssignReferenceToTemplate(value);
    if (applyTemplate && this.nodes != null && this.Template == null)
    {
      for (int index = this.nodes.Count - 1; index > -1; --index)
      {
        if (this.nodes[index].ClonedByTemplateWithParent)
          this.nodes[index].Remove(updateUI, updateLayout);
      }
    }
    if (!applyTemplate)
      return;
    this.UpdateTemplateLinks(true, true, updateUI, updateLayout);
  }

  public void ReplaceTemplatesRecursive(DocumentTreeNode value)
  {
    if (this.Template == value)
      return;
    this.AssignReferenceToTemplate(value);
    for (int index = 0; index < this.NodesCount && index < value.NodesCount; ++index)
      this.Nodes[index].ReplaceTemplatesRecursive(value.Nodes[index]);
  }

  /// <summary>Назначить новую ссылку на шаблон</summary>
  /// <param name="templateId">Идентификатор ссылки</param>
  /// <param name="updateLink">Обновить ссылку</param>
  protected virtual void AssignReferenceToTemplate(string templateId, bool updateLink)
  {
    if (this.referenceToTemplate == null)
      this.referenceToTemplate = new ReferenceToTemplate(this);
    if (!(this.referenceToTemplate.NodeId != templateId))
      return;
    this.referenceToTemplate.SetReference((DocumentTreeNode) null);
    this.referenceToTemplate.SetReference(templateId, updateLink);
  }

  /// <summary>Назначить новую ссылку на шаблон</summary>
  /// <param name="template">Шаблон</param>
  protected virtual void AssignReferenceToTemplate(DocumentTreeNode template)
  {
    if (template != null)
    {
      if (this.referenceToTemplate == null)
        this.referenceToTemplate = new ReferenceToTemplate(this);
      if (!(this.referenceToTemplate.NodeId != template.Id) && this.referenceToTemplate.NodeLink == template)
        return;
      this.referenceToTemplate.SetReference((DocumentTreeNode) null);
      this.referenceToTemplate.SetReference(template);
    }
    else
    {
      this.referenceToTemplate.SetReference((DocumentTreeNode) null);
      this.referenceToTemplate = (ReferenceToTemplate) null;
    }
  }

  /// <summary>Найти шаблон этого узла по идентификатору templateId</summary>
  /// <returns>Шаблон узла</returns>
  public abstract DocumentTreeNode FindTemplate(string templateId);

  /// <summary>Создать копию элемента используя этот узел как шаблон</summary>
  /// <param name="copyChildren">Копировать дочерние узлы</param>
  /// <param name="copyDataNodes">Копировать узлы-данные в таблицах</param>
  /// <returns>Копия узла</returns>
  public virtual DocumentTreeNode CloneFromTemplate(bool copyChildren, bool copyDataNodes)
  {
    IDictionary links = (IDictionary) new HybridDictionary();
    DocumentTreeNode documentTreeNode = this.InternalClone(copyChildren, true, copyDataNodes, true, links);
    documentTreeNode.CallOnDeserializationRecursive();
    this.RestoreLinks(copyChildren, true, true, links);
    documentTreeNode.AfterCloneFromTemplate();
    return documentTreeNode;
  }

  /// <summary>Создать копию элемента используя этот узел как шаблон</summary>
  /// <returns>Копия узла</returns>
  public DocumentTreeNode CloneFromTemplate() => this.CloneFromTemplate(true, true);

  /// <summary>Имеют ли элементы шаблон</summary>
  /// <param name="context"></param>
  /// <returns></returns>
  public virtual bool HasTemplate() => this.TemplateId != null;

  /// <summary>Действия производимые после копирования из шаблона</summary>
  public virtual void AfterCloneFromTemplate()
  {
  }

  /// <summary>Рекурсивно отвязать узел от шаблона, сохранив структуру и данные</summary>
  public virtual void DisconnectTemplateRecursive()
  {
    this.Template = (DocumentTreeNode) null;
    if (this.nodes == null)
      return;
    for (int index = 0; index < this.nodes.Count; ++index)
      this.nodes[index].DisconnectTemplateRecursive();
  }

  /// <summary>Сбросить наследование параметров от родителей</summary>
  public virtual void ResetInheritance()
  {
  }

  /// <summary>Сервис идентификаторов. Используется для генерации уникальных ид.,
  /// проверки уникальности</summary>
  [Browsable(false)]
  public virtual IUniqueIdService IdService
  {
    [DebuggerStepThrough] get => this.idService;
    set
    {
      if (this.idService == value)
        return;
      if (this.idService != null)
        this.idService.RemoveId((object) this.id);
      this.idService = value;
      if (this.idService != null)
      {
        string id = this.id;
        DocumentTreeNode idServeceOwner = (DocumentTreeNode) null;
        if (this.ShouldReserveIdForTemplateClone(out idServeceOwner))
        {
          this.ReserveIdForTemplateClone(idServeceOwner);
        }
        else
        {
          DocumentTreeNode documentTreeNode = (DocumentTreeNode) null;
          if (id != null)
            documentTreeNode = this.idService[(object) id] as DocumentTreeNode;
          if (id != null && documentTreeNode == null)
            this.idService.AddId((object) id, (object) this);
          else if (id == null || documentTreeNode != null && this.idService[(object) id] != this)
          {
            id = this.idService.GenerateUniqueId((object) id).ToString();
            this.idService.AddId((object) id, (object) this);
          }
          if (this.id != id)
            this.Id = id;
        }
      }
      if (this.nodes == null)
        return;
      for (int index = 0; index < this.nodes.Count; ++index)
        this.nodes[index].IdService = this.idService;
    }
  }

  /// <summary>Ограниченная версия привязки к сервису Id.
  /// Используется для восстановления связей. При конфликтах ид генерирует исключение.</summary>
  /// <param name="value">Сервис Id</param>
  /// <param name="recursive">Назначить всем дочерним элементам</param>
  public void AssignIdService(IUniqueIdService value, bool recursive)
  {
    if (this.idService == value)
      return;
    this.idService = value;
    if (this.idService != null)
      this.idService.AddId((object) this.id, (object) this);
    if (!recursive || this.nodes == null)
      return;
    for (int index = 0; index < this.nodes.Count; ++index)
      this.nodes[index].AssignIdService(this.idService, recursive);
  }

  /// <summary>Идентификатор элемента. Уникальность обеспечивается IdService</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_113")]
  [CustomDescription("Attribute.Interfaces.Document_114")]
  [CustomCategory("Attribute.Interfaces.Document_115")]
  public virtual string Id
  {
    [DebuggerStepThrough] get => this.id;
    set
    {
      if (value == "")
        value = (string) null;
      if (!(this.id != value))
        return;
      if (this.idService != null)
      {
        if (value == null)
          throw new Exception("Значение идентификатора не может быть пустым!");
        if (value != null && (!this.idService.ContainsId((object) value) || this.idService[(object) value] != this))
          this.idService.AddId((object) value, (object) this);
        if (this.id != null && this.idService[(object) this.id] == this)
          this.idService.RemoveId((object) this.id);
      }
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.CreateUndo((object) this, nameof (Id), (object) this.Id, (object) value);
      this.id = value;
      if (this.connectionList != null)
      {
        for (int index = 0; index < this.connectionList.Count; ++index)
        {
          if (this.connectionList[index] is ReferenceToNodeId connection)
            connection.SetReference(this);
        }
      }
      DocumentTreeNode idServeceOwner = (DocumentTreeNode) null;
      if (this.ShouldReserveIdForTemplateClone(out idServeceOwner))
        this.SetPropertiesChangedFlag(true, true, false, true, true);
      this.OnNameChanged(new NameChanged_EventArgs(this.Name));
    }
  }

  /// <summary>Вспомогательный метод. Возвращает true, если строка Null или ""</summary>
  /// <param name="str">Строка</param>
  /// <returns>Возвращает true, если строка Null или ""</returns>
  public static bool IsEmptyString(string str) => str == null || str == string.Empty;

  /// <summary>Имя узла</summary>
  [RefreshProperties(RefreshProperties.All)]
  [CustomDisplayName("Attribute.Interfaces.Document_116")]
  [CustomDescription("Attribute.Interfaces.Document_117")]
  [CustomCategory("Attribute.Interfaces.Document_118")]
  public virtual string Name
  {
    [DebuggerStepThrough] get => this.GetName();
    set => this.SetName(value, true, true);
  }

  /// <summary>Получить имя элемента</summary>
  /// <returns>Имя элемента</returns>
  public virtual string GetName()
  {
    string str = (string) null;
    if (!DocumentTreeNode.IsEmptyString(this.name))
      str = this.name;
    else if ((this.overrideFlags2 & OverrideFlags2.Name) == OverrideFlags2.None)
    {
      DocumentTreeNode template = this.Template;
      if (template != null)
        str = template.Name;
    }
    return str ?? "";
  }

  /// <summary>Назначить свойству Name новое значение</summary>
  /// <param name="value">Значение</param>
  /// <param name="updateUI">Обновить изображение</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public virtual void SetName(string value, bool updateUI, bool updateLayout)
  {
    if (!(this.Name != value))
      return;
    if (value != null && value != "" && (value.ToLower() == "/d" || value == "debug" || value.ToLower() == ".в"))
      ImDocumentData.ShowDebugInfo = true;
    else
      this.SetAttributeValue(DocumentTreeNode.AttributeName_Name, value, updateUI: updateUI, updateLayout: updateLayout);
  }

  /// <summary>Установить значение Name</summary>
  /// <param name="value">Новое значение</param>
  protected void AssignName(string value)
  {
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((object) this, "Name", (object) this.Name, (object) value);
    if (!(this.Name != value))
      return;
    this.name = value;
    this.overrideFlags2 |= OverrideFlags2.Name;
    this.OnNameChanged(new NameChanged_EventArgs(this.name));
  }

  /// <summary>Узел является владельцем сервиса уникальных идентификаторов</summary>
  [Browsable(false)]
  public virtual bool IsIdServiceOwner
  {
    [DebuggerStepThrough] get => false;
  }

  /// <summary>Заменить десятичный разделитель</summary>
  /// <param name="value"></param>
  /// <returns></returns>
  public static string ReplaceDS(string value) => value.Replace(',', '.');

  /// <summary>Получить имя типа из XML атрибутов</summary>
  /// <param name="xmlAttributes">XML атрибуты</param>
  /// <returns>Имя типа</returns>
  protected string GetTypeNameFromXmlAttributes(List<StringKeyValue> xmlAttributes)
  {
    if (xmlAttributes != null)
    {
      for (int index = 0; index < xmlAttributes.Count; ++index)
      {
        if (xmlAttributes[index].Key == "type")
          return xmlAttributes[index].Value;
      }
    }
    return (string) null;
  }

  /// <summary>Имя типа сохраняемое в XML</summary>
  [Category("Debug")]
  [Browsable(false)]
  public virtual string TypeNameForXml
  {
    [DebuggerStepThrough] get
    {
      string fromXmlAttributes = this.GetTypeNameFromXmlAttributes(this.unknownXmlAttributes);
      return fromXmlAttributes != null && fromXmlAttributes != "" ? fromXmlAttributes : this.GetType().Name;
    }
  }

  /// <summary>Добавить неизвесный атрибут</summary>
  /// <param name="key">Имя атрибута</param>
  /// <param name="value">Значение атрибута</param>
  public void AddUnknownXmlAttribute(string key, string value)
  {
    if (this.unknownXmlAttributes == null)
      this.unknownXmlAttributes = new List<StringKeyValue>();
    this.unknownXmlAttributes.Add(new StringKeyValue(key, value));
  }

  /// <summary>XML атрибуты, не распознанные при загрузке</summary>
  [Browsable(false)]
  [Category("Debug")]
  public virtual List<StringKeyValue> UnknownXmlAttributes
  {
    get => this.unknownXmlAttributes;
    set => this.unknownXmlAttributes = value;
  }

  /// <summary>XML элементы, не распознанные при загрузке</summary>
  [Browsable(false)]
  [Category("Debug")]
  public virtual string UnknownXmlElements
  {
    [DebuggerStepThrough] get => this.unknownXmlElements;
    set => this.unknownXmlElements = value;
  }

  /// <summary>Конвертер для типа Color</summary>
  public static ColorConverter ColorConverter
  {
    [DebuggerStepThrough] get
    {
      if (DocumentTreeNode.colorConverter == null)
        DocumentTreeNode.colorConverter = new ColorConverter();
      return DocumentTreeNode.colorConverter;
    }
  }

  internal static Type GetTypeFromXmlTypeName(string typeName)
  {
    return !DocumentTreeNode.IsEmptyString(typeName) ? DocumentTreeNode.TypeNameDictionary[(object) typeName] as Type : throw new ArgumentNullException(nameof (typeName));
  }

  internal static DocumentTreeNode CreateNodeFromXmlTypeName(string typeName)
  {
    DocumentTreeNode nodeFromXmlTypeName = (DocumentTreeNode) null;
    if (DocumentTreeNode.TypeConstructorDictionary[(object) typeName] is EmptyConstructorDelegate typeConstructor)
      nodeFromXmlTypeName = typeConstructor() as DocumentTreeNode;
    if (nodeFromXmlTypeName == null)
    {
      Type type = typeof (DocumentTreeNode);
      string str = type.Namespace;
      nodeFromXmlTypeName = Assembly.GetAssembly(type).CreateInstance($"{str}.{typeName}") as DocumentTreeNode;
    }
    return nodeFromXmlTypeName;
  }

  /// <summary>Записать поля в XML</summary>
  /// <param name="elementName">Имя элемента XML, под которым нужно сохранить данные</param>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  public void WriteToXml(string elementName, XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    xw.WriteStartElement(elementName);
    this.WriteXmlAttributes(xw, objectRefId);
    this.WriteXmlElements(xw, objectRefId);
    xw.WriteEndElement();
  }

  /// <summary>Сохранить данные в атрибуты XML</summary>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  public virtual void WriteXmlAttributes(XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    DocumentTreeNode template = this.Template;
    bool firstTime = false;
    xw.WriteAttributeString("refId", objectRefId.GetId((object) this, out firstTime).ToString((IFormatProvider) CultureInfo.InvariantCulture));
    if (this.id == null || this.id == "" || this.IdService.ContainsId((object) this.id) && this.IdService[(object) this.id] != this)
      this.Id = this.IdService.GenerateUniqueId().ToString();
    xw.WriteAttributeString("nodeId", this.id);
    if (!DocumentTreeNode.IsEmptyString(this.name) && (template == null || (this.overrideFlags2 & OverrideFlags2.Name) != OverrideFlags2.None))
      xw.WriteAttributeString("name", this.name);
    if (this.referenceToTemplate != null && this.referenceToTemplate.NodeId != null)
      xw.WriteAttributeString("templateId", this.referenceToTemplate.NodeId.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    if (this.IsTemplate && (!this.cloneByTemplateWithParent || this is PageData))
      xw.WriteAttributeString("clone", this.cloneByTemplateWithParent.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    if (!this.IsTemplate && this.clonedByTemplateWithParent)
      xw.WriteAttributeString("cloned", this.clonedByTemplateWithParent.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    if (this.overrideFlags2 != OverrideFlags2.None)
      xw.WriteAttributeString("override", ((int) this.overrideFlags2).ToString((IFormatProvider) CultureInfo.InvariantCulture));
    if (this.unknownXmlAttributes == null)
      return;
    for (int index = 0; index < this.unknownXmlAttributes.Count; ++index)
    {
      if (this.unknownXmlAttributes[index].Key != "type")
        xw.WriteAttributeString(this.unknownXmlAttributes[index].Key, this.unknownXmlAttributes[index].Value);
    }
  }

  /// <summary>Сохранить данные в элементы XML</summary>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  public virtual void WriteXmlElements(XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    this.WriteAdditionalAttributesToXml(xw, objectRefId);
    if (this.unknownXmlElements != null && this.unknownXmlElements != "")
      xw.WriteRaw(this.unknownXmlElements);
    if (this.nodes == null || this.nodes.Count <= 0)
      return;
    xw.WriteStartElement("Nodes");
    for (int index = 0; index < this.nodes.Count; ++index)
      this.nodes[index].WriteToXml(XmlConvert.EncodeName(this.nodes[index].TypeNameForXml), xw, objectRefId);
    xw.WriteEndElement();
  }

  /// <summary>Сохранить ссылку на другой объект как элемент XML</summary>
  /// <param name="name">Имя элемента XML</param>
  /// <param name="element">Объект, ссылка на который сохраняется</param>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  /// <param name="firstTime">Первый ли раз идентификатор на объект</param>
  protected void WriteXmlElementReference(
    string name,
    object element,
    XmlWriter xw,
    ObjectIDGenerator objectRefId,
    out bool firstTime)
  {
    if (element != null)
      xw.WriteElementString(name, objectRefId.GetId(element, out firstTime).ToString((IFormatProvider) CultureInfo.InvariantCulture));
    else
      firstTime = false;
  }

  /// <summary>Получить виртуальные атрибуты плагинов</summary>
  /// <param name="attrCollection">Коллекция в которую добавляются атрибуты</param>
  /// <returns>Коллекция имён добавленных атрибутов</returns>
  private StringCollection GetPluginVirtualAttributes(AdditionalAttributeCollection attrCollection)
  {
    if (attrCollection == null)
      throw new ArgumentNullException(nameof (attrCollection));
    StringCollection attributeNames = new StringCollection();
    if (this.getPluginVirtualAttributeNames != null)
    {
      this.getPluginVirtualAttributeNames((object) this, attributeNames, true);
      if (attributeNames.Count > 0)
      {
        for (int index = attributeNames.Count - 1; index >= 0; --index)
        {
          GetVirtualAttributeResult virtualAttributeValue = this.GetVirtualAttributeValue(attributeNames[index], true);
          if (virtualAttributeValue.Found)
            attrCollection.SetAttributeStringValue(attributeNames[index], virtualAttributeValue.Value);
          else
            attributeNames.RemoveAt(index);
        }
      }
    }
    return attributeNames;
  }

  /// <summary>Сохранить дополнительные атрибуты в XML</summary>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  protected void WriteAdditionalAttributesToXml(XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    if (this.additionalAttributes == null)
      this.additionalAttributes = new AdditionalAttributeCollection(this);
    StringCollection virtualAttributes = this.GetPluginVirtualAttributes(this.additionalAttributes);
    if (this.additionalAttributes == null || this.additionalAttributes.Count <= 0)
      return;
    WriteReadXmlHelper.WriteStringDictionaryToXml("AdditionalAttributes", this.additionalAttributes.Attributes, "Attr", xw, objectRefId);
    for (int index = 0; index < virtualAttributes.Count; ++index)
      this.additionalAttributes.RemoveAttribute(virtualAttributes[index]);
  }

  /// <summary>Для внутреннего пользования!
  /// Восстановить поля в оригинальном типе. Для объектов которые были загружены на сервере в базовые типы,
  /// а затем восстанавливаются в оригинальных типах на клиенте</summary>
  /// <param name="links">Ссылки</param>
  /// <returns></returns>
  public DocumentTreeNode RestoreToOriginalType(IDictionary links)
  {
    if (links == null)
      throw new ArgumentNullException(nameof (links));
    string fromXmlAttributes = this.GetTypeNameFromXmlAttributes(this.unknownXmlAttributes);
    DocumentTreeNode owner = (DocumentTreeNode) null;
    if (fromXmlAttributes != null && fromXmlAttributes != "")
      owner = DocumentTreeNode.CreateNodeFromXmlTypeName(fromXmlAttributes);
    if (owner != null)
    {
      owner.CopyFields(this, false, true, true, false, true, links);
      owner.RestoreFieldsFromUnknownXml();
    }
    else
      owner = this.InternalClone(false, true, false, false, links);
    if (this.nodes != null)
    {
      if (owner.nodes == null)
        owner.nodes = new DocumentTreeNodeCollection(owner);
      for (int index = 0; index < this.nodes.Count; ++index)
      {
        DocumentTreeNode originalType = this.nodes[index].RestoreToOriginalType(links);
        owner.AddChildNode(originalType, false, true, false, false);
      }
    }
    return owner;
  }

  /// <summary>Восстановить поля из UnknownXmlAttributes и UnknownXmlElements</summary>
  public virtual void RestoreFieldsFromUnknownXml()
  {
    this.unknownXmlAttributes = (List<StringKeyValue>) null;
    this.unknownXmlElements = (string) null;
  }

  private static void InitReadFieldDict()
  {
    DocumentTreeNode.ReadFieldsDict = new Dictionary<string, ReadFieldFromXmlDelegate>();
    DocumentTreeNode.ReadFieldsDict.Add("refId", new ReadFieldFromXmlDelegate(DocumentTreeNode.ReadRefId));
    DocumentTreeNode.ReadFieldsDict.Add("nodeId", new ReadFieldFromXmlDelegate(DocumentTreeNode.ReadNodeId));
    DocumentTreeNode.ReadFieldsDict.Add("templateId", new ReadFieldFromXmlDelegate(DocumentTreeNode.ReadTemplateId));
    DocumentTreeNode.ReadFieldsDict.Add("Nodes", new ReadFieldFromXmlDelegate(DocumentTreeNode.ReadNodes));
    DocumentTreeNode.ReadFieldsDict.Add("name", new ReadFieldFromXmlDelegate(DocumentTreeNode.ReadName));
    DocumentTreeNode.ReadFieldsDict.Add("cloned", new ReadFieldFromXmlDelegate(DocumentTreeNode.ReadCloned));
    DocumentTreeNode.ReadFieldsDict.Add("clone", new ReadFieldFromXmlDelegate(DocumentTreeNode.ReadClone));
    DocumentTreeNode.ReadFieldsDict.Add("AdditionalAttributes", new ReadFieldFromXmlDelegate(DocumentTreeNode.ReadAdditionalAttributes));
    DocumentTreeNode.ReadFieldsDict.Add("override", new ReadFieldFromXmlDelegate(DocumentTreeNode.ReadOverride));
  }

  private static void ReadRefId(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    if (readArgs.ObjectsId.Contains((object) readArgs.Reader.Value))
      return;
    readArgs.ObjectsId.Add((object) readArgs.Reader.Value, (object) docNode);
  }

  private static void ReadNodeId(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    docNode.id = readArgs.Reader.Value;
    if (docNode.id == null || docNode.id == "")
      docNode.id = Guid.NewGuid().ToString();
    if (docNode.idService == null)
      return;
    DocumentTreeNode documentTreeNode = docNode.idService[(object) docNode.id] as DocumentTreeNode;
    if (documentTreeNode == docNode)
      return;
    if (documentTreeNode != null)
      documentTreeNode.Id = docNode.idService.GenerateUniqueId((object) documentTreeNode.Id).ToString();
    docNode.idService.AddId((object) docNode.id, (object) docNode);
  }

  private static void ReadTemplateId(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    string str = readArgs.Reader.Value;
    if (docNode.referenceToTemplate == null)
      docNode.referenceToTemplate = new ReferenceToTemplate(docNode, str);
    else
      docNode.referenceToTemplate.SetReference(str, false);
    DocumentTreeNode documentTreeNode = readArgs.TemplateRoot == null ? docNode.TemplateRoot : readArgs.TemplateRoot;
    if (documentTreeNode == null)
      return;
    DocumentTreeNode node = documentTreeNode.FindNode(str);
    if (!docNode.CanUseNodeAsTemplate(node))
      return;
    docNode.referenceToTemplate.AssignNodeLink(node);
  }

  protected static void ReadNodes(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    DocumentTreeNodeCollection treeNodeCollection = docNode.nodes;
    if (treeNodeCollection == null)
    {
      treeNodeCollection = new DocumentTreeNodeCollection(docNode);
      docNode.nodes = treeNodeCollection;
    }
    lock (treeNodeCollection)
    {
      string localName1 = readArgs.Reader.LocalName;
      bool flag1 = readArgs.Reader.IsEmptyElement;
      if (readArgs.Version > 12 && !readArgs.DataOnly)
      {
        while (!flag1 && readArgs.Reader.Read())
        {
          switch (readArgs.Reader.NodeType)
          {
            case XmlNodeType.Element:
              string localName2 = readArgs.Reader.LocalName;
              DocumentTreeNode nodeFromXmlTypeName1 = DocumentTreeNode.CreateNodeFromXmlTypeName(localName2);
              if (nodeFromXmlTypeName1 != null)
              {
                nodeFromXmlTypeName1.AssignNeedUpdateLayoutFlag(false);
                nodeFromXmlTypeName1.suspendUpdateLayoutCount = docNode.suspendUpdateLayoutCount;
                docNode.nodes.AddInternal(nodeFromXmlTypeName1);
                if (docNode.idService != null && nodeFromXmlTypeName1.idService != docNode.idService)
                  nodeFromXmlTypeName1.idService = docNode.idService;
                nodeFromXmlTypeName1.AssignParent(docNode, false, false, true);
                nodeFromXmlTypeName1.ReadFromXml(readArgs);
                nodeFromXmlTypeName1.ApplyTemplateProperties(nodeFromXmlTypeName1.Template, false, false, true);
                continue;
              }
              LogManager.AddLine($"ImDoc. Ошибка ReadNodesFromXml - Неизвестный тип:{docNode.GetType().Namespace}.{localName2}");
              docNode.unknownXmlElements += readArgs.Reader.ReadOuterXml();
              readArgs.SkipRead = true;
              continue;
            case XmlNodeType.EndElement:
              if (localName1 == readArgs.Reader.LocalName)
              {
                flag1 = true;
                continue;
              }
              continue;
            default:
              continue;
          }
        }
      }
      else
      {
        while (!flag1 && readArgs.Reader.Read())
        {
          switch (readArgs.Reader.NodeType)
          {
            case XmlNodeType.Element:
              string typeName = readArgs.Reader.LocalName;
              string str1 = typeName;
              bool flag2 = true;
              if (readArgs.Version < 10 && (typeName == "TableColumn" || typeName == "TableRow"))
              {
                flag2 = typeName == "TableColumn";
                typeName = !(docNode.GetType().Namespace == "Intermech.Interfaces.Document") ? "TableElement" : "TableData";
              }
              if (readArgs.Version < 13)
              {
                switch (typeName)
                {
                  case "TextData":
                    typeName = "OldTextData";
                    break;
                  case "TableData":
                    typeName = "OldTableData";
                    break;
                }
              }
              string str2 = (string) null;
              if (readArgs.DataOnly)
              {
                switch (typeName)
                {
                  case "TableElement":
                    str2 = typeName;
                    typeName = "TableData";
                    break;
                  case "TextBoxElement":
                    str2 = typeName;
                    typeName = "TextData";
                    break;
                  case "LabelElement":
                    str2 = typeName;
                    typeName = "TextData";
                    break;
                  case "ContainerElement":
                    str2 = typeName;
                    typeName = "ContainerData";
                    break;
                  case "Page":
                    str2 = typeName;
                    typeName = "PageData";
                    break;
                  case "Polyline":
                    str2 = typeName;
                    typeName = "PolylineData";
                    break;
                }
              }
              DocumentTreeNode nodeFromXmlTypeName2 = DocumentTreeNode.CreateNodeFromXmlTypeName(typeName);
              if (nodeFromXmlTypeName2 != null)
              {
                if (str2 != null)
                  nodeFromXmlTypeName2.AddUnknownXmlAttribute("type", str2);
                if (readArgs.Version < 10 && str1 == "TableRow")
                  nodeFromXmlTypeName2.GetType().GetField("isColumn", BindingFlags.Instance | BindingFlags.NonPublic).SetValue((object) nodeFromXmlTypeName2, (object) flag2);
                nodeFromXmlTypeName2.suspendUpdateLayoutCount = docNode.suspendUpdateLayoutCount;
                if (docNode.idService != null && nodeFromXmlTypeName2.idService != docNode.idService)
                  nodeFromXmlTypeName2.idService = docNode.idService;
                docNode.nodes.AddInternal(nodeFromXmlTypeName2);
                nodeFromXmlTypeName2.AssignParent(docNode, false, false, true);
                nodeFromXmlTypeName2.ReadFromXml(readArgs);
                nodeFromXmlTypeName2.ApplyTemplateProperties(nodeFromXmlTypeName2.Template, false, false, true);
                continue;
              }
              LogManager.AddLine($"ImDoc. Ошибка ReadNodesFromXml - Неизвестный тип:{docNode.GetType().Namespace}.{typeName}");
              continue;
            case XmlNodeType.EndElement:
              if (localName1 == readArgs.Reader.LocalName)
              {
                flag1 = true;
                continue;
              }
              continue;
            default:
              continue;
          }
        }
      }
    }
  }

  private static void ReadName(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    docNode.name = readArgs.Reader.Value;
    docNode.overrideFlags2 |= OverrideFlags2.Name;
  }

  private static void ReadCloned(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    docNode.clonedByTemplateWithParent = bool.Parse(readArgs.Reader.Value);
  }

  private static void ReadClone(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    docNode.cloneByTemplateWithParent = bool.Parse(readArgs.Reader.Value);
  }

  private static void ReadOverride(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    docNode.overrideFlags2 = (OverrideFlags2) int.Parse(readArgs.Reader.Value);
    if ((docNode.overrideFlags2 & OverrideFlags2.NonSkipBeforeAtStartPage) == OverrideFlags2.None)
      return;
    docNode.SetFlags((byte) 64 /*0x40*/, true);
    docNode.overrideFlags2 &= ~OverrideFlags2.NonSkipBeforeAtStartPage;
  }

  private static void ReadAdditionalAttributes(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    if (docNode.AdditionalAttributes == null)
      docNode.AdditionalAttributes = new AdditionalAttributeCollection(docNode);
    docNode.ReadAdditionalAttributesFromXml(readArgs);
  }

  /// <summary>Загрузить поле из текущего узла XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns>Возвращает true, если поле загружено</returns>
  public virtual bool ReadFieldFromXml(XmlReadArgs readArgs)
  {
    if (DocumentTreeNode.ReadFieldsDict != null)
    {
      ReadFieldFromXmlDelegate fieldFromXmlDelegate;
      DocumentTreeNode.ReadFieldsDict.TryGetValue(readArgs.Reader.LocalName, out fieldFromXmlDelegate);
      if (fieldFromXmlDelegate != null)
      {
        fieldFromXmlDelegate(this, readArgs);
        return true;
      }
    }
    switch (readArgs.Reader.LocalName)
    {
      case "AdditionalAttributes":
        if (this.additionalAttributes == null)
          this.AdditionalAttributes = new AdditionalAttributeCollection(this);
        this.ReadAdditionalAttributesFromXml(readArgs);
        return true;
      case "Nodes":
        if (this.nodes == null)
          this.nodes = new DocumentTreeNodeCollection(this);
        this.ReadNodesFromXml(readArgs);
        return true;
      case "clone":
        this.cloneByTemplateWithParent = bool.Parse(readArgs.Reader.Value);
        return true;
      case "cloned":
        this.clonedByTemplateWithParent = bool.Parse(readArgs.Reader.Value);
        return true;
      case "name":
        this.name = readArgs.Reader.Value;
        return true;
      case "nodeId":
        this.id = readArgs.Reader.Value;
        return true;
      case "override":
        DocumentTreeNode.ReadOverride(this, readArgs);
        return true;
      case "refId":
        readArgs.ObjectsId.Add((object) readArgs.Reader.Value, (object) this);
        return true;
      case "templateId":
        if (this.referenceToTemplate == null)
          this.referenceToTemplate = new ReferenceToTemplate(this);
        this.referenceToTemplate.SetReference(readArgs.Reader.Value, false);
        return true;
      default:
        if (readArgs.Version < 10)
        {
          switch (readArgs.Reader.LocalName)
          {
            case "cloneByTemplateWithParent":
              this.cloneByTemplateWithParent = bool.Parse(readArgs.Reader.Value);
              return true;
            case "clonedByTemplateWithParent":
              this.clonedByTemplateWithParent = bool.Parse(readArgs.Reader.Value);
              return true;
          }
        }
        return false;
    }
  }

  /// <summary>Загрузить дочерние узлы</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  protected void ReadNodesFromXml(XmlReadArgs readArgs)
  {
    DocumentTreeNode.ReadNodes(this, readArgs);
  }

  /// <summary>Запускается после загрузки дерева документа из XML</summary>
  protected virtual void ReadNodeFromXmlPostProcess(XmlReadArgs readArgs)
  {
    if (this.nodes == null)
      return;
    for (int index = 0; index < this.nodes.Count; ++index)
      this.nodes[index].ReadNodeFromXmlPostProcess(readArgs);
  }

  /// <summary>Загрузить дополнительные атрибуты</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  protected virtual void ReadAdditionalAttributesFromXml(XmlReadArgs readArgs)
  {
    if (readArgs.Version < 12)
    {
      string localName = readArgs.Reader.LocalName;
      string name = (string) null;
      string attributeValue = (string) null;
      bool flag = readArgs.Reader.IsEmptyElement;
      while (!flag && readArgs.Reader.Read())
      {
        switch (readArgs.Reader.NodeType)
        {
          case XmlNodeType.Element:
            name = readArgs.Reader.LocalName;
            if (readArgs.Reader.IsEmptyElement)
            {
              this.SetAttributeValue(XmlConvert.DecodeName(name), attributeValue, false, false, false);
              name = (string) null;
              attributeValue = (string) null;
              continue;
            }
            continue;
          case XmlNodeType.Text:
            attributeValue = readArgs.Reader.Value;
            continue;
          case XmlNodeType.EndElement:
            if (name == readArgs.Reader.LocalName)
            {
              this.SetAttributeValue(XmlConvert.DecodeName(name), attributeValue, false, false, false);
              name = (string) null;
              attributeValue = (string) null;
              continue;
            }
            if (localName == readArgs.Reader.LocalName)
            {
              flag = true;
              continue;
            }
            continue;
          default:
            continue;
        }
      }
    }
    else
    {
      IDictionary dictionary = (IDictionary) new HybridDictionary();
      WriteReadXmlHelper.ReadStringDictionaryFromXml(dictionary, readArgs);
      if (dictionary.Count <= 0)
        return;
      if (this.additionalAttributes == null)
        this.AdditionalAttributes = new AdditionalAttributeCollection(this);
      this.additionalAttributes.Attributes = dictionary;
    }
  }

  /// <summary>Загрузить узел из XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  public virtual void ReadFromXml(XmlReadArgs readArgs)
  {
    if (readArgs.Version < 10)
      this.ReadFromXmlOldFormats_Before(readArgs);
    WriteReadXmlHelper.ReadFromXml((IWriteReadXml) this, readArgs);
    if (readArgs.Version < 17)
      this.ReadFromXmlOldFormats_After(readArgs);
    this.SetFlags((byte) 1, false);
    this.SetFlags((byte) 2, false);
  }

  /// <summary>Загрузить узел из XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  public virtual void ReadFromXmlOldFormats_Before(XmlReadArgs readArgs)
  {
  }

  /// <summary>Загрузить узел из XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  public virtual void ReadFromXmlOldFormats_After(XmlReadArgs readArgs)
  {
  }

  /// <summary>Добавить ссылку на объект, который нужно восстановить после загрузки</summary>
  /// <param name="obj">Объект, владеющий ссылкой</param>
  /// <param name="objectReferences">Список ссылок этого объекта, в который добавляется ссылка</param>
  /// <param name="fieldName">Имя поля объекта, которое ссылается на другой объект</param>
  /// <param name="refId">Идентификатор другого объекта</param>
  public static void AddObjectReference(
    object obj,
    IDictionary objectReferences,
    string fieldName,
    string refId)
  {
    NameValueCollection nameValueCollection;
    if (!objectReferences.Contains(obj))
    {
      nameValueCollection = new NameValueCollection();
      objectReferences.Add(obj, (object) nameValueCollection);
    }
    else if (objectReferences[obj] != null)
    {
      nameValueCollection = (NameValueCollection) objectReferences[obj];
    }
    else
    {
      nameValueCollection = new NameValueCollection();
      objectReferences[obj] = (object) nameValueCollection;
    }
    nameValueCollection.Add(fieldName, refId);
  }

  /// <summary>Восстановить ссылки на объекты в свойствах узлов</summary>
  /// <param name="objectsId">Список идентификаторов объектов</param>
  /// <param name="objectReferences">Список ссылок на объекты</param>
  public void RestoreObjectReferences(
    IDictionary objectsId,
    IDictionary objectReferences,
    bool skipIfNotFound,
    bool removeRestored)
  {
    ICollection keys = objectReferences.Keys;
    object[] objArray = new object[keys.Count];
    keys.CopyTo((Array) objArray, 0);
    foreach (object key1 in objArray)
    {
      NameValueCollection objectReference = (NameValueCollection) objectReferences[key1];
      foreach (string allKey in objectReference.AllKeys)
      {
        bool flag = false;
        string[] values = objectReference.GetValues(allKey);
        object obj1 = (object) null;
        if (key1 is IList list && allKey == "item")
        {
          if (values != null)
          {
            foreach (string key2 in values)
            {
              if (!objectsId.Contains((object) key2))
              {
                if (!skipIfNotFound)
                  LogManager.AddLine(LocalizationHolder.rm.GetString("Interfaces.Document_12") + key2);
                flag = true;
              }
              else
              {
                object obj2 = objectsId[(object) key2];
                list.Add(obj2);
              }
            }
            if (removeRestored && !flag)
              objectReference.Remove(allKey);
          }
        }
        else
        {
          if (values != null && values.Length > 1)
            throw new Exception(string.Format(LocalizationHolder.rm.GetString("Interfaces.Document_13"), (object) allKey, (object) key1.GetType().Name));
          if (values != null && values[0] != "")
          {
            if (!objectsId.Contains((object) values[0]))
            {
              if (!skipIfNotFound)
                throw new Exception(LocalizationHolder.rm.GetString("Interfaces.Document_14") + values[0]);
              flag = true;
            }
            if (!flag)
              obj1 = objectsId[(object) values[0]];
          }
          if (!flag)
          {
            FieldInfo field = FindFieldHelper.FindField(key1.GetType(), allKey);
            if (field == (FieldInfo) null)
              throw new Exception(string.Format(LocalizationHolder.rm.GetString("Interfaces.Document_15"), (object) allKey, (object) key1.GetType().FullName));
            if (allKey == "template")
              this.Template = obj1 as DocumentTreeNode;
            else
              field.SetValue(key1, obj1);
            if (removeRestored)
              objectReference.Remove(allKey);
          }
        }
      }
      if (removeRestored && objectReference.Count == 0)
        objectReferences.Remove(key1);
    }
  }

  protected virtual void SetPropertyValue(object value, [CallerMemberName] string propName = "")
  {
    if (this.AdditionalAttributes == null)
      this.AdditionalAttributes = new AdditionalAttributeCollection(this);
    AddAttrValue addAttrValue1;
    if (!this.additionalAttributes.ContainsAttribute(propName) && DocumentTreeNode.propertyBindings.TryGetValue(propName, out addAttrValue1))
    {
      AddAttrValue attributeValue = addAttrValue1.Clone();
      attributeValue.Value = value is AddAttrValue addAttrValue2 ? addAttrValue2.Value : value;
      this.additionalAttributes.SetAttributeValue(propName, (object) attributeValue);
    }
    else
      this.additionalAttributes.SetAttributeValue(propName, value);
  }

  protected virtual object GetPropertyValue([CallerMemberName] string propName = "")
  {
    if (this.additionalAttributes != null && this.additionalAttributes.ContainsAttribute(propName))
      return this.additionalAttributes.GetAttributeValue(propName);
    AddAttrValue addAttrValue;
    return DocumentTreeNode.propertyBindings.TryGetValue(propName, out addAttrValue) ? addAttrValue.Clone().Value : (object) null;
  }

  /// <summary>Привязка свойства к системе доп. атрибутов</summary>
  protected static void BindPropertyToAdditionalAttribute(
    string propertyName,
    Type propertyType,
    object defaultValue = null,
    TypeConverter converter = null,
    bool showInPropGrid = false)
  {
    if (string.IsNullOrWhiteSpace(propertyName))
      throw new ArgumentEmptyStringNotAllowedException(nameof (propertyName), "Имя свойства не задано.");
    if (DocumentTreeNode.propertyBindings.ContainsKey(propertyName))
      throw new ArgumentItemValidationExceptionException("AdditionalAttributes", $"Атрибут с именем '{propertyName}' уже присутствует в коллекции.");
    AddAttrValue addAttrValue = new AddAttrValue(defaultValue, propertyType, converter, showInPropGrid);
    DocumentTreeNode.propertyBindings.Add(propertyName, addAttrValue);
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

  /// <summary>Удалить свойство из списка</summary>
  /// <param name="properties">Список свойств</param>
  /// <param name="propertyName">Имя свойства</param>
  protected void RemoveProperty(IDictionary properties, string propertyName)
  {
    properties.Remove((object) propertyName);
  }

  /// <summary>Отфильтровать словарь по словарю допустимых ключей.
  /// Если допустимые ключи не заданы, то очистить словарь</summary>
  /// <param name="dict">Фильтруемый словарь</param>
  /// <param name="include">Допустимые ключи</param>
  public static void FilterDictionary(IDictionary dict, IDictionary include)
  {
    if (dict == null)
      throw new ArgumentNullException(nameof (dict));
    if (include != null && include.Count > 0)
    {
      foreach (DictionaryEntry dictionaryEntry in dict)
      {
        if (!include.Contains(dictionaryEntry.Key))
          dict.Remove(dictionaryEntry.Key);
      }
    }
    else
      dict.Clear();
  }

  /// <summary>Отфильтровать свойства элемента для показа в PopertyGrid</summary>
  /// <param name="properties">Список PropertyDescriptor свойств</param>
  /// <param name="attributes">Массив атрибутов элемента</param>
  protected virtual void FilterProperties(IDictionary properties, Attribute[] attributes)
  {
    if (ImDocumentData.ShowDebugInfo)
      return;
    if (this.IsTemplate)
      this.RemoveProperty(properties, "Template");
    else
      this.RemoveProperty(properties, "CloneByTemplateWithParent");
    this.RemoveProperty(properties, "TemplateId");
    this.RemoveProperty(properties, "NodesCount");
    this.RemoveProperty(properties, "SuspendedUpdateLayoutFlag");
    this.RemoveProperty(properties, "NeedUpdateLayoutFlag");
    this.RemoveProperty(properties, "SuspendedApplyThisTemplateFlag");
    this.RemoveProperty(properties, "ClonedByTemplateWithParent");
    this.RemoveProperty(properties, "TypeNameForXml");
  }

  /// <summary>Получить словарь атрибутов свойств, которые нужно перекрыть</summary>
  /// <returns>Атрибуты свойств</returns>
  protected virtual IDictionary GetPropertyAttributes()
  {
    return DocumentTreeNode.OverridePropertyAttributes;
  }

  /// <summary>Получить дескрипторы для свойств</summary>
  /// <param name="attributes">Атрибуты свойств</param>
  /// <returns>Коллекция дескрипторов свойств</returns>
  protected virtual PropertyDescriptorCollection GetProperties(Attribute[] attributes)
  {
    PropertyDescriptorCollection properties1 = TypeDescriptor.GetProperties((object) this, attributes, true);
    HybridDictionary properties2 = new HybridDictionary(200);
    IDictionary propertyAttributes = this.GetPropertyAttributes();
    foreach (PropertyDescriptor PropDesc in properties1)
    {
      if (!(PropDesc is CustomPropertyDescriptor propertyDescriptor))
      {
        propertyDescriptor = new CustomPropertyDescriptor(PropDesc);
        if (propertyAttributes != null && propertyAttributes[(object) propertyDescriptor.Name] != null && propertyAttributes[(object) propertyDescriptor.Name] is PropertyAttributeWrapper attributeWrapper)
        {
          for (int index = 0; index < attributeWrapper.AttributesForTypes.Count; ++index)
          {
            if (attributeWrapper[index].PropertyOwnerType.IsAssignableFrom(this.GetType()))
              propertyDescriptor.AddAttribute(attributeWrapper[index].Attribute);
          }
        }
      }
      properties2.Add((object) propertyDescriptor.Name, (object) propertyDescriptor);
    }
    CustomPropertyDescriptor propertyDescriptor1 = new CustomPropertyDescriptor((PropertyDescriptor) new AdditionalAttributesDescriptor(new Attribute[4]
    {
      (Attribute) new RefreshPropertiesAttribute(RefreshProperties.All),
      (Attribute) new DisplayNameAttribute(LocalizationHolder.rm.GetString("Interfaces.Document_136")),
      (Attribute) new DescriptionAttribute(LocalizationHolder.rm.GetString("Interfaces.Document_137")),
      (Attribute) new CategoryAttribute(LocalizationHolder.rm.GetString("Interfaces.Document_138"))
    }));
    if (propertyAttributes != null && propertyAttributes[(object) propertyDescriptor1.Name] != null && propertyAttributes[(object) propertyDescriptor1.Name] is PropertyAttributeWrapper attributeWrapper1)
    {
      for (int index = 0; index < attributeWrapper1.AttributesForTypes.Count; ++index)
      {
        if (attributeWrapper1[index].PropertyOwnerType.IsAssignableFrom(this.GetType()))
          propertyDescriptor1.AddAttribute(attributeWrapper1[index].Attribute);
      }
    }
    properties2.Add((object) propertyDescriptor1.Name, (object) propertyDescriptor1);
    foreach (PropertyDescriptor property in AdditionalPropertiesManager.Instance.GetProperties(this.OwnerDocument, this))
    {
      if (!(property is CustomPropertyDescriptor propertyDescriptor2))
        propertyDescriptor2 = new CustomPropertyDescriptor(property);
      properties2.Add((object) propertyDescriptor2.Name, (object) propertyDescriptor2);
    }
    this.FilterProperties((IDictionary) properties2, attributes);
    this.globalizedProps = new PropertyDescriptorCollection((PropertyDescriptor[]) null);
    foreach (DictionaryEntry dictionaryEntry in properties2)
      this.globalizedProps.Add((PropertyDescriptor) dictionaryEntry.Value);
    return this.globalizedProps;
  }

  object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd) => (object) this;

  /// <summary>Создать копию узла</summary>
  /// <returns>Копия узла</returns>
  public DocumentTreeNode Clone() => this.Clone(true, true);

  /// <summary>Внутренний метод копирования</summary>
  /// <param name="copyChildren">Копировать дочерние узлы</param>
  /// <param name="copyData">Копировать дочерние узлы</param>
  /// <param name="copyDataNodes">Копировать узлы являющиеся ячейками данных для таблиц</param>
  /// <param name="templateClone">Копия по шаблону</param>
  /// <param name="links">Ссылки (указатели) на данные</param>
  /// <returns>Копия узла</returns>
  public virtual DocumentTreeNode InternalClone(
    bool copyChildren,
    bool copyData,
    bool copyDataNodes,
    bool templateClone,
    IDictionary links)
  {
    DocumentTreeNode element = (DocumentTreeNode) null;
    this.CreateEmptyElement(ref element);
    element.CopyFields(this, copyChildren, copyData, copyDataNodes, templateClone, true, links);
    return element;
  }

  /// <summary>Создать копию узла</summary>
  /// <param name="copyChildren">Копировать дочерние узлы</param>
  /// <param name="copyData">Копировать дочерние узлы</param>
  /// <returns>Копия узла</returns>
  public virtual DocumentTreeNode Clone(bool copyChildren, bool copyData)
  {
    IDictionary links = (IDictionary) new HybridDictionary();
    DocumentTreeNode documentTreeNode = this.InternalClone(copyChildren, copyData, true, false, links);
    documentTreeNode.OnDeserialization((object) this);
    this.RestoreLinks(copyChildren, false, true, links);
    documentTreeNode.clonedByTemplateWithParent = false;
    return documentTreeNode;
  }

  object ICloneable.Clone() => (object) this.Clone();

  /// <summary>Копировать поля из src</summary>
  /// <param name="src">Источник</param>
  /// <param name="copyChildren">Копировать дочерние узлы</param>
  /// <param name="copyData">Копировать данные</param>
  /// <param name="copyDataNodes">Копировать узлы являющиеся ячейками данных для таблиц</param>
  /// <param name="templateClone">Копирование по шаблону</param>
  /// <param name="externalLink">Копировать внешние ссылки</param>
  /// <param name="links">Словарь скопированных ссылок.
  /// В этом методе в словарь вставляются /links.Add(src, clone);/ связки оригинал-копия.
  /// В методе RestoreLinks - восстанавливаются ссылки на копируемые объекты</param>
  protected virtual void CopyFields(
    DocumentTreeNode src,
    bool copyChildren,
    bool copyData,
    bool copyDataNodes,
    bool templateClone,
    bool externalLink,
    IDictionary links)
  {
    links.Add((object) src, (object) this);
    this.isVirtualNode = src.isVirtualNode;
    this.id = src.id;
    AdditionalAttributeCollection attrCollection = src.additionalAttributes ?? new AdditionalAttributeCollection(src);
    StringCollection virtualAttributes = this.GetPluginVirtualAttributes(attrCollection);
    if (attrCollection.Count > 0 || src.additionalAttributes != null)
    {
      this.AdditionalAttributes = (AdditionalAttributeCollection) attrCollection.Clone();
      if (virtualAttributes.Count > 0 && src.additionalAttributes != null)
      {
        for (int index = 0; index < virtualAttributes.Count; ++index)
          src.additionalAttributes.RemoveAttribute(virtualAttributes[index]);
      }
    }
    else
      this.AdditionalAttributes = (AdditionalAttributeCollection) null;
    if (this.referenceToTemplate != null)
    {
      this.referenceToTemplate.DisconnectLink();
      this.referenceToTemplate = (ReferenceToTemplate) null;
    }
    if (src.unknownXmlAttributes != null)
    {
      this.unknownXmlAttributes = new List<StringKeyValue>(src.unknownXmlAttributes.Count);
      for (int index = 0; index < src.unknownXmlAttributes.Count; ++index)
        this.unknownXmlAttributes.Add(src.unknownXmlAttributes[index].Clone());
    }
    else
      this.unknownXmlAttributes = (List<StringKeyValue>) null;
    this.unknownXmlElements = src.unknownXmlElements;
    this.needUpdateLayoutFlag |= src.needUpdateLayoutFlag;
    if (templateClone)
    {
      this.referenceToTemplate = new ReferenceToTemplate(this);
      this.referenceToTemplate.SetReference(src);
      this.overrideFlags = OverrideFlags.None;
      this.overrideFlags2 = src.overrideFlags2 & ~(OverrideFlags2.NextPageTemplateId | OverrideFlags2.LastPageTemplateId | OverrideFlags2.Name | OverrideFlags2.Reference);
      this.overrideFlags3 = OverrideFlags3.None;
      this.name = (string) null;
    }
    else
    {
      this.name = src.Name;
      this.overrideFlags = src.overrideFlags;
      this.overrideFlags2 = src.overrideFlags2;
      this.overrideFlags3 = src.overrideFlags3;
      if (src.referenceToTemplate != null)
      {
        this.referenceToTemplate = (ReferenceToTemplate) src.referenceToTemplate.Clone();
        this.referenceToTemplate.AssignOwnerNode(this);
      }
    }
    this.cloneByTemplateWithParent = src.cloneByTemplateWithParent;
    this.clonedByTemplateWithParent = src.clonedByTemplateWithParent;
    if (src.Nodes == null)
      return;
    if (copyChildren)
    {
      this.nodes = new DocumentTreeNodeCollection(this, src.Nodes.Count);
      int index = 0;
      for (int count = src.Nodes.Count; index < count; ++index)
      {
        DocumentTreeNode node = src.Nodes[index];
        if ((!templateClone || node.CloneByTemplateWithParent) && (copyDataNodes || !node.IsDataNode))
        {
          if (!this.isVirtualNode)
          {
            DocumentTreeNode documentTreeNode = node.InternalClone(true, copyData, copyDataNodes, templateClone, links);
            if (templateClone)
              documentTreeNode.clonedByTemplateWithParent = true;
            this.nodes.AddInternal(documentTreeNode);
            documentTreeNode.AssignParent(this, false, false, true);
            documentTreeNode.IdService = this.IdService;
            if (!this.needUpdateLayoutFlag && documentTreeNode.needUpdateLayoutFlag && this is TableData)
              this.AssignNeedUpdateLayoutFlag(true);
          }
          else
            this.nodes.AddInternal(node);
        }
        else if (templateClone && this is TableData)
          this.AssignNeedUpdateLayoutFlag(true);
      }
    }
    else
      this.nodes = new DocumentTreeNodeCollection(this, 0);
  }

  /// <summary>Только для внутреннего использования. Синхронизировать объекты</summary>
  /// <param name="src">Источник</param>
  public virtual void AssignProperties(DocumentTreeNode src)
  {
    if (src.nodes == null)
    {
      if (this.nodes != null)
      {
        this.Clear(false, false);
        this.Nodes = (DocumentTreeNodeCollection) null;
      }
    }
    else if (this.nodes == null)
      this.nodes = new DocumentTreeNodeCollection(this, src.nodes.Count);
    for (int index = 0; index < this.nodes.Count; ++index)
    {
      if (index < src.nodes.Count)
      {
        if (this.nodes[index].GetType().IsAssignableFrom(src.nodes[index].GetType()))
          this.nodes[index].AssignProperties(src.nodes[index]);
        else
          this.InsertChildNode(index, src.nodes[index].Clone(true, true), false, true, false, false);
      }
      else
        this.RemoveChildNodeAt(index, false, false, false);
    }
    IDictionary links = (IDictionary) new HybridDictionary();
    this.CopyFields(src, false, true, true, false, false, links);
  }

  /// <summary>Восстановить сохраненные ссылки</summary>
  /// <param name="copyChildren">Копировать дочерние узлы</param>
  /// <param name="templateClone">Копирование по шаблону</param>
  /// <param name="externalLink">Копировать внешние ссылки</param>
  /// <param name="links">Словарь скопированных ссылок</param>
  public virtual void RestoreLinks(
    bool copyChildren,
    bool templateClone,
    bool externalLink,
    IDictionary links)
  {
    if (!copyChildren || this.nodes == null)
      return;
    for (int index = 0; index < this.nodes.Count; ++index)
    {
      if (!templateClone || this.nodes[index].CloneByTemplateWithParent)
        this.nodes[index].RestoreLinks(copyChildren, templateClone, externalLink, links);
    }
  }

  /// <summary>Рекурсивно вызвать OnDeserialization</summary>
  public void CallOnDeserializationRecursive()
  {
    this.OnDeserialization((object) null);
    if (this.nodes == null)
      return;
    for (int index = 0; index < this.nodes.Count; ++index)
      this.nodes[index].CallOnDeserializationRecursive();
  }

  /// <summary>Метод вызываемый при десериализации.
  /// Реализация IDeserializationCallback</summary>
  public virtual void OnDeserialization(object sender)
  {
    if (this.additionalAttributes != null)
    {
      AdditionalAttributeCollection additionalAttributes = this.AdditionalAttributes;
      this.AdditionalAttributes = (AdditionalAttributeCollection) null;
      this.AdditionalAttributes = additionalAttributes;
    }
    if (this.nodes != null)
      this.nodes.SetOwner(this, false, false, true);
    if (this.referenceToTemplate == null)
      return;
    this.referenceToTemplate.AssignOwnerNode(this);
  }

  public virtual void ClearExternalLinks(IEnumerable<DocumentTreeNode> parents)
  {
  }

  /// <summary>Вспомогательный метод. Получить атрибут заданного класса у MemberInfo</summary>
  /// <param name="mi">Информация члена</param>
  /// <param name="type">Тип атрибута</param>
  /// <returns>Экземпляр атрибута или null, если атрибут не найден</returns>
  private static object GetAttribute(MemberInfo mi, Type type)
  {
    object[] customAttributes = mi.GetCustomAttributes(type, true);
    return customAttributes.Length != 0 ? customAttributes[0] : (object) null;
  }

  /// <summary>Поле является внешней ссылкой</summary>
  /// <param name="mi">Информация о поле</param>
  /// <returns>Возвращает true, если поле имеет атрибут ExternalLinkAttribute
  /// с соответствующим значением свойства IsExternal</returns>
  protected virtual bool IsExternalLinkField(MemberInfo mi, IEnumerable<DocumentTreeNode> rootNodes)
  {
    object[] customAttributes = mi.GetCustomAttributes(typeof (ExternalLinkAttribute), true);
    return customAttributes.Length != 0 && ((ExternalLinkAttribute) customAttributes[0]).IsExternal;
  }

  /// <summary>Поле является ссылкой на дочерние узлы</summary>
  /// <param name="mi">Информация о поле</param>
  /// <returns>Возвращает true, если поле имеет атрибут ChildLinkAttribute
  /// с соответствующим значением свойства IsChildLink</returns>
  private static bool IsChildLinkField(MemberInfo mi)
  {
    object[] customAttributes = mi.GetCustomAttributes(typeof (ChildLinkAttribute), true);
    return customAttributes.Length != 0 && ((ChildLinkAttribute) customAttributes[0]).IsChildLink;
  }

  /// <summary>Полное имя поля, включая имя класса, в котором оно декларировано</summary>
  /// <param name="field">Информация о поле</param>
  /// <returns>Полное имя поля</returns>
  protected static string FullFieldName(FieldInfo field)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(field.DeclaringType.FullName);
    stringBuilder.Append(".");
    stringBuilder.Append(field.Name);
    return stringBuilder.ToString();
  }

  /// <summary>Установить значение поля</summary>
  /// <param name="fieldName">Имя поля</param>
  /// <param name="value">Значение поля</param>
  /// <returns>true, если поле найдено</returns>
  protected virtual bool SetFieldValue(string fieldName, object value)
  {
    bool flag = false;
    switch (fieldName)
    {
      case "Intermech.Document.Model.DocumentTreeNode.id":
        this.id = (string) value;
        break;
      case "Intermech.Document.Model.DocumentTreeNode.additionalAttributes":
        this.additionalAttributes = (AdditionalAttributeCollection) value;
        break;
      case "Intermech.Document.Model.DocumentTreeNode.referenceToTemplate":
        this.referenceToTemplate = (ReferenceToTemplate) value;
        break;
      case "Intermech.Document.Model.DocumentTreeNode.cloneByTemplateWithParent":
        this.cloneByTemplateWithParent = (bool) value;
        break;
      case "Intermech.Document.Model.DocumentTreeNode.clonedByTemplateWithParent":
        this.clonedByTemplateWithParent = (bool) value;
        break;
      default:
        flag = true;
        break;
    }
    return flag;
  }

  /// <summary>Конструктор необходимый для десериализации (ISerializable).
  /// В каждом классе должна быть заглушка обращающаяся к базовому конструктору.
  /// Например:
  /// protected Page(SerializationInfo info, StreamingContext context): base(info, context) {}</summary>
  /// <param name="info">Заполненный данными SerializationInfo</param>
  /// <param name="context">Контекст десериализации</param>
  protected DocumentTreeNode(SerializationInfo info, StreamingContext context)
  {
    this.InitFields();
    FieldInfo[] serializableFields = FindFieldHelper.FindSerializableFields(this.GetType());
    SerializationInfoEnumerator enumerator = info.GetEnumerator();
    int num = 0;
    while (enumerator.MoveNext())
    {
      bool flag = this.SetFieldValue(enumerator.Name, enumerator.Value);
      if (flag)
      {
        for (int index = num; index < serializableFields.Length; ++index)
        {
          if (DocumentTreeNode.FullFieldName(serializableFields[index]) == enumerator.Name)
          {
            serializableFields[index].SetValue((object) this, enumerator.Value);
            flag = false;
            if (num == index)
            {
              num = index + 1;
              break;
            }
            break;
          }
        }
      }
      if (flag)
        throw new Exception(string.Format(LocalizationHolder.rm.GetString("Interfaces.Document_17"), (object) enumerator.Name));
    }
  }

  /// <summary>Получить данные объекта, которые нужно сериализовать.
  /// Реализация интерфейса ISerializable</summary>
  /// <param name="info">Заполненный данными SerializationInfo</param>
  /// <param name="context">Контекст десериализации</param>
  public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    bool flag1 = context.Context is AdditionalContext context1 && context1.RootNodes != null;
    bool flag2 = context1 != null && (context1.Flags & ContextFlags.WithoutChilds) > ContextFlags.None;
    FieldInfo[] serializableFields = FindFieldHelper.FindSerializableFields(this.GetType());
    for (int index = 0; index < serializableFields.Length; ++index)
    {
      string str = DocumentTreeNode.FullFieldName(serializableFields[index]);
      if (!flag2 || !DocumentTreeNode.IsChildLinkField((MemberInfo) serializableFields[index]))
      {
        if (flag1 && this.IsExternalLinkField((MemberInfo) serializableFields[index], (IEnumerable<DocumentTreeNode>) context1.RootNodes))
        {
          info.AddValue(DocumentTreeNode.FullFieldName(serializableFields[index]), (object) null);
        }
        else
        {
          object obj;
          if (this.GetFieldValue(str, out obj))
            info.AddValue(str, obj);
          else
            info.AddValue(str, serializableFields[index].GetValue((object) this));
        }
      }
    }
  }

  protected virtual bool GetFieldValue(string fieldName, out object value)
  {
    value = (object) null;
    return false;
  }

  /// <summary>Проверить можно ли вставить объект из буфера в этот узел</summary>
  /// <param name="nodeClipboardInfo">Информация об узле в буфере</param>
  /// <returns>Возвращает true, если объект из буфера можно ли вставить в этот узел</returns>
  public virtual bool CanPasteFromClipboard(NodeClipboardInfo nodeClipboardInfo)
  {
    return this.CanAddChildElement(nodeClipboardInfo.NodeType);
  }

  public virtual void Dispose()
  {
    this.referenceToTemplate = (ReferenceToTemplate) null;
    DocumentTreeNodeCollection nodes = this.nodes;
    if (nodes != null)
    {
      lock (nodes)
      {
        for (int index = 0; index < this.NodesCount; ++index)
        {
          DocumentTreeNode documentTreeNode = nodes[index];
          if (documentTreeNode != null)
          {
            documentTreeNode.Dispose();
            documentTreeNode.parent = (DocumentTreeNode) null;
          }
        }
        nodes.ClearInternal();
        this.Nodes = (DocumentTreeNodeCollection) null;
      }
    }
    if (this.IdService != null && this.IdService is IDisposable idService)
      idService.Dispose();
    this.idService = (IUniqueIdService) null;
  }

  public DocumentTreeNode DebugFindNodeWithInvalidTemplate()
  {
    if (!this.DebugValidateTemplate())
      return this;
    if (this.NodesCount > 0)
    {
      foreach (DocumentTreeNode node in this.Nodes)
      {
        DocumentTreeNode withInvalidTemplate = node.DebugFindNodeWithInvalidTemplate();
        if (withInvalidTemplate != null)
          return withInvalidTemplate;
      }
    }
    return (DocumentTreeNode) null;
  }

  public bool DebugValidateTemplate()
  {
    return this.IsTemplate || this.OwnerDocument == null || this.Template == null || this.OwnerDocument.Template == this.Template.OwnerDocument;
  }
}
