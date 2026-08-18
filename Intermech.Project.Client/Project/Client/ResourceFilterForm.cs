// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Client.ResourceFilterForm
// Assembly: Intermech.Project.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D968BDD9-29F0-4E24-8F57-6E851EE47258
// Assembly location: D:\IPS\Client\Intermech.Project.Client.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Client;

public class ResourceFilterForm : Form
{
  private IContainer components;
  private Label _label1;
  private ComboBox _resourcesComboBox;
  private Button _okButton;
  private Button _cancButton;

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  internal Label Label1
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._label1.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  internal ComboBox ResourcesComboBox
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._resourcesComboBox.CheckInitializedIn<ComboBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  internal Button OkButton
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._okButton.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  internal Button CancButton
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._cancButton.CheckInitializedIn<Button>((object) this);
    }
  }

  public ResourceFilterForm([CanBeNull] Intermech.Project.Project p)
  {
    this.InitializeComponent();
    if (p == null)
      return;
    this.ResourcesComboBox.Items.AddRange((object[]) p.AllResources.ToArray());
  }

  public long ResourceID
  {
    get
    {
      return !(this.ResourcesComboBox.SelectedItem is Resource selectedItem) ? 0L : selectedItem.ObjectID;
    }
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ResourceFilterForm));
    this._label1 = new Label();
    this._resourcesComboBox = new ComboBox();
    this._okButton = new Button();
    this._cancButton = new Button();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._label1, "_label1");
    this._label1.Name = "_label1";
    componentResourceManager.ApplyResources((object) this._resourcesComboBox, "_resourcesComboBox");
    this._resourcesComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
    this._resourcesComboBox.FormattingEnabled = true;
    this._resourcesComboBox.Name = "_resourcesComboBox";
    componentResourceManager.ApplyResources((object) this._okButton, "_okButton");
    this._okButton.DialogResult = DialogResult.OK;
    this._okButton.Name = "_okButton";
    componentResourceManager.ApplyResources((object) this._cancButton, "_cancButton");
    this._cancButton.DialogResult = DialogResult.Cancel;
    this._cancButton.Name = "_cancButton";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._cancButton);
    this.Controls.Add((Control) this._okButton);
    this.Controls.Add((Control) this._resourcesComboBox);
    this.Controls.Add((Control) this._label1);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ResourceFilterForm);
    this.ShowInTaskbar = false;
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
