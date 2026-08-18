// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.DwgCreator.ChoiceNewObjectType
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.Client.Core.ObjectCreator;
using Intermech.Client.Core.ObjectCreator.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Pdm;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Cadmech.Integrator.DwgCreator;

internal class ChoiceNewObjectType : ObjectCreatorControl
{
  private List<Guid> _outObjTypes = new List<Guid>();
  private bool _autoSelected;
  private IContainer components;
  private ComboBox comboBox1;
  private Label label1;
  private CheckBox checkBox1;

  public ChoiceNewObjectType(CreatedObjectItem objItem)
    : base(objItem)
  {
    this.InitializeComponent();
    this._NeedSaveWhenNotVisible = true;
  }

  public void SetPageData()
  {
    this._autoSelected = true;
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDocumentTypeSettingsService customService = (IDocumentTypeSettingsService) sessionKeeper.Session.GetCustomService(typeof (IDocumentTypeSettingsService));
        if (customService == null)
          return;
        DocumentTypeSettings settings = customService.GetSettings(sessionKeeper.Session.SessionGUID, this.CreatedObject.ObjectTypeID);
        if (!(settings.OutputObjectTypes != string.Empty))
          return;
        foreach (string outputObjectType in DocumentTypeSettings.SplitOutputObjectTypes(settings.OutputObjectTypes))
        {
          IDBObjectType objectType = sessionKeeper.Session.GetObjectType(new Guid(outputObjectType), false);
          if (objectType != null)
          {
            this.comboBox1.Items.Add((object) objectType.ObjectTypeName);
            this._outObjTypes.Add((objectType as IDBGuid).GUID);
          }
        }
      }
    }
    finally
    {
      this._autoSelected = false;
      if (this.comboBox1.Items.Count > 0)
        this.comboBox1.SelectedIndex = 0;
    }
  }

  public override bool Save(PageSaveArgs args)
  {
    if (!this.checkBox1.Checked && this.comboBox1.Items.Count > 0 && this.comboBox1.SelectedIndex >= 0)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(this.CreatedObject.ObjectID);
        IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(new Guid("cad0001f-306c-11d8-b4e9-00304f19f545"));
        IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(new Guid("cad00020-306c-11d8-b4e9-00304f19f545"));
        string oldDesignation = attributeByGuid != null ? attributeByGuid.AsString : string.Empty;
        if (oldDesignation != string.Empty)
        {
          string designationWithoutCode = PDMHelper.GetDesignationWithoutCode(sessionKeeper.Session, oldDesignation, dbObject.ObjectType);
          Guid outObjType = this._outObjTypes[this.comboBox1.SelectedIndex];
          DBRecordSetParams dbRecordSetParams = new DBRecordSetParams(new ConditionStructure[2]
          {
            new ConditionStructure(new Guid("cad0001f-306c-11d8-b4e9-00304f19f545"), RelationalOperators.Equal, (object) designationWithoutCode, LogicalOperators.AND, 0),
            new ConditionStructure(-6, RelationalOperators.In, (object) new long[2]
            {
              0L,
              sessionKeeper.Session.UserID
            }, LogicalOperators.AND, 0, false)
          }, new ColumnDescriptor[2]
          {
            new ColumnDescriptor((object) -2, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0),
            new ColumnDescriptor((object) attributeType.AttributeID, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0)
          });
          IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(outObjType);
          IDBObjectType objectType1 = sessionKeeper.Session.GetObjectType(new Guid("cad00268-306c-11d8-b4e9-00304f19f545"));
          List<int> childTypes = ObjectTypesCacheHelper.GetChildTypes(sessionKeeper.Session, objectType1.ObjectType);
          DBRecordSetParams paramSet = dbRecordSetParams;
          DataTable dataTable = objectCollection.Select(paramSet);
          if (dataTable.Rows.Count > 0)
          {
            IDBObjectType objectType2 = sessionKeeper.Session.GetObjectType(outObjType);
            SelectObject selectObject = new SelectObject();
            string empty = string.Empty;
            string str1 = !childTypes.Contains(objectType2.ObjectType) ? (dataTable.Rows.Count > 0 ? "В базе данных найдены объекты с таким-же обозначением, " : "В базе данных найден объект с таким-же обозначением, ") : (dataTable.Rows.Count > 0 ? "В базе данных найдены изделия с таким-же обозначением, " : "В базе данных найдено изделие с таким-же обозначением, ");
            string str2 = "как и у создаваемого документа. Укажите к каким версиям";
            string str3 = "найденных объектов привязать создаваемый документ:";
            List<ListViewItem> listViewItemList = new List<ListViewItem>(dataTable.Rows.Count);
            foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
              listViewItemList.Add(new ListViewItem(new string[3]
              {
                Convert.ToString(row[0]),
                designationWithoutCode,
                Convert.ToString(row[1])
              })
              {
                Tag = (object) Convert.ToInt64(row[0])
              });
            selectObject.SetData(str1, str2, str3, listViewItemList.ToArray());
            if (selectObject.ShowDialog() == DialogResult.OK)
            {
              if (this.NewObjectSelectedEvent != null)
              {
                long[] selectedObjectIds = selectObject.SelectedObjectIDs;
                if (selectedObjectIds != null)
                  this.NewObjectSelectedEvent(selectedObjectIds);
              }
            }
          }
        }
      }
    }
    args.Error = (Exception) null;
    return true;
  }

  public event ChoiceNewObjectType.ObjectTypeChanged ObjectTypeChangedEvent;

  public event ChoiceNewObjectType.NewObjectSelected NewObjectSelectedEvent;

  private void checkBox1_CheckedChanged(object sender, EventArgs e)
  {
    if (this._autoSelected || this.ObjectTypeChangedEvent == null)
      return;
    if (this.checkBox1.Checked)
      this.ObjectTypeChangedEvent(Guid.Empty);
    else
      this.ObjectTypeChangedEvent(this._outObjTypes[this.comboBox1.SelectedIndex]);
  }

  private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this._autoSelected || this.ObjectTypeChangedEvent == null || this.checkBox1.Checked)
      return;
    this.ObjectTypeChangedEvent(this._outObjTypes[this.comboBox1.SelectedIndex]);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ChoiceNewObjectType));
    this.comboBox1 = new ComboBox();
    this.label1 = new Label();
    this.checkBox1 = new CheckBox();
    this.SuspendLayout();
    this.comboBox1.AccessibleDescription = (string) null;
    this.comboBox1.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.comboBox1, "comboBox1");
    this.comboBox1.BackgroundImage = (Image) null;
    this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
    this.comboBox1.Font = (Font) null;
    this.comboBox1.FormattingEnabled = true;
    this.comboBox1.Name = "comboBox1";
    this.comboBox1.SelectedIndexChanged += new EventHandler(this.comboBox1_SelectedIndexChanged);
    this.label1.AccessibleDescription = (string) null;
    this.label1.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Font = (Font) null;
    this.label1.Name = "label1";
    this.checkBox1.AccessibleDescription = (string) null;
    this.checkBox1.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.checkBox1, "checkBox1");
    this.checkBox1.BackgroundImage = (Image) null;
    this.checkBox1.Font = (Font) null;
    this.checkBox1.Name = "checkBox1";
    this.checkBox1.UseVisualStyleBackColor = true;
    this.checkBox1.CheckedChanged += new EventHandler(this.checkBox1_CheckedChanged);
    this.AccessibleDescription = (string) null;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.BackgroundImage = (Image) null;
    this.Controls.Add((Control) this.checkBox1);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.comboBox1);
    this.Font = (Font) null;
    this.Name = nameof (ChoiceNewObjectType);
    this.Tag = (object) "   ";
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  internal delegate void ObjectTypeChanged(Guid objTypeGuid);

  internal delegate void NewObjectSelected(long[] objectIDs);
}
