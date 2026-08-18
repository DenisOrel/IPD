// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.MessagesView
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using Intermech.Navigator;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using Telerik.WinControls.Themes;
using Telerik.WinControls.UI;
using Telerik.WinForms.Documents.FormatProviders.Html;
using Telerik.WinForms.Documents.Model;

#nullable disable
namespace Intermech.Workflow.Design;

/// <summary>Summary description for MessagesView.</summary>
public class MessagesView : UserControl, Intermech.Navigator.Views.IView
{
  private IContainer components;
  private Windows8Theme windows8Theme1;
  private RadRichTextEditor radRichTextEditor;
  private long _objectID;
  private bool _oneMessageMode;
  private HtmlTemplates _tpl = new HtmlTemplates(Path.Combine(Holder.WorkflowTempPath, "templates\\mailmessages"));
  private bool _layoutLoaded;
  internal int ObjectTypeID = -1;

  public MessagesView()
  {
    this.InitializeComponent();
    this.radRichTextEditor.BackColor = Color.FromArgb(14211031);
    this.radRichTextEditor.HyperlinkToolTipFormatString = "Нажмите {1} для перехода по ссылке. (Адрес ссылки: {0})";
  }

  public MessagesView(bool oneMessageMode)
    : this()
  {
    this._oneMessageMode = oneMessageMode;
  }

  /// <summary>Clean up any resources being used.</summary>
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (MessagesView));
    this.radRichTextEditor = new RadRichTextEditor();
    this.windows8Theme1 = new Windows8Theme();
    this.radRichTextEditor.BeginInit();
    this.SuspendLayout();
    this.radRichTextEditor.AllowScaling = false;
    this.radRichTextEditor.BorderColor = Color.FromArgb(172, 172, 172);
    componentResourceManager.ApplyResources((object) this.radRichTextEditor, "radRichTextEditor");
    this.radRichTextEditor.EnableTheming = false;
    this.radRichTextEditor.IsContextMenuEnabled = false;
    this.radRichTextEditor.IsPasteOptionsPopupEnabled = false;
    this.radRichTextEditor.IsReadOnly = true;
    this.radRichTextEditor.IsSelectionMiniToolBarEnabled = false;
    this.radRichTextEditor.LayoutMode = new DocumentLayoutMode?(DocumentLayoutMode.Flow);
    this.radRichTextEditor.Name = "radRichTextEditor";
    this.radRichTextEditor.SelectionFill = Color.FromArgb(128 /*0x80*/, 78, 158, (int) byte.MaxValue);
    this.radRichTextEditor.SelectionStroke = Color.FromArgb(0, 0, 115, (int) byte.MaxValue);
    this.radRichTextEditor.ThemeName = "Windows8";
    this.radRichTextEditor.HyperlinkClicked += new EventHandler<HyperlinkClickedEventArgs>(this.radRichTextEditor1_HyperlinkClicked);
    this.Controls.Add((Control) this.radRichTextEditor);
    this.Name = nameof (MessagesView);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Tag = (object) "    ";
    this.radRichTextEditor.EndInit();
    this.ResumeLayout(false);
  }

  public int ImageIndex => Holder.MessagesImageIndex;

  public int OrderID => 0;

  public string Caption => LocalizationHolder.rm.GetString("Workflow.Design_63");

  public void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    this._objectID = (items.GetItemData(0, typeof (IDBObjectID)) as IDBObjectID).Value;
  }

  public void Deactivate(Intermech.Navigator.Views.IView nextView) => this._objectID = 0L;

  private string ImgHtml(int typeID, string cssclass)
  {
    string str = Holder.WorkflowTempPath + (typeID.ToString() + ".png");
    if (!File.Exists(str))
    {
      int index = BaseHolder.IconService.IndexOf(4, typeID);
      if (index == -1)
        return "";
      this._tpl.SaveImage(BaseHolder.IconService.ImageList, index, str);
    }
    return $"<img src=\"data:image/png;base64,{this.GetImageBase64(str)}\" class=\"{cssclass}\" />";
  }

  private string ImgHtml(string imgname, string cssclass)
  {
    string str = Holder.WorkflowTempPath + (imgname + ".png");
    if (!File.Exists(str))
    {
      int index = BaseHolder.NamedList.ImageIndex(imgname);
      if (index == -1)
        return "";
      this._tpl.SaveImage(BaseHolder.NamedList.ImageList, index, str);
    }
    return $"<img src=\"data:image/png;base64,{this.GetImageBase64(str)}\" class=\"{cssclass}\" />";
  }

  private string GetImageBase64(string imagePath)
  {
    using (Image image = Image.FromFile(imagePath))
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        image.Save((Stream) memoryStream, image.RawFormat);
        return Convert.ToBase64String(memoryStream.ToArray());
      }
    }
  }

  public void Activate(Intermech.Navigator.Views.IView previousView)
  {
    if (this._objectID == 0L)
      return;
    if (!this._layoutLoaded)
    {
      this._layoutLoaded = true;
      Point lLocation = new Point();
      Size lSize = new Size();
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      dictionary.Add("Template", "");
      FormStorage.LoadLayout((Control) this, (IDictionary) dictionary, true, out lLocation, out lSize);
      this._tpl.CurrentTemplateName = dictionary["Template"];
    }
    string str1 = "";
    string s1 = "";
    List<long> longList = (List<long>) null;
    IDBObject act = (IDBObject) null;
    Dictionary<long, object[]> rows = new Dictionary<long, object[]>();
    string str2 = "";
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      IDBAttribute histattr = (IDBAttribute) null;
      bool isMessage = false;
      if (!this._oneMessageMode)
      {
        act = session.GetObject(this._objectID, false);
        if (act == null)
        {
          if (Statics.IsApplicationClosing)
            return;
          throw new NotificationException(string.Format(LocalizationHolder.rm.GetString("Workflow.Design_ActivityNotFound"), (object) this._objectID));
        }
        isMessage = wfConsts.IsMessage(act.TypeID);
        if (isMessage)
        {
          IDBObject dbObject = act;
          long objectID = 0;
          IDBAttribute attributeById1 = dbObject.GetAttributeByID(wfConsts.AttrActivityID);
          if (attributeById1 != null)
            objectID = attributeById1.AsInteger;
          IDBAttribute attributeById2 = dbObject.GetAttributeByID(wfConsts.AttrSubjectID);
          if (attributeById2 != null)
            str1 = this._tpl.Current.GetVal("MessageTitlePrefix") + attributeById2.AsString;
          IDBAttribute attributeById3 = dbObject.GetAttributeByID(wfConsts.AttrActivityMessageID);
          if (attributeById3 != null)
          {
            s1 = attributeById3.AsString;
            s1 = $"<pre style=\"white-space: pre-wrap; white-space: -moz-pre-wrap; white-space: -pre-wrap; white-space: -o-pre-wrap; word-wrap: break-word;\">{this.AddEllipsisIfNeeded(s1, (MessageRow) null, this._objectID)}<pre>";
            s1 = this._tpl.Current.GetVal("MessageTextPrefix") + s1;
          }
          act = objectID == 0L || !wfConsts.IsWorkflowMessage(act.TypeID) ? (IDBObject) null : session.GetObject(objectID, false);
        }
        else
        {
          IDBAttribute attributeById = act.GetAttributeByID(wfConsts.AttrDescriptionID);
          if (attributeById != null)
          {
            string asString = attributeById.AsString;
            if (asString.Trim() != "")
            {
              str1 = LocalizationHolder.rm.GetString("ActivityDescription");
              s1 = asString;
            }
          }
        }
        if (act != null)
          histattr = act.GetAttributeByID(wfConsts.AttrExecHistoryID);
      }
      if (histattr != null || this._oneMessageMode)
        longList = MiscFunx.GetHistoryData(session, isMessage, histattr, rows, act, this._oneMessageMode, this.ObjectID, (long) this.ObjectTypeID);
      List<MessageRow> subrows = new List<MessageRow>();
      if (rows.Count > 0)
      {
        if (longList != null)
        {
          string curSiteGuid = (string) null;
          foreach (long num1 in longList)
          {
            if (rows.ContainsKey(num1) && rows[num1].Length >= 8)
            {
              subrows = MiscFunx.GetMessageRows(rows[num1], subrows, session, num1, ref curSiteGuid);
              foreach (MessageRow mr in subrows)
              {
                object[] data = mr.Data;
                string s2 = data[3].ToString();
                if (s2 != "")
                {
                  string str3 = "";
                  string str4 = "";
                  if (data[5] != DBNull.Value && data[5].ToString() != "")
                  {
                    ActivityResult int32 = (ActivityResult) Convert.ToInt32(data[5]);
                    str4 = int32 != ActivityResult.Back ? this.ImgHtml("wfNext", "ar") : this.ImgHtml("wfBack", "ar");
                    str3 += SimpleFuncs.GetEnumDescription((Enum) int32);
                  }
                  DateTime dateTime = DateTime.Now;
                  object obj1 = data[7];
                  object obj2 = data[2];
                  if (data.Length > 9 && data[9] != DBNull.Value)
                  {
                    obj1 = data[9];
                    obj2 = data[10];
                  }
                  long num2 = obj1.Equals((object) DBNull.Value) ? 0L : (long) Convert.ToInt32(obj1);
                  string str5 = !obj2.Equals((object) DBNull.Value) ? obj2.ToString() : LocalizationHolder.rm.GetString("Workflow.Design_64");
                  if (data[6] != DBNull.Value)
                    dateTime = Convert.ToDateTime(data[6], (IFormatProvider) CultureInfo.InvariantCulture);
                  if (mr.SrcSiteName != "")
                    str5 = $"{mr.SrcSiteName} / {str5}";
                  if (str3 == "")
                    str3 = LocalizationHolder.rm.GetString("Workflow.Design_65");
                  this._tpl.Assign("Activityid", (object) num1);
                  this._tpl.Assign("ActivityImage", (object) this.ImgHtml(Convert.ToInt32(data[4]), "a"));
                  string str6 = mr.RemoteProcessName;
                  if (str6 != "")
                    str6 = str6 == null ? mr.SrcSiteName + " / " : $"{mr.SrcSiteName} / {str6} / ";
                  this._tpl.Assign("ActivityName", (object) (str6 + data[1].ToString()));
                  this._tpl.Assign("ActivityResult", (object) str3);
                  this._tpl.Assign("ActivityResultImage", (object) str4);
                  this._tpl.Assign("Time", (object) dateTime.ToString());
                  this._tpl.Assign("Sender", (object) str5);
                  if (num2 == 0L)
                    num2 = wfConsts.SystemUserID;
                  this._tpl.Assign("SenderID", (object) num2);
                  this._tpl.Assign("Text", (object) this.AddEllipsisIfNeeded(s2, mr, num1));
                  str2 += this._tpl.Parse("messages");
                }
              }
            }
          }
        }
      }
    }
    if (s1 != "")
    {
      this._tpl.Assign("Title", (object) str1);
      this._tpl.Assign("Text", (object) s1);
      s1 = this._tpl.Parse("message");
    }
    if ((act != null || this._oneMessageMode) && str2 == "")
      str2 = this._tpl.Parse("nomessages");
    string path = Holder.WorkflowTempPath + "mailmsg.htm";
    if (this._oneMessageMode)
      this._tpl.Assign("HeaderText", (object) LocalizationHolder.rm.GetString("ActivityMessageHeader"));
    else if (act == null)
      this._tpl.Assign("HeaderText", (object) "");
    else
      this._tpl.Assign("HeaderText", (object) LocalizationHolder.rm.GetString("PrevActivitiesMessageHeader"));
    this._tpl.Assign("Message", (object) s1);
    this._tpl.Assign("Messages", (object) str2);
    string Html = this._tpl.Parse("index");
    if (this._tpl.Current.ImgBGColor == Color.Transparent)
      Html = ImageFuncs.AddTransparentPNGSupport(this.GetType().Assembly, Holder.WorkflowTempPath, Html);
    using (StreamWriter streamWriter = new StreamWriter(path))
      streamWriter.Write(Html);
    using (Stream input = (Stream) File.OpenRead(path))
      this.radRichTextEditor.Document = new HtmlFormatProvider().Import(input);
  }

  private long ConvertToObjectID(string s) => this.ConvertToObjectID(s, false);

  private long ConvertToObjectID(string s, bool silent)
  {
    long result = 0;
    if (long.TryParse(s, out result))
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (sessionKeeper.Session.GetObject(result, false) == null)
          result = 0L;
      }
    }
    else
    {
      Guid objectGUID = Guid.Empty;
      try
      {
        objectGUID = new Guid($"{{{s}}}");
      }
      catch
      {
      }
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(objectGUID, false);
        if (dbObject != null)
          return dbObject.ObjectID;
      }
    }
    if (!silent && result == 0L)
      wfFunx.SayError(string.Format(LocalizationHolder.GetString("ObjectNotFound"), (object) s));
    return result;
  }

  public long ObjectID
  {
    get => this._objectID;
    set
    {
      if (this._objectID == value)
        return;
      this._objectID = value;
      this.Activate((Intermech.Navigator.Views.IView) null);
    }
  }

  private string AddEllipsisIfNeeded(string s, MessageRow mr, long activityID)
  {
    if (s.Length == wfConsts.MaxStoredTextLength)
    {
      s = HtmlUtils.CloseTags(s);
      if (mr == null || mr.RemoteProcessName == "")
      {
        s += " &hellip; ";
        s += string.Format(LocalizationHolder.rm.GetString("Workflow.Design_67"), (object) activityID);
      }
    }
    s = HtmlUtils.CloseTags(s);
    s = HtmlUtils.nl2br(s);
    return s;
  }

  private void radRichTextEditor1_HyperlinkClicked(object sender, HyperlinkClickedEventArgs e)
  {
    string[] strArray = e.URL.Split('=');
    switch (strArray[0])
    {
      case "activity":
        wfFunx.ShowActivityProperties(Convert.ToInt64(strArray[1]));
        break;
      case "message":
        wfFunx.ShowActivityMessage(Convert.ToInt64(strArray[1]));
        break;
      case "object":
        long objectId1 = this.ConvertToObjectID(strArray[1]);
        if (objectId1 != 0L)
        {
          ServiceContainer viewServices = new ServiceContainer();
          viewServices.AddService(typeof (IViewState), (object) new ViewStateService());
          Utils.OpenNewWindow((IDescriptor) new Intermech.Navigator.DBObjects.Descriptor(objectId1), (System.IServiceProvider) viewServices);
          break;
        }
        break;
      case "process":
        long objectId2 = this.ConvertToObjectID(strArray[1]);
        if (objectId2 != 0L)
        {
          wfFunx.ViewProcess(objectId2);
          break;
        }
        break;
      case "view":
        long objectId3 = this.ConvertToObjectID(strArray[1]);
        if (objectId3 != 0L)
        {
          wfFunx.TryViewByNavigator(objectId3);
          break;
        }
        break;
      case "edit":
        long objectId4 = this.ConvertToObjectID(strArray[1]);
        if (objectId4 != 0L)
        {
          wfFunx.TryEditByNavigator(objectId4);
          break;
        }
        break;
    }
    e.Handled = false;
  }
}
