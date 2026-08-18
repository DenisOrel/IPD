// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Services.DataProviders.TechCardDataEnumerableSimpleProvider`1
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Diagnostics;
using System.Collections.Generic;

#nullable disable
namespace Intermech.TechCard.Client.Services.DataProviders;

/// <summary>Провайдер данных элементов для RelObjInfoItem</summary>
internal class TechCardDataEnumerableSimpleProvider<T> : 
  ITechCardDataEnumerableProvider<T>,
  ITechCardDataProvider<IEnumerable<T>>
{
  /// <summary>
  /// 
  /// </summary>
  private readonly IEnumerable<T> _items;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  public TechCardDataEnumerableSimpleProvider([NotNull] IEnumerable<T> items)
  {
    this._items = items;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public IEnumerable<T> Execute() => this._items;
}
