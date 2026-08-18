// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.DataExchange.CaptureChangesLists
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.ControlFlow;
using Intermech.Data.SectionEntities;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.DataExchange;

/// <summary>
/// Описывает результаты анализа и захвата изменений, сделанных в файлах объекта с помощью приложения-редактора.
/// </summary>
internal sealed class CaptureChangesLists
{
  private readonly LinkedList<SectionEntity> requireWriteObjects;
  private readonly LinkedList<SectionEntity> importedObjects;
  private readonly LinkedList<IAction> serverActions;
  private readonly LinkedList<IAction> clientActions;

  /// <summary>Создает объект.</summary>
  public CaptureChangesLists()
  {
    this.requireWriteObjects = new LinkedList<SectionEntity>();
    this.importedObjects = new LinkedList<SectionEntity>();
    this.serverActions = new LinkedList<IAction>();
    this.clientActions = new LinkedList<IAction>();
  }

  /// <summary>
  /// Возвращает список рабочих элементов для объектов, которые должны быть взяты на изменение перед
  /// проведением в них изменений.
  /// </summary>
  public ICollection<SectionEntity> RequireWriteObjects
  {
    get => (ICollection<SectionEntity>) this.requireWriteObjects;
  }

  /// <summary>
  /// Возвращает список импортированных объектов, подлежащих регистрации в рабочей области файлового хранилища.
  /// </summary>
  public ICollection<SectionEntity> ImportedObjects
  {
    get => (ICollection<SectionEntity>) this.importedObjects;
  }

  /// <summary>
  /// Возвращает список серверных операций по захвату изменений. Все эти операции выполняются в одной транзакции.
  /// </summary>
  public ICollection<IAction> ServerActions => (ICollection<IAction>) this.serverActions;

  /// <summary>
  /// Возвращает список клиентских операций по захвату изменений. Как правило, в этот список попадают операции
  /// обновления пользовательского интерфейса.
  /// </summary>
  public ICollection<IAction> ClientActions => (ICollection<IAction>) this.clientActions;
}
