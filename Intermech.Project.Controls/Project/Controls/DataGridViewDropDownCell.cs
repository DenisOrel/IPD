// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.DataGridViewDropDownCell
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Diagnostics;
using System;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Controls;

internal class DataGridViewDropDownCell : DataGridViewComboBoxCell, ICloneable, IDisposable
{
  [NotNull]
  protected override object GetFormattedValue(
    [CanBeNull] object value,
    int rowIndex,
    [CanBeNull] ref DataGridViewCellStyle cellStyle,
    [CanBeNull] TypeConverter valueTypeConverter,
    [CanBeNull] TypeConverter formattedValueTypeConverter,
    DataGridViewDataErrorContexts context)
  {
    return (object) value?.ToString() ?? (object) string.Empty;
  }
}
