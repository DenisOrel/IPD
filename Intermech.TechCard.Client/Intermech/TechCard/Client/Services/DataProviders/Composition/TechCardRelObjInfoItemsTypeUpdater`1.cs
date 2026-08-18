// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Services.DataProviders.Composition.TechCardRelObjInfoItemsTypeUpdater`1
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Compositions.CompositionService;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.TechCard.Client.Services.DataProviders.Composition;

/// <summary>
/// Провайдер для обновления "недостающих" данных элементов для RelObjInfoItem
/// </summary>
/// <summary>
/// 
/// </summary>
/// <param name="sourceProvider"></param>
internal class TechCardRelObjInfoItemsTypeUpdater<T>(
  [NotNull] ITechCardDataEnumerableProvider<T> sourceProvider,
  bool needCacheData = true) : TechCardDataEnumerableWithActionProvider<T>(sourceProvider, new Action<ICollection<T>>(TechCardRelObjInfoItemsTypeUpdater<T>.UpdateUnknownData), needCacheData)
  where T : RelObjInfoItem
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  private static void UpdateUnknownData(ICollection<T> items)
  {
    if (!items.Any<T>())
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      RelInfoHelper<T>.UpdateUnknownTypes((IEnumerable<T>) items, sessionKeeper.Session);
  }
}
