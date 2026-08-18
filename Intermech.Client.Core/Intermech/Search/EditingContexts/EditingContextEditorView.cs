
// Type: Intermech.Search.EditingContexts.EditingContextEditorView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.Search.Utilities;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Search.EditingContexts;

[ViewDescriptionProvider(typeof (EditingContextEditorView.EditingContextEditorViewDescriptionProvider))]
public sealed class EditingContextEditorView : UserControl, IView
{
  private LazyService<INamedImageList> _namedImageList = new LazyService<INamedImageList>();
  private long _editingContextVersionID;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private EditingContextEditorControl _editingContextEditorControl;

  public static bool CheckParamsForInitializeView(
    ISelectedItems selectedItems,
    System.IServiceProvider serviceProvider)
  {
    if (selectedItems == null)
      throw new ArgumentNullException(nameof (selectedItems));
    return selectedItems.Count == 1 && selectedItems.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData && !ObjectHelper.IsUnknownObjectVersionID(itemData.ObjectID) && itemData.ObjectType != -1 && EditingContextsHelper.IsEditingContextObjectTypeID(itemData.ObjectType);
  }

  public EditingContextEditorView() => this.InitializeComponent();

  public void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    if (items == null)
      throw new ArgumentNullException(nameof (items));
    this._editingContextVersionID = EditingContextEditorView.CheckParamsForInitializeView(items, provider) ? (items.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID).ObjectID : throw new ArgumentException();
  }

  public void Activate(IView previousView)
  {
    this._editingContextEditorControl.EditingContextVersionID = this._editingContextVersionID;
  }

  public void Deactivate(IView nextView)
  {
    if (!this._editingContextEditorControl.HasChanges || MessageBox.Show($"Состав контекста редактирования был изменен.{Environment.NewLine}Сохранить изменения?", "Intermech Professional Solution", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
      return;
    this._editingContextEditorControl.AcceptChanges();
  }

  public string Caption => LocalizationHolder.rm.GetString("Client.Core_1225");

  public int ImageIndex => this._namedImageList.Value.ImageIndex("imgObjectsFilter");

  public int OrderID => 0;

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
    this._editingContextEditorControl = new EditingContextEditorControl();
    ((ISupportInitialize) this._editingContextEditorControl).BeginInit();
    this.SuspendLayout();
    this._editingContextEditorControl.Dock = DockStyle.Fill;
    this._editingContextEditorControl.Location = new Point(0, 0);
    this._editingContextEditorControl.Name = "_editingContextEditorControl";
    this._editingContextEditorControl.Size = new Size(631, 300);
    this._editingContextEditorControl.TabIndex = 0;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._editingContextEditorControl);
    this.Name = "EditingContextView";
    this.Size = new Size(631, 300);
    ((ISupportInitialize) this._editingContextEditorControl).EndInit();
    this.ResumeLayout(false);
  }

  private sealed class EditingContextEditorViewDescriptionProvider : BaseViewDescriptionProvider
  {
    public override ViewDescription DoGetViewDescription(
      ISelectedItems selectedItems,
      System.IServiceProvider serviceProvider)
    {
      LazyService<INamedImageList> lazyService = new LazyService<INamedImageList>();
      return new ViewDescription()
      {
        Caption = LocalizationHolder.rm.GetString("Client.Core_1225"),
        ImageIndex = lazyService.Value.ImageIndex("imgObjectsFilter"),
        OrderID = 0
      };
    }
  }
}
