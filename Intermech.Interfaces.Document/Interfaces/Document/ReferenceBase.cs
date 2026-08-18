// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.ReferenceBase
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Ссылка на абстракный объект из узла документа</summary>
[TypeConverter(typeof (ReferenceBaseConverter))]
[Serializable]
public class ReferenceBase : ICloneable, IWriteReadXml, IUnknownXmlElement
{
  /// <summary>Список классов Ссылок, нужен для выбора пользователем типа ссылки</summary>
  public static readonly List<Type> ReferenceClassList = new List<Type>();
  [NonSerialized]
  private DocumentTreeNode ownerNode;
  [NonSerialized]
  private ReferenceViewType viewType;

  /// <summary>Конструктор</summary>
  public ReferenceBase()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="ownerNode">Узел владелец</param>
  public ReferenceBase(DocumentTreeNode ownerNode) => this.AssignOwnerNode(ownerNode);

  /// <summary>Документ владелец ссылающегося узла</summary>
  [Browsable(false)]
  public virtual ImDocumentData OwnerDocument
  {
    [DebuggerStepThrough] get
    {
      if (this.ownerNode is ImDocumentData)
        return this.ownerNode as ImDocumentData;
      return this.ownerNode is IDocumentElement ownerNode ? ownerNode.OwnerDocument : (ImDocumentData) null;
    }
  }

  /// <summary>Документ владелец ссылающегося узла</summary>
  [Browsable(false)]
  public virtual PageData OwnerPage
  {
    [DebuggerStepThrough] get
    {
      return this.ownerNode is PageElementNode ownerNode ? ownerNode.Page : (PageData) null;
    }
  }

  /// <summary>Узел владелец ссылки</summary>
  [Browsable(false)]
  public virtual DocumentTreeNode OwnerNode
  {
    [DebuggerStepThrough] get => this.ownerNode;
  }

  /// <summary>Ссылка используется только при печати документа</summary>
  [Browsable(false)]
  public virtual bool PrintReference
  {
    [DebuggerStepThrough] get => this.viewType.HasFlag((Enum) ReferenceViewType.Print);
    set
    {
      if (value)
        this.viewType |= ReferenceViewType.Print;
      else
        this.viewType &= ~ReferenceViewType.Print;
    }
  }

  /// <summary>Ссылка используется только при печати документа</summary>
  [Browsable(false)]
  public virtual bool IsDependOnPrint
  {
    [DebuggerStepThrough] get => this.PrintReference;
  }

  /// <summary>Назначить узел владелец</summary>
  /// <param name="value">Новый узел владелец</param>
  public virtual void AssignOwnerNode(DocumentTreeNode value)
  {
    if (this.ownerNode == value)
      return;
    this.ownerNode = value;
  }

  /// <summary>Разорвать связь</summary>
  public virtual void DisconnectLink()
  {
  }

  /// <summary>Обновить связь</summary>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public void UpdateLink(bool updateUI, bool updateLayout)
  {
    this.UpdateLink(false, updateUI, updateLayout);
  }

  /// <summary>Обновить связь</summary>
  /// <param name="forceUpdate">Обновлять даже для пассивных ссылок</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public virtual void UpdateLink(bool forceUpdate, bool updateUI, bool updateLayout)
  {
  }

  /// <summary>Ссылка зависит от документа. Т.е. при смене документа ее нужно обновлять.</summary>
  [Browsable(false)]
  public virtual bool IsDependOnDocument
  {
    [DebuggerStepThrough] get => false;
  }

  /// <summary>Ссылка зависит от страницы. Т.е. при смене страницы ее нужно обновлять.</summary>
  [Browsable(false)]
  public virtual bool IsDependOnPage
  {
    [DebuggerStepThrough] get => false;
  }

  /// <summary>Ссылка зависит от родительского узла. Т.е. при смене родителя ее нужно обновлять.</summary>
  [Browsable(false)]
  public virtual bool IsDependOnParent
  {
    [DebuggerStepThrough] get => false;
  }

  /// <summary>Пустая ссылка</summary>
  [Browsable(false)]
  public virtual bool IsEmpty
  {
    [DebuggerStepThrough] get => false;
  }

  /// <summary>Связь подключена. Есть ссылки на объекты</summary>
  [Browsable(false)]
  public virtual bool IsConnected
  {
    [DebuggerStepThrough] get => false;
  }

  /// <summary>Можно редактировать по месту. Для ссылок на атрибуты</summary>
  [Browsable(false)]
  [Category("Debug")]
  public virtual bool CanInplaceEdit
  {
    [DebuggerStepThrough] get => false;
  }

  /// <summary>Возможен вызов дополнительного редактора для элемента</summary>
  [Browsable(false)]
  public virtual bool CanCallEditor
  {
    [DebuggerStepThrough] get => false;
  }

  /// <summary>Вызвать редактор</summary>
  /// <returns>Результат вызова</returns>
  public virtual bool CallEditor() => false;

  /// <summary>Необходимо сохранять в файл текстовое значение полученное по ссылке</summary>
  [Browsable(false)]
  public virtual bool NeedSaveTextValueToFile
  {
    [DebuggerStepThrough] get => false;
  }

  /// <summary>Необходимо сохранять в кэш текстовое значение полученное по ссылке</summary>
  [Browsable(false)]
  public virtual bool NeedSaveTextValueToCache
  {
    [DebuggerStepThrough] get => false;
  }

  /// <summary>Клонировать ссылку</summary>
  /// <returns>Копию ссылки</returns>
  public ReferenceBase Clone()
  {
    ReferenceBase instance = (ReferenceBase) Activator.CreateInstance(this.GetType(), true);
    instance.CopyData(this);
    return instance;
  }

  /// <summary>Копировать данные</summary>
  /// <param name="src">Источник данных</param>
  /// <param name="saveText">Сохранять данные</param>
  public virtual void CopyData(ReferenceBase src, bool copyText = true)
  {
    this.UnknownXmlElements = src.UnknownXmlElements;
    this.UnknownXmlAttributes = src.UnknownXmlAttributes;
    this.viewType = src.viewType;
  }

  /// <summary>Клонировать ссылку</summary>
  /// <returns>Копию ссылки</returns>
  object ICloneable.Clone() => (object) this.Clone();

  /// <summary>Имя типа ссылки для хранения в XML</summary>
  [Browsable(false)]
  public virtual string TypeNameForXml
  {
    [DebuggerStepThrough] get => this.GetType().Name;
  }

  /// <summary>Имя базового типа ссылки для хранения в XML.
  /// Этот тип используется если TypeNameForXml не найден</summary>
  [Browsable(false)]
  protected virtual string BaseTypeNameForXml
  {
    [DebuggerStepThrough] get
    {
      string str = typeof (ReferenceBase).Namespace;
      Type type = this.GetType();
      while (type.Namespace != str)
        type = type.BaseType;
      return type.Name;
    }
  }

  /// <summary>Загрузить поле из текущего узла XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns>Возвращает true, если поле загружено</returns>
  public virtual bool ReadFieldFromXml(XmlReadArgs readArgs)
  {
    if (readArgs.Reader.LocalName == "type" || readArgs.Reader.LocalName == "baseType")
      return true;
    if (!(readArgs.Reader.LocalName == "viewType"))
      return false;
    this.viewType = (ReferenceViewType) Convert.ToInt32(readArgs.Reader.Value);
    return true;
  }

  /// <summary>Записать поля в XML</summary>
  /// <param name="elementName">Имя элемента XML, под которым нужно сохранить данные</param>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  public virtual void WriteToXml(string elementName, XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    xw.WriteStartElement(elementName);
    this.WriteXmlAttributes(xw, objectRefId);
    this.WriteXmlElements(xw, objectRefId);
    xw.WriteEndElement();
  }

  /// <summary>Сохранить данные в атрибуты XML</summary>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  protected virtual void WriteXmlAttributes(XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    string str1 = (string) null;
    string str2 = (string) null;
    List<StringKeyValue> unknownXmlAttributes = this.UnknownXmlAttributes;
    if (unknownXmlAttributes != null)
    {
      for (int index = 0; (str1 == null || str1 == "" || str2 == null || str2 == "") && index < unknownXmlAttributes.Count; ++index)
      {
        if (unknownXmlAttributes[index].Key == "type")
          str1 = unknownXmlAttributes[index].Value;
        else if (unknownXmlAttributes[index].Key == "baseType")
          str2 = unknownXmlAttributes[index].Value;
      }
    }
    if (str1 == null || str1 == "")
      str1 = this.TypeNameForXml;
    if (str2 == null || str2 == "")
      str2 = this.BaseTypeNameForXml;
    if (str1 == null || str1 == "")
      str1 = this.TypeNameForXml;
    xw.WriteAttributeString("type", str1);
    if (str2 == null || str2 == "")
      str2 = this.BaseTypeNameForXml;
    if (str2 != str1)
      xw.WriteAttributeString("baseType", str2);
    if (unknownXmlAttributes != null)
    {
      for (int index = 0; index < unknownXmlAttributes.Count; ++index)
      {
        if (unknownXmlAttributes[index].Key != "type" && unknownXmlAttributes[index].Key != "baseType")
          xw.WriteAttributeString(unknownXmlAttributes[index].Key, unknownXmlAttributes[index].Value);
      }
    }
    if (this.viewType == ReferenceViewType.Edit)
      return;
    xw.WriteAttributeString("viewType", ((int) this.viewType).ToString());
  }

  /// <summary>Сохранить данные в элементы XML</summary>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  protected virtual void WriteXmlElements(XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    if (this.UnknownXmlElements == null || !(this.UnknownXmlElements != ""))
      return;
    xw.WriteRaw(this.UnknownXmlElements);
  }

  /// <summary>Загрузить ссылку из XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  public virtual void ReadFromXml(XmlReadArgs readArgs)
  {
    WriteReadXmlHelper.ReadFromXml((IWriteReadXml) this, readArgs);
  }

  /// <summary>Создать и загрузить cсылку из XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  public static ReferenceBase LoadFromXml(XmlReadArgs readArgs)
  {
    return (ReferenceBase) WriteReadXmlHelper.ReadTypedElementFromXml(readArgs);
  }

  /// <summary>XML атрибуты, не распознанные при загрузке</summary>
  [Browsable(false)]
  public virtual List<StringKeyValue> UnknownXmlAttributes
  {
    [DebuggerStepThrough] get => (List<StringKeyValue>) null;
    set
    {
    }
  }

  /// <summary>XML элементы, не распознанные при загрузке</summary>
  [Browsable(false)]
  public virtual string UnknownXmlElements
  {
    [DebuggerStepThrough] get => (string) null;
    set
    {
    }
  }

  /// <summary>Добваить неизветсный атрибут</summary>
  /// <param name="key">Имя атрибута</param>
  /// <param name="value">Значение атрибута</param>
  public virtual void AddUnknownXmlAttribute(string key, string value)
  {
  }

  /// <summary>Можно ли показывать содержимое связи</summary>
  /// <returns></returns>
  public virtual bool CanShowReference()
  {
    return !this.IsDependOnPrint || this.OwnerDocument == null || this.OwnerDocument.NowPrinting;
  }
}
