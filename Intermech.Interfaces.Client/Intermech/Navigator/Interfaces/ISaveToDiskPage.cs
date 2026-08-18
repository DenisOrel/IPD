// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.ISaveToDiskPage
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Windows.Forms;

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Интерфейс управления страницей дополнительных настроек окна команды "Сохранить на диск"
/// </summary>
public interface ISaveToDiskPage
{
  /// <summary>Имя страницы, напр "CertSheetOptions"</summary>
  string PageName { get; }

  /// <summary>Индекс страницы; &gt; 0</summary>
  int Index { get; }

  /// <summary>Название страницы дополнительных настроек</summary>
  string Caption { get; }

  /// <summary>Визуальный компонент страницы дополнительных настроек</summary>
  UserControl Control { get; }

  /// <summary>
  /// Разрешает сохранение дополнительных настроек данной страницы
  /// </summary>
  bool CommitEnabled { get; }

  /// <summary>
  /// Фиксация настроек для последующегго сохранения, возможно в другом потоке.
  /// </summary>
  ISaveToDiskProcessor Commit();

  /// <summary>Отмена сохранения настроек</summary>
  void Cancel();
}
