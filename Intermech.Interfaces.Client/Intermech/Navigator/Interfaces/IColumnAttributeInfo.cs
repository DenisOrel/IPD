// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.IColumnAttributeInfo
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces;
using Intermech.Kernel.Search;

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Интерфейс с информацией о типе атрибута, для которого сформирована колонка
/// </summary>
public interface IColumnAttributeInfo
{
  /// <summary>Информация о типе атрибута</summary>
  IMSAttributeType Attribute { get; }

  /// <summary>
  /// Информация об источнике атрибута. Допустимы только значения Object и Relation.
  /// </summary>
  AttributeSourceTypes AttrSource { get; }
}
