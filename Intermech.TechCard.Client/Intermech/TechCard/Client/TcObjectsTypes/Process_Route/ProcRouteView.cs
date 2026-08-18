// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.Process_Route.ProcRouteView
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.TechCard;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.Process_Route;

/// <summary>Summary description for ProcRouteView.</summary>
[Obsolete("Will be removed in IPS 8.0")]
public class ProcRouteView : UserControl
{
  private GroupBox grbProcRoute;
  private Label lblDesign;
  private Label lblSborka;
  private Label label4;
  private Panel panel1;
  private Label lblName;
  private TextBox tbxName;
  private TextBox tbxDesignation;
  private TextBox tbxMemberZak;
  private Button btnMemberSbr;
  private Button btnMemberzak;
  private TextBox tbxMemberSbr;
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;
  /// <summary>Ид. версии изделия</summary>
  private long _artObjId;
  /// <summary>Ид. родительской сборки</summary>
  private long _memberSbr;
  /// <summary>Ид. версии заказа</summary>
  private long _memberZak;
  /// <summary>Ид. версии МО</summary>
  private long _procRoute;
  /// <summary>Флаг изменения объекта</summary>
  private bool _modified;
  /// <summary>Кнопка "ОК"</summary>
  public Button btnApply;
  /// <summary>Кнопка "Отмена"</summary>
  public Button btnCancel;

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ProcRouteView));
    this.grbProcRoute = new GroupBox();
    this.btnMemberzak = new Button();
    this.btnMemberSbr = new Button();
    this.tbxMemberZak = new TextBox();
    this.tbxMemberSbr = new TextBox();
    this.label4 = new Label();
    this.lblSborka = new Label();
    this.tbxDesignation = new TextBox();
    this.lblDesign = new Label();
    this.tbxName = new TextBox();
    this.lblName = new Label();
    this.panel1 = new Panel();
    this.btnCancel = new Button();
    this.btnApply = new Button();
    this.grbProcRoute.SuspendLayout();
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    this.grbProcRoute.Controls.Add((Control) this.btnMemberzak);
    this.grbProcRoute.Controls.Add((Control) this.btnMemberSbr);
    this.grbProcRoute.Controls.Add((Control) this.tbxMemberZak);
    this.grbProcRoute.Controls.Add((Control) this.tbxMemberSbr);
    this.grbProcRoute.Controls.Add((Control) this.label4);
    this.grbProcRoute.Controls.Add((Control) this.lblSborka);
    this.grbProcRoute.Controls.Add((Control) this.tbxDesignation);
    this.grbProcRoute.Controls.Add((Control) this.lblDesign);
    this.grbProcRoute.Controls.Add((Control) this.tbxName);
    this.grbProcRoute.Controls.Add((Control) this.lblName);
    componentResourceManager.ApplyResources((object) this.grbProcRoute, "grbProcRoute");
    this.grbProcRoute.Name = "grbProcRoute";
    this.grbProcRoute.TabStop = false;
    componentResourceManager.ApplyResources((object) this.btnMemberzak, "btnMemberzak");
    this.btnMemberzak.Name = "btnMemberzak";
    this.btnMemberzak.Click += new EventHandler(this.btnMemberzak_Click);
    componentResourceManager.ApplyResources((object) this.btnMemberSbr, "btnMemberSbr");
    this.btnMemberSbr.Name = "btnMemberSbr";
    this.btnMemberSbr.Click += new EventHandler(this.btnMemberSbr_Click);
    componentResourceManager.ApplyResources((object) this.tbxMemberZak, "tbxMemberZak");
    this.tbxMemberZak.Name = "tbxMemberZak";
    componentResourceManager.ApplyResources((object) this.tbxMemberSbr, "tbxMemberSbr");
    this.tbxMemberSbr.Name = "tbxMemberSbr";
    componentResourceManager.ApplyResources((object) this.label4, "label4");
    this.label4.Name = "label4";
    componentResourceManager.ApplyResources((object) this.lblSborka, "lblSborka");
    this.lblSborka.Name = "lblSborka";
    componentResourceManager.ApplyResources((object) this.tbxDesignation, "tbxDesignation");
    this.tbxDesignation.Name = "tbxDesignation";
    componentResourceManager.ApplyResources((object) this.lblDesign, "lblDesign");
    this.lblDesign.Name = "lblDesign";
    componentResourceManager.ApplyResources((object) this.tbxName, "tbxName");
    this.tbxName.Name = "tbxName";
    componentResourceManager.ApplyResources((object) this.lblName, "lblName");
    this.lblName.Name = "lblName";
    this.panel1.Controls.Add((Control) this.btnCancel);
    this.panel1.Controls.Add((Control) this.btnApply);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    this.btnCancel.DialogResult = DialogResult.Cancel;
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
    this.btnApply.DialogResult = DialogResult.OK;
    componentResourceManager.ApplyResources((object) this.btnApply, "btnApply");
    this.btnApply.Name = "btnApply";
    this.btnApply.Click += new EventHandler(this.btnApply_Click);
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.grbProcRoute);
    this.Name = nameof (ProcRouteView);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.grbProcRoute.ResumeLayout(false);
    this.grbProcRoute.PerformLayout();
    this.panel1.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  /// <summary>Clean up any resources being used.</summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>Get article object id for current process route</summary>
  /// <returns></returns>
  private long GetArtObjectID()
  {
    if (this._artObjId != 0L)
      return this._artObjId;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive((IEnumerable<int>) TechCardConsts.ObjectTypes.ArticleObjectTypes);
      ConditionStructure[] conditions = new ConditionStructure[1]
      {
        new ConditionStructure(-7, RelationalOperators.In, (object) childrenIdRecursive.ToArray(), LogicalOperators.NONE, 0, false)
      };
      List<TechCardUtils.SostavTreeItem> parentSostavTree = TechCardUtils.GetParentSostavTree(this.ProcRoute, sessionKeeper.Session, new int[1]
      {
        TechCardConsts.RelTypes.TechRelationID
      }, false, conditions);
      this._artObjId = 0L;
      if (parentSostavTree.Count != 0)
      {
        childrenIdRecursive.Sort();
        foreach (TechCardUtils.SostavTreeItem sostavTreeItem in parentSostavTree)
        {
          if (sostavTreeItem != null && childrenIdRecursive.BinarySearch(sostavTreeItem.ObjectTypeID) >= 0)
          {
            this._artObjId = sostavTreeItem.ProjID;
            break;
          }
        }
      }
      return this._artObjId;
    }
  }

  /// <summary>Выбор объекта - сборочной единицы</summary>
  private void SelectMemberSbrObj()
  {
    if (this._procRoute == 0L)
      return;
    Dictionary<long, int> objIdList = new Dictionary<long, int>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive((IEnumerable<int>) TechCardConsts.ObjectTypes.ArticleObjectTypes);
      ConditionStructure[] conditions = new ConditionStructure[1]
      {
        new ConditionStructure(-7, RelationalOperators.In, (object) childrenIdRecursive.ToArray(), LogicalOperators.NONE, 0, false)
      };
      List<TechCardUtils.SostavTreeItem> parentSostavTree = TechCardUtils.GetParentSostavTree(this.ArtObjectID, sessionKeeper.Session, new int[1]
      {
        TechCardConsts.RelTypes.ProektRelationID
      }, false, conditions);
      if (parentSostavTree != null)
      {
        childrenIdRecursive.Sort();
        foreach (TechCardUtils.SostavTreeItem sostavTreeItem in parentSostavTree)
        {
          if (sostavTreeItem != null && childrenIdRecursive.BinarySearch(sostavTreeItem.ObjectTypeID) >= 0)
            objIdList[sostavTreeItem.ProjID] = sostavTreeItem.ObjectTypeID;
        }
      }
    }
    List<long> longList = TechCardClientConst.SelectObjectOnlyDlg(TechCardConsts.ObjectTypes.ArticleBaseID, (IDictionary<long, int>) objIdList, LocalizationHolder.rm.GetString("TechCard.Client_210"), LocalizationHolder.rm.GetString("TechCard.Client_211"));
    if (longList == null || longList.Count <= 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this._memberSbr = longList[0];
      this.tbxMemberSbr.Text = TechCardConsts.Utils.GetObjectString(this._memberSbr, sessionKeeper.Session);
      this._memberZak = 0L;
      this.tbxMemberZak.Text = "";
    }
  }

  /// <summary>Выбор объекта - заказа</summary>
  private void SelectMemberZakObj()
  {
    if (this._procRoute == 0L)
      return;
    bool flag = false;
    string text = LocalizationHolder.rm.GetString(sc_19562.ssp_techcard_19563());
    if (MessageBox.Show(text, LocalizationHolder.rm.GetString("TechCard.Client_213"), MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
      flag = true;
    if (flag)
    {
      long num = this._memberSbr != 0L ? this._memberSbr : this.ArtObjectID;
      ConditionStructure[] conditions = new ConditionStructure[1]
      {
        new ConditionStructure(-7, RelationalOperators.In, (object) MetaDataHelper.GetObjectTypeChildrenIDRecursive((IEnumerable<int>) TechCardConsts.ObjectTypes.ArticleObjectTypes).ToArray(), LogicalOperators.NONE, 0, false)
      };
      List<TechCardUtils.SostavTreeItem> parentSostavTree;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        parentSostavTree = TechCardUtils.GetParentSostavTree(num, sessionKeeper.Session, new int[1]
        {
          TechCardConsts.RelTypes.ProektRelationID
        }, true, conditions);
      Dictionary<long, int> objIdList = new Dictionary<long, int>();
      if (parentSostavTree != null)
      {
        List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechCardConsts.ObjectTypes.ZakazObjectID);
        childrenIdRecursive.Sort();
        foreach (TechCardUtils.SostavTreeItem sostavTreeItem in parentSostavTree)
        {
          if (sostavTreeItem != null && childrenIdRecursive.BinarySearch(sostavTreeItem.ObjectTypeID) >= 0)
            objIdList.Add(sostavTreeItem.ProjID, sostavTreeItem.ObjectTypeID);
        }
      }
      if (objIdList.Count == 0)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          text = string.Format(LocalizationHolder.rm.GetString(sc_19562.ssp_techcard_19564()), (object) TechCardConsts.Utils.GetObjectString(num, sessionKeeper.Session), (object) num);
        if (MessageBox.Show(text, LocalizationHolder.rm.GetString("TechCard.Client_142"), MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
          flag = false;
      }
      else
      {
        List<long> longList = TechCardClientConst.SelectObjectOnlyDlg(TechCardConsts.ObjectTypes.ZakazObjectID, (IDictionary<long, int>) objIdList, LocalizationHolder.rm.GetString("TechCard.Client_215"), LocalizationHolder.rm.GetString("TechCard.Client_216"));
        if (longList != null && longList.Count > 0)
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            this._memberZak = longList[0];
            this.tbxMemberZak.Text = TechCardConsts.Utils.GetObjectString(this._memberZak, sessionKeeper.Session);
          }
        }
      }
    }
    if (flag)
      return;
    long objectId = TechCardClientConst.SelectObjectDlg(new Guid("cad00580-306c-11d8-b4e9-00304f19f545"), LocalizationHolder.rm.GetString(sc_19562.ssp_techcard_19565()));
    if (objectId == 0L)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this._memberZak = objectId;
      this.tbxMemberZak.Text = TechCardConsts.Utils.GetObjectString(objectId, sessionKeeper.Session);
    }
  }

  /// <summary>Обновление контролов формы</summary>
  private void UpdateControls()
  {
    this.btnApply.Enabled = this.btnCancel.Enabled = this._modified;
    this.btnMemberSbr.Enabled = this.btnMemberzak.Enabled = this._procRoute != 0L;
  }

  /// <summary>Load data</summary>
  private void LoadProcRoute()
  {
    if (this._procRoute == 0L)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this._procRoute, false);
      if (dbObject == null)
        return;
      AttributeValues[] attributesValues = dbObject.GetAttributesValues(GetAttributeValuesModes.IncludeGuid | GetAttributeValuesModes.IncludeDescriptions);
      Dictionary<Guid, AttributeValues> dictionary;
      if (attributesValues == null)
      {
        dictionary = new Dictionary<Guid, AttributeValues>();
      }
      else
      {
        dictionary = new Dictionary<Guid, AttributeValues>(attributesValues.Length);
        foreach (AttributeValues attributeValues in attributesValues)
        {
          if (attributeValues != null)
            dictionary.Add(attributeValues.AttributeGuid, attributeValues);
        }
      }
      Guid key = new Guid("cad00020-306c-11d8-b4e9-00304f19f545");
      this.tbxName.TextChanged -= new EventHandler(this.tbxTextChanged);
      AttributeValues attributeValues1;
      try
      {
        if (dictionary.TryGetValue(key, out attributeValues1))
          this.tbxName.Text = attributeValues1.Values.Length != 0 ? attributeValues1.Values[0].ToString() : string.Empty;
      }
      finally
      {
        this.tbxName.TextChanged += new EventHandler(this.tbxTextChanged);
      }
      key = new Guid("cad0001f-306c-11d8-b4e9-00304f19f545");
      this.tbxDesignation.TextChanged -= new EventHandler(this.tbxTextChanged);
      try
      {
        if (dictionary.TryGetValue(key, out attributeValues1))
          this.tbxDesignation.Text = attributeValues1.Values.Length != 0 ? attributeValues1.Values[0].ToString() : string.Empty;
      }
      finally
      {
        this.tbxDesignation.TextChanged += new EventHandler(this.tbxTextChanged);
      }
      Guid sborkaObjectAttrGuid = TechCardConsts.AttributeTypes.MemberOfSborkaObjectAttrGUID;
      this.tbxMemberSbr.TextChanged -= new EventHandler(this.tbxTextChanged);
      try
      {
        if (dictionary.TryGetValue(sborkaObjectAttrGuid, out attributeValues1))
        {
          long result = 0;
          if (attributeValues1.Values.Length != 0)
            long.TryParse(attributeValues1.Values[0].ToString(), out result);
          this._memberSbr = result;
          this.tbxMemberSbr.Text = attributeValues1.Descriptions.Length != 0 ? attributeValues1.Descriptions[0].ToString() : string.Empty;
        }
      }
      finally
      {
        this.tbxMemberSbr.TextChanged += new EventHandler(this.tbxTextChanged);
      }
      Guid zakazObjectAttrGuid = TechCardConsts.AttributeTypes.MemberOfZakazObjectAttrGUID;
      this.tbxMemberZak.TextChanged -= new EventHandler(this.tbxTextChanged);
      try
      {
        if (dictionary.TryGetValue(zakazObjectAttrGuid, out attributeValues1))
        {
          long result = 0;
          if (attributeValues1.Values.Length != 0)
            long.TryParse(attributeValues1.Values[0].ToString(), out result);
          this._memberZak = result;
          this.tbxMemberZak.Text = attributeValues1.Descriptions.Length != 0 ? attributeValues1.Descriptions[0].ToString() : string.Empty;
        }
      }
      finally
      {
        this.tbxMemberZak.TextChanged += new EventHandler(this.tbxTextChanged);
      }
      this.Modified = false;
    }
  }

  /// <summary>Save data</summary>
  private void SaveProcRoute()
  {
    if (this._procRoute == 0L)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this._procRoute, false);
      if (dbObject == null)
        return;
      AttributeValues[] valuesList = new AttributeValues[4]
      {
        new AttributeValues(TechCardConsts.AttributeTypes.NameAttrTypeID, (object) this.tbxName.Text),
        new AttributeValues(TechCardConsts.AttributeTypes.DesignationAttrTypeID, (object) this.tbxDesignation.Text),
        new AttributeValues(TechCardConsts.AttributeTypes.MemberOfSborkaObjectAttrID, (object) this._memberSbr),
        new AttributeValues(TechCardConsts.AttributeTypes.MemberOfZakazObjectAttrID, (object) this._memberZak)
      };
      dbObject.SetAttributesValues(valuesList);
      this.Modified = false;
    }
  }

  /// <summary>Конструктор</summary>
  public ProcRouteView() => this.InitializeComponent();

  /// <summary>Статус изменения</summary>
  public bool Modified
  {
    get => this._modified;
    set
    {
      this._modified = value;
      this.UpdateControls();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public long ArtObjectID => this.GetArtObjectID();

  /// <summary>Маршрут обработки</summary>
  public long ProcRoute
  {
    get => this._procRoute;
    set
    {
      if (this._procRoute == value)
        return;
      this._procRoute = value;
      this.LoadProcRoute();
      this.UpdateControls();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnApply_Click(object sender, EventArgs e) => this.SaveProcRoute();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnCancel_Click(object sender, EventArgs e) => this.LoadProcRoute();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tbxTextChanged(object sender, EventArgs e) => this.Modified = true;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnMemberSbr_Click(object sender, EventArgs e) => this.SelectMemberSbrObj();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnMemberzak_Click(object sender, EventArgs e) => this.SelectMemberZakObj();
}
