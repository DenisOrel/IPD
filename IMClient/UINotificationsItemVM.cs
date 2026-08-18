
// Type: IMClient.UINotificationsItemVM




using Intermech.Interfaces.Client;
using Intermech.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace IMClient
{
    internal sealed class UINotificationsItemVM : ViewModel
    {
      private readonly UINotification notification;
      private UINotificationsVM parent;

      public UINotificationsItemVM(UINotification notification) => this.notification = notification;

      public UINotificationsVM Parent
      {
        [DebuggerStepThrough] get => this.parent;
      }

      internal void SetParent(UINotificationsVM newParent) => this.parent = newParent;

      public UINotification Notification
      {
        [DebuggerStepThrough] get => this.notification;
      }

      public string Caption
      {
        [DebuggerStepThrough] get
        {
          return !string.IsNullOrEmpty(this.notification.Caption) ? this.notification.Caption : ProgramConsts.ApplicationTitle;
        }
      }

      public string Message
      {
        [DebuggerStepThrough] get => this.notification.Message;
      }

      public ICollection<UINotificationAction> Actions
      {
        [DebuggerStepThrough] get => this.notification.Actions;
      }

      public UINotificationIcon Icon
      {
        [DebuggerStepThrough] get => this.notification.Icon;
      }

      public DateTime DateTime
      {
        [DebuggerStepThrough] get => this.notification.DateTime;
      }
    }
}
