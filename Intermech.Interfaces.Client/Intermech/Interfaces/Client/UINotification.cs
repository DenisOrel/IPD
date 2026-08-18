// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.UINotification
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Уведомление для отображения в интерфейсе пользователя.
/// Показывается в виде всплывающей подсказки и в виде элемента списка в окне "Уведомления".
/// Реализация является immutable.
/// </summary>
[DataContract(Namespace = "")]
[Serializable]
public class UINotification
{
  /// <summary>
  /// Создает объект.
  /// Вместо прямого вызова конструктора следует использовать класс <see cref="T:Intermech.Interfaces.Client.UINotificationBuilder" />
  /// </summary>
  /// <param name="dateTime"></param>
  /// <param name="message"></param>
  /// <param name="caption"></param>
  /// <param name="icon"></param>
  /// <param name="error"></param>
  /// <param name="oldContentAction"></param>
  /// <param name="contentAction"></param>
  /// <param name="actions"></param>
  internal UINotification(
    DateTime dateTime,
    string message,
    string caption,
    UINotificationIcon icon,
    Exception error,
    Action oldContentAction,
    UINotificationAction contentAction,
    ICollection<UINotificationAction> actions)
  {
    if (message == null)
      throw new ArgumentNullException(nameof (message));
    if (caption == null)
      throw new ArgumentNullException(nameof (caption));
    if (actions == null)
      throw new ArgumentNullException(nameof (actions));
    this.DateTime = dateTime;
    this.Message = message;
    this.Caption = caption;
    this.Icon = icon;
    this.Error = error;
    this.OldContentAction = oldContentAction;
    this.ContentAction = contentAction;
    this.Actions = actions;
  }

  [DataMember]
  public string Message { get; private set; }

  [DataMember]
  public string Caption { get; private set; }

  [DataMember]
  public DateTime DateTime { get; private set; }

  [DataMember]
  public UINotificationIcon Icon { get; private set; }

  [DataMember]
  public Exception Error { get; private set; }

  public Action OldContentAction { get; private set; }

  [DataMember(IsRequired = false)]
  public UINotificationAction ContentAction { get; private set; }

  [DataMember]
  public ICollection<UINotificationAction> Actions { get; private set; }

  [OnDeserialized]
  private void OnDeserializedMethod(StreamingContext context)
  {
    if (this.Actions != null)
      return;
    this.Actions = (ICollection<UINotificationAction>) new UINotificationAction[0];
  }
}
