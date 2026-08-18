
// Type: Intermech.Navigator.Controls.RecentObjectsWindow
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Infralution.Controls;
using Intermech.Bars;
using Intermech.Docking;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;


namespace Intermech.Navigator.Controls;

/// <summary>Окно "Недавние объекты"</summary>
public class RecentObjectsWindow : WellKnownNavWindow, IRecentObjectsWindow
{
  /// <summary>Guid окна "Недавние объекты"</summary>
  public static readonly Guid _persistStateGuidNew = new Guid("{105DC5E6-8847-44E9-B00F-ACBE4F3D17FE}");

  /// <summary>Создать окно "Недавние объекты"</summary>
  public RecentObjectsWindow()
  {
    this.Guid = RecentObjectsWindow._persistStateGuidNew;
    if (ServicesManager.GetService(typeof (IRecentObjectsWindow)) is IRecentObjectsWindow)
      return;
    ServicesManager.AddService(typeof (IRecentObjectsWindow), (object) this);
  }

  /// <summary>Обновить содержимое окна</summary>
  void IRecentObjectsWindow.Update() => this.FullWindowRefresh();

  /// <summary>Обновить содержимое окна посредством уведомления</summary>
  void IRecentObjectsWindow.Notify()
  {
    if (this._notificationService == null)
      return;
    this._notificationService.FireEvent((object) this, new NotificationEventArgs("RecentObjectsChanged"));
  }

  /// <summary>Восстановить состояние окна</summary>
  /// <param name="xmlDoc">Документ XML, в котором хранится состояние окна</param>
  public override void RestoreState(XmlDocument xmlDoc)
  {
    base.RestoreState(xmlDoc);
    if (ServicesManager.GetService(typeof (IRecentObjectsWindow)) is IRecentObjectsWindow)
      return;
    ServicesManager.AddService(typeof (IRecentObjectsWindow), (object) this);
  }

  /// <summary>Форма активирована</summary>
  public override void Activated()
  {
    if (!(ServicesManager.GetService(typeof (IRecentObjectsWindow)) is IRecentObjectsWindow))
      ServicesManager.AddService(typeof (IRecentObjectsWindow), (object) this);
    base.Activated();
  }

  /// <summary>Восстановить окно</summary>
  /// <param name="guid"></param>
  /// <param name="persistString"></param>
  /// <returns></returns>
  public new static DockControl RestoreWindowCallback(Guid guid, string persistString)
  {
    if (guid != RecentObjectsWindow._persistStateGuidNew)
      return (DockControl) null;
    try
    {
      XmlDocument xmlDoc = new XmlDocument();
      xmlDoc.LoadXml(persistString);
      RecentObjectsWindow recentObjectsWindow = new RecentObjectsWindow();
      try
      {
        recentObjectsWindow.RestoreState(xmlDoc);
        if (recentObjectsWindow.RootDescriptor == null)
        {
          recentObjectsWindow.WellKnownName = string.Empty;
          recentObjectsWindow.HideOnClose = false;
          recentObjectsWindow.Close();
          return (DockControl) null;
        }
        bool flag1 = false;
        if (ServicesManager.GetService(typeof (IEnableTreeMultiSelectService)) is IEnableTreeMultiSelectService service1)
          flag1 = service1.EnableTreeMultiSelect(recentObjectsWindow.RootDescriptor, (System.IServiceProvider) recentObjectsWindow.Services);
        if (flag1)
          recentObjectsWindow.TreeView.CheckBoxStyle = NavigatorTreeViewCheckBoxStyle.TwoState;
        bool flag2 = true;
        if (ServicesManager.GetService(typeof (IEnableTreeColumnsSortingService)) is IEnableTreeColumnsSortingService service2)
          flag2 = service2.EnableTreeColumnsSorting(recentObjectsWindow.RootDescriptor, (System.IServiceProvider) recentObjectsWindow.Services);
        recentObjectsWindow.TreeView.DisableColumnsSorting = !flag2;
        recentObjectsWindow.btClearSorting.Enabled = flag2;
        if (!flag2)
          recentObjectsWindow.btClearSorting.Checked = true;
        bool flag3 = false;
        if (ServicesManager.GetService(typeof (INavigatorTreeCollapseService)) is INavigatorTreeCollapseService service3)
          flag3 = service3.EnableTreeCollapse(recentObjectsWindow.RootDescriptor, (System.IServiceProvider) recentObjectsWindow.Services);
        if (flag3 && !recentObjectsWindow.spTreeView.IsCollapsed)
          recentObjectsWindow.spTreeView.ToggleState();
        return (DockControl) recentObjectsWindow;
      }
      catch
      {
        recentObjectsWindow.WellKnownName = string.Empty;
        recentObjectsWindow.HideOnClose = false;
        recentObjectsWindow.Close();
        recentObjectsWindow.Dispose();
      }
    }
    catch (Exception ex)
    {
      return (DockControl) null;
    }
    return (DockControl) null;
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (RecentObjectsWindow));
    this.pnTreeView.SuspendLayout();
    this.TreeViewControl.TreeView.BeginInit();
    this.TreeViewControl.SuspendLayout();
    this.SuspendLayout();
    this.TreeViewControl.BtnClearSorting.AutoToggle = AutoToggleType.Single;
    this.TreeViewControl.BtnClearSorting.CommandName = "btCancelSort";
    this.TreeViewControl.BtnClearSorting.Image = (Image) componentResourceManager.GetObject("TreeViewControl.BtnClearSorting.Image");
    this.TreeViewControl.BtnClearSorting.ToolTipText = "Режим ручной сортировки";
    this.TreeViewControl.BtnSetupSorting.CommandName = "btSetupSorting";
    this.TreeViewControl.BtnSetupSorting.Image = (Image) componentResourceManager.GetObject("TreeViewControl.BtnSetupSorting.Image");
    this.TreeViewControl.BtnSetupSorting.ToolTipText = "Выполнить настройку ручной сортировки";
    this.TreeViewControl.ImagesToolbar.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("TreeViewControl.ImagesToolbar.ImageStream");
    this.TreeViewControl.ImagesToolbar.TransparentColor = Color.Transparent;
    this.TreeViewControl.ImagesToolbar.Images.SetKeyName(0, "");
    this.TreeViewControl.ImagesToolbar.Images.SetKeyName(1, "");
    this.TreeViewControl.ImagesToolbar.Images.SetKeyName(2, "");
    this.TreeViewControl.ImagesToolbar.Images.SetKeyName(3, "");
    this.TreeViewControl.LabelSpace.BeginGroup = true;
    this.TreeViewControl.LabelSpace.CommandName = "labelSpace";
    this.TreeViewControl.LabelSpace.Enabled = false;
    this.TreeViewControl.LabelSpace.Stretch = true;
    this.TreeViewControl.LabelSpace.Text = " ";
    this.TreeViewControl.LabelSpace.ToolTipText = " ";
    this.TreeViewControl.TreeToolbar.FlipLastItem = true;
    this.TreeViewControl.TreeToolbar.FullMenus = true;
    this.TreeViewControl.TreeToolbar.Guid = new Guid("3fb71a02-4b93-44ea-84a6-db6e9ca5869f");
    this.TreeViewControl.TreeToolbar.Hidden = false;
    this.TreeViewControl.TreeToolbar.Items.AddRange(new ToolbarItemBase[3]
    {
      (ToolbarItemBase) this.TreeViewControl.BtnClearSorting,
      (ToolbarItemBase) this.TreeViewControl.BtnSetupSorting,
      (ToolbarItemBase) this.TreeViewControl.LabelSpace
    });
    this.TreeViewControl.TreeToolbar.Location = new Point(0, 0);
    this.TreeViewControl.TreeToolbar.Name = "_tbTreePanel";
    this.TreeViewControl.TreeToolbar.Size = new Size((int) byte.MaxValue, 24);
    this.TreeViewControl.TreeToolbar.TabIndex = 8;
    this.TreeViewControl.TreeToolbar.Text = "";
    this.TreeViewControl.TreeView.BackgroundImageMode = ImageDrawMode.Tile;
    this.TreeViewControl.TreeView.BorderStyle = BorderStyle.Fixed3D;
    this.TreeViewControl.TreeView.HeaderStyle.HorzAlignment = StringAlignment.Near;
    this.TreeViewControl.TreeView.RowEvenStyle.WordWrap = false;
    this.TreeViewControl.TreeView.RowOddStyle.WordWrap = false;
    this.TreeViewControl.TreeView.RowSelectedStyle.WordWrap = false;
    this.TreeViewControl.TreeView.RowStyle.BorderColor = SystemColors.Control;
    this.TreeViewControl.TreeView.RowStyle.BorderStyle = Border3DStyle.Adjust;
    this.TreeViewControl.TreeView.RowStyle.BorderWidth = 1;
    this.TreeViewControl.TreeView.RowStyle.WordWrap = false;
    this.TreeViewControl.TreeView.SelectionMode = Infralution.Controls.VirtualTree.SelectionMode.FullRow;
    this.TreeViewControl.TreeView.Size = new Size((int) byte.MaxValue, 313);
    this.Name = nameof (RecentObjectsWindow);
    this.TreeListColumns = (NodeColumnCollection) componentResourceManager.GetObject("$this.TreeListColumns");
    this.pnTreeView.ResumeLayout(false);
    this.TreeViewControl.TreeView.EndInit();
    this.TreeViewControl.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
