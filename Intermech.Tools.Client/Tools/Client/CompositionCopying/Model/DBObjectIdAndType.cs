// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.DBObjectIdAndType
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model;

internal class DBObjectIdAndType
{
  public DBObjectIdAndType(long objectId, int objectTypeId)
  {
    this.ObjectId = objectId;
    this.ObjectTypeId = objectTypeId;
  }

  public long ObjectId { get; }

  public int ObjectTypeId { get; }
}
