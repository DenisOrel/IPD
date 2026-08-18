
// Type: Intermech.Expressions.ExpressionUITypeEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;


namespace Intermech.Expressions;

public class ExpressionUITypeEditor : UITypeEditor
{
  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.Modal;
  }

  public override object EditValue(
    ITypeDescriptorContext context,
    IServiceProvider provider,
    object value)
  {
    if (value == null)
      value = (object) string.Empty;
    if (value.GetType() != typeof (string))
      return value;
    string expression = value as string;
    return ExpressionEditor.EditExpression(ref expression, (ICollection) null, (CreateVariableEventHandler) null) ? (object) expression : value;
  }
}
