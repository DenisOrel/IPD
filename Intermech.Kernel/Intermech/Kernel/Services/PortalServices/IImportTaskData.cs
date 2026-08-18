// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.IImportTaskData
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces.Server;
using System;
using System.Collections.Generic;
using System.IO;


namespace Intermech.Kernel.Services.PortalServices;

internal interface IImportTaskData
{
  Dictionary<Guid, ImportedInfo> ReadLinks(int count, BinaryReader reader);

  void SaveLinks(Dictionary<Guid, ImportedInfo> data, BinaryWriter writer);
}
