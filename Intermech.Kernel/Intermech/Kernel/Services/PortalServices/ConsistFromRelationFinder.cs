// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.ConsistFromRelationFinder
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System.Data;


namespace Intermech.Kernel.Services.PortalServices;

internal sealed class ConsistFromRelationFinder(
  IDBObjectType objectType,
  IDBRelationsApplicabilityCollection aplicabilities) : RelationFinder(objectType, aplicabilities)
{
  protected override int ObjectColumn => -2;

  protected override DataTable GetApplicabilitiesList(int anotherTypeID)
  {
    return this.aplicabilities.GetApplicabilitiesList(-1, anotherTypeID, this.objectType.ObjectType);
  }

  protected override DataTable GetApplicabilityTable(
    IDBRelationCollection relationCollection,
    int anotherTypeID,
    long objectID)
  {
    return relationCollection.ConsistFrom(this.GetSelectParams(anotherTypeID), objectID);
  }
}
