// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.CheckAttribute4ObjectTypeCollection
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using System.Collections.Generic;


namespace Intermech.Kernel.Briefcase;

internal sealed class CheckAttribute4ObjectTypeCollection(
  UserSession session,
  List<int> briefAttributes,
  IDBObjectType objType,
  string uniIdentifiler,
  CheckOptions options) : CheckAttribute4TypeCollection<IDBObjectType>(session, briefAttributes, objType, BriefcaseConsts.logAttribute4ObjectTypeCategory, string.Format(BriefcaseConsts.logAttribute4objTypeAddUniIdentifiler, (object) uniIdentifiler), options)
{
}
