
// Type: Intermech.Navigator.IObjectLCStepsCache
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Navigator;

/// <summary>Интерфейс для работы с кэшем шагов жизненного цикла</summary>
public interface IObjectLCStepsCache
{
  /// <summary>
  /// Получить название шага жизненного цикла по его идентификатору
  /// </summary>
  /// <param name="lcStepID">Идентификатор шага ЖЦ</param>
  /// <returns>Название шага ЖЦ</returns>
  string GetName(int lcStepID);

  /// <summary>
  /// Получить идентификатор уровня продвижения, на котором находится указанный шаг ЖЦ
  /// </summary>
  /// <param name="lcStepID">Идентификатор шага ЖЦ</param>
  /// <returns>Идентификатор уровня продвижения, на котором находится указанный шаг ЖЦ</returns>
  int GetLevelID(int lcStepID);

  /// <summary>
  /// Получить идентификатор схемы, на которой находится указанный шаг ЖЦ
  /// </summary>
  /// <param name="lcStepID">Идентификатор шага ЖЦ</param>
  /// <returns>Идентификатор схемы, на которой находится указанный шаг ЖЦ</returns>
  int GetSchemaID(int lcStepID);
}
