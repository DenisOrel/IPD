// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.Copies.SendCopiesForm
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using Intermech.Archives.Common;
using Intermech.Client.Core;
using Intermech.Controls;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Copies;
using Intermech.Interfaces.WebPortal;
using Intermech.Kernel.Search;
using Intermech.Navigator;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Search.Interfaces.Copies;
using Intermech.Security;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Archives.Copies;

/// <summary>Форма для отправки копий документов</summary>
public class SendCopiesForm : Form
{
  /// <summary>
  /// id версии документа, который запишем в Актуальную копию для листа рассылки
  /// </summary>
  private long _actualCopyID;
  /// <summary>
  /// заголовок версии документа, который запишем в Актульную копию для листа рассылки
  /// </summary>
  private string _actualCopyCaption = string.Empty;
  /// <summary>лист рассылки для документа</summary>
  private readonly List<long> _deliveryListIDs;
  /// <summary>Абонент, которому высылаем копии</summary>
  private long _subscrID;
  /// <summary>Получатель копии</summary>
  private long _recipientID;
  /// <summary>Альбом, в который будет добавлена копия</summary>
  private long _albumID;
  /// <summary>Дата</summary>
  private DateTime _date;
  /// <summary>режим работу формы</summary>
  private SendCopiesForm.Mode _mode = SendCopiesForm.Mode.SendToSubscriber;
  /// <summary>список копий документа, из которых можно выбирать</summary>
  private List<long> _enabledCopiesIDs = new List<long>();
  /// <summary>список альбомов, доступных для выбора</summary>
  private List<long> _albumIDs = new List<long>();
  /// <summary>список копий документа которые нужно разослать</summary>
  private List<long> _copiesIDs = new List<long>();
  /// <summary>Все копии выделенных документов</summary>
  private List<CopyNodeInfo> _allCopies = new List<CopyNodeInfo>();
  /// <summary>список абонентов, которые добавлены в лист рассылки</summary>
  private List<long> _subscribers = new List<long>();
  /// <summary>
  ///  сколько копий документа уже выслано абоненту
  /// (для реализации 1132372)
  /// </summary>
  private int _rCount;
  /// <summary>
  ///  сколько копий документа нужно выслать абоненту
  /// (для реализации 1132372)
  /// </summary>
  private int _nCount;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Label label1;
  private Label label2;
  private Label label3;
  private Label label4;
  private Label label5;
  private ButtonEdit beSubscribers;
  private ButtonEdit beRecipient;
  private DateTimePicker dtDate;
  private ButtonEdit beAlbum;
  private ListBox lbCopies;
  private Button btnOK;
  private Button btnCancel;
  private Button btnAdd;
  private Button btnDelete;
  private ToolTip toolTip1;
  private PictureBox pbWarning;
  private TextBox lbInfo;

  /// <summary>
  /// id версии копии, который запишем в Актульную копию для листа рассылки
  /// </summary>
  public long ActualCopyID
  {
    get => this._actualCopyID;
    set => this._actualCopyID = value;
  }

  /// <summary>
  /// заголовок версии копии, который запишем в Актульную копию для листа рассылки
  /// используется только при вызове формы с вкладки Лист рассылки для заполнения новой информацией
  /// </summary>
  public string ActualCopyCaption
  {
    get => this._actualCopyCaption;
    set => this._actualCopyCaption = value;
  }

  /// <summary>
  /// Выбрать копии, которые будут высланы указанному абоненту
  /// </summary>
  /// <param name="deliveryListID">id листа рассылки</param>
  /// <param name="subscrID">id выбранного абонента</param>
  /// <param name="id">id объекта, для которого высылаются копия </param>
  /// <param name="count"> сколько копій нужно выслать абоненту</param>
  public SendCopiesForm(long deliveryListID, long subscrID, long id, int count)
  {
    this.InitializeComponent();
    this.dtDate.Value = this._date = DateTime.Now;
    this._deliveryListIDs = new List<long>()
    {
      deliveryListID
    };
    this._mode = SendCopiesForm.Mode.SendToSubscriber;
    this._subscrID = subscrID;
    this.SetSubscriberAndRecipient();
    if (subscrID != 0L)
      this.beSubscribers.Enabled = false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DataTable dataTable = sessionKeeper.Session.ObjectsSelect(ConstsHolder.CopyOfDocumentID, new DBRecordSetParams(new ConditionStructure[3]
      {
        new ConditionStructure(-4, RelationalOperators.Equal, (object) ConstsHolder.SendLCStepID, LogicalOperators.AND, 0, false),
        new ConditionStructure(ConstsHolder.AlbumSubscriberID, RelationalOperators.Equal, (object) subscrID, (object) null, LogicalOperators.AND, 0, false, AttributeSourceTypes.Auto, ColumnContents.ID),
        new ConditionStructure(ConstsHolder.OriginalObjectID, RelationalOperators.Equal, (object) id, LogicalOperators.NONE, 0, false)
      }, (object[]) null)
      {
        RecordCount = 0
      });
      this._rCount = dataTable == null || dataTable.Rows.Count != 1 ? 0 : Convert.ToInt32(dataTable.Rows[0][0]);
      this._nCount = count;
      this.pbWarning.Visible = this.lbInfo.Visible = true;
      this.lbInfo.Text = string.Format(ServiceHolder.rm.GetString("Archives_153"), (object) this._nCount);
      this.lbInfo.Text += string.Format(ServiceHolder.rm.GetString("Archives_154"), (object) this._rCount);
    }
    this.FindEnabledCopies(id);
    this.CreateSubscribersDescriptions();
    this.CreateAlbumDescriptions();
    this.UpdateControls();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 2650);
  }

  /// <summary>
  /// Выбрать абонентов, которым будут высланы указанные копии
  /// </summary>
  /// <param name="subscrID">ИД абонента</param>
  /// <param name="docID">ИД документа, для которого вызвана вкладка. для нескольких документов не задается.</param>
  /// <param name="deliveryListIDs">id листов рассылки</param>
  /// <param name="allCopies">все копии из вкладки Копии документов</param>
  /// <param name="copiesForSending">копии документов, которые нужно выслать</param>
  public SendCopiesForm(
    List<CopyNodeInfo> allCopies,
    List<CopyNodeInfo> copiesForSending,
    long subscrID,
    List<long> deliveryListIDs,
    long docID = 0)
  {
    this.InitializeComponent();
    this._allCopies = allCopies;
    this.dtDate.Value = this._date = DateTime.Now;
    this._deliveryListIDs = deliveryListIDs;
    this._subscrID = subscrID;
    this._mode = SendCopiesForm.Mode.SendCopies;
    this.SetSubscriberAndRecipient();
    if (subscrID != 0L)
      this.beSubscribers.Enabled = false;
    for (int index = 0; index < copiesForSending.Count; ++index)
    {
      this._copiesIDs.Add(copiesForSending[index].CopyObjectID);
      this.lbCopies.Items.Add((object) new MyElement((object) copiesForSending[index].CopyObjectID, copiesForSending[index].СopyCaption, (object) null));
    }
    if (docID != 0L)
      this.FindEnabledCopies(docID);
    else
      this.FindEnabledCopies(this._allCopies);
    this.CreateAlbumDescriptions();
    this.CreateSubscribersDescriptions();
    this.lbCopies.Height += 45;
    this.UpdateControls();
  }

  /// <summary>Устанавливает абонента и получателя копий.</summary>
  private void SetSubscriberAndRecipient()
  {
    if (this._subscrID == 0L)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this._subscrID, false);
      if (dbObject == null)
        return;
      this.beSubscribers.Text = dbObject.Caption;
      this._recipientID = 0L;
      this.beRecipient.Text = string.Empty;
      if (dbObject.ObjectType == ConstsHolder.UsersTypeID)
      {
        this._recipientID = this._subscrID;
        this.beRecipient.Text = dbObject.Caption;
      }
      else
      {
        if (!MetaDataHelper.IsObjectTypeChildOf(dbObject.ObjectType, ConstsHolder.OrganizationUnitsTypeID) && dbObject.ObjectType != ConstsHolder.SitesTypeID)
          return;
        IDBAttribute attributeById = dbObject.GetAttributeByID(ConstsHolder.RecipientID);
        if (attributeById == null || attributeById.IsNull)
          return;
        this._recipientID = attributeById.AsInteger;
        this.beRecipient.Text = attributeById.Description;
      }
    }
  }

  /// <summary>
  /// Ищет доступные копии для нескольких выделенных документов
  /// </summary>
  /// <param name="allCopies">Все копии документов</param>
  private void FindEnabledCopies(List<CopyNodeInfo> allCopies)
  {
    this._enabledCopiesIDs = (this._subscrID != 0L ? (IEnumerable<CopyNodeInfo>) allCopies.Where<CopyNodeInfo>((System.Func<CopyNodeInfo, bool>) (copy =>
    {
      if (copy.LСStepID != ConstsHolder.CreateLCStepID)
        return false;
      return copy.SubscriberID == this._subscrID || copy.SubscriberID == 0L;
    })).ToList<CopyNodeInfo>() : (IEnumerable<CopyNodeInfo>) allCopies.Where<CopyNodeInfo>((System.Func<CopyNodeInfo, bool>) (copy => copy.LСStepID == ConstsHolder.CreateLCStepID && copy.SubscriberID == this._subscrID)).ToList<CopyNodeInfo>()).Select<CopyNodeInfo, long>((System.Func<CopyNodeInfo, long>) (copy => copy.CopyObjectID)).ToList<long>();
  }

  /// <summary>Ищет доступные копии для одного документа</summary>
  /// <param name="docID"> ID документа (не версии!)</param>
  private void FindEnabledCopies(long docID)
  {
    this._enabledCopiesIDs = new List<long>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DBRecordSetParams dbRecordSetParams = new DBRecordSetParams(new ConditionStructure[4]
      {
        new ConditionStructure(-4, RelationalOperators.Equal, (object) ConstsHolder.CreateLCStepID, LogicalOperators.AND, 0, false),
        new ConditionStructure(ConstsHolder.OriginalObjectID, RelationalOperators.Equal, (object) docID, LogicalOperators.AND, 0, false),
        new ConditionStructure(ConstsHolder.AlbumSubscriberID, RelationalOperators.Equal, (object) this._subscrID, LogicalOperators.OR, 1, false),
        new ConditionStructure(ConstsHolder.AlbumSubscriberID, RelationalOperators.Empty, (object) null, LogicalOperators.NONE, -1, false)
      }, new object[1]{ (object) -2 });
      DataTable dataTable = sessionKeeper.Session.ObjectsSelect(ConstsHolder.CopyOfDocumentID, dbRecordSetParams);
      if (dataTable == null || dataTable.Rows.Count == 0)
        return;
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        this._enabledCopiesIDs.Add(Convert.ToInt64(row[0]));
    }
  }

  /// <summary>Загрузка</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void SendCopiesForm_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  /// <summary>Сохранение</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void SendCopiesForm_FormClosing(object sender, FormClosingEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }

  /// <summary>Обновить состояние контролов</summary>
  public void UpdateControls()
  {
    this.btnDelete.Enabled = this.lbCopies.SelectedIndex != -1;
    this.beAlbum.Enabled = this._albumIDs != null && this._albumIDs.Count > 0;
    if (this._albumIDs.Count != 0)
      return;
    this.beAlbum.Text = string.Empty;
    this._albumID = 0L;
  }

  /// <summary>выслать копии</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnOK_Click(object sender, EventArgs e)
  {
    if (this._subscrID == 0L)
    {
      int num1 = (int) IMMessageBox.Show(ServiceHolder.rm.GetString("Archives_111"), ServiceHolder.rm.GetString("Archives_115"), MessageBoxButtonsAdv.OK, IMMessageBoxImage.Warning);
    }
    else if (this._recipientID == 0L)
    {
      int num2 = (int) IMMessageBox.Show(ServiceHolder.rm.GetString("Archives_111"), ServiceHolder.rm.GetString("Archives_116"), MessageBoxButtonsAdv.OK, IMMessageBoxImage.Warning);
    }
    else if (this._copiesIDs.Count == 0)
    {
      int num3 = (int) IMMessageBox.Show(ServiceHolder.rm.GetString("Archives_111"), ServiceHolder.rm.GetString("Archives_117"), MessageBoxButtonsAdv.OK, IMMessageBoxImage.Warning);
    }
    else
    {
      if (this._mode == SendCopiesForm.Mode.SendToSubscriber)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDocumentCopyService customService1 = sessionKeeper.Session.GetCustomService(typeof (IDocumentCopyService)) as IDocumentCopyService;
          ICopiesService customService2 = sessionKeeper.Session.GetCustomService(typeof (ICopiesService)) as ICopiesService;
          if (customService1 == null || customService2 == null)
            return;
          customService1.SendCopies(this._subscrID, this._recipientID, this._deliveryListIDs[0], this._copiesIDs, this._date, this._albumID, (object) sessionKeeper.Session.SessionGUID);
          this._actualCopyID = this._copiesIDs[0];
          this._actualCopyCaption = sessionKeeper.Session.GetObject(this._actualCopyID).NameInMessages;
        }
      }
      else
      {
        Dictionary<long, List<CopyNodeInfo>> copiesForDocs = new Dictionary<long, List<CopyNodeInfo>>();
        foreach (long copiesId in this._copiesIDs)
        {
          long copyID = copiesId;
          CopyNodeInfo copyNodeInfo = this._allCopies.Where<CopyNodeInfo>((System.Func<CopyNodeInfo, bool>) (copy => copy.CopyObjectID == copyID)).ToList<CopyNodeInfo>()[0];
          long docId = copyNodeInfo.DocID;
          List<CopyNodeInfo> copyNodeInfoList;
          if (copiesForDocs.TryGetValue(docId, out copyNodeInfoList))
            copyNodeInfoList.Add(copyNodeInfo);
          else
            copiesForDocs.Add(docId, new List<CopyNodeInfo>()
            {
              copyNodeInfo
            });
        }
        string warningMessageText = this.GetWarningMessageText(copiesForDocs);
        if (warningMessageText != string.Empty)
        {
          using (SendingCopyWarningForm sendingCopyWarningForm = new SendingCopyWarningForm())
          {
            sendingCopyWarningForm.MessageText = warningMessageText;
            int num4 = (int) sendingCopyWarningForm.ShowDialog();
            if (sendingCopyWarningForm.DialogResult == DialogResult.Cancel)
              return;
          }
        }
        using (SessionKeeper sk = new SessionKeeper())
        {
          IDocumentCopyService customService3 = sk.Session.GetCustomService(typeof (IDocumentCopyService)) as IDocumentCopyService;
          ICopiesService customService4 = sk.Session.GetCustomService(typeof (ICopiesService)) as ICopiesService;
          if (customService3 == null || customService4 == null)
            throw new KernelException("Не найдена серверная служба работы с копиями.");
          foreach (KeyValuePair<long, List<CopyNodeInfo>> keyValuePair in copiesForDocs)
          {
            long deliveryListId = customService4.GetDeliveryListID(sk.Session.SessionGUID, keyValuePair.Key);
            foreach (CopyNodeInfo copyNodeInfo in keyValuePair.Value)
            {
              try
              {
                DocumentCommandsProvider.CheckUsersRightsForDocRegistration(sk, copyNodeInfo.DocObjectID);
              }
              catch (AccessDeniedException ex)
              {
                AccessDeniedExceptionForm.OnExceptionHandler((object) null, new Intermech.Interfaces.ExceptionEventArgs((Exception) ex));
                continue;
              }
              IDocumentCopyService documentCopyService = customService3;
              long subscrId = this._subscrID;
              long recipientId = this._recipientID;
              long listID = deliveryListId;
              List<long> copiesID = new List<long>();
              copiesID.Add(copyNodeInfo.CopyObjectID);
              DateTime date = this._date;
              long albumId = this._albumID;
              // ISSUE: variable of a boxed type
              __Boxed<Guid> sessionGuid = (System.ValueType) sk.Session.SessionGUID;
              documentCopyService.SendCopies(subscrId, recipientId, listID, copiesID, date, albumId, (object) sessionGuid);
            }
          }
        }
      }
      this.DialogResult = DialogResult.OK;
    }
  }

  private string GetWarningMessageText(Dictionary<long, List<CopyNodeInfo>> copiesForDocs)
  {
    string empty = string.Empty;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetCustomService(typeof (ICopiesService)) is ICopiesService customService))
        throw new KernelException("Не найден сервис для работы с копиями.");
      Dictionary<long, int> copiesForDocsCount = new Dictionary<long, int>();
      foreach (KeyValuePair<long, List<CopyNodeInfo>> copiesForDoc in copiesForDocs)
        copiesForDocsCount.Add(copiesForDoc.Key, copiesForDoc.Value.Count);
      return customService.GetWarningAboutExceededCopies(copiesForDocsCount, this._subscrID, sessionKeeper.Session.SessionGUID);
    }
  }

  /// <summary>Добавить копию</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnAdd_Click(object sender, EventArgs e)
  {
    IDescriptor rootDescriptor = (IDescriptor) new ListDescriptor(Intermech.Navigator.Consts.CategorySelectObjectsNode, ConstsHolder.CopyOfDocumentID, ServiceHolder.rm.GetString("Archives_99"), (IList) this._enabledCopiesIDs);
    object[] objArray = SelectionWindow.Select(ServiceHolder.rm.GetString("Archives_118"), rootDescriptor, typeof (IDBTypedObjectID), SelectionOptions.SelectObjects | SelectionOptions.ForceRebuildNavTree);
    if (objArray == null || objArray.Length == 0)
      return;
    foreach (object obj in objArray)
    {
      IDBTypedObjectID dbTypedObjectId = obj as IDBTypedObjectID;
      if (!this._copiesIDs.Contains(dbTypedObjectId.ObjectID))
      {
        this.lbCopies.Items.Add((object) new MyElement((object) dbTypedObjectId.ObjectID, dbTypedObjectId.Caption, (object) null));
        this._copiesIDs.Add(dbTypedObjectId.ObjectID);
      }
    }
  }

  /// <summary>Удалить выбранную копию</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnDelete_Click(object sender, EventArgs e)
  {
    MyElement selectedItem = this.lbCopies.SelectedItem as MyElement;
    if (this._copiesIDs.Contains(Convert.ToInt64(selectedItem.Value)))
      this._copiesIDs.Remove(Convert.ToInt64(selectedItem.Value));
    this.lbCopies.Items.Remove((object) selectedItem);
  }

  /// <summary>изменилась выбранная копия</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void lbCopies_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.btnDelete.Enabled = this.lbCopies.SelectedIndex != -1;
  }

  /// <summary>изменилась дата</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void dtDate_ValueChanged(object sender, EventArgs e) => this._date = this.dtDate.Value;

  /// <summary>выбрать альбом пользователя</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void beAlbum_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    IDescriptor rootDescriptor = (IDescriptor) new ListDescriptor(Intermech.Navigator.Consts.CategoryVersionsObjectNode, ConstsHolder.DocAlbumID, ServiceHolder.rm.GetString("Archives_151"), (IList) this._albumIDs);
    object[] objArray = SelectionWindow.Select(ServiceHolder.rm.GetString("Archives_119"), rootDescriptor, typeof (IDBTypedObjectID), SelectionOptions.SelectObjects | SelectionOptions.DisableSelectFromViews | SelectionOptions.DisableMultiselect);
    if (objArray == null || objArray.Length != 1)
      return;
    IDBTypedObjectID dbTypedObjectId = objArray[0] as IDBTypedObjectID;
    this._albumID = dbTypedObjectId.ObjectID;
    this.beAlbum.Text = dbTypedObjectId.Caption;
  }

  /// <summary>Выбрать получателя копии</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void beRecipient_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    DescriptorCollection descriptors = new DescriptorCollection();
    Dictionary<int, List<long>> objectIDs = new Dictionary<int, List<long>>();
    if (this._subscribers.Count != 0)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        foreach (long subscriber in this._subscribers)
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(subscriber, false);
          if (dbObject != null)
          {
            List<long> longList;
            if (objectIDs.TryGetValue(dbObject.ObjectType, out longList))
              longList.Add(subscriber);
            else
              objectIDs.Add(dbObject.ObjectType, new List<long>()
              {
                subscriber
              });
          }
        }
      }
    }
    descriptors.Add((IDescriptor) new DictDescriptor(Intermech.Navigator.Consts.CategoryAllObjectTypes, 0, ServiceHolder.rm.GetString("Archives_152"), objectIDs));
    descriptors.Add((IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(MetaDataHelper.GetObjectTypeID("cad00002-306c-11d8-b4e9-00304f19f545")));
    Intermech.Navigator.CustomNode.Descriptor rootDescriptor = new Intermech.Navigator.CustomNode.Descriptor(ServiceHolder.rm.GetString("Archives_120"), descriptors);
    object[] objArray = SelectionWindow.Select(ServiceHolder.rm.GetString("Archives_121"), (IDescriptor) rootDescriptor, typeof (IDBTypedObjectID), SelectionOptions.SelectObjects | SelectionOptions.DisableSelectFromTree | SelectionOptions.DisableMultiselect, new int[1]
    {
      MetaDataHelper.GetObjectTypeID("cad00002-306c-11d8-b4e9-00304f19f545")
    });
    if (objArray == null || objArray.Length != 1)
      return;
    IDBTypedObjectID dbTypedObjectId = objArray[0] as IDBTypedObjectID;
    this._recipientID = dbTypedObjectId.ObjectID;
    this.beRecipient.Text = dbTypedObjectId.Caption;
  }

  /// <summary>Выбрать абонента</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void beSubscribers_Click(object sender, EventArgs e)
  {
    DescriptorCollection descriptors = new DescriptorCollection();
    if (this._subscribers.Count != 0)
    {
      Dictionary<int, List<long>> objectIDs = new Dictionary<int, List<long>>();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        foreach (long subscriber in this._subscribers)
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(subscriber);
          int parentObjectTypeId = MetaDataHelper.GetTopParentObjectTypeID(dbObject.TypeID);
          List<long> longList = (List<long>) null;
          if (!objectIDs.TryGetValue(parentObjectTypeId, out longList))
          {
            longList = new List<long>();
            objectIDs.Add(parentObjectTypeId, longList);
          }
          longList.Add(dbObject.ObjectID);
        }
      }
      IDescriptor descriptor = (IDescriptor) new DictDescriptor(Intermech.Navigator.Consts.CategoryAllObjectTypes, -1, ServiceHolder.rm.GetString("Archives_152"), objectIDs);
      descriptors.Add(descriptor);
    }
    descriptors.Add((IDescriptor) new OrganizationalUnitsDescriptor());
    descriptors.Add((IDescriptor) new UsersGroupsDescriptor());
    int objectTypeId = MetaDataHelper.GetObjectTypeID(PortalConsts.objtypeSites);
    if (objectTypeId != -1)
      descriptors.Add((IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(objectTypeId));
    Intermech.Navigator.CustomNode.Descriptor rootDescriptor = new Intermech.Navigator.CustomNode.Descriptor(ServiceHolder.rm.GetString("Archives_120"), descriptors);
    object[] objArray = SelectionWindow.Select(ServiceHolder.rm.GetString("Archives_122"), (IDescriptor) rootDescriptor, typeof (IDBTypedObjectID), SelectionOptions.SelectObjects | SelectionOptions.DisableMultiselect);
    if (objArray == null || objArray.Length != 1)
      return;
    this._subscrID = (objArray[0] as IDBTypedObjectID).ObjectID;
    this.SetSubscriberAndRecipient();
    this.CreateAlbumDescriptions();
    this.UpdateControls();
  }

  /// <summary>формируем дескрипторы для выбора абонента</summary>
  private void CreateSubscribersDescriptions()
  {
    this._subscribers.Clear();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (long deliveryListId in this._deliveryListIDs)
      {
        IDBAttribute objectAttributeById = sessionKeeper.Session.GetObjectAttributeByID(deliveryListId, ConstsHolder.SubscribersID);
        if (objectAttributeById != null)
        {
          for (int index = 0; index < objectAttributeById.ValuesCount; ++index)
          {
            if (objectAttributeById.Values[index] != DBNull.Value && objectAttributeById.Values[index] != null && !(objectAttributeById.Values[index].ToString() == string.Empty))
            {
              long int64 = Convert.ToInt64(objectAttributeById.Values[index]);
              if (!this._subscribers.Contains(int64))
                this._subscribers.Add(int64);
            }
          }
        }
      }
    }
  }

  /// <summary>формируем дескрипторы для выбора альбома абонента</summary>
  private void CreateAlbumDescriptions()
  {
    if (this._subscrID == 0L)
      return;
    this._albumIDs = new List<long>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DBRecordSetParams dbRecordSetParams = new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(ConstsHolder.AlbumSubscriberID, RelationalOperators.Equal, (object) this._subscrID, (object) null, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Auto, ColumnContents.ID)
      }, new object[1]{ (object) -2 });
      foreach (DataRow row in (InternalDataCollectionBase) sessionKeeper.Session.ObjectsSelect(ConstsHolder.DocAlbumID, dbRecordSetParams).Rows)
        this._albumIDs.Add(Convert.ToInt64(row[0]));
    }
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SendCopiesForm));
    this.label1 = new Label();
    this.label2 = new Label();
    this.label3 = new Label();
    this.label4 = new Label();
    this.label5 = new Label();
    this.beSubscribers = new ButtonEdit();
    this.beRecipient = new ButtonEdit();
    this.dtDate = new DateTimePicker();
    this.beAlbum = new ButtonEdit();
    this.lbCopies = new ListBox();
    this.btnOK = new Button();
    this.btnCancel = new Button();
    this.btnAdd = new Button();
    this.btnDelete = new Button();
    this.toolTip1 = new ToolTip();
    this.pbWarning = new PictureBox();
    this.lbInfo = new TextBox();
    this.beSubscribers.Properties.BeginInit();
    this.beRecipient.Properties.BeginInit();
    this.beAlbum.Properties.BeginInit();
    ((ISupportInitialize) this.pbWarning).BeginInit();
    this.SuspendLayout();
    this.label1.AutoSize = true;
    this.label1.Location = new Point(12, 12);
    this.label1.Name = "label1";
    this.label1.Size = new Size(49, 13);
    this.label1.TabIndex = 0;
    this.label1.Text = "Абонент";
    this.label2.AutoSize = true;
    this.label2.Location = new Point(12, 60);
    this.label2.Name = "label2";
    this.label2.Size = new Size(99, 13);
    this.label2.TabIndex = 1;
    this.label2.Text = "Получатель копии";
    this.label3.AutoSize = true;
    this.label3.Location = new Point(12, 204);
    this.label3.Name = "label3";
    this.label3.Size = new Size(166, 13);
    this.label3.TabIndex = 2;
    this.label3.Text = "Высылаемые копии документа";
    this.label4.AutoSize = true;
    this.label4.Location = new Point(12, 108);
    this.label4.Name = "label4";
    this.label4.Size = new Size(121, 13);
    this.label4.TabIndex = 3;
    this.label4.Text = "Дата получения копии";
    this.label5.AutoSize = true;
    this.label5.Location = new Point(12, 156);
    this.label5.Name = "label5";
    this.label5.Size = new Size(109, 13);
    this.label5.TabIndex = 4;
    this.label5.Text = "Альбом документов";
    this.beSubscribers.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.beSubscribers.EditValue = (object) "";
    this.beSubscribers.Location = new Point(15, 31 /*0x1F*/);
    this.beSubscribers.Name = "beSubscribers";
    this.beSubscribers.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.beSubscribers.Properties.ReadOnly = true;
    this.beSubscribers.Size = new Size(301, 20);
    this.beSubscribers.TabIndex = 5;
    this.beSubscribers.Click += new EventHandler(this.beSubscribers_Click);
    this.beRecipient.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.beRecipient.EditValue = (object) "";
    this.beRecipient.Location = new Point(15, 79);
    this.beRecipient.Name = "beRecipient";
    this.beRecipient.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.beRecipient.Properties.ReadOnly = true;
    this.beRecipient.Size = new Size(301, 20);
    this.beRecipient.TabIndex = 6;
    this.beRecipient.ButtonClick += new ButtonPressedEventHandler(this.beRecipient_ButtonClick);
    this.dtDate.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.dtDate.Location = new Point(15, (int) sbyte.MaxValue);
    this.dtDate.Name = "dtDate";
    this.dtDate.Size = new Size(301, 20);
    this.dtDate.TabIndex = 7;
    this.dtDate.ValueChanged += new EventHandler(this.dtDate_ValueChanged);
    this.beAlbum.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.beAlbum.EditValue = (object) "";
    this.beAlbum.Location = new Point(15, 175);
    this.beAlbum.Name = "beAlbum";
    this.beAlbum.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.beAlbum.Properties.ReadOnly = true;
    this.beAlbum.Size = new Size(301, 20);
    this.beAlbum.TabIndex = 8;
    this.beAlbum.ButtonClick += new ButtonPressedEventHandler(this.beAlbum_ButtonClick);
    this.lbCopies.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.lbCopies.FormattingEnabled = true;
    this.lbCopies.Location = new Point(15, 250);
    this.lbCopies.Name = "lbCopies";
    this.lbCopies.Size = new Size(301, 56);
    this.lbCopies.TabIndex = 9;
    this.lbCopies.SelectedIndexChanged += new EventHandler(this.lbCopies_SelectedIndexChanged);
    this.btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnOK.Location = new Point(131, 364);
    this.btnOK.Name = "btnOK";
    this.btnOK.Size = new Size(90, 27);
    this.btnOK.TabIndex = 10;
    this.btnOK.Text = "ОК";
    this.btnOK.UseVisualStyleBackColor = true;
    this.btnOK.Click += new EventHandler(this.btnOK_Click);
    this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Location = new Point(227, 364);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(90, 27);
    this.btnCancel.TabIndex = 11;
    this.btnCancel.Text = "Отмена";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.btnAdd.Image = (Image) componentResourceManager.GetObject("btnAdd.Image");
    this.btnAdd.ImageAlign = ContentAlignment.BottomCenter;
    this.btnAdd.Location = new Point(14, 222);
    this.btnAdd.Name = "btnAdd";
    this.btnAdd.Size = new Size(24, 24);
    this.btnAdd.TabIndex = 12;
    this.toolTip1.SetToolTip((Control) this.btnAdd, "Добавить копию документа");
    this.btnAdd.UseVisualStyleBackColor = true;
    this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
    this.btnDelete.Enabled = false;
    this.btnDelete.Image = (Image) componentResourceManager.GetObject("btnDelete.Image");
    this.btnDelete.ImageAlign = ContentAlignment.BottomCenter;
    this.btnDelete.Location = new Point(44, 222);
    this.btnDelete.Name = "btnDelete";
    this.btnDelete.Size = new Size(24, 24);
    this.btnDelete.TabIndex = 13;
    this.toolTip1.SetToolTip((Control) this.btnDelete, "Удалить копию документа");
    this.btnDelete.UseVisualStyleBackColor = true;
    this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
    this.pbWarning.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.pbWarning.Image = (Image) componentResourceManager.GetObject("pbWarning.Image");
    this.pbWarning.Location = new Point(18, 313);
    this.pbWarning.Name = "pbWarning";
    this.pbWarning.Size = new Size(22, 28);
    this.pbWarning.TabIndex = 15;
    this.pbWarning.TabStop = false;
    this.pbWarning.Visible = false;
    this.lbInfo.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.lbInfo.Location = new Point(45, 313);
    this.lbInfo.Multiline = true;
    this.lbInfo.Name = "lbInfo";
    this.lbInfo.ReadOnly = true;
    this.lbInfo.ScrollBars = ScrollBars.Vertical;
    this.lbInfo.Size = new Size(271, 41);
    this.lbInfo.TabIndex = 16 /*0x10*/;
    this.lbInfo.TabStop = false;
    this.lbInfo.Visible = false;
    this.AcceptButton = (IButtonControl) this.btnOK;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.ClientSize = new Size(328, 402);
    this.Controls.Add((Control) this.lbInfo);
    this.Controls.Add((Control) this.pbWarning);
    this.Controls.Add((Control) this.btnDelete);
    this.Controls.Add((Control) this.btnAdd);
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.btnOK);
    this.Controls.Add((Control) this.lbCopies);
    this.Controls.Add((Control) this.beAlbum);
    this.Controls.Add((Control) this.dtDate);
    this.Controls.Add((Control) this.beRecipient);
    this.Controls.Add((Control) this.beSubscribers);
    this.Controls.Add((Control) this.label5);
    this.Controls.Add((Control) this.label4);
    this.Controls.Add((Control) this.label3);
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this.label1);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.MinimumSize = new Size(336, 440);
    this.Name = nameof (SendCopiesForm);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Выслать копии";
    this.FormClosing += new FormClosingEventHandler(this.SendCopiesForm_FormClosing);
    this.Load += new EventHandler(this.SendCopiesForm_Load);
    this.beSubscribers.Properties.EndInit();
    this.beRecipient.Properties.EndInit();
    this.beAlbum.Properties.EndInit();
    ((ISupportInitialize) this.pbWarning).EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private enum Mode
  {
    /// <summary>
    /// Выслать указанные копии абонентам
    /// (при выборе копий на закладке Копии документа)
    /// </summary>
    SendCopies,
    /// <summary>
    /// Выслать указанному абоненту копии
    /// (при выборе абонента на закладке Лист рассылки)
    /// </summary>
    SendToSubscriber,
  }
}
