// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.ScaleType
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

#nullable disable
namespace Intermech.Project.Controls;

public enum ScaleType
{
  /// <summary>Дни</summary>
  [CustomDescription("ScaleTypeDays")] Days = 1,
  /// <summary>Недели</summary>
  [CustomDescription("ScaleTypeWeeks")] Weeks = 2,
  /// <summary>Месяцы</summary>
  [CustomDescription("ScaleTypeMonths")] Months = 3,
  /// <summary>Кварталы</summary>
  [CustomDescription("ScaleTypeQuarters")] Quarters = 4,
  /// <summary>Годы</summary>
  [CustomDescription("ScaleTypeYears")] Years = 5,
}
