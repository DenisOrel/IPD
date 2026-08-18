// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Data.IImbaseAttributeLocatorData
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

#nullable disable
namespace Intermech.Tools.Data;

/// <summary>
/// Позволяет реализовать декодер исходных данных для алгоритма поиска изделия по атрибуту записи Imbase.
/// </summary>
public interface IImbaseAttributeLocatorData
{
  /// <summary>
  /// Возвращает идентификатор типа объекта, создаваемого по записи Imbase.
  /// </summary>
  int ObjectTypeId { get; }

  /// <summary>Возвращает идентификатор атрибута записи Imbase.</summary>
  int ImbaseAttributeId { get; }

  /// <summary>Возвращает значение атрибута записи Imbase.</summary>
  string ImbaseAttributeValue { get; }
}
