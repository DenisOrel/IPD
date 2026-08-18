// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.CRelationType
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Data;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Summary description for CRelationType.</summary>
internal class CRelationType : 
  CMetadataExtentions,
  IDBRelationType,
  IDBAttributableType,
  IDBSubjectArea,
  IDeletable,
  IDBSecurity
{
  private IDBAttribute4TypeCollection _Attributes;

  public CRelationType(ClientSession uSession, int aRelationTypeID)
    : base(uSession, aRelationTypeID)
  {
    this._RelationTypeID = aRelationTypeID;
    this.InitOptions(6, (long) aRelationTypeID, "IMS_RELATION_TYPES", LocalizationHolder.rm.GetString("Interfaces.Client_122"));
  }

  public override object GetServerObject()
  {
    this._clientSession.Guard.ValidateCall();
    return (object) this._clientSession.Session.GetRelationType(this._id);
  }

  public string ShortName
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.paramsTable[0]["F_SHORT_NAME"].ToString();
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (!(this.ShortName != value))
        return;
      this._clientSession.Session.GetRelationType(this._id).ShortName = value;
      this.ReloadClientCache();
    }
  }

  public void RebuildView()
  {
    this._clientSession.Guard.ValidateCall();
    this._clientSession.Session.GetRelationType(this._id).RebuildView();
  }

  public IDBAttribute4TypeCollection VisibleAttributes
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return (IDBAttribute4TypeCollection) new CAttribute4RelationTypeCollection(this._clientSession, this.RelationType, true);
    }
  }

  public bool HasAttribute(int attributeID)
  {
    this._clientSession.Guard.ValidateCall();
    if (attributeID < 0)
      return ObligatoryObjectAttributesHelper.GetAttributeSourceType((ObligatoryObjectAttributes) attributeID) == AttributeSourceTypes.Relation;
    return this.AnyAttributes || this.Attributes.GetAttributeByID(attributeID, false) != null;
  }

  public string Description
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.paramsTable[0]["F_DESCRIPTION"].ToString();
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (!(this.Description != value))
        return;
      this._clientSession.Session.GetRelationType(this._id).Description = value;
      this.ReloadClientCache();
    }
  }

  public bool AnyAttributes
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return Convert.ToBoolean(this.paramsTable[0]["F_ANY_ATTRIBUTES"]);
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (this.AnyAttributes == value)
        return;
      this._clientSession.Session.GetRelationType(this._id).AnyAttributes = value;
      this.ReloadClientCache();
    }
  }

  public bool CheckoutFile
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return Convert.ToBoolean(this.paramsTable[0]["F_CHKOUTFILE"]);
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (this.CheckoutFile == value)
        return;
      this._clientSession.Session.GetRelationType(this._id).CheckoutFile = value;
      this.ReloadClientCache();
    }
  }

  public int RelationType
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._id;
    }
  }

  public IDBAttribute4TypeCollection Attributes
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      if (this._Attributes == null)
        this._Attributes = (IDBAttribute4TypeCollection) new CAttribute4RelationTypeCollection(this._clientSession, this.RelationType, false);
      return this._Attributes;
    }
  }

  public string ReverseName
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.paramsTable[0]["F_REVERSE_NAME"].ToString();
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (!(this.ReverseName != value))
        return;
      this._clientSession.Session.GetRelationType(this._id).ReverseName = value;
      this.ReloadClientCache();
    }
  }

  public int Delete(long DeleteMode)
  {
    this._clientSession.Guard.ValidateCall();
    int num = this._clientSession.Session.GetRelationType(this._id).Delete(DeleteMode);
    this.ReloadClientCache();
    return num;
  }

  public string TypeName
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.paramsTable[0]["F_TYPE_NAME"].ToString();
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (!(this.TypeName != value))
        return;
      this._clientSession.Session.GetRelationType(this._id).TypeName = value;
      this.ReloadClientCache();
    }
  }

  public byte[] Icon
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.paramsTable[0]["F_ICON"] == DBNull.Value ? new byte[0] : (byte[]) this.paramsTable[0]["F_ICON"];
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      this._clientSession.Session.GetRelationType(this._id).Icon = value;
      this.ReloadClientCache();
    }
  }

  public RelationTypeProperties PropertiesStructure
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return new RelationTypeProperties(this.RelationType, this.TypeName, this.ReverseName, this.Note, this.CheckoutFile, this.SaveHistory, this.Description, this.GUID, this.SubjectAreas, this.AnyAttributes, this.ShortName, this.Options);
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      this._clientSession.Session.GetRelationType(this._id).PropertiesStructure = value;
      if (this._clientSession.Session.GetRelationType(this._id).PropertiesStructure.AreaID != value.AreaID)
        this._clientSession.ClientCache.ClearVisibleList(4);
      this.ReloadClientCache();
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
      this._clientSession.Session.GetRelationType(this._id).Note = value;
      this.ReloadClientCache();
    }
  }

  public RelationTypeOptions Options
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return (RelationTypeOptions) Convert.ToInt32(this.paramsTable[0]["F_OPTIONS"]);
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (this.Options == value)
        return;
      this._clientSession.Session.GetRelationType(this._id).Options = value;
      this.ReloadClientCache();
    }
  }

  public bool SaveHistory
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return Convert.ToBoolean(this.paramsTable[0]["F_SAVE_HISTORY"]);
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

  public string SubjectAreasCaption
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._clientSession.Session.GetSubjectAreaCollection().GetAreasCaption(this.SubjectAreas);
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
      (this._clientSession.Session.GetRelationType(this._id) as IDBSubjectArea).SubjectAreas = value;
      this._clientSession.ClientCache.ClearVisibleList(6);
      this.ReloadClientCache();
    }
  }

  public string ObjectName
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return string.Format(LocalizationHolder.rm.GetString("Interfaces.Client_123"), (object) this.Description);
    }
  }

  public bool CheckAccess(ActionType rightID, bool aThrowACException)
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetRelationType(this._id) as IDBSecurity).CheckAccess(rightID, aThrowACException);
  }

  public bool CheckAccess(ActionType rightID)
  {
    this._clientSession.Guard.ValidateCall();
    return this.CheckAccess(rightID, true);
  }

  public bool CheckAccess(ActionType rightID, bool defaultAccess, bool aThrowACException)
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetRelationType(this._id) as IDBSecurity).CheckAccess(rightID, defaultAccess, aThrowACException);
  }

  public bool CheckAccess(ActionType rightID, bool defaultAccess, CheckAccessFlags flags)
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetRelationType(this._id) as IDBSecurity).CheckAccess(rightID, defaultAccess, flags);
  }

  public bool IsAccessTypeDeny
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return (this._clientSession.Session.GetRelationType(this._id) as IDBSecurity).IsAccessTypeDeny;
    }
  }

  public bool IsLastDefault
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return (this._clientSession.Session.GetRelationType(this._id) as IDBSecurity).IsLastDefault;
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
    return (this._clientSession.Session.GetRelationType(this._id) as IDBSecurity).GetAccessList(out actions, out users);
  }

  public void SetAccess(DataTable accessList, params object[] AddInfo)
  {
    this._clientSession.Guard.ValidateCall();
    this._clientSession.ClientCache.ClearVisibleList(6);
    (this._clientSession.Session.GetRelationType(this._id) as IDBSecurity).SetAccess(accessList, AddInfo);
  }

  public IDBSecurity[] GetRelatedSecurity()
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetRelationType(this._id) as IDBSecurity).GetRelatedSecurity();
  }

  public void RestoreAdminAccess()
  {
    this._clientSession.Guard.ValidateCall();
    (this._clientSession.Session.GetRelationType(this._id) as IDBSecurity).RestoreAdminAccess();
  }

  /// <summary>
  /// Возвращает описатель типа атрибута номер attributeID применительно к данному типу объектов/связей.
  /// Если тип не может принимать такие атрибуты, то функция возвращает null.
  /// </summary>
  public IDBAttributeType GetAttributeType(int attributeID)
  {
    this._clientSession.Guard.ValidateCall();
    IDBAttributeType attributeType = (IDBAttributeType) this.Attributes.GetAttributeByID(attributeID, false);
    if (attributeType == null && this.AnyAttributes)
      attributeType = this._clientSession.GetAttributeType(attributeID, false);
    return attributeType;
  }

  /// <summary>
  /// Возвращает описатель типа атрибута с именем attributeName применительно к данному типу объектов/связей.
  /// Если тип не может принимать такие атрибуты, то функция возвращает null.
  /// </summary>
  public IDBAttributeType GetAttributeType(string attributeName)
  {
    this._clientSession.Guard.ValidateCall();
    IDBAttributeType attributeType = this._clientSession.GetAttributeType(attributeName, false);
    return attributeType == null ? (IDBAttributeType) null : this.GetAttributeType(attributeType.AttributeID);
  }
}
