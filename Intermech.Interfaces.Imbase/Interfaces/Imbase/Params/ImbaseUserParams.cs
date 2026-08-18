// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.Params.ImbaseUserParams
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Imbase.Params;

/// <summary>Пользовательские параметры Imbase</summary>
[Serializable]
public class ImbaseUserParams
{
  public ImbaseUserParams()
  {
    this.TableRecordsApplicabilityColors = new TableRecordsApplicabilityColors();
  }

  /// <summary>Скрывать пустые колонки</summary>
  public bool HideEmptyColumns { get; set; }

  /// <summary>Замораживать первую колонку</summary>
  public bool FreezeFirstColumn { get; set; }

  /// <summary>Использовать выбор материала через Марочник</summary>
  public bool UseIMHSelector { get; set; }

  /// <summary>Сохранять положение колонок</summary>
  public bool SaveColumnsState { get; set; }

  /// <summary>Сохранять фильтр</summary>
  public bool SaveFilterState { get; set; }

  /// <summary>Сохранять пользовательский фильтр</summary>
  public bool SaveUserFilterState { get; set; }

  /// <summary>
  /// Использовать режим расширенного лога - пишется инфа обо все ошибках и указываются, какие атрибуты и почему попали в синхронизацию
  /// </summary>
  public bool UseExtendedLog { get; set; }

  public TableRecordsApplicabilityColors TableRecordsApplicabilityColors { get; set; }
}
