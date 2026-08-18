// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.Operations.PrescanDBObjectRecord
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model.Operations;

internal sealed class PrescanDBObjectRecord
{
  public PrescanDBObjectRecord(
    DBObjectGraphVertex dbObjectVertex,
    List<DBObjectAttributeEntry> attributes,
    List<DBObjectFileEntry> files)
  {
    this.DBObjectVertex = dbObjectVertex;
    this.Attributes = attributes;
    this.Files = files;
  }

  public DBObjectGraphVertex DBObjectVertex { get; }

  public List<DBObjectAttributeEntry> Attributes { get; }

  public List<DBObjectFileEntry> Files { get; }

  public DBObjectContent Content { get; set; }
}
