
// Type: Intermech.Navigator.Conditions.AttributeConditionControls.MasterForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Navigator.Conditions.AttributeConditionControls;

public class MasterForm : ConditionForm
{
  private int _index;
  private SelectAttributeStepControl _attributeControl;
  private IEditValueStepControl _editValueControl;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panel1;
  private Button bPrev;
  private Button bNext;
  private Button bCancel;

  public MasterForm() => this.InitializeComponent();

  protected override void OnInitialized()
  {
    this._attributeControl = new SelectAttributeStepControl(this.dataProvider, this.objectTypeIDs);
    this._editValueControl = this.GetEditValueStepControl(this.dataProvider, this.objectTypeIDs);
    this._attributeControl.StepControlStateChanged += new StepControlStateChangedHandler(this.StepControlStateChanged);
    this._attributeControl.AttributeForConditionChanged += new AttributeForConditionChangedHandler(this.AttributeForConditionChanged);
    this._editValueControl.StepControlStateChanged += new StepControlStateChangedHandler(this.StepControlStateChanged);
    if (this.conditionStructure.Equals((object) ConditionStructure.Empty) || this.conditionStructure.Attribute == null)
    {
      this.AttributeForConditionChanged((object) this, new AttributeForConditionChangedEventArgs());
    }
    else
    {
      object obj = this.conditionStructure.Attribute;
      string empty = string.Empty;
      string str;
      if (this.conditionStructure.Attribute is Guid && SystemGUIDs.IsSystemGUID((Guid) this.conditionStructure.Attribute))
      {
        obj = (object) MetaDataHelper.GetAttributeTypeID((Guid) this.conditionStructure.Attribute);
        str = MetaDataHelper.GetAttributeTypeName((Guid) this.conditionStructure.Attribute);
      }
      else
        str = this.dataProvider.GetAttributeName(obj);
      this.AttributeForConditionChanged((object) this, new AttributeForConditionChangedEventArgs(obj, str));
      this._attributeControl.Attribute = new ConditionAttributeInfo(obj, str, this.dataProvider.GetFieldType(this.conditionStructure.Attribute));
      this._attributeControl.AttributeSource = this.conditionStructure.AttributeSource;
      this._index = 1;
    }
    this.RefreshConrtols(this.conditionStructure);
  }

  protected virtual IEditValueStepControl GetEditValueStepControl(
    IConditionDataProvider dataProvider,
    int[] objectTypeIDs)
  {
    return (IEditValueStepControl) new EditValueStepControl(dataProvider, objectTypeIDs);
  }

  private void AttributeForConditionChanged(object sender, AttributeForConditionChangedEventArgs e)
  {
    this.SetTextForAttribute(e.AttributeID != null ? (string.IsNullOrEmpty(e.AttributeName) ? e.AttributeID.ToString() : e.AttributeName) : "<Не выбрано>");
  }

  private void SetTextForAttribute(string attributeName)
  {
    this.Text = $"Условие для атрибута {attributeName}";
  }

  private void StepControlStateChanged(object sender, StepControlStateChangedEventArgs e)
  {
    this.bNext.Enabled = e.NextEnable;
  }

  private void RefreshConrtols(ConditionStructure conditionStructure)
  {
    this.panel1.Controls.Clear();
    if (this._index == 0)
    {
      this._attributeControl.RefreshControl();
      this.panel1.Controls.Add((Control) this._attributeControl);
      this.bPrev.Enabled = false;
      this.bNext.Text = "Далее >";
    }
    else if (this._index == 1)
    {
      conditionStructure.AttributeSource = this._attributeControl.AttributeSource;
      this._editValueControl.OnActivate(this._attributeControl.Attribute, conditionStructure);
      this.panel1.Controls.Add((Control) this._editValueControl.Control);
      this.bPrev.Enabled = true;
      this.bNext.Text = "ОК";
    }
    this.panel1.Controls[0].Dock = DockStyle.Fill;
  }

  private void Prev_Click(object sender, EventArgs e)
  {
    --this._index;
    this.RefreshConrtols(ConditionStructure.Empty);
  }

  private void Next_Click(object sender, EventArgs e)
  {
    if (this._index == 1)
    {
      this.DialogResult = DialogResult.OK;
      this.Close();
    }
    else
    {
      ++this._index;
      this.RefreshConrtols(ConditionStructure.Empty);
    }
  }

  public override ConditionStructure Result => this._editValueControl.ConditionStructure;

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
    this.panel1 = new Panel();
    this.bPrev = new Button();
    this.bNext = new Button();
    this.bCancel = new Button();
    this.SuspendLayout();
    this.panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.panel1.Location = new Point(12, 12);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(630, 244);
    this.panel1.TabIndex = 15;
    this.bPrev.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bPrev.Location = new Point(264, 262);
    this.bPrev.Name = "bPrev";
    this.bPrev.Size = new Size(121, 27);
    this.bPrev.TabIndex = 14;
    this.bPrev.Text = "< Назад";
    this.bPrev.UseVisualStyleBackColor = true;
    this.bPrev.Click += new EventHandler(this.Prev_Click);
    this.bNext.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bNext.Location = new Point(394, 262);
    this.bNext.Name = "bNext";
    this.bNext.Size = new Size(121, 27);
    this.bNext.TabIndex = 13;
    this.bNext.Text = "Далее >";
    this.bNext.UseVisualStyleBackColor = true;
    this.bNext.Click += new EventHandler(this.Next_Click);
    this.bCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Location = new Point(521, 262);
    this.bCancel.Name = "bCancel";
    this.bCancel.Size = new Size(121, 27);
    this.bCancel.TabIndex = 12;
    this.bCancel.Text = "Отмена";
    this.bCancel.UseVisualStyleBackColor = true;
    this.AcceptButton = (IButtonControl) this.bNext;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.bCancel;
    this.ClientSize = new Size(654, 301);
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.bPrev);
    this.Controls.Add((Control) this.bNext);
    this.Controls.Add((Control) this.bCancel);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.MinimumSize = new Size(670, 340);
    this.Name = nameof (MasterForm);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = nameof (MasterForm);
    this.ResumeLayout(false);
  }
}
