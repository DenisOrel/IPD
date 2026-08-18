
// Type: Intermech.UI.Wpf.ViewModels.WizardRunState
// Assembly: Intermech.Bcl.Wpf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 91600B17-2177-4703-BAB9-56FCFFBCBBA2
:\IPS\Client\Intermech.Bcl.Wpf.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.Wpf.xml


namespace Intermech.UI.Wpf.ViewModels;

/// <summary>Текущее состояние мастера</summary>
public enum WizardRunState
{
  /// <summary>Мастер не запущен</summary>
  NotStarted,
  /// <summary>Мастер запущен и выполняется</summary>
  Started,
  /// <summary>Выполение мастера успешно завершено</summary>
  Completed,
  /// <summary>Выполнение мастера прервано</summary>
  Cancelled,
}
