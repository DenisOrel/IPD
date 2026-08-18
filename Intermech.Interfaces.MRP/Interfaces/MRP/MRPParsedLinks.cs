// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.MRPParsedLinks
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>
/// Класс, ссылающийся на список обработанных существующих связей
/// </summary>
public class MRPParsedLinks
{
  /// <summary>Словарь обработанных связей</summary>
  private Dictionary<long, bool> items = new Dictionary<long, bool>();

  /// <summary>Добавить идентификатор связи в контейнер</summary>
  /// <param name="linkID">Идентификатор связи</param>
  public void Add(long linkID)
  {
    if (linkID == 0L)
      throw new ArgumentException();
    lock (this.items)
      this.items[Math.Abs(linkID)] = true;
  }

  /// <summary>Проверить наличие идентификатора связи в контейнере</summary>
  /// <param name="linkID">Искомая связь (знак не имеет значения)</param>
  /// <returns>true - связь найдена в контейнере</returns>
  public bool Exists(long linkID)
  {
    lock (this.items)
      return this.items.ContainsKey(Math.Abs(linkID));
  }
}
