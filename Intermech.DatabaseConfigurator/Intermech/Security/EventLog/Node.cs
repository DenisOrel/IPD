// Decompiled with JetBrains decompiler
// Type: Intermech.Security.EventLog.Node
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Kernel.Search;
using Intermech.Navigator.EventLog;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

#nullable disable
namespace Intermech.Security.EventLog;

internal class Node : CompositeNode, IContextAware
{
  protected override List<PartSlot> CreateFolderSlots()
  {
    return this.SlotsFromSinglePart((INodePart) new FiltersNodePart());
  }

  protected override List<PartSlot> CreateNonFolderSlots()
  {
    EventsNodePart part = new EventsNodePart((ConditionStructure[]) null, (HybridDictionary) null);
    part.Services = this.Services;
    return this.SlotsFromSinglePart((INodePart) part);
  }

  public IServiceProvider Services { get; set; }
}
