// Decompiled with JetBrains decompiler
// Type: Intermech.Compass3D.Integrator.Drawing2DFormatter
// Assembly: Intermech.Compass3D.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E9700F29-129D-4EBE-8417-980BAD3DC32C
// Assembly location: D:\IPS\Client\Intermech.Compass3D.Integrator.dll

using Intermech.CADInterface.Proxies;
using Intermech.Data;
using Intermech.Tools.Integrators.CADInterface;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Compass3D.Integrator;

internal sealed class Drawing2DFormatter : CADInterfaceFormatter
{
  protected override ValueBag DoRead(IValueBagContainer container, ICollection<StringKey> valueKeys)
  {
    ValueBag values = base.DoRead(container, valueKeys);
    this.MakeDesignTypeReadonly(values);
    return values;
  }

  private void MakeDesignTypeReadonly(ValueBag values)
  {
    values.Find((StringKey) CADVirtualAttributes.DocumentDesignType)?.Flags.Set(NamedFlags.ReadOnly);
  }
}
