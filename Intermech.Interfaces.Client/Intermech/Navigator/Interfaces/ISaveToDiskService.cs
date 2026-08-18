// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.ISaveToDiskService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Интерфейс управления страницами дополнительных настроек в окно команды "Сохранить на диск"
/// </summary>
public interface ISaveToDiskService
{
  /// <summary>
  /// Зарегистрировать провайдер страницы дополнительных настроек
  /// </summary>
  /// <param name="provider"></param>
  void RegisterProvider(ISaveToDiskPageProvider provider);

  /// <summary>
  /// Разрегистрировать провайдер страницы дополнительных настроек
  /// </summary>
  /// <param name="provider"></param>
  void UnregisterProvider(ISaveToDiskPageProvider provider);

  /// <summary>
  /// Список зарегистрированных в клиенте провайдеров страниц дополнительных настроек
  /// </summary>
  ISaveToDiskPageProvider[] Providers { get; }
}
