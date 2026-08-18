// Decompiled with JetBrains decompiler
// Type: Intermech.Office.Interfaces.IRegistrationNumberGenerator
// Assembly: Intermech.Office.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9492297C-4143-4944-80A1-CEF9501FC1B8
// Assembly location: D:\IPS\Client\Intermech.Office.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Office.Interfaces.xml

using Intermech.Diagnostics;
using System;

#nullable disable
namespace Intermech.Office.Interfaces;

/// <summary>Служба генерации регистрационного номера для канцелярского документа.</summary>
public interface IRegistrationNumberGenerator
{
  /// <summary>Флаг автоматической генерации регистрационного номера для типа.</summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии.</param>
  /// <param name="docTypeID">Тип документа.</param>
  /// <param name="type">Вид канцелярского документа.</param>
  bool IsAutoGenerate(Guid sessionGuid, int docTypeID, OfficeDocumentTypes type);

  /// <summary>Флаг автоматической генерации регистрационного номера для типа для регистрации во внутренней канцелярии.</summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии.</param>
  /// <param name="docTypeID">Тип документа.</param>
  /// <param name="type">Вид канцелярского документа.</param>
  /// <param name="unitID">Идентификатор подразделения.</param>
  bool IsAutoGenerate(Guid sessionGuid, int docTypeID, OfficeDocumentTypes type, long unitID);

  /// <summary>Флаг отсутствия необходимости присвоения регистрационного номера внутренней канцелярии.</summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии.</param>
  /// <param name="docTypeID">Тип документа.</param>
  /// <param name="type">Вид канцелярского документа.</param>
  /// <param name="unitID">Идентификатор подразделения.</param>
  bool IsEmptyRegNumbersEnabled(
    Guid sessionGuid,
    int docTypeID,
    OfficeDocumentTypes type,
    long unitID);

  /// <summary>Генерация следующего значения для документа.</summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии.</param>
  /// <param name="documentID">Идентификатор канцелярского документа.</param>
  /// <param name="docTypeID">Тип документа.</param>
  /// <param name="type">Вид канцелярского документа.</param>
  /// <param name="classifierID">Идентификатор классификатора, который участвует в расчете.</param>
  [NotNull]
  string Generate(
    Guid sessionGuid,
    long documentID,
    int docTypeID,
    OfficeDocumentTypes type,
    long classifierID);

  /// <summary>Генерация следующего значения для регистрации во внутренней канцелярии.</summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии.</param>
  /// <param name="documentID">Идентификатор канцелярского документа.</param>
  /// <param name="docTypeID">Тип документа.</param>
  /// <param name="type">Вид канцелярского документа.</param>
  /// <param name="classifierID">Идентификатор классификатора, который участвует в расчете.</param>
  /// <param name="unitID">Идентификатор подразделения.</param>
  [NotNull]
  string PrivateGenerate(
    Guid sessionGuid,
    long documentID,
    int docTypeID,
    OfficeDocumentTypes type,
    long classifierID,
    long unitID);

  /// <summary>Генерация следующего значения для регистрации во внутренней канцелярии.</summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии.</param>
  /// <param name="documentID">Идентификатор канцелярского документа.</param>
  /// <param name="docTypeID">Тип документа.</param>
  /// <param name="type">Вид канцелярского документа.</param>
  /// <param name="unitID">Идентификатор подразделения.</param>
  [NotNull]
  string PrivateGenerate(
    Guid sessionGuid,
    long documentID,
    int docTypeID,
    OfficeDocumentTypes type,
    long unitID);

  /// <summary>Генерация следующего значения для документа.</summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии.</param>
  /// <param name="documentID">Идентификатор канцелярского документа.</param>
  /// <param name="docTypeID">Тип документа.</param>
  /// <param name="type">Вид канцелярского документа.</param>
  [NotNull]
  string Generate(Guid sessionGuid, long documentID, int docTypeID, OfficeDocumentTypes type);

  /// <summary>Участвует в расчете регистрационного номера классификатор.</summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии.</param>
  /// <param name="docTypeID">Тип документа.</param>
  /// <param name="type">Вид канцелярского документа.</param>
  bool IsClassifierPresent(Guid sessionGuid, int docTypeID, OfficeDocumentTypes type);

  /// <summary>Участвует в расчете внутреннего регистрационного номера классификатор.</summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии.</param>
  /// <param name="docTypeID">Тип документа.</param>
  /// <param name="type">Вид канцелярского документа.</param>
  /// <param name="unitID">Идентификатор подразделения.</param>
  bool IsClassifierPresent(Guid sessionGuid, int docTypeID, OfficeDocumentTypes type, long unitID);

  /// <summary>Принудительный сброс счетчика.</summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии.</param>
  /// <param name="docTypeID">Тип документа.</param>
  /// <param name="type">Вид канцелярского документа.</param>
  bool ResetCounter(Guid sessionGuid, int docTypeID, OfficeDocumentTypes type);

  /// <summary>Принудительный сброс счетчика.</summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии.</param>
  /// <param name="docTypeID">Тип документа.</param>
  /// <param name="type">Вид канцелярского документа.</param>
  /// <param name="unitID">Идентификатор подразделения.</param>
  bool ResetCounter(Guid sessionGuid, int docTypeID, OfficeDocumentTypes type, long unitID);
}
