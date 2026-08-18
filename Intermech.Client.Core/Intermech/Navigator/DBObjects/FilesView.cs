
// Type: Intermech.Navigator.DBObjects.FilesView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.PropertyEditors;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.Navigator.DBObjects;

/// <summary>Summary description for ObjectFilesView.</summary>
[ViewDescriptionProvider(typeof (FilesView.FilesViewDescriptionProvider))]
public class FilesView : UserControl, IView
{
  private int _imageIndex;
  private long _objID;
  private bool _firstEnter;
  private System.ComponentModel.Container components;
  private FileAttributeEditForm faed;
  private INotificationService _ns;

  public FilesView()
  {
    this.InitializeComponent();
    this._imageIndex = -1;
    this._ns = ServicesManager.GetService(typeof (INotificationService)) as INotificationService;
    if (this._ns == null)
      return;
    this._ns.Subscribe("ObjectsCheckedIn", new NotificationEventHandler(this.NotifyProcessing));
    this._ns.Subscribe("ObjectsCheckedOut", new NotificationEventHandler(this.NotifyProcessing));
    this._ns.Subscribe("ObjectsChangesCancelled", new NotificationEventHandler(this.NotifyProcessing));
  }

  /// <summary>
  /// Обработка чекина/чекаута объекта. Необходима при использовании на форме карточки кнопок взятия/завершения редактирования.
  /// </summary>
  /// <param name="sender">The sender.</param>
  /// <param name="e">The <see cref="T:Intermech.Interfaces.Client.NotificationEventArgs" /> instance containing the event data.</param>
  private void NotifyProcessing(object sender, NotificationEventArgs e)
  {
    if (!(e is DBObjectsEventArgs objectsEventArgs) || !objectsEventArgs.ObjectIDs.Contains(this._objID) && !objectsEventArgs.ObjectIDs.Contains(-this._objID))
      return;
    if (e.EventName == "ObjectsCheckedIn")
    {
      if (this._objID >= 0L)
        return;
      this._objID = -this._objID;
    }
    else if (e.EventName == "ObjectsCheckedOut")
    {
      if (this._objID <= 0L)
        return;
      this._objID = -this._objID;
    }
    else
    {
      if (!(e.EventName == "ObjectsChangesCancelled") || this._objID >= 0L)
        return;
      this._objID = -this._objID;
    }
  }

  /// <summary>Clean up any resources being used.</summary>
  protected override void Dispose(bool disposing)
  {
    this._ns = ServicesManager.GetService(typeof (INotificationService)) as INotificationService;
    if (this._ns != null)
    {
      this._ns.Unsubscribe("ObjectsCheckedIn", new NotificationEventHandler(this.NotifyProcessing));
      this._ns.Unsubscribe("ObjectsCheckedOut", new NotificationEventHandler(this.NotifyProcessing));
      this._ns.Unsubscribe("ObjectsChangesCancelled", new NotificationEventHandler(this.NotifyProcessing));
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FilesView));
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Name = nameof (FilesView);
    this.ResumeLayout(false);
  }

  public void Initialize(ISelectedItems items, System.IServiceProvider services)
  {
    this._objID = (items.GetItemData(0, typeof (IDBObjectID)) as IDBObjectID).Value;
    this._firstEnter = true;
  }

  public void Activate(IView previousView)
  {
    if (!this._firstEnter)
      return;
    if (this.faed == null)
    {
      this.faed = new FileAttributeEditForm();
      this.faed.SetParent((Control) this);
    }
    if (!this.faed.Loaded || this.faed.Loaded && this.faed.Id != this._objID)
      this.faed.LoadElement(this._objID, AttributableElements.Object, true, true, true);
    this._firstEnter = false;
  }

  public void Deactivate(IView nextView)
  {
  }

  public string Caption => LocalizationHolder.rm.GetString("Client.Core_297");

  public int OrderID => 50;

  public int ImageIndex
  {
    get
    {
      if (this._imageIndex < 0)
        this._imageIndex = Holder.NamedImageList.ImageIndex("imgFilesList");
      return this._imageIndex;
    }
  }

  private sealed class FilesViewDescriptionProvider : BaseViewDescriptionProvider
  {
    public override ViewDescription DoGetViewDescription(
      ISelectedItems selectedItems,
      System.IServiceProvider serviceProvider)
    {
      return new ViewDescription()
      {
        Caption = LocalizationHolder.rm.GetString("Client.Core_297"),
        ImageIndex = Holder.NamedImageList.ImageIndex("imgFilesList"),
        OrderID = 50
      };
    }
  }
}
