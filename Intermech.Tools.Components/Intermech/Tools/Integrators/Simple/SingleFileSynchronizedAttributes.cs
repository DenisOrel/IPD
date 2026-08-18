// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Simple.SingleFileSynchronizedAttributes
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Interfaces;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.Simple;

public class SingleFileSynchronizedAttributes(IIntegratorSettingsService settingService) : 
  SynchronizedObjectAttributes(settingService)
{
  protected override ICollection<StringKey> GetUserDefinedAttributes()
  {
    ICollection<StringKey> definedAttributes = base.GetUserDefinedAttributes();
    foreach (GlobalId<int> documentAttribute in ((SingleFileSettings) this.Service.GetSettingsObject()).DocumentAttributes)
      definedAttributes.Add((StringKey) documentAttribute.Name);
    return definedAttributes;
  }
}
