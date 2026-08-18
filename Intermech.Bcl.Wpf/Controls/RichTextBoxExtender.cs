
// Type: Intermech.UI.Wpf.Controls.RichTextBoxExtender
// Assembly: Intermech.Bcl.Wpf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 91600B17-2177-4703-BAB9-56FCFFBCBBA2
:\IPS\Client\Intermech.Bcl.Wpf.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.Wpf.xml

using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;


namespace Intermech.UI.Wpf.Controls;

public static class RichTextBoxExtender
{
  public static readonly DependencyProperty DocumentProperty = DependencyProperty.RegisterAttached("Document", typeof (FlowDocument), typeof (RichTextBoxExtender), new PropertyMetadata((object) null, new PropertyChangedCallback(RichTextBoxExtender.OnBindableDocumentChanged)));

  public static FlowDocument GetDocument(DependencyObject obj)
  {
    return (FlowDocument) obj.GetValue(RichTextBoxExtender.DocumentProperty);
  }

  public static void SetDocument(DependencyObject obj, FlowDocument value)
  {
    obj.SetValue(RichTextBoxExtender.DocumentProperty, (object) value);
  }

  private static void OnBindableDocumentChanged(
    DependencyObject obj,
    DependencyPropertyChangedEventArgs e)
  {
    if (!(obj is RichTextBox richTextBox))
      return;
    richTextBox.Document = (FlowDocument) e.NewValue;
  }
}
