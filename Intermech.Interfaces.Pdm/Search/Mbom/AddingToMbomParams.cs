// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Mbom.AddingToMbomParams
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using Intermech.Interfaces;
using Intermech.Search.Utilities;
using System;

#nullable disable
namespace Intermech.Search.Mbom;

[Serializable]
public sealed class AddingToMbomParams
{
  public static bool Check(AddingToMbomParams addingToMbomParams)
  {
    if (addingToMbomParams == null)
      throw new ArgumentNullException(nameof (addingToMbomParams));
    return addingToMbomParams.Count != null;
  }

  public AddingToMbomParams(long mbomVersionID, long objectVersionID)
  {
    if (ObjectHelper.IsUnknownObjectVersionID(mbomVersionID))
      throw new ArgumentException();
    if (ObjectHelper.IsUnknownObjectVersionID(objectVersionID))
      throw new ArgumentException();
    this.MbomVersionID = mbomVersionID;
    this.ObjectVersionID = objectVersionID;
  }

  public long MbomVersionID { get; private set; }

  public long ObjectVersionID { get; private set; }

  public MeasuredValue Count { get; set; }
}
