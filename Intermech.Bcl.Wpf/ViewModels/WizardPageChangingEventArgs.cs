
// Type: Intermech.UI.Wpf.ViewModels.WizardPageChangingEventArgs
// Assembly: Intermech.Bcl.Wpf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 91600B17-2177-4703-BAB9-56FCFFBCBBA2
:\IPS\Client\Intermech.Bcl.Wpf.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.Wpf.xml

using System;


namespace Intermech.UI.Wpf.ViewModels;

/// <summary>
/// Аргументы события перехода с одной страницы мастер на другую.
/// </summary>
public class WizardPageChangingEventArgs : EventArgs
{
  /// <summary>Создает объект.</summary>
  /// <param name="previousPage">Предыдущая страница мастера. Параметр может быть равен null</param>
  /// <param name="nextPage">Следующая страница мастера. Параметр может быть равен null</param>
  public WizardPageChangingEventArgs(WizardPageVM previousPage = null, WizardPageVM nextPage = null)
  {
    this.NextPage = nextPage;
    this.PreviousPage = previousPage;
  }

  /// <summary>
  /// Предыдущая страница мастера. Значение свойства может быть равно null.
  /// </summary>
  public WizardPageVM PreviousPage { get; }

  /// <summary>
  /// Следующая страница мастера. Значение свойства может быть равно null.
  /// </summary>
  public WizardPageVM NextPage { get; }
}
