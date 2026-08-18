// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.MetadataUpdates.CalculateObjectIDValue
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;


namespace Intermech.Kernel.Services.MetadataUpdates;

internal sealed class CalculateObjectIDValue(IUserSession session, Guid guid) : 
  CalculateObjectIdentifierValue(session, guid)
{
  protected override long GetValueFromBase() => this.session.GetObject(this.guid, true).ObjectID;
}
