// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.Instances.CreateInstancesParams
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.Search.Pdm.Instances;

[Serializable]
public sealed class CreateInstancesParams
{
  public static bool CheckCreateInstancesParams(CreateInstancesParams createInstancesParams)
  {
    if (createInstancesParams == null)
      throw new ArgumentNullException(nameof (createInstancesParams));
    return createInstancesParams.Blanks != null && createInstancesParams.Blanks.Length != 0 && ((IEnumerable<InstanceBlank>) createInstancesParams.Blanks).Select<InstanceBlank, long>((Func<InstanceBlank, long>) (o => o.PrototypeVersionID)).Distinct<long>().All<long>((Func<long, bool>) (o => InstancesHelper.CheckObjectForCreateInstances(o)));
  }

  public InstanceBlank[] Blanks { get; set; }
}
