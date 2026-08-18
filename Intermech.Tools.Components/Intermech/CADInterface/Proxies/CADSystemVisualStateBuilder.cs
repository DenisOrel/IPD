// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.CADSystemVisualStateBuilder
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Runtime.ComInterop.Proxies;
using System.Collections.Generic;

#nullable disable
namespace Intermech.CADInterface.Proxies;

/// <summary>
/// Построитель для сохраненного состояния UI CAD-системы.
/// </summary>
/// <remarks>Реализация не является thread safe.</remarks>
public class CADSystemVisualStateBuilder : 
  ApplicationVisualStateBuilder<CADSystemProxy, CADSystemVisualStateFlags>
{
  /// <summary>Создает объект.</summary>
  public CADSystemVisualStateBuilder()
    : base(CADSystemVisualStateFlags.None)
  {
  }

  /// <summary>Сохраняет состояние UI приложения.</summary>
  /// <param name="application">Объект приложения</param>
  /// <param name="flags">Набор флагов, определяющих сохраняемые элементы UI</param>
  /// <param name="stateItems">Коллекция сохраненных элементов UI</param>
  protected override void DoSaveState(
    CADSystemProxy cadSystem,
    CADSystemVisualStateFlags flags,
    List<ApplicationVisualStateItem<CADSystemProxy>> stateItems)
  {
    base.DoSaveState(cadSystem, flags, stateItems);
    if ((flags & CADSystemVisualStateFlags.OpenDocuments) != CADSystemVisualStateFlags.None)
    {
      OpenDocumentsVisualStateItem documentsVisualStateItem = new OpenDocumentsVisualStateItem();
      documentsVisualStateItem.SaveState(cadSystem);
      stateItems.Add((ApplicationVisualStateItem<CADSystemProxy>) documentsVisualStateItem);
    }
    if ((flags & CADSystemVisualStateFlags.ActiveDocument) == CADSystemVisualStateFlags.None)
      return;
    ActiveDocumentVisualStateItem documentVisualStateItem = new ActiveDocumentVisualStateItem();
    documentVisualStateItem.SaveState(cadSystem);
    stateItems.Add((ApplicationVisualStateItem<CADSystemProxy>) documentVisualStateItem);
  }
}
