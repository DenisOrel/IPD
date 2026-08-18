
// Type: Intermech.Navigator.Notifications.UpdateManager
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Navigator.Notifications;

/// <summary>
/// Применяет операции, сгенерированные элементом навигации в результате
/// обработки события обновления, к элементу управления пользовательского
/// интерфейса.
/// </summary>
public class UpdateManager
{
  public static void UpdateView(INodeView nodeView, IUpdateAnalyser analyser)
  {
    if (nodeView == null)
      throw new ArgumentException("nodeView cannot be null!", nameof (nodeView));
    if (analyser == null)
      throw new ArgumentException("analyser cannot be null!", nameof (analyser));
    UpdatePlan plan = new UpdatePlan();
    analyser.Preprocess((IUpdatePlan) plan);
    for (int index = 0; index < nodeView.Count; ++index)
    {
      plan.CurrentIndex = index;
      analyser.Process(nodeView[index], (IUpdatePlan) plan);
    }
    analyser.Postprocess((IUpdatePlan) plan);
    plan.Execute(nodeView);
  }
}
