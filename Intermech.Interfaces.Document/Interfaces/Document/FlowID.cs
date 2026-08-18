// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.FlowID
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Класс для идентификации потоков данных</summary>
[TypeConverter(typeof (LocalizedExpandableObjectConverter))]
[Serializable]
public class FlowID : IWriteReadXml, ICloneable
{
  private string name;
  private FlowID templateFlowID;

  /// <summary>Имя потока</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_124")]
  [CustomDescription("Attribute.Interfaces.Document_125")]
  [CustomCategory("Attribute.Interfaces.Document_126")]
  public string Name
  {
    [DebuggerStepThrough] get => this.name;
    set => this.name = value;
  }

  /// <summary>Шаблон потока</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_127")]
  [CustomDescription("Attribute.Interfaces.Document_128")]
  [CustomCategory("Attribute.Interfaces.Document_129")]
  [ReadOnly(true)]
  public FlowID TemplateFlowID
  {
    [DebuggerStepThrough] get => this.templateFlowID;
    set => this.templateFlowID = value;
  }

  /// <summary>Конструктор</summary>
  public FlowID()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="name">Имя потока</param>
  public FlowID(string name) => this.name = name;

  /// <summary>Записать поля в XML</summary>
  /// <param name="elementName">Имя элемента XML, под которым нужно сохранить данные</param>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  public void WriteToXml(string elementName, XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    xw.WriteStartElement(elementName);
    bool firstTime;
    xw.WriteAttributeString("refId", objectRefId.GetId((object) this, out firstTime).ToString((IFormatProvider) CultureInfo.InvariantCulture));
    if (this.name != null)
      xw.WriteAttributeString("name", this.name);
    if (this.templateFlowID != null)
    {
      long id = objectRefId.GetId((object) this.templateFlowID, out firstTime);
      xw.WriteAttributeString("template", id.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    }
    xw.WriteEndElement();
  }

  /// <summary>Загрузить из XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  public void ReadFromXml(XmlReadArgs readArgs)
  {
    WriteReadXmlHelper.ReadFromXml((IWriteReadXml) this, readArgs);
  }

  /// <summary>Прочитать одно поле из XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns>Возвращает true, если поле прочитано</returns>
  public bool ReadFieldFromXml(XmlReadArgs readArgs)
  {
    switch (readArgs.Reader.LocalName)
    {
      case "refId":
        readArgs.ObjectsId.Add((object) readArgs.Reader.Value, (object) this);
        if (this.name != null && this.name != "")
          this.name = readArgs.Reader.Value;
        return true;
      case "name":
        this.name = readArgs.Reader.Value;
        return true;
      case "template":
        string str = readArgs.Reader.Value;
        if (str != null && str != "")
        {
          this.templateFlowID = readArgs.ObjectsId[(object) str] as FlowID;
          if (this.templateFlowID == null)
            DocumentTreeNode.AddObjectReference((object) this, readArgs.ObjectReferences, "templateFlowID", str);
        }
        return true;
      default:
        return false;
    }
  }

  /// <summary>Клонировать экземляр</summary>
  /// <returns>Копия</returns>
  public FlowID Clone()
  {
    return new FlowID()
    {
      name = this.name,
      templateFlowID = this.templateFlowID
    };
  }

  object ICloneable.Clone() => (object) this.Clone();
}
