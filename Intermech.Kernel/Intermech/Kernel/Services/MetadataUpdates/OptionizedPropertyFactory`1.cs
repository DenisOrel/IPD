// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.MetadataUpdates.OptionizedPropertyFactory`1
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Xml;


namespace Intermech.Kernel.Services.MetadataUpdates;

internal class OptionizedPropertyFactory<TOptions> : PropertyFactory where TOptions : Enum, IConvertible
{
  protected override IPropertyNode GetPropertyNode(
    IUserSession session,
    XmlNode node,
    string nodeID)
  {
    return !nodeID.Contains("F_OPTIONS") ? base.GetPropertyNode(session, node, nodeID) : (IPropertyNode) new XMLOptionPropertyNode<TOptions>(session, node, nodeID);
  }

  public TOptions GetOptions(TOptions options) => this.GetOptions(options, false);

  public TOptions GetOptions(TOptions options, bool obligatoryOnly)
  {
    List<IPropertyNode> all = this.propertyNodes.FindAll((Predicate<IPropertyNode>) (x => x is XMLOptionPropertyNode<TOptions>));
    if (all != null)
    {
      foreach (IPropertyNode propertyNode in all)
      {
        if (!obligatoryOnly || propertyNode.Obligatory)
        {
          Tuple<TOptions, int> tuple = (Tuple<TOptions, int>) ((XMLPropertyNode<Tuple<TOptions, int>>) propertyNode).Value;
          options = (TOptions) Enum.ToObject(typeof (TOptions), tuple.Item2 > 0 ? options.ToInt64((IFormatProvider) null) | tuple.Item1.ToInt64((IFormatProvider) null) : options.ToInt64((IFormatProvider) null) & ~tuple.Item1.ToInt64((IFormatProvider) null));
        }
      }
    }
    return options;
  }
}
