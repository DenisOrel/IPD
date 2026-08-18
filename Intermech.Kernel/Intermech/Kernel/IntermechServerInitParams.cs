// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.IntermechServerInitParams
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.ApplicationModel;
using Intermech.Interfaces.Data.Metadata;
using Intermech.Interfaces.Plugins;
using Intermech.Interfaces.Server;
using Intermech.Runtime;
using System;


namespace Intermech.Kernel;

public sealed class IntermechServerInitParams
{
  public bool RebuildViewsMode { get; set; }

  public bool OnlyPatchBase { get; set; }

  public bool ClearPatchFlag { get; set; }

  public bool SkipMetadataScripts { get; set; }

  public bool SkipPlugins { get; set; }

  public ISharedLibraryInitializerService SharedLibraryInitializerService { get; set; }

  public IMetadataChangeMonitor MetadataChangeMonitor { get; set; }

  public MetadataResolverFactory MetadataResolversFactory { get; set; }

  public ICustomServices CustomServices { get; set; }

  public Action<PluginManager> PluginManagerConfigureAction { get; set; }

  public void Validate()
  {
    if (this.SharedLibraryInitializerService == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "SharedLibraryInitializerService");
    if (this.MetadataChangeMonitor == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "MetadataChangeMonitor");
    if (this.MetadataResolversFactory == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "MetadataResolversFactory");
    if (this.CustomServices == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "CustomServices");
  }
}
