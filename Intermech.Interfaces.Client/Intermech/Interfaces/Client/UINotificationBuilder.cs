// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.UINotificationBuilder
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Collections;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

public sealed class UINotificationBuilder
{
  private DateTime dateTime;
  private string message;
  private string caption;
  private UINotificationIcon icon;
  private Exception error;
  private Action oldContentAction;
  private UINotificationActionBuilder contentActionBuilder;
  private List<UINotificationActionBuilder> actionBuilders;

  public UINotificationBuilder()
  {
    this.dateTime = DateTime.Now;
    this.message = string.Empty;
    this.caption = string.Empty;
    this.icon = UINotificationIcon.None;
    this.actionBuilders = new List<UINotificationActionBuilder>();
  }

  public DateTime DateTime
  {
    [DebuggerStepThrough] get => this.dateTime;
    [DebuggerStepThrough] set => this.dateTime = value;
  }

  public string Message
  {
    [DebuggerStepThrough] get => this.message;
    [DebuggerStepThrough] set
    {
      this.message = value ?? throw new ArgumentNullException(nameof (value));
    }
  }

  public string Caption
  {
    [DebuggerStepThrough] get => this.caption;
    [DebuggerStepThrough] set
    {
      this.caption = value ?? throw new ArgumentNullException(nameof (value));
    }
  }

  public UINotificationIcon Icon
  {
    [DebuggerStepThrough] get => this.icon;
    [DebuggerStepThrough] set => this.icon = value;
  }

  public Exception Error
  {
    [DebuggerStepThrough] get => this.error;
    [DebuggerStepThrough] set => this.error = value;
  }

  /// <summary>
  /// Старый обработчик нажатия на тело уведомления (в форме делегата).
  /// Значение свойства может быть не задано, для изменения следует использовать метод <see cref="M:Intermech.Interfaces.Client.UINotificationBuilder.SetContentAction(System.Action)" />.
  /// </summary>
  public Action OldContentAction
  {
    [DebuggerStepThrough] get => this.oldContentAction;
  }

  /// <summary>
  /// Новый обработчик нажатия на тело уведомления
  /// Значение свойства может быть не задано, для изменения следует использовать метод <see cref="M:Intermech.Interfaces.Client.UINotificationBuilder.SetContentAction(Intermech.Interfaces.Client.UINotificationActionBuilder)" />.
  /// </summary>
  public UINotificationActionBuilder ContentAction
  {
    [DebuggerStepThrough] get => this.contentActionBuilder;
  }

  public List<UINotificationActionBuilder> Actions
  {
    [DebuggerStepThrough] get => this.actionBuilders;
  }

  public void FillFromException(Exception exception)
  {
    this.error = exception != null ? exception : throw new ArgumentNullException(nameof (exception));
    this.message = exception.Message;
    this.caption = "Ошибка";
    this.icon = UINotificationIcon.Error;
    this.SetContentAction(new UINotificationActionBuilder("UI.Notifications.ShowError"));
    foreach (ErrorRecoveryAction enumerateRecoveryAction in exception.EnumerateRecoveryActions())
    {
      if (enumerateRecoveryAction != null)
      {
        if (!(enumerateRecoveryAction is OpenFileRecoveryAction fileRecoveryAction))
        {
          if (enumerateRecoveryAction is OpenIPSObjectRecoveryAction objectRecoveryAction)
            this.actionBuilders.Add(new UINotificationActionBuilder("UI.Notifications.RecoverError", new Uri($"ips://object/{objectRecoveryAction.ObjectId}"))
            {
              AnchorText = objectRecoveryAction.ObjectId.ToString()
            });
        }
        else
          this.actionBuilders.Add(new UINotificationActionBuilder("UI.Notifications.RecoverError", new Uri($"file:///{fileRecoveryAction.FilePath}"))
          {
            AnchorText = fileRecoveryAction.FilePath
          });
      }
    }
  }

  public void SetContentAction(Action action) => this.oldContentAction = action;

  public void SetContentAction(UINotificationActionBuilder action)
  {
    this.contentActionBuilder = action;
  }

  public UINotification Build()
  {
    return new UINotification(this.dateTime, this.message, this.caption, this.icon, this.error, this.oldContentAction, this.contentActionBuilder != null ? this.contentActionBuilder.Build() : (UINotificationAction) null, (ICollection<UINotificationAction>) new ReadOnlyCollectionWrapper<UINotificationAction>(this.actionBuilders.Count != 0 ? (ICollection<UINotificationAction>) this.actionBuilders.ConvertAll<UINotificationAction>((Converter<UINotificationActionBuilder, UINotificationAction>) (x => x.Build())).ToArray() : (ICollection<UINotificationAction>) new UINotificationAction[0]));
  }
}
