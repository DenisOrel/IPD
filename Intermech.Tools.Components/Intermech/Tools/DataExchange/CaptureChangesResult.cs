// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.DataExchange.CaptureChangesResult
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.DataExchange;

public sealed class CaptureChangesResult
{
  private readonly long objectId;
  private readonly string fullPath;
  private readonly CaptureChangesDatabase database;

  public CaptureChangesResult(long objectId, string fullPath, CaptureChangesDatabase database)
  {
    this.objectId = objectId;
    this.fullPath = fullPath;
    this.database = database;
  }

  public long ObjectId => this.objectId;

  public string FullPath => this.fullPath;

  public CaptureChangesDatabase Database => this.database;

  public List<long> ChangedObjectIds { get; set; } = new List<long>();

  public List<string> Errors { get; internal set; } = new List<string>();
}
