// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Workflow.IExecutedActivity
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

#nullable disable
namespace Intermech.Interfaces.Workflow;

/// <summary>
/// Интерфейс содержащий методы для запуска действий на выполнение. Интерфейс является служебным потому вынесен как отдельный.
/// </summary>
public interface IExecutedActivity : 
  IActivity,
  IMailObject,
  IDBObject,
  IDBAttributable,
  IDBSessionable,
  IPluginsData,
  IDBSecurityCollection,
  IDBSecurity
{
  /// <summary>Выполнение текущего шага</summary>
  /// <returns></returns>
  bool Execute(bool goNext);

  /// <summary>Прерывание выполнения</summary>
  bool Abort();

  /// <summary>Отправка дальше/возврат назад</summary>
  /// <param name="goNext"> true - идём дальше, false - идём назад</param>
  void NextStep(bool goNext);

  /// <summary>Сделать клона текущего действия</summary>
  /// <returns></returns>
  IActivity Clone();
}
