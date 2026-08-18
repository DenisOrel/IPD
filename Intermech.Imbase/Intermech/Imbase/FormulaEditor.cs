// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.FormulaEditor
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Expressions;
using Intermech.Imbase.Editors;
using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.Imbase;

internal class FormulaEditor : ModalEditor
{
  public override object EditValue(
    ITypeDescriptorContext context,
    IServiceProvider provider,
    object value)
  {
    if (context == null || !(context.Instance is StructureEditorPropGridDescriptor instance))
      return value;
    string expression = TableEditor.RenameFormulaFields(value == null || value == DBNull.Value ? string.Empty : value.ToString(), StructureEditorPropGridDescriptor.AttTypePropsList.ToArray(), true);
    ExpressionEditor.EditExpression(ref expression, StructureEditorPropGridDescriptor.AttTypePropsList.ToArray(), instance.AttributeID, (ParseEventHandler) null);
    return !string.IsNullOrEmpty(expression) ? (object) TableEditor.RenameFormulaFields(expression, StructureEditorPropGridDescriptor.AttTypePropsList.ToArray(), false) : (object) expression;
  }
}
