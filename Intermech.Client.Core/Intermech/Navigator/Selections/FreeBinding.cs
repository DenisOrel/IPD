
// Type: Intermech.Navigator.Selections.FreeBinding
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.DB;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Parts;
using System;


namespace Intermech.Navigator.Selections;

/// <summary>
/// Обеспечивает функционирование свободного дерева выборок, т.е. просто
/// входящего в состав какого-либо объекта базы данных (например, рабочего
/// стола).
/// </summary>
internal class FreeBinding : IBinding
{
  private static readonly IBinding _value = (IBinding) new FreeBinding();

  public static IBinding Value => FreeBinding._value;

  public ConditionStructure[] GetConditions(long selObjectID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return ((ISelectionsService) ServicesManager.GetService(typeof (ISelectionsService))).GetConditionStructures((object) sessionKeeper.Session, selObjectID);
  }

  public INodePart GetPart(IConditionsProvider conditionsProvider)
  {
    ObjectsPart objectsPart = this.GetObjectsPart(conditionsProvider);
    objectsPart.AcceptManagedEvents = false;
    return (INodePart) objectsPart;
  }

  public string ViewCaption => LocalizationHolder.rm.GetString("Client.Core_277");

  protected virtual ObjectsPart GetObjectsPart(IConditionsProvider conditionsProvider)
  {
    return new ObjectsPart(conditionsProvider, (IServiceProvider) null);
  }
}
