// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.ActivityPropertyPages.RollbackSettingPageControl
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Interfaces;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design.ActivityPropertyPages;

public class RollbackSettingPageControl : UserControl
{
  private bool _readOnly;
  private ActivitySettings _settings;
  private WorkflowNode _activityNode;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private GroupBox RollbackGroupBox;
  private RadioButton RollRadioButton5;
  private RadioButton RollRadioButton1;
  private RadioButton RollRadioButton2;
  private RadioButton RollRadioButton3;
  private RadioButton RollRadioButton4;

  public RollbackSettingPageControl() => this.InitializeComponent();

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

  public bool LoadRollbackSettingControl(
    ActivitySettings settings,
    IDBObject activityObject,
    WorkflowNode activityNode)
  {
    bool flag = false;
    this._settings = settings;
    this._activityNode = activityNode;
    IDBAttribute attributeById = settings.ActivityType != wfConsts.StartTypeID ? activityObject.GetAttributeByID(wfConsts.AttrRollbackKindID) : (IDBAttribute) null;
    if (attributeById != null)
    {
      this.RollbackKind = attributeById.AsInteger;
      this.RollRadioButton4.Enabled = this.RollRadioButton4.Enabled && settings.ActivityType != wfConsts.RegisterTypeID;
    }
    else
      flag = true;
    return flag;
  }

  private long RollbackKind
  {
    get
    {
      for (int index = 0; index < this.RollbackGroupBox.Controls.Count; ++index)
      {
        if (this.RollbackGroupBox.Controls[index] is RadioButton && ((RadioButton) this.RollbackGroupBox.Controls[index]).Checked)
          return (long) Convert.ToInt32(this.RollbackGroupBox.Controls[index].Tag);
      }
      return 0;
    }
    set
    {
      bool flag1 = false;
      bool flag2 = false;
      if (this._activityNode != null)
      {
        foreach (WorkflowLink link in this._activityNode.Links)
        {
          if (link != null)
          {
            if (link.Backward && link.FromNode == this._activityNode)
              flag1 = true;
            else if (link.LinkKind == LinkKind.ParallelBlock && link.ToNode == this._activityNode)
              flag2 = true;
          }
        }
        if ((int) value == 2 && !flag1 || (int) value == 4 && !flag2)
          value = 0L;
        if (flag2)
          value = 4L;
        else if (flag1)
          value = 2L;
      }
      bool flag3 = flag2 | flag1;
      this.RollRadioButton1.Enabled = !flag3;
      this.RollRadioButton2.Enabled = !flag3;
      this.RollRadioButton3.Enabled = flag1;
      this.RollRadioButton4.Enabled = !flag3;
      for (int index = 0; index < this.RollbackGroupBox.Controls.Count; ++index)
      {
        if (this.RollbackGroupBox.Controls[index] is RadioButton && value == (long) Convert.ToInt32(this.RollbackGroupBox.Controls[index].Tag))
        {
          (this.RollbackGroupBox.Controls[index] as RadioButton).Checked = true;
          break;
        }
      }
      if (!this.RollRadioButton5.Checked)
        return;
      this.RollRadioButton5.Bounds = this.RollRadioButton1.Bounds;
      this.RollRadioButton5.Visible = true;
      this.RollRadioButton1.Visible = false;
    }
  }

  public bool Save(IDBObject activityToSave, bool modified)
  {
    IDBAttribute attributeById = activityToSave.GetAttributeByID(wfConsts.AttrRollbackKindID);
    if (attributeById != null && attributeById.AsInteger != this.RollbackKind)
    {
      attributeById.AsInteger = this.RollbackKind;
      modified = true;
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
    this.RollbackGroupBox = new GroupBox();
    this.RollRadioButton5 = new RadioButton();
    this.RollRadioButton1 = new RadioButton();
    this.RollRadioButton2 = new RadioButton();
    this.RollRadioButton3 = new RadioButton();
    this.RollRadioButton4 = new RadioButton();
    this.RollbackGroupBox.SuspendLayout();
    this.SuspendLayout();
    this.RollbackGroupBox.Controls.Add((Control) this.RollRadioButton5);
    this.RollbackGroupBox.Controls.Add((Control) this.RollRadioButton1);
    this.RollbackGroupBox.Controls.Add((Control) this.RollRadioButton2);
    this.RollbackGroupBox.Controls.Add((Control) this.RollRadioButton3);
    this.RollbackGroupBox.Controls.Add((Control) this.RollRadioButton4);
    this.RollbackGroupBox.Dock = DockStyle.Top;
    this.RollbackGroupBox.Location = new Point(0, 0);
    this.RollbackGroupBox.Name = "RollbackGroupBox";
    this.RollbackGroupBox.Size = new Size(391, 131);
    this.RollbackGroupBox.TabIndex = 1;
    this.RollbackGroupBox.TabStop = false;
    this.RollbackGroupBox.Text = "При возврате назад вернуться";
    this.RollRadioButton5.AutoSize = true;
    this.RollRadioButton5.ImeMode = ImeMode.NoControl;
    this.RollRadioButton5.Location = new Point(244, 99);
    this.RollRadioButton5.Name = "RollRadioButton5";
    this.RollRadioButton5.Size = new Size(133, 21);
    this.RollRadioButton5.TabIndex = 4;
    this.RollRadioButton5.Tag = (object) "4";
    this.RollRadioButton5.Text = "В начало блока";
    this.RollRadioButton5.Visible = false;
    this.RollRadioButton1.AutoSize = true;
    this.RollRadioButton1.Checked = true;
    this.RollRadioButton1.ImeMode = ImeMode.NoControl;
    this.RollRadioButton1.Location = new Point(12, 25);
    this.RollRadioButton1.Name = "RollRadioButton1";
    this.RollRadioButton1.Size = new Size(160 /*0xA0*/, 21);
    this.RollRadioButton1.TabIndex = 0;
    this.RollRadioButton1.TabStop = true;
    this.RollRadioButton1.Text = "В начало маршрута";
    this.RollRadioButton2.AutoSize = true;
    this.RollRadioButton2.ImeMode = ImeMode.NoControl;
    this.RollRadioButton2.Location = new Point(12, 50);
    this.RollRadioButton2.Name = "RollRadioButton2";
    this.RollRadioButton2.Size = new Size(200, 21);
    this.RollRadioButton2.TabIndex = 1;
    this.RollRadioButton2.Tag = (object) "1";
    this.RollRadioButton2.Text = "На предыдущее действие";
    this.RollRadioButton3.AutoSize = true;
    this.RollRadioButton3.Enabled = false;
    this.RollRadioButton3.ImeMode = ImeMode.NoControl;
    this.RollRadioButton3.Location = new Point(12, 74);
    this.RollRadioButton3.Name = "RollRadioButton3";
    this.RollRadioButton3.Size = new Size(104, 21);
    this.RollRadioButton3.TabIndex = 2;
    this.RollRadioButton3.Tag = (object) "2";
    this.RollRadioButton3.Text = "По стрелке";
    this.RollRadioButton4.AutoSize = true;
    this.RollRadioButton4.ImeMode = ImeMode.NoControl;
    this.RollRadioButton4.Location = new Point(12, 99);
    this.RollRadioButton4.Name = "RollRadioButton4";
    this.RollRadioButton4.Size = new Size(153, 21);
    this.RollRadioButton4.TabIndex = 3;
    this.RollRadioButton4.Tag = (object) "3";
    this.RollRadioButton4.Text = "Возврат запрещён";
    this.AutoScaleDimensions = new SizeF(120f, 120f);
    this.AutoScaleMode = AutoScaleMode.Dpi;
    this.BackColor = SystemColors.ControlLightLight;
    this.Controls.Add((Control) this.RollbackGroupBox);
    this.Name = nameof (RollbackSettingPageControl);
    this.Size = new Size(391, 133);
    this.RollbackGroupBox.ResumeLayout(false);
    this.RollbackGroupBox.PerformLayout();
    this.ResumeLayout(false);
  }
}
