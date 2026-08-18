// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.CAttributesGroupInfoCollection
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Обработчик списка групп атрибутов</summary>
internal class CAttributesGroupInfoCollection(
  MetadataInfoParentContext serviceContext,
  object parentID,
  bool filtering) : MetadataInfoCollection(serviceContext, parentID, filtering), IDBAttributesGroupInfoCollection, IDBCollection
{
  protected override string DBKeyField
  {
    [DebuggerStepThrough] get => "F_GROUP_ID";
  }

  protected override string DBTableName
  {
    [DebuggerStepThrough] get => "IMS_ATTR_GROUPS";
  }

  protected override string GetParentSQL()
  {
    return Convert.ToInt32(this.ParentID) < 0 ? string.Empty : $" F_PARENT_ID = {this.ParentID.ToString()} ";
  }
}
