// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Views.AttributesEditPageView
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
using Telerik.Windows.Controls;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Views;

internal class AttributesEditPageView : UserControl, IComponentConnector
{
  internal RadioButton TextValueRB;
  internal RadioButton AttributeValueRB;
  internal Grid ButtonsGrid;
  internal RadVirtualGrid VirtualGrid;
  internal WizardPageErrorsControl PageErrorsBlock;
  private bool _contentLoaded;

  public AttributesEditPageView() => this.InitializeComponent();

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  public void InitializeComponent()
  {
    if (this._contentLoaded)
      return;
    this._contentLoaded = true;
    Application.LoadComponent((object) this, new Uri("/Intermech.Tools.Client;component/subsystems/compositioncopying/views/attributeseditpageview.xaml", UriKind.Relative));
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  internal Delegate _CreateDelegate(Type delegateType, string handler)
  {
    return Delegate.CreateDelegate(delegateType, (object) this, handler);
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  [EditorBrowsable(EditorBrowsableState.Never)]
  void IComponentConnector.Connect(int connectionId, object target)
  {
    switch (connectionId)
    {
      case 1:
        this.TextValueRB = (RadioButton) target;
        break;
      case 2:
        this.AttributeValueRB = (RadioButton) target;
        break;
      case 3:
        this.ButtonsGrid = (Grid) target;
        break;
      case 4:
        this.VirtualGrid = (RadVirtualGrid) target;
        break;
      case 5:
        this.PageErrorsBlock = (WizardPageErrorsControl) target;
        break;
      default:
        this._contentLoaded = true;
        break;
    }
  }
}
