// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.CheckThread
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;


namespace Intermech.Kernel.Briefcase;

internal sealed class CheckThread : DBSessionable
{
  private List<int> _progress = new List<int>();
  private DataSet _metaData;
  private DataSet _importMetaData;
  private CheckOptions _options;
  private Thread _thread;
  private Guid _numOfCheck;
  public SetImportProgressEventHandler SetImportProgressEvent;

  public List<CheckMetadataLogItem> CheckLog { get; private set; }

  public CheckThread(
    UserSession session,
    DataSet metaData,
    DataSet importMetaData,
    CheckOptions options,
    Guid numOfCheck)
    : base(session)
  {
    this._metaData = metaData;
    this._importMetaData = importMetaData;
    this._options = options;
    this._numOfCheck = numOfCheck;
  }

  public void Start(string threadName)
  {
    this.CheckLog = new List<CheckMetadataLogItem>();
    this._thread = new Thread(new ThreadStart(this.ThreadMethod))
    {
      IsBackground = true,
      Name = threadName
    };
    this._thread.Start();
  }

  public void Cancel()
  {
    if (this._thread == null || !this._thread.IsAlive)
      return;
    this._thread.Abort();
    this._thread.Join();
  }

  private void GenerateStateEvent(BriefcaseImportProgress bip)
  {
    if (this.SetImportProgressEvent == null)
      return;
    this.SetImportProgressEvent((object) this, new SetImportProgressEventArgs(this._numOfCheck, bip));
    this._progress.Add(bip.Percent);
  }

  private void ThreadMethod()
  {
    BriefcaseImportProgress bip = new BriefcaseImportProgress(OperationType.CheckingMetaData);
    string sessionName = $"ThreadMethod_{Guid.NewGuid()}";
    IUserSession session = this.UserSession.Clone(true, sessionName);
    try
    {
      bip.Percent = 0;
      this.GenerateStateEvent(bip);
      DataRow[] dataRowArray = this._importMetaData.Tables[BriefcaseConsts.XmlMetadataTableName].Select();
      int num = 0;
      foreach (DataRow dataRow in dataRowArray)
      {
        ICheckItem checkItem = (ICheckItem) null;
        switch (Convert.ToInt32(dataRow[BriefcaseConsts.XmlCategoryTag]))
        {
          case 3:
            DataRow briefRow1 = this._metaData.Tables["IMS_ATTRIBUTES"].Rows.Find(dataRow[BriefcaseConsts.XmlIdTag]);
            checkItem = (ICheckItem) new CheckAttributeType(session as UserSession, this._metaData, briefRow1, this._options);
            break;
          case 4:
            DataRow briefRow2 = this._metaData.Tables["IMS_OBJECT_TYPES"].Rows.Find(dataRow[BriefcaseConsts.XmlIdTag]);
            checkItem = (ICheckItem) new CheckObjectType(session as UserSession, this._metaData, briefRow2, this._options);
            break;
          case 6:
            DataRow briefRow3 = this._metaData.Tables["IMS_RELATION_TYPES"].Rows.Find(dataRow[BriefcaseConsts.XmlIdTag]);
            checkItem = (ICheckItem) new CheckRelationType(session as UserSession, this._metaData, briefRow3, this._options);
            break;
          case 7:
            DataRow briefRow4 = this._metaData.Tables["IMS_LC_STEPS"].Rows.Find(dataRow[BriefcaseConsts.XmlIdTag]);
            checkItem = (ICheckItem) new CheckLCStep(session as UserSession, this._metaData, briefRow4, this._options);
            break;
          case 8:
            DataRow briefRow5 = this._metaData.Tables["IMS_LEVELS"].Rows.Find(dataRow[BriefcaseConsts.XmlIdTag]);
            checkItem = (ICheckItem) new CheckLCLevel(session as UserSession, this._metaData, briefRow5, this._options);
            break;
          case 9:
            DataRow briefRow6 = this._metaData.Tables["IMS_LANGUAGES"].Rows.Find(dataRow[BriefcaseConsts.XmlIdTag]);
            checkItem = (ICheckItem) new CheckLanguage(session as UserSession, this._metaData, briefRow6, this._options);
            break;
          case 11:
            DataRow briefRow7 = this._metaData.Tables["IMS_SUBJECT_AREAS"].Rows.Find(dataRow[BriefcaseConsts.XmlIdTag]);
            checkItem = (ICheckItem) new CheckSubjectArea(session as UserSession, this._metaData, briefRow7, this._options);
            break;
          case 12:
            DataRow briefRow8 = this._metaData.Tables["IMS_ATTR_GROUPS"].Rows.Find(dataRow[BriefcaseConsts.XmlIdTag]);
            checkItem = (ICheckItem) new CheckAttributesGroup(session as UserSession, this._metaData, briefRow8, this._options);
            break;
          case 16 /*0x10*/:
            DataRow briefRow9 = this._metaData.Tables["IMS_LC_SCHEMAS"].Rows.Find(dataRow[BriefcaseConsts.XmlIdTag]);
            checkItem = (ICheckItem) new CheckLCShema(session as UserSession, this._metaData, briefRow9, this._options);
            break;
        }
        try
        {
          checkItem.Initialize();
          if (checkItem.Existing)
          {
            if ((this._options & CheckOptions.CreateOnly) != CheckOptions.CreateOnly)
              checkItem.Check();
          }
        }
        finally
        {
          ILogged<CheckMetadataLogItem> logged = checkItem as ILogged<CheckMetadataLogItem>;
          if (logged.Log.Count > 0)
            this.CheckLog.AddRange((IEnumerable<CheckMetadataLogItem>) logged.Log);
          ++num;
          bip.Percent = (int) Math.Ceiling((double) (100 * num / dataRowArray.Length));
          this.GenerateStateEvent(bip);
        }
      }
      this.GenerateStateEvent(new BriefcaseImportProgress(OperationType.CheckingTerminate)
      {
        Percent = 100,
        CheckErrors = this.CheckLog
      });
    }
    catch (Exception ex)
    {
      this.CheckLog.Add(new CheckMetadataLogItem(CheckMetadataLogItemType.Error, "Ошибка при проверке данных портфеля", string.Empty, ex.Message));
      this.GenerateStateEvent(new BriefcaseImportProgress(OperationType.CheckingTerminate)
      {
        Percent = 100,
        CheckErrors = this.CheckLog
      });
    }
    finally
    {
      session.Logout(sessionName);
    }
  }
}
