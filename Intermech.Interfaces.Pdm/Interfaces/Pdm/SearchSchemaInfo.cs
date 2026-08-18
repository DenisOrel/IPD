// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.SearchSchemaInfo
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Pdm;

[Serializable]
public class SearchSchemaInfo : IEquatable<SearchSchemaInfo>
{
  public long SchemeID { get; private set; }

  public string Name { get; private set; }

  public List<long> Roles { get; private set; }

  public SearchSchemaInfo(string name, long schemeID, List<long> roles)
  {
    this.Name = name;
    this.SchemeID = schemeID;
    this.Roles = roles;
  }

  public override bool Equals(object obj)
  {
    return !(obj is SearchSchemaInfo other) ? base.Equals(obj) : this.Equals(other);
  }

  public bool Equals(SearchSchemaInfo other)
  {
    return other != null && this.SchemeID.Equals(other.SchemeID);
  }

  public override int GetHashCode() => this.SchemeID.GetHashCode();
}
