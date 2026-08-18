
// Type: IMClient.UINotificationsItemToInlinesConverter




using Intermech;
using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Navigation;


namespace IMClient
{
    [ValueConversion(typeof (UINotificationsItemVM), typeof (IEnumerable<Inline>))]
    internal sealed class UINotificationsItemToInlinesConverter : IValueConverter
    {
      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
        return value is UINotificationsItemVM itemVM ? (object) this.ConvertInternal(itemVM) : DependencyProperty.UnsetValue;
      }

      private List<Inline> ConvertInternal(UINotificationsItemVM itemVM)
      {
        string message = itemVM.Message;
        ICollection<UINotificationAction> actions = itemVM.Actions;
        List<InTextActionPlacementRecord> actionPlacementRecordList = this.PlaceUIActions(message, (IEnumerable<UINotificationAction>) actions);
        if (actionPlacementRecordList.Count != 0)
        {
          List<Inline> inlineList = new List<Inline>(actionPlacementRecordList.Count * 2 + 1);
          int startIndex = 0;
          foreach (InTextActionPlacementRecord actionPlacementRecord in actionPlacementRecordList)
          {
            if (startIndex < actionPlacementRecord.Index)
              inlineList.Add((Inline) new Run(message.Substring(startIndex, actionPlacementRecord.Index - startIndex)));
            Hyperlink hyperlink = new Hyperlink((Inline) new Run(actionPlacementRecord.AnchorText));
            hyperlink.NavigateUri = actionPlacementRecord.ActionUri;
            hyperlink.Tag = (object) Tuple.Create<UINotificationsItemVM, UINotificationAction>(itemVM, (UINotificationAction) actionPlacementRecord.Tag);
            hyperlink.RequestNavigate += new RequestNavigateEventHandler(this.OnNavigateInternal);
            inlineList.Add((Inline) hyperlink);
            startIndex = actionPlacementRecord.Index + actionPlacementRecord.AnchorText.Length;
          }
          if (startIndex < message.Length)
            inlineList.Add((Inline) new Run(message.Substring(startIndex, message.Length - startIndex)));
          return inlineList;
        }
        return new List<Inline>(1) { (Inline) new Run(message) };
      }

      private List<InTextActionPlacementRecord> PlaceUIActions(
        string message,
        IEnumerable<UINotificationAction> uiActions)
      {
        List<InTextActionPlacementRecord> collection = new List<InTextActionPlacementRecord>();
        foreach (UINotificationAction uiAction in uiActions)
        {
          if (uiAction.AnchorText != string.Empty && (this.IsOpenAction(uiAction) || this.IsRecoverErrorAction(uiAction)))
          {
            string anchorText = uiAction.AnchorText;
            for (int index = message.IndexOf(anchorText); index >= 0; index = message.IndexOf(anchorText, index + anchorText.Length))
              new InTextActionPlacementRecord(index, anchorText, uiAction.Data, (object) uiAction).TryPutIntoCollectionIfNotOverlapped((ICollection<InTextActionPlacementRecord>) collection);
          }
        }
        return collection;
      }

      private bool IsOpenAction(UINotificationAction action)
      {
        return action.Name == "UI.Notifications.Open" && action.Data != (Uri) null;
      }

      private bool IsRecoverErrorAction(UINotificationAction action)
      {
        return action.Name == "UI.Notifications.RecoverError" && action.Data != (Uri) null;
      }

      private void OnNavigateInternal(object sender, RequestNavigateEventArgs e)
      {
        if (!(((FrameworkContentElement) e.Source).Tag is Tuple<UINotificationsItemVM, UINotificationAction> tag))
          return;
        UINotificationsItemVM notificationsItemVm1;
        UINotificationAction notificationAction;
        tag.Deconstruct<UINotificationsItemVM, UINotificationAction>(out notificationsItemVm1, out notificationAction);
        UINotificationsItemVM notificationsItemVm2 = notificationsItemVm1;
        UINotificationAction action = notificationAction;
        if (notificationsItemVm2 == null || notificationsItemVm2.Parent == null)
          return;
        EventHandler<UINotificationActionEventArgs> notificationActionHandler = notificationsItemVm2.Parent.NotificationActionHandler;
        if (notificationActionHandler == null)
          return;
        notificationActionHandler((object) null, new UINotificationActionEventArgs(notificationsItemVm2.Notification, action));
      }

      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      {
        throw new NotSupportedException();
      }
    }
}
