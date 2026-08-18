// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Server.ExpertTableFoundationCache
// Assembly: Intermech.Expert.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8532AAAD-1C72-4C22-AA34-A49C95D2B71F
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Expert.Server.dll

using Intermech.Interfaces.Expert;
using System.Collections;

#nullable disable
namespace Intermech.Expert.Server;

internal class ExpertTableFoundationCache : ArrayList
{
  public override bool Contains(object item) => this.IndexOf(item) >= 0;

  public int Add(ExpertTableFoundationClass fClass) => this.Add((object) fClass);

  public override int IndexOf(object value)
  {
    if (value is CalcAttrPair)
    {
      CalcAttrPair calcAttrPair = value as CalcAttrPair;
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].CalcAttrPair.Equals((object) calcAttrPair))
          return index;
      }
    }
    return base.IndexOf(value);
  }

  public ExpertTableFoundationClass this[int index]
  {
    get => base[index] as ExpertTableFoundationClass;
    set => this[index] = (object) value;
  }

  public ExpertTableFoundationClass GetClassByCalcAttrPair(CalcAttrPair calcAttrPair)
  {
    int index = this.IndexOf((object) calcAttrPair);
    return index >= 0 ? this[index] : (ExpertTableFoundationClass) null;
  }
}
