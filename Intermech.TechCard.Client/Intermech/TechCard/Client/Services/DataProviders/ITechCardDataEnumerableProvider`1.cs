// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Services.DataProviders.ITechCardDataEnumerableProvider`1
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.TechCard.Client.Services.DataProviders;

/// <summary>Интерфейс для получения IEnumerable</summary>
/// <typeparam name="T"></typeparam>
public interface ITechCardDataEnumerableProvider<out T> : ITechCardDataProvider<IEnumerable<T>>
{
}
