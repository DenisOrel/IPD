// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Services.DataProviders.ITechCardDataProvider`1
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

#nullable disable
namespace Intermech.TechCard.Client.Services.DataProviders;

/// <summary>Интерфейс для получения данных</summary>
public interface ITechCardDataProvider<out T>
{
  /// <summary>Получение данных</summary>
  /// <returns></returns>
  T Execute();
}
