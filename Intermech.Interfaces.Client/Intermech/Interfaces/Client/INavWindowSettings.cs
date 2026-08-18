// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.INavWindowSettings
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Интерфейс позволяет хранить различные настройки для элементов управления,
/// находящихся внутри окон "Навигатора"
/// </summary>
public interface INavWindowSettings
{
  /// <summary>Считать или установить настройки с указанным ключом</summary>
  /// <param name="key">Ключ настроек</param>
  /// <returns>Настройки с указанным ключом</returns>
  object this[object key] { get; set; }

  /// <summary>Удалить все настройки</summary>
  void Reset();
}
