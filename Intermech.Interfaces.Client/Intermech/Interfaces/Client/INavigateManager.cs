// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.INavigateManager
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Интерфейс для связи INavigate скнопками управления на тоолбаре
/// </summary>
public interface INavigateManager
{
  /// <summary>Подключает интерфейс к кнопкам управления</summary>
  /// <param name="navigate">Подключаемый интерфейс навигации</param>
  void Attach(INavigate navigate);
}
