
// Type: Intermech.Search.Discussions.IDiscussionsRemoteFacade
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Search.Discussions
{
    public interface IDiscussionsRemoteFacade
    {
      MessageDto AddMessage(long objectVersionID, string caption, string text);

      MessageDto[] FindMessagesForAllObjectVersions(long objectVersionId);

      MessageDto[] FindMessagesForObject(long objectVersionId);

      MessageDto[] GetMessages(long discussionVersionId);

      MessageDto ReplaceMessage(MessageIdDto id, string caption, string text);

      void RemoveMessage(MessageIdDto id);

      bool CanDiscuss(long objectVersionId);

      MessageDto[] FindMessages(MessageIdDto[] ids);

      AddImageResultDto AddImage(AddImageParamsDto addImageParams);
    }
}
