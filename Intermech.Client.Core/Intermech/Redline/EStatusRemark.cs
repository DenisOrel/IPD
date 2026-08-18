
// Type: Intermech.Redline.EStatusRemark
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Redline;

/// <summary>Статус замечания</summary>
[Flags]
public enum EStatusRemark
{
  /// <summary>Нет типов замечаний</summary>
  eNone = 0,
  /// <summary>Согласовано</summary>
  /// 
  ///             [Report("Согласовано", "Фильтр: Согласованные", "imgFilterAgreed")]
  [Report("Client.Core.EStatusRemark.Agreed", "Client.Core.EStatusRemark.TipTextAgreed", "imgFilterAgreed")] eAgreed = 1,
  /// <summary>Исправлено</summary>
  /// 
  ///             [Report("Исправлено", "Фильтр: Исправленные", "imgFilterCorrected")]
  [Report("Client.Core.EStatusRemark.Corrected", "Client.Core.EStatusRemark.TipTextCorrected", "imgFilterCorrected")] eCorrected = 2,
  /// <summary>Не исправлено</summary>
  /// 
  ///             [Report("Не исправлено", "Фильтр: Не исправленные", "imgFilterInconsistent")]
  [Report("Client.Core.EStatusRemark.Inconsistent", "Client.Core.EStatusRemark.TipTextInconsistent", "imgFilterInconsistent")] eInconsistent = 4,
  /// <summary>Отклонено</summary>
  /// 
  ///             [Report("Отклонено", "Фильтр: Отклоненные", "imgFilterRejected")]
  [Report("Client.Core.EStatusRemark.Rejected", "Client.Core.EStatusRemark.TipTextRejected", "imgFilterRejected")] eRejected = 8,
  /// <summary>все типы замечаний</summary>
  eAll = eRejected | eInconsistent | eCorrected | eAgreed, // 0x0000000F
}
