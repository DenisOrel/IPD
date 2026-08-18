// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Common_Dialogs.ArticleWithDocForm.NonDraftGroupMasterControl
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using DevExpress.IM.XtraEditors.Controls;
using DevExpress.IM.XtraEditors.Repository;
using DevExpress.IM.XtraTreeList;
using DevExpress.IM.XtraTreeList.Columns;
using DevExpress.IM.XtraTreeList.Nodes;
using Intermech.AVS.Properties;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using Intermech.PropertyEditors.AttrProcessor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS.Common_Dialogs.ArticleWithDocForm;

/// <summary>Главная закладка для записи групповой спецификации</summary>
internal class NonDraftGroupMasterControl : PageUserControl
{
  /// <summary>Текущий индекс в списке связей</summary>
  private int _index;
  /// <summary>Текущие значения связей с исполнениями</summary>
  private List<NonDraftGroupMasterControl.RelationCounts> _quantities;
  /// <summary>Структура с информацией по объектам для классификации</summary>
  private ClassificatedObjects _classifObjects;
  private List<AttributeProcessor> _attrProcessors;
  private RepositoryItemButtonEdit be = new RepositoryItemButtonEdit();
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Label lFullName;
  private Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox tbOKPCode;
  private Label label11;
  private Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox tbSmotri;
  private Label label10;
  private Button bEditName;
  private Button bEditDesignation;
  private Label label2;
  private Label label1;
  private Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox tbName;
  private Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox tbDesignation;
  private Panel panel1;
  private Button bClassificate;
  private Panel panel2;
  private Label label5;
  private Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox tbNote;
  private Label label7;
  private Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox tbZone;
  private Label label6;
  private Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox tbPosDesignation;
  private Label label4;
  private Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox tbPosition;
  private TreeList treeList1;
  private TreeListColumn treeListColumn1;
  private TreeListColumn treeListColumn2;
  private Button bEditCount;
  private Label label3;
  private Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox tbCount;
  private Button bEditMaterial;
  private Label label8;
  private Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox tbSize;
  private Label label9;
  private Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox tbMaterial;
  private CheckBox cbPodbor;
  private ToolTip toolTip1;

  public NonDraftGroupMasterControl(
    List<IDBRelation> relations,
    ClassificatedObjects classifObjects,
    CommonDataType disableControls,
    List<AVSRow> selectedSpecRows)
  {
    this.InitializeComponent();
    this.Init(relations, classifObjects, disableControls, selectedSpecRows);
  }

  internal void Init(
    List<IDBRelation> relations,
    ClassificatedObjects classifObjects,
    CommonDataType disableControls,
    List<AVSRow> selectedSpecRows)
  {
    this.Init(-1L, AttributableElements.Relation, disableControls);
    this.treeListColumn2.ColumnEdit = (RepositoryItem) this.be;
    this.be.ButtonClick -= new ButtonPressedEventHandler(this.be_ButtonClick);
    this.be.Validating -= new CancelEventHandler(this.be_Validating);
    this.be.ParseEditValue -= new ConvertEditValueEventHandler(this.be_ParseEditValue);
    this.be.ButtonClick += new ButtonPressedEventHandler(this.be_ButtonClick);
    this.be.TextEditStyle = TextEditStyles.Standard;
    this.be.Validating += new CancelEventHandler(this.be_Validating);
    this.be.ParseEditValue += new ConvertEditValueEventHandler(this.be_ParseEditValue);
    if (selectedSpecRows != null && selectedSpecRows.Count > 0)
      this.selectedSpecRow = selectedSpecRows[0];
    if (!AvsConfig.PositionDesignation.ShowPosDesignation)
    {
      this.label6.Visible = false;
      this.tbPosDesignation.Visible = false;
    }
    else
    {
      this.label6.Visible = true;
      this.tbPosDesignation.Visible = true;
    }
    this.cbPodbor.Visible = AvsConfig.Podbor.ShowPodbor;
    this._classifObjects = classifObjects;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (this.selectedSpecRow != null && this.selectedSpecRow.avsDocument.productsInfo != null)
      {
        this._quantities = new List<NonDraftGroupMasterControl.RelationCounts>(this.selectedSpecRow.avsDocument.productsInfo.Count);
        for (int index1 = 0; index1 < this.selectedSpecRow.avsDocument.productsInfo.Count; ++index1)
        {
          ProductInfo productInfo = this.selectedSpecRow.avsDocument.productsInfo[index1];
          IDBRelation dbRelation = (IDBRelation) null;
          for (int index2 = 0; index2 < relations.Count; ++index2)
          {
            if (relations[index2].ProjID == productInfo.Id)
            {
              dbRelation = relations[index2];
              break;
            }
          }
          NonDraftGroupMasterControl.RelationCounts relationCounts = new NonDraftGroupMasterControl.RelationCounts();
          relationCounts.InstanceCaption = productInfo.Designation;
          relationCounts.InstanceID = productInfo.Id;
          if (dbRelation != null)
          {
            relationCounts.RelationID = dbRelation.RelationID;
            IDBAttribute attributeById = dbRelation.GetAttributeByID(FormHelper.AttributeCountID);
            if (CompareValuesHelper.NormalizedValue(attributeById.Value) != null)
              relationCounts.Count = attributeById.Value as MeasuredValue;
          }
          this._quantities.Add(relationCounts);
        }
      }
      else
      {
        this._quantities = new List<NonDraftGroupMasterControl.RelationCounts>(relations.Count);
        for (int index = 0; index < relations.Count; ++index)
        {
          QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(relations[index].ProjID);
          if (!objectInfo.Empty)
          {
            NonDraftGroupMasterControl.RelationCounts relationCounts = new NonDraftGroupMasterControl.RelationCounts();
            relationCounts.InstanceID = relations[index].ProjID;
            relationCounts.InstanceCaption = objectInfo.Caption;
            relationCounts.RelationID = relations[index].RelationID;
            IDBAttribute attributeById = relations[index].GetAttributeByID(FormHelper.AttributeCountID);
            if (CompareValuesHelper.NormalizedValue(attributeById.Value) != null)
              relationCounts.Count = attributeById.Value as MeasuredValue;
            this._quantities.Add(relationCounts);
          }
        }
      }
      this._attrProcessors = new List<AttributeProcessor>(relations.Count);
      for (int index = 0; index < this._quantities.Count; ++index)
        this._attrProcessors.Add(new AttributeProcessor(this._quantities[index].RelationID, AttributableElements.Relation, true));
    }
  }

  private void be_ParseEditValue(object sender, ConvertEditValueEventArgs e)
  {
    if (!this.ChangeCount((TextBox) this.tbCount, this.OnLeaveCount(e, this._quantities[this._index].Count), ref this._quantities[this._index].Count))
      return;
    this.ChangeCountIntoGrid();
  }

  private void be_Validating(object sender, CancelEventArgs e)
  {
  }

  private void be_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    if (!this.ChangeCount((TextBox) this.tbCount, this.OnEditCount(this.treeList1, this._quantities[this._index].Count), ref this._quantities[this._index].Count))
      return;
    this.ChangeCountIntoGrid();
  }

  public override void Save(IUserSession session, OpenModes mode, CreatedPair pair)
  {
    this.commonData.Zona = this.tbZone.Text;
    this.commonData.Position = this.tbPosition.Text;
    this.commonData.Note = this.tbNote.Text;
    this.commonData.PosDesignation = this.tbPosDesignation.Text;
    this.commonData.Podbor = this.cbPodbor.Checked;
    if (mode == OpenModes.InView && !this.changed)
      return;
    if (this.selectedSpecRow != null && this.selectedSpecRow.avsDocument != null)
      this.selectedSpecRow.avsDocument.SuspendDocumentAndGridUpdates(true, false);
    try
    {
      for (int index = 0; index < this._attrProcessors.Count; ++index)
      {
        if (this.selectedSpecRow == null && this._quantities[index] != null && this._quantities[index].RelationID != -1L && this._quantities[index].Count == null)
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
            sessionKeeper.Session.GetRelation(this._quantities[index].RelationID)?.Delete(0L);
        }
        else if (this.selectedSpecRow != null && this._quantities[index] != null)
        {
          this.selectedSpecRow.GetRelationIndexForProduct(this._quantities[index].InstanceID);
          int productIndex = this.selectedSpecRow.avsDocument.GetProductIndex(this._quantities[index].InstanceID);
          if (productIndex != -1)
            this.SaveCount(productIndex, (object) this._quantities[index].Count);
          this.selectedSpecRow.SetFieldValue(this.selectedSpecRow.Field_Position, -1, -1, (object) this.tbPosition.Text, true, false, true, true, false, false);
          this.selectedSpecRow.SetFieldValue(this.selectedSpecRow.Field_Zone, -1, -1, (object) this.tbZone.Text, true, true, true, true, false, false);
          this.selectedSpecRow.SetFieldValue(this.selectedSpecRow.Field_Note, -1, -1, (object) this.tbNote.Text, true, false, true, true, false, false);
          this.selectedSpecRow.SetFieldValue(this.selectedSpecRow.Field_PosDesignation, -1, -1, (object) this.tbPosDesignation.Text, true, false, true, true, false, false);
          this.selectedSpecRow.SetFieldValue(this.selectedSpecRow.Attr_Podbor, -1, -1, (object) this.cbPodbor.Checked, true, false, true, true, false, false);
        }
        else if (this._quantities[index] != null && this._quantities[index].RelationID != -1L)
        {
          this._attrProcessors[index].Load(this._quantities[index].RelationID, AttributableElements.Relation, ClientConsts.GetAttributeValuesModes, false);
          this._attrProcessors[index].StartTransaction();
          long relationId = this._quantities[index].RelationID;
          AttributableElements elementKind = AttributableElements.Relation;
          try
          {
            if (this._attrProcessors[index].FindAttributeValues(FormHelper.AttributeCountID) == null)
            {
              AttributeValues attributeValues = AttributeProcessor.CreateAttributeValues(FormHelper.AttributeCountID, relationId, elementKind);
              this._attrProcessors[index].ActualAttributeValues.Add(attributeValues);
            }
            if (this._attrProcessors[index].FindAttributeValues(FormHelper.AttributePositionID) == null)
            {
              AttributeValues attributeValues = AttributeProcessor.CreateAttributeValues(FormHelper.AttributePositionID, relationId, elementKind);
              this._attrProcessors[index].ActualAttributeValues.Add(attributeValues);
            }
            if (this._attrProcessors[index].FindAttributeValues(FormHelper.AttributeZoneID) == null)
            {
              AttributeValues attributeValues = AttributeProcessor.CreateAttributeValues(FormHelper.AttributeZoneID, relationId, elementKind);
              this._attrProcessors[index].ActualAttributeValues.Add(attributeValues);
            }
            if (this._attrProcessors[index].FindAttributeValues(FormHelper.AttributeNoteID) == null)
            {
              AttributeValues attributeValues = AttributeProcessor.CreateAttributeValues(FormHelper.AttributeNoteID, relationId, elementKind);
              this._attrProcessors[index].ActualAttributeValues.Add(attributeValues);
            }
            if (this._attrProcessors[index].FindAttributeValues(FormHelper.AttributePosDesignationID) == null)
            {
              AttributeValues attributeValues = AttributeProcessor.CreateAttributeValues(FormHelper.AttributePosDesignationID, relationId, elementKind);
              this._attrProcessors[index].ActualAttributeValues.Add(attributeValues);
            }
            if (this._attrProcessors[index].FindAttributeValues(AvsIDCache.Attr_Podbor) == null)
            {
              AttributeValues attributeValues = AttributeProcessor.CreateAttributeValues(AvsIDCache.Attr_Podbor, this.attributableElementID, this.attributableElement);
              this._attrProcessors[index].ActualAttributeValues.Add(attributeValues);
            }
            if (this.selectedSpecRow == null)
              this._attrProcessors[index].SetValue(FormHelper.AttributeCountID, (object) this._quantities[index].Count);
            this._attrProcessors[index].SetValue(FormHelper.AttributePositionID, (object) this.tbPosition.Text);
            this._attrProcessors[index].SetValue(FormHelper.AttributeZoneID, (object) this.tbZone.Text);
            this._attrProcessors[index].SetValue(FormHelper.AttributeNoteID, (object) this.tbNote.Text);
            this._attrProcessors[index].SetValue(FormHelper.AttributePosDesignationID, (object) this.tbPosDesignation.Text);
            this._attrProcessors[index].SetValue(AvsIDCache.Attr_Podbor, (object) this.cbPodbor.Checked);
            this._attrProcessors[index].Save();
            this._attrProcessors[index].CommitTransaction();
          }
          catch
          {
            this._attrProcessors[index].RollbackTransaction();
            throw;
          }
        }
      }
    }
    finally
    {
      if (this.selectedSpecRow != null && this.selectedSpecRow.avsDocument != null)
        this.selectedSpecRow.avsDocument.ResumeDocumentAndGridUpdates(0, true, true, true, false);
    }
    this.changed = false;
  }

  protected override void OnReload(IUserSession session, OpenModes mode)
  {
    if (mode == OpenModes.InViewReadOnly)
      this.SetAllReadOnly((Control) this);
    for (int index = 1; index < this._attrProcessors.Count; ++index)
    {
      if (this._quantities[index].RelationID != -1L)
        this._attrProcessors[index].Load(this._quantities[index].RelationID, AttributableElements.Relation, ClientConsts.GetAttributeValuesModes, false);
    }
    if (mode == OpenModes.View || mode == OpenModes.InViewReadOnly)
    {
      this.tbDesignation.Enabled = false;
      this.tbName.Enabled = false;
      this.bEditDesignation.Enabled = false;
      this.bEditName.Enabled = false;
      this.bClassificate.Enabled = false;
      this.tbMaterial.Enabled = false;
      this.bEditMaterial.Enabled = false;
      this.tbSize.Enabled = false;
      this.tbOKPCode.Enabled = false;
      this.tbSmotri.Enabled = false;
      this.cbPodbor.Enabled = false;
    }
    if ((this.disableControls & CommonDataType.OKPCode) == CommonDataType.OKPCode)
      this.tbOKPCode.Enabled = false;
    if ((this.disableControls & CommonDataType.Designation) == CommonDataType.Designation)
    {
      this.tbDesignation.Enabled = false;
      this.bEditDesignation.Enabled = false;
    }
    if ((this.disableControls & CommonDataType.Name) == CommonDataType.Name)
    {
      this.tbName.Enabled = false;
      this.bEditName.Enabled = false;
    }
    if ((this.disableControls & CommonDataType.Material) == CommonDataType.Material)
    {
      this.tbMaterial.Enabled = false;
      this.bEditMaterial.Enabled = false;
    }
    if ((this.disableControls & CommonDataType.Size) == CommonDataType.Size)
      this.tbSize.Enabled = false;
    if (!AvsConfig.PositionDesignation.ShowPosDesignation)
    {
      this.label6.Visible = false;
      this.tbPosDesignation.Visible = false;
    }
    else
    {
      this.label6.Visible = true;
      this.tbPosDesignation.Visible = true;
    }
    this.ReloadCommonData(CommonDataType.All);
    if (this.FormType == FormType.NonDraftB)
    {
      this.label11.Visible = false;
      this.label10.Visible = false;
      this.tbOKPCode.Visible = false;
      this.tbSmotri.Visible = false;
      Point location = this.tbName.Location;
      int y1 = location.Y;
      location = this.tbPosition.Location;
      int y2 = location.Y;
      int height = y1 - y2 + 25;
      Label label3 = this.label3;
      label3.Location = label3.Location + new Size(0, height);
      Label label4 = this.label4;
      label4.Location = label4.Location + new Size(0, height);
      Label label7 = this.label7;
      label7.Location = label7.Location + new Size(0, height);
      Label label8 = this.label8;
      label8.Location = label8.Location + new Size(0, height);
      Label label6 = this.label6;
      label6.Location = label6.Location + new Size(0, height);
      Label label9 = this.label9;
      label9.Location = label9.Location + new Size(0, height);
      Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox tbPosition = this.tbPosition;
      tbPosition.Location = tbPosition.Location + new Size(0, height);
      Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox tbZone = this.tbZone;
      tbZone.Location = tbZone.Location + new Size(0, height);
      Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox tbCount = this.tbCount;
      tbCount.Location = tbCount.Location + new Size(0, height);
      Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox tbPosDesignation = this.tbPosDesignation;
      tbPosDesignation.Location = tbPosDesignation.Location + new Size(0, height);
      Button bEditCount = this.bEditCount;
      bEditCount.Location = bEditCount.Location + new Size(0, height);
      Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox tbMaterial = this.tbMaterial;
      tbMaterial.Location = tbMaterial.Location + new Size(0, height);
      Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox tbSize = this.tbSize;
      tbSize.Location = tbSize.Location + new Size(0, height);
      Button bEditMaterial = this.bEditMaterial;
      bEditMaterial.Location = bEditMaterial.Location + new Size(0, height);
      TreeList treeList1_1 = this.treeList1;
      treeList1_1.Location = treeList1_1.Location + new Size(0, height);
      TreeList treeList1_2 = this.treeList1;
      treeList1_2.Size = treeList1_2.Size - new Size(0, height);
      CheckBox cbPodbor = this.cbPodbor;
      cbPodbor.Location = cbPodbor.Location + new Size(0, height);
      this.Height += height;
    }
    this.UpdateControlsState();
    this.treeList1.FocusedNodeChanged -= new FocusedNodeChangedEventHandler(this.treeList1_FocusedNodeChanged);
    this.treeList1.Nodes.Clear();
    AVSRow selectedSpecRow = this.selectedSpecRow;
    for (int index = 0; index < this._quantities.Count; ++index)
      this.treeList1.AppendNode((object) new object[2]
      {
        (object) this._quantities[index].InstanceCaption,
        (object) this.StringValueCount(this._quantities[index].Count)
      }, (TreeListNode) null).Tag = (object) index;
    this._index = 0;
    this.treeList1.FocusedNode = this.treeList1.Nodes[0];
    this.treeList1.FocusedNodeChanged += new FocusedNodeChangedEventHandler(this.treeList1_FocusedNodeChanged);
    object obj1 = (object) null;
    if (this._quantities.Count > 0)
      obj1 = (object) this._quantities[0].Count;
    this.tbCount.Text = CompareValuesHelper.NormalizedValue(obj1) == null || !(obj1 is MeasuredValue) ? string.Empty : ((MeasuredValue) obj1).ToString();
    if (this._attrProcessors != null && this._attrProcessors.Count > 0)
    {
      AttributeProcessor attrProcessor = this._attrProcessors[0];
      if (this.selectedSpecRow != null)
      {
        if (this.selectedSpecRow.avsDocument.productsInfo != null && this.selectedSpecRow.avsDocument.productsInfo.Count > 0)
        {
          int relationIndexForProduct = this.selectedSpecRow.GetRelationIndexForProduct(this._quantities[0].InstanceID);
          this.tbPosition.Text = this.selectedSpecRow.GetFieldStringValue(this.selectedSpecRow.Field_Position, relationIndexForProduct, -1, (List<RelationAttributeValuesCache>) null, false);
          this.tbZone.Text = this.selectedSpecRow.GetFieldStringValue(this.selectedSpecRow.Field_Zone, relationIndexForProduct, -1, (List<RelationAttributeValuesCache>) null, false);
          this.tbNote.Text = this.selectedSpecRow.GetFieldStringValue(this.selectedSpecRow.Field_Note, relationIndexForProduct, -1, (List<RelationAttributeValuesCache>) null, false);
          this.cbPodbor.Checked = this.selectedSpecRow.GetFieldBoolValue(this.selectedSpecRow.Attr_Podbor, relationIndexForProduct, -1, (List<RelationAttributeValuesCache>) null, true);
        }
      }
      else if (attrProcessor.Id != -1L && attrProcessor.Loaded)
      {
        if (attrProcessor.FindAttributeValues(AvsIDCache.Attr_Podbor) != null)
          this.cbPodbor.Checked = Convert.ToBoolean(attrProcessor.GetValue(AvsIDCache.Attr_Podbor));
        if (attrProcessor.FindAttributeValues(FormHelper.AttributePositionID) != null)
        {
          object obj2 = attrProcessor.GetValue(FormHelper.AttributePositionID);
          this.tbPosition.Text = CompareValuesHelper.NormalizedValue(obj2) != null ? Convert.ToString(obj2) : string.Empty;
        }
        if (attrProcessor.FindAttributeValues(FormHelper.AttributeZoneID) != null)
        {
          object obj3 = attrProcessor.GetValue(FormHelper.AttributeZoneID);
          this.tbZone.Text = CompareValuesHelper.NormalizedValue(obj3) != null ? Convert.ToString(obj3) : string.Empty;
        }
        if (attrProcessor.FindAttributeValues(FormHelper.AttributeNoteID) != null)
        {
          object obj4 = attrProcessor.GetValue(FormHelper.AttributeNoteID);
          this.tbNote.Text = CompareValuesHelper.NormalizedValue(obj4) != null ? Convert.ToString(obj4) : string.Empty;
        }
        if (attrProcessor.FindAttributeValues(FormHelper.AttributePosDesignationID) != null)
        {
          object obj5 = attrProcessor.GetValue(FormHelper.AttributePosDesignationID);
          this.tbPosDesignation.Text = CompareValuesHelper.NormalizedValue(obj5) != null ? Convert.ToString(obj5) : string.Empty;
        }
      }
    }
    this.tbName.ReadOnly |= this.IsReadOnly(FormHelper.AttributeNameID);
    this.tbDesignation.ReadOnly |= this.IsReadOnly(FormHelper.AttributeDesignationID);
  }

  /// <summary>Пришло сообщение об изменениии общих атрибутов</summary>
  protected override void OnCommonDataChanged(CommonDataType type) => this.ReloadCommonData(type);

  /// <summary>Обработка события применения классификации</summary>
  /// <param name="type"></param>
  private void ReloadCommonData(CommonDataType type)
  {
    switch (type)
    {
      case CommonDataType.All:
        this.tbDesignation.Text = this.commonData.Designation;
        this.tbName.Text = this.commonData.Name;
        this.lFullName.Text = "Полное наименование    " + this.commonData.FullName;
        this.tbMaterial.Text = this.commonData.Material.Caption;
        this.tbSize.Text = this.commonData.Size;
        this.tbOKPCode.Text = this.commonData.OKPCode;
        this.tbPosDesignation.Text = this.commonData.PosDesignation;
        this.tbSmotri.Text = this.commonData.Smotri;
        this.OnChanged();
        break;
      case CommonDataType.Designation:
        this.tbDesignation.Text = this.commonData.Designation;
        this.OnChanged();
        break;
      case CommonDataType.Name:
        this.tbName.Text = this.commonData.Name;
        this.lFullName.Text = "Полное наименование    " + this.commonData.FullName;
        this.OnChanged();
        break;
      case CommonDataType.OKPCode:
        this.tbOKPCode.Text = this.commonData.OKPCode;
        this.OnChanged();
        break;
    }
  }

  /// <summary>Обработка события применения классификации</summary>
  /// <param name="values"></param>
  public override void OnSetClassifyAttributes(IObjectClassificator oc, long clasifID)
  {
    long objectID = this._classifObjects.documentID != 0L ? this._classifObjects.documentID : this._classifObjects.articleID;
    AttributeValues[] clasificatorAttributes = oc.GetClasificatorAttributes(objectID);
    if (clasificatorAttributes == null || clasificatorAttributes.Length == 0)
      return;
    foreach (AttributeValues attributeValues in clasificatorAttributes)
    {
      if (attributeValues.Values != null && attributeValues.Values.Length != 0)
      {
        if (attributeValues.AttributeID == FormHelper.AttributeDesignationID)
          this.commonData.Designation = Convert.ToString(attributeValues.Values[0]);
        else if (attributeValues.AttributeID == FormHelper.AttributeNameID)
          this.commonData.Name = Convert.ToString(attributeValues.Values[0]);
        else if (attributeValues.AttributeID == FormHelper.AttributeFormatID)
          this.commonData.Format = Convert.ToString(attributeValues.Values[0]);
      }
    }
  }

  /// <summary>
  /// Сформировать строку для отображения количества в TreeView
  /// </summary>
  /// <param name="mv"></param>
  /// <returns></returns>
  private string StringValueCount(MeasuredValue mv) => mv == null ? "-" : mv.ToString();

  /// <summary>
  /// Изменить значение атрибута "Количество" для текущего исполнения в гриде
  /// </summary>
  private void ChangeCountIntoGrid()
  {
    this.UpdateControlsState();
    for (int index = 0; index < this.treeList1.Nodes.Count; ++index)
      this.treeList1.Nodes[index].SetValue((object) 1, (object) this.StringValueCount(this._quantities[index].Count));
  }

  private void UpdateControlsState()
  {
    bool flag = false;
    for (int index = 0; index < this._quantities.Count; ++index)
    {
      if (this._quantities[index].Count != null)
      {
        flag = true;
        break;
      }
    }
    this.tbPosDesignation.ReadOnly = !flag;
  }

  /// <summary>Нажали кновку вызова редактора атрибута "Обозначение"</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void bEditDesignation_Click(object sender, EventArgs e)
  {
    this.OnEditDesignation(this._classifObjects.articleID);
  }

  /// <summary>
  /// Нажали кновку вызова редактора атрибута "Наименование"
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void bEditName_Click(object sender, EventArgs e)
  {
    this.OnEditName(this._classifObjects.articleID);
  }

  /// <summary>
  /// Вышли из поля для редактирования атрибута "Обозначение"
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tbDesignation_Leave(object sender, EventArgs e)
  {
    this.OnDesignationLeave(this.tbDesignation.Text);
  }

  /// <summary>
  /// Вышли из поля для редактирования атрибута "Наименование"
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tbName_Leave(object sender, EventArgs e) => this.OnNameLeave(this.tbName.Text);

  /// <summary>Нажали кнопку классификации</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void bClassificate_Click(object sender, EventArgs e)
  {
    this.OnClassifier(this._classifObjects);
  }

  /// <summary>
  /// Вышли из поля для редактирования атрибута "Количество"
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tbCount_Leave(object sender, EventArgs e)
  {
    bool flag = true;
    for (int index = 0; index < this.treeList1.Nodes.Count; ++index)
    {
      if (!this.ChangeCount((TextBox) this.tbCount, this.OnLeaveCount((TextBox) this.tbCount, this._quantities[index].Count), ref this._quantities[index].Count))
      {
        flag = false;
        break;
      }
    }
    if (!flag)
      return;
    this.ChangeCountIntoGrid();
  }

  /// <summary>Нажали кновку вызова редактора атрибута "Количество"</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void bEditCount_Click(object sender, EventArgs e)
  {
    bool flag = false;
    if (this.ChangeCount((TextBox) this.tbCount, this.OnEditCount((TextBox) this.tbCount, this._quantities[this._index].Count), ref this._quantities[this._index].Count))
      flag = true;
    if (!flag)
      return;
    for (int index = 0; index < this.treeList1.Nodes.Count; ++index)
      this._quantities[index].Count = this._quantities[this._index].Count;
    this.ChangeCountIntoGrid();
  }

  /// <summary>Изменился фокус в TreeView</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void treeList1_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
  {
    this._index = (int) e.Node.Tag;
    this.tbCount.Text = this._quantities[this._index].Count != null ? this._quantities[this._index].Count.ToString() : string.Empty;
  }

  protected override AttributeProcessor GetAttributeProcessorForValue(out AVSRow row)
  {
    row = this.selectedSpecRow;
    if (this._index < 0 || this._index >= this._attrProcessors.Count)
      return (AttributeProcessor) null;
    return this._attrProcessors[this._index].Id != -1L ? this._attrProcessors[this._index] : this._attrProcessors[0];
  }

  protected override void SetValue(int attributeID, object newValue)
  {
    this._attrProcessors[this._index].SetValue(attributeID, newValue);
  }

  private void OnTextChanged(object sender, EventArgs e) => this.OnChanged();

  private void bEditMaterial_Click(object sender, EventArgs e)
  {
    object obj = this.ChangeInEditor(FormHelper.AttributeMaterialID, (object) this.commonData.Material.ObjectID);
    try
    {
      if (CompareValuesHelper.NormalizedValue(obj) == null)
      {
        this.commonData.Material = new MaterialInfo(0L, string.Empty);
      }
      else
      {
        long int64 = Convert.ToInt64(obj);
        if (this.commonData.Material.ObjectID == int64)
          return;
        MaterialInfo material = this.GetMaterial(int64);
        this.tbMaterial.Text = material.Caption;
        this.commonData.Material = material;
      }
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show(ex.Message, "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
  }

  private void tbSize_Leave(object sender, EventArgs e)
  {
    if (!(this.commonData.Size != this.tbSize.Text))
      return;
    this.commonData.Size = this.tbSize.Text;
    this.OnChanged();
  }

  private void TbOKPCodeLeave(object sender, EventArgs e)
  {
    if (!(this.commonData.OKPCode != this.tbOKPCode.Text))
      return;
    this.commonData.OKPCode = this.tbOKPCode.Text;
    this.OnChanged();
  }

  private void TbSearchLeave(object sender, EventArgs e)
  {
    if (!(this.commonData.Smotri != this.tbSmotri.Text))
      return;
    this.commonData.Smotri = this.tbSmotri.Text;
    this.OnChanged();
  }

  private void cbPodbor_CheckedChanged(object sender, EventArgs e)
  {
    this.OnPodborChanged(this.cbPodbor.Checked);
  }

  private void treeList1_GetCustomNodeCellEdit(object sender, GetCustomNodeCellEditEventArgs e)
  {
    if (this.selectedSpecRow == null || e.Column != this.treeListColumn2 || !(e.Node.Tag is int))
      return;
    this.be.ReadOnly = this.selectedSpecRow.GetReadOnlyCount(this.selectedSpecRow.avsDocument.GetProductIndex(this._quantities[(int) e.Node.Tag].InstanceID));
    e.RepositoryItem = (RepositoryItem) this.be;
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    if (disposing && this.be != null)
    {
      this.be.ButtonClick -= new ButtonPressedEventHandler(this.be_ButtonClick);
      this.be.Validating -= new CancelEventHandler(this.be_Validating);
      this.be.ParseEditValue -= new ConvertEditValueEventHandler(this.be_ParseEditValue);
      this.be.Dispose();
      this.be = (RepositoryItemButtonEdit) null;
    }
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    this.bEditName = new Button();
    this.bEditDesignation = new Button();
    this.label2 = new Label();
    this.label1 = new Label();
    this.tbName = new Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox();
    this.tbDesignation = new Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox();
    this.panel1 = new Panel();
    this.bClassificate = new Button();
    this.panel2 = new Panel();
    this.label5 = new Label();
    this.tbNote = new Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox();
    this.label7 = new Label();
    this.tbZone = new Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox();
    this.label6 = new Label();
    this.tbPosDesignation = new Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox();
    this.label4 = new Label();
    this.tbPosition = new Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox();
    this.treeList1 = new TreeList();
    this.treeListColumn1 = new TreeListColumn();
    this.treeListColumn2 = new TreeListColumn();
    this.bEditCount = new Button();
    this.label3 = new Label();
    this.tbCount = new Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox();
    this.bEditMaterial = new Button();
    this.label8 = new Label();
    this.tbSize = new Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox();
    this.label9 = new Label();
    this.tbMaterial = new Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox();
    this.label10 = new Label();
    this.tbSmotri = new Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox();
    this.label11 = new Label();
    this.tbOKPCode = new Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox();
    this.lFullName = new Label();
    this.cbPodbor = new CheckBox();
    this.toolTip1 = new ToolTip(this.components);
    this.panel1.SuspendLayout();
    this.panel2.SuspendLayout();
    this.treeList1.BeginInit();
    this.SuspendLayout();
    this.bEditName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bEditName.Location = new Point(728, 108);
    this.bEditName.Name = "bEditName";
    this.bEditName.Size = new Size(24, 23);
    this.bEditName.TabIndex = 86;
    this.bEditName.TabStop = false;
    this.bEditName.Text = "...";
    this.bEditName.UseVisualStyleBackColor = true;
    this.bEditName.Click += new EventHandler(this.bEditName_Click);
    this.bEditDesignation.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bEditDesignation.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    this.bEditDesignation.Location = new Point(728, 81);
    this.bEditDesignation.Name = "bEditDesignation";
    this.bEditDesignation.Size = new Size(24, 23);
    this.bEditDesignation.TabIndex = 85;
    this.bEditDesignation.TabStop = false;
    this.bEditDesignation.Text = "...";
    this.bEditDesignation.UseVisualStyleBackColor = true;
    this.bEditDesignation.Click += new EventHandler(this.bEditDesignation_Click);
    this.label2.AutoSize = true;
    this.label2.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    this.label2.Location = new Point(25, 113);
    this.label2.Name = "label2";
    this.label2.Size = new Size(99, 13);
    this.label2.TabIndex = 84;
    this.label2.Text = "Наименование:";
    this.label1.AutoSize = true;
    this.label1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    this.label1.Location = new Point(25, 87);
    this.label1.Name = "label1";
    this.label1.Size = new Size(89, 13);
    this.label1.TabIndex = 83;
    this.label1.Text = "Обозначение:";
    this.tbName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbName.BackColor = Color.White;
    this.tbName.Location = new Point(144 /*0x90*/, 109);
    this.tbName.Name = "tbName";
    this.tbName.Size = new Size(584, 20);
    this.tbName.TabIndex = 1;
    this.tbName.Leave += new EventHandler(this.tbName_Leave);
    this.tbDesignation.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbDesignation.BackColor = Color.White;
    this.tbDesignation.Location = new Point(144 /*0x90*/, 83);
    this.tbDesignation.Name = "tbDesignation";
    this.tbDesignation.Size = new Size(584, 20);
    this.tbDesignation.TabIndex = 0;
    this.tbDesignation.Leave += new EventHandler(this.tbDesignation_Leave);
    this.panel1.BackColor = SystemColors.ControlLight;
    this.panel1.Controls.Add((Control) this.bClassificate);
    this.panel1.Dock = DockStyle.Top;
    this.panel1.Location = new Point(3, 3);
    this.panel1.Name = "panel1";
    this.panel1.Padding = new Padding(3);
    this.panel1.Size = new Size(770, 47);
    this.panel1.TabIndex = 14;
    this.bClassificate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bClassificate.Image = (Image) Resources.classify;
    this.bClassificate.Location = new Point(723, 10);
    this.bClassificate.Name = "bClassificate";
    this.bClassificate.Size = new Size(26, 26);
    this.bClassificate.TabIndex = 96 /*0x60*/;
    this.bClassificate.UseVisualStyleBackColor = true;
    this.bClassificate.Click += new EventHandler(this.bClassificate_Click);
    this.panel2.BackColor = SystemColors.ControlLight;
    this.panel2.Controls.Add((Control) this.label5);
    this.panel2.Controls.Add((Control) this.tbNote);
    this.panel2.Dock = DockStyle.Bottom;
    this.panel2.Location = new Point(3, 365);
    this.panel2.Margin = new Padding(0);
    this.panel2.Name = "panel2";
    this.panel2.Size = new Size(770, 80 /*0x50*/);
    this.panel2.TabIndex = 12;
    this.label5.AutoSize = true;
    this.label5.Location = new Point(22, 15);
    this.label5.Name = "label5";
    this.label5.Size = new Size(73, 13);
    this.label5.TabIndex = 9;
    this.label5.Text = "Примечание:";
    this.tbNote.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.tbNote.BackColor = Color.White;
    this.tbNote.Location = new Point(25, 31 /*0x1F*/);
    this.tbNote.Multiline = true;
    this.tbNote.Name = "tbNote";
    this.tbNote.ScrollBars = ScrollBars.Both;
    this.tbNote.Size = new Size(663, 40);
    this.tbNote.TabIndex = 94;
    this.tbNote.TextChanged += new EventHandler(this.OnTextChanged);
    this.label7.AutoSize = true;
    this.label7.Location = new Point(25, 234);
    this.label7.Name = "label7";
    this.label7.Size = new Size(35, 13);
    this.label7.TabIndex = 76;
    this.label7.Text = "Зона:";
    this.tbZone.BackColor = Color.White;
    this.tbZone.Location = new Point(144 /*0x90*/, 230);
    this.tbZone.Name = "tbZone";
    this.tbZone.Size = new Size(186, 20);
    this.tbZone.TabIndex = 5;
    this.tbZone.TextChanged += new EventHandler(this.OnTextChanged);
    this.label6.AutoSize = true;
    this.label6.Location = new Point(25, 336);
    this.label6.Name = "label6";
    this.label6.Size = new Size(101, 13);
    this.label6.TabIndex = 74;
    this.label6.Text = "Поз. обозначение:";
    this.tbPosDesignation.BackColor = Color.White;
    this.tbPosDesignation.Location = new Point(144 /*0x90*/, 333);
    this.tbPosDesignation.Name = "tbPosDesignation";
    this.tbPosDesignation.Size = new Size(186, 20);
    this.tbPosDesignation.TabIndex = 11;
    this.tbPosDesignation.TextChanged += new EventHandler(this.OnTextChanged);
    this.label4.AutoSize = true;
    this.label4.Location = new Point(25, 207);
    this.label4.Name = "label4";
    this.label4.Size = new Size(54, 13);
    this.label4.TabIndex = 72;
    this.label4.Text = "Позиция:";
    this.tbPosition.BackColor = Color.White;
    this.tbPosition.Location = new Point(144 /*0x90*/, 203);
    this.tbPosition.Name = "tbPosition";
    this.tbPosition.Size = new Size(186, 20);
    this.tbPosition.TabIndex = 4;
    this.tbPosition.TextChanged += new EventHandler(this.OnTextChanged);
    this.treeList1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.treeList1.Columns.AddRange(new TreeListColumn[2]
    {
      this.treeListColumn1,
      this.treeListColumn2
    });
    this.treeList1.Location = new Point(347, 203);
    this.treeList1.MenuOptions = MenuOptionsFlags.None;
    this.treeList1.Name = "treeList1";
    this.treeList1.Size = new Size(405, 141);
    this.treeList1.TabIndex = 13;
    this.treeList1.Text = "treeList1";
    this.treeList1.ViewOptions = ViewOptionsFlags.AutoWidth | ViewOptionsFlags.ShowColumns | ViewOptionsFlags.ShowIndicator | ViewOptionsFlags.ShowRoot | ViewOptionsFlags.ShowVertLines | ViewOptionsFlags.ShowFocusedFrame;
    this.treeList1.GetCustomNodeCellEdit += new GetCustomNodeCellEditEventHandler(this.treeList1_GetCustomNodeCellEdit);
    this.treeList1.FocusedNodeChanged += new FocusedNodeChangedEventHandler(this.treeList1_FocusedNodeChanged);
    this.treeListColumn1.Caption = "Исполнение";
    this.treeListColumn1.FieldName = "treeListColumn1";
    this.treeListColumn1.Name = "treeListColumn1";
    this.treeListColumn1.VisibleIndex = 0;
    this.treeListColumn1.Width = 300;
    this.treeListColumn2.Caption = "Количество";
    this.treeListColumn2.FieldName = "treeListColumn2";
    this.treeListColumn2.Name = "treeListColumn2";
    this.treeListColumn2.VisibleIndex = 1;
    this.treeListColumn2.Width = 50;
    this.bEditCount.Location = new Point(220, (int) byte.MaxValue);
    this.bEditCount.Name = "bEditCount";
    this.bEditCount.Size = new Size(24, 23);
    this.bEditCount.TabIndex = 7;
    this.bEditCount.Text = "...";
    this.bEditCount.UseVisualStyleBackColor = true;
    this.bEditCount.Visible = false;
    this.bEditCount.Click += new EventHandler(this.bEditCount_Click);
    this.label3.AutoSize = true;
    this.label3.Location = new Point(25, 260);
    this.label3.Name = "label3";
    this.label3.Size = new Size(69, 13);
    this.label3.TabIndex = 91;
    this.label3.Text = "Количество:";
    this.label3.Visible = false;
    this.tbCount.BackColor = Color.White;
    this.tbCount.Location = new Point(144 /*0x90*/, 256 /*0x0100*/);
    this.tbCount.Name = "tbCount";
    this.tbCount.Size = new Size(76, 20);
    this.tbCount.TabIndex = 6;
    this.tbCount.Visible = false;
    this.tbCount.Leave += new EventHandler(this.tbCount_Leave);
    this.bEditMaterial.Location = new Point(307, 280);
    this.bEditMaterial.Name = "bEditMaterial";
    this.bEditMaterial.Size = new Size(24, 23);
    this.bEditMaterial.TabIndex = 91;
    this.bEditMaterial.Text = "...";
    this.bEditMaterial.UseVisualStyleBackColor = true;
    this.bEditMaterial.Click += new EventHandler(this.bEditMaterial_Click);
    this.label8.AutoSize = true;
    this.label8.Location = new Point(25, 311);
    this.label8.Name = "label8";
    this.label8.Size = new Size(57, 13);
    this.label8.TabIndex = 96 /*0x60*/;
    this.label8.Text = "Размеры:";
    this.tbSize.BackColor = Color.White;
    this.tbSize.Location = new Point(144 /*0x90*/, 307);
    this.tbSize.Name = "tbSize";
    this.tbSize.Size = new Size(186, 20);
    this.tbSize.TabIndex = 10;
    this.tbSize.TextChanged += new EventHandler(this.OnTextChanged);
    this.tbSize.Leave += new EventHandler(this.tbSize_Leave);
    this.label9.AutoSize = true;
    this.label9.Location = new Point(25, 284);
    this.label9.Name = "label9";
    this.label9.Size = new Size(60, 13);
    this.label9.TabIndex = 94;
    this.label9.Text = "Материал:";
    this.tbMaterial.BackColor = Color.White;
    this.tbMaterial.Location = new Point(144 /*0x90*/, 280);
    this.tbMaterial.Name = "tbMaterial";
    this.tbMaterial.ReadOnly = true;
    this.tbMaterial.Size = new Size(163, 20);
    this.tbMaterial.TabIndex = 9;
    this.label10.AutoSize = true;
    this.label10.Location = new Point(25, 175);
    this.label10.Name = "label10";
    this.label10.Size = new Size(48 /*0x30*/, 13);
    this.label10.TabIndex = 105;
    this.label10.Text = "Смотри:";
    this.tbSmotri.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbSmotri.BackColor = Color.White;
    this.tbSmotri.Location = new Point(144 /*0x90*/, 172);
    this.tbSmotri.Name = "tbSmotri";
    this.tbSmotri.Size = new Size(608, 20);
    this.tbSmotri.TabIndex = 3;
    this.tbSmotri.TextChanged += new EventHandler(this.OnTextChanged);
    this.tbSmotri.Leave += new EventHandler(this.TbSearchLeave);
    this.label11.AutoSize = true;
    this.label11.Location = new Point(25, 149);
    this.label11.Name = "label11";
    this.label11.Size = new Size(55, 13);
    this.label11.TabIndex = 103;
    this.label11.Text = "Код ОКП:";
    this.tbOKPCode.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbOKPCode.BackColor = Color.White;
    this.tbOKPCode.Location = new Point(144 /*0x90*/, 146);
    this.tbOKPCode.Name = "tbOKPCode";
    this.tbOKPCode.Size = new Size(608, 20);
    this.tbOKPCode.TabIndex = 2;
    this.tbOKPCode.TextChanged += new EventHandler(this.OnTextChanged);
    this.tbOKPCode.Leave += new EventHandler(this.TbOKPCodeLeave);
    this.lFullName.AutoSize = true;
    this.lFullName.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    this.lFullName.Location = new Point(25, 64 /*0x40*/);
    this.lFullName.Name = "lFullName";
    this.lFullName.Size = new Size(41, 13);
    this.lFullName.TabIndex = 106;
    this.lFullName.Text = "label7";
    this.cbPodbor.AutoSize = true;
    this.cbPodbor.Location = new Point(144 /*0x90*/, 259);
    this.cbPodbor.Name = "cbPodbor";
    this.cbPodbor.Size = new Size(64 /*0x40*/, 17);
    this.cbPodbor.TabIndex = 8;
    this.cbPodbor.Text = "Подбор";
    this.toolTip1.SetToolTip((Control) this.cbPodbor, "Изделие с электромонтажом входит в подбор");
    this.cbPodbor.UseVisualStyleBackColor = true;
    this.cbPodbor.CheckedChanged += new EventHandler(this.cbPodbor_CheckedChanged);
    this.AutoScaleDimensions = new SizeF(96f, 96f);
    this.AutoScaleMode = AutoScaleMode.Dpi;
    this.Controls.Add((Control) this.cbPodbor);
    this.Controls.Add((Control) this.lFullName);
    this.Controls.Add((Control) this.label10);
    this.Controls.Add((Control) this.tbSmotri);
    this.Controls.Add((Control) this.label11);
    this.Controls.Add((Control) this.tbOKPCode);
    this.Controls.Add((Control) this.bEditMaterial);
    this.Controls.Add((Control) this.label8);
    this.Controls.Add((Control) this.tbSize);
    this.Controls.Add((Control) this.label9);
    this.Controls.Add((Control) this.tbMaterial);
    this.Controls.Add((Control) this.bEditCount);
    this.Controls.Add((Control) this.label3);
    this.Controls.Add((Control) this.tbCount);
    this.Controls.Add((Control) this.treeList1);
    this.Controls.Add((Control) this.bEditName);
    this.Controls.Add((Control) this.bEditDesignation);
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.tbName);
    this.Controls.Add((Control) this.tbDesignation);
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.panel2);
    this.Controls.Add((Control) this.label7);
    this.Controls.Add((Control) this.tbZone);
    this.Controls.Add((Control) this.label6);
    this.Controls.Add((Control) this.tbPosDesignation);
    this.Controls.Add((Control) this.label4);
    this.Controls.Add((Control) this.tbPosition);
    this.MinimumSize = new Size(580, 350);
    this.Name = nameof (NonDraftGroupMasterControl);
    this.Padding = new Padding(3);
    this.Size = new Size(776, 448);
    this.panel1.ResumeLayout(false);
    this.panel2.ResumeLayout(false);
    this.panel2.PerformLayout();
    this.treeList1.EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  /// <summary>Информация по связи</summary>
  private class RelationCounts
  {
    /// <summary>Идентификатор связи</summary>
    public long RelationID = -1;
    /// <summary>Идентификатор головного изделия</summary>
    public long InstanceID;
    /// <summary>Заголовок головного изделия</summary>
    public string InstanceCaption;
    /// <summary>Количество</summary>
    public MeasuredValue Count;
  }
}
