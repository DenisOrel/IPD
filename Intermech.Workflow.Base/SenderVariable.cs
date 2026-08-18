// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.SenderVariable
// Assembly: Intermech.Workflow.Base, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 43DB3E33-56C8-49B7-85B7-A2947193D068
// Assembly location: D:\IPS\Client\Intermech.Workflow.Base.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Base.xml

using Intermech.Interfaces;
using System;


namespace Intermech.Workflow
{
    /// <summary>Системная переменная отправитель</summary>
    [Serializable]
    public class SenderVariable(VarList owner, IDBObject obj) : CalculatedSystemVariable(owner, obj, wfConsts.SysVarSenderID)
    {
      protected override string CalcValue()
      {
        long ID = 0;
        IDBObject dbObject = this.GetObject();
        try
        {
          IDBAttribute attributeById = dbObject.GetAttributeByID(wfConsts.AttrSenderID);
          if (attributeById != null)
            ID = attributeById.AsInteger;
          ParticipantList participantList = new ParticipantList(dbObject.Session);
          if (ID != 0L)
            participantList.AddParticipant(ParticipantKind.User, ID);
          return participantList.AsString;
        }
        finally
        {
          this.ReleaseObject();
        }
      }
    }
}
