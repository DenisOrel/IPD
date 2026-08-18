// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.IPdmServerPlugin
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>
/// Интерфейс для проверки наличия загруженного плагина на сервере приложений
/// </summary>
public interface IPdmServerPlugin
{
  /// <summary>Guid плагина</summary>
  Guid PluginGuid { get; }

  /// <summary>Блокировать автосоздание связей для всех исполнений при добавлении связи в одно</summary>
  /// <param name="articleObjectID">Идентификатор версии исполнения</param>
  /// <param name="partID">Идентификатор добавляемого в состав объекта</param>
  void LockAutoCreateRelationForArticle(long articleObjectID, long partID);

  /// <summary>Разблокировать автосоздание связей для всех исполнений при добавлении связи в одно</summary>
  /// <param name="articleObjectID">Идентификатор версии исполнения</param>
  /// <param name="partID">Идентификатор добавляемого в состав объекта</param>
  void UnlockAutoCreateRelationForArticle(long articleObjectID, long partID);
}
