
// Type: Intermech.Client.Core.Navigator.Classes.Providers.INavigatorVirtualColumnProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System.Data;


namespace Intermech.Client.Core.Navigator.Classes.Providers;

/// <summary>Провайдер для виртуальных полей навигатора</summary>
public interface INavigatorVirtualColumnProvider : ISpecialFieldsSupported
{
  /// <summary>
  /// Возвращает идентификатор поля источника данных для указанной
  /// виртуальной колонки. Если данная колонка не поддерживается, то
  /// метод возвращает null.
  /// </summary>
  /// <param name="column">Виртуальная колонка навигатора</param>
  /// <returns></returns>
  object MapColumnToField(INodeItems nodeItems, NodeColumn column);

  /// <summary>Добавление данных по виртуальным полям в таблицу</summary>
  /// <param name="sourceTable">Исходная таблица</param>
  /// <returns></returns>
  DataTable GetDataTable(INodeQuery nodeQuery, NavigatorVirtualColumnProviderArgs args);
}
