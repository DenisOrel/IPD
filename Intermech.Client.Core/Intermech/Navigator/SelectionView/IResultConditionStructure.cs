
// Type: Intermech.Navigator.SelectionView.IResultConditionStructure
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Kernel.Search;


namespace Intermech.Navigator.SelectionView;

/// <summary>
/// Интерфейс который должны поддерживать ОБА редактора условий выборки
/// (когда перейдем на один - можно удалить)
/// </summary>
internal interface IResultConditionStructure
{
  ConditionStructure ResultConditionStructure { get; }
}
