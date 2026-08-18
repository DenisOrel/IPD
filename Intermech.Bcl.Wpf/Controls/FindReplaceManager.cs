
// Type: Intermech.UI.Wpf.Controls.FindReplaceManager
// Assembly: Intermech.Bcl.Wpf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 91600B17-2177-4703-BAB9-56FCFFBCBBA2
:\IPS\Client\Intermech.Bcl.Wpf.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.Wpf.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;


namespace Intermech.UI.Wpf.Controls;

/// <summary>
/// This class ensures that the settings and text to be found is preserved when the find/replace dialog is closed
/// We need two-way binding, otherwise we could just make all properties static properties of the window
/// </summary>
public class FindReplaceManager : DependencyObject
{
  public static readonly DependencyProperty EditorsProperty = DependencyProperty.Register(nameof (Editors), typeof (IEnumerable), typeof (FindReplaceManager), new PropertyMetadata((PropertyChangedCallback) null));
  public static readonly DependencyProperty CurrentEditorProperty = DependencyProperty.Register(nameof (CurrentEditor), typeof (IFindReplaceTextEditor), typeof (FindReplaceManager), new PropertyMetadata((PropertyChangedCallback) null));
  public static readonly DependencyProperty TextToFindProperty = DependencyProperty.Register(nameof (TextToFind), typeof (string), typeof (FindReplaceManager), (PropertyMetadata) new UIPropertyMetadata((object) ""));
  public static readonly DependencyProperty ReplacementTextProperty = DependencyProperty.Register(nameof (ReplacementText), typeof (string), typeof (FindReplaceManager), (PropertyMetadata) new UIPropertyMetadata((object) ""));
  public static readonly DependencyProperty SearchUpProperty = DependencyProperty.Register(nameof (SearchUp), typeof (bool), typeof (FindReplaceManager), (PropertyMetadata) new UIPropertyMetadata((object) false));
  public static readonly DependencyProperty CaseSensitiveProperty = DependencyProperty.Register(nameof (CaseSensitive), typeof (bool), typeof (FindReplaceManager), (PropertyMetadata) new UIPropertyMetadata((object) false));
  public static readonly DependencyProperty UseRegExProperty = DependencyProperty.Register(nameof (UseRegEx), typeof (bool), typeof (FindReplaceManager), (PropertyMetadata) new UIPropertyMetadata((object) false));
  public static readonly DependencyProperty WholeWordProperty = DependencyProperty.Register(nameof (WholeWord), typeof (bool), typeof (FindReplaceManager), (PropertyMetadata) new UIPropertyMetadata((object) false));
  public static readonly DependencyProperty AcceptsReturnProperty = DependencyProperty.Register(nameof (AcceptsReturn), typeof (bool), typeof (FindReplaceManager), (PropertyMetadata) new UIPropertyMetadata((object) false));
  public static readonly DependencyProperty SearchInProperty = DependencyProperty.Register(nameof (SearchIn), typeof (FindReplaceSearchScope), typeof (FindReplaceManager), (PropertyMetadata) new UIPropertyMetadata((object) FindReplaceSearchScope.CurrentDocument));
  public static readonly DependencyProperty WindowLeftProperty = DependencyProperty.Register(nameof (WindowLeft), typeof (double), typeof (FindReplaceManager), (PropertyMetadata) new UIPropertyMetadata((object) double.NaN));
  public static readonly DependencyProperty WindowTopProperty = DependencyProperty.Register(nameof (WindowTop), typeof (double), typeof (FindReplaceManager), (PropertyMetadata) new UIPropertyMetadata((object) double.NaN));
  public static readonly DependencyProperty ShowSearchInProperty = DependencyProperty.Register(nameof (ShowSearchIn), typeof (bool), typeof (FindReplaceManager), (PropertyMetadata) new UIPropertyMetadata((object) false));
  public static readonly DependencyProperty AllowReplaceProperty = DependencyProperty.Register(nameof (AllowReplace), typeof (bool), typeof (FindReplaceManager), (PropertyMetadata) new UIPropertyMetadata((object) true));
  public static readonly DependencyProperty OwnerWindowProperty = DependencyProperty.Register(nameof (OwnerWindow), typeof (IFindReplaceTextEditorWindow), typeof (FindReplaceManager), (PropertyMetadata) new UIPropertyMetadata((PropertyChangedCallback) null));
  private FindReplaceDialog dialog;

  public FindReplaceManager()
  {
    this.ReplacementText = "";
    this.SearchIn = FindReplaceSearchScope.CurrentDocument;
  }

  /// <summary>Instance of the dialog window</summary>
  private FindReplaceDialog Dialog
  {
    get
    {
      if (this.dialog == null)
      {
        this.dialog = this.CreateDialog();
        this.dialog.Closed += (EventHandler) ((_param1, _param2) => this.dialog = (FindReplaceDialog) null);
      }
      return this.dialog;
    }
  }

  private bool IsDialogOpen => this.dialog != null && this.dialog.IsVisible;

  private FindReplaceDialog CreateDialog()
  {
    FindReplaceDialog findReplaceWindow = new FindReplaceDialog();
    findReplaceWindow.DataContext = (object) this;
    DpiScale dpi = VisualTreeHelper.GetDpi((Visual) findReplaceWindow);
    if (double.IsNaN(this.WindowLeft) || double.IsNaN(this.WindowTop))
    {
      double num = 200.0 * dpi.DpiScaleY;
      if (this.OwnerWindow != null)
      {
        this.WindowLeft = (double) this.OwnerWindow.Left + ((double) this.OwnerWindow.Width - findReplaceWindow.Width * dpi.DpiScaleX) / 2.0;
        this.WindowTop = (double) this.OwnerWindow.Top + ((double) this.OwnerWindow.Height - num) / 2.0;
      }
      else
      {
        System.Windows.Point point = new System.Windows.Point((double) SystemInformation.VirtualScreen.X, (double) SystemInformation.VirtualScreen.Y);
        System.Windows.Size size;
        ref System.Windows.Size local = ref size;
        Rectangle virtualScreen = SystemInformation.VirtualScreen;
        double width = (double) virtualScreen.Width;
        virtualScreen = SystemInformation.VirtualScreen;
        double height = (double) virtualScreen.Height;
        local = new System.Windows.Size(width, height);
        this.WindowLeft = point.X + (size.Width - findReplaceWindow.Width * dpi.DpiScaleX) / 2.0;
        this.WindowTop = point.Y + (size.Height - num) / 2.0;
      }
      this.WindowLeft /= dpi.DpiScaleX;
      this.WindowTop /= dpi.DpiScaleY;
    }
    if (this.OwnerWindow != null)
      this.OwnerWindow.SetOwnerWindow((Window) findReplaceWindow);
    return findReplaceWindow;
  }

  public CommandBinding FindBinding
  {
    get
    {
      return new CommandBinding((ICommand) ApplicationCommands.Find, (ExecutedRoutedEventHandler) ((s, e) => this.ShowAsFind()));
    }
  }

  public CommandBinding FindNextBinding
  {
    get
    {
      return new CommandBinding((ICommand) NavigationCommands.Search, (ExecutedRoutedEventHandler) ((s, e) => this.FindNext(e.Parameter != null)));
    }
  }

  public CommandBinding ReplaceBinding
  {
    get
    {
      return new CommandBinding((ICommand) ApplicationCommands.Replace, (ExecutedRoutedEventHandler) ((s, e) =>
      {
        if (!this.AllowReplace)
          return;
        this.ShowAsReplace();
      }));
    }
  }

  /// <summary>
  /// The list of editors in which the search should take place.
  /// The elements must either implement the IEditor interface, or
  /// InterfaceConverter should bne set.
  /// </summary>
  public IEnumerable Editors
  {
    get => (IEnumerable) this.GetValue(FindReplaceManager.EditorsProperty);
    set => this.SetValue(FindReplaceManager.EditorsProperty, (object) value);
  }

  /// <summary>
  /// The editor in which the current search operation takes place.
  /// </summary>
  public IFindReplaceTextEditor CurrentEditor
  {
    get => (IFindReplaceTextEditor) this.GetValue(FindReplaceManager.CurrentEditorProperty);
    set => this.SetValue(FindReplaceManager.CurrentEditorProperty, (object) value);
  }

  public string TextToFind
  {
    get => (string) this.GetValue(FindReplaceManager.TextToFindProperty);
    set => this.SetValue(FindReplaceManager.TextToFindProperty, (object) value);
  }

  public string ReplacementText
  {
    get => (string) this.GetValue(FindReplaceManager.ReplacementTextProperty);
    set => this.SetValue(FindReplaceManager.ReplacementTextProperty, (object) value);
  }

  public bool SearchUp
  {
    get => (bool) this.GetValue(FindReplaceManager.SearchUpProperty);
    set => this.SetValue(FindReplaceManager.SearchUpProperty, (object) value);
  }

  public bool CaseSensitive
  {
    get => (bool) this.GetValue(FindReplaceManager.CaseSensitiveProperty);
    set => this.SetValue(FindReplaceManager.CaseSensitiveProperty, (object) value);
  }

  public bool UseRegEx
  {
    get => (bool) this.GetValue(FindReplaceManager.UseRegExProperty);
    set => this.SetValue(FindReplaceManager.UseRegExProperty, (object) value);
  }

  public bool WholeWord
  {
    get => (bool) this.GetValue(FindReplaceManager.WholeWordProperty);
    set => this.SetValue(FindReplaceManager.WholeWordProperty, (object) value);
  }

  public bool AcceptsReturn
  {
    get => (bool) this.GetValue(FindReplaceManager.AcceptsReturnProperty);
    set => this.SetValue(FindReplaceManager.AcceptsReturnProperty, (object) value);
  }

  public FindReplaceSearchScope SearchIn
  {
    get => (FindReplaceSearchScope) this.GetValue(FindReplaceManager.SearchInProperty);
    set => this.SetValue(FindReplaceManager.SearchInProperty, (object) value);
  }

  public double WindowLeft
  {
    get => (double) this.GetValue(FindReplaceManager.WindowLeftProperty);
    set => this.SetValue(FindReplaceManager.WindowLeftProperty, (object) value);
  }

  public double WindowTop
  {
    get => (double) this.GetValue(FindReplaceManager.WindowTopProperty);
    set => this.SetValue(FindReplaceManager.WindowTopProperty, (object) value);
  }

  /// <summary>Determines whether to display the Search in combo box</summary>
  public bool ShowSearchIn
  {
    get => (bool) this.GetValue(FindReplaceManager.ShowSearchInProperty);
    set => this.SetValue(FindReplaceManager.ShowSearchInProperty, (object) value);
  }

  /// <summary>
  /// Determines whether the "Replace"-page in the dialog in shown or not.
  /// </summary>
  public bool AllowReplace
  {
    get => (bool) this.GetValue(FindReplaceManager.AllowReplaceProperty);
    set => this.SetValue(FindReplaceManager.AllowReplaceProperty, (object) value);
  }

  /// <summary>
  /// The Window that serves as the parent of the Find/Replace dialog
  /// </summary>
  public IFindReplaceTextEditorWindow OwnerWindow
  {
    get => (IFindReplaceTextEditorWindow) this.GetValue(FindReplaceManager.OwnerWindowProperty);
    set => this.SetValue(FindReplaceManager.OwnerWindowProperty, (object) value);
  }

  private IFindReplaceTextEditor GetNextEditor(bool previous = false)
  {
    if (!this.ShowSearchIn || this.SearchIn == FindReplaceSearchScope.CurrentDocument || this.Editors == null)
      return this.CurrentEditor;
    List<IFindReplaceTextEditor> replaceTextEditorList = new List<IFindReplaceTextEditor>(this.Editors.Cast<IFindReplaceTextEditor>());
    int num = replaceTextEditorList.IndexOf(this.CurrentEditor);
    if (num >= 0)
    {
      int index = (num + (previous ? replaceTextEditorList.Count - 1 : 1)) % replaceTextEditorList.Count;
      this.CurrentEditor = replaceTextEditorList[index];
    }
    return this.CurrentEditor;
  }

  /// <summary>
  /// Constructs a regular expression according to the currently selected search parameters.
  /// </summary>
  /// <param name="ForceLeftToRight"></param>
  /// <returns>The regular expression.</returns>
  public Regex GetRegEx(bool ForceLeftToRight = false)
  {
    RegexOptions options = RegexOptions.None;
    if (this.SearchUp && !ForceLeftToRight)
      options |= RegexOptions.RightToLeft;
    if (!this.CaseSensitive)
      options |= RegexOptions.IgnoreCase;
    Regex regEx;
    if (this.UseRegEx)
    {
      regEx = new Regex(this.TextToFind, options);
    }
    else
    {
      string pattern = Regex.Escape(this.TextToFind);
      if (this.WholeWord)
        pattern = $"\\b{pattern}\\b";
      regEx = new Regex(pattern, options);
    }
    return regEx;
  }

  public void ReplaceAll(bool AskBefore = true)
  {
    IFindReplaceTextEditor replaceTextEditor = this.CurrentEditor;
    if (replaceTextEditor == null)
      return;
    MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show($"Вы действительно хотите заменить все вхождения '{this.TextToFind}' на '{this.ReplacementText}'?", "Заменить все", MessageBoxButton.YesNoCancel, MessageBoxImage.Exclamation);
    if (AskBefore && messageBoxResult != MessageBoxResult.Yes)
      return;
    object currentEditor = (object) this.CurrentEditor;
    do
    {
      Regex regEx = this.GetRegEx(true);
      int num = 0;
      replaceTextEditor.BeginChange();
      string text = replaceTextEditor.Text;
      foreach (Match match in regEx.Matches(text))
      {
        replaceTextEditor.Replace(num + match.Index, match.Length, this.ReplacementText);
        num += this.ReplacementText.Length - match.Length;
      }
      replaceTextEditor.EndChange();
      replaceTextEditor = this.GetNextEditor();
    }
    while (this.CurrentEditor != currentEditor);
  }

  /// <summary>
  /// Shows this instance of FindReplaceDialog, with the Find page active
  /// </summary>
  public void ShowAsFind()
  {
    this.Dialog.tabMain.SelectedIndex = 0;
    this.Dialog.Show();
    this.Dialog.Activate();
    this.Dialog.txtFind.Focus();
    this.Dialog.txtFind.SelectAll();
  }

  /// <summary>
  /// Shows this instance of FindReplaceDialog, with the Replace page active
  /// </summary>
  public void ShowAsReplace()
  {
    this.Dialog.tabMain.SelectedIndex = 1;
    this.Dialog.Show();
    this.Dialog.Activate();
    this.Dialog.txtFind2.Focus();
    this.Dialog.txtFind2.SelectAll();
  }

  public void ShowAsReplace(object target)
  {
    this.CurrentEditor = (IFindReplaceTextEditor) target;
    this.ShowAsReplace();
  }

  public void FindNext(object target, bool InvertLeftRight = false)
  {
    this.CurrentEditor = (IFindReplaceTextEditor) target;
    this.FindNext(InvertLeftRight);
  }

  public void FindNext(bool InvertLeftRight = false)
  {
    IFindReplaceTextEditor replaceTextEditor = this.CurrentEditor;
    if (replaceTextEditor == null)
      return;
    Regex regEx;
    if (InvertLeftRight)
    {
      this.SearchUp = !this.SearchUp;
      regEx = this.GetRegEx();
      this.SearchUp = !this.SearchUp;
    }
    else
      regEx = this.GetRegEx();
    Match match1 = regEx.Match(replaceTextEditor.Text, regEx.Options.HasFlag((Enum) RegexOptions.RightToLeft) ? replaceTextEditor.SelectionStart : replaceTextEditor.SelectionStart + replaceTextEditor.SelectionLength);
    if (match1.Success)
    {
      replaceTextEditor.Select(match1.Index, match1.Length);
    }
    else
    {
      object currentEditor = (object) this.CurrentEditor;
      do
      {
        if (this.ShowSearchIn)
        {
          replaceTextEditor = this.GetNextEditor(regEx.Options.HasFlag((Enum) RegexOptions.RightToLeft));
          if (replaceTextEditor == null)
            break;
        }
        Match match2 = !regEx.Options.HasFlag((Enum) RegexOptions.RightToLeft) ? regEx.Match(replaceTextEditor.Text, 0) : regEx.Match(replaceTextEditor.Text, replaceTextEditor.Text.Length);
        if (match2.Success)
        {
          replaceTextEditor.Select(match2.Index, match2.Length);
          break;
        }
      }
      while (this.CurrentEditor != currentEditor);
    }
  }

  public void FindPrevious() => this.FindNext(true);

  public void Replace()
  {
    IFindReplaceTextEditor currentEditor = this.CurrentEditor;
    if (currentEditor == null)
      return;
    Regex regEx = this.GetRegEx();
    string str = currentEditor.Text.Substring(currentEditor.SelectionStart, currentEditor.SelectionLength);
    string input = str;
    Match match = regEx.Match(input);
    if (match.Success && match.Index == 0 && match.Length == str.Length)
      currentEditor.Replace(currentEditor.SelectionStart, currentEditor.SelectionLength, this.ReplacementText);
    this.FindNext();
  }

  /// <summary>Closes the Find/Replace dialog, if it is open</summary>
  public void CloseWindow()
  {
    if (!this.IsDialogOpen)
      return;
    this.Dialog.Close();
  }
}
