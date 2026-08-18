
// Type: Intermech.Navigator.SelectionView.SelectionPropertiesView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Selections;
using Intermech.Navigator.Views;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.Navigator.SelectionView;

[ViewDescriptionProvider(typeof (SelectionPropertiesView.SelectionPropertiesViewDescriptionProvider))]
public class SelectionPropertiesView : UserControl, IView
{
  private bool _activate;
  private long _objectID;
  private int _objectTypeID;
  private System.IServiceProvider _services;
  private SelectionDialog sForm;
  /// <summary>иконка для вьюшки</summary>
  private int imageIndex = -1;
  private INotificationService notificationService;
  protected NotificationEventHandler notifyHandler;
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  public SelectionPropertiesView() => this.InitializeComponent();

  public void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    this._services = provider;
    this._objectID = (items.GetItemData(0, typeof (IDBObjectID)) as IDBObjectID).Value;
    this._objectTypeID = (items.GetItemData(0, typeof (IDBObjectTypeID)) as IDBObjectTypeID).Value;
    this._activate = false;
  }

  public void Activate(IView previousView)
  {
    if (this.sForm == null)
    {
      this.sForm = new SelectionDialog();
      this.sForm.SetParent((Control) this, true);
    }
    if (this._activate)
      return;
    this.sForm.SelectionLoad(this._objectID, this._objectTypeID);
    if (this._services != null)
    {
      this.notificationService = ServicesManager.GetService(typeof (INotificationService)) as INotificationService;
      this.notifyHandler = new NotificationEventHandler(this.ReloadData);
      this.notificationService.Subscribe("ObjectsChanged", this.notifyHandler);
    }
    this._activate = true;
  }

  public void Deactivate(IView nextView)
  {
    if (this.sForm == null)
      return;
    this._services.GetService(typeof (IViewState));
    if (this.sForm.IsModified)
    {
      if (MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_315"), LocalizationHolder.rm.GetString("Client.Core_316"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        this.sForm.SelectionSave(false);
      else
        this._activate = false;
      this.sForm.IsModified = false;
    }
    if (this.notifyHandler == null)
      return;
    this.notificationService.Unsubscribe("ObjectsChanged", this.notifyHandler);
  }

  public void ReloadData(object sender, NotificationEventArgs e)
  {
    if (!(e is DBObjectsEventArgs objectsEventArgs) || objectsEventArgs.ObjectIDs[0] != this._objectID)
      return;
    this.sForm.SelectionLoad(this._objectID, this._objectTypeID);
  }

  public string Caption
  {
    get
    {
      return !MetaDataHelper.IsObjectTypeChildOf(this._objectTypeID, MetaDataHelper.GetObjectTypeID("cad00156-306c-11d8-b4e9-00304f19f545")) ? LocalizationHolder.rm.GetString("Client.Core_1520") : LocalizationHolder.rm.GetString("Client.Core_1215");
    }
  }

  public int ImageIndex
  {
    get
    {
      if (this.imageIndex < 0)
        this.imageIndex = Holder.NamedImageList.ImageIndex("imgCard");
      return this.imageIndex;
    }
  }

  public int OrderID => 1;

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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SelectionPropertiesView));
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Name = nameof (SelectionPropertiesView);
    this.ResumeLayout(false);
  }

  private sealed class SelectionPropertiesViewDescriptionProvider : BaseViewDescriptionProvider
  {
    public override ViewDescription DoGetViewDescription(
      ISelectedItems selectedItems,
      System.IServiceProvider serviceProvider)
    {
      bool flag = MetaDataHelper.IsObjectTypeChildOf((selectedItems.GetItemData(0, typeof (IDBObjectTypeID)) as IDBObjectTypeID).Value, MetaDataHelper.GetObjectTypeID("cad00156-306c-11d8-b4e9-00304f19f545"));
      return new ViewDescription()
      {
        Caption = flag ? LocalizationHolder.rm.GetString("Client.Core_1215") : LocalizationHolder.rm.GetString("Client.Core_1520"),
        ImageIndex = Holder.NamedImageList.ImageIndex("imgCard"),
        OrderID = 1
      };
    }
  }
}
