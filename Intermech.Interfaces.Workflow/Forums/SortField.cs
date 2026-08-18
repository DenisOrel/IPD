// Decompiled with JetBrains decompiler
// Type: Intermech.Forums.SortField
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

#nullable disable
namespace Intermech.Forums;

/// <summary>
/// по какому полю сортировать сообщения
/// в форуме?
/// </summary>
public enum SortField
{
  /// <summary>по дате</summary>
  Date,
  /// <summary>по заголовку сообщения</summary>
  Caption,
  /// <summary>по имени пользователя</summary>
  UserName,
}
