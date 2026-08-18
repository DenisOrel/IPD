// Decompiled with JetBrains decompiler
// Type: Intermech.Office.Interfaces.IOfficeRegistrationService
// Assembly: Intermech.Office.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9492297C-4143-4944-80A1-CEF9501FC1B8
// Assembly location: D:\IPS\Client\Intermech.Office.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Office.Interfaces.xml

using Intermech.Diagnostics;
using System;

#nullable disable
namespace Intermech.Office.Interfaces;

/// <summary>Сервис для регистрации документов в канцеляриях.</summary>
public interface IOfficeRegistrationService
{
  /// <summary>Регистрация во внутренней канцелярии.</summary>
  /// <param name="sessionGuid">Пользовательская сессия.</param>
  /// <param name="unitID">Идентификатор подразделения.</param>
  /// <param name="documentID">Идентификатор документа.</param>
  /// <param name="regNumber">Внутренний регистрационный номер.</param>
  bool PrivateRegister(Guid sessionGuid, long unitID, long documentID, [NotNull] string regNumber);

  /// <summary>Функция возвращает признак, зарегистрирован ли документ documentID в канцелярии подразделения unitID.</summary>
  /// <param name="sessionGuid">Пользовательская сессия.</param>
  /// <param name="unitID">Идентификатор подразделения.</param>
  /// <param name="documentID">Идентификатор документа.</param>
  bool IsDocumentPrivateRegister(Guid sessionGuid, long unitID, long documentID);

  /// <summary>Функция возвращает признак, зарегистрирован ли документ documentID в общей канцелярии.</summary>
  /// <param name="sessionGuid">Пользовательская сессия.</param>
  /// <param name="documentID">Идентификатор документа.</param>
  bool IsDocumentRegister(Guid sessionGuid, long documentID);

  /// <summary>Функция возвращает внутренний регистрационный номер документа.</summary>
  /// <param name="sessionGuid">Пользовательская сессия.</param>
  /// <param name="documentID">Идентификатор документа.</param>
  [NotNull]
  string GetPrivateRegNumber(Guid sessionGuid, long documentID);

  /// <summary>Обновление регистрационного номера.</summary>
  /// <param name="sessionGuid">Пользовательская сессия.</param>
  /// <param name="documentID">Идентификатор документа.</param>
  /// <param name="regNumber">Новый внутренний регистрационный номер.</param>
  void UpdatePrivateRegNumber(Guid sessionGuid, long documentID, [NotNull] string regNumber);

  /// <summary>Возвращает ид. канцелярии для указанного юзера.</summary>
  /// <param name="userID">Ид. пользователя.</param>
  /// <returns>Ид. канцелярии или Consts.UnknownObjectId.</returns>
  long GetUserUnit(long userID);
}
