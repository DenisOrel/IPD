
// Type: IMClient.UINotificationMessageAndActions




using Intermech.Interfaces.Client;
using System.Collections.Generic;


namespace IMClient
{
    internal sealed class UINotificationMessageAndActions
    {
      public UINotificationMessageAndActions(string message, IEnumerable<UINotificationAction> actions)
      {
        this.Message = message;
        this.Actions = actions;
      }

      public string Message { get; }

      public IEnumerable<UINotificationAction> Actions { get; }
    }
}
