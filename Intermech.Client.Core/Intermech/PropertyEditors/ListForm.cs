
// Type: Intermech.PropertyEditors.ListForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using DevExpress.IM.XtraGrid;
using DevExpress.IM.XtraGrid.Columns;
using DevExpress.IM.XtraGrid.Views.Base;
using DevExpress.IM.XtraGrid.Views.Grid;
using DevExpress.IM.XtraGrid.Views.Grid.ViewInfo;
using Intermech.Client.Core;
using Intermech.Client.Core.Configurator;
using Intermech.Controls;
using Intermech.DataFormats;
using Intermech.Holders;
using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using Intermech.Search;
using Intermech.Search.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

/// <summary>Форма "Список"</summary>
public class ListForm : TabPageForm
{
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;
  private ContextMenu contextMenu;
  private MenuItem exportMenuItem;
  private GridView gridView;
  private GridControl gridControl;
  private MenuItem copyMenuItem;
  private MenuItem excludeMenuItem;
  private MenuItem combineMenuItem;
  private MenuItem deleteMenuItem;
  private bool sortingFlag;
  private List<object> selectedRowKeys;
  /// <summary>
  /// при нажатии вместе C+L+R колонки в гридах сбрасываются по умолчанию
  /// </summary>
  private readonly Keys[] engKeys = new Keys[3]
  {
    Keys.C,
    Keys.L,
    Keys.R
  };
  private bool[] engPressed = new bool[3];

  public ListForm(Guid aInstGuid)
    : base(aInstGuid)
  {
    this.InitializeComponent();
    this.ApplyVisualSettings();
  }

  private void ApplyVisualSettings()
  {
    if (!ServiceLocator.IsRegistered<IConfigurationOptionRepository>() || !(ServiceLocator.Get<IConfigurationOptionRepository>().Find(ConfigurationOptionKeys.UI_GridFont) is Font font))
      return;
    this.gridControl.Font = font;
    this.gridView.ViewStylesInfo.Row.Font = font;
    this.gridView.ViewStylesInfo.FocusedRow.Font = font;
    this.gridView.ViewStylesInfo.FocusedCell.Font = font;
    this.gridView.ViewStylesInfo.SelectedRow.Font = font;
    this.gridView.ViewStylesInfo.FilterPanel.Font = font;
    this.gridView.ViewStylesInfo.FooterPanel.Font = font;
    this.gridView.ViewStylesInfo.GroupPanel.Font = font;
    this.gridView.ViewStylesInfo.HeaderPanel.Font = font;
    this.gridView.ViewStylesInfo.EvenRow.Font = font;
    this.gridView.ViewStylesInfo.OddRow.Font = font;
    this.gridView.ViewStylesInfo.HorzLine.Font = font;
    this.gridView.ViewStylesInfo.GroupRow.Font = font;
    this.gridView.ViewStylesInfo.GroupFooter.Font = font;
    this.gridView.ViewStylesInfo.FixedLine.Font = font;
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ListForm));
    this.contextMenu = new ContextMenu();
    this.exportMenuItem = new MenuItem();
    this.excludeMenuItem = new MenuItem();
    this.copyMenuItem = new MenuItem();
    this.combineMenuItem = new MenuItem();
    this.deleteMenuItem = new MenuItem();
    this.gridControl = new GridControl();
    this.gridView = new GridView();
    this.gridControl.BeginInit();
    this.gridView.BeginInit();
    this.SuspendLayout();
    this.contextMenu.MenuItems.AddRange(new MenuItem[5]
    {
      this.exportMenuItem,
      this.excludeMenuItem,
      this.copyMenuItem,
      this.combineMenuItem,
      this.deleteMenuItem
    });
    this.contextMenu.Popup += new EventHandler(this.contextMenu_Popup);
    this.exportMenuItem.Index = 0;
    componentResourceManager.ApplyResources((object) this.exportMenuItem, "exportMenuItem");
    this.exportMenuItem.Click += new EventHandler(this.exportMenuItem_Click);
    this.excludeMenuItem.Index = 1;
    componentResourceManager.ApplyResources((object) this.excludeMenuItem, "excludeMenuItem");
    this.excludeMenuItem.Click += new EventHandler(this.excludeMenuItem_Click);
    this.copyMenuItem.Index = 2;
    componentResourceManager.ApplyResources((object) this.copyMenuItem, "copyMenuItem");
    this.copyMenuItem.Click += new EventHandler(this.copyMenuItem_Click);
    this.combineMenuItem.Index = 3;
    componentResourceManager.ApplyResources((object) this.combineMenuItem, "combineMenuItem");
    this.combineMenuItem.Click += new EventHandler(this.combineMenuItem_Click);
    this.deleteMenuItem.Index = 4;
    componentResourceManager.ApplyResources((object) this.deleteMenuItem, "deleteMenuItem");
    this.deleteMenuItem.Click += new EventHandler(this.deleteMenuItem_Click);
    this.gridControl.ContextMenu = this.contextMenu;
    componentResourceManager.ApplyResources((object) this.gridControl, "gridControl");
    this.gridControl.EmbeddedNavigator.Name = "";
    this.gridControl.MainView = (BaseView) this.gridView;
    this.gridControl.Name = "gridControl";
    this.gridView.GridControl = this.gridControl;
    componentResourceManager.ApplyResources((object) this.gridView, "gridView");
    this.gridView.Name = "gridView";
    this.gridView.OptionsBehavior.Editable = false;
    this.gridView.OptionsSelection.MultiSelect = true;
    this.gridView.OptionsView.ColumnAutoWidth = false;
    this.gridView.StartSorting += new EventHandler(this.gridView_StartSorting);
    this.gridView.EndSorting += new EventHandler(this.gridView_EndSorting);
    this.gridView.KeyDown += new KeyEventHandler(this.gridView_KeyDown);
    this.gridView.KeyUp += new KeyEventHandler(this.gridView_KeyUp);
    this.gridView.KeyPress += new KeyPressEventHandler(this.gridView_KeyPress);
    this.gridView.DoubleClick += new EventHandler(this.gridView_DoubleClick);
    this.Controls.Add((Control) this.gridControl);
    this.Name = nameof (ListForm);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Tag = (object) "  ";
    this.gridControl.EndInit();
    this.gridView.EndInit();
    this.ResumeLayout(false);
  }

  public GridControl GridControl => this.gridControl;

  public GridView GridView => this.gridView;

  public override void FillForm(IFolder folder)
  {
    this.exportMenuItem.Visible = ServicesManager.GetService(typeof (IBriefcase)) is IBriefcase;
    this._folder = folder as CustomFolder;
    this.copyMenuItem.Visible = this._folder.ListCategoryValue == 3;
    this.excludeMenuItem.Visible = this._folder.ListCategoryValue == 3 && (int) this._folder.Id != -1;
    this.combineMenuItem.Visible = this._folder.ListCategoryValue == 3;
    this.deleteMenuItem.Visible = true;
  }

  public override void FormLostFocus(IFolder folder)
  {
    if (this._folder != folder as CustomFolder)
      return;
    this.SaveLayout(folder as CustomFolder);
  }

  /// <summary>id раздела справки</summary>
  public override string HelpTopicID
  {
    get
    {
      if (this._folder == null)
        return base.HelpTopicID;
      if (this._folder is AttributeGroupFolder)
        return "1016";
      return this._folder is ObjectTypesFolder || this._folder is ObjectTypeFolder ? "1022" : "1004";
    }
  }

  /// <summary>Clean up any resources being used.</summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      if (this.components != null)
        this.components.Dispose();
      if (this.gridView != null)
        this.gridView.Dispose();
    }
    base.Dispose(disposing);
  }

  private void SaveLayout(CustomFolder folder)
  {
    MemoryStream ms = new MemoryStream();
    this.gridView.SaveLayoutToStream((Stream) ms);
    if (!(ServicesManager.GetService(typeof (IGuidMapper)) is IGuidMapper service))
      return;
    Guid key = service[folder.ListCategoryValue];
    if (!(key != Guid.Empty))
      return;
    ConfigCache.SetConfig(key, ms);
  }

  private string GetSelectedText()
  {
    int[] selectedRows = this.gridView.GetSelectedRows();
    if (selectedRows == null || selectedRows.Length == 0)
      return "";
    StringBuilder stringBuilder = new StringBuilder();
    foreach (int index1 in selectedRows)
    {
      if (selectedRows[index1] >= 0)
      {
        for (int index2 = 0; index2 < this.gridView.Columns.Count; ++index2)
        {
          object rowCellValue = this.gridView.GetRowCellValue(selectedRows[index1], this.gridView.Columns[index2]);
          stringBuilder.Append(rowCellValue.ToString());
          stringBuilder.Append("\t");
        }
      }
      stringBuilder.Append("\r\n");
    }
    return stringBuilder.ToString();
  }

  private ArrayList GetSelectedKeysArray()
  {
    int[] selectedRows = this.gridView.GetSelectedRows();
    if (selectedRows == null || selectedRows.Length == 0)
      return (ArrayList) null;
    string fieldName = Intermech.Consts.KeyFieldByCategory(this._folder.ListCategoryValue);
    if (fieldName == string.Empty)
      return (ArrayList) null;
    GridColumn column = this.gridView.Columns.ColumnByFieldName(fieldName);
    if (column == null)
      return (ArrayList) null;
    ArrayList arrayList = new ArrayList();
    for (int index = 0; index < selectedRows.Length; ++index)
    {
      if (selectedRows[index] >= 0)
      {
        object obj = this.gridView.GetRowCellValue(selectedRows[index], column);
        if (obj is Decimal)
          obj = (object) Convert.ToInt32(obj);
        arrayList.Add(obj);
      }
    }
    return arrayList.Count == 0 ? (ArrayList) null : arrayList;
  }

  private void contextMenu_Popup(object sender, EventArgs e)
  {
    ArrayList selectedKeysArray = this.GetSelectedKeysArray();
    if (this._folder.ListCategoryValue == 3)
    {
      if (selectedKeysArray != null)
      {
        this.exportMenuItem.Enabled = selectedKeysArray.Count > 0;
        this.copyMenuItem.Enabled = selectedKeysArray.Count > 0;
        this.excludeMenuItem.Enabled = selectedKeysArray.Count > 0;
        this.deleteMenuItem.Enabled = selectedKeysArray.Count > 0;
        bool flag = true;
        int num = 0;
        foreach (object obj in selectedKeysArray)
        {
          if (SystemGUIDs.IsSystemGUID(MetaDataHelper.GetAttributeTypeGuid(Convert.ToInt32(obj))))
            ++num;
          if (num > 1)
          {
            flag = false;
            break;
          }
        }
        this.combineMenuItem.Enabled = selectedKeysArray.Count > 1 & flag;
        this.combineMenuItem.Visible = selectedKeysArray.Count > 1;
      }
      else
        this.exportMenuItem.Enabled = this.copyMenuItem.Enabled = this.combineMenuItem.Enabled = this.combineMenuItem.Visible = this.excludeMenuItem.Enabled = this.deleteMenuItem.Enabled = false;
    }
    if (!(this._folder is AttributeTypeAssignedGroupFolder))
      return;
    for (int index = 0; index < this.contextMenu.GetContextMenu().MenuItems.Count; ++index)
    {
      if (this.contextMenu.GetContextMenu().MenuItems[index].Visible && this.contextMenu.GetContextMenu().MenuItems[index].Enabled)
        this.contextMenu.GetContextMenu().MenuItems[index].Enabled = false;
    }
  }

  private void gridView_DoubleClick(object sender, EventArgs e)
  {
    if (this.gridView.CalcHitInfo(this.gridView.GridControl.PointToClient(Control.MousePosition)).HitTest != GridHitTest.RowCell)
      return;
    this.DblClick(sender);
  }

  private void gridView_KeyPress(object sender, KeyPressEventArgs e)
  {
    if (e.KeyChar != '\r')
      return;
    this.DblClick(sender);
  }

  private void DblClick(object sender)
  {
    string fieldName = Intermech.Consts.KeyFieldByCategory(this._folder.ListCategoryValue);
    if (fieldName == string.Empty || this.gridView.GetChildRowCount(this.gridView.FocusedRowHandle) != 0)
      return;
    object rowCellValue = this.gridView.GetRowCellValue(this.gridView.FocusedRowHandle, this.gridView.Columns.ColumnByFieldName(fieldName));
    EventsHolder.FireFolderDClick(sender, this.instGuid, new EventsHolder.FolderArgs(this._folder.ListCategoryValue, rowCellValue, (IFolder) this._folder));
  }

  private void deleteMenuItem_Click(object sender, EventArgs e)
  {
    ArrayList selectedKeysArray = this.GetSelectedKeysArray();
    if (selectedKeysArray == null || IMMessageBox.Show(MessageDialogs.msgConfirmDelete, MessageDialogs.msgReallyDelete, MessageBoxButtons.YesNo, IMMessageBoxImage.Question) != DialogResult.Yes)
      return;
    DeleteActionsForm deleteActionsForm = (DeleteActionsForm) null;
    bool flag1 = false;
    bool flag2 = false;
    int num = 0;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index = 0; index < selectedKeysArray.Count; ++index)
      {
        long DeleteMode = 0;
        if (ClientCommons.GetServerObjectByCategory(sessionKeeper.Session, this._folder.ListCategoryValue, selectedKeysArray[index]) is IDeletable objectByCategory)
        {
          try
          {
            objectByCategory.Delete(DeleteMode);
            ++num;
          }
          catch (Exception ex)
          {
            if (selectedKeysArray.Count == 1)
              throw;
            if (!flag1)
            {
              if (deleteActionsForm == null)
                deleteActionsForm = new DeleteActionsForm();
              switch (deleteActionsForm.ShowDialog(ex))
              {
                case DialogResult.Retry:
                  flag1 = true;
                  break;
                case DialogResult.Ignore:
                  break;
                default:
                  flag2 = true;
                  break;
              }
            }
          }
        }
        if (flag2)
          break;
      }
    }
    if (selectedKeysArray.Count <= 0 || num <= 0)
      return;
    (this._folder.node.Tag as IFolder).Update();
  }

  private void exportMenuItem_Click(object sender, EventArgs e)
  {
    if (!(ServicesManager.GetService(typeof (IBriefcase)) is IBriefcase service))
      return;
    ArrayList selectedKeysArray = this.GetSelectedKeysArray();
    if (selectedKeysArray == null)
      return;
    ExportAttribute exportAttributes = this._folder.GetExportAttributes((object[]) selectedKeysArray.ToArray(typeof (object)));
    if (!service.AddIntoExportList(new ExportAttribute[1]
    {
      exportAttributes
    }))
      return;
    service.ShowView(2);
  }

  private void copyMenuItem_Click(object sender, EventArgs e)
  {
    if (!(ServicesManager.GetService(typeof (IClipboard)) is IClipboard service))
      return;
    ArrayList selectedKeysArray = this.GetSelectedKeysArray();
    if (selectedKeysArray == null || this._folder.ListCategoryValue != 3)
      return;
    ArrayList idList = new ArrayList();
    for (int index = 0; index < selectedKeysArray.Count; ++index)
      idList.Add((object) new DBAttributeID(Convert.ToInt32(selectedKeysArray[index])));
    DBAttributeIDCollection clipboardObject = new DBAttributeIDCollection(idList);
    service.SetDataObject((object) clipboardObject);
  }

  private void excludeMenuItem_Click(object sender, EventArgs e)
  {
    ArrayList selectedKeysArray = this.GetSelectedKeysArray();
    if (selectedKeysArray == null || this._folder.ListCategoryValue != 3)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (this._folder.GetServerObject(sessionKeeper.Session) is IDBAttributesGroup serverObject)
        serverObject.ExcludeAttribute((int[]) selectedKeysArray.ToArray(typeof (int)));
      if (selectedKeysArray.Count <= 0)
        return;
      (this._folder.node.Tag as IFolder).Update();
    }
  }

  /// <summary>Объединить атрибуты.</summary>
  /// <param name="sender">The source of the event.</param>
  /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
  /// <exception cref="T:System.NotImplementedException"></exception>
  private void combineMenuItem_Click(object sender, EventArgs e)
  {
    ArrayList selectedKeysArray = this.GetSelectedKeysArray();
    if (selectedKeysArray == null)
      return;
    List<DBAttributeID> attrIDs = new List<DBAttributeID>();
    for (int index = 0; index < selectedKeysArray.Count; ++index)
      attrIDs.Add(new DBAttributeID(Convert.ToInt32(selectedKeysArray[index])));
    using (CombineAttrForm combineAttrForm = new CombineAttrForm(attrIDs))
    {
      int num = (int) combineAttrForm.ShowDialog();
      if (combineAttrForm.DialogResult != DialogResult.OK)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (!(sessionKeeper.Session.GetCustomService(typeof (IAdminUtilsService)) is IAdminUtilsService customService))
          return;
        string[] strArray = customService.CombineAttributes(sessionKeeper.Session.SessionGUID, combineAttrForm.DeleteAttrIDs, combineAttrForm.RemainAttrID, combineAttrForm.CombineAttributeMode);
        IOutputView service = ServicesManager.GetService(typeof (IOutputView)) as IOutputView;
        string category = "Объединение атрибутов";
        service.ClearText(category);
        for (int index = 0; index < strArray.Length; ++index)
          service.WriteString(category, strArray[index]);
        service.WriteString(category, string.Empty);
        service.Activate(category);
        service.ShowView();
        ((IClientSession) sessionKeeper.Session).ClientCache.ReloadCache(sessionKeeper.Session);
        if (this._folder.node.Text != "Все атрибуты")
        {
          string currentFolderName = this._folder.node.Text;
          TreeNode nodeParent = this._folder.NodeParent;
          TreeNode treeNode1 = nodeParent.Nodes.OfType<TreeNode>().FirstOrDefault<TreeNode>((System.Func<TreeNode, bool>) (node => node.Text.Equals("Все атрибуты")));
          if (treeNode1 == null)
            return;
          if (treeNode1.Tag is IFolder tag)
            tag.Update();
          TreeNode treeNode2 = nodeParent.Nodes.OfType<TreeNode>().FirstOrDefault<TreeNode>((System.Func<TreeNode, bool>) (node => node.Text.Equals(currentFolderName)));
          if (treeNode2 != null)
            tag = treeNode2.Tag as IFolder;
          tag?.Update();
        }
        else
        {
          if (!(this._folder.node.Tag is IFolder tag))
            return;
          tag.Update();
        }
      }
    }
  }

  private void gridView_StartSorting(object sender, EventArgs e)
  {
    this.sortingFlag = true;
    this.selectedRowKeys = (List<object>) null;
    int[] selectedRows = this.gridView.GetSelectedRows();
    if (selectedRows == null || selectedRows.Length == 0)
      return;
    this.selectedRowKeys = new List<object>();
    for (int index = 0; index < selectedRows.Length; ++index)
    {
      if (this.gridView.GetRow(selectedRows[index]) is DataRowView row)
        this.selectedRowKeys.Add(row.Row.ItemArray[0]);
    }
  }

  private void gridView_EndSorting(object sender, EventArgs e)
  {
    if (this.sortingFlag && this.selectedRowKeys != null && this.selectedRowKeys.Count > 0)
    {
      for (int rowHandle = 0; rowHandle < this.gridView.DataRowCount; ++rowHandle)
      {
        if (this.gridView.GetRow(rowHandle) is DataRowView row && this.selectedRowKeys.IndexOf(row.Row.ItemArray[0]) != -1)
          this.gridView.SelectRow(rowHandle);
      }
    }
    this.selectedRowKeys = (List<object>) null;
    this.sortingFlag = false;
  }

  private void ClearKeyStates()
  {
    this.engPressed[0] = false;
    this.engPressed[1] = false;
    this.engPressed[2] = false;
  }

  private void gridView_KeyDown(object sender, KeyEventArgs e)
  {
    int keyCode = (int) e.KeyCode;
    if ((Keys) keyCode == this.engKeys[0])
      this.engPressed[0] = true;
    if ((Keys) keyCode == this.engKeys[1])
      this.engPressed[1] = true;
    if ((Keys) keyCode == this.engKeys[2])
      this.engPressed[2] = true;
    if (!this.engPressed[0] || !this.engPressed[1] || !this.engPressed[2])
      return;
    this.gridView.PopulateColumns();
    this.ClearKeyStates();
  }

  private void gridView_KeyUp(object sender, KeyEventArgs e)
  {
    int keyCode = (int) e.KeyCode;
    if (keyCode == 67)
      this.engPressed[0] = false;
    if (keyCode == 76)
      this.engPressed[1] = false;
    if (keyCode != 82)
      return;
    this.engPressed[2] = false;
  }
}
