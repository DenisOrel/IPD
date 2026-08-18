// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Print.CreateNewPrintSchemeDlg
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Common;
using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Navigator.Interfaces;
using Intermech.UI;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Print;

public class CreateNewPrintSchemeDlg : 
  ProjectDialogBase,
  IComponent,
  IDisposable,
  IDropTarget,
  ISynchronizeInvoke,
  IWin32Window,
  IBindableComponent,
  IContainerControl,
  IContextAware,
  ISupportSaveLocks,
  INamedContext,
  ICanBeReadOnly,
  ICanBeReadOnly2,
  IClientProjectContext,
  IProjectViewContext
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Label _labelName;
  private TextBox _textBoxName;

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Label LabelName
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._labelName.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal TextBox TextBoxName
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._textBoxName.CheckInitializedIn<TextBox>((object) this);
    }
  }

  public CreateNewPrintSchemeDlg() => this.InitializeComponent();

  public CreateNewPrintSchemeDlg([CanBeNull] Form centerOnForm, [NotNull] string contextName)
    : base(centerOnForm, (System.IServiceProvider) null, contextName)
  {
    this.InitializeComponent();
  }

  public CreateNewPrintSchemeDlg(
    [CanBeNull] Form centerOnForm,
    [CanBeNull] System.IServiceProvider ownerServices,
    [NotNull] string contextName)
    : base(centerOnForm, ownerServices, contextName)
  {
    this.InitializeComponent();
  }

  [NotNull]
  public string SchemeName
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.TextBoxName.Text;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this.TextBoxName.Text = value;
    }
  }

  private void _textBoxName_TextChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.UpdateCommands();
  }

  /// <summary>Дополнительная проверка (кроме IsReadOnly и блокировки сохранения - _saveLocker.IsLocked), должна ли быть включена кнопка OK</summary>
  /// <returns>true если кнопка может быть включена</returns>
  protected override bool OkButtonCanBeEnabled()
  {
    return base.OkButtonCanBeEnabled() && this.TextBoxName.Text.Trim().Any<char>();
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this._labelName = new Label();
    this._textBoxName = new TextBox();
    this._pnlDialogButtons.SuspendLayout();
    this._panelBtns.SuspendLayout();
    this.SuspendLayout();
    this._pnlDialogButtons.Location = new Point(0, 61);
    this._pnlDialogButtons.Size = new Size(377, 36);
    this._pnlDialogButtons.TabIndex = 1;
    this._okButton.Enabled = false;
    this._bevelDialogButtons.Location = new Point(0, 59);
    this._bevelDialogButtons.Shape = BevelShape.Box;
    this._bevelDialogButtons.Size = new Size(377, 2);
    this._bevelDialogButtons.Style = BevelStyle.Lowered;
    this._panelBtns.Location = new Point(204, 0);
    this._labelName.AutoSize = true;
    this._labelName.Location = new Point(11, 10);
    this._labelName.Name = "_labelName";
    this._labelName.Size = new Size(122, 13);
    this._labelName.TabIndex = 4;
    this._labelName.Text = "Наименование схемы:";
    this._textBoxName.Location = new Point(12, 29);
    this._textBoxName.Name = "_textBoxName";
    this._textBoxName.Size = new Size(353, 20);
    this._textBoxName.TabIndex = 0;
    this._textBoxName.TextChanged += new EventHandler(this._textBoxName_TextChanged);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(377, 97);
    this.Controls.Add((Control) this._textBoxName);
    this.Controls.Add((Control) this._labelName);
    this.Name = nameof (CreateNewPrintSchemeDlg);
    this.Text = "Сохранение схемы печати";
    this.Controls.SetChildIndex((Control) this._pnlDialogButtons, 0);
    this.Controls.SetChildIndex((Control) this._bevelDialogButtons, 0);
    this.Controls.SetChildIndex((Control) this._labelName, 0);
    this.Controls.SetChildIndex((Control) this._textBoxName, 0);
    this._pnlDialogButtons.ResumeLayout(false);
    this._panelBtns.ResumeLayout(false);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
