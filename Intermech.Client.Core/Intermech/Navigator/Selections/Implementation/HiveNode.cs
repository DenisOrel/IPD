
// Type: Intermech.Navigator.Selections.Implementation.HiveNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.DB;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Navigator.Selections.Implementation;

public class HiveNode : CompositeNode, IContextAware
{
  private int _selTypeID;
  private ITopBinding _binding;
  private IConditionsProvider _externalConditions;
  /// <summary>Контейнер сервисов</summary>
  private IServiceProvider _services;

  public HiveNode(int selTypeID, ITopBinding binding)
    : this(selTypeID, binding, (IConditionsProvider) null)
  {
  }

  public HiveNode(int selTypeID, ITopBinding binding, IConditionsProvider externalConditions)
  {
    this._selTypeID = selTypeID;
    this._binding = binding;
    this._externalConditions = externalConditions;
  }

  /// <summary>Контейнер сервисов</summary>
  public IServiceProvider Services
  {
    [DebuggerStepThrough] get => this._services;
    set => this._services = value;
  }

  protected override List<PartSlot> CreateFolderSlots()
  {
    return this._binding == null ? (List<PartSlot>) null : this.SlotsFromSinglePart((INodePart) new TopSelectionsPart(this._selTypeID, this._binding, this._externalConditions, this.Services));
  }
}
