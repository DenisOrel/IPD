// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.IConditionControllersService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Сервис зарегистрированных в системе контроллеров условий выборок
/// </summary>
public interface IConditionControllersService
{
  void RegisterController(IConditionController controller);

  IConditionController[] Controllers { get; }

  IConditionController[] GetConditionControllersForSelection(
    SelectionDataSource selectionDataSource,
    SelectionType selectionType,
    bool isInner);

  [Obsolete("Функция будет удалена в IPS 7.0")]
  IConditionController[] GetConditionControllersForSelection(
    SelectionDataSource selectionDataSource,
    SelectionType selectionType);
}
