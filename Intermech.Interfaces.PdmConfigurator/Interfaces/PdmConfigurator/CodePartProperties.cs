// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.PdmConfigurator.CodePartProperties
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

using System;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.PdmConfigurator;

/// <summary>Класс для описания чаcти шифра изделия</summary>
[Serializable]
public class CodePartProperties : IXMLStorageLoadSave, IAssignable
{
  /// <summary>тип части шифра</summary>
  public CodePartType codePartType = CodePartType.Undefined;
  /// <summary>
  /// значение части шифра
  /// CodePartType.FixedText - строка;
  /// CodePartType.ObjectAttribute - int32;
  /// CodePartType.OptionCode, CodePartType.OptionValueCode - int64;
  /// </summary>
  public object codePartValue = (object) string.Empty;

  /// <summary>проверить, пустой ли элемент</summary>
  public bool Empty => this.codePartType == CodePartType.Undefined && this.codePartValue == null;

  /// <summary>создать описание части шифра</summary>
  /// <param name="type"> тип части шифра</param>
  /// <param name="value"> значение части шифра</param>
  public CodePartProperties(CodePartType type, object value)
  {
    this.codePartType = type;
    this.codePartValue = value;
  }

  /// <summary>создать описание части шифра</summary>
  public CodePartProperties()
  {
  }

  /// <summary>Загрузить данные из указанного узла настроек</summary>
  /// <param name="xmlStorage">Хранилище настроек</param>
  /// <param name="node">Узел с данными</param>
  public void Load(XMLSettingsStorage xmlStorage, XmlNode node)
  {
    this.Clear();
    if (node == null || node.Name != "j")
      return;
    string attributeValue = xmlStorage.GetAttributeValue(node, "f", "");
    if (string.IsNullOrEmpty(attributeValue) || attributeValue.Length < 2 || attributeValue.IndexOf(":") <= 0)
      return;
    this.codePartType = (CodePartType) Convert.ToInt32(attributeValue.Substring(0, attributeValue.IndexOf(":")));
    this.codePartValue = (object) attributeValue.Substring(attributeValue.IndexOf(":") + 1);
  }

  /// <summary>
  /// Сохранить данные в состав указанного родительского узла
  /// </summary>
  /// <param name="xmlStorage">Хранилище настроек</param>
  /// <param name="parentNode">Родительский узел или null (тогда сохранение можно выполнять в корневой узел)</param>
  public void Save(XMLSettingsStorage xmlStorage, XmlNode parentNode)
  {
    if (this.Empty)
      return;
    XmlNode node = xmlStorage.AddNode(parentNode, "j");
    xmlStorage.SetAttributeValue(node, "f", $"{(object) (int) this.codePartType}:{this.codePartValue}");
  }

  /// <summary>Очистить поля класса</summary>
  public void Clear()
  {
    this.codePartType = CodePartType.Undefined;
    this.codePartValue = (object) string.Empty;
  }

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник</param>
  public void Assign(object source)
  {
    if (this == source)
      return;
    this.Clear();
    if (!(source is CodePartProperties codePartProperties))
      return;
    this.codePartType = codePartProperties.codePartType;
    this.codePartValue = codePartProperties.codePartValue;
  }
}
