// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.RemarkAttributesView
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using ImSSP;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS;

/// <summary>
/// Закладка "Навигатора" для редактора списка атрибутов, отображаемых в примечаниях
/// спецификаций - "Атрибуты в примечаниях"
/// (назначается для объекта "Общий шаблон спецификаций")
/// </summary>
[ViewDescriptionProvider(typeof (RemarkAttributesView.CustomViewDescriptionProvider))]
public class RemarkAttributesView : UserControl, IView
{
  protected RemarkAttributesForm form;
  /// <summary>Индекс изображения для закладки</summary>
  protected int imageIndex = -1;
  /// <summary>Идентификатор версии объекта, для которого отобразилась закладка</summary>
  protected long settingsObjectID;
  /// <summary>Идентификатор атрибута в котором хранятся настройки</summary>
  protected int settingsAttributeID;
  /// <summary>Загруженные настройки</summary>
  protected NoteFieldSettings noteFieldSettings;
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  /// <summary>Создать закладку</summary>
  public RemarkAttributesView() => this.InitializeComponent();

  /// <summary>
  /// Выполняет инициализацию закладки после ее создания. Реализация
  /// этого метода должна работать быстро, т.е. все длительные операции
  /// желательно выполнять при первом вызове метода Activate.
  /// </summary>
  /// <param name="items">Коллекция выбранных пользователем элементов навигации.</param>
  /// <param name="provider">Контейнер сервисов, которыми может пользоваться закладка.</param>
  void IView.Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    this.settingsObjectID = 0L;
    if (items.Count != 1)
      return;
    IDBTypedObjectID itemData = (IDBTypedObjectID) items.GetItemData(0, typeof (IDBTypedObjectID));
    Guid guid = Guid.Empty;
    if (itemData != null)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        guid = sessionKeeper.Session.GetObjectInfo(itemData.ObjectID).VersionGuid;
    }
    if (guid != new Guid("cad0026f-306c-11d8-b4e9-00304f19f545") && guid != AvsIDCache.StdTemplateElementList)
      return;
    this.settingsObjectID = itemData != null ? itemData.ObjectID : this.settingsObjectID;
    this.settingsAttributeID = AvsIDCache.Attr_NoteFieldSettings;
  }

  /// <summary>
  /// Уведомляет закладку о том, что она стала видима на экране. Этот метод вызывается при
  /// первом показе закладки, а также при переключении на нее с другой закладки.
  /// </summary>
  /// <param name="previousView">
  /// Закладка, с которой осуществляется переключение. Может быть null для самой первой
  /// показываемой на экране закладки.
  /// </param>
  void IView.Activate(IView previousView)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.noteFieldSettings = new NoteFieldSettings();
      this.noteFieldSettings.LoadFromDBObjectAttribute(this.settingsObjectID, this.settingsAttributeID, sessionKeeper.Session);
    }
    RemarkAttributesFormParams formParams = new RemarkAttributesFormParams(this.noteFieldSettings.Items, this.noteFieldSettings.Options);
    if (this.form == null)
    {
      this.form = new RemarkAttributesForm(this.settingsObjectID, this.settingsAttributeID, (AttributesListFormParams) formParams, 1, false);
      this.form.SetParent((Control) this);
      this.form.OnApplyPressed += new RemarkAttributesChangedEventHandler(this.editor_OnApplyPressed);
      this.form.OnCancelPressed += new RemarkAttributesChangedEventHandler(this.editor_OnCancelPressed);
    }
    else
      this.form.Init((AttributesListFormParams) formParams, 1);
  }

  /// <summary>
  /// Уведомляет закладку о том, что она перестала быть видима на экране. Этот метод
  /// вызывается при переключении на другую закладку, а также удалении всех закладок.
  /// </summary>
  /// <param name="nextView">
  /// Закладка, на которую осуществляется переключение. Может быть null, если выполяется
  /// не переключение, а удаление закладок.
  /// </param>
  void IView.Deactivate(IView nextView)
  {
    if (this.form == null || !this.form.IsChanged || this.form.ReadOnly || MessageBox.Show(sc_903.ssp_avs_904(), "Атрибуты в примечаниях спецификаций", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) != DialogResult.Yes)
      return;
    int objectType = -1;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      AttributesListFormParams.CopyTo(this.form.FormResult.Items, this.noteFieldSettings.Items);
      this.noteFieldSettings.Options = (this.form.FormResult as RemarkAttributesFormParams).Options;
      this.noteFieldSettings.SaveToDBObjectAttribute(this.settingsObjectID, this.settingsAttributeID, sessionKeeper.Session);
      objectType = sessionKeeper.Session.GetObjectInfo(this.settingsObjectID).ObjectTypeID;
      this.form.IsChanged = false;
    }
    if (!(ServicesManager.GetService(typeof (INotificationService)) is INotificationService service))
      return;
    service.FireEvent((object) this.form, (NotificationEventArgs) new DBObjectsExtendedEventArgs(this.settingsObjectID, objectType, new AttributeValues(AvsIDCache.Attr_NoteFieldSettings, (object) null), new AttributeValues(AvsIDCache.Attr_NoteFieldSettings, (object) null)));
  }

  /// <summary>
  /// Возвращает название закладки, которое будет отображаться на экране. Навигатор
  /// получает значение этого свойства после того, как закладка будет проинициализирована
  /// в методе Initialize.
  /// </summary>
  string IView.Caption
  {
    [DebuggerStepThrough] get => "Атрибуты в графе Примечание";
  }

  /// <summary>
  /// Возвращает индекс иконки, которая будет отображаться на экране,
  /// в именованном списке иконок. Навигатор получает значение этого свойства после того,
  /// как закладка будет проинициализирована в методе Initialize.
  /// </summary>
  int IView.ImageIndex
  {
    [DebuggerStepThrough] get
    {
      if (this.imageIndex < 0)
        this.imageIndex = (ServicesManager.GetService(typeof (INamedImageList)) as INamedImageList).ImageIndex("");
      return this.imageIndex;
    }
  }

  /// <summary>
  /// Возвращает индекс расположения закладки среди других закладок
  /// при выводе на экран. Навигатор сортирует отображаемые закладки в
  /// порядке возрастания этого значения. Значение этого свойства
  /// навигатор получает после того, как закладка будет проинициализирована в
  /// методе Initialize.
  /// </summary>
  int IView.OrderID
  {
    [DebuggerStepThrough] get => 16 /*0x10*/;
  }

  /// <summary>Нажата кнопка "ОК"/"Применить"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void editor_OnApplyPressed(object sender, RemarkAttributesEventArgs e)
  {
    if (this.form.ReadOnly)
    {
      int num = (int) MessageBox.Show(sc_903.ssp_avs_905(), "Информация", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }
    else
    {
      int objectType = -1;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        AttributesListFormParams.CopyTo(e.FormParams.Items, this.noteFieldSettings.Items);
        this.noteFieldSettings.Options = (e.FormParams as RemarkAttributesFormParams).Options;
        this.noteFieldSettings.SaveToDBObjectAttribute(this.settingsObjectID, this.settingsAttributeID, sessionKeeper.Session);
        objectType = sessionKeeper.Session.GetObjectInfo(this.settingsObjectID).ObjectTypeID;
        this.form.IsChanged = false;
      }
      if (!(ServicesManager.GetService(typeof (INotificationService)) is INotificationService service))
        return;
      service.FireEvent((object) this.form, (NotificationEventArgs) new DBObjectsExtendedEventArgs(this.settingsObjectID, objectType, new AttributeValues(AvsIDCache.Attr_NoteFieldSettings, (object) null), new AttributeValues(AvsIDCache.Attr_NoteFieldSettings, (object) null)));
    }
  }

  /// <summary>Нажата кнопка "Отмена"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void editor_OnCancelPressed(object sender, RemarkAttributesEventArgs e)
  {
    if (this.form.ReadOnly || MessageBox.Show(sc_903.ssp_avs_906(), "Отмена изменений", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.noteFieldSettings.LoadFromDBObjectAttribute(this.settingsObjectID, this.settingsAttributeID, sessionKeeper.Session);
    this.form.Init((AttributesListFormParams) new RemarkAttributesFormParams(this.noteFieldSettings.Items, this.noteFieldSettings.Options), 1);
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
    this.SuspendLayout();
    this.AutoScaleMode = AutoScaleMode.Inherit;
    this.MinimumSize = new Size(200, 100);
    this.Name = nameof (RemarkAttributesView);
    this.Size = new Size(200, 100);
    this.ResumeLayout(false);
  }

  private sealed class CustomViewDescriptionProvider : BaseViewDescriptionProvider
  {
    public override ViewDescription DoGetViewDescription(
      ISelectedItems selectedItems,
      System.IServiceProvider serviceProvider)
    {
      return new ViewDescription()
      {
        Caption = "Атрибуты в примечаниях",
        ImageIndex = -1,
        OrderID = 16 /*0x10*/
      };
    }
  }
}
