// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.IUnknownXmlElement
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Интерфейс для классов поддерживающих загрузку неизвестных типов из XML</summary>
public interface IUnknownXmlElement
{
  /// <summary>XML атрибуты, не распознанные при загрузке</summary>
  List<StringKeyValue> UnknownXmlAttributes { get; set; }

  /// <summary>XML элементы, не распознанные при загрузке</summary>
  string UnknownXmlElements { get; set; }

  /// <summary>Добавить неизвесный атрибут</summary>
  /// <param name="key">Имя атрибута</param>
  /// <param name="value">Значение атрибута</param>
  void AddUnknownXmlAttribute(string key, string value);
}
