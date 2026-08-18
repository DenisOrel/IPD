
// Type: Intermech.Client.Core.RecentObjectsSettingsViewPage
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Extensions;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Search.RecentObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary>
/// Закладка для окна "Настройки", управляющая списком недавних объектов
/// </summary>
public class RecentObjectsSettingsViewPage : 
  UserControl,
  IPropertyPage,
  IPropertyPageSearchOptionEvents
{
  /// <summary>Контейнер служб</summary>
  private System.IServiceProvider _provider;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panelMain;
  private Label labelCount;
  private NumericUpDown edCount;
  private Label labelActions;
  private CheckedListBox cbActions;

  /// <summary>Создать и инициализировать экземпляр вьюшки</summary>
  public RecentObjectsSettingsViewPage(System.IServiceProvider provider)
  {
    this.InitializeComponent();
    this._provider = provider;
    this.cbActions.Items.Clear();
    this.cbActions.Items.AddRange(new object[10]
    {
      (object) RecentObjectAction.Create.GetDescription<RecentObjectAction>(),
      (object) RecentObjectAction.CheckOut.GetDescription<RecentObjectAction>(),
      (object) RecentObjectAction.CheckIn.GetDescription<RecentObjectAction>(),
      (object) RecentObjectAction.CancelChanges.GetDescription<RecentObjectAction>(),
      (object) RecentObjectAction.SaveChanges.GetDescription<RecentObjectAction>(),
      (object) RecentObjectAction.OpenInNewWindow.GetDescription<RecentObjectAction>(),
      (object) RecentObjectAction.Open.GetDescription<RecentObjectAction>(),
      (object) RecentObjectAction.Edit.GetDescription<RecentObjectAction>(),
      (object) RecentObjectAction.View.GetDescription<RecentObjectAction>(),
      (object) RecentObjectAction.Print.GetDescription<RecentObjectAction>()
    });
    this.UpdateControls();
    this.LoadSettings();
    if (!(this._provider.GetService(typeof (IPropertyPagesService)) is IPropertyPagesService service))
      return;
    service.AddPage(LocalizationHolder.rm.GetString("Client.Core_547"), (IPropertyPage) this);
  }

  /// <summary>Загрузить текущие настройки в контролы</summary>
  public virtual void LoadSettings()
  {
    RecentObjectsSettings recentObjectsSettings = ((IRecentObjectsClientService) ServicesManager.GetService(typeof (IRecentObjectsClientService))).GetCurrentUserRecentObjectsSettings();
    this.edCount.Value = (Decimal) recentObjectsSettings.RecentObjectsMaxCount;
    this.cbActions.SetItemChecked(0, recentObjectsSettings.AllowableRecentObjectActions.HasFlag((Enum) RecentObjectAction.Create));
    this.cbActions.SetItemChecked(1, recentObjectsSettings.AllowableRecentObjectActions.HasFlag((Enum) RecentObjectAction.CheckOut));
    this.cbActions.SetItemChecked(2, recentObjectsSettings.AllowableRecentObjectActions.HasFlag((Enum) RecentObjectAction.CheckIn));
    this.cbActions.SetItemChecked(3, recentObjectsSettings.AllowableRecentObjectActions.HasFlag((Enum) RecentObjectAction.CancelChanges));
    this.cbActions.SetItemChecked(4, recentObjectsSettings.AllowableRecentObjectActions.HasFlag((Enum) RecentObjectAction.SaveChanges));
    this.cbActions.SetItemChecked(5, recentObjectsSettings.AllowableRecentObjectActions.HasFlag((Enum) RecentObjectAction.OpenInNewWindow));
    this.cbActions.SetItemChecked(6, recentObjectsSettings.AllowableRecentObjectActions.HasFlag((Enum) RecentObjectAction.Open));
    this.cbActions.SetItemChecked(7, recentObjectsSettings.AllowableRecentObjectActions.HasFlag((Enum) RecentObjectAction.Edit));
    this.cbActions.SetItemChecked(8, recentObjectsSettings.AllowableRecentObjectActions.HasFlag((Enum) RecentObjectAction.View));
    this.cbActions.SetItemChecked(9, recentObjectsSettings.AllowableRecentObjectActions.HasFlag((Enum) RecentObjectAction.Print));
  }

  /// <summary>Внести изменения в текущие настройки</summary>
  public virtual void SaveSettings()
  {
    int int32 = Convert.ToInt32(this.edCount.Value);
    RecentObjectAction allowableRecentObjectActions = RecentObjectAction.None;
    if (this.cbActions.GetItemChecked(0))
      allowableRecentObjectActions |= RecentObjectAction.Create;
    if (this.cbActions.GetItemChecked(1))
      allowableRecentObjectActions |= RecentObjectAction.CheckOut;
    if (this.cbActions.GetItemChecked(2))
      allowableRecentObjectActions |= RecentObjectAction.CheckIn;
    if (this.cbActions.GetItemChecked(3))
      allowableRecentObjectActions |= RecentObjectAction.CancelChanges;
    if (this.cbActions.GetItemChecked(4))
      allowableRecentObjectActions |= RecentObjectAction.SaveChanges;
    if (this.cbActions.GetItemChecked(5))
      allowableRecentObjectActions |= RecentObjectAction.OpenInNewWindow;
    if (this.cbActions.GetItemChecked(6))
      allowableRecentObjectActions |= RecentObjectAction.Open;
    if (this.cbActions.GetItemChecked(7))
      allowableRecentObjectActions |= RecentObjectAction.Edit;
    if (this.cbActions.GetItemChecked(8))
      allowableRecentObjectActions |= RecentObjectAction.View;
    if (this.cbActions.GetItemChecked(9))
      allowableRecentObjectActions |= RecentObjectAction.Print;
    ((IRecentObjectsClientService) ServicesManager.GetService(typeof (IRecentObjectsClientService))).SetCurrentUserRecentObjectsSettings(new RecentObjectsSettings(int32, allowableRecentObjectActions));
  }

  /// <summary>вернуть id раздела в хелпе для данной страницы</summary>
  public string HelpTopicID => "814";

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
  public string PageName => LocalizationHolder.rm.GetString("Client.Core_296");

  /// <summary>
  /// Текст заголовка (пустое значение - заголовок не отображается)
  /// </summary>
  public string HeaderText
  {
    [DebuggerStepThrough] get => this.PageName;
  }

  /// <summary>Применить изменения редактора</summary>
  public void Apply() => this.SaveSettings();

  /// <summary>Отменить изменения редактора</summary>
  public void Cancel() => this.LoadSettings();

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

  /// <summary>Изменилось значение в любом из контролов</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoUpdateControls(object sender, EventArgs e)
  {
    this.UpdateControls();
    this.OnChanged();
  }

  private void cbActions_ItemCheck(object sender, ItemCheckEventArgs e)
  {
    this.UpdateControls();
    this.OnChanged();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (RecentObjectsSettingsViewPage));
    this.panelMain = new Panel();
    this.cbActions = new CheckedListBox();
    this.labelActions = new Label();
    this.labelCount = new Label();
    this.edCount = new NumericUpDown();
    this.panelMain.SuspendLayout();
    this.edCount.BeginInit();
    this.SuspendLayout();
    this.panelMain.Controls.Add((System.Windows.Forms.Control) this.cbActions);
    this.panelMain.Controls.Add((System.Windows.Forms.Control) this.labelActions);
    this.panelMain.Controls.Add((System.Windows.Forms.Control) this.labelCount);
    this.panelMain.Controls.Add((System.Windows.Forms.Control) this.edCount);
    componentResourceManager.ApplyResources((object) this.panelMain, "panelMain");
    this.panelMain.Name = "panelMain";
    componentResourceManager.ApplyResources((object) this.cbActions, "cbActions");
    this.cbActions.FormattingEnabled = true;
    this.cbActions.Items.AddRange(new object[10]
    {
      (object) componentResourceManager.GetString("cbActions.Items0"),
      (object) componentResourceManager.GetString("cbActions.Items1"),
      (object) componentResourceManager.GetString("cbActions.Items2"),
      (object) componentResourceManager.GetString("cbActions.Items3"),
      (object) componentResourceManager.GetString("cbActions.Items4"),
      (object) componentResourceManager.GetString("cbActions.Items5"),
      (object) componentResourceManager.GetString("cbActions.Items6"),
      (object) componentResourceManager.GetString("cbActions.Items7"),
      (object) componentResourceManager.GetString("cbActions.Items8"),
      (object) componentResourceManager.GetString("cbActions.Items9")
    });
    this.cbActions.Name = "cbActions";
    this.cbActions.ItemCheck += new ItemCheckEventHandler(this.cbActions_ItemCheck);
    componentResourceManager.ApplyResources((object) this.labelActions, "labelActions");
    this.labelActions.Name = "labelActions";
    componentResourceManager.ApplyResources((object) this.labelCount, "labelCount");
    this.labelCount.Name = "labelCount";
    componentResourceManager.ApplyResources((object) this.edCount, "edCount");
    this.edCount.Minimum = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this.edCount.Name = "edCount";
    this.edCount.Value = new Decimal(new int[4]
    {
      25,
      0,
      0,
      0
    });
    this.edCount.ValueChanged += new EventHandler(this.DoUpdateControls);
    this.AutoScaleMode = AutoScaleMode.Inherit;
    this.Controls.Add((System.Windows.Forms.Control) this.panelMain);
    this.MinimumSize = new Size(450, 250);
    this.Name = nameof (RecentObjectsSettingsViewPage);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.panelMain.ResumeLayout(false);
    this.edCount.EndInit();
    this.ResumeLayout(false);
  }
}
