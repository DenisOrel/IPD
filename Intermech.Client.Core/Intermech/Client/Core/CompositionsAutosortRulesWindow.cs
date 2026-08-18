
// Type: Intermech.Client.Core.CompositionsAutosortRulesWindow
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Docking;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;


namespace Intermech.Client.Core;

/// <summary>
/// Закладка "Редактор правил автоматической сортировки и отображения составов"
/// </summary>
[CustomDescription("Attribute.Client.Core_142")]
public class CompositionsAutosortRulesWindow : DockControl
{
  /// <summary>Заголовок окна</summary>
  public static readonly string AutosortName = LocalizationHolder.rm.GetString("Client.Core_569");
  /// <summary>Название изображения</summary>
  public const string AutosortImageName = "imgObject";
  /// <summary>Название окна</summary>
  public const string AutosortWindowName = "desktopAutosortWindow";
  /// <summary>Guid, характеризующий экземпляры данного класса</summary>
  private static readonly Guid _persistStateGuid = new Guid("{973404B8-F4F4-4F7B-A0AB-D1CDF0DD02EF}");
  /// <summary>Выполнена ли активация окна</summary>
  protected bool _activated;
  /// <summary>Выполнена ли загрузка данных в окно</summary>
  protected bool _loaded;
  /// <summary>Сервис службы уведомлений</summary>
  protected Intermech.Client.Core.NotificationService _notificationService;
  /// <summary>Сервис именованных изображений</summary>
  private INamedImageList _images;
  /// <summary>Контейнер сервисов</summary>
  protected AdvancedServiceContainer _services;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panelBottom;
  private AutosortRulesEditor editor;
  private Button btnApply;
  private Button btnCancel;

  /// <summary>Конструктор</summary>
  public CompositionsAutosortRulesWindow()
  {
    this.InitializeComponent();
    this.Guid = CompositionsAutosortRulesWindow._persistStateGuid;
    if (!this.DesignMode)
      this.InitializeServices();
    this.Text = CompositionsAutosortRulesWindow.AutosortName;
    int num = this._images != null ? this._images.ImageIndex("imgObject") : -1;
    this.ShowImageInDocumentTab = num >= 0;
    this.TabImageIndex = num;
    this._activated = false;
    HelpProvidersClass.SetHelpOptionForControl((Control) this, this.HelpID);
  }

  /// <summary>Инициализировать службу уведомлений</summary>
  /// <returns></returns>
  protected virtual Intermech.Client.Core.NotificationService InitializeNotificationService()
  {
    SwitchedNotificationService notificationService = new SwitchedNotificationService();
    notificationService.Parent = ServicesManager.GetService(typeof (INotificationService)) as Intermech.Client.Core.NotificationService;
    return (Intermech.Client.Core.NotificationService) notificationService;
  }

  /// <summary>Освободить ресурсы службы уведомлений</summary>
  /// <param name="notificationService"></param>
  protected virtual void DisposeNotificationService(INotificationService notificationService)
  {
    ((IDisposable) notificationService).Dispose();
  }

  /// <summary>Управление службой уведомлений</summary>
  /// <param name="notificationService">Управляемая служба уведомлений</param>
  /// <param name="enabled">Разрешить или отключить службу уведомлений</param>
  protected virtual void EnableNotifications(INotificationService notificationService, bool enabled)
  {
    if (!(notificationService is SwitchedNotificationService notificationService1))
      return;
    notificationService1.Enabled = enabled;
  }

  /// <summary>Служба уведомлений</summary>
  protected INotificationService NotificationService
  {
    get => (INotificationService) this._notificationService;
  }

  /// <summary>Контрол активирован</summary>
  public override void Activated()
  {
    base.Activated();
    if (!this._activated)
      this.EnableNotifications(this.NotificationService, this.IsOpen | UISettings.AutoupdateNonActiveWindows);
    if (!this._loaded)
    {
      this.editor.RulesEditorAccessRights = (ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole).IsAdmin ? RulesEditorAccessRights.FullAccess : RulesEditorAccessRights.ReadOnly;
      this.editor.Init();
      this._loaded = true;
    }
    this.UpdateControls();
    this._activated = true;
  }

  /// <summary>Контрол деактивирован</summary>
  public override void Deactivated()
  {
    base.Deactivated();
    if (this._activated)
      this.EnableNotifications(this.NotificationService, this.IsOpen | UISettings.AutoupdateNonActiveWindows);
    this.UpdateControls();
    this._activated = false;
  }

  /// <summary>Инициализировать сервисы</summary>
  private void InitializeServices()
  {
    this._notificationService = this.InitializeNotificationService();
    this._services = new AdvancedServiceContainer();
    this._images = ServicesManager.GetService(typeof (INamedImageList)) as INamedImageList;
    if (this._notificationService == null)
      return;
    this._services.AddService(typeof (INotificationService), (object) this._notificationService);
  }

  /// <summary>Деинициализировать сервисы</summary>
  private void DisposeServices()
  {
    this._services.RemoveService(typeof (INotificationService));
    this._notificationService = (Intermech.Client.Core.NotificationService) null;
    this._services = (AdvancedServiceContainer) null;
    this._images = (INamedImageList) null;
  }

  /// <summary>Обновить статус контролов</summary>
  public virtual void UpdateControls()
  {
    this.btnApply.Enabled = this.editor.IsChanged & this.editor.RulesEditorAccessRights != 0;
    this.btnCancel.Enabled = this.btnApply.Enabled;
  }

  /// <summary>В коллекции правил есть изменения</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void editor_OnChanged(object sender, EventArgs e) => this.UpdateControls();

  /// <summary>Нажата кнопка "Применить"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoApply(object sender, EventArgs e)
  {
    this.editor.ApplyChanges();
    this.UpdateControls();
  }

  /// <summary>Нажата кнопка "Отменить"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoCancel(object sender, EventArgs e)
  {
    if (!this.editor.IsChanged || MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_570"), LocalizationHolder.rm.GetString("Client.Core_132"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
      return;
    this.editor.CancelChanges();
    this.UpdateControls();
  }

  /// <summary>Закрывается контрол</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void CompositionsAutosortRulesWindow_Closing(object sender, CancelEventArgs e)
  {
    if (!this.editor.IsChanged || MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_571"), LocalizationHolder.rm.GetString("Client.Core_132"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
      return;
    this.DoApply(sender, (EventArgs) e);
  }

  /// <summary>
  /// Возвращает строку состояния окна, которая может быть использована для восстановления окна в
  /// следующем сеансе работы приложения.
  /// </summary>
  /// <returns>Строка состояния окна навигатора.</returns>
  protected override string GetPersistString()
  {
    try
    {
      XmlDocument state = this.GetState();
      using (TextWriter w1 = (TextWriter) new StringWriter())
      {
        XmlWriter w2 = (XmlWriter) new XmlTextWriter(w1);
        state.WriteTo(w2);
        w2.Flush();
        w2.Close();
        return w1.ToString();
      }
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show(ex.Message, LocalizationHolder.rm.GetString("Client.Core_572"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
      return (string) null;
    }
  }

  /// <summary>Возвращает состояние окна в виде XML документа.</summary>
  /// <returns>Состояние окна в виде XML</returns>
  protected virtual XmlDocument GetState()
  {
    XmlDocument state = new XmlDocument();
    XmlNode element = (XmlNode) state.CreateElement(nameof (CompositionsAutosortRulesWindow));
    state.AppendChild((XmlNode) state.CreateXmlDeclaration("1.0", (string) null, (string) null));
    state.AppendChild(element);
    return state;
  }

  /// <summary>Требуется восстановление окна</summary>
  /// <param name="guid">Guid окна</param>
  /// <param name="persistString">Строка с сохранённым состоянием окна</param>
  /// <returns>Элемент управления с окном</returns>
  public static DockControl RestoreWindowCallback(Guid guid, string persistString)
  {
    if (guid != CompositionsAutosortRulesWindow._persistStateGuid)
      return (DockControl) null;
    try
    {
      return !(ServicesManager.GetService(typeof (ICurrentUserAndRole)) is ICurrentUserAndRole service) || !service.IsAdmin ? (DockControl) null : (DockControl) new CompositionsAutosortRulesWindow();
    }
    catch (Exception ex)
    {
      IOutputView service = ServicesManager.GetService(typeof (IOutputView)) as IOutputView;
      service.WriteString("Navigator", LocalizationHolder.rm.GetString("Client.Core_326"));
      service.WriteString("Navigator", ex.Message);
      return (DockControl) null;
    }
  }

  /// <summary>вернуть раздел справки для контрола</summary>
  public override string HelpID => "1069";

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && Holder.WellKnownNavigators != null)
      Holder.WellKnownNavigators.Unregister((Control) this);
    if (disposing && this.components != null)
    {
      if (Holder.WellKnownNavigators != null)
        Holder.WellKnownNavigators.Unregister((Control) this);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (CompositionsAutosortRulesWindow));
    this.panelBottom = new Panel();
    this.btnCancel = new Button();
    this.btnApply = new Button();
    this.editor = new AutosortRulesEditor();
    this.panelBottom.SuspendLayout();
    this.SuspendLayout();
    this.panelBottom.Controls.Add((Control) this.btnCancel);
    this.panelBottom.Controls.Add((Control) this.btnApply);
    componentResourceManager.ApplyResources((object) this.panelBottom, "panelBottom");
    this.panelBottom.Name = "panelBottom";
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.btnCancel.Click += new EventHandler(this.DoCancel);
    componentResourceManager.ApplyResources((object) this.btnApply, "btnApply");
    this.btnApply.Name = "btnApply";
    this.btnApply.UseVisualStyleBackColor = true;
    this.btnApply.Click += new EventHandler(this.DoApply);
    this.editor.RulesEditorAccessRights = RulesEditorAccessRights.ReadOnly;
    componentResourceManager.ApplyResources((object) this.editor, "editor");
    this.editor.MinimumSize = new Size(500, 200);
    this.editor.Name = "editor";
    this.editor.Tag = (object) "  ";
    this.editor.Changed += new EventHandler(this.editor_OnChanged);
    this.AutoScaleMode = AutoScaleMode.Inherit;
    this.Controls.Add((Control) this.editor);
    this.Controls.Add((Control) this.panelBottom);
    this.Name = nameof (CompositionsAutosortRulesWindow);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Closing += new CancelEventHandler(this.CompositionsAutosortRulesWindow_Closing);
    this.panelBottom.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
