// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Dialogs.ArtsCompositionReportForm
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using ImSSP;
using Infralution.Controls;
using Infralution.Controls.VirtualTree;
using Intermech.Bars;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Configuration;
using Intermech.Interfaces.Pdm;
using Intermech.Interfaces.TechCard;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.TechCard.Client.Classes.Tasks;
using Intermech.TechCard.Client.Navigator.Descriptors;
using Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Params;
using Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Reports;
using Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Tasks;
using Intermech.TechCard.Client.Tools.Controls.Navigator;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Dialogs;

/// <summary>Форма для создания контекстной сборочной единицы</summary>
/// <summary>Форма для создания контекстной сборочной единицы</summary>
public class ArtsCompositionReportForm : ArtsCompositionBaseForm
{
  /// <summary>Наименование TreeView</summary>
  private readonly string _treeViewName = nameof (_techNavTreeView);
  /// <summary>Требуется ли загрузка данных</summary>
  private bool _needLoadData;
  /// <summary>TechCard treeView</summary>
  private TechCardNavTreeViewControl _techNavTreeView;
  /// <summary>
  ///  Коллекция пар значений [Версия объекта, Ид. базовой величины] = [Количества в КСЕ и ТП]
  /// </summary>
  private readonly IDictionary<Tuple<long, long>, ElementQuantity> _elemQtyList = (IDictionary<Tuple<long, long>, ElementQuantity>) new ConcurrentDictionary<Tuple<long, long>, ElementQuantity>();
  /// <summary>Информация по составу изделий</summary>
  /// <remarks>Конструкторский состав</remarks>
  private readonly List<ArtsCompositionReportObjectItem> _designCompItems = new List<ArtsCompositionReportObjectItem>();
  /// <summary>Информация по единицам состава с входимостью</summary>
  /// <remarks>Технологический состав</remarks>
  private readonly List<ArtsCompositionReportObjectItem> _techCompItems = new List<ArtsCompositionReportObjectItem>();
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ImageList imagesToolbars;
  private Panel panelPage1;
  private Panel pnlTop;
  private ToolStrip tsMain;
  private ToolStripLabel tslblContext;
  private ToolStripComboBox tscbContext;
  private ToolStripSeparator toolStripSeparator1;
  private Panel pnlTopHeader;
  private Label lblCaption;
  private PictureBox pictCaption;
  private Panel pnlButtons;
  private Button btnOK;
  private Panel pnlClient;
  private ContextMenuBarItem contextMenuComposition;
  private MenuButtonItem mnpAddToCC;
  private MenuButtonItem mnpRefresh;
  private MenuButtonItem menuButtonItem1;
  private Panel pnlProgress;
  private Label lblProgressInfo;
  private ProgressBar prgbarProgress;
  private Panel pnlBottom;
  private GroupBox grbMain;
  private CheckBox chbApplicabilityDesign;
  private RadioButton rbtnReportModeNotUsedInTP;
  private RadioButton rbtnReportModeUsedInTp;
  private CheckBox chbApplicabilityTechProc;
  private RadioButton rbtnReportModeUsedOnlyInTP;

  /// <summary>Инициализация кастом контролов</summary>
  private void InitializeCustomControls()
  {
    if (this.DesignMode)
      return;
    this.SuspendLayout();
    try
    {
      this._techNavTreeView = new TechCardNavTreeViewControl();
      this._techNavTreeView.BeginInit();
      try
      {
        this._techNavTreeView.AllowDrop = true;
        this._techNavTreeView.AllowMultiSelect = false;
        this._techNavTreeView.AllowUserPinnedColumns = false;
        this._techNavTreeView.BackgroundImageMode = ImageDrawMode.Tile;
        this._techNavTreeView.BorderStyle = BorderStyle.Fixed3D;
        this._techNavTreeView.CheckBoxStyle = NavigatorTreeViewCheckBoxStyle.None;
        this._techNavTreeView.CheckoutMode = TechCheckoutMode.Manual;
        this._techNavTreeView.CheckRootNode = false;
        this._techNavTreeView.DisableIMContextMenu = true;
        this._techNavTreeView.DisableKeyDownEvents = true;
        this._techNavTreeView.DisableKeyUpEvents = true;
        this._techNavTreeView.DisablePacketsReading = false;
        this._techNavTreeView.Dock = DockStyle.Fill;
        this._techNavTreeView.HeaderStyle.HorzAlignment = StringAlignment.Near;
        this._techNavTreeView.LineStyle = LineStyle.Dot;
        this._techNavTreeView.Location = new Point(0, 0);
        this._techNavTreeView.Name = this._treeViewName;
        this._techNavTreeView.RowEvenStyle.WordWrap = false;
        this._techNavTreeView.RowOddStyle.WordWrap = false;
        this._techNavTreeView.RowSelectedStyle.WordWrap = false;
        this._techNavTreeView.RowStyle.BorderColor = SystemColors.Control;
        this._techNavTreeView.RowStyle.BorderStyle = Border3DStyle.Adjust;
        this._techNavTreeView.RowStyle.BorderWidth = 1;
        this._techNavTreeView.RowStyle.WordWrap = false;
        this._techNavTreeView.SelectBeforeEdit = true;
        this._techNavTreeView.SelectionMode = Infralution.Controls.VirtualTree.SelectionMode.FullRow;
        this._techNavTreeView.ShowRootRow = false;
        this._techNavTreeView.Size = new Size(292, 266);
        this._techNavTreeView.SuppressErrorMessages = true;
        this._techNavTreeView.TabIndex = 1;
        this._techNavTreeView.Tag = (object) " ";
        this.AutoScaleDimensions = new SizeF(6f, 13f);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.ClientSize = new Size(292, 266);
        this.pnlClient.Controls.Add((Control) this._techNavTreeView);
        this._techNavTreeView.Dock = DockStyle.Fill;
        this._techNavTreeView.BringToFront();
        this.Name = "TechCompositionDescrForm";
      }
      finally
      {
        this._techNavTreeView.EndInit();
      }
    }
    finally
    {
      this.ResumeLayout(false);
    }
    BarManager service = ServiceUtils.GetService<BarManager>((object) ApplicationServices.Container, false);
    if (service != null)
    {
      service.RendererChanged += new EventHandler(this.ToolbarRendererChanged);
      this.ToolbarRendererChanged((object) service, EventArgs.Empty);
    }
    this.pnlProgress.Visible = false;
  }

  /// <summary>Загрузить данные в форму</summary>
  /// <returns>true, если загрузка прошла успешно</returns>
  protected override bool LoadControlData()
  {
    base.LoadControlData();
    this.LoadContextsList(this.tscbContext.ComboBox);
    this.UpdateControls();
    this._dataProvider.LoadedDesignData = false;
    this._dataProvider.LoadedTechData = false;
    this._needLoadData = true;
    return true;
  }

  /// <summary>Обновить дерево КСЕ</summary>
  /// <param name="sender"></param>
  /// <param name="e">Параметры</param>
  private void DoRefreshComposition(object sender, EventArgs e)
  {
    this.StartLoadData();
    this._techNavTreeView.ClearTreeCore(false);
    ArtsCompositionDataProvider.PluginData.CurrentSet = 0;
    IDescriptor currentDescriptor = this.GetCurrentDescriptor();
    ArtsCompositionReportMode repMode;
    ArtsCompositionReportApplicabilityMode applicabilityMode;
    this.GetCurrentReportMode(out repMode, out applicabilityMode);
    this.SaveSettings(false);
    this._techNavTreeView.OnGetSupportedColumnsEventHandler -= new GetSupportedColumnsEventHandler(this.techNavTreeViewOnOnGetSupportedColumnsEventHandler);
    this._techNavTreeView.SetColumns(this.GetReportModeSupportedColumns(sender, true));
    this._techNavTreeView.OnGetSupportedColumnsEventHandler += new GetSupportedColumnsEventHandler(this.techNavTreeViewOnOnGetSupportedColumnsEventHandler);
    this._techNavTreeView.Name = $"{this._treeViewName}_{(object) (int) repMode}_{(object) (int) applicabilityMode}";
    this.LoadSettings(false);
    this._techNavTreeView.Build(currentDescriptor);
    this.UpdateControls();
  }

  /// <summary>Загрузка расположения и размеров формы</summary>
  protected override void LoadSettings(bool loadFormPosition)
  {
    base.LoadSettings(loadFormPosition);
    object formSetting1 = this._formSettings[(object) "chbApplicabilityDesign.Checked"];
    if (formSetting1 != null)
    {
      this.chbApplicabilityDesign.CheckedChanged -= new EventHandler(this.chbApplicability_CheckedChanged);
      this.chbApplicabilityDesign.Checked = (int) formSetting1 > 0;
      this.chbApplicabilityDesign.CheckedChanged += new EventHandler(this.chbApplicability_CheckedChanged);
    }
    object formSetting2 = this._formSettings[(object) "chbApplicabilityTechProc.Checked"];
    if (formSetting2 != null)
    {
      this.chbApplicabilityTechProc.CheckedChanged -= new EventHandler(this.chbApplicability_CheckedChanged);
      this.chbApplicabilityTechProc.Checked = (int) formSetting2 > 0;
      this.chbApplicabilityTechProc.CheckedChanged += new EventHandler(this.chbApplicability_CheckedChanged);
    }
    IConfigurationManager service = ServiceUtils.GetService<IConfigurationManager>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    string name = this.GetType().ToString();
    IConfiguration config = service.Open(name);
    if (this._techNavTreeView == null)
      return;
    TechCardNavTreeViewUtils.LoadSettings(config, (NavigatorTreeView) this._techNavTreeView);
  }

  /// <summary>Сохранение расположения и размеров формы</summary>
  protected override void SaveSettings(bool saveFormPosition)
  {
    this._formSettings[(object) "chbApplicabilityDesign.Checked"] = (object) (this.chbApplicabilityDesign.Checked ? 1 : 0);
    this._formSettings[(object) "chbApplicabilityTechProc.Checked"] = (object) (this.chbApplicabilityTechProc.Checked ? 1 : 0);
    base.SaveSettings(saveFormPosition);
    IConfigurationManager service = ServiceUtils.GetService<IConfigurationManager>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    string name = this.GetType().ToString();
    IConfiguration config = service.Open(name) ?? service.Create(name);
    if (config == null)
      return;
    TechCardNavTreeViewUtils.SaveSettings(config, (NavigatorTreeView) this._techNavTreeView);
  }

  /// <summary>Добавление поля для кол-ва, которое осталось</summary>
  /// <param name="nodeColl"></param>
  /// <param name="setDefaultWidth"></param>
  private static void AddColumnsRemainCount(NodeColumnCollection nodeColl, bool setDefaultWidth = false)
  {
    if (nodeColl == null)
      return;
    IColumnSchemes service = ServiceUtils.GetService<IColumnSchemes>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    NodeColumn column = service.CreateColumn(Intermech.Navigator.Consts.CurrentObjectColumnSchemeGuid, (object) TechCardConsts.AttributeTypes.CountRemainAttrID);
    if (setDefaultWidth)
      nodeColl.Add(column, 100);
    else
      nodeColl.Add(column);
  }

  /// <summary>Добавление поля для кол-ва по ТП</summary>
  /// <param name="nodeColl"></param>
  /// <param name="setDefaultWidth"></param>
  private static void AddColumnsTechProcCount(NodeColumnCollection nodeColl, bool setDefaultWidth = false)
  {
    if (nodeColl == null)
      return;
    IColumnSchemes service = ServiceUtils.GetService<IColumnSchemes>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    NodeColumn column1 = service.CreateColumn(Intermech.Navigator.Consts.CurrentObjectColumnSchemeGuid, (object) TechCardConsts.AttributeTypes.Count4TechProcAttrID);
    if (setDefaultWidth)
      nodeColl.Add(column1, 100);
    else
      nodeColl.Add(column1);
    NodeColumn column2 = service.CreateColumn(Intermech.Navigator.Consts.CurrentObjectColumnSchemeGuid, (object) TechCardConsts.AttributeTypes.Count4ArticleAttrID);
    if (setDefaultWidth)
      nodeColl.Add(column2, 100);
    else
      nodeColl.Add(column2);
  }

  /// <summary>Добавление полей для конструкторской входимости</summary>
  /// <param name="nodeColl"></param>
  /// <param name="setDefaultWidth"></param>
  private static void AddColumnsApplicabilityDesign(
    NodeColumnCollection nodeColl,
    bool setDefaultWidth = false)
  {
    if (nodeColl == null)
      return;
    IColumnSchemes service = ServiceUtils.GetService<IColumnSchemes>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    NodeColumn column1 = service.CreateColumn(Intermech.Navigator.Consts.CurrentObjectColumnSchemeGuid, (object) TechCardConsts.AttributeTypes.Count4CompositionAttrID);
    if (setDefaultWidth)
      nodeColl.Add(column1, 100);
    else
      nodeColl.Add(column1);
    NodeColumn column2 = service.CreateColumn(Intermech.Navigator.Consts.CurrentObjectColumnSchemeGuid, (object) MetaDataHelper.GetAttributeTypeID("cad00270-306c-11d8-b4e9-00304f19f545"));
    if (setDefaultWidth)
      nodeColl.Add(column2, 100);
    else
      nodeColl.Add(column2);
    NodeColumn column3 = service.CreateColumn(Intermech.Navigator.Consts.CurrentObjectColumnSchemeGuid, (object) TechCardConsts.AttributeTypes.MemberOfSborkaObjectAttrID);
    if (setDefaultWidth)
      nodeColl.Add(column3, 250);
    else
      nodeColl.Add(column3);
  }

  /// <summary>Добавление полей для конструкторской входимости</summary>
  /// <param name="nodeColl"></param>
  /// <param name="setDefaultWidth"></param>
  private static void AddColumnsApplicabilityTechProc(
    NodeColumnCollection nodeColl,
    bool setDefaultWidth = false)
  {
    if (nodeColl == null)
      return;
    IColumnSchemes service = ServiceUtils.GetService<IColumnSchemes>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    NodeColumn column = service.CreateColumn(Intermech.Navigator.Consts.CurrentObjectColumnSchemeGuid, (object) TechCardConsts.AttributeTypes.MemberOfTechProcObjAttrId);
    if (setDefaultWidth)
      nodeColl.Add(column, 250);
    else
      nodeColl.Add(column);
  }

  /// <summary>Получение списка полей для текущего режима</summary>
  /// <param name="sender"></param>
  /// <param name="defaultMode"></param>
  /// <returns></returns>
  private NodeColumnCollection GetReportModeSupportedColumns(object sender, bool defaultMode)
  {
    ArtsCompositionReportMode repMode;
    ArtsCompositionReportApplicabilityMode applicabilityMode;
    this.GetCurrentReportMode(out repMode, out applicabilityMode);
    NodeColumnCollection nodeColl = defaultMode ? Intermech.Navigator.Utils.CaptionColumnOnly(NodeColumnSortOrder.Ascending) : TechCardNavTreeViewUtils.GetObjectColumnsOnly(sender);
    switch (repMode)
    {
      case ArtsCompositionReportMode.UsedInTp:
        ArtsCompositionReportForm.AddColumnsRemainCount(nodeColl, defaultMode);
        ArtsCompositionReportForm.AddColumnsTechProcCount(nodeColl, defaultMode);
        break;
      case ArtsCompositionReportMode.NotUsedInTp:
        ArtsCompositionReportForm.AddColumnsRemainCount(nodeColl, defaultMode);
        break;
      case ArtsCompositionReportMode.UsedInTpOnly:
        ArtsCompositionReportForm.AddColumnsTechProcCount(nodeColl, defaultMode);
        break;
    }
    if (applicabilityMode.HasFlag((Enum) ArtsCompositionReportApplicabilityMode.Design))
      ArtsCompositionReportForm.AddColumnsApplicabilityDesign(nodeColl, defaultMode);
    if (applicabilityMode.HasFlag((Enum) ArtsCompositionReportApplicabilityMode.TechProc))
      ArtsCompositionReportForm.AddColumnsApplicabilityTechProc(nodeColl, defaultMode);
    return nodeColl;
  }

  /// <summary>Подготовить панель к показу</summary>
  /// <param name="panel">Панель</param>
  private void ShowPanel(Panel panel)
  {
    panel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    panel.Left = 0;
    panel.Top = 0;
    panel.Width = panel.Parent.ClientSize.Width;
    panel.Height = panel.Parent.ClientSize.Height;
  }

  /// <summary>Обновить контролы</summary>
  protected override void UpdateControls()
  {
    this.btnOK.Enabled = true;
    this.ShowPanel(this.panelPage1);
    this.chbApplicabilityDesign.Enabled = !this.rbtnReportModeUsedOnlyInTP.Checked;
    this.chbApplicabilityTechProc.Enabled = !this.rbtnReportModeNotUsedInTP.Checked;
  }

  /// <summary>
  /// 
  /// </summary>
  protected override void DoBeforeLoadData(object sender, EventArgs args)
  {
    this.pnlProgress.Height = 43;
    this.pnlProgress.Visible = true;
    this.prgbarProgress.Value = 0;
    this.prgbarProgress.Minimum = 0;
    this.prgbarProgress.Maximum = 100;
    this.btnOK.Enabled = false;
    if (!this._dataProvider.LoadedDesignData || !this._dataProvider.LoadedTechData)
      this.lblProgressInfo.Text = LocalizationHolder.rm.GetString("TechCard.Client_390");
    Application.DoEvents();
  }

  /// <summary>
  /// 
  /// </summary>
  protected override void DoAfterLoadData(object sender, EventArgs args)
  {
    Application.DoEvents();
    if (this._dataProvider.LoadedDesignData && this._dataProvider.LoadedTechData)
    {
      this.FillElemQtyList();
      this.FillCompositionTechItemsInfo();
      this.FillCompositionCaptions();
    }
    this.pnlProgress.Visible = false;
    this.btnOK.Enabled = true;
    this.UpdateControls();
  }

  /// <summary>
  /// 
  /// </summary>
  protected override void DoProcessChanged(object sender, ProgressChangedEventArgs args)
  {
    if (!this.pnlProgress.Visible)
      return;
    this.prgbarProgress.Value = args.ProgressPercentage;
  }

  /// <summary>
  /// Проанализировать исходные данные и заполнить список сравнения состава
  /// </summary>
  private void FillElemQtyList()
  {
    this._elemQtyList.Clear();
    this._designCompItems.Clear();
    this._techCompItems.Clear();
    List<ElementQuantity> elemQtyList;
    if (this._dataProvider.TableDesign != null && this.GetElemQtyInfoList(this._dataProvider.TableDesign, (IList<ArtsCompositionReportObjectItem>) this._designCompItems, out elemQtyList, 2, this._dataProvider.TableDesign.Columns.IndexOf("F_PROJ_ID")))
    {
      foreach (ElementQuantity elementQuantity in elemQtyList)
      {
        MeasuredValue designQuantity = elementQuantity.DesignQuantity;
        long num = designQuantity != null ? designQuantity.MeasureID : -1L;
        this._elemQtyList[new Tuple<long, long>(elementQuantity.TypedInfoItem.ItemID, num)] = elementQuantity;
      }
    }
    if (this._dataProvider.TableTech == null)
      return;
    DataTable tableTech = this._dataProvider.TableTech;
    List<ArtsCompositionReportObjectItem> techCompItems = this._techCompItems;
    ref List<ElementQuantity> local = ref elemQtyList;
    DataColumnCollection columns1 = this._dataProvider.TableTech.Columns;
    Guid guid = TechCardConsts.AttributeTypes.ObjectRefAttrGuid;
    string columnName1 = guid.ToString();
    int idxColumnArticleId = columns1.IndexOf(columnName1);
    DataColumnCollection columns2 = this._dataProvider.TableTech.Columns;
    guid = TechCardConsts.AttributeTypes.MemberOfSborkaObjectAttrGUID;
    string columnName2 = guid.ToString();
    int idxColumnArtProjId = columns2.IndexOf(columnName2);
    int idxColumnObjProjId = this._dataProvider.TableTech.Columns.IndexOf("F_PROJ_ID");
    if (!this.GetElemQtyInfoList(tableTech, (IList<ArtsCompositionReportObjectItem>) techCompItems, out local, idxColumnArticleId, idxColumnArtProjId, 2, idxColumnObjProjId))
      return;
    foreach (ElementQuantity elementQuantity1 in elemQtyList)
    {
      elementQuantity1.TechQuantity = elementQuantity1.DesignQuantity;
      elementQuantity1.DesignQuantity = (MeasuredValue) null;
      MeasuredValue techQuantity = elementQuantity1.TechQuantity;
      long num = techQuantity != null ? techQuantity.MeasureID : -1L;
      Tuple<long, long> key = new Tuple<long, long>(elementQuantity1.TypedInfoItem.ItemID, num);
      ElementQuantity elementQuantity2;
      if (this._elemQtyList.TryGetValue(key, out elementQuantity2))
        elementQuantity2.TechQuantity = elementQuantity1.TechQuantity;
      else
        this._elemQtyList[key] = elementQuantity1;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  private void FillCompositionTechItemsInfo()
  {
    List<ITypedInfoItem> list1 = this._techCompItems.Where<ArtsCompositionReportObjectItem>((System.Func<ArtsCompositionReportObjectItem, bool>) (item => item.ObjectInfo.ItemTypeID == -1)).Select<ArtsCompositionReportObjectItem, ITypedInfoItem>((System.Func<ArtsCompositionReportObjectItem, ITypedInfoItem>) (item => item.ObjectInfo)).ToList<ITypedInfoItem>();
    list1.AddRange(this._techCompItems.Where<ArtsCompositionReportObjectItem>((System.Func<ArtsCompositionReportObjectItem, bool>) (item => item.ProjObjectInfo.ItemTypeID == -1)).Select<ArtsCompositionReportObjectItem, ITypedInfoItem>((System.Func<ArtsCompositionReportObjectItem, ITypedInfoItem>) (item => item.ProjObjectInfo)));
    Dictionary<long, int> objectId2TypeCache = this._dataProvider.TableTech.AsEnumerable().ToDictionary<DataRow, long, int>((System.Func<DataRow, long>) (row => DataSetProcessor.GetInt64Value(row, 2, 0L)), (System.Func<DataRow, int>) (row => DataSetProcessor.GetInt32Value(row, 0, -1)));
    this._techCompItems.ForEach((Action<ArtsCompositionReportObjectItem>) (item => item.ObjectInfo.ItemTypeID = objectId2TypeCache[item.ObjectInfo.ItemID]));
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      List<ObjInfoItem> list2 = list1.Where<ITypedInfoItem>((System.Func<ITypedInfoItem, bool>) (item => item.ItemTypeID == -1)).Select<ITypedInfoItem, ObjInfoItem>((System.Func<ITypedInfoItem, ObjInfoItem>) (item => item as ObjInfoItem)).ToList<ObjInfoItem>();
      List<ObjInfoItem> objInfoItemList = ServiceUtils.GetService<ITypedInfoService>((object) sessionKeeper.Session, false)?.UpdateUnknownTypes((IEnumerable<ObjInfoItem>) list2, (object) sessionKeeper.Session.SessionGUID);
      foreach (ObjInfoItem objInfoItem in list2)
      {
        // ISSUE: explicit non-virtual call
        int index = objInfoItemList != null ? __nonvirtual (objInfoItemList.IndexOf(objInfoItem)) : -1;
        if (index != -1)
          objInfoItem.ObjTypeID = objInfoItemList[index].ObjTypeID;
      }
    }
    Dictionary<long, ArtsCompositionReportObjectItem> id2CompositionItemCache = this._techCompItems.ToDictionary<ArtsCompositionReportObjectItem, long, ArtsCompositionReportObjectItem>((System.Func<ArtsCompositionReportObjectItem, long>) (x => x.ObjectInfo.ItemID), (System.Func<ArtsCompositionReportObjectItem, ArtsCompositionReportObjectItem>) (x => x));
    System.Func<long, ITypedInfoItem> getTpObject = (System.Func<long, ITypedInfoItem>) null;
    getTpObject = (System.Func<long, ITypedInfoItem>) (objectId =>
    {
      ArtsCompositionReportObjectItem reportObjectItem;
      if (!id2CompositionItemCache.TryGetValue(objectId, out reportObjectItem) || reportObjectItem?.ProjObjectInfo == null)
        return (ITypedInfoItem) null;
      return !MetaDataHelper.IsObjectTypeChildOf(reportObjectItem.ProjObjectInfo.ItemTypeID, TechCardConsts.ObjectTypes.EdinicaSostavaID) ? reportObjectItem.ProjObjectInfo : getTpObject(reportObjectItem.ProjObjectInfo.ItemID);
    });
    this._techCompItems.ForEach((Action<ArtsCompositionReportObjectItem>) (item => item.ExtraFields[TechCardConsts.AttributeTypes.MemberOfTechProcObjAttrId] = (object) getTpObject(item.ObjectInfo.ItemID)));
    foreach (ArtsCompositionReportObjectItem techCompItem in this._techCompItems)
    {
      ArtsCompositionReportObjectItem techCompositionItem = techCompItem;
      ArtsCompositionReportObjectItem reportObjectItem = this._designCompItems.FirstOrDefault<ArtsCompositionReportObjectItem>((System.Func<ArtsCompositionReportObjectItem, bool>) (item => item.Equals(techCompositionItem)));
      if (reportObjectItem != null)
      {
        foreach (KeyValuePair<int, object> extraField in (IEnumerable<KeyValuePair<int, object>>) reportObjectItem.ExtraFields)
        {
          if (!techCompositionItem.ExtraFields.ContainsKey(extraField.Key))
            techCompositionItem.ExtraFields[extraField.Key] = extraField.Value;
        }
      }
    }
  }

  /// <summary>Заполнение отсутствующих заголовков родителей</summary>
  private void FillCompositionCaptions()
  {
    List<IObjInfoCaption> list = this._designCompItems.Where<ArtsCompositionReportObjectItem>((System.Func<ArtsCompositionReportObjectItem, bool>) (item => string.IsNullOrEmpty(item.ArtProjObjectInfo is IObjInfoCaption artProjObjectInfo1 ? artProjObjectInfo1.Caption : (string) null))).Select<ArtsCompositionReportObjectItem, IObjInfoCaption>((System.Func<ArtsCompositionReportObjectItem, IObjInfoCaption>) (item => item.ArtProjObjectInfo as IObjInfoCaption)).ToList<IObjInfoCaption>();
    list.AddRange((IEnumerable<IObjInfoCaption>) this._designCompItems.Where<ArtsCompositionReportObjectItem>((System.Func<ArtsCompositionReportObjectItem, bool>) (item => string.IsNullOrEmpty(item.ArtObjectInfo is IObjInfoCaption artObjectInfo1 ? artObjectInfo1.Caption : (string) null))).Select<ArtsCompositionReportObjectItem, IObjInfoCaption>((System.Func<ArtsCompositionReportObjectItem, IObjInfoCaption>) (item => item.ArtObjectInfo as IObjInfoCaption)).ToList<IObjInfoCaption>());
    list.AddRange(this._techCompItems.Where<ArtsCompositionReportObjectItem>((System.Func<ArtsCompositionReportObjectItem, bool>) (item => string.IsNullOrEmpty(item.ArtProjObjectInfo is IObjInfoCaption artProjObjectInfo2 ? artProjObjectInfo2.Caption : (string) null))).Select<ArtsCompositionReportObjectItem, IObjInfoCaption>((System.Func<ArtsCompositionReportObjectItem, IObjInfoCaption>) (item => item.ArtProjObjectInfo as IObjInfoCaption)));
    list.AddRange(this._techCompItems.Where<ArtsCompositionReportObjectItem>((System.Func<ArtsCompositionReportObjectItem, bool>) (item => string.IsNullOrEmpty(item.ProjObjectInfo is IObjInfoCaption projObjectInfo ? projObjectInfo.Caption : (string) null))).Select<ArtsCompositionReportObjectItem, IObjInfoCaption>((System.Func<ArtsCompositionReportObjectItem, IObjInfoCaption>) (item => item.ProjObjectInfo as IObjInfoCaption)));
    list.AddRange(this._techCompItems.Where<ArtsCompositionReportObjectItem>((System.Func<ArtsCompositionReportObjectItem, bool>) (item => string.IsNullOrEmpty(item.ArtObjectInfo is IObjInfoCaption artObjectInfo2 ? artObjectInfo2.Caption : (string) null))).Select<ArtsCompositionReportObjectItem, IObjInfoCaption>((System.Func<ArtsCompositionReportObjectItem, IObjInfoCaption>) (item => item.ArtObjectInfo as IObjInfoCaption)));
    list.AddRange(this._techCompItems.Where<ArtsCompositionReportObjectItem>((System.Func<ArtsCompositionReportObjectItem, bool>) (item => string.IsNullOrEmpty(item.ObjectInfo is IObjInfoCaption objectInfo ? objectInfo.Caption : (string) null))).Select<ArtsCompositionReportObjectItem, IObjInfoCaption>((System.Func<ArtsCompositionReportObjectItem, IObjInfoCaption>) (item => item.ObjectInfo as IObjInfoCaption)));
    Dictionary<long, string> objCaptions = new Dictionary<long, string>();
    if (this._dataProvider.TableDesign != null)
    {
      foreach (DataRow row in (InternalDataCollectionBase) this._dataProvider.TableDesign.Rows)
        objCaptions[DataSetProcessor.GetInt64Value(row, 2, 0L)] = Convert.ToString(row[4]);
    }
    if (this._dataProvider.TableTech != null)
    {
      foreach (DataRow row in (InternalDataCollectionBase) this._dataProvider.TableTech.Rows)
        objCaptions[DataSetProcessor.GetInt64Value(row, 2, 0L)] = Convert.ToString(row[4]);
    }
    foreach (IObjInfoCaption objInfoCaption in list)
    {
      string str;
      if (objCaptions.TryGetValue(objInfoCaption.ItemID, out str))
        objInfoCaption.Caption = str;
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!TechCardConsts.Utils.GetObjectString(list.Where<IObjInfoCaption>((System.Func<IObjInfoCaption, bool>) (item => string.IsNullOrEmpty(item.Caption))).Select<IObjInfoCaption, long>((System.Func<IObjInfoCaption, long>) (item => item.ItemID)).ToList<long>(), -1, sessionKeeper.Session, out objCaptions))
        return;
    }
    foreach (IObjInfoCaption objInfoCaption in list)
    {
      string str;
      if (objCaptions.TryGetValue(objInfoCaption.ItemID, out str))
        objInfoCaption.Caption = str;
    }
  }

  /// <summary>
  /// Получение количества (в конструкторском поле) для таблицы
  /// </summary>
  /// <param name="dataTable"></param>
  /// <param name="compositionItemList"></param>
  /// <param name="elemQtyList"></param>
  /// <param name="idxColumnArticleId"></param>
  /// <param name="idxColumnArtProjId"></param>
  /// <param name="idxColumnObjectId"></param>
  /// <param name="idxColumnObjProjId"></param>
  /// <returns></returns>
  private bool GetElemQtyInfoList(
    DataTable dataTable,
    IList<ArtsCompositionReportObjectItem> compositionItemList,
    out List<ElementQuantity> elemQtyList,
    int idxColumnArticleId,
    int idxColumnArtProjId,
    int idxColumnObjectId = -1,
    int idxColumnObjProjId = -1)
  {
    if (dataTable == null)
    {
      elemQtyList = (List<ElementQuantity>) null;
      return false;
    }
    int count = dataTable.Rows.Count;
    Dictionary<Tuple<long, long>, ElementQuantity> dictionary = new Dictionary<Tuple<long, long>, ElementQuantity>();
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      ObjInfoItem artObjectInfo = (ObjInfoItem) new ObjInfoCaptionItem(Math.Abs(DataSetProcessor.GetInt64Value(row, idxColumnArticleId, 0L)));
      ObjInfoItem artProjObjectInfo = (ObjInfoItem) new ObjInfoCaptionItem(idxColumnArtProjId != -1 ? DataSetProcessor.GetInt64Value(row, idxColumnArtProjId, 0L) : 0L);
      ObjInfoItem objInfoItem1 = idxColumnObjectId != -1 ? (ObjInfoItem) new ObjInfoCaptionItem(DataSetProcessor.GetInt64Value(row, idxColumnObjectId, 0L)) : (ObjInfoItem) null;
      ObjInfoItem objInfoItem2 = idxColumnObjProjId != -1 ? (ObjInfoItem) new ObjInfoCaptionItem(DataSetProcessor.GetInt64Value(row, idxColumnObjProjId, 0L)) : (ObjInfoItem) null;
      string stringValue = DataSetProcessor.GetStringValue(row, 3, string.Empty);
      long num = -1;
      MeasuredValue mValue = (MeasuredValue) null;
      try
      {
        if (!string.IsNullOrEmpty(stringValue))
        {
          mValue = MeasureHelper.ConvertToMeasuredValue(stringValue);
          num = MeasureHelper.ConvertToBaseMeasure(mValue).MeasureID;
        }
      }
      catch (KernelException ex)
      {
      }
      ArtsCompositionReportObjectItem reportObjectItem = new ArtsCompositionReportObjectItem(mValue, (ITypedInfoItem) artObjectInfo, (ITypedInfoItem) artProjObjectInfo)
      {
        ObjectInfo = (ITypedInfoItem) objInfoItem1,
        ProjObjectInfo = (ITypedInfoItem) objInfoItem2
      };
      compositionItemList.Add(reportObjectItem);
      for (int index = 0; index < dataTable.Columns.Count; ++index)
      {
        string columnName = dataTable.Columns[index].ColumnName;
        if (GuidHelper.IsGuid(columnName))
          reportObjectItem.ExtraFields[MetaDataHelper.GetAttributeTypeID(columnName)] = (object) Convert.ToString(row[index]);
      }
      Tuple<long, long> key = new Tuple<long, long>(artObjectInfo.ObjectID, num);
      ElementQuantity elementQuantity;
      if (!dictionary.TryGetValue(key, out elementQuantity))
      {
        elementQuantity = new ElementQuantity((ITypedInfoItem) artObjectInfo, stringValue, string.Empty);
        dictionary[key] = elementQuantity;
      }
      else if (mValue != null)
      {
        MeasuredValue baseMeasure = MeasureHelper.ConvertToBaseMeasure(mValue);
        try
        {
          elementQuantity.DesignQuantity = elementQuantity.DesignQuantity == null ? mValue : MeasureHelper.Add(elementQuantity.DesignQuantity, baseMeasure, false);
        }
        catch (KernelExceptionID ex)
        {
        }
      }
    }
    elemQtyList = dictionary.Values.ToList<ElementQuantity>();
    return count > 0;
  }

  /// <summary>Текущий режим отображения состава</summary>
  /// <returns></returns>
  private void GetCurrentReportMode(
    out ArtsCompositionReportMode repMode,
    out ArtsCompositionReportApplicabilityMode applicabilityMode)
  {
    repMode = ArtsCompositionReportMode.Unknown;
    if (this.rbtnReportModeUsedInTp.Checked)
      repMode = ArtsCompositionReportMode.UsedInTp;
    else if (this.rbtnReportModeNotUsedInTP.Checked)
      repMode = ArtsCompositionReportMode.NotUsedInTp;
    else if (this.rbtnReportModeUsedOnlyInTP.Checked)
      repMode = ArtsCompositionReportMode.UsedInTpOnly;
    applicabilityMode = ArtsCompositionReportApplicabilityMode.None;
    if (this.chbApplicabilityDesign.Enabled && this.chbApplicabilityDesign.Checked)
      applicabilityMode |= ArtsCompositionReportApplicabilityMode.Design;
    if (!this.chbApplicabilityTechProc.Enabled || !this.chbApplicabilityTechProc.Checked)
      return;
    applicabilityMode |= ArtsCompositionReportApplicabilityMode.TechProc;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objVirtualField"></param>
  /// <param name="elemQty"></param>
  private void AppendVirtualField4Count(
    TechObjectListVirtualDescriptor.ObjVirtualField objVirtualField,
    ElementQuantity elemQty)
  {
    objVirtualField.Add(TechCardConsts.AttributeTypes.Count4TechProcAttrID, elemQty.TechQuantity == null || elemQty.TechQuantity.MeasureID == 0L ? (object) (MeasuredValue) null : (object) elemQty.TechQuantity);
    objVirtualField.Add(TechCardConsts.AttributeTypes.Count4ArticleAttrID, elemQty.DesignQuantity == null || elemQty.DesignQuantity.MeasureID == 0L ? (object) (MeasuredValue) null : (object) elemQty.DesignQuantity);
    objVirtualField.Add(TechCardConsts.AttributeTypes.CountRemainAttrID, (object) elemQty.RemainQuantity);
  }

  /// <summary>Получение дескриптора для текущего режима отображения</summary>
  /// <returns></returns>
  private IDescriptor GetCurrentDescriptor()
  {
    ArtsCompositionReportMode repMode;
    ArtsCompositionReportApplicabilityMode applicabilityMode;
    this.GetCurrentReportMode(out repMode, out applicabilityMode);
    if (repMode == ArtsCompositionReportMode.Unknown)
      return this.GetCurrentDescriptor_Empty();
    return applicabilityMode != ArtsCompositionReportApplicabilityMode.None ? this.GetCurrentDescriptor_WithApplicability(repMode) : this.GetCurrentDescriptor_WithNoApplicability(repMode);
  }

  /// <summary>Получение дескриптора для текущего режима отображения</summary>
  /// <returns></returns>
  private IDescriptor GetCurrentDescriptor_Empty()
  {
    int articleBaseId = TechCardConsts.ObjectTypes.ArticleBaseID;
    TechObjectListVirtualDescriptor currentDescriptorEmpty = new TechObjectListVirtualDescriptor(Intermech.Navigator.Consts.CategorySelectObjectListsNode, articleBaseId, string.Empty, (IList) new List<long>());
    currentDescriptorEmpty.Mode = TechObjectListMode.UniqueValue;
    return (IDescriptor) currentDescriptorEmpty;
  }

  /// <summary>
  /// Получение дескриптора для текущего режима отображения без применяемости
  /// </summary>
  /// <returns></returns>
  private IDescriptor GetCurrentDescriptor_WithNoApplicability(ArtsCompositionReportMode reportMode)
  {
    int typeId = TechCardConsts.ObjectTypes.ArticleBaseID;
    TechObjectListMode techObjectListMode = TechObjectListMode.MultiValue;
    List<long> objectIDs = new List<long>();
    List<TechObjectListVirtualDescriptor.ObjVirtualField> collection = new List<TechObjectListVirtualDescriptor.ObjVirtualField>();
    if (TechCardConsts.ObjectTypes.TechArtCompositionTypes != null && TechCardConsts.ObjectTypes.TechArtCompositionTypes.Any<int>())
      typeId = TechCardConsts.ObjectTypes.TechArtCompositionTypes[0];
    List<long> list1 = this._techCompItems.Select<ArtsCompositionReportObjectItem, long>((System.Func<ArtsCompositionReportObjectItem, long>) (item => item.ArtObjectInfo.ItemID)).ToList<long>();
    List<long> list2 = this._designCompItems.Select<ArtsCompositionReportObjectItem, long>((System.Func<ArtsCompositionReportObjectItem, long>) (item => item.ArtObjectInfo.ItemID)).ToList<long>();
    GenericListHelper.MakeUnique<long>(list1);
    GenericListHelper.MakeUnique<long>(list2);
    foreach (KeyValuePair<Tuple<long, long>, ElementQuantity> elemQty in (IEnumerable<KeyValuePair<Tuple<long, long>, ElementQuantity>>) this._elemQtyList)
    {
      switch (reportMode)
      {
        case ArtsCompositionReportMode.UsedInTp:
          if (elemQty.Value.TechQuantity != null)
            break;
          continue;
        case ArtsCompositionReportMode.NotUsedInTp:
          if (elemQty.Value.TechQuantity != null || list1.BinarySearch(elemQty.Value.TypedInfoItem.ItemID) >= 0)
            continue;
          break;
        case ArtsCompositionReportMode.UsedInTpOnly:
          if (elemQty.Value.DesignQuantity != null || list2.BinarySearch(elemQty.Value.TypedInfoItem.ItemID) >= 0 || elemQty.Value.TechQuantity == null)
            continue;
          break;
      }
      objectIDs.Add(elemQty.Value.TypedInfoItem.ItemID);
      TechObjectListVirtualDescriptor.ObjVirtualField objVirtualField = new TechObjectListVirtualDescriptor.ObjVirtualField(elemQty.Value.TypedInfoItem.ItemID);
      this.AppendVirtualField4Count(objVirtualField, elemQty.Value);
      collection.Add(objVirtualField);
    }
    string empty = string.Empty;
    TechObjectListVirtualDescriptor virtualDescriptor = new TechObjectListVirtualDescriptor(Intermech.Navigator.Consts.CategorySelectObjectListsNode, typeId, empty, (IList) objectIDs);
    virtualDescriptor.Mode = techObjectListMode;
    TechObjectListVirtualDescriptor withNoApplicability = virtualDescriptor;
    if (objectIDs.Count != 0 && collection.Count != 0)
      withNoApplicability.VirtualData.AddRange((IEnumerable<TechObjectListVirtualDescriptor.ObjVirtualField>) collection);
    return (IDescriptor) withNoApplicability;
  }

  /// <summary>Получение дескриптора для текущего режима отображения</summary>
  /// <returns></returns>
  private IDescriptor GetCurrentDescriptor_WithApplicability(ArtsCompositionReportMode reportMode)
  {
    int typeId = TechCardConsts.ObjectTypes.ArticleBaseID;
    TechObjectListMode techObjectListMode = TechObjectListMode.MultiValue;
    List<long> objectIDs = new List<long>();
    List<TechObjectListVirtualDescriptor.ObjVirtualField> collection = new List<TechObjectListVirtualDescriptor.ObjVirtualField>();
    if (TechCardConsts.ObjectTypes.TechArtCompositionTypes != null && TechCardConsts.ObjectTypes.TechArtCompositionTypes.Any<int>())
      typeId = TechCardConsts.ObjectTypes.TechArtCompositionTypes[0];
    List<long> longList = new List<long>();
    foreach (KeyValuePair<Tuple<long, long>, ElementQuantity> elemQty in (IEnumerable<KeyValuePair<Tuple<long, long>, ElementQuantity>>) this._elemQtyList)
    {
      if (elemQty.Value.TechQuantity != null && !longList.Contains(elemQty.Value.TypedInfoItem.ItemID))
        longList.Add(elemQty.Value.TypedInfoItem.ItemID);
    }
    foreach (ArtsCompositionReportObjectItem reportObjectItem in reportMode == ArtsCompositionReportMode.NotUsedInTp ? this._designCompItems : this._techCompItems)
    {
      if (reportObjectItem != null)
      {
        long num = reportObjectItem.MValue != null ? MeasureHelper.GetBaseMeasureID_ByMeasureID(reportObjectItem.MValue.MeasureID) : -1L;
        ElementQuantity elemQty;
        if (this._elemQtyList.TryGetValue(new Tuple<long, long>(reportObjectItem.ArtObjectInfo.ItemID, num), out elemQty))
        {
          switch (reportMode)
          {
            case ArtsCompositionReportMode.UsedInTp:
              if (elemQty.TechQuantity != null)
                break;
              continue;
            case ArtsCompositionReportMode.NotUsedInTp:
              if (elemQty.TechQuantity == null)
                break;
              continue;
            case ArtsCompositionReportMode.UsedInTpOnly:
              if (elemQty.DesignQuantity != null || elemQty.TechQuantity == null)
                continue;
              break;
          }
          objectIDs.Add(elemQty.TypedInfoItem.ItemID);
          TechObjectListVirtualDescriptor.ObjVirtualField objVirtualField = new TechObjectListVirtualDescriptor.ObjVirtualField(elemQty.TypedInfoItem.ItemID);
          this.AppendVirtualField4Count(objVirtualField, elemQty);
          objVirtualField.Add(TechCardConsts.AttributeTypes.MemberOfSborkaObjectAttrID, reportObjectItem.ArtProjObjectInfo is IObjInfoCaption artProjObjectInfo ? (object) artProjObjectInfo.Caption : (object) (string) null);
          objVirtualField.Add(TechCardConsts.AttributeTypes.Count4CompositionAttrID, (object) reportObjectItem.MValue);
          foreach (KeyValuePair<int, object> extraField in (IEnumerable<KeyValuePair<int, object>>) reportObjectItem.ExtraFields)
            objVirtualField.Add(extraField.Key, extraField.Value);
          collection.Add(objVirtualField);
        }
      }
    }
    string empty = string.Empty;
    TechObjectListVirtualDescriptor virtualDescriptor = new TechObjectListVirtualDescriptor(Intermech.Navigator.Consts.CategorySelectObjectListsNode, typeId, empty, (IList) objectIDs);
    virtualDescriptor.Mode = techObjectListMode;
    TechObjectListVirtualDescriptor withApplicability = virtualDescriptor;
    if (objectIDs.Count != 0 && collection.Count != 0)
      withApplicability.VirtualData.AddRange((IEnumerable<TechObjectListVirtualDescriptor.ObjVirtualField>) collection);
    return (IDescriptor) withApplicability;
  }

  /// <summary>
  /// 
  /// </summary>
  protected override void InitializeCustomServices()
  {
    ArtsCompositionBaseForm.PluginsService.RegisterClientPlugin(ArtsCompositionDataProvider.PluginData.PluginGuid, (IClientPluginsDataTransfer) ArtsCompositionDataProvider.PluginData);
    base.InitializeCustomServices();
  }

  /// <summary>
  /// 
  /// </summary>
  protected override void ReleaseCustomServices()
  {
    ArtsCompositionBaseForm.PluginsService.UnregisterClientPlugin(ArtsCompositionDataProvider.PluginData.PluginGuid);
    base.ReleaseCustomServices();
  }

  /// <summary>Создать экземпляр формы</summary>
  /// <remarks></remarks>
  private ArtsCompositionReportForm(ArtsCompositionDataProvider dataProvider)
    : base(dataProvider)
  {
    this.InitializeComponent();
    this.InitializeCustomControls();
    if (this.DesignMode)
      return;
    this.InitializeData();
  }

  /// <summary>Вызвать форму как модальное окно</summary>
  /// <param name="caption">Заголовок формы</param>
  /// <param name="projDbObjectId">Идентификатор конструкторской сборочной единицы</param>
  /// <param name="techDbObjectId">Идентификатор версии технологического объекта (На данный момент ТП)</param>
  /// <param name="viewServices">Контейнер сервисов</param>
  /// <returns>Результат вызова формы</returns>
  public static DialogResult Execute(
    string caption,
    long projDbObjectId,
    long techDbObjectId,
    System.IServiceProvider viewServices)
  {
    ArtsCompositionBaseForm.PluginsService = ArtsCompositionBaseForm.PluginsService ?? ServiceUtils.GetService<IClientPluginsService>((object) ApplicationServices.Container, false);
    ArtsCompositionBaseForm.FiltrationService = ArtsCompositionBaseForm.FiltrationService ?? ServiceUtils.GetService<IFiltrationService>((object) ApplicationServices.Container, false);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(projDbObjectId, false);
      if (dbObject == null)
        return DialogResult.Cancel;
      List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive((IEnumerable<int>) new List<int>((IEnumerable<int>) TechCardConsts.ObjectTypes.TechArtCompositionTypes));
      childrenIdRecursive.AddRange((IEnumerable<int>) TechCardConsts.ObjectTypes.TechArtCompositionTypes);
      if (childrenIdRecursive.IndexOf(dbObject.ObjectType) < 0)
        return DialogResult.Cancel;
    }
    IArtsCompositionParams settings = (IArtsCompositionParams) null;
    ServiceUtils.GetService<IArtsCompositionParamsService>((object) ApplicationServices.Container, false)?.LoadSettings(out settings);
    using (ArtsCompositionReportForm compositionReportForm = new ArtsCompositionReportForm(new ArtsCompositionDataProvider((AsyncTaskBase<ObjInfoItem, DataTable>) new AsyncTask<ObjInfoItem, DataTable>((IAsyncTaskAction<ObjInfoItem, DataTable>) new ArtsCompositionTaskActionDesign(ArtsCompositionDataProvider.PluginData.AddContexts, settings == null || settings.DesignQuantityMode == ArtsCompositionQuantityMode.FullExpanded ? SearchDirection.RecursiveContains : SearchDirection.Contains)
    {
      ExtraColumns = (IEnumerable<ColumnDescriptor>) new ColumnDescriptor[1]
      {
        new ColumnDescriptor((object) MetaDataHelper.GetAttributeID((object) "cad00270-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0)
      }
    }, SynchronizationContext.Current), (AsyncTaskBase<ObjInfoItem, DataTable>) new AsyncTask<ObjInfoItem, DataTable>((IAsyncTaskAction<ObjInfoItem, DataTable>) new ArtsCompositionTaskActionTechProc(ArtsCompositionDataProvider.PluginData.AddContexts2), SynchronizationContext.Current))))
    {
      if (!compositionReportForm.Initialize(projDbObjectId, techDbObjectId, viewServices))
        return DialogResult.Abort;
      compositionReportForm.Text = caption != string.Empty ? caption : LocalizationHolder.rm.GetString(sc_19396.ssp_techcard_19397());
      return compositionReportForm.ShowDialog();
    }
  }

  /// <summary>Вызов закрытия формы по кнопке</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnOK_Click(object sender, EventArgs e) => this.Close();

  /// <summary>Закрытие формы</summary>
  /// <param name="sender"></param>
  /// <param name="e">Параметры</param>
  private void ArtsCompositionCreatorForm_FormClosing(object sender, FormClosingEventArgs e)
  {
    if (e.CloseReason != CloseReason.UserClosing)
      return;
    this.CancelLoadData();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void ArtsCompositionCreatorForm_Shown(object sender, EventArgs e)
  {
    if (!this._needLoadData)
      return;
    this.DoRefreshComposition((object) this, (EventArgs) null);
  }

  /// <summary>Сохраним настройки формы в настройках пользователя</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void ArtsCompositionCreatorForm_FormClosed(object sender, FormClosedEventArgs e)
  {
    if (this._techNavTreeView != null)
      this._techNavTreeView.Services = (System.IServiceProvider) null;
    this.SaveSettings(true);
  }

  /// <summary>Изменился контекст состава в дереве КСЕ</summary>
  /// <param name="sender"></param>
  /// <param name="e">Параметры</param>
  private void cbContext_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (!this._needLoadData || this.tscbContext.SelectedIndex < 0 || !(this.tscbContext.Items[this.tscbContext.SelectedIndex] is MyElement myElement))
      return;
    ArtsCompositionDataProvider.PluginData.AddContexts[1] = (long) myElement.Value;
    this._dataProvider.LoadedDesignData = false;
    this.DoRefreshComposition((object) this, (EventArgs) null);
  }

  /// <summary>
  /// Пришло событие "Изменился рендер панелей инструментов"
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void ToolbarRendererChanged(object sender, EventArgs e)
  {
  }

  /// <summary>Изменение признака отображения по входимости</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void chbApplicability_CheckedChanged(object sender, EventArgs e)
  {
    this.UpdateControls();
    this.DoRefreshComposition(sender, (EventArgs) null);
  }

  /// <summary>Изменения режима отображения изделий</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void rbtnReportMode_CheckedChanged(object sender, EventArgs e)
  {
    if (!(sender is RadioButton radioButton) || !radioButton.Checked)
      return;
    this.UpdateControls();
    this.DoRefreshComposition(sender, (EventArgs) null);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <returns></returns>
  public NodeColumnCollection techNavTreeViewOnOnGetSupportedColumnsEventHandler(object sender)
  {
    return this.GetReportModeSupportedColumns(sender, false);
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      BarManager service = ServiceUtils.GetService<BarManager>((object) ApplicationServices.Container, false);
      if (service != null)
        service.RendererChanged -= new EventHandler(this.ToolbarRendererChanged);
    }
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ArtsCompositionReportForm));
    this.imagesToolbars = new ImageList(this.components);
    this.panelPage1 = new Panel();
    this.pnlClient = new Panel();
    this.pnlProgress = new Panel();
    this.lblProgressInfo = new Label();
    this.prgbarProgress = new ProgressBar();
    this.pnlBottom = new Panel();
    this.grbMain = new GroupBox();
    this.rbtnReportModeUsedOnlyInTP = new RadioButton();
    this.chbApplicabilityTechProc = new CheckBox();
    this.chbApplicabilityDesign = new CheckBox();
    this.rbtnReportModeNotUsedInTP = new RadioButton();
    this.rbtnReportModeUsedInTp = new RadioButton();
    this.pnlButtons = new Panel();
    this.btnOK = new Button();
    this.pnlTop = new Panel();
    this.pnlTopHeader = new Panel();
    this.lblCaption = new Label();
    this.pictCaption = new PictureBox();
    this.tsMain = new ToolStrip();
    this.tslblContext = new ToolStripLabel();
    this.tscbContext = new ToolStripComboBox();
    this.toolStripSeparator1 = new ToolStripSeparator();
    this.contextMenuComposition = new ContextMenuBarItem();
    this.mnpAddToCC = new MenuButtonItem();
    this.mnpRefresh = new MenuButtonItem();
    this.menuButtonItem1 = new MenuButtonItem();
    this.panelPage1.SuspendLayout();
    this.pnlProgress.SuspendLayout();
    this.pnlBottom.SuspendLayout();
    this.grbMain.SuspendLayout();
    this.pnlButtons.SuspendLayout();
    this.pnlTop.SuspendLayout();
    this.pnlTopHeader.SuspendLayout();
    ((ISupportInitialize) this.pictCaption).BeginInit();
    this.tsMain.SuspendLayout();
    this.SuspendLayout();
    this.imagesToolbars.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imagesToolbars.ImageStream");
    this.imagesToolbars.TransparentColor = Color.Transparent;
    this.imagesToolbars.Images.SetKeyName(0, "arrow_right_blue.ico");
    this.imagesToolbars.Images.SetKeyName(1, "arrow_left_blue.ico");
    this.imagesToolbars.Images.SetKeyName(2, "refresh.ico");
    this.panelPage1.Controls.Add((Control) this.pnlClient);
    this.panelPage1.Controls.Add((Control) this.pnlProgress);
    this.panelPage1.Controls.Add((Control) this.pnlBottom);
    this.panelPage1.Controls.Add((Control) this.pnlButtons);
    this.panelPage1.Controls.Add((Control) this.pnlTop);
    componentResourceManager.ApplyResources((object) this.panelPage1, "panelPage1");
    this.panelPage1.Name = "panelPage1";
    componentResourceManager.ApplyResources((object) this.pnlClient, "pnlClient");
    this.pnlClient.Name = "pnlClient";
    this.pnlProgress.Controls.Add((Control) this.lblProgressInfo);
    this.pnlProgress.Controls.Add((Control) this.prgbarProgress);
    componentResourceManager.ApplyResources((object) this.pnlProgress, "pnlProgress");
    this.pnlProgress.Name = "pnlProgress";
    componentResourceManager.ApplyResources((object) this.lblProgressInfo, "lblProgressInfo");
    this.lblProgressInfo.Name = "lblProgressInfo";
    componentResourceManager.ApplyResources((object) this.prgbarProgress, "prgbarProgress");
    this.prgbarProgress.Name = "prgbarProgress";
    this.pnlBottom.Controls.Add((Control) this.grbMain);
    componentResourceManager.ApplyResources((object) this.pnlBottom, "pnlBottom");
    this.pnlBottom.Name = "pnlBottom";
    this.grbMain.Controls.Add((Control) this.rbtnReportModeUsedOnlyInTP);
    this.grbMain.Controls.Add((Control) this.chbApplicabilityTechProc);
    this.grbMain.Controls.Add((Control) this.chbApplicabilityDesign);
    this.grbMain.Controls.Add((Control) this.rbtnReportModeNotUsedInTP);
    this.grbMain.Controls.Add((Control) this.rbtnReportModeUsedInTp);
    componentResourceManager.ApplyResources((object) this.grbMain, "grbMain");
    this.grbMain.Name = "grbMain";
    this.grbMain.TabStop = false;
    componentResourceManager.ApplyResources((object) this.rbtnReportModeUsedOnlyInTP, "rbtnReportModeUsedOnlyInTP");
    this.rbtnReportModeUsedOnlyInTP.Name = "rbtnReportModeUsedOnlyInTP";
    this.rbtnReportModeUsedOnlyInTP.TabStop = true;
    this.rbtnReportModeUsedOnlyInTP.UseVisualStyleBackColor = true;
    this.rbtnReportModeUsedOnlyInTP.CheckedChanged += new EventHandler(this.rbtnReportMode_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.chbApplicabilityTechProc, "chbApplicabilityTechProc");
    this.chbApplicabilityTechProc.Name = "chbApplicabilityTechProc";
    this.chbApplicabilityTechProc.UseVisualStyleBackColor = true;
    this.chbApplicabilityTechProc.CheckedChanged += new EventHandler(this.chbApplicability_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.chbApplicabilityDesign, "chbApplicabilityDesign");
    this.chbApplicabilityDesign.Name = "chbApplicabilityDesign";
    this.chbApplicabilityDesign.UseVisualStyleBackColor = true;
    this.chbApplicabilityDesign.CheckedChanged += new EventHandler(this.chbApplicability_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.rbtnReportModeNotUsedInTP, "rbtnReportModeNotUsedInTP");
    this.rbtnReportModeNotUsedInTP.Name = "rbtnReportModeNotUsedInTP";
    this.rbtnReportModeNotUsedInTP.TabStop = true;
    this.rbtnReportModeNotUsedInTP.UseVisualStyleBackColor = true;
    this.rbtnReportModeNotUsedInTP.CheckedChanged += new EventHandler(this.rbtnReportMode_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.rbtnReportModeUsedInTp, "rbtnReportModeUsedInTp");
    this.rbtnReportModeUsedInTp.Checked = true;
    this.rbtnReportModeUsedInTp.Name = "rbtnReportModeUsedInTp";
    this.rbtnReportModeUsedInTp.TabStop = true;
    this.rbtnReportModeUsedInTp.UseVisualStyleBackColor = true;
    this.rbtnReportModeUsedInTp.CheckedChanged += new EventHandler(this.rbtnReportMode_CheckedChanged);
    this.pnlButtons.Controls.Add((Control) this.btnOK);
    componentResourceManager.ApplyResources((object) this.pnlButtons, "pnlButtons");
    this.pnlButtons.Name = "pnlButtons";
    componentResourceManager.ApplyResources((object) this.btnOK, "btnOK");
    this.btnOK.Cursor = Cursors.Default;
    this.btnOK.DialogResult = DialogResult.OK;
    this.btnOK.Name = "btnOK";
    this.btnOK.Click += new EventHandler(this.btnOK_Click);
    componentResourceManager.ApplyResources((object) this.pnlTop, "pnlTop");
    this.pnlTop.Controls.Add((Control) this.pnlTopHeader);
    this.pnlTop.Controls.Add((Control) this.tsMain);
    this.pnlTop.Name = "pnlTop";
    this.pnlTopHeader.Controls.Add((Control) this.lblCaption);
    this.pnlTopHeader.Controls.Add((Control) this.pictCaption);
    componentResourceManager.ApplyResources((object) this.pnlTopHeader, "pnlTopHeader");
    this.pnlTopHeader.Name = "pnlTopHeader";
    componentResourceManager.ApplyResources((object) this.lblCaption, "lblCaption");
    this.lblCaption.ForeColor = SystemColors.GrayText;
    this.lblCaption.Name = "lblCaption";
    componentResourceManager.ApplyResources((object) this.pictCaption, "pictCaption");
    this.pictCaption.Name = "pictCaption";
    this.pictCaption.TabStop = false;
    componentResourceManager.ApplyResources((object) this.tsMain, "tsMain");
    this.tsMain.Items.AddRange(new ToolStripItem[3]
    {
      (ToolStripItem) this.tslblContext,
      (ToolStripItem) this.tscbContext,
      (ToolStripItem) this.toolStripSeparator1
    });
    this.tsMain.Name = "tsMain";
    this.tslblContext.Name = "tslblContext";
    componentResourceManager.ApplyResources((object) this.tslblContext, "tslblContext");
    this.tscbContext.DropDownStyle = ComboBoxStyle.DropDownList;
    this.tscbContext.Name = "tscbContext";
    componentResourceManager.ApplyResources((object) this.tscbContext, "tscbContext");
    this.tscbContext.SelectedIndexChanged += new EventHandler(this.cbContext_SelectedIndexChanged);
    this.toolStripSeparator1.Name = "toolStripSeparator1";
    componentResourceManager.ApplyResources((object) this.toolStripSeparator1, "toolStripSeparator1");
    componentResourceManager.ApplyResources((object) this.contextMenuComposition, "contextMenuComposition");
    this.contextMenuComposition.Items.AddRange(new ToolbarItemBase[3]
    {
      (ToolbarItemBase) this.mnpAddToCC,
      (ToolbarItemBase) this.mnpRefresh,
      (ToolbarItemBase) this.menuButtonItem1
    });
    this.contextMenuComposition.ShowText = true;
    componentResourceManager.ApplyResources((object) this.mnpAddToCC, "mnpAddToCC");
    this.mnpAddToCC.ImageIndex = 0;
    this.mnpAddToCC.ShowText = true;
    this.mnpRefresh.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.mnpRefresh, "mnpRefresh");
    this.mnpRefresh.ImageIndex = 2;
    this.mnpRefresh.ShowText = true;
    componentResourceManager.ApplyResources((object) this.menuButtonItem1, "menuButtonItem1");
    this.menuButtonItem1.ShowText = true;
    this.AutoScaleMode = AutoScaleMode.Inherit;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.panelPage1);
    this.Name = nameof (ArtsCompositionReportForm);
    this.FormClosing += new FormClosingEventHandler(this.ArtsCompositionCreatorForm_FormClosing);
    this.FormClosed += new FormClosedEventHandler(this.ArtsCompositionCreatorForm_FormClosed);
    this.Shown += new EventHandler(this.ArtsCompositionCreatorForm_Shown);
    this.panelPage1.ResumeLayout(false);
    this.panelPage1.PerformLayout();
    this.pnlProgress.ResumeLayout(false);
    this.pnlBottom.ResumeLayout(false);
    this.grbMain.ResumeLayout(false);
    this.grbMain.PerformLayout();
    this.pnlButtons.ResumeLayout(false);
    this.pnlTop.ResumeLayout(false);
    this.pnlTop.PerformLayout();
    this.pnlTopHeader.ResumeLayout(false);
    ((ISupportInitialize) this.pictCaption).EndInit();
    this.tsMain.ResumeLayout(false);
    this.tsMain.PerformLayout();
    this.ResumeLayout(false);
  }
}
