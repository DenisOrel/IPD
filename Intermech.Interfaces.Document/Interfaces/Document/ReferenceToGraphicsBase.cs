// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.ReferenceToGraphicsBase
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using Intermech.IO;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.Document;

[Serializable]
public class ReferenceToGraphicsBase : ReferenceToDBObjectBase
{
  protected string attributeName;
  /// <summary>Имя типа сохряняемое в XML</summary>
  public new static string XmlTypeName = "RefToGr";
  protected List<string> layers;
  protected string fileName;
  protected Guid fileAttrGuid = Guid.Empty;
  protected int fileAttrID = -1;
  protected Image image;

  /// <summary>Слои, которые нужно отображать, если null, то отображаются все слои.</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_403")]
  [CustomDescription("Attribute.Interfaces.Document_404")]
  [CustomCategory("Attribute.Interfaces.Document_405")]
  public List<string> Layers
  {
    [DebuggerStepThrough] get => this.layers;
    set => this.layers = value;
  }

  /// <summary>Кэш изображения полученного по ссылке</summary>
  [Browsable(false)]
  public Image ImageCache
  {
    [DebuggerStepThrough] get => this.image;
    set => this.image = value;
  }

  /// <summary>Имя файлового атрибута, если null, то используется первый файл</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_406")]
  [CustomDescription("Attribute.Interfaces.Document_407")]
  [CustomCategory("Attribute.Interfaces.Document_408")]
  public string FileName
  {
    [DebuggerStepThrough] get => this.fileName;
  }

  /// <summary>Идентификатор атрибута хранящего файл</summary>
  [Browsable(false)]
  public int FileAttrID
  {
    [DebuggerStepThrough] get => this.fileAttrID;
  }

  /// <summary>Guid атрибута хранящего файл, если Guid.Empty, то используется атрибут "Файл"</summary>
  [Browsable(false)]
  public Guid FileAttrGuid
  {
    [DebuggerStepThrough] get => this.fileAttrGuid;
  }

  /// <summary>Имя атрибута</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_385")]
  [CustomDescription("Attribute.Interfaces.Document_386")]
  [CustomCategory("Attribute.Interfaces.Document_387")]
  public virtual string AttributeName
  {
    [DebuggerStepThrough] get => this.attributeName;
    set => this.attributeName = value;
  }

  /// <summary>Назначить данные атрибута</summary>
  /// <param name="attrGuid">Guid атрибута хранящего файл, если Guid.Empty, то используется атрибут "Файл"</param>
  /// <param name="attributeName">Имя файла в атрибуте, если null, то используется первый файл</param>
  /// <param name="fileName">Имя файла в атрибуте, если null, то используется первый файл</param>
  /// <param name="layers">Слои, которые нужно отображать, если null, то отображаются все слои</param>
  public void AssignAttributeInfo(
    Guid attrGuid,
    string attributeName,
    string fileName,
    List<string> layers)
  {
    this.fileAttrGuid = attrGuid;
    this.fileAttrID = -1;
    this.attributeName = attributeName;
    this.fileName = fileName;
    this.layers = layers;
  }

  /// <summary>Обновить информацию об атрибуте.
  /// Получить FileAttrID по заданному FileAttrGuid, или наоборот.</summary>
  public virtual void UpdateAttributeInfo()
  {
  }

  /// <summary>Получить список имен атрибутов, которые можно выбрать в ComboBox</summary>
  /// <returns>Список имен атрибутов</returns>
  public virtual string[] GetAttributeNameList() => (string[]) null;

  /// <summary>Идентификатор атрибута получен</summary>
  [Browsable(false)]
  public virtual bool IsConnectedAttributeRef
  {
    [DebuggerStepThrough] get
    {
      return this.fileAttrID != -1 && this.fileAttrGuid != Guid.Empty && this.IsConnectedObjectRef;
    }
  }

  /// <summary>Идентификатор объекта получен</summary>
  public override bool IsConnectedObjectRef
  {
    get => !this.IsReferenceToRelation ? this.DBObjectID != -1L : this.DBRelationID != -1L;
  }

  /// <summary>Связь подключена. Есть ссылки на объекты</summary>
  public override bool IsConnected => this.IsConnectedAttributeRef;

  public override void DisconnectLink()
  {
    this.image = (Image) null;
    base.DisconnectLink();
  }

  /// <summary>Создать пустой экземпляр класса с инициализацией только самых неоходимых полей.
  /// Используется в словаре кострукторов.</summary>
  public new static object EmptyConstructor() => (object) new ReferenceToGraphicsBase();

  /// <summary>Создать пустой экземпляр класса с инициализацией только самых неоходимых полей.
  /// Используется в словаре кострукторов.</summary>
  public new static object EmptyConstructorActiveLink()
  {
    ReferenceToGraphicsBase referenceToGraphicsBase = new ReferenceToGraphicsBase();
    referenceToGraphicsBase.passiveLink = false;
    return (object) referenceToGraphicsBase;
  }

  /// <summary>Коструктор</summary>
  public ReferenceToGraphicsBase()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="ownerNode">Узел владелец</param>
  /// <param name="refType">Тип ссылки на объект БД</param>
  /// <param name="dbObjectInfo">Идентификаторы и информация об объекте</param>
  /// <param name="fileAttrGuid">Guid атрибута хранящего файл, если Guid.Empty, то используется имя атрибута</param>
  /// <param name="attributeName">Имя атрибута, если null</param>
  /// <param name="fileName">Имя файла в атрибуте, если null, то используется первый файл</param>
  /// <param name="layers">Слои, которые нужно отображать, если null, то отображаются все слои</param>
  /// <param name="passiveLink">Пассивная ссылка</param>
  public ReferenceToGraphicsBase(
    DocumentTreeNode ownerNode,
    RefToDBObjectType refType,
    DBObjectInfoBase dbObjectInfo,
    Guid fileAttrGuid,
    string attributeName,
    string fileName,
    List<string> layers,
    bool passiveLink)
    : base(ownerNode, refType, dbObjectInfo, passiveLink)
  {
    this.AssignAttributeInfo(fileAttrGuid, attributeName, fileName, layers);
  }

  /// <summary>Конструктор</summary>
  /// <param name="ownerNode">Узел владелец</param>
  /// <param name="dbObjectGuid">Guid версии объекта БД</param>
  /// <param name="layers">Слои, которые нужно отображать, если null, то отображаются все слои</param>
  /// <param name="passiveLink">Пассивная ссылка</param>
  public ReferenceToGraphicsBase(
    DocumentTreeNode ownerNode,
    Guid dbObjectGuid,
    List<string> layers,
    bool passiveLink)
    : base(ownerNode, dbObjectGuid, passiveLink)
  {
    this.layers = layers;
  }

  /// <summary>Конструктор</summary>
  /// <param name="dbObjectGuid">Guid версии объекта БД</param>
  /// <param name="layers">Слои, которые нужно отображать, если null, то отображаются все слои</param>
  /// <param name="passiveLink">Пассивная ссылка</param>
  public ReferenceToGraphicsBase(Guid dbObjectGuid, List<string> layers, bool passiveLink)
    : this((DocumentTreeNode) null, dbObjectGuid, layers, passiveLink)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="dbObjectGuid">Guid версии объекта БД</param>
  /// <param name="passiveLink">Пассивная ссылка</param>
  public ReferenceToGraphicsBase(Guid dbObjectGuid, bool passiveLink)
    : base((DocumentTreeNode) null, dbObjectGuid, passiveLink)
  {
  }

  /// <summary>Вызвать диалог выбора атрибута для ссылки</summary>
  public virtual void CallSelectAttributeDialog()
  {
  }

  public override bool CanCallEditor => base.CanCallEditor;

  /// <summary>Копировать данные</summary>
  /// <param name="src">Источник данных</param>
  /// <param name="saveText">Сохранять данные</param>
  public override void CopyData(ReferenceBase src, bool copyText = true)
  {
    base.CopyData(src, copyText);
    if (!(src is ReferenceToGraphicsBase referenceToGraphicsBase))
      return;
    if (referenceToGraphicsBase.layers != null)
      this.layers = new List<string>((IEnumerable<string>) referenceToGraphicsBase.layers);
    this.fileName = referenceToGraphicsBase.fileName;
    this.fileAttrGuid = referenceToGraphicsBase.fileAttrGuid;
    this.fileAttrID = referenceToGraphicsBase.fileAttrID;
    this.attributeName = referenceToGraphicsBase.attributeName;
    this.image = referenceToGraphicsBase.image;
  }

  /// <summary>Загрузить поле из текущего узла XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns>Возвращает true, если поле загружено</returns>
  public override bool ReadFieldFromXml(XmlReadArgs readArgs)
  {
    if (base.ReadFieldFromXml(readArgs))
      return true;
    switch (readArgs.Reader.LocalName)
    {
      case "Layers":
        string[] collection = (string[]) WriteReadXmlHelper.ReadArrayFromXml(typeof (string), readArgs);
        if (collection != null && collection.Length != 0)
          this.layers = new List<string>((IEnumerable<string>) collection);
        return true;
      case "fileName":
        this.fileName = readArgs.Reader.Value;
        return true;
      case "fileAttr":
        this.fileAttrGuid = new Guid(readArgs.Reader.Value);
        return true;
      case "attributeName":
        this.attributeName = readArgs.Reader.Value;
        return true;
      case "Image":
        string str = (string) null;
        if (readArgs.Reader.HasAttributes)
        {
          int i = 0;
          for (int attributeCount = readArgs.Reader.AttributeCount; i < attributeCount; ++i)
          {
            readArgs.Reader.MoveToAttribute(i);
            if (readArgs.Reader.LocalName == "refId")
              str = readArgs.Reader.Value;
          }
          readArgs.Reader.MoveToElement();
        }
        if (!readArgs.Reader.HasValue && !readArgs.Reader.IsEmptyElement)
          readArgs.Reader.Read();
        if (readArgs.Reader.HasValue)
          this.image = Image.FromStream((Stream) new MemoryStream(Convert.FromBase64String(readArgs.Reader.Value)));
        if (str != null)
        {
          if (this.image == null)
            this.image = readArgs.ObjectsId[(object) str] as Image;
          else if (!readArgs.ObjectsId.Contains((object) str))
            readArgs.ObjectsId.Add((object) str, (object) this.image);
          if (this.image == null)
            DocumentTreeNode.AddObjectReference((object) this, readArgs.ObjectReferences, "image", str);
        }
        return true;
      default:
        return false;
    }
  }

  /// <summary>Сохранить данные в атрибуты XML</summary>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  protected override void WriteXmlAttributes(XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    base.WriteXmlAttributes(xw, objectRefId);
    if (this.fileName != null && this.fileName != "")
      xw.WriteAttributeString("fileName", this.fileName);
    if (this.attributeName != null && this.attributeName != "")
      xw.WriteAttributeString("attributeName", this.attributeName);
    if (!(this.fileAttrGuid != Guid.Empty))
      return;
    xw.WriteAttributeString("fileAttr", this.fileAttrGuid.ToString());
  }

  /// <summary>Сохранить данные в элементы XML</summary>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  protected override void WriteXmlElements(XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    base.WriteXmlElements(xw, objectRefId);
    if (this.image != null)
    {
      xw.WriteStartElement("Image");
      bool firstTime;
      string str = objectRefId.GetId((object) this.image, out firstTime).ToString((IFormatProvider) CultureInfo.InvariantCulture);
      xw.WriteAttributeString("refId", str);
      if (firstTime)
      {
        ImChunkedStream imChunkedStream = new ImChunkedStream();
        this.SaveImageToStream(this.image, (Stream) imChunkedStream);
        WriteReadXmlHelper.WriteBase64ToCurrentXmlElement((Stream) imChunkedStream, xw);
      }
      xw.WriteEndElement();
    }
    if (this.layers == null || this.layers.Count <= 0)
      return;
    WriteReadXmlHelper.WriteArrayToXml("Layers", (IList) this.layers.ToArray(), "layers", xw, objectRefId);
  }

  /// <summary>Сохранить Image в Stream</summary>
  /// <param name="image">Изображение</param>
  /// <param name="stream">Поток</param>
  protected virtual void SaveImageToStream(Image image, Stream stream)
  {
    if (image == null)
      throw new ArgumentNullException(nameof (image));
    if (stream == null)
      throw new ArgumentNullException(nameof (stream));
    image.Save(stream, image.RawFormat);
  }

  /// <summary>Имя базового типа ссылки для хранения в XML.
  /// Этот тип используется если TypeNameForXml не найден</summary>
  protected override string BaseTypeNameForXml => ReferenceToGraphicsBase.XmlTypeName;

  /// <summary>Имя типа сохряняемое в XML</summary>
  public override string TypeNameForXml => ReferenceToGraphicsBase.XmlTypeName;

  /// <summary>Получить бинарный поток изображения</summary>
  /// <returns></returns>
  public virtual Stream GetGraphicsStream() => (Stream) null;
}
