// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.Substitutes.SaveSubstitutesParams
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.Search.Pdm.Substitutes;

[Serializable]
public sealed class SaveSubstitutesParams
{
  public static bool Check(SaveSubstitutesParams @params)
  {
    if (@params == null)
      throw new ArgumentNullException("@params");
    return !ObjectHelper.IsUnknownObjectVersionID(@params.ProjectVersionID) && (@params.InstanceVersionIds == null || @params.InstanceVersionIds != null && ((IEnumerable<long>) @params.InstanceVersionIds).Where<long>((Func<long, bool>) (o => ObjectHelper.IsUnknownObjectVersionID(o))).Count<long>() == 0) && @params.Pack != null && @params.RelationTypeID != -1 && SubstitutesHelper.IsSuitableForSubstitutesRelationType(@params.RelationTypeID);
  }

  public SubstitutePack Pack { get; set; }

  public long ProjectVersionID { get; set; }

  public long[] InstanceVersionIds { get; set; }

  public int RelationTypeID { get; set; }

  public Dictionary<long, string> GroupsAffected { get; set; }
}
