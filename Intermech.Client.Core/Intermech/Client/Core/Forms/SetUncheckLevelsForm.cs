
// Type: Intermech.Client.Core.Forms.SetUncheckLevelsForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Common;
using Intermech.Diagnostics;
using Intermech.Navigator.Interfaces;
using Intermech.UI;
using Intermech.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core.Forms;

/// <summary>Диалог ввода количества уровней, которые надо отменить или ниже которого надо снять все отметки</summary>
internal class SetUncheckLevelsForm : 
  IpsBaseDialog,
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
  ICanBeReadOnly2
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Label label1;
  private NumericUpDown _editLevels;
  private Label label2;

  public static SetUncheckLevelsFormResult? Query(
    [CanBeNull] Form parentForm,
    [CanBeNull] System.IServiceProvider ownerServices,
    [CanBeNull] string contextName,
    int maxLevel)
  {
    using (SetUncheckLevelsForm uncheckLevelsForm = new SetUncheckLevelsForm(parentForm, ownerServices, contextName, maxLevel))
      return uncheckLevelsForm.ShowDialog() == DialogResult.OK ? new SetUncheckLevelsFormResult?(new SetUncheckLevelsFormResult(uncheckLevelsForm.Value)) : new SetUncheckLevelsFormResult?();
  }

  public SetUncheckLevelsForm()
    : this((Form) null, (System.IServiceProvider) null, string.Empty, int.MaxValue)
  {
  }

  public SetUncheckLevelsForm(
    [CanBeNull] Form parentForm,
    [CanBeNull] System.IServiceProvider ownerServices,
    [CanBeNull] string contextName,
    int maxLevel)
    : base(parentForm, ownerServices, contextName)
  {
    this.InitializeComponent();
    this._editLevels.Maximum = (Decimal) maxLevel;
  }

  /// <summary>Загрузка свойств из словаря, полученного из FormStorage при вызове LoadPropertiesFromStorage</summary>
  public override void ParseDictionaryFromFormStorage([NotNull] Dictionary<string, object> dic)
  {
    base.ParseDictionaryFromFormStorage(dic);
    object val2;
    if (!dic.TryGetValue("Value", out val2))
      return;
    this._editLevels.Value = Math.Min(this._editLevels.Maximum, (Decimal) val2);
  }

  /// <summary>Загрузка свойств в словарь, который будет сохранён в FormStorage при вызове SavePropertiesToStorage</summary>
  public override void FillPropsDictionary([NotNull] Dictionary<string, object> dic)
  {
    base.FillPropsDictionary(dic);
    dic["Value"] = (object) this._editLevels.Value;
  }

  public int Value
  {
    [DebuggerStepThrough] get => (int) this._editLevels.Value;
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
    this.label1 = new Label();
    this._editLevels = new NumericUpDown();
    this.label2 = new Label();
    this._editLevels.BeginInit();
    this.SuspendLayout();
    this._pnlDialogButtons.Location = new Point(0, 90);
    this._pnlDialogButtons.Size = new Size(291, 36);
    this._pnlDialogButtons.TabIndex = 1;
    this._bevelDialogButtons.Location = new Point(0, 88);
    this._bevelDialogButtons.Shape = BevelShape.Box;
    this._bevelDialogButtons.Size = new Size(291, 2);
    this._bevelDialogButtons.Style = BevelStyle.Lowered;
    this.label1.Location = new Point(12, 9);
    this.label1.Name = "label1";
    this.label1.Size = new Size(240 /*0xF0*/, 47);
    this.label1.TabIndex = 4;
    this.label1.Text = "Ниже какого уровня вложенности объектов снять отметки?";
    this._editLevels.Location = new Point(146, 32 /*0x20*/);
    this._editLevels.Minimum = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this._editLevels.Name = "_editLevels";
    this._editLevels.Size = new Size(82, 20);
    this._editLevels.TabIndex = 0;
    this._editLevels.Value = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this.label2.Location = new Point(12, 61);
    this.label2.Name = "label2";
    this.label2.Size = new Size(240 /*0xF0*/, 20);
    this.label2.TabIndex = 4;
    this.label2.Text = "корневой объект считася нулевым уровнем";
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(291, 126);
    this.Controls.Add((Control) this._editLevels);
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this.label1);
    this.Name = nameof (SetUncheckLevelsForm);
    this.Text = "Снятие отметок";
    this.Controls.SetChildIndex((Control) this._pnlDialogButtons, 0);
    this.Controls.SetChildIndex((Control) this._bevelDialogButtons, 0);
    this.Controls.SetChildIndex((Control) this.label1, 0);
    this.Controls.SetChildIndex((Control) this.label2, 0);
    this.Controls.SetChildIndex((Control) this._editLevels, 0);
    this._editLevels.EndInit();
    this.ResumeLayout(false);
  }
}
