
// Type: Intermech.Search.CompositionContexts.CompositionContextSet
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;
using System.Drawing.Design;


namespace Intermech.Search.CompositionContexts;

[Editor(typeof (CompositionContextSetTypeEditor), typeof (UITypeEditor))]
[TypeConverter(typeof (CompositionContextSetConverter))]
[Serializable]
public sealed class CompositionContextSet
{
  static CompositionContextSet()
  {
    CompositionContextSet.Empty = new CompositionContextSet(CompositionContextClientHelper.CompositionContextsCommon);
    CompositionContextSet.Default = new CompositionContextSet(CompositionContextClientHelper.CompositionContextsDefault);
  }

  public static CompositionContextSet Empty { get; private set; }

  public static CompositionContextSet Default { get; private set; }

  public CompositionContextSet(CompositionContext[] compositionContexts)
  {
    this.CompositionContexts = compositionContexts != null ? compositionContexts : throw new ArgumentNullException(nameof (compositionContexts));
  }

  public CompositionContext[] CompositionContexts { get; private set; }

  public override bool Equals(object obj)
  {
    if (this == obj)
      return true;
    if (!(obj is CompositionContextSet compositionContextSet) || this.CompositionContexts.Length != compositionContextSet.CompositionContexts.Length)
      return false;
    for (int index = 0; index < this.CompositionContexts.Length; ++index)
    {
      if (this.CompositionContexts[index].Value != compositionContextSet.CompositionContexts[index].Value)
        return false;
    }
    return true;
  }

  public override int GetHashCode()
  {
    int hashCode = 0;
    foreach (CompositionContext compositionContext in this.CompositionContexts)
      hashCode ^= compositionContext.GetHashCode();
    return hashCode;
  }
}
