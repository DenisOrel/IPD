// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.ActivityPropertyPages.TermsSettingPageControl
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design.ActivityPropertyPages;

public class TermsSettingPageControl : UserControl
{
  private bool _readOnly;
  private ActivitySettings _settings;
  private Control[] _termEdits;
  private CheckBox[] _termCheckboxes;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private GroupBox TermOptsGroupBox;
  private RadioButton TermStartRB2;
  private RadioButton TermStartRB1;
  private Panel panel6;
  private GroupBox GroupBox10;
  private ButtonEdit unreadTermEdit;
  private CheckBox UnreadRollbackCheckBox;
  private Panel panel5;
  private GroupBox GroupBox8;
  private ButtonEdit termEdit;
  private CheckBox UncompleteRollbackCheckBox;

  public TermsSettingPageControl() => this.InitializeComponent();

  public bool ReadOnly
  {
    get => this._readOnly;
    set
    {
      this._readOnly = value;
      if (!this._readOnly)
        return;
      ControlFuncs.SetControlsReadOnly((Control) this, value);
    }
  }

  public bool LoadTermsSettingPageControl(
    ActivitySettings settings,
    IDBObject activityObject,
    bool participantVisible)
  {
    this._settings = settings;
    bool flag = false;
    if (participantVisible)
    {
      this.unreadTermEdit.Enabled = false;
      this._settings.Terms = new Terms((IUserSession) null);
      this._settings.Terms.Load(activityObject);
      this._termEdits = new Control[2]
      {
        (Control) this.termEdit,
        (Control) this.unreadTermEdit
      };
      this._termCheckboxes = new CheckBox[2]
      {
        this.UncompleteRollbackCheckBox,
        this.UnreadRollbackCheckBox
      };
      if (settings.ActivityFlags.HasFlag((Enum) ActivityFlags.StartTermsWithWorkOffers))
        this.TermStartRB1.Checked = true;
      else
        this.TermStartRB2.Checked = true;
      this.UpdateTerms();
    }
    else
      flag = true;
    return flag;
  }

  private void UpdateTerm(int index)
  {
    this._termEdits[index].Text = this._settings.Terms.AsList[index].Period != null ? this._settings.Terms.AsList[index].Period.PeriodText : LocalizationHolder.rm.GetString("Workflow.Design_19");
  }

  private void UpdateTerms()
  {
    for (int index = 0; index < this._settings.Terms.AsList.Count; ++index)
    {
      this.UpdateTerm(index);
      this._termCheckboxes[index].Checked = this._settings.Terms.AsList[index].Enabled;
    }
  }

  private bool EditTerm(int index)
  {
    if (!TimePeriodForm.Edit(this._settings.ObjectIDwithVars, this._settings.Terms.AsList[index]))
      return false;
    this.UpdateTerm(index);
    return true;
  }

  private void termEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    if (!(sender is ButtonEdit) || ((Control) sender).Tag == null)
      return;
    this.EditTerm(Convert.ToInt32(((Control) sender).Tag));
  }

  private void UncompleteRollbackCheckBox_CheckedChanged(object sender, EventArgs e)
  {
    this._settings.Terms.AsList[0].Enabled = (sender as CheckBox).Checked;
  }

  private void UnreadRollbackCheckBox_CheckedChanged(object sender, EventArgs e)
  {
    this.unreadTermEdit.Enabled = (sender as CheckBox).Checked && !this.ReadOnly;
    this._settings.Terms.AsList[1].Enabled = (sender as CheckBox).Checked;
  }

  private void UnreadRollbackCheckBox_Click(object sender, EventArgs e)
  {
    if (!this.UnreadRollbackCheckBox.Checked || this.EditTerm(1))
      return;
    this.UnreadRollbackCheckBox.Checked = false;
  }

  public void SetOptsGroupVisible(bool anyPartChecked)
  {
    this.TermOptsGroupBox.Visible = anyPartChecked;
  }

  public bool Save(IDBObject activityToSave, bool modified, bool anyPartChecked)
  {
    if (this._settings.Terms != null)
    {
      if (anyPartChecked && this.TermStartRB1.Checked)
        this._settings.ActivityFlags |= ActivityFlags.StartTermsWithWorkOffers;
      else
        this._settings.ActivityFlags &= ~ActivityFlags.StartTermsWithWorkOffers;
      if (this._settings.Terms.Modified)
      {
        this._settings.Terms.Save(activityToSave);
        modified = true;
      }
    }
    return modified;
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
    this.TermOptsGroupBox = new GroupBox();
    this.TermStartRB2 = new RadioButton();
    this.TermStartRB1 = new RadioButton();
    this.panel6 = new Panel();
    this.GroupBox10 = new GroupBox();
    this.unreadTermEdit = new ButtonEdit();
    this.UnreadRollbackCheckBox = new CheckBox();
    this.panel5 = new Panel();
    this.GroupBox8 = new GroupBox();
    this.termEdit = new ButtonEdit();
    this.UncompleteRollbackCheckBox = new CheckBox();
    this.TermOptsGroupBox.SuspendLayout();
    this.GroupBox10.SuspendLayout();
    this.unreadTermEdit.Properties.BeginInit();
    this.GroupBox8.SuspendLayout();
    this.termEdit.Properties.BeginInit();
    this.SuspendLayout();
    this.TermOptsGroupBox.Controls.Add((Control) this.TermStartRB2);
    this.TermOptsGroupBox.Controls.Add((Control) this.TermStartRB1);
    this.TermOptsGroupBox.Dock = DockStyle.Top;
    this.TermOptsGroupBox.Location = new Point(0, 221);
    this.TermOptsGroupBox.Name = "TermOptsGroupBox";
    this.TermOptsGroupBox.Size = new Size(718, 87);
    this.TermOptsGroupBox.TabIndex = 14;
    this.TermOptsGroupBox.TabStop = false;
    this.TermOptsGroupBox.Text = "Отсчет сроков";
    this.TermOptsGroupBox.Visible = false;
    this.TermStartRB2.AutoSize = true;
    this.TermStartRB2.ImeMode = ImeMode.NoControl;
    this.TermStartRB2.Location = new Point(12, 50);
    this.TermStartRB2.Name = "TermStartRB2";
    this.TermStartRB2.Size = new Size(240 /*0xF0*/, 21);
    this.TermStartRB2.TabIndex = 1;
    this.TermStartRB2.Text = "С начала выполнения действия";
    this.TermStartRB2.UseVisualStyleBackColor = true;
    this.TermStartRB1.AutoSize = true;
    this.TermStartRB1.ImeMode = ImeMode.NoControl;
    this.TermStartRB1.Location = new Point(12, 25);
    this.TermStartRB1.Name = "TermStartRB1";
    this.TermStartRB1.Size = new Size(265, 21);
    this.TermStartRB1.TabIndex = 0;
    this.TermStartRB1.Text = "С момента рассылки исполнителям";
    this.TermStartRB1.UseVisualStyleBackColor = true;
    this.panel6.Dock = DockStyle.Top;
    this.panel6.Location = new Point(0, 209);
    this.panel6.Name = "panel6";
    this.panel6.Size = new Size(718, 12);
    this.panel6.TabIndex = 15;
    this.GroupBox10.Controls.Add((Control) this.unreadTermEdit);
    this.GroupBox10.Controls.Add((Control) this.UnreadRollbackCheckBox);
    this.GroupBox10.Dock = DockStyle.Top;
    this.GroupBox10.Location = new Point(0, 108);
    this.GroupBox10.Name = "GroupBox10";
    this.GroupBox10.Size = new Size(718, 101);
    this.GroupBox10.TabIndex = 12;
    this.GroupBox10.TabStop = false;
    this.GroupBox10.Text = "Срок прочтения";
    this.unreadTermEdit.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.unreadTermEdit.EditValue = (object) "";
    this.unreadTermEdit.Location = new Point(12, 60);
    this.unreadTermEdit.Name = "unreadTermEdit";
    this.unreadTermEdit.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton((object) "", ButtonPredefines.Ellipsis)
    });
    this.unreadTermEdit.Properties.ReadOnly = true;
    this.unreadTermEdit.Size = new Size(694, 22);
    this.unreadTermEdit.TabIndex = 5;
    this.unreadTermEdit.Tag = (object) "1";
    this.unreadTermEdit.ButtonClick += new ButtonPressedEventHandler(this.termEdit_ButtonClick);
    this.UnreadRollbackCheckBox.AutoSize = true;
    this.UnreadRollbackCheckBox.ImeMode = ImeMode.NoControl;
    this.UnreadRollbackCheckBox.Location = new Point(12, 29);
    this.UnreadRollbackCheckBox.Name = "UnreadRollbackCheckBox";
    this.UnreadRollbackCheckBox.Size = new Size(330, 21);
    this.UnreadRollbackCheckBox.TabIndex = 0;
    this.UnreadRollbackCheckBox.Text = "Возврат, если не прочтено в указанный срок";
    this.UnreadRollbackCheckBox.CheckedChanged += new EventHandler(this.UnreadRollbackCheckBox_CheckedChanged);
    this.UnreadRollbackCheckBox.Click += new EventHandler(this.UnreadRollbackCheckBox_Click);
    this.panel5.Dock = DockStyle.Top;
    this.panel5.Location = new Point(0, 95);
    this.panel5.Name = "panel5";
    this.panel5.Size = new Size(718, 13);
    this.panel5.TabIndex = 13;
    this.GroupBox8.Controls.Add((Control) this.termEdit);
    this.GroupBox8.Controls.Add((Control) this.UncompleteRollbackCheckBox);
    this.GroupBox8.Dock = DockStyle.Top;
    this.GroupBox8.Location = new Point(0, 0);
    this.GroupBox8.Name = "GroupBox8";
    this.GroupBox8.Size = new Size(718, 95);
    this.GroupBox8.TabIndex = 11;
    this.GroupBox8.TabStop = false;
    this.GroupBox8.Text = "Срок выполнения";
    this.termEdit.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.termEdit.EditValue = (object) "";
    this.termEdit.Location = new Point(12, 29);
    this.termEdit.Name = "termEdit";
    this.termEdit.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton((object) "", ButtonPredefines.Ellipsis)
    });
    this.termEdit.Properties.ReadOnly = true;
    this.termEdit.Size = new Size(694, 22);
    this.termEdit.TabIndex = 5;
    this.termEdit.Tag = (object) "0";
    this.termEdit.ButtonClick += new ButtonPressedEventHandler(this.termEdit_ButtonClick);
    this.UncompleteRollbackCheckBox.AutoSize = true;
    this.UncompleteRollbackCheckBox.ImeMode = ImeMode.NoControl;
    this.UncompleteRollbackCheckBox.Location = new Point(12, 63 /*0x3F*/);
    this.UncompleteRollbackCheckBox.Name = "UncompleteRollbackCheckBox";
    this.UncompleteRollbackCheckBox.Size = new Size(340, 21);
    this.UncompleteRollbackCheckBox.TabIndex = 1;
    this.UncompleteRollbackCheckBox.Text = "Возврат, если не выполнено в указанный срок";
    this.UncompleteRollbackCheckBox.CheckedChanged += new EventHandler(this.UncompleteRollbackCheckBox_CheckedChanged);
    this.AutoScaleDimensions = new SizeF(120f, 120f);
    this.AutoScaleMode = AutoScaleMode.Dpi;
    this.BackColor = SystemColors.ControlLightLight;
    this.Controls.Add((Control) this.TermOptsGroupBox);
    this.Controls.Add((Control) this.panel6);
    this.Controls.Add((Control) this.GroupBox10);
    this.Controls.Add((Control) this.panel5);
    this.Controls.Add((Control) this.GroupBox8);
    this.Name = nameof (TermsSettingPageControl);
    this.Size = new Size(718, 327);
    this.TermOptsGroupBox.ResumeLayout(false);
    this.TermOptsGroupBox.PerformLayout();
    this.GroupBox10.ResumeLayout(false);
    this.GroupBox10.PerformLayout();
    this.unreadTermEdit.Properties.EndInit();
    this.GroupBox8.ResumeLayout(false);
    this.GroupBox8.PerformLayout();
    this.termEdit.Properties.EndInit();
    this.ResumeLayout(false);
  }
}
