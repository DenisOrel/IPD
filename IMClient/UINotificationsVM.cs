
// Type: IMClient.UINotificationsVM




using Intermech;
using Intermech.Interfaces.Client;
using Intermech.UI;
using System;
using System.Diagnostics;
using System.Windows;


namespace IMClient
{
    internal sealed class UINotificationsVM : ViewModel
    {
      private double fontSize;
      private readonly UINotificationsItemsCollection items;
      private readonly PluggableCommand<UINotificationsItemVM> fireItemActionCommand;
      private readonly PluggableCommand<UINotificationsItemVM> closeItemCommand;
      private EventHandler<UINotificationActionEventArgs> handleNotificationAction;

      public UINotificationsVM()
      {
        this.fontSize = 11.0;
        this.items = new UINotificationsItemsCollection(this);
        this.fireItemActionCommand = new PluggableCommand<UINotificationsItemVM>(new Action<UINotificationsItemVM>(this.OnFireItemAction));
        this.closeItemCommand = new PluggableCommand<UINotificationsItemVM>(new Action<UINotificationsItemVM>(this.OnCloseItem));
      }

      public double FontSize
      {
        [DebuggerStepThrough] get => this.fontSize;
        set
        {
          if (value <= 0.0)
            throw new ArgumentOutOfRangeException(nameof (value));
          if (this.fontSize == value)
            return;
          this.fontSize = value;
          this.RaisePropertyChanged(nameof (FontSize));
        }
      }

      public UINotificationsItemsCollection Items
      {
        [DebuggerStepThrough] get => this.items;
      }

      public PluggableCommand<UINotificationsItemVM> FireItemActionCommand
      {
        [DebuggerStepThrough] get => this.fireItemActionCommand;
      }

      public PluggableCommand<UINotificationsItemVM> CloseItemCommand
      {
        [DebuggerStepThrough] get => this.closeItemCommand;
      }

      public EventHandler<UINotificationActionEventArgs> NotificationActionHandler
      {
        [DebuggerStepThrough] get => this.handleNotificationAction;
        [DebuggerStepThrough] set => this.handleNotificationAction = value;
      }

      private void OnFireItemAction(UINotificationsItemVM item)
      {
        UINotification notification = item != null ? item.Notification : throw new ArgumentNullException(nameof (item));
        if (notification.ContentAction != null)
        {
          EventHandler<UINotificationActionEventArgs> notificationAction = this.handleNotificationAction;
          if (notificationAction == null)
            return;
          notificationAction((object) null, new UINotificationActionEventArgs(notification, notification.ContentAction));
        }
        else
        {
          if (notification.OldContentAction == null)
            return;
          notification.OldContentAction();
        }
      }

      private void OnCloseItem(UINotificationsItemVM item)
      {
        if (item == null)
          throw new ArgumentNullException(nameof (item));
        this.Items.Remove(item);
      }

      private MessageBoxImage ToMessageBoxImage(UINotificationIcon itemIcon)
      {
        switch (itemIcon)
        {
          case UINotificationIcon.None:
            return MessageBoxImage.None;
          case UINotificationIcon.Info:
            return MessageBoxImage.Asterisk;
          case UINotificationIcon.Warning:
            return MessageBoxImage.Exclamation;
          case UINotificationIcon.Error:
            return MessageBoxImage.Hand;
          default:
            throw new NotSupportedEnumException((Enum) itemIcon);
        }
      }
    }
}
