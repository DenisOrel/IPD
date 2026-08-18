// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.EmailConsts
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Interfaces;
using System;

#nullable disable
namespace Intermech.Workflow.Client;

internal static class EmailConsts
{
  public static readonly Guid EMailMessageColumnSchemeGuid = new Guid("{CD0E13E8-CB78-44c7-999E-EBB949E1F5E2}");
  public const string EmailImported = "EmailImported";
  public static int CategoryEmailMessage = -1;
  public static int CategoryEmail = -1;
  public static readonly Guid CategoryEmailMessageGuid = new Guid("{1ED0CFCC-3E75-4337-9BD0-4A94BC7A5326}");
  public static readonly Guid CategoryEmailGuid = new Guid("{C7E9CC95-AC0E-4aa4-BDAC-AD98DD91EBDC}");

  public static object ConvertValue(IUserSession session, EmailMessage message, Guid attributeGuid)
  {
    if (attributeGuid.Equals(wfConsts.attributeMessageIDGuid))
      return (object) message.MessagetID;
    if (attributeGuid.Equals(wfConsts.attributeSender))
      return (object) message.From;
    if (attributeGuid.Equals(wfConsts.attributeEmailSender))
      return (object) message.FromEmail;
    if (attributeGuid.Equals(wfConsts.attributeEmailData))
      return (object) (message.Date + session.TimeZoneOffset);
    return attributeGuid.Equals(wfConsts.AttrSubjectGuid) ? (object) message.Subject : (object) null;
  }
}
