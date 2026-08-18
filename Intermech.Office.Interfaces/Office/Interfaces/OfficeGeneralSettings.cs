// Decompiled with JetBrains decompiler
// Type: Intermech.Office.Interfaces.OfficeGeneralSettings
// Assembly: Intermech.Office.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9492297C-4143-4944-80A1-CEF9501FC1B8
// Assembly location: D:\IPS\Client\Intermech.Office.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Office.Interfaces.xml

using Intermech.Diagnostics;
using System;

#nullable disable
namespace Intermech.Office.Interfaces;

/// <summary>Общие настройки канцелярии.</summary>
[Serializable]
public class OfficeGeneralSettings
{
  public OfficeGeneralSettings(
    CountResetTypes incomingDocResetType,
    CountResetTypes outgoingDocResetType,
    CountResetTypes internalDocResetType,
    long templateID,
    [CanBeNull] string autoSendEmail,
    long userID,
    bool privateOffice,
    bool filterResolutions,
    long addresseeTemplateID,
    long consistentControlResolutionTemplateID,
    long consistentNonControlResolutionTemplateID,
    long parallelControlResolutionTemplateID,
    long parallelNonControlResolutionTemplateID,
    bool incomingPrivateFolderEnable,
    int captionAttributeForEmailMessages)
  {
    this.IncomingDocResetType = incomingDocResetType;
    this.InternalDocResetType = internalDocResetType;
    this.OutgoingDocResetType = outgoingDocResetType;
    this.AutoSendEmail = autoSendEmail;
    this.UserID = userID;
    this.PrivateOffice = privateOffice;
    this.FilterResolutions = filterResolutions;
    this.TemplateID = templateID;
    this.ConsistentControlResolutionTemplateID = consistentControlResolutionTemplateID;
    this.ConsistentNonControlResolutionTemplateID = consistentNonControlResolutionTemplateID;
    this.ParallelControlResolutionTemplateID = parallelControlResolutionTemplateID;
    this.ParallelNonControlResolutionTemplateID = parallelNonControlResolutionTemplateID;
    this.AddresseeTemplateID = addresseeTemplateID;
    this.IncomingPrivateFolderEnable = incomingPrivateFolderEnable;
    this.CaptionAttributeForEmailMessages = captionAttributeForEmailMessages;
  }

  /// <summary>Счетчик для входящих канцелярских документов.</summary>
  public CountResetTypes IncomingDocResetType { get; }

  /// <summary>Счетчик для исходящих канцелярских документов.</summary>
  public CountResetTypes OutgoingDocResetType { get; }

  /// <summary>Счетчик для внутренних канцелярских документов.</summary>
  /// <value>The type of the internal document reset.</value>
  public CountResetTypes InternalDocResetType { get; }

  /// <summary>Шаблон процесса отправки документов.</summary>
  public long TemplateID { get; }

  public long ConsistentControlResolutionTemplateID { get; }

  public long ConsistentNonControlResolutionTemplateID { get; }

  public long ParallelControlResolutionTemplateID { get; }

  public long ParallelNonControlResolutionTemplateID { get; }

  /// <summary>Шаблон процесса для отправки уведомлений адресатам.</summary>
  public long AddresseeTemplateID { get; }

  /// <summary>Почтовый адрес отправителя.</summary>
  [CanBeNull]
  public string AutoSendEmail { get; }

  /// <summary>Пользователь-отправитель.</summary>
  public long UserID { get; }

  /// <summary>Внутренняя канцелярия у подразделений.</summary>
  public bool PrivateOffice { get; }

  /// <summary>Фильтровать ли поручения.</summary>
  public bool FilterResolutions { get; }

  /// <summary>
  /// Отображать ли узел "Входящие (подразделение)" в дереве навигатора
  /// </summary>
  public bool IncomingPrivateFolderEnable { get; }

  /// <summary>
  /// Идентификатор атрибута, в который записывается тема письма при регистрации его в документ
  /// </summary>
  public int CaptionAttributeForEmailMessages { get; }
}
