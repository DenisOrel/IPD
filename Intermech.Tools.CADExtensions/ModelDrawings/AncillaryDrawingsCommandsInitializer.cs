// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.CADExtensions.ModelDrawings.AncillaryDrawingsCommandsInitializer
// Assembly: Intermech.Tools.CADExtensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 35CC158B-C7AB-4543-B377-24CF4B98BDA2
// Assembly location: D:\IPS\Client\Intermech.Tools.CADExtensions.dll

using Intermech.ApplicationModel;
using Intermech.Interfaces;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Tools.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.CADExtensions.ModelDrawings;

internal sealed class AncillaryDrawingsCommandsInitializer : InitializerModule
{
  private IFactory navigatorFactory;
  private Func<AncillaryDrawingsCommandsProvider> commandsProviderFactory;
  private IDCache idCache;

  public AncillaryDrawingsCommandsInitializer(
    IFactory navigatorFactory,
    Func<AncillaryDrawingsCommandsProvider> commandsProviderFactory,
    IDCache idCache)
  {
    this.navigatorFactory = navigatorFactory;
    this.commandsProviderFactory = commandsProviderFactory;
    this.idCache = idCache;
  }

  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.AddCommandItemsToContextMenuTemplate();
    this.AddCommandsProviderToNavigator();
  }

  private void AddCommandItemsToContextMenuTemplate()
  {
    MenuTemplate contextMenuTemplate = this.navigatorFactory.ContextMenuTemplate;
    contextMenuTemplate.BeginUpdate();
    try
    {
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("ModelDrawing3D", "Чертежи 3D", -1, 23, 30));
      MenuTemplateNode menuTemplateNode = contextMenuTemplate["ModelDrawing3D"];
      menuTemplateNode.Nodes.Add(new MenuTemplateNode("EditModelDrawing", "Редактировать чертеж", -1, 23, 31 /*0x1F*/));
      menuTemplateNode.Nodes.Add(new MenuTemplateNode("ShowModelDrawing", "Смотреть чертеж", -1, 23, 32 /*0x20*/));
      menuTemplateNode.Nodes.Add(new MenuTemplateNode("ShowModelDrawingWithOptions", "Смотреть чертеж...", -1, 23, 33));
      menuTemplateNode.Nodes.Add(new MenuTemplateNode("CreateModelDrawingAuthenticFile", "Создать аутентичный файл чертежа", -1, 23, 35));
    }
    finally
    {
      contextMenuTemplate.EndUpdate();
    }
  }

  private void AddCommandsProviderToNavigator()
  {
    List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(this.idCache.AssemblyDocuments.Id);
    childrenIdRecursive.AddRange((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(this.idCache.PartDocuments.Id));
    if (childrenIdRecursive.Count == 0)
      return;
    AncillaryDrawingsCommandsProvider provider = this.commandsProviderFactory();
    foreach (int typeID in childrenIdRecursive)
      this.navigatorFactory.AddCommandsProvider(1, typeID, (ICommandsProvider) provider);
  }
}
