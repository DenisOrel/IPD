// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Commands.ParameterCardCommand
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.AutoSelection;
using Intermech.Interfaces.Client;
using Intermech.Navigator.ContextCommands;
using Intermech.TechCard.Client.Commands.Edit;
using Intermech.TechCard.Client.UI.Controls;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.TechCard.Client.Commands;

/// <summary>
/// Реализация команды "Карточка объекта" для технологических объектов
/// </summary>
/// <remarks>
/// Если позволяет контекст команды - применяем изменения к объектам ЕТП по ГТП / ТТП
/// </remarks>
internal class ParameterCardCommand : EditCommand
{
  /// <summary>Конструктор</summary>
  public ParameterCardCommand()
    : base("ParametersCard")
  {
  }

  /// <summary>
  /// Проверка допустимости команды "Редактировать" для тек. параметров
  /// </summary>
  /// <returns></returns>
  protected override bool AllowCommand()
  {
    bool flag = base.AllowCommand();
    if (this.ContextServices.GetService(typeof (AutoSelectionMode)) != null)
      flag = false;
    if (flag)
      return true;
    ObjectCommands.ParametersCardCommand(this.Items, this.ContextServices, (object) null);
    return false;
  }

  /// <summary>Обработка объектов</summary>
  protected override void DoProceedItems()
  {
    IList<CategoryValue> modificationsList;
    if (!(this.Items.GetItemData(0, typeof (IDBRelationID)) is IDBRelationID) || !(this.Items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID) || !new EditCommandAction(new EditCommandActionParam(this.Items, this.ContextServices)).Execute(out modificationsList) || modificationsList == null || !modificationsList.Any<CategoryValue>())
      return;
    foreach (NotificationEventArgs notificationEvent in TechcardClientControlsUtils.GetNotificationEvents(modificationsList))
      this.Notifications.QueueEvent(notificationEvent);
  }
}
