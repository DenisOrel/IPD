// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.EmailSender
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

#nullable disable
namespace Intermech.Workflow.Server;

public class EmailSender
{
  internal static readonly string WorkOfferConst = "@wo@";
  private static Regex _hrefRehex = new Regex("<a href=\"#.*?>", RegexOptions.Compiled | RegexOptions.Singleline);

  private static bool Enabled => GlobalMailSettings.Cfg.SendEmailNotifications;

  public static void Send(IUserSession session, long[] toUserIDs, string subject, string message)
  {
    if (!EmailSender.Enabled || !(session.GetCustomService(typeof (IEmailService)) is IEmailService customService) || customService.Servers == null || customService.Servers.Length < 1)
      return;
    EmailServer server = customService.Servers[0];
    EmailAccaunt[] accaunts = customService.GetAccaunts(server.Guid);
    if (accaunts == null || accaunts.Length == 0)
      return;
    Guid guid = accaunts[0].Guid;
    foreach (long toUserId in toUserIDs)
    {
      IDBObject dbObject = session.GetObject(toUserId, false);
      if (dbObject != null)
      {
        IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(new Guid("cad002de-306c-11d8-b4e9-00304f19f545"));
        if (attributeByGuid != null && !string.IsNullOrEmpty(attributeByGuid.AsString))
        {
          string asString = attributeByGuid.AsString;
          try
          {
            customService.SendMessage(session.SessionGUID, guid, asString, subject, message);
          }
          catch
          {
          }
        }
      }
    }
  }

  public static void Send(
    IUserSession session,
    long[] toUserIDs,
    string subject,
    string message,
    IDBObject obj)
  {
    if (!EmailSender.Enabled)
      return;
    string str1 = "IPS";
    string str2 = "Получатель=recip,Отправитель=sender,Дата=date,Процесс=process,Шаг=activity,Заголовки вложений=attachCaption";
    string empty1 = string.Empty;
    WFActivity act = obj as WFActivity;
    NameValueCollection nameValueCollection = new NameValueCollection();
    IDBAttribute attributeById1 = obj.GetAttributeByID(wfConsts.AttrSenderID);
    if (attributeById1 != null)
    {
      QuickObjectInfo objectInfo = session.GetObjectInfo(attributeById1.AsInteger);
      nameValueCollection["sender"] = objectInfo.Caption;
    }
    string empty2 = string.Empty;
    foreach (long toUserId in toUserIDs)
    {
      QuickObjectInfo objectInfo = session.GetObjectInfo(toUserId);
      if (!string.IsNullOrEmpty(empty2))
        empty2 += ", ";
      empty2 += objectInfo.Caption;
    }
    nameValueCollection["recip"] = empty2;
    IDBAttribute attributeById2 = obj.GetAttributeByID(wfConsts.AttrStartedID);
    if (attributeById2 != null)
      nameValueCollection["date"] = attributeById2.AsDateTime.ToString();
    if (act != null)
    {
      nameValueCollection["activity"] = act.Name;
      nameValueCollection["process"] = act.ProcessName;
      Dictionary<long, object[]> rows = new Dictionary<long, object[]>();
      IDBAttribute attributeById3 = act.GetAttributeByID(wfConsts.AttrExecHistoryID);
      if (attributeById3 != null)
      {
        List<long> historyData = MiscFunx.GetHistoryData(session, wfConsts.IsMessage(act.TypeID), attributeById3, rows, (IDBObject) act, false, act.ObjectID, (long) act.ObjectType);
        List<MessageRow> subrows = new List<MessageRow>();
        if (rows.Count > 0 && historyData != null)
        {
          StringBuilder stringBuilder = new StringBuilder();
          string str3 = "style =\"padding: 10px 23px 10px 26px; margin: 0px; FONT-WEIGHT: bold; font-size: 12px; height: 37px; background: #ccc;\"";
          stringBuilder.Append($"<table style=\"width: 100% \"><tr><th {str3}>Сообщения пользователей на предыдущих шагах выполнения</th></tr>");
          string str4 = "style =\"width: 100%; padding: 16px 23px 16px 23px; margin: 0px; BORDER-TOP: #fff 1px solid; BORDER-BOTTOM: #989898 1px solid; background: #f0f0f0;\"";
          string str5 = "style =\"padding-left: 0px; width: 100%;\"";
          string str6 = "style =\"padding: 0px 0px 10px 5px; width: 85px;\"";
          string str7 = "style =\"padding: 0px 2px 15px 10px; FONT-WEIGHT: bold;\"";
          string str8 = "style =\"padding: 0px 0px 10px 10px; FLOAT: left;\"";
          string str9 = "style =\"padding: 10px 20px 10px 20px; font-size: 100%; margin: 0px; BORDER: #bdbdbd 1px solid; background: #fff;white-space: pre-wrap;\"";
          string curSiteGuid = (string) null;
          foreach (long num in historyData)
          {
            if (rows.ContainsKey(num) && rows[num].Length >= 8)
            {
              subrows = MiscFunx.GetMessageRows(rows[num], subrows, session, num, ref curSiteGuid);
              if (subrows.Count > 0)
                stringBuilder.Append($"<tr><td {str4}><table>");
              foreach (MessageRow messageRow in subrows)
              {
                object[] data = messageRow.Data;
                string str10 = data[3].ToString();
                if (!string.IsNullOrEmpty(str10))
                {
                  string str11 = string.Empty;
                  if (data[5] != DBNull.Value && !string.IsNullOrEmpty(data[5].ToString()))
                  {
                    ActivityResult int32 = (ActivityResult) Convert.ToInt32(data[5]);
                    str11 += SimpleFuncs.GetEnumDescription((Enum) int32);
                  }
                  DateTime dateTime = DateTime.Now;
                  string empty3 = string.Empty;
                  object obj1 = data[7];
                  object obj2 = data[2];
                  if (data.Length > 9 && data[9] != DBNull.Value)
                  {
                    obj1 = data[9];
                    obj2 = data[10];
                  }
                  long int32_1 = obj1.Equals((object) DBNull.Value) ? 0L : (long) Convert.ToInt32(obj1);
                  string str12 = obj2.Equals((object) DBNull.Value) ? "Система" : obj2.ToString();
                  if (data[6] != DBNull.Value)
                    dateTime = Convert.ToDateTime(data[6], (IFormatProvider) CultureInfo.InvariantCulture);
                  if (!string.IsNullOrEmpty(messageRow.SrcSiteName))
                    str12 = $"{messageRow.SrcSiteName} / {str12}";
                  if (string.IsNullOrEmpty(str11))
                    str11 = "Нет";
                  string str13 = messageRow.RemoteProcessName;
                  if (str13 != "")
                    str13 = str13 == null ? messageRow.SrcSiteName + " / " : $"{messageRow.SrcSiteName} / {str13} / ";
                  string str14 = str13 + data[1].ToString();
                  if (int32_1 == 0L)
                  {
                    long systemUserId = wfConsts.SystemUserID;
                  }
                  string str15 = $"<tr {str5}><td {str6}>Отправитель:</td><td {str7}><a>{str12}</a></td></tr><tr {str5}><td {str6}>Шаг:</td><td {str8}><a>{str14}</a></td></tr><tr {str5}><td {str6}>Отправлено:</td><td {str8}>{str11}</td></tr><tr {str5}><td {str6}>Дата:</td><td {str8}>{dateTime.ToString()}</td></tr><tr><td {str9} colspan=\"2\">{str10}</td></tr>";
                  stringBuilder.Append(str15);
                }
              }
              if (subrows.Count > 0)
                stringBuilder.Append("</table></td></tr>");
            }
          }
          stringBuilder.Append("</table>");
          empty1 = stringBuilder.ToString();
          stringBuilder.Clear();
        }
      }
      if (act.Attachments.Count > 0)
      {
        StringBuilder stringBuilder = new StringBuilder();
        foreach (Attachment attachment in (List<Attachment>) act.Attachments)
        {
          IDBObject dbObject = session.GetObject(attachment.ObjectID, false);
          if (dbObject != null)
          {
            if (stringBuilder.Length > 0)
              stringBuilder.Append(", ");
            stringBuilder.Append(dbObject.Caption);
          }
        }
        nameValueCollection["attachCaption"] = stringBuilder.ToString();
        stringBuilder.Clear();
      }
    }
    else
    {
      IDBAttribute attributeById4 = obj.GetAttributeByID(wfConsts.AttrActivityID);
      if (attributeById4 != null)
        nameValueCollection["activity"] = attributeById4.AsString;
      IDBAttribute attributeById5 = obj.GetAttributeByID(wfConsts.AttrProcessID);
      if (attributeById5 != null)
        nameValueCollection["process"] = attributeById5.AsString;
    }
    bool flag = false;
    string str16 = $"В <a href=\"ips://mail\">почту {str1}</a>";
    if (subject == EmailSender.WorkOfferConst)
      subject = $"{str1} | Новое предложение | {nameValueCollection["activity"]} - {nameValueCollection["process"]}";
    else if (act == null)
    {
      subject = $"{str1} | Новое сообщение | {subject}";
    }
    else
    {
      subject = $"{str1} | Новая задача | {nameValueCollection["activity"]} - {nameValueCollection["process"]}";
      message = $"{str16} пришла задача \"{nameValueCollection["activity"]}\" процесса \"{nameValueCollection["process"]}\".";
      flag = true;
    }
    if (!flag && !string.IsNullOrEmpty(message))
    {
      message = EmailSender._hrefRehex.Replace(message, "<a href=\"ips://mail\">");
      message = $"{str16} пришло сообщение:<br />{message}";
    }
    string str17 = string.Empty;
    string str18 = str2;
    char[] chArray1 = new char[1]{ ',' };
    foreach (string str19 in str18.Split(chArray1))
    {
      char[] chArray2 = new char[1]{ '=' };
      string[] strArray = str19.Split(chArray2);
      if (strArray.Length == 2)
      {
        string str20 = nameValueCollection[strArray[1]];
        if (!string.IsNullOrEmpty(str20))
          str17 = $"{str17}<tr><th>{strArray[0]} </th><td> {str20}</td></tr>\r\n";
      }
    }
    if (!string.IsNullOrEmpty(str17))
      str17 = $"<table style=\"border:none; text-align: left\">\r\n{str17}\r\n</table>";
    if (!string.IsNullOrEmpty(str17))
      message = $"{message}<br />\r\n<h4>Дополнительно</h4>\r\n{str17}";
    if (!string.IsNullOrEmpty(empty1))
      message = $"{message}<br />\r\n{empty1}";
    EmailSender.Send(session, toUserIDs, subject, message);
  }

  public static void Send(
    IUserSession session,
    long toUserID,
    string subject,
    string message,
    IDBObject obj)
  {
    EmailSender.Send(session, new long[1]{ toUserID }, subject, message, obj);
  }
}
