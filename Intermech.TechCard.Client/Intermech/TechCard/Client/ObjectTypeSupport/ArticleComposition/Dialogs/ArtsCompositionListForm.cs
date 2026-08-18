// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Dialogs.ArtsCompositionListForm
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Pdm;
using Intermech.Interfaces.TechCard;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using Intermech.TechCard.Client.Classes.Tasks;
using Intermech.TechCard.Client.Navigator.Descriptors;
using Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Params;
using Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Threading;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Dialogs;

/// <summary>
/// Форма для создания контекстной сборочной единицы из списка объектов
/// </summary>
internal class ArtsCompositionListForm : ArtsCompositionForm
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  /// <summary>
  /// 
  /// </summary>
  public ArtsCompositionListForm(
    ArtsCompositionForm.ObjCreateParams objCreateParams,
    ArtsCompositionDataProvider dataProvider)
    : base(objCreateParams, dataProvider)
  {
    this.InitializeComponent();
    this.InitializeCustomControls();
  }

  /// <summary>Инициализация кастом контролов</summary>
  private void InitializeCustomControls()
  {
    this._techNavControl.TreeView.SetColumns(Intermech.Navigator.Utils.CaptionColumnOnly(NodeColumnSortOrder.None));
  }

  /// <summary>Загрузить данные в дерево навигатора</summary>
  /// <returns>true, если загрузка прошла успешно</returns>
  protected override void LoadFormTreeData()
  {
    ArtsCompositionDataProvider.PluginData.CurrentSet = 0;
    DescriptorCollection descriptors = new DescriptorCollection();
    foreach (int artCompositionType in (IEnumerable<int>) TechCardConsts.ObjectTypes.TechArtCompositionTypes)
      descriptors.Add((IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(artCompositionType));
    IDescriptor descriptor = descriptors.Count != 1 ? (IDescriptor) new TechDescriptor(Intermech.Navigator.Consts.CategorySelectObjectListsNode, TechCardConsts.ObjectTypes.ArticleBaseID, LocalizationHolder.rm.GetString("TechCard.Client_227"), descriptors) : descriptors[0];
    this._techNavControl.TreeView.OnGetSupportedColumnsEventHandler += new Intermech.Navigator.Controls.GetSupportedColumnsEventHandler(Intermech.Navigator.Utils.GetNavigatorColumns);
    this._techNavControl.RootDescriptor = descriptor;
  }

  /// <summary>Обновить дерево КСЕ</summary>
  /// <param name="sender"></param>
  /// <param name="e">Параметры</param>
  protected override void DoRefreshComposition(object sender, EventArgs e)
  {
    ArtsCompositionDataProvider.PluginData.CurrentSet = 0;
    DescriptorCollection descriptors = new DescriptorCollection();
    foreach (int artCompositionType in (IEnumerable<int>) TechCardConsts.ObjectTypes.TechArtCompositionTypes)
      descriptors.Add((IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(artCompositionType));
    this._techNavControl.TreeView.Build(descriptors.Count != 1 ? (IDescriptor) new TechDescriptor(Intermech.Navigator.Consts.CategorySelectObjectListsNode, TechCardConsts.ObjectTypes.ArticleBaseID, LocalizationHolder.rm.GetString("TechCard.Client_227"), descriptors) : descriptors[0]);
    this.UpdateControls();
    this._dataProvider.LoadedDesignData = false;
  }

  /// <summary>Вызвать форму как модальное окно</summary>
  /// <param name="frmCaption">Заголовок формы</param>
  /// <param name="techDbObjId">Идентификатор версии технологического объекта (На данный момент ТП)</param>
  /// <param name="objCreateParams">Параметры создания объектов</param>
  /// <param name="viewServices">Контейнер сервисов</param>
  /// <returns>Результат вызова формы</returns>
  public static DialogResult Execute(
    string frmCaption,
    long techDbObjId,
    ArtsCompositionForm.ObjCreateParams objCreateParams,
    System.IServiceProvider viewServices)
  {
    ArtsCompositionBaseForm.PluginsService = ArtsCompositionBaseForm.PluginsService ?? ServiceUtils.GetService<IClientPluginsService>((object) ApplicationServices.Container, false);
    ArtsCompositionBaseForm.FiltrationService = ArtsCompositionBaseForm.FiltrationService ?? ServiceUtils.GetService<IFiltrationService>((object) ApplicationServices.Container, false);
    IArtsCompositionParams settings = (IArtsCompositionParams) null;
    ServiceUtils.GetService<IArtsCompositionParamsService>((object) ApplicationServices.Container, false)?.LoadSettings(out settings);
    ArtsCompositionDataProvider dataProvider = new ArtsCompositionDataProvider((AsyncTaskBase<ObjInfoItem, DataTable>) new AsyncTask<ObjInfoItem, DataTable>((IAsyncTaskAction<ObjInfoItem, DataTable>) new ArtsCompositionTaskActionDesign(ArtsCompositionDataProvider.PluginData.AddContexts, SearchDirection.RecursiveContains)
    {
      ObjectGrouping = ((settings != null ? (int) settings.DesignQuantityMode : 0) == 0)
    }, SynchronizationContext.Current), (AsyncTaskBase<ObjInfoItem, DataTable>) new AsyncTask<ObjInfoItem, DataTable>((IAsyncTaskAction<ObjInfoItem, DataTable>) new ArtsCompositionTaskActionTechProc(ArtsCompositionDataProvider.PluginData.AddContexts2), SynchronizationContext.Current));
    using (ArtsCompositionListForm compositionListForm = new ArtsCompositionListForm(objCreateParams, dataProvider))
    {
      if (!compositionListForm.Initialize(0L, techDbObjId, viewServices))
        return DialogResult.Abort;
      compositionListForm.Text = frmCaption != string.Empty ? frmCaption : LocalizationHolder.rm.GetString(sc_19392.ssp_techcard_19393());
      return compositionListForm.ShowDialog();
    }
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ArtsCompositionListForm));
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Name = nameof (ArtsCompositionListForm);
    this.ResumeLayout(false);
  }
}
