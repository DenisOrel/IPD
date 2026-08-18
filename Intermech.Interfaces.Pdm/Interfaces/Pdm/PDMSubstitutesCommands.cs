// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.PDMSubstitutesCommands
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>
/// Перечислитель, в котором указаны разрешённые команды для работы с допустимыми заменителями
/// </summary>
[Flags]
public enum PDMSubstitutesCommands : long
{
  /// <summary>Ни одна команда не допустима</summary>
  None = 0,
  /// <summary>Разрешена команда "Создать группу заменителей"</summary>
  CreateSubstitutesGroup = 1,
  /// <summary>Разрешена команда "Сделать заменитель актуальным"</summary>
  MakeActualSubstitute = 2,
  /// <summary>Разрешена команда "Настроить группу заменителей"</summary>
  EditSubstitutesGroup = 4,
  /// <summary>Разрешена команда "Удалить группу заменителей"</summary>
  DeleteSubstitutesGroup = 8,
}
