// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.Synchronization.SynchronizationAttributesUpdater
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Imbase.Server.UpdaterStates;
using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Imbase.Server.Synchronization;

internal class SynchronizationAttributesUpdater
{
  public IUserSession Session { get; }

  public IDBObject Obj { get; set; }

  public long ImbaseObjId { get; }

  public long ImbaseRecId { get; }

  public bool CreateVersion { get; }

  public HashSet<AttributeValues> NewAttributeValues { get; }

  public GetAttributeValuesModes AttributeValuesModes { get; } = GetAttributeValuesModes.IncludeName | GetAttributeValuesModes.IncludeGuid | GetAttributeValuesModes.CheckWriteAccess;

  public IObjUpdaterState State { get; set; }

  public ILogSupport Log { get; }

  public SynchronizationAttributesUpdater(
    IUserSession session,
    IDBObject obj,
    long imbaseObjId,
    long recordId,
    HashSet<AttributeValues> attributeVals,
    bool createVersion,
    ILogSupport log)
  {
    this.Session = session;
    this.Obj = obj;
    this.ImbaseObjId = imbaseObjId;
    this.ImbaseRecId = recordId;
    this.NewAttributeValues = attributeVals;
    this.CreateVersion = createVersion;
    this.Log = log;
    this.State = (IObjUpdaterState) new CheckBindWithImbaseState();
  }

  public SynchObjectsStatus Update() => this.State.Handle(this);
}
