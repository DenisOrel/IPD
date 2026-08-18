// Decompiled with JetBrains decompiler
// Type: Intermech.DataFormats.DBTypeIDs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.DataFormats;

public class DBTypeIDs : IDBTypeIDs
{
  private int[] _objectTypeIDs;
  private int[] _relationTypeIDs;

  public DBTypeIDs(int[] objectTypeIDs, int[] relationTypeIDs)
  {
    this._objectTypeIDs = objectTypeIDs;
    this._relationTypeIDs = relationTypeIDs;
  }

  public int[] ObjectTypeIDs => this._objectTypeIDs;

  public int[] RelationTypeIDs => this._relationTypeIDs;
}
