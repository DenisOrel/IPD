// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.SynchronizedArticleAttributes
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Tools.Data;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

public class SynchronizedArticleAttributes : SynchronizedObjectAttributes
{
  protected SynchronizedArticleAttributes(IIntegratorSettingsService service)
    : base(service)
  {
  }

  protected override ICollection<StringKey> GetPredefinedAttributes()
  {
    ICollection<StringKey> predefinedAttributes = base.GetPredefinedAttributes();
    predefinedAttributes.Add((StringKey) IDCache.Default.Designation.Text);
    predefinedAttributes.Add((StringKey) IDCache.Default.OKPCode.Text);
    predefinedAttributes.Add((StringKey) IDCache.Default.Name.Text);
    predefinedAttributes.Add((StringKey) IDCache.Default.Mass.Text);
    predefinedAttributes.Add((StringKey) IDCache.Default.Material.Text);
    return predefinedAttributes;
  }
}
