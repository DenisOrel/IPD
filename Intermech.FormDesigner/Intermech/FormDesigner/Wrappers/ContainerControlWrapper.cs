// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.ContainerControlWrapper
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Localization;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.FormDesigner.Wrappers;

internal class ContainerControlWrapper : ScrollableControlWrapper
{
  private ContainerControl _parent = (ContainerControl) new UserControl();

  public ContainerControlWrapper()
  {
  }

  public ContainerControlWrapper(ContainerControl parent)
    : base((ScrollableControl) parent)
  {
    this._parent = parent;
  }

  [CustomDisplayName("Attribute.FormDesigner_91")]
  [CustomCategory("Attribute.FormDesigner_92")]
  [CustomDescription("Attribute.FormDesigner_93")]
  [Browsable(false)]
  public virtual Control ActiveControl
  {
    get => this._parent.ActiveControl;
    set => this.SetValue(this._pdc[nameof (ActiveControl)], (object) value);
  }

  [CustomDisplayName("Attribute.FormDesigner_94")]
  [CustomCategory("Attribute.FormDesigner_92")]
  [CustomDescription("Attribute.FormDesigner_93")]
  [Browsable(false)]
  public virtual Form ParentForm => this._parent.ParentForm;
}
