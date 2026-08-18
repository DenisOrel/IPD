// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Server.VedomostiSortingCache
// Assembly: Intermech.Expert.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8532AAAD-1C72-4C22-AA34-A49C95D2B71F
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Expert.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Expert;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Expert.Server;

public class VedomostiSortingCache
{
  private IUserSession _iUserSession;
  private SortSchema _sortSchema;
  private string currentTriple = "";
  private SectionSortSchema activeSchema;

  public VedomostiSortingCache(IUserSession iUserSession, long scriptID, List<Triple> list)
    : this(iUserSession, scriptID, list, "По умолчанию")
  {
  }

  public VedomostiSortingCache(
    IUserSession iUserSession,
    long scriptID,
    List<Triple> list,
    string currentTriple)
  {
    this._iUserSession = iUserSession;
    this._sortSchema = (SortSchema) VedomostiSettingsStructure.CreateSettingsLevelFromObject(this._iUserSession, scriptID, -1, -1L, AvsIDCache.Attr_SortSchema, typeof (SortSchema));
    this._sortSchema.TripleList = list;
    this.CurrentTriple = currentTriple;
  }

  public string CurrentTriple
  {
    get => this.currentTriple;
    set
    {
      this.currentTriple = value;
      this.activeSchema = this._sortSchema.GetSectionSchemaByTripleName(this.currentTriple);
      if (this.activeSchema == null)
        this.activeSchema = this._sortSchema.GetSectionSchemaByTripleName("По умолчанию");
      if (this.activeSchema != null)
        return;
      this.activeSchema = new SectionSortSchema("По умолчанию");
      this.activeSchema.LoadDefaultVedomostiSchema(this._iUserSession);
    }
  }

  public int Compare(long objId1, long objId2, HybridRowExp dr1, HybridRowExp dr2)
  {
    if (dr1 != null && dr2 != null && this.activeSchema != null)
    {
      foreach (AttributeSortSchema attributeSortSchema in this.activeSchema.AttributeSortSchemas)
      {
        int indexByName = dr1.Columns.GetIndexByName(attributeSortSchema.attributeGuid.ToString());
        if (indexByName != -1)
        {
          object x = dr1[indexByName];
          object y = dr2[indexByName];
          int num = ((IComparer) attributeSortSchema).Compare(x, y);
          if (num != 0)
            return num;
        }
      }
    }
    return 0;
  }
}
