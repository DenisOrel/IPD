
// Type: Intermech.Navigator.DBLCStep
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System;


namespace Intermech.Navigator;

/// <summary>Шаг жизненного цикла</summary>
[Serializable]
public class DBLCStep : ICloneable, IComparable, IComparable<DBLCStep>
{
  /// <summary>Идентификатор шага ЖЦ</summary>
  protected int _lcStepID;
  /// <summary>Идентификатор уровня продвижения</summary>
  protected int _levelID;
  /// <summary>Идентификатор схемы</summary>
  protected int _schemaID;
  /// <summary>
  /// Если равно true, то этап удален (чтобы сохранять историю)
  /// </summary>
  protected bool _deleted;
  /// <summary>
  /// Способ модификации объектов.
  /// 0 - объекты модифицируются в базе без взятия их на изменение,
  /// 1 - объекты должны быть предварительно взяты на изменение,
  /// 2 - нужно выпустить новую версию объекта (текущую изменять нельзя),
  /// 3 - объект модифицировать никак нельзя
  /// (можно только перевести на следующи уровень продвижения или добавить связи).
  /// </summary>
  protected ObjectModifyModes _modifyMode;
  /// <summary>Глобальный идентификатор шага ЖЦ</summary>
  protected Guid _guid;
  /// <summary>Название шага ЖЦ</summary>
  protected string _lcName;

  /// <summary>Создать пустой экземпляр класса</summary>
  public DBLCStep()
  {
  }

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="lcStepID">Идентификатор шага ЖЦ</param>
  /// <param name="levelID">Идентификатор уровня продвижения</param>
  /// <param name="schemaID">Идентификатор схемы</param>
  /// <param name="deleted">Если не равно 0, то этап удален (чтобы сохранять историю)</param>
  /// <param name="modifyMode">Способ модификаци объектов</param>
  /// <param name="guid">Глобальный идентификатор шага ЖЦ</param>
  /// <param name="lcName">Название шага ЖЦ</param>
  public DBLCStep(
    int lcStepID,
    int levelID,
    int schemaID,
    bool deleted,
    ObjectModifyModes modifyMode,
    Guid guid,
    string lcName)
  {
    this._lcStepID = lcStepID;
    this._levelID = levelID;
    this._schemaID = schemaID;
    this._deleted = deleted;
    this._modifyMode = modifyMode;
    this._guid = guid;
    this._lcName = lcName;
  }

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="lcStep">Описание шага ЖЦ из кэша метаданных</param>
  public DBLCStep(IDBLifecycleStep lcStep)
  {
    if (lcStep == null)
      return;
    this._lcStepID = lcStep.LCStep;
    this._levelID = lcStep.LevelID;
    this._schemaID = lcStep.SchemaID;
    this._deleted = lcStep.IsDeleted;
    this._modifyMode = lcStep.ObjectModifyMode;
    this._lcName = lcStep.LCName;
    this._guid = lcStep.Properties.StepGuid;
  }

  /// <summary>Идентификатор шага ЖЦ</summary>
  public int LCStepID => this._lcStepID;

  /// <summary>Идентификатор уровня продвижения</summary>
  public int LevelID => this._levelID;

  /// <summary>Идентификатор схемы</summary>
  public int SchemaID => this._schemaID;

  /// <summary>
  /// Если равно true, то этап удален (чтобы сохранять историю)
  /// </summary>
  public bool Deleted => this._deleted;

  /// <summary>
  /// Способ модификаци объектов.
  /// 0 - объекты модифицируются в базе без взятия их на изменение,
  /// 1 - объекты должны быть предварительно взяты на изменение,
  /// 2 - нужно выпустить новую версию объекта (текущую изменять нельзя),
  /// 3 - объект модифицировать никак нельзя
  /// (можно только перевести на следующи уровень продвижения или добавить связи).
  /// </summary>
  public ObjectModifyModes ModifyMode => this._modifyMode;

  /// <summary>Глобальный идентификатор шага ЖЦ</summary>
  public Guid Guid => this._guid;

  /// <summary>Название шага ЖЦ</summary>
  public string LCName => this._lcName;

  /// <summary>Выполнить синхронизацию с кэшем метаданных</summary>
  /// <param name="lcStepID">Идентификатор шага ЖЦ (может быть новым)</param>
  /// <param name="session">Сессия, в рамках которой выполняется работа с кэшем метаданных</param>
  public virtual void SyncMetadata(int lcStepID, IUserSession session)
  {
    if (session == null)
      return;
    IDBLifecycleStep lifecycleStep = session.GetLifecycleStep(lcStepID);
    this._lcStepID = lifecycleStep.LCStep;
    this._levelID = lifecycleStep.LevelID;
    this._schemaID = lifecycleStep.SchemaID;
    this._deleted = lifecycleStep.IsDeleted;
    this._modifyMode = lifecycleStep.ObjectModifyMode;
    this._lcName = lifecycleStep.LCName;
    this._guid = lifecycleStep.Properties.StepGuid;
  }

  /// <summary>Создать копию экземпляра класса</summary>
  /// <returns>Копия экземпляра класса</returns>
  public object Clone()
  {
    return (object) new DBLCStep(this.LCStepID, this.LevelID, this.SchemaID, this.Deleted, this.ModifyMode, this.Guid, this.LCName);
  }

  /// <summary>Сравнить с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>0, если объекты равны</returns>
  public int CompareTo(object obj) => this.CompareTo(obj as DBLCStep);

  /// <summary>Сравнить с указанным объектом</summary>
  /// <param name="other">Объект для сравнения</param>
  /// <returns>-1, 0 или 1</returns>
  public int CompareTo(DBLCStep other)
  {
    return other == null ? 1 : this.LCStepID.CompareTo(other.LCStepID);
  }

  /// <summary>Выполнить сравнение с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>true, если объекты равны</returns>
  public override bool Equals(object obj) => this.CompareTo(obj) == 0;

  /// <summary>Вернуть 32-битный хэш-код экземпляра класса</summary>
  /// <returns>32-битный хэш-код экземпляра класса</returns>
  public override int GetHashCode() => this.LCStepID.GetHashCode();

  /// <summary>Вернуть строковое представление экземпляра объекта</summary>
  /// <returns>Строковое представление экземпляра объекта</returns>
  public override string ToString() => $"[{this._lcStepID}] {this._lcName}";
}
