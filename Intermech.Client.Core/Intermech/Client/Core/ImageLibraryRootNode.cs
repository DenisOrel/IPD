
// Type: Intermech.Client.Core.ImageLibraryRootNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Client.Core;

/// <summary>Summary description for ImageLibrary.</summary>
public class ImageLibraryRootNode : CompositeNode, IContextAware
{
  /// <summary>Контейнер сервисов</summary>
  private IServiceProvider _services;

  /// <summary>Контейнер сервисов</summary>
  public IServiceProvider Services
  {
    [DebuggerStepThrough] get => this._services;
    set => this._services = value;
  }

  protected override List<PartSlot> CreateFolderSlots()
  {
    return this.SlotsFromSinglePart((INodePart) new TopObjectsPart(Intermech.Client.Core.Thumbnail.Consts.ImageLibraryFolderTypeID, this.Services));
  }

  protected override List<PartSlot> CreateNonFolderSlots()
  {
    return this.SlotsFromSinglePart((INodePart) new ObjectsPart(Intermech.Client.Core.Thumbnail.Consts.ImageLibraryItemTypeID, this.Services));
  }
}
