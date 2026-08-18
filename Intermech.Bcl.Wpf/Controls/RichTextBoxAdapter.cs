
// Type: Intermech.UI.Wpf.Controls.RichTextBoxAdapter
// Assembly: Intermech.Bcl.Wpf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 91600B17-2177-4703-BAB9-56FCFFBCBBA2
:\IPS\Client\Intermech.Bcl.Wpf.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.Wpf.xml

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;


namespace Intermech.UI.Wpf.Controls;

/// <summary>
/// Адаптер для WPF RichTextBox, используемый для интеграции с <see cref="T:Intermech.UI.Wpf.Controls.FindReplaceManager" />
/// </summary>
public class RichTextBoxAdapter : FindReplaceTextEditorAdapter, IFindReplaceTextEditor
{
  private RichTextBox editorControl;
  private TextRange oldsel;

  /// <summary>Создает объект.</summary>
  /// <param name="editorControl">Элемент WPF RichTextBox</param>
  /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="editorControl" /> содержит null</exception>
  public RichTextBoxAdapter(RichTextBox editorControl)
  {
    this.editorControl = editorControl != null ? editorControl : throw new ArgumentNullException(nameof (editorControl));
  }

  public string Text
  {
    get
    {
      return new TextRange(this.editorControl.Document.ContentStart, this.editorControl.Document.ContentEnd).Text;
    }
  }

  public int SelectionStart
  {
    get
    {
      return this.GetPos(this.editorControl.Document.ContentStart, this.editorControl.Selection.Start);
    }
  }

  public int SelectionLength => this.editorControl.Selection.Text.Length;

  public void BeginChange() => this.editorControl.BeginChange();

  public void EndChange() => this.editorControl.EndChange();

  public void Select(int start, int length)
  {
    TextPointer contentStart = this.editorControl.Document.ContentStart;
    this.editorControl.Selection.Select(this.GetPoint(contentStart, start), this.GetPoint(contentStart, start + length));
    this.editorControl.ScrollToVerticalOffset(this.editorControl.Selection.Start.GetCharacterRect(LogicalDirection.Forward).Top);
    this.editorControl.Selection.ApplyPropertyValue(TextElement.BackgroundProperty, (object) Brushes.Yellow);
    this.oldsel = new TextRange(this.editorControl.Selection.Start, this.editorControl.Selection.End);
    this.editorControl.SelectionChanged += new RoutedEventHandler(this.rtb_SelectionChanged);
  }

  private void rtb_SelectionChanged(object sender, RoutedEventArgs e)
  {
    this.oldsel.ApplyPropertyValue(TextElement.BackgroundProperty, (object) null);
    this.editorControl.SelectionChanged -= new RoutedEventHandler(this.rtb_SelectionChanged);
  }

  public void Replace(int start, int length, string ReplaceWith)
  {
    TextPointer contentStart = this.editorControl.Document.ContentStart;
    new TextRange(this.GetPoint(contentStart, start), this.GetPoint(contentStart, start + length)).Text = ReplaceWith;
  }
}
