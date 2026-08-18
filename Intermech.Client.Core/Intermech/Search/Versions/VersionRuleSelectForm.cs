
// Type: Intermech.Search.Versions.VersionRuleSelectForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Infralution.Controls.VirtualTree;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;


namespace Intermech.Search.Versions;

public class VersionRuleSelectForm : Form
{
  private BindingList<VersionsRule> _dataSource = new BindingList<VersionsRule>();
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Infralution.Controls.VirtualTree.VirtualTree _tree;
  private Button _cancelButton;
  private Button _okButton;
  private Column _captionColumn;

  public VersionRuleSelectForm()
  {
    this.InitializeComponent();
    this.InitializeTree();
  }

  public BindingList<VersionsRule> DataSource
  {
    get => this._dataSource;
    set
    {
      if (this._dataSource == value)
        return;
      this._dataSource = value ?? new BindingList<VersionsRule>();
      this._tree.DataSource = (object) this._dataSource;
    }
  }

  public List<VersionsRule> SelectedVersionRules
  {
    get => this._tree.SelectedItems.Cast<VersionsRule>().ToList<VersionsRule>();
  }

  public VersionsRule SelectedVersionRule => this._tree.SelectedItem as VersionsRule;

  private void InitializeTree()
  {
    ICategoryTypeIconService categoryTypeIconService = ServiceLocator.Get<ICategoryTypeIconService>();
    VersionRuleSelectForm.AdvancedObjectRowBinding objectRowBinding = new VersionRuleSelectForm.AdvancedObjectRowBinding();
    objectRowBinding.ImageIndexProvider = new Func<object, int>(this.GetImageIndex);
    objectRowBinding.ImageList = categoryTypeIconService.ImageList;
    objectRowBinding.ShowPrefixColumn = true;
    objectRowBinding.Type = typeof (VersionsRule);
    VersionRuleSelectForm.AdvancedObjectCellBinding objectCellBinding = new VersionRuleSelectForm.AdvancedObjectCellBinding();
    objectCellBinding.Column = this._captionColumn;
    objectCellBinding.Field = "RuleObjectCaption";
    objectRowBinding.CellBindings.Add((CellBinding) objectCellBinding);
    this._tree.RowBindings.Add((RowBinding) objectRowBinding);
  }

  private int GetImageIndex(object versionRule)
  {
    return ServiceLocator.Get<ICategoryTypeIconService>().IndexOf(4, ((VersionsRule) versionRule).RuleObjectType);
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
    this.components = (IContainer) new System.ComponentModel.Container();
    this._cancelButton = new Button();
    this._okButton = new Button();
    this._tree = new Infralution.Controls.VirtualTree.VirtualTree();
    this._captionColumn = new Column();
    this._tree.BeginInit();
    this.SuspendLayout();
    this._cancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._cancelButton.DialogResult = DialogResult.Cancel;
    this._cancelButton.Location = new Point(384, 287);
    this._cancelButton.Name = "_cancelButton";
    this._cancelButton.Size = new Size(75, 23);
    this._cancelButton.TabIndex = 1;
    this._cancelButton.Text = "Отмена";
    this._cancelButton.UseVisualStyleBackColor = true;
    this._okButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._okButton.DialogResult = DialogResult.OK;
    this._okButton.Location = new Point(303, 287);
    this._okButton.Name = "_okButton";
    this._okButton.Size = new Size(75, 23);
    this._okButton.TabIndex = 2;
    this._okButton.Text = "ОК";
    this._okButton.UseVisualStyleBackColor = true;
    this._tree.AllowDrop = true;
    this._tree.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this._tree.Columns.Add(this._captionColumn);
    this._tree.ImageList = (ImageList) null;
    this._tree.Location = new Point(0, 0);
    this._tree.MainColumn = this._captionColumn;
    this._tree.Name = "_tree";
    this._tree.ShowColumnHeaders = false;
    this._tree.ShowRootRow = false;
    this._tree.Size = new Size(471, 281);
    this._tree.TabIndex = 0;
    this._captionColumn.Caption = "Заголовок";
    this._captionColumn.HeaderStyle.HorzAlignment = StringAlignment.Near;
    this._captionColumn.Name = "_captionColumn";
    this._captionColumn.Width = 300;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(471, 322);
    this.Controls.Add((Control) this._okButton);
    this.Controls.Add((Control) this._cancelButton);
    this.Controls.Add((Control) this._tree);
    this.Name = nameof (VersionRuleSelectForm);
    this.SizeGripStyle = SizeGripStyle.Show;
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Форма выбора правила подбора версий";
    this._tree.EndInit();
    this.ResumeLayout(false);
  }

  private sealed class AdvancedObjectRowBinding : ObjectRowBinding
  {
    public Func<object, int> ImageIndexProvider { get; set; }

    public ImageList ImageList { get; set; }

    public override void GetRowData(Row row, RowData rowData)
    {
      rowData.ImageList = this.ImageList;
      if (this.ImageIndexProvider != null)
        rowData.ImageIndex = this.ImageIndexProvider(row.Item);
      base.GetRowData(row, rowData);
    }
  }

  private sealed class AdvancedObjectCellBinding : ObjectCellBinding
  {
    public override void GetCellData(Row row, CellData cellData)
    {
      base.GetCellData(row, cellData);
      if (row.Item == null || cellData.Value != null)
        return;
      FieldInfo field = row.Item.GetType().GetField(this.Field);
      cellData.Value = field.GetValue(row.Item);
    }
  }
}
