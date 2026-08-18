
// Type: Intermech.UI.Wpf.WinformsInterop.WinformsInteropInitializer
// Assembly: Intermech.Bcl.Wpf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 91600B17-2177-4703-BAB9-56FCFFBCBBA2
:\IPS\Client\Intermech.Bcl.Wpf.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.Wpf.xml

using Intermech.Diagnostics;
using Intermech.Runtime;
using Intermech.Threading;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Markup;


namespace Intermech.UI.Wpf.WinformsInterop;

/// <summary>
/// Ленивый инициализатор поддержки WPF внутри WinForms.
/// Реализация является thread safe
/// </summary>
internal sealed class WinformsInteropInitializer
{
  private AtomicBoolean isInitialized;

  /// <summary>Создает объект.</summary>
  public WinformsInteropInitializer() => this.isInitialized = new AtomicBoolean();

  /// <summary>
  /// Возвращает признак, что инициализация уже была выполнена.
  /// </summary>
  public bool IsInitialized
  {
    [DebuggerStepThrough] get => this.isInitialized.Value;
  }

  /// <summary>Выполняет инициализацию WPF внутри WinForms.</summary>
  public void Initialize()
  {
    if (!this.isInitialized.TryModify(false, true))
      return;
    this.ApplyCurrentUICultureSilently();
  }

  private void ApplyCurrentUICultureSilently()
  {
    try
    {
      FrameworkElement.LanguageProperty.OverrideMetadata(typeof (FrameworkElement), (PropertyMetadata) new FrameworkPropertyMetadata((object) XmlLanguage.GetLanguage(CultureInfo.CurrentUICulture.Name), FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.Inherits));
    }
    catch (Exception ex)
    {
      string currentMethodName = this.GetCurrentMethodName(nameof (ApplyCurrentUICultureSilently));
      SuppressedExceptions.TraceException(ex, currentMethodName);
    }
  }

  /// <summary>Возвращает экземпляр по умолчанию</summary>
  public static WinformsInteropInitializer Instance { get; } = new WinformsInteropInitializer();
}
