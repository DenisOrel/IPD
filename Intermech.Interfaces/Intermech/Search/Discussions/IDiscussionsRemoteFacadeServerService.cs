
// Type: Intermech.Search.Discussions.IDiscussionsRemoteFacadeServerService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Search.Discussions
{
    public interface IDiscussionsRemoteFacadeServerService
    {
      MessageDto AddMessage(Guid userSessionGuid, long objectVersionId, string caption, string text);

      MessageDto[] FindMessagesForAllObjectVersions(Guid userSessionGuid, long objectId);

      MessageDto[] FindMessagesForObject(Guid userSessionGuid, long objectVersionId);

      MessageDto[] GetMessages(Guid userSessionGuid, long discussionVersionId);

      MessageDto ReplaceMessage(Guid userSessionGuid, MessageIdDto id, string caption, string text);

      void RemoveMessage(Guid userSessionGuid, MessageIdDto id);

      bool CanDiscuss(Guid userSessionGuid, long objectVersionId);

      MessageDto[] FindMessages(Guid userSessionGuid, MessageIdDto[] ids);

      AddImageResultDto AddImage(Guid userSessionGuid, AddImageParamsDto addImageParams);
    }
}
