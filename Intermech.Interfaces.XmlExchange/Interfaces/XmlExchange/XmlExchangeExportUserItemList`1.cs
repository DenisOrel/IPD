// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.XmlExchange.XmlExchangeExportUserItemList`1
// Assembly: Intermech.Interfaces.XmlExchange, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 28E8BDE9-A52D-45A9-B86E-D22E5A0BD9E6
// Assembly location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.xml

using System;
using System.Linq;

#nullable disable
namespace Intermech.Interfaces.XmlExchange;

/// <summary>Список базовых классов настроек</summary>
[Serializable]
public abstract class XmlExchangeExportUserItemList<T> : XmlExchangeExportList<T> where T : XmlExchangeExportUserItem, new()
{
  /// <summary>Поиск элемента по локальному идентификатору</summary>
  /// <returns></returns>
  public T GetItemByUserID(int userId)
  {
    return this.FirstOrDefault<T>((Func<T, bool>) (item => (object) item != null && item.UserID2Int == userId));
  }
}
