// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Client.Configs.Visual.Editor.FieldContents.FormulaFieldContentsEditor
// Assembly: Intermech.TechCard.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 92A871D8-0A89-4621-8C49-8F2DEC6669D9
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Client.dll

using Intermech.Expert;
using Intermech.Expert.Editor;
using Intermech.Localization;
using Intermech.TechCard.Document.Interfaces.Configs.Attributes;
using Intermech.TechCard.Document.Interfaces.Configs.Structure.FieldContents;
using System;
using System.ComponentModel;
using System.Drawing.Design;

#nullable disable
namespace Intermech.TechCard.Document.Client.Configs.Visual.Editor.FieldContents;

[FieldContentsTypeEditor(FieldContentsType.Formula)]
internal class FormulaFieldContentsEditor : UITypeEditor
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
    if (!(value is FormulaFieldContents formulaFieldContents))
      return value;
    TempFormula tF = formulaFieldContents.TemplateFormula == null ? new TempFormula(DataType.String) : formulaFieldContents.TemplateFormula.Clone() as TempFormula;
    bool flag;
    using (FormEditor formEditor = new FormEditor())
    {
      formEditor.CanReturnEmpty = true;
      string title = LocalizationHolder.rm.GetString("TechCard.Document_009");
      flag = formEditor.Execute(ref tF, title, true);
    }
    if (!flag)
      return value;
    return (object) new FormulaFieldContents()
    {
      TemplateFormula = (tF.Clone() as TempFormula)
    };
  }
}
