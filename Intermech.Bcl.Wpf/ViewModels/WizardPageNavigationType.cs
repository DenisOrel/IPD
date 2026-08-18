
// Type: Intermech.UI.Wpf.ViewModels.WizardPageNavigationType
// Assembly: Intermech.Bcl.Wpf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 91600B17-2177-4703-BAB9-56FCFFBCBBA2
:\IPS\Client\Intermech.Bcl.Wpf.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.Wpf.xml


namespace Intermech.UI.Wpf.ViewModels;

/// <summary>Тип перехода между страницами мастера.</summary>
public enum WizardPageNavigationType
{
  /// <summary>Переход по кнопке Назад</summary>
  Backward,
  /// <summary>Переход по кнопке Вперед/Готово</summary>
  Forward,
  /// <summary>Завершение работы мастера</summary>
  Finish,
  /// <summary>Прерывание работы мастера</summary>
  Cancel,
}
