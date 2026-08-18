
// Type: Intermech.UI.Wpf.Controls.TextBlockExtender
// Assembly: Intermech.Bcl.Wpf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 91600B17-2177-4703-BAB9-56FCFFBCBBA2
:\IPS\Client\Intermech.Bcl.Wpf.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.Wpf.xml

using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;


namespace Intermech.UI.Wpf.Controls;

public static class TextBlockExtender
{
  public static readonly DependencyProperty InlinesProperty = DependencyProperty.RegisterAttached("Inlines", typeof (IEnumerable<Inline>), typeof (TextBlockExtender), new PropertyMetadata((object) null, new PropertyChangedCallback(TextBlockExtender.OnBindableInlinesChanged)));

  public static IEnumerable<Inline> GetInlines(DependencyObject obj)
  {
    return (IEnumerable<Inline>) obj.GetValue(TextBlockExtender.InlinesProperty);
  }

  public static void SetInlines(DependencyObject obj, IEnumerable<Inline> value)
  {
    obj.SetValue(TextBlockExtender.InlinesProperty, (object) value);
  }

  private static void OnBindableInlinesChanged(
    DependencyObject obj,
    DependencyPropertyChangedEventArgs e)
  {
    if (!(obj is TextBlock textBlock))
      return;
    textBlock.Inlines.Clear();
    textBlock.Inlines.AddRange((IEnumerable) e.NewValue);
  }
}
