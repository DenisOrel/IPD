// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Workflow.IApproveActivity
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Workflow;

public interface IApproveActivity : 
  IActivity,
  IMailObject,
  IDBObject,
  IDBAttributable,
  IDBSessionable,
  IPluginsData,
  IDBSecurityCollection,
  IDBSecurity
{
  List<long> GetUnsignedObjects();

  bool CheckAllSigned(bool silent, out HashSet<long> participantIDs, bool checkAll = false);
}
