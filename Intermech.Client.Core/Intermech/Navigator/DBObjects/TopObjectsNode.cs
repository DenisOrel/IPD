
// Type: Intermech.Navigator.DBObjects.TopObjectsNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Navigator.DBObjects;

public class TopObjectsNode : CompositeNode, IContextAware
{
  private int _objTypeID = -1;
  /// <summary>Контейнер сервисов</summary>
  private IServiceProvider _services;

  public TopObjectsNode(int objTypeID) => this._objTypeID = objTypeID;

  /// <summary>Контейнер сервисов</summary>
  public IServiceProvider Services
  {
    [DebuggerStepThrough] get => this._services;
    set => this._services = value;
  }

  protected override List<PartSlot> CreateFolderSlots()
  {
    return this.SlotsFromSinglePart((INodePart) new TopObjectsPart(this._objTypeID, this.Services));
  }
}
