// Decompiled with JetBrains decompiler
// Type: Intermech.Project.ConstraintType
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

#nullable disable
namespace Intermech.Project;

public enum ConstraintType
{
  Undefined = -1, // 0xFFFFFFFF
  [CustomDescription("AsSoonAsPossible")] AsSoonAsPossible = 0,
  [CustomDescription("AsLateAsPossible")] AsLateAsPossible = 1,
  [CustomDescription("StartNoEarlierThan")] StartNoEarlierThan = 4,
  [CustomDescription("StartNoLaterThan")] StartNoLaterThan = 5,
  [CustomDescription("FinishNoEarlierThan")] FinishNoEarlierThan = 6,
  [CustomDescription("FinishNoLaterThan")] FinishNoLaterThan = 7,
  [CustomDescription("ManualPlanning")] ManualPlanning = 8,
}
