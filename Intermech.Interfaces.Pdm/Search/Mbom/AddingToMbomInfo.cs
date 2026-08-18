// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Mbom.AddingToMbomInfo
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using Intermech.Interfaces;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Search.Mbom;

[Serializable]
public sealed class AddingToMbomInfo
{
  public AddingToMbomInfo(long objectVersionID)
  {
    this.ObjectVersionID = !ObjectHelper.IsUnknownObjectVersionID(objectVersionID) ? objectVersionID : throw new ArgumentException();
    this.Children = new Dictionary<long, AddingToMbomInfo>();
  }

  public long ObjectVersionID { get; private set; }

  public MeasuredValue RemainingCount { get; set; }

  public MeasuredValue TotalCount { get; set; }

  public Dictionary<long, AddingToMbomInfo> Children { get; private set; }

  public AddingToMbomStatuses Statuses { get; set; }

  public string ErrorMessage { get; set; }

  public IEnumerable<AddingToMbomInfo> GetDescendants()
  {
    foreach (KeyValuePair<long, AddingToMbomInfo> child in this.Children)
    {
      KeyValuePair<long, AddingToMbomInfo> keyValuePair = child;
      yield return keyValuePair.Value;
      foreach (AddingToMbomInfo descendant in keyValuePair.Value.GetDescendants())
        yield return descendant;
      keyValuePair = new KeyValuePair<long, AddingToMbomInfo>();
    }
  }

  public IEnumerable<AddingToMbomInfo> GetDescendantsAndSelf()
  {
    foreach (AddingToMbomInfo descendant in this.GetDescendants())
      yield return descendant;
    yield return this;
  }
}
