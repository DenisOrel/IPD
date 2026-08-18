// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.NotifyView
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using DevExpress.IM.Utils;
using DevExpress.IM.XtraTreeList;
using DevExpress.IM.XtraTreeList.Columns;
using DevExpress.IM.XtraTreeList.Nodes;
using Intermech.Bars;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Workflow;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Client;

[ViewDescriptionProvider(typeof (NotifyView.NotifyViewDescriptionProvider))]
public class NotifyView : UserControl, IView
{
  private readonly string _caption = LocalizationHolder.rm.GetString("Workflow.Client_75");
  private readonly int _imageIndex = -1;
  private readonly MyElement _currentUser;
  private bool _userIsPresent;
  private readonly bool _isAdmin;
  private bool _isViewForOneObject;
  private long _objectID;
  private long _ID = -1;
  private List<Notify> _notifies = new List<Notify>();
  private long _notifyID = -1;
  private List<long> _objectsIDs = new List<long>();
  private List<long> _ids = new List<long>();
  private Dictionary<long, long> _notificationsForObjects = new Dictionary<long, long>();
  private TimeSpan _currentTimeZoneOffset;
  private INotifySubscriberService _notifyService;
  private IContainer components;
  private TreeList treeList1;
  private TreeListColumn treeListColumn1;
  private TreeListColumn treeListColumn2;
  private TreeListColumn treeListColumn3;
  private ToolTipController toolTipController1;
  protected Intermech.Bars.ToolBar tbViewBar;
  protected ButtonItem bNotify;
  protected ButtonItem bEdit;
  protected ButtonItem bDelete;
  protected ButtonItem bNotifyUsers;
  private Panel panel1;
  private TreeListColumn treeListColumn4;

  public NotifyView()
  {
    this.InitializeComponent();
    this.treeList1.MenuOptions = MenuOptionsFlags.None;
    if (ServicesManager.GetService(typeof (ICurrentUserAndRole)) is ICurrentUserAndRole service1)
    {
      this._isAdmin = service1.IsAdmin;
      this.bNotifyUsers.Visible = this._isAdmin;
      this._currentUser = new MyElement((object) service1.UserID, service1.UserName, (object) null);
    }
    if (ApplicationServices.Container.GetService(typeof (INamedImageList)) is INamedImageList service2)
      this._imageIndex = service2.ImageIndex("imgCopies");
    ICategoryTypeIconService service3 = (ICategoryTypeIconService) ApplicationServices.Container.GetService(typeof (ICategoryTypeIconService));
    if (service3 != null)
    {
      Icon icon = service3.GetIcon(4, MetaDataHelper.GetObjectTypeID("cad00002-306c-11d8-b4e9-00304f19f545"));
      if (icon != null)
      {
        ImageList imageList = new ImageList()
        {
          ColorDepth = ColorDepth.Depth32Bit
        };
        imageList.Images.Add(icon);
        this.treeList1.StateImageList = imageList;
      }
    }
    if (!(ApplicationServices.Container.GetService(typeof (BarManager)) is BarManager service4))
      return;
    service4.RendererChanged += new EventHandler(this.RendererChanged);
    this.RendererChanged((object) service4, EventArgs.Empty);
  }

  private void FillControls()
  {
    this.treeList1.Nodes.Clear();
    this._userIsPresent = false;
    this.treeList1.BeginUpdate();
    foreach (Notify notify in this._notifies)
    {
      if (notify.UserID == Convert.ToInt64(this._currentUser.Value))
        this._userIsPresent = true;
      if (this._isViewForOneObject)
      {
        TreeListNode treeListNode = this.treeList1.AppendNode((object) new object[4]
        {
          (object) notify.UserName,
          (object) (notify.Date + this._currentTimeZoneOffset),
          null,
          (object) notify.Comment
        }, (TreeListNode) null);
        treeListNode.Tag = (object) notify;
        if (this.treeList1.StateImageList != null)
          treeListNode.StateImageIndex = 0;
      }
      else
      {
        TreeListNode treeListNode = this.treeList1.AppendNode((object) new object[4]
        {
          (object) notify.UserName,
          null,
          null,
          null
        }, (TreeListNode) null);
        treeListNode.Tag = (object) notify;
        if (this.treeList1.StateImageList != null)
          treeListNode.StateImageIndex = 0;
      }
    }
    this.treeList1.EndUpdate();
    this.treeList1_FocusedNodeChanged((object) this, (FocusedNodeChangedEventArgs) null);
  }

  private void LoadData()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (MetaDataHelper.GetAttribute4ObjectType(wfConsts.NotifyObjectTypeID, wfConsts.AttrListAttributesID) != null)
      {
        this._notifies = new List<Notify>();
        this.bDelete.Enabled = false;
        this.bEdit.Enabled = false;
        this.bNotify.Enabled = false;
        this.bNotifyUsers.Enabled = false;
        this.treeList1.Visible = false;
        return;
      }
      this._currentTimeZoneOffset = sessionKeeper.Session.TimeZoneOffset;
      this._notifyService = sessionKeeper.Session.GetCustomService(typeof (INotifySubscriberService)) as INotifySubscriberService;
      if (this._notifyService == null)
        throw new KernelException("Не найден сервис INotifySubscriberService");
      if (this._isViewForOneObject)
      {
        this._notifyID = -1L;
        string errorMessage;
        this._notifies = this._notifyService.GetNotifications(sessionKeeper.Session.SessionGUID, this._ID, ref this._notifyID, out errorMessage);
        if (this._notifies.Count == 0)
        {
          if (!string.IsNullOrEmpty(errorMessage))
          {
            int num = (int) MessageBox.Show(errorMessage, "Ошибка чтения данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          }
        }
      }
      else
      {
        this._notificationsForObjects = this._notifyService.GetNotificationsForObjects(sessionKeeper.Session.SessionGUID, this._ids);
        this._notifies = !this._notificationsForObjects.Values.Contains<long>(0L) ? this._notifyService.GetCommonNotifies(sessionKeeper.Session.SessionGUID, this._notificationsForObjects) : new List<Notify>();
      }
    }
    this.FillControls();
  }

  public void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    this._isViewForOneObject = items.Count == 1;
    if (this._isViewForOneObject)
    {
      this._objectsIDs.Clear();
      this._ids.Clear();
      IDBTypedObjectID itemData = (IDBTypedObjectID) items.GetItemData(0, typeof (IDBTypedObjectID));
      if (itemData == null)
      {
        this._ID = -1L;
      }
      else
      {
        this._objectID = itemData.ObjectID;
        this._ID = itemData.ID;
      }
    }
    else
    {
      List<long> longList1 = new List<long>(items.Count);
      List<long> longList2 = new List<long>();
      for (int index = 0; index < items.Count; ++index)
      {
        IDBTypedObjectID itemData = (IDBTypedObjectID) items.GetItemData(index, typeof (IDBTypedObjectID));
        if (itemData == null)
        {
          this._objectsIDs.Clear();
          this._ids.Clear();
          this._ID = -1L;
          this._objectID = 0L;
          return;
        }
        longList1.Add(itemData.ObjectID);
        if (!longList2.Contains(itemData.ID))
          longList2.Add(itemData.ID);
      }
      if (longList2.Count == 1)
      {
        this._isViewForOneObject = true;
        IDBTypedObjectID itemData = (IDBTypedObjectID) items.GetItemData(0, typeof (IDBTypedObjectID));
        this._objectID = itemData.ObjectID;
        this._ID = itemData.ID;
        this._objectsIDs.Clear();
        this._ids.Clear();
        this._isViewForOneObject = true;
      }
      else
      {
        this._objectsIDs = longList1;
        this._ids = longList2;
        this._objectID = 0L;
        this._ID = -1L;
      }
    }
  }

  public void Activate(IView previousView) => this.LoadData();

  public void Deactivate(IView nextView)
  {
  }

  public string Caption => this._caption;

  public int ImageIndex => this._imageIndex;

  public int OrderID => 36;

  protected void RendererChanged(object sender, EventArgs e)
  {
    this.tbViewBar.Renderer = (sender as BarManager).Renderer;
  }

  private void SetButtons()
  {
    if (this._isViewForOneObject)
    {
      this.bNotify.Enabled = !this._userIsPresent;
      bool flag = this.EditEnable();
      this.bEdit.Enabled = flag;
      this.bDelete.Enabled = flag;
    }
    else
    {
      this.bNotify.Enabled = true;
      this.bEdit.Enabled = false;
      this.bDelete.Enabled = false;
    }
  }

  private bool EditEnable()
  {
    TreeListNode focusedNode = this.treeList1.FocusedNode;
    if (focusedNode == null || focusedNode.Tag == null || !(focusedNode.Tag is Notify tag))
      return false;
    return this._isAdmin || tag.UserID == Convert.ToInt64(this._currentUser.Value);
  }

  private void DeleteNotify(SessionKeeper sk)
  {
    Notify tag = (Notify) this.treeList1.FocusedNode.Tag;
    this._notifies.Remove(tag);
    IDBObject dbObject = sk.Session.GetObject(this._notifyID).CheckOut();
    IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(new Guid("cad00628-306c-11d8-b4e9-00304f19f545"));
    int num = -1;
    for (int index = 0; index < attributeByGuid.ValuesCount; ++index)
    {
      if ((long) Convert.ToInt32(attributeByGuid.Values[index]) == tag.UserID)
      {
        num = index;
        break;
      }
    }
    switch (num)
    {
      case -1:
        return;
      case 0:
        if (attributeByGuid.ValuesCount == 1)
        {
          dbObject.CheckIn();
          sk.Session.GetObject(this._notifyID).Delete(0L);
          this._notifyID = -1L;
          return;
        }
        break;
    }
    attributeByGuid.Index = num;
    attributeByGuid.DeleteValue();
    dbObject.CheckIn();
  }

  private void bNotify_Click(object sender, EventArgs e)
  {
    if (this._isViewForOneObject)
    {
      NotifyRules notifyRules = new NotifyRules(NotifyOptions.None, (List<int>) null, new long[1]
      {
        this._objectID
      }, string.Empty);
      if (notifyRules.ShowDialog() != DialogResult.OK)
        return;
      this._notifies.Add(new Notify()
      {
        UserID = Convert.ToInt64(this._currentUser.Value),
        UserName = this._currentUser.Caption,
        Attributes = notifyRules.Attributes,
        Date = DateTime.UtcNow,
        Options = notifyRules.Options,
        Comment = notifyRules.Comment
      });
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        this._notifyID = this._notifyService.AddNotify(sessionKeeper.Session.SessionGUID, this._ID, this._notifyID, this._notifies);
      this.FillControls();
    }
    else
    {
      NotifyRules notifyRules = new NotifyRules(NotifyOptions.None, (List<int>) null, this._objectsIDs.ToArray(), string.Empty);
      if (notifyRules.ShowDialog() == DialogResult.OK)
      {
        List<string> stringList;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          Notify notify = new Notify()
          {
            Attributes = notifyRules.Attributes,
            Comment = notifyRules.Comment,
            Date = DateTime.UtcNow,
            Options = notifyRules.Options,
            UserID = Convert.ToInt64(this._currentUser.Value),
            UserName = this._currentUser.Caption
          };
          stringList = this._notifyService.AddNotifies(sessionKeeper.Session.SessionGUID, this._ids, this._notificationsForObjects, new List<Notify>()
          {
            notify
          });
        }
        foreach (string text in stringList)
        {
          int num = (int) MessageBox.Show(text, LocalizationHolder.rm.GetString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
      }
      this.LoadData();
    }
  }

  private void bNotifyUsers_Click(object sender, EventArgs e)
  {
    object[] objArray = SelectionWindow.Select(LocalizationHolder.rm.GetString("Workflow.Client_76"), LocalizationHolder.rm.GetString("Workflow.Client_77"), (IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(MetaDataHelper.GetObjectTypeID("cad00002-306c-11d8-b4e9-00304f19f545")), typeof (IDBObjectID), SelectionOptions.SelectObjects);
    if (objArray == null || objArray.Length == 0)
      return;
    List<(string, long)> valueTupleList1 = new List<(string, long)>();
    for (int index = 0; index < objArray.Length; ++index)
    {
      if (objArray[index] is IDBObjectID dbObjectId)
        valueTupleList1.Add((dbObjectId.Caption, dbObjectId.Value));
    }
    List<(string, long)> valueTupleList2 = new List<(string, long)>();
    foreach ((string, long) valueTuple in valueTupleList1)
    {
      foreach (Notify notify in this._notifies)
      {
        if (valueTuple.Item2 == notify.UserID)
        {
          valueTupleList2.Add(valueTuple);
          break;
        }
      }
    }
    if (this._isViewForOneObject)
    {
      if (valueTupleList2.Count > 0)
      {
        string format = valueTupleList2.Count == 1 ? "Пользователь {0} уже подписан на уведомления для этого объекта" : "Пользователи {0} уже подписаны на уведомления для этого объекта";
        StringBuilder stringBuilder = new StringBuilder();
        for (int index = 0; index < valueTupleList2.Count; ++index)
        {
          if (index > 0)
            stringBuilder.Append(", ");
          stringBuilder.Append(valueTupleList2[index].Item1);
        }
        int num = (int) MessageBox.Show(string.Format(format, (object) stringBuilder), "Ошибка подписки пользователей", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      else
      {
        NotifyRules notifyRules = new NotifyRules(NotifyOptions.None, (List<int>) null, new long[1]
        {
          this._objectID
        }, string.Empty);
        if (notifyRules.ShowDialog() != DialogResult.OK)
          return;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          foreach ((string, long) valueTuple in valueTupleList1)
            this._notifies.Add(new Notify(valueTuple.Item2, valueTuple.Item1, DateTime.UtcNow, notifyRules.Options, notifyRules.Attributes, notifyRules.Comment));
          this._notifyID = this._notifyService.AddNotify(sessionKeeper.Session.SessionGUID, this._ID, this._notifyID, this._notifies);
          this.FillControls();
        }
      }
    }
    else
    {
      NotifyRules notifyRules = new NotifyRules(NotifyOptions.None, (List<int>) null, this._objectsIDs.ToArray(), string.Empty);
      if (notifyRules.ShowDialog() == DialogResult.OK)
      {
        List<Notify> notifies = new List<Notify>();
        foreach ((string, long) valueTuple in valueTupleList1)
          notifies.Add(new Notify(valueTuple.Item2, valueTuple.Item1, DateTime.UtcNow, notifyRules.Options, notifyRules.Attributes, notifyRules.Comment));
        List<string> stringList;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          stringList = this._notifyService.AddNotifies(sessionKeeper.Session.SessionGUID, this._ids, this._notificationsForObjects, notifies);
        foreach (string text in stringList)
        {
          int num = (int) MessageBox.Show(text, LocalizationHolder.rm.GetString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
      }
      this.LoadData();
    }
  }

  private void treeList1_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
  {
    this.SetButtons();
  }

  private void bDelete_Click(object sender, EventArgs e)
  {
    using (SessionKeeper sk = new SessionKeeper())
    {
      this.DeleteNotify(sk);
      this.FillControls();
    }
  }

  private void bEdit_Click(object sender, EventArgs e)
  {
    TreeListNode focusedNode = this.treeList1.FocusedNode;
    if (focusedNode == null || focusedNode.Tag == null || !(focusedNode.Tag is Notify tag))
      return;
    NotifyRules notifyRules = new NotifyRules(tag.Options, tag.Attributes, new long[1]
    {
      this._objectID
    }, tag.Comment);
    if (notifyRules.ShowDialog() != DialogResult.OK)
      return;
    int index = this._notifies.IndexOf(tag);
    if (index >= 0)
    {
      this._notifies[index].Options = notifyRules.Options;
      this._notifies[index].Attributes = notifyRules.Attributes;
      this._notifies[index].Date = DateTime.UtcNow;
      this._notifies[index].Comment = notifyRules.Comment;
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this._notifyID = this._notifyService.AddNotify(sessionKeeper.Session.SessionGUID, this._ID, this._notifyID, this._notifies);
      this.FillControls();
    }
  }

  private void treeList1_CustomDrawNodeCell(object sender, CustomDrawNodeCellEventArgs e)
  {
    if (e.Column != this.treeListColumn3 || e.Node == null || e.Node.Tag == null || !(e.Node.Tag is Notify tag))
      return;
    Brush brush1 = this.treeList1.FocusedNode == e.Node ? SystemBrushes.Highlight : SystemBrushes.Window;
    e.Graphics.FillRectangle(brush1, e.Bounds);
    INamedImageList service = (INamedImageList) ApplicationServices.Container.GetService(typeof (INamedImageList));
    if (service != null)
    {
      int num1 = 5;
      int height1 = e.Bounds.Height;
      Size imageSize = service.ImageList.ImageSize;
      int height2 = imageSize.Height;
      float num2 = (float) (height1 - height2) / 2f;
      Point point = new Point(e.Bounds.Left + num1, e.Bounds.Top + (int) num2);
      foreach (int num3 in Enum.GetValues(typeof (NotifyOptions)))
      {
        if (num3 != 0 && (tag.Options & (NotifyOptions) num3) == (NotifyOptions) num3)
        {
          NotifyOptionsAttribute[] customAttributes = (NotifyOptionsAttribute[]) typeof (NotifyOptions).GetField(((NotifyOptions) num3).ToString()).GetCustomAttributes(typeof (NotifyOptionsAttribute), false);
          if (customAttributes.Length == 1)
          {
            e.Graphics.DrawImage(service.ImageList.Images[service.ImageIndex(customAttributes[0].ImageName)], point);
            ref Point local = ref point;
            int x = local.X;
            imageSize = service.ImageList.ImageSize;
            int num4 = imageSize.Width + num1;
            local.X = x + num4;
          }
        }
      }
    }
    else
    {
      Brush brush2 = this.treeList1.FocusedNode == e.Node ? SystemBrushes.HighlightText : SystemBrushes.WindowText;
      string s = string.Empty;
      foreach (int num in Enum.GetValues(typeof (NotifyOptions)))
      {
        if (num != 0 && (tag.Options & (NotifyOptions) num) == (NotifyOptions) num)
        {
          NotifyOptionsAttribute[] customAttributes = (NotifyOptionsAttribute[]) typeof (NotifyOptions).GetField(((NotifyOptions) num).ToString()).GetCustomAttributes(typeof (NotifyOptionsAttribute), false);
          if (customAttributes.Length == 1)
            s = $"{s}{customAttributes[0].ShortName} ";
        }
      }
      float num5 = (float) (e.Bounds.Height - e.Style.Font.Height) / 2f;
      e.Graphics.DrawString(s, e.Style.Font, brush2, (float) e.Bounds.Left, (float) e.Bounds.Top + num5);
    }
    e.Handled = true;
  }

  private void ToolTipController_GetActiveObjectInfo(
    object sender,
    ToolTipControllerGetActiveObjectInfoEventArgs e)
  {
    TreeListHitInfo hitInfo = this.treeList1.GetHitInfo(e.ControlMousePosition);
    if (hitInfo.Node?.Tag == null || !(hitInfo.Node.Tag is Notify tag))
      return;
    string empty = string.Empty;
    string format = "\n - {0}";
    string text = !this._isViewForOneObject ? empty + string.Format(LocalizationHolder.rm.GetString("Workflow.Client_100"), (object) tag.UserName) : empty + string.Format(LocalizationHolder.rm.GetString("Workflow.Client_4"), (object) tag.UserName, (object) (tag.Date + this._currentTimeZoneOffset));
    foreach (int num in Enum.GetValues(typeof (NotifyOptions)))
    {
      if (num != 0 && (tag.Options & (NotifyOptions) num) == (NotifyOptions) num)
      {
        if (num == 32 /*0x20*/)
        {
          if (tag.Attributes != null && tag.Attributes.Count > 0)
          {
            text += string.Format(format, (object) EnumDescConverter.GetEnumDescription((Enum) (NotifyOptions) num));
            text += ":";
            for (int index = 0; index < tag.Attributes.Count; ++index)
              text += $"\n\t{Convert.ToChar(183)} {MetaDataHelper.GetAttributeTypeName(tag.Attributes[index])}";
          }
        }
        else
          text += string.Format(format, (object) EnumDescConverter.GetEnumDescription((Enum) (NotifyOptions) num));
      }
    }
    e.Info = new ToolTipControlInfo((object) tag.GetHashCode(), text);
  }

  private void buttonItem1_Click(object sender, EventArgs e)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      INotifySubscriberService customService = sessionKeeper.Session.GetCustomService(typeof (INotifySubscriberService)) as INotifySubscriberService;
      Notify notify = new Notify(1271673L, "Petrov", NotifyOptions.CheckOut | NotifyOptions.Delete | NotifyOptions.Version | NotifyOptions.AttributeValueChanged, new List<int>()
      {
        17491,
        1033
      }, "blahblah");
      Guid sessionGuid = sessionKeeper.Session.SessionGUID;
      Notify deletingNotify = notify;
      customService.DeleteNotificationForObject(sessionGuid, 1311260L, deletingNotify);
    }
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      if (this.components != null)
        this.components.Dispose();
      if (ApplicationServices.Container.GetService(typeof (BarManager)) is BarManager service)
      {
        this.tbViewBar.Renderer = (IToolBarRenderer) new EmptyToolbarRenderer();
        service.RendererChanged -= new EventHandler(this.RendererChanged);
      }
    }
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (NotifyView));
    this.treeList1 = new TreeList();
    this.treeListColumn1 = new TreeListColumn();
    this.treeListColumn2 = new TreeListColumn();
    this.treeListColumn3 = new TreeListColumn();
    this.treeListColumn4 = new TreeListColumn();
    this.toolTipController1 = new ToolTipController();
    this.tbViewBar = new Intermech.Bars.ToolBar();
    this.bNotify = new ButtonItem();
    this.bNotifyUsers = new ButtonItem();
    this.bEdit = new ButtonItem();
    this.bDelete = new ButtonItem();
    this.panel1 = new Panel();
    this.treeList1.BeginInit();
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.treeList1, "treeList1");
    this.treeList1.Columns.AddRange(new TreeListColumn[4]
    {
      this.treeListColumn1,
      this.treeListColumn2,
      this.treeListColumn3,
      this.treeListColumn4
    });
    this.treeList1.Name = "treeList1";
    this.treeList1.Styles.AddReplace("FocusedCell", (object) new ViewStyle("FocusedCell", "TreeList", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseImage, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Highlight, SystemColors.HighlightText));
    this.treeList1.ToolTipController = this.toolTipController1;
    this.treeList1.FocusedNodeChanged += new FocusedNodeChangedEventHandler(this.treeList1_FocusedNodeChanged);
    this.treeList1.CustomDrawNodeCell += new CustomDrawNodeCellEventHandler(this.treeList1_CustomDrawNodeCell);
    componentResourceManager.ApplyResources((object) this.treeListColumn1, "treeListColumn1");
    this.treeListColumn1.Name = "treeListColumn1";
    componentResourceManager.ApplyResources((object) this.treeListColumn2, "treeListColumn2");
    this.treeListColumn2.Name = "treeListColumn2";
    componentResourceManager.ApplyResources((object) this.treeListColumn3, "treeListColumn3");
    this.treeListColumn3.Name = "treeListColumn3";
    componentResourceManager.ApplyResources((object) this.treeListColumn4, "treeListColumn4");
    this.treeListColumn4.Name = "treeListColumn4";
    this.toolTipController1.Style = new ViewStyle("ToolTip style");
    this.toolTipController1.GetActiveObjectInfo += new ToolTipControllerGetActiveObjectInfoEventHandler(this.ToolTipController_GetActiveObjectInfo);
    this.tbViewBar.FullMenus = true;
    this.tbViewBar.Guid = new Guid("2337b74f-5d86-4565-809f-c0fa244e17e8");
    this.tbViewBar.Hidden = false;
    this.tbViewBar.Items.AddRange(new ToolbarItemBase[4]
    {
      (ToolbarItemBase) this.bNotify,
      (ToolbarItemBase) this.bNotifyUsers,
      (ToolbarItemBase) this.bEdit,
      (ToolbarItemBase) this.bDelete
    });
    componentResourceManager.ApplyResources((object) this.tbViewBar, "tbViewBar");
    this.tbViewBar.Name = "tbViewBar";
    this.tbViewBar.Overflow = ToolBarOverflow.Wrap;
    componentResourceManager.ApplyResources((object) this.bNotify, "bNotify");
    this.bNotify.Icon = (Icon) componentResourceManager.GetObject("bNotify.Icon");
    this.bNotify.Click += new EventHandler(this.bNotify_Click);
    componentResourceManager.ApplyResources((object) this.bNotifyUsers, "bNotifyUsers");
    this.bNotifyUsers.Icon = (Icon) componentResourceManager.GetObject("bNotifyUsers.Icon");
    this.bNotifyUsers.Click += new EventHandler(this.bNotifyUsers_Click);
    componentResourceManager.ApplyResources((object) this.bEdit, "bEdit");
    this.bEdit.Icon = (Icon) componentResourceManager.GetObject("bEdit.Icon");
    this.bEdit.Click += new EventHandler(this.bEdit_Click);
    this.bDelete.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.bDelete, "bDelete");
    this.bDelete.Icon = (Icon) componentResourceManager.GetObject("bDelete.Icon");
    this.bDelete.Click += new EventHandler(this.bDelete_Click);
    this.panel1.Controls.Add((Control) this.treeList1);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.tbViewBar);
    this.Name = nameof (NotifyView);
    this.treeList1.EndInit();
    this.panel1.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private sealed class NotifyViewDescriptionProvider : BaseViewDescriptionProvider
  {
    public override ViewDescription DoGetViewDescription(
      ISelectedItems selectedItems,
      System.IServiceProvider serviceProvider)
    {
      if (!(serviceProvider.GetService(typeof (INamedImageList)) is INamedImageList service))
        service = ServicesManager.GetService(typeof (INamedImageList)) as INamedImageList;
      INamedImageList namedImageList = service;
      return new ViewDescription()
      {
        Caption = LocalizationHolder.rm.GetString("Workflow.Client_75"),
        ImageIndex = namedImageList.ImageIndex("imgCopies"),
        OrderID = 36
      };
    }
  }
}
