// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.MetadataUpdates.XMLOptionPropertyNode`1
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Xml;


namespace Intermech.Kernel.Services.MetadataUpdates;

internal class XMLOptionPropertyNode<TOption>(IUserSession session, XmlNode node, string nodeID) : 
  XMLPropertyNode<Tuple<TOption, int>>(session, node, nodeID)
  where TOption : Enum
{
  protected override Tuple<TOption, int> GetValue(IUserSession session, string nodeAttributeValue)
  {
    string str = this.Name.Replace("F_OPTIONS", string.Empty);
    return str != string.Empty ? new Tuple<TOption, int>((TOption) Enum.Parse(typeof (TOption), str), Convert.ToInt32(nodeAttributeValue)) : (Tuple<TOption, int>) null;
  }
}
