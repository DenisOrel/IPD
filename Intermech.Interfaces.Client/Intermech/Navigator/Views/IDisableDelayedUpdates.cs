// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Views.IDisableDelayedUpdates
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Navigator.Views;

/// <summary>
/// Наличие этого интерфейса в сервисах обязывает закладки выполнять немедленное обновление своих контролов
/// (проблема проявляется при восстановлении вложенных закладок и их выделенных строк - отложенное обновление
/// генерирует перечитывание списка строк и сбрасывает восстановленные выделенные строки)
/// </summary>
public interface IDisableDelayedUpdates
{
  /// <summary>
  /// Если true, то отложенное обновление закладок запрещено
  /// </summary>
  bool Disabled { get; set; }
}
