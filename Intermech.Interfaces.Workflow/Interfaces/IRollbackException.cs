// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.IRollbackException
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

#nullable disable
namespace Intermech.Interfaces;

/// <summary>
/// Интерфейс, которым помечаются исключения, которые специально генерируются в серверных скриптах с целью возврата назад.
/// Для таких исключений не добавляются строки "Возвращено автоматически. При выполнении действия произошла ошибка", "Ошибка выполнения сценария",
/// т.е. текст сообщения скрипта будет полностью совпадать с текстом исключения.
/// </summary>
public interface IRollbackException
{
}
