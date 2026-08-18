// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.Scripting.CSharp.UpdatedDBObjectInfo
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using System;

#nullable disable
namespace Intermech.DatabaseConfigurator.Scripting.CSharp;

internal sealed class UpdatedDBObjectInfo
{
  public UpdatedDBObjectInfo(long objectId, int objectTypeId, bool isNew)
  {
    if (objectId == 0L)
      throw new ArgumentException("Идентификатор версии объекта не задан.", nameof (objectId));
    if (objectTypeId == -1)
      throw new ArgumentException("Идентификатор типа объекта не задан.", nameof (objectTypeId));
    this.ObjectId = objectId;
    this.ObjectTypeId = objectTypeId;
    this.IsNew = isNew;
  }

  public long ObjectId { get; private set; }

  public int ObjectTypeId { get; private set; }

  public bool IsNew { get; private set; }
}
