// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.UnknownReferenceToObject
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Вспомогательный класс для загрузки неизвестных типов ссылок на объект</summary>
[Serializable]
public class UnknownReferenceToObject : ReferenceBase, IEditableReferenceToObject
{
  private List<StringKeyValue> unknownXmlAttributes;
  private string unknownXmlElements;

  /// <summary>Создать пустой экземпляр класса с инициализацией только самых неоходимых полей.
  /// Используется в словаре кострукторов.</summary>
  public static object EmptyConstructor() => (object) new UnknownReferenceToObject();

  /// <summary>Конструктор</summary>
  /// <param name="ownerNode">Узел владелец</param>
  public UnknownReferenceToObject(DocumentTreeNode ownerNode)
    : base(ownerNode)
  {
  }

  /// <summary>Коструктор</summary>
  public UnknownReferenceToObject()
  {
  }

  /// <summary>Получить подтипы ссылки</summary>
  /// <param name="owner">Владелец ссылки</param>
  /// <param name="refInterface">Интерфейс, которому должна удовлетворять ссылка</param>
  /// <returns>Массив имен подтипов ссылки. Имена должны быть уникальными в пределах одного типа ссылки</returns>
  public virtual string[] GetReferenceSubTypes(DocumentTreeNode owner, Type refInterface)
  {
    if (owner == null || !(owner is INodeWithReference nodeWithReference) || nodeWithReference.Reference == null || !(nodeWithReference.Reference.GetType() == typeof (UnknownReferenceToObject)))
      return (string[]) null;
    return new string[1]
    {
      LocalizationHolder.rm.GetString("Interfaces.Document_84")
    };
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
    if (num == -1)
      throw new ArgumentException(LocalizationHolder.rm.GetString("Interfaces.Document_85"), "subType = " + subType);
  }

  /// <summary>Получить индекс текущего подтипа ссылки</summary>
  /// <param name="refInterface">Интерфейс, которому должна удовлетворять ссылка</param>
  /// <returns>Индекс текущего подтипа ссылки</returns>
  public virtual int GetReferenceSubTypeIndex(Type refInterface)
  {
    return refInterface == typeof (IEditableReferenceToObject) ? 0 : -1;
  }

  /// <summary>Имя объекта с которым связана ссылка. Если объект не найден, то null</summary>
  public virtual string ObjectCaption
  {
    [DebuggerStepThrough] get => "?";
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

  /// <summary>Получить список имен атрибутов, которые можно выбрать в ComboBox</summary>
  /// <returns>Список имен атрибутов</returns>
  public virtual string[] GetLinkAttributeNameList() => (string[]) null;

  /// <summary>XML атрибуты, не распознанные при загрузке</summary>
  public override List<StringKeyValue> UnknownXmlAttributes
  {
    get => this.unknownXmlAttributes;
    set => this.unknownXmlAttributes = value;
  }

  /// <summary>XML элементы, не распознанные при загрузке</summary>
  public override string UnknownXmlElements
  {
    get => this.unknownXmlElements;
    set => this.unknownXmlElements = value;
  }

  /// <summary>Сохранить данные в атрибуты XML</summary>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  protected override void WriteXmlAttributes(XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    base.WriteXmlAttributes(xw, objectRefId);
  }

  /// <summary>Загрузить поле из текущего узла XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns>Возвращает true, если поле загружено</returns>
  public override bool ReadFieldFromXml(XmlReadArgs readArgs)
  {
    return !(readArgs.Reader.LocalName == "type") && !(readArgs.Reader.LocalName == "baseType") && base.ReadFieldFromXml(readArgs);
  }
}
