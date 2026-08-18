
// Type: Intermech.Search.CompositionByObjectTypesFilters.CompositionByObjectTypesFilterEditorView
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


namespace Intermech.Search.CompositionByObjectTypesFilters;

public sealed class CompositionByObjectTypesFilterEditorView : UserControl, IView
{
  private IDBTypedObjectID _typedObjectID;
  private CompositionByObjectTypesFilter _filter;
  private CompositionByObjectTypesFilter _filterBackup;
  private bool _hasChanges;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private CompositionByObjectTypesFilterEditorControl _editorControl;
  private TableLayoutPanel tableLayoutPanel1;
  private FlowLayoutPanel flowLayoutPanel1;
  private Button _cancelButton;
  private Button _acceptButton;

  public static bool TryGetSuitableSingleTypedObjectID(
    ISelectedItems selectedItems,
    out IDBTypedObjectID typedObjectID)
  {
    if (SelectedItemsHelper.TryGetSingleTypedObjectIDWithObjectVersionIDAndObjectTypeID(selectedItems, out typedObjectID) && typedObjectID.ObjectType == CompositionByObjectTypesFiltersConstants.CompositionByObjectTypesFilterObjectTypeID)
      return true;
    typedObjectID = (IDBTypedObjectID) null;
    return false;
  }

  public CompositionByObjectTypesFilterEditorView() => this.InitializeComponent();

  public void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    if (items == null)
      throw new ArgumentNullException(nameof (items));
    IDBTypedObjectID typedObjectID = (IDBTypedObjectID) null;
    if (!CompositionByObjectTypesFilterEditorView.TryGetSuitableSingleTypedObjectID(items, out typedObjectID))
      throw new ArgumentException();
    this._typedObjectID = typedObjectID;
  }

  public void Activate(IView previousView)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.SetFilter(((ICompositionByObjectTypesFiltersServerService) sessionKeeper.Session.GetCustomService(typeof (ICompositionByObjectTypesFiltersServerService))).FindFilterByVersionID(sessionKeeper.Session.SessionGUID, this._typedObjectID.ObjectID));
  }

  public void Deactivate(IView nextView)
  {
    if (!this._hasChanges || MessageBox.Show("Фильтр был изменен. Сохранить изменения?", "Intermech Professional Solution", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
      return;
    this.AcceptChanges();
  }

  public string Caption => "Редактор фильтра состава по типам объектов";

  public int ImageIndex => -1;

  public int OrderID => 0;

  private void Filter_PropertyChanged(object sender, PropertyChangedEventArgs e)
  {
    this._hasChanges = true;
    this.UpdateView();
  }

  private void AcceptButton_Click(object sender, EventArgs e) => this.AcceptChanges();

  private void CancelButton_Click(object sender, EventArgs e) => this.CancelChanges();

  private void SetFilter(CompositionByObjectTypesFilter filter)
  {
    if (this._filter != null)
      this._filter.PropertyChanged -= new PropertyChangedEventHandler(this.Filter_PropertyChanged);
    this._filter = filter;
    if (this._filter != null)
      this._filter.PropertyChanged += new PropertyChangedEventHandler(this.Filter_PropertyChanged);
    this._filterBackup = this._filter.Clone();
    this._editorControl.Filter = this._filter;
    this._hasChanges = false;
    this.UpdateView();
  }

  private void UpdateView()
  {
    this._acceptButton.Enabled = this._hasChanges;
    this._cancelButton.Enabled = this._hasChanges;
  }

  private void AcceptChanges()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ICompositionByObjectTypesFiltersServerService customService = (ICompositionByObjectTypesFiltersServerService) sessionKeeper.Session.GetCustomService(typeof (ICompositionByObjectTypesFiltersServerService));
      this._filter.PropertyChanged -= new PropertyChangedEventHandler(this.Filter_PropertyChanged);
      try
      {
        customService.SaveFilter(sessionKeeper.Session.SessionGUID, this._typedObjectID.ObjectID, this._filter);
      }
      finally
      {
        this._filter.PropertyChanged += new PropertyChangedEventHandler(this.Filter_PropertyChanged);
      }
    }
    this.SetFilter(this._filter);
  }

  private void CancelChanges()
  {
    if (MessageBox.Show("Фильтр был изменен. Отменить изменения?", "Intermech Professional Solution", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
      return;
    this.SetFilter(this._filterBackup);
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
    this._editorControl = new CompositionByObjectTypesFilterEditorControl();
    this.tableLayoutPanel1 = new TableLayoutPanel();
    this.flowLayoutPanel1 = new FlowLayoutPanel();
    this._cancelButton = new Button();
    this._acceptButton = new Button();
    this._editorControl.BeginInit();
    this.tableLayoutPanel1.SuspendLayout();
    this.flowLayoutPanel1.SuspendLayout();
    this.SuspendLayout();
    this._editorControl.Dock = DockStyle.Fill;
    this._editorControl.Location = new Point(3, 3);
    this._editorControl.Name = "_editorControl";
    this._editorControl.Size = new Size(421, 180);
    this._editorControl.TabIndex = 0;
    this.tableLayoutPanel1.ColumnCount = 1;
    this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel1.Controls.Add((Control) this._editorControl, 0, 0);
    this.tableLayoutPanel1.Controls.Add((Control) this.flowLayoutPanel1, 0, 1);
    this.tableLayoutPanel1.Dock = DockStyle.Fill;
    this.tableLayoutPanel1.Location = new Point(0, 0);
    this.tableLayoutPanel1.Name = "tableLayoutPanel1";
    this.tableLayoutPanel1.RowCount = 2;
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40f));
    this.tableLayoutPanel1.Size = new Size(427, 226);
    this.tableLayoutPanel1.TabIndex = 1;
    this.flowLayoutPanel1.Controls.Add((Control) this._cancelButton);
    this.flowLayoutPanel1.Controls.Add((Control) this._acceptButton);
    this.flowLayoutPanel1.Dock = DockStyle.Fill;
    this.flowLayoutPanel1.FlowDirection = FlowDirection.RightToLeft;
    this.flowLayoutPanel1.Location = new Point(3, 189);
    this.flowLayoutPanel1.Name = "flowLayoutPanel1";
    this.flowLayoutPanel1.Size = new Size(421, 34);
    this.flowLayoutPanel1.TabIndex = 1;
    this._cancelButton.Location = new Point(343, 3);
    this._cancelButton.Name = "_cancelButton";
    this._cancelButton.Size = new Size(75, 23);
    this._cancelButton.TabIndex = 0;
    this._cancelButton.Text = "Отмена";
    this._cancelButton.UseVisualStyleBackColor = true;
    this._cancelButton.Click += new EventHandler(this.CancelButton_Click);
    this._acceptButton.Location = new Point(262, 3);
    this._acceptButton.Name = "_acceptButton";
    this._acceptButton.Size = new Size(75, 23);
    this._acceptButton.TabIndex = 0;
    this._acceptButton.Text = "Применить";
    this._acceptButton.UseVisualStyleBackColor = true;
    this._acceptButton.Click += new EventHandler(this.AcceptButton_Click);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.tableLayoutPanel1);
    this.Name = nameof (CompositionByObjectTypesFilterEditorView);
    this.Size = new Size(427, 226);
    this._editorControl.EndInit();
    this.tableLayoutPanel1.ResumeLayout(false);
    this.flowLayoutPanel1.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
