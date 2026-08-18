
// Type: Intermech.Navigator.SelectionView.SelectionView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Selections.Implementation;
using Intermech.Navigator.Views;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.Navigator.SelectionView;

/// <summary>Summary description for SelectionView.</summary>
[ViewDescriptionProvider(typeof (Intermech.Navigator.SelectionView.SelectionView.SelectionViewDescriptionProvider))]
public class SelectionView : UserControl, INavigatorView, IView
{
  /// <summary>Загружен ли объект</summary>
  private bool _loaded;
  /// <summary>Форма, которая размещается на данной вьюшке</summary>
  private SelectionForm _editorForm;
  private System.IServiceProvider _services;
  private long _objectID;
  private List<long> _objIDList;
  private int imageIndex = -1;
  private bool _inited;
  /// <summary>Название вьюшки - "Условия выборки"</summary>
  public static string SelectionViewCaption = LocalizationHolder.rm.GetString("Client.Core_702");
  private IContainer components;
  private bool _gotopreview;

  /// <summary>Конструктор</summary>
  public SelectionView() => this.InitializeComponent();

  public void Initialize(ISelectedItems items, System.IServiceProvider services)
  {
    if (!this._inited)
    {
      this._services = services;
      if (this._services != null)
        (ServicesManager.GetService(typeof (INotificationService)) as INotificationService).Subscribe("ObjectsChanged", new NotificationEventHandler(this.OnSelectionsChanged));
      this._inited = true;
    }
    long num = (items.GetItemData(0, typeof (IDBObjectID)) as IDBObjectID).Value;
    if (this._objectID == num)
      return;
    this._objectID = num;
    if (items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID && services.GetService(typeof (NavigatorTreeView)) is NavigatorTreeView service && service.SelectedNodes.Length == 1)
    {
      this._objIDList = new List<long>();
      for (NavigatorTreeNode navigatorTreeNode1 = service.SelectedNodes[0]; navigatorTreeNode1 != null && navigatorTreeNode1.InTree; navigatorTreeNode1 = navigatorTreeNode1.Parent)
      {
        NavigatorTreeNode navigatorTreeNode2 = navigatorTreeNode1;
        if (navigatorTreeNode2.NodeID is SelectionNodeID)
          this._objIDList.Add((navigatorTreeNode2.NodeID as SelectionNodeID).ObjectID);
        else
          break;
      }
    }
    this._loaded = false;
  }

  public void Activate(IView previousView)
  {
    if (!this._loaded)
    {
      if (this._editorForm == null)
      {
        this._editorForm = new SelectionForm();
        this._editorForm.SetParent((Control) this);
        IViewState service = this._services.GetService(typeof (IViewState)) as IViewState;
        this._editorForm.ParentMode = service == null || (service.ViewState & ViewStateFlags.InDialog) <= ViewStateFlags.None ? SelectionFormMode.InView : SelectionFormMode.IndependentForm;
        this._editorForm.SetGoEnable(this.GetChildrenView() != null);
        if (service != null)
          this._editorForm.ReadOnly = (service.ViewState & ViewStateFlags.InParametersCard) > ViewStateFlags.None && (service.ViewState & ViewStateFlags.ReadOnly) > ViewStateFlags.None;
      }
      this._editorForm.SelectionLoad(this._objectID, this._objIDList);
      this._loaded = true;
    }
    this._gotopreview = previousView is ThumbnailDocs;
  }

  private void OnSelectionsChanged(object sender, NotificationEventArgs e)
  {
    if (this._editorForm == null || !(e.EventName == "ObjectsChanged") || !(e is DBObjectsEventArgs) || sender == this._editorForm)
      return;
    DBObjectsEventArgs objectsEventArgs = (DBObjectsEventArgs) e;
    if (objectsEventArgs.ObjectIDs == null || objectsEventArgs.ObjectIDs.Count <= 0)
      return;
    for (int index = 0; index < objectsEventArgs.ObjectIDs.Count; ++index)
    {
      if (this._objectID == objectsEventArgs.ObjectIDs[index])
      {
        this._editorForm.SelectionLoad(this._objectID, this._objIDList);
        break;
      }
    }
  }

  internal void GoClick()
  {
    IViewPage viewPage = this._gotopreview ? this.GetChildrenView("DocumentsThumbnailView") : this.GetChildrenView();
    if (viewPage.View == null)
      return;
    this._services.GetService<IViewsManager>().ActiveViewPage = viewPage;
  }

  public void Deactivate(IView nextView)
  {
    if (this._editorForm == null)
      return;
    if (this._editorForm.IsModified)
    {
      if (MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_416"), LocalizationHolder.rm.GetString("Client.Core_417"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
      {
        this._editorForm.SelectionSave();
        if (nextView != null)
          this.RefreshDependedViews();
      }
      this._editorForm.IsModified = false;
    }
    else
    {
      if (!this._editorForm.IsNeedRefresh)
        return;
      if (nextView != null)
        this.RefreshDependedViews();
      this._editorForm.IsNeedRefresh = false;
    }
  }

  private void RefreshDependedViews()
  {
    IViewPage childrenView1 = this.GetChildrenView();
    if (childrenView1 != null && childrenView1.View != null && childrenView1.View is IViewData)
      (childrenView1.View as IViewData).Refresh();
    IViewPage childrenView2 = this.GetChildrenView("DocumentsThumbnailView");
    if (childrenView2 == null || childrenView2.View == null || !(childrenView2.View is ThumbnailDocs))
      return;
    (childrenView2.View as ThumbnailDocs).needRefresh = true;
  }

  private IViewPage GetChildrenView(string viewName = "ChildrenView")
  {
    IViewsManager service = (IViewsManager) this._services.GetService(typeof (IViewsManager));
    if (service != null)
    {
      for (int index = 0; index < service.ViewPages.Count; ++index)
      {
        if (service.ViewPages[index].Name == viewName)
          return service.ViewPages[index];
      }
    }
    return (IViewPage) null;
  }

  public string Caption => Intermech.Navigator.SelectionView.SelectionView.SelectionViewCaption;

  public int OrderID => 30;

  public int ImageIndex
  {
    get
    {
      if (this.imageIndex < 0)
        this.imageIndex = Holder.NamedImageList.ImageIndex("imgPropPage");
      return this.imageIndex;
    }
  }

  /// <summary>Clean up any resources being used.</summary>
  protected override void Dispose(bool disposing)
  {
    this._objectID = 0L;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Intermech.Navigator.SelectionView.SelectionView));
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Name = nameof (SelectionView);
    this.ResumeLayout(false);
  }

  private sealed class SelectionViewDescriptionProvider : BaseViewDescriptionProvider
  {
    public override ViewDescription DoGetViewDescription(
      ISelectedItems selectedItems,
      System.IServiceProvider serviceProvider)
    {
      return new ViewDescription()
      {
        Caption = LocalizationHolder.rm.GetString("Client.Core_702"),
        ImageIndex = Holder.NamedImageList.ImageIndex("imgPropPage"),
        OrderID = 30
      };
    }
  }
}
