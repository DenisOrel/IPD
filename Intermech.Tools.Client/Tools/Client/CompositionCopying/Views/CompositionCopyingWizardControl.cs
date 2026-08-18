// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Views.CompositionCopyingWizardControl
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using WpfBindingErrors;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Views;

internal class CompositionCopyingWizardControl : UserControl, IComponentConnector
{
  private bool _contentLoaded;

  public CompositionCopyingWizardControl()
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

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  public void InitializeComponent()
  {
    if (this._contentLoaded)
      return;
    this._contentLoaded = true;
    Application.LoadComponent((object) this, new Uri("/Intermech.Tools.Client;component/subsystems/compositioncopying/views/compositioncopyingwizardcontrol.xaml", UriKind.Relative));
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  [EditorBrowsable(EditorBrowsableState.Never)]
  void IComponentConnector.Connect(int connectionId, object target) => this._contentLoaded = true;
}
