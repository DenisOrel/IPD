
// Type: Intermech.UI.Wpf.Controls.TextBoxAdapter
// Assembly: Intermech.Bcl.Wpf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 91600B17-2177-4703-BAB9-56FCFFBCBBA2
:\IPS\Client\Intermech.Bcl.Wpf.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.Wpf.xml

using System;
using System.Windows.Controls;


namespace Intermech.UI.Wpf.Controls;

/// <summary>
/// Адаптер для WPF TextBox, используемый для интеграции с <see cref="T:Intermech.UI.Wpf.Controls.FindReplaceManager" />
/// </summary>
public class TextBoxAdapter : FindReplaceTextEditorAdapter, IFindReplaceTextEditor
{
  private TextBox editorControl;

  /// <summary>Создает объект.</summary>
  /// <param name="editorControl">Элемент WPF TextBox</param>
  /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="editorControl" /> содержит null</exception>
  public TextBoxAdapter(TextBox editorControl)
  {
    this.editorControl = editorControl != null ? editorControl : throw new ArgumentNullException(nameof (editorControl));
  }

  public string Text => this.editorControl.Text;

  public int SelectionStart => this.editorControl.SelectionStart;

  public int SelectionLength => this.editorControl.SelectionLength;

  public void BeginChange() => this.editorControl.BeginChange();

  public void EndChange() => this.editorControl.EndChange();

  public void Select(int start, int length) => this.editorControl.Select(start, length);

  public void Replace(int start, int length, string ReplaceWith)
  {
    this.editorControl.Text = this.editorControl.Text.Substring(0, start) + ReplaceWith + this.editorControl.Text.Substring(start + length);
  }
}
