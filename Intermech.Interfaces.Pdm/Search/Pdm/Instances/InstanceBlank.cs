// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.Instances.InstanceBlank
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using Intermech.Search.Utilities;
using System;

#nullable disable
namespace Intermech.Search.Pdm.Instances;

[Serializable]
public sealed class InstanceBlank
{
  public InstanceBlank(long prototypeVersionID)
  {
    this.PrototypeVersionID = !ObjectHelper.IsUnknownObjectVersionID(prototypeVersionID) ? prototypeVersionID : throw new ArgumentException();
  }

  public long PrototypeVersionID { get; private set; }

  public long BasedOnVersionID { get; set; } = -1;

  public string Designation { get; set; }

  public string Number { get; set; }

  public bool CopyCompositionAndAttributesOfPrototype { get; set; }
}
