// Decompiled with JetBrains decompiler
// Type: Intermech.Pdm.Server.RelationsComparerService
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Pdm;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Pdm.Server;

public class RelationsComparerService : LongLifeObject, IRelationsComparerService
{
  protected Dictionary<Guid, IRelationsComparer> _comparers = new Dictionary<Guid, IRelationsComparer>();
  protected Dictionary<int, List<IRelationsComparer>> _attrComparers = new Dictionary<int, List<IRelationsComparer>>();
  private static RelationsComparerService.IRelationsComparerClass _relationsComparerClass = new RelationsComparerService.IRelationsComparerClass();

  protected virtual void ResetCache() => this._attrComparers.Clear();

  public virtual List<IRelationsComparer> GetAttributeComparers(int attr)
  {
    if (this._attrComparers.ContainsKey(attr))
      return this._attrComparers[attr];
    List<IRelationsComparer> attributeComparers = new List<IRelationsComparer>();
    foreach (KeyValuePair<Guid, IRelationsComparer> comparer in this._comparers)
    {
      if (comparer.Value.CanCompareByAttribute(attr))
        attributeComparers.Add(comparer.Value);
    }
    attributeComparers.Sort((IComparer<IRelationsComparer>) RelationsComparerService._relationsComparerClass);
    this._attrComparers[attr] = attributeComparers;
    return attributeComparers;
  }

  public virtual void RegisterRelationsComparer(IRelationsComparer relationsComparer)
  {
    if (relationsComparer == null)
      return;
    if (this._comparers.ContainsKey(relationsComparer.ComparerGuid))
      this._comparers.Remove(relationsComparer.ComparerGuid);
    this._comparers.Add(relationsComparer.ComparerGuid, relationsComparer);
    this.ResetCache();
  }

  public virtual void UnregisterRelationsComparer(IRelationsComparer relationsComparer)
  {
    if (relationsComparer == null || !this._comparers.ContainsKey(relationsComparer.ComparerGuid))
      return;
    this._comparers.Remove(relationsComparer.ComparerGuid);
    this.ResetCache();
  }

  public virtual void UnregisterRelationsComparer(Guid relationsComparerGuid)
  {
    if (!this._comparers.ContainsKey(relationsComparerGuid))
      return;
    this._comparers.Remove(relationsComparerGuid);
    this.ResetCache();
  }

  public virtual bool EqualsTo(IUserSession session, int attrID, long prjLinkID1, long prjLinkID2)
  {
    return this.EqualsTo(session, new List<int>() { attrID }, prjLinkID1, prjLinkID2);
  }

  public virtual bool EqualsTo(
    IUserSession session,
    List<int> attrIDs,
    long prjLinkID1,
    long prjLinkID2)
  {
    if (this._comparers.Count == 0)
      return prjLinkID1 == prjLinkID2;
    if (session == null || attrIDs == null || attrIDs.Count == 0)
      return false;
    if (prjLinkID1 == prjLinkID2)
      return true;
    bool flag = true;
    List<int> attrIDs1 = new List<int>(1);
    attrIDs1.Add(0);
    for (int index1 = 0; index1 < attrIDs.Count; ++index1)
    {
      attrIDs1[0] = attrIDs[index1];
      List<IRelationsComparer> attributeComparers = this.GetAttributeComparers(attrIDs[index1]);
      if (attributeComparers.Count == 0)
        return false;
      for (int index2 = 0; index2 < attributeComparers.Count; ++index2)
      {
        flag &= attributeComparers[index2].EqualsTo(session, attrIDs1, prjLinkID1, prjLinkID2);
        if (!flag)
          return false;
      }
    }
    return flag;
  }

  public virtual bool EqualsTo(
    IUserSession session,
    int attrID,
    long prjLinkID1,
    long prjLinkID2,
    DataRow row1,
    DataRow row2,
    bool useSubstAttrs)
  {
    return this.EqualsTo(session, new List<int>() { attrID }, prjLinkID1, prjLinkID2, row1, row2, useSubstAttrs);
  }

  public virtual bool EqualsTo(
    IUserSession session,
    List<int> attrIDs,
    long prjLinkID1,
    long prjLinkID2,
    DataRow row1,
    DataRow row2,
    bool useSubstAttrs)
  {
    if (this._comparers.Count == 0)
      return prjLinkID1 == prjLinkID2;
    if (session == null || attrIDs == null || attrIDs.Count == 0)
      return false;
    if (prjLinkID1 == prjLinkID2)
      return true;
    bool flag = true;
    List<int> attrIDs1 = new List<int>(1);
    attrIDs1.Add(0);
    for (int index1 = 0; index1 < attrIDs.Count; ++index1)
    {
      attrIDs1[0] = attrIDs[index1];
      List<IRelationsComparer> attributeComparers = this.GetAttributeComparers(attrIDs[index1]);
      if (attributeComparers.Count == 0)
        return false;
      for (int index2 = 0; index2 < attributeComparers.Count; ++index2)
      {
        flag &= attributeComparers[index2].EqualsTo(session, attrIDs1, prjLinkID1, prjLinkID2, row1, row2, useSubstAttrs);
        if (!flag)
          return false;
      }
    }
    return flag;
  }

  private class IRelationsComparerClass : IComparer<IRelationsComparer>
  {
    public int Compare(IRelationsComparer x, IRelationsComparer y)
    {
      return x == null || y == null ? 0 : x.Capabilities.CompareTo((object) y.Capabilities);
    }
  }
}
