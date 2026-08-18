
// Type: Intermech.Search.Discussions.StandardDiscussionsRemoteFacade
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace Intermech.Search.Discussions
{
    public sealed class StandardDiscussionsRemoteFacade : IDiscussionsRemoteFacade
    {
      private static readonly Regex CitWithAuthorRegex = new Regex("[cit=\"(.+)\"]");

      public MessageDto AddMessage(long objectVersionId, string caption, string text)
      {
        if (ObjectHelper.IsUnknownObjectVersionID(objectVersionId))
          throw new ArgumentException();
        if (string.IsNullOrEmpty(text))
          throw new ArgumentException();
        long[] discussionsForObject = this.FindDiscussionsForObject(objectVersionId);
        long discussionVersionId = discussionsForObject.Length == 0 ? this.CreateNewDiscussionForObject(objectVersionId) : ((IEnumerable<long>) discussionsForObject).FirstOrDefault<long>();
        MessageDto newMessage = this.CreateNewMessage(discussionVersionId, caption, text);
        List<MessageDto> list = ((IEnumerable<MessageDto>) this.GetMessages(discussionVersionId)).ToList<MessageDto>();
        list.Add(newMessage);
        this.SaveMessages(discussionVersionId, list.ToArray());
        this.FillContext(newMessage);
        return newMessage;
      }

      public MessageDto[] FindMessagesForAllObjectVersions(long objectVersionId)
      {
        if (ObjectHelper.IsUnknownObjectVersionID(objectVersionId))
          throw new ArgumentException();
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(objectVersionId);
          return this.FindMessages(new List<ConditionStructure>()
          {
            new ConditionStructure()
            {
              Attribute = (object) DiscussionsConstants.DiscussedObjectGuidAttributeTypeId,
              RelationalOperator = RelationalOperators.Equal,
              Value = (object) dbObject.GUID,
              SQL = string.Empty
            }
          }.ToArray());
        }
      }

      public MessageDto[] FindMessagesForObject(long objectVersionId)
      {
        if (ObjectHelper.IsUnknownObjectVersionID(objectVersionId))
          throw new ArgumentException();
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(objectVersionId);
          return this.FindMessages(new ConditionStructure[1]
          {
            new ConditionStructure()
            {
              Attribute = (object) DiscussionsConstants.DiscussedObjectVersionGuidAttributeTypeId,
              RelationalOperator = RelationalOperators.Equal,
              Value = (object) dbObject.ObjectGUID,
              SQL = string.Empty
            }
          });
        }
      }

      public MessageDto[] GetMessages(long discussionVersionId)
      {
        if (ObjectHelper.IsUnknownObjectVersionID(discussionVersionId))
          throw new ArgumentException();
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(discussionVersionId);
          IDBAttribute attributeById = dbObject.GetAttributeByID(DiscussionsConstants.DiscussionAttributeTypeId);
          StringBuilder stringBuilder = new StringBuilder();
          for (int index = 0; index < attributeById.ValuesCount; ++index)
          {
            attributeById.Index = index;
            stringBuilder.Append(attributeById.AsString ?? string.Empty);
          }
          string text = stringBuilder.ToString();
          if (string.IsNullOrEmpty(text))
            return new MessageDto[0];
          MessageDto[] messages = new DiscussionParser().Parse(dbObject.ObjectID, text);
          this.FillContext((IEnumerable<MessageDto>) messages);
          return messages;
        }
      }

      public void RemoveMessage(MessageIdDto id)
      {
        if (id == null)
          throw new ArgumentNullException(nameof (id));
        MessageDto[] array = ((IEnumerable<MessageDto>) this.GetMessages(id.DiscussionVersionId)).Where<MessageDto>((System.Func<MessageDto, bool>) (o => !o.Id.Equals((object) id))).ToArray<MessageDto>();
        this.SaveMessages(id.DiscussionVersionId, array);
      }

      public MessageDto ReplaceMessage(MessageIdDto id, string caption, string text)
      {
        if (id == null)
          throw new ArgumentNullException(nameof (id));
        if (string.IsNullOrEmpty(text))
          throw new ArgumentException();
        MessageDto[] messages = this.GetMessages(id.DiscussionVersionId);
        MessageDto message = ((IEnumerable<MessageDto>) messages).FirstOrDefault<MessageDto>((System.Func<MessageDto, bool>) (o => o.Id.Equals((object) id)));
        if (message == null)
          throw new Exception();
        message.Caption = caption;
        message.LastModificationTimestamp = new DateTime?(DateTime.UtcNow);
        message.Text = text;
        this.SaveMessages(id.DiscussionVersionId, messages);
        this.FillContext(message);
        return message;
      }

      public bool CanDiscuss(long objectVersionId)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(objectVersionId, false);
          if (dbObject != null)
            return MetaDataHelper.GetObjectType(dbObject.ObjectType).Options.HasFlag((Enum) ObjectTypeOptions.ForumEnabled);
        }
        return false;
      }

      public MessageDto[] FindMessages(MessageIdDto[] ids)
      {
        using (new SessionKeeper())
        {
          long[] array = ((IEnumerable<MessageIdDto>) ids).Select<MessageIdDto, long>((System.Func<MessageIdDto, long>) (messageId => messageId.DiscussionVersionId)).Distinct<long>().ToArray<long>();
          List<MessageDto> source = new List<MessageDto>();
          foreach (long discussionVersionId in array)
            source.AddRange((IEnumerable<MessageDto>) this.GetMessages(discussionVersionId));
          return source.Where<MessageDto>((System.Func<MessageDto, bool>) (message => ((IEnumerable<MessageIdDto>) ids).Contains<MessageIdDto>(message.Id))).ToArray<MessageDto>();
        }
      }

      public AddImageResultDto AddImage(AddImageParamsDto addImageParams)
      {
        if (addImageParams == null)
          throw new ArgumentNullException(nameof (addImageParams));
        if (!AddImageParamsDto.Check(addImageParams))
          throw new ArgumentException();
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject dbObject1;
          if (ObjectHelper.IsUnknownObjectVersionID(addImageParams.DiscussionVersionId))
          {
            IDBObject dbObject2 = sessionKeeper.Session.GetObject(addImageParams.ObjectVersionId);
            long[] discussions = this.FindDiscussions(new ConditionStructure[1]
            {
              new ConditionStructure()
              {
                Attribute = (object) DiscussionsConstants.DiscussedObjectVersionGuidAttributeTypeId,
                RelationalOperator = RelationalOperators.Equal,
                Value = (object) dbObject2.ObjectGUID,
                SQL = string.Empty
              }
            });
            if (discussions.Length != 0)
            {
              dbObject1 = sessionKeeper.Session.GetObject(discussions[0]);
            }
            else
            {
              long discussionForObject = this.CreateNewDiscussionForObject(addImageParams.ObjectVersionId);
              dbObject1 = sessionKeeper.Session.GetObject(discussionForObject);
            }
          }
          else
            dbObject1 = sessionKeeper.Session.GetObject(addImageParams.DiscussionVersionId);
          if (!(dbObject1.GetAttributeByID(Constants.FileAttributeTypeID) is IDBFileAttribute aIDBAttribute))
            aIDBAttribute = dbObject1.Attributes.AddAttribute(Constants.FileAttributeTypeID, false) as IDBFileAttribute;
          aIDBAttribute.Index = aIDBAttribute.AddValue((object) null);
          BlobInformation aBlobInformation = new BlobInformation()
          {
            ArcMethod = ArcMethods.ZLibPacked,
            FileName = Guid.NewGuid().ToString("D") + Path.GetExtension(addImageParams.FileName),
            RealFileSize = (long) addImageParams.Blob.Length,
            Author = sessionKeeper.Session.UserID,
            ModifyDate = DateTime.UtcNow
          };
          using (MemoryStream aSourceStream = new MemoryStream(addImageParams.Blob))
            new BlobProcWriter((IDBAttribute) aIDBAttribute, 0, aBlobInformation, (Stream) aSourceStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).WriteData();
          return new AddImageResultDto()
          {
            ObjectVersionGuid = dbObject1.ObjectGUID,
            FileName = aBlobInformation.FileName
          };
        }
      }

      private long[] FindDiscussionsForObject(long objectVersionId)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(objectVersionId);
          return this.FindDiscussions(new ConditionStructure[1]
          {
            new ConditionStructure()
            {
              Attribute = (object) DiscussionsConstants.DiscussedObjectVersionGuidAttributeTypeId,
              RelationalOperator = RelationalOperators.Equal,
              Value = (object) dbObject.ObjectGUID,
              SQL = string.Empty
            }
          });
        }
      }

      private long[] FindDiscussions(ConditionStructure[] conditions)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          DataTable dataTable = sessionKeeper.Session.GetObjectCollection(DiscussionsConstants.DiscussionObjectTypeId).Select(new DBRecordSetParams()
          {
            Columns = new object[1]
            {
              (object) ObligatoryObjectAttributes.F_OBJECT_ID
            },
            Conditions = conditions,
            RecordCount = -1
          });
          List<long> longList = new List<long>();
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
            longList.Add(DataSetProcessor.GetInt64Value(row, 0, 0L));
          return longList.ToArray();
        }
      }

      private long CreateNewDiscussionForObject(long objectVersionId)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject dbObject1 = sessionKeeper.Session.GetObject(objectVersionId);
          IDBObject dbObject2 = sessionKeeper.Session.GetObjectCollection(DiscussionsConstants.DiscussionObjectTypeId).Create();
          dbObject2.SetAttributesValues(new AttributeValues[2]
          {
            new AttributeValues(DiscussionsConstants.DiscussedObjectGuidAttributeTypeId, (object) dbObject1.GUID),
            new AttributeValues(DiscussionsConstants.DiscussedObjectVersionGuidAttributeTypeId, (object) dbObject1.ObjectGUID)
          });
          dbObject2.CommitCreation(true);
          return dbObject2.ObjectID;
        }
      }

      private MessageDto CreateNewMessage(long discussionVersionId, string caption, string text)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(sessionKeeper.Session.UserID);
          DateTime utcNow = DateTime.UtcNow;
          DateTime dateTime = new DateTime(utcNow.Year, utcNow.Month, utcNow.Day, utcNow.Hour, utcNow.Minute, utcNow.Second, utcNow.Kind);
          return new MessageDto()
          {
            Id = new MessageIdDto()
            {
              AuthorVersionGuid = dbObject.ObjectGUID,
              CreationTimestamp = dateTime,
              DiscussionVersionId = discussionVersionId
            },
            AuthorName = dbObject.Caption,
            Caption = caption,
            Text = text
          };
        }
      }

      private MessageDto[] FindMessages(ConditionStructure[] conditions)
      {
        List<MessageDto> messages = new List<MessageDto>();
        foreach (long discussion in this.FindDiscussions(conditions))
          messages.AddRange((IEnumerable<MessageDto>) this.GetMessages(discussion));
        this.FillContext((IEnumerable<MessageDto>) messages);
        return messages.ToArray();
      }

      private void SaveMessages(long discussionVersionId, MessageDto[] messages)
      {
        string str = new DiscussionSerializer().Serialize(messages);
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(discussionVersionId);
          IDBAttribute attributeById = dbObject.GetAttributeByID(DiscussionsConstants.DiscussionAttributeTypeId);
          attributeById.ClearValues();
          IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(DiscussionsConstants.DiscussionAttributeTypeId);
          long num = (long) str.Length / attributeType.SizeType + ((long) str.Length % attributeType.SizeType > 0L ? 1L : 0L);
          List<object> objectList = new List<object>();
          for (int index = 0; (long) index < num; ++index)
          {
            int startIndex = (int) ((long) index * attributeType.SizeType);
            objectList.Add((object) str.Substring(startIndex, Math.Min(str.Length - startIndex, (int) attributeType.SizeType)));
          }
          if (objectList.Count > 0)
            attributeById.Values = objectList.ToArray();
          else
            dbObject.Delete((long) Consts.PurgeMode);
        }
      }

      private void FillContext(IEnumerable<MessageDto> messages)
      {
        foreach (MessageDto message in messages)
          this.FillContext(message);
      }

      private void FillContext(MessageDto message)
      {
        MessageContextDto messageContextDto = new MessageContextDto();
        if (!string.IsNullOrEmpty(message.Text))
        {
          List<Guid> guidList = new List<Guid>();
          foreach (Match match in StandardDiscussionsRemoteFacade.CitWithAuthorRegex.Matches(message.Text))
          {
            if (match.Groups != null && match.Groups.Count > 0)
            {
              Guid result = Guid.Empty;
              if (Guid.TryParse(match.Groups[0].Value, out result))
                guidList.Add(result);
            }
          }
          if (guidList.Count > 0)
          {
            using (SessionKeeper sessionKeeper = new SessionKeeper())
            {
              IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(Constants.UserObjectTypeID);
              DBRecordSetParams dbRecordSetParams = new DBRecordSetParams();
              dbRecordSetParams.Columns = new object[2]
              {
                (object) ObligatoryObjectAttributes.F_GUID,
                (object) ObligatoryObjectAttributes.CAPTION
              };
              // ISSUE: explicit reference operation
              (^ref dbRecordSetParams).Conditions = new ConditionStructure[1]
              {
                new ConditionStructure()
                {
                  Attribute = (object) ObligatoryObjectAttributes.F_GUID,
                  RelationalOperator = RelationalOperators.In,
                  Value = (object) guidList.ToArray(),
                  SQL = string.Empty
                }
              };
              dbRecordSetParams.RecordCount = -1;
              DBRecordSetParams paramSet = dbRecordSetParams;
              foreach (DataRow row in (InternalDataCollectionBase) objectCollection.Select(paramSet).Rows)
              {
                Guid guidValue = DataSetProcessor.GetGuidValue(row, 0, Guid.Empty);
                string stringValue = DataSetProcessor.GetStringValue(row, 0, (string) null);
                messageContextDto.ObjectVersionGuidToCaptionMap[guidValue] = stringValue;
              }
            }
          }
        }
        message.Context = messageContextDto;
      }
    }
}
