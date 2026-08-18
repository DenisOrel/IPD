// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.ImportBriefcaseRelation
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Data;


namespace Intermech.Kernel.Briefcase;

internal sealed class ImportBriefcaseRelation(
  UserSession session,
  DataSet metadata,
  ImportingRelation briefRelation) : ImportRelation(session, metadata, briefRelation)
{
  protected override IDBRelation FindRelation()
  {
    if (Convert.ToInt64(this.BriefRelation.Relation.ProjId) == this.session.IdentHelper.AllUsersGroupID && Convert.ToInt32(this.BriefRelation.Relation.RelationType) == this.session.IdentHelper.SimpleRelationTypeID)
    {
      IDBRelation relation = this.session.GetRelation(this.session.IdentHelper.AllUsersGroupID, Convert.ToInt64(this.BriefRelation.Relation.PartId), this.session.IdentHelper.SimpleRelationTypeID);
      if (relation != null)
        return relation;
    }
    return (IDBRelation) null;
  }
}
