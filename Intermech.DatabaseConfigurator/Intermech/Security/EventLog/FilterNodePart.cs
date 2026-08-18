// Decompiled with JetBrains decompiler
// Type: Intermech.Security.EventLog.FilterNodePart
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.DB;
using Intermech.Navigator.EventLog;
using System;
using System.Collections.Specialized;

#nullable disable
namespace Intermech.Security.EventLog;

internal class FilterNodePart : EventsNodePartBase
{
  private IConditionsProvider condProvider;

  public FilterNodePart(IConditionsProvider condProvider)
  {
    this.condProvider = condProvider != null ? condProvider : throw new ArgumentNullException(nameof (condProvider), LocalizationHolder.rm.GetString("DatabaseConfigurator_105"));
  }

  protected override ConditionStructure[] Conditions => this.condProvider.GetConditions();

  protected override HybridDictionary ConditionTags => (HybridDictionary) null;
}
