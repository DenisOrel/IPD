// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.ReferenceToNodeAttributeBase
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Базовый класс ссылка на узел и его атрибут</summary>
[Serializable]
public class ReferenceToNodeAttributeBase : 
  ReferenceToNodeId,
  ITextSourceWithCallChain,
  ITextSource,
  IEditableReferenceToTextSource,
  IEditableReferenceToObject
{
  [NonSerialized]
  private TextChanged_EventHandler textChanged;
  /// <summary>Тип базы ссылки</summary>
  protected BaseReferenceNodeType referenceBaseType;
  private string attributeName;

  /// <summary>Конструктор</summary>
  public ReferenceToNodeAttributeBase()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="ownerNode">Узел владелец</param>
  public ReferenceToNodeAttributeBase(DocumentTreeNode ownerNode)
    : base(ownerNode)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="ownerNode">Узел владелец</param>
  /// <param name="nodeId">Идентификатор узла на который ссылается</param>
  /// <param name="attributeName">Имя атрибута</param>
  public ReferenceToNodeAttributeBase(
    DocumentTreeNode ownerNode,
    string nodeId,
    string attributeName)
    : base(ownerNode)
  {
    this.attributeName = attributeName;
    this.AssignNodeId(nodeId);
  }

  /// <summary>Конструктор</summary>
  /// <param name="ownerNode">Владелец ссылки</param>
  /// <param name="referenceBaseType">Тип базового узла ссылки</param>
  /// <param name="nodeId">Идентификатор узла на который ссылается</param>
  /// <param name="attributeName">Имя атрибута на котроый ссылается</param>
  public ReferenceToNodeAttributeBase(
    DocumentTreeNode ownerNode,
    BaseReferenceNodeType referenceBaseType,
    string nodeId,
    string attributeName)
    : this(ownerNode, nodeId, attributeName)
  {
    this.referenceBaseType = referenceBaseType;
  }

  /// <summary>Имя атрибута</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_409")]
  [CustomDescription("Attribute.Interfaces.Document_410")]
  [System.ComponentModel.ReadOnly(true)]
  public virtual string AttributeName
  {
    [DebuggerStepThrough] get => this.attributeName;
    set => this.attributeName = value;
  }

  /// <summary>Назначить значение свойству AttributeName</summary>
  /// <param name="value">Значение</param>
  public virtual void AssignAttributeName(string value)
  {
    if (value == "")
      this.attributeName = (string) null;
    else
      this.attributeName = value;
  }

  /// <summary>Пустая ссылка</summary>
  public override bool IsEmpty
  {
    get
    {
      switch (this.ReferenceBaseType)
      {
        case BaseReferenceNodeType.ntThisNode:
          return this.attributeName != null;
        case BaseReferenceNodeType.ntParentNode:
          return this.attributeName != null;
        case BaseReferenceNodeType.ntParentPage:
          return this.attributeName != null;
        case BaseReferenceNodeType.ntParentDocument:
          return this.attributeName != null;
        case BaseReferenceNodeType.ntSelectedNode:
          return this.NodeId != null && this.attributeName != null;
        case BaseReferenceNodeType.ntUseParentLink:
          return this.attributeName != null;
        case BaseReferenceNodeType.ntUseParentDocumentLink:
          return this.attributeName != null;
        default:
          return true;
      }
    }
  }

  /// <summary>Тип элемент, атрибут которого отображает элемент</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_581")]
  [CustomDescription("Attribute.Interfaces.Document_582")]
  public virtual BaseReferenceNodeType ReferenceBaseType
  {
    [DebuggerStepThrough] get => this.referenceBaseType;
  }

  /// <summary>Ссылка зависит от документа. Т.е. при смене документа ее нужно обновлять.</summary>
  public override bool IsDependOnDocument
  {
    get
    {
      return this.referenceBaseType == BaseReferenceNodeType.ntParentDocument || this.referenceBaseType == BaseReferenceNodeType.ntUseParentDocumentLink || this.referenceBaseType == BaseReferenceNodeType.ntSelectedNode;
    }
  }

  /// <summary>Ссылка зависит от страницы. Т.е. при смене страницы ее нужно обновлять.</summary>
  public override bool IsDependOnPage
  {
    get => this.referenceBaseType == BaseReferenceNodeType.ntParentPage;
  }

  /// <summary>Ссылка зависит от родительского узла. Т.е. при смене родителя ее нужно обновлять.</summary>
  public override bool IsDependOnParent
  {
    get
    {
      return this.referenceBaseType == BaseReferenceNodeType.ntParentNode || this.referenceBaseType == BaseReferenceNodeType.ntUseParentLink;
    }
  }

  /// <summary>База для ссылки на узел (для внутреннего использования)</summary>
  public override DocumentTreeNode NodeLinkBase => this.OwnerNode;

  /// <summary>Можно редактировать по месту. Для ссылок на атрибуты</summary>
  public override bool CanInplaceEdit => true;

  /// <summary>Получить значение атрибута</summary>
  /// <returns>Значение атрибута</returns>
  public string GetAttributeValue()
  {
    return this.NodeLink != null && this.AttributeName != null ? this.NodeLink.GetAttributeValue(this.AttributeName, false) : (string) null;
  }

  /// <summary>Получить строковое значение атрибута</summary>
  /// <param name="callChain">Цепочка вызовов для защиты от циклических связей. Если null, то работает без проверок</param>
  /// <returns>Строковое значение атрибута</returns>
  public virtual string GetAttributeStringValue(List<DocumentTreeNode> callChain)
  {
    if (!this.CanShowReference())
      return "";
    string attributeStringValue = "";
    if (this.NodeLink == null)
      this.UpdateLink(false, false);
    if (this.NodeLink != null && this.AttributeName != null)
      attributeStringValue = this.NodeLink.GetAttributeValue(this.AttributeName, true, callChain);
    return attributeStringValue;
  }

  /// <summary>Обработка события изменения значения атрибута</summary>
  public virtual void OnAttributeValueChanged()
  {
  }

  /// <summary>Установить значение атрибута</summary>
  /// <param name="value">Значение атрибута</param>
  /// <param name="saveUndo">Сохранять данные для Undo</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public void SetAttributeValue(string value, bool saveUndo, bool updateUI, bool updateLayout)
  {
    this.SetAttributeValue(value, saveUndo, updateUI, updateLayout, (List<DocumentTreeNode>) null);
  }

  /// <summary>Установить значение атрибута</summary>
  /// <param name="value">Значение атрибута</param>
  /// <param name="saveUndo">Сохранять данные для Undo</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  /// <param name="callChain">Цепочка вызовов, для защиты от зацикливания</param>
  internal void SetAttributeValue(
    string value,
    bool saveUndo,
    bool updateUI,
    bool updateLayout,
    List<DocumentTreeNode> callChain)
  {
    if (this.NodeLink == null || this.AttributeName == null)
      return;
    this.NodeLink.SetAttributeValue(this.AttributeName, value, saveUndo, updateUI, updateLayout, callChain);
  }

  /// <summary>Найти узел по ссылке</summary>
  /// <returns>Узел, на который ссылается</returns>
  public virtual DocumentTreeNode FindLinkedNode()
  {
    DocumentTreeNode linkedNode = (DocumentTreeNode) null;
    if (this.NodeLinkBase != null)
    {
      switch (this.ReferenceBaseType)
      {
        case BaseReferenceNodeType.ntThisNode:
          linkedNode = this.NodeLinkBase;
          break;
        case BaseReferenceNodeType.ntParentNode:
          linkedNode = this.NodeLinkBase.Parent;
          break;
        case BaseReferenceNodeType.ntParentPage:
          if (this.NodeLinkBase is PageElementNode nodeLinkBase1)
          {
            linkedNode = (DocumentTreeNode) nodeLinkBase1.Page;
            break;
          }
          break;
        case BaseReferenceNodeType.ntParentDocument:
          if (this.NodeLinkBase is IDocumentElement nodeLinkBase2)
          {
            linkedNode = (DocumentTreeNode) nodeLinkBase2.OwnerDocument;
            break;
          }
          break;
        case BaseReferenceNodeType.ntSelectedNode:
          if ((this.NodeId == null || this.NodeId == "") && this.OwnerNode != null)
          {
            if (this.OwnerNode.Template is INodeWithReference template && template.Reference is ReferenceToNodeAttributeBase reference && reference.ReferenceBaseType == BaseReferenceNodeType.ntSelectedNode && reference.NodeId != null && reference.NodeId != "")
            {
              if (reference.NodeLink == null)
                reference.UpdateLink(false, false);
              if (reference.NodeLink != null)
              {
                linkedNode = reference.NodeLink == reference.OwnerNode ? this.OwnerNode : this.OwnerNode.FindNearestNodeFromTemplate(reference.NodeLink);
                break;
              }
              break;
            }
            break;
          }
          linkedNode = this.NodeLinkBase.FindNode(this.NodeId);
          break;
        case BaseReferenceNodeType.ntUseParentLink:
          if (this.NodeLinkBase.Parent is INodeWithReference parent && parent.Reference is ReferenceToNode reference1)
          {
            if (reference1.NodeLink == null)
              reference1.UpdateLink(false, false, false);
            linkedNode = reference1.NodeLink;
            break;
          }
          break;
        case BaseReferenceNodeType.ntUseParentDocumentLink:
          IDocumentElement nodeLinkBase3 = this.NodeLinkBase as IDocumentElement;
          INodeWithReference nodeWithReference = (INodeWithReference) null;
          if (nodeLinkBase3 != null)
            nodeWithReference = (INodeWithReference) nodeLinkBase3.OwnerDocument;
          if (nodeWithReference != null && nodeWithReference.Reference is ReferenceToNode reference2)
          {
            if (reference2.NodeLink == null)
              reference2.UpdateLink(false, false, false);
            linkedNode = reference2.NodeLink;
            break;
          }
          break;
      }
    }
    return linkedNode;
  }

  /// <summary>Обновить связь</summary>
  /// <param name="forceUpdate">Обновлять даже для пассивных ссылок</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public override void UpdateLink(bool forceUpdate, bool updateUI, bool updateLayout)
  {
    if (!(this.nodeLink == null | forceUpdate))
      return;
    this.AssignNodeLink(this.FindLinkedNode());
  }

  /// <summary>Сохранить данные в атрибуты XML</summary>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  protected override void WriteXmlAttributes(XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    string nodeId = this.NodeId;
    this.AssignNodeId((string) null);
    base.WriteXmlAttributes(xw, objectRefId);
    this.AssignNodeId(nodeId);
    xw.WriteAttributeString("referenceBaseType", this.referenceBaseType.ToString());
    if (this.referenceBaseType == BaseReferenceNodeType.ntSelectedNode && this.NodeId != null && this.NodeId != "")
      xw.WriteAttributeString("nodeId", this.NodeId);
    if (this.attributeName != null && this.nodeLink != null && this.nodeLink.ContainsVirtualAttribute(this.attributeName) && DocumentTreeNode.VirtualAttributeNameDict_Loc_Inv.ContainsKey(this.attributeName))
    {
      xw.WriteAttributeString("attributeName", DocumentTreeNode.VirtualAttributeNameDict_Loc_Inv[this.attributeName]);
      xw.WriteAttributeString("virtAttr", "1");
    }
    else
      xw.WriteAttributeString("attributeName", this.attributeName);
  }

  /// <summary>Загрузить поле из текущего узла XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns>Возвращает true, если поле загружено</returns>
  public override bool ReadFieldFromXml(XmlReadArgs readArgs)
  {
    if ("referenceBaseType" == readArgs.Reader.LocalName)
    {
      this.referenceBaseType = (BaseReferenceNodeType) Enum.Parse(typeof (BaseReferenceNodeType), readArgs.Reader.Value);
      return true;
    }
    if ("attributeName" == readArgs.Reader.LocalName)
    {
      if (!readArgs.Reader.HasValue)
        readArgs.Reader.Read();
      this.attributeName = readArgs.Reader.Value;
      if (readArgs.Version < 24 && DocumentTreeNode.VirtualAttributeNameDict_Loc_Inv.ContainsKey(this.attributeName))
      {
        this.attributeName = DocumentTreeNode.VirtualAttributeNameDict_Loc_Inv[this.attributeName];
        if (DocumentTreeNode.VirtualAttributeNameDict_Inv_Loc.ContainsKey(this.attributeName))
          this.attributeName = DocumentTreeNode.VirtualAttributeNameDict_Inv_Loc[this.attributeName];
      }
      if (readArgs.Version < 36 && this.attributeName == "PageCount")
        this.attributeName = "LastDocPageNumber";
      return true;
    }
    if ("virtAttr" == readArgs.Reader.LocalName)
    {
      if (!readArgs.Reader.HasValue)
        readArgs.Reader.Read();
      if (readArgs.Reader.Value == "1" && DocumentTreeNode.VirtualAttributeNameDict_Inv_Loc.ContainsKey(this.attributeName))
        this.attributeName = DocumentTreeNode.VirtualAttributeNameDict_Inv_Loc[this.attributeName];
      return true;
    }
    return base.ReadFieldFromXml(readArgs);
  }

  /// <summary>Копировать данные</summary>
  /// <param name="src">Источник данных</param>
  /// <param name="saveText">Сохранять данные</param>
  public override void CopyData(ReferenceBase src, bool copyText = true)
  {
    base.CopyData(src, copyText);
    if (!(src is ReferenceToNodeAttributeBase nodeAttributeBase))
      return;
    this.attributeName = nodeAttributeBase.attributeName;
    this.referenceBaseType = nodeAttributeBase.referenceBaseType;
  }

  /// <summary>Назначить узел владелец</summary>
  /// <param name="value">Новый узел владелец</param>
  public override void AssignOwnerNode(DocumentTreeNode value)
  {
    if ((this.referenceBaseType == BaseReferenceNodeType.ntParentNode || this.referenceBaseType == BaseReferenceNodeType.ntUseParentLink) && this.OwnerNode != null)
      this.OwnerNode.ParentChanged -= new ParentChanged_EventHandler(this.OwnerNode_ParentChanged);
    base.AssignOwnerNode(value);
    if (this.referenceBaseType != BaseReferenceNodeType.ntParentNode && this.referenceBaseType != BaseReferenceNodeType.ntUseParentLink || this.OwnerNode == null)
      return;
    this.OwnerNode.ParentChanged += new ParentChanged_EventHandler(this.OwnerNode_ParentChanged);
    this.UpdateLink(false, false);
  }

  private void OwnerNode_ParentChanged(object sender, ParentChanged_EventArgs e)
  {
  }

  /// <summary>Имя базового типа ссылки для хранения в XML.
  /// Этот тип используется если TypeNameForXml не найден</summary>
  [Browsable(false)]
  protected override string BaseTypeNameForXml
  {
    [DebuggerStepThrough] get => nameof (ReferenceToNodeAttributeBase);
  }

  /// <summary>Имя типа ссылки для хранения в XML</summary>
  [Browsable(false)]
  public override string TypeNameForXml
  {
    [DebuggerStepThrough] get => "ReferenceToNodeAttribute";
  }

  /// <summary>Получить подтипы ссылки</summary>
  /// <param name="owner">Владелец ссылки</param>
  /// <param name="refInterface">Интерфейс, которому должна удовлетворять ссылка</param>
  /// <returns>Массив имен подтипов ссылки. Имена должны быть уникальными в пределах одного типа ссылки</returns>
  public virtual string[] GetReferenceSubTypes(DocumentTreeNode owner, Type refInterface)
  {
    if (refInterface == typeof (IEditableReferenceToTextSource))
      return new List<string>(7)
      {
        LocalizationHolder.rm.GetString("Interfaces.Document_103"),
        LocalizationHolder.rm.GetString("Interfaces.Document_104"),
        LocalizationHolder.rm.GetString("Interfaces.Document_105"),
        LocalizationHolder.rm.GetString("Interfaces.Document_106"),
        LocalizationHolder.rm.GetString("Interfaces.Document_107"),
        LocalizationHolder.rm.GetString("Interfaces.Document_108")
      }.ToArray();
    if (!(refInterface == typeof (IEditableReferenceToObject)))
      return (string[]) null;
    return new List<string>(7)
    {
      LocalizationHolder.rm.GetString("Interfaces.Document_110"),
      LocalizationHolder.rm.GetString("Interfaces.Document_111"),
      LocalizationHolder.rm.GetString("Interfaces.Document_112"),
      LocalizationHolder.rm.GetString("Interfaces.Document_113"),
      LocalizationHolder.rm.GetString("Interfaces.Document_114"),
      LocalizationHolder.rm.GetString("Interfaces.Document_115")
    }.ToArray();
  }

  /// <summary>Установить заданный подтип ссылки</summary>
  /// <param name="owner">Владелец ссылки</param>
  /// <param name="subType">Имя подтипа ссылки</param>
  /// <param name="refInterface">Интерфейс, которому должна удовлетворять ссылка</param>
  public virtual void SetReferenceSubType(
    DocumentTreeNode owner,
    string subType,
    Type refInterface)
  {
    string[] referenceSubTypes = this.GetReferenceSubTypes(owner, refInterface);
    int num = -1;
    if (referenceSubTypes != null && referenceSubTypes.Length != 0)
      num = Array.IndexOf<string>(referenceSubTypes, subType);
    this.referenceBaseType = num != -1 ? (BaseReferenceNodeType) num : throw new ArgumentException(LocalizationHolder.rm.GetString("Interfaces.Document_117"), "subType = " + subType);
    this.UpdateLink(false, false);
  }

  /// <summary>Получить индекс текущего подтипа ссылки</summary>
  /// <param name="refInterface">Интерфейс, которому должна удовлетворять ссылка</param>
  /// <returns>Индекс текущего подтипа ссылки</returns>
  public virtual int GetReferenceSubTypeIndex(Type refInterface) => (int) this.referenceBaseType;

  /// <summary>Имя объекта с которым связана ссылка. Если объект не найден, то null</summary>
  [Browsable(false)]
  public virtual string ObjectCaption
  {
    [DebuggerStepThrough] get => this.NodeLink != null ? this.NodeLink.GetDefautCaption() : "";
  }

  /// <summary>Можно ли вызвать диалог выбора объекта для ссылки</summary>
  [Browsable(false)]
  public virtual bool CanCallSelectObjectDialog
  {
    [DebuggerStepThrough] get => false;
  }

  /// <summary>Вызвать диалог выбора объекта для ссылки</summary>
  public virtual void CallSelectObjectDialog()
  {
  }

  /// <summary>Ссылка на атрибут объекта</summary>
  [Browsable(false)]
  public bool IsReferenceToAttribute
  {
    [DebuggerStepThrough] get => true;
  }

  /// <summary>Можно вызвать диалог выбора атрибута для ссылки</summary>
  [Browsable(false)]
  public virtual bool CanCallSelectAttributeDialog
  {
    [DebuggerStepThrough] get => false;
  }

  /// <summary>Вызвать диалог выбора атрибута для ссылки</summary>
  public virtual void CallSelectAttributeDialog()
  {
  }

  /// <summary>Получить список имен атрибутов, которые можно выбрать в ComboBox</summary>
  /// <returns>Список имен атрибутов</returns>
  public virtual string[] GetAttributeNameList()
  {
    if (this.NodeLink == null)
      return (string[]) null;
    StringCollection attributeNames = this.NodeLink.GetAttributeNames(true);
    List<string> stringList = new List<string>();
    foreach (object obj in (IEnumerable) attributeNames)
    {
      string str = obj.ToString();
      if (str != DocumentTreeNode.AttributeName_DocumentHasCheckSum)
        stringList.Add(str);
    }
    return stringList.ToArray();
  }

  /// <summary>Получить список имен атрибутов, которые можно выбрать в ComboBox</summary>
  /// <returns>Список имен атрибутов</returns>
  public virtual string[] GetLinkAttributeNameList() => (string[]) null;

  /// <summary>Обновить информацию об атрибуте. Имеет смысл для ссылок на атрибуты объектов БД.</summary>
  public void UpdateAttributeInfo()
  {
  }

  /// <summary>Можно вызвать диалог выбора ссылочного атрибута</summary>
  [Browsable(false)]
  public bool CanCallSelectLinkAttributeDialog => false;

  /// <summary>Вызвать диалог выбора ссылочного атрибута</summary>
  public void CallSelectLinkAttributeDialog()
  {
  }

  /// <summary>Используется ссылочный атрибут</summary>
  [Browsable(false)]
  public bool UseLinkAttribute
  {
    [DebuggerStepThrough] get => false;
  }

  /// <summary>Имя ссылочного атрибута</summary>
  [Browsable(false)]
  public string LinkAttributeName
  {
    get => "";
    set
    {
    }
  }

  /// <summary>Текст</summary>
  [Browsable(false)]
  public virtual string Text
  {
    [DebuggerStepThrough] get => this.GetAttributeStringValue((List<DocumentTreeNode>) null);
    set => this.SetAttributeValue(value, true, true, true);
  }

  /// <summary>Получить текст с защитой от циклических ссылок</summary>
  /// <param name="callChain">Цепочка вызова</param>
  /// <returns></returns>
  public string GetAcyclicText(List<DocumentTreeNode> callChain)
  {
    return this.GetAttributeStringValue(callChain);
  }

  /// <summary>Назначить значение Text</summary>
  /// <param name="value">Значение</param>
  /// <param name="saveUndo">Сохранять данные для Undo</param>
  /// <param name="updateUI">Обновить изображение</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public void SetText(string value, bool saveUndo, bool updateUI, bool updateLayout)
  {
    this.SetAttributeValue(value, saveUndo, updateUI, updateLayout, (List<DocumentTreeNode>) null);
  }

  /// <summary>Назначить значение Text</summary>
  /// <param name="value">Значение</param>
  /// <param name="saveUndo">Сохранять данные для Undo</param>
  /// <param name="updateUI">Обновить изображение</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  /// <param name="callChain">Цепочка вызовов, для защиты от зацикливания</param>
  public void SetText(
    string value,
    bool saveUndo,
    bool updateUI,
    bool updateLayout,
    List<DocumentTreeNode> callChain)
  {
    this.SetAttributeValue(value, saveUndo, updateUI, updateLayout, callChain);
  }

  /// <summary>Присвоить значение переменной Text без вызова обработчиков. Для внутреннего пользования!</summary>
  /// <param name="value">Значение</param>
  public void AssignText(string value)
  {
  }

  /// <summary>Только для чтения</summary>
  [TypeConverter(typeof (CustomBooleanConverter))]
  [CustomDisplayName("Attribute.Interfaces.Document_195")]
  [CustomDescription("Attribute.Interfaces.Document_196")]
  [CustomCategory("Attribute.Interfaces.Document_197")]
  public virtual bool ReadOnly
  {
    [DebuggerStepThrough] get => false;
  }

  /// <summary>Событие Текст изменен</summary>
  public event TextChanged_EventHandler TextChanged
  {
    add => this.textChanged += value;
    remove => this.textChanged -= value;
  }

  /// <summary>Вызывает событие Текст изменен</summary>
  /// <param name="oldText">Старое значение</param>
  /// <param name="newText">Новое значение</param>
  /// <param name="saveModificationDate">Изменения не влияющие на дату модификации документа</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public virtual void OnTextChanged(
    string oldText,
    string newText,
    bool saveModificationDate,
    bool updateUI,
    bool updateLayout)
  {
    this.OnTextChanged(new TextChanged_EventArgs(oldText, newText, true, true, saveModificationDate, updateUI, updateLayout));
  }

  /// <summary>Вызывает событие Текст изменен</summary>
  /// <param name="e">Данные события</param>
  protected virtual void OnTextChanged(TextChanged_EventArgs e)
  {
    if (this.OwnerNode is TextData ownerNode)
      ownerNode.OnTextChanged(e);
    if (this.textChanged == null)
      return;
    this.textChanged((object) this, e);
  }
}
