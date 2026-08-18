
// Type: Intermech.Navigator.Conditions.ConditionControllersService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;


namespace Intermech.Navigator.Conditions;

/// <summary>
/// Сервис зарегистрированных в системе контроллеров условий выборок
/// </summary>
internal sealed class ConditionControllersService : IConditionControllersService
{
  private readonly List<IConditionController> _controllers;

  public ConditionControllersService()
  {
    this._controllers = new List<IConditionController>();
    this.RegisterController((IConditionController) new AttributeConditionController());
    this.RegisterController((IConditionController) new InputOperatorConditionController());
    this.RegisterController((IConditionController) new InputObjectAttributeController());
    this.RegisterController((IConditionController) new FormulaConditionController());
    this.RegisterController((IConditionController) new InLCHistoryController());
    this.RegisterController((IConditionController) new InGlobalIndexController());
    this.RegisterController((IConditionController) new OwnerController());
  }

  public void RegisterController(IConditionController controller)
  {
    this._controllers.Add(controller);
  }

  public IConditionController[] GetConditionControllersForSelection(
    SelectionDataSource selectionDataSource,
    SelectionType selectionType)
  {
    return this.GetConditionControllersForSelection(selectionDataSource, selectionType, false);
  }

  public IConditionController[] GetConditionControllersForSelection(
    SelectionDataSource selectionDataSource,
    SelectionType selectionType,
    bool isInner)
  {
    List<IConditionController> conditionControllerList = new List<IConditionController>();
    foreach (IConditionController controller in this._controllers)
    {
      if ((controller.SupportedDataSource & selectionDataSource) > SelectionDataSource.Unknown && Array.Exists<SelectionType>(controller.SupportedTypes, (Predicate<SelectionType>) (x => x == selectionType)) && (!isInner || isInner && controller.IsInnerSupported))
        conditionControllerList.Add(controller);
    }
    return conditionControllerList.Count <= 0 ? (IConditionController[]) null : conditionControllerList.ToArray();
  }

  public IConditionController[] Controllers
  {
    get
    {
      return this._controllers.Count <= 0 ? (IConditionController[]) null : this._controllers.ToArray();
    }
  }
}
