
// Type: Intermech.Tools.Integrators.PrepareNewObjectsService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Tools.Integrators;

/// <summary>
/// Реализует базовый класс для сервиса интегратора, предназначенного для подготовки к использованию новых объектов, создаваемых внутри IPS.
/// </summary>
/// <summary>Создает объект.</summary>
/// <param name="owner">Владелец компонента</param>
/// <exception cref="T:System.ArgumentNullException">Ссылка на владельца компонента не может быть null</exception>
public class PrepareNewObjectsService(IIntegrator owner) : 
  IntegratorService(owner),
  IPrepareNewObjectsService
{
  /// <summary>
  /// Позволяет обработать и настроить новый объект (а также его файловую копию, если это документ) сразу после создания.
  /// Данный метод вызывается для всех объектов, обрабатываемых интегратором.
  /// </summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  public virtual void PrepareNewObject(long objectId) => this.RequireReadyState();

  /// <summary>
  /// Позволяет обработать и настроить файлы объекта при создании по прототипу. Метод вызывается сразу после создания заготовки нового объекта.
  /// Как правило, обработка заключается в удалении из нового объекта идентифицирующих сведений, относящихся к объекту-прототипу.
  /// К таким сведениям относятся значения атрибутов "Обозначение", "Код ОКП", "Наименование" и др.
  /// </summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <param name="prototypeId">Идентификатор прототипа объекта</param>
  public virtual void PreparePrototypedObjectFiles(long objectId, long prototypeId)
  {
    this.RequireReadyState();
  }
}
