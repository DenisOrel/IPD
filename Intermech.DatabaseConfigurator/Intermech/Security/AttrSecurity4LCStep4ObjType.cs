// Decompiled with JetBrains decompiler
// Type: Intermech.Security.AttrSecurity4LCStep4ObjType
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Interfaces;
using System;

#nullable disable
namespace Intermech.Security;

internal class AttrSecurity4LCStep4ObjType : ISecurityCallback
{
  private int lcStepId;
  private int objTypeId;

  public AttrSecurity4LCStep4ObjType(int aLcStepId, int aObjTypeId)
  {
    this.lcStepId = aLcStepId;
    this.objTypeId = aObjTypeId;
  }

  public int MaintainedCategory => 3;

  public Tuple<int, object> Applicability => (Tuple<int, object>) null;

  public IDBSecurity GetSecurity(IUserSession session, object id)
  {
    return session.GetAttributeLCSecurity((int) id, this.lcStepId, this.objTypeId);
  }
}
