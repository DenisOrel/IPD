
// Type: Intermech.Navigator.ObjectLCStepsCache
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System.Collections.Generic;


namespace Intermech.Navigator;

/// <summary>Кэш шагов жизненного цикла</summary>
public class ObjectLCStepsCache : ICache, IObjectLCStepsCache
{
  /// <summary>Кэш шагов жизненного цикла</summary>
  protected static Dictionary<int, DBLCStep> _lcSteps = new Dictionary<int, DBLCStep>();

  /// <summary>Выполнить сброс содержимого кэша</summary>
  public void Reset()
  {
    lock (ObjectLCStepsCache._lcSteps)
      ObjectLCStepsCache._lcSteps.Clear();
  }

  /// <summary>
  /// Получить название шага жизненного цикла по его идентификатору
  /// </summary>
  /// <param name="lcStepID">Идентификатор шага ЖЦ</param>
  /// <returns>Название шага ЖЦ</returns>
  public virtual string GetName(int lcStepID) => MetaDataHelper.GetLCStepName(lcStepID);

  /// <summary>
  /// Получить идентификатор уровня продвижения, на котором находится указанный шаг ЖЦ
  /// </summary>
  /// <param name="lcStepID">Идентификатор шага ЖЦ</param>
  /// <returns>Идентификатор уровня продвижения, на котором находится указанный шаг ЖЦ</returns>
  public virtual int GetLevelID(int lcStepID) => MetaDataHelper.GetLCStep(lcStepID).LevelID;

  /// <summary>
  /// Получить идентификатор схемы, на которой находится указанный шаг ЖЦ
  /// </summary>
  /// <param name="lcStepID">Идентификатор шага ЖЦ</param>
  /// <returns>Идентификатор схемы, на которой находится указанный шаг ЖЦ</returns>
  public virtual int GetSchemaID(int lcStepID) => MetaDataHelper.GetLCStep(lcStepID).SchemaID;
}
