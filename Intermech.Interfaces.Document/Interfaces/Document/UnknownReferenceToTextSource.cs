// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.UnknownReferenceToTextSource
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

/// <summary>Вспомогательный класс для загрузки неизвестных типов ссылок на источник текста</summary>
[Serializable]
public class UnknownReferenceToTextSource : 
  UnknownReferenceToObject,
  ITextSource,
  IEditableReferenceToTextSource,
  IEditableReferenceToObject
{
  [NonSerialized]
  private TextChanged_EventHandler textChanged;
  private string text;
  private string attributeName;

  /// <summary>Создать пустой экземпляр класса с инициализацией только самых неоходимых полей.
  /// Используется в словаре кострукторов.</summary>
  public new static object EmptyConstructor() => (object) new UnknownReferenceToTextSource();

  /// <summary>Конструктор</summary>
  /// <param name="ownerNode">Узел владелец</param>
  public UnknownReferenceToTextSource(DocumentTreeNode ownerNode)
    : base(ownerNode)
  {
  }

  /// <summary>Коструктор</summary>
  public UnknownReferenceToTextSource()
  {
  }

  /// <summary>Загрузить поле из текущего узла XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns>Возвращает true, если поле загружено</returns>
  public override bool ReadFieldFromXml(XmlReadArgs readArgs)
  {
    if (readArgs.Reader.LocalName == "Text")
    {
      if (!readArgs.Reader.IsEmptyElement)
      {
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        if (readArgs.Reader.NodeType == XmlNodeType.Text || readArgs.Reader.NodeType == XmlNodeType.Whitespace)
          this.text = readArgs.Reader.Value;
      }
      return true;
    }
    if ("attributeName" == readArgs.Reader.LocalName)
    {
      if (!readArgs.Reader.HasValue)
        readArgs.Reader.Read();
      this.attributeName = readArgs.Reader.Value;
      return true;
    }
    return base.ReadFieldFromXml(readArgs);
  }

  /// <summary>Сохранить данные в элементы XML</summary>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  protected override void WriteXmlElements(XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    base.WriteXmlElements(xw, objectRefId);
    if (this.text == null)
      return;
    xw.WriteElementString("Text", this.text);
  }

  /// <summary>Сохранить данные в атрибуты XML</summary>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  protected override void WriteXmlAttributes(XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    base.WriteXmlAttributes(xw, objectRefId);
    if (this.attributeName == null || !(this.attributeName != ""))
      return;
    xw.WriteAttributeString("attributeName", this.attributeName);
  }

  /// <summary>Текст</summary>
  public string Text
  {
    [DebuggerStepThrough] get => !this.CanShowReference() ? "" : this.text;
    set => this.SetText(value, true, true, true);
  }

  /// <summary>Получить текст с защитой от циклических ссылок</summary>
  /// <param name="callChain">Цепочка вызовов</param>
  /// <returns></returns>
  public string GetAcyclicText(List<DocumentTreeNode> callChain) => this.Text;

  /// <summary>Назначить значение Text</summary>
  /// <param name="value">Значение</param>
  /// <param name="saveUndo">Сохранять данные для Undo</param>
  /// <param name="updateUI">Обновить изображение</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public void SetText(string value, bool saveUndo, bool updateUI, bool updateLayout)
  {
    this.text = value;
  }

  /// <summary>Присвоить значение переменной Text без вызова обработчиков. Для внутреннего пользования!</summary>
  /// <param name="value">Значение</param>
  public void AssignText(string value)
  {
    if (!(this.text != value))
      return;
    this.text = value;
  }

  /// <summary>Только для чтения</summary>
  public bool ReadOnly
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
  /// <param name="e">Данные события</param>
  protected virtual void OnTextChanged(TextChanged_EventArgs e)
  {
    if (this.OwnerNode is TextData ownerNode)
      ownerNode.OnTextChanged(e);
    if (this.textChanged == null)
      return;
    this.textChanged((object) this, e);
  }

  /// <summary>Ссылка на атрибут объекта</summary>
  [Browsable(false)]
  public virtual bool IsReferenceToAttribute
  {
    [DebuggerStepThrough] get => true;
  }

  /// <summary>Имя атрибута</summary>
  [ReadOnlyForDlg(true)]
  public virtual string AttributeName
  {
    [DebuggerStepThrough] get => this.attributeName;
    set
    {
    }
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
  public virtual string[] GetAttributeNameList() => (string[]) null;

  /// <summary>Получить список имен атрибутов, которые можно выбрать в ComboBox</summary>
  /// <returns>Список имен атрибутов</returns>
  public override string[] GetLinkAttributeNameList() => (string[]) null;

  /// <summary>Обновить информацию об атрибуте. Имеет смысл для ссылок на атрибуты объектов БД.</summary>
  public void UpdateAttributeInfo()
  {
  }

  /// <summary>Получить подтипы ссылки</summary>
  /// <param name="owner">Владелец ссылки</param>
  /// <param name="refInterface">Интерфейс, которому должна удовлетворять ссылка</param>
  /// <returns>Массив имен подтипов ссылки. Имена должны быть уникальными в пределах одного типа ссылки</returns>
  public override string[] GetReferenceSubTypes(DocumentTreeNode owner, Type refInterface)
  {
    if (owner == null || !(owner is INodeWithReference nodeWithReference) || nodeWithReference.Reference == null || !(nodeWithReference.Reference.GetType() == typeof (UnknownReferenceToTextSource)))
      return (string[]) null;
    return new string[1]
    {
      LocalizationHolder.rm.GetString("Interfaces.Document_86")
    };
  }

  /// <summary>Установить заданный подтип ссылки</summary>
  /// <param name="owner">Владелец ссылки</param>
  /// <param name="subType">Имя подтипа ссылки</param>
  /// <param name="refInterface">Интерфейс, которому должна удовлетворять ссылка</param>
  public override void SetReferenceSubType(
    DocumentTreeNode owner,
    string subType,
    Type refInterface)
  {
    string[] referenceSubTypes = this.GetReferenceSubTypes(owner, refInterface);
    int num = -1;
    if (referenceSubTypes != null && referenceSubTypes.Length != 0)
      num = Array.IndexOf<string>(referenceSubTypes, subType);
    if (num == -1)
      throw new ArgumentException(LocalizationHolder.rm.GetString("Interfaces.Document_87"), "subType = " + subType);
  }

  /// <summary>Получить индекс текущего подтипа ссылки</summary>
  /// <param name="refInterface">Интерфейс, которому должна удовлетворять ссылка</param>
  /// <returns>Индекс текущего подтипа ссылки</returns>
  public override int GetReferenceSubTypeIndex(Type refInterface)
  {
    return refInterface == typeof (IEditableReferenceToTextSource) ? 0 : -1;
  }

  /// <summary>Имя объекта с которым связана ссылка. Если объект не найден, то null</summary>
  public override string ObjectCaption => base.ObjectCaption;

  /// <summary>Можно ли вызвать диалог выбора объекта для ссылки</summary>
  public override bool CanCallSelectObjectDialog => base.CanCallSelectObjectDialog;

  /// <summary>Вызвать диалог выбора объекта для ссылки</summary>
  public override void CallSelectObjectDialog() => base.CallSelectObjectDialog();

  /// <summary>Копировать данные</summary>
  /// <param name="saveText">Сохранять данные</param>
  public override void CopyData(ReferenceBase src, bool copyText = true)
  {
    base.CopyData(src, copyText);
    if (!(src is UnknownReferenceToTextSource referenceToTextSource))
      return;
    if (copyText)
      this.text = referenceToTextSource.text;
    this.attributeName = referenceToTextSource.attributeName;
  }
}
