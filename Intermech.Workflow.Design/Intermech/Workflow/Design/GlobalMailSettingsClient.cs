// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.GlobalMailSettingsClient
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Client.Core;
using Intermech.Interfaces.Workflow;
using System.ComponentModel;

#nullable disable
namespace Intermech.Workflow.Design;

public class GlobalMailSettingsClient : GlobalMailSettings
{
  private UserPropertyClass _workflowAdminUserIDProp;
  private CalendarPropertyClass _calendarIDProp;

  [TypeConverter(typeof (YesNoBooleanConverter))]
  public override bool ClearMailFoldersOnCompletion
  {
    get => this._clearMailFoldersOnCompletion;
    set => this._clearMailFoldersOnCompletion = value;
  }

  [TypeConverter(typeof (YesNoBooleanConverter))]
  public override bool ClearMailFoldersOnTermination
  {
    get => this._clearMailFoldersOnTermination;
    set => this._clearMailFoldersOnTermination = value;
  }

  [TypeConverter(typeof (YesNoBooleanConverter))]
  public override bool NotifyAdminAboutErrors
  {
    get => this._notifyAdminAboutErrors;
    set => this._notifyAdminAboutErrors = value;
  }

  [CustomDisplayName("Attribute.Workflow.Server_5")]
  [CustomDescription("Attribute.Workflow.Server_6")]
  public UserPropertyClass WorkflowAdminUserIDProp
  {
    get
    {
      if (this._workflowAdminUserIDProp == null)
        this._workflowAdminUserIDProp = new UserPropertyClass(this.WorkflowAdminUserID);
      return this._workflowAdminUserIDProp;
    }
    set
    {
      this._workflowAdminUserIDProp = value;
      if (value == null)
        return;
      this.WorkflowAdminUserID = value.ObjectID;
    }
  }

  [TypeConverter(typeof (YesNoBooleanConverter))]
  public override bool CollSkipEmptyVars
  {
    get => this._collSkipEmptyVars;
    set => this._collSkipEmptyVars = value;
  }

  [TypeConverter(typeof (YesNoBooleanConverter))]
  public override bool SendTempRightsError
  {
    get => this._sendTempRightsError;
    set => this._sendTempRightsError = value;
  }

  [TypeConverter(typeof (YesNoBooleanConverter))]
  public override bool CreateActivitiesOnDemand
  {
    get => this._createActivitiesOnDemand;
    set => this._createActivitiesOnDemand = value;
  }

  [TypeConverter(typeof (YesNoBooleanConverter))]
  public override bool LaunchBaseSchemesOnly
  {
    get => this._launchBaseSchemesOnly;
    set => this._launchBaseSchemesOnly = value;
  }

  [TypeConverter(typeof (YesNoBooleanConverter))]
  public override bool SendEmailNotifications
  {
    get => this._sendEmailNotifications;
    set => this._sendEmailNotifications = value;
  }

  [CustomDisplayName("CalendarID")]
  [CustomDescription("CalendarIDDesc")]
  [DefaultValue(typeof (CalendarPropertyClass), null)]
  public CalendarPropertyClass CalendarIDProp
  {
    get
    {
      if (this._calendarIDProp == null)
        this._calendarIDProp = new CalendarPropertyClass(this.CalendarID);
      return this._calendarIDProp;
    }
    set
    {
      this._calendarIDProp = value;
      if (value != null)
        this.CalendarID = value.ObjectID;
      else
        this.CalendarID = 0L;
    }
  }

  public override long CalendarID
  {
    get => this._calendarID;
    set
    {
      this._calendarID = value;
      this._calendarIDProp = (CalendarPropertyClass) null;
    }
  }

  public override long WorkflowAdminUserID
  {
    get => this._workflowAdminUserID;
    set
    {
      this._workflowAdminUserID = value;
      this._workflowAdminUserIDProp = (UserPropertyClass) null;
    }
  }

  [TypeConverter(typeof (YesNoBooleanConverter))]
  public override bool DeleteFileLinkObjects
  {
    get => this._deleteFileLinkObjects;
    set => this._deleteFileLinkObjects = value;
  }
}
