
// Type: Intermech.Navigator.DBObjects.RelationPropertiesView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Commands;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace Intermech.Navigator.DBObjects;

/// <summary>Закладка "Свойства связи"</summary>
[ViewDescriptionProvider(typeof (RelationPropertiesView.RelationPropertiesViewDescriptionProvider))]
public class RelationPropertiesView : PropertiesView
{
  /// <summary>Коллекция именованных значков</summary>
  protected static INamedImageList _namedImageList;
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  /// <summary>Создать закладку</summary>
  public RelationPropertiesView() => this.InitializeComponent();

  protected override void GetDataFromNodeId()
  {
    IDBRelationID data = (IDBRelationID) this._parentNode.GetData(this._nodeID, typeof (IDBRelationID));
    this._objID = 0L;
    this._objTypeID = -1;
    this._prjLinkID = data == null ? -1L : data.Value;
  }

  /// <summary>Инициализировать ресурсы закладки</summary>
  protected override void InitResources() => base.InitResources();

  /// <summary>
  /// Загружает изображение до вызова конструктора экземпляра закладки, для метода DoGetViewDescription
  /// Иначе иконка не отображается
  /// </summary>
  internal static void LoadImage()
  {
    if (RelationPropertiesView._namedImageList != null)
      return;
    RelationPropertiesView._namedImageList = ServicesManager.GetService(typeof (INamedImageList)) as INamedImageList;
    using (MemoryStream memoryStream = ClientCoreResourcesAccess.LoadResurce(ClientCoreResourcesAccess.nameSpace + "Relation.ico"))
    {
      using (Icon icon = new Icon((Stream) memoryStream))
        RelationPropertiesView._namedImageList.Add(icon, "imgRelation");
    }
  }

  /// <summary>Освободить ресурсы закладки</summary>
  protected override void ReleaseResources() => base.ReleaseResources();

  /// <summary>
  /// Возвращает название закладки, которое будет отображаться на экране. Навигатор
  /// получает значение этого свойства после того, как закладка будет проинициализирована
  /// в методе Initialize.
  /// </summary>
  public override string Caption => RelationPropertiesView.GetCaption(this._services);

  public static string GetCaption(System.IServiceProvider serviceProvider)
  {
    NavigatorViewOptions service = serviceProvider != null ? serviceProvider.GetService(typeof (NavigatorViewOptions)) as NavigatorViewOptions : (NavigatorViewOptions) null;
    return service == null || service.Context == NavigatorViewContext.MainViews ? LocalizationHolder.rm.GetString("Client.Core_312") : LocalizationHolder.rm.GetString("Client.Core_1359");
  }

  /// <summary>
  /// Возвращает индекс иконки, которая будет отображаться на экране,
  /// в именованном списке иконок. Навигатор получает значение этого свойства после того,
  /// как закладка будет проинициализирована в методе Initialize.
  /// </summary>
  public override int ImageIndex
  {
    get
    {
      if (this._imageIndex < 0)
        this._imageIndex = Holder.NamedImageList.ImageIndex("imgRelation");
      return this._imageIndex;
    }
  }

  /// <summary>
  /// Возвращает индекс расположения закладки среди других закладок
  /// при выводе на экран. Навигатор сортирует отображаемые закладки в
  /// порядке возрастания этого значения. Значение этого свойства
  /// навигатор получает после того, как закладка будет проинициализирована в
  /// методе Initialize.
  /// </summary>
  public override int OrderID
  {
    [DebuggerStepThrough] get => base.OrderID + 1;
  }

  /// <summary>
  /// Событие возникает перед завершением изменений в объекте
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected override void CommandsBeforeCheckIn(object sender, BeforeObjectCommandArgs e)
  {
    if (e.ObjectId == this._projID && this.PropertyGrid.Visible)
      this.SaveData();
    base.CommandsBeforeCheckIn(sender, e);
  }

  /// <summary>Событие от глобальной службы уведомлений</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected override void GlobalNotificationEventFired(object sender, NotificationEventArgs e)
  {
    if (e.EventName == "ApplicationClosing")
    {
      ApplicationClosingEventArgs closingEventArgs = e as ApplicationClosingEventArgs;
      if (!this.PropertyGrid.IsChanged)
        return;
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_313"), LocalizationHolder.rm.GetString("Client.Core_314"), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
      if (num == 6)
        this.SaveData();
      if (num != 2)
        return;
      closingEventArgs.Cancel = true;
    }
    else
      base.GlobalNotificationEventFired(sender, e);
  }

  /// <summary>
  /// Сохранить изменения из редактора свойств в объект (связь) после диалога с пользователем
  /// </summary>
  protected override void SaveIfModified()
  {
    if (!this.PropertyGrid.IsChanged)
      return;
    if (MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_313"), LocalizationHolder.rm.GetString("Client.Core_314"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
      this.SaveData();
    else
      this.LoadData();
  }

  /// <summary>Загрузить информацию в редактор свойств</summary>
  protected override void LoadData()
  {
    Control parent = this.PropertyGrid.Parent;
    try
    {
      this.PropertyGrid.Parent = (Control) null;
      this.PropertyGrid.Load(this._prjLinkID, AttributableElements.Relation, GetAttributeValuesModes.IncludeName | GetAttributeValuesModes.IncludeGroupName | GetAttributeValuesModes.CheckWriteAccess | GetAttributeValuesModes.IncludeDescriptions | GetAttributeValuesModes.CheckVisibility, false, PropertiesView.tabTypes);
      if (this.IsReadOnly)
        this.ForceGridToReadOnly();
    }
    finally
    {
      this.PropertyGrid.Parent = parent;
    }
    this.UpdateControls();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (RelationPropertiesView));
    this.pnButtons.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    componentResourceManager.ApplyResources((object) this.pnButtons, "pnButtons");
    componentResourceManager.ApplyResources((object) this.btApply, "btApply");
    componentResourceManager.ApplyResources((object) this.btCancel, "btCancel");
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Inherit;
    this.Name = nameof (RelationPropertiesView);
    this.pnButtons.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private sealed class RelationPropertiesViewDescriptionProvider : 
    PropertiesView.PropertiesViewDescriptionProvider
  {
    public override ViewDescription DoGetViewDescription(
      ISelectedItems selectedItems,
      System.IServiceProvider serviceProvider)
    {
      ViewDescription viewDescription = base.DoGetViewDescription(selectedItems, serviceProvider);
      viewDescription.Caption = RelationPropertiesView.GetCaption(serviceProvider);
      RelationPropertiesView.LoadImage();
      viewDescription.ImageIndex = Holder.NamedImageList.ImageIndex("imgRelation");
      ++viewDescription.OrderID;
      return viewDescription;
    }
  }
}
