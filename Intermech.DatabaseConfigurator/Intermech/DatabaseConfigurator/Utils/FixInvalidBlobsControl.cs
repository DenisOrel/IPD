// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.Utils.FixInvalidBlobsControl
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.Controls;
using Intermech.Docking;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.DatabaseConfigurator.Utils;

public class FixInvalidBlobsControl : DockControl
{
  public static Guid controlGuid = new Guid("{16790686-82c2-4988-85a5-435be790a351}");
  private List<InvalidBlobInfo> blobInfos = new List<InvalidBlobInfo>();
  private IContainer components;
  private Intermech.Bars.ToolBar tbFix;
  private ButtonItem btnOpenInNewWindow;
  private ButtonItem btnDeleteBlob;
  private ButtonItem btnDeleteObject;
  private ButtonItem btnCheckObject;
  private TreeView tvlnvalidBlobInfo;
  private ButtonItem btnCheckFiles;

  public FixInvalidBlobsControl()
  {
    this.InitializeComponent();
    if (ServicesManager.GetService(typeof (BarManager)) is BarManager service1)
    {
      this.tbFix.Renderer = (IToolBarRenderer) new EmptyToolbarRenderer();
      service1.RendererChanged += new EventHandler(this.barManager_RendererChanged);
      this.ToolbarRendererChanged((object) service1, EventArgs.Empty);
    }
    if (ServicesManager.GetService(typeof (INamedImageList)) is INamedImageList service2)
      this.btnOpenInNewWindow.Image = service2.ImageList.Images[service2.ImageIndex("imgNavigator")];
    this.tvlnvalidBlobInfo.ImageList = Statics.IconSrv.ImageList;
    INotificationService service3 = ServicesManager.ServiceContainer.GetService(typeof (INotificationService)) as INotificationService;
    service3.Subscribe("ObjectsRemoved", new NotificationEventHandler(this.NotificationEvent));
    service3.Subscribe("ObjectsCheckedOut", new NotificationEventHandler(this.NotificationEvent));
    service3.Subscribe("ObjectsCheckedIn", new NotificationEventHandler(this.NotificationEvent));
  }

  protected virtual void ToolbarRendererChanged(object sender, EventArgs e)
  {
    this.tbFix.Renderer = (sender as BarManager).Renderer;
  }

  private void barManager_RendererChanged(object sender, EventArgs e)
  {
    this.tbFix.Renderer = (sender as BarManager).Renderer;
  }

  public void LoadInformation(List<InvalidBlobInfo> blobInfos)
  {
    this.blobInfos = blobInfos;
    blobInfos.Sort();
    this.tvlnvalidBlobInfo.Nodes.Clear();
    long num1 = 0;
    TreeNode treeNode = (TreeNode) null;
    foreach (InvalidBlobInfo blobInfo in blobInfos)
    {
      long objectId = blobInfo.objectID;
      long num2;
      if (num1 != objectId)
      {
        string objectCaption = blobInfo.objectCaption;
        int num3 = Statics.IconSrv.IndexOf(4, blobInfo.objectTypeID);
        TreeNodeCollection nodes = this.tvlnvalidBlobInfo.Nodes;
        num2 = blobInfo.objectID;
        string key = num2.ToString();
        string text = objectCaption;
        int imageIndex = num3;
        int selectedImageIndex = num3;
        treeNode = nodes.Add(key, text, imageIndex, selectedImageIndex);
        treeNode.Tag = (object) blobInfo.objectID;
        num1 = objectId;
      }
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(blobInfo.attrID);
      int num4 = Statics.IconSrv.IndexOf(3, 0, (object) attributeType.FieldType);
      string str1 = blobInfo.fileName == string.Empty ? string.Format(LocalizationHolder.rm.GetString("DatabaseConfigurator_238"), (object) blobInfo.blobID) : blobInfo.fileName;
      string str2 = string.Format(LocalizationHolder.rm.GetString("DatabaseConfigurator_239"), (object) attributeType.Name, (object) str1);
      TreeNodeCollection nodes1 = treeNode.Nodes;
      num2 = blobInfo.blobID;
      string key1 = num2.ToString();
      string text1 = str2;
      int imageIndex1 = num4;
      int selectedImageIndex1 = num4;
      nodes1.Add(key1, text1, imageIndex1, selectedImageIndex1).Tag = (object) blobInfo;
    }
    this.tvlnvalidBlobInfo.ExpandAll();
  }

  private void biOpenInNewWindow_Click(object sender, EventArgs e)
  {
    if (this.tvlnvalidBlobInfo.SelectedNode == null)
      return;
    ISelectedItems items = Intermech.Navigator.ContextMenu.Services.GetItems(Convert.ToInt64(this.tvlnvalidBlobInfo.SelectedNode.Tag));
    ServiceContainer viewServices1 = new ServiceContainer();
    viewServices1.AddService(typeof (IViewState), (object) new ViewStateService());
    ServiceContainer viewServices2 = viewServices1;
    Intermech.Navigator.ContextMenu.Services.InvokeCommand("OpenInNewWindow", Intermech.Navigator.ContextMenu.Services.GetCommandsTable(items, (System.IServiceProvider) viewServices2), (System.IServiceProvider) viewServices1);
  }

  public void UpdateControls()
  {
    bool flag1 = false;
    bool flag2 = false;
    foreach (TreeNode node1 in this.tvlnvalidBlobInfo.Nodes)
    {
      if (!(flag1 & flag2))
      {
        if (node1.Checked)
          flag1 = true;
        if (!flag2)
        {
          foreach (TreeNode node2 in node1.Nodes)
          {
            flag2 = node2.Checked;
            if (flag2)
              break;
          }
        }
      }
      else
        break;
    }
    this.btnDeleteObject.Enabled = flag1;
    this.btnDeleteBlob.Enabled = flag2;
  }

  private void btnDeleteObject_Click(object sender, EventArgs e)
  {
    if (MessageBox.Show(LocalizationHolder.rm.GetString("DatabaseConfigurator_240"), LocalizationHolder.rm.GetString("DatabaseConfigurator_228"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
      return;
    bool flag1 = false;
    bool flag2 = true;
    bool flag3 = false;
    List<long> objectIDs = new List<long>();
    foreach (TreeNode node in this.tvlnvalidBlobInfo.Nodes)
    {
      if (!flag3)
      {
        if (node.Checked && node.Level == 0)
        {
          long int64 = Convert.ToInt64(node.Tag);
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(int64, false);
            if (objectActualCopy != null)
            {
              try
              {
                objectActualCopy.Delete(0L);
                objectIDs.Add(int64);
                continue;
              }
              catch (Exception ex)
              {
                if (!flag1)
                {
                  List<IMMessageBoxButton> messageBoxButtonList = new List<IMMessageBoxButton>();
                  messageBoxButtonList.Add(new IMMessageBoxButton(LocalizationHolder.rm.GetString("DatabaseConfigurator_241"), DialogResult.Abort));
                  messageBoxButtonList.Add(new IMMessageBoxButton(LocalizationHolder.rm.GetString("DatabaseConfigurator_242"), DialogResult.Yes));
                  messageBoxButtonList.Add(new IMMessageBoxButton(LocalizationHolder.rm.GetString("DatabaseConfigurator_243"), DialogResult.Ignore));
                  messageBoxButtonList.Add(new IMMessageBoxButton(LocalizationHolder.rm.GetString("DatabaseConfigurator_244"), DialogResult.Retry));
                  messageBoxButtonList.Add(new IMMessageBoxButton(LocalizationHolder.rm.GetString("DatabaseConfigurator_245"), DialogResult.No));
                  while (flag2)
                  {
                    switch (IMMessageBox.Show(LocalizationHolder.rm.GetString("DatabaseConfigurator_246"), LocalizationHolder.rm.GetString("DatabaseConfigurator_247"), messageBoxButtonList.ToArray(), IMMessageBoxImage.Question))
                    {
                      case DialogResult.Cancel:
                      case DialogResult.Abort:
                        flag3 = true;
                        goto label_20;
                      case DialogResult.Retry:
                        flag1 = true;
                        goto label_20;
                      case DialogResult.Ignore:
                        goto label_20;
                      case DialogResult.Yes:
                        if (sessionKeeper.Session.GetCustomService(typeof (IFixAttributeService)) is IFixAttributeService customService)
                        {
                          customService.PugreObject(objectActualCopy.ObjectID, sessionKeeper.Session.SessionGUID);
                          objectIDs.Add(int64);
                          goto label_20;
                        }
                        goto label_20;
                      case DialogResult.No:
                        ExceptionHelper.ExceptionService.ShowException(ex);
                        flag2 = true;
                        continue;
                      default:
                        continue;
                    }
                  }
                  continue;
                }
                continue;
              }
            }
            else
              continue;
          }
        }
        else
          continue;
      }
      else
        break;
label_20:;
    }
    if (objectIDs != null && objectIDs.Count > 0)
    {
      DBObjectsEventArgs e1 = new DBObjectsEventArgs("ObjectsRemoved", (IList<long>) objectIDs);
      Holder.NotificationService.FireEvent((object) null, (NotificationEventArgs) e1);
    }
    this.UpdateControls();
  }

  private void NotificationEvent(object sender, NotificationEventArgs e)
  {
    if (e.EventName == "ObjectsRemoved")
    {
      if (!(e is DBObjectsEventArgs objectsEventArgs))
        return;
      foreach (long objectId in (IEnumerable<long>) objectsEventArgs.ObjectIDs)
        this.tvlnvalidBlobInfo.Nodes.RemoveByKey(objectId.ToString());
    }
    else
    {
      if (!(e.EventName == "ObjectsCheckedOut") && !(e.EventName == "ObjectsCheckedIn") || !(e is DBObjectsCheckOutEventArgs checkOutEventArgs))
        return;
      for (int index = 0; index < checkOutEventArgs.ObjectIDs.Count; ++index)
      {
        TreeNode[] treeNodeArray = this.tvlnvalidBlobInfo.Nodes.Find(checkOutEventArgs.ObjectIDs[index].ToString(), true);
        if (treeNodeArray != null && treeNodeArray.Length == 1)
          treeNodeArray[0].Name = checkOutEventArgs.NewObjectIDs[index].ToString();
      }
    }
  }

  private void btnDeleteBlob_Click(object sender, EventArgs e)
  {
    if (MessageBox.Show(LocalizationHolder.rm.GetString("DatabaseConfigurator_248"), LocalizationHolder.rm.GetString("DatabaseConfigurator_228"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
      return;
    List<long> longList = new List<long>();
    bool flag1 = false;
    bool flag2 = true;
    bool flag3 = false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (TreeNode node1 in this.tvlnvalidBlobInfo.Nodes)
      {
        if (!flag1)
        {
          foreach (TreeNode node2 in node1.Nodes)
          {
            if (!flag1)
            {
              if (node2.Checked)
              {
                if (node2.Level == 1)
                {
                  try
                  {
                    InvalidBlobInfo tag = (InvalidBlobInfo) node2.Tag;
                    if (sessionKeeper.Session.GetCustomService(typeof (IFixAttributeService)) is IFixAttributeService customService)
                    {
                      customService.DeleteBlob(tag, sessionKeeper.Session.SessionGUID);
                      longList.Add(tag.blobID);
                      continue;
                    }
                    continue;
                  }
                  catch (Exception ex)
                  {
                    if (!flag3)
                    {
                      List<IMMessageBoxButton> messageBoxButtonList = new List<IMMessageBoxButton>();
                      messageBoxButtonList.Add(new IMMessageBoxButton(LocalizationHolder.rm.GetString("DatabaseConfigurator_241"), DialogResult.Abort));
                      messageBoxButtonList.Add(new IMMessageBoxButton(LocalizationHolder.rm.GetString("DatabaseConfigurator_243"), DialogResult.Ignore));
                      messageBoxButtonList.Add(new IMMessageBoxButton(LocalizationHolder.rm.GetString("DatabaseConfigurator_244"), DialogResult.Retry));
                      messageBoxButtonList.Add(new IMMessageBoxButton(LocalizationHolder.rm.GetString("DatabaseConfigurator_245"), DialogResult.No));
                      while (flag2)
                      {
                        switch (IMMessageBox.Show(LocalizationHolder.rm.GetString("DatabaseConfigurator_246"), LocalizationHolder.rm.GetString("DatabaseConfigurator_249"), messageBoxButtonList.ToArray(), IMMessageBoxImage.Question))
                        {
                          case DialogResult.Cancel:
                          case DialogResult.Abort:
                            flag1 = true;
                            goto label_19;
                          case DialogResult.Retry:
                            flag3 = true;
                            goto label_19;
                          case DialogResult.Ignore:
                            goto label_19;
                          case DialogResult.No:
                            ExceptionHelper.ExceptionService.ShowException(ex);
                            flag2 = true;
                            continue;
                          default:
                            continue;
                        }
                      }
                      continue;
                    }
                    continue;
                  }
                }
                else
                  continue;
              }
              else
                continue;
            }
            else
              break;
label_19:;
          }
        }
        else
          break;
      }
    }
    foreach (long num in longList)
    {
      TreeNode[] treeNodeArray = this.tvlnvalidBlobInfo.Nodes.Find(num.ToString(), true);
      if (treeNodeArray.Length != 0 && treeNodeArray[0].Level != 0)
      {
        if (treeNodeArray[0].Parent.Nodes.Count == 1)
          this.tvlnvalidBlobInfo.Nodes.Remove(treeNodeArray[0].Parent);
        else
          this.tvlnvalidBlobInfo.Nodes.Remove(treeNodeArray[0]);
      }
    }
    this.UpdateControls();
  }

  private void tvlnvalidBlobInfo_AfterSelect(object sender, TreeViewEventArgs e)
  {
    this.btnOpenInNewWindow.Enabled = e.Node.Level == 0;
  }

  private void tvlnvalidBlobInfo_AfterCheck(object sender, TreeViewEventArgs e)
  {
    this.UpdateControls();
    if (!e.Node.Checked || e.Node.Level != 0)
      return;
    foreach (TreeNode node in e.Node.Nodes)
      node.Checked = true;
  }

  private void tvlnvalidBlobInfo_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
  {
    if (e.Node == null || e.Node.Level != 0)
      return;
    this.btnOpenInNewWindow.PerformClick();
  }

  private void btnCheckObject_Click(object sender, EventArgs e)
  {
    this.btnCheckObject.Checked = !this.btnCheckObject.Checked;
    bool flag = this.btnCheckFiles.Checked = this.btnCheckObject.Checked;
    foreach (TreeNode node1 in this.tvlnvalidBlobInfo.Nodes)
    {
      node1.Checked = flag;
      foreach (TreeNode node2 in node1.Nodes)
        node2.Checked = flag;
    }
  }

  private void btnCheckFiles_Click(object sender, EventArgs e)
  {
    this.btnCheckFiles.Checked = !this.btnCheckFiles.Checked;
    bool flag = this.btnCheckFiles.Checked;
    foreach (TreeNode node1 in this.tvlnvalidBlobInfo.Nodes)
    {
      foreach (TreeNode node2 in node1.Nodes)
        node2.Checked = flag;
    }
  }

  protected override void Dispose(bool disposing)
  {
    if (ServicesManager.GetService(typeof (BarManager)) is BarManager service)
    {
      this.tbFix.Renderer = (IToolBarRenderer) new EmptyToolbarRenderer();
      service.RendererChanged -= new EventHandler(this.barManager_RendererChanged);
    }
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FixInvalidBlobsControl));
    this.tbFix = new Intermech.Bars.ToolBar();
    this.btnOpenInNewWindow = new ButtonItem();
    this.btnDeleteObject = new ButtonItem();
    this.btnDeleteBlob = new ButtonItem();
    this.btnCheckObject = new ButtonItem();
    this.btnCheckFiles = new ButtonItem();
    this.tvlnvalidBlobInfo = new TreeView();
    this.SuspendLayout();
    this.tbFix.FullMenus = true;
    this.tbFix.Guid = new Guid("1226a39c-0ebc-4443-8805-a1cb2c01848a");
    this.tbFix.Hidden = false;
    this.tbFix.Items.AddRange(new ToolbarItemBase[5]
    {
      (ToolbarItemBase) this.btnOpenInNewWindow,
      (ToolbarItemBase) this.btnDeleteObject,
      (ToolbarItemBase) this.btnDeleteBlob,
      (ToolbarItemBase) this.btnCheckObject,
      (ToolbarItemBase) this.btnCheckFiles
    });
    this.tbFix.Location = new Point(0, 0);
    this.tbFix.Name = "tbFix";
    this.tbFix.Size = new Size(376, 24);
    this.tbFix.TabIndex = 0;
    this.tbFix.Text = "toolBar1";
    this.btnOpenInNewWindow.CommandName = "OpenInNewWindow";
    this.btnOpenInNewWindow.Enabled = false;
    this.btnOpenInNewWindow.ToolTipText = "Открыть в новом окне";
    this.btnOpenInNewWindow.Click += new EventHandler(this.biOpenInNewWindow_Click);
    this.btnDeleteObject.BeginGroup = true;
    this.btnDeleteObject.CommandName = "DeleteObject";
    this.btnDeleteObject.Enabled = false;
    this.btnDeleteObject.Image = (Image) componentResourceManager.GetObject("btnDeleteObject.Image");
    this.btnDeleteObject.ToolTipText = "Удалить объект";
    this.btnDeleteObject.Click += new EventHandler(this.btnDeleteObject_Click);
    this.btnDeleteBlob.CommandName = "DeleteBlob";
    this.btnDeleteBlob.Enabled = false;
    this.btnDeleteBlob.Image = (Image) Intermech.DatabaseConfigurator.Properties.Resources.file_delete;
    this.btnDeleteBlob.ToolTipText = "Удалить блоб";
    this.btnDeleteBlob.Click += new EventHandler(this.btnDeleteBlob_Click);
    this.btnCheckObject.BeginGroup = true;
    this.btnCheckObject.CommandName = "btnSelect";
    this.btnCheckObject.Image = (Image) componentResourceManager.GetObject("btnCheckObject.Image");
    this.btnCheckObject.ToolTipText = "Выделить всё";
    this.btnCheckObject.Click += new EventHandler(this.btnCheckObject_Click);
    this.btnCheckFiles.CommandName = "btnRefresh";
    this.btnCheckFiles.Image = (Image) componentResourceManager.GetObject("btnCheckFiles.Image");
    this.btnCheckFiles.ToolTipText = "Выделить все файлы";
    this.btnCheckFiles.Click += new EventHandler(this.btnCheckFiles_Click);
    this.tvlnvalidBlobInfo.CheckBoxes = true;
    this.tvlnvalidBlobInfo.Dock = DockStyle.Fill;
    this.tvlnvalidBlobInfo.FullRowSelect = true;
    this.tvlnvalidBlobInfo.HideSelection = false;
    this.tvlnvalidBlobInfo.Location = new Point(0, 24);
    this.tvlnvalidBlobInfo.Name = "tvlnvalidBlobInfo";
    this.tvlnvalidBlobInfo.Size = new Size(376, 209);
    this.tvlnvalidBlobInfo.TabIndex = 2;
    this.tvlnvalidBlobInfo.NodeMouseDoubleClick += new TreeNodeMouseClickEventHandler(this.tvlnvalidBlobInfo_NodeMouseDoubleClick);
    this.tvlnvalidBlobInfo.AfterCheck += new TreeViewEventHandler(this.tvlnvalidBlobInfo_AfterCheck);
    this.tvlnvalidBlobInfo.AfterSelect += new TreeViewEventHandler(this.tvlnvalidBlobInfo_AfterSelect);
    this.AllowedStates = DockLocation.Bottom;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.tvlnvalidBlobInfo);
    this.Controls.Add((Control) this.tbFix);
    this.HideOnClose = true;
    this.Name = nameof (FixInvalidBlobsControl);
    this.Size = new Size(376, 233);
    this.Text = "Нечитаемые файлы";
    this.ResumeLayout(false);
  }
}
