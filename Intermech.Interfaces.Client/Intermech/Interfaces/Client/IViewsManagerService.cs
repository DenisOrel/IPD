// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IViewsManagerService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Интерфейс службы, позволяющей влиять на работу менеджеров закладок "Навигатора"
/// </summary>
public interface IViewsManagerService
{
  /// <summary>
  /// Событие, вызываемое после того, как перестроятся закладки
  /// </summary>
  event ActivateViewEventHandler OnActivateView;

  /// <summary>
  /// Сгенерировать событие (разослать уведомление всем подписчикам)
  /// </summary>
  /// <param name="sender">Отправитель события (менеджер закладок)</param>
  /// <param name="e">Аргументы события</param>
  void FireActivateViewEvent(object sender, ActivateViewEventArgs e);
}
