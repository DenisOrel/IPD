
// Type: Intermech.UI.Wpf.Controls.FindReplaceDialog
// Assembly: Intermech.Bcl.Wpf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 91600B17-2177-4703-BAB9-56FCFFBCBBA2
:\IPS\Client\Intermech.Bcl.Wpf.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.Wpf.xml

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;


namespace Intermech.UI.Wpf.Controls;

/// <summary>Interaction logic for FindReplaceDialog.xaml</summary>
internal class FindReplaceDialog : Window, IComponentConnector
{
  internal TabControl tabMain;
  internal TabItem tabFind;
  internal TextBox txtFind;
  internal TabItem tabReplace;
  internal TextBox txtFind2;
  internal TextBox txtReplace;
  private bool _contentLoaded;

  public FindReplaceDialog()
  {
    this.InitializeComponent();
    if (DesignerProperties.GetIsInDesignMode((DependencyObject) this))
      return;
    this.PreviewKeyDown += new KeyEventHandler(this.OnWindowKeyDown);
    this.CommandBindings.Add(new CommandBinding((ICommand) FindReplaceDialogCommands.FindNext, new ExecutedRoutedEventHandler(this.OnFindNextCommand)));
    this.CommandBindings.Add(new CommandBinding((ICommand) FindReplaceDialogCommands.Replace, new ExecutedRoutedEventHandler(this.ReplaceClick)));
    this.CommandBindings.Add(new CommandBinding((ICommand) FindReplaceDialogCommands.ReplaceAll, new ExecutedRoutedEventHandler(this.ReplaceAllClick)));
  }

  private void OnWindowKeyDown(object sender, KeyEventArgs e)
  {
    if (e.Key != Key.Escape)
      return;
    this.Close();
  }

  private FindReplaceManager Manager => this.DataContext as FindReplaceManager;

  private void OnFindNextCommand(object sender, ExecutedRoutedEventArgs e)
  {
    if (this.Manager == null)
      return;
    this.Manager.FindNext();
  }

  private void ReplaceClick(object sender, ExecutedRoutedEventArgs e)
  {
    if (this.Manager == null)
      return;
    this.Manager.Replace();
  }

  private void ReplaceAllClick(object sender, ExecutedRoutedEventArgs e)
  {
    if (this.Manager == null)
      return;
    this.Manager.ReplaceAll();
  }

  private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
  {
    if (e.HeightChanged && this.SizeToContent == SizeToContent.Manual)
      this.Height = this.DesiredSize.Height;
    if (this.SizeToContent == SizeToContent.Height)
      return;
    this.SizeToContent = SizeToContent.Height;
  }

  /// <summary>InitializeComponent</summary>
  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  public void InitializeComponent()
  {
    if (this._contentLoaded)
      return;
    this._contentLoaded = true;
    Application.LoadComponent((object) this, new Uri("/Intermech.Bcl.Wpf;component/ui/wpf/controls/findreplacedialog.xaml", UriKind.Relative));
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  [EditorBrowsable(EditorBrowsableState.Never)]
  void IComponentConnector.Connect(int connectionId, object target)
  {
    switch (connectionId)
    {
      case 1:
        ((FrameworkElement) target).SizeChanged += new SizeChangedEventHandler(this.Window_SizeChanged);
        break;
      case 2:
        this.tabMain = (TabControl) target;
        break;
      case 3:
        this.tabFind = (TabItem) target;
        break;
      case 4:
        this.txtFind = (TextBox) target;
        break;
      case 5:
        this.tabReplace = (TabItem) target;
        break;
      case 6:
        this.txtFind2 = (TextBox) target;
        break;
      case 7:
        this.txtReplace = (TextBox) target;
        break;
      default:
        this._contentLoaded = true;
        break;
    }
  }
}
