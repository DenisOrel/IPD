
// Type: Intermech.Client.Core.FormDesigner.Controls.AttrMeasuredListBox
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.PropertyEditors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>Редактор списка атрибутов типа "ftMeasured".</summary>
public class AttrMeasuredListBox : AttrListBoxBase
{
  private MeasureDescriptor _defMeasure;
  private ArrayList _measures = new ArrayList();
  private List<string> _measuresSName = new List<string>(0);
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  /// <summary>Отступы от краев в элементе управления.</summary>
  /// <remarks>Здесь нужно только для того, чтобы запреить сериализацию</remarks>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public new Padding Padding
  {
    get => base.Padding;
    private set => base.Padding = value;
  }

  /// <summary>Конструктор.</summary>
  public AttrMeasuredListBox()
  {
    this.InitializeComponent();
    this.Name = string.Empty;
    this.MenuItemClick += new EventHandler(this.On_btnAddEdit_Click);
  }

  /// <summary>Добавить элемент.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_btnAddEdit_Click(object sender, EventArgs e)
  {
    if (this.AttributeInfo == null || this._attrValues == null)
      return;
    this.LoadDefaultMeasure();
    if (Convert.ToInt32(sender is ToolStripMenuItem toolStripMenuItem ? toolStripMenuItem.Tag : (sender as ControlButton).Tag) == 0)
    {
      MeasuredValue measuredValue = MeasureHelper.ConvertToMeasuredValue("0", this._defMeasure, false);
      using (MeasureForm measureForm = new MeasureForm())
      {
        if (measureForm.ExecuteDialog(ref measuredValue, this._measures.ToArray(typeof (MeasureDescriptor)) as MeasureDescriptor[]) != DialogResult.OK || this.IsValueExist(measuredValue.Caption, this._lst.Items.Count))
          return;
        this._lst.SelectedIndex = this._lst.Items.Add((object) measuredValue);
        this.Modified = true;
      }
    }
    else
    {
      if (this._lst.SelectedItem == null || !(this._lst.SelectedItem is MeasuredValue selectedItem))
        return;
      using (MeasureForm measureForm = new MeasureForm())
      {
        if (measureForm.ExecuteDialog(ref selectedItem, this._measures.ToArray(typeof (MeasureDescriptor)) as MeasureDescriptor[]) != DialogResult.OK || this.IsValueExist(selectedItem.Caption, this._lst.SelectedIndex))
          return;
        this._lst.Items[this._lst.SelectedIndex] = (object) selectedItem;
        this.Modified = true;
      }
    }
  }

  /// <summary>Guid атрибута и типа объекта/связи.</summary>
  public override AttributeValues Values
  {
    get => base.Values;
    set
    {
      base.Values = value;
      if (value == null)
        return;
      this._measures = MeasureEditor.GetMeasureDescriptorListByAttributeId(value.AttributeID);
      this._measuresSName.Clear();
      foreach (MeasureDescriptor measure in this._measures)
      {
        if (!this._measuresSName.Contains(measure.ShortName))
          this._measuresSName.Add(measure.ShortName.Trim());
      }
    }
  }

  /// <summary>Проверка на существование значения.</summary>
  /// <param name="value">Проверяемое значение</param>
  /// <param name="itemIndex">Индекс добавляемого/редактируемого элемента</param>
  /// <returns>Результат проверки</returns>
  private bool IsValueExist(string value, int itemIndex)
  {
    bool flag = false;
    for (int index = 0; index < this._lst.Items.Count; ++index)
    {
      if (!((this._lst.Items[index] as MeasuredValue).Caption != value) && index != itemIndex)
      {
        int num = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("FormDesigner_ListControls_ValueExist"), (object) value), LocalizationHolder.rm.GetString("FormDesigner_ListControls_DublicationValue"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
        flag = true;
      }
    }
    return flag;
  }

  /// <summary>Let's find deafult measure.</summary>
  private void LoadDefaultMeasure()
  {
    if (this._defMeasure != null)
      return;
    if (this.ParentInfo == null)
    {
      this._defMeasure = this._measuresSName.Count > 0 ? MeasureHelper.FindDescriptor(this._measuresSName[0]) : (MeasureDescriptor) null;
    }
    else
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        long sizeType = MetaDataHelper.GetAttributeType(this.AttributeInfo.AttributeGuid).SizeType;
        if (this.ParentTypeID == -1)
        {
          if (this.ParentInfo.ElementKind == AttributableElements.Object)
            this.ParentTypeID = sessionKeeper.Session.GetObjectInfo(this.ParentInfo.ElementIdentifier).ObjectTypeID;
          else if (this.ParentInfo.ElementKind == AttributableElements.Relation)
          {
            IDBRelation relation = sessionKeeper.Session.GetRelation(this.ParentInfo.ElementIdentifier, false);
            this.ParentTypeID = relation != null ? relation.RelationType : -1;
          }
        }
        IDBMeasureAttributeType attributeById = ClientCommons.GetAttributableType(this.ParentTypeID, this.ParentInfo.ElementKind).Attributes.GetAttributeByID(this._attrValues.AttributeID) as IDBMeasureAttributeType;
        long measureID;
        if (attributeById != null)
        {
          measureID = attributeById.DefaultMeasureID;
          if (measureID == 0L)
          {
            IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(this._attrValues.AttributeID);
            if (attributeType != null && attributeType.AttributeType == FieldTypes.ftMeasured && attributeType.PropertiesStructure.MetadataExtensions != null && attributeType.PropertiesStructure.MetadataExtensions.Contains((object) "MU_PHYSICAL_ID"))
            {
              object metadataExtension = attributeType.PropertiesStructure.MetadataExtensions[(object) "MU_PHYSICAL_ID"];
              if (metadataExtension != null)
              {
                List<long> longList = new List<long>((IEnumerable<long>) (long[]) metadataExtension);
                if (longList.Count > 0)
                  sizeType = longList[0];
              }
            }
            measureID = MeasureHelper.GetBaseMeasureID(sizeType);
          }
        }
        else
          measureID = MeasureHelper.GetBaseMeasureID(sizeType);
        this._defMeasure = measureID > 0L || MeasureHelper.Measures.Length == 0 ? MeasureHelper.FindDescriptor(measureID) : (this._measures.Count > 0 ? this._measures[0] as MeasureDescriptor : MeasureHelper.Measures[0]);
      }
    }
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this.MenuItemClick -= new EventHandler(this.On_btnAddEdit_Click);
      if (this.components != null)
        this.components.Dispose();
    }
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AttrMeasuredListBox));
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Name = nameof (AttrMeasuredListBox);
    this.Controls.SetChildIndex((Control) this._lst, 0);
    this.ResumeLayout(false);
  }
}
