
// Type: IMClient.Remoting.RemotingClientSponsorValidator




using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Net.Sockets;
using System.Text;


namespace IMClient.Remoting
{
    internal sealed class RemotingClientSponsorValidator
    {
      private IServerEventLogService serverEventLog;
      private IUINotificationService uiNotificationService;
      private const string logFileName = "remoting_client_connectivity.log";

      public RemotingClientSponsorValidator(
        IServerEventLogService serverEventLog,
        IUINotificationService uiNotificationService)
      {
        if (serverEventLog == null)
          throw new ArgumentNullException(nameof (serverEventLog));
        if (uiNotificationService == null)
          throw new ArgumentNullException(nameof (uiNotificationService));
        this.serverEventLog = serverEventLog;
        this.uiNotificationService = uiNotificationService;
      }

      public void CheckClientBackwardConnectivity()
      {
        Exception exception;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          try
          {
            sessionKeeper.Session.CheckClientBackwardConnectivity((IMClientLiveStatus) new RemotingClientSponsorValidator.TestObject());
            exception = (Exception) null;
          }
          catch (Exception ex)
          {
            exception = ex;
          }
        }
        if (exception == null)
          return;
        this.AddToServerLog(exception);
        this.ShowUINotification(exception);
      }

      private void AddToServerLog(Exception exception)
      {
        string text = exception.Message;
        Exception innerException = exception.InnerException;
        if (innerException != null)
          text = $"{text} {this.GetErrorDetails(innerException)}";
        this.serverEventLog.AddToTrace(text, "remoting_client_connectivity.log");
      }

      private object GetErrorDetails(Exception exception)
      {
        return exception is SocketException socketException ? (object) $"Socket error: {socketException.SocketErrorCode} ({socketException.ErrorCode}). {socketException.Message}" : (object) $"{exception.GetType().Name}: {exception.Message}";
      }

      private void ShowUINotification(Exception exception)
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(exception.Message);
        stringBuilder.AppendLine();
        stringBuilder.AppendLine();
        stringBuilder.AppendFormat("Для устранения проблемы требуется переконфигурировать клиентский канал Remoting в файле {0}.", (object) ProgramConsts.ApplicationConfigurationFileName);
        stringBuilder.Append(" ");
        stringBuilder.Append("Подробнее об этом можно узнать в приложении 3 руководства администратора IPS.");
        this.uiNotificationService.ShowNotification(new UINotificationBuilder()
        {
          Message = stringBuilder.ToString(),
          Icon = UINotificationIcon.Warning
        }.Build());
      }

      private sealed class TestObject : MarshalByRefObject, IMClientLiveStatus
      {
        public void KnockKnock()
        {
        }
      }
    }
}
