// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.MetadataUpdates.LevelNode
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Xml;


namespace Intermech.Kernel.Services.MetadataUpdates;

internal sealed class LevelNode(IUserSession session, XmlNode node) : XMLPropertyNode<int>(session, node, "F_LEVEL_ID")
{
  protected override int GetValue(IUserSession session, string nodeAttributeValue)
  {
    return nodeAttributeValue != string.Empty && GuidHelper.IsGuid(nodeAttributeValue) ? session.GetLifecycleLevel(new Guid(nodeAttributeValue), true).LevelID : 0;
  }
}
