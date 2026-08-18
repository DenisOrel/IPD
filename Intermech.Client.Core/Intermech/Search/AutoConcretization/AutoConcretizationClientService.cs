
// Type: Intermech.Search.AutoConcretization.AutoConcretizationClientService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Search.Concretization;
using Intermech.Search.UI;
using Intermech.Search.Utilities;
using System;
using System.Windows;


namespace Intermech.Search.AutoConcretization;

public sealed class AutoConcretizationClientService : IAutoConcretizationClientService
{
  private IConcretizationClientService _concretizationClientService;

  public AutoConcretizationClientService(
    IConcretizationClientService concretizationClientService)
  {
    this._concretizationClientService = concretizationClientService != null ? concretizationClientService : throw new ArgumentNullException(nameof (concretizationClientService));
  }

  public bool CanDisableAutoConcretization(NavigatorTreeNode navigatorTreeNode)
  {
    if (navigatorTreeNode == null)
      throw new ArgumentNullException(nameof (navigatorTreeNode));
    long objectVersionID = 0;
    return this.TryGetObjectVersionIDSupportingAutoConcretization(navigatorTreeNode, out objectVersionID);
  }

  public bool CanEnableAutoConcretization(NavigatorTreeNode navigatorTreeNode)
  {
    if (navigatorTreeNode == null)
      throw new ArgumentNullException(nameof (navigatorTreeNode));
    long objectVersionID = 0;
    return this.TryGetObjectVersionIDSupportingAutoConcretization(navigatorTreeNode, out objectVersionID);
  }

  public void DisableAutoConcretization(NavigatorTreeNode navigatorTreeNode)
  {
    if (navigatorTreeNode == null)
      throw new ArgumentNullException(nameof (navigatorTreeNode));
    long objectVersionID = 0;
    if (!this.TryGetObjectVersionIDSupportingAutoConcretization(navigatorTreeNode, out objectVersionID))
      throw new ArgumentException();
    if (!this.CanModifyCompositionAutoConcretizationAttribute(objectVersionID))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      using (NotificationContext.Create(sessionKeeper.Session, (object) this))
        ((IAutoConcretizationServerService) sessionKeeper.Session.GetCustomService(typeof (IAutoConcretizationServerService))).DisableAutoConcretization(sessionKeeper.Session.SessionGUID, objectVersionID);
    }
    if (MessageBox.Show("Состав объекта будет абстрагирован. Желаете продолжить?", "Intermech Professional Solution", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
      return;
    this._concretizationClientService.AbstractComposition(navigatorTreeNode);
  }

  public void EnableAutoConcretization(NavigatorTreeNode navigatorTreeNode)
  {
    if (navigatorTreeNode == null)
      throw new ArgumentException(nameof (navigatorTreeNode));
    long objectVersionID = 0;
    if (!this.TryGetObjectVersionIDSupportingAutoConcretization(navigatorTreeNode, out objectVersionID))
      throw new ArgumentException();
    if (!this.CanModifyCompositionAutoConcretizationAttribute(objectVersionID))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      using (NotificationContext.Create(sessionKeeper.Session, (object) this))
        ((IAutoConcretizationServerService) sessionKeeper.Session.GetCustomService(typeof (IAutoConcretizationServerService))).EnableAutoConcretization(sessionKeeper.Session.SessionGUID, objectVersionID);
    }
    this._concretizationClientService.ConcretizeComposition(navigatorTreeNode);
  }

  private bool TryGetObjectVersionIDSupportingAutoConcretization(
    NavigatorTreeNode navigatorTreeNode,
    out long objectVersionID)
  {
    if (navigatorTreeNode.NodeID is NodeID nodeId && !ObjectHelper.IsUnknownObjectVersionID(nodeId.ObjectID) && !ObjectTypeHelper.IsUnknownObjectTypeID(nodeId.ObjectTypeID) && AutoConcretizationHelper.IsCompositionAutoConcretizationAttributeExists(nodeId.ObjectTypeID))
    {
      objectVersionID = nodeId.ObjectID;
      return true;
    }
    objectVersionID = 0L;
    return false;
  }

  private bool CanModifyCompositionAutoConcretizationAttribute(long objectVersionID)
  {
    bool flag = false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      flag = ((IAutoConcretizationServerService) sessionKeeper.Session.GetCustomService(typeof (IAutoConcretizationServerService))).CanModifyCompositionAutoConcretizationAttribute(sessionKeeper.Session.SessionGUID, objectVersionID);
    if (!flag)
    {
      int num = (int) MessageBox.Show("Невозможно выполнить операцию, изменение атрибута Атоматическая конкретизация состава запрещено.\r\nВозможно объект не взят на редактирование.", "Intermech Professional Solution", MessageBoxButton.OK, MessageBoxImage.Asterisk);
    }
    return flag;
  }
}
