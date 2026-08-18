
// Type: Intermech.Controls.ExceptionViewerControl
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using WpfBindingErrors;


namespace Intermech.Controls;

/// <summary>Interaction logic for ExceptionViewerControl.xaml</summary>
internal class ExceptionViewerControl : UserControl, IComponentConnector
{
  private bool _contentLoaded;

  public ExceptionViewerControl()
  {
    this.InitializeComponent();
    DesignerProperties.GetIsInDesignMode((DependencyObject) this);
  }

  [Conditional("RELEASE")]
  private void EnableBindingExceptionThrower()
  {
    if (BindingExceptionThrower.IsAttached)
      return;
    BindingExceptionThrower.Attach();
  }

  /// <summary>InitializeComponent</summary>
  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  public void InitializeComponent()
  {
    if (this._contentLoaded)
      return;
    this._contentLoaded = true;
    Application.LoadComponent((object) this, new Uri("/Intermech.Controls;component/exceptionviewercontrol.xaml", UriKind.Relative));
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  [EditorBrowsable(EditorBrowsableState.Never)]
  void IComponentConnector.Connect(int connectionId, object target) => this._contentLoaded = true;
}
