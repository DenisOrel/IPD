// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.ServerFunx
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.Interfaces;
using System;

#nullable disable
namespace Intermech.Workflow.Server;

internal class ServerFunx
{
  internal static IDBObject CreateWorkOffer(
    IUserSession session,
    int typeID,
    long RecipID,
    string Text,
    WFActivity activity,
    ProcessPriority priority = ProcessPriority.Normal,
    long fromUserID = 0)
  {
    IDBObject messageBase = ServerFunx.CreateMessageBase(session, typeID, RecipID, Text, activity.ProcessID, activity.ObjectID, fromUserID, priority);
    EmailSender.Send(session, RecipID, EmailSender.WorkOfferConst, Text, messageBase);
    return messageBase;
  }

  private static IDBObject CreateMessageBase(
    IUserSession session,
    int typeID,
    long RecipID,
    string Text,
    long ProcessID,
    long ActivityID,
    long FromUserID,
    ProcessPriority priority = ProcessPriority.Normal)
  {
    IDBObject messageBase = session.GetObjectCollection(typeID).Create();
    if (ProcessID != 0L)
    {
      messageBase.GetAttributeByID(wfConsts.AttrProcessID).AsInteger = ProcessID;
      IDBObject dbObject = session.GetObject(ProcessID, false);
      if (dbObject != null)
        messageBase.ProjectID = dbObject.ProjectID;
    }
    if (ActivityID != 0L)
      messageBase.GetAttributeByID(wfConsts.AttrActivityID).AsInteger = ActivityID;
    messageBase.GetAttributeByID(wfConsts.AttrPriorityID).AsInteger = (long) priority;
    messageBase.GetAttributeByID(wfConsts.AttrRecipID).AsInteger = RecipID;
    if (FromUserID == 0L)
      FromUserID = session.UserID;
    messageBase.GetAttributeByID(wfConsts.AttrSenderID).AsInteger = FromUserID;
    messageBase.GetAttributeByID(wfConsts.AttrActivityMessageID).AsString = Text;
    messageBase.GetAttributeByID(wfConsts.AttrStartedID).AsDateTime = DateTime.Now;
    messageBase.Attributes.AddAttribute(wfConsts.AttrRecipStatusID, false, new object[1]
    {
      (object) 0
    });
    messageBase.CommitCreation(false);
    return messageBase;
  }

  internal static IDBObject CreateMessage(
    IUserSession session,
    int TypeID,
    long RecipID,
    string Subject,
    string Text,
    long ProcessID,
    long ActivityID,
    long FromUserID)
  {
    IDBObject messageBase = ServerFunx.CreateMessageBase(session, TypeID, RecipID, Text, ProcessID, ActivityID, FromUserID);
    messageBase.GetAttributeByID(wfConsts.AttrSubjectID).AsString = Subject;
    EmailSender.Send(session, RecipID, Subject, Text, messageBase);
    return messageBase;
  }

  internal static IDBObject CreateMessage(
    IUserSession session,
    long RecipID,
    string Subject,
    string Text,
    long ProcessID,
    long ActivityID,
    long FromUserID)
  {
    return ServerFunx.CreateMessage(session, wfConsts.MessageTypeID, RecipID, Subject, Text, ProcessID, ActivityID, FromUserID);
  }

  internal static IDBObject CreateMessage(
    IUserSession session,
    long RecipID,
    string Subject,
    string Text,
    long ProcessID,
    long ActivityID)
  {
    return ServerFunx.CreateMessage(session, RecipID, Subject, Text, ProcessID, ActivityID, 0L);
  }

  internal static string ReplaceTextMacros(string text, VarList variableList)
  {
    string str1 = string.Empty;
    int startIndex1 = 0;
    int num = 0;
    int startIndex2 = 0;
    while (startIndex1 != -1 && startIndex1 < text.Length)
    {
      startIndex1 = text.IndexOf('%', startIndex1);
      if (startIndex1 != -1)
      {
        num = startIndex1 + 1;
        while (num < text.Length && text[num] != ' ' && text[num] != '%')
          ++num;
        if (num < text.Length && text[num] == '%')
        {
          string name = text.Substring(startIndex1 + 1, num - startIndex1 - 1);
          Variable variable = variableList.GetVariable(name);
          string str2 = variable == null ? (!(name == string.Empty) ? $"%{name}%" : "%") : variable.UserValue;
          str1 = str1 + text.Substring(startIndex2, startIndex1 - startIndex2) + str2;
          ++num;
        }
        else
          str1 += text.Substring(startIndex2, num - startIndex2);
        startIndex1 = num;
        startIndex2 = startIndex1;
      }
      else
        str1 += text.Substring(num, text.Length - num);
    }
    return str1;
  }

  internal static void CopyAttachmentsFlag(IDBObject currentObject, IDBObject msg)
  {
    IDBAttribute attributeById = currentObject.GetAttributeByID(wfConsts.AttrAttachmentsID);
    if (attributeById == null)
      return;
    ServerFunx.WriteAttachmentsFlag(msg, attributeById.AsInteger);
  }

  internal static void WriteAttachmentsFlag(IDBObject msg, long value)
  {
    IDBAttribute attributeById = msg.GetAttributeByID(wfConsts.AttrAttachmentsID);
    if (attributeById == null || attributeById.AsInteger == value)
      return;
    attributeById.AsInteger = value;
  }
}
