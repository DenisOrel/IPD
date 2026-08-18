
// Type: Intermech.Client.Core.CompositionView.CVLocalButton
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Client.Core.CompositionView;

/// <summary>Общий класс для локальных кнопок</summary>
[Serializable]
public class CVLocalButton
{
  /// <summary>
  /// Поиск предыдущих и следующих объектов/связей относительно узла навигатора
  /// </summary>
  /// <param name="node"></param>
  /// <param name="prevObjectId"></param>
  /// <param name="prevRelationId"></param>
  /// <param name="nextObjectId"></param>
  /// <param name="nextRelationId"></param>
  internal static void GetSiblingObjects(
    NavigatorTreeNode node,
    out IDBTypedObjectID prevObjectId,
    out IDBRelationID prevRelationId,
    out IDBTypedObjectID nextObjectId,
    out IDBRelationID nextRelationId)
  {
    nextObjectId = (IDBTypedObjectID) null;
    prevObjectId = (IDBTypedObjectID) null;
    nextRelationId = (IDBRelationID) null;
    prevRelationId = (IDBRelationID) null;
    NavigatorTreeNode parent = node.Parent;
    if (parent == null)
      return;
    int num = parent.Children.IndexOf(node);
    NavigatorTreeNode navigatorTreeNode;
    switch (num)
    {
      case -1:
        return;
      case 0:
        navigatorTreeNode = (NavigatorTreeNode) null;
        break;
      default:
        navigatorTreeNode = parent.Children[num - 1];
        break;
    }
    NavigatorTreeNode node1 = navigatorTreeNode;
    NavigatorTreeNode child = num == parent.Children.Count - 1 ? (NavigatorTreeNode) null : parent.Children[num + 1];
    if (node1 != null)
    {
      INode nodeHandler = node1.Tree.GetNodeHandler(node1);
      prevObjectId = (IDBTypedObjectID) nodeHandler.GetData(node1.NodeID, typeof (IDBTypedObjectID));
      prevRelationId = nodeHandler.GetData(node1.NodeID, typeof (IDBRelationID)) as IDBRelationID;
    }
    if (child == null)
      return;
    INode nodeHandler1 = child.Tree.GetNodeHandler(child);
    nextObjectId = (IDBTypedObjectID) nodeHandler1.GetData(child.NodeID, typeof (IDBTypedObjectID));
    nextRelationId = nodeHandler1.GetData(child.NodeID, typeof (IDBRelationID)) as IDBRelationID;
  }

  /// <summary>Проверка видимости команд для текущей кнопки</summary>
  /// <param name="args">Параметры команды</param>
  /// <returns></returns>
  public static CVButtonEnabled Check(CVButtonBase button, CVLocalButton.CVButtonArgs args)
  {
    CVLocalButton.CVLocalButtonAnalyzer localButtonAnalyzer = new CVLocalButton.CVLocalButtonAnalyzer();
    localButtonAnalyzer.Proceed(button, args);
    return localButtonAnalyzer.ButtonEnabled;
  }

  /// <summary>Обработка нажатия кнопки</summary>
  /// <param name="args">Параметры команды</param>
  public static void Click(CVButtonBase button, CVLocalButton.CVButtonClickArgs args)
  {
    new CVLocalButton.CVLocalButtonClickHandler().Proceed(button, (CVLocalButton.CVButtonArgs) args);
  }

  /// <summary>Проверка видимости команд для текущей кнопки</summary>
  /// <param name="button">Кнопка, для которой проверяем команды</param>
  /// <param name="sourceView"></param>
  /// <param name="compView"></param>
  /// <param name="compManager"></param>
  /// <returns></returns>
  [Obsolete("Методы будут удалены в версии 4.0")]
  public static CVButtonEnabled Check(
    CVButtonBase button,
    NavigatorTreeView sourceView,
    NavigatorTreeView compView,
    IViewsManager compManager)
  {
    if (button == null)
      throw new ArgumentNullException(nameof (button));
    List<IDBTypedObjectID> selectedItems = button.GetSelectedItems(compView, compManager);
    CVLocalButton.CVButtonArgs args = new CVLocalButton.CVButtonArgs(sourceView, selectedItems);
    return CVLocalButton.Check(button, args);
  }

  /// <summary>Обработка нажатия кнопки</summary>
  /// <param name="button">Кнопка, для которой выполняем команды</param>
  /// <param name="method"></param>
  /// <param name="sourceView"></param>
  /// <param name="compView"></param>
  /// <param name="compManager"></param>
  [Obsolete("Методы будут удалены в версии 4.0")]
  public static void Click(
    CVButtonBase button,
    CVButtonMethod method,
    NavigatorTreeView sourceView,
    NavigatorTreeView compView,
    IViewsManager compManager)
  {
    if (sourceView == null)
      return;
    CVLocalButton.Click(button, method, sourceView, sourceView.FocusedNode, compView, compManager);
  }

  /// <summary>Обработка нажатия кнопки</summary>
  /// <param name="button">Кнопка, для которой выполняем команды</param>
  /// <param name="method"></param>
  /// <param name="sourceView"></param>
  /// <param name="focusedNode"></param>
  /// <param name="compView"></param>
  /// <param name="compManager"></param>
  [Obsolete("Методы будут удалены в версии 4.0")]
  public static void Click(
    CVButtonBase button,
    CVButtonMethod method,
    NavigatorTreeView sourceView,
    NavigatorTreeNode focusedNode,
    NavigatorTreeView compView,
    IViewsManager compManager)
  {
    List<IDBTypedObjectID> selectedItems = button.GetSelectedItems(compView, compManager);
    CVLocalButton.Click(button, method, sourceView, focusedNode, selectedItems);
  }

  /// <summary>Обработка нажатия кнопки</summary>
  /// <param name="button">Кнопка, для которой выполняем команды</param>
  /// <param name="method"></param>
  /// <param name="sourceView"></param>
  /// <param name="selectedItems">Список элементов для вставки</param>
  [Obsolete("Методы будут удалены в версии 4.0")]
  public static void Click(
    CVButtonBase button,
    CVButtonMethod method,
    NavigatorTreeView sourceView,
    List<IDBTypedObjectID> selectedItems)
  {
    if (sourceView == null)
      return;
    CVLocalButton.Click(button, method, sourceView, sourceView.FocusedNode, selectedItems);
  }

  /// <summary>Обработка нажатия кнопки</summary>
  /// <param name="button">Кнопка, для которой выполняем команды</param>
  /// <param name="method"></param>
  /// <param name="sourceView"></param>
  /// <param name="focusedNode"></param>
  /// <param name="selectedItems">Список элементов для вставки</param>
  [Obsolete("Методы будут удалены в версии 4.0")]
  public static void Click(
    CVButtonBase button,
    CVButtonMethod method,
    NavigatorTreeView sourceView,
    NavigatorTreeNode focusedNode,
    List<IDBTypedObjectID> selectedItems)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      CVLocalButton.Click(button, method, sourceView, focusedNode, selectedItems, sessionKeeper.Session);
  }

  /// <summary>Обработка нажатия кнопки</summary>
  /// <param name="button">Кнопка, для которой выполняем команды</param>
  /// <param name="method"></param>
  /// <param name="sourceView"></param>
  /// <param name="focusedNode"></param>
  /// <param name="selectedItems">Список элементов для вставки</param>
  /// <param name="session"></param>
  [Obsolete("Методы будут удалены в версии 4.0")]
  public static void Click(
    CVButtonBase button,
    CVButtonMethod method,
    NavigatorTreeView sourceView,
    NavigatorTreeNode focusedNode,
    List<IDBTypedObjectID> selectedItems,
    IUserSession session)
  {
    CVLocalButton.CVButtonClickArgs cvButtonClickArgs = new CVLocalButton.CVButtonClickArgs(method, sourceView, selectedItems);
    cvButtonClickArgs.TargetTreeNode = focusedNode;
    CVLocalButton.CVButtonClickArgs args = cvButtonClickArgs;
    CVLocalButton.Click(button, args);
  }

  /// <summary>Обработка нажатия кнопки</summary>
  /// <remarks>Расчет атрибута сортировки произодиться на сервере, поэтому рекомендуется использовать медот с ITreeNode для дерева навигатора</remarks>
  /// <param name="button">Кнопка, для которой выполняем команды</param>
  /// <param name="method"></param>
  /// <param name="focusedNode"></param>
  /// <param name="selectedItems">Список элементов для вставки</param>
  /// <param name="session"></param>
  [Obsolete("Методы будут удалены в версии 4.0")]
  public static void Click(
    CVButtonBase button,
    CVButtonMethod method,
    ISelectedItems items,
    List<IDBTypedObjectID> selectedItems,
    IUserSession session)
  {
    if (button == null)
      throw new ArgumentNullException(nameof (button));
    if (items == null)
      throw new ArgumentNullException(nameof (items));
    if (selectedItems == null || selectedItems.Count == 0)
      return;
    object sender = (object) null;
    List<long> relationIDs1 = new List<long>();
    IDBTypedObjectID itemData1 = items.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    List<string> stringList = new List<string>();
    IDBTypedObjectID parentData = items.GetParentData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    IDBRelationID itemData2 = items.GetItemData(0, typeof (IDBRelationID)) as IDBRelationID;
    IDBTypedObjectID dbTypedObjectId1 = (IDBTypedObjectID) null;
    switch (method)
    {
      case CVButtonMethod.Add:
        dbTypedObjectId1 = parentData;
        break;
      case CVButtonMethod.InsertBefore:
      case CVButtonMethod.InsertInto:
      case CVButtonMethod.InsertAfter:
      case CVButtonMethod.Replace:
        dbTypedObjectId1 = itemData1;
        break;
    }
    if (dbTypedObjectId1 == null)
      return;
    Dictionary<int, List<cvRelationInfo>> possibleRelations = CompositionViewHelper.GetPossibleRelations(dbTypedObjectId1.ObjectType, true);
    switch (method)
    {
      case CVButtonMethod.Add:
      case CVButtonMethod.InsertBefore:
      case CVButtonMethod.InsertInto:
      case CVButtonMethod.InsertAfter:
        ICompositionsAutomaticSortingSession session1 = session.GetCustomService(typeof (ICompositionsAutomaticSortingService)) is ICompositionsAutomaticSortingService customService ? customService.CreateSession((object) session.SessionGUID) : (ICompositionsAutomaticSortingSession) null;
        try
        {
          session1?.PrefetchObjectComposition((IEnumerable<long>) new long[1]
          {
            dbTypedObjectId1.ObjectID
          }, (object) session.SessionGUID);
          List<long> relationIDs2 = relationIDs1;
          CompositionViewEvents.RaiseBeforeAllCreations(sender, session);
          button.DoBeforeAllCreation(dbTypedObjectId1, selectedItems, session);
          try
          {
            foreach (IDBTypedObjectID selectedItem in selectedItems)
            {
              string errorString;
              IDBObject dbObject = button.DoCreateObject(dbTypedObjectId1, selectedItem, possibleRelations, session, false, out errorString);
              if (dbObject == null)
              {
                stringList.Add($"{stringList.Count + 1}. {errorString}");
              }
              else
              {
                IDBTypedObjectID dbTypedObjectId2 = (IDBTypedObjectID) CVButtonBase.GetDBTypedObjectID(dbObject);
                cvRelationInfo cvRelationInfo = possibleRelations[dbObject.ObjectType][0];
                NewRelationProperties newRelPros = new NewRelationProperties(dbTypedObjectId1.ObjectID, dbObject.ID)
                {
                  PartObjectID = dbObject.ObjectID
                };
                IDBRelationID relation = button.DoCreateRelation(cvRelationInfo.RelationTypeID, newRelPros, dbTypedObjectId1, dbTypedObjectId2, session);
                relationIDs2.Add(relation.Value);
                if (session1 != null)
                {
                  CompositionSortingProjInfo[] relationInfo = new CompositionSortingProjInfo[1]
                  {
                    new CompositionSortingProjInfo(relation.Value, relation.RelationType, dbTypedObjectId1.ObjectID, dbTypedObjectId1.ObjectType, dbObject.ObjectType, 0L)
                  };
                  switch (method)
                  {
                    case CVButtonMethod.Add:
                    case CVButtonMethod.InsertInto:
                      session1.ProceedRelation((IEnumerable<CompositionSortingProjInfo>) relationInfo, (object) session.SessionGUID);
                      break;
                    case CVButtonMethod.InsertBefore:
                      session1.ProceedRelation((IEnumerable<CompositionSortingProjInfo>) relationInfo, CompositionTargetMode.InsertBefore, itemData2.Value, (object) session.SessionGUID);
                      break;
                    case CVButtonMethod.InsertAfter:
                      session1.ProceedRelation((IEnumerable<CompositionSortingProjInfo>) relationInfo, CompositionTargetMode.InsertAfter, itemData2.Value, (object) session.SessionGUID);
                      break;
                  }
                }
                CompositionViewEvents.RaiseCreateRelation((object) null, method, dbTypedObjectId1.ObjectID, dbObject.ObjectID, relation.Value, relationIDs2);
                button.DoCommitObject(dbObject, session);
              }
            }
          }
          finally
          {
            button.DoAfterAllCreation(session);
            CompositionViewEvents.RaiseAfterAllCreations(sender, session);
          }
          switch (method)
          {
            default:
              if (relationIDs2.Count > 0)
                CompositionViewHelper.UpdateSourceTreeView((object) null, (object) null, (NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", (IList<long>) relationIDs2));
              if (stringList.Count <= 0)
                return;
              stringList.Insert(0, LocalizationHolder.rm.GetString("Client.Core_20"));
              int num = (int) MessageBox.Show(string.Join("\r\n", stringList.ToArray()), LocalizationHolder.rm.GetString("Client.Core_21"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
              return;
          }
        }
        finally
        {
          customService?.DisposeSession((object) session.SessionGUID);
        }
      case CVButtonMethod.Replace:
        List<long> relationIDs3 = new List<long>();
        List<long> projIDs = new List<long>();
        List<int> relTypeIDs = new List<int>();
        CompositionViewEvents.RaiseBeforeAllCreations(sender, session);
        button.DoBeforeAllCreation(dbTypedObjectId1, selectedItems, session);
        try
        {
          string errorString;
          IDBObject dbObject = button.DoCreateObject(dbTypedObjectId1, selectedItems[0], possibleRelations, session, false, out errorString);
          if (dbObject == null)
          {
            stringList.Add($"{stringList.Count + 1}. {errorString}");
            break;
          }
          IDBTypedObjectID dbTypedObjectId3 = (IDBTypedObjectID) CVButtonBase.GetDBTypedObjectID(dbObject);
          IDBRelation relation1 = session.GetRelation(itemData2.Value);
          IDBRelationID relation2 = button.DoCreateRelation(relation1.RelationType, new NewRelationProperties(relation1.RelationID, dbTypedObjectId1.ObjectID, dbObject.ID)
          {
            PartObjectID = dbObject.ObjectID
          }, dbTypedObjectId1, dbTypedObjectId3, session);
          if (relation2 != null)
          {
            relationIDs1.Add(relation2.Value);
            CompositionViewEvents.RaiseCreateRelation((object) null, method, dbTypedObjectId1.ObjectID, dbObject.ObjectID, relation2.Value, relationIDs1);
            relation1.Delete(0L);
            relationIDs3.Add(relation1.RelationID);
            projIDs.Add(relation1.ProjID);
            relTypeIDs.Add(relation1.RelationType);
            button.DoCommitObject(dbObject, session);
          }
        }
        finally
        {
          button.DoAfterAllCreation(session);
          CompositionViewEvents.RaiseAfterAllCreations(sender, session);
        }
        if (relationIDs3.Count > 0)
          CompositionViewHelper.UpdateSourceTreeView((object) null, (object) null, (NotificationEventArgs) new DBRelationsEventArgs("RelationsRemoved", (IList<long>) relationIDs3, (IList<long>) projIDs, (IList<int>) null, (IList<int>) relTypeIDs));
        if (relationIDs1.Count > 0)
          CompositionViewHelper.UpdateSourceTreeView((object) null, (object) null, (NotificationEventArgs) new DBRelationsManagedEventArgs("ManagedRelationsCreated", (IList<long>) relationIDs1, true));
        if (stringList.Count <= 0)
          break;
        stringList.Insert(0, LocalizationHolder.rm.GetString("Client.Core_18"));
        int num1 = (int) MessageBox.Show(string.Join("\r\n", stringList.ToArray()), LocalizationHolder.rm.GetString("Client.Core_19"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        break;
    }
  }

  /// <summary>Класс для базовых аргументов кнопок</summary>
  public class CVButtonArgs
  {
    /// <summary>
    /// 
    /// </summary>
    protected NavigatorTreeView _targetView;
    /// <summary>
    /// 
    /// </summary>
    protected NavigatorTreeNode _targetTreeNode;
    /// <summary>
    /// 
    /// </summary>
    protected List<IDBTypedObjectID> _selectedItems;

    /// <summary>Конструктор</summary>
    public CVButtonArgs()
    {
    }

    /// <summary>Конструктор</summary>
    /// <param name="targetView"></param>
    public CVButtonArgs(NavigatorTreeView targetView)
      : this(targetView, (List<IDBTypedObjectID>) null)
    {
    }

    /// <summary>Конструктор</summary>
    /// <param name="targetView"></param>
    /// <param name="currentNode"></param>
    /// <param name="selectedItems"></param>
    public CVButtonArgs(NavigatorTreeView targetView, List<IDBTypedObjectID> selectedItems)
    {
      this._targetView = targetView;
      this._selectedItems = selectedItems;
    }

    /// <summary>
    /// 
    /// </summary>
    public NavigatorTreeView TargetView
    {
      [DebuggerStepThrough] get => this._targetView;
      [DebuggerStepThrough] protected internal set => this._targetView = value;
    }

    /// <summary>
    /// 
    /// </summary>
    public NavigatorTreeNode TargetTreeNode
    {
      get
      {
        if (this._targetTreeNode != null)
          return this._targetTreeNode;
        return this.TargetView == null ? (NavigatorTreeNode) null : this.TargetView.FocusedNode;
      }
      [DebuggerStepThrough] set => this._targetTreeNode = value;
    }

    /// <summary>
    /// 
    /// </summary>
    public List<IDBTypedObjectID> SelectedItems
    {
      [DebuggerStepThrough] get => this._selectedItems;
      [DebuggerStepThrough] protected internal set => this._selectedItems = value;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public class CVButtonClickArgs : CVLocalButton.CVButtonArgs
  {
    /// <summary>
    /// 
    /// </summary>
    protected CVButtonMethod _method;
    /// <summary>
    /// 
    /// </summary>
    protected NavWindowBase _targetWindow;

    /// <summary>Конструктор</summary>
    public CVButtonClickArgs()
    {
    }

    /// <summary>Конструктор</summary>
    /// <param name="method"></param>
    /// <param name="targetView"></param>
    public CVButtonClickArgs(CVButtonMethod method, NavigatorTreeView targetView)
      : this(method, targetView, (List<IDBTypedObjectID>) null)
    {
    }

    /// <summary>Конструктор</summary>
    /// <param name="method"></param>
    /// <param name="targetView"></param>
    /// <param name="currentNode"></param>
    /// <param name="selectedItems"></param>
    public CVButtonClickArgs(
      CVButtonMethod method,
      NavigatorTreeView targetView,
      List<IDBTypedObjectID> selectedItems)
      : base(targetView, selectedItems)
    {
      this._method = method;
    }

    /// <summary>
    /// 
    /// </summary>
    public CVButtonMethod Method
    {
      [DebuggerStepThrough] get => this._method;
      [DebuggerStepThrough] protected internal set => this._method = value;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>Окно навигатора для отправки уведомлений. Используется в случае,
    /// когда комманда выполняется для "неактивного" окна, в которое глобальные
    /// уведомления не доходят (в)</remarks>
    public NavWindowBase TargetWindow
    {
      [DebuggerStepThrough] get => this._targetWindow;
      [DebuggerStepThrough] set => this._targetWindow = value;
    }

    /// <summary>Список копируемых атрибьутов связей</summary>
    public List<int> CopyRelAttrs { get; set; }
  }

  /// <summary>
  /// 
  /// </summary>
  internal abstract class CVLocalButtonBaseHandler
  {
    /// <summary>
    /// 
    /// </summary>
    protected CVButtonBase _button;
    /// <summary>Параметры кнопки</summary>
    protected CVLocalButton.CVButtonArgs _args;
    /// <summary>
    /// 
    /// </summary>
    protected List<IDBTypedObjectID> _selectedItems;
    /// <summary>
    /// 
    /// </summary>
    protected IDBTypedObjectID _targetObjID;
    /// <summary>
    /// 
    /// </summary>
    protected IDBTypedObjectID _targetParentObjID;
    /// <summary>
    /// 
    /// </summary>
    protected IDBRelationID _targetRelationID;
    /// <summary>
    /// 
    /// </summary>
    protected Dictionary<int, List<cvRelationInfo>> _targetObjApplCache;
    /// <summary>
    /// 
    /// </summary>
    protected Dictionary<int, List<cvRelationInfo>> _targetParentObjApplCache;

    /// <summary>
    /// 
    /// </summary>
    protected virtual void ValidateArgs(CVLocalButton.CVButtonArgs args)
    {
      if (args == null)
        throw new ArgumentNullException(nameof (args));
      if (args.TargetView == null)
        throw new ArgumentNullException("args.TargetView");
    }

    /// <summary>Анализ данных комманд</summary>
    /// <returns></returns>
    protected virtual bool ValidateCommandData()
    {
      if (this._args.TargetTreeNode == null)
        return false;
      this._selectedItems = this._args.SelectedItems;
      if (this._selectedItems == null || this._selectedItems.Count == 0)
        return false;
      this._selectedItems = this._button.DoConvertTypes(this._selectedItems);
      return this._selectedItems != null && this._selectedItems.Count != 0;
    }

    /// <summary>Вызов (выполение) комманд кнопок</summary>
    protected virtual void DoProceed_Commands()
    {
      NavigatorTreeNode parent = this._args.TargetTreeNode.Parent;
      IFocusedItem focusedItem = (IFocusedItem) new FocusedItem((NodeColumn) null, this._args.TargetTreeNode.NodeID, this._args.TargetView.GetNodeIDPath(parent), this._args.TargetView.GetNodeHandler(this._args.TargetTreeNode), this._args.TargetView.Services);
      this._targetObjID = focusedItem.GetItemData(typeof (IDBTypedObjectID)) as IDBTypedObjectID;
      if (this._targetObjID == null)
        return;
      this._targetObjApplCache = CompositionViewHelper.GetPossibleRelations(this._targetObjID.ObjectType, true);
      if (parent != null)
      {
        this._targetParentObjID = focusedItem.GetParentData(typeof (IDBTypedObjectID)) as IDBTypedObjectID;
        this._targetRelationID = focusedItem.GetItemData(typeof (IDBRelationID)) as IDBRelationID;
        if (this._targetParentObjID != null)
          this._targetParentObjApplCache = CompositionViewHelper.GetPossibleRelations(this._targetParentObjID.ObjectType, true);
      }
      this.DoProceed_Commands_Internal();
    }

    /// <summary>
    /// 
    /// </summary>
    protected abstract void DoProceed_Commands_Internal();

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public virtual bool Proceed(CVButtonBase button, CVLocalButton.CVButtonArgs args)
    {
      this._button = button;
      this._args = args;
      if (button == null)
        throw new ArgumentNullException(nameof (button));
      this.ValidateArgs(args);
      if (!this.ValidateCommandData())
        return false;
      this.DoProceed_Commands();
      return true;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  internal class CVLocalButtonAnalyzer : CVLocalButton.CVLocalButtonBaseHandler
  {
    /// <summary>
    /// 
    /// </summary>
    private CVButtonEnabled _buttonEnabled;

    /// <summary>
    /// 
    /// </summary>
    private void Check_Command_Add()
    {
      if (this._targetParentObjApplCache == null)
        return;
      this._buttonEnabled.Add = CompositionViewHelper.IsObjectsCanAddToObject(this._selectedItems, this._targetParentObjApplCache);
    }

    /// <summary>
    /// 
    /// </summary>
    private void Check_Command_InsertInto()
    {
      this._buttonEnabled.InsertInto = CompositionViewHelper.IsObjectsCanAddToObject(this._selectedItems, this._targetObjApplCache) && CompositionViewHelper.IsRelationTypesInVisibleRelations(this._selectedItems, this._targetObjID.ObjectType, this._targetObjApplCache);
    }

    /// <summary>
    /// 
    /// </summary>
    private void Check_Command_Replace()
    {
      this._buttonEnabled.Replace = this._selectedItems.Count.Equals(1) && this._selectedItems[0].ObjectType.Equals(this._targetObjID.ObjectType) && !this._selectedItems[0].ObjectID.Equals(this._targetObjID.ObjectID);
    }

    /// <summary>
    /// 
    /// </summary>
    private void Check_Command_InsertByPosition()
    {
      if (this._targetParentObjID == null || this._targetRelationID == null || !this._buttonEnabled.Add || this._args.TargetView.TreeColumns.HasSortedColumns())
        return;
      cvRelationInfo cvRelationInfo = new cvRelationInfo(this._targetRelationID.RelationType, false);
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        cvRelationInfo.HasSortAttribute = sessionKeeper.Session.IdentHelper.IsSortedRelationType(this._targetRelationID.RelationType);
        if (!cvRelationInfo.HasSortAttribute)
          return;
      }
      IDBTypedObjectID prevObjectId;
      IDBRelationID prevRelationId;
      IDBTypedObjectID nextObjectId;
      IDBRelationID nextRelationId;
      CVLocalButton.GetSiblingObjects(this._args.TargetTreeNode, out prevObjectId, out prevRelationId, out nextObjectId, out nextRelationId);
      bool flag1 = true;
      bool flag2 = true;
      foreach (IDBTypedObjectID selectedItem in this._selectedItems)
      {
        if (prevObjectId != null && prevObjectId.ObjectType == this._targetObjID.ObjectType && prevObjectId.ObjectType != selectedItem.ObjectType)
          flag1 = false;
        if (nextObjectId != null && nextObjectId.ObjectType == this._targetObjID.ObjectType && nextObjectId.ObjectType != selectedItem.ObjectType)
          flag2 = false;
        if (!flag2)
        {
          if (!flag1)
            break;
        }
      }
      this._buttonEnabled.InsertBefore = flag1;
      this._buttonEnabled.InsertAfter = flag2;
      if (!flag2 && !flag1)
        return;
      this.Check_command_InsertByPosition_ByRule(prevObjectId, nextObjectId, prevRelationId, nextRelationId);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="prevObjectId"></param>
    /// <param name="nextObjectId"></param>
    /// <param name="prevRelationId"></param>
    /// <param name="nextRelationId"></param>
    private void Check_command_InsertByPosition_ByRule(
      IDBTypedObjectID prevObjectId,
      IDBTypedObjectID nextObjectId,
      IDBRelationID prevRelationId,
      IDBRelationID nextRelationId)
    {
      if (!(ServicesManager.GetService(typeof (ICurrentUserAndRole)) is ICurrentUserAndRole service))
      {
        this.Check_command_InsertByPosition_ByDbRel();
      }
      else
      {
        int index = service.Rule.IndexOfParentObjectType(this._targetParentObjID.ObjectType, true);
        if (index == -1)
          return;
        ParentObjectType parentObjectType = service.Rule.ParentObjectTypes[index];
        List<int> visibleRelations = service.Rule.GetObjectTypeVisibleRelations(this._targetParentObjID.ObjectType, true);
        int num1 = visibleRelations.IndexOf(this._targetRelationID.RelationType);
        int num2 = prevRelationId != null ? visibleRelations.IndexOf(prevRelationId.RelationType) : -1;
        int num3 = nextRelationId != null ? visibleRelations.IndexOf(nextRelationId.RelationType) : -1;
        ChildRelationType childRelationType1 = num1 != -1 ? parentObjectType[this._targetRelationID.RelationType] : (ChildRelationType) null;
        ChildRelationType childRelationType2 = num2 != -1 ? parentObjectType[prevRelationId.RelationType] : (ChildRelationType) null;
        ChildRelationType childRelationType3 = num3 != -1 ? parentObjectType[nextRelationId.RelationType] : (ChildRelationType) null;
        bool flag1 = this._buttonEnabled.InsertBefore;
        bool flag2 = this._buttonEnabled.InsertAfter;
        foreach (IDBTypedObjectID selectedItem in this._selectedItems)
        {
          cvRelationInfo cvRelationInfo = this._targetParentObjApplCache[selectedItem.ObjectType][0];
          int num4 = visibleRelations.IndexOf(cvRelationInfo.RelationTypeID);
          if (num4 > num1)
          {
            flag1 = false;
          }
          else
          {
            if (flag1 && num1 == num4)
              flag1 = childRelationType1.CompareTo(selectedItem.ObjectType, this._targetObjID.ObjectType) <= 0 & flag1;
            if (flag1 && num2 == num4)
              flag1 = childRelationType2.CompareTo(selectedItem.ObjectType, prevObjectId.ObjectType) >= 0 & flag1;
          }
          if (num4 < num1)
          {
            flag2 = false;
          }
          else
          {
            if (flag2 && num1 == num4)
              flag2 = childRelationType1.CompareTo(selectedItem.ObjectType, this._targetObjID.ObjectType) >= 0 & flag2;
            if (flag2 && num3 == num4)
              flag2 = childRelationType3.CompareTo(selectedItem.ObjectType, nextObjectId.ObjectType) <= 0 & flag2;
          }
          if (!flag2)
          {
            if (!flag1)
              break;
          }
        }
        this._buttonEnabled.InsertBefore = flag1;
        this._buttonEnabled.InsertAfter = flag2;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    private void Check_command_InsertByPosition_ByDbRel()
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBRelation relation = sessionKeeper.Session.GetRelation(this._targetRelationID.Value, false);
        if (relation == null)
          return;
        cvRelationInfo cvRelationInfo = new cvRelationInfo(this._targetRelationID.RelationType, sessionKeeper.Session.IdentHelper.IsSortedRelationType(this._targetRelationID.RelationType));
        IDBAttribute attributeById = relation.GetAttributeByID(sessionKeeper.Session.IdentHelper.SortIndexID);
        bool flag = attributeById != null && !attributeById.IsNull;
        if (flag)
        {
          foreach (IDBTypedObjectID selectedItem in this._selectedItems)
          {
            if (!this._targetParentObjApplCache[selectedItem.ObjectType].Contains(cvRelationInfo))
            {
              flag = false;
              break;
            }
          }
        }
        this._buttonEnabled.InsertBefore = flag && this._buttonEnabled.InsertBefore;
        this._buttonEnabled.InsertAfter = flag && this._buttonEnabled.InsertAfter;
      }
    }

    /// <summary>Проверка доступности комманд кнопок</summary>
    protected override void DoProceed_Commands_Internal()
    {
      this.Check_Command_Add();
      this.Check_Command_InsertInto();
      this.Check_Command_Replace();
      this.Check_Command_InsertByPosition();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override bool Proceed(CVButtonBase button, CVLocalButton.CVButtonArgs args)
    {
      this._buttonEnabled = CVButtonEnabled.Empty;
      return base.Proceed(button, args);
    }

    /// <summary>
    /// 
    /// </summary>
    public CVButtonEnabled ButtonEnabled => this._buttonEnabled;
  }

  /// <summary>Обработчик нажатия кнопки</summary>
  internal class CVLocalButtonClickHandler : CVLocalButton.CVLocalButtonBaseHandler
  {
    /// <summary>Текущее правило сортировки составов</summary>
    private CompositionsAutosortRule _userRule;
    /// <summary>Список ошибок редактирования</summary>
    private readonly List<string> _errorList = new List<string>();
    /// <summary>Список уведмления для навигатора</summary>
    private readonly List<NotificationEventArgs> _notificationList = new List<NotificationEventArgs>();

    /// <summary>Обработка комманды "Добавить"</summary>
    private void DoExecute_Add()
    {
      this.DoExecute_InsertInto(this._args.TargetTreeNode.Parent, this._targetParentObjApplCache, CVButtonMethod.Add);
    }

    /// <summary>Обработка команды "Заменить"</summary>
    private void DoExecute_Replace()
    {
      List<IDBRelationID> source1 = new List<IDBRelationID>();
      List<IDBRelationID> source2 = new List<IDBRelationID>();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IUserSession session = sessionKeeper.Session;
        CompositionViewEvents.RaiseBeforeAllCreations((object) this._args.TargetView, session);
        this._button.DoBeforeAllCreation(this._targetParentObjID, this._selectedItems, session);
        try
        {
          string errorString;
          IDBObject dbObject = this._button.DoCreateObject(this._targetParentObjID, this._selectedItems[0], this._targetParentObjApplCache, session, false, out errorString);
          if (dbObject == null)
          {
            this._errorList.Add($"{this._errorList.Count + 1}. {errorString}");
            return;
          }
          IDBTypedObjectID dbTypedObjectId = (IDBTypedObjectID) CVButtonBase.GetDBTypedObjectID(dbObject);
          IDBRelation relation1 = session.GetRelation(this._targetRelationID.Value);
          NewRelationProperties newRelPros = new NewRelationProperties(relation1.RelationID, this._targetParentObjID.ObjectID, dbObject.ID)
          {
            PartObjectID = dbObject.ObjectID
          };
          IDBRelationID relation2 = this._button.DoCreateRelation(relation1.RelationType, newRelPros, this._targetParentObjID, dbTypedObjectId, session);
          if (relation2 != null)
          {
            source2.Add(relation2);
            CompositionViewEvents.RaiseCreateRelation((object) null, (this._args as CVLocalButton.CVButtonClickArgs).Method, this._targetParentObjID.ObjectID, dbObject.ObjectID, relation2.Value, source2.Select<IDBRelationID, long>((Func<IDBRelationID, long>) (item => item.Value)).ToList<long>());
            relation1.Delete(0L);
            source1.Add(this._targetRelationID);
            this._button.DoCommitObject(dbObject, session);
          }
        }
        finally
        {
          this._button.DoAfterAllCreation(session);
          CompositionViewEvents.RaiseAfterAllCreations((object) this._args.TargetView, session);
        }
      }
      if (source1.Count > 0)
        this._notificationList.Add((NotificationEventArgs) new DBRelationsEventArgs("RelationsRemoved", (IList<long>) source1.Select<IDBRelationID, long>((Func<IDBRelationID, long>) (item => item.Value)).ToList<long>(), (IList<long>) source1.Select<IDBRelationID, long>((Func<IDBRelationID, long>) (item => item.ProjID)).ToList<long>(), (IList<int>) null, (IList<int>) source1.Select<IDBRelationID, int>((Func<IDBRelationID, int>) (item => item.RelationType)).ToList<int>()));
      if (source2.Count > 0)
        this._notificationList.Add((NotificationEventArgs) new DBRelationsManagedEventArgs("ManagedRelationsCreated", (IList<long>) source2.Select<IDBRelationID, long>((Func<IDBRelationID, long>) (item => item.Value)).ToList<long>(), true));
      if (this._errorList.Count <= 0)
        return;
      this._errorList.Insert(0, LocalizationHolder.rm.GetString("Client.Core_18"));
    }

    /// <summary>Обработка команды "Вставить в"</summary>
    private void DoExecute_InsertInto()
    {
      this.DoExecute_InsertInto(this._args.TargetTreeNode, this._targetObjApplCache, CVButtonMethod.InsertInto);
    }

    /// <summary>Обработка команды "Вставить перед"</summary>
    private void DoExecute_InsertBefore()
    {
      this.DoExecute_InsertByPosition(CVButtonMethod.InsertBefore);
    }

    /// <summary>Обработка команды "Вставить после"</summary>
    private void DoExecute_InsertAfter()
    {
      this.DoExecute_InsertByPosition(CVButtonMethod.InsertAfter);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="buttonMethod"></param>
    private void DoExecute_InsertByPosition(CVButtonMethod buttonMethod)
    {
      if (buttonMethod != CVButtonMethod.InsertBefore && buttonMethod != CVButtonMethod.InsertAfter)
        throw new ArgumentOutOfRangeException(nameof (buttonMethod));
      NavigatorTreeNode parent = this._args.TargetTreeNode.Parent;
      if (parent == null)
        return;
      int num1 = parent.Children.IndexOf(this._args.TargetTreeNode);
      CompositionSortingInfoCache<CompositionViewSortingInfoItem> sortingInfoCache = new CompositionSortingInfoCache<CompositionViewSortingInfoItem>((ICompositionSortingComparer<CompositionViewSortingInfoItem>) new CompositionSortingInfoItemComparer<CompositionViewSortingInfoItem>(this._userRule, buttonMethod == CVButtonMethod.InsertBefore ? CompositionSortingDirectionMode.Desc : CompositionSortingDirectionMode.Asc));
      int num2 = buttonMethod == CVButtonMethod.InsertBefore ? num1 - 1 : num1 + 1;
      int num3 = buttonMethod == CVButtonMethod.InsertBefore ? -1 : parent.Children.Count;
      int num4 = buttonMethod == CVButtonMethod.InsertBefore ? -1 : 1;
      for (int index = num2; index != num3; index += num4)
      {
        NavigatorTreeNode child = parent.Children[index];
        NavigatorTreeNode node = child;
        INode nodeHandler = node.Tree.GetNodeHandler(node);
        IDBRelationID data1 = nodeHandler.GetData(node.NodeID, typeof (IDBRelationID)) as IDBRelationID;
        IDBTypedObjectID data2 = nodeHandler.GetData(node.NodeID, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
        sortingInfoCache.AddItem(new CompositionViewSortingInfoItem(data1, data2, child));
      }
      List<IDBRelationID> source = new List<IDBRelationID>();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IUserSession session = sessionKeeper.Session;
        long asInteger = session.GetRelationAttributeByID(this._targetRelationID.Value, session.IdentHelper.SortIndexID).AsInteger;
        CompositionViewEvents.RaiseBeforeAllCreations((object) this._args.TargetView, session);
        this._button.DoBeforeAllCreation(this._targetParentObjID, this._selectedItems, session);
        try
        {
          Dictionary<int, int> dictionary1 = new Dictionary<int, int>(this._selectedItems.Count);
          Dictionary<int, long> dictionary2 = new Dictionary<int, long>(this._selectedItems.Count);
          foreach (IDBTypedObjectID selectedItem in this._selectedItems)
          {
            int num5;
            if (!dictionary1.TryGetValue(selectedItem.ObjectType, out num5))
              num5 = 0;
            dictionary1[selectedItem.ObjectType] = num5 + 1;
          }
          int num6 = buttonMethod == CVButtonMethod.InsertBefore ? 0 : this._selectedItems.Count - 1;
          int num7 = buttonMethod == CVButtonMethod.InsertBefore ? this._selectedItems.Count : -1;
          int num8 = buttonMethod == CVButtonMethod.InsertBefore ? 1 : -1;
          for (int index = num6; index != num7; index += num8)
          {
            IDBTypedObjectID selectedItem = this._selectedItems[index];
            string errorString;
            IDBObject dbObject = this._button.DoCreateObject(this._targetParentObjID, selectedItem, this._targetParentObjApplCache, session, false, out errorString);
            if (dbObject == null)
            {
              this._errorList.Add($"{this._errorList.Count + 1}. {errorString}");
            }
            else
            {
              IDBTypedObjectID dbTypedObjectId = (IDBTypedObjectID) CVButtonBase.GetDBTypedObjectID(dbObject);
              cvRelationInfo cvRelationInfo = this._targetParentObjApplCache[dbObject.ObjectType][0];
              NewRelationProperties newRelPros = new NewRelationProperties(this._targetParentObjID.ObjectID, dbObject.ID)
              {
                PartObjectID = dbObject.ObjectID
              };
              List<AttributeValues> attributeValuesList = new List<AttributeValues>();
              CVLocalButton.CVButtonClickArgs clickArgs = this._args as CVLocalButton.CVButtonClickArgs;
              if (clickArgs != null && clickArgs.CopyRelAttrs != null && selectedItem is IDBRelationID dbRelationId1)
              {
                IDBRelation relation = session.GetRelation(dbRelationId1.Value, false);
                if (relation != null)
                {
                  AttributeValues[] attributesValues = relation.GetAttributesValues(GetAttributeValuesModes.CheckWriteAccess | GetAttributeValuesModes.IncludeCaption);
                  attributeValuesList.AddRange(((IEnumerable<AttributeValues>) attributesValues).Where<AttributeValues>((Func<AttributeValues, bool>) (item => clickArgs.CopyRelAttrs.Contains(item.AttributeID))));
                }
              }
              long num9 = 0;
              if (cvRelationInfo.HasSortAttribute)
              {
                switch (buttonMethod)
                {
                  case CVButtonMethod.InsertBefore:
                    num9 = this.DoExecute_InsertBefore_CalcSortValue((object) selectedItem, (object) cvRelationInfo, (object) dbObject, (object) asInteger, (object) sortingInfoCache, (object) dictionary1, (object) dictionary2);
                    break;
                  case CVButtonMethod.InsertAfter:
                    num9 = this.DoExecute_InsertAfter_CalcSortValue((object) selectedItem, (object) cvRelationInfo, (object) dbObject, (object) asInteger, (object) sortingInfoCache, (object) dictionary1, (object) dictionary2);
                    break;
                }
                AttributeValues attributeValues = new AttributeValues(session.IdentHelper.SortIndexID, (object) num9);
                attributeValuesList.Add(attributeValues);
              }
              if (attributeValuesList.Count != 0)
                newRelPros.ValuesList = attributeValuesList.ToArray();
              IDBRelationID relation1 = this._button.DoCreateRelation(cvRelationInfo.RelationTypeID, newRelPros, this._targetParentObjID, dbTypedObjectId, session);
              if (relation1 != null && relation1.Value != 0L)
              {
                IDBRelationID dbRelationId2 = (IDBRelationID) new DBRelationID(relation1.Value, relation1.PartID, relation1.RelationType, num9, relation1.RelGuid, relation1.ProjID);
                if (buttonMethod == CVButtonMethod.InsertBefore)
                {
                  sortingInfoCache.AddFirstItem(new CompositionViewSortingInfoItem(dbRelationId2, dbTypedObjectId));
                  source.Add(dbRelationId2);
                }
                else
                {
                  sortingInfoCache.AddItem(new CompositionViewSortingInfoItem(dbRelationId2, dbTypedObjectId));
                  source.Insert(0, dbRelationId2);
                }
                CompositionViewEvents.RaiseCreateRelation((object) null, buttonMethod, this._targetParentObjID.ObjectID, dbObject.ObjectID, dbRelationId2.Value, source.Select<IDBRelationID, long>((Func<IDBRelationID, long>) (item => item.Value)).ToList<long>());
                this._button.DoCommitObject(dbObject, session);
              }
            }
          }
        }
        finally
        {
          this._button.DoAfterAllCreation(session);
          CompositionViewEvents.RaiseAfterAllCreations((object) this._args.TargetView, session);
        }
      }
      if (source.Count > 0)
        this._notificationList.Add((NotificationEventArgs) new DBRelationsManagedEventArgs("ManagedRelationsInsert", (IList<long>) source.Select<IDBRelationID, long>((Func<IDBRelationID, long>) (item => item.Value)).ToList<long>(), true, (object) this._args.TargetView, (object) this._args.TargetTreeNode, buttonMethod == CVButtonMethod.InsertBefore ? NodesInsertPosition.Before : NodesInsertPosition.After));
      if (this._errorList.Count <= 0)
        return;
      this._errorList.Insert(0, LocalizationHolder.rm.GetString("Client.Core_18"));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="param"></param>
    /// <returns></returns>
    private long DoExecute_InsertBefore_CalcSortValue(params object[] param)
    {
      long num1 = 0;
      if (param == null || param.Length < 7)
        return num1;
      IDBTypedObjectID dbTypedObjectId = param[0] as IDBTypedObjectID;
      cvRelationInfo cvRelationInfo = (cvRelationInfo) param[1];
      IDBObject dbObject = param[2] as IDBObject;
      long num2 = (long) param[3];
      if (!(param[4] is CompositionSortingInfoCache<CompositionViewSortingInfoItem> sortingInfoCache))
        throw new ArgumentNullException();
      if (!(param[5] is Dictionary<int, int> dictionary1))
        throw new ArgumentNullException();
      if (!(param[6] is Dictionary<int, long> dictionary2))
        throw new ArgumentNullException();
      if (!cvRelationInfo.HasSortAttribute)
        return num1;
      CompositionViewSortingInfoItem closedObjectRec = sortingInfoCache.FindClosedObjectRec(this._targetParentObjID.ObjectType, cvRelationInfo.RelationTypeID, dbObject.ObjectType, CompositionSortingLookupMode.Less);
      int num3 = dictionary1[dbTypedObjectId.ObjectType];
      bool flag = false;
      if (closedObjectRec != null)
      {
        num1 = closedObjectRec.Sorting;
        if (closedObjectRec.RelTypeID == cvRelationInfo.RelationTypeID && closedObjectRec.RelTypeID != this._targetRelationID.RelationType)
        {
          num1 += 1000000L;
          dictionary1[dbTypedObjectId.ObjectType] = num3 - 1;
        }
        else
          flag = true;
      }
      else
        flag = true;
      if (flag)
      {
        long num4;
        if (!dictionary2.TryGetValue(dbTypedObjectId.ObjectType, out num4))
        {
          num4 = (num2 - num1) / (long) (num3 + 1);
          dictionary2.Add(dbTypedObjectId.ObjectType, num4);
        }
        num1 = num2 - num4 * (long) num3;
        dictionary1[dbTypedObjectId.ObjectType] = num3 - 1;
      }
      return num1;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="param"></param>
    /// <returns></returns>
    private long DoExecute_InsertAfter_CalcSortValue(params object[] param)
    {
      long num1 = 0;
      if (param == null || param.Length < 7)
        return num1;
      IDBTypedObjectID dbTypedObjectId = param[0] as IDBTypedObjectID;
      cvRelationInfo cvRelationInfo = (cvRelationInfo) param[1];
      IDBObject dbObject = param[2] as IDBObject;
      long num2 = (long) param[3];
      CompositionSortingInfoCache<CompositionViewSortingInfoItem> sortingInfoCache = param[4] as CompositionSortingInfoCache<CompositionViewSortingInfoItem>;
      Dictionary<int, int> dictionary1 = param[5] as Dictionary<int, int>;
      Dictionary<int, long> dictionary2 = param[6] as Dictionary<int, long>;
      if (!cvRelationInfo.HasSortAttribute)
        return num1;
      CompositionViewSortingInfoItem closedObjectRec = sortingInfoCache.FindClosedObjectRec(this._targetParentObjID.ObjectType, cvRelationInfo.RelationTypeID, dbObject.ObjectType, CompositionSortingLookupMode.More);
      int num3 = dictionary1[dbTypedObjectId.ObjectType];
      long num4;
      if (closedObjectRec != null)
      {
        long sorting = closedObjectRec.Sorting;
        if (this._targetRelationID.RelationType == cvRelationInfo.RelationTypeID && this._targetRelationID.RelationType != closedObjectRec.RelTypeID)
        {
          num4 = num2 + 1000000L;
          dictionary1[dbTypedObjectId.ObjectType] = num3 - 1;
        }
        else
        {
          long num5;
          if (!dictionary2.TryGetValue(dbTypedObjectId.ObjectType, out num5))
          {
            num5 = (sorting - num2) / (long) (num3 + 1);
            if (num5 == 0L)
            {
              int num6 = sortingInfoCache.InfoItems.IndexOf(closedObjectRec);
              long num7 = sortingInfoCache.InfoItems.Count != num6 ? 1000000L / (long) (sortingInfoCache.InfoItems.Count - num6 + 1) : 1000000L;
              IUserSession session = dbObject.Session;
              for (int index = num6; index < sortingInfoCache.InfoItems.Count; ++index)
              {
                long initValue = num2 + num7 * (long) (index - num6 + 1);
                CompositionViewSortingInfoItem infoItem = sortingInfoCache.InfoItems[index];
                if (infoItem != null && infoItem.Sorting < initValue)
                {
                  IDBRelation relation = session.GetRelation(infoItem.PrjLinkID);
                  if (relation != null)
                  {
                    relation.SetAttributesValues(new AttributeValues[1]
                    {
                      new AttributeValues(session.IdentHelper.SortIndexID, (object) initValue)
                    });
                    infoItem.Sorting = initValue;
                    this._notificationList.Add((NotificationEventArgs) new DBRelationsEventArgs("RelationsChanged", relation.RelationID, this._targetParentObjID.ObjectID, this._targetParentObjID.ObjectType, relation.RelationType));
                  }
                }
                else
                  break;
              }
              dictionary2.Clear();
              num5 = num7 / (long) (num3 + 1);
            }
            dictionary2.Add(dbTypedObjectId.ObjectType, num5);
          }
          num4 = num2 + (long) num3 * num5;
          dictionary1[dbTypedObjectId.ObjectType] = num3 - 1;
        }
      }
      else
      {
        num4 = num2 + 1000000L;
        dictionary1[dbTypedObjectId.ObjectType] = num3 - 1;
      }
      return num4;
    }

    /// <summary>Реализация команд Добавить/Вставить</summary>
    /// <param name="targetTreeNode"></param>
    /// <param name="targetObjApplCache"></param>
    /// <param name="buttonMethod"></param>
    private void DoExecute_InsertInto(
      NavigatorTreeNode targetTreeNode,
      Dictionary<int, List<cvRelationInfo>> targetObjApplCache,
      CVButtonMethod buttonMethod)
    {
      if (targetTreeNode == null)
        throw new ArgumentNullException(nameof (targetTreeNode));
      if (targetObjApplCache == null)
        throw new ArgumentNullException(nameof (targetObjApplCache));
      List<long> relationIDs = new List<long>();
      Dictionary<int, TreeNodeRec> dictionary1 = new Dictionary<int, TreeNodeRec>();
      IDBTypedObjectID data1 = this._args.TargetView.GetNodeHandler(targetTreeNode).GetData(targetTreeNode.NodeID, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
      Dictionary<int, Dictionary<int, long>> dictionary2 = new Dictionary<int, Dictionary<int, long>>();
      this._args.TargetView.TreeColumns.HasSortedColumns();
      CompositionSortingInfoCache<CompositionViewSortingInfoItem> sortingInfoCache = new CompositionSortingInfoCache<CompositionViewSortingInfoItem>((ICompositionSortingComparer<CompositionViewSortingInfoItem>) new CompositionSortingInfoItemComparer<CompositionViewSortingInfoItem>(this._userRule, CompositionSortingDirectionMode.Desc));
      for (int index = targetTreeNode.Children.Count - 1; index >= 0; --index)
      {
        NavigatorTreeNode child = targetTreeNode.Children[index];
        NavigatorTreeNode node = child;
        INode nodeHandler = node.Tree.GetNodeHandler(node);
        if (nodeHandler.GetData(node.NodeID, typeof (IDBRelationID)) is IDBRelationID data3)
        {
          IDBTypedObjectID data2 = (IDBTypedObjectID) nodeHandler.GetData(node.NodeID, typeof (IDBTypedObjectID));
          sortingInfoCache.AddItem(new CompositionViewSortingInfoItem(data3, data2, child));
        }
      }
      if (!this._args.TargetView.ManualSort)
        sortingInfoCache.SortItems();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IUserSession session = sessionKeeper.Session;
        CompositionViewEvents.RaiseBeforeAllCreations((object) this._args.TargetView, session);
        this._button.DoBeforeAllCreation(data1, this._selectedItems, session);
        try
        {
          foreach (IDBTypedObjectID selectedItem in this._selectedItems)
          {
            string errorString;
            IDBObject dbObject = this._button.DoCreateObject(data1, selectedItem, targetObjApplCache, session, false, out errorString);
            if (dbObject == null)
            {
              this._errorList.Add($"{this._errorList.Count + 1}. {errorString}");
            }
            else
            {
              IDBTypedObjectID dbTypedObjectId = (IDBTypedObjectID) CVButtonBase.GetDBTypedObjectID(dbObject);
              cvRelationInfo cvRelationInfo = targetObjApplCache[dbObject.ObjectType][0];
              NewRelationProperties newRelPros = new NewRelationProperties(data1.ObjectID, dbObject.ID);
              newRelPros.PartObjectID = dbObject.ObjectID;
              List<AttributeValues> attributeValuesList = new List<AttributeValues>();
              CVLocalButton.CVButtonClickArgs clickArgs = this._args as CVLocalButton.CVButtonClickArgs;
              if (clickArgs != null && clickArgs.CopyRelAttrs != null && selectedItem is IDBRelationID dbRelationId1)
              {
                IDBRelation relation = session.GetRelation(dbRelationId1.Value, false);
                if (relation != null)
                {
                  AttributeValues[] attributesValues = relation.GetAttributesValues(GetAttributeValuesModes.CheckWriteAccess | GetAttributeValuesModes.IncludeCaption);
                  attributeValuesList.AddRange(((IEnumerable<AttributeValues>) attributesValues).Where<AttributeValues>((Func<AttributeValues, bool>) (item => clickArgs.CopyRelAttrs.Contains(item.AttributeID))));
                }
              }
              long num1 = 1000000;
              if (cvRelationInfo.HasSortAttribute)
              {
                if (!dictionary2.ContainsKey(cvRelationInfo.RelationTypeID))
                  dictionary2.Add(cvRelationInfo.RelationTypeID, new Dictionary<int, long>());
                Dictionary<int, long> dictionary3 = dictionary2[cvRelationInfo.RelationTypeID];
                if (dictionary3.ContainsKey(dbObject.ObjectType))
                {
                  num1 = dictionary3[dbObject.ObjectType] + 1000000L;
                  if (dictionary1.ContainsKey(dbObject.ObjectType))
                    relationIDs = dictionary1[dbObject.ObjectType].RelIDList;
                }
                else
                {
                  CompositionViewSortingInfoItem closedObjectRec = sortingInfoCache.FindClosedObjectRec(data1.ObjectType, cvRelationInfo.RelationTypeID, dbObject.ObjectType, CompositionSortingLookupMode.Less);
                  if (closedObjectRec != null)
                  {
                    long num2 = closedObjectRec.Sorting;
                    if (closedObjectRec.RelTypeID != cvRelationInfo.RelationTypeID || closedObjectRec.PartObjType != dbObject.ObjectType)
                    {
                      CompositionViewSortingInfoItem prevObject = sortingInfoCache.GetPrevObject(closedObjectRec);
                      if (prevObject != null)
                      {
                        if (num2 < prevObject.Sorting)
                          num2 = (num2 + prevObject.Sorting) / 2L;
                      }
                      else
                        num2 = (Convert.ToInt64(num2 / 1000000000L) + 1L) * 1000000000L;
                    }
                    num1 = num2 + 1000000L;
                  }
                  else if (sortingInfoCache.InfoItems.Count != 0)
                  {
                    closedObjectRec = sortingInfoCache.FindClosedObjectRec(data1.ObjectType, cvRelationInfo.RelationTypeID, dbObject.ObjectType, CompositionSortingLookupMode.More);
                    if (closedObjectRec != null)
                      num1 = (Convert.ToInt64(closedObjectRec.Sorting / 1000000000L) - 1L) * 1000000000L;
                  }
                  dictionary3.Add(dbObject.ObjectType, num1);
                  if (dictionary1.ContainsKey(dbObject.ObjectType))
                    relationIDs = dictionary1[dbObject.ObjectType].RelIDList;
                  else if (closedObjectRec != null && closedObjectRec.TreeNode != null)
                  {
                    TreeNodeRec treeNodeRec = new TreeNodeRec(closedObjectRec.TreeNode, num1 < closedObjectRec.Sorting ? NodesInsertPosition.Before : NodesInsertPosition.After);
                    relationIDs = treeNodeRec.RelIDList;
                    dictionary1.Add(dbObject.ObjectType, treeNodeRec);
                  }
                }
                AttributeValues attributeValues = new AttributeValues(session.IdentHelper.SortIndexID, (object) num1);
                attributeValuesList.Add(attributeValues);
                dictionary2[cvRelationInfo.RelationTypeID][dbObject.ObjectType] = num1;
              }
              if (attributeValuesList.Count != 0)
                newRelPros.ValuesList = attributeValuesList.ToArray();
              IDBRelationID relation1 = this._button.DoCreateRelation(cvRelationInfo.RelationTypeID, newRelPros, data1, dbTypedObjectId, session);
              relationIDs.Add(relation1.Value);
              IDBRelationID dbRelationId2 = (IDBRelationID) new DBRelationID(relation1.Value, relation1.PartID, relation1.RelationType, num1, relation1.RelGuid, relation1.ProjID);
              sortingInfoCache.AddFirstItem(new CompositionViewSortingInfoItem(dbRelationId2, dbTypedObjectId));
              CompositionViewEvents.RaiseCreateRelation((object) null, buttonMethod, data1.ObjectID, dbObject.ObjectID, dbRelationId2.Value, relationIDs);
              this._button.DoCommitObject(dbObject, session);
            }
          }
        }
        finally
        {
          this._button.DoAfterAllCreation(session);
          CompositionViewEvents.RaiseAfterAllCreations((object) this._args.TargetView, session);
        }
      }
      if (dictionary1.Count > 0)
      {
        foreach (TreeNodeRec treeNodeRec in dictionary1.Values)
          this._notificationList.Add((NotificationEventArgs) new DBRelationsManagedEventArgs("ManagedRelationsInsert", (IList<long>) treeNodeRec.RelIDList, true, (object) this._args.TargetView, (object) treeNodeRec.TreeNode, treeNodeRec.Position));
      }
      if (relationIDs.Count > 0)
        this._notificationList.Add((NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", (IList<long>) relationIDs));
      if (this._errorList.Count <= 0)
        return;
      this._errorList.Insert(0, LocalizationHolder.rm.GetString("Client.Core_20"));
    }

    /// <summary>Отправка уведомлений навигатору</summary>
    private void DoExecute_Notification()
    {
      if (this._notificationList.Count != 0)
      {
        foreach (NotificationEventArgs notification in this._notificationList)
          CompositionViewHelper.UpdateSourceTreeView((object) null, (object) (this._args as CVLocalButton.CVButtonClickArgs).TargetWindow, notification);
      }
      if (this._errorList.Count == 0)
        return;
      int num = (int) MessageBox.Show(string.Join("\r\n", this._errorList.ToArray()), LocalizationHolder.rm.GetString("Client.Core_19"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }

    /// <summary>
    /// 
    /// </summary>
    protected override void DoProceed_Commands_Internal()
    {
      this._userRule = (CompositionsAutosortRule) null;
      if (ServicesManager.GetService(typeof (ICurrentUserAndRole)) is ICurrentUserAndRole service)
        this._userRule = service.Rule;
      if (this._userRule == null)
        return;
      CVButtonMethod cvButtonMethod = (this._args as CVLocalButton.CVButtonClickArgs).Method;
      NavigatorTreeNode parent = this._args.TargetTreeNode.Parent;
      if (parent != null && parent.Level != 0)
      {
        if (!this._args.TargetView.ManualSort && (cvButtonMethod == CVButtonMethod.InsertAfter || cvButtonMethod == CVButtonMethod.InsertBefore))
          cvButtonMethod = CVButtonMethod.Add;
        switch (cvButtonMethod)
        {
          case CVButtonMethod.Add:
            this.DoExecute_Add();
            break;
          case CVButtonMethod.InsertBefore:
            this.DoExecute_InsertBefore();
            break;
          case CVButtonMethod.InsertInto:
            this.DoExecute_InsertInto();
            break;
          case CVButtonMethod.InsertAfter:
            this.DoExecute_InsertAfter();
            break;
          case CVButtonMethod.Replace:
            this.DoExecute_Replace();
            break;
        }
      }
      else if (cvButtonMethod == CVButtonMethod.InsertInto)
        this.DoExecute_InsertInto();
      this.DoExecute_Notification();
    }

    /// <summary>
    /// 
    /// </summary>
    public override bool Proceed(CVButtonBase button, CVLocalButton.CVButtonArgs args)
    {
      this._errorList.Clear();
      this._notificationList.Clear();
      return base.Proceed(button, args);
    }
  }
}
