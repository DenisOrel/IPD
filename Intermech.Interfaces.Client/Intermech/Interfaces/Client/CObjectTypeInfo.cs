// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.CObjectTypeInfo
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Получение инфы о типе объекта</summary>
internal class CObjectTypeInfo(MetadataInfoParentContext serviceContext, int metadataID) : 
  CAttributableTypeInfo(serviceContext, metadataID),
  IDBObjectTypeInfo,
  IDBAttributableTypeInfo
{
  public override string ObjectName
  {
    [DebuggerStepThrough] get => $"Тип объектов '{this.ObjectTypeName}'";
  }

  public override IDBAttribute4TypeInfoCollection Attributes
  {
    [DebuggerStepThrough] get
    {
      if (this._Attributes == null)
        this._Attributes = (IDBAttribute4TypeInfoCollection) new CAttribute4ObjectTypeInfoCollection(this.ServiceContext, this.MetadataID, false);
      return this._Attributes;
    }
  }

  public override IDBAttribute4TypeInfoCollection VisibleAttributes
  {
    get
    {
      return (IDBAttribute4TypeInfoCollection) new CAttribute4ObjectTypeInfoCollection(this.ServiceContext, this.MetadataID, true);
    }
  }

  public int ObjectType
  {
    [DebuggerStepThrough] get => this.MetadataID;
  }

  public string ObjectTypeName
  {
    [DebuggerStepThrough] get => this.paramsTable[0]["F_OBJ_TYPE_NAME"].ToString();
  }

  public string ObjectTypeShortName
  {
    [DebuggerStepThrough] get => this.paramsTable[0]["F_SHORT_NAME"].ToString();
  }

  public string ObjectInstanceName
  {
    [DebuggerStepThrough] get => this.paramsTable[0]["F_OBJ_NAME"].ToString();
  }

  public byte[] Icon
  {
    [DebuggerStepThrough] get
    {
      return this.paramsTable[0]["F_ICON"] == DBNull.Value ? new byte[0] : (byte[]) this.paramsTable[0]["F_ICON"];
    }
  }

  public ObjectVersionModes Versionable
  {
    [DebuggerStepThrough] get
    {
      return (ObjectVersionModes) Convert.ToInt32(this.paramsTable[0]["F_VERSIONABLE"]);
    }
  }

  public string Note
  {
    [DebuggerStepThrough] get => this.paramsTable[0]["F_NOTE"].ToString();
  }

  public int DefaultRelation
  {
    [DebuggerStepThrough] get => Convert.ToInt32(this.paramsTable[0]["F_DEFAULT_RELATION"]);
  }

  public int ParentTypeID
  {
    [DebuggerStepThrough] get
    {
      lock (СObjectType.ParentsArrays)
      {
        if (СObjectType.ParentsArrays.IndexOfKey(this.ObjectType) >= 0)
          return СObjectType.ParentsArrays[this.ObjectType];
      }
      DataRow[] dataRowArray = this.ServiceContext.ClientCache.GetTable("IMS_OBJTYPES_TREE").Select("F_OBJECT_TYPE = " + this.ObjectType.ToString());
      lock (СObjectType.ParentsArrays)
      {
        СObjectType.ParentsArrays[this.ObjectType] = dataRowArray.Length != 0 ? Convert.ToInt32(dataRowArray[0]["F_PARENT_ID"]) : -1;
        return СObjectType.ParentsArrays[this.ObjectType];
      }
    }
  }

  public int CaptionAttribute
  {
    [DebuggerStepThrough] get => Convert.ToInt32(this.paramsTable[0]["F_CAPTION_ATTRIBUTE"]);
  }

  public InheritModes PublicLC
  {
    [DebuggerStepThrough] get => (InheritModes) Convert.ToInt32(this.paramsTable[0]["F_PUBLIC_LC"]);
  }

  public ObjectTypeProperties PropertiesStructure
  {
    [DebuggerStepThrough] get
    {
      return new ObjectTypeProperties(this.ObjectType, this.ObjectTypeName, this.ObjectInstanceName, this.Note, this.Versionable, this.DefaultRelation, this.SubjectAreas, this.GUID, this.CaptionAttribute, this.AnyAttributes, this.PublicLC, this.ObjectTypeShortName, this.LifetimeReserve, this.Options, this.SchemaID);
    }
  }

  public string SubjectAreas
  {
    [DebuggerStepThrough] get => this.paramsTable[0]["F_AREA_ID"].ToString();
  }

  public int LifetimeReserve
  {
    [DebuggerStepThrough] get => Convert.ToInt32(this.paramsTable[0]["F_DEL_TIME"]);
  }

  public ObjectTypeOptions Options
  {
    [DebuggerStepThrough] get
    {
      return (ObjectTypeOptions) Convert.ToInt32(this.paramsTable[0]["F_OPTIONS"]);
    }
  }

  public int SchemaID
  {
    [DebuggerStepThrough] get => Convert.ToInt32(this.paramsTable[0]["F_SCHEMA_ID"]);
  }

  public bool IsLocalType
  {
    [DebuggerStepThrough] get
    {
      return (this.Options & ObjectTypeOptions.LocalObjectType) == ObjectTypeOptions.LocalObjectType;
    }
  }

  protected override string DBTableName => "IMS_OBJECT_TYPES";

  protected override string MetadataNotFoundMessage
  {
    get => $"Тип объекта номер {this.MetadataID} не найден.";
  }

  /// <summary>
  /// Возвращает словарь тип_объектов=тип_связей, который характеризует какие объекты можно включать в состав данного типа объектов и каким типом связей
  /// </summary>
  /// <param name="getAll">Возвращать ли весь список или первый попавшийся элемент</param>
  /// <returns></returns>
  private Dictionary<int, int> FillPossibleChildren(bool getAll)
  {
    Dictionary<int, int> dictionary = new Dictionary<int, int>();
    List<int> intList = new List<int>();
    foreach (IMSApplicability typeApplicability in MetaDataHelper.GetObjectTypeApplicabilities(this.ObjectType))
    {
      if (typeApplicability.ApplicabilityMode != ApplicabilityModes.Disabled)
      {
        int childObjectTypeId = typeApplicability.ChildObjectTypeID;
        int relationTypeId = typeApplicability.RelationTypeID;
        foreach (int num in MetaDataHelper.GetObjectTypeChildrenIDRecursive(childObjectTypeId))
        {
          IMSApplicability applicability = MetaDataHelper.GetApplicability(this.ObjectType, num, relationTypeId);
          IMSObjectType objectType = MetaDataHelper.GetObjectType(num);
          if (applicability != null && objectType != null && applicability.ApplicabilityMode != ApplicabilityModes.Disabled)
          {
            int options = (int) applicability.Options;
            if ((applicability.Options & ApplicabilityOptions.DefaultRelation) == ApplicabilityOptions.DefaultRelation || !dictionary.ContainsKey(num))
            {
              dictionary[num] = relationTypeId;
              if (!getAll)
                return dictionary;
            }
          }
        }
      }
    }
    return dictionary;
  }

  public Dictionary<int, int> GetPossibleChildren() => this.FillPossibleChildren(true);

  public bool HasPossibleChildren() => this.FillPossibleChildren(false).Count > 0;
}
