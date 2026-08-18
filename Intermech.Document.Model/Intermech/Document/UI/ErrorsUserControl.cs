// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.ErrorsUserControl
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Bars;
using Intermech.Docking;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.UI;

/// <summary> Панель свойств выбранных объектов или документов в редакторе спецификаций </summary>
public class ErrorsUserControl : DockControl
{
  public static Guid DockGuid = new Guid("{7D16BC9F-8E41-45C0-A3CA-66576633963D}");
  private MenuBar menuBar;
  private ContextMenuBarItem contextMenuBarItem;
  private ImDocumentEditorFormBase form;
  private List<ImErrorMessage> errorRows;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ListBox listBox1;

  /// <summary> Конструктор по-умолчанию </summary>
  public ErrorsUserControl(DockManager manager, ImDocumentEditorFormBase form)
  {
    this.InitializeComponent();
    this.PersistState = false;
    this.Collapsible = false;
    this.HideOnClose = true;
    this.ErrorRows = this.errorRows;
    this.Manager = manager;
    this.form = form;
    this.menuBar = new MenuBar();
    this.contextMenuBarItem = new ContextMenuBarItem();
    this.menuBar.Guid = new Guid("e4443c46-71b0-4e60-ac43-db1201338395");
    this.menuBar.Items.AddRange(new ToolbarItemBase[1]
    {
      (ToolbarItemBase) this.contextMenuBarItem
    });
    this.menuBar.Location = new Point(0, 0);
    this.menuBar.Name = nameof (menuBar);
    this.menuBar.OwnerForm = (Form) null;
    this.menuBar.Size = new Size(360, 22);
    this.menuBar.TabIndex = 0;
    this.menuBar.Text = nameof (menuBar);
    this.menuBar.Visible = false;
    this.Guid = ErrorsUserControl.DockGuid;
  }

  protected override void OnMouseUp(MouseEventArgs e) => base.OnMouseUp(e);

  /// <summary>Отобразить окно с ошибками</summary>
  /// <param name="errorRows">Ошибки</param>
  public void Show(List<ImErrorMessage> errorRows)
  {
    this.ErrorRows = errorRows;
    this.Show();
  }

  public new void Show()
  {
    this.form.DockManagerStorage.GetSettings((DockControl) this).Open((DockControl) this, this.Manager);
  }

  /// <summary>Добавить ошибку</summary>
  /// <param name="row">Запись с ошибкой</param>
  /// <param name="message">Текст ошибки</param>
  public void AddError(ImErrorMessage row)
  {
    if (this.errorRows == null)
      this.errorRows = new List<ImErrorMessage>();
    if (!this.errorRows.Any<ImErrorMessage>((Func<ImErrorMessage, bool>) (r => r.Text.Equals(row.Text, StringComparison.Ordinal))))
    {
      this.errorRows.Add(row);
      row.ErrorsControl = this;
    }
    this.UpdateErrors();
    this.Show();
  }

  /// <summary>Очистить ошибки</summary>
  public void Clear()
  {
    this.errorRows = (List<ImErrorMessage>) null;
    this.UpdateErrors();
  }

  private void UpdateErrors()
  {
    this.listBox1.Items.Clear();
    if (this.errorRows == null)
      return;
    foreach (ImErrorMessage errorRow in this.ErrorRows)
      this.listBox1.Items.Add((object) new ErrorsUserControl.RowWrapper()
      {
        Row = errorRow
      });
  }

  /// <summary> Получение списка ОБЪЕКТОВ  </summary>
  public List<ImErrorMessage> ErrorRows
  {
    get => this.errorRows;
    set
    {
      this.errorRows = value;
      if (this.errorRows != null)
      {
        foreach (ImErrorMessage errorRow in this.errorRows)
          errorRow.ErrorsControl = this;
      }
      this.UpdateErrors();
    }
  }

  /// <summary> Получение списка строк описаний </summary>
  public List<string> ErrorStringsRows
  {
    get
    {
      List<string> errorStringsRows = new List<string>();
      for (int index = 0; index < this.listBox1.Items.Count; ++index)
      {
        string str = this.listBox1.Items[index].ToString();
        errorStringsRows.Add(str);
      }
      return errorStringsRows;
    }
  }

  private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
  {
    if (!(this.listBox1.SelectedItem is ErrorsUserControl.RowWrapper selectedItem))
      return;
    selectedItem.Row.DoubleClick();
  }

  private void listBox1_MouseUp(object sender, MouseEventArgs e)
  {
    if (e.Button != MouseButtons.Right || !(this.listBox1.SelectedItem is ErrorsUserControl.RowWrapper selectedItem))
      return;
    List<ToolbarItemBase> contextMenuItems = new List<ToolbarItemBase>();
    ContextMenuBarItem contextMenuBarItem = this.contextMenuBarItem;
    contextMenuBarItem.Items.Clear();
    selectedItem.Row.GetContextMenu(contextMenuItems);
    foreach (ToolbarItemBase toolbarItemBase in contextMenuItems)
      contextMenuBarItem.Items.Add(toolbarItemBase);
    contextMenuBarItem.Show((Control) this, e.Location);
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
    this.listBox1 = new ListBox();
    this.SuspendLayout();
    this.listBox1.Dock = DockStyle.Fill;
    this.listBox1.FormattingEnabled = true;
    this.listBox1.Location = new Point(0, 0);
    this.listBox1.Name = "listBox1";
    this.listBox1.Size = new Size(605, 257);
    this.listBox1.TabIndex = 20;
    this.listBox1.MouseDoubleClick += new MouseEventHandler(this.listBox1_MouseDoubleClick);
    this.listBox1.MouseUp += new MouseEventHandler(this.listBox1_MouseUp);
    this.Controls.Add((Control) this.listBox1);
    this.Name = nameof (ErrorsUserControl);
    this.Size = new Size(605, 257);
    this.Text = "Список ошибок";
    this.ResumeLayout(false);
  }

  private class RowWrapper
  {
    private ImErrorMessage row;

    public ImErrorMessage Row
    {
      get => this.row;
      set => this.row = value;
    }

    public override string ToString() => this.row.Text;
  }
}
