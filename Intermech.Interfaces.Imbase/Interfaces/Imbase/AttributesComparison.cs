// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.AttributesComparison
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Imbase;

/// <summary>Сопоставление атрибутов</summary>
public sealed class AttributesComparison
{
  /// <summary>Глобальный идентификатор в базе-источнике</summary>
  public Guid SourceGuid { get; set; }

  /// <summary>Наименование в базе-источнике</summary>
  public string SourceName { get; set; }

  /// <summary>Глобальный идентификатор в текущей базе</summary>
  public Guid DestinationGuid { get; set; }

  /// <summary>Новое сопоставление</summary>
  /// <param name="sourceGuid">Глобальный идентификатор в базе-источнике</param>
  /// <param name="sourceName">Наименование в базе-источнике</param>
  /// <param name="destinationGuid">Глобальный идентификатор в текущей базе</param>
  public AttributesComparison(Guid sourceGuid, string sourceName, Guid destinationGuid)
  {
    this.SourceGuid = sourceGuid;
    this.SourceName = sourceName;
    this.DestinationGuid = destinationGuid;
  }

  /// <summary>Читаем из базы</summary>
  /// <param name="source"></param>
  public AttributesComparison(string source)
  {
    string[] strArray = !string.IsNullOrEmpty(source) ? source.Split(';') : throw new ArgumentOutOfRangeException(nameof (source));
    this.SourceGuid = new Guid(strArray[0]);
    this.SourceName = strArray[1];
    this.DestinationGuid = new Guid(strArray[2]);
  }

  /// <summary>Сопоставление ввиде строки, хранящейся в базе</summary>
  /// <returns></returns>
  public string ToBase() => $"{this.SourceGuid};{this.SourceName};{this.DestinationGuid}";

  /// <summary>Строка отображающая сопоставление</summary>
  /// <returns></returns>
  public override string ToString()
  {
    return $"{(!string.IsNullOrEmpty(this.SourceName) ? (object) this.SourceName : (object) this.SourceGuid.ToString())} = {MetaDataHelper.GetAttributeTypeName(this.DestinationGuid)}";
  }
}
