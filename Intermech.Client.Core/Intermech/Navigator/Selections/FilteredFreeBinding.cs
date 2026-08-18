
// Type: Intermech.Navigator.Selections.FilteredFreeBinding
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.DB;
using Intermech.Navigator.DBObjects;
using System;


namespace Intermech.Navigator.Selections;

/// <summary>
/// Для отображения неотфильтрованного списка объектов (Consts.NoFilterQuery), входящих в выборку
/// </summary>
internal sealed class FilteredFreeBinding : FreeBinding
{
  private static readonly IBinding _value = (IBinding) new FilteredFreeBinding();

  public new static IBinding Value => FilteredFreeBinding._value;

  protected override ObjectsPart GetObjectsPart(IConditionsProvider conditionsProvider)
  {
    return (ObjectsPart) new FilteredObjectsPart(conditionsProvider, (IServiceProvider) null);
  }
}
