
// Type: Intermech.UI.Wpf.Markup.InDesignModeExtension
// Assembly: Intermech.Bcl.Wpf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 91600B17-2177-4703-BAB9-56FCFFBCBBA2
:\IPS\Client\Intermech.Bcl.Wpf.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.Wpf.xml

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Markup;
using System.Xaml;


namespace Intermech.UI.Wpf.Markup;

/// <summary>
/// Расширение разметки XAML, позволяющее эвристически определять DesignMode.
/// </summary>
[MarkupExtensionReturnType(typeof (bool))]
public class InDesignModeExtension : MarkupExtension
{
  /// <summary>
  /// Эвристически определяет DesignMode и возвращает значение типа <see cref="T:System.Boolean" />.
  /// </summary>
  /// <param name="serviceProvider">Провайдер сервисов</param>
  /// <returns>Результат работы расширения</returns>
  public override object ProvideValue(IServiceProvider serviceProvider)
  {
    IRootObjectProvider service = (IRootObjectProvider) serviceProvider.GetService(typeof (IRootObjectProvider));
    return service != null ? (object) (bool) (!(service.RootObject is DependencyObject rootObject) ? 0 : (DesignerProperties.GetIsInDesignMode(rootObject) ? 1 : 0)) : (object) true;
  }
}
