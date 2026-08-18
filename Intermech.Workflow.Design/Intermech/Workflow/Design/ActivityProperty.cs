// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.ActivityProperty
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Workflow;
using Intermech.Workflow.Design.ActivityPropertyPages;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

public class ActivityProperty : Form
{
  private bool _readOnly;
  private ActivitySettings _activitySettings;
  private WorkflowNode _activityNode;
  public bool NameModified;
  private bool _modified;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private TabControl activitySettings;
  private TabPage generalSettingPage;
  private TabPage attachmentSettingPage;
  private Panel buttonPanel;
  private Button okBtn;
  private Button cancelBtn;
  private TabPage attachmentTypeSettingPage;
  private TabPage caseSettingPage;
  private TabPage conditionSettingPage;
  private TabPage participantsSettingPage;
  private TabPage signsSettingPage;
  private TabPage archiveSettingPage;
  private TabPage subProcessSettingPage;
  private TabPage remoteSubProcessSettingPage;
  private TabPage termsSettingPage;
  private TabPage timerSettingPage;
  private TabPage rollbackSettingPage;
  private TabPage statusSettingPage;
  private TabPage messagesSettingPage;
  private TabPage scriptsSettingPage;
  private GeneralSettingPageControl generalSettingPageControl;
  private AttachmentSettingPageControl attachmentSettingPageControl;
  private AttachmentTypeSettingPageControl attachmentTypeSettingPageControl;
  private CaseSettingPageControl caseSettingPageControl;
  private ConditionSettingPageControl conditionSettingPageControl;
  private ParticipantsSettingPageControl participantsSettingPageControl;
  private SignsSettingPageControl signsSettingPageControl;
  private ArchiveSettingPageControl archiveSettingPageControl;
  private SubProcessSettingPageControl subProcessSettingPageControl;
  private RemoteSubProcessSettingPageControl remoteSubProcessSettingPageControl;
  private TermsSettingPageControl termsSettingPageControl;
  private TimerSettingPageControl timerSettingPageControl;
  private RollbackSettingPageControl rollbackSettingPageControl;
  private StatusSettingPageControl statusSettingPageControl;
  private MessagesSettingPageControl messagesSettingPageControl;
  private ScriptsSettingPageControl scriptsSettingPageControl;
  private ImageList TabsIL;

  public ActivityProperty() => this.InitializeComponent();

  public bool ReadOnly
  {
    get => this._readOnly;
    set
    {
      this._readOnly = value;
      this.scriptsSettingPageControl.ReadOnly = value;
      this.conditionSettingPageControl.ReadOnly = value;
      this.caseSettingPageControl.ReadOnly = value;
      this.messagesSettingPageControl.ReadOnly = value;
      this.attachmentSettingPageControl.ReadOnly = value;
      this.generalSettingPageControl.ReadOnly = value;
      this.archiveSettingPageControl.ReadOnly = value;
      this.attachmentTypeSettingPageControl.ReadOnly = value;
      this.participantsSettingPageControl.ReadOnly = value;
      this.remoteSubProcessSettingPageControl.ReadOnly = value;
      this.rollbackSettingPageControl.ReadOnly = value;
      this.signsSettingPageControl.ReadOnly = value;
      this.statusSettingPageControl.ReadOnly = value;
      this.subProcessSettingPageControl.ReadOnly = value;
      this.termsSettingPageControl.ReadOnly = value;
      this.timerSettingPageControl.ReadOnly = value;
    }
  }

  public Dictionary<long, LinkKind> CaseLinksWithModifiedLinkType
  {
    get
    {
      return this.caseSettingPage != null ? this.caseSettingPageControl.CaseLinksWithModifiedLinkType : new Dictionary<long, LinkKind>();
    }
  }

  public List<long> LocalScriptsToDeleted
  {
    get => this.scriptsSettingPageControl.LocalScriptsToDeleted;
    set => this.scriptsSettingPageControl.LocalScriptsToDeleted = value;
  }

  public Dictionary<int, long> NewScripts
  {
    get => this.scriptsSettingPageControl.NewScripts;
    set => this.scriptsSettingPageControl.NewScripts = value;
  }

  public bool AddedNewScriptToDelete => this.scriptsSettingPageControl.AddedNewScriptToDelete;

  /// <summary>Загрузить настройки действия в форму</summary>
  public void LoadProperty(IDBObject activityObject, WorkflowNode activityNode)
  {
    IUserSession session = activityObject.Session;
    this._activitySettings = new ActivitySettings()
    {
      ActivityType = activityObject.ObjectType,
      ActivityObjectID = activityObject.ObjectID,
      ExtProperties = new ExtProperties(activityObject, wfConsts.AttrAddInfoID)
    };
    this._activityNode = activityNode;
    if (!this.ReadOnly && activityObject is IActivity)
    {
      if (activityNode?.View != null)
      {
        try
        {
          IDBObject process = activityNode.View.GetProcess(session);
          VarList varList = new VarList(session, false, false);
          varList.Load(process);
          varList.AddSystemVariables(process);
          foreach (Variable variable in varList)
          {
            this._activitySettings.ActivityExpressionAttributes.Add(MiscFunx.CreateExpressionVariable(variable.Name, variable.VarType));
            this._activitySettings.ActivityAllAttributeValues.Add(new AttributeValues(variable.AttrTypeID, variable.TypedValue)
            {
              AttributeName = variable.Name
            });
          }
          if (process is IScheme)
          {
            foreach (Variable variable in (VarList) new GlobalVariablesList(process, false, false))
            {
              this._activitySettings.ActivityExpressionAttributes.Add(MiscFunx.CreateExpressionVariable(variable.Name, variable.VarType));
              this._activitySettings.ActivityAllAttributeValues.Add(new AttributeValues(variable.AttrTypeID, variable.TypedValue)
              {
                AttributeName = variable.Name
              });
            }
          }
        }
        catch (Exception ex)
        {
          if (ApplicationServices.Container.GetService(typeof (IOutputView)) is IOutputView service)
            service.WriteString("Ошибки", "При попытке получения переменных действия произошла ошибка: " + ex.Message);
        }
      }
    }
    if (activityNode != null)
    {
      this._activitySettings.ActivityIcon = activityNode.Image.Image;
    }
    else
    {
      Icon icon = BaseHolder.IconService.GetIcon(4, this._activitySettings.ActivityType);
      if (icon != null)
        this._activitySettings.ActivityIcon = (Image) icon.ToBitmap();
    }
    if (this._activitySettings.ActivityType == wfConsts.SchemesTypeID || this._activitySettings.ActivityType == wfConsts.ProcessesTypeID)
      this._activitySettings.ProcessID = this._activitySettings.ActivityObjectID;
    else if (activityNode?.View != null)
    {
      this._activitySettings.ProcessID = activityNode.View.ProcessID;
    }
    else
    {
      IDBAttribute attributeById = activityObject.GetAttributeByID(wfConsts.AttrProcessID);
      if (attributeById != null)
        this._activitySettings.ProcessID = attributeById.AsInteger;
      if (this._activitySettings.ProcessID < 0L)
        this._activitySettings.ProcessID = -this._activitySettings.ProcessID;
    }
    IDBAttribute attributeById1 = activityObject.GetAttributeByID(wfConsts.AttrNameID);
    if (attributeById1 != null)
    {
      this._activitySettings.ActivityName = attributeById1.AsString;
      string str = LocalizationHolder.rm.GetString("Workflow.Design_7");
      if (this._activitySettings.ActivityType == wfConsts.SchemesTypeID)
        str = LocalizationHolder.rm.GetString("Workflow.Design_8");
      else if (this._activitySettings.ActivityType == wfConsts.ProcessesTypeID)
        str = LocalizationHolder.rm.GetString("Workflow.Design_9");
      this.Text = $"{str} \"{this._activitySettings.ActivityName}\"";
    }
    else
      this._activitySettings.ActivityName = (string) null;
    this._activitySettings.ActivityDescription = activityObject.Attributes.FindByID(wfConsts.AttrDescriptionID)?.AsString;
    this.generalSettingPageControl.LoadGeneralPropertyControl(this._activitySettings, activityObject);
    bool flag1 = this.participantsSettingPageControl.LoadParticipantSettingControl(this._activitySettings, activityObject);
    if (flag1)
    {
      this.activitySettings.TabPages.Remove(this.participantsSettingPage);
      this.participantsSettingPage = (TabPage) null;
    }
    bool flag2 = this.rollbackSettingPageControl.LoadRollbackSettingControl(this._activitySettings, activityObject, activityNode);
    if (flag2)
    {
      this.activitySettings.TabPages.Remove(this.rollbackSettingPage);
      this.rollbackSettingPage = (TabPage) null;
    }
    if (this.messagesSettingPageControl.LoadMessagesSettingPageControl(this._activitySettings, activityObject, !flag2, session))
    {
      this.activitySettings.TabPages.Remove(this.messagesSettingPage);
      this.messagesSettingPage = (TabPage) null;
    }
    if (this.conditionSettingPageControl.LoadConditionSettingPageControl(this._activitySettings, activityObject))
    {
      this.activitySettings.TabPages.Remove(this.conditionSettingPage);
      this.conditionSettingPage = (TabPage) null;
    }
    if (this.caseSettingPageControl.LoadCaseSettingPageControl(this._activitySettings, activityObject, activityNode))
    {
      this.activitySettings.TabPages.Remove(this.caseSettingPage);
      this.caseSettingPage = (TabPage) null;
    }
    if (this.subProcessSettingPageControl.LoadSubProcessSettingPageControl(this._activitySettings, activityObject))
    {
      this.activitySettings.TabPages.Remove(this.subProcessSettingPage);
      this.subProcessSettingPage = (TabPage) null;
    }
    if (this.signsSettingPageControl.LoadSignsSettingPageControl(this._activitySettings, activityObject, activityNode, session))
    {
      this.activitySettings.TabPages.Remove(this.signsSettingPage);
      this.signsSettingPage = (TabPage) null;
    }
    if (this.statusSettingPageControl.LoadStatusSettingPageControl(this._activitySettings, activityObject))
    {
      this.activitySettings.TabPages.Remove(this.statusSettingPage);
      this.statusSettingPage = (TabPage) null;
    }
    if (this.timerSettingPageControl.LoadTimerSettingPageControl(this._activitySettings, activityObject, session))
    {
      this.activitySettings.TabPages.Remove(this.timerSettingPage);
      this.timerSettingPage = (TabPage) null;
    }
    int num = this.attachmentSettingPageControl.LoadAttachmentSettingControl(this._activitySettings, activityObject, !flag1) ? 1 : 0;
    this.attachmentTypeSettingPageControl.LoadAttachmentTypeSettingControl(this._activitySettings);
    bool flag3 = num == 0;
    if (num != 0)
    {
      this.activitySettings.TabPages.Remove(this.attachmentSettingPage);
      this.attachmentSettingPage = (TabPage) null;
    }
    if (flag3)
    {
      this.activitySettings.TabPages.Remove(this.attachmentTypeSettingPage);
      this.attachmentTypeSettingPage = (TabPage) null;
    }
    if (this.scriptsSettingPageControl.LoadScriptsSettingPageControl(this._activitySettings, activityObject, !flag1, session))
    {
      this.activitySettings.TabPages.Remove(this.scriptsSettingPage);
      this.scriptsSettingPage = (TabPage) null;
    }
    if (this.archiveSettingPageControl.LoadArchiveSettingPageControl(this._activitySettings, activityObject, session))
    {
      this.activitySettings.TabPages.Remove(this.archiveSettingPage);
      this.archiveSettingPage = (TabPage) null;
    }
    if (this.termsSettingPageControl.LoadTermsSettingPageControl(this._activitySettings, activityObject, !flag1))
    {
      this.activitySettings.TabPages.Remove(this.termsSettingPage);
      this.termsSettingPage = (TabPage) null;
    }
    if (!this.remoteSubProcessSettingPageControl.LoadRemoteSubProcessSettingPageControl(this._activitySettings, activityObject))
      return;
    this.activitySettings.TabPages.Remove(this.remoteSubProcessSettingPage);
    this.remoteSubProcessSettingPage = (TabPage) null;
  }

  /// <summary>Сохранить новые настройки действия</summary>
  public bool SaveProperty(IDBObject activityToSave)
  {
    if (this.ReadOnly)
      return false;
    this._activitySettings.ActivityFlags = (ActivityFlags) 0;
    long num = 0;
    bool modified1 = this.generalSettingPageControl.Save(activityToSave);
    this.NameModified = this.generalSettingPageControl.NameModified;
    bool modified2 = this.participantsSettingPageControl.Save(activityToSave, this.scriptsSettingPageControl.AdditionalParticipantsModified, modified1);
    IDBAttribute attributeById = activityToSave.GetAttributeByID(wfConsts.AttrAddIDID);
    if (attributeById != null)
      num = attributeById.AsInteger;
    if (this.rollbackSettingPage != null)
      modified2 = this.rollbackSettingPageControl.Save(activityToSave, modified2);
    if (this.messagesSettingPage != null)
      modified2 = this.messagesSettingPageControl.Save(activityToSave, modified2);
    if (this._activitySettings.ActivityType == wfConsts.CondTypeID)
      modified2 = this.conditionSettingPageControl.Save(activityToSave, modified2);
    if (this._activitySettings.ActivityType == wfConsts.CaseTypeID)
      modified2 = this.caseSettingPageControl.Save(activityToSave, modified2);
    if (this._activitySettings.ActivityType == wfConsts.SubProcessTypeID)
      modified2 = this.subProcessSettingPageControl.Save(activityToSave, modified2);
    if (this.signsSettingPage != null)
      modified2 = this.signsSettingPageControl.Save(activityToSave, modified2);
    if (this.statusSettingPage != null)
      modified2 = this.statusSettingPageControl.Save(activityToSave, modified2);
    bool modified3 = this.timerSettingPageControl.Save(activityToSave, modified2);
    bool modified4 = this.attachmentSettingPageControl.Save(activityToSave, modified3, this.attachmentSettingPage != null, this.participantsSettingPage != null);
    if (this.attachmentTypeSettingPage != null)
      modified4 = this.attachmentTypeSettingPageControl.Save(activityToSave, modified4);
    if (this.scriptsSettingPage != null)
      modified4 = this.scriptsSettingPageControl.Save(activityToSave, modified4);
    if (this._activitySettings.ActivityType == wfConsts.RegisterTypeID)
      modified4 = this.archiveSettingPageControl.Save(activityToSave, modified4);
    if (this._activitySettings.ActivityType == wfConsts.RemoteSubProcessTypeID)
      modified4 = this.remoteSubProcessSettingPageControl.Save(activityToSave, modified4);
    bool flag = this.termsSettingPageControl.Save(activityToSave, modified4, this.participantsSettingPageControl.AnyPartChecked);
    if (num != (long) this._activitySettings.ActivityFlags)
    {
      flag = true;
      activityToSave.Attributes.AddAttribute(wfConsts.AttrAddIDID, false, new object[1]
      {
        (object) (int) this._activitySettings.ActivityFlags
      });
    }
    if (this._activitySettings.ExtProperties != null && this._activitySettings.ExtProperties.Modified)
    {
      this._activitySettings.ExtProperties.Save(activityToSave);
      flag = true;
    }
    if (this._modified)
      flag = true;
    return flag;
  }

  private void ActivityProperty_Resize(object sender, EventArgs e)
  {
    this.signsSettingPageControl.ResizeControl();
  }

  private void ActivityProperty_KeyDown(object sender, KeyEventArgs e)
  {
    if (e.KeyCode == Keys.Escape)
      this.DialogResult = DialogResult.Cancel;
    else if (e.Shift && e.Control && e.Alt)
    {
      if (e.KeyCode == Keys.F)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBAttribute objectAttributeById = sessionKeeper.Session.GetObjectAttributeByID(this._activitySettings.ActivityObjectID, wfConsts.AttrFormID);
          long asInteger = objectAttributeById != null ? objectAttributeById.AsInteger : 0L;
          if (asInteger <= 0L)
            return;
          IDBAttribute objectAttributeByGuid = sessionKeeper.Session.GetObjectAttributeByGuid(asInteger, new Guid("cad0011d-306c-11d8-b4e9-00304f19f545"));
          if (!(objectAttributeByGuid is IBlobReader))
            return;
          using (FileStream aDestStream = new FileStream(Holder.WorkflowTempPath + "form.xml", FileMode.Create))
            new BlobProcReader(objectAttributeByGuid, 0, (Stream) aDestStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).ReadData();
        }
      }
      else if (e.KeyCode == Keys.V)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject src = sessionKeeper.Session.GetObject(this._activitySettings.ActivityObjectID);
          if (src == null)
            return;
          int num = (int) MessageBox.Show(new VarList(src, false, false).ToString());
        }
      }
      else if (e.KeyCode == Keys.C)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(this._activitySettings.ActivityObjectID);
          if (dbObject == null || MessageBox.Show("Clear vars?", "", MessageBoxButtons.YesNo) != DialogResult.Yes)
            return;
          new VarList(sessionKeeper.Session, false, false).Save(dbObject, true);
          this._modified = true;
        }
      }
      else if (e.KeyCode == Keys.I)
      {
        if (this._activityNode == null)
          return;
        int num = (int) MessageBox.Show(string.Format(this._activityNode.DebugInfo));
      }
      else
      {
        if (e.KeyCode != Keys.T || this._activitySettings.PeriodInformation == null)
          return;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject varSource = sessionKeeper.Session.GetObject(this._activitySettings.ActivityObjectID);
          if (varSource == null)
            return;
          int num = (int) MessageBox.Show("ExecTime in UTC=" + this._activitySettings.PeriodInformation.GetExecTime(varSource).ToString());
        }
      }
    }
    else
    {
      if (!e.Shift || e.KeyCode != Keys.Insert || this.ReadOnly || this.activitySettings.SelectedTab != this.participantsSettingPage)
        return;
      this.participantsSettingPageControl.AddStarterVariable();
    }
  }

  private void activitySettings_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this.activitySettings.TabPages[this.activitySettings.SelectedIndex] != this.termsSettingPage)
      return;
    this.termsSettingPageControl.SetOptsGroupVisible(this.participantsSettingPageControl.AnyPartChecked);
  }

  private void ActivityProperty_Load(object sender, EventArgs e)
  {
    Dictionary<string, int> dictionary = new Dictionary<string, int>()
    {
      {
        "RanksPanelH",
        this.signsSettingPageControl.RanksPanelHeight
      }
    };
    FormStorage.LoadLayout((Control) this, (IDictionary) dictionary);
    this.signsSettingPageControl.RanksPanelHeight = dictionary["RanksPanelH"];
  }

  private void ActivityProperty_FormClosed(object sender, FormClosedEventArgs e)
  {
    FormStorage.SaveLayout((Control) this, (IDictionary) new Dictionary<string, int>()
    {
      {
        "RanksPanelH",
        this.signsSettingPageControl.RanksPanelHeight
      }
    });
  }

  private void ActivityProperty_HelpButtonClicked(object sender, CancelEventArgs e)
  {
    this.ShowHelpTopic();
  }

  private void ActivityProperty_HelpRequested(object sender, HelpEventArgs hlpevent)
  {
    this.ShowHelpTopic();
  }

  /// <summary>выбор раздела для активной закладки</summary>
  private void ShowHelpTopic()
  {
    int topicID = 1291;
    if (this.activitySettings.SelectedTab == this.generalSettingPage)
      topicID = 1292;
    else if (this.activitySettings.SelectedTab == this.attachmentSettingPage)
      topicID = 1293;
    else if (this.activitySettings.SelectedTab == this.caseSettingPage)
      topicID = 1297;
    else if (this.activitySettings.SelectedTab == this.conditionSettingPage)
      topicID = 1296;
    else if (this.activitySettings.SelectedTab == this.participantsSettingPage)
      topicID = 1294;
    else if (this.activitySettings.SelectedTab == this.signsSettingPage)
      topicID = 1295;
    else if (this.activitySettings.SelectedTab == this.archiveSettingPage)
      topicID = 1302;
    else if (this.activitySettings.SelectedTab == this.rollbackSettingPage)
      topicID = 1298;
    else if (this.activitySettings.SelectedTab == this.statusSettingPage)
      topicID = 1304;
    else if (this.activitySettings.SelectedTab == this.subProcessSettingPage)
      topicID = 1300;
    else if (this.activitySettings.SelectedTab == this.timerSettingPage)
      topicID = 1303;
    else if (this.activitySettings.SelectedTab == this.termsSettingPage)
      topicID = 1299;
    else if (this.activitySettings.SelectedTab == this.messagesSettingPage)
      topicID = 1301;
    else if (this.activitySettings.SelectedTab == this.scriptsSettingPage)
      topicID = 1305;
    HelpProvidersClass.ShowHelpTopic(topicID);
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ActivityProperty));
    this.activitySettings = new TabControl();
    this.generalSettingPage = new TabPage();
    this.generalSettingPageControl = new GeneralSettingPageControl();
    this.attachmentSettingPage = new TabPage();
    this.attachmentSettingPageControl = new AttachmentSettingPageControl();
    this.attachmentTypeSettingPage = new TabPage();
    this.attachmentTypeSettingPageControl = new AttachmentTypeSettingPageControl();
    this.caseSettingPage = new TabPage();
    this.caseSettingPageControl = new CaseSettingPageControl();
    this.conditionSettingPage = new TabPage();
    this.conditionSettingPageControl = new ConditionSettingPageControl();
    this.participantsSettingPage = new TabPage();
    this.participantsSettingPageControl = new ParticipantsSettingPageControl();
    this.signsSettingPage = new TabPage();
    this.signsSettingPageControl = new SignsSettingPageControl();
    this.archiveSettingPage = new TabPage();
    this.archiveSettingPageControl = new ArchiveSettingPageControl();
    this.subProcessSettingPage = new TabPage();
    this.subProcessSettingPageControl = new SubProcessSettingPageControl();
    this.remoteSubProcessSettingPage = new TabPage();
    this.remoteSubProcessSettingPageControl = new RemoteSubProcessSettingPageControl();
    this.termsSettingPage = new TabPage();
    this.termsSettingPageControl = new TermsSettingPageControl();
    this.timerSettingPage = new TabPage();
    this.timerSettingPageControl = new TimerSettingPageControl();
    this.rollbackSettingPage = new TabPage();
    this.rollbackSettingPageControl = new RollbackSettingPageControl();
    this.statusSettingPage = new TabPage();
    this.statusSettingPageControl = new StatusSettingPageControl();
    this.messagesSettingPage = new TabPage();
    this.messagesSettingPageControl = new MessagesSettingPageControl();
    this.scriptsSettingPage = new TabPage();
    this.scriptsSettingPageControl = new ScriptsSettingPageControl();
    this.TabsIL = new ImageList(this.components);
    this.buttonPanel = new Panel();
    this.okBtn = new Button();
    this.cancelBtn = new Button();
    this.activitySettings.SuspendLayout();
    this.generalSettingPage.SuspendLayout();
    this.attachmentSettingPage.SuspendLayout();
    this.attachmentTypeSettingPage.SuspendLayout();
    this.caseSettingPage.SuspendLayout();
    this.conditionSettingPage.SuspendLayout();
    this.participantsSettingPage.SuspendLayout();
    this.signsSettingPage.SuspendLayout();
    this.archiveSettingPage.SuspendLayout();
    this.subProcessSettingPage.SuspendLayout();
    this.remoteSubProcessSettingPage.SuspendLayout();
    this.termsSettingPage.SuspendLayout();
    this.timerSettingPage.SuspendLayout();
    this.rollbackSettingPage.SuspendLayout();
    this.statusSettingPage.SuspendLayout();
    this.messagesSettingPage.SuspendLayout();
    this.scriptsSettingPage.SuspendLayout();
    this.buttonPanel.SuspendLayout();
    this.SuspendLayout();
    this.activitySettings.Controls.Add((Control) this.generalSettingPage);
    this.activitySettings.Controls.Add((Control) this.attachmentSettingPage);
    this.activitySettings.Controls.Add((Control) this.attachmentTypeSettingPage);
    this.activitySettings.Controls.Add((Control) this.caseSettingPage);
    this.activitySettings.Controls.Add((Control) this.conditionSettingPage);
    this.activitySettings.Controls.Add((Control) this.participantsSettingPage);
    this.activitySettings.Controls.Add((Control) this.signsSettingPage);
    this.activitySettings.Controls.Add((Control) this.archiveSettingPage);
    this.activitySettings.Controls.Add((Control) this.subProcessSettingPage);
    this.activitySettings.Controls.Add((Control) this.remoteSubProcessSettingPage);
    this.activitySettings.Controls.Add((Control) this.termsSettingPage);
    this.activitySettings.Controls.Add((Control) this.timerSettingPage);
    this.activitySettings.Controls.Add((Control) this.rollbackSettingPage);
    this.activitySettings.Controls.Add((Control) this.statusSettingPage);
    this.activitySettings.Controls.Add((Control) this.messagesSettingPage);
    this.activitySettings.Controls.Add((Control) this.scriptsSettingPage);
    this.activitySettings.Dock = DockStyle.Fill;
    this.activitySettings.ImageList = this.TabsIL;
    this.activitySettings.Location = new Point(6, 5);
    this.activitySettings.Name = "activitySettings";
    this.activitySettings.SelectedIndex = 0;
    this.activitySettings.Size = new Size(821, 678);
    this.activitySettings.TabIndex = 0;
    this.activitySettings.SelectedIndexChanged += new EventHandler(this.activitySettings_SelectedIndexChanged);
    this.generalSettingPage.Controls.Add((Control) this.generalSettingPageControl);
    this.generalSettingPage.ImageIndex = 0;
    this.generalSettingPage.Location = new Point(4, 25);
    this.generalSettingPage.Name = "generalSettingPage";
    this.generalSettingPage.Size = new Size(813, 649);
    this.generalSettingPage.TabIndex = 0;
    this.generalSettingPage.Text = "Общие";
    this.generalSettingPageControl.BackColor = SystemColors.ControlLightLight;
    this.generalSettingPageControl.Dock = DockStyle.Fill;
    this.generalSettingPageControl.Location = new Point(0, 0);
    this.generalSettingPageControl.Name = "generalSettingPageControl";
    this.generalSettingPageControl.Padding = new Padding(5);
    this.generalSettingPageControl.ReadOnly = false;
    this.generalSettingPageControl.Size = new Size(813, 649);
    this.generalSettingPageControl.TabIndex = 0;
    this.attachmentSettingPage.Controls.Add((Control) this.attachmentSettingPageControl);
    this.attachmentSettingPage.ImageIndex = 2;
    this.attachmentSettingPage.Location = new Point(4, 25);
    this.attachmentSettingPage.Name = "attachmentSettingPage";
    this.attachmentSettingPage.Size = new Size(813, 649);
    this.attachmentSettingPage.TabIndex = 1;
    this.attachmentSettingPage.Text = "Вложения";
    this.attachmentSettingPageControl.AutoSize = true;
    this.attachmentSettingPageControl.BackColor = SystemColors.ControlLightLight;
    this.attachmentSettingPageControl.Dock = DockStyle.Fill;
    this.attachmentSettingPageControl.Location = new Point(0, 0);
    this.attachmentSettingPageControl.Name = "attachmentSettingPageControl";
    this.attachmentSettingPageControl.Padding = new Padding(10, 11, 11, 11);
    this.attachmentSettingPageControl.ReadOnly = false;
    this.attachmentSettingPageControl.Size = new Size(813, 649);
    this.attachmentSettingPageControl.TabIndex = 0;
    this.attachmentTypeSettingPage.Controls.Add((Control) this.attachmentTypeSettingPageControl);
    this.attachmentTypeSettingPage.ImageIndex = 2;
    this.attachmentTypeSettingPage.Location = new Point(4, 25);
    this.attachmentTypeSettingPage.Name = "attachmentTypeSettingPage";
    this.attachmentTypeSettingPage.Size = new Size(813, 649);
    this.attachmentTypeSettingPage.TabIndex = 2;
    this.attachmentTypeSettingPage.Text = "Типы вложений";
    this.attachmentTypeSettingPageControl.BackColor = SystemColors.ControlLightLight;
    this.attachmentTypeSettingPageControl.Dock = DockStyle.Fill;
    this.attachmentTypeSettingPageControl.Location = new Point(0, 0);
    this.attachmentTypeSettingPageControl.Name = "attachmentTypeSettingPageControl";
    this.attachmentTypeSettingPageControl.Padding = new Padding(10, 11, 11, 11);
    this.attachmentTypeSettingPageControl.ReadOnly = false;
    this.attachmentTypeSettingPageControl.Size = new Size(813, 649);
    this.attachmentTypeSettingPageControl.TabIndex = 0;
    this.caseSettingPage.Controls.Add((Control) this.caseSettingPageControl);
    this.caseSettingPage.ImageIndex = 5;
    this.caseSettingPage.Location = new Point(4, 25);
    this.caseSettingPage.Name = "caseSettingPage";
    this.caseSettingPage.Size = new Size(813, 649);
    this.caseSettingPage.TabIndex = 3;
    this.caseSettingPage.Text = "Условные переходы";
    this.caseSettingPageControl.BackColor = SystemColors.ControlLightLight;
    this.caseSettingPageControl.Dock = DockStyle.Fill;
    this.caseSettingPageControl.Location = new Point(0, 0);
    this.caseSettingPageControl.Name = "caseSettingPageControl";
    this.caseSettingPageControl.Padding = new Padding(10, 11, 11, 11);
    this.caseSettingPageControl.ReadOnly = false;
    this.caseSettingPageControl.Size = new Size(813, 649);
    this.caseSettingPageControl.TabIndex = 0;
    this.conditionSettingPage.Controls.Add((Control) this.conditionSettingPageControl);
    this.conditionSettingPage.ImageIndex = 3;
    this.conditionSettingPage.Location = new Point(4, 25);
    this.conditionSettingPage.Name = "conditionSettingPage";
    this.conditionSettingPage.Size = new Size(813, 649);
    this.conditionSettingPage.TabIndex = 4;
    this.conditionSettingPage.Text = "Условия";
    this.conditionSettingPageControl.BackColor = SystemColors.ControlLightLight;
    this.conditionSettingPageControl.Dock = DockStyle.Fill;
    this.conditionSettingPageControl.Location = new Point(0, 0);
    this.conditionSettingPageControl.Name = "conditionSettingPageControl";
    this.conditionSettingPageControl.Padding = new Padding(10, 11, 11, 11);
    this.conditionSettingPageControl.ReadOnly = false;
    this.conditionSettingPageControl.Size = new Size(813, 649);
    this.conditionSettingPageControl.TabIndex = 0;
    this.participantsSettingPage.Controls.Add((Control) this.participantsSettingPageControl);
    this.participantsSettingPage.ImageIndex = 1;
    this.participantsSettingPage.Location = new Point(4, 25);
    this.participantsSettingPage.Name = "participantsSettingPage";
    this.participantsSettingPage.Size = new Size(813, 649);
    this.participantsSettingPage.TabIndex = 5;
    this.participantsSettingPage.Text = "Исполнители";
    this.participantsSettingPageControl.BackColor = SystemColors.ControlLightLight;
    this.participantsSettingPageControl.Dock = DockStyle.Fill;
    this.participantsSettingPageControl.Location = new Point(0, 0);
    this.participantsSettingPageControl.MinimumSize = new Size(770, 500);
    this.participantsSettingPageControl.Name = "participantsSettingPageControl";
    this.participantsSettingPageControl.Padding = new Padding(10, 11, 11, 11);
    this.participantsSettingPageControl.ReadOnly = false;
    this.participantsSettingPageControl.Size = new Size(813, 649);
    this.participantsSettingPageControl.TabIndex = 0;
    this.signsSettingPage.Controls.Add((Control) this.signsSettingPageControl);
    this.signsSettingPage.ImageIndex = 8;
    this.signsSettingPage.Location = new Point(4, 25);
    this.signsSettingPage.Name = "signsSettingPage";
    this.signsSettingPage.Size = new Size(813, 649);
    this.signsSettingPage.TabIndex = 6;
    this.signsSettingPage.Text = "Подписи";
    this.signsSettingPageControl.AlienSettingsActs = "";
    this.signsSettingPageControl.BackColor = SystemColors.ControlLightLight;
    this.signsSettingPageControl.Dock = DockStyle.Fill;
    this.signsSettingPageControl.Location = new Point(0, 0);
    this.signsSettingPageControl.Name = "signsSettingPageControl";
    this.signsSettingPageControl.Padding = new Padding(10, 11, 11, 11);
    this.signsSettingPageControl.RanksPanelHeight = 228;
    this.signsSettingPageControl.ReadOnly = false;
    this.signsSettingPageControl.Size = new Size(813, 649);
    this.signsSettingPageControl.TabIndex = 0;
    this.archiveSettingPage.Controls.Add((Control) this.archiveSettingPageControl);
    this.archiveSettingPage.ImageIndex = 10;
    this.archiveSettingPage.Location = new Point(4, 25);
    this.archiveSettingPage.Name = "archiveSettingPage";
    this.archiveSettingPage.Size = new Size(813, 649);
    this.archiveSettingPage.TabIndex = 7;
    this.archiveSettingPage.Text = "Архив";
    this.archiveSettingPageControl.BackColor = SystemColors.ControlLightLight;
    this.archiveSettingPageControl.Dock = DockStyle.Fill;
    this.archiveSettingPageControl.Location = new Point(0, 0);
    this.archiveSettingPageControl.Name = "archiveSettingPageControl";
    this.archiveSettingPageControl.Padding = new Padding(10, 11, 11, 11);
    this.archiveSettingPageControl.ReadOnly = false;
    this.archiveSettingPageControl.Size = new Size(813, 649);
    this.archiveSettingPageControl.TabIndex = 0;
    this.subProcessSettingPage.Controls.Add((Control) this.subProcessSettingPageControl);
    this.subProcessSettingPage.ImageIndex = 4;
    this.subProcessSettingPage.Location = new Point(4, 25);
    this.subProcessSettingPage.Name = "subProcessSettingPage";
    this.subProcessSettingPage.Size = new Size(813, 649);
    this.subProcessSettingPage.TabIndex = 8;
    this.subProcessSettingPage.Text = "Подпроцесс";
    this.subProcessSettingPageControl.BackColor = SystemColors.ControlLightLight;
    this.subProcessSettingPageControl.Dock = DockStyle.Fill;
    this.subProcessSettingPageControl.Location = new Point(0, 0);
    this.subProcessSettingPageControl.Name = "subProcessSettingPageControl";
    this.subProcessSettingPageControl.Padding = new Padding(10, 11, 11, 11);
    this.subProcessSettingPageControl.ReadOnly = false;
    this.subProcessSettingPageControl.Size = new Size(813, 649);
    this.subProcessSettingPageControl.TabIndex = 0;
    this.remoteSubProcessSettingPage.Controls.Add((Control) this.remoteSubProcessSettingPageControl);
    this.remoteSubProcessSettingPage.ImageIndex = 15;
    this.remoteSubProcessSettingPage.Location = new Point(4, 25);
    this.remoteSubProcessSettingPage.Name = "remoteSubProcessSettingPage";
    this.remoteSubProcessSettingPage.Size = new Size(813, 649);
    this.remoteSubProcessSettingPage.TabIndex = 9;
    this.remoteSubProcessSettingPage.Text = "Удалённый подпроцесс";
    this.remoteSubProcessSettingPageControl.BackColor = SystemColors.ControlLightLight;
    this.remoteSubProcessSettingPageControl.Dock = DockStyle.Fill;
    this.remoteSubProcessSettingPageControl.Location = new Point(0, 0);
    this.remoteSubProcessSettingPageControl.Name = "remoteSubProcessSettingPageControl";
    this.remoteSubProcessSettingPageControl.Padding = new Padding(10, 11, 11, 11);
    this.remoteSubProcessSettingPageControl.ReadOnly = false;
    this.remoteSubProcessSettingPageControl.Size = new Size(813, 649);
    this.remoteSubProcessSettingPageControl.TabIndex = 0;
    this.termsSettingPage.Controls.Add((Control) this.termsSettingPageControl);
    this.termsSettingPage.ImageIndex = 13;
    this.termsSettingPage.Location = new Point(4, 25);
    this.termsSettingPage.Name = "termsSettingPage";
    this.termsSettingPage.Size = new Size(813, 649);
    this.termsSettingPage.TabIndex = 10;
    this.termsSettingPage.Text = "Сроки";
    this.termsSettingPageControl.BackColor = SystemColors.ControlLightLight;
    this.termsSettingPageControl.Dock = DockStyle.Fill;
    this.termsSettingPageControl.Location = new Point(0, 0);
    this.termsSettingPageControl.Name = "termsSettingPageControl";
    this.termsSettingPageControl.Padding = new Padding(10, 11, 11, 11);
    this.termsSettingPageControl.ReadOnly = false;
    this.termsSettingPageControl.Size = new Size(813, 649);
    this.termsSettingPageControl.TabIndex = 0;
    this.timerSettingPage.Controls.Add((Control) this.timerSettingPageControl);
    this.timerSettingPage.ImageIndex = 9;
    this.timerSettingPage.Location = new Point(4, 25);
    this.timerSettingPage.Name = "timerSettingPage";
    this.timerSettingPage.Size = new Size(813, 649);
    this.timerSettingPage.TabIndex = 11;
    this.timerSettingPage.Text = "Таймер";
    this.timerSettingPageControl.BackColor = SystemColors.ControlLightLight;
    this.timerSettingPageControl.Dock = DockStyle.Fill;
    this.timerSettingPageControl.Location = new Point(0, 0);
    this.timerSettingPageControl.Name = "timerSettingPageControl";
    this.timerSettingPageControl.Padding = new Padding(10, 11, 11, 11);
    this.timerSettingPageControl.ReadOnly = false;
    this.timerSettingPageControl.Size = new Size(813, 649);
    this.timerSettingPageControl.TabIndex = 0;
    this.rollbackSettingPage.Controls.Add((Control) this.rollbackSettingPageControl);
    this.rollbackSettingPage.ImageIndex = 7;
    this.rollbackSettingPage.Location = new Point(4, 25);
    this.rollbackSettingPage.Name = "rollbackSettingPage";
    this.rollbackSettingPage.Size = new Size(813, 649);
    this.rollbackSettingPage.TabIndex = 12;
    this.rollbackSettingPage.Text = "Возврат";
    this.rollbackSettingPageControl.BackColor = SystemColors.ControlLightLight;
    this.rollbackSettingPageControl.Dock = DockStyle.Fill;
    this.rollbackSettingPageControl.Location = new Point(0, 0);
    this.rollbackSettingPageControl.Name = "rollbackSettingPageControl";
    this.rollbackSettingPageControl.Padding = new Padding(10, 11, 11, 11);
    this.rollbackSettingPageControl.ReadOnly = false;
    this.rollbackSettingPageControl.Size = new Size(813, 649);
    this.rollbackSettingPageControl.TabIndex = 0;
    this.statusSettingPage.Controls.Add((Control) this.statusSettingPageControl);
    this.statusSettingPage.ImageIndex = 12;
    this.statusSettingPage.Location = new Point(4, 25);
    this.statusSettingPage.Name = "statusSettingPage";
    this.statusSettingPage.Size = new Size(813, 649);
    this.statusSettingPage.TabIndex = 13;
    this.statusSettingPage.Text = "Статус";
    this.statusSettingPageControl.BackColor = SystemColors.ControlLightLight;
    this.statusSettingPageControl.Dock = DockStyle.Fill;
    this.statusSettingPageControl.Location = new Point(0, 0);
    this.statusSettingPageControl.Name = "statusSettingPageControl";
    this.statusSettingPageControl.Padding = new Padding(10, 11, 11, 11);
    this.statusSettingPageControl.ReadOnly = false;
    this.statusSettingPageControl.Size = new Size(813, 649);
    this.statusSettingPageControl.TabIndex = 0;
    this.messagesSettingPage.Controls.Add((Control) this.messagesSettingPageControl);
    this.messagesSettingPage.ImageIndex = 14;
    this.messagesSettingPage.Location = new Point(4, 25);
    this.messagesSettingPage.Name = "messagesSettingPage";
    this.messagesSettingPage.Size = new Size(813, 649);
    this.messagesSettingPage.TabIndex = 14;
    this.messagesSettingPage.Text = "Сообщения";
    this.messagesSettingPageControl.BackColor = SystemColors.ControlLightLight;
    this.messagesSettingPageControl.Dock = DockStyle.Fill;
    this.messagesSettingPageControl.Location = new Point(0, 0);
    this.messagesSettingPageControl.Name = "messagesSettingPageControl";
    this.messagesSettingPageControl.Padding = new Padding(10, 11, 11, 11);
    this.messagesSettingPageControl.ReadOnly = false;
    this.messagesSettingPageControl.Size = new Size(813, 649);
    this.messagesSettingPageControl.TabIndex = 0;
    this.scriptsSettingPage.Controls.Add((Control) this.scriptsSettingPageControl);
    this.scriptsSettingPage.ImageIndex = 11;
    this.scriptsSettingPage.Location = new Point(4, 25);
    this.scriptsSettingPage.Name = "scriptsSettingPage";
    this.scriptsSettingPage.Size = new Size(813, 649);
    this.scriptsSettingPage.TabIndex = 15;
    this.scriptsSettingPage.Text = "Сценарии";
    this.scriptsSettingPageControl.BackColor = SystemColors.ControlLightLight;
    this.scriptsSettingPageControl.Dock = DockStyle.Fill;
    this.scriptsSettingPageControl.Location = new Point(0, 0);
    this.scriptsSettingPageControl.Name = "scriptsSettingPageControl";
    this.scriptsSettingPageControl.Padding = new Padding(10, 11, 11, 11);
    this.scriptsSettingPageControl.ReadOnly = false;
    this.scriptsSettingPageControl.Size = new Size(813, 649);
    this.scriptsSettingPageControl.TabIndex = 0;
    this.TabsIL.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("TabsIL.ImageStream");
    this.TabsIL.TransparentColor = Color.Fuchsia;
    this.TabsIL.Images.SetKeyName(0, "");
    this.TabsIL.Images.SetKeyName(1, "user_gr.ico");
    this.TabsIL.Images.SetKeyName(2, "addon.ico");
    this.TabsIL.Images.SetKeyName(3, "");
    this.TabsIL.Images.SetKeyName(4, "");
    this.TabsIL.Images.SetKeyName(5, "");
    this.TabsIL.Images.SetKeyName(6, "");
    this.TabsIL.Images.SetKeyName(7, "back.ico");
    this.TabsIL.Images.SetKeyName(8, "sign.ico");
    this.TabsIL.Images.SetKeyName(9, "");
    this.TabsIL.Images.SetKeyName(10, "arc.ico");
    this.TabsIL.Images.SetKeyName(11, "");
    this.TabsIL.Images.SetKeyName(12, "status.png");
    this.TabsIL.Images.SetKeyName(13, "time.ico");
    this.TabsIL.Images.SetKeyName(14, "info_z.ico");
    this.TabsIL.Images.SetKeyName(15, "Подпроцесс2_16.bmp");
    this.buttonPanel.Controls.Add((Control) this.okBtn);
    this.buttonPanel.Controls.Add((Control) this.cancelBtn);
    this.buttonPanel.Dock = DockStyle.Bottom;
    this.buttonPanel.Location = new Point(6, 683);
    this.buttonPanel.Name = "buttonPanel";
    this.buttonPanel.Size = new Size(821, 65);
    this.buttonPanel.TabIndex = 1;
    this.okBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.okBtn.DialogResult = DialogResult.OK;
    this.okBtn.Location = new Point(623, 21);
    this.okBtn.Name = "okBtn";
    this.okBtn.Size = new Size(90, 27);
    this.okBtn.TabIndex = 1;
    this.okBtn.Text = "ОК";
    this.okBtn.UseVisualStyleBackColor = true;
    this.cancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.cancelBtn.DialogResult = DialogResult.Cancel;
    this.cancelBtn.Location = new Point(719, 21);
    this.cancelBtn.Name = "cancelBtn";
    this.cancelBtn.Size = new Size(90, 27);
    this.cancelBtn.TabIndex = 0;
    this.cancelBtn.Text = "Отмена";
    this.cancelBtn.UseVisualStyleBackColor = true;
    this.AcceptButton = (IButtonControl) this.okBtn;
    this.AutoScaleDimensions = new SizeF(120f, 120f);
    this.AutoScaleMode = AutoScaleMode.Dpi;
    this.CancelButton = (IButtonControl) this.cancelBtn;
    this.ClientSize = new Size(832, 753);
    this.Controls.Add((Control) this.activitySettings);
    this.Controls.Add((Control) this.buttonPanel);
    this.HelpButton = true;
    this.KeyPreview = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.MinimumSize = new Size(850, 700);
    this.Name = nameof (ActivityProperty);
    this.Padding = new Padding(6, 5, 5, 5);
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Свойства действия";
    this.HelpButtonClicked += new CancelEventHandler(this.ActivityProperty_HelpButtonClicked);
    this.FormClosed += new FormClosedEventHandler(this.ActivityProperty_FormClosed);
    this.Load += new EventHandler(this.ActivityProperty_Load);
    this.HelpRequested += new HelpEventHandler(this.ActivityProperty_HelpRequested);
    this.KeyDown += new KeyEventHandler(this.ActivityProperty_KeyDown);
    this.Resize += new EventHandler(this.ActivityProperty_Resize);
    this.activitySettings.ResumeLayout(false);
    this.generalSettingPage.ResumeLayout(false);
    this.attachmentSettingPage.ResumeLayout(false);
    this.attachmentSettingPage.PerformLayout();
    this.attachmentTypeSettingPage.ResumeLayout(false);
    this.caseSettingPage.ResumeLayout(false);
    this.conditionSettingPage.ResumeLayout(false);
    this.participantsSettingPage.ResumeLayout(false);
    this.signsSettingPage.ResumeLayout(false);
    this.archiveSettingPage.ResumeLayout(false);
    this.subProcessSettingPage.ResumeLayout(false);
    this.remoteSubProcessSettingPage.ResumeLayout(false);
    this.termsSettingPage.ResumeLayout(false);
    this.timerSettingPage.ResumeLayout(false);
    this.rollbackSettingPage.ResumeLayout(false);
    this.statusSettingPage.ResumeLayout(false);
    this.messagesSettingPage.ResumeLayout(false);
    this.scriptsSettingPage.ResumeLayout(false);
    this.buttonPanel.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
