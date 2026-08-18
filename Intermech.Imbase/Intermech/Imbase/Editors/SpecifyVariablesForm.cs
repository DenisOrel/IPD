// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Editors.SpecifyVariablesForm
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Expressions;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Editors;

public class SpecifyVariablesForm : Form
{
  private ArrayList _startingGuidsList;
  private ArrayList _existentGuidsList;
  private List<AttributeTypeProperties> _typePropsList;
  private DataTable _tableData;
  private NewAttrsDictionary _newAttrDict = new NewAttrsDictionary();
  private Dictionary<string, string> _retAttrDict = new Dictionary<string, string>();
  private Parser _parser;
  private IContainer components;
  private Panel panel1;
  private Button btnCancel;
  private Button btnOK;
  private DataGridView dgvAttrList;
  private Button btnFormulaEditor;
  private DataGridViewCheckBoxColumn colAddAttr;
  private DataGridViewTextBoxColumn colAttrName;
  private DataGridViewTextBoxColumn colFormula;
  private ContextMenuStrip contextMenu;
  private ToolStripMenuItem miSelectAll;
  private ToolStripMenuItem miClearAll;
  private ToolStripMenuItem miInvert;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
  private Label _lbMsg;

  public SpecifyVariablesForm(
    ArrayList NewGuidList,
    ArrayList ExistentGuidList,
    List<AttributeTypeProperties> TypePropsList)
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 886);
    this._startingGuidsList = NewGuidList != null ? NewGuidList : new ArrayList();
    this._existentGuidsList = ExistentGuidList != null ? ExistentGuidList : new ArrayList();
    if (TypePropsList != null)
    {
      AttributeTypeProperties[] attributeTypePropertiesArray = new AttributeTypeProperties[TypePropsList.Count];
      TypePropsList.CopyTo(attributeTypePropertiesArray, 0);
      this._typePropsList = new List<AttributeTypeProperties>((IEnumerable<AttributeTypeProperties>) attributeTypePropertiesArray);
    }
    else
      this._typePropsList = new List<AttributeTypeProperties>();
    this._parser = new Parser();
    this._parser.AutoDetectVariables = true;
    this._parser.Validate = false;
  }

  public SpecifyVariablesForm(
    DataTable Table,
    ArrayList NewGuidList,
    ArrayList ExistentGuidList,
    List<AttributeTypeProperties> TypePropsList)
    : this(NewGuidList, ExistentGuidList, TypePropsList)
  {
    this._tableData = Table;
  }

  public bool VerifyAddedAttributes
  {
    get
    {
      foreach (object startingGuids in this._startingGuidsList)
        this.CreateAttributeList(startingGuids, true);
      foreach (DataGridViewRow row in (IEnumerable) this.dgvAttrList.Rows)
      {
        string str = row.Cells["colAttrName"].Value.ToString();
        string text = row.Cells["colFormula"].Value.ToString();
        if (!string.IsNullOrEmpty(text))
        {
          ExpressionTree expressionTree = this._parser.Parse(text);
          if (expressionTree != null)
          {
            foreach (string key in this._newAttrDict.Keys)
            {
              if (!(key == str))
              {
                for (int index = 0; index < expressionTree.Variables.Count; ++index)
                {
                  if (string.Compare(expressionTree.Variables[index].Name, key, true) == 0)
                  {
                    if (expressionTree.Variables[index].Name != key)
                      text = text.Replace(expressionTree.Variables[index].Name, key);
                    this._newAttrDict[key].Add(row.Index);
                    break;
                  }
                }
              }
            }
          }
        }
        row.Cells["colFormula"].Value = (object) text;
        row.Cells["colFormula"].Tag = (object) true;
        if (!this.VerifyFormula(row.Cells["colFormula"].Value.ToString()))
        {
          row.Cells["colFormula"].Style.ForeColor = Color.Red;
          row.Cells["colFormula"].Tag = (object) false;
        }
      }
      bool verifybtnApplyEnabled = this.VerifybtnApplyEnabled;
      this._lbMsg.Visible = !verifybtnApplyEnabled;
      return this.btnOK.Enabled = verifybtnApplyEnabled;
    }
  }

  private bool VerifybtnApplyEnabled
  {
    get
    {
      foreach (DataGridViewRow row in (IEnumerable) this.dgvAttrList.Rows)
      {
        if (!Convert.ToBoolean(row.Cells["colFormula"].Tag))
          return false;
      }
      return true;
    }
  }

  public Dictionary<string, string> RetAttrDictionary
  {
    get
    {
      foreach (DataGridViewRow row in (IEnumerable) this.dgvAttrList.Rows)
      {
        if (Convert.ToBoolean(row.Cells["colAddAttr"].EditedFormattedValue))
        {
          AttributeTypeProperties tag = (AttributeTypeProperties) row.Tag;
          this._retAttrDict.Add(row.Cells["colAttrName"].Tag.ToString(), tag.Formula);
        }
      }
      return this._retAttrDict;
    }
  }

  private void CreateAttributeList(object guid, bool attrChecked)
  {
    if (this._existentGuidsList.Contains((object) guid.ToString()) || this._startingGuidsList.Contains((object) guid.ToString()) && !attrChecked || this._newAttrDict.ContainsKey((object) guid.ToString()))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      try
      {
        IDBAttributeType attributeType1 = sessionKeeper.Session.GetAttributeType(new Guid(guid.ToString()), false);
        if (attributeType1 == null || this._newAttrDict.ContainsKey((object) attributeType1.Name))
          return;
        DataGridViewRowCollection rows = this.dgvAttrList.Rows;
        DataGridViewRow dataGridViewRow1 = new DataGridViewRow();
        dataGridViewRow1.CreateCells(this.dgvAttrList);
        dataGridViewRow1.Cells[0].Value = (object) attrChecked;
        dataGridViewRow1.Cells[1].Value = (object) attributeType1.Name;
        dataGridViewRow1.Cells[1].Tag = (object) attributeType1.PropertiesStructure.AttributeGuid;
        dataGridViewRow1.Cells[2].Value = (object) attributeType1.Formula;
        AttributeTypeProperties propertiesStructure = attributeType1.PropertiesStructure;
        dataGridViewRow1.Tag = (object) propertiesStructure;
        DataGridViewRow dataGridViewRow2 = dataGridViewRow1;
        rows.Add(dataGridViewRow2);
        this._newAttrDict.Add(attributeType1.Name, new List<int>());
        if (attrChecked && !this._typePropsList.Contains(propertiesStructure))
          this._typePropsList.Add(propertiesStructure);
        if (string.IsNullOrEmpty(attributeType1.Formula))
          return;
        ExpressionTree expressionTree = this._parser.Parse(attributeType1.Formula);
        if (expressionTree == null)
          return;
        for (int index = 0; index < expressionTree.Variables.Count; ++index)
        {
          IDBAttributeType attributeType2 = sessionKeeper.Session.GetAttributeType(expressionTree.Variables[index].ToString(), false);
          if (attributeType2 != null)
            this.CreateAttributeList((object) attributeType2.PropertiesStructure.AttributeGuid, false);
        }
      }
      catch (Exception ex)
      {
        ExceptionHelper.ExceptionService.ShowException(ex);
      }
    }
  }

  private void OnbtnOK_Click(object sender, EventArgs e)
  {
    AttributeTypeProperties[] array = this._typePropsList.ToArray();
    foreach (DataGridViewRow row in (IEnumerable) this.dgvAttrList.Rows)
    {
      if (Convert.ToBoolean(row.Cells["colAddAttr"].EditedFormattedValue))
      {
        string strFormula = row.Cells["colFormula"].Value.ToString();
        if (!string.IsNullOrEmpty(strFormula))
        {
          AttributeTypeProperties tag = (AttributeTypeProperties) row.Tag with
          {
            Formula = TableEditor.RenameFormulaFields(this.ParseFinishFormula(strFormula), array, false)
          };
          row.Tag = (object) tag;
        }
      }
    }
  }

  private void OnbtnFormulaEditor_Click(object sender, EventArgs e)
  {
    if (this.dgvAttrList.SelectedRows.Count <= 0)
      return;
    string expression = this.dgvAttrList.SelectedRows[0].Cells["colFormula"].Value.ToString();
    int attributeId = ((AttributeTypeProperties) this.dgvAttrList.SelectedRows[0].Tag).AttributeID;
    if (!ExpressionEditor.EditExpression(ref expression, this._typePropsList.ToArray(), attributeId, (ParseEventHandler) null))
      return;
    this.dgvAttrList.SelectedRows[0].Cells["colFormula"].Value = (object) expression;
    foreach (string key in this._newAttrDict.Keys)
    {
      if (this._newAttrDict[key].Contains(this.dgvAttrList.SelectedRows[0].Index))
        this._newAttrDict[key].Remove(this.dgvAttrList.SelectedRows[0].Index);
    }
    if (!string.IsNullOrEmpty(expression))
    {
      ExpressionTree expressionTree = this._parser.Parse(expression);
      if (expressionTree != null)
      {
        for (int index = 0; index < expressionTree.Variables.Count; ++index)
        {
          if (this._newAttrDict.ContainsKey((object) expressionTree.Variables[index].ToString()))
            this._newAttrDict[expressionTree.Variables[index].ToString()].Add(this.dgvAttrList.SelectedRows[0].Index);
        }
      }
    }
    this.dgvAttrList.SelectedRows[0].Cells["colFormula"].Style.ForeColor = Color.Black;
    this.dgvAttrList.SelectedRows[0].Cells["colFormula"].Tag = (object) true;
    this._lbMsg.Visible = !(this.btnOK.Enabled = this.VerifybtnApplyEnabled);
  }

  private void OndgvAttrList_CellContentClick(object sender, DataGridViewCellEventArgs e)
  {
    if (e.RowIndex < 0 || e.ColumnIndex < 0 || e.ColumnIndex != 0)
      return;
    AttributeTypeProperties tag = (AttributeTypeProperties) this.dgvAttrList.Rows[e.RowIndex].Tag;
    if (Convert.ToBoolean((sender as DataGridView).CurrentCell.EditedFormattedValue))
    {
      if (!this._typePropsList.Contains(tag))
        this._typePropsList.Add(tag);
      foreach (int index in this._newAttrDict[tag.Name])
      {
        if (this.VerifyFormula(this.dgvAttrList.Rows[index].Cells["colFormula"].Value.ToString()))
        {
          this.dgvAttrList.Rows[index].Cells["colFormula"].Style.ForeColor = Color.Black;
          this.dgvAttrList.Rows[index].Cells["colFormula"].Tag = (object) true;
        }
      }
      if (!this.VerifyFormula(this.dgvAttrList.Rows[e.RowIndex].Cells["colFormula"].Value.ToString()))
      {
        this.dgvAttrList.Rows[e.RowIndex].Cells["colFormula"].Style.ForeColor = Color.Red;
        this.dgvAttrList.Rows[e.RowIndex].Cells["colFormula"].Tag = (object) false;
      }
    }
    else
    {
      if (this._typePropsList.Contains(tag))
        this._typePropsList.Remove(tag);
      foreach (int index in this._newAttrDict[tag.Name])
      {
        if (Convert.ToBoolean(this.dgvAttrList.Rows[index].Cells["colAddAttr"].Value))
        {
          this.dgvAttrList.Rows[index].Cells["colFormula"].Style.ForeColor = Color.Red;
          this.dgvAttrList.Rows[index].Cells["colFormula"].Tag = (object) false;
        }
      }
      this.dgvAttrList.Rows[e.RowIndex].Cells["colFormula"].Style.ForeColor = Color.Black;
      this.dgvAttrList.Rows[e.RowIndex].Cells["colFormula"].Tag = (object) true;
    }
    this._lbMsg.Visible = !(this.btnOK.Enabled = this.VerifybtnApplyEnabled);
  }

  private void OnMenuItem_Click(object sender, EventArgs e)
  {
    ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
    int index = this.dgvAttrList.SelectedRows[0].Index;
    this.dgvAttrList.BeginEdit(false);
    switch (Convert.ToInt32(toolStripMenuItem.Tag))
    {
      case 0:
        IEnumerator enumerator1 = ((IEnumerable) this.dgvAttrList.Rows).GetEnumerator();
        try
        {
          while (enumerator1.MoveNext())
          {
            DataGridViewRow current = (DataGridViewRow) enumerator1.Current;
            if (!Convert.ToBoolean(current.Cells["colAddAttr"].Value))
            {
              current.Cells["colAddAttr"].Value = (object) true;
              this.dgvAttrList.CurrentCell = current.Cells["colAddAttr"];
              this.OndgvAttrList_CellContentClick((object) this.dgvAttrList, new DataGridViewCellEventArgs(0, current.Index));
            }
          }
          break;
        }
        finally
        {
          if (enumerator1 is IDisposable disposable)
            disposable.Dispose();
        }
      case 1:
        IEnumerator enumerator2 = ((IEnumerable) this.dgvAttrList.Rows).GetEnumerator();
        try
        {
          while (enumerator2.MoveNext())
          {
            DataGridViewRow current = (DataGridViewRow) enumerator2.Current;
            if (Convert.ToBoolean(current.Cells["colAddAttr"].Value))
            {
              current.Cells["colAddAttr"].Value = (object) false;
              this.dgvAttrList.CurrentCell = current.Cells["colAddAttr"];
              this.OndgvAttrList_CellContentClick((object) this.dgvAttrList, new DataGridViewCellEventArgs(0, current.Index));
            }
          }
          break;
        }
        finally
        {
          if (enumerator2 is IDisposable disposable)
            disposable.Dispose();
        }
      case 2:
        IEnumerator enumerator3 = ((IEnumerable) this.dgvAttrList.Rows).GetEnumerator();
        try
        {
          while (enumerator3.MoveNext())
          {
            DataGridViewRow current = (DataGridViewRow) enumerator3.Current;
            current.Cells["colAddAttr"].Value = (object) !Convert.ToBoolean(current.Cells["colAddAttr"].Value);
            this.dgvAttrList.CurrentCell = current.Cells["colAddAttr"];
            this.OndgvAttrList_CellContentClick((object) this.dgvAttrList, new DataGridViewCellEventArgs(0, current.Index));
          }
          break;
        }
        finally
        {
          if (enumerator3 is IDisposable disposable)
            disposable.Dispose();
        }
    }
    this.dgvAttrList.Rows[index].Selected = true;
    this.dgvAttrList.EndEdit();
  }

  private string ParseFinishFormula(string strFormula)
  {
    ExpressionTree expressionTree = this._parser.Parse(strFormula);
    if (expressionTree == null)
      return strFormula;
    bool flag1 = false;
    for (int index = 0; index < expressionTree.Variables.Count; ++index)
    {
      string newValue = $"[{expressionTree.Variables[index].Name}]";
      if (strFormula.IndexOf(newValue) == -1)
      {
        strFormula = strFormula.Replace(expressionTree.Variables[index].Name, newValue);
        flag1 = true;
      }
    }
    if (flag1)
    {
      int num1 = 0;
      bool flag2 = false;
      while (num1 < strFormula.Length)
      {
        if (strFormula[num1] == '[')
        {
          if (flag2)
          {
            strFormula = strFormula.Remove(num1, 1);
            continue;
          }
          flag2 = true;
        }
        else if (strFormula[num1] == ']')
          flag2 = false;
        ++num1;
      }
      int num2 = strFormula.Length - 1;
      while (num2 > -1)
      {
        if (strFormula[num2] == ']')
        {
          if (flag2)
          {
            strFormula = strFormula.Remove(num2, 1);
            continue;
          }
          flag2 = true;
        }
        else if (strFormula[num2] == '[')
          flag2 = false;
        --num2;
      }
    }
    return strFormula;
  }

  private bool VerifyFormula(string Formula)
  {
    if (!string.IsNullOrEmpty(Formula))
    {
      ExpressionTree expressionTree = this._parser.Parse(Formula);
      if (expressionTree == null)
        return true;
      for (int index = 0; index < expressionTree.Variables.Count; ++index)
      {
        string key = expressionTree.Variables[index].ToString();
        if (this._newAttrDict.ContainsKey((object) key) && !Convert.ToBoolean(this.dgvAttrList.Rows[this._newAttrDict.KeyPosition(key)].Cells["colAddAttr"].EditedFormattedValue))
          return false;
      }
    }
    return true;
  }

  public void CreateAttributeList()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (object startingGuids in this._startingGuidsList)
      {
        try
        {
          Guid anAttributeGuid = new Guid(startingGuids.ToString());
          IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(anAttributeGuid, false);
          if (attributeType != null)
          {
            DataRow[] dataRowArray = this._tableData.Select($"{"F_ATTRIBUTE_GUID"}='{startingGuids}'");
            if (dataRowArray.Length != 0)
            {
              DataGridViewRowCollection rows = this.dgvAttrList.Rows;
              DataGridViewRow dataGridViewRow1 = new DataGridViewRow();
              dataGridViewRow1.CreateCells(this.dgvAttrList);
              dataGridViewRow1.Cells[0].Value = (object) true;
              dataGridViewRow1.Cells[1].Value = (object) attributeType.Name;
              dataGridViewRow1.Cells[1].Tag = (object) anAttributeGuid;
              dataGridViewRow1.Cells[2].Value = (object) dataRowArray[0]["F_FORMULA"].ToString();
              AttributeTypeProperties attributeTypeProperties = new AttributeTypeProperties(attributeType.Name, attributeType.AttributeType);
              attributeTypeProperties.AttributeID = attributeType.AttributeID;
              attributeTypeProperties.AttributeGuid = anAttributeGuid;
              dataGridViewRow1.Tag = (object) attributeTypeProperties;
              DataGridViewRow dataGridViewRow2 = dataGridViewRow1;
              rows.Add(dataGridViewRow2);
              this._newAttrDict.Add(attributeType.Name, new List<int>());
              this._typePropsList.Add(attributeTypeProperties);
            }
          }
        }
        catch (Exception ex)
        {
          ExceptionHelper.ExceptionService.ShowException(ex);
        }
      }
      AttributeTypeProperties[] array = this._typePropsList.ToArray();
      foreach (DataGridViewRow row in (IEnumerable) this.dgvAttrList.Rows)
      {
        string formula = row.Cells["colFormula"].Value.ToString();
        if (!string.IsNullOrEmpty(formula))
        {
          string text = TableEditor.RenameFormulaFields(formula, array, true);
          row.Cells["colFormula"].Value = (object) text;
          ExpressionTree expressionTree = this._parser.Parse(text);
          if (expressionTree != null)
          {
            foreach (string key in this._newAttrDict.Keys)
            {
              for (int index = 0; index < expressionTree.Variables.Count; ++index)
              {
                if (string.Compare(expressionTree.Variables[index].Name, key) == 0)
                {
                  this._newAttrDict[key].Add(row.Index);
                  break;
                }
              }
            }
          }
        }
        row.Cells["colFormula"].Tag = (object) true;
        if (!this.VerifyFormula(row.Cells["colFormula"].Value.ToString()))
        {
          row.Cells["colFormula"].Style.ForeColor = Color.Red;
          row.Cells["colFormula"].Tag = (object) false;
        }
      }
      this._lbMsg.Visible = !(this.btnOK.Enabled = this.VerifybtnApplyEnabled);
    }
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this._parser?.Dispose();
      this._parser = (Parser) null;
      this.components?.Dispose();
    }
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SpecifyVariablesForm));
    DataGridViewCellStyle gridViewCellStyle1 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle2 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle3 = new DataGridViewCellStyle();
    this.panel1 = new Panel();
    this.btnFormulaEditor = new Button();
    this.btnCancel = new Button();
    this.btnOK = new Button();
    this.dgvAttrList = new DataGridView();
    this.colAddAttr = new DataGridViewCheckBoxColumn();
    this.colAttrName = new DataGridViewTextBoxColumn();
    this.colFormula = new DataGridViewTextBoxColumn();
    this.contextMenu = new ContextMenuStrip(this.components);
    this.miSelectAll = new ToolStripMenuItem();
    this.miClearAll = new ToolStripMenuItem();
    this.miInvert = new ToolStripMenuItem();
    this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
    this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
    this._lbMsg = new Label();
    this.panel1.SuspendLayout();
    ((ISupportInitialize) this.dgvAttrList).BeginInit();
    this.contextMenu.SuspendLayout();
    this.SuspendLayout();
    this.panel1.Controls.Add((Control) this.btnFormulaEditor);
    this.panel1.Controls.Add((Control) this.btnCancel);
    this.panel1.Controls.Add((Control) this.btnOK);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.btnFormulaEditor, "btnFormulaEditor");
    this.btnFormulaEditor.Name = "btnFormulaEditor";
    this.btnFormulaEditor.UseVisualStyleBackColor = true;
    this.btnFormulaEditor.Click += new EventHandler(this.OnbtnFormulaEditor_Click);
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.btnOK, "btnOK");
    this.btnOK.DialogResult = DialogResult.OK;
    this.btnOK.Name = "btnOK";
    this.btnOK.UseVisualStyleBackColor = true;
    this.btnOK.Click += new EventHandler(this.OnbtnOK_Click);
    this.dgvAttrList.AllowUserToAddRows = false;
    this.dgvAttrList.AllowUserToDeleteRows = false;
    this.dgvAttrList.AllowUserToResizeRows = false;
    this.dgvAttrList.BackgroundColor = SystemColors.Window;
    this.dgvAttrList.BorderStyle = BorderStyle.None;
    this.dgvAttrList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
    this.dgvAttrList.Columns.AddRange((DataGridViewColumn) this.colAddAttr, (DataGridViewColumn) this.colAttrName, (DataGridViewColumn) this.colFormula);
    this.dgvAttrList.ContextMenuStrip = this.contextMenu;
    componentResourceManager.ApplyResources((object) this.dgvAttrList, "dgvAttrList");
    this.dgvAttrList.MultiSelect = false;
    this.dgvAttrList.Name = "dgvAttrList";
    this.dgvAttrList.RowHeadersVisible = false;
    this.dgvAttrList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
    this.dgvAttrList.CellContentClick += new DataGridViewCellEventHandler(this.OndgvAttrList_CellContentClick);
    this.colAddAttr.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
    gridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
    gridViewCellStyle1.NullValue = (object) false;
    gridViewCellStyle1.Padding = new Padding(6, 0, 0, 0);
    this.colAddAttr.DefaultCellStyle = gridViewCellStyle1;
    componentResourceManager.ApplyResources((object) this.colAddAttr, "colAddAttr");
    this.colAddAttr.Name = "colAddAttr";
    this.colAddAttr.Resizable = DataGridViewTriState.False;
    gridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
    gridViewCellStyle2.Padding = new Padding(6, 0, 0, 0);
    this.colAttrName.DefaultCellStyle = gridViewCellStyle2;
    componentResourceManager.ApplyResources((object) this.colAttrName, "colAttrName");
    this.colAttrName.Name = "colAttrName";
    this.colAttrName.ReadOnly = true;
    this.colFormula.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    componentResourceManager.ApplyResources((object) this.colFormula, "colFormula");
    this.colFormula.Name = "colFormula";
    this.colFormula.ReadOnly = true;
    this.colFormula.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.contextMenu.Items.AddRange(new ToolStripItem[3]
    {
      (ToolStripItem) this.miSelectAll,
      (ToolStripItem) this.miClearAll,
      (ToolStripItem) this.miInvert
    });
    this.contextMenu.Name = "contextMenu";
    componentResourceManager.ApplyResources((object) this.contextMenu, "contextMenu");
    this.miSelectAll.Name = "miSelectAll";
    componentResourceManager.ApplyResources((object) this.miSelectAll, "miSelectAll");
    this.miSelectAll.Tag = (object) "0";
    this.miSelectAll.Click += new EventHandler(this.OnMenuItem_Click);
    this.miClearAll.Name = "miClearAll";
    componentResourceManager.ApplyResources((object) this.miClearAll, "miClearAll");
    this.miClearAll.Tag = (object) "1";
    this.miClearAll.Click += new EventHandler(this.OnMenuItem_Click);
    this.miInvert.Name = "miInvert";
    componentResourceManager.ApplyResources((object) this.miInvert, "miInvert");
    this.miInvert.Tag = (object) "2";
    this.miInvert.Click += new EventHandler(this.OnMenuItem_Click);
    gridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
    gridViewCellStyle3.Padding = new Padding(6, 0, 0, 0);
    this.dataGridViewTextBoxColumn1.DefaultCellStyle = gridViewCellStyle3;
    componentResourceManager.ApplyResources((object) this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
    this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
    this.dataGridViewTextBoxColumn1.ReadOnly = true;
    this.dataGridViewTextBoxColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    componentResourceManager.ApplyResources((object) this.dataGridViewTextBoxColumn2, "dataGridViewTextBoxColumn2");
    this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
    this.dataGridViewTextBoxColumn2.ReadOnly = true;
    this.dataGridViewTextBoxColumn2.SortMode = DataGridViewColumnSortMode.NotSortable;
    componentResourceManager.ApplyResources((object) this._lbMsg, "_lbMsg");
    this._lbMsg.ForeColor = Color.Red;
    this._lbMsg.Name = "_lbMsg";
    this.AcceptButton = (IButtonControl) this.btnOK;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.Controls.Add((Control) this.dgvAttrList);
    this.Controls.Add((Control) this._lbMsg);
    this.Controls.Add((Control) this.panel1);
    this.HelpButton = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (SpecifyVariablesForm);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.panel1.ResumeLayout(false);
    this.panel1.PerformLayout();
    ((ISupportInitialize) this.dgvAttrList).EndInit();
    this.contextMenu.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
