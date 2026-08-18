// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Workflow.ISchemeCheckOut
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

#nullable disable
namespace Intermech.Interfaces.Workflow;

public interface ISchemeCheckOut : 
  IScheme,
  IMailObject,
  IDBObject,
  IDBAttributable,
  IDBSessionable,
  IPluginsData,
  IDBSecurityCollection,
  IDBSecurity,
  ISchemeActivityCreator
{
  /// <summary>
  /// Поле для взятия схемы на редактирование без проверки есть или нет запущенные процессы. Используется в системных целях. Использование просто так запрещено!!!
  /// </summary>
  /// <returns></returns>
  int CheckOutSchemeWithoutEditable { get; set; }
}
