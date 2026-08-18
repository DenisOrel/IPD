// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.IMProject
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;

#nullable disable
namespace Intermech.Project.Controls;

/// <summary>Общие статические методы, константы и данные IMProject.Controls</summary>
public abstract class IMProject : Intermech.Project.IMProject
{
  public static Color DefaultTaskColor = Color.Blue;
  [NotNull]
  public static Pen DefaultTaskPen = new Pen(IMProject.DefaultTaskColor);
  [NotNull]
  public static Brush DefaultTaskBrush = (Brush) new HatchBrush(HatchStyle.Percent50, IMProject.DefaultTaskColor, SystemColors.Window);
}
