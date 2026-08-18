
// Type: Intermech.Navigator.DBObjects.RelationsListNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Navigator.DBObjects;

public class RelationsListNode : CompositeNode, IContextAware
{
  private long _objID;
  private IList _prjlinkIDs;
  /// <summary>Контейнер сервисов</summary>
  private IServiceProvider _services;

  public RelationsListNode(long objID, IList prjlinkIDs)
  {
    this._objID = objID;
    this._prjlinkIDs = prjlinkIDs;
    this.options = NodeOptions.CanContainsRelationsList;
  }

  /// <summary>Контейнер сервисов</summary>
  public IServiceProvider Services
  {
    [DebuggerStepThrough] get => this._services;
    set => this._services = value;
  }

  /// <summary>
  /// Создает и возвращает части, которые отвечают за элементы-папки.
  /// </summary>
  /// <returns>Коллекция частей</returns>
  protected override List<PartSlot> CreateFolderSlots()
  {
    return this.SlotsFromSinglePart((INodePart) new RelationsListPart(this._objID, this._prjlinkIDs, this.Services));
  }
}
