// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.TypeAttribute
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using System;


namespace Intermech.Kernel.Services.PortalServices;

internal class TypeAttribute : Attribute
{
  public Type Type;

  public TypeAttribute(Type type) => this.Type = type;
}
