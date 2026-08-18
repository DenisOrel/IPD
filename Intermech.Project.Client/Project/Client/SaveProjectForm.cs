// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Client.SaveProjectForm
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

public class SaveProjectForm : Form
{
  private IContainer components;
  private Label _label1;
  private TextBox _nameTextBox;
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
  internal TextBox NameTextBox
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._nameTextBox.CheckInitializedIn<TextBox>((object) this);
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

  public SaveProjectForm() => this.InitializeComponent();

  public static bool Show([NotNull] Intermech.Project.Project p)
  {
    using (SaveProjectForm saveProjectForm = new SaveProjectForm())
    {
      saveProjectForm.NameTextBox.Text = p.Name;
      if (saveProjectForm.ShowDialog() != DialogResult.OK)
        return false;
      p.Name = saveProjectForm.NameTextBox.Text;
      return true;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SaveProjectForm));
    this._label1 = new Label();
    this._nameTextBox = new TextBox();
    this._okButton = new Button();
    this._cancButton = new Button();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._label1, "_label1");
    this._label1.Name = "_label1";
    componentResourceManager.ApplyResources((object) this._nameTextBox, "_nameTextBox");
    this._nameTextBox.Name = "_nameTextBox";
    componentResourceManager.ApplyResources((object) this._okButton, "_okButton");
    this._okButton.DialogResult = DialogResult.OK;
    this._okButton.Name = "_okButton";
    this._okButton.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._cancButton, "_cancButton");
    this._cancButton.DialogResult = DialogResult.Cancel;
    this._cancButton.Name = "_cancButton";
    this._cancButton.UseVisualStyleBackColor = true;
    this.AcceptButton = (IButtonControl) this._okButton;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this._cancButton;
    this.Controls.Add((Control) this._cancButton);
    this.Controls.Add((Control) this._okButton);
    this.Controls.Add((Control) this._nameTextBox);
    this.Controls.Add((Control) this._label1);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (SaveProjectForm);
    this.ShowInTaskbar = false;
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
