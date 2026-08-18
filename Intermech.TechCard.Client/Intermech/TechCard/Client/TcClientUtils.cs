// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcClientUtils
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Client.Core.CompositionView;
using Intermech.Interfaces;
using Intermech.Interfaces.AutoSelection;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.TechCard;
using Intermech.Interfaces.TechCard.TechNumeration;
using Intermech.Navigator.ContextMenu;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client;

/// <summary>Utils class</summary>
internal static class TcClientUtils
{
  /// <summary>
  /// Вспомогательная функция для поиска заданного пункта меню.
  /// Если заданный пункт меню не найден, то создается новый.
  /// </summary>
  /// <param name="nodes"></param>
  /// <param name="name"></param>
  /// <param name="text"></param>
  /// <param name="imageIndex"></param>
  /// <param name="groupId"></param>
  /// <param name="orderId"></param>
  /// <param name="shortcut"></param>
  /// <returns></returns>
  internal static MenuTemplateNode FindOrCreate(
    MenuTemplateNodeCollection nodes,
    string name,
    string text,
    int imageIndex,
    int groupId,
    int orderId,
    Keys shortcut = Keys.None)
  {
    foreach (MenuTemplateNode node in nodes)
    {
      if (!(node.Name != name))
        return node;
    }
    MenuTemplateNode node1 = new MenuTemplateNode(name, text, imageIndex, groupId, orderId, shortcut);
    nodes.Add(node1);
    return node1;
  }

  /// <summary>
  /// Заглушка для предоставления обработчика DynamicSelectionEventHandler
  /// при вызове IImbaseSelector.DynamicSelection для группового создания объектов через справочник
  /// </summary>
  /// <param name="selectedObjectId"></param>
  /// <param name="mode"></param>
  /// <returns></returns>
  public static bool DynamicSelectionEventHandlerImpl(
    long selectedObjectId,
    DynamicSelectionMode mode)
  {
    return true;
  }

  /// <summary>
  /// Обработчик события BeforeAllCreations в CompositionView
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="session"></param>
  public static void CompositionViewBeforeAllCreation(object sender, IUserSession session)
  {
    ((ITechNumerationService) session.GetCustomService(typeof (ITechNumerationService)))?.CreateSession(session.SessionGUID)?.BeginLogging();
  }

  /// <summary>
  /// Обработчик события AfterAllCreations в CompositionView
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="session"></param>
  public static void CompositionViewAfterAllCreation(object sender, IUserSession session)
  {
    ITechNumerationService customService = (ITechNumerationService) session.GetCustomService(typeof (ITechNumerationService));
    if (customService == null)
      return;
    ITechNumerationSession session1 = customService.CreateSession(session.SessionGUID);
    try
    {
      ITechNumerationLog numerationLog = session1?.GetNumerationLog();
      if (numerationLog == null || numerationLog.ObjectsLog.Count == 0 && numerationLog.RelationsLog.Count == 0)
        return;
      INotificationService service = ServiceUtils.GetService<INotificationService>((object) ApplicationServices.Container, false);
      if (service == null)
        return;
      service.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", numerationLog.ObjectsLog, true));
      service.FireEvent((object) null, (NotificationEventArgs) new DBRelationsEventArgs("RelationsChanged", numerationLog.RelationsLog));
    }
    finally
    {
      customService.DisposeSession(session.SessionGUID);
    }
  }

  /// <summary>
  /// Обработчик события ObjectAfterCreation в CompositionView
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  /// <returns></returns>
  public static void ComposionViewFolderObjectAfterCreation(
    object sender,
    CompositionViewObjectEventArgs e)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(e.ObjectID);
      if (dbObject == null || !TechCardConsts.Utils.IsTechcardObjectType((object) dbObject.ObjectType))
        return;
      ServiceUtils.GetService<IAutoSelectionService>((object) ApplicationServices.Container, false)?.ExecuteSelection(dbObject.ObjectID, AutoSelectionMode.AutoObject);
    }
  }
}
