
// Type: Intermech.Navigator.Snapshots.IDataTableSource
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Diagnostics;
using System.Data;


namespace Intermech.Navigator.Snapshots;

/// <summary>Интерфейс источника таблицы данных</summary>
public interface IDataTableSource
{
  /// <summary>Получить таблицу данных</summary>
  [NotNull]
  DataTable DataTable { get; }
}
