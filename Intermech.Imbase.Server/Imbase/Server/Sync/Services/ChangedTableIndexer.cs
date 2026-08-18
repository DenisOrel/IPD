// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.Sync.Services.ChangedTableIndexer
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Interfaces;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.Imbase.Server.Sync.Services;

internal class ChangedTableIndexer : LongLifeObject, IChangedTableIndexer
{
  private HashSet<long> _tableObjsIds = new HashSet<long>();

  public void AddTableObjID(long objId) => this._tableObjsIds.Add(objId);

  public void Clear() => this._tableObjsIds.Clear();

  public long[] GetChangedTableIds() => this._tableObjsIds.ToArray<long>();
}
