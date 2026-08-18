// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.FormulaConverter
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Imbase.Editors;
using System;
using System.ComponentModel;
using System.Globalization;

#nullable disable
namespace Intermech.Imbase;

internal class FormulaConverter : TypeConverter
{
  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    if (value == null || value == DBNull.Value)
      return (object) string.Empty;
    string formula = value.ToString();
    return string.IsNullOrEmpty(formula) ? (object) string.Empty : (object) TableEditor.RenameFormulaFields(formula, StructureEditorPropGridDescriptor.AttTypePropsList.ToArray(), true);
  }
}
