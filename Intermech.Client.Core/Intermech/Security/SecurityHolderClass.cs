
// Type: Intermech.Security.SecurityHolderClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Data;
using System.Windows.Forms;


namespace Intermech.Security;

internal class SecurityHolderClass : SecurityHolderList
{
  private SecurityHolderClass owner;
  private bool isSecurityReadonly;
  private bool relatedSecurityExpanded;
  private bool conditionsEnabled;
  private bool initialized;
  private bool isCompatibleRights = true;
  private bool isIdenticalAccess = true;
  private bool isIdenticalRelatedAccess = true;
  private bool isChangedFlag;
  private object id;
  private object[] ids;
  private ISecurityCallback securityCallback;
  private CategoryDescriptor categoryDescriptor;
  private int icoImageIndex = -1;
  private int catIcoImageIndex = -1;
  private string objectName = string.Empty;
  private DataTable accessDataTable;
  private ActionProperties[] actions;
  private QuickObjectInfo[] users;

  public SecurityHolderClass Owner => this.owner;

  public SecurityHolderClass RootOwner
  {
    get
    {
      SecurityHolderClass rootOwner = this;
      while (rootOwner.Owner != null)
        rootOwner = rootOwner.Owner;
      return rootOwner;
    }
  }

  public bool IsRelatedSecurity => this.owner != null;

  public bool IsSecurityReadOnly => this.isSecurityReadonly;

  public bool RelatedSecurityExpanded => this.relatedSecurityExpanded;

  public bool ConditionsEnabled => this.conditionsEnabled;

  public bool Initialized => this.initialized;

  public bool IsCompatibleRights => this.isCompatibleRights;

  public bool IsIdenticalAccess => this.isIdenticalAccess;

  public bool IsIdenticalRelatedAccess => this.isIdenticalRelatedAccess;

  public bool IsChangedFlag
  {
    get => this.isChangedFlag || this.IsChanged;
    set
    {
      this.isChangedFlag = value;
      this.IsChanged = value;
    }
  }

  public bool isChangedFlagOnly
  {
    get => this.isChangedFlag;
    set => this.isChangedFlag = value;
  }

  public object Id => this.id;

  public object RootId => this.RootOwner.Id;

  public object[] Ids => this.ids;

  public ISecurityCallback SecurityCallback => this.securityCallback;

  public ISecurityCallback RootSecurityCallback => this.RootOwner.SecurityCallback;

  public CategoryDescriptor CategoryDescriptor => this.categoryDescriptor;

  public int IcoImageIndex => this.icoImageIndex;

  public int CatIcoImageIndex => this.catIcoImageIndex;

  public string ObjectName => this.objectName;

  public DataTable AccessDataTable => this.accessDataTable;

  /// <summary>
  /// переназначение таблицы прав. использовать только в крайних случаях.
  /// иначе перечитывать класс.
  /// </summary>
  public void AssignAccessDataTable(DataTable dt) => this.accessDataTable = dt;

  public ActionProperties[] Actions => this.actions;

  public QuickObjectInfo[] Users => this.users;

  public SecurityHolderClass(object[] aId, ISecurityCallback aSecurityCallback)
  {
    this.ids = aId;
    this.securityCallback = aSecurityCallback;
    this.id = aId[0];
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.initialized = this.Initialize(sessionKeeper.Session);
      if (this.initialized)
        this.InitializeRelatedSecurity();
    }
    if (this.initialized)
      return;
    this.isSecurityReadonly = true;
  }

  private SecurityHolderClass(IDBSecurity aIDBSecurity, SecurityHolderClass aOwner)
  {
    this.owner = aOwner;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.initialized = this.Initialize(sessionKeeper.Session, aIDBSecurity);
  }

  private bool Initialize(IUserSession session) => this.Initialize(session, (IDBSecurity) null);

  private bool Initialize(IUserSession session, IDBSecurity iDBS)
  {
    bool flag1 = true;
    bool flag2 = iDBS == null;
    this.isSecurityReadonly = false;
    this.isIdenticalAccess = true;
    this.isCompatibleRights = true;
    this.isIdenticalRelatedAccess = true;
    this.conditionsEnabled = false;
    IDBSecurity dbSecurity = (IDBSecurity) null;
    bool flag3 = true;
    while (flag3)
    {
      flag3 = false;
      dbSecurity = !flag2 ? iDBS : this.securityCallback.GetSecurity(session, this.id);
      if (dbSecurity == null)
        return false;
      this.conditionsEnabled = dbSecurity.EnabledConditionAccess;
      if (flag2 && !(dbSecurity is IDBSecurityCollection) && this.ids.Length > 1)
        return false;
      IDBSecurityCollection securityCollection = dbSecurity as IDBSecurityCollection;
      if (flag2 && this.ids.Length > 1 && securityCollection != null)
      {
        this.isCompatibleRights = securityCollection.IsCompatibleElements(SecurityClass.ObjectArrayToInt64Array(this.ids));
        if (!this.isCompatibleRights)
          return false;
        this.isIdenticalAccess = securityCollection.IsIdenticalAccess(SecurityClass.ObjectArrayToInt64Array(this.ids));
      }
      try
      {
        this.accessDataTable = SecurityProcs.GroupRightsByUID(dbSecurity.GetAccessList(out this.actions, out this.users));
        if (flag2 && this.ids.Length > 1 && !this.isIdenticalAccess)
          this.accessDataTable.Rows.Clear();
        if (!flag2 && !this.owner.IsIdenticalRelatedAccess)
          this.accessDataTable.Rows.Clear();
        this.isSecurityReadonly = Convert.ToInt16(this.accessDataTable.ExtendedProperties[(object) "ReadOnly"]) == (short) 1;
        this.objectName = !flag2 ? (securityCollection == null ? dbSecurity.ObjectName : (this.RootOwner.Ids == null || this.RootOwner.Ids.Length <= 1 ? dbSecurity.ObjectName : securityCollection.SecurityCollectionName)) : (this.ids.Length <= 1 ? dbSecurity.ObjectName : (securityCollection == null ? dbSecurity.ObjectName : securityCollection.SecurityCollectionName));
        this.categoryDescriptor = dbSecurity.Descriptor;
      }
      catch (Exception ex)
      {
        if (ex is AccessDeniedException & flag2 && this.ids.Length == 1 && this.CheckUserInAdminRole())
        {
          switch (IMMessageBox.Show(ex.Message, LocalizationHolder.rm.GetString("Client.Core_1010"), MessageBoxButtons.YesNo, IMMessageBoxImage.Question))
          {
            case DialogResult.Yes:
              dbSecurity = this.securityCallback.GetSecurity(session, this.id);
              dbSecurity?.RestoreAdminAccess();
              flag3 = true;
              continue;
          }
        }
        this.accessDataTable = (DataTable) null;
        this.categoryDescriptor = new CategoryDescriptor();
        this.objectName = string.Empty;
        throw;
      }
    }
    if (Statics.IconSrv != null)
    {
      switch (this.categoryDescriptor.CategoryType)
      {
        case 1:
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            IDBObject dbObject = sessionKeeper.Session.GetObject(this.categoryDescriptor.CategoryID, false);
            if (dbObject != null)
            {
              this.icoImageIndex = Statics.IconSrv.IndexOf(4, dbObject.ObjectType);
              this.catIcoImageIndex = Statics.IconSrv.IndexOf(this.categoryDescriptor.CategoryType);
              break;
            }
            break;
          }
        case 5:
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            IDBRelation relation = sessionKeeper.Session.GetRelation(this.categoryDescriptor.CategoryID, false);
            if (relation != null)
            {
              this.icoImageIndex = Statics.IconSrv.IndexOf(6, relation.RelationType);
              this.catIcoImageIndex = Statics.IconSrv.IndexOf(this.categoryDescriptor.CategoryType);
              break;
            }
            break;
          }
        case 7:
          if (dbSecurity is IDBLifecycleLevel dbLifecycleLevel)
          {
            this.icoImageIndex = Statics.IconSrv.IndexOf(8, dbLifecycleLevel.LevelID);
            this.catIcoImageIndex = Statics.IconSrv.IndexOf(this.categoryDescriptor.CategoryType);
            break;
          }
          break;
        case 25:
        case 26:
        case 30:
          this.icoImageIndex = Statics.IconSrv.IndexOf(this.categoryDescriptor.CategoryType, 1);
          this.catIcoImageIndex = Statics.IconSrv.IndexOf(this.categoryDescriptor.CategoryType);
          break;
        case 29:
          int int32 = Convert.ToInt32(this.categoryDescriptor.CategoryID >> 40 & 16777215L /*0xFFFFFF*/);
          this.icoImageIndex = Statics.IconSrv.IndexOf(3, -1, (object) (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetAttributeType(int32).AttributeType);
          this.catIcoImageIndex = Statics.IconSrv.IndexOf(this.categoryDescriptor.CategoryType);
          break;
        default:
          this.icoImageIndex = Statics.IconSrv.IndexOf(this.categoryDescriptor.CategoryType, Convert.ToInt32(this.categoryDescriptor.CategoryID));
          this.catIcoImageIndex = Statics.IconSrv.IndexOf(this.categoryDescriptor.CategoryType);
          break;
      }
    }
    return flag1;
  }

  public bool InitializeRelatedSecurity()
  {
    if (!this.relatedSecurityExpanded)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        this.Clear();
        IDBSecurity security = this.GetSecurity(sessionKeeper.Session);
        if (security == null)
          return false;
        IDBSecurity[] dbSecurityArray;
        if (this.ids.Length == 1)
          dbSecurityArray = security.GetRelatedSecurity();
        else if (security is IDBSecurityCollection securityCollection1)
        {
          IDBSecurityCollection securityCollection = securityCollection1.GetRelatedSecurityCollection(SecurityClass.ObjectArrayToInt64Array(this.ids));
          if (securityCollection == null)
          {
            this.isIdenticalRelatedAccess = false;
            return false;
          }
          this.isIdenticalRelatedAccess = securityCollection.IsIdenticalAccess(SecurityClass.ObjectArrayToInt64Array(this.ids));
          if (!this.isIdenticalRelatedAccess)
            return false;
          dbSecurityArray = new IDBSecurity[1]
          {
            (IDBSecurity) securityCollection
          };
        }
        else
        {
          this.isIdenticalRelatedAccess = false;
          return false;
        }
        if (dbSecurityArray != null)
        {
          for (int index = 0; index < dbSecurityArray.Length; ++index)
          {
            try
            {
              SecurityHolderClass securityHolderClass = new SecurityHolderClass(dbSecurityArray[index], this);
              if (securityHolderClass.Initialized)
                this.Add((object) securityHolderClass);
            }
            catch (AccessDeniedException ex)
            {
            }
          }
        }
        this.relatedSecurityExpanded = true;
      }
    }
    return true;
  }

  private bool CheckUserInAdminRole()
  {
    return (ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole).IsAdmin;
  }

  public static bool FindQuickObjectInfo(
    long objectId,
    QuickObjectInfo[] usersCache,
    out QuickObjectInfo qoi)
  {
    qoi = new QuickObjectInfo(-1L, "", -1, Guid.Empty, -1L);
    bool quickObjectInfo = false;
    if (usersCache != null)
    {
      for (int index = 0; index < usersCache.Length; ++index)
      {
        if (usersCache[index].ObjectID == objectId)
        {
          quickObjectInfo = true;
          qoi = usersCache[index];
          break;
        }
      }
    }
    if (!quickObjectInfo)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(objectId);
        if (dbObject == null)
        {
          int num = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("Client.Core_1008"), (object) objectId));
        }
        else
        {
          qoi = new QuickObjectInfo(dbObject.ObjectID, dbObject.Caption, dbObject.ObjectType, dbObject.ObjectGUID, dbObject.ID);
          quickObjectInfo = true;
        }
      }
    }
    return quickObjectInfo;
  }

  public IDBSecurity GetSecurity(IUserSession session)
  {
    return SecurityHolderClass.FindSecurity(this.RootSecurityCallback.GetSecurity(session, this.RootId), this.categoryDescriptor);
  }

  public IDBSecurity GetSecurity(IUserSession session, object id)
  {
    return SecurityHolderClass.FindSecurity(this.RootSecurityCallback.GetSecurity(session, id), this.categoryDescriptor);
  }

  public static IDBSecurity FindSecurity(IDBSecurity security, CategoryDescriptor cd)
  {
    if (security.Descriptor.Equals((object) cd))
      return security;
    IDBSecurity[] relatedSecurity = security.GetRelatedSecurity();
    if (relatedSecurity != null)
    {
      for (int index = 0; index < relatedSecurity.Length; ++index)
      {
        IDBSecurity security1 = SecurityHolderClass.FindSecurity(relatedSecurity[index], cd);
        if (security1 != null)
          return security1;
      }
    }
    return (IDBSecurity) null;
  }

  public DataRow GetRight(long uid, QuickObjectInfo quickObjectInfo, ActionProperties ap)
  {
    DataRow[] dataRowArray = this.accessDataTable.Select($"F_RIGHT_TYPE<>{Intermech.Consts.DeleteRecord.ToString()} and {SecurityProcs.F_UID}={uid.ToString()} and F_USER_ID={quickObjectInfo.ObjectID.ToString()} and F_RIGHT_ID={((int) ap.ActionID).ToString()}");
    return dataRowArray.Length != 0 ? dataRowArray[0] : (DataRow) null;
  }

  public DataRow[] GetRights4User(long uid, QuickObjectInfo quickObjectInfo)
  {
    return this.accessDataTable.Select($"F_RIGHT_TYPE<>{Intermech.Consts.DeleteRecord.ToString()} and {SecurityProcs.F_UID}={uid.ToString()} and F_USER_ID={quickObjectInfo.ObjectID.ToString()}");
  }
}
