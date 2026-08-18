// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.Email.EmailDownloader
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.Workflow;
using Intermech.Interfaces.Workflow.Email;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading;

#nullable disable
namespace Intermech.Workflow.Server.Email;

internal class EmailDownloader
{
  public EmailDownloadProperties Properties;
  private string _accauntEmail;
  private bool _stop;
  private bool _removeMessages;
  public DownloadCompleteEventHandler DownloadCompleteEvent;
  private bool _writeToLog;

  public EmailDownloader(string accauntEmail, bool removeMessages, bool writeToLog)
  {
    this._accauntEmail = accauntEmail;
    this._removeMessages = removeMessages;
    this._writeToLog = writeToLog;
    this.Properties = new EmailDownloadProperties();
  }

  public void StartDownload(IUserSession session)
  {
    new Thread(new ParameterizedThreadStart(this.DownloadMethod))
    {
      IsBackground = true,
      Name = $"DownloadEmail_{this._accauntEmail}"
    }.Start((object) session);
  }

  public void StopDownload() => this._stop = true;

  private void DownloadMethod(object arg)
  {
    IUserSession userSession = ((IUserSession) arg).Clone("EmailDownloader.DownloadMethod");
    try
    {
      this.Properties.Percent = 1;
      EmailAccaunt accaunt = ((IEmailService) userSession.GetCustomService(typeof (IEmailService))).GetAccaunt(this._accauntEmail);
      if (accaunt == null)
        throw new Exception(string.Format(LocalizationHolder.rm.GetString("Workflow.Server_42"), (object) this._accauntEmail));
      IDBObjectCollection objectCollection = userSession.GetObjectCollection(wfConsts.objtypeEmailMessages);
      DataTable dataTable = objectCollection.Select(new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(wfConsts.attributeEmail, RelationalOperators.Equal, (object) this._accauntEmail, LogicalOperators.AND, 0)
      }, new ColumnDescriptor[1]
      {
        new ColumnDescriptor((object) wfConsts.attributeMessageIDGuid, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0)
      }));
      List<string> presentMessageIDs = new List<string>(dataTable.Rows.Count);
      for (int index = 0; index < dataTable.Rows.Count; ++index)
      {
        string str = Convert.ToString(dataTable.Rows[index][0]);
        if (str != string.Empty && presentMessageIDs.IndexOf(str) < 0)
          presentMessageIDs.Add(str);
      }
      this.Properties.Percent = 2;
      IEmailService customService = (IEmailService) userSession.GetCustomService(typeof (IEmailService));
      int num = 0;
      int attributeTypeId1 = MetaDataHelper.GetAttributeTypeID(wfConsts.attributeEmailSender);
      int attributeTypeId2 = MetaDataHelper.GetAttributeTypeID(wfConsts.attributeMessageIDGuid);
      int attributeTypeId3 = MetaDataHelper.GetAttributeTypeID(wfConsts.attributeEmail);
      int attributeTypeId4 = MetaDataHelper.GetAttributeTypeID(wfConsts.attributeInReplyTo);
      int attributeTypeId5 = MetaDataHelper.GetAttributeTypeID(wfConsts.attributeSender);
      int attributeTypeId6 = MetaDataHelper.GetAttributeTypeID(wfConsts.attributeEmailData);
      List<string> deleteList = (List<string>) null;
      if (this._removeMessages)
        deleteList = new List<string>();
      for (List<EmailMessage> inboxMessages = customService.GetInboxMessages(userSession.SessionGUID, accaunt.Guid, presentMessageIDs); inboxMessages != null && inboxMessages.Count > 0; inboxMessages = customService.GetInboxMessages(userSession.SessionGUID, accaunt.Guid, presentMessageIDs))
      {
        if (this.Properties.Percent < 99)
          this.Properties.Percent += 3;
        List<string> files = new List<string>();
        for (int index1 = 0; index1 < inboxMessages.Count; ++index1)
        {
          EmailMessage emailMessage = inboxMessages[index1];
          if (emailMessage != null && emailMessage.MessagetID != null)
          {
            presentMessageIDs.Add(emailMessage.MessagetID);
            try
            {
              IDBObject dbObject = objectCollection.Create();
              dbObject.Attributes.AddAttribute(attributeTypeId1, false, new object[1]
              {
                (object) emailMessage.FromEmail
              });
              dbObject.Attributes.AddAttribute(attributeTypeId2, false, new object[1]
              {
                (object) emailMessage.MessagetID
              });
              dbObject.Attributes.AddAttribute(attributeTypeId3, false, new object[1]
              {
                (object) this._accauntEmail
              });
              dbObject.Attributes.AddAttribute(wfConsts.AttrActivityMessageID, false, new object[1]
              {
                (object) emailMessage.Message
              });
              dbObject.Attributes.AddAttribute(wfConsts.AttrSubjectID, false, new object[1]
              {
                (object) emailMessage.Subject
              });
              dbObject.Attributes.AddAttribute(attributeTypeId6, false, new object[1]
              {
                (object) emailMessage.Date
              });
              dbObject.Attributes.AddAttribute(attributeTypeId5, false, new object[1]
              {
                (object) emailMessage.From
              });
              if (!string.IsNullOrEmpty(emailMessage.InReplyTo))
                dbObject.Attributes.AddAttribute(attributeTypeId4, false, new object[1]
                {
                  (object) emailMessage.InReplyTo
                });
              if (emailMessage.FileNames != null && emailMessage.FileNames.Count > 0)
              {
                IDBAttribute dbAttribute = dbObject.Attributes.AddAttribute(userSession.IdentHelper.FileAttributeID, false);
                for (int index2 = 0; index2 < emailMessage.FileNames.Count; ++index2)
                {
                  long attachmentLength = customService.GetAttachmentLength(emailMessage.FileNames[index2].StotageFileName);
                  if (attachmentLength != 0L)
                  {
                    if (index2 > 0)
                      dbAttribute.AddValue((object) null);
                    IBlobWriter blobWriter = dbAttribute as IBlobWriter;
                    blobWriter.OpenBlob(new BlobInformation(attachmentLength, attachmentLength, DateTime.Now, Path.Combine(dbObject.ObjectGUID.ToString(), Path.GetFileName(emailMessage.FileNames[index2].FileName)), ArcMethods.NotPacked, string.Empty), false);
                    blobWriter.WriteDataBlock(customService.GetAttachmentData(emailMessage.FileNames[index2].StotageFileName, 0, Convert.ToInt32(attachmentLength)));
                    files.Add(emailMessage.FileNames[index2].StotageFileName);
                  }
                }
              }
              dbObject.CommitCreation(true);
              if (this._removeMessages)
                deleteList.Add(emailMessage.MessagetID);
              ++num;
            }
            catch (Exception ex)
            {
              if (this._writeToLog)
                ((IEventLogHelper) ApplicationServices.Container.GetService(typeof (IEventLogHelper))).AddToTrace($"Ошибка при сохранении письма от {emailMessage.FromEmail} ({emailMessage.Subject}) для {this._accauntEmail}: {ex.Message}", Consts.traceAlways, string.Empty);
            }
          }
        }
        if (files.Count > 0)
          customService.ClearTempFiles(files);
      }
      if (this._removeMessages)
        customService.ClearInbox(userSession.SessionGUID, accaunt.Guid, deleteList);
      this.Properties.Percent = 100;
      this.Properties.CountMessages = num;
      this.Properties.State = EmailDownloadState.Completed;
    }
    catch (Exception ex)
    {
      if (this._writeToLog)
        ((IEventLogHelper) ApplicationServices.Container.GetService(typeof (IEventLogHelper))).AddToTrace(string.Format(LocalizationHolder.rm.GetString("Workflow.Server_43"), (object) this._accauntEmail, (object) ex.Message), Consts.traceAlways, string.Empty);
      this.Properties.State = EmailDownloadState.Error;
      this.Properties.ErrorException = ex;
    }
    finally
    {
      userSession.Logout("EmailDownloader.DownloadMethod");
      if (this.DownloadCompleteEvent != null)
        this.DownloadCompleteEvent((object) this, new DownloadCompleteEventArgs(this._accauntEmail));
    }
  }
}
