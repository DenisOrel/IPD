// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.TechCard.ITechNumerationSuppressList
// Assembly: Intermech.Interfaces.TechCard, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B0F892EF-B72A-4A7D-8F43-9EB461AAC859
// Assembly location: D:\IPS\Client\Intermech.Interfaces.TechCard.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.TechCard.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.TechCard;

/// <summary>Интерфейс для запрета нумерации элементов</summary>
public interface ITechNumerationSuppressList
{
  /// <summary>Add item to list</summary>
  /// <param name="itemId"></param>
  void AddItem(long itemId);

  /// <summary>Add items to list</summary>
  /// <param name="itemIDs"></param>
  void AddItems(IEnumerable<long> itemIDs);

  /// <summary>Remove item from list</summary>
  /// <param name="itemId"></param>
  void RemoveItem(long itemId);

  /// <summary>Remove items from list</summary>
  /// <param name="itemIDs"></param>
  void RemoveItems(IEnumerable<long> itemIDs);

  /// <summary>Check item in list</summary>
  /// <param name="itemId"></param>
  /// <returns></returns>
  bool ContainItem(long itemId);

  /// <summary>Clear list</summary>
  void Clear();
}
