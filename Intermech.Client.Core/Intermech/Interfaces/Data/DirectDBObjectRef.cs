
// Type: Intermech.Interfaces.Data.DirectDBObjectRef
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Interfaces.Data;

public sealed class DirectDBObjectRef : IDBObjectRef
{
  private readonly long objectId;

  public DirectDBObjectRef(long objectId)
  {
    this.objectId = objectId != 0L ? objectId : throw new ArgumentException();
  }

  public long GetObjectId() => this.objectId;
}
