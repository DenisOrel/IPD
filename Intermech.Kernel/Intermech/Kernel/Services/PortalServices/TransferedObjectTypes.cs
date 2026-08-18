// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.TransferedObjectTypes
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll


namespace Intermech.Kernel.Services.PortalServices;

internal enum TransferedObjectTypes
{
  [Type(typeof (Intermech.Interfaces.WebPortal.TransferedObject))] TransferedObject,
  [Type(typeof (Intermech.Interfaces.Server.ExtendedTransferedObject))] ExtendedTransferedObject,
  [Type(typeof (Intermech.Interfaces.Server.WebPortal.PersistentObject))] PersistentObject,
  [Type(typeof (Intermech.Interfaces.Server.WebPortal.PersistentRelation))] PersistentRelation,
}
