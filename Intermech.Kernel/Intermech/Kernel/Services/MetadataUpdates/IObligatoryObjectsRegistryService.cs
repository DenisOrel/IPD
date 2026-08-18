// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.MetadataUpdates.IObligatoryObjectsRegistryService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces.MetadataUpdates;


namespace Intermech.Kernel.Services.MetadataUpdates;

internal interface IObligatoryObjectsRegistryService
{
  void RegisterObligatoryObject(int categoryID, object id);

  void RegisterObligatoryObjectElement(int categoryID, object id, ObligatoryElementKey elementKey);
}
