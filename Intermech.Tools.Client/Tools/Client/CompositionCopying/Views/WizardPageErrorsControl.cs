// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Views.WizardPageErrorsControl
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Views;

internal class WizardPageErrorsControl : UserControl, IComponentConnector
{
  internal ToggleButton errorToggleButton;
  internal ToggleButton warningToggleButton;
  internal Button ObjectCard;
  private bool _contentLoaded;

  public WizardPageErrorsControl() => this.InitializeComponent();

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  public void InitializeComponent()
  {
    if (this._contentLoaded)
      return;
    this._contentLoaded = true;
    Application.LoadComponent((object) this, new Uri("/Intermech.Tools.Client;component/subsystems/compositioncopying/views/wizardpageerrorscontrol.xaml", UriKind.Relative));
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  [EditorBrowsable(EditorBrowsableState.Never)]
  void IComponentConnector.Connect(int connectionId, object target)
  {
    switch (connectionId)
    {
      case 1:
        this.errorToggleButton = (ToggleButton) target;
        break;
      case 2:
        this.warningToggleButton = (ToggleButton) target;
        break;
      case 3:
        this.ObjectCard = (Button) target;
        break;
      default:
        this._contentLoaded = true;
        break;
    }
  }
}
