// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Views.DisableDelayedUpdates
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Navigator.Views;

/// <summary>
/// Наличие этого класса в сервисах обязывает закладки выполнять немедленное обновление своих контролов
/// (проблема проявляется при восстановлении вложенных закладок и их выделенных строк - отложенное обновление
/// генерирует перечитывание списка строк и сбрасывает восстановленные выделенные строки)
/// </summary>
public class DisableDelayedUpdates : IDisableDelayedUpdates
{
  /// <summary>
  /// Если true, то отложенное обновление закладок запрещено
  /// </summary>
  protected bool _disabled;

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="disabled">Если true, то отложенное обновление закладок запрещено</param>
  public DisableDelayedUpdates(bool disabled) => this._disabled = disabled;

  /// <summary>
  /// Если true, то отложенное обновление закладок запрещено
  /// </summary>
  public bool Disabled
  {
    get => this._disabled;
    set => this._disabled = value;
  }
}
