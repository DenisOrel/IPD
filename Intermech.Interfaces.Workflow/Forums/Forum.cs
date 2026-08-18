// Decompiled with JetBrains decompiler
// Type: Intermech.Forums.Forum
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace Intermech.Forums;

/// <summary>
/// форум. состоит из обсуждений,
/// подобранных в соответствии со способом, выбранным пользователем.
/// каждое обсуждение состоит из пользовательских сообщений.
/// </summary>
[Serializable]
public class Forum : List<UserMessage>
{
  /// <summary>Загрузить сообщения из объекта обсуждение в форум</summary>
  /// <param name="discGuid">guid версии обсуждения</param>
  /// <param name="session">сессия </param>
  public void LoadDiscussion(Guid discGuid, IUserSession session)
  {
    IDBObject dbObject = session.GetObject(discGuid, false);
    if (dbObject == null)
      return;
    IDBAttribute dbAttribute = dbObject.Attributes.AddAttribute(ForumsConsts.forumAttributeID, false);
    IDBAttribute attributeById = dbObject.GetAttributeByID(ForumsConsts.discussedObjectGuidAttributeID);
    StringBuilder stringBuilder = new StringBuilder();
    if (dbAttribute == null)
      return;
    if (dbAttribute.ValuesCount == 1)
    {
      stringBuilder.Append(DataSetProcessor.GetStringValue(dbAttribute.Value, string.Empty));
    }
    else
    {
      object[] values = dbAttribute.Values;
      if (values != null)
      {
        for (int index = 0; index < values.Length; ++index)
          stringBuilder.Append(DataSetProcessor.GetStringValue(values[index], string.Empty));
      }
    }
    string str = stringBuilder.ToString();
    if (string.IsNullOrEmpty(str))
      return;
    string[] strArray = str.Split(ForumsConsts.Splitter, StringSplitOptions.None);
    if (strArray.Length < 2)
      return;
    StringsHelper.HexToInt32(strArray[0]);
    string codeString = str.Substring(strArray[0].Length + ForumsConsts.Splitter.Length);
    for (int length = codeString.Length; length > 0; length = codeString.Length)
    {
      UserMessage userMessage = new UserMessage();
      int startIndex = userMessage.FromString(codeString);
      userMessage.DicsObjectGuid = dbObject.ObjectGUID.ToString();
      userMessage.DiscussedObjectGuid = attributeById.AsString;
      if (startIndex > 0)
        this.Add(userMessage);
      codeString = startIndex <= 0 || startIndex >= codeString.Length ? string.Empty : codeString.Substring(startIndex);
    }
  }

  /// <summary>Удалить сообщение из обсуждения</summary>
  /// <param name="discObject"></param>
  /// <param name="deletedMessage"></param>
  /// <param name="session"></param>
  public void DeleteMessage(IDBObject discObject, UserMessage deletedMessage, IUserSession session)
  {
    this.Remove(deletedMessage);
    IDBAttribute attributeById = discObject.GetAttributeByID(ForumsConsts.forumAttributeID);
    attributeById.ClearValues();
    foreach (UserMessage curMessage in (List<UserMessage>) this)
    {
      if (curMessage.DicsObjectGuid == deletedMessage.DicsObjectGuid)
        this.AddMessage(curMessage, attributeById, session);
    }
  }

  /// <summary>добавить сообщение</summary>
  /// <param name="curMessage"></param>
  /// <param name="stringAttr"></param>
  /// <param name="session"></param>
  public void AddMessage(UserMessage curMessage, IDBAttribute stringAttr, IUserSession session)
  {
    string str1 = curMessage.ToString();
    string str2 = stringAttr.Values[stringAttr.ValuesCount - 1].ToString() + str1;
    bool flag = stringAttr.ValuesCount == 1;
    if (!flag)
    {
      stringAttr.Index = stringAttr.ValuesCount - 1;
      stringAttr.DeleteValue();
    }
    while (str2.Length > 0)
    {
      string empty = string.Empty;
      int int32 = Convert.ToInt32(session.GetAttributeType(new Guid(ForumsConsts.forumAttributeGuid)).SizeType);
      string newValue;
      if (str2.Length > int32)
      {
        newValue = str2.Substring(0, int32);
        str2 = str2.Substring(int32, str2.Length - int32);
      }
      else
      {
        newValue = str2;
        str2 = string.Empty;
      }
      if (flag)
      {
        stringAttr.Value = (object) newValue;
        flag = false;
      }
      else
        stringAttr.AddValue((object) newValue);
      if (!this.Contains(curMessage))
        this.Add(curMessage);
    }
  }

  /// <summary>изменить сообщение</summary>
  /// <param name="discObject"></param>
  /// <param name="oldMessage"></param>
  /// <param name="newMessage"></param>
  /// <param name="session"></param>
  public void ChangeMessage(IDBObject discObject, Guid discGuid, IUserSession session)
  {
    IDBAttribute attributeById = discObject.GetAttributeByID(ForumsConsts.forumAttributeID);
    attributeById.ClearValues();
    foreach (UserMessage curMessage in (List<UserMessage>) this)
    {
      if (curMessage.DicsObjectGuid == discGuid.ToString())
        this.AddMessage(curMessage, attributeById, session);
    }
  }

  /// <summary>Найти сообщение пользователя</summary>
  /// <param name="messageID"></param>
  public UserMessage FindMessage(string messageID)
  {
    int int32 = Convert.ToInt32(messageID);
    foreach (UserMessage message in (List<UserMessage>) this)
    {
      if (message.GetHashCode() == int32)
        return message;
    }
    return (UserMessage) null;
  }
}
