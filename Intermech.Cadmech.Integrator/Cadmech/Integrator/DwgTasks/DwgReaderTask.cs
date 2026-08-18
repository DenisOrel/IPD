// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.DwgTasks.DwgReaderTask
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Cadmech.Integrator.DwgTasks;

internal sealed class DwgReaderTask : DwgTask
{
  public List<string> GetXRefs()
  {
    this.CheckDrawingIsOpen();
    return new List<string>((IEnumerable<string>) (Intermech.Client.Core.Show.Net.ShowDll.ShowDll.GetReferenceOnly() ?? new string[0]));
  }

  public ValueBag SeekStamp(List<StringKey> stampParameters, Predicate<ValueBag> stampPredicate)
  {
    if (stampPredicate == null)
      throw new ArgumentNullException(nameof (stampPredicate));
    ValueBag result = (ValueBag) null;
    if (this.TryProcessStamp((ICollection<StringKey>) stampParameters, stampPredicate, (Action<ValueBag>) (stampBag => result = stampBag.Clone())))
      result.SetFlagForAll(NamedFlags.ReadOnly);
    return result;
  }
}
