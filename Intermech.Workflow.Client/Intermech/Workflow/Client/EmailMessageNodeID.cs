// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.EmailMessageNodeID
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Interfaces;
using Intermech.Navigator.DBObjects;
using System;

#nullable disable
namespace Intermech.Workflow.Client;

public class EmailMessageNodeID : NodeID, IComparable, IEmailMessageNodeID
{
  private string _messageID;
  private string _inReplyTo;
  private long _officeDocID;

  public EmailMessageNodeID(
    int objTypeId,
    long objId,
    long id,
    long checkedOutBy,
    long prjLinkId,
    int lcStepID,
    string caption,
    int relTypeID,
    long owner,
    long sorting,
    ObjectFiltrationState state,
    long version,
    long baseVersion,
    string siteID,
    long modificationID,
    string messageID,
    string inReplyTo,
    long officeDocID)
    : base(objTypeId, objId, id, checkedOutBy, prjLinkId, lcStepID, caption, relTypeID, owner, sorting, state, version, baseVersion, siteID, 0L, Guid.Empty, modificationID)
  {
    this._messageID = messageID;
    this._inReplyTo = inReplyTo;
    this._officeDocID = officeDocID;
  }

  public int CompareTo(object obj)
  {
    if (!(obj is EmailMessageNodeID emailMessageNodeId))
      return -1;
    return emailMessageNodeId.ObjectID != this.ObjectID ? 1 : 0;
  }

  public string MessageID => this._messageID;

  public string InReplyTo => this._inReplyTo;

  public long OfficeDocID => this._officeDocID;
}
