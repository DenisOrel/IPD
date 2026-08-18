// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.RectangleBorder
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Стиль линий прямоугольника.
/// Используется в прямоугольных элементах документа. Поддерживает для каждой стороны свой стиль линии.</summary>
[TypeConverter(typeof (RectangleBorderConverter))]
[Serializable]
public abstract class RectangleBorder : ICloneable, IWriteReadXml
{
  /// <summary>Нет назначенных свойств</summary>
  [Browsable(false)]
  public bool IsEmpty
  {
    get
    {
      return this.Top == null && this.Bottom == null && this.Left == null && this.Right == null && this.InnerHorizontal == null;
    }
  }

  /// <summary>Стиль линии верхней границы</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_280")]
  [CustomDescription("Attribute.Interfaces.Document_281")]
  [CustomCategory("Attribute.Interfaces.Document_282")]
  public abstract BorderLine Top { get; set; }

  /// <summary>Стиль линии нижней границы</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_283")]
  [CustomDescription("Attribute.Interfaces.Document_284")]
  [CustomCategory("Attribute.Interfaces.Document_282")]
  public abstract BorderLine Bottom { get; set; }

  /// <summary>Стиль линии левой границы</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_286")]
  [CustomDescription("Attribute.Interfaces.Document_287")]
  [CustomCategory("Attribute.Interfaces.Document_282")]
  public abstract BorderLine Left { get; set; }

  /// <summary>Стиль линии правой границы</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_289")]
  [CustomDescription("Attribute.Interfaces.Document_290")]
  [CustomCategory("Attribute.Interfaces.Document_282")]
  public abstract BorderLine Right { get; set; }

  /// <summary>Стиль внутренней горизонтальной линии</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_553")]
  [CustomDescription("Attribute.Interfaces.Document_554")]
  [CustomCategory("Attribute.Interfaces.Document_282")]
  public abstract BorderLine InnerHorizontal { get; set; }

  /// <summary>Реализация интерфейса ICloneable</summary>
  /// <returns>Копия экземпляра класса</returns>
  object ICloneable.Clone() => (object) this.Clone();

  /// <summary>Создать полную копию экземпляра класса</summary>
  /// <returns>Копия экземпляра класса</returns>
  public abstract RectangleBorder Clone();

  /// <summary>Записать поля в XML</summary>
  /// <param name="elementName">Имя элемента XML, под которым нужно сохранить данные</param>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  public abstract void WriteToXml(string elementName, XmlWriter xw, ObjectIDGenerator objectRefId);

  /// <summary>Прочитать одно поле из XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns>Возвращает true, если поле прочитано</returns>
  public abstract bool ReadFieldFromXml(XmlReadArgs readArgs);

  /// <summary>Загрузить из XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  public virtual void ReadFromXml(XmlReadArgs readArgs)
  {
    WriteReadXmlHelper.ReadFromXml((IWriteReadXml) this, readArgs);
  }
}
