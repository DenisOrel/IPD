
// Type: Intermech.PropertyEditors.PossibleValuesForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

/// <summary>Summary description for PossibleValuesForm.</summary>
public class PossibleValuesForm : Form
{
  private DataTable dataTable;
  private DataTable resDataTable;
  private FieldTypes fieldType;
  private ArrayList objTypes;
  private string fldName = string.Empty;
  private bool isDateTime;
  private bool isObjectLink;
  private bool isChanged;
  private bool textBoxTextChanged;
  private bool descrBoxTextChanged;
  private bool isFullChanged;
  private bool _BlockOnChange;
  private bool _BlockOnChange_2;
  private TextBox textBox;
  private DateTimePicker dateTimePicker;
  private Button homeBtn;
  private Button upBtn;
  private Button downBtn;
  private Button endBtn;
  private ImageList imageList;
  private Button okBtn;
  private Button addBtn;
  private Button editBtn;
  private Button delBtn;
  private Button cancBtn;
  private Button cancelBtn;
  private TextBox descrBox;
  private ListView listView;
  private ColumnHeader columnValue;
  private ColumnHeader columnDescription;
  private Button descrBtn;
  private ToolTip toolTip;
  private Label label1;
  private Label label2;
  private SplitContainer splitContainer;
  private IContainer components;

  public PossibleValuesForm()
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 1008);
  }

  /// <summary>Clean up any resources being used.</summary>
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
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (PossibleValuesForm));
    this.okBtn = new Button();
    this.cancBtn = new Button();
    this.textBox = new TextBox();
    this.addBtn = new Button();
    this.editBtn = new Button();
    this.delBtn = new Button();
    this.dateTimePicker = new DateTimePicker();
    this.homeBtn = new Button();
    this.imageList = new ImageList(this.components);
    this.upBtn = new Button();
    this.downBtn = new Button();
    this.endBtn = new Button();
    this.cancelBtn = new Button();
    this.descrBox = new TextBox();
    this.listView = new ListView();
    this.columnValue = new ColumnHeader();
    this.columnDescription = new ColumnHeader();
    this.descrBtn = new Button();
    this.toolTip = new ToolTip(this.components);
    this.label1 = new Label();
    this.label2 = new Label();
    this.splitContainer = new SplitContainer();
    this.splitContainer.BeginInit();
    this.splitContainer.Panel1.SuspendLayout();
    this.splitContainer.Panel2.SuspendLayout();
    this.splitContainer.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.okBtn, "okBtn");
    this.okBtn.DialogResult = DialogResult.OK;
    this.okBtn.Name = "okBtn";
    this.okBtn.Click += new EventHandler(this.okBtn_Click);
    componentResourceManager.ApplyResources((object) this.cancBtn, "cancBtn");
    this.cancBtn.DialogResult = DialogResult.Cancel;
    this.cancBtn.Name = "cancBtn";
    componentResourceManager.ApplyResources((object) this.textBox, "textBox");
    this.textBox.Name = "textBox";
    this.textBox.TextChanged += new EventHandler(this.textBox_TextChanged);
    componentResourceManager.ApplyResources((object) this.addBtn, "addBtn");
    this.addBtn.Name = "addBtn";
    this.addBtn.Click += new EventHandler(this.addBtn_Click);
    componentResourceManager.ApplyResources((object) this.editBtn, "editBtn");
    this.editBtn.Name = "editBtn";
    this.editBtn.Click += new EventHandler(this.editBtn_Click);
    componentResourceManager.ApplyResources((object) this.delBtn, "delBtn");
    this.delBtn.Name = "delBtn";
    this.delBtn.Click += new EventHandler(this.delBtn_Click);
    componentResourceManager.ApplyResources((object) this.dateTimePicker, "dateTimePicker");
    this.dateTimePicker.Format = DateTimePickerFormat.Custom;
    this.dateTimePicker.MaxDate = new DateTime(2099, 12, 31 /*0x1F*/, 0, 0, 0, 0);
    this.dateTimePicker.MinDate = new DateTime(1900, 1, 1, 0, 0, 0, 0);
    this.dateTimePicker.Name = "dateTimePicker";
    this.dateTimePicker.Value = new DateTime(2005, 1, 1, 0, 0, 0, 0);
    this.dateTimePicker.ValueChanged += new EventHandler(this.textBox_TextChanged);
    componentResourceManager.ApplyResources((object) this.homeBtn, "homeBtn");
    this.homeBtn.ImageList = this.imageList;
    this.homeBtn.Name = "homeBtn";
    this.homeBtn.Click += new EventHandler(this.homeBtn_Click);
    this.imageList.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageList.ImageStream");
    this.imageList.TransparentColor = Color.Transparent;
    this.imageList.Images.SetKeyName(0, "");
    this.imageList.Images.SetKeyName(1, "");
    this.imageList.Images.SetKeyName(2, "");
    this.imageList.Images.SetKeyName(3, "");
    componentResourceManager.ApplyResources((object) this.upBtn, "upBtn");
    this.upBtn.ImageList = this.imageList;
    this.upBtn.Name = "upBtn";
    this.upBtn.Click += new EventHandler(this.upBtn_Click);
    componentResourceManager.ApplyResources((object) this.downBtn, "downBtn");
    this.downBtn.ImageList = this.imageList;
    this.downBtn.Name = "downBtn";
    this.downBtn.Click += new EventHandler(this.downBtn_Click);
    componentResourceManager.ApplyResources((object) this.endBtn, "endBtn");
    this.endBtn.ImageList = this.imageList;
    this.endBtn.Name = "endBtn";
    this.endBtn.Click += new EventHandler(this.endBtn_Click);
    componentResourceManager.ApplyResources((object) this.cancelBtn, "cancelBtn");
    this.cancelBtn.Name = "cancelBtn";
    this.cancelBtn.Click += new EventHandler(this.cancelBtn_Click);
    componentResourceManager.ApplyResources((object) this.descrBox, "descrBox");
    this.descrBox.Name = "descrBox";
    this.descrBox.TextChanged += new EventHandler(this.descrBox_TextChanged);
    componentResourceManager.ApplyResources((object) this.listView, "listView");
    this.listView.Columns.AddRange(new ColumnHeader[2]
    {
      this.columnValue,
      this.columnDescription
    });
    this.listView.FullRowSelect = true;
    this.listView.GridLines = true;
    this.listView.HideSelection = false;
    this.listView.MultiSelect = false;
    this.listView.Name = "listView";
    this.listView.UseCompatibleStateImageBehavior = false;
    this.listView.View = View.Details;
    this.listView.ColumnClick += new ColumnClickEventHandler(this.listView_ColumnClick);
    this.listView.SelectedIndexChanged += new EventHandler(this.listView_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this.columnValue, "columnValue");
    componentResourceManager.ApplyResources((object) this.columnDescription, "columnDescription");
    componentResourceManager.ApplyResources((object) this.descrBtn, "descrBtn");
    this.descrBtn.Name = "descrBtn";
    this.toolTip.SetToolTip((Control) this.descrBtn, componentResourceManager.GetString("descrBtn.ToolTip"));
    this.descrBtn.UseVisualStyleBackColor = true;
    this.descrBtn.Click += new EventHandler(this.descrBtn_Click);
    this.toolTip.AutomaticDelay = 250;
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    componentResourceManager.ApplyResources((object) this.splitContainer, "splitContainer");
    this.splitContainer.Name = "splitContainer";
    this.splitContainer.Panel1.Controls.Add((Control) this.label1);
    this.splitContainer.Panel1.Controls.Add((Control) this.textBox);
    this.splitContainer.Panel1.Controls.Add((Control) this.dateTimePicker);
    this.splitContainer.Panel2.Controls.Add((Control) this.label2);
    this.splitContainer.Panel2.Controls.Add((Control) this.descrBtn);
    this.splitContainer.Panel2.Controls.Add((Control) this.descrBox);
    this.AcceptButton = (IButtonControl) this.okBtn;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.CancelButton = (IButtonControl) this.cancBtn;
    this.Controls.Add((Control) this.splitContainer);
    this.Controls.Add((Control) this.listView);
    this.Controls.Add((Control) this.cancelBtn);
    this.Controls.Add((Control) this.endBtn);
    this.Controls.Add((Control) this.downBtn);
    this.Controls.Add((Control) this.upBtn);
    this.Controls.Add((Control) this.homeBtn);
    this.Controls.Add((Control) this.delBtn);
    this.Controls.Add((Control) this.editBtn);
    this.Controls.Add((Control) this.addBtn);
    this.Controls.Add((Control) this.cancBtn);
    this.Controls.Add((Control) this.okBtn);
    this.Name = nameof (PossibleValuesForm);
    this.ShowInTaskbar = false;
    this.Load += new EventHandler(this.PossibleValuesForm_Load);
    this.splitContainer.Panel1.ResumeLayout(false);
    this.splitContainer.Panel1.PerformLayout();
    this.splitContainer.Panel2.ResumeLayout(false);
    this.splitContainer.Panel2.PerformLayout();
    this.splitContainer.EndInit();
    this.splitContainer.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private void upBtn_Click(object sender, EventArgs e) => this.MoveItem(mvDirections.mvUp);

  private void homeBtn_Click(object sender, EventArgs e) => this.MoveItem(mvDirections.mvHome);

  private void downBtn_Click(object sender, EventArgs e) => this.MoveItem(mvDirections.mvDown);

  private void endBtn_Click(object sender, EventArgs e) => this.MoveItem(mvDirections.mvEnd);

  private void MoveItem(mvDirections mvMode)
  {
    if (this.listView.SelectedItems.Count == 0)
      return;
    this._BlockOnChange_2 = true;
    try
    {
      ListViewItem selectedItem = this.listView.SelectedItems[0];
      switch (mvMode)
      {
        case mvDirections.mvHome:
          selectedItem.Remove();
          this.listView.Items.Insert(0, selectedItem);
          break;
        case mvDirections.mvUp:
          int index1 = selectedItem.Index;
          selectedItem.Remove();
          this.listView.Items.Insert(index1 - 1, selectedItem);
          break;
        case mvDirections.mvDown:
          int index2 = selectedItem.Index;
          selectedItem.Remove();
          this.listView.Items.Insert(index2 + 1, selectedItem);
          break;
        case mvDirections.mvEnd:
          selectedItem.Remove();
          this.listView.Items.Add(selectedItem);
          break;
      }
      selectedItem.Selected = true;
    }
    finally
    {
      this._BlockOnChange_2 = false;
    }
    this.isFullChanged = true;
    this.UpdateButtons();
  }

  public bool SetData(DataTable dt, FieldTypes ft, ArrayList aObjTypes)
  {
    this.fieldType = ft;
    this.objTypes = (ArrayList) null;
    if (this.fieldType == FieldTypes.ftObjectLink || this.fieldType == FieldTypes.ftObjectLinkByID)
    {
      this.objTypes = aObjTypes;
      if (this.objTypes == null)
        this.objTypes = new ArrayList((ICollection) new int[1]
        {
          -1
        });
      else if (this.objTypes.Count == 0)
        this.objTypes.Add((object) -1);
    }
    this.dataTable = dt;
    if (this.dataTable != null)
    {
      this.CheckOIDfield(this.dataTable);
      this.resDataTable = this.dataTable.Clone();
      this.fldName = ClientCommons.ExtractValueFieldName(this.dataTable);
      return !(this.fldName == string.Empty);
    }
    this.resDataTable = (DataTable) null;
    return false;
  }

  /// <summary>
  /// Проверка наличия поля F_OID.
  /// В случае отсутствия добавление поля и инициализация значений копиями из F_INLIST_ID
  /// </summary>
  /// <param name="dataTable"></param>
  private bool CheckOIDfield(DataTable dataTable)
  {
    bool flag = false;
    if (dataTable != null && dataTable.Columns.IndexOf("F_OID") == -1)
    {
      dataTable.Columns.Add("F_OID", typeof (int));
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        row["F_OID"] = row["F_INLIST_ID"];
      flag = true;
    }
    return flag;
  }

  public DataTable GetData() => this.resDataTable;

  private void PossibleValuesForm_Load(object sender, EventArgs e) => this.FillForm();

  private void ApplyTagToItem(ListViewItem li)
  {
    if (!(li.Tag is PossibleValuesClass))
      return;
    li.Text = ((PossibleValuesClass) li.Tag).ToString();
    li.SubItems[1].Text = ((PossibleValuesClass) li.Tag).Description;
  }

  private void FillForm()
  {
    this.listView.Items.Clear();
    if (this.dataTable != null)
    {
      foreach (DataRow dataRow in this.dataTable.Select("", "F_INLIST_ID ASC"))
      {
        PossibleValuesClass possibleValuesClass = new PossibleValuesClass(dataRow[this.fldName], this.fieldType, dataRow["F_INLIST_ID"], dataRow["F_OID"], dataRow["F_DESCRIPTION"].ToString());
        ListViewItem li = this.listView.Items.Add(string.Empty);
        li.SubItems.Add(string.Empty);
        li.Tag = (object) possibleValuesClass;
        this.ApplyTagToItem(li);
      }
    }
    this.isDateTime = this.fieldType == FieldTypes.ftDateTime;
    this.isObjectLink = this.fieldType == FieldTypes.ftObjectLink || this.fieldType == FieldTypes.ftObjectLinkByID;
    this.dateTimePicker.Visible = this.isDateTime && !this.isObjectLink;
    this.textBox.Visible = !this.isDateTime && !this.isObjectLink;
    this.cancelBtn.Visible = !this.isObjectLink;
    this.descrBtn.Visible = this.isObjectLink;
    this.isChanged = false;
    this.textBoxTextChanged = false;
    this.descrBoxTextChanged = false;
    this.isFullChanged = false;
    if (this.listView.Items.Count > 0)
      this.listView.Items[0].Selected = true;
    this.UpdateButtons();
  }

  private void UpdateButtons() => this.UpdateButtons((object) null);

  private void UpdateButtons(object sender)
  {
    this.addBtn.Enabled = this.isObjectLink || this.isChanged && this.textBoxTextChanged;
    this.editBtn.Enabled = !this.isObjectLink ? this.isChanged && this.listView.SelectedItems.Count != 0 : this.listView.SelectedItems.Count != 0;
    this.delBtn.Enabled = !this.isObjectLink ? !this.isChanged && this.listView.SelectedItems.Count != 0 : this.listView.SelectedItems.Count != 0;
    this.cancelBtn.Enabled = !this.isObjectLink && this.isChanged;
    this.homeBtn.Enabled = !this.isChanged && this.listView.SelectedItems.Count != 0 && this.listView.SelectedItems[0].Index != 0;
    this.upBtn.Enabled = !this.isChanged && this.listView.SelectedItems.Count != 0 && this.listView.SelectedItems[0].Index != 0;
    this.downBtn.Enabled = !this.isChanged && this.listView.SelectedItems.Count != 0 && this.listView.SelectedItems[0].Index != this.listView.Items.Count - 1;
    this.endBtn.Enabled = !this.isChanged && this.listView.SelectedItems.Count != 0 && this.listView.SelectedItems[0].Index != this.listView.Items.Count - 1;
    this.okBtn.Enabled = this.isFullChanged && !this.isChanged;
  }

  private void textBox_TextChanged(object sender, EventArgs e)
  {
    if (this.isObjectLink || this._BlockOnChange)
      return;
    this.isChanged = true;
    this.textBoxTextChanged = true;
    this.UpdateButtons(sender);
  }

  private void descrBox_TextChanged(object sender, EventArgs e)
  {
    if (this.isObjectLink || this._BlockOnChange)
      return;
    this.isChanged = true;
    this.descrBoxTextChanged = true;
    this.UpdateButtons(sender);
  }

  private void cancelBtn_Click(object sender, EventArgs e)
  {
    this.isChanged = false;
    this.textBoxTextChanged = false;
    this.descrBoxTextChanged = false;
    this.listView_SelectedIndexChanged((object) null, (EventArgs) null);
    this.UpdateButtons();
  }

  private void delBtn_Click(object sender, EventArgs e)
  {
    if (this.listView.SelectedItems.Count == 0)
      return;
    int index = this.listView.SelectedItems[0].Index;
    this.isChanged = false;
    this.textBoxTextChanged = false;
    this.descrBoxTextChanged = false;
    this.isFullChanged = true;
    this.listView.Items.RemoveAt(index);
    if (index < this.listView.Items.Count)
      this.listView.Items[index].Selected = true;
    else if (this.listView.Items.Count > 0)
      this.listView.Items[this.listView.Items.Count - 1].Selected = true;
    this.UpdateButtons();
  }

  private PossibleValuesClass GetPossibleValuesClass(PossibleValuesClass lPVC)
  {
    object aInListId = lPVC != null ? lPVC.InListId : (object) DBNull.Value;
    object aOId = lPVC != null ? lPVC.OId : (object) DBNull.Value;
    try
    {
      if (this.fieldType == FieldTypes.ftInteger)
        return new PossibleValuesClass((object) Convert.ToInt64(this.textBox.Text), this.fieldType, aInListId, aOId, this.descrBox.Text);
      if (this.fieldType == FieldTypes.ftDouble)
        return new PossibleValuesClass((object) Convert.ToDouble(this.textBox.Text), this.fieldType, aInListId, aOId, this.descrBox.Text);
      if (this.fieldType == FieldTypes.ftDateTime)
        return new PossibleValuesClass((object) Convert.ToDateTime(this.dateTimePicker.Value.ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern)), this.fieldType, aInListId, aOId, this.descrBox.Text);
      if (this.fieldType == FieldTypes.ftGuid)
        return new PossibleValuesClass((object) new Guid(this.textBox.Text), this.fieldType, aInListId, aOId, this.descrBox.Text);
      if (this.fieldType == FieldTypes.ftBoolean)
        return new PossibleValuesClass((object) BoolSrv.BoolConvert(this.textBox.Text), this.fieldType, aInListId, aOId, this.descrBox.Text);
      if (this.fieldType == FieldTypes.ftObjectLink || this.fieldType == FieldTypes.ftObjectLinkByID)
        return new PossibleValuesClass((object) Convert.ToInt64(-1), this.fieldType, aInListId, aOId, this.descrBox.Text);
      if (this.fieldType == FieldTypes.ftString)
        return new PossibleValuesClass((object) this.textBox.Text, this.fieldType, aInListId, aOId, this.descrBox.Text);
      if (this.fieldType == FieldTypes.ftMeasured)
        return new PossibleValuesClass((object) this.textBox.Text, this.fieldType, aInListId, aOId, this.descrBox.Text);
      throw new Exception(LocalizationHolder.rm.GetString("Client.Core_980"));
    }
    catch
    {
      return (PossibleValuesClass) null;
    }
  }

  private void addBtn_Click(object sender, EventArgs e)
  {
    if (this.isObjectLink)
    {
      IDBObjectID[] dbObjectIdArray = SelectorForm.SelectObjects((int[]) this.objTypes.ToArray(typeof (int)), this.fieldType == FieldTypes.ftObjectLink);
      if (dbObjectIdArray != null)
      {
        for (int index = 0; index < dbObjectIdArray.Length; ++index)
        {
          PossibleValuesClass possibleValuesClass = this.GetPossibleValuesClass((PossibleValuesClass) null);
          if (possibleValuesClass == null)
            return;
          possibleValuesClass.Value = (object) (this.fieldType == FieldTypes.ftObjectLink ? dbObjectIdArray[index].Value : dbObjectIdArray[index].ID);
          int[] numArray = this.CheckForExists(possibleValuesClass.Value);
          if (numArray.Length != 0)
          {
            this.listView.Items[numArray[0]].Selected = true;
          }
          else
          {
            ListViewItem li = this.listView.SelectedItems.Count <= 0 ? this.listView.Items.Add(string.Empty) : this.listView.Items.Insert(this.listView.SelectedItems[this.listView.SelectedItems.Count - 1].Index + 1, string.Empty);
            li.SubItems.Add(string.Empty);
            li.Tag = (object) possibleValuesClass;
            this.ApplyTagToItem(li);
            li.Selected = true;
          }
        }
      }
      this.isFullChanged = true;
      this.UpdateButtons();
    }
    else
    {
      if (!this.isChanged)
        return;
      PossibleValuesClass possibleValuesClass = this.GetPossibleValuesClass((PossibleValuesClass) null);
      if (possibleValuesClass == null)
        return;
      int[] numArray = this.CheckForExists(possibleValuesClass.Value);
      if (numArray.Length != 0)
      {
        this.listView.Items[numArray[0]].Selected = true;
        this.isChanged = false;
        this.textBoxTextChanged = false;
        this.descrBoxTextChanged = false;
      }
      else
      {
        ListViewItem li = this.listView.SelectedItems.Count <= 0 ? this.listView.Items.Add(string.Empty) : this.listView.Items.Insert(this.listView.SelectedItems[this.listView.SelectedItems.Count - 1].Index + 1, string.Empty);
        li.SubItems.Add(string.Empty);
        li.Tag = (object) possibleValuesClass;
        this.ApplyTagToItem(li);
        li.Selected = true;
        this.isChanged = false;
        this.textBoxTextChanged = false;
        this.descrBoxTextChanged = false;
        this.isFullChanged = true;
      }
      this.UpdateButtons();
    }
  }

  private void editBtn_Click(object sender, EventArgs e)
  {
    if (this.listView.SelectedItems.Count == 0)
      return;
    int index = this.listView.SelectedItems[0].Index;
    PossibleValuesClass possibleValuesClass = this.GetPossibleValuesClass((PossibleValuesClass) this.listView.SelectedItems[0].Tag);
    if (possibleValuesClass == null)
      return;
    bool flag = false;
    this._BlockOnChange_2 = true;
    try
    {
      if (this.isObjectLink)
      {
        IDBObjectID[] dbObjectIdArray = SelectorForm.SelectObjects((int[]) this.objTypes.ToArray(typeof (int)), possibleValuesClass.FieldType == FieldTypes.ftObjectLink);
        if (dbObjectIdArray != null)
        {
          int[] numArray = this.CheckForExists((object) (possibleValuesClass.FieldType == FieldTypes.ftObjectLink ? dbObjectIdArray[0].Value : dbObjectIdArray[0].ID));
          if (numArray.Length == 0)
          {
            flag = true;
            possibleValuesClass.Value = (object) dbObjectIdArray[0].Value;
          }
          else
            index = numArray[0];
        }
      }
      else
      {
        int[] numArray = this.CheckForExists(possibleValuesClass.Value);
        if (numArray.Length == 0)
          flag = true;
        else
          index = numArray[0];
      }
      if (flag)
      {
        this.listView.Items[index].Tag = (object) possibleValuesClass;
        this.ApplyTagToItem(this.listView.Items[index]);
      }
      else if (((PossibleValuesClass) this.listView.Items[index].Tag).Description != this.descrBox.Text)
      {
        ((PossibleValuesClass) this.listView.Items[index].Tag).Description = this.descrBox.Text;
        this.ApplyTagToItem(this.listView.Items[index]);
        flag = true;
      }
    }
    finally
    {
      this._BlockOnChange_2 = false;
    }
    this.listView.Items[index].Selected = true;
    this.isChanged = false;
    this.textBoxTextChanged = false;
    this.descrBoxTextChanged = false;
    if (flag)
      this.isFullChanged = true;
    this.UpdateButtons();
  }

  private void descrBtn_Click(object sender, EventArgs e)
  {
    if (this.listView.SelectedItems.Count == 0)
      return;
    int index = this.listView.SelectedItems[0].Index;
    PossibleValuesClass possibleValuesClass = this.GetPossibleValuesClass((PossibleValuesClass) this.listView.SelectedItems[0].Tag);
    if (possibleValuesClass == null || !(this.listView.Items[index].Tag is PossibleValuesClass) || !(((PossibleValuesClass) this.listView.Items[index].Tag).Description != possibleValuesClass.Description))
      return;
    ((PossibleValuesClass) this.listView.Items[index].Tag).Description = possibleValuesClass.Description;
    this.ApplyTagToItem(this.listView.Items[index]);
    this.isChanged = false;
    this.textBoxTextChanged = false;
    this.descrBoxTextChanged = false;
    this.isFullChanged = true;
    this.UpdateButtons();
  }

  private void okBtn_Click(object sender, EventArgs e)
  {
    if (!this.isFullChanged)
      return;
    this.resDataTable.Clear();
    for (int index = 0; index < this.listView.Items.Count; ++index)
    {
      DataRow row = this.resDataTable.NewRow();
      row["F_INLIST_ID"] = (object) index;
      row["F_OID"] = ((PossibleValuesClass) this.listView.Items[index].Tag).OId;
      row[this.fldName] = ((PossibleValuesClass) this.listView.Items[index].Tag).Value;
      row["F_DESCRIPTION"] = (object) ((PossibleValuesClass) this.listView.Items[index].Tag).Description;
      this.resDataTable.Rows.Add(row);
    }
  }

  private void changeBtn_Click(object sender, EventArgs e)
  {
  }

  private int[] CheckForExists(object aValue)
  {
    ArrayList arrayList = new ArrayList();
    for (int index = 0; index < this.listView.Items.Count; ++index)
    {
      if (((PossibleValuesClass) this.listView.Items[index].Tag).Value.Equals(aValue))
        arrayList.Add((object) index);
    }
    return (int[]) arrayList.ToArray(typeof (int));
  }

  private void listView_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this._BlockOnChange_2)
      return;
    this._BlockOnChange = true;
    try
    {
      if (this.listView.SelectedItems.Count != 0)
      {
        if (this.isDateTime)
        {
          try
          {
            this.dateTimePicker.Value = Convert.ToDateTime(((PossibleValuesClass) this.listView.SelectedItems[0].Tag).Value);
          }
          catch
          {
            int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_981"), LocalizationHolder.rm.GetString("Client.Core_82"));
            this.dateTimePicker.Value = DateTime.Today;
          }
        }
        else
        {
          this.textBox.Tag = ((PossibleValuesClass) this.listView.SelectedItems[0].Tag).Value;
          this.textBox.Text = ((PossibleValuesClass) this.listView.SelectedItems[0].Tag).Value.ToString();
        }
        this.descrBox.Text = ((PossibleValuesClass) this.listView.SelectedItems[0].Tag).Description;
      }
      else
      {
        if (this.isDateTime)
        {
          this.dateTimePicker.Value = DateTime.Today;
        }
        else
        {
          this.textBox.Tag = (object) null;
          this.textBox.Text = string.Empty;
        }
        this.descrBox.Text = string.Empty;
      }
    }
    finally
    {
      this._BlockOnChange = false;
    }
    this.isChanged = false;
    this.textBoxTextChanged = false;
    this.descrBoxTextChanged = false;
    this.UpdateButtons();
  }

  private void listView_ColumnClick(object sender, ColumnClickEventArgs e)
  {
    this.listView.ListViewItemSorter = (IComparer) new ListSorter(e.Column, SortOrder.Ascending);
    this.listView.Sort();
    this.listView.ListViewItemSorter = (IComparer) null;
    this.isFullChanged = true;
    this.UpdateButtons();
  }
}
