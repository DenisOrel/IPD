
// Type: Intermech.UI.Wpf.ViewModels.WizardPageNavigationEventArgs
// Assembly: Intermech.Bcl.Wpf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 91600B17-2177-4703-BAB9-56FCFFBCBBA2
:\IPS\Client\Intermech.Bcl.Wpf.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.Wpf.xml

using System;


namespace Intermech.UI.Wpf.ViewModels;

/// <summary>Аргументы события перехода между страницами мастера.</summary>
public class WizardPageNavigationEventArgs : EventArgs
{
  /// <summary>Создает объект.</summary>
  /// <param name="navigationType">Тип перехода между страницами мастера</param>
  public WizardPageNavigationEventArgs(WizardPageNavigationType navigationType)
  {
    this.NavigationType = navigationType;
  }

  /// <summary>Возвращает тип перехода между страницами мастера.</summary>
  public WizardPageNavigationType NavigationType { get; }

  /// <summary>
  /// Возвращает или задает признак, что операция перехода должна быть прервана.
  /// </summary>
  public bool Cancel { get; set; }
}
