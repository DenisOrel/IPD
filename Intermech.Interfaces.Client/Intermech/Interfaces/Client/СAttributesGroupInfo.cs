// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.СAttributesGroupInfo
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Data;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Получатель инфы о группе атрибутов</summary>
internal class СAttributesGroupInfo(MetadataInfoParentContext serviceContext, int metadataID) : 
  MetadataInfoObject(serviceContext, metadataID),
  IDBAttributesGroupInfo
{
  private IDBAttributeTypeInfoCollection _Attributes;

  protected override string DBTableName => "IMS_ATTR_GROUPS";

  protected override string MetadataNotFoundMessage
  {
    get => $"Группа атрибутов номер {this.MetadataID} не найдена.";
  }

  public override string ObjectName => $"Группа атрибутов '{this.GroupName}'";

  public int GroupID
  {
    [DebuggerStepThrough] get => this.MetadataID;
  }

  public string GroupName
  {
    [DebuggerStepThrough] get => this.paramsTable[0]["F_GROUP_NAME"].ToString();
  }

  public string Note
  {
    [DebuggerStepThrough] get => this.paramsTable[0]["F_NOTE"].ToString();
  }

  public int ParentID
  {
    [DebuggerStepThrough] get => Convert.ToInt32(this.paramsTable[0]["F_PARENT_ID"]);
  }

  public IDBAttributeTypeInfoCollection Attributes
  {
    [DebuggerStepThrough] get
    {
      if (this._Attributes == null)
        this._Attributes = this.ServiceContext.Parent.GetAttributeTypeCollection(this.MetadataID, false);
      return this._Attributes;
    }
  }

  public bool HasAttribute(int attrID)
  {
    bool flag = false;
    foreach (DataRow row in (InternalDataCollectionBase) this.Attributes.Select(string.Empty).Rows)
    {
      if (Convert.ToInt32(row["F_ATTRIBUTE_ID"]) == attrID)
      {
        flag = true;
        break;
      }
    }
    return flag;
  }
}
