// Decompiled with JetBrains decompiler
// Type: Intermech.Forums.IUserMessageSelector
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

#nullable disable
namespace Intermech.Forums;

/// <summary>интерфейс для выбора сообщений из обсуждения</summary>
public interface IUserMessageSelector
{
  /// <summary>
  /// Выбирает сообщения из обуждения.
  /// Возвращает строки в формате
  /// guid_обсуждения;id_сообщения
  /// </summary>
  /// <returns></returns>
  object[] SelectMessages();
}
