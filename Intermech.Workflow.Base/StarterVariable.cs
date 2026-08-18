// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.StarterVariable
// Assembly: Intermech.Workflow.Base, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 43DB3E33-56C8-49B7-85B7-A2947193D068
// Assembly location: D:\IPS\Client\Intermech.Workflow.Base.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Base.xml

using Intermech.Interfaces;
using System;


namespace Intermech.Workflow
{
    /// <summary>Системная переменная стартер процесса</summary>
    [Serializable]
    public class StarterVariable(VarList owner, IDBObject obj) : CalculatedSystemVariable(owner, obj, wfConsts.SysVarStarterID)
    {
      protected override string CalcValue()
      {
        IDBObject dbObject = this.GetObject();
        try
        {
          if (dbObject == null)
            return (string) null;
          long ownerId = dbObject.OwnerID;
          ParticipantList participantList = new ParticipantList(dbObject.Session);
          participantList.AddParticipant(ParticipantKind.User, ownerId);
          return participantList.AsString;
        }
        finally
        {
          this.ReleaseObject();
        }
      }
    }
}
