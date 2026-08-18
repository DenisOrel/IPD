// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Common_Dialogs.SpecSectionsEditor
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.AVS.AVSProperties;
using Intermech.AVS.Properties;
using Intermech.Controls;
using Intermech.Document.Client;
using Intermech.Document.DBCore;
using Intermech.Document.Model;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using Intermech.PropertyEditors;
using Intermech.PropertyEditors.AttrProcessor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS.Common_Dialogs;

public class SpecSectionsEditor : Form
{
  private List<SectionEditorInfo> Sections;
  private List<SectionEditorInfo> TemplateSections = new List<SectionEditorInfo>();
  private List<SectionEditorInfo> templSectionsToRemove = new List<SectionEditorInfo>();
  private List<SectionEditorInfo> templSectionsToAdd = new List<SectionEditorInfo>();
  private SectionEditorInfo ActiveSection;
  private long TemplateId;
  private SettingsStructure settingsStructure;
  private int _settingsHolderObjType = -1;
  private bool isValid = true;
  private bool enableEditTemplate;
  private AVSCommonPropertiesSchema avsCommonPropertiesSchema;
  private int lockStatus;
  internal static IAttributePropertyDescriberService attributePropertyDescriberService = ServicesManager.GetService(typeof (IAttributePropertyDescriberService)) as IAttributePropertyDescriberService;
  private Point pos = Point.Empty;
  private bool mousePressed;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private DataSet _dataSet;
  private TextBox tName;
  private Button bTypeAdd;
  private Button bTypeEdit;
  private Button bTypeDelete;
  private Label label1;
  private Label label2;
  private Label label3;
  private Label label4;
  private NumericUpDown tSortIndex;
  private Label label5;
  private Button bCancel;
  private Button bApply;
  private Button bOk;
  private Button bAddImBase;
  private Button bEditImBase;
  private Button bDeleteImBase;
  private Button bSectionUp;
  private Button bRemoveSection;
  private Button bAddSection;
  private Button bSectionDown;
  private NumericUpDown tNumber;
  private DataGridView dgTypes;
  private DataGridView dgImBase;
  private ImageList imageList1;
  private DataGridView dgAllItems;
  private DataGridViewImageColumn dataGridViewImageColumn1;
  private DataGridViewCheckBoxColumn Column5;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
  private TextBox tbTemplateName;
  private Label label6;
  protected Panel panelInfo;
  private TextBox textInfo;
  private PictureBox pictureInfo;
  private DataGridViewTextBoxColumn Column2;
  private DataGridViewTextBoxColumn Column3;

  public SpecSectionsEditor(SettingsStructure settingsStructure, long templateId)
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 1806);
    this.settingsStructure = settingsStructure;
    Guid templateGuid = Guid.Empty;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(templateId);
      this._settingsHolderObjType = objectInfo.ObjectTypeID;
      templateGuid = objectInfo.VersionGuid;
    }
    if (settingsStructure == null)
    {
      AVSDocumentTypeSettings settingsForTemplate = AVSDocumentsSettings.Instance.GetDocumentTypeSettingsForTemplate(templateGuid, out InheritanceSettingsLevel _);
      if (settingsForTemplate != null)
      {
        settingsStructure = settingsForTemplate.SettingsInheritanceStructure;
      }
      else
      {
        AVSDocumentTypeSettings typeForDbObjectType = AVSDocumentsSettings.Instance.GetDefaultDocumentTypeForDBObjectType(this._settingsHolderObjType, AVSDocumentType.Specification);
        settingsStructure = typeForDbObjectType == null ? (SettingsStructure) new UserAVSDocumentSettingsStructure() : typeForDbObjectType.SettingsInheritanceStructure;
      }
    }
    this.TemplateId = templateId;
    Image image1 = DocumentMenuHelper.LoadImageFromResurces(this.GetType().Assembly, "Intermech.AVS.Resources.Section.png");
    if (image1 != null)
      this.imageList1.Images.Add("Section", image1);
    Image image2 = DocumentMenuHelper.LoadImageFromResurces(this.GetType().Assembly, "Intermech.AVS.Resources.Edit.png");
    if (image2 != null)
      this.imageList1.Images.Add("Edit", image2);
    AttributeProcessor attributeProcessor = new AttributeProcessor();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.Sections = SectionEditorInfo.GetAllowableSpecSections(sessionKeeper.Session);
      this.TemplateSections = SectionEditorInfo.GetAllowableSpecSections(sessionKeeper.Session, templateId, this.Sections);
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.avsCommonPropertiesSchema = (AVSCommonPropertiesSchema) settingsStructure.CreateSettingsLevelFromObject(sessionKeeper.Session, templateId, this._settingsHolderObjType, -1L, AvsIDCache.Attr_ConstructorDocumentProperties, typeof (AVSCommonPropertiesSchema));
      this.enableEditTemplate = !this.avsCommonPropertiesSchema.ReadOnly;
    }
    this.UpdateList();
  }

  private void UpdateList()
  {
    this.dgAllItems.Columns[1].ReadOnly = !this.enableEditTemplate;
    ++this.lockStatus;
    int num = -1;
    SectionEditorInfo sectionEditorInfo = (SectionEditorInfo) null;
    if (this.dgAllItems.SelectedRows.Count > 0)
      sectionEditorInfo = this.dgAllItems.SelectedRows[0].Tag as SectionEditorInfo;
    this.dgAllItems.Rows.Clear();
    this.Sections.Sort();
    foreach (SectionEditorInfo section in this.Sections)
    {
      Image image = this.imageList1.Images["Section"];
      if (section.Changed || section.New)
        image = this.imageList1.Images["Edit"];
      int index = this.dgAllItems.Rows.Add((object) image, (object) this.TemplateSections.Contains(section), (object) section.ToString());
      this.dgAllItems.Rows[index].Tag = (object) section;
      if (section.Deleted)
        this.dgAllItems.Rows[index].Visible = false;
    }
    int index1 = this.Sections.IndexOf(sectionEditorInfo);
    if (this.dgAllItems.Rows.Count > 0)
    {
      this.dgAllItems.ClearSelection();
      if (sectionEditorInfo == null || index1 >= this.dgAllItems.Rows.Count || index1 == -1)
      {
        this.dgAllItems.Rows[0].Selected = true;
        num = 0;
      }
      else
        this.dgAllItems.Rows[index1].Selected = true;
    }
    --this.lockStatus;
    this.UpdateStatus();
  }

  private void UpdateStatus()
  {
    if (this.lockStatus > 0)
      return;
    this.bAddImBase.Enabled = this.ActiveSection != null;
    this.bDeleteImBase.Enabled = this.ActiveSection != null && this.dgImBase.SelectedRows.Count == 1;
    this.bEditImBase.Enabled = this.ActiveSection != null && this.dgImBase.SelectedRows.Count == 1;
    this.bTypeAdd.Enabled = this.ActiveSection != null;
    this.bTypeDelete.Enabled = this.ActiveSection != null && this.dgTypes.SelectedRows.Count == 1;
    this.bTypeEdit.Enabled = this.ActiveSection != null && this.dgTypes.SelectedRows.Count == 1;
    this.bAddSection.Enabled = true;
    bool flag = false;
    if (this.ActiveSection != null)
      flag = this.ActiveSection.SectionGuid.ToString().StartsWith("cad");
    this.bRemoveSection.Enabled = this.ActiveSection != null && !flag;
    this.bSectionDown.Enabled = this.ActiveSection != null && this.dgAllItems.SelectedRows.Count == 1 && this.dgAllItems.SelectedRows[0].Index < this.Sections.Count - 1;
    this.bSectionUp.Enabled = this.ActiveSection != null && this.dgAllItems.SelectedRows.Count == 1 && this.dgAllItems.SelectedRows[0].Index > 0;
    this.tName.Enabled = this.ActiveSection != null;
    this.tNumber.Enabled = this.ActiveSection != null;
    this.tSortIndex.Enabled = this.ActiveSection != null;
  }

  public void UpdateControls()
  {
    ++this.lockStatus;
    if (this.ActiveSection != null)
    {
      this.tName.Text = this.ActiveSection.Caption;
      if (this.avsCommonPropertiesSchema != null)
        this.tbTemplateName.Text = this.avsCommonPropertiesSchema.GetSectionCaption(this.ActiveSection.SectionGuid);
      this.tNumber.Value = this.ActiveSection.RazdelSP == -1L ? 0M : (Decimal) this.ActiveSection.RazdelSP;
      this.tSortIndex.Value = (Decimal) this.ActiveSection.SortIndex;
      this.dgTypes.Rows.Clear();
      foreach (SectionItem partType in (List<SectionItem>) this.ActiveSection.PartTypes)
        this.dgTypes.Rows.Add(new object[1]
        {
          this.ActiveSection.PartTypes.Converter.ConvertTo(partType.PropValue, typeof (string))
        });
      this.dgImBase.Rows.Clear();
      foreach (SectionItem imBaseCatalog in (List<SectionItem>) this.ActiveSection.ImBaseCatalogs)
        this.dgImBase.Rows.Add(new object[1]
        {
          this.ActiveSection.ImBaseCatalogs.Converter.ConvertTo(imBaseCatalog.PropValue, typeof (string))
        });
    }
    else
    {
      this.tName.Text = "";
      this.tbTemplateName.Text = "";
      this.tSortIndex.Value = 0M;
      this.tNumber.Value = 0M;
      this.dgImBase.Rows.Clear();
      this.dgTypes.Rows.Clear();
    }
    this.tName.ReadOnly = this.ActiveSection == null;
    this.tbTemplateName.ReadOnly = this.ActiveSection == null || !this.enableEditTemplate;
    this.tSortIndex.ReadOnly = this.ActiveSection == null;
    this.tNumber.ReadOnly = this.ActiveSection == null;
    this.dgImBase.ReadOnly = true;
    this.dgTypes.ReadOnly = true;
    this.panelInfo.Visible = !this.enableEditTemplate;
    this.textInfo.Text = string.Format("Шаблон спецификации взят на редактирование другим пользователем, редактирование списка разделов запрещено");
    --this.lockStatus;
    this.UpdateStatus();
  }

  private void bApply_Click(object sender, EventArgs e)
  {
    this.isValid = this.Validate();
    if (!this.isValid)
      return;
    this.StoreChanges();
    this.UpdateList();
  }

  private new bool Validate()
  {
    List<string> list = this.Sections.Where<SectionEditorInfo>((System.Func<SectionEditorInfo, bool>) (s => !s.Deleted)).GroupBy<SectionEditorInfo, long>((System.Func<SectionEditorInfo, long>) (ss => ss.RazdelSP)).Where<IGrouping<long, SectionEditorInfo>>((System.Func<IGrouping<long, SectionEditorInfo>, bool>) (g => g.Count<SectionEditorInfo>() > 1)).SelectMany<IGrouping<long, SectionEditorInfo>, SectionEditorInfo>((System.Func<IGrouping<long, SectionEditorInfo>, IEnumerable<SectionEditorInfo>>) (i => (IEnumerable<SectionEditorInfo>) i)).Select<SectionEditorInfo, string>((System.Func<SectionEditorInfo, string>) (i => i.Caption)).ToList<string>();
    if (list.Count > 0)
    {
      int num = (int) IMMessageBox.Show(this.Text, "Следующие разделы имеют неуникальный номер:", MessageBoxButtons.OK, (IList<string>) list);
      return false;
    }
    if (this.TemplateSections.Count != 0)
      return true;
    int num1 = (int) IMMessageBox.Show(this.Text, "Выберите хотя бы один допустимый раздел.", MessageBoxButtons.OK);
    return false;
  }

  private void bTypeEdit_Click(object sender, EventArgs e)
  {
    int index = this.dgTypes.SelectedRows[0].Index;
    UITypeEditor editor = this.ActiveSection.PartTypes.Editor;
    if (this.ActiveSection.PartTypes.Editor != null)
    {
      object obj = editor.EditValue((System.IServiceProvider) null, this.ActiveSection.PartTypes[index].PropValue);
      this.ActiveSection.PartTypes[index].PropValue = obj;
    }
    this.ActiveSection.Changed = true;
    this.UpdateControls();
  }

  private void bTypeAdd_Click(object sender, EventArgs e)
  {
    UITypeEditor editor = this.ActiveSection.PartTypes.Editor;
    if (this.ActiveSection.PartTypes.Editor != null)
    {
      object propDescriptorValue = this.ActiveSection.PartTypes.Describer.GetPropDescriptorValue((IElementInfo) null, this.ActiveSection.PartTypes.AttrId, (object) null);
      object obj = editor.EditValue((System.IServiceProvider) null, propDescriptorValue);
      if (new SectionItem(obj, true, this.ActiveSection.PartTypes).Value is Guid guid1 && guid1 != Guid.Empty)
      {
        this.ActiveSection.PartTypes.RemoveAll((Predicate<SectionItem>) (pt => pt.Value is Guid guid && guid == Guid.Empty));
        this.ActiveSection.PartTypes.Add(new SectionItem(obj, true, this.ActiveSection.PartTypes));
      }
    }
    this.ActiveSection.Changed = true;
    this.UpdateControls();
  }

  private void bTypeDelete_Click(object sender, EventArgs e)
  {
    this.ActiveSection.PartTypes.RemoveAt(this.dgTypes.SelectedRows[0].Index);
    this.ActiveSection.Changed = true;
    this.UpdateControls();
  }

  private void bAddImBase_Click(object sender, EventArgs e)
  {
    UITypeEditor editor = this.ActiveSection.ImBaseCatalogs.Editor;
    if (this.ActiveSection.ImBaseCatalogs.Editor != null)
    {
      object propDescriptorValue = this.ActiveSection.ImBaseCatalogs.Describer.GetPropDescriptorValue((IElementInfo) null, this.ActiveSection.ImBaseCatalogs.AttrId, (object) null);
      object obj = editor.EditValue((System.IServiceProvider) null, propDescriptorValue);
      if (obj != null && obj.ToString() != null)
        this.ActiveSection.ImBaseCatalogs.Add(new SectionItem(obj, true, this.ActiveSection.ImBaseCatalogs));
    }
    this.ActiveSection.Changed = true;
    this.UpdateControls();
  }

  private void bEditImBase_Click(object sender, EventArgs e)
  {
    int index = this.dgImBase.SelectedRows[0].Index;
    UITypeEditor editor = this.ActiveSection.ImBaseCatalogs.Editor;
    if (this.ActiveSection.ImBaseCatalogs.Editor != null)
    {
      object obj = editor.EditValue((System.IServiceProvider) null, this.ActiveSection.ImBaseCatalogs[index].PropValue);
      this.ActiveSection.ImBaseCatalogs[index].PropValue = obj;
    }
    this.ActiveSection.Changed = true;
    this.UpdateControls();
  }

  private void bDeleteImBase_Click(object sender, EventArgs e)
  {
    this.ActiveSection.ImBaseCatalogs.RemoveAt(this.dgImBase.SelectedRows[0].Index);
    this.ActiveSection.Changed = true;
    this.UpdateControls();
  }

  private void tName_Validated(object sender, EventArgs e)
  {
    if (sender == this.tName)
    {
      if (this.ActiveSection.Caption != this.tName.Text)
        this.ActiveSection.Changed = true;
      this.ActiveSection.Caption = this.tName.Text;
    }
    if (sender == this.tNumber)
    {
      if ((Decimal) this.ActiveSection.RazdelSP != this.tNumber.Value)
        this.ActiveSection.Changed = true;
      this.ActiveSection.RazdelSP = (long) this.tNumber.Value;
    }
    if (sender == this.tSortIndex)
    {
      if ((Decimal) this.ActiveSection.SortIndex != this.tSortIndex.Value)
        this.ActiveSection.Changed = true;
      this.ActiveSection.SortIndex = (long) this.tSortIndex.Value;
    }
    this.UpdateList();
  }

  private void bAddSection_Click(object sender, EventArgs e)
  {
    SectionEditorInfo sectionEditorInfo = new SectionEditorInfo();
    sectionEditorInfo.New = true;
    sectionEditorInfo.Changed = true;
    sectionEditorInfo.SortIndex = this.Sections.Count <= 0 ? 10L : this.Sections.Max<SectionEditorInfo>((System.Func<SectionEditorInfo, long>) (x => x.SortIndex)) + 10L;
    this.Sections.Add(sectionEditorInfo);
    this.UpdateList();
    this.dgAllItems.Rows[this.Sections.IndexOf(sectionEditorInfo)].Selected = true;
  }

  private void bRemoveSection_Click(object sender, EventArgs e)
  {
    if (this.ActiveSection.New)
    {
      this.Sections.Remove(this.ActiveSection);
    }
    else
    {
      if (this.TemplateSections.Contains(this.ActiveSection))
      {
        this.TemplateSections.Remove(this.ActiveSection);
        this.templSectionsToRemove.Add(this.ActiveSection);
      }
      this.ActiveSection.Deleted = true;
    }
    this.UpdateList();
    this.ActiveSection = (SectionEditorInfo) null;
    this.UpdateControls();
  }

  private void bSectionUp_Click(object sender, EventArgs e)
  {
    SectionEditorInfo section1 = this.Sections[this.dgAllItems.SelectedRows[0].Index];
    SectionEditorInfo section2 = this.Sections[this.dgAllItems.SelectedRows[0].Index - 1];
    this.SwapSiblingSections(section2, section1);
    section1.Changed = true;
    section2.Changed = true;
    this.UpdateList();
  }

  private void bSectionDown_Click(object sender, EventArgs e)
  {
    SectionEditorInfo section1 = this.Sections[this.dgAllItems.SelectedRows[0].Index];
    SectionEditorInfo section2 = this.Sections[this.dgAllItems.SelectedRows[0].Index + 1];
    this.SwapSiblingSections(section1, section2);
    section1.Changed = true;
    section2.Changed = true;
    this.UpdateList();
  }

  /// <summary>Поменять местами соседние секции</summary>
  /// <param name="firstSection">первая секция</param>
  /// <param name="secondSection">следующая за ней секция</param>
  private void SwapSiblingSections(SectionEditorInfo firstSection, SectionEditorInfo secondSection)
  {
    long sortIndex = firstSection.SortIndex;
    firstSection.SortIndex = secondSection.SortIndex;
    secondSection.SortIndex = sortIndex;
    if (firstSection.SortIndex != secondSection.SortIndex)
      return;
    int index = this.Sections.IndexOf(secondSection) + 1;
    if (index < this.Sections.Count)
    {
      SectionEditorInfo section = this.Sections[index];
      firstSection.SortIndex = (section.SortIndex + firstSection.SortIndex) / 2L;
    }
    else
      firstSection.SortIndex += 10L;
  }

  private void dgTypes_SelectionChanged(object sender, EventArgs e) => this.UpdateStatus();

  private void Save()
  {
    Dictionary<string, List<SectionEditorInfo>> changeList = new Dictionary<string, List<SectionEditorInfo>>();
    changeList["new"] = new List<SectionEditorInfo>();
    changeList["upd"] = new List<SectionEditorInfo>();
    changeList["del"] = new List<SectionEditorInfo>();
    List<SectionEditorInfo> sectionEditorInfoList = new List<SectionEditorInfo>();
    sectionEditorInfoList.AddRange((IEnumerable<SectionEditorInfo>) this.Sections);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (SectionEditorInfo sectionEditorInfo in sectionEditorInfoList)
      {
        if (sectionEditorInfo.Deleted || sectionEditorInfo.Changed || sectionEditorInfo.New)
        {
          try
          {
            IDBObject dbObj = (IDBObject) null;
            if (sectionEditorInfo.SectionGuid != Guid.Empty)
              dbObj = sessionKeeper.Session.GetObject(sectionEditorInfo.SectionGuid);
            if (sectionEditorInfo.Deleted && dbObj != null)
            {
              dbObj.Delete(0L);
              this.Sections.Remove(sectionEditorInfo);
              changeList["del"].Add(sectionEditorInfo);
            }
            else
            {
              if (sectionEditorInfo.New && dbObj == null)
              {
                dbObj = sessionKeeper.Session.GetObjectCollection(AvsIDCache.ObjType_SpecificationSection).Create();
                sectionEditorInfo.New = false;
                sectionEditorInfo.SectionGuid = dbObj.ObjectGUID;
                sectionEditorInfo.SectionID = dbObj.ObjectID;
                sectionEditorInfo.SectionType = dbObj.ObjectType;
                dbObj.CommitCreation(true);
                changeList["new"].Add(sectionEditorInfo);
              }
              if (sectionEditorInfo.Changed)
              {
                if (dbObj != null)
                {
                  DBObjectHelper.SetDBAttributeValues(dbObj, sectionEditorInfo.GetAttrubuteValues().ToArray());
                  sectionEditorInfo.Changed = false;
                  changeList["upd"].Add(sectionEditorInfo);
                }
              }
            }
          }
          catch (Exception ex)
          {
            ExceptionHelper.ExceptionService.ShowException(ex);
          }
        }
      }
      IDBObject dbObject = sessionKeeper.Session.GetObject(this.TemplateId, false);
      if (dbObject != null)
      {
        bool flag = false;
        if (dbObject.CheckoutBy == 0L && dbObject.ObjectModifyMode == ObjectModifyModes.Checkout)
        {
          dbObject = dbObject.CheckOut();
          flag = true;
        }
        if (dbObject != null && (dbObject.CheckoutBy == sessionKeeper.Session.UserID || dbObject.ObjectModifyMode == ObjectModifyModes.InBase))
        {
          object[] initValues = this.TemplateSections.Select<SectionEditorInfo, long>((System.Func<SectionEditorInfo, long>) (s => s.SectionID)).Distinct<long>().OfType<object>().ToArray<object>();
          if (initValues.Length == 0)
            initValues = (object[]) null;
          IDBAttribute attributeById = dbObject.GetAttributeByID(AvsIDCache.Attr_AllowableSections);
          if (attributeById == null)
          {
            if (initValues != null)
              dbObject.Attributes.AddAttribute(AvsIDCache.Attr_AllowableSections, false, initValues);
          }
          else if (initValues != null)
            attributeById.Values = initValues;
          else
            attributeById.ClearValues();
          if (this.avsCommonPropertiesSchema != null)
          {
            this.avsCommonPropertiesSchema.SaveParams();
            if (AVSPlugin.NotificationService != null)
              AVSPlugin.NotificationService.FireEvent((object) this, (NotificationEventArgs) new DBObjectsExtendedEventArgs(dbObject.ObjectID, dbObject.ObjectType, new AttributeValues(AvsIDCache.Attr_AllowableSections, (object) null), new AttributeValues(AvsIDCache.Attr_AllowableSections, (object) null)));
          }
          this.UpdateSortingScheme(this.TemplateId, changeList);
          if (flag)
            dbObject.CheckIn();
        }
      }
      SpecificationSectionInfo.CacheSpecSections(sessionKeeper.Session);
      SpecificationSectionInfo.CacheSpecSections(sessionKeeper.Session, this.TemplateId, new AVSDocumentType?());
    }
  }

  private void UpdateSortingScheme(
    long templateId,
    Dictionary<string, List<SectionEditorInfo>> changeList)
  {
    if (templateId.IsUndefinedId() || changeList.All<KeyValuePair<string, List<SectionEditorInfo>>>((System.Func<KeyValuePair<string, List<SectionEditorInfo>>, bool>) (e => e.Value.Count == 0)))
      return;
    FormSetupSorting templateSetupSorting = AVSPlugin.GetTemplateSetupSorting(AVSPlugin.Instance.activeImDocumentEditorForm?.Document, templateId, this._settingsHolderObjType, -1L);
    foreach (SectionEditorInfo sectionEditorInfo in changeList["new"])
      templateSetupSorting.AddSectionToScheme(sectionEditorInfo.SectionGuid, sectionEditorInfo.Caption, this.Sections.IndexOf(sectionEditorInfo));
    foreach (SectionEditorInfo sectionEditorInfo in changeList["del"])
      templateSetupSorting.RemoveSectionFromScheme(sectionEditorInfo.SectionGuid);
    templateSetupSorting.Changed = true;
    templateSetupSorting.SaveChanges();
  }

  private void bOk_Click(object sender, EventArgs e)
  {
    this.isValid = this.Validate();
    if (!this.isValid)
      return;
    this.StoreChanges();
  }

  /// <summary>Сохранить изменения с учетом дополнительных действий</summary>
  private void StoreChanges()
  {
    try
    {
      foreach (SectionEditorInfo sectionEditorInfo in this.templSectionsToRemove)
        sectionEditorInfo.Deleted = false;
      this.Save();
      foreach (SectionEditorInfo sectionEditorInfo in this.templSectionsToRemove)
      {
        sectionEditorInfo.Deleted = true;
        if (this.templSectionsToAdd.Contains(sectionEditorInfo))
          this.templSectionsToAdd.Remove(sectionEditorInfo);
      }
      if (this.templSectionsToAdd.Count > 0)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          List<SectionEditorInfo> allowableSpecSections = SectionEditorInfo.GetAllowableSpecSections(sessionKeeper.Session);
          foreach (SectionEditorInfo sectionEditorInfo1 in this.templSectionsToAdd)
          {
            SectionEditorInfo t = sectionEditorInfo1;
            SectionEditorInfo sectionEditorInfo2 = allowableSpecSections.FirstOrDefault<SectionEditorInfo>((System.Func<SectionEditorInfo, bool>) (s => s.RazdelSP == t.RazdelSP));
            long num = sectionEditorInfo2 != null ? sectionEditorInfo2.SectionID : t.SectionID;
            t.SectionID = num;
            if (this.TemplateSections.All<SectionEditorInfo>((System.Func<SectionEditorInfo, bool>) (ts => ts.RazdelSP != t.RazdelSP)))
              this.TemplateSections.Add(t);
          }
        }
      }
      this.Save();
    }
    finally
    {
      this.templSectionsToRemove.Clear();
      this.templSectionsToAdd.Clear();
    }
  }

  private void dgItems_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
  {
    if (Math.Abs(e.Location.X - this.pos.X) <= 2 && Math.Abs(e.Location.Y - this.pos.Y) <= 2)
      return;
    this.pos = e.Location;
    if (!this.mousePressed || e.Button != MouseButtons.Left || this.dgAllItems.SelectedRows.Count != 1)
      return;
    int num = (int) this.dgAllItems.DoDragDrop((object) this.Sections[this.dgAllItems.SelectedRows[0].Index], DragDropEffects.Move);
  }

  private void dgItems_DragOver(object sender, DragEventArgs e)
  {
    if (!(e.Data.GetData(typeof (SectionEditorInfo)) is SectionEditorInfo))
      return;
    Point client = this.dgAllItems.PointToClient(new Point(e.X, e.Y));
    SectionEditorInfo sectionEditorInfo1 = (SectionEditorInfo) null;
    SectionEditorInfo sectionEditorInfo2 = (SectionEditorInfo) null;
    bool flag = false;
    DataGridView.HitTestInfo hitTestInfo = this.dgAllItems.HitTest(client.X, client.Y);
    if (hitTestInfo.RowIndex != -1)
    {
      sectionEditorInfo1 = this.Sections[hitTestInfo.RowIndex];
      if (hitTestInfo.RowIndex < this.Sections.Count - 1)
        sectionEditorInfo2 = this.Sections[hitTestInfo.RowIndex + 1];
    }
    if (sectionEditorInfo1 == null || sectionEditorInfo2 == null)
      flag = true;
    else if (sectionEditorInfo1.SortIndex != sectionEditorInfo2.SortIndex - 1L)
      flag = true;
    if (flag)
      e.Effect = DragDropEffects.Move;
    else
      e.Effect = DragDropEffects.None;
  }

  private void dgItems_DragDrop(object sender, DragEventArgs e)
  {
    this.mousePressed = false;
    SectionEditorInfo data = e.Data.GetData(typeof (SectionEditorInfo)) as SectionEditorInfo;
    Point client = this.dgAllItems.PointToClient(new Point(e.X, e.Y));
    SectionEditorInfo sectionEditorInfo1 = (SectionEditorInfo) null;
    SectionEditorInfo sectionEditorInfo2 = (SectionEditorInfo) null;
    long num1 = -1;
    DataGridView.HitTestInfo hitTestInfo = this.dgAllItems.HitTest(client.X, client.Y);
    if (hitTestInfo.RowIndex != -1)
    {
      sectionEditorInfo1 = this.Sections[hitTestInfo.RowIndex];
      if (hitTestInfo.RowIndex < this.Sections.Count - 1)
        sectionEditorInfo2 = this.Sections[hitTestInfo.RowIndex + 1];
    }
    if (sectionEditorInfo1 == null)
      sectionEditorInfo1 = this.Sections[this.Sections.Count - 1];
    if (sectionEditorInfo2 == null)
      num1 = sectionEditorInfo1.SortIndex + 10L;
    else if (sectionEditorInfo1.SortIndex != sectionEditorInfo2.SortIndex - 1L)
      num1 = sectionEditorInfo1.SortIndex + (sectionEditorInfo2.SortIndex - sectionEditorInfo1.SortIndex) / 2L;
    if (sectionEditorInfo1 == data)
      num1 = -1L;
    if (num1 == -1L)
      return;
    long sortIndex = data.SortIndex;
    data.SortIndex = num1;
    long num2 = num1;
    if (sortIndex == num2)
      return;
    data.Changed = true;
    this.UpdateList();
  }

  private void dgAllItems_SelectionChanged(object sender, EventArgs e)
  {
    this.ActiveSection = this.dgAllItems.SelectedRows.Count != 1 ? (SectionEditorInfo) null : this.Sections[this.dgAllItems.SelectedRows[0].Index];
    this.UpdateControls();
    this.UpdateStatus();
  }

  private void dgAllItems_MouseDoubleClick(object sender, MouseEventArgs e)
  {
    this.bAdd_Click((object) null, EventArgs.Empty);
  }

  private void bAdd_Click(object sender, EventArgs e)
  {
    if (this.dgAllItems.SelectedRows.Count <= 0)
      return;
    for (int index = 0; index < this.dgAllItems.SelectedRows.Count; ++index)
    {
      SectionEditorInfo section = this.Sections[this.dgAllItems.SelectedRows[index].Index];
      if (section != null && !this.TemplateSections.Contains(section))
        this.TemplateSections.Add(section);
    }
  }

  private void dgAllItems_MouseDown(object sender, MouseEventArgs e)
  {
  }

  private void dgAllItems_MouseUp(object sender, MouseEventArgs e) => this.mousePressed = false;

  private void dgAllItems_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
  {
    this.mousePressed = true;
  }

  private void dgAllItems_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
  {
    this.mousePressed = false;
  }

  private void dgAllItems_CellValueChanged(object sender, DataGridViewCellEventArgs e)
  {
  }

  private void dgAllItems_CellEndEdit(object sender, DataGridViewCellEventArgs e)
  {
  }

  private void dgAllItems_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
  {
  }

  private void dgAllItems_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
  {
  }

  private void dgAllItems_CellValidated(object sender, DataGridViewCellEventArgs e)
  {
  }

  private void dgAllItems_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
  {
    if (e.ColumnIndex != 1 || !this.enableEditTemplate)
      return;
    bool flag = (bool) this.dgAllItems.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
    this.dgAllItems.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = (object) !flag;
    SectionEditorInfo tag = this.dgAllItems.Rows[e.RowIndex].Tag as SectionEditorInfo;
    if (this.TemplateSections.Contains(tag))
      this.TemplateSections.Remove(tag);
    else if (tag.New)
      this.templSectionsToAdd.Add(tag);
    else
      this.TemplateSections.Add(tag);
  }

  private void tbTemplateName_Validated(object sender, EventArgs e)
  {
    string text = this.tbTemplateName.Text;
    if (this.avsCommonPropertiesSchema == null || this.ActiveSection == null)
      return;
    this.avsCommonPropertiesSchema.SetSectionCaption(this.ActiveSection.SectionGuid, text);
  }

  private void dgAllItems_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
  {
    DataGridViewCell cell = this.dgAllItems.Rows[e.RowIndex].Cells[1];
    if (!(cell is DataGridViewCheckBoxCell viewCheckBoxCell) || this.enableEditTemplate)
      return;
    viewCheckBoxCell.FlatStyle = FlatStyle.Flat;
    viewCheckBoxCell.Style.ForeColor = Color.DarkGray;
    cell.ReadOnly = true;
  }

  private void SpecSectionsEditor_FormClosing(object sender, FormClosingEventArgs e)
  {
    e.Cancel = !this.isValid && this.DialogResult == DialogResult.OK;
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
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SpecSectionsEditor));
    this._dataSet = new DataSet();
    this.tName = new TextBox();
    this.label1 = new Label();
    this.label2 = new Label();
    this.label3 = new Label();
    this.label4 = new Label();
    this.tSortIndex = new NumericUpDown();
    this.label5 = new Label();
    this.bCancel = new Button();
    this.bApply = new Button();
    this.bOk = new Button();
    this.tNumber = new NumericUpDown();
    this.dgTypes = new DataGridView();
    this.Column2 = new DataGridViewTextBoxColumn();
    this.dgImBase = new DataGridView();
    this.Column3 = new DataGridViewTextBoxColumn();
    this.imageList1 = new ImageList(this.components);
    this.dgAllItems = new DataGridView();
    this.dataGridViewImageColumn1 = new DataGridViewImageColumn();
    this.Column5 = new DataGridViewCheckBoxColumn();
    this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
    this.tbTemplateName = new TextBox();
    this.label6 = new Label();
    this.panelInfo = new Panel();
    this.textInfo = new TextBox();
    this.pictureInfo = new PictureBox();
    this.bAddSection = new Button();
    this.bRemoveSection = new Button();
    this.bSectionDown = new Button();
    this.bSectionUp = new Button();
    this.bDeleteImBase = new Button();
    this.bTypeDelete = new Button();
    this.bEditImBase = new Button();
    this.bTypeEdit = new Button();
    this.bAddImBase = new Button();
    this.bTypeAdd = new Button();
    this._dataSet.BeginInit();
    this.tSortIndex.BeginInit();
    this.tNumber.BeginInit();
    ((ISupportInitialize) this.dgTypes).BeginInit();
    ((ISupportInitialize) this.dgImBase).BeginInit();
    ((ISupportInitialize) this.dgAllItems).BeginInit();
    this.panelInfo.SuspendLayout();
    ((ISupportInitialize) this.pictureInfo).BeginInit();
    this.SuspendLayout();
    this._dataSet.DataSetName = "NewDataSet";
    this.tName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.tName.Location = new Point(343, 18);
    this.tName.Name = "tName";
    this.tName.Size = new Size(338, 20);
    this.tName.TabIndex = 2;
    this.tName.Validated += new EventHandler(this.tName_Validated);
    this.label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.label1.AutoSize = true;
    this.label1.Location = new Point(529, 252);
    this.label1.Name = "label1";
    this.label1.Size = new Size(107, 13);
    this.label1.TabIndex = 7;
    this.label1.Text = "Индекс сортировки";
    this.label1.Visible = false;
    this.label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.label2.AutoSize = true;
    this.label2.Location = new Point(340, 2);
    this.label2.Name = "label2";
    this.label2.Size = new Size(83, 13);
    this.label2.TabIndex = 8;
    this.label2.Text = "Наименование";
    this.label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.label3.AutoSize = true;
    this.label3.Location = new Point(340, 41);
    this.label3.Name = "label3";
    this.label3.Size = new Size(86, 13);
    this.label3.TabIndex = 9;
    this.label3.Text = "Номер раздела";
    this.label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.label4.AutoSize = true;
    this.label4.Location = new Point(340, 168);
    this.label4.Name = "label4";
    this.label4.Size = new Size(142, 13);
    this.label4.TabIndex = 10;
    this.label4.Text = "Ссылка на каталог ImBase";
    this.tSortIndex.Location = new Point(388, 313);
    this.tSortIndex.Maximum = new Decimal(new int[4]
    {
      1410065408,
      2,
      0,
      0
    });
    this.tSortIndex.Name = "tSortIndex";
    this.tSortIndex.Size = new Size(257, 20);
    this.tSortIndex.TabIndex = 11;
    this.tSortIndex.Visible = false;
    this.tSortIndex.Validated += new EventHandler(this.tName_Validated);
    this.label5.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.label5.AutoSize = true;
    this.label5.Location = new Point(340, 82);
    this.label5.Name = "label5";
    this.label5.Size = new Size(100, 13);
    this.label5.TabIndex = 10;
    this.label5.Text = "Допустимые типы";
    this.bCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Location = new Point(433, 323);
    this.bCancel.Name = "bCancel";
    this.bCancel.Size = new Size(121, 27);
    this.bCancel.TabIndex = 12;
    this.bCancel.Text = "Отмена";
    this.bCancel.UseVisualStyleBackColor = true;
    this.bApply.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bApply.Location = new Point(560, 323);
    this.bApply.Name = "bApply";
    this.bApply.Size = new Size(121, 27);
    this.bApply.TabIndex = 13;
    this.bApply.Text = "Применить";
    this.bApply.UseVisualStyleBackColor = true;
    this.bApply.Click += new EventHandler(this.bApply_Click);
    this.bOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bOk.DialogResult = DialogResult.OK;
    this.bOk.Location = new Point(306, 323);
    this.bOk.Name = "bOk";
    this.bOk.Size = new Size(121, 27);
    this.bOk.TabIndex = 14;
    this.bOk.Text = "ОК";
    this.bOk.UseVisualStyleBackColor = true;
    this.bOk.Click += new EventHandler(this.bOk_Click);
    this.tNumber.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.tNumber.Location = new Point(343, 57);
    this.tNumber.Maximum = new Decimal(new int[4]
    {
      -1304428544 /*0xB2400000*/,
      434162106,
      542,
      0
    });
    this.tNumber.Name = "tNumber";
    this.tNumber.Size = new Size(338, 20);
    this.tNumber.TabIndex = 11;
    this.tNumber.Validated += new EventHandler(this.tName_Validated);
    this.dgTypes.AllowUserToAddRows = false;
    this.dgTypes.AllowUserToDeleteRows = false;
    this.dgTypes.AllowUserToResizeColumns = false;
    this.dgTypes.AllowUserToResizeRows = false;
    this.dgTypes.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.dgTypes.BackgroundColor = Color.White;
    this.dgTypes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
    this.dgTypes.ColumnHeadersVisible = false;
    this.dgTypes.Columns.AddRange((DataGridViewColumn) this.Column2);
    this.dgTypes.Location = new Point(343, 98);
    this.dgTypes.MultiSelect = false;
    this.dgTypes.Name = "dgTypes";
    this.dgTypes.ReadOnly = true;
    this.dgTypes.RowHeadersVisible = false;
    this.dgTypes.RowTemplate.Height = 20;
    this.dgTypes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
    this.dgTypes.Size = new Size(316, 65);
    this.dgTypes.TabIndex = 15;
    this.dgTypes.SelectionChanged += new EventHandler(this.dgTypes_SelectionChanged);
    this.Column2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    this.Column2.HeaderText = "Column2";
    this.Column2.Name = "Column2";
    this.Column2.ReadOnly = true;
    this.dgImBase.AllowUserToAddRows = false;
    this.dgImBase.AllowUserToDeleteRows = false;
    this.dgImBase.AllowUserToResizeColumns = false;
    this.dgImBase.AllowUserToResizeRows = false;
    this.dgImBase.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
    this.dgImBase.BackgroundColor = Color.White;
    this.dgImBase.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
    this.dgImBase.ColumnHeadersVisible = false;
    this.dgImBase.Columns.AddRange((DataGridViewColumn) this.Column3);
    this.dgImBase.Location = new Point(343, 184);
    this.dgImBase.MultiSelect = false;
    this.dgImBase.Name = "dgImBase";
    this.dgImBase.ReadOnly = true;
    this.dgImBase.RowHeadersVisible = false;
    this.dgImBase.RowTemplate.Height = 20;
    this.dgImBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
    this.dgImBase.Size = new Size(316, 65);
    this.dgImBase.TabIndex = 15;
    this.dgImBase.SelectionChanged += new EventHandler(this.dgTypes_SelectionChanged);
    this.Column3.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    this.Column3.HeaderText = "Column3";
    this.Column3.Name = "Column3";
    this.Column3.ReadOnly = true;
    this.imageList1.ColorDepth = ColorDepth.Depth32Bit;
    this.imageList1.ImageSize = new Size(16 /*0x10*/, 16 /*0x10*/);
    this.imageList1.TransparentColor = Color.Transparent;
    this.dgAllItems.AllowDrop = true;
    this.dgAllItems.AllowUserToAddRows = false;
    this.dgAllItems.AllowUserToDeleteRows = false;
    this.dgAllItems.AllowUserToResizeColumns = false;
    this.dgAllItems.AllowUserToResizeRows = false;
    this.dgAllItems.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.dgAllItems.BackgroundColor = Color.White;
    this.dgAllItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
    this.dgAllItems.ColumnHeadersVisible = false;
    this.dgAllItems.Columns.AddRange((DataGridViewColumn) this.dataGridViewImageColumn1, (DataGridViewColumn) this.Column5, (DataGridViewColumn) this.dataGridViewTextBoxColumn1);
    this.dgAllItems.Location = new Point(7, 9);
    this.dgAllItems.MultiSelect = false;
    this.dgAllItems.Name = "dgAllItems";
    this.dgAllItems.ReadOnly = true;
    this.dgAllItems.RowHeadersVisible = false;
    this.dgAllItems.RowTemplate.Height = 20;
    this.dgAllItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
    this.dgAllItems.Size = new Size(300, 288);
    this.dgAllItems.TabIndex = 15;
    this.dgAllItems.CellEndEdit += new DataGridViewCellEventHandler(this.dgAllItems_CellEndEdit);
    this.dgAllItems.CellMouseClick += new DataGridViewCellMouseEventHandler(this.dgAllItems_CellMouseClick);
    this.dgAllItems.CellMouseDown += new DataGridViewCellMouseEventHandler(this.dgAllItems_CellMouseDown);
    this.dgAllItems.CellMouseMove += new DataGridViewCellMouseEventHandler(this.dgItems_CellMouseMove);
    this.dgAllItems.CellMouseUp += new DataGridViewCellMouseEventHandler(this.dgAllItems_CellMouseUp);
    this.dgAllItems.CellParsing += new DataGridViewCellParsingEventHandler(this.dgAllItems_CellParsing);
    this.dgAllItems.CellValidated += new DataGridViewCellEventHandler(this.dgAllItems_CellValidated);
    this.dgAllItems.CellValidating += new DataGridViewCellValidatingEventHandler(this.dgAllItems_CellValidating);
    this.dgAllItems.CellValueChanged += new DataGridViewCellEventHandler(this.dgAllItems_CellValueChanged);
    this.dgAllItems.RowPostPaint += new DataGridViewRowPostPaintEventHandler(this.dgAllItems_RowPostPaint);
    this.dgAllItems.SelectionChanged += new EventHandler(this.dgAllItems_SelectionChanged);
    this.dgAllItems.DragDrop += new DragEventHandler(this.dgItems_DragDrop);
    this.dgAllItems.DragOver += new DragEventHandler(this.dgItems_DragOver);
    this.dgAllItems.MouseDoubleClick += new MouseEventHandler(this.dgAllItems_MouseDoubleClick);
    this.dgAllItems.MouseDown += new MouseEventHandler(this.dgAllItems_MouseDown);
    this.dgAllItems.MouseUp += new MouseEventHandler(this.dgAllItems_MouseUp);
    this.dataGridViewImageColumn1.HeaderText = "Column4";
    this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
    this.dataGridViewImageColumn1.ReadOnly = true;
    this.dataGridViewImageColumn1.Resizable = DataGridViewTriState.False;
    this.dataGridViewImageColumn1.Width = 20;
    this.Column5.FillWeight = 20f;
    this.Column5.HeaderText = "Column5";
    this.Column5.Name = "Column5";
    this.Column5.ReadOnly = true;
    this.Column5.Resizable = DataGridViewTriState.False;
    this.Column5.Width = 20;
    this.dataGridViewTextBoxColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    this.dataGridViewTextBoxColumn1.HeaderText = "Column1";
    this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
    this.dataGridViewTextBoxColumn1.ReadOnly = true;
    this.tbTemplateName.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.tbTemplateName.Location = new Point(343, 277);
    this.tbTemplateName.Name = "tbTemplateName";
    this.tbTemplateName.Size = new Size(316, 20);
    this.tbTemplateName.TabIndex = 2;
    this.tbTemplateName.Validated += new EventHandler(this.tbTemplateName_Validated);
    this.label6.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.label6.AutoSize = true;
    this.label6.Location = new Point(340, 261);
    this.label6.Name = "label6";
    this.label6.Size = new Size(163, 13);
    this.label6.TabIndex = 8;
    this.label6.Text = "Заголовок раздела документа";
    this.panelInfo.BackColor = SystemColors.Info;
    this.panelInfo.BorderStyle = BorderStyle.Fixed3D;
    this.panelInfo.Controls.Add((Control) this.textInfo);
    this.panelInfo.Controls.Add((Control) this.pictureInfo);
    this.panelInfo.ForeColor = SystemColors.InfoText;
    this.panelInfo.Location = new Point(7, 303);
    this.panelInfo.Name = "panelInfo";
    this.panelInfo.Size = new Size(298, 56);
    this.panelInfo.TabIndex = 16 /*0x10*/;
    this.textInfo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.textInfo.BackColor = SystemColors.Info;
    this.textInfo.ForeColor = SystemColors.InfoText;
    this.textInfo.Location = new Point(30, 3);
    this.textInfo.Multiline = true;
    this.textInfo.Name = "textInfo";
    this.textInfo.ReadOnly = true;
    this.textInfo.Size = new Size(261, 46);
    this.textInfo.TabIndex = 2;
    this.textInfo.Text = "Шаблон спецификации взят на редактирование другим пользователем, редактирование списка разделов запрещено\r\n";
    this.pictureInfo.BackColor = SystemColors.Info;
    this.pictureInfo.Dock = DockStyle.Left;
    this.pictureInfo.Image = (Image) componentResourceManager.GetObject("pictureInfo.Image");
    this.pictureInfo.ImeMode = ImeMode.NoControl;
    this.pictureInfo.Location = new Point(0, 0);
    this.pictureInfo.Name = "pictureInfo";
    this.pictureInfo.Size = new Size(28, 52);
    this.pictureInfo.SizeMode = PictureBoxSizeMode.CenterImage;
    this.pictureInfo.TabIndex = 1;
    this.pictureInfo.TabStop = false;
    this.bAddSection.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bAddSection.Image = (Image) Resources.AddStandart;
    this.bAddSection.Location = new Point(313, 10);
    this.bAddSection.Name = "bAddSection";
    this.bAddSection.Size = new Size(23, 23);
    this.bAddSection.TabIndex = 12;
    this.bAddSection.TextAlign = ContentAlignment.TopCenter;
    this.bAddSection.UseVisualStyleBackColor = true;
    this.bAddSection.Click += new EventHandler(this.bAddSection_Click);
    this.bRemoveSection.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bRemoveSection.Image = (Image) Resources.DeleteStandart;
    this.bRemoveSection.Location = new Point(313, 39);
    this.bRemoveSection.Name = "bRemoveSection";
    this.bRemoveSection.Size = new Size(23, 23);
    this.bRemoveSection.TabIndex = 12;
    this.bRemoveSection.TextAlign = ContentAlignment.TopCenter;
    this.bRemoveSection.UseVisualStyleBackColor = true;
    this.bRemoveSection.Click += new EventHandler(this.bRemoveSection_Click);
    this.bSectionDown.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bSectionDown.Image = (Image) Resources.arrow_down_blueStandart;
    this.bSectionDown.Location = new Point(313, 97);
    this.bSectionDown.Name = "bSectionDown";
    this.bSectionDown.Size = new Size(23, 23);
    this.bSectionDown.TabIndex = 12;
    this.bSectionDown.TextAlign = ContentAlignment.TopCenter;
    this.bSectionDown.UseVisualStyleBackColor = true;
    this.bSectionDown.Click += new EventHandler(this.bSectionDown_Click);
    this.bSectionUp.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bSectionUp.Image = (Image) Resources.ArrowUp;
    this.bSectionUp.Location = new Point(313, 68);
    this.bSectionUp.Name = "bSectionUp";
    this.bSectionUp.Size = new Size(23, 23);
    this.bSectionUp.TabIndex = 12;
    this.bSectionUp.TextAlign = ContentAlignment.TopCenter;
    this.bSectionUp.UseVisualStyleBackColor = true;
    this.bSectionUp.Click += new EventHandler(this.bSectionUp_Click);
    this.bDeleteImBase.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bDeleteImBase.Image = (Image) componentResourceManager.GetObject("bDeleteImBase.Image");
    this.bDeleteImBase.Location = new Point(658, 227);
    this.bDeleteImBase.Name = "bDeleteImBase";
    this.bDeleteImBase.Size = new Size(23, 23);
    this.bDeleteImBase.TabIndex = 6;
    this.bDeleteImBase.UseVisualStyleBackColor = true;
    this.bDeleteImBase.Click += new EventHandler(this.bDeleteImBase_Click);
    this.bTypeDelete.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bTypeDelete.Image = (Image) componentResourceManager.GetObject("bTypeDelete.Image");
    this.bTypeDelete.Location = new Point(658, 141);
    this.bTypeDelete.Name = "bTypeDelete";
    this.bTypeDelete.Size = new Size(23, 23);
    this.bTypeDelete.TabIndex = 6;
    this.bTypeDelete.UseVisualStyleBackColor = true;
    this.bTypeDelete.Click += new EventHandler(this.bTypeDelete_Click);
    this.bEditImBase.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bEditImBase.Image = (Image) componentResourceManager.GetObject("bEditImBase.Image");
    this.bEditImBase.Location = new Point(658, 205);
    this.bEditImBase.Name = "bEditImBase";
    this.bEditImBase.Size = new Size(23, 23);
    this.bEditImBase.TabIndex = 6;
    this.bEditImBase.UseVisualStyleBackColor = true;
    this.bEditImBase.Click += new EventHandler(this.bEditImBase_Click);
    this.bTypeEdit.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bTypeEdit.Image = (Image) componentResourceManager.GetObject("bTypeEdit.Image");
    this.bTypeEdit.Location = new Point(658, 119);
    this.bTypeEdit.Name = "bTypeEdit";
    this.bTypeEdit.Size = new Size(23, 23);
    this.bTypeEdit.TabIndex = 6;
    this.bTypeEdit.UseVisualStyleBackColor = true;
    this.bTypeEdit.Click += new EventHandler(this.bTypeEdit_Click);
    this.bAddImBase.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bAddImBase.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    this.bAddImBase.Image = (Image) Resources.AddStandart;
    this.bAddImBase.Location = new Point(658, 183);
    this.bAddImBase.Name = "bAddImBase";
    this.bAddImBase.Size = new Size(23, 23);
    this.bAddImBase.TabIndex = 6;
    this.bAddImBase.UseVisualStyleBackColor = true;
    this.bAddImBase.Click += new EventHandler(this.bAddImBase_Click);
    this.bTypeAdd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bTypeAdd.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    this.bTypeAdd.Image = (Image) Resources.AddStandart;
    this.bTypeAdd.Location = new Point(658, 97);
    this.bTypeAdd.Name = "bTypeAdd";
    this.bTypeAdd.Size = new Size(23, 23);
    this.bTypeAdd.TabIndex = 6;
    this.bTypeAdd.UseVisualStyleBackColor = true;
    this.bTypeAdd.Click += new EventHandler(this.bTypeAdd_Click);
    this.AcceptButton = (IButtonControl) this.bOk;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.bCancel;
    this.ClientSize = new Size(693, 362);
    this.Controls.Add((Control) this.panelInfo);
    this.Controls.Add((Control) this.dgImBase);
    this.Controls.Add((Control) this.dgTypes);
    this.Controls.Add((Control) this.dgAllItems);
    this.Controls.Add((Control) this.bOk);
    this.Controls.Add((Control) this.bApply);
    this.Controls.Add((Control) this.bAddSection);
    this.Controls.Add((Control) this.bRemoveSection);
    this.Controls.Add((Control) this.bSectionDown);
    this.Controls.Add((Control) this.bSectionUp);
    this.Controls.Add((Control) this.bCancel);
    this.Controls.Add((Control) this.tNumber);
    this.Controls.Add((Control) this.tSortIndex);
    this.Controls.Add((Control) this.label5);
    this.Controls.Add((Control) this.label4);
    this.Controls.Add((Control) this.label3);
    this.Controls.Add((Control) this.label6);
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.bDeleteImBase);
    this.Controls.Add((Control) this.bTypeDelete);
    this.Controls.Add((Control) this.bEditImBase);
    this.Controls.Add((Control) this.bTypeEdit);
    this.Controls.Add((Control) this.bAddImBase);
    this.Controls.Add((Control) this.bTypeAdd);
    this.Controls.Add((Control) this.tbTemplateName);
    this.Controls.Add((Control) this.tName);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MinimumSize = new Size(570, 390);
    this.Name = nameof (SpecSectionsEditor);
    this.ShowInTaskbar = false;
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Разделы конструкторского документа";
    this.FormClosing += new FormClosingEventHandler(this.SpecSectionsEditor_FormClosing);
    this._dataSet.EndInit();
    this.tSortIndex.EndInit();
    this.tNumber.EndInit();
    ((ISupportInitialize) this.dgTypes).EndInit();
    ((ISupportInitialize) this.dgImBase).EndInit();
    ((ISupportInitialize) this.dgAllItems).EndInit();
    this.panelInfo.ResumeLayout(false);
    this.panelInfo.PerformLayout();
    ((ISupportInitialize) this.pictureInfo).EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
