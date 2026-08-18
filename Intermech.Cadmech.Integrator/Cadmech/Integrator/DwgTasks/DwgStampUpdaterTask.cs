// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.DwgTasks.DwgStampUpdaterTask
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Cadmech.Integrator.DwgTasks;

internal sealed class DwgStampUpdaterTask : DwgTask
{
  private bool drawingIsModified;

  protected override void CloseDrawing()
  {
    if (this.drawingIsModified)
    {
      Intermech.Client.Core.Show.Net.ShowDll.ShowDll.SaveDWGFile(this.DrawingFilePath);
      this.drawingIsModified = false;
    }
    base.CloseDrawing();
  }

  public bool UpdateStamp(
    ICollection<StringKey> stampParameters,
    Predicate<ValueBag> stampPredicate,
    ICollection<ValueRecord> newParameters)
  {
    if (stampParameters == null)
      throw new ArgumentNullException(nameof (stampParameters));
    if (stampPredicate == null)
      throw new ArgumentNullException(nameof (stampPredicate));
    if (newParameters == null)
      throw new ArgumentNullException(nameof (newParameters));
    return newParameters.Count == 0 || this.TryProcessStamp(stampParameters, stampPredicate, (Action<ValueBag>) (stampBag =>
    {
      foreach (ValueRecord newParameter in (IEnumerable<ValueRecord>) newParameters)
      {
        if (newParameter.DataType == typeof (string))
        {
          string str = newParameter.Read<string>((string) null);
          Intermech.Client.Core.Show.Net.ShowDll.ShowDll.SetParameter((string) newParameter.Key, str);
          this.drawingIsModified = ((this.drawingIsModified ? 1 : 0) | 1) != 0;
        }
      }
    }));
  }
}
