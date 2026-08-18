// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.MetadataUpdates.LCStepNode
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Xml;


namespace Intermech.Kernel.Services.MetadataUpdates;

internal sealed class LCStepNode(IUserSession session, XmlNode node) : TypeNode(session, node, "F_LC_STEP", -1)
{
  protected override int GetTypeID(IUserSession session, Guid guid)
  {
    return session.GetLifecycleStep(guid, true).LCStep;
  }
}
