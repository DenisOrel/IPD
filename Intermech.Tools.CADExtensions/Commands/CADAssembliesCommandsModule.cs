// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.CADExtensions.Commands.CADAssembliesCommandsModule
// Assembly: Intermech.Tools.CADExtensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 35CC158B-C7AB-4543-B377-24CF4B98BDA2
// Assembly location: D:\IPS\Client\Intermech.Tools.CADExtensions.dll

using Intermech.ApplicationModel;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.CADInterface;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.CADExtensions.Commands;

internal sealed class CADAssembliesCommandsModule : InitializerModule
{
  private IStartupService startupService;
  private IIntegratorRegistry integratorRegistry;
  private INamedImageList namedImageListService;
  private IFactory navigatorFactory;
  private Func<CADAssembliesCommandsProvider> commandsProviderFactory;

  public CADAssembliesCommandsModule(
    IStartupService startupService,
    IIntegratorRegistry integratorRegistry,
    INamedImageList namedImageListService,
    IFactory navigatorFactory,
    Func<CADAssembliesCommandsProvider> commandsProviderFactory)
  {
    if (startupService == null)
      throw new ArgumentNullException(nameof (startupService));
    if (integratorRegistry == null)
      throw new ArgumentNullException(nameof (integratorRegistry));
    if (namedImageListService == null)
      throw new ArgumentNullException(nameof (namedImageListService));
    if (navigatorFactory == null)
      throw new ArgumentNullException(nameof (navigatorFactory));
    if (commandsProviderFactory == null)
      throw new ArgumentNullException(nameof (commandsProviderFactory));
    this.startupService = startupService;
    this.integratorRegistry = integratorRegistry;
    this.namedImageListService = namedImageListService;
    this.navigatorFactory = navigatorFactory;
    this.commandsProviderFactory = commandsProviderFactory;
  }

  protected override void DoInitialize()
  {
    base.DoInitialize();
    if (this.startupService.IsStartupCompleted)
      this.OnStartupComplete((object) null, EventArgs.Empty);
    else
      this.startupService.StartupComplete += new EventHandler(this.OnStartupComplete);
  }

  private void OnStartupComplete(object sender, EventArgs e) => this.RegisterCommandProvider();

  private void RegisterCommandProvider()
  {
    List<GlobalId<int>> assemblyDocumentTypes = this.GetCADAssemblyDocumentTypes();
    if (assemblyDocumentTypes.Count == 0)
      return;
    this.AddCommandItemsToContextMenuTemplate();
    CADAssembliesCommandsProvider provider = this.commandsProviderFactory();
    foreach (LocalId<int> localId in assemblyDocumentTypes)
      this.navigatorFactory.AddCommandsProvider(1, localId.Id, (ICommandsProvider) provider);
  }

  private List<GlobalId<int>> GetCADAssemblyDocumentTypes()
  {
    List<GlobalId<int>> assemblyDocumentTypes = new List<GlobalId<int>>(16 /*0x10*/);
    foreach (object integrator in this.integratorRegistry.GetIntegrators())
    {
      ICADSettingsService service = ServiceUtils.GetService<ICADSettingsService>(integrator, false);
      if (service != null)
      {
        try
        {
          DocumentGroup byName = service.GetCADSettings().FileDocumentGroups.FindByName("Assembly", false);
          if (byName != null)
            assemblyDocumentTypes.AddRange((IEnumerable<GlobalId<int>>) byName.DocumentTypes);
        }
        catch
        {
        }
      }
    }
    return assemblyDocumentTypes;
  }

  private void AddCommandItemsToContextMenuTemplate()
  {
    MenuTemplate contextMenuTemplate = this.navigatorFactory.ContextMenuTemplate;
    contextMenuTemplate.BeginUpdate();
    try
    {
      MenuTemplateNode node = contextMenuTemplate[CADAssembliesCommandsConsts.IntegratorsMenuName];
      if (node == null)
      {
        node = new MenuTemplateNode(CADAssembliesCommandsConsts.IntegratorsMenuName, CADAssembliesCommandsConsts.IntegratorsMenuDisplayName, -1, 24, 30);
        contextMenuTemplate.Nodes.Add(node);
      }
      node.Nodes.Add(new MenuTemplateNode(CADAssembliesCommandsConsts.SaveChangesSubtreeCommandName, CADAssembliesCommandsConsts.SaveChangesSubtreeDisplayName, this.namedImageListService.ImageIndex("imgSaveChanges"), 24, 30));
      node.Nodes.Add(new MenuTemplateNode(CADAssembliesCommandsConsts.CheckInSubtreeCommandName, CADAssembliesCommandsConsts.CheckInSubtreeDisplayName, this.namedImageListService.ImageIndex("imgCheckIn"), 24, 31 /*0x1F*/));
    }
    finally
    {
      contextMenuTemplate.EndUpdate();
    }
  }

  protected override void DoShutdown()
  {
    this.startupService.StartupComplete -= new EventHandler(this.OnStartupComplete);
    base.DoShutdown();
  }
}
