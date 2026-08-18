// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.GlobalMailSettings
// Assembly: Intermech.Workflow.Base, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 43DB3E33-56C8-49B7-85B7-A2947193D068
// Assembly location: D:\IPS\Client\Intermech.Workflow.Base.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Base.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using System.ComponentModel;


namespace Intermech.Workflow
{
    public class GlobalMailSettings
    {
      protected bool _clearMailFoldersOnCompletion;
      protected bool _clearMailFoldersOnTermination = true;
      protected long _workflowAdminUserID;
      protected bool _notifyAdminAboutErrors;
      protected bool _collSkipEmptyVars;
      protected bool _sendTempRightsError = true;
      protected bool _createActivitiesOnDemand = true;
      protected bool _launchBaseSchemesOnly = true;
      protected bool _sendEmailNotifications;
      protected long _calendarID;
      protected bool _deleteFileLinkObjects;
      private static GlobalMailSettings _cfg;
      public const string ModuleName = "Workflow";
      public const string SectionID = "Global";

      [CustomDisplayName("Attribute.Workflow.Server_1")]
      [CustomDescription("Attribute.Workflow.Server_2")]
      [DefaultValue(false)]
      public virtual bool ClearMailFoldersOnCompletion
      {
        get => this._clearMailFoldersOnCompletion;
        set => this._clearMailFoldersOnCompletion = value;
      }

      [CustomDisplayName("Attribute.Workflow.Server_3")]
      [CustomDescription("Attribute.Workflow.Server_4")]
      [DefaultValue(true)]
      public virtual bool ClearMailFoldersOnTermination
      {
        get => this._clearMailFoldersOnTermination;
        set => this._clearMailFoldersOnTermination = value;
      }

      [Browsable(false)]
      public virtual long WorkflowAdminUserID
      {
        get => this._workflowAdminUserID;
        set => this._workflowAdminUserID = value;
      }

      [CustomDisplayName("Attribute.Workflow.Server_7")]
      [CustomDescription("Attribute.Workflow.Server_8")]
      [DefaultValue(false)]
      public virtual bool NotifyAdminAboutErrors
      {
        get => this._notifyAdminAboutErrors;
        set => this._notifyAdminAboutErrors = value;
      }

      [CustomDisplayName("CollSkipEmptyVars")]
      [CustomDescription("CollSkipEmptyVarsDesc")]
      [DefaultValue(false)]
      public virtual bool CollSkipEmptyVars
      {
        get => this._collSkipEmptyVars;
        set => this._collSkipEmptyVars = value;
      }

      [CustomDisplayName("SendTempRightsError")]
      [CustomDescription("SendTempRightsErrorDesc")]
      [DefaultValue(true)]
      public virtual bool SendTempRightsError
      {
        get => this._sendTempRightsError;
        set => this._sendTempRightsError = value;
      }

      [CustomDisplayName("CreateActivitiesOnDemand")]
      [CustomDescription("CreateActivitiesOnDemandDesc")]
      [Browsable(false)]
      [DefaultValue(true)]
      public virtual bool CreateActivitiesOnDemand
      {
        get => this._createActivitiesOnDemand;
        set => this._createActivitiesOnDemand = value;
      }

      [Browsable(false)]
      public bool ValidateProcessOnStart => false;

      [CustomDisplayName("LaunchBaseSchemesOnly")]
      [CustomDescription("LaunchBaseSchemesOnlyDesc")]
      [Browsable(false)]
      [DefaultValue(true)]
      public virtual bool LaunchBaseSchemesOnly
      {
        get => this._launchBaseSchemesOnly;
        set => this._launchBaseSchemesOnly = value;
      }

      [CustomDisplayName("SendEmailNotifications")]
      [CustomDescription("SendEmailNotificationsDesc")]
      [DefaultValue(false)]
      public virtual bool SendEmailNotifications
      {
        get => this._sendEmailNotifications;
        set => this._sendEmailNotifications = value;
      }

      [Browsable(false)]
      public virtual long CalendarID
      {
        get => this._calendarID;
        set => this._calendarID = value;
      }

      [DisplayName("Удалять вместе с процессом объекты типа \"Ссылка на файл\"")]
      [Description("Если в процессе использовались объекты типа \"Ссылка на файл\" они будут удалены вместе с процессом при условии, что данные объекты больше нигде не используются.")]
      [DefaultValue(false)]
      public virtual bool DeleteFileLinkObjects
      {
        get => this._deleteFileLinkObjects;
        set => this._deleteFileLinkObjects = value;
      }

      [Browsable(false)]
      public static GlobalMailSettings Cfg => GlobalMailSettings._cfg;

      public static void Init(IUserSession session)
      {
        if (GlobalMailSettings._cfg != null)
          return;
        GlobalMailSettings._cfg = new GlobalMailSettings();
        GlobalMailSettings._cfg.Load(session);
      }

      public void Save(IUserSession session)
      {
        IDBConfigurations configurations = session.Configurations;
        configurations.WriteBool("Workflow", "Global", "ClearMailFoldersOnCompletion", this.ClearMailFoldersOnCompletion, 0L);
        configurations.WriteBool("Workflow", "Global", "ClearMailFoldersOnTermination", this.ClearMailFoldersOnTermination, 0L);
        configurations.WriteInteger("Workflow", "Global", "WorkflowAdminUserID", this.WorkflowAdminUserID, 0L);
        configurations.WriteBool("Workflow", "Global", "NotifyAdminAboutErrors", this.NotifyAdminAboutErrors, 0L);
        configurations.WriteBool("Workflow", "Global", "CollSkipEmptyVars", this.CollSkipEmptyVars, 0L);
        configurations.WriteBool("Workflow", "Global", "SendTempRightsError", this.SendTempRightsError, 0L);
        configurations.WriteBool("Workflow", "Global", "CreateActivitiesOnDemand", true, 0L);
        configurations.WriteBool("Workflow", "Global", "LaunchBaseSchemesOnly", true, 0L);
        configurations.WriteBool("Workflow", "Global", "SendEmailNotifications", this.SendEmailNotifications, 0L);
        configurations.WriteBool("Workflow", "Global", "DeleteFileLinkObjects", this.DeleteFileLinkObjects, 0L);
        configurations.WriteInteger("Workflow", "Global", "CalendarID", this.CalendarID, 0L);
      }

      public void Load(IUserSession session)
      {
        if (!(ApplicationServices.Container.GetService(typeof (IDBConfigurations)) is IDBConfigurations dbConfigurations1))
          dbConfigurations1 = session.Configurations;
        IDBConfigurations dbConfigurations2 = dbConfigurations1;
        this.ClearMailFoldersOnCompletion = dbConfigurations2.ReadBool("Workflow", "Global", "ClearMailFoldersOnCompletion", this.ClearMailFoldersOnCompletion, DBConfigMode.GlobalOnly);
        this.ClearMailFoldersOnTermination = dbConfigurations2.ReadBool("Workflow", "Global", "ClearMailFoldersOnTermination", this.ClearMailFoldersOnTermination, DBConfigMode.GlobalOnly);
        this.WorkflowAdminUserID = dbConfigurations2.ReadInteger("Workflow", "Global", "WorkflowAdminUserID", this.WorkflowAdminUserID, DBConfigMode.GlobalOnly);
        if (this.WorkflowAdminUserID == 0L)
          this.WorkflowAdminUserID = session.IdentHelper.SysdbaID;
        this.NotifyAdminAboutErrors = dbConfigurations2.ReadBool("Workflow", "Global", "NotifyAdminAboutErrors", this.NotifyAdminAboutErrors, DBConfigMode.GlobalOnly);
        this.CollSkipEmptyVars = dbConfigurations2.ReadBool("Workflow", "Global", "CollSkipEmptyVars", this.CollSkipEmptyVars, DBConfigMode.GlobalOnly);
        this.SendTempRightsError = dbConfigurations2.ReadBool("Workflow", "Global", "SendTempRightsError", this.SendTempRightsError, DBConfigMode.GlobalOnly);
        this.CreateActivitiesOnDemand = true;
        this.LaunchBaseSchemesOnly = true;
        this.SendEmailNotifications = dbConfigurations2.ReadBool("Workflow", "Global", "SendEmailNotifications", this.SendEmailNotifications, DBConfigMode.GlobalOnly);
        this.DeleteFileLinkObjects = dbConfigurations2.ReadBool("Workflow", "Global", "DeleteFileLinkObjects", this.DeleteFileLinkObjects, DBConfigMode.GlobalOnly);
        this.CalendarID = dbConfigurations2.ReadInteger("Workflow", "Global", "CalendarID", this.CalendarID, DBConfigMode.GlobalOnly);
      }

      public void Assign(GlobalMailSettings src)
      {
        this.ClearMailFoldersOnCompletion = src.ClearMailFoldersOnCompletion;
        this.ClearMailFoldersOnTermination = src.ClearMailFoldersOnTermination;
        this.WorkflowAdminUserID = src.WorkflowAdminUserID;
        this.NotifyAdminAboutErrors = src.NotifyAdminAboutErrors;
        this.CollSkipEmptyVars = src.CollSkipEmptyVars;
        this.SendTempRightsError = src.SendTempRightsError;
        this.CreateActivitiesOnDemand = true;
        this.LaunchBaseSchemesOnly = true;
        this.SendEmailNotifications = src.SendEmailNotifications;
        this.DeleteFileLinkObjects = src.DeleteFileLinkObjects;
        this.CalendarID = src.CalendarID;
      }
    }
}
