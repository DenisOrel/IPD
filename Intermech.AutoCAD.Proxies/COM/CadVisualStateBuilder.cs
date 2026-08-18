// Decompiled with JetBrains decompiler
// Type: Intermech.AutoCAD.Proxies.COM.CadVisualStateBuilder
// Assembly: Intermech.AutoCAD.Proxies, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 36C988AC-B8D8-43EC-AA22-521DF7E8A085
// Assembly location: D:\IPS\Client\Intermech.AutoCAD.Proxies.dll
// XML documentation location: D:\IPS\Client\Intermech.AutoCAD.Proxies.xml

using Intermech.Runtime.ComInterop.Proxies;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AutoCAD.Proxies.COM;

/// <summary>
/// Построитель для сохраненного состояния UI CAD-системы.
/// Реализация следует общему поведению AutoCAD, BricsCAD, nanoCAD.
/// </summary>
/// <remarks>Реализация не является thread safe.</remarks>
public class CadVisualStateBuilder : ApplicationVisualStateBuilder<ICadProxy, CadVisualStateFlags>
{
  /// <summary>Создает объект.</summary>
  public CadVisualStateBuilder()
    : base(CadVisualStateFlags.None)
  {
  }

  /// <summary>Сохраняет состояние UI приложения.</summary>
  /// <param name="cadSystem">Объект приложения</param>
  /// <param name="flags">Набор флагов, определяющих сохраняемые элементы UI</param>
  /// <param name="stateItems">Коллекция сохраненных элементов UI</param>
  protected override void DoSaveState(
    ICadProxy cadSystem,
    CadVisualStateFlags flags,
    List<ApplicationVisualStateItem<ICadProxy>> stateItems)
  {
    base.DoSaveState(cadSystem, flags, stateItems);
    if ((flags & CadVisualStateFlags.OpenDocuments) != CadVisualStateFlags.None)
    {
      OpenDocumentsVisualStateItem documentsVisualStateItem = new OpenDocumentsVisualStateItem();
      documentsVisualStateItem.SaveState(cadSystem);
      stateItems.Add((ApplicationVisualStateItem<ICadProxy>) documentsVisualStateItem);
    }
    if ((flags & CadVisualStateFlags.ActiveDocument) == CadVisualStateFlags.None)
      return;
    ActiveDocumentVisualStateItem documentVisualStateItem = new ActiveDocumentVisualStateItem();
    documentVisualStateItem.SaveState(cadSystem);
    stateItems.Add((ApplicationVisualStateItem<ICadProxy>) documentVisualStateItem);
  }
}
