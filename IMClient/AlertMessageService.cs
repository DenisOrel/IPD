
// Type: IMClient.AlertMessageService




using Intermech.ApplicationModel;
using Intermech.Interfaces;
using System;
using System.Windows.Forms;


namespace IMClient
{
    internal sealed class AlertMessageService : AlertMessageServiceBase
    {
      private IApplicationEventLogService eventLogService;
      private IOptionalService<IOutputView> outputViewProvider;
      private IOptionalService<ISplashService> splashServiceFactory;

      public AlertMessageService(
        IApplicationEventLogService eventLogService,
        IOptionalService<IOutputView> outputViewProvider,
        IOptionalService<ISplashService> splashServiceProvider)
      {
        if (eventLogService == null)
          throw new ArgumentNullException(nameof (eventLogService));
        if (outputViewProvider == null)
          throw new ArgumentNullException(nameof (outputViewProvider));
        if (splashServiceProvider == null)
          throw new ArgumentNullException(nameof (splashServiceProvider));
        this.eventLogService = eventLogService;
        this.outputViewProvider = outputViewProvider;
        this.splashServiceFactory = splashServiceProvider;
      }

      protected override void DoShowMessage(
        string caption,
        string message,
        AlertMessageType messageType)
      {
        base.DoShowMessage(caption, message, messageType);
        this.ShowMessageDialog(caption, message, messageType);
        string str = this.CombineCaptionWithMessage(caption, message);
        this.eventLogService.DefaultLog.Write(str, this.MessageTypeToEventLogItemType(messageType));
        this.outputViewProvider.TryGet()?.WriteString("Особые события", str);
      }

      private void ShowMessageDialog(string caption, string message, AlertMessageType messageType)
      {
        ISplashService splashService = this.splashServiceFactory.TryGet();
        try
        {
          splashService?.HideSplash();
          int num = (int) MessageBox.Show(message, caption, MessageBoxButtons.OK, this.MessageTypeToIconType(messageType));
        }
        finally
        {
          splashService?.ShowSplash();
        }
      }

      private MessageBoxIcon MessageTypeToIconType(AlertMessageType messageType)
      {
        if (messageType == AlertMessageType.Warning)
          return MessageBoxIcon.Exclamation;
        return messageType == AlertMessageType.Error ? MessageBoxIcon.Hand : MessageBoxIcon.Asterisk;
      }
    }
}
