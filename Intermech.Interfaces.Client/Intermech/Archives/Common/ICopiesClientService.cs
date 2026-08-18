// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.Common.ICopiesClientService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Archives.Common;

/// <summary>
/// Клиентский сервис для работы с копиями и листами рассылки
/// </summary>
public interface ICopiesClientService
{
  /// <summary>Копировать лист рассылки.</summary>
  /// <param name="copiedDeliveryListID">ИД копируемого листа рассылки</param>
  /// <param name="docsDeliveryLists">Список листов рассылки, в которые будут скопированы абоненты</param>
  void CopyDeliveryList(long copiedDeliveryListID, List<long> docsDeliveryLists);
}
