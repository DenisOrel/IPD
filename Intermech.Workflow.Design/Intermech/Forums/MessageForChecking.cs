// Decompiled with JetBrains decompiler
// Type: Intermech.Forums.MessageForChecking
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

#nullable disable
namespace Intermech.Forums;

/// Класс для передачи ИД сообщения, которое надо выделить на форуме при загрузке.
///             Используется при переходе по ссылке из другого форума.
internal class MessageForChecking : IMessageForCheckingService
{
  private readonly string _messageId;

  public MessageForChecking(string messageId) => this._messageId = messageId;

  /// <summary>ИД сообщения</summary>
  /// <returns></returns>
  public string MessageId() => this._messageId;
}
