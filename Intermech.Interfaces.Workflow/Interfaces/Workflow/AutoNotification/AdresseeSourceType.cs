// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Workflow.AutoNotification.AdresseeSourceType
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

#nullable disable
namespace Intermech.Interfaces.Workflow.AutoNotification;

/// <summary>Тип источника адресата</summary>
public enum AdresseeSourceType
{
  None,
  /// <summary>Автор связи</summary>
  RelationAuthor,
  /// <summary>Автор объекта</summary>
  ObjectAuthor,
  /// <summary>Владелец объекта</summary>
  ObjectOwner,
  /// <summary>Менеджер проекта</summary>
  ProjectManager,
  /// <summary>Автор указан в атрибуте</summary>
  AuthorInAttribute,
  /// <summary>
  /// Руководитель подразделения, которому принадлежит автор объекта
  /// </summary>
  AuthorsDepartmentChief,
  /// <summary>
  /// Руководитель подразделения, которому принадлежит владелец объекта
  /// </summary>
  OwnersDepartmentChief,
  /// <summary>Автор вычисляется скриптом</summary>
  GetByScript,
}
