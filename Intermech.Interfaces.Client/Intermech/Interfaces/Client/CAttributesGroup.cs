// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.CAttributesGroup
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Localization;
using System;
using System.Data;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Summary description for CAttributesGroup.</summary>
internal class CAttributesGroup : 
  CacheObject,
  IDBAttributesGroup,
  IDBLanguage,
  IDBSubjectArea,
  IDeletable,
  IDBSecurity
{
  private IDBAttributeTypeCollection _Attributes;

  public CAttributesGroup(ClientSession uSession, int aGroupID)
    : base(uSession, aGroupID)
  {
    this.InitOptions(12, (long) aGroupID, "IMS_ATTR_GROUPS", LocalizationHolder.rm.GetString("Interfaces.Client_115"));
  }

  public override object GetServerObject()
  {
    this._clientSession.Guard.ValidateCall();
    return (object) this._clientSession.Session.GetAttributesGroup(this._id);
  }

  public int ExcludeAttribute(params int[] attributeIDs)
  {
    this._clientSession.Guard.ValidateCall();
    int num = this._clientSession.Session.GetAttributesGroup(this._id).ExcludeAttribute(attributeIDs);
    this.ReloadClientCache();
    return num;
  }

  public void SetGUID(Guid guid)
  {
    this._clientSession.Guard.ValidateCall();
    this._clientSession.Session.GetAttributesGroup(this._id).SetGUID(guid);
    this.ReloadClientCache();
  }

  public int IncludeAttribute(int attributeID)
  {
    this._clientSession.Guard.ValidateCall();
    int num = this._clientSession.Session.GetAttributesGroup(this._id).IncludeAttribute(attributeID);
    this.ReloadClientCache();
    return num;
  }

  public int IncludeAttribute(int[] attributeIDs)
  {
    this._clientSession.Guard.ValidateCall();
    int num = this._clientSession.Session.GetAttributesGroup(this._id).IncludeAttribute(attributeIDs);
    this.ReloadClientCache();
    return num;
  }

  public IDBAttributeTypeCollection Attributes
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      if (this._Attributes == null)
        this._Attributes = this._clientSession.GetAttributeTypeCollection(this._id);
      return this._Attributes;
    }
  }

  public int Delete(long DeleteMode)
  {
    this._clientSession.Guard.ValidateCall();
    int num = this._clientSession.Session.GetAttributesGroup(this._id).Delete(DeleteMode);
    this.ReloadClientCache();
    return num;
  }

  public int GroupID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._id;
    }
  }

  public string Note
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.paramsTable[0]["F_NOTE"].ToString();
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (!(this.Note != value))
        return;
      this._clientSession.Session.GetAttributesGroup(this._id).Note = value;
      this.ReloadClientCache();
    }
  }

  public bool HasAttribute(int attrID)
  {
    this._clientSession.Guard.ValidateCall();
    return this._clientSession.Session.GetAttributesGroup(this._id).HasAttribute(attrID);
  }

  public int ParentID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return Convert.ToInt32(this.paramsTable[0]["F_PARENT_ID"]);
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (this.ParentID == value)
        return;
      this._clientSession.Session.GetAttributesGroup(this._id).ParentID = value;
      DataTable table = this._clientSession.ClientCache.GetTable(this._DBTableName);
      DataRow[] dataRowArray = table.Select("F_GROUP_ID = " + this.GroupID.ToString());
      if (dataRowArray.Length != 0)
      {
        dataRowArray[0]["F_PARENT_ID"] = (object) value;
        table.AcceptChanges();
      }
      this.paramsTable[0]["F_PARENT_ID"] = (object) value;
    }
  }

  public string GroupName
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.paramsTable[0]["F_GROUP_NAME"].ToString();
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (!(this.GroupName != value))
        return;
      this._clientSession.Session.GetAttributesGroup(this._id).GroupName = value;
      this.ReloadClientCache();
    }
  }

  public string LanguageName
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.LanguageID == string.Empty ? string.Empty : this._clientSession.GetLanguage(this.LanguageID).LanguageName;
    }
  }

  public string LanguageID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.paramsTable[0]["F_LANGUAGE_ID"].ToString().Trim();
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (!(this.LanguageID != value))
        return;
      (this._clientSession.Session.GetAttributesGroup(this._id) as IDBLanguage).LanguageID = value;
      this._clientSession.ClientCache.ClearVisibleList(3);
      this.ReloadClientCache();
    }
  }

  public bool IsDefaultLanguage
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.LanguageID == string.Empty || this._clientSession.GetLanguage(this.LanguageID).IsDefaultLanguage;
    }
  }

  public string SubjectAreas
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.paramsTable[0]["F_AREA_ID"].ToString();
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (!(this.SubjectAreas != value))
        return;
      (this._clientSession.Session.GetAttributesGroup(this._id) as IDBSubjectArea).SubjectAreas = value;
      this._clientSession.ClientCache.ClearVisibleList(12);
      this.ReloadClientCache();
    }
  }

  public string SubjectAreasCaption
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._clientSession.Session.GetSubjectAreaCollection().GetAreasCaption(this.SubjectAreas);
    }
  }

  public override Guid GUID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return base.GUID;
    }
  }

  public string ObjectName
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return string.Format(LocalizationHolder.rm.GetString("Interfaces.Client_1"), (object) this.GroupName);
    }
  }

  public bool CheckAccess(ActionType rightID, bool aThrowACException)
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetAttributesGroup(this._id) as IDBSecurity).CheckAccess(rightID, aThrowACException);
  }

  public bool CheckAccess(ActionType rightID)
  {
    this._clientSession.Guard.ValidateCall();
    return this.CheckAccess(rightID, true);
  }

  public bool CheckAccess(ActionType rightID, bool defaultAccess, bool aThrowACException)
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetAttributesGroup(this._id) as IDBSecurity).CheckAccess(rightID, defaultAccess, aThrowACException);
  }

  public bool CheckAccess(ActionType rightID, bool defaultAccess, CheckAccessFlags flags)
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetAttributesGroup(this._id) as IDBSecurity).CheckAccess(rightID, defaultAccess, flags);
  }

  public bool IsAccessTypeDeny
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return (this._clientSession.Session.GetAttributesGroup(this._id) as IDBSecurity).IsAccessTypeDeny;
    }
  }

  public bool IsLastDefault
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return (this._clientSession.Session.GetAttributesGroup(this._id) as IDBSecurity).IsLastDefault;
    }
  }

  public CategoryDescriptor Descriptor
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return new CategoryDescriptor(this._CategoryType, this._CategoryID);
    }
  }

  public DataTable GetAccessList(out ActionProperties[] actions, out QuickObjectInfo[] users)
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetAttributesGroup(this._id) as IDBSecurity).GetAccessList(out actions, out users);
  }

  public void SetAccess(DataTable accessList, params object[] AddInfo)
  {
    this._clientSession.Guard.ValidateCall();
    this._clientSession.ClientCache.ClearVisibleList(12);
    (this._clientSession.Session.GetAttributesGroup(this._id) as IDBSecurity).SetAccess(accessList, AddInfo);
  }

  public IDBSecurity[] GetRelatedSecurity()
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetAttributesGroup(this._id) as IDBSecurity).GetRelatedSecurity();
  }

  public void RestoreAdminAccess()
  {
    this._clientSession.Guard.ValidateCall();
    (this._clientSession.Session.GetAttributesGroup(this._id) as IDBSecurity).RestoreAdminAccess();
  }
}
