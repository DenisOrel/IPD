// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Client.Configs.Visual.Editor.FieldContents.TemplateFieldContentsEditor
// Assembly: Intermech.TechCard.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 92A871D8-0A89-4621-8C49-8F2DEC6669D9
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Client.dll

using Intermech.Expressions;
using Intermech.TechCard.Document.Interfaces.Configs.Attributes;
using Intermech.TechCard.Document.Interfaces.Configs.Structure;
using Intermech.TechCard.Document.Interfaces.Configs.Structure.FieldContents;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;

#nullable disable
namespace Intermech.TechCard.Document.Client.Configs.Visual.Editor.FieldContents;

[FieldContentsTypeEditor(FieldContentsType.Template)]
internal class TemplateFieldContentsEditor : UITypeEditor
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
    if (!(value is TemplateFieldContents templateFieldContents))
      return value;
    List<Variable> variables = new List<Variable>();
    TypeConverter converter = TypeDescriptor.GetConverter(typeof (AttributeSettings));
    foreach (AttributeSettings templateAttribute in templateFieldContents.TemplateAttributes)
    {
      Variable variable = converter.ConvertTo((object) templateAttribute, typeof (Variable)) as Variable;
      variables.Add(variable);
    }
    string expression = templateFieldContents.ToString();
    if (!TemplateEditor.EditExpression(ref expression, (IList) variables, (CreateVariableEventHandler) null))
      return (object) templateFieldContents;
    List<AttributeSettings> attributes = new List<AttributeSettings>();
    foreach (Variable variable in variables)
      attributes.Add(converter.ConvertFrom((object) variable) as AttributeSettings);
    return (object) new TemplateFieldContents(expression, (IEnumerable<AttributeSettings>) attributes);
  }
}
