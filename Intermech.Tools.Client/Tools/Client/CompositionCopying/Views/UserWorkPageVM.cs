// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Views.UserWorkPageVM
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Data;
using Intermech.Interfaces.Data.Actions;
using Intermech.IO;
using Intermech.Kernel.Search;
using Intermech.Tools.Client.CompositionCopying.Model;
using Intermech.UI;
using Intermech.UI.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Xml;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Views;

internal class UserWorkPageVM : WizardPageVM
{
  private CopyingSession _session;
  private string pageDescription;
  private PluggableCommand _saveToHtmlCommand;
  private bool _enableSaveButton;

  public UserWorkPageVM()
    : base("Готово")
  {
    this._saveToHtmlCommand = new PluggableCommand(new Action(this.SaveToHtml));
  }

  public UserWorkPageVM(CopyingSession session)
    : this()
  {
    this._session = session != null ? session : throw new ArgumentNullException(nameof (session));
  }

  public string PageDescription
  {
    [DebuggerStepThrough] get => this.pageDescription;
    set
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      if (!(this.pageDescription != value))
        return;
      this.pageDescription = value;
      this.RaisePropertyChanged("pageDescription");
    }
  }

  public PluggableCommand SaveToHtmlCommand => this._saveToHtmlCommand;

  public List<UserWorkItem> UserWorkItems => this._session.UserWorkItems;

  public bool VisibleSaveButton => this.UserWorkItems.Count > 0;

  public bool EnableSaveButton
  {
    get => this._enableSaveButton;
    set
    {
      this._enableSaveButton = value;
      this.RaisePropertyChanged(nameof (EnableSaveButton));
    }
  }

  protected override void DoActivate(
    WizardPageNavigationType navigationType,
    WizardPageVM previousPage)
  {
    base.DoActivate(navigationType, previousPage);
    if (this._session == null)
      return;
    this.PageDescription = this.UserWorkItems.Count > 0 ? "Работа мастера завершена, но перед использованием скопированных документов необходимо выполнить следующие действия вручную:" : "Работа мастера завершена в полностью автоматическом режиме. Скопированные документы готовы к использованию.";
    this.IsCompleted = true;
    this.EnableSaveButton = true;
  }

  protected override void DoDeactivate(
    WizardPageNavigationType navigationType,
    WizardPageVM nextPage)
  {
    base.DoDeactivate(navigationType, nextPage);
    CopyingSession session = this._session;
  }

  private void SaveToHtml()
  {
    if (this._session.UserWorkItems.Count == 0)
      return;
    IMSObjectType objectType = MetaDataHelper.GetObjectType(new Guid("CADD99D2-306C-11D8-B4E9-00304F19F545"));
    DateTime now = DateTime.Now;
    string text = $"Отчет о копировании документа '{this._session.Graph.RootVertext.Caption}' от {now}";
    XmlDocument xmlDocument = new XmlDocument();
    try
    {
      xmlDocument.AppendChild((XmlNode) xmlDocument.CreateElement("html"));
      XmlNode xmlNode1 = xmlDocument.DocumentElement.AppendChild((XmlNode) xmlDocument.CreateElement("head"));
      XmlNode xmlNode2 = xmlDocument.DocumentElement.AppendChild((XmlNode) xmlDocument.CreateElement("body"));
      XmlElement element1 = xmlDocument.CreateElement("title");
      element1.AppendChild((XmlNode) xmlDocument.CreateTextNode(text));
      xmlNode1.AppendChild((XmlNode) element1);
      XmlText textNode = xmlDocument.CreateTextNode("table, th, td { border: 1px solid black; }");
      XmlElement element2 = xmlDocument.CreateElement("style");
      element2.AppendChild((XmlNode) textNode);
      xmlNode1.AppendChild((XmlNode) element2);
      XmlElement element3 = xmlDocument.CreateElement("h1");
      element3.AppendChild((XmlNode) xmlDocument.CreateTextNode(text));
      xmlNode2.AppendChild((XmlNode) element3);
      XmlElement element4 = xmlDocument.CreateElement("p");
      element4.AppendChild((XmlNode) xmlDocument.CreateTextNode($"Дата создания: {now:f}"));
      xmlNode2.AppendChild((XmlNode) element4);
      XmlElement element5 = xmlDocument.CreateElement("p");
      element5.AppendChild((XmlNode) xmlDocument.CreateTextNode($"Идентификатор сессии копирования: {this._session.UniqueId}"));
      xmlNode2.AppendChild((XmlNode) element5);
      XmlElement element6 = xmlDocument.CreateElement("h2");
      element6.AppendChild((XmlNode) xmlDocument.CreateTextNode("Список работ для выполнения пользователем: "));
      xmlNode2.AppendChild((XmlNode) element6);
      XmlElement element7 = xmlDocument.CreateElement("table");
      XmlElement element8 = xmlDocument.CreateElement("tr");
      XmlElement element9 = xmlDocument.CreateElement("th");
      element9.AppendChild((XmlNode) xmlDocument.CreateTextNode("Описание требуемой работы"));
      element8.AppendChild((XmlNode) element9);
      element7.AppendChild((XmlNode) element8);
      foreach (UserWorkItem userWorkItem in this._session.UserWorkItems)
      {
        XmlElement element10 = xmlDocument.CreateElement("tr");
        XmlElement element11 = xmlDocument.CreateElement("td");
        element11.AppendChild((XmlNode) xmlDocument.CreateTextNode(userWorkItem.Text));
        element10.AppendChild((XmlNode) element11);
        element7.AppendChild((XmlNode) element10);
      }
      xmlNode2.AppendChild((XmlNode) element7);
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObjectCollection(objectType.ObjectTypeID).Create();
        dbObject.Attributes.AddAttribute(sessionKeeper.Session.IdentHelper.NameID, false).Value = (object) text;
        string uniqueFileName = ServiceUtils.GetService<IFileNamesService>((object) sessionKeeper.Session, true).GetUniqueFileName($"report_{this._session.UniqueId}.html", Math.Abs(dbObject.ObjectID), sessionKeeper.Session.SessionGUID);
        string tempFileName = Path.GetTempFileName();
        try
        {
          File.WriteAllText(tempFileName, xmlDocument.OuterXml, Encoding.UTF8);
          new FileInfo(tempFileName).LastAccessTime = now;
          UploadFileInfo[] items = new UploadFileInfo[1]
          {
            new UploadFileInfo(uniqueFileName, tempFileName)
          };
          new UploadFilesAction((IDBObjectRef) new DirectDBObjectRef(dbObject.ObjectID), (IList<UploadFileInfo>) items).Perform();
        }
        finally
        {
          FileUtils.DeleteFileSilently(tempFileName);
        }
        long num1 = Math.Abs(dbObject.ObjectID);
        dbObject.CommitCreation(true);
        if (num1 != -1L)
        {
          if (ApplicationServices.Container.GetService(typeof (INotificationService)) is INotificationService service)
            service.FireEvent((object) this, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", num1, objectType.ObjectTypeID));
          this.AddToWorkspace(num1, sessionKeeper.Session);
          int num2 = (int) MessageBox.Show("Отчет сохранен в базе данных IPS и помещен на рабочий стол пользователя", "Отчёт сохранён");
        }
      }
      this.EnableSaveButton = false;
    }
    finally
    {
      xmlDocument.RemoveAll();
    }
  }

  private void AddToWorkspace(long reportID, IUserSession session)
  {
    IDBRelationType relationType = session.GetRelationType(new Guid("cad0005e-306c-11d8-b4e9-00304f19f545"));
    session.GetRelationCollection(relationType.RelationType).Create(this.GetWorkspaceId(session), reportID);
  }

  private long GetWorkspaceId(IUserSession session)
  {
    return Convert.ToInt64(session.GetObjectCollection(session.IdentHelper.WorkspaceTypeID).Select(new DBRecordSetParams()
    {
      RecordCount = 1,
      Columns = new object[1]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID
      },
      Conditions = new ConditionStructure[1]
      {
        new ConditionStructure(-8, RelationalOperators.Equal, (object) session.UserID, LogicalOperators.NONE, 0, true)
      }
    }).Rows[0][0]);
  }
}
