// Decompiled with JetBrains decompiler
// Type: Intermech.Project.IDBProjectTaskCollection
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;

#nullable disable
namespace Intermech.Project;

/// <summary>Интерфейс фабрики задач IMProject</summary>
[DBObjectTypeHandler("cad00e92-306c-11d8-b4e9-00304f19f545", true)]
public interface IDBProjectTaskCollection : 
  IDBObjectCollection,
  IDBRecords,
  IDBSessionable,
  IDBAttributableCollection
{
  /// <summary>Создает заготовку новой задачи IMProject</summary>
  [NotNull]
  IDBProjectTask Create();

  /// <summary>Создает заготовку новой задачи IMProject на основе прототипа prototype</summary>
  [NotNull]
  IDBProjectTask Create([NotNull] IDBProjectTask prototype);

  /// <summary>Создает заготовку новой задачи IMProject на основе прототипа prototype</summary>
  [NotNull]
  IDBProjectTask Create(long prototypeID);
}
