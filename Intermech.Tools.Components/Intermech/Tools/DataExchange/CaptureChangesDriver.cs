// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.DataExchange.CaptureChangesDriver
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data.EntityDb;
using Intermech.Data.SectionEntities;
using Intermech.UI;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.DataExchange;

public abstract class CaptureChangesDriver : ICaptureChangesDriver
{
  private bool active;

  /// <summary>
  /// Подготавливает драйвер к обработке нового объекта. Этот метод следует использовать для контроля установки свойств объекта, а также
  /// создания вспомогательных объектов и сервисов.
  /// </summary>
  /// <exception cref="T:Intermech.Tools.Integrators.DataExchange.CaptureChangesConfigurationException">Одно из свойств объекта не инициализировано должным образом</exception>
  public void BeginAction()
  {
    if (this.active)
      throw new InvalidOperationException("Method BeginAction already called.");
    this.ValidateDriverProperties();
    try
    {
      this.InitializeDriver();
      this.active = true;
    }
    catch
    {
      this.EndAction();
      throw;
    }
  }

  protected virtual void ValidateDriverProperties()
  {
  }

  protected virtual void InitializeDriver()
  {
  }

  /// <summary>
  /// Очищает драйвер в конце обработки объекта. Этот метод следует использовать для освобождения ссылок на вспомогательные объекты и сервисы.
  /// Метод не должен сбрасывать исключений, так как он может являться частью обработчика уже возникшего исключения.
  /// </summary>
  public void EndAction()
  {
    try
    {
      this.ClearDriver();
    }
    catch
    {
    }
    finally
    {
      this.active = false;
    }
  }

  protected virtual void ClearDriver()
  {
  }

  /// <summary>
  /// Возвращает признак, что метод BeginAction был выполнен без ошибок.
  /// </summary>
  public bool Active => this.active;

  /// <summary>
  /// Позволяет убедиться, что метод BeginAction() был вызван.
  /// </summary>
  /// <exception cref="T:InvalidOperationException">Метод BeginAction() не был вызван</exception>
  protected void ValidateActive()
  {
    if (!this.active)
      throw new InvalidOperationException("Method BeginAction must be called first.");
  }

  /// <summary>Анализирует документы на наличие изменений.</summary>
  /// <param name="ctx">Контекст выполнения. Этот объект содержит все необходимые исходные данные, а также вспомогательные объекты</param>
  /// <param name="progressSink">Индикатор хода выполнения процесса. Параметр может быть не задан</param>
  public void Invoke(CaptureChangesContext ctx, IPercentageProgressSink progressSink = null)
  {
    if (ctx == null)
      throw new ArgumentNullException(nameof (ctx));
    if (progressSink == null)
      progressSink = ProgressSinks.NullPercentageSink;
    this.ValidateActive();
    this.DoInvoke(ctx, progressSink);
  }

  protected abstract void DoInvoke(CaptureChangesContext ctx, IPercentageProgressSink progressSink);

  /// <summary>
  /// Удаляет данные драйвера из базы данных контекста. Это требуется, чтобы базу данных можно было безопасно вернуть в качестве результата выполнения.
  /// Этот метод вызывается даже в случае, когда процесс обработки прерывается по исключительной ситуации.
  /// </summary>
  /// <param name="database">База данных контекста</param>
  public void DetachDatabase(CaptureChangesDatabase database)
  {
    if (database == null)
      throw new ArgumentNullException(nameof (database));
    this.ValidateActive();
    try
    {
      this.DoDetachDatabase(database);
    }
    catch
    {
    }
  }

  /// <summary>
  /// Удаляет данные драйвера из базы данных контекста. Это требуется, чтобы базу данных можно было безопасно вернуть в качестве результата выполнения.
  /// Этот метод вызывается даже в случае, когда процесс обработки прерывается по исключительной ситуации.
  /// </summary>
  /// <param name="database">База данных контекста</param>
  protected virtual void DoDetachDatabase(CaptureChangesDatabase database)
  {
    if (database == null)
      throw new ArgumentNullException(nameof (database));
    ICollection<Type> removableSectionTypes = this.GetRemovableSectionTypes();
    foreach (SectionEntity dbItem in (EntityDatabase) database)
    {
      this.DoDetachItem(dbItem);
      if (removableSectionTypes != null && removableSectionTypes.Count > 0)
      {
        foreach (Type sectionType in (IEnumerable<Type>) removableSectionTypes)
          dbItem.Sections.Remove(sectionType);
      }
    }
  }

  /// <summary>
  /// Удаляет данные драйвера из объекта в базе данных контекста. Это требуется, чтобы базу данных можно было безопасно вернуть в качестве результата выполнения.
  /// Этот метод вызывается даже в случае, когда процесс обработки прерывается по исключительной ситуации.
  /// </summary>
  /// <param name="dbItem">Объект из базы данных контекста</param>
  protected virtual void DoDetachItem(SectionEntity dbItem)
  {
  }

  /// <summary>
  /// Возвращает список типов секций, которые драйвер использует для хранения своих временных данных.
  /// Этот метод используется в процессе очистки базы данных контекста для определения секций, которые нужно удалить.
  /// </summary>
  /// <returns>Коллекция типов секций, которые нужно удалить из базы данных контекста</returns>
  protected virtual ICollection<Type> GetRemovableSectionTypes()
  {
    return (ICollection<Type>) new List<Type>();
  }

  /// <summary>
  /// Вызывается в самом конце после успешного завершения процесса.
  /// Метод может использоваться драйвером для извлечения полезных сведений из рабочего контекста.
  /// </summary>
  public void Postprocess()
  {
    this.ValidateActive();
    this.DoPostprocess();
  }

  /// <summary>
  /// Вызывается в самом конце после успешного завершения процесса.
  /// Метод может использоваться драйвером для извлечения полезных сведений из рабочего контекста.
  /// </summary>
  protected virtual void DoPostprocess()
  {
  }
}
