// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.ColumnInfo
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Diagnostics;
using System.ComponentModel;

#nullable disable
namespace Intermech.Project.Controls;

public class ColumnInfo
{
  /// <summary>
  /// Имя стандартной колонки в гриде, или DataPropertyName. Может также иметь формат ".%Attribute_ID%"
  /// </summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public string Name { get; }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [NotNull]
  public string Text { get; }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public FieldTypes Type { get; }

  public ColumnInfo([NotNull] string name, [NotNull] string text, FieldTypes type)
  {
    this.Name = name;
    this.Text = text;
    this.Type = type;
  }

  public override string ToString() => this.Text;
}
