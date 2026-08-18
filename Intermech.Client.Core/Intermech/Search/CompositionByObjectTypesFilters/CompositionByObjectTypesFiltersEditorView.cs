
// Type: Intermech.Search.CompositionByObjectTypesFilters.CompositionByObjectTypesFiltersEditorView
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


namespace Intermech.Search.CompositionByObjectTypesFilters;

public sealed class CompositionByObjectTypesFiltersEditorView : UserControl, IView
{
  private IDBTypedObjectID _typedObjectID;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private CompositionByObjectTypesFiltersEditorControl _editor;

  public static bool TryGetSuitableSingleTypedObjectID(
    ISelectedItems selectedItems,
    out IDBTypedObjectID typedObjectID)
  {
    if (SelectedItemsHelper.TryGetSingleTypedObjectIDWithObjectVersionIDAndObjectTypeID(selectedItems, out typedObjectID) && typedObjectID.ObjectType == Constants.RoleConfigurationObjectTypeID)
      return true;
    typedObjectID = (IDBTypedObjectID) null;
    return false;
  }

  public CompositionByObjectTypesFiltersEditorView() => this.InitializeComponent();

  public void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    if (items == null)
      throw new ArgumentNullException(nameof (items));
    IDBTypedObjectID typedObjectID;
    if (!CompositionByObjectTypesFiltersEditorView.TryGetSuitableSingleTypedObjectID(items, out typedObjectID))
      throw new ArgumentException();
    this._typedObjectID = typedObjectID;
  }

  public void Activate(IView previousView)
  {
    this._editor.ObjectVersionID = this._typedObjectID.ObjectID;
  }

  public void Deactivate(IView nextView)
  {
  }

  public string Caption => "Фильтры составов по типам объектов";

  public int ImageIndex => -1;

  public int OrderID => int.MaxValue;

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
    this._editor = new CompositionByObjectTypesFiltersEditorControl();
    this._editor.BeginInit();
    this.SuspendLayout();
    this._editor.Dock = DockStyle.Fill;
    this._editor.Location = new Point(0, 0);
    this._editor.Name = "_editor";
    this._editor.Size = new Size(553, 341);
    this._editor.TabIndex = 0;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._editor);
    this.Name = nameof (CompositionByObjectTypesFiltersEditorView);
    this.Size = new Size(553, 341);
    this._editor.EndInit();
    this.ResumeLayout(false);
  }
}
