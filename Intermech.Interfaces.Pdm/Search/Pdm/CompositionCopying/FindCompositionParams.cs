// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.CompositionCopying.FindCompositionParams
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using Intermech.Kernel.Search;
using Intermech.Search.CompositionContexts;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Search.Pdm.CompositionCopying;

[Serializable]
public sealed class FindCompositionParams
{
  public static bool Check(FindCompositionParams @params)
  {
    if (@params == null)
      throw new ArgumentNullException("@params");
    return @params.RelationTypes != null && @params.RelationTypes.Length != 0 && !RelationTypeHelper.IsAnyUnknownRelationTypeID((IEnumerable<int>) @params.RelationTypes);
  }

  public FindCompositionParams(long projectVersionID)
  {
    this.ProjectVersionID = !ObjectHelper.IsUnknownObjectVersionID(projectVersionID) ? projectVersionID : throw new ArgumentException();
  }

  public long ProjectVersionID { get; private set; }

  public int[] RelationTypes { get; set; }

  public int[] ObjectTypes { get; set; }

  public string FiltrationOwnerID { get; set; }

  public CompositionContext[] CompositionContexts { get; set; }

  public DBRecordSetParams RecordSetParams { get; set; }
}
