// Decompiled with JetBrains decompiler
// Type: Intermech.Forums.ForumView
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Bars;
using Intermech.DataFormats;
using Intermech.Extensions;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Workflow;
using Intermech.IO;
using Intermech.Navigator;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.Workflow.Design.Properties;
using mshtml;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Forums;

public class ForumView : UserControl, IView, ICommandTarget, ISelectedItemsHost
{
  /// <summary>
  /// 
  /// </summary>
  private long objectID;
  /// <summary>
  /// 
  /// </summary>
  private Guid objectGuid = Guid.Empty;
  /// <summary>
  /// 
  /// </summary>
  private long id;
  /// <summary>
  /// для какого типа показываем закладку
  /// (для Обсуждения часть функциональсности скроем)
  /// </summary>
  private int objTypeID = -1;
  /// <summary>Наименование объекта, для которого открыто обсуждение</summary>
  private string _objCaption = string.Empty;
  private bool firstEnter;
  /// <summary>guid текущего пользователя</summary>
  private Guid userGuid = Guid.Empty;
  /// <summary>id текущего пользователя</summary>
  private long userId;
  /// <summary>отображаемый форум</summary>
  private Forum forum;
  /// <summary>Количество непрочитанных пользователем сообщений</summary>
  private int unreadMessageCount;
  /// <summary>
  /// Какое сообщение выбираем по умолчанию
  /// Используется при переходе с другой вкладки
  /// </summary>
  private string _selectedMessageIdByDefault;
  /// <summary>способ сбора обсуждений, выбранный пользователем</summary>
  private ForumFormat ff = ForumFormat.None;
  private List<Panel> controls = new List<Panel>();
  /// <summary>
  /// Гуиды пользователей, которым надо отправить уведомление о том, что их упомянули в обсуждении
  /// </summary>
  private List<string> _usersGuidsForNotification = new List<string>();
  /// <summary>для фильтрации состава</summary>
  private IFiltrationService fSvc;
  private IViewsManagerService views;
  public System.IServiceProvider services;
  private IViewState state;
  private INotificationService notificationService;
  /// <summary>для указания цветов в таблице</summary>
  private UIColorsScheme colorsScheme;
  /// <summary>тэг начала ссылки</summary>
  private string objStartRef = "[ref=\"";
  /// <summary>тэг конца ссылки</summary>
  private string[] objEndRef = new string[1]{ "[/ref]" };
  /// <summary>тэг начала ссылки</summary>
  private string urlStartRef = "[url=\"";
  /// <summary>тэг конца ссылки</summary>
  private string[] urlEndRef = new string[1]{ "[/url]" };
  /// <summary>тэг начала ссылки</summary>
  private string citStart = "[cit";
  /// <summary>тэг конца ссылки</summary>
  private string citEnd = "[/cit]";
  private Regex RgxUrl = new Regex("[a-zA-Z0-9\\-\\.]+\\.[a-zA-Z]{2,3}(:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\\-\\._\\?\\,\\'/\\\\\\+&%\\$#\\=~])*");
  private IHTMLTxtRange lastRange;
  private ForumView.SearchFlags searchFlags;
  /// <summary>новый поиск</summary>
  private bool isNewText = true;
  private SortOrder order = SortOrder.Descending;
  private SortField field;
  /// <summary>печатать весь документ?</summary>
  private bool printWholeDocument;
  private UserMessageSelectedItems selectedItems;
  /// <summary>тэг начала полужирного шрифта</summary>
  private string boldSt = "[b]";
  /// <summary>тэг окончания полужирного шрифта</summary>
  private string boldEnd = "[/b]";
  /// <summary>тэг начала текста курсивом</summary>
  private string italicSt = "[i]";
  /// <summary>тэг окончания текста курсивом</summary>
  private string italicEnd = "[/i]";
  /// <summary>тэг начала подчёркнутого текста</summary>
  private string underlineSt = "[u]";
  /// <summary>тэг окончания подчёркнутого текста</summary>
  private string underlineEnd = "[/u]";
  /// <summary>тэг начала зачёркнутого текста</summary>
  private string strikeSt = "[s]";
  /// <summary>тэг окончания зачёркнутого текста</summary>
  private string strikeEnd = "[/s]";
  /// <summary>тэг начало изменения цвета шрифта</summary>
  private string colorSt = "[color:";
  /// <summary>тэг окончания изменение цвета шрифта</summary>
  private string colorEnd = "[/color]";
  /// <summary>тэг начала изменения цвета фона</summary>
  private string backSt = "[background-color:";
  /// <summary>тэг окончания изменения цвета фона</summary>
  private string backEnd = "[/background-color]";
  /// <summary>тэг начала изменения размера шрифта</summary>
  private string sizeSt = "[font-size:";
  /// <summary>тэг окончания изменения размера шрифта</summary>
  private string sizeEnd = "[/font-size]";
  private UserMessage changedMessage;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Intermech.Bars.ToolBar tbOptions;
  private Panel pMessage;
  private TextBox tbCaption;
  private Panel pForum;
  private Button btnSend;
  private Button btnObjRef;
  private Button btnWebRef;
  private Button btnCitation;
  private ImageList ilForums;
  private ToolTip ttForum;
  private ComboBoxItem cbSort;
  private ButtonItem btnPrint;
  private ComboBoxItem cbForumFormat;
  private WebBrowser wbForum;
  private TextBox tbCurrentMessage;
  private Button btnMessage;
  private ButtonItem btnSort;
  private Panel pSearch;
  private TextBox tbSearch;
  private Label label1;
  private Button btnBackward;
  private Button btnForward;
  private CheckBox cbRegister;
  private Panel panel1;
  private Splitter splitter1;
  private ContextMenuStrip cmsBrowser;
  private ToolStripMenuItem CopyText;
  private WebBrowser wbPrint;
  private ButtonItem btnPrintView;
  private ComboBoxItem cbPrintMode;
  private Button btnImage;
  private OpenFileDialog ofdImages;
  private Label label2;
  private ToolStripMenuItem tsView;
  private ToolStripMenuItem tsPrint;
  private ToolStripSeparator toolStripSeparator1;
  private Button btnUnderline;
  private Button btnItalic;
  private Button btnBold;
  private FontDialog fontDialog1;
  private Button btnStrike;
  private ComboBox cbSize;
  private Button btnBackColor;
  private Button btnFontColor;
  private ColorDialog colorDialog1;
  private Button btnCancel;
  private Button btnChange;
  private ButtonItem btnUpdate;
  private Button btnAddImageFromClipboard;
  private Button btnMentionUsers;

  public ForumView()
  {
    this.InitializeComponent();
    if (ApplicationServices.Container.GetService(typeof (BarManager)) is BarManager service)
    {
      service.RendererChanged += new EventHandler(this.ToolbarRendererChanged);
      this.ToolbarRendererChanged((object) service, EventArgs.Empty);
    }
    this.cbSort.ComboBox.SelectedIndexChanged += new EventHandler(this.SortComboBox_SelectedIndexChanged);
    this.cbForumFormat.ComboBox.SelectedIndexChanged += new EventHandler(this.FFComboBox_SelectedIndexChanged);
    this.cbPrintMode.ComboBox.SelectedIndexChanged += new EventHandler(this.cbPrintMode_SelectedIndexChanged);
    this.cbSize.SelectedIndex = 0;
    this.cbSize.SelectedIndexChanged += new EventHandler(this.cbSize_SelectedIndexChanged);
  }

  /// <summary>Выполняет инициализацию закладки после ее создания.</summary>
  /// <param name="items">Коллекция выбранных пользователем элементов навигации.</param>
  /// <param name="provider">Контейнер сервисов, которыми может пользоваться закладка.</param>
  public void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    IDBTypedObjectID itemData = items.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    this.objectID = itemData.ObjectID;
    this.objTypeID = itemData.ObjectType;
    this.id = itemData.ID;
    this._objCaption = itemData.Caption;
    this.notificationService = ServicesManager.GetService(typeof (INotificationService)) as INotificationService;
    if (this.notificationService != null)
    {
      this.notificationService.Unsubscribe("NavigatorWindowOpened", new NotificationEventHandler(this.AfterWindowOpenEvent));
      this.notificationService.Subscribe("NavigatorWindowOpened", new NotificationEventHandler(this.AfterWindowOpenEvent));
    }
    this.firstEnter = true;
    this.ff = this.objTypeID == ForumsConsts.forumObjectTypeID ? ForumFormat.None : ForumFormat.Version;
    this.views = ApplicationServices.Container.GetService(typeof (IViewsManagerService)) as IViewsManagerService;
    this.fSvc = ApplicationServices.Container.GetService(typeof (IFiltrationService)) as IFiltrationService;
    this.services = provider;
    if (ApplicationServices.Container.GetService(typeof (INamedImageList)) is INamedImageList service1)
    {
      this.btnPrintView.Image = service1.ImageList.Images[service1.ImageIndex("imgPrintPreview")];
      this.btnPrint.Image = service1.ImageList.Images[service1.ImageIndex("imgPrint")];
    }
    this.selectedItems = new UserMessageSelectedItems(items.GetParentPath(0), this.services);
    this.state = this.services.GetService(typeof (IViewState)) as IViewState;
    this.colorsScheme = (ApplicationServices.Container.GetService(typeof (INavGraphicsCache)) as INavGraphicsCache).CurrentColorsScheme;
    this.cbForumFormat.Items.Clear();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ICurrentUserAndRole service2 = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
      this.userGuid = service2.UserGuid;
      this.userId = service2.UserID;
      this.forum = !(sessionKeeper.Session.GetCustomService(typeof (IForumsService)) is IForumsService customService) ? (Forum) null : customService.GenerationForum(this.objectID, this.id, this.ff, this.fSvc.FiltrationServiceOwnerID, sessionKeeper.Session.SessionGUID);
      this.unreadMessageCount = this.CountUnreadMessage();
    }
  }

  private void AfterWindowOpenEvent(object sender, NotificationEventArgs e)
  {
    if (!(e is NavigatorWindowOpenedEventArgs windowOpenedEventArgs) || windowOpenedEventArgs.ServiceProvider == null || !(windowOpenedEventArgs.ServiceProvider.GetService(typeof (IMessageForCheckingService)) is IMessageForCheckingService service))
      return;
    this._selectedMessageIdByDefault = service.MessageId();
  }

  /// <summary>
  /// Посчитать количество сообщений, непросмотренных текущим юзером.
  /// </summary>
  /// <returns></returns>
  private int CountUnreadMessage()
  {
    int num = 0;
    foreach (UserMessage userMessage in (List<UserMessage>) this.forum)
    {
      if (userMessage.ReadByUsers.Count != 0 && !userMessage.ReadByUsers.Contains(this.userGuid.ToString()))
        ++num;
    }
    return num;
  }

  /// <summary>
  /// Уведомляет закладку о том, что она стала видима на экране. Этот метод вызывается при
  /// первом показе закладки, а также при переключении на нее с другой закладки.
  /// </summary>
  /// <param name="previousView">
  /// Закладка, с которой осуществляется переключение. Может быть null для самой первой
  /// показываемой на экране закладки.
  /// </param>
  public void Activate(IView previousView)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject1 = sessionKeeper.Session.GetObject(this.objectID, false);
      this.objectGuid = dbObject1.ObjectGUID;
      (dbObject1 as IDBSecurity).CheckAccess(ActionType.View, true, true);
      if (!this.firstEnter)
        return;
      this.cbForumFormat.Items.Add((object) LocalizationHolder.rm.GetString("Workflow.Design_160"));
      this.cbForumFormat.Items.Add((object) LocalizationHolder.rm.GetString("Workflow.Design_161"));
      this.cbForumFormat.Items.Add((object) LocalizationHolder.rm.GetString("Workflow.Design_162"));
      this.cbForumFormat.Items.Add((object) LocalizationHolder.rm.GetString("Workflow.Design_163"));
      if (dbObject1.ModificationID != 0L)
      {
        IDBObject dbObject2 = sessionKeeper.Session.GetObject(dbObject1.ModificationID, false);
        if (dbObject2 != null)
          this.cbForumFormat.ComboBox.Items.Add((object) string.Format(LocalizationHolder.rm.GetString("Workflow.Design_164"), (object) dbObject2.Caption));
      }
      if (sessionKeeper.Session.GetCustomService(typeof (IForumsService)) is IForumsService customService)
      {
        foreach (UserMessage userMessage in (List<UserMessage>) this.forum)
        {
          userMessage.ReadByUsers.SafeAdd<string>(this.userGuid.ToString());
          customService.ChangeMessage(this.forum, new Guid(userMessage.DicsObjectGuid), sessionKeeper.Session.SessionGUID, false);
        }
      }
      this.unreadMessageCount = 0;
      this.Parent.Text = this.Caption;
      this.LoadMessages();
      this.Init();
      this.firstEnter = false;
    }
  }

  /// <summary>
  /// Уведомляет закладку о том, что она перестала быть видима на экране. Этот метод
  /// вызывается при переключении на другую закладку, а также удалении всех закладок.
  /// </summary>
  /// <param name="nextView">
  /// Закладка, на которую осуществляется переключение. Может быть null, если выполяется
  /// не переключение, а удаление закладок.
  /// </param>
  public void Deactivate(IView nextView)
  {
    if (this.notificationService == null)
      return;
    this.notificationService.Unsubscribe("NavigatorWindowOpened", new NotificationEventHandler(this.AfterWindowOpenEvent));
  }

  /// <summary>
  /// Возвращает название закладки, которое будет отображаться на экране. Навигатор
  /// получает значение этого свойства после того, как закладка будет проинициализирована
  /// в методе Initialize.
  /// </summary>
  public string Caption
  {
    get
    {
      return this.unreadMessageCount > 0 ? LocalizationHolder.rm.GetString("Discussion") + $" ({this.unreadMessageCount})" : LocalizationHolder.rm.GetString("Discussion");
    }
  }

  /// <summary>
  /// Возвращает индекс иконки, которая будет отображаться на экране,
  /// в именованном списке иконок. Навигатор получает значение этого свойства после того,
  /// как закладка будет проинициализирована в методе Initialize.
  /// </summary>
  public int ImageIndex => BaseHolder.NamedList.ImageIndex("forum");

  /// <summary>
  /// Возвращает индекс расположения закладки среди других закладок
  /// при выводе на экран. Навигатор сортирует отображаемые закладки в
  /// порядке возрастания этого значения. Значение этого свойства
  /// навигатор получает после того, как закладка будет проинициализирована в
  /// методе Initialize.
  /// </summary>
  public int OrderID
  {
    get
    {
      return this.ff == ForumFormat.None && (this.state.ViewState & ViewStateFlags.InDialog) == ViewStateFlags.InDialog && (this.state.ViewState & ViewStateFlags.InParametersCard) == ViewStateFlags.None ? -1 : 55;
    }
  }

  /// <summary>
  /// Пришло событие "Изменился рендерер панелей инструментов"
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void ToolbarRendererChanged(object sender, EventArgs e)
  {
    this.tbOptions.Renderer = (sender as BarManager).Renderer;
  }

  public void Init()
  {
    if (this.objTypeID == ForumsConsts.forumObjectTypeID)
      this.cbForumFormat.Items.Add((object) LocalizationHolder.rm.GetString("Workflow.Design_165"));
    this.panel1.Visible = this.objTypeID != ForumsConsts.forumObjectTypeID;
    this.cbForumFormat.Enabled = this.objTypeID != ForumsConsts.forumObjectTypeID;
    this.cbSort.ComboBox.SelectedIndex = 0;
    this.cbForumFormat.ComboBox.SelectedIndex = 0;
    this.cbPrintMode.ComboBox.SelectedIndex = 0;
    this.order = SortOrder.Descending;
    this.field = SortField.Date;
    this.btnSort.ImageIndex = 4;
    this.tbCurrentMessage.Clear();
  }

  /// <summary>Загрузить сформированный форум</summary>
  private void LoadMessages()
  {
    this.cbForumFormat.Locked = true;
    this.SortMessages();
    this.selectedItems.Ivalidate();
    do
    {
      Application.DoEvents();
    }
    while (this.wbForum.ReadyState != WebBrowserReadyState.Complete && this.wbForum.ReadyState != WebBrowserReadyState.Uninitialized);
    this.wbForum.Navigate("about:blank");
    HtmlDocument document = this.wbForum.Document;
    document.Write("<HTML><BODY></BODY></HTML>");
    HtmlElement element1 = document.CreateElement("table");
    element1.Style = $"width: 100%; height: auto; background-color:{ColorTranslator.ToHtml(this.colorsScheme.ForumMessageBkColor)};font: messagebox;border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;border-right: black 1px solid;";
    element1.Id = "forum_table";
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (this.forum != null)
      {
        for (int index = 0; index < this.forum.Count; ++index)
        {
          int num1 = 1;
          UserMessage userMessage = this.forum[index];
          HtmlElement element2 = document.CreateElement("tr");
          element2.Id = $"{userMessage.DicsObjectGuid};{userMessage.GetHashCode()}";
          HtmlElement element3 = document.CreateElement("td");
          element3.Style = "width: 16px;";
          HtmlElement element4 = document.CreateElement("input");
          element4.SetAttribute("type", "checkbox");
          element4.Id = $"{userMessage.DicsObjectGuid};{userMessage.GetHashCode()}";
          element3.AppendChild(element4);
          HtmlElement element5 = document.CreateElement("td");
          HtmlElement element6 = document.CreateElement("table");
          element6.Style = "left: 35px; width: 100%; top: 15px; height: 100%;font: messagebox;\r\n                    border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;border-right: black 1px solid;";
          HtmlElement element7 = document.CreateElement("tr");
          element7.Style = $"height: 10px; background-color: {ColorTranslator.ToHtml(this.colorsScheme.ForumCaptionBkColor)};color: {ColorTranslator.ToHtml(this.colorsScheme.ForumCaptionColor)}";
          HtmlElement element8 = document.CreateElement("td");
          IDBObject dbObject1 = GuidHelper.IsGuid(userMessage.UserGuid) ? sessionKeeper.Session.GetObject(new Guid(userMessage.UserGuid), false) : (IDBObject) null;
          string str1 = dbObject1 != null ? dbObject1.Caption : string.Format(LocalizationHolder.rm.GetString("Workflow.Design_166"), (object) userMessage.UserGuid);
          element8.InnerHtml = $"<b>{str1}<br/>{userMessage.Date.ToLocalTime()}</b>";
          element7.AppendChild(element8);
          IDBObject dbObject2 = GuidHelper.IsGuid(userMessage.SiteGuid) ? sessionKeeper.Session.GetObject(new Guid(userMessage.SiteGuid), false) : (IDBObject) null;
          if (dbObject2 != null)
          {
            HtmlElement element9 = document.CreateElement("td");
            element9.InnerHtml = $"<b>{dbObject2.Caption}</b>";
            element9.Style = "text-align:right;width:20%";
            element7.AppendChild(element9);
            ++num1;
          }
          if (this.ff != ForumFormat.None)
          {
            if (userMessage.UserGuid == this.userGuid.ToString())
            {
              HtmlElement element10 = document.CreateElement("td");
              element10.InnerHtml = string.Format(LocalizationHolder.rm.GetString("Workflow.Design_167"), (object) userMessage.DicsObjectGuid, (object) userMessage.GetHashCode());
              element10.Style = "text-align:right;width:10%";
              element7.AppendChild(element10);
              int num2 = num1 + 1;
              HtmlElement element11 = document.CreateElement("td");
              element11.InnerHtml = string.Format(LocalizationHolder.rm.GetString("Workflow.Design_168"), (object) userMessage.DicsObjectGuid, (object) userMessage.GetHashCode());
              element11.Style = "text-align:right;width:10%";
              element7.AppendChild(element11);
              num1 = num2 + 1;
            }
            HtmlElement element12 = document.CreateElement("td");
            element12.InnerHtml = string.Format(LocalizationHolder.rm.GetString("Workflow.Design_169"), (object) userMessage.DicsObjectGuid, (object) userMessage.GetHashCode());
            element12.Style = "text-align:right;width:10%";
            element7.AppendChild(element12);
            ++num1;
          }
          HtmlElement element13 = document.CreateElement("tr");
          HtmlElement element14 = document.CreateElement("td");
          element14.SetAttribute("colspan", num1.ToString());
          element14.Style = "color: " + ColorTranslator.ToHtml(this.colorsScheme.ForumMessageColor);
          string parseString = this.SetStyleTags(this.SetStyleTags(this.SetStyleTags(userMessage.Message.Replace("<", "&lt;").Replace(">", "&gt;").Replace("\n", "<br/>").Replace(this.boldSt, "<b>").Replace(this.boldEnd, "</b>").Replace(this.italicSt, "<i>").Replace(this.italicEnd, "</i>").Replace(this.underlineSt, "<u>").Replace(this.underlineEnd, "</u>").Replace(this.strikeSt, "<s>").Replace(this.strikeEnd, "</s>"), this.colorSt, this.colorEnd), this.backSt, this.backEnd), this.sizeSt, this.sizeEnd);
          element14.InnerHtml = userMessage.Caption + "<br />";
          this.CitationParseMessage(parseString, element14, document);
          element13.AppendChild(element14);
          HtmlElement newElement = (HtmlElement) null;
          if (userMessage.ModifyDate != DateTime.MinValue)
          {
            newElement = document.CreateElement("tr");
            HtmlElement element15 = document.CreateElement("td");
            element15.Style = "color: " + ColorTranslator.ToHtml(this.colorsScheme.ForumMessageColor);
            element15.InnerHtml = string.Format(LocalizationHolder.rm.GetString("Workflow.Design_170"), (object) userMessage.ModifyDate.ToLocalTime());
            element15.Style = "text-align:left;";
            newElement.AppendChild(element15);
            --num1;
          }
          if (this.ff != ForumFormat.None)
          {
            QuickObjectInfo quickObjectInfo1;
            if (!GuidHelper.IsGuid(userMessage.DiscussedObjectGuid))
              quickObjectInfo1 = new QuickObjectInfo()
              {
                ObjectTypeID = -1
              };
            else
              quickObjectInfo1 = sessionKeeper.Session.GetObjectInfo(new Guid(userMessage.DiscussedObjectGuid));
            QuickObjectInfo quickObjectInfo2 = quickObjectInfo1;
            IDBObject dbObject3 = GuidHelper.IsGuid(userMessage.DiscussedObjectGuid) ? sessionKeeper.Session.GetObject(new Guid(userMessage.DiscussedObjectGuid), false) : (IDBObject) null;
            string str2;
            if (dbObject3 != null)
            {
              str2 = dbObject3.Caption;
              if (dbObject3.VersionID > 0)
                str2 = $"{str2} [{(object) dbObject3.VersionID}]";
            }
            else
              str2 = string.Format(LocalizationHolder.rm.GetString("Workflow.Design_171"), (object) userMessage.DiscussedObjectGuid);
            if (quickObjectInfo2.Empty || Math.Abs(quickObjectInfo2.ObjectID) != Math.Abs(this.objectID))
            {
              newElement = newElement == (HtmlElement) null ? document.CreateElement("tr") : newElement;
              HtmlElement element16 = document.CreateElement("td");
              element16.Style = "color: " + ColorTranslator.ToHtml(this.colorsScheme.ForumMessageColor);
              element16.Style += ";text-align:right";
              element16.SetAttribute("colspan", num1.ToString());
              element16.InnerHtml = $"<a href=\"#object={userMessage.DiscussedObjectGuid}\"; title=\"{LocalizationHolder.rm.GetString("Workflow.Design_172")}\">{str2}</a>";
              newElement.AppendChild(element16);
            }
          }
          element6.AppendChild(element7);
          element6.AppendChild(element13);
          if (newElement != (HtmlElement) null)
            element6.AppendChild(newElement);
          element5.AppendChild(element6);
          element2.AppendChild(element3);
          element2.AppendChild(element5);
          element1.AppendChild(element2);
        }
      }
    }
    document.Body.AppendChild(element1);
    this.wbForum.DocumentText = document.GetElementsByTagName("HTML")[0].OuterHtml;
    this.cbForumFormat.Locked = false;
    this.isNewText = true;
  }

  /// <summary>проставить тэги для изменения стиля</summary>
  /// <param name="text">текст, в котором ищем тэги</param>
  /// <param name="startTag">тэг начала изменения стиля</param>
  /// <param name="endTag">тэг окончания изменения стиля</param>
  /// <returns></returns>
  private string SetStyleTags(string text, string startTag, string endTag)
  {
    int startIndex1 = 0;
    while (startIndex1 <= text.Length && text.IndexOf(startTag, startIndex1) > -1)
    {
      int startIndex2 = text.IndexOf(startTag);
      int num1 = text.IndexOf(endTag, startIndex2);
      int num2 = text.IndexOf("]", startIndex2);
      if (num2 >= num1)
      {
        startIndex1 = num1;
      }
      else
      {
        string newValue = $"<span style=\"{text.Substring(startIndex2 + 1, num2 - startIndex2 - 1)}\">";
        text = text.Replace(text.Substring(startIndex2, num2 - startIndex2 + 1), newValue);
        startIndex1 = startIndex2 + newValue.Length;
      }
    }
    text = text.Replace(endTag, "</span>");
    return text;
  }

  /// <summary>изменили способ сбора обсуждений</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void FFComboBox_SelectedIndexChanged(object sender, EventArgs e) => this.ReloadForum();

  private void ReloadForum()
  {
    if (this.ff != ForumFormat.None)
      this.ff = (ForumFormat) Enum.GetValues(typeof (ForumFormat)).GetValue(this.cbForumFormat.ComboBox.SelectedIndex);
    if (this.firstEnter)
      return;
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        this.forum = !(sessionKeeper.Session.GetCustomService(typeof (IForumsService)) is IForumsService customService) ? (Forum) null : customService.GenerationForum(this.objectID, this.id, this.ff, this.fSvc.FiltrationServiceOwnerID, sessionKeeper.Session.SessionGUID);
      this.LoadMessages();
    }
    catch
    {
    }
  }

  /// <summary>добавить сообщение</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnSend_Click(object sender, EventArgs e)
  {
    UserMessage message = new UserMessage();
    message.Caption = this.tbCaption.Text;
    message.Date = DateTime.UtcNow;
    message.Message = this.tbCurrentMessage.Text;
    message.UserGuid = this.userGuid.ToString();
    message.DiscussedObjectGuid = this.objectGuid.ToString();
    message.ReadByUsers.Add(this.userGuid.ToString());
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (sessionKeeper.Session.GetCustomService(typeof (IForumsService)) is IForumsService customService)
        customService.AddMessageToDiscussion(message, this.objectID, this.id, ref this.forum, sessionKeeper.Session.SessionGUID);
      this.LoadMessages();
      this.NotifyMentionedUsers();
    }
    this.tbCaption.Text = string.Empty;
    this.tbCurrentMessage.Text = string.Empty;
  }

  /// <summary>
  /// Отправление указанным в тексте пользователям сообщения
  /// </summary>
  private void SendNotificationsToUsers()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      string objectInstanceName = sessionKeeper.Session.GetObjectType(this.objTypeID).ObjectInstanceName;
      IRouterService customService = sessionKeeper.Session.GetCustomService(typeof (IRouterService)) as IRouterService;
      string Subject = LocalizationHolder.rm.GetString("Interfaces.Workflow_MentionInDiscussion");
      string Text = string.Format(LocalizationHolder.rm.GetString("Interfaces.Workflow_ObjectReference"), (object) this.objectGuid.ToString(), (object) objectInstanceName, (object) this._objCaption);
      foreach (string g in this._usersGuidsForNotification)
      {
        long objectId = sessionKeeper.Session.GetObject(new Guid(g)).ObjectID;
        customService.CreateMessage(sessionKeeper.Session.SessionGUID, objectId, Subject, Text, sessionKeeper.Session.UserID);
      }
    }
  }

  /// <summary>
  /// Проверка списка пользователей на тему того, не удалили ли кого-то добавленного через кнопку руками
  /// </summary>
  private void CheckUsersReferenceInText()
  {
    foreach (string str in new List<string>((IEnumerable<string>) this._usersGuidsForNotification))
    {
      if (!this.tbCurrentMessage.Text.Contains(str))
        this._usersGuidsForNotification.Remove(str);
    }
  }

  private void ClearUsersList() => this._usersGuidsForNotification.Clear();

  private void tbCurrentMessage_TextChanged(object sender, EventArgs e)
  {
    this.btnSend.Enabled = this.tbCurrentMessage.Text.Length != 0;
  }

  /// <summary>оформляем цитаты</summary>
  /// <param name="parseString"></param>
  /// <returns></returns>
  private void CitationParseMessage(string parseString, HtmlElement parent, HtmlDocument doc)
  {
    int length = parseString.IndexOf(this.citStart);
    int num1 = 1;
    int num2 = -1;
    int num3 = length;
    int startIndex1 = 0;
    string str1 = parseString;
    while (num1 != 0)
    {
      int num4 = str1.IndexOf(this.citEnd, startIndex1);
      if (num4 < 0)
      {
        this.UrlParse(parseString, parent, doc);
        return;
      }
      --num1;
      int startIndex2 = num3 + 1;
      num3 = str1.IndexOf(this.citStart, startIndex2);
      if (num3 != -1 && num3 < num4)
      {
        ++num1;
        startIndex1 = num4 + 1;
      }
      else
      {
        str1 = parseString.Substring(length + 5, num4 - length - 5);
        num2 = num4;
        startIndex1 = 0;
      }
    }
    if (length > -1 && num2 > -1 && length < num2)
    {
      this.UrlParse(parseString.Substring(0, length), parent, doc);
      string parseString1 = string.Empty;
      string str2 = LocalizationHolder.rm.GetString("Workflow.Design_173");
      if (parseString[length + this.citStart.Length] == ']')
        parseString1 = parseString.Substring(length + this.citStart.Length + 1, num2 - (length + this.citStart.Length + 1));
      else if (length + this.citStart.Length + 2 + 36 <= num2)
      {
        string str3 = parseString.Substring(length + this.citStart.Length + 2, 36);
        parseString1 = parseString.Substring(length + this.citStart.Length + 4 + 36, num2 - (length + this.citStart.Length + 4 + 36));
        if (GuidHelper.IsGuid(str3))
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            IDBObject dbObject = GuidHelper.IsGuid(str3) ? sessionKeeper.Session.GetObject(new Guid(str3), false) : (IDBObject) null;
            str2 = dbObject != null ? string.Format(LocalizationHolder.rm.GetString("Workflow.Design_174"), (object) dbObject.Caption) : string.Format(LocalizationHolder.rm.GetString("Workflow.Design_175"), (object) str3);
          }
        }
      }
      HtmlElement element1 = doc.CreateElement("table");
      element1.Style = "left: 5%; position: relative; top: 0px; border-right: black 1px solid;border-top: black 1px solid; border-left: black 1px solid; border-bottom:black 1px solid; width: 95%;font: messagebox;";
      HtmlElement element2 = doc.CreateElement("tr");
      HtmlElement element3 = doc.CreateElement("td");
      element3.Style = $"height: 10px;background-color: {ColorTranslator.ToHtml(this.colorsScheme.ForumCaptionBkColor)};color: {ColorTranslator.ToHtml(this.colorsScheme.ForumCaptionColor)}";
      element3.InnerHtml = $"<b>{str2}</b>";
      element2.AppendChild(element3);
      element1.AppendChild(element2);
      HtmlElement element4 = doc.CreateElement("tr");
      HtmlElement element5 = doc.CreateElement("td");
      element5.Style = "color: " + ColorTranslator.ToHtml(this.colorsScheme.ForumMessageColor);
      this.CitationParseMessage(parseString1, element5, doc);
      element4.AppendChild(element5);
      element1.AppendChild(element4);
      parent.AppendChild(element1);
      this.CitationParseMessage(parseString.Substring(num2 + 6), parent, doc);
    }
    else
      this.UrlParse(parseString, parent, doc);
  }

  /// <summary>оформляем ссылки</summary>
  /// <param name="message"></param>
  /// <param name="parent"></param>
  /// <param name="doc"></param>
  private void RefParseMessage(string message, HtmlElement parent, HtmlDocument doc)
  {
    foreach (string str1 in message.Split(this.objEndRef, StringSplitOptions.None))
    {
      int length = str1.IndexOf(this.objStartRef);
      if (length > -1)
      {
        parent.InnerHtml += str1.Substring(0, length);
        string[] strArray1 = str1.Substring(length + this.objStartRef.Length).Split(new string[1]
        {
          "\"]"
        }, StringSplitOptions.None);
        if (strArray1.Length != 2)
        {
          parent.InnerHtml += str1;
        }
        else
        {
          string str2 = strArray1[0];
          string str3 = strArray1[1];
          string[] strArray2 = str2.Split(new string[1]
          {
            ";"
          }, StringSplitOptions.RemoveEmptyEntries);
          if (strArray2.Length != 0 && GuidHelper.IsGuid(strArray2[0]))
          {
            string str4 = strArray2[0];
            if (strArray2.Length == 1)
            {
              HtmlElement element = doc.CreateElement("a");
              string str5 = $"#object={str4}";
              string str6 = LocalizationHolder.rm.GetString("Workflow.Design_176");
              element.SetAttribute("href", str5);
              element.SetAttribute("title", str6);
              element.InnerText = str3;
              parent.AppendChild(element);
            }
            else if (strArray2.Length == 2)
            {
              HtmlElement element = doc.CreateElement("a");
              string str7 = $"#message={strArray2[0]};{strArray2[1]}";
              string str8 = LocalizationHolder.rm.GetString("Workflow.Design_177");
              element.SetAttribute("href", str7);
              element.SetAttribute("title", str8);
              element.InnerText = str3;
              parent.AppendChild(element);
            }
            else if (strArray2.Length == 3)
            {
              HtmlElement element = doc.CreateElement("img");
              string publishImagePath = this.GetPublishImagePath(strArray2[0], strArray2[1]);
              element.SetAttribute("src", publishImagePath);
              element.SetAttribute("alt", strArray2[2]);
              parent.AppendChild(element);
            }
            else
              parent.InnerHtml += str3;
          }
          else
            parent.InnerHtml += str3;
        }
      }
      else
        parent.InnerHtml += str1;
    }
  }

  private void UrlParse(string message, HtmlElement parent, HtmlDocument doc)
  {
    foreach (string message1 in message.Split(this.urlEndRef, StringSplitOptions.None))
    {
      int length = message1.IndexOf(this.urlStartRef);
      if (length > -1)
      {
        this.RefParseMessage(message1.Substring(0, length), parent, doc);
        string message2 = message1.Substring(length + this.urlStartRef.Length);
        string[] strArray = message2.Split(new string[1]
        {
          "\"]"
        }, StringSplitOptions.None);
        if (strArray.Length != 2)
        {
          this.RefParseMessage(message1.Substring(0, length), parent, doc);
        }
        else
        {
          string input = strArray[0];
          string str1 = strArray[1];
          if (this.RgxUrl.IsMatch(input))
          {
            HtmlElement element = doc.CreateElement("a");
            string str2 = $"#web={input}";
            string str3 = LocalizationHolder.rm.GetString("Workflow.Design_178");
            element.SetAttribute("href", str2);
            element.SetAttribute("title", str3);
            element.InnerText = str1;
            parent.AppendChild(element);
          }
          else
            this.RefParseMessage(message2, parent, doc);
        }
      }
      else
        this.RefParseMessage(message1, parent, doc);
    }
  }

  /// <summary>ответ на сообщения</summary>
  /// <param name="details">цитируемое сообщение</param>
  private void CitationMessage(string details)
  {
    if (this.btnChange.Visible)
    {
      this.btnCancel.Visible = this.btnChange.Visible = false;
      this.btnSend.Visible = true;
      this.tbCurrentMessage.Text = string.Empty;
    }
    string[] strArray = details.Split(';');
    if (strArray.Length != 2)
      return;
    string str = strArray[0];
    UserMessage message = this.forum.FindMessage(strArray[1]);
    if (message == null)
      return;
    this.tbCurrentMessage.Text += $"{this.citStart}=\"{message.UserGuid}\"]{message.Message}{this.citEnd}";
    this.tbCurrentMessage.Focus();
    this.tbCurrentMessage.SelectionStart = this.tbCurrentMessage.Text.Length;
    this.tbCurrentMessage.SelectionLength = 0;
  }

  /// <summary>вставка ссылки на объект ips</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnObjRef_Click(object sender, EventArgs e)
  {
    Intermech.Navigator.SelectionWindow.RegisterAnalyze((ISelectedItemsAnalyzer) new SelectedOpenInNewWindowAnalizer(), true);
    long[] numArray = Intermech.Navigator.SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("Workflow.Design_179"), LocalizationHolder.rm.GetString("Workflow.Design_180"), SelectionOptions.SelectObjects | SelectionOptions.ForceRebuildNavTree);
    if (numArray == null || numArray.Length == 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (long objectID in numArray)
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(objectID, false);
        if (dbObject != null)
        {
          int selectionStart = this.tbCurrentMessage.SelectionStart;
          if (this.tbCurrentMessage.SelectedText != string.Empty)
            this.tbCurrentMessage.Text = this.tbCurrentMessage.Text.Remove(this.tbCurrentMessage.SelectionStart, this.tbCurrentMessage.SelectionLength);
          this.tbCurrentMessage.Text = this.tbCurrentMessage.Text.Insert(selectionStart, $"[ref=\"{dbObject.ObjectGUID}\"]{dbObject.Caption}[/ref] ");
        }
      }
    }
  }

  /// <summary>вставка ссылки на сообщение из другого обсуждения</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnMessage_Click(object sender, EventArgs e)
  {
    object[] objArray = (ApplicationServices.Container.GetService(typeof (IUserMessageSelector)) as IUserMessageSelector).SelectMessages();
    if (objArray == null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index = 0; index < objArray.Length; ++index)
      {
        string str1 = objArray[index].ToString();
        string[] strArray = str1.Split(';');
        if (strArray.Length == 2)
        {
          string str2 = strArray[0];
          string str3 = strArray[1];
          IDBObject dbObject = GuidHelper.IsGuid(str2) ? sessionKeeper.Session.GetObject(new Guid(str2), false) : (IDBObject) null;
          string str4 = dbObject == null || dbObject.Caption == string.Empty ? string.Format(LocalizationHolder.rm.GetString("Workflow.Design_181"), (object) str2) : dbObject.Caption;
          this.tbCurrentMessage.Text += $"[ref=\"{str1}\"]{str4}[/ref] ";
        }
      }
    }
  }

  /// <summary>вставка ссылки на web-ресурс</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnWebRef_Click(object sender, EventArgs e)
  {
    using (RefWindow refWindow = new RefWindow())
    {
      if (refWindow.ShowDialog() != DialogResult.OK)
        return;
      this.tbCurrentMessage.Text += $"[url=\"{refWindow.url}\"]{refWindow.name}[/url]";
    }
  }

  /// <summary>вставка тэгов для цитирования сообщения</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnCitation_Click(object sender, EventArgs e)
  {
    TextBox tbCurrentMessage = this.tbCurrentMessage;
    tbCurrentMessage.Text = $"{tbCurrentMessage.Text}{this.citStart}]{this.citEnd}";
  }

  /// <summary>перемещение по ссылкам</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void wbForum_Navigating(object sender, WebBrowserNavigatingEventArgs e)
  {
    if (!e.Url.Fragment.StartsWith("#"))
      return;
    string[] strArray = e.Url.Fragment.Remove(0, 1).Split('=');
    try
    {
      switch (strArray[0])
      {
        case "object":
          this.OpenInNewForm(strArray[1]);
          break;
        case "message":
          this.OpenMessage(strArray[1]);
          break;
        case "web":
          Process.Start(strArray[1]);
          break;
        case "answer":
          this.CitationMessage(strArray[1]);
          break;
        case "change":
          this.ChangeMessage(strArray[1]);
          break;
        case "delete":
          this.DeleteMessage(strArray[1]);
          break;
      }
    }
    catch
    {
    }
    finally
    {
      e.Cancel = true;
    }
  }

  /// <summary>открыть объект в новом окне</summary>
  /// <param name="guid"></param>
  private void OpenInNewForm(string guid)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = GuidHelper.IsGuid(guid) ? sessionKeeper.Session.GetObject(new Guid(guid), false) : (IDBObject) null;
      if (dbObject == null)
        return;
      ISelectedItems items = Intermech.Navigator.ContextMenu.Services.GetItems(dbObject.ObjectID);
      ServiceContainer viewServices1 = new ServiceContainer();
      viewServices1.AddService(typeof (IViewState), (object) new ViewStateService());
      ServiceContainer viewServices2 = viewServices1;
      Intermech.Navigator.ContextMenu.Services.InvokeCommand("OpenInNewWindow", Intermech.Navigator.ContextMenu.Services.GetCommandsTable(items, (System.IServiceProvider) viewServices2), (System.IServiceProvider) viewServices1);
    }
  }

  /// <summary>открыть обсуждение в новом окне</summary>
  /// <param name="details">строка, содержащая id обсуждения и id сообщения</param>
  private void OpenMessage(string details)
  {
    string[] strArray = details.Split(new char[1]{ ';' }, StringSplitOptions.RemoveEmptyEntries);
    if (strArray.Length != 2)
      return;
    string str1 = strArray[0];
    if (!GuidHelper.IsGuid(str1))
      return;
    string str2 = strArray[1];
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(new Guid(str1), false);
      if (dbObject == null)
        return;
      ISelectedItems items = Intermech.Navigator.ContextMenu.Services.GetItems(dbObject.ObjectID);
      ServiceContainer viewServices1 = new ServiceContainer();
      viewServices1.AddService(typeof (IViewState), (object) new ViewStateService());
      viewServices1.AddService(typeof (IMessageForCheckingService), (object) new MessageForChecking(details));
      if (this.views != null)
        this.views.OnActivateView += new ActivateViewEventHandler(this.views_OnActivateView);
      ServiceContainer viewServices2 = viewServices1;
      Intermech.Navigator.ContextMenu.Services.InvokeCommand("OpenInNewWindow", Intermech.Navigator.ContextMenu.Services.GetCommandsTable(items, (System.IServiceProvider) viewServices2), (System.IServiceProvider) viewServices1);
      if (this.views == null)
        return;
      this.views.OnActivateView -= new ActivateViewEventHandler(this.views_OnActivateView);
    }
  }

  /// <summary>активировать закладку Обсуждение</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void views_OnActivateView(object sender, ActivateViewEventArgs e)
  {
    e.NewViewName = nameof (ForumView);
  }

  private void cbRegister_CheckedChanged(object sender, EventArgs e)
  {
    this.searchFlags ^= ForumView.SearchFlags.MatchCase;
  }

  /// <summary>поиск слова. вперёд</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnForward_Click(object sender, EventArgs e) => this.OnSearch(true);

  /// <summary>поиск слова. назад</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnBackward_Click(object sender, EventArgs e) => this.OnSearch(false);

  /// <summary>поиск слова</summary>
  /// <param name="forward">Найти следующее?</param>
  private bool OnSearch(bool forward)
  {
    bool flag = false;
    string text = this.tbSearch.Text;
    if (this.wbForum.Document != (HtmlDocument) null)
    {
      if (!(this.wbForum.Document.DomDocument is mshtml.IHTMLDocument2 domDocument) || !(domDocument.body is IHTMLBodyElement body) || domDocument.selection == null)
        return false;
      IHTMLTxtRange htmlTxtRange = domDocument.selection.createRange() as IHTMLTxtRange;
      if (!this.isNewText)
      {
        this.lastRange.move("character", forward ? text.Length : -text.Length);
        htmlTxtRange = this.lastRange;
      }
      if (htmlTxtRange != null)
      {
        if (forward)
          htmlTxtRange.moveStart("character", text.Length);
        else
          htmlTxtRange.moveEnd("character", -text.Length);
      }
      else
        htmlTxtRange = body.createTextRange();
      flag = htmlTxtRange.findText(text, forward ? text.Length : -text.Length, (int) this.searchFlags);
      if (flag)
      {
        htmlTxtRange.select();
        htmlTxtRange.scrollIntoView(!forward);
        this.lastRange = htmlTxtRange;
        this.isNewText = false;
      }
      else
      {
        IHTMLTxtRange textRange = body.createTextRange();
        if (textRange == null)
          return false;
        flag = textRange.findText(text, forward ? text.Length : -text.Length, (int) this.searchFlags);
        if (flag)
        {
          textRange.select();
          textRange.scrollIntoView(!forward);
          this.lastRange = textRange;
        }
      }
    }
    return flag;
  }

  private void tbSearch_TextChanged(object sender, EventArgs e)
  {
    this.isNewText = true;
    this.btnForward.Enabled = this.btnBackward.Enabled = this.tbSearch.Text.Length > 0;
    if (this.tbSearch.Text.Length <= 0)
      return;
    this.OnSearch(true);
  }

  private void tbCurrentMessage_DragEnter(object sender, DragEventArgs e)
  {
    e.Effect = DragDropEffects.Copy;
  }

  private void tbCurrentMessage_DragDrop(object sender, DragEventArgs e)
  {
    try
    {
      if (!(this.wbForum.Document != (HtmlDocument) null) || !(this.wbForum.Document.DomDocument is mshtml.IHTMLDocument2 domDocument) || !(domDocument.selection.createRange() is IHTMLTxtRange range) || range.text == null)
        return;
      this.tbCurrentMessage.Text += $"[cit]{range.text}[/cit]";
    }
    finally
    {
      e.Effect = DragDropEffects.None;
    }
  }

  /// <summary>копирование</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void wbForum_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
  {
  }

  /// <summary>доступность команд меню</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void cmsBrowser_Opening(object sender, CancelEventArgs e)
  {
    bool flag = false;
    HtmlDocument document = this.wbForum.Document;
    if (document != (HtmlDocument) null && document.DomDocument is mshtml.IHTMLDocument2 domDocument)
      flag = domDocument.queryCommandEnabled("copy");
    this.cmsBrowser.Items[0].Enabled = flag;
  }

  /// <summary>копирование выделенного текста</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void CopyText_Click(object sender, EventArgs e)
  {
    this.wbForum.Document.ExecCommand("copy", false, (object) null);
  }

  /// <summary>Печатать весь документ или только выбранные сообщения</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void cbPrintMode_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.printWholeDocument = this.cbPrintMode.ComboBox.SelectedIndex == 0;
  }

  public bool QueryStatus(ICommandState commandState)
  {
    if (!(commandState.CommandName == "Print") && !(commandState.CommandName == "PrintPreview"))
      return false;
    commandState.Enabled = true;
    return true;
  }

  public bool Execute(ICommandState commandState)
  {
    if (commandState.CommandName == "Print")
      this.Print();
    else if (commandState.CommandName == "PrintPreview")
      this.PrintPreview();
    return false;
  }

  private void PrintPreview()
  {
    this.CreatePrintDocument();
    this.wbPrint.ShowPrintPreviewDialog();
  }

  private void Print()
  {
    this.CreatePrintDocument();
    this.wbPrint.ShowPrintDialog();
  }

  private void CreatePrintDocument()
  {
    this.wbPrint.Navigate("about:blank");
    HtmlDocument document = this.wbPrint.Document;
    document.Write("<HTML><head><meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\" /></head><BODY></BODY></HTML>");
    do
    {
      Application.DoEvents();
    }
    while (this.wbPrint.IsBusy);
    HtmlElement elementById = this.wbForum.Document.GetElementById("forum_table");
    HtmlElement element1 = document.CreateElement("table");
    if (!(elementById != (HtmlElement) null))
      return;
    element1.Style = elementById.Style;
    HtmlElementCollection children1 = elementById.Children[0].Children;
    if (children1 == null || children1.Count == 0)
      return;
    foreach (HtmlElement htmlElement1 in children1)
    {
      string str = "true";
      HtmlElementCollection children2 = htmlElement1.Children;
      if (children2 == null || children2.Count != 2)
        return;
      if (!this.printWholeDocument)
      {
        HtmlElementCollection elementsByTagName = children2[0].GetElementsByTagName("input");
        if (elementsByTagName != null && elementsByTagName.Count != 0)
          str = elementsByTagName[0].GetAttribute("checked").ToLower();
        else
          continue;
      }
      if (str == "true")
      {
        HtmlElement htmlElement2 = children2[1];
        HtmlElement element2 = document.CreateElement("tr");
        HtmlElement element3 = document.CreateElement("td");
        element3.InnerHtml = htmlElement2.InnerHtml;
        element2.AppendChild(element3);
        element1.AppendChild(element2);
      }
    }
    document.Body.AppendChild(element1);
  }

  private void btnPrint_Click(object sender, EventArgs e) => this.Print();

  private void btnPrintView_Click(object sender, EventArgs e) => this.PrintPreview();

  /// <summary>просмотр перед печатью для выделенных</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsView_Click(object sender, EventArgs e)
  {
    bool printWholeDocument = this.printWholeDocument;
    this.printWholeDocument = false;
    this.PrintPreview();
    this.printWholeDocument = printWholeDocument;
  }

  /// <summary>вывод на печать для выделенных</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsPrint_Click(object sender, EventArgs e)
  {
    bool printWholeDocument = this.printWholeDocument;
    this.printWholeDocument = false;
    this.Print();
    this.printWholeDocument = printWholeDocument;
  }

  /// <summary>изменить направление сортировки</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnSort_Click(object sender, EventArgs e)
  {
    this.order = this.order == SortOrder.Descending ? SortOrder.Ascending : SortOrder.Descending;
    this.btnSort.ImageIndex = this.order == SortOrder.Descending ? 4 : 3;
    this.btnSort.ToolTipText = this.order == SortOrder.Descending ? LocalizationHolder.rm.GetString("Workflow.Design_182") : LocalizationHolder.rm.GetString("Workflow.Design_183");
    this.LoadMessages();
  }

  /// <summary>изменили сортировку сообщений</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void SortComboBox_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this.firstEnter)
      return;
    this.field = (SortField) this.cbSort.ComboBox.SelectedIndex;
    this.LoadMessages();
  }

  private void SortMessages()
  {
    this.forum.Sort(UserMessageComparer.MessageComparer(this.field, this.order));
  }

  private void wbForum_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
  {
    HtmlDocument document = this.wbForum.Document;
    if (!(document != (HtmlDocument) null) || this.ff != ForumFormat.None)
      return;
    HtmlElementCollection elementsByTagName = document.GetElementsByTagName("input");
    for (int index = 0; index < elementsByTagName.Count; ++index)
    {
      HtmlElement curCheckBox = elementsByTagName[index];
      curCheckBox.AttachEventHandler("onclick", (EventHandler) ((_param1, _param2) => this.SelectMessage((object) curCheckBox, EventArgs.Empty)));
      if (this._selectedMessageIdByDefault != string.Empty && curCheckBox.Id == this._selectedMessageIdByDefault)
      {
        curCheckBox.SetAttribute("checked", "checked");
        this.SelectMessage((object) curCheckBox, EventArgs.Empty);
      }
    }
  }

  private void SelectMessage(object sender, EventArgs e)
  {
    HtmlElement htmlElement = sender as HtmlElement;
    string attribute = htmlElement.GetAttribute("id");
    if (htmlElement.GetAttribute("checked").ToLower() == "true")
      this.selectedItems.AddMessage(attribute);
    else
      this.selectedItems.RemoveMessage(attribute);
    this.OnSelectedItemsChanged();
  }

  /// <summary>Выбранная коллекция элементов навигации</summary>
  public ISelectedItems SelectedItems => (ISelectedItems) this.selectedItems;

  /// <summary>событие изменения коллекции элементов навигации</summary>
  public event EventHandler SelectedItemsChanged;

  private void OnSelectedItemsChanged()
  {
    EventHandler selectedItemsChanged = this.SelectedItemsChanged;
    if (selectedItemsChanged == null)
      return;
    selectedItemsChanged((object) this, EventArgs.Empty);
  }

  private void btnImage_Click(object sender, EventArgs e)
  {
    this.ofdImages.Title = LocalizationHolder.rm.GetString("Workflow.Design_184");
    this.ofdImages.Multiselect = false;
    string str = "*.bmp;*.gif;*.tif;*.tiff;*.png;*.jpg;*.jpeg;*.exif;*.ico;*.emf;*.wmf";
    this.ofdImages.Filter = string.Format(LocalizationHolder.rm.GetString("Workflow.Design_185"), (object) str);
    if (this.ofdImages.ShowDialog() != DialogResult.OK)
      return;
    this.AddImagesFromFilesToObjAndTextBox(new StringCollection()
    {
      this.ofdImages.FileName
    });
  }

  /// <summary>
  /// Добавляем картинки из файов в файлы обсуждения и отображаемый текстбокс
  /// </summary>
  /// <param name="strings"></param>
  private void AddImagesFromFilesToObjAndTextBox(StringCollection fileNames)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetCustomService(typeof (IForumsService)) is IForumsService customService))
        return;
      IDBObject discussion = customService.CreateDiscussion(this.objectID, (object) sessionKeeper.Session.SessionGUID);
      long objectId = discussion.ObjectID;
      string curDiscussionGUID = discussion.ObjectGUID.ToString();
      IDBAttribute dbAttribute = discussion.GetAttributeByID(ForumsConsts.fileAttrTypeID);
      int fileIndex = 0;
      if (dbAttribute == null)
        dbAttribute = discussion.Attributes.AddAttribute(ForumsConsts.fileAttrTypeID, false);
      foreach (string fileName in fileNames)
      {
        dbAttribute.Index = 0;
        if (!dbAttribute.IsNull)
        {
          fileIndex = dbAttribute.AddValue((object) null);
          dbAttribute.Index = fileIndex;
        }
        string uniqueName = DateTime.Now.Ticks.ToString() + Path.GetExtension(fileName);
        this.WriteFileToDiscussionAttribute(objectId, fileName, uniqueName, fileIndex);
        this.AddFileLinkToTextBox(curDiscussionGUID, uniqueName, fileName);
      }
    }
  }

  /// <summary>Записываем ссылку на файл в текстбокс</summary>
  /// <param name="curDiscussionGUID"></param>
  /// <param name="uniqueName"></param>
  /// <param name="filename"></param>
  private void AddFileLinkToTextBox(string curDiscussionGUID, string uniqueName, string filename)
  {
    this.tbCurrentMessage.Text += $"[ref=\"{curDiscussionGUID};{uniqueName};{Path.GetFileName(filename)}\"][/ref] ";
  }

  /// <summary>Записываем файл в атрибут по указанному индексу</summary>
  /// <param name="curDiscussionID"></param>
  /// <param name="filename"></param>
  /// <param name="uniqueName"></param>
  /// <param name="fileIndex"></param>
  private void WriteFileToDiscussionAttribute(
    long curDiscussionID,
    string filename,
    string uniqueName,
    int fileIndex)
  {
    FileStream aSourceStream;
    try
    {
      aSourceStream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
    }
    catch
    {
      throw new Exception(LocalizationHolder.rm.GetString("Workflow.Design_186") + filename);
    }
    BlobInformation aBlobInformation = new BlobInformation(0L, 0L, File.GetLastWriteTime(filename), uniqueName, ArcMethods.ZLibPacked, string.Empty);
    new BlobProcWriter(curDiscussionID, AttributableElements.Object, ForumsConsts.fileAttrTypeID, fileIndex, 0, aBlobInformation, (Stream) aSourceStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).WriteData();
  }

  /// <summary>Получить путь к заданному файлу</summary>
  /// <param name="guid">guid объекта-обсуждения, у которого читаем файл</param>
  /// <param name="fileName">имя файла</param>
  /// <returns></returns>
  private string GetPublishImagePath(string guid, string fileName)
  {
    string publishImagePath = string.Empty;
    if (GuidHelper.IsGuid(guid))
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(new Guid(guid), false);
        if (dbObject != null)
        {
          try
          {
            IFileVault service = (IFileVault) ApplicationServices.Container.GetService(typeof (IFileVault));
            if (service != null)
              publishImagePath = service.PublishTree(dbObject.ObjectID, fileName, VersionsRuleSources.GetCurrentWindowRule(), (IFileArea) service.ViewArea);
          }
          catch
          {
            publishImagePath = string.Empty;
          }
        }
      }
    }
    return publishImagePath;
  }

  /// <summary>Вставка картинки из буфера обмена</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnAddImageFromClipboard_Click(object sender, EventArgs e)
  {
    if (Clipboard.ContainsFileDropList())
    {
      this.AddImagesFromFilesToObjAndTextBox(Clipboard.GetFileDropList());
    }
    else
    {
      if (!Clipboard.ContainsImage())
        return;
      this.AddImageFromClipboard();
    }
  }

  /// <summary>
  /// Добавляем картинку из буфера обмена в файлы объекта обсуждения и добавляем ссылку на нее в текст бокс
  /// </summary>
  private void AddImageFromClipboard()
  {
    Image image = Clipboard.GetImage();
    string str1 = "." + new ImageFormatConverter().ConvertToString((object) image.RawFormat).ToLower();
    ImageCodecInfo[] imageEncoders = ImageCodecInfo.GetImageEncoders();
    ImageCodecInfo encoder = (ImageCodecInfo) null;
    Guid guid1 = ImageFormat.Png.Guid;
    Guid guid2 = image.RawFormat.Guid;
    Guid guid3;
    foreach (ImageCodecInfo imageCodecInfo in imageEncoders)
    {
      guid3 = imageCodecInfo.FormatID;
      if (guid3.Equals(guid2))
      {
        encoder = imageCodecInfo;
        break;
      }
      if (encoder == null)
      {
        guid3 = imageCodecInfo.FormatID;
        if (guid3.Equals(guid1))
          encoder = imageCodecInfo;
      }
    }
    guid3 = encoder.FormatID;
    if (guid3.Equals(guid1))
      str1 = ".png";
    ImChunkedStream aSourceStream;
    try
    {
      aSourceStream = new ImChunkedStream();
      image.Save((Stream) aSourceStream, encoder, (EncoderParameters) null);
    }
    catch
    {
      throw;
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetCustomService(typeof (IForumsService)) is IForumsService customService))
        return;
      IDBObject discussion = customService.CreateDiscussion(this.objectID, (object) sessionKeeper.Session.SessionGUID);
      long objectId = discussion.ObjectID;
      guid3 = discussion.ObjectGUID;
      string curDiscussionGUID = guid3.ToString();
      IDBAttribute dbAttribute = discussion.GetAttributeByID(ForumsConsts.fileAttrTypeID);
      int aIndex = 0;
      if (dbAttribute == null)
        dbAttribute = discussion.Attributes.AddAttribute(ForumsConsts.fileAttrTypeID, false);
      if (!dbAttribute.IsNull)
      {
        aIndex = dbAttribute.AddValue((object) null);
        dbAttribute.Index = aIndex;
      }
      string filename = new Random().Next(int.MaxValue).ToString() + str1;
      string str2 = DateTime.Now.Ticks.ToString() + str1;
      BlobInformation aBlobInformation = new BlobInformation(0L, 0L, DateTime.Now, str2, ArcMethods.ZLibPacked, string.Empty);
      new BlobProcWriter(objectId, AttributableElements.Object, ForumsConsts.fileAttrTypeID, aIndex, 0, aBlobInformation, (Stream) aSourceStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).WriteData();
      this.AddFileLinkToTextBox(curDiscussionGUID, str2, filename);
    }
  }

  /// <summary>теги для полужирного текста</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnBold_Click(object sender, EventArgs e)
  {
    string selectedText = this.tbCurrentMessage.SelectedText;
    if (selectedText.StartsWith(this.boldSt) && selectedText.EndsWith(this.boldEnd))
    {
      string str = selectedText.Remove(0, this.boldSt.Length);
      this.tbCurrentMessage.SelectedText = str.Remove(str.IndexOf(this.boldEnd), this.boldEnd.Length);
    }
    else
      this.tbCurrentMessage.SelectedText = $"{this.boldSt}{selectedText}{this.boldEnd}";
  }

  /// <summary>тэги для курсива</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnItalic_Click(object sender, EventArgs e)
  {
    string selectedText = this.tbCurrentMessage.SelectedText;
    if (selectedText.StartsWith(this.italicSt) && selectedText.EndsWith(this.italicEnd))
    {
      string str = selectedText.Remove(0, this.italicSt.Length);
      this.tbCurrentMessage.SelectedText = str.Remove(str.IndexOf(this.italicEnd), this.italicEnd.Length);
    }
    else
      this.tbCurrentMessage.SelectedText = $"{this.italicSt}{this.tbCurrentMessage.SelectedText}{this.italicEnd}";
  }

  /// <summary>тэги для подчёркавания</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnUnderline_Click(object sender, EventArgs e)
  {
    string selectedText = this.tbCurrentMessage.SelectedText;
    if (selectedText.StartsWith(this.underlineSt) && selectedText.EndsWith(this.underlineEnd))
    {
      string str = selectedText.Remove(0, this.underlineSt.Length);
      this.tbCurrentMessage.SelectedText = str.Remove(str.IndexOf(this.underlineEnd), this.underlineEnd.Length);
    }
    else
      this.tbCurrentMessage.SelectedText = $"{this.underlineSt}{this.tbCurrentMessage.SelectedText}{this.underlineEnd}";
  }

  /// <summary>подчёркнутый текст</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnStrike_Click(object sender, EventArgs e)
  {
    string selectedText = this.tbCurrentMessage.SelectedText;
    if (selectedText.StartsWith(this.strikeSt) && selectedText.EndsWith(this.strikeEnd))
    {
      string str = selectedText.Remove(0, this.strikeSt.Length);
      this.tbCurrentMessage.SelectedText = str.Remove(str.IndexOf(this.strikeEnd), this.strikeEnd.Length);
    }
    else
      this.tbCurrentMessage.SelectedText = $"{this.strikeSt}{this.tbCurrentMessage.SelectedText}{this.strikeEnd}";
  }

  /// <summary>изменить цвет шрифта</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnFontColor_Click(object sender, EventArgs e)
  {
    if (this.colorDialog1.ShowDialog() != DialogResult.OK)
      return;
    string selectedText = this.tbCurrentMessage.SelectedText;
    this.tbCurrentMessage.SelectedText = $"{this.colorSt}{ColorTranslator.ToHtml(this.colorDialog1.Color)}]{this.tbCurrentMessage.SelectedText}{this.colorEnd}";
  }

  /// <summary>изменить цвет фона</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnBackColor_Click(object sender, EventArgs e)
  {
    if (this.colorDialog1.ShowDialog() != DialogResult.OK)
      return;
    string selectedText = this.tbCurrentMessage.SelectedText;
    this.tbCurrentMessage.SelectedText = $"{this.backSt}{ColorTranslator.ToHtml(this.colorDialog1.Color)}]{this.tbCurrentMessage.SelectedText}{this.backEnd}";
  }

  /// <summary>изменить размер шрифта</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void cbSize_SelectedIndexChanged(object sender, EventArgs e)
  {
    string selectedText = this.tbCurrentMessage.SelectedText;
    this.tbCurrentMessage.SelectedText = $"{this.sizeSt}{this.cbSize.SelectedItem}]{this.tbCurrentMessage.SelectedText}{this.sizeEnd}";
  }

  /// <summary>нажали на ссылку - редактирования сообщения</summary>
  /// <param name="details"></param>
  private void ChangeMessage(string details)
  {
    this.btnCancel.Visible = this.btnChange.Visible = true;
    this.btnSend.Visible = false;
    string[] strArray = details.Split(';');
    if (strArray.Length != 2)
      return;
    string str = strArray[0];
    this.changedMessage = this.forum.FindMessage(strArray[1]);
    if (this.changedMessage == null)
      return;
    this.tbCaption.Text = this.changedMessage.Caption;
    this.tbCurrentMessage.Text = this.changedMessage.Message;
  }

  /// <summary>нажали ссылку -удалить сообщение</summary>
  /// <param name="details"></param>
  private void DeleteMessage(string details)
  {
    if (MessageBox.Show(LocalizationHolder.rm.GetString("Workflow.Design_188"), LocalizationHolder.rm.GetString("Workflow.Design_189"), MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.OK)
      return;
    string[] strArray = details.Split(';');
    if (strArray.Length != 2)
      return;
    string str = strArray[0];
    UserMessage message = this.forum.FindMessage(strArray[1]);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (sessionKeeper.Session.GetCustomService(typeof (IForumsService)) is IForumsService customService)
        customService.DeleteMessage(ref this.forum, message, sessionKeeper.Session.SessionGUID);
      this.LoadMessages();
    }
  }

  /// <summary>изменить сообщение</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnChange_Click(object sender, EventArgs e)
  {
    this.changedMessage.Caption = this.tbCaption.Text;
    this.changedMessage.Message = this.tbCurrentMessage.Text;
    this.changedMessage.ModifyDate = DateTime.UtcNow;
    this.changedMessage.ReadByUsers.Clear();
    this.changedMessage.ReadByUsers.Add(this.userGuid.ToString());
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (sessionKeeper.Session.GetCustomService(typeof (IForumsService)) is IForumsService customService)
        customService.ChangeMessage(this.forum, new Guid(this.changedMessage.DicsObjectGuid), sessionKeeper.Session.SessionGUID, true);
    }
    this.tbCurrentMessage.Clear();
    this.btnCancel.Visible = this.btnChange.Visible = false;
    this.btnSend.Visible = true;
    this.LoadMessages();
    this.NotifyMentionedUsers();
  }

  /// <summary>
  /// Обработать список упомянутых пользователей и выслать им уведомления об упоминании
  /// </summary>
  private void NotifyMentionedUsers()
  {
    this.CheckUsersReferenceInText();
    this.SendNotificationsToUsers();
    this.ClearUsersList();
  }

  /// <summary>отменить редактирование сообщения</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnCancel_Click(object sender, EventArgs e)
  {
    this.tbCurrentMessage.Clear();
    this.btnCancel.Visible = this.btnChange.Visible = false;
    this.btnSend.Visible = true;
  }

  private void btnUpdate_Click(object sender, EventArgs e) => this.ReloadForum();

  private void btnMentionUsers_Click(object sender, EventArgs e)
  {
    object[] objArray = Intermech.Navigator.SelectionWindow.Select(LocalizationHolder.rm.GetString("Interfaces.Workflow_ChooseUsersForMention"), (IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(MetaDataHelper.GetObjectTypeID("cad00002-306c-11d8-b4e9-00304f19f545")), typeof (IDBTypedObjectID), SelectionOptions.SelectObjects | SelectionOptions.DisableSelectFromTree);
    if (objArray == null || objArray.Length == 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (object obj in objArray)
      {
        if (obj is IDBTypedObjectID dbTypedObjectId)
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(dbTypedObjectId.ObjectID, false);
          if (dbObject != null)
          {
            int selectionStart = this.tbCurrentMessage.SelectionStart;
            if (this.tbCurrentMessage.SelectedText != string.Empty)
              this.tbCurrentMessage.Text = this.tbCurrentMessage.Text.Remove(this.tbCurrentMessage.SelectionStart, this.tbCurrentMessage.SelectionLength);
            this.tbCurrentMessage.Text = this.tbCurrentMessage.Text.Insert(selectionStart, $"[ref=\"{dbObject.ObjectGUID}\"]{dbObject.Caption}[/ref] ");
            if (dbObject.TypeID == MetaDataHelper.GetObjectTypeID("cad00002-306c-11d8-b4e9-00304f19f545"))
              this._usersGuidsForNotification.SafeAdd<string>(dbObject.ObjectGUID.ToString());
          }
        }
      }
    }
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (ApplicationServices.Container.GetService(typeof (BarManager)) is BarManager service)
    {
      this.tbOptions.Renderer = (IToolBarRenderer) new EmptyToolbarRenderer();
      service.RendererChanged -= new EventHandler(this.ToolbarRendererChanged);
    }
    if (this.wbForum != null)
    {
      this.wbForum.DocumentCompleted -= new WebBrowserDocumentCompletedEventHandler(this.wbForum_DocumentCompleted);
      this.wbForum.Navigating -= new WebBrowserNavigatingEventHandler(this.wbForum_Navigating);
      this.wbForum.PreviewKeyDown -= new PreviewKeyDownEventHandler(this.wbForum_PreviewKeyDown);
    }
    if (disposing && this.components != null)
      this.components.Dispose();
    this.cbSort.ComboBox.SelectedIndexChanged -= new EventHandler(this.SortComboBox_SelectedIndexChanged);
    this.cbForumFormat.ComboBox.SelectedIndexChanged -= new EventHandler(this.FFComboBox_SelectedIndexChanged);
    this.cbPrintMode.ComboBox.SelectedIndexChanged -= new EventHandler(this.cbPrintMode_SelectedIndexChanged);
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ForumView));
    this.tbOptions = new Intermech.Bars.ToolBar();
    this.ilForums = new ImageList(this.components);
    this.cbForumFormat = new ComboBoxItem();
    this.cbSort = new ComboBoxItem();
    this.btnUpdate = new ButtonItem();
    this.btnSort = new ButtonItem();
    this.btnPrintView = new ButtonItem();
    this.btnPrint = new ButtonItem();
    this.cbPrintMode = new ComboBoxItem();
    this.pMessage = new Panel();
    this.btnAddImageFromClipboard = new Button();
    this.btnCancel = new Button();
    this.btnChange = new Button();
    this.btnBackColor = new Button();
    this.btnFontColor = new Button();
    this.cbSize = new ComboBox();
    this.btnStrike = new Button();
    this.btnUnderline = new Button();
    this.btnItalic = new Button();
    this.btnBold = new Button();
    this.label2 = new Label();
    this.btnImage = new Button();
    this.wbPrint = new WebBrowser();
    this.tbCurrentMessage = new TextBox();
    this.btnMessage = new Button();
    this.btnSend = new Button();
    this.btnObjRef = new Button();
    this.btnWebRef = new Button();
    this.btnCitation = new Button();
    this.tbCaption = new TextBox();
    this.pForum = new Panel();
    this.wbForum = new WebBrowser();
    this.cmsBrowser = new ContextMenuStrip(this.components);
    this.CopyText = new ToolStripMenuItem();
    this.toolStripSeparator1 = new ToolStripSeparator();
    this.tsView = new ToolStripMenuItem();
    this.tsPrint = new ToolStripMenuItem();
    this.pSearch = new Panel();
    this.cbRegister = new CheckBox();
    this.btnBackward = new Button();
    this.btnForward = new Button();
    this.tbSearch = new TextBox();
    this.label1 = new Label();
    this.ttForum = new ToolTip(this.components);
    this.panel1 = new Panel();
    this.splitter1 = new Splitter();
    this.ofdImages = new OpenFileDialog();
    this.fontDialog1 = new FontDialog();
    this.colorDialog1 = new ColorDialog();
    this.btnMentionUsers = new Button();
    this.pMessage.SuspendLayout();
    this.pForum.SuspendLayout();
    this.cmsBrowser.SuspendLayout();
    this.pSearch.SuspendLayout();
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    this.tbOptions.FullMenus = true;
    this.tbOptions.Guid = new Guid("93c31108-7d26-4e2b-88a2-a6d616a39ecb");
    this.tbOptions.Hidden = false;
    this.tbOptions.ImageList = this.ilForums;
    this.tbOptions.Items.AddRange(new ToolbarItemBase[7]
    {
      (ToolbarItemBase) this.cbForumFormat,
      (ToolbarItemBase) this.cbSort,
      (ToolbarItemBase) this.btnUpdate,
      (ToolbarItemBase) this.btnSort,
      (ToolbarItemBase) this.btnPrintView,
      (ToolbarItemBase) this.btnPrint,
      (ToolbarItemBase) this.cbPrintMode
    });
    componentResourceManager.ApplyResources((object) this.tbOptions, "tbOptions");
    this.tbOptions.Name = "tbOptions";
    this.tbOptions.Stretch = true;
    this.ilForums.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("ilForums.ImageStream");
    this.ilForums.TransparentColor = Color.Magenta;
    this.ilForums.Images.SetKeyName(0, "print.ico");
    this.ilForums.Images.SetKeyName(1, "history.png");
    this.ilForums.Images.SetKeyName(2, "Опубликованные объекты_cr.png");
    this.ilForums.Images.SetKeyName(3, "sort.ico");
    this.ilForums.Images.SetKeyName(4, "z_a.png");
    this.ilForums.Images.SetKeyName(5, "types.ico");
    this.ilForums.Images.SetKeyName(6, "выбор_сообщения.ico");
    this.ilForums.Images.SetKeyName(7, "web_ссылка.ico");
    this.ilForums.Images.SetKeyName(8, "цитата.ico");
    this.ilForums.Images.SetKeyName(9, "image.ico");
    this.ilForums.Images.SetKeyName(10, "Bold1.png");
    this.ilForums.Images.SetKeyName(11, "kursive1.png");
    this.ilForums.Images.SetKeyName(12, "Underline1.png");
    this.ilForums.Images.SetKeyName(13, "FillColor.png");
    this.ilForums.Images.SetKeyName(14, "FontColor.png");
    this.ilForums.Images.SetKeyName(15, "strikethrough.png");
    this.ilForums.Images.SetKeyName(16 /*0x10*/, "imagefromclipboard.ico");
    this.ilForums.Images.SetKeyName(17, "userIcon.ico");
    this.cbForumFormat.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.cbForumFormat, "cbForumFormat");
    this.cbForumFormat.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbForumFormat.MinimumControlWidth = 50;
    this.cbForumFormat.Padding.Bottom = 0;
    this.cbForumFormat.Padding.Left = 1;
    this.cbForumFormat.Padding.Right = 1;
    this.cbForumFormat.Padding.Top = 0;
    this.cbForumFormat.Stretch = true;
    this.cbSort.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.cbSort, "cbSort");
    this.cbSort.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbSort.Items.AddRange(new object[3]
    {
      (object) "По дате добавления",
      (object) "По заголовку",
      (object) "По автору сообщения"
    });
    this.cbSort.MinimumControlWidth = 50;
    this.cbSort.Padding.Bottom = 0;
    this.cbSort.Padding.Left = 1;
    this.cbSort.Padding.Right = 1;
    this.cbSort.Padding.Top = 0;
    this.cbSort.Stretch = true;
    componentResourceManager.ApplyResources((object) this.btnUpdate, "btnUpdate");
    this.btnUpdate.Image = (Image) Resources.refresh;
    this.btnUpdate.Click += new EventHandler(this.btnUpdate_Click);
    componentResourceManager.ApplyResources((object) this.btnSort, "btnSort");
    this.btnSort.ImageIndex = 3;
    this.btnSort.Click += new EventHandler(this.btnSort_Click);
    this.btnPrintView.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.btnPrintView, "btnPrintView");
    this.btnPrintView.Click += new EventHandler(this.btnPrintView_Click);
    componentResourceManager.ApplyResources((object) this.btnPrint, "btnPrint");
    this.btnPrint.ImageIndex = 0;
    this.btnPrint.Click += new EventHandler(this.btnPrint_Click);
    componentResourceManager.ApplyResources((object) this.cbPrintMode, "cbPrintMode");
    this.cbPrintMode.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbPrintMode.Items.AddRange(new object[2]
    {
      (object) "Печатать все сообщения",
      (object) "Печатать выбранные сообщения"
    });
    this.cbPrintMode.MinimumControlWidth = 50;
    this.cbPrintMode.Padding.Bottom = 0;
    this.cbPrintMode.Padding.Left = 1;
    this.cbPrintMode.Padding.Right = 1;
    this.cbPrintMode.Padding.Top = 0;
    this.cbPrintMode.Stretch = true;
    this.pMessage.BackColor = Color.LightGray;
    this.pMessage.Controls.Add((Control) this.btnMentionUsers);
    this.pMessage.Controls.Add((Control) this.btnAddImageFromClipboard);
    this.pMessage.Controls.Add((Control) this.btnCancel);
    this.pMessage.Controls.Add((Control) this.btnChange);
    this.pMessage.Controls.Add((Control) this.btnBackColor);
    this.pMessage.Controls.Add((Control) this.btnFontColor);
    this.pMessage.Controls.Add((Control) this.cbSize);
    this.pMessage.Controls.Add((Control) this.btnStrike);
    this.pMessage.Controls.Add((Control) this.btnUnderline);
    this.pMessage.Controls.Add((Control) this.btnItalic);
    this.pMessage.Controls.Add((Control) this.btnBold);
    this.pMessage.Controls.Add((Control) this.label2);
    this.pMessage.Controls.Add((Control) this.btnImage);
    this.pMessage.Controls.Add((Control) this.wbPrint);
    this.pMessage.Controls.Add((Control) this.tbCurrentMessage);
    this.pMessage.Controls.Add((Control) this.btnMessage);
    this.pMessage.Controls.Add((Control) this.btnSend);
    this.pMessage.Controls.Add((Control) this.btnObjRef);
    this.pMessage.Controls.Add((Control) this.btnWebRef);
    this.pMessage.Controls.Add((Control) this.btnCitation);
    this.pMessage.Controls.Add((Control) this.tbCaption);
    componentResourceManager.ApplyResources((object) this.pMessage, "pMessage");
    this.pMessage.Name = "pMessage";
    componentResourceManager.ApplyResources((object) this.btnAddImageFromClipboard, "btnAddImageFromClipboard");
    this.btnAddImageFromClipboard.ImageList = this.ilForums;
    this.btnAddImageFromClipboard.Name = "btnAddImageFromClipboard";
    this.ttForum.SetToolTip((Control) this.btnAddImageFromClipboard, componentResourceManager.GetString("btnAddImageFromClipboard.ToolTip"));
    this.btnAddImageFromClipboard.UseVisualStyleBackColor = true;
    this.btnAddImageFromClipboard.Click += new EventHandler(this.btnAddImageFromClipboard_Click);
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.Name = "btnCancel";
    this.ttForum.SetToolTip((Control) this.btnCancel, componentResourceManager.GetString("btnCancel.ToolTip"));
    this.btnCancel.UseVisualStyleBackColor = true;
    this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
    componentResourceManager.ApplyResources((object) this.btnChange, "btnChange");
    this.btnChange.Name = "btnChange";
    this.ttForum.SetToolTip((Control) this.btnChange, componentResourceManager.GetString("btnChange.ToolTip"));
    this.btnChange.UseVisualStyleBackColor = true;
    this.btnChange.Click += new EventHandler(this.btnChange_Click);
    componentResourceManager.ApplyResources((object) this.btnBackColor, "btnBackColor");
    this.btnBackColor.ImageList = this.ilForums;
    this.btnBackColor.Name = "btnBackColor";
    this.ttForum.SetToolTip((Control) this.btnBackColor, componentResourceManager.GetString("btnBackColor.ToolTip"));
    this.btnBackColor.UseVisualStyleBackColor = true;
    this.btnBackColor.Click += new EventHandler(this.btnBackColor_Click);
    componentResourceManager.ApplyResources((object) this.btnFontColor, "btnFontColor");
    this.btnFontColor.ImageList = this.ilForums;
    this.btnFontColor.Name = "btnFontColor";
    this.ttForum.SetToolTip((Control) this.btnFontColor, componentResourceManager.GetString("btnFontColor.ToolTip"));
    this.btnFontColor.UseVisualStyleBackColor = true;
    this.btnFontColor.Click += new EventHandler(this.btnFontColor_Click);
    this.cbSize.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbSize.FormattingEnabled = true;
    this.cbSize.Items.AddRange(new object[16 /*0x10*/]
    {
      (object) componentResourceManager.GetString("cbSize.Items"),
      (object) componentResourceManager.GetString("cbSize.Items1"),
      (object) componentResourceManager.GetString("cbSize.Items2"),
      (object) componentResourceManager.GetString("cbSize.Items3"),
      (object) componentResourceManager.GetString("cbSize.Items4"),
      (object) componentResourceManager.GetString("cbSize.Items5"),
      (object) componentResourceManager.GetString("cbSize.Items6"),
      (object) componentResourceManager.GetString("cbSize.Items7"),
      (object) componentResourceManager.GetString("cbSize.Items8"),
      (object) componentResourceManager.GetString("cbSize.Items9"),
      (object) componentResourceManager.GetString("cbSize.Items10"),
      (object) componentResourceManager.GetString("cbSize.Items11"),
      (object) componentResourceManager.GetString("cbSize.Items12"),
      (object) componentResourceManager.GetString("cbSize.Items13"),
      (object) componentResourceManager.GetString("cbSize.Items14"),
      (object) componentResourceManager.GetString("cbSize.Items15")
    });
    componentResourceManager.ApplyResources((object) this.cbSize, "cbSize");
    this.cbSize.Name = "cbSize";
    this.ttForum.SetToolTip((Control) this.cbSize, componentResourceManager.GetString("cbSize.ToolTip"));
    componentResourceManager.ApplyResources((object) this.btnStrike, "btnStrike");
    this.btnStrike.ImageList = this.ilForums;
    this.btnStrike.Name = "btnStrike";
    this.ttForum.SetToolTip((Control) this.btnStrike, componentResourceManager.GetString("btnStrike.ToolTip"));
    this.btnStrike.UseVisualStyleBackColor = true;
    this.btnStrike.Click += new EventHandler(this.btnStrike_Click);
    this.btnUnderline.Cursor = Cursors.Default;
    componentResourceManager.ApplyResources((object) this.btnUnderline, "btnUnderline");
    this.btnUnderline.ImageList = this.ilForums;
    this.btnUnderline.Name = "btnUnderline";
    this.ttForum.SetToolTip((Control) this.btnUnderline, componentResourceManager.GetString("btnUnderline.ToolTip"));
    this.btnUnderline.UseVisualStyleBackColor = true;
    this.btnUnderline.Click += new EventHandler(this.btnUnderline_Click);
    componentResourceManager.ApplyResources((object) this.btnItalic, "btnItalic");
    this.btnItalic.ImageList = this.ilForums;
    this.btnItalic.Name = "btnItalic";
    this.ttForum.SetToolTip((Control) this.btnItalic, componentResourceManager.GetString("btnItalic.ToolTip"));
    this.btnItalic.UseVisualStyleBackColor = true;
    this.btnItalic.Click += new EventHandler(this.btnItalic_Click);
    componentResourceManager.ApplyResources((object) this.btnBold, "btnBold");
    this.btnBold.ImageList = this.ilForums;
    this.btnBold.Name = "btnBold";
    this.ttForum.SetToolTip((Control) this.btnBold, componentResourceManager.GetString("btnBold.ToolTip"));
    this.btnBold.UseVisualStyleBackColor = true;
    this.btnBold.Click += new EventHandler(this.btnBold_Click);
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    componentResourceManager.ApplyResources((object) this.btnImage, "btnImage");
    this.btnImage.ImageList = this.ilForums;
    this.btnImage.Name = "btnImage";
    this.ttForum.SetToolTip((Control) this.btnImage, componentResourceManager.GetString("btnImage.ToolTip"));
    this.btnImage.UseVisualStyleBackColor = true;
    this.btnImage.Click += new EventHandler(this.btnImage_Click);
    this.wbPrint.IsWebBrowserContextMenuEnabled = false;
    componentResourceManager.ApplyResources((object) this.wbPrint, "wbPrint");
    this.wbPrint.Name = "wbPrint";
    this.wbPrint.ScriptErrorsSuppressed = true;
    this.wbPrint.ScrollBarsEnabled = false;
    this.wbPrint.TabStop = false;
    this.wbPrint.WebBrowserShortcutsEnabled = false;
    this.tbCurrentMessage.AllowDrop = true;
    componentResourceManager.ApplyResources((object) this.tbCurrentMessage, "tbCurrentMessage");
    this.tbCurrentMessage.Name = "tbCurrentMessage";
    this.tbCurrentMessage.TextChanged += new EventHandler(this.tbCurrentMessage_TextChanged);
    this.tbCurrentMessage.DragDrop += new DragEventHandler(this.tbCurrentMessage_DragDrop);
    this.tbCurrentMessage.DragEnter += new DragEventHandler(this.tbCurrentMessage_DragEnter);
    componentResourceManager.ApplyResources((object) this.btnMessage, "btnMessage");
    this.btnMessage.ImageList = this.ilForums;
    this.btnMessage.Name = "btnMessage";
    this.ttForum.SetToolTip((Control) this.btnMessage, componentResourceManager.GetString("btnMessage.ToolTip"));
    this.btnMessage.UseVisualStyleBackColor = true;
    this.btnMessage.Click += new EventHandler(this.btnMessage_Click);
    componentResourceManager.ApplyResources((object) this.btnSend, "btnSend");
    this.btnSend.Name = "btnSend";
    this.ttForum.SetToolTip((Control) this.btnSend, componentResourceManager.GetString("btnSend.ToolTip"));
    this.btnSend.UseVisualStyleBackColor = true;
    this.btnSend.Click += new EventHandler(this.btnSend_Click);
    componentResourceManager.ApplyResources((object) this.btnObjRef, "btnObjRef");
    this.btnObjRef.ImageList = this.ilForums;
    this.btnObjRef.Name = "btnObjRef";
    this.ttForum.SetToolTip((Control) this.btnObjRef, componentResourceManager.GetString("btnObjRef.ToolTip"));
    this.btnObjRef.UseVisualStyleBackColor = true;
    this.btnObjRef.Click += new EventHandler(this.btnObjRef_Click);
    componentResourceManager.ApplyResources((object) this.btnWebRef, "btnWebRef");
    this.btnWebRef.ImageList = this.ilForums;
    this.btnWebRef.Name = "btnWebRef";
    this.ttForum.SetToolTip((Control) this.btnWebRef, componentResourceManager.GetString("btnWebRef.ToolTip"));
    this.btnWebRef.UseVisualStyleBackColor = true;
    this.btnWebRef.Click += new EventHandler(this.btnWebRef_Click);
    componentResourceManager.ApplyResources((object) this.btnCitation, "btnCitation");
    this.btnCitation.ImageList = this.ilForums;
    this.btnCitation.Name = "btnCitation";
    this.ttForum.SetToolTip((Control) this.btnCitation, componentResourceManager.GetString("btnCitation.ToolTip"));
    this.btnCitation.UseVisualStyleBackColor = true;
    this.btnCitation.Click += new EventHandler(this.btnCitation_Click);
    componentResourceManager.ApplyResources((object) this.tbCaption, "tbCaption");
    this.tbCaption.Name = "tbCaption";
    componentResourceManager.ApplyResources((object) this.pForum, "pForum");
    this.pForum.BackColor = SystemColors.Control;
    this.pForum.Controls.Add((Control) this.wbForum);
    this.pForum.Controls.Add((Control) this.pSearch);
    this.pForum.Name = "pForum";
    this.wbForum.AllowWebBrowserDrop = false;
    this.wbForum.ContextMenuStrip = this.cmsBrowser;
    componentResourceManager.ApplyResources((object) this.wbForum, "wbForum");
    this.wbForum.IsWebBrowserContextMenuEnabled = false;
    this.wbForum.Name = "wbForum";
    this.wbForum.ScriptErrorsSuppressed = true;
    this.wbForum.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(this.wbForum_DocumentCompleted);
    this.wbForum.Navigating += new WebBrowserNavigatingEventHandler(this.wbForum_Navigating);
    this.wbForum.PreviewKeyDown += new PreviewKeyDownEventHandler(this.wbForum_PreviewKeyDown);
    this.cmsBrowser.ImageScalingSize = new Size(20, 20);
    this.cmsBrowser.Items.AddRange(new ToolStripItem[4]
    {
      (ToolStripItem) this.CopyText,
      (ToolStripItem) this.toolStripSeparator1,
      (ToolStripItem) this.tsView,
      (ToolStripItem) this.tsPrint
    });
    this.cmsBrowser.Name = "cmsBrowser";
    componentResourceManager.ApplyResources((object) this.cmsBrowser, "cmsBrowser");
    this.cmsBrowser.Opening += new CancelEventHandler(this.cmsBrowser_Opening);
    this.CopyText.Name = "CopyText";
    componentResourceManager.ApplyResources((object) this.CopyText, "CopyText");
    this.CopyText.Click += new EventHandler(this.CopyText_Click);
    this.toolStripSeparator1.Name = "toolStripSeparator1";
    componentResourceManager.ApplyResources((object) this.toolStripSeparator1, "toolStripSeparator1");
    this.tsView.Name = "tsView";
    componentResourceManager.ApplyResources((object) this.tsView, "tsView");
    this.tsView.Click += new EventHandler(this.tsView_Click);
    this.tsPrint.Name = "tsPrint";
    componentResourceManager.ApplyResources((object) this.tsPrint, "tsPrint");
    this.tsPrint.Click += new EventHandler(this.tsPrint_Click);
    this.pSearch.BackColor = Color.LightGray;
    this.pSearch.Controls.Add((Control) this.cbRegister);
    this.pSearch.Controls.Add((Control) this.btnBackward);
    this.pSearch.Controls.Add((Control) this.btnForward);
    this.pSearch.Controls.Add((Control) this.tbSearch);
    this.pSearch.Controls.Add((Control) this.label1);
    componentResourceManager.ApplyResources((object) this.pSearch, "pSearch");
    this.pSearch.Name = "pSearch";
    componentResourceManager.ApplyResources((object) this.cbRegister, "cbRegister");
    this.cbRegister.Name = "cbRegister";
    this.cbRegister.UseVisualStyleBackColor = true;
    this.cbRegister.CheckedChanged += new EventHandler(this.cbRegister_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.btnBackward, "btnBackward");
    this.btnBackward.Name = "btnBackward";
    this.ttForum.SetToolTip((Control) this.btnBackward, componentResourceManager.GetString("btnBackward.ToolTip"));
    this.btnBackward.UseVisualStyleBackColor = true;
    this.btnBackward.Click += new EventHandler(this.btnBackward_Click);
    componentResourceManager.ApplyResources((object) this.btnForward, "btnForward");
    this.btnForward.Name = "btnForward";
    this.ttForum.SetToolTip((Control) this.btnForward, componentResourceManager.GetString("btnForward.ToolTip"));
    this.btnForward.UseVisualStyleBackColor = true;
    this.btnForward.Click += new EventHandler(this.btnForward_Click);
    componentResourceManager.ApplyResources((object) this.tbSearch, "tbSearch");
    this.tbSearch.Name = "tbSearch";
    this.ttForum.SetToolTip((Control) this.tbSearch, componentResourceManager.GetString("tbSearch.ToolTip"));
    this.tbSearch.TextChanged += new EventHandler(this.tbSearch_TextChanged);
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    this.panel1.Controls.Add((Control) this.pMessage);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.splitter1, "splitter1");
    this.splitter1.Name = "splitter1";
    this.splitter1.TabStop = false;
    this.ofdImages.RestoreDirectory = true;
    this.colorDialog1.AnyColor = true;
    componentResourceManager.ApplyResources((object) this.btnMentionUsers, "btnMentionUsers");
    this.btnMentionUsers.ImageList = this.ilForums;
    this.btnMentionUsers.Name = "btnMentionUsers";
    this.ttForum.SetToolTip((Control) this.btnMentionUsers, componentResourceManager.GetString("btnMentionUsers.ToolTip"));
    this.btnMentionUsers.UseVisualStyleBackColor = true;
    this.btnMentionUsers.Click += new EventHandler(this.btnMentionUsers_Click);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.splitter1);
    this.Controls.Add((Control) this.pForum);
    this.Controls.Add((Control) this.tbOptions);
    this.Controls.Add((Control) this.panel1);
    this.Name = nameof (ForumView);
    this.pMessage.ResumeLayout(false);
    this.pMessage.PerformLayout();
    this.pForum.ResumeLayout(false);
    this.cmsBrowser.ResumeLayout(false);
    this.pSearch.ResumeLayout(false);
    this.pSearch.PerformLayout();
    this.panel1.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private enum SearchFlags
  {
    Default = 0,
    WholeWord = 2,
    MatchCase = 4,
  }
}
