// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.AdvStructure.SyncObjectStructureSettingsDialog
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Client.Core;
using Intermech.Client.Core.Forms;
using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.UI;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Controls.AdvStructure;

/// <summary>Диалог настроек дерева синхронизации состава</summary>
internal class SyncObjectStructureSettingsDialog : SelectObjectCompositionsSettingsForm
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private RadioButton _radioButtonAutoLoadChecked;

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DebuggerHidden]
  protected internal RadioButton RadioButtonAutoLoadChecked
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._radioButtonAutoLoadChecked.CheckInitializedIn<RadioButton>((object) this);
    }
  }

  public SyncObjectStructureSettingsDialog() => this.InitializeComponent();

  public SyncObjectStructureSettingsDialog(
    [CanBeNull] Form parentForm,
    [CanBeNull] System.IServiceProvider ownerServices,
    [CanBeNull] string contextName,
    [NotNull] SelectObjectCompositionSettings settings)
    : base(parentForm, ownerServices, contextName, settings)
  {
    this.InitializeComponent();
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  protected override void OnShown([NotNull] EventArgs e)
  {
    base.OnShown(e);
    Size clientSize = this.ClientSize;
    int width = clientSize.Width;
    clientSize = this.ClientSize;
    int height = clientSize.Height - this.GroupBoxOnLoad.Height;
    this.ClientSize = new Size(width, height);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this._radioButtonAutoLoadChecked = new RadioButton();
    this._groupBoxOnLoad.SuspendLayout();
    this._panelCommonOptions.SuspendLayout();
    this._editAutoLoadCompositionDepth.BeginInit();
    this._groupBoxWarningIf.SuspendLayout();
    this._editWarningWhenCheckedCountMoreThanCount.BeginInit();
    this._pnlDialogButtons.SuspendLayout();
    this._panelBtns.SuspendLayout();
    this.SuspendLayout();
    this._groupBoxOnLoad.Controls.Add((Control) this._radioButtonAutoLoadChecked);
    this._groupBoxOnLoad.Size = new Size(437, 125);
    this._groupBoxOnLoad.Visible = false;
    this._groupBoxOnLoad.Controls.SetChildIndex((Control) this._checkBoxCheckAllObjectsOnLoad, 0);
    this._groupBoxOnLoad.Controls.SetChildIndex((Control) this._radioButtonAutoLoadCompositionNone, 0);
    this._groupBoxOnLoad.Controls.SetChildIndex((Control) this._radioButtonAutoLoadCompositionFull, 0);
    this._groupBoxOnLoad.Controls.SetChildIndex((Control) this._radioButtonAutoLoadCompositionDepth, 0);
    this._groupBoxOnLoad.Controls.SetChildIndex((Control) this._editAutoLoadCompositionDepth, 0);
    this._groupBoxOnLoad.Controls.SetChildIndex((Control) this._radioButtonAutoLoadChecked, 0);
    this._checkBoxCheckAllObjectsOnLoad.Enabled = false;
    this._checkBoxCheckAllObjectsOnLoad.Visible = false;
    this._panelCommonOptions.Size = new Size(437, 40);
    this._radioButtonAutoLoadCompositionDepth.Location = new Point(23, 72);
    this._radioButtonAutoLoadCompositionFull.CausesValidation = false;
    this._radioButtonAutoLoadCompositionFull.Checked = false;
    this._radioButtonAutoLoadCompositionFull.Location = new Point(23, 24);
    this._radioButtonAutoLoadCompositionFull.TabIndex = 0;
    this._radioButtonAutoLoadCompositionFull.TabStop = false;
    this._radioButtonAutoLoadCompositionNone.Location = new Point(23, 97);
    this._editAutoLoadCompositionDepth.Location = new Point(258, 72);
    this._groupBoxWarningIf.Location = new Point(0, 165);
    this._groupBoxWarningIf.Size = new Size(437, 81);
    this._editWarningWhenCheckedCountMoreThanCount.Location = new Point(303, 23);
    this._checkBoxWarningWhenCheckedCountMoreThan.Size = new Size(273, 17);
    this._checkBoxWarningWhenCheckedCountMoreThan.Text = "Число новых отмеченных объектов больше, чем";
    this._checkBoxWarningWhenCheckedNotLoaded.Size = new Size(317, 17);
    this._checkBoxWarningWhenCheckedNotLoaded.Text = "Отмеченны новых объекты, состав которых не загружен";
    this._pnlDialogButtons.Location = new Point(0, 248);
    this._pnlDialogButtons.Size = new Size(437, 36);
    this._bevelDialogButtons.Location = new Point(0, 246);
    this._bevelDialogButtons.Shape = BevelShape.Box;
    this._bevelDialogButtons.Size = new Size(437, 2);
    this._bevelDialogButtons.Style = BevelStyle.Lowered;
    this._panelBtns.Location = new Point(264, 0);
    this._radioButtonAutoLoadChecked.AutoSize = true;
    this._radioButtonAutoLoadChecked.Checked = true;
    this._radioButtonAutoLoadChecked.Location = new Point(23, 47);
    this._radioButtonAutoLoadChecked.Name = "_radioButtonAutoLoadChecked";
    this._radioButtonAutoLoadChecked.Size = new Size(392, 17);
    this._radioButtonAutoLoadChecked.TabIndex = 1;
    this._radioButtonAutoLoadChecked.TabStop = true;
    this._radioButtonAutoLoadChecked.Text = "Загружать состав всех ранее импортированных (отмеченных) объектов";
    this._radioButtonAutoLoadChecked.UseVisualStyleBackColor = true;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(437, 284);
    this.Name = nameof (SyncObjectStructureSettingsDialog);
    this.Text = "Настройки выбора объектов для синхронизации";
    this._groupBoxOnLoad.ResumeLayout(false);
    this._groupBoxOnLoad.PerformLayout();
    this._panelCommonOptions.ResumeLayout(false);
    this._panelCommonOptions.PerformLayout();
    this._editAutoLoadCompositionDepth.EndInit();
    this._groupBoxWarningIf.ResumeLayout(false);
    this._groupBoxWarningIf.PerformLayout();
    this._editWarningWhenCheckedCountMoreThanCount.EndInit();
    this._pnlDialogButtons.ResumeLayout(false);
    this._panelBtns.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
