
// Type: Intermech.Search.Discussions.DiscussionsRemoteFacadeProxy
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;


namespace Intermech.Search.Discussions
{
    public sealed class DiscussionsRemoteFacadeProxy : IDiscussionsRemoteFacade
    {
      public MessageDto AddMessage(long objectVersionId, string caption, string text)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          return ((IDiscussionsRemoteFacadeServerService) sessionKeeper.Session.GetCustomService(typeof (IDiscussionsRemoteFacadeServerService))).AddMessage(sessionKeeper.Session.SessionGUID, objectVersionId, caption, text);
      }

      public bool CanDiscuss(long objectVersionId)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          return ((IDiscussionsRemoteFacadeServerService) sessionKeeper.Session.GetCustomService(typeof (IDiscussionsRemoteFacadeServerService))).CanDiscuss(sessionKeeper.Session.SessionGUID, objectVersionId);
      }

      public MessageDto[] FindMessages(MessageIdDto[] ids)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          return ((IDiscussionsRemoteFacadeServerService) sessionKeeper.Session.GetCustomService(typeof (IDiscussionsRemoteFacadeServerService))).FindMessages(sessionKeeper.Session.SessionGUID, ids);
      }

      public MessageDto[] FindMessagesForAllObjectVersions(long objectVersionId)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          return ((IDiscussionsRemoteFacadeServerService) sessionKeeper.Session.GetCustomService(typeof (IDiscussionsRemoteFacadeServerService))).FindMessagesForAllObjectVersions(sessionKeeper.Session.SessionGUID, objectVersionId);
      }

      public MessageDto[] FindMessagesForObject(long objectVersionId)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          return ((IDiscussionsRemoteFacadeServerService) sessionKeeper.Session.GetCustomService(typeof (IDiscussionsRemoteFacadeServerService))).FindMessagesForObject(sessionKeeper.Session.SessionGUID, objectVersionId);
      }

      public MessageDto[] GetMessages(long discussionVersionId)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          return ((IDiscussionsRemoteFacadeServerService) sessionKeeper.Session.GetCustomService(typeof (IDiscussionsRemoteFacadeServerService))).GetMessages(sessionKeeper.Session.SessionGUID, discussionVersionId);
      }

      public void RemoveMessage(MessageIdDto id)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          ((IDiscussionsRemoteFacadeServerService) sessionKeeper.Session.GetCustomService(typeof (IDiscussionsRemoteFacadeServerService))).RemoveMessage(sessionKeeper.Session.SessionGUID, id);
      }

      public MessageDto ReplaceMessage(MessageIdDto id, string caption, string text)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          return ((IDiscussionsRemoteFacadeServerService) sessionKeeper.Session.GetCustomService(typeof (IDiscussionsRemoteFacadeServerService))).ReplaceMessage(sessionKeeper.Session.SessionGUID, id, caption, text);
      }

      public AddImageResultDto AddImage(AddImageParamsDto addImageParams)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          return ((IDiscussionsRemoteFacadeServerService) sessionKeeper.Session.GetCustomService(typeof (IDiscussionsRemoteFacadeServerService))).AddImage(sessionKeeper.Session.SessionGUID, addImageParams);
      }
    }
}
