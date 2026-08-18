
// Type: Intermech.Commands.SaveChangesCommand
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;


namespace Intermech.Commands;

internal class SaveChangesCommand : ObjectCommand
{
  private SaveChangesMode? mode;

  public SaveChangesCommand()
    : base("SaveChanges")
  {
    this.DisplayName = LocalizationHolder.rm.GetString("Client.Core_1593");
  }

  /// <summary>
  /// Возвращает или задает режим выполнения команды - обычный или в составе другой команды.
  /// Значение свойства может быть не задано и равно null.
  /// </summary>
  /// <remarks>
  /// Режим выполнения команды может быть передан команде двумя способами: либо через этого свойство,
  /// либо через свойство <see cref="P:Command.ContextServices" /> в виде объекта типа <see cref="T:SaveChangesModeHolder" />.
  /// Значение этого свойства более приоритетно, чем значение свойства <see cref="P:Command.ContextServices" />.
  /// </remarks>
  public SaveChangesMode? Mode
  {
    get => this.mode;
    set => this.mode = value;
  }

  /// <summary>
  /// Возвращает режим выполнения команды - обычный или в составе другой команды.
  /// </summary>
  /// <returns>Режим выполнения команды</returns>
  protected SaveChangesMode GetSaveChangesMode()
  {
    if (this.Mode.HasValue)
      return this.Mode.Value;
    SaveChangesModeHolder service = (SaveChangesModeHolder) this.ContextServices.GetService(typeof (SaveChangesModeHolder));
    return service != null ? service.Value : SaveChangesMode.Default;
  }

  protected override void DoExecute()
  {
    ObjectCommandEvents.SaveChanges.RaiseBefore((Command) this, new BeforeObjectCommandArgs(this.ObjectId));
    try
    {
      ServiceUtils.GetService<ICaptureFileChangesService>((object) ServicesManager.ServiceContainer, false)?.CaptureChanges(this.ObjectId, this.GetSaveChangesMode(), this.ContextServices);
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        sessionKeeper.Session.GetObject(this.ObjectId).SaveChanges();
        if (this.UpdateUI)
          this.Notifications.QueueEvent((NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", this.ObjectId));
      }
      ObjectCommandEvents.SaveChanges.RaiseAfter((Command) this, new AfterObjectCommandArgs(this.ObjectId, this.ObjectId));
      ObjectCommandEvents.SaveChanges.RaiseCleanup((Command) this, CleanupCommandArgs.Empty);
    }
    catch (Exception ex)
    {
      ObjectCommandEvents.SaveChanges.RaiseCleanup((Command) this, new CleanupCommandArgs(ex));
      throw;
    }
  }
}
