// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.Holder
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Docking;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Plugins;
using Intermech.Interfaces.Workflow;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Views;
using Intermech.PropertyEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

public class Holder : BaseHolder
{
  public static Guid CategorySchemesGuid = new Guid("{CBBA69BF-FE07-4943-880C-26F69DE5B745}");
  public static int CategorySchemesID;
  private static Guid CategoryAttachmentsGuid = new Guid("{E5FEED8B-96FA-429b-B142-BB76B987FC76}");
  public static int CategoryAttachmentsID;
  public static int LowImageIndex = -1;
  public static int HighImageIndex = -1;
  public static int AttachsImageIndex = -1;
  public static int MailImageIndex = -1;
  public static int MessagesImageIndex = -1;
  public static int AnswerImageIndex = -1;
  public static int ViewImageIndex = -1;
  public static int CloseBtnImageIndex = -1;
  public static int LaunchProcessImageIndex = -1;
  public static RecentList RecentLaunched = (RecentList) null;
  public static RecentList RecentSchemes = (RecentList) null;
  public static int ParticipantVariableIndex = -1;
  public static Guid wfEditorDockGuid = new Guid("{2EF128CC-B6C4-4186-88FF-1E04DA77A055}");
  public static int[] ActivityResultImageIndex = new int[2]
  {
    -1,
    -1
  };
  public static EditorSettings EditorSettings = (EditorSettings) null;
  public static EditorsList Editors = new EditorsList();
  public static int TaskRejectedIndex = -1;
  public static int RejectWOImageIndex = -1;
  public static VarTypeImageIndex VarTypeImageIndex = new VarTypeImageIndex();
  private static ImageList _usersImageList = new ImageList();
  public static int IconsUserImageIndex = -1;
  public static int IconsGroupImageIndex = -1;
  public static int IconsRankImageIndex = -1;
  public static int UserImageIndex = -1;
  public static int GroupImageIndex = -1;
  public static int RankImageIndex = -1;
  private static NavigatorTreeView _lastMailTree = (NavigatorTreeView) null;
  public static bool IsInboxActive = false;
  public static bool ShowOnlyBaseVersion = false;
  public static bool ShowOnlyBaseVersionInStartProcess = false;
  private static int _schemeImageIndex = -2;
  private static Icon _schemeIcon = (Icon) null;
  public static int SchemeNamedImageIndex = -1;
  public static int SchemeGroupNamedImageIndex = -1;
  private static string _workflowTempPath = "";
  private static bool? _isAdmin = new bool?();
  private static Image _loadingImage = (Image) null;
  private static Image _questionImage = (Image) null;
  private static Image _warningImage = (Image) null;
  private static VersionsRule _AllVersionsRule = (VersionsRule) null;
  private static ImageList _validatedImageList = new ImageList();

  public static ImageList UsersImageList => Holder._usersImageList;

  public static NavigatorTreeView LastMailTree
  {
    get => Holder._lastMailTree;
    set
    {
      if (Holder._lastMailTree != null)
        Holder._lastMailTree.Disposed -= new EventHandler(Holder.lastMailTree_Disposed);
      Holder._lastMailTree = value;
      Holder._lastMailTree.Disposed += new EventHandler(Holder.lastMailTree_Disposed);
    }
  }

  private static void lastMailTree_Disposed(object sender, EventArgs e)
  {
    Holder._lastMailTree = (NavigatorTreeView) null;
  }

  public new static void Init(IPackage plugin, System.IServiceProvider serviceProvider)
  {
    if (BaseHolder.Inited(typeof (Holder)))
      return;
    BaseHolder.Init(plugin, serviceProvider);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      wfConsts.Init(sessionKeeper.Session);
    try
    {
      Directory.Delete(Holder.WorkflowTempPath, true);
    }
    catch
    {
    }
    ResourceFuncs.ExtractResourcesFolder(typeof (Holder).Assembly, "templates", Holder.WorkflowTempPath);
    Holder.CategorySchemesID = BaseHolder.GuidMapper[Holder.CategorySchemesGuid];
    if (Holder.CategorySchemesID == 0)
      Holder.CategorySchemesID = BaseHolder.GuidMapper.Register(Holder.CategorySchemesGuid);
    Holder.CategoryAttachmentsID = BaseHolder.GuidMapper[Holder.CategoryAttachmentsGuid];
    if (Holder.CategoryAttachmentsID == 0)
      Holder.CategoryAttachmentsID = BaseHolder.GuidMapper.Register(Holder.CategoryAttachmentsGuid);
    BaseHolder.Factory.AddNodeType(1, wfConsts.SchemeCategoriesID, typeof (SchemesNode));
    BaseHolder.Factory.AddViewsProvider(Holder.CategorySchemesID, (IViewsProvider) new SchemesViewsProvider());
    BaseHolder.Factory.AddNodeType(Holder.CategorySchemesID, typeof (SchemesRootNode));
    BaseHolder.Factory.AddCommandsProvider(Holder.CategorySchemesID, (ICommandsProvider) new SchemesRootCommandProvider());
    BaseHolder.Factory.AddViewsProvider(1, wfConsts.SchemeCategoriesID, (IViewsProvider) new SchemesViewsProvider());
    Icon icon1 = BaseHolder.IconService.GetIcon(4, wfConsts.SchemesTypeID);
    BaseHolder.IconService.AddIcon(icon1, Holder.CategorySchemesID, 0);
    if (Holder.RecentLaunched == null)
    {
      Holder.RecentLaunched = new RecentList("RecentLaunched");
      Holder.RecentLaunched.Load();
    }
    if (Holder.RecentSchemes == null)
    {
      Holder.RecentSchemes = new RecentList("RecentSchemes");
      Holder.RecentSchemes.Load();
    }
    Holder.EditorSettings = EditorSettings.Load();
    if (BaseHolder.NamedList.ImageIndex("wfNext") == -1)
    {
      BaseHolder.LoadImages(new string[24]
      {
        "next",
        "back",
        "accept",
        "reject",
        "view.ico",
        "process",
        "delete",
        "undelete",
        "mail_settings.ico",
        "abort",
        "launch",
        "low",
        "high",
        "process_completed",
        "process_terminated",
        "partvar",
        "rejected",
        "attach_add.ico",
        "attach_remove.ico",
        "attach.ico",
        "messages.ico",
        "answer.ico",
        "closebtn",
        "forum.ico"
      }, new string[24]
      {
        "wfNext",
        "wfBack",
        "wfAcceptWO",
        "wfRejectWO",
        "wfViewProcess",
        "wfProcess",
        "wfDelete",
        "wfUndelete",
        "wfSettings",
        "wfAbort",
        "wfLaunch",
        "wfLow",
        "wfHigh",
        "process_completed",
        "process_terminated",
        "partvar",
        "wfRejected",
        "wfAddAttach",
        "wfRemoveAttach",
        "wfAttach",
        "wfMessages",
        "wfAnswer",
        "wfCloseBtn",
        "forum"
      });
      Holder.LowImageIndex = BaseHolder.NamedList.ImageIndex("wfLow");
      Holder.HighImageIndex = BaseHolder.NamedList.ImageIndex("wfHigh");
      Holder.AttachsImageIndex = BaseHolder.NamedList.ImageIndex("wfAttach");
      Holder.MessagesImageIndex = BaseHolder.NamedList.ImageIndex("wfMessages");
      Holder.AnswerImageIndex = BaseHolder.NamedList.ImageIndex("wfAnswer");
      Holder.ViewImageIndex = BaseHolder.NamedList.ImageIndex("imgView");
      Holder.CloseBtnImageIndex = BaseHolder.NamedList.ImageIndex("wfCloseBtn");
      Holder.LaunchProcessImageIndex = BaseHolder.NamedList.ImageIndex("wfLaunch");
      Holder.IconsUserImageIndex = BaseHolder.IconService.IndexOf(4, wfConsts.UserTypeID);
      Holder.IconsGroupImageIndex = BaseHolder.IconService.IndexOf(4, wfConsts.GroupTypeID);
      Holder.IconsRankImageIndex = BaseHolder.IconService.IndexOf(4, wfConsts.RanksTypeID);
      Holder.UsersImageList.Images.Add(BaseHolder.ImageTo16x16(BaseHolder.IconService.ImageList.Images[Holder.IconsUserImageIndex]));
      Holder.UsersImageList.Images.Add(BaseHolder.ImageTo16x16(BaseHolder.IconService.ImageList.Images[Holder.IconsGroupImageIndex]));
      Holder.UsersImageList.Images.Add(BaseHolder.NamedList.ImageList.Images[BaseHolder.NamedList.ImageIndex("partvar")]);
      Holder.UsersImageList.Images.Add(BaseHolder.ImageTo16x16(BaseHolder.IconService.ImageList.Images[Holder.IconsRankImageIndex]));
      Holder.UserImageIndex = 0;
      Holder.GroupImageIndex = 1;
      Holder.RankImageIndex = 3;
      Holder.ActivityResultImageIndex[1] = BaseHolder.NamedList.ImageIndex("wfBack");
      Holder.ActivityResultImageIndex[0] = BaseHolder.NamedList.ImageIndex("wfNext");
      Holder.TaskRejectedIndex = BaseHolder.NamedList.ImageIndex("wfRejected");
      Holder.RejectWOImageIndex = BaseHolder.NamedList.ImageIndex("wfRejectWO");
      Image image1 = (Image) null;
      Image image2 = Holder.SchemeImageIndex <= -1 ? (Image) new Bitmap(16 /*0x10*/, 16 /*0x10*/) : BaseHolder.ImageTo16x16(BaseHolder.IconService.ImageList.Images[Holder.SchemeImageIndex]);
      Holder.SchemeNamedImageIndex = BaseHolder.NamedList.Add(image2, "wfSchemesType");
      int index = BaseHolder.IconService.IndexOf(4, wfConsts.SchemeCategoriesID);
      image1 = (Image) null;
      Image image3 = index == -1 ? (Image) new Bitmap(16 /*0x10*/, 16 /*0x10*/) : BaseHolder.ImageTo16x16(BaseHolder.IconService.ImageList.Images[index]);
      Holder.SchemeGroupNamedImageIndex = BaseHolder.NamedList.Add(image3, "wfSchemeGroupType");
      Holder.ValidatedImageList.Images.Add(Holder.WarningImage);
    }
    foreach (VarType varType in Enum.GetValues(typeof (VarType)))
    {
      FType fieldTypeEx = MiscFunx.GetFieldTypeEx(varType);
      if (fieldTypeEx != null)
      {
        Icon icon2 = fieldTypeEx.ResourceImageName == null ? BaseHolder.IconService.GetIconEx(3, -1, (object) fieldTypeEx.FieldType) : wfFunx.BitmapToIcon(BaseHolder.LoadResImage(".img.vartypes." + fieldTypeEx.ResourceImageName));
        if (icon2 != null)
          BaseHolder.IconService.AddIcon(icon2, 3, -1, (object) varType);
      }
    }
    MenuTemplate contextMenuTemplate = BaseHolder.Factory.ContextMenuTemplate;
    contextMenuTemplate.BeginUpdate();
    try
    {
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("CreateSchemeGroup", LocalizationHolder.rm.GetString("CreateSchemeGroup"), Holder.SchemeGroupNamedImageIndex, 0, 0));
    }
    finally
    {
      contextMenuTemplate.EndUpdate();
    }
    BaseHolder.NotificationService.Subscribe("ObjectsRemoved", new NotificationEventHandler(Holder.SchemeDeletedEvent));
    if (ApplicationServices.Container.GetService(typeof (IAttributePropertyDescriberService)) is IAttributePropertyDescriberService service)
    {
      int[] numArray = new int[3]
      {
        wfConsts.SysVarStarterID,
        wfConsts.SysVarSenderID,
        wfConsts.AttrParticipantsID
      };
      foreach (int attributeId in numArray)
      {
        if (service.GetDescriber(attributeId) == null)
          service.RegisterDescriber(attributeId, (IAttributePropertyDescriber) new ParticipantsPropertyDescriber());
      }
    }
    ParticipantList.OnGetParticipantName = new OnGetParticipantName(wfFunx.GetParticipantName);
    MiscFunx.ReloadVariablesCacheNeeded += new MiscFunx.ReloadVariablesCacheEvent(Holder.ReloadVariablesCache);
  }

  private static void ReloadVariablesCache(IUserSession session)
  {
    IClientCache clientCache = (session as IClientSession).ClientCache;
    clientCache.ReloadCacheCategory(3, session);
    bool forced = MetaDataHelper.Forced;
    MetaDataHelper.Forced = true;
    try
    {
      MetaDataHelper.SyncAttrTypesMetadata(clientCache.CacheDataSet);
    }
    finally
    {
      MetaDataHelper.Forced = forced;
    }
  }

  private static void SchemeDeletedEvent(object sender, NotificationEventArgs e)
  {
    if (!(e is DBObjectsEventArgs objectsEventArgs))
      return;
    foreach (long objectId in (IEnumerable<long>) objectsEventArgs.ObjectIDs)
    {
      if (Holder.Editors.FindEditor(objectId, true) is wfEditorForm editor && editor.Parent is DockControl)
      {
        DockControl parent = (DockControl) editor.Parent;
        parent.Closing -= new CancelEventHandler(editor.FormClosingHandler);
        parent.Close();
      }
    }
    Holder.RecentLaunched.RemoveRecent(objectsEventArgs.ObjectIDs);
    Holder.RecentSchemes.RemoveRecent(objectsEventArgs.ObjectIDs);
  }

  public static int SchemeImageIndex
  {
    get
    {
      if (Holder._schemeImageIndex == -2)
        Holder._schemeImageIndex = BaseHolder.IconService.IndexOf(4, wfConsts.SchemesTypeID);
      return Holder._schemeImageIndex;
    }
  }

  public static Icon SchemeIcon
  {
    get
    {
      if (Holder._schemeIcon == null && Holder.SchemeImageIndex > -1)
        Holder._schemeIcon = BaseHolder.IconService.GetIndexIcon(Holder.SchemeImageIndex);
      return Holder._schemeIcon;
    }
  }

  public static string WorkflowTempPath
  {
    get
    {
      if (Holder._workflowTempPath == "")
      {
        Holder._workflowTempPath = Path.GetTempPath() + "_IPSWorkflow\\";
        if (!Directory.Exists(Holder._workflowTempPath))
          Directory.CreateDirectory(Holder._workflowTempPath);
      }
      return Holder._workflowTempPath;
    }
  }

  public static bool IsAdmin
  {
    get
    {
      if (!Holder._isAdmin.HasValue)
        Holder._isAdmin = new bool?((ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole).IsAdmin);
      return Holder._isAdmin.Value;
    }
  }

  public static bool СanShowAllVersions { get; set; }

  public static Image LoadingImage
  {
    get
    {
      if (Holder._loadingImage == null)
        Holder._loadingImage = (Image) BaseHolder.LoadResImage(".img.loading.gif");
      return Holder._loadingImage;
    }
  }

  public static Image QuestionImage
  {
    get
    {
      if (Holder._questionImage == null)
        Holder._questionImage = (Image) BaseHolder.LoadResImage(".img.question.bmp");
      return Holder._questionImage;
    }
  }

  public static Image WarningImage
  {
    get
    {
      if (Holder._warningImage == null)
        Holder._warningImage = (Image) BaseHolder.LoadResImage(".img.warning.bmp");
      return Holder._warningImage;
    }
  }

  public static VersionsRule AllVersionsRule
  {
    get
    {
      if (Holder._AllVersionsRule == null && (ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IVersionRulesCacheService)) is IVersionRulesCacheService customService)
        Holder._AllVersionsRule = customService.AllVersionsRule;
      return Holder._AllVersionsRule;
    }
  }

  public static ImageList ValidatedImageList => Holder._validatedImageList;
}
