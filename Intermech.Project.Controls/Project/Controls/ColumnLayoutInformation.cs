// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.ColumnLayoutInformation
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Diagnostics;
using System;

#nullable disable
namespace Intermech.Project.Controls;

[Serializable]
internal struct ColumnLayoutInformation(
  [NotNull] string columnName,
  [NotNull] string text,
  int displayIndex,
  bool visible,
  int width)
{
  [NotNull]
  public string ColumnName = columnName;
  public int DisplayIndex = displayIndex;
  public bool Visible = visible;
  public int Width = width;
  [NotNull]
  public string Text = text;
}
