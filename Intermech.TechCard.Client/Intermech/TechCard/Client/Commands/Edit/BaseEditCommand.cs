// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Commands.Edit.BaseEditCommand
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Commands;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Navigator.Interfaces;

#nullable disable
namespace Intermech.TechCard.Client.Commands.Edit;

/// <summary>
/// Базовый класс реализации команды Изменить / Редактировать для технологических объектов
/// </summary>
/// <summary>Конструктор</summary>
internal abstract class BaseEditCommand(string commandName = "editObjectNode") : 
  ExtendedSelectedItemsCommand(commandName)
{
  /// <summary>изменение объекта возможно только в контексте</summary>
  protected bool _checkProjLink = true;

  /// <summary>Проверка параметров команды</summary>
  /// <returns></returns>
  private bool ValidateCommandArgs()
  {
    return this.Items != null && this.ContextServices != null && this.Items.Count > 0;
  }

  /// <summary>Выполнение команды в дереве навигатора</summary>
  private void ProceedCommand()
  {
    if (this.Items == null || this.Items.Count == 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.DoBeforeProceedItems(sessionKeeper.Session);
    try
    {
      this.DoProceedItems();
    }
    finally
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        this.DoAfterProceedItems(sessionKeeper.Session);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  protected override void DoExecute()
  {
    if (!this.ValidateCommandArgs() || !this.AllowCommand())
      return;
    this.ProceedCommand();
  }

  /// <summary>Проверка допустимости команды для тек. параметров</summary>
  /// <returns></returns>
  protected virtual bool AllowCommand()
  {
    if (((this.ContextServices.GetService(typeof (IViewState)) is IViewState service ? (long) service.ViewState : 0L) & 2L) != 0L)
      return false;
    for (int index = 0; index < this.Items.Count; ++index)
    {
      if (!(this.Items.GetItemData(index, typeof (IDBTypedObjectID)) is IDBTypedObjectID) || this._checkProjLink && (!(this.Items.GetItemData(index, typeof (IDBRelationID)) is IDBRelationID itemData) || itemData.Value == 0L || itemData.Value == -1L))
        return false;
    }
    return true;
  }

  /// <summary>Обработка объектов</summary>
  protected abstract void DoProceedItems();
}
