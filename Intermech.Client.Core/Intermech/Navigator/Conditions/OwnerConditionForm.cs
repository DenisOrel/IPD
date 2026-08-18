
// Type: Intermech.Navigator.Conditions.OwnerConditionForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using Intermech.Security;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Navigator.Conditions;

public class OwnerConditionForm : ConditionForm
{
  private long _selectedID;
  private readonly int _userTypeID;
  private readonly int _groupTypeID;
  private readonly int _rankTypeID;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ComboBox cbOperator;
  private GroupBox groupBox1;
  private RadioButton rbUser;
  private Button bOK;
  private Button bCancel;
  protected MaskedTextBox tbTextUser;
  protected Button bOpenDialogUser;
  private CheckBox cbCurrentUser;
  protected MaskedTextBox tbSubordinate;
  protected Button bSubordinate;
  private RadioButton rbSubordinate;
  protected MaskedTextBox tbPosition;
  protected Button bPosition;
  private RadioButton rbPosition;
  protected MaskedTextBox tbGroup;
  protected Button bGroup;
  private RadioButton rbGroup;

  public OwnerConditionForm()
  {
    this.InitializeComponent();
    this._userTypeID = MetaDataHelper.GetObjectTypeID("cad00002-306c-11d8-b4e9-00304f19f545");
    this._groupTypeID = MetaDataHelper.GetObjectTypeID("cad00003-306c-11d8-b4e9-00304f19f545");
    this._rankTypeID = MetaDataHelper.GetObjectTypeID("cad00147-306c-11d8-b4e9-00304f19f545");
  }

  protected override void OnInitialized()
  {
    base.OnInitialized();
    this.cbOperator.Items.Clear();
    this.cbOperator.Items.AddRange((object[]) this._operatorItems);
    this.cbOperator.SelectedIndex = this.conditionStructure.RelationalOperator.Equals((object) RelationalOperators.NotEqual) || this.conditionStructure.RelationalOperator.Equals((object) RelationalOperators.NotIn) ? 1 : 0;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      long fromConditionValue = OwnerController.GetObjectIDFromConditionValue(this.conditionStructure);
      if (fromConditionValue != 0L)
      {
        QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(fromConditionValue);
        if (!objectInfo.Empty)
        {
          if (objectInfo.ObjectTypeID == this._rankTypeID)
            this.FillControl(this.rbPosition, this.tbPosition, objectInfo.Caption);
          else if (objectInfo.ObjectTypeID == this._userTypeID)
            this.FillControl(this.rbSubordinate, this.tbSubordinate, objectInfo.Caption);
          else if (objectInfo.ObjectTypeID == this._groupTypeID)
            this.FillControl(this.rbGroup, this.tbGroup, objectInfo.Caption);
          this._selectedID = fromConditionValue;
        }
      }
      else if (this.conditionStructure.Value is long objectID && objectID != 0L)
      {
        QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(objectID);
        if (!objectInfo.Empty)
        {
          this.FillControl(this.rbUser, this.tbTextUser, objectInfo.Caption);
          this._selectedID = objectID;
        }
      }
      else if (this.conditionStructure.Value is string str)
      {
        if (str.Equals(Intermech.Consts.CurrentUserFunction))
          this.rbUser.Checked = this.cbCurrentUser.Checked = true;
      }
    }
    this.bOK.Enabled = false;
  }

  private void FillControl(RadioButton radioButton, MaskedTextBox textBox, string caption)
  {
    radioButton.Checked = true;
    textBox.Text = caption;
  }

  public override ConditionStructure Result
  {
    get
    {
      this.conditionStructure.Attribute = (object) -8;
      this.conditionStructure.RelationalOperator = this.cbOperator.SelectedIndex == 0 ? RelationalOperators.Equal : RelationalOperators.NotEqual;
      if (this.rbUser.Checked)
      {
        if (this.cbCurrentUser.Checked)
          this.conditionStructure.Value = (object) Intermech.Consts.CurrentUserFunction;
        else
          this.conditionStructure.Value = (object) this._selectedID;
      }
      else if (this.rbPosition.Checked)
        this.conditionStructure.Value = (object) new ConditionRankIDReplacer(this._selectedID);
      else
        this.conditionStructure.Value = (object) new ConditionGroupIDReplacer(this._selectedID, false);
      return this.conditionStructure;
    }
  }

  private OwnerConditionForm.OperatorItem[] _operatorItems
  {
    get
    {
      return new OwnerConditionForm.OperatorItem[2]
      {
        new OwnerConditionForm.OperatorItem(RelationalOperators.Equal),
        new OwnerConditionForm.OperatorItem(RelationalOperators.NotEqual)
      };
    }
  }

  private void CbCurrentUser_CheckedChanged(object sender, EventArgs e)
  {
    if (this.cbCurrentUser.Checked)
      this.SetEmptyValue();
    this.bOK.Enabled = this.cbCurrentUser.Checked;
    this.tbTextUser.Enabled = this.bOpenDialogUser.Enabled = !this.cbCurrentUser.Checked;
  }

  private void RbGroup_CheckedChanged(object sender, EventArgs e)
  {
    this.cbCurrentUser.Checked = false;
    this.SetEnableControls(false);
    this.SetEmptyValue();
    this.tbGroup.Enabled = this.bGroup.Enabled = true;
  }

  private void RbPosition_CheckedChanged(object sender, EventArgs e)
  {
    this.cbCurrentUser.Checked = false;
    this.SetEnableControls(false);
    this.SetEmptyValue();
    this.tbPosition.Enabled = this.bPosition.Enabled = true;
  }

  private void RbSubordinate_CheckedChanged(object sender, EventArgs e)
  {
    this.cbCurrentUser.Checked = false;
    this.SetEnableControls(false);
    this.SetEmptyValue();
    this.tbSubordinate.Enabled = this.bSubordinate.Enabled = true;
  }

  private void RbUser_CheckedChanged(object sender, EventArgs e)
  {
    this.SetEnableControls(false);
    this.tbTextUser.Enabled = this.bOpenDialogUser.Enabled = this.cbCurrentUser.Enabled = true;
  }

  private void SetEnableControls(bool enable)
  {
    this.tbTextUser.Enabled = this.tbGroup.Enabled = this.tbPosition.Enabled = this.tbSubordinate.Enabled = this.bOpenDialogUser.Enabled = this.bGroup.Enabled = this.bPosition.Enabled = this.bSubordinate.Enabled = this.cbCurrentUser.Enabled = enable;
  }

  private void SetEmptyValue()
  {
    this.tbTextUser.Text = this.tbGroup.Text = this.tbPosition.Text = this.tbSubordinate.Text = string.Empty;
    this._selectedID = 0L;
    this.bOK.Enabled = false;
  }

  private void BOpenDialogUser_Click(object sender, EventArgs e)
  {
    IDBObjectID dbObjectId = this.OnSelectDialog(this._userTypeID, (IDescriptor) new UsersGroupsDescriptor(), "Выберите пользователя");
    if (dbObjectId == null)
      return;
    this._selectedID = dbObjectId.Value;
    this.tbTextUser.Text = dbObjectId.Caption;
    this.bOK.Enabled = true;
  }

  private void BGroup_Click(object sender, EventArgs e)
  {
    IDBObjectID dbObjectId = this.OnSelectDialog(this._groupTypeID, (IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(this._groupTypeID), "Выберите группу пользователей");
    if (dbObjectId == null)
      return;
    this._selectedID = dbObjectId.Value;
    this.tbGroup.Text = dbObjectId.Caption;
    this.bOK.Enabled = true;
  }

  private void BPosition_Click(object sender, EventArgs e)
  {
    IDBObjectID dbObjectId = this.OnSelectDialog(this._rankTypeID, (IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(this._rankTypeID), "Выберите должность");
    if (dbObjectId == null)
      return;
    this._selectedID = dbObjectId.Value;
    this.tbPosition.Text = dbObjectId.Caption;
    this.bOK.Enabled = true;
  }

  private void BSubordinate_Click(object sender, EventArgs e)
  {
    IDBObjectID dbObjectId = this.OnSelectDialog(this._userTypeID, (IDescriptor) new UsersGroupsDescriptor(), "Выберите руководителя");
    if (dbObjectId == null)
      return;
    this._selectedID = dbObjectId.Value;
    this.tbSubordinate.Text = dbObjectId.Caption;
    this.bOK.Enabled = true;
  }

  private IDBObjectID OnSelectDialog(int objectType, IDescriptor descriptor, string message)
  {
    return !(SelectionWindow.Select(message, descriptor, typeof (IDBObjectID), SelectionOptions.SelectObjects | SelectionOptions.DisableMultiselect, new int[1]
    {
      objectType
    }) is IDBObjectID[] dbObjectIdArray) ? (IDBObjectID) null : dbObjectIdArray[0];
  }

  private void CbOperator_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.bOK.Enabled = this.cbCurrentUser.Checked || this._selectedID != 0L;
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
    this.cbOperator = new ComboBox();
    this.groupBox1 = new GroupBox();
    this.tbSubordinate = new MaskedTextBox();
    this.bSubordinate = new Button();
    this.rbSubordinate = new RadioButton();
    this.tbPosition = new MaskedTextBox();
    this.bPosition = new Button();
    this.rbPosition = new RadioButton();
    this.tbGroup = new MaskedTextBox();
    this.bGroup = new Button();
    this.rbGroup = new RadioButton();
    this.cbCurrentUser = new CheckBox();
    this.tbTextUser = new MaskedTextBox();
    this.bOpenDialogUser = new Button();
    this.rbUser = new RadioButton();
    this.bOK = new Button();
    this.bCancel = new Button();
    this.groupBox1.SuspendLayout();
    this.SuspendLayout();
    this.cbOperator.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbOperator.FormattingEnabled = true;
    this.cbOperator.Items.AddRange(new object[2]
    {
      (object) "Равно",
      (object) "Не равно"
    });
    this.cbOperator.Location = new Point(34, 26);
    this.cbOperator.Name = "cbOperator";
    this.cbOperator.Size = new Size(151, 21);
    this.cbOperator.TabIndex = 0;
    this.cbOperator.SelectedIndexChanged += new EventHandler(this.CbOperator_SelectedIndexChanged);
    this.groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.groupBox1.Controls.Add((Control) this.tbSubordinate);
    this.groupBox1.Controls.Add((Control) this.bSubordinate);
    this.groupBox1.Controls.Add((Control) this.rbSubordinate);
    this.groupBox1.Controls.Add((Control) this.tbPosition);
    this.groupBox1.Controls.Add((Control) this.bPosition);
    this.groupBox1.Controls.Add((Control) this.rbPosition);
    this.groupBox1.Controls.Add((Control) this.tbGroup);
    this.groupBox1.Controls.Add((Control) this.bGroup);
    this.groupBox1.Controls.Add((Control) this.rbGroup);
    this.groupBox1.Controls.Add((Control) this.cbCurrentUser);
    this.groupBox1.Controls.Add((Control) this.tbTextUser);
    this.groupBox1.Controls.Add((Control) this.bOpenDialogUser);
    this.groupBox1.Controls.Add((Control) this.rbUser);
    this.groupBox1.Location = new Point(12, 53);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(346, 328);
    this.groupBox1.TabIndex = 1;
    this.groupBox1.TabStop = false;
    this.tbSubordinate.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbSubordinate.BackColor = SystemColors.Window;
    this.tbSubordinate.Enabled = false;
    this.tbSubordinate.Location = new Point(40, 281);
    this.tbSubordinate.Margin = new Padding(0);
    this.tbSubordinate.Name = "tbSubordinate";
    this.tbSubordinate.Size = new Size((int) byte.MaxValue, 20);
    this.tbSubordinate.TabIndex = 15;
    this.bSubordinate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bSubordinate.Enabled = false;
    this.bSubordinate.ImeMode = ImeMode.NoControl;
    this.bSubordinate.Location = new Point(297, 280);
    this.bSubordinate.Margin = new Padding(0);
    this.bSubordinate.Name = "bSubordinate";
    this.bSubordinate.Size = new Size(24, 23);
    this.bSubordinate.TabIndex = 14;
    this.bSubordinate.Text = "...";
    this.bSubordinate.UseVisualStyleBackColor = true;
    this.bSubordinate.Click += new EventHandler(this.BSubordinate_Click);
    this.rbSubordinate.AutoSize = true;
    this.rbSubordinate.Location = new Point(22, 251);
    this.rbSubordinate.Name = "rbSubordinate";
    this.rbSubordinate.Size = new Size(170, 17);
    this.rbSubordinate.TabIndex = 13;
    this.rbSubordinate.Text = "Подчиненный руководителя:";
    this.rbSubordinate.UseVisualStyleBackColor = true;
    this.rbSubordinate.CheckedChanged += new EventHandler(this.RbSubordinate_CheckedChanged);
    this.tbPosition.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbPosition.BackColor = SystemColors.Window;
    this.tbPosition.Enabled = false;
    this.tbPosition.Location = new Point(40, 211);
    this.tbPosition.Margin = new Padding(0);
    this.tbPosition.Name = "tbPosition";
    this.tbPosition.Size = new Size((int) byte.MaxValue, 20);
    this.tbPosition.TabIndex = 12;
    this.bPosition.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bPosition.Enabled = false;
    this.bPosition.ImeMode = ImeMode.NoControl;
    this.bPosition.Location = new Point(297, 210);
    this.bPosition.Margin = new Padding(0);
    this.bPosition.Name = "bPosition";
    this.bPosition.Size = new Size(24, 23);
    this.bPosition.TabIndex = 11;
    this.bPosition.Text = "...";
    this.bPosition.UseVisualStyleBackColor = true;
    this.bPosition.Click += new EventHandler(this.BPosition_Click);
    this.rbPosition.AutoSize = true;
    this.rbPosition.Location = new Point(22, 181);
    this.rbPosition.Name = "rbPosition";
    this.rbPosition.Size = new Size(116, 17);
    this.rbPosition.TabIndex = 10;
    this.rbPosition.Text = "Имеет должность";
    this.rbPosition.UseVisualStyleBackColor = true;
    this.rbPosition.CheckedChanged += new EventHandler(this.RbPosition_CheckedChanged);
    this.tbGroup.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbGroup.BackColor = SystemColors.Window;
    this.tbGroup.Enabled = false;
    this.tbGroup.Location = new Point(40, 141);
    this.tbGroup.Margin = new Padding(0);
    this.tbGroup.Name = "tbGroup";
    this.tbGroup.Size = new Size((int) byte.MaxValue, 20);
    this.tbGroup.TabIndex = 9;
    this.bGroup.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bGroup.Enabled = false;
    this.bGroup.ImeMode = ImeMode.NoControl;
    this.bGroup.Location = new Point(297, 140);
    this.bGroup.Margin = new Padding(0);
    this.bGroup.Name = "bGroup";
    this.bGroup.Size = new Size(24, 23);
    this.bGroup.TabIndex = 8;
    this.bGroup.Text = "...";
    this.bGroup.UseVisualStyleBackColor = true;
    this.bGroup.Click += new EventHandler(this.BGroup_Click);
    this.rbGroup.AutoSize = true;
    this.rbGroup.Location = new Point(22, 111);
    this.rbGroup.Name = "rbGroup";
    this.rbGroup.Size = new Size(105, 17);
    this.rbGroup.TabIndex = 7;
    this.rbGroup.Text = "Входит в группу";
    this.rbGroup.UseVisualStyleBackColor = true;
    this.rbGroup.CheckedChanged += new EventHandler(this.RbGroup_CheckedChanged);
    this.cbCurrentUser.AutoSize = true;
    this.cbCurrentUser.ImeMode = ImeMode.NoControl;
    this.cbCurrentUser.Location = new Point(40, 73);
    this.cbCurrentUser.Name = "cbCurrentUser";
    this.cbCurrentUser.Size = new Size(145, 17);
    this.cbCurrentUser.TabIndex = 6;
    this.cbCurrentUser.Text = "Текущий пользователь";
    this.cbCurrentUser.UseVisualStyleBackColor = true;
    this.cbCurrentUser.CheckedChanged += new EventHandler(this.CbCurrentUser_CheckedChanged);
    this.tbTextUser.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbTextUser.BackColor = SystemColors.Window;
    this.tbTextUser.Location = new Point(40, 49);
    this.tbTextUser.Margin = new Padding(0);
    this.tbTextUser.Name = "tbTextUser";
    this.tbTextUser.Size = new Size((int) byte.MaxValue, 20);
    this.tbTextUser.TabIndex = 5;
    this.bOpenDialogUser.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bOpenDialogUser.ImeMode = ImeMode.NoControl;
    this.bOpenDialogUser.Location = new Point(297, 48 /*0x30*/);
    this.bOpenDialogUser.Margin = new Padding(0);
    this.bOpenDialogUser.Name = "bOpenDialogUser";
    this.bOpenDialogUser.Size = new Size(24, 23);
    this.bOpenDialogUser.TabIndex = 4;
    this.bOpenDialogUser.Text = "...";
    this.bOpenDialogUser.UseVisualStyleBackColor = true;
    this.bOpenDialogUser.Click += new EventHandler(this.BOpenDialogUser_Click);
    this.rbUser.AutoSize = true;
    this.rbUser.Checked = true;
    this.rbUser.Location = new Point(22, 19);
    this.rbUser.Name = "rbUser";
    this.rbUser.Size = new Size(98, 17);
    this.rbUser.TabIndex = 0;
    this.rbUser.TabStop = true;
    this.rbUser.Text = "Пользователь";
    this.rbUser.UseVisualStyleBackColor = true;
    this.rbUser.CheckedChanged += new EventHandler(this.RbUser_CheckedChanged);
    this.bOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bOK.DialogResult = DialogResult.OK;
    this.bOK.Enabled = false;
    this.bOK.Location = new Point(110, 391);
    this.bOK.Name = "bOK";
    this.bOK.Size = new Size(121, 27);
    this.bOK.TabIndex = 2;
    this.bOK.Text = "ОК";
    this.bOK.UseVisualStyleBackColor = true;
    this.bCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Location = new Point(237, 391);
    this.bCancel.Name = "bCancel";
    this.bCancel.Size = new Size(121, 27);
    this.bCancel.TabIndex = 3;
    this.bCancel.Text = "Отмена";
    this.bCancel.UseVisualStyleBackColor = true;
    this.AcceptButton = (IButtonControl) this.bOK;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.bCancel;
    this.ClientSize = new Size(370, 430);
    this.Controls.Add((Control) this.bCancel);
    this.Controls.Add((Control) this.bOK);
    this.Controls.Add((Control) this.groupBox1);
    this.Controls.Add((Control) this.cbOperator);
    this.MinimumSize = new Size(288, 460);
    this.Name = nameof (OwnerConditionForm);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Владелец";
    this.groupBox1.ResumeLayout(false);
    this.groupBox1.PerformLayout();
    this.ResumeLayout(false);
  }

  private class OperatorItem
  {
    public RelationalOperators RelationalOperator { get; private set; }

    public OperatorItem(RelationalOperators relationalOperator)
    {
      this.RelationalOperator = relationalOperator;
    }

    public override string ToString()
    {
      return EnumDescConverter.GetEnumDescription((Enum) this.RelationalOperator);
    }
  }
}
