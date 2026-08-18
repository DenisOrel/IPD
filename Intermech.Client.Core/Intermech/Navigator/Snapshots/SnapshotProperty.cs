
// Type: Intermech.Navigator.Snapshots.SnapshotProperty
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.Navigator.Snapshots;

public class SnapshotProperty : UserControl, IView
{
  /// <summary>id итерации</summary>
  private long snapshotID;
  /// <summary>id версии объекта</summary>
  private long objectID;
  /// <summary>индекс иконки закладки</summary>
  private int imageIndex = -1;
  /// <summary>описание итерации, св-ва которой показываем</summary>
  private SnapshotsNodeID nodeID;
  private bool loaded;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private PropertyGrid shanpshotProperty;

  public SnapshotProperty() => this.InitializeComponent();

  /// <summary>Порядковый номер закладки</summary>
  public int OrderID => 0;

  /// <summary>заголовок закладки</summary>
  public string Caption => LocalizationHolder.rm.GetString("Client.Core_1408");

  /// <summary>индекс иконки закладки</summary>
  public int ImageIndex
  {
    get
    {
      if (this.imageIndex < 0)
        this.imageIndex = Holder.NamedImageList.ImageIndex("imgProp");
      return this.imageIndex;
    }
  }

  public void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    this.nodeID = (SnapshotsNodeID) items.GetItemData(0, typeof (SnapshotsNodeID));
    this.objectID = this.nodeID.ObjectID;
    this.snapshotID = this.nodeID.SnapshotID;
    this.loaded = false;
  }

  public void Activate(IView previousView)
  {
    if (this.loaded)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.shanpshotProperty.SelectedObject = (object) new ShapshotDescriptorHolder(this.nodeID, sessionKeeper.Session.GetSnapshot(this.snapshotID).GetAttributes(this.objectID));
    this.loaded = true;
  }

  public void Deactivate(IView nextView)
  {
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SnapshotProperty));
    this.shanpshotProperty = new PropertyGrid();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.shanpshotProperty, "shanpshotProperty");
    this.shanpshotProperty.Name = "shanpshotProperty";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.shanpshotProperty);
    this.Name = nameof (SnapshotProperty);
    this.ResumeLayout(false);
  }
}
