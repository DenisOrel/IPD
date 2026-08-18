// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.ObjectAnalyzerHelper
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.WebPortal;
using System.Collections.Generic;


namespace Intermech.Kernel.Services.PortalServices;

internal static class ObjectAnalyzerHelper
{
  public static ICustomObjectAnalyzer GetAnalyzer(
    IUserSession session,
    PublishType publishType,
    ExtendedPublishOptions options,
    Dictionary<int, List<int>> freeAttributesCache)
  {
    return (ICustomObjectAnalyzer) new CustomObjectAnalyzer(session, publishType, options, freeAttributesCache);
  }
}
