
// Type: Intermech.Navigator.SelectionView.SelectionFormToolBarService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using System.Collections.Generic;


namespace Intermech.Navigator.SelectionView;

public class SelectionFormToolBarService : ISelectionFormCustomCommandsService
{
  public void Register(ISelectionFormCustomCommandsSubscriber subscriber)
  {
    this.Subscribers.Add(subscriber);
  }

  public void UnRegister(ISelectionFormCustomCommandsSubscriber subscriber)
  {
    this.Subscribers.Remove(subscriber);
  }

  public List<ISelectionFormCustomCommandsSubscriber> Subscribers { get; } = new List<ISelectionFormCustomCommandsSubscriber>();

  public bool EnableButton(
    ConditionStructure[] allConditions,
    ConditionStructure current,
    string name)
  {
    foreach (ISelectionFormCustomCommandsSubscriber subscriber in this.Subscribers)
    {
      bool flag1 = false;
      ConditionStructure[] allConditions1 = allConditions;
      ConditionStructure current1 = current;
      string name1 = name;
      ref bool local = ref flag1;
      bool flag2 = subscriber.EnableButton(allConditions1, current1, name1, ref local);
      if (flag1)
        return flag2;
    }
    return false;
  }
}
