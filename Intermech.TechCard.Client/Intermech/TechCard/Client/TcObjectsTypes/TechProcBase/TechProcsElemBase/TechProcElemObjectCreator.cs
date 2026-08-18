// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.TechProcBase.TechProcsElemBase.TechProcElemObjectCreator
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.AutoSelection;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.TechCard;
using Intermech.Interfaces.TechCard.TechRelation;
using Intermech.TechCard.Client.Common;
using Intermech.TechCard.Client.UI.Forms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.TechProcBase.TechProcsElemBase;

/// <summary>
/// Summary description for TechProcElemBaseObjectCreator.
/// </summary>
public class TechProcElemObjectCreator : Form
{
  /// <summary>идентификатор созданного Фрагмента</summary>
  internal long _newObjID;
  /// <summary>тип создаваемого Фрагмента</summary>
  internal int _newObjTypeID = -1;
  /// <summary>прототип создаваемого Фрагмента</summary>
  internal long _templObjID;
  /// <summary>Режим создания версии объекта</summary>
  internal bool _versionMode;
  /// <summary>
  /// идентификаторы объектов с которыми надо связать созданный объект
  /// </summary>
  internal long[] _relObjIDs;
  /// <summary>
  /// типы связей которыми надо связать созданный объект с заданными объектами
  /// </summary>
  internal int[] _relTypeIDs;
  /// <summary>дата с которой начинают действовать созданные связи</summary>
  internal DateTime _startTime = DateTime.Now;
  /// <summary>
  /// 
  /// </summary>
  internal string _desigOrg = string.Empty;
  /// <summary>
  /// 
  /// </summary>
  internal string _desigClassif = string.Empty;
  private Button btnCancel;
  private Button btnOk;
  private GroupBox grbMain;
  private ComboBox cbProd;
  private TextBox tbxDesignation;
  private TextBox tbxName;
  private Label lblProduct;
  private Label lblDesign;
  private Label lblName;
  private ErrorProvider errorProvider;
  private IContainer components;

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (TechProcElemObjectCreator));
    this.btnCancel = new Button();
    this.btnOk = new Button();
    this.grbMain = new GroupBox();
    this.cbProd = new ComboBox();
    this.tbxDesignation = new TextBox();
    this.tbxName = new TextBox();
    this.lblProduct = new Label();
    this.lblDesign = new Label();
    this.lblName = new Label();
    this.errorProvider = new ErrorProvider(this.components);
    this.grbMain.SuspendLayout();
    ((ISupportInitialize) this.errorProvider).BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    componentResourceManager.ApplyResources((object) this.btnOk, "btnOk");
    this.btnOk.DialogResult = DialogResult.OK;
    this.btnOk.Name = "btnOk";
    this.btnOk.Click += new EventHandler(this.btnOk_Click);
    componentResourceManager.ApplyResources((object) this.grbMain, "grbMain");
    this.grbMain.Controls.Add((Control) this.cbProd);
    this.grbMain.Controls.Add((Control) this.tbxDesignation);
    this.grbMain.Controls.Add((Control) this.tbxName);
    this.grbMain.Controls.Add((Control) this.lblProduct);
    this.grbMain.Controls.Add((Control) this.lblDesign);
    this.grbMain.Controls.Add((Control) this.lblName);
    this.grbMain.Name = "grbMain";
    this.grbMain.TabStop = false;
    componentResourceManager.ApplyResources((object) this.cbProd, "cbProd");
    this.cbProd.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
    this.cbProd.AutoCompleteSource = AutoCompleteSource.ListItems;
    this.cbProd.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbProd.Name = "cbProd";
    this.cbProd.Sorted = true;
    this.cbProd.SelectionChangeCommitted += new EventHandler(this.cbProd_SelectionChangeCommitted);
    componentResourceManager.ApplyResources((object) this.tbxDesignation, "tbxDesignation");
    this.tbxDesignation.Name = "tbxDesignation";
    this.tbxDesignation.TextChanged += new EventHandler(this.tbxDesignation_TextChanged);
    componentResourceManager.ApplyResources((object) this.tbxName, "tbxName");
    this.tbxName.Name = "tbxName";
    componentResourceManager.ApplyResources((object) this.lblProduct, "lblProduct");
    this.lblProduct.Name = "lblProduct";
    componentResourceManager.ApplyResources((object) this.lblDesign, "lblDesign");
    this.lblDesign.Name = "lblDesign";
    componentResourceManager.ApplyResources((object) this.lblName, "lblName");
    this.lblName.Name = "lblName";
    this.errorProvider.ContainerControl = (ContainerControl) this;
    componentResourceManager.ApplyResources((object) this.errorProvider, "errorProvider");
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.grbMain);
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.btnOk);
    this.Name = nameof (TechProcElemObjectCreator);
    this.ShowInTaskbar = false;
    this.FormClosed += new FormClosedEventHandler(this.TechProcElemObjectCreator_FormClosed);
    this.grbMain.ResumeLayout(false);
    this.grbMain.PerformLayout();
    ((ISupportInitialize) this.errorProvider).EndInit();
    this.ResumeLayout(false);
  }

  /// <summary>Clean up any resources being used.</summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  private void InitializeData(IUserSession session)
  {
  }

  /// <summary>Заполнение видов производств</summary>
  /// <param name="session"></param>
  /// <param name="productID">Ид. вида пр-ва</param>
  private void FillProductionList(IUserSession session, long productID)
  {
    object obj = (object) null;
    this.cbProd.BeginUpdate();
    try
    {
      this.cbProd.Items.Clear();
      foreach (TechProduction techProduction in TechCardClientConst.GetTechProductions(session, true))
      {
        if (techProduction != null)
        {
          this.cbProd.Items.Add((object) techProduction);
          if (techProduction.ID == productID)
            obj = (object) techProduction;
        }
      }
    }
    finally
    {
      this.cbProd.EndUpdate();
      if (this.cbProd.Items.Count > 0)
      {
        if (obj == null)
          obj = this.cbProd.Items[0];
        this.cbProd.SelectedItem = obj;
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  private void UpdateTpData()
  {
    this.ValidateTpData();
    this.UpdateButtons();
  }

  /// <summary>
  /// 
  /// </summary>
  private void UpdateProdData()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.ClassifyObjName(sessionKeeper.Session);
      this.ValidateProdData();
    }
    this.UpdateButtons();
  }

  /// <summary>
  /// 
  /// </summary>
  private void UpdateButtons() => this.btnOk.Enabled = !this.HasControlErrorMsgs();

  /// <summary>Классификация наименнования / обозначения объекта</summary>
  /// <param name="session"></param>
  private bool ClassifyObjName(IUserSession session) => false;

  /// <summary>
  /// 
  /// </summary>
  private bool ValidateTpData()
  {
    this.errorProvider.SetError((Control) this.tbxDesignation, string.Empty);
    if (!(this.tbxDesignation.Text == string.Empty))
      return true;
    this.errorProvider.SetError((Control) this.tbxDesignation, Intermech.Localization.LocalizationHolder.rm.GetString(sc_19699.ssp_techcard_19700()));
    return false;
  }

  /// <summary>
  /// 
  /// </summary>
  private bool ValidateProdData()
  {
    this.errorProvider.SetError((Control) this.cbProd, string.Empty);
    if (this.GetProductionID() != 0L)
      return true;
    this.errorProvider.SetError((Control) this.cbProd, string.Format(Intermech.Localization.LocalizationHolder.rm.GetString(sc_19699.ssp_techcard_19701()), (object) Intermech.Localization.LocalizationHolder.rm.GetString(sc_19699.ssp_techcard_19702())));
    return false;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  private bool HasControlErrorMsgs()
  {
    return this.errorProvider.GetError((Control) this.cbProd) != string.Empty || this.errorProvider.GetError((Control) this.tbxDesignation) != string.Empty;
  }

  /// <summary>Создание объекта</summary>
  private void CreateObject()
  {
    if (this.HasControlErrorMsgs())
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      List<long> relationIDs1 = new List<long>();
      List<long> projIDs = new List<long>();
      List<int> relTypeIDs = new List<int>();
      List<long> relationIDs2 = new List<long>();
      IDBTransactions customService = sessionKeeper.Session.GetCustomService(typeof (IDBTransactions)) as IDBTransactions;
      try
      {
        customService?.StartTransaction();
        IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(this._newObjTypeID);
        if (objectCollection == null)
        {
          customService?.Rollback();
        }
        else
        {
          IDBObject dbObject1 = this._templObjID == 0L || this._templObjID == -1L ? objectCollection.Create() : (this._versionMode ? objectCollection.CreateVersion(this._templObjID) : objectCollection.Create(this._templObjID));
          if (dbObject1 == null)
          {
            customService?.Rollback();
          }
          else
          {
            long objectId = dbObject1.ObjectID;
            AttributeValues[] valuesList = new AttributeValues[3]
            {
              new AttributeValues(TechCardConsts.AttributeTypes.NameAttrTypeID, (object) this.tbxName.Text),
              new AttributeValues(TechCardConsts.AttributeTypes.DesignationAttrTypeID, (object) this.tbxDesignation.Text),
              new AttributeValues(TechCardConsts.AttributeTypes.ProductionAttrID, (object) ((TechProduction) this.cbProd.SelectedItem).ID)
            };
            dbObject1.SetAttributesValues(valuesList);
            TechcardClientUtils.StartCreateRelations((IEnumerable<long>) this._relObjIDs, sessionKeeper.Session);
            try
            {
              List<IDBRelation> relations = TechcardClientUtils.CreateRelations(sessionKeeper.Session, objectId, this._relTypeIDs, this._relObjIDs, this._startTime, TechCreateRelMode.tcrmEnterIn);
              if (relations != null)
              {
                foreach (IDBRelation dbRelation in relations)
                {
                  relationIDs1.Add(dbRelation.RelationID);
                  projIDs.Add(dbRelation.ProjID);
                  relTypeIDs.Add(dbRelation.RelationType);
                }
              }
              if ((this._templObjID == 0L ? 0 : (this._templObjID != -1L ? 1 : 0)) == 0)
              {
                IAutoSelectionService service = ServiceUtils.GetService<IAutoSelectionService>((object) ApplicationServices.Container, false);
                if (service != null)
                {
                  long relationId = 0;
                  if (relations != null && relations.Count != 0 && relations[0] != null)
                    relationId = relations[0].RelationID;
                  relationIDs2 = service.ExecuteSelection(objectId, relationId, AutoSelectionMode.AutoObject);
                }
              }
            }
            finally
            {
              TechcardClientUtils.StopCreateRelations(sessionKeeper.Session);
            }
            IDBObject dbObject2 = sessionKeeper.Session.GetObject(objectId);
            if (dbObject2 != null && dbObject2.IsCreationMode)
            {
              dbObject2.CommitCreation(true);
              objectId = dbObject2.ObjectID;
            }
            this._newObjID = objectId;
            customService?.Commit();
            INotificationService service1 = ServiceUtils.GetService<INotificationService>((object) ApplicationServices.Container, false);
            if (service1 == null)
              return;
            service1.FireEvent((object) null, (NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", (IList<long>) relationIDs1, (IList<long>) projIDs, (IList<int>) null, (IList<int>) relTypeIDs));
            if (relationIDs2 == null || relationIDs2.Count <= 0)
              return;
            service1.FireEvent((object) null, (NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", (IList<long>) relationIDs2));
          }
        }
      }
      catch (Exception ex)
      {
        customService?.Rollback();
        throw;
      }
    }
  }

  /// <summary>Загрузка настроек</summary>
  /// <param name="productID"></param>
  private void LoadSettings(ref long productID)
  {
    HybridDictionary config = new HybridDictionary(2);
    TechCardFormUtils.LoadSettings((Control) this, TechCardFormUtils.Mode.LocationOnly, (IDictionary) config);
    if (productID != 0L || !config.Contains((object) "prodID"))
      return;
    productID = (long) config[(object) "prodID"];
  }

  /// <summary>Сохранение настроек</summary>
  private void SaveSettings()
  {
    TechCardFormUtils.SaveSettings((Control) this, TechCardFormUtils.Mode.LocationOnly, (IDictionary) new HybridDictionary(1)
    {
      {
        (object) "prodID",
        (object) ((TechProduction) this.cbProd.SelectedItem).ID
      }
    });
  }

  /// <summary>Конструктор</summary>
  /// <param name="TpTypeID"></param>
  /// <param name="TemplateID"></param>
  /// <param name="RelationTypeIDs"></param>
  /// <param name="RelatedObjectIDs"></param>
  /// <param name="StartDate"></param>
  /// <param name="versionMode"></param>
  public TechProcElemObjectCreator(
    int TpTypeID,
    long TemplateID,
    int[] RelationTypeIDs,
    long[] RelatedObjectIDs,
    DateTime StartDate,
    bool versionMode)
  {
    this.InitializeComponent();
    this._newObjTypeID = TpTypeID;
    this._templObjID = TemplateID;
    this._relObjIDs = RelatedObjectIDs;
    this._relTypeIDs = RelationTypeIDs;
    this._startTime = StartDate;
    this._versionMode = versionMode;
    if (this.DesignMode)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.InitializeData(sessionKeeper.Session);
      long productID = 0;
      this.LoadSettings(ref productID);
      this.FillProductionList(sessionKeeper.Session, productID);
    }
    this.ValidateTpData();
    this.ValidateProdData();
    this.UpdateButtons();
  }

  /// <summary>Ид. вида производства</summary>
  internal long GetProductionID()
  {
    return this.cbProd.SelectedItem == null ? 0L : ((TechProduction) this.cbProd.SelectedItem).ID;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnOk_Click(object sender, EventArgs e) => this.CreateObject();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void cbProd_SelectionChangeCommitted(object sender, EventArgs e) => this.UpdateProdData();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tbxDesignation_TextChanged(object sender, EventArgs e) => this.UpdateTpData();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void TechProcElemObjectCreator_FormClosed(object sender, FormClosedEventArgs e)
  {
    this.SaveSettings();
  }
}
