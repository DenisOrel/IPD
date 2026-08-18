// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Workflow.ISubProcess
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

#nullable disable
namespace Intermech.Interfaces.Workflow;

public interface ISubProcess : 
  IExecutedActivity,
  IActivity,
  IMailObject,
  IDBObject,
  IDBAttributable,
  IDBSessionable,
  IPluginsData,
  IDBSecurityCollection,
  IDBSecurity
{
  /// <summary>
  /// Возвращает идентификатор шаблона который установлен для действия подпроцесса.
  /// В случае если установлена опция запуска только базовых версий возвращает её
  /// в остальных случаях возвращает версию выбранную разработчиком шаблона ранее.
  /// </summary>
  long SubSchemeID { get; }
}
