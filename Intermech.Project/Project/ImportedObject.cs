// Decompiled with JetBrains decompiler
// Type: Intermech.Project.ImportedObject
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Metadata;
using System;

#nullable disable
namespace Intermech.Project;

/// <summary>Дескриптор импортированного в проект объекта</summary>
public class ImportedObject
{
  [CanBeNull]
  private ImportObjectSettings _importSettings;
  /// <summary>Ссылка на проект. Нужна для обращения в БД</summary>
  [NotNull]
  private readonly Intermech.Project.Project _ownerProject;
  /// <summary>Идентификатор версии импортированного в проект объекта</summary>
  [NotEmpty]
  public readonly long ObjectVersionID;
  /// <summary>Глобальный идентификатор версии импортированного в проекта объекта</summary>
  private Guid? _objectVersionGuid;
  /// <summary>Идентификатор связи из проекта в импортированное в проект изделия (пока проект не сохранён = 0)</summary>
  [CanBeEmpty]
  public long RelationID;

  /// <summary>Глобальный идентификатор версии импортированного в проекта объекта</summary>
  [NotEmpty]
  public Guid ObjectVersionGuid
  {
    get
    {
      return this._objectVersionGuid ?? (this._objectVersionGuid = new Guid?(Session.Invoke<Guid>((Session.SessionHandler<Guid>) (session => session.GetObjectInfo(this.ObjectVersionID).VersionGuid)))).Value;
    }
  }

  /// <summary>Настройки импорта</summary>
  [NotNull]
  public ImportObjectSettings ImportSettings
  {
    get => this._importSettings ?? (this._importSettings = this.CreateImportObjectSettingsFromDB());
  }

  public long ObjectIterationID { get; private set; }

  /// <summary>Метод для отложенной загрузки настроек из БД</summary>
  [NotNull]
  private ImportObjectSettings CreateImportObjectSettingsFromDB()
  {
    return this._ownerProject.InvokeSession<ImportObjectSettings>((Session.SessionHandler<ImportObjectSettings>) (session => ImportObjectSettings.CreateFromDB(session, this.RelationID)));
  }

  /// <summary>Private constructor</summary>
  private ImportedObject([NotNull] Intermech.Project.Project ownerProject, [NotEmpty] long objectVersionID, long objectIterationID)
  {
    this._ownerProject = ownerProject;
    this.ObjectVersionID = Math.Abs(objectVersionID);
    this.ObjectIterationID = objectIterationID;
    this.RelationID = 0L;
  }

  /// <summary>Конструктор для сохранённых в БД дескрипторов импортов в проект объектов</summary>
  public ImportedObject(
    [NotNull] Intermech.Project.Project ownerProject,
    [NotEmpty] long objectVersionID,
    long objectIterationID,
    [NotEmpty] long relationID)
    : this(ownerProject, objectVersionID, objectIterationID)
  {
    this.RelationID = relationID;
  }

  /// <summary>Конструктор для дескрипторов ещё не сохранённых в БД дескрипторов импортов в проект объектов</summary>
  public ImportedObject(
    [NotNull] Intermech.Project.Project ownerProject,
    [NotEmpty] long objectVersionID,
    long objectIterationID,
    [NotNull] ImportObjectSettings importSettings)
    : this(ownerProject, objectVersionID, objectIterationID)
  {
    this._importSettings = importSettings;
  }

  /// <summary>Serves as a hash function for a particular type</summary>
  public override int GetHashCode() => this.ObjectVersionID.GetHashCode();

  /// <summary>Вызывается при обнаружении факта удаления итерации. Обнуляет соотв. свойство в атрибутах итерации</summary>
  public void MarkObjectIterationAsDeleted()
  {
    if (this.ObjectIterationID == 0L)
      return;
    this.ObjectIterationID = 0L;
    if (this.RelationID == 0L)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      sessionKeeper.Session.GetRelation(this.RelationID, false)?.Attributes.FindByID((int) (IpsMetadataEntityBase<int>) Intermech.Metadata.Attributes.IterationID)?.Delete(0L);
  }
}
