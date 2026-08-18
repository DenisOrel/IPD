
// Type: Intermech.Client.Core.ElementStatusesView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Configuration;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.DBObjects;
using Intermech.Search;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;


namespace Intermech.Client.Core;

/// <summary>
/// Вьюшка, позволяющая настроить список плагинов, которым разрешено добавлять свои статусы в столбец "Статусы элемента"
/// </summary>
public class ElementStatusesView : UserControl, IPropertyPage, IPropertyPageSearchOptionEvents
{
  /// <summary>Список Guid запрещённых плагинов</summary>
  private List<string> FDisabledPlugins;
  /// <summary>Служба по управлению статусами элементов</summary>
  private IElementStatusesClientService _elementStatusesClientService;
  /// <summary>Контейнер служб</summary>
  private System.IServiceProvider _provider;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private iGrid grid;
  private iGCellStyle iGrid1DefaultCellStyle;
  private iGColHdrStyle iGrid1DefaultColHdrStyle;
  private iGCellStyle iGrid1RowTextColCellStyle;
  private iGCellStyle gridCol0CellStyle;
  private iGColHdrStyle gridCol0ColHdrStyle;
  private iGCellStyle gridCol1CellStyle;
  private iGColHdrStyle gridCol1ColHdrStyle;
  private iGCellStyle gridCol2CellStyle;
  private iGColHdrStyle gridCol2ColHdrStyle;
  private Panel panelBottom;
  private Label label1;
  private CheckBox cbShowVersionsLog;

  /// <summary>Создать и инициализировать экземпляр вьюшки</summary>
  public ElementStatusesView(System.IServiceProvider provider)
  {
    this.InitializeComponent();
    this._provider = provider;
    if (this.FDisabledPlugins == null)
      this.FDisabledPlugins = new List<string>(0);
    this._elementStatusesClientService = ServicesManager.GetService(typeof (IElementStatusesClientService)) as IElementStatusesClientService;
    if (this._elementStatusesClientService.DisabledPlugins != null)
    {
      for (int index = 0; index < this._elementStatusesClientService.DisabledPlugins.Count; ++index)
        this.FDisabledPlugins.Add(this._elementStatusesClientService.DisabledPlugins[index]);
    }
    this.FillPluginsList(this.FDisabledPlugins);
    this.UpdateControls();
    INavGraphicsCache service1 = ServicesManager.GetService(typeof (INavGraphicsCache)) as INavGraphicsCache;
    this.grid.BackColor = service1.CurrentColorsScheme.Background;
    this.grid.ForeColor = service1.CurrentColorsScheme.Foreground;
    this.grid.HighlightBackColor = service1.CurrentColorsScheme.BackgroundSelected;
    this.grid.HighlightForeColor = service1.CurrentColorsScheme.ForegroundSelected;
    this.grid.HighlightBackColorNoFocus = service1.CurrentColorsScheme.BackgroundSelectedInactive;
    this.grid.HighlightForeColorNoFocus = service1.CurrentColorsScheme.ForegroundSelectedInactive;
    if (!(this._provider.GetService(typeof (IPropertyPagesService)) is IPropertyPagesService service2))
      return;
    service2.AddPage(LocalizationHolder.rm.GetString("Client.Core_882"), (IPropertyPage) this);
  }

  /// <summary>
  /// Пересчитать ширину колонок в списке при изменении размера вьюшки
  /// </summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void DoRecalcColumns(object sender, EventArgs e)
  {
    int num = this.grid.ClientSize.Width - 30 - this.grid.Cols[0].Width - this.grid.Cols[1].Width;
    if (num <= 0)
      return;
    this.grid.Cols[2].Width = num;
  }

  /// <summary>вернуть id раздела в хелпе для данной страницы</summary>
  public string HelpTopicID => "1981";

  /// <summary>Событие будет дёргаться при необходимости</summary>
  private void OnChanged()
  {
    if (this.Changed == null)
      return;
    this.Changed((object) this, new EventArgs());
  }

  /// <summary>Обработчик событий</summary>
  public event EventHandler Changed;

  /// <summary>
  /// Что за хрень мы добавили в окно настроек? Ответ - контрол
  /// </summary>
  public PropertyPageType Type => PropertyPageType.Control;

  /// <summary>
  /// Контрол, который будет размещён на главной форме настроек
  /// </summary>
  public object Control => (object) this;

  /// <summary>Название странички в главной форме настроек</summary>
  public string PageName => LocalizationHolder.rm.GetString("Client.Core_883");

  /// <summary>
  /// Текст заголовка (пустое значение - заголовок не отображается)
  /// </summary>
  public string HeaderText
  {
    [DebuggerStepThrough] get => this.PageName;
  }

  /// <summary>Применить изменения редактора</summary>
  public void Apply()
  {
    if (this._elementStatusesClientService.DisabledPlugins != null)
    {
      this._elementStatusesClientService.DisabledPlugins.Clear();
      for (int index = 0; index < this.grid.Rows.Count; ++index)
      {
        ElementStatusesPluginDescription tag = this.grid.Rows[index].Tag as ElementStatusesPluginDescription;
        if (!(bool) this.grid.Rows[index].Cells[0].Value)
          this._elementStatusesClientService.DisabledPlugins.Add(tag.PluginGuid.ToString());
      }
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        this._elementStatusesClientService.SaveUserSettings(sessionKeeper.Session);
    }
    IConfigurationManager service = ServicesManager.GetService(typeof (IConfigurationManager)) as IConfigurationManager;
    (service.Open("UISettings") ?? service.Create("UISettings")).SetProperty("ShowVersionsLog", this.cbShowVersionsLog.Checked.ToString());
    UISettings.ShowVersionsLog = this.cbShowVersionsLog.Checked;
    if (!ServiceLocator.IsRegistered<StatusesInfoService>())
      return;
    ServiceLocator.Get<StatusesInfoService>().Reload();
  }

  /// <summary>Отменить изменения редактора</summary>
  public void Cancel()
  {
    this.FillPluginsList(this._elementStatusesClientService.DisabledPlugins);
    this.cbShowVersionsLog.Checked = UISettings.ShowVersionsLog;
  }

  /// <summary>
  /// Возвращает список имен настроек, содержащихся в контроле
  /// </summary>
  public List<string> GetOptionNames()
  {
    return !(this.Control is System.Windows.Forms.Control control) ? new List<string>() : IPropertyPageHelper.GetOptionNames(control);
  }

  /// <summary>Обновить контролы</summary>
  private void UpdateControls()
  {
  }

  /// <summary>
  /// Заполнить список плагинов, выделить в списке плагины, к которым идёт обращение за статусами
  /// </summary>
  /// <param name="disabledPlugins">Список Guid плагинов, которые запрещены для выдачи статусов</param>
  private void FillPluginsList(List<string> disabledPlugins)
  {
    List<Guid> guidList = new List<Guid>();
    if (disabledPlugins != null)
    {
      foreach (string disabledPlugin in disabledPlugins)
      {
        Guid empty = Guid.Empty;
        ref Guid local = ref empty;
        if (Guid.TryParse(disabledPlugin, out local))
          guidList.Add(empty);
      }
    }
    try
    {
      this.grid.BeginUpdate();
      this.grid.Rows.Clear();
      if (this._elementStatusesClientService == null)
        return;
      IDictionaryEnumerator enumerator = (IDictionaryEnumerator) this._elementStatusesClientService.Plugins.GetEnumerator();
      enumerator.Reset();
      while (enumerator.MoveNext())
      {
        ElementStatusesPluginDescription pluginDescription = enumerator.Value as ElementStatusesPluginDescription;
        iGRow iGrow = this.grid.Rows.Add();
        iGrow.Key = pluginDescription.PluginGuid;
        iGrow.Tag = (object) pluginDescription;
        Guid result = Guid.Empty;
        bool flag = Guid.TryParse(pluginDescription.PluginGuid, out result);
        iGrow.Cells[0].Value = (object) (bool) (disabledPlugins == null ? 1 : (disabledPlugins == null ? 0 : (!flag || guidList.Contains(result) ? (flag ? 0 : (!disabledPlugins.Contains(pluginDescription.PluginGuid) ? 1 : 0)) : 1)));
        iGrow.Cells[1].Value = (object) pluginDescription.PluginName;
        iGrow.Cells[2].Value = (object) pluginDescription.StatusesDescription;
      }
    }
    finally
    {
      this.grid.EndUpdate();
      this.UpdateControls();
    }
  }

  /// <summary>Нажата клавиша</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void grid_KeyPress(object sender, KeyPressEventArgs e)
  {
    if (e.KeyChar != ' ')
      return;
    iGRow row = this.grid.SelectedCells.Count > 0 ? this.grid.SelectedCells[0].Row : (iGRow) null;
    if (row == null)
      return;
    row.Cells[0].Value = (object) !(bool) row.Cells[0].Value;
    this.OnChanged();
    e.Handled = true;
  }

  /// <summary>
  /// Пользователь завершает внесение изменений в видимость очередного плагина
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void grid_BeforeCommitEdit(object sender, iGBeforeCommitEditEventArgs e)
  {
    this.OnChanged();
  }

  /// <summary>Изменился флажок "Показывать протокол подбора версий"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void cbShowVersionsLog_CheckedChanged(object sender, EventArgs e) => this.OnChanged();

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
    iGColPattern iGcolPattern1 = new iGColPattern();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ElementStatusesView));
    iGColPattern iGcolPattern2 = new iGColPattern();
    iGColPattern iGcolPattern3 = new iGColPattern();
    this.gridCol2CellStyle = new iGCellStyle(true);
    this.gridCol2ColHdrStyle = new iGColHdrStyle(true);
    this.gridCol0CellStyle = new iGCellStyle(true);
    this.gridCol0ColHdrStyle = new iGColHdrStyle(true);
    this.gridCol1CellStyle = new iGCellStyle(true);
    this.gridCol1ColHdrStyle = new iGColHdrStyle(true);
    this.grid = new iGrid();
    this.iGrid1DefaultCellStyle = new iGCellStyle(true);
    this.iGrid1DefaultColHdrStyle = new iGColHdrStyle(true);
    this.iGrid1RowTextColCellStyle = new iGCellStyle(true);
    this.panelBottom = new Panel();
    this.cbShowVersionsLog = new CheckBox();
    this.label1 = new Label();
    ((ISupportInitialize) this.grid).BeginInit();
    this.panelBottom.SuspendLayout();
    this.SuspendLayout();
    this.gridCol2CellStyle.Flags = iGCellFlags.DisplayImage;
    this.gridCol2CellStyle.ImageAlign = iGContentAlignment.MiddleCenter;
    this.gridCol2CellStyle.SingleClickEdit = iGBool.True;
    this.gridCol2CellStyle.TextAlign = iGContentAlignment.MiddleCenter;
    this.gridCol2CellStyle.Type = iGCellType.Check;
    this.gridCol2CellStyle.ValueType = typeof (bool);
    this.gridCol0CellStyle.ReadOnly = iGBool.True;
    this.gridCol0CellStyle.SingleClickEdit = iGBool.False;
    this.gridCol0CellStyle.TextAlign = iGContentAlignment.MiddleLeft;
    this.gridCol0CellStyle.Type = iGCellType.NotSet;
    this.gridCol0ColHdrStyle.TextAlign = iGContentAlignment.MiddleLeft;
    this.gridCol1CellStyle.ReadOnly = iGBool.True;
    this.gridCol1CellStyle.TextAlign = iGContentAlignment.MiddleLeft;
    this.gridCol1ColHdrStyle.TextAlign = iGContentAlignment.MiddleLeft;
    iGcolPattern1.AllowGrouping = false;
    iGcolPattern1.AllowMoving = false;
    iGcolPattern1.AllowSizing = false;
    iGcolPattern1.CellStyle = this.gridCol2CellStyle;
    iGcolPattern1.ColHdrStyle = this.gridCol2ColHdrStyle;
    componentResourceManager.ApplyResources((object) iGcolPattern1, "iGColPattern1");
    iGcolPattern2.AllowGrouping = false;
    iGcolPattern2.AllowMoving = false;
    iGcolPattern2.CellStyle = this.gridCol0CellStyle;
    iGcolPattern2.ColHdrStyle = this.gridCol0ColHdrStyle;
    componentResourceManager.ApplyResources((object) iGcolPattern2, "iGColPattern2");
    iGcolPattern3.AllowGrouping = false;
    iGcolPattern3.AllowMoving = false;
    iGcolPattern3.CellStyle = this.gridCol1CellStyle;
    iGcolPattern3.ColHdrStyle = this.gridCol1ColHdrStyle;
    componentResourceManager.ApplyResources((object) iGcolPattern3, "iGColPattern3");
    this.grid.Cols.AddRange(new iGColPattern[3]
    {
      iGcolPattern1,
      iGcolPattern2,
      iGcolPattern3
    });
    this.grid.DefaultCol.CellStyle = this.iGrid1DefaultCellStyle;
    this.grid.DefaultCol.ColHdrStyle = this.iGrid1DefaultColHdrStyle;
    this.grid.DefaultRow.Height = (int) componentResourceManager.GetObject("resource.Height");
    this.grid.DefaultRow.NormalCellHeight = (int) componentResourceManager.GetObject("resource.NormalCellHeight");
    this.grid.DefaultRow.Sortable = false;
    componentResourceManager.ApplyResources((object) this.grid, "grid");
    this.grid.Header.AllowPress = false;
    this.grid.Header.Height = (int) componentResourceManager.GetObject("grid.Header.Height");
    this.grid.Header.HotTrackFlags = iGHdrHotTrackFlags.None;
    this.grid.Name = "grid";
    this.grid.RowMode = true;
    this.grid.RowModeHasCurCell = true;
    this.grid.RowTextCol.CellStyle = this.iGrid1RowTextColCellStyle;
    this.grid.SingleClickEdit = true;
    this.grid.BeforeCommitEdit += new iGBeforeCommitEditEventHandler(this.grid_BeforeCommitEdit);
    this.grid.KeyPress += new KeyPressEventHandler(this.grid_KeyPress);
    this.grid.Resize += new EventHandler(this.DoRecalcColumns);
    this.panelBottom.BorderStyle = BorderStyle.Fixed3D;
    this.panelBottom.Controls.Add((System.Windows.Forms.Control) this.cbShowVersionsLog);
    componentResourceManager.ApplyResources((object) this.panelBottom, "panelBottom");
    this.panelBottom.Name = "panelBottom";
    componentResourceManager.ApplyResources((object) this.cbShowVersionsLog, "cbShowVersionsLog");
    this.cbShowVersionsLog.Name = "cbShowVersionsLog";
    this.cbShowVersionsLog.UseVisualStyleBackColor = true;
    this.cbShowVersionsLog.CheckedChanged += new EventHandler(this.cbShowVersionsLog_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Inherit;
    this.Controls.Add((System.Windows.Forms.Control) this.grid);
    this.Controls.Add((System.Windows.Forms.Control) this.label1);
    this.Controls.Add((System.Windows.Forms.Control) this.panelBottom);
    this.ForeColor = SystemColors.ControlText;
    this.Name = nameof (ElementStatusesView);
    ((ISupportInitialize) this.grid).EndInit();
    this.panelBottom.ResumeLayout(false);
    this.panelBottom.PerformLayout();
    this.ResumeLayout(false);
  }
}
