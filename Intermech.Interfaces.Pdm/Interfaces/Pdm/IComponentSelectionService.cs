// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.IComponentSelectionService
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>Сервис для работы с подборными компонентами</summary>
public interface IComponentSelectionService
{
  /// <summary>Создать подбор в составе</summary>
  /// <param name="sessionGuid">Глобальный идентификатор пользовательской сессии</param>
  /// <param name="projectID">Идентификато версии сборки в которую включается подборный компонент, версия уже должна быть взята на изменение</param>
  /// <param name="objectID">Идентификато версии подборного компонента</param>
  /// <param name="posDesignation">Значение позиционного обозначения основного компонента</param>
  /// <param name="countOnRegulation">Количество на регулировку, шт</param>
  /// <returns>Идентификатор созданной связи</returns>
  long CreateComponentSelection(
    Guid sessionGuid,
    long projectID,
    long objectID,
    string posDesignation,
    MeasuredValue countOnRegulation);

  void ResetComponentSelection(
    Guid sessionGuid,
    long projectID,
    Guid relationGuid,
    out long changedRelationId,
    out List<long> removedRelationIds);
}
