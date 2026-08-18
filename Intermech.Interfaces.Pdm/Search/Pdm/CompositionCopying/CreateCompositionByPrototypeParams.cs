// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.CompositionCopying.CreateCompositionByPrototypeParams
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using Intermech.Search.CompositionContexts;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Search.Pdm.CompositionCopying;

[Serializable]
public sealed class CreateCompositionByPrototypeParams
{
  public static bool Check(CreateCompositionByPrototypeParams @params)
  {
    if (@params == null)
      throw new ArgumentNullException("@params");
    return (@params.Instances == null || @params.Instances != null && !ObjectHelper.IsAnyUnknownObjectVersionID((IEnumerable<long>) @params.Instances)) && @params.RelationTypes != null && @params.RelationTypes.Length != 0 && !RelationTypeHelper.IsAnyUnknownRelationTypeID((IEnumerable<int>) @params.RelationTypes) && @params.NewObjects != null && @params.NewObjects.Count > 0;
  }

  public CreateCompositionByPrototypeParams(long objectVersionID)
  {
    this.ObjectVersionID = !ObjectHelper.IsUnknownObjectVersionID(objectVersionID) ? objectVersionID : throw new ArgumentException();
  }

  public int[] AllowableForCreateCopyObjectTypes { get; set; }

  public long ObjectVersionID { get; private set; }

  public long[] Instances { get; set; }

  public int[] RelationTypes { get; set; }

  public string FiltrationOwnerID { get; set; }

  public CompositionContext[] CompositionContexts { get; set; }

  public Dictionary<long, long> NewObjects { get; set; }

  public long[] NeedToExculdeObjects { get; set; }

  public long[] NeedToUsePrototypeInsteadObjects { get; set; }
}
