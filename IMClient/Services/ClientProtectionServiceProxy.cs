
// Type: IMClient.Services.ClientProtectionServiceProxy




using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Protection;


namespace IMClient.Services
{
    internal class ClientProtectionServiceProxy : IProtectionMessageService
    {
      public void SendMessage(string subject, string text)
      {
        ((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IProtectionMessageService)) as IProtectionMessageService).SendMessage(subject, text);
      }
    }
}
