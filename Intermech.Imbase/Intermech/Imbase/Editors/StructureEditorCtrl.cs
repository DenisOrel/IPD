// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Editors.StructureEditorCtrl
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using ImSSP;
using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Imbase;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.PropertyEditors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Editors;

internal class StructureEditorCtrl : UserControl
{
  private DataSet _newDS;
  private ArrayList _addedGuids = new ArrayList();
  private bool _isColsOrderCahnged;
  private bool _readOnly;
  private IContainer components;
  private Panel _pnlBottom;
  private Button _btnReplace;
  private Button _btnTemplate;
  private Button _btnDel;
  private ImageList _imgList;
  private SplitContainer splitContainer1;
  private PropertyGrid _pgStructure;
  private Panel _pnl;
  private TableLayoutPanel _btnsLayoutPnl;
  private Button _btnTopmost;
  private Button _btnBottommost;
  private Button _btnBottom;
  private Button _btnTop;
  private Button _btnAdd;
  private ListView _lv;
  private ColumnHeader _header;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

  internal DataSet ChangedDataSet => this._newDS;

  internal bool IsColumnsOrderChanged => this._isColsOrderCahnged;

  internal ListView.ListViewItemCollection Items => this._lv.Items;

  internal StructureEditorCtrl()
  {
    this.InitializeComponent();
    this.Dock = DockStyle.Fill;
    this._lv.SmallImageList = Statics.IconSrv.ImageList;
    if (this._lv.Columns.Count <= 0 || this._lv.Columns[0] == null)
      return;
    this._lv.Columns[0].Width = -2;
  }

  internal event EventHandler DataChanged;

  private void On_btnAdd_Click(object sender, EventArgs e)
  {
    bool flag = false;
    using (AttributesSelectDlg attributesSelectDlg = new AttributesSelectDlg(true))
    {
      attributesSelectDlg.ShowCreateAttrBtn = true;
      attributesSelectDlg.SelectorFilter = (ISelectorFilter) new ForbiddenAttrs(this.AddedAttrsIDs());
      attributesSelectDlg.ForbiddenAttrsTypesFilter.AddRange((IEnumerable<FieldTypes>) new FieldTypes[8]
      {
        FieldTypes.ftBlob,
        FieldTypes.ftFile,
        FieldTypes.ftShortBlob,
        FieldTypes.ftSystem,
        FieldTypes.ftExternalLink,
        FieldTypes.ftPassword,
        FieldTypes.ftAutoInc,
        FieldTypes.ftObjectLinkByID
      });
      if (attributesSelectDlg.ShowDialog((IWin32Window) this) != DialogResult.OK || attributesSelectDlg.SelectedAttributesGuid.Count == 0)
        return;
      Dictionary<string, string> dictionary = this.ValidatingAddAttr(new ArrayList((ICollection) attributesSelectDlg.SelectedAttributesGuid));
      foreach (string key in dictionary.Keys)
      {
        ListViewItem listViewItem = this.AddItem(key);
        if (listViewItem != null)
        {
          (listViewItem.Tag as StructureEditorPropGridDescriptor).Formula = dictionary[key];
          flag = true;
        }
      }
    }
    if (!flag)
      return;
    if (this._lv.Items.Count > 0)
    {
      ListViewItem listViewItem = this._lv.Items[this._lv.Items.Count - 1];
      listViewItem.Selected = true;
      this._pgStructure.SelectedObject = listViewItem.Tag;
    }
    else
      this._pgStructure.SelectedObject = (object) null;
    this._btnDel.Enabled = this._btnReplace.Enabled = true;
    this.Changed(new EventArgs());
  }

  private void On_btnDel_Click(object sender, EventArgs e)
  {
    if (this._lv.SelectedItems.Count == 0)
      return;
    ListViewItem selectedItem = this._lv.SelectedItems[0];
    List<string> arrAttr = new List<string>();
    if (!this.ValidateInFormulas(selectedItem.Name, arrAttr))
    {
      StringBuilder stringBuilder = new StringBuilder();
      int num1 = 0;
      foreach (string str in arrAttr)
        stringBuilder.Append($"\n\"{str}\"{(System.ValueType) (char) (++num1 < arrAttr.Count ? 44 : 46)}");
      string caption = LocalizationHolder.rm.GetString("Imbase.StructureEditor.DelAttribute.ErrorCaption");
      int num2 = (int) MessageBox.Show((IWin32Window) this, string.Format(LocalizationHolder.rm.GetString("Imbase.StructureEditor.DelAttribute.ErrorMessage"), (object) stringBuilder.ToString()), caption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    else if (!this.ValidateInDependency(selectedItem.Name, this._lv, arrAttr))
    {
      StringBuilder stringBuilder = new StringBuilder();
      int num3 = 0;
      foreach (string str in arrAttr)
        stringBuilder.Append($"\n\"{str}\"{(System.ValueType) (char) (++num3 < arrAttr.Count ? 44 : 46)}");
      string caption = LocalizationHolder.rm.GetString("Imbase.StructureEditor.DelAttribute.ErrorCaption");
      int num4 = (int) MessageBox.Show((IWin32Window) this, $"Атрибут не может быть исключен, так как используется при расчете зависимостей атрибутов:{stringBuilder.ToString()}", caption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    else
    {
      string caption = LocalizationHolder.rm.GetString("Imbase.StructureEditor.DelAttribute.QuestionCaption");
      if (MessageBox.Show((IWin32Window) this, $"{LocalizationHolder.rm.GetString("Imbase.StructureEditor.DelAttribute.QuestionMessage")} {selectedItem.Text}?", caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
        return;
      foreach (AttributeTypeProperties attTypeProps in StructureEditorPropGridDescriptor.AttTypePropsList)
      {
        if (string.Compare(attTypeProps.AttributeGuid.ToString(), selectedItem.Name) == 0)
        {
          StructureEditorPropGridDescriptor.AttTypePropsList.Remove(attTypeProps);
          break;
        }
      }
      this._addedGuids.Remove((object) selectedItem.Name);
      this._lv.Items.Remove(selectedItem);
      if (this._lv.Items.Count == 0)
      {
        this._pgStructure.SelectedObject = (object) null;
        this._btnDel.Enabled = this._btnReplace.Enabled = false;
        this.Changed((EventArgs) null);
      }
      else
      {
        if (this._lv.SelectedItems.Count == 0)
          this._lv.Items[this._lv.Items.Count - 1].Selected = true;
        this.Changed(new EventArgs());
      }
    }
  }

  private void On_btnReplace_Click(object sender, EventArgs e)
  {
    if (this._lv.SelectedItems.Count == 0)
      return;
    ListViewItem selectedItem = this._lv.SelectedItems[0];
    if (selectedItem.Tag == null)
      return;
    StructureEditorPropGridDescriptor tag1 = selectedItem.Tag as StructureEditorPropGridDescriptor;
    FieldTypes fieldType = tag1.FieldType;
    string name1 = selectedItem.Name;
    using (AttributesSelectDlg attributesSelectDlg = new AttributesSelectDlg(false))
    {
      attributesSelectDlg.SelectorFilter = (ISelectorFilter) new ForbiddenAttrs(this.AddedAttrsIDs());
      attributesSelectDlg.AllowedAttrsTypesFilter = ImbaseAttrsTypesConverter.ListReplaceableTypes(fieldType);
      if (attributesSelectDlg.ShowDialog((IWin32Window) this) != DialogResult.OK)
        return;
      FieldTypes fieldTypes = FieldTypes.ftUnknown;
      string name2 = string.Empty;
      AttributeTypeProperties attributeTypeProperties = new AttributeTypeProperties();
      int id = 0;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(attributesSelectDlg.SelectedAttributesGuid[0]);
        if (attributeType == null)
          return;
        fieldTypes = attributeType.AttributeType;
        name2 = attributeType.Name;
        attributeTypeProperties = attributeType.PropertiesStructure;
        id = attributeType.AttributeID;
      }
      Guid guid = attributesSelectDlg.SelectedAttributesGuid[0];
      string str1 = guid.ToString();
      if (ImbaseAttrsTypesConverter.CompleteReplaceability(fieldType, fieldTypes) == ImbaseAttrsTypesConverter.MatrixValue.Part && tag1.Required == 2)
      {
        using (ReplaceAttributeDialog replaceAttributeDialog = new ReplaceAttributeDialog(this._newDS.Tables["IMS_DATA"], name1, str1, fieldTypes))
        {
          if (!replaceAttributeDialog.ValidatingValues())
          {
            if (replaceAttributeDialog.ShowDialog() == DialogResult.Cancel)
              return;
          }
        }
      }
      selectedItem.ImageIndex = Statics.IconSrv.IndexOf(3, -1, (object) fieldTypes);
      ListViewItem listViewItem1 = selectedItem;
      guid = attributesSelectDlg.SelectedAttributesGuid[0];
      string str2 = guid.ToString();
      listViewItem1.Name = str2;
      selectedItem.Text = name2;
      tag1.AttrTypeProps = attributeTypeProperties;
      tag1.DefaultValue = (object) string.Empty;
      tag1.Units = string.Empty;
      foreach (ListViewItem listViewItem2 in this._lv.Items)
      {
        if (listViewItem2.Tag != null)
        {
          StructureEditorPropGridDescriptor tag2 = listViewItem2.Tag as StructureEditorPropGridDescriptor;
          string formula = tag2.Formula;
          if (!string.IsNullOrEmpty(formula))
            tag2.Formula = formula.Replace(name1, str1);
        }
      }
      DataTable table = this._newDS.Tables["IMS_DATA"];
      if (table != null && table.Columns.Contains(name1))
        table.Columns[name1].ColumnName = str1;
      this._addedGuids.Remove((object) name1);
      this._addedGuids.Add((object) str1);
      tag1.ChangeAttrTypeProps(attributesSelectDlg.SelectedAttributesGuid[0], id, name2, fieldTypes);
    }
    this.Changed(new EventArgs());
    this._pgStructure.Refresh();
  }

  private void On_btnTemplate_Click(object sender, EventArgs e)
  {
    DescriptorCollection descriptors = new DescriptorCollection();
    descriptors.Add((IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(Intermech.Imbase.Consts.ImbaseTableTypeID));
    descriptors.Add((IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(Intermech.Imbase.Consts.ImbaseTableRefTypeID));
    DataSet dataSet = (DataSet) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      long[] catalogsList = (sessionKeeper.Session.GetCustomService(typeof (IImbaseServer)) as IImbaseServer).GetCatalogsList(sessionKeeper.Session.SessionGUID);
      if (catalogsList != null && catalogsList.Length != 0)
      {
        descriptors.Add((IDescriptor) new ImbaseRootNodeDescriptor(new List<long>((IEnumerable<long>) catalogsList)));
        Intermech.Navigator.SelectionWindow.RegisterAnalyze((ISelectedItemsAnalyzer) new ObjectTypesSelectedItemsAnalyzer(new List<int>()
        {
          Intermech.Imbase.Consts.ImbaseTableRefTypeID,
          Intermech.Imbase.Consts.ImbaseTableTypeID
        }, false), true);
      }
      IDBTypedObjectID[] dbTypedObjectIdArray = (IDBTypedObjectID[]) Intermech.Navigator.SelectionWindow.Select(LocalizationHolder.rm.GetString(sc_7783.ssp_imbase_7786()), (IDescriptor) new Intermech.Navigator.CustomNode.Descriptor(LocalizationHolder.rm.GetString("Imbase.Client_110"), descriptors), typeof (IDBTypedObjectID), (System.IServiceProvider) null, SelectionOptions.Default | SelectionOptions.DisableMultiselect);
      if (dbTypedObjectIdArray == null || dbTypedObjectIdArray.Length == 0)
        return;
      dataSet = TableLoadHelper.GetTables(sessionKeeper.Session, dbTypedObjectIdArray[0].ObjectID, true);
      if (dataSet == null)
      {
        long tableReference = TableLoadHelper.GetTableReference(sessionKeeper.Session, dbTypedObjectIdArray[0].ObjectID);
        dataSet = TableLoadHelper.GetTables(sessionKeeper.Session, tableReference, false);
      }
    }
    if (dataSet == null || dataSet.Tables.Count == 0)
      return;
    DataTable table = dataSet.Tables["IMS_ATTR_TYPES"];
    if (table == null)
      return;
    ArrayList NewGuidList = new ArrayList();
    foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
    {
      if (!this._addedGuids.Contains(row["F_ATTRIBUTE_GUID"]))
        NewGuidList.Add(row["F_ATTRIBUTE_GUID"]);
    }
    bool flag = false;
    using (SpecifyVariablesForm specifyVariablesForm = new SpecifyVariablesForm(table, NewGuidList, this._addedGuids, StructureEditorPropGridDescriptor.AttTypePropsList))
    {
      specifyVariablesForm.CreateAttributeList();
      if (specifyVariablesForm.ShowDialog((IWin32Window) this) != DialogResult.OK)
        return;
      Dictionary<string, string> retAttrDictionary = specifyVariablesForm.RetAttrDictionary;
      foreach (string key in retAttrDictionary.Keys)
      {
        ListViewItem listViewItem = this.AddItem(key);
        if (listViewItem != null)
        {
          StructureEditorPropGridDescriptor tag = listViewItem.Tag as StructureEditorPropGridDescriptor;
          DataRow[] dataRowArray = table.Select($"{"F_ATTRIBUTE_GUID"}='{key}'");
          if (dataRowArray.Length != 0)
          {
            tag.Required = Convert.ToInt32(dataRowArray[0]["F_REQUIRED"]);
            tag.Formula = retAttrDictionary[key];
            tag.Unique = Convert.ToInt32(dataRowArray[0]["F_UNIQUE"]);
            tag.DefaultValue = dataRowArray[0]["F_DEFAULT_VALUE"];
            tag.Options = Convert.ToInt32(dataRowArray[0]["F_OPTIONS"]);
            tag.Units = dataRowArray[0]["F_UNITS"] == null || dataRowArray[0]["F_UNITS"] == DBNull.Value ? string.Empty : dataRowArray[0]["F_UNITS"].ToString();
            tag.Computed = Convert.ToInt32(dataRowArray[0]["F_COMPUTED"]);
            flag = true;
          }
        }
      }
    }
    if (!flag)
      return;
    if (this._lv.Items.Count > 0)
    {
      ListViewItem listViewItem = this._lv.Items[this._lv.Items.Count - 1];
      listViewItem.Selected = true;
      this._pgStructure.SelectedObject = listViewItem.Tag;
    }
    else
      this._pgStructure.SelectedObject = (object) null;
    this._btnDel.Enabled = this._btnReplace.Enabled = true;
    this.Changed(new EventArgs());
  }

  private void On_lv_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this._lv.SelectedItems.Count > 0)
    {
      StructureEditorPropGridDescriptor propGridDescriptor = (StructureEditorPropGridDescriptor) null;
      if (this._lv.SelectedItems[0].Tag != null)
      {
        propGridDescriptor = this._lv.SelectedItems[0].Tag as StructureEditorPropGridDescriptor;
        if (!this._readOnly)
          this._btnReplace.Enabled = ImbaseAttrsTypesConverter.IsReplaceableType(propGridDescriptor.FieldType);
      }
      this._pgStructure.SelectedObject = (object) propGridDescriptor;
    }
    this.SetEnabledButtons();
  }

  private void On_pgStructure_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
  {
    this.Changed(new EventArgs());
  }

  private void OnButton_Click(object sender, EventArgs e)
  {
    int index = this._lv.SelectedItems[0].Index;
    switch (Convert.ToInt32((sender as Button).Tag))
    {
      case 0:
        for (int begIndex = index; begIndex > 0; --begIndex)
          this.ChangeRows(begIndex, begIndex - 1);
        break;
      case 1:
        this.ChangeRows(index, index - 1);
        break;
      case 2:
        this.ChangeRows(index, index + 1);
        break;
      case 3:
        for (int begIndex = index; begIndex < this._lv.Items.Count - 1; ++begIndex)
          this.ChangeRows(begIndex, begIndex + 1);
        break;
    }
    this._isColsOrderCahnged = true;
    this.Changed(new EventArgs());
    this.SetEnabledButtons();
  }

  internal void LoadData(DataSet ds, bool readOnly, bool createDescriptors)
  {
    this._newDS = ds;
    this._readOnly = readOnly;
    StructureEditorPropGridDescriptor.AttTypePropsList = new List<AttributeTypeProperties>();
    if (this._readOnly)
    {
      this._btnAdd.Enabled = this._btnDel.Enabled = this._btnTemplate.Enabled = this._btnReplace.Enabled = false;
      this.splitContainer1.Panel2.Enabled = false;
    }
    if (this._newDS.Tables["IMS_ATTR_TYPES"] == null)
      return;
    DataTable table = this._newDS.Tables["IMS_ATTR_TYPES"];
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      int index = 0;
      while (index < table.Rows.Count)
      {
        DataRow row = table.Rows[index];
        string g = row["F_ATTRIBUTE_GUID"].ToString();
        IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(new Guid(g), false);
        if (attributeType == null)
        {
          table.Rows.Remove(row);
        }
        else
        {
          this._addedGuids.Add((object) g);
          ++index;
          int imageIndex = Statics.IconSrv.IndexOf(3, -1, (object) attributeType.AttributeType);
          ListViewItem listViewItem = new ListViewItem(attributeType.Name, imageIndex);
          listViewItem.Name = g;
          this._lv.Items.Add(listViewItem);
          if (createDescriptors)
            listViewItem.Tag = (object) new StructureEditorPropGridDescriptor((Control) this, attributeType.PropertiesStructure.AttributeGuid, Convert.ToInt32(row["F_REQUIRED"]), Convert.ToInt32(row["F_COMPUTED"]), row["F_FORMULA"] == null || row["F_FORMULA"] == DBNull.Value ? string.Empty : row["F_FORMULA"].ToString(), Convert.ToInt32(row["F_UNIQUE"]), row["F_DEFAULT_VALUE"], Convert.ToInt32(row["F_OPTIONS"]), row["F_UNITS"] == null || row["F_UNITS"] == DBNull.Value ? string.Empty : row["F_UNITS"].ToString(), attributeType, this._newDS.Tables["IMS_DATA"], sessionKeeper.Session);
        }
      }
    }
    if (this._lv.Items.Count == 0)
      return;
    this._lv.Items[0].Selected = true;
  }

  internal void SaveData()
  {
    if (this._newDS == null || !this._newDS.Tables.Contains("IMS_DATA") || !this._newDS.Tables.Contains("IMS_ATTR_TYPES"))
      return;
    DataTable table1 = this._newDS.Tables["IMS_ATTR_TYPES"];
    DataTable table2 = this._newDS.Tables["IMS_DATA"];
    List<string> stringList = new List<string>(table2.Columns.Count - 2);
    foreach (DataColumn column in (InternalDataCollectionBase) table2.Columns)
    {
      if (!stringList.Contains(column.ColumnName) && !(column.ColumnName == "F_GUID") && !(column.ColumnName == "F_KEY"))
        stringList.Add(column.ColumnName);
    }
    table1.Clear();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (ListViewItem listViewItem in this._lv.Items)
      {
        if (listViewItem.Tag != null)
        {
          StructureEditorPropGridDescriptor tag = listViewItem.Tag as StructureEditorPropGridDescriptor;
          IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(tag.AttributeGuid, false);
          if (attributeType != null)
          {
            DataRow row = table1.NewRow();
            row["F_ATTRIBUTE_GUID"] = (object) tag.AttributeGuid;
            row["F_REQUIRED"] = (object) tag.Required;
            row["F_COMPUTED"] = (object) tag.Computed;
            row["F_FORMULA"] = (object) tag.Formula;
            row["F_UNIQUE"] = (object) tag.Unique;
            row["F_DEFAULT_VALUE"] = tag.DefaultValue;
            row["F_OPTIONS"] = (object) tag.Options;
            row["F_UNITS"] = (object) tag.Units;
            table1.Rows.Add(row);
            if (tag.Required == Convert.ToInt32((object) RequiredModes.AutoRequired))
            {
              if (stringList.Contains(listViewItem.Name))
                stringList.Remove(listViewItem.Name);
              else
                TableLoadHelper.CreateDataColumn(table2, attributeType);
              if ((tag.MultiValueMode == MultiValueModes.MultiValuesFromList || tag.MultiValueMode == MultiValueModes.SingleValueFromList) && table2.Columns.Contains(listViewItem.Name))
              {
                object filteredPossibleValues = (object) tag.FilteredPossibleValues;
                if (filteredPossibleValues == null)
                  table2.Columns[listViewItem.Name].ExtendedProperties.Remove((object) "F_FILTERED_POSSIBLE_VALUES");
                else
                  table2.Columns[listViewItem.Name].ExtendedProperties[(object) "F_FILTERED_POSSIBLE_VALUES"] = filteredPossibleValues;
              }
              if (tag.MultiValueMode == MultiValueModes.SingleValueFromList && table2.Columns.Contains(listViewItem.Name))
              {
                object dependenPossibleValues = (object) tag.DependenPossibleValues;
                if (dependenPossibleValues != null)
                  table2.Columns[listViewItem.Name].ExtendedProperties[(object) "F_DEPEND_POSSIBLE_VALUES"] = dependenPossibleValues;
                else
                  table2.Columns[listViewItem.Name].ExtendedProperties.Remove((object) "F_DEPEND_POSSIBLE_VALUES");
              }
            }
          }
        }
      }
      foreach (string str in stringList)
      {
        IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(new Guid(str), false);
        if (attributeType == null)
          table2.Columns.Remove(str);
        else
          TableLoadHelper.RemoveDataColumn(table2, attributeType);
      }
    }
  }

  internal void SetVisibleReplaceBtn(bool visible) => this._btnReplace.Visible = visible;

  private List<int> AddedAttrsIDs()
  {
    List<int> intList = new List<int>(this._lv.Items.Count);
    foreach (ListViewItem listViewItem in this._lv.Items)
    {
      if (listViewItem.Tag is StructureEditorPropGridDescriptor tag && !intList.Contains(tag.AttributeID))
        intList.Add(tag.AttributeID);
    }
    return intList;
  }

  private ListViewItem AddItem(string guid)
  {
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(new Guid(guid));
        if (attributeType == null)
          return (ListViewItem) null;
        this._addedGuids.Add((object) guid);
        object defaultValue = (object) null;
        if (attributeType.DefaultValue != DBNull.Value && attributeType.DefaultValue != null)
        {
          DataTable possibleValues = attributeType.GetPossibleValues();
          if (possibleValues != null && possibleValues.Rows.Count > 0)
          {
            foreach (DataRow row in (InternalDataCollectionBase) possibleValues.Rows)
            {
              if (row[2] == DBNull.Value || row[2] == null || string.IsNullOrEmpty(row[2].ToString()))
                row[2] = row[1];
            }
            DataRow[] dataRowArray = possibleValues.Select($"{attributeType.PossibleValueFieldName}='{attributeType.DefaultValue}'");
            if (dataRowArray.Length != 0)
              defaultValue = dataRowArray[0][attributeType.TextFieldName];
          }
          else
            defaultValue = (object) attributeType.DefaultValueDescription;
        }
        else if (attributeType.AttributeType == FieldTypes.ftBoolean)
          defaultValue = (object) false;
        int imageIndex = Statics.IconSrv.IndexOf(3, -1, (object) attributeType.AttributeType);
        return this._lv.Items.Add(new ListViewItem(attributeType.Name, imageIndex)
        {
          Name = attributeType.PropertiesStructure.AttributeGuid.ToString(),
          Tag = (object) new StructureEditorPropGridDescriptor((Control) this, attributeType.PropertiesStructure.AttributeGuid, 2, 0, string.Empty, 0, defaultValue, Convert.ToInt32((object) attributeType.Options), string.Empty, attributeType, this._newDS.Tables["IMS_DATA"], sessionKeeper.Session)
        });
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
    return (ListViewItem) null;
  }

  private void Changed(EventArgs args)
  {
    EventHandler dataChanged = this.DataChanged;
    if (dataChanged == null)
      return;
    dataChanged((object) this, args);
  }

  private void ChangeRows(int begIndex, int endIndex)
  {
    ListViewItem listViewItem = this._lv.Items[endIndex];
    this._lv.Items.Remove(listViewItem);
    this._lv.Items.Insert(begIndex, listViewItem);
  }

  private void SetEnabledButtons()
  {
    if (this._lv.Items.Count > 1 && this._lv.SelectedItems.Count > 0)
    {
      int index = this._lv.SelectedItems[0].Index;
      if (index == 0)
      {
        this._btnBottom.Enabled = this._btnBottommost.Enabled = true;
        this._btnTop.Enabled = this._btnTopmost.Enabled = false;
      }
      else if (index == this._lv.Items.Count - 1)
      {
        this._btnTopmost.Enabled = this._btnTop.Enabled = true;
        this._btnBottom.Enabled = this._btnBottommost.Enabled = false;
      }
      else
        this._btnBottom.Enabled = this._btnBottommost.Enabled = this._btnTopmost.Enabled = this._btnTop.Enabled = true;
    }
    else
      this._btnBottom.Enabled = this._btnBottommost.Enabled = this._btnTopmost.Enabled = this._btnTop.Enabled = false;
    this._btnDel.Enabled = this._btnReplace.Enabled = this._lv.SelectedItems.Count > 0 && !this._readOnly;
  }

  private Dictionary<string, string> ValidatingAddAttr(ArrayList guids)
  {
    using (SpecifyVariablesForm specifyVariablesForm = new SpecifyVariablesForm(guids, this._addedGuids, StructureEditorPropGridDescriptor.AttTypePropsList))
    {
      if (!specifyVariablesForm.VerifyAddedAttributes)
      {
        if (specifyVariablesForm.ShowDialog() != DialogResult.OK)
          goto label_7;
      }
      return specifyVariablesForm.RetAttrDictionary;
    }
label_7:
    return new Dictionary<string, string>();
  }

  private bool ValidateInFormulas(string guid, List<string> arrAttr)
  {
    string str = $"[{guid}]";
    int count = StructureEditorPropGridDescriptor.AttTypePropsList.Count;
    for (int index = 0; index < count; ++index)
    {
      string formula = StructureEditorPropGridDescriptor.AttTypePropsList[index].Formula;
      if (!string.IsNullOrEmpty(formula) && formula.IndexOf(str, StringComparison.InvariantCultureIgnoreCase) >= 0)
        arrAttr.Add(StructureEditorPropGridDescriptor.AttTypePropsList[index].Name);
    }
    return arrAttr.Count == 0;
  }

  private bool ValidateInDependency(string guid, ListView listView, List<string> arrAttr)
  {
    foreach (ListViewItem listViewItem in listView.Items)
    {
      if (listViewItem.Tag != null && listViewItem.Tag is StructureEditorPropGridDescriptor tag && !tag.AttributeGuid.ToString().Equals(guid))
      {
        Tuple<string, List<Tuple<object, object>>> dependenPossibleValues = tag.DependenPossibleValues;
        if (dependenPossibleValues != null && dependenPossibleValues.Item1.Equals(guid))
          arrAttr.Add(tag.Name);
      }
    }
    return arrAttr.Count == 0;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (StructureEditorCtrl));
    this.splitContainer1 = new SplitContainer();
    this._lv = new ListView();
    this._header = new ColumnHeader();
    this._pgStructure = new PropertyGrid();
    this._pnl = new Panel();
    this._btnsLayoutPnl = new TableLayoutPanel();
    this._btnTopmost = new Button();
    this._imgList = new ImageList(this.components);
    this._btnBottommost = new Button();
    this._btnBottom = new Button();
    this._btnTop = new Button();
    this._pnlBottom = new Panel();
    this._btnReplace = new Button();
    this._btnTemplate = new Button();
    this._btnDel = new Button();
    this._btnAdd = new Button();
    this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
    this.splitContainer1.BeginInit();
    this.splitContainer1.Panel1.SuspendLayout();
    this.splitContainer1.Panel2.SuspendLayout();
    this.splitContainer1.SuspendLayout();
    this._btnsLayoutPnl.SuspendLayout();
    this._pnlBottom.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.splitContainer1, "splitContainer1");
    this.splitContainer1.Name = "splitContainer1";
    this.splitContainer1.Panel1.Controls.Add((Control) this._lv);
    this.splitContainer1.Panel2.Controls.Add((Control) this._pgStructure);
    this.splitContainer1.Panel2.Controls.Add((Control) this._pnl);
    this.splitContainer1.Panel2.Controls.Add((Control) this._btnsLayoutPnl);
    componentResourceManager.ApplyResources((object) this.splitContainer1.Panel2, "splitContainer1.Panel2");
    this._lv.Columns.AddRange(new ColumnHeader[1]
    {
      this._header
    });
    componentResourceManager.ApplyResources((object) this._lv, "_lv");
    this._lv.FullRowSelect = true;
    this._lv.HeaderStyle = ColumnHeaderStyle.Nonclickable;
    this._lv.HideSelection = false;
    this._lv.MultiSelect = false;
    this._lv.Name = "_lv";
    this._lv.UseCompatibleStateImageBehavior = false;
    this._lv.View = View.Details;
    this._lv.SelectedIndexChanged += new EventHandler(this.On_lv_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this._header, "_header");
    componentResourceManager.ApplyResources((object) this._pgStructure, "_pgStructure");
    this._pgStructure.Name = "_pgStructure";
    this._pgStructure.ToolbarVisible = false;
    this._pgStructure.PropertyValueChanged += new PropertyValueChangedEventHandler(this.On_pgStructure_PropertyValueChanged);
    componentResourceManager.ApplyResources((object) this._pnl, "_pnl");
    this._pnl.Name = "_pnl";
    componentResourceManager.ApplyResources((object) this._btnsLayoutPnl, "_btnsLayoutPnl");
    this._btnsLayoutPnl.Controls.Add((Control) this._btnTopmost, 0, 0);
    this._btnsLayoutPnl.Controls.Add((Control) this._btnBottommost, 0, 3);
    this._btnsLayoutPnl.Controls.Add((Control) this._btnBottom, 0, 2);
    this._btnsLayoutPnl.Controls.Add((Control) this._btnTop, 0, 1);
    this._btnsLayoutPnl.Name = "_btnsLayoutPnl";
    componentResourceManager.ApplyResources((object) this._btnTopmost, "_btnTopmost");
    this._btnTopmost.ImageList = this._imgList;
    this._btnTopmost.Name = "_btnTopmost";
    this._btnTopmost.Tag = (object) "0";
    this._btnTopmost.UseVisualStyleBackColor = true;
    this._btnTopmost.Click += new EventHandler(this.OnButton_Click);
    this._imgList.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("_imgList.ImageStream");
    this._imgList.TransparentColor = Color.Transparent;
    this._imgList.Images.SetKeyName(0, "Top.ico");
    this._imgList.Images.SetKeyName(1, "Up.ico");
    this._imgList.Images.SetKeyName(2, "Down.ico");
    this._imgList.Images.SetKeyName(3, "Bottom.ico");
    componentResourceManager.ApplyResources((object) this._btnBottommost, "_btnBottommost");
    this._btnBottommost.ImageList = this._imgList;
    this._btnBottommost.Name = "_btnBottommost";
    this._btnBottommost.Tag = (object) "3";
    this._btnBottommost.UseVisualStyleBackColor = true;
    this._btnBottommost.Click += new EventHandler(this.OnButton_Click);
    componentResourceManager.ApplyResources((object) this._btnBottom, "_btnBottom");
    this._btnBottom.ImageList = this._imgList;
    this._btnBottom.Name = "_btnBottom";
    this._btnBottom.Tag = (object) "2";
    this._btnBottom.UseVisualStyleBackColor = true;
    this._btnBottom.Click += new EventHandler(this.OnButton_Click);
    componentResourceManager.ApplyResources((object) this._btnTop, "_btnTop");
    this._btnTop.ImageList = this._imgList;
    this._btnTop.Name = "_btnTop";
    this._btnTop.Tag = (object) "1";
    this._btnTop.UseVisualStyleBackColor = true;
    this._btnTop.Click += new EventHandler(this.OnButton_Click);
    this._pnlBottom.Controls.Add((Control) this._btnReplace);
    this._pnlBottom.Controls.Add((Control) this._btnTemplate);
    this._pnlBottom.Controls.Add((Control) this._btnDel);
    this._pnlBottom.Controls.Add((Control) this._btnAdd);
    componentResourceManager.ApplyResources((object) this._pnlBottom, "_pnlBottom");
    this._pnlBottom.Name = "_pnlBottom";
    componentResourceManager.ApplyResources((object) this._btnReplace, "_btnReplace");
    this._btnReplace.Name = "_btnReplace";
    this._btnReplace.UseVisualStyleBackColor = true;
    this._btnReplace.Click += new EventHandler(this.On_btnReplace_Click);
    componentResourceManager.ApplyResources((object) this._btnTemplate, "_btnTemplate");
    this._btnTemplate.Name = "_btnTemplate";
    this._btnTemplate.UseVisualStyleBackColor = true;
    this._btnTemplate.Click += new EventHandler(this.On_btnTemplate_Click);
    componentResourceManager.ApplyResources((object) this._btnDel, "_btnDel");
    this._btnDel.Name = "_btnDel";
    this._btnDel.UseVisualStyleBackColor = true;
    this._btnDel.Click += new EventHandler(this.On_btnDel_Click);
    componentResourceManager.ApplyResources((object) this._btnAdd, "_btnAdd");
    this._btnAdd.Name = "_btnAdd";
    this._btnAdd.UseVisualStyleBackColor = true;
    this._btnAdd.Click += new EventHandler(this.On_btnAdd_Click);
    this.dataGridViewTextBoxColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    componentResourceManager.ApplyResources((object) this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
    this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
    this.dataGridViewTextBoxColumn1.ReadOnly = true;
    this.dataGridViewTextBoxColumn1.Resizable = DataGridViewTriState.False;
    this.dataGridViewTextBoxColumn1.SortMode = DataGridViewColumnSortMode.NotSortable;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.splitContainer1);
    this.Controls.Add((Control) this._pnlBottom);
    this.DoubleBuffered = true;
    this.Name = nameof (StructureEditorCtrl);
    this.splitContainer1.Panel1.ResumeLayout(false);
    this.splitContainer1.Panel2.ResumeLayout(false);
    this.splitContainer1.Panel2.PerformLayout();
    this.splitContainer1.EndInit();
    this.splitContainer1.ResumeLayout(false);
    this._btnsLayoutPnl.ResumeLayout(false);
    this._pnlBottom.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
