// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.ITablesMergingService
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System;
using System.Data;

#nullable disable
namespace Intermech.Interfaces.Imbase;

public interface ITablesMergingService
{
  /// <summary>Произвести анализ и слияние lданных таблиц Imbase</summary>
  /// <param name="sessionGuid">Глобальный идентификатор пользовательской сессии</param>
  /// <param name="tableID">Идентификатор табдицы Imbase в текущей базе</param>
  /// <param name="importData">Импортируемая таблица Imbase, после отработки метода содержит результат слияния</param>
  /// <param name="saveToBase">Сохранить результат в базу</param>
  /// <returns>true - данные были слиты, fasle - данные импортируются как есть</returns>
  bool Merge(Guid sessionGuid, long tableID, DataSet importData, bool saveToBase);

  /// <summary>
  /// Проверить атрибут на совместимость с атрибутом в системе
  /// </summary>
  /// <param name="sessionGuid">Глобальный идентификатор пользовательской сессии</param>
  /// <param name="attrType">Идентификатор типа атрибута в системе</param>
  /// <param name="possibleValues">Таблица с допустимыми значениями если они есть</param>
  /// <param name="inID">Идентификатор атрибута в таблице с допустимыми значениями</param>
  /// <param name="inFieldType">Тип данных</param>
  /// <param name="inSize">Длина, для строковых</param>
  /// <param name="inMultiValueMode">Режим работы со списковыми параметрами</param>
  /// <param name="errorMessage">Описание ошибки если она есть</param>
  /// <returns>Результат проверки</returns>
  bool CheckAttribute(
    Guid sessionGuid,
    int attrTypeID,
    DataTable possibleValues,
    int inID,
    FieldTypes inFieldType,
    string inSize,
    MultiValueModes inMultiValueMode,
    out string errorMessage);
}
