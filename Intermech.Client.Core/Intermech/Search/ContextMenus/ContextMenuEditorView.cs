
// Type: Intermech.Search.ContextMenus.ContextMenuEditorView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Search.ContextMenus;

public sealed class ContextMenuEditorView : UserControl, IView
{
  private IDBTypedObjectID _typedObjectID;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ContextMenuEditorControl _contextMenuEditorControl;

  public static bool TryGetSuitableSingleTypedObjectID(
    ISelectedItems selectedItems,
    out IDBTypedObjectID typedObjectID)
  {
    if (SelectedItemsHelper.TryGetSingleTypedObjectIDWithObjectVersionIDAndObjectTypeID(selectedItems, out typedObjectID) && typedObjectID.ObjectType == ContextMenuConstants.ContextMenuObjectTypeID)
      return true;
    typedObjectID = (IDBTypedObjectID) null;
    return false;
  }

  public ContextMenuEditorView() => this.InitializeComponent();

  public string Caption => "Редактор контекстного меню";

  public int ImageIndex => -1;

  public int OrderID => 0;

  public void Activate(IView previousView)
  {
    this._contextMenuEditorControl.ContextMenuVersionID = this._typedObjectID.ObjectID;
  }

  public void Deactivate(IView nextView)
  {
    if (!this._contextMenuEditorControl.HasChanges || MessageBox.Show("Контекстное меню было изменено. Сохранить изменения?", "Intermech Professional Solution", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
      return;
    this._contextMenuEditorControl.Accept();
  }

  public void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    if (items == null)
      throw new ArgumentNullException(nameof (items));
    IDBTypedObjectID typedObjectID = (IDBTypedObjectID) null;
    if (!ContextMenuEditorView.TryGetSuitableSingleTypedObjectID(items, out typedObjectID))
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
    this._contextMenuEditorControl = new ContextMenuEditorControl();
    this._contextMenuEditorControl.BeginInit();
    this.SuspendLayout();
    this._contextMenuEditorControl.Dock = DockStyle.Fill;
    this._contextMenuEditorControl.Location = new Point(0, 0);
    this._contextMenuEditorControl.Name = "_contextMenuEditorControl";
    this._contextMenuEditorControl.Size = new Size(760, 393);
    this._contextMenuEditorControl.TabIndex = 0;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._contextMenuEditorControl);
    this.Name = nameof (ContextMenuEditorView);
    this.Size = new Size(760, 393);
    this._contextMenuEditorControl.EndInit();
    this.ResumeLayout(false);
  }
}
