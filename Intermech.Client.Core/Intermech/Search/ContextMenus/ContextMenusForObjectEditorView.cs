
// Type: Intermech.Search.ContextMenus.ContextMenusForObjectEditorView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Search.ContextMenus;

public sealed class ContextMenusForObjectEditorView : UserControl, IView
{
  private IDBTypedObjectID _typedObjectID;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ContextMenusForObjectEditorControl _contextMenusForObjectEditorControl;

  public static bool TryGetSuitableSingleTypedObjectID(
    ISelectedItems selectedItems,
    out IDBTypedObjectID typedObjectID)
  {
    if (SelectedItemsHelper.TryGetSingleTypedObjectIDWithObjectVersionIDAndObjectTypeID(selectedItems, out typedObjectID) && typedObjectID.ObjectType == Constants.RoleObjectTypeID)
      return true;
    typedObjectID = (IDBTypedObjectID) null;
    return false;
  }

  public ContextMenusForObjectEditorView() => this.InitializeComponent();

  public string Caption => "Контекстные меню";

  public int ImageIndex => -1;

  public int OrderID => 1000;

  public void Activate(IView previousView)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this._contextMenusForObjectEditorControl.ObjectVersionID = sessionKeeper.Session.GetObjectAttributeByID(this._typedObjectID.ObjectID, Constants.RoleConfigurationAttributeTypeID).AsInteger;
  }

  public void Deactivate(IView nextView)
  {
  }

  public void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    if (items == null)
      throw new ArgumentNullException(nameof (items));
    IDBTypedObjectID typedObjectID = (IDBTypedObjectID) null;
    if (!ContextMenusForObjectEditorView.TryGetSuitableSingleTypedObjectID(items, out typedObjectID))
      throw new ArgumentException();
    this._typedObjectID = typedObjectID;
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
    this._contextMenusForObjectEditorControl = new ContextMenusForObjectEditorControl();
    this._contextMenusForObjectEditorControl.BeginInit();
    this.SuspendLayout();
    this._contextMenusForObjectEditorControl.Dock = DockStyle.Fill;
    this._contextMenusForObjectEditorControl.Location = new Point(0, 0);
    this._contextMenusForObjectEditorControl.Name = "_contextMenusForObjectEditorControl";
    this._contextMenusForObjectEditorControl.Size = new Size(707, 370);
    this._contextMenusForObjectEditorControl.TabIndex = 0;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._contextMenusForObjectEditorControl);
    this.Name = nameof (ContextMenusForObjectEditorView);
    this.Size = new Size(707, 370);
    this._contextMenusForObjectEditorControl.EndInit();
    this.ResumeLayout(false);
  }
}
