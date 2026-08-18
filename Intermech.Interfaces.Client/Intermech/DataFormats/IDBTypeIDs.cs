// Decompiled with JetBrains decompiler
// Type: Intermech.DataFormats.IDBTypeIDs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.DataFormats;

/// <summary>
/// Интерфейс-формат для передачи данных об идентификаторах типов объектов и связей
/// базы данных через clipboard, а также между различными частями универсального клиента.
/// </summary>
public interface IDBTypeIDs
{
  /// <summary>Список идентификаторов типов объектов</summary>
  int[] ObjectTypeIDs { get; }

  /// <summary>Список идентификаторов типов связей</summary>
  int[] RelationTypeIDs { get; }
}
