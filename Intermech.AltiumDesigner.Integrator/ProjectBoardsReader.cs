// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.ProjectBoardsReader
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.Data;
using Intermech.Tools.Data;
using Intermech.Tools.Integrators.Electrical;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal sealed class ProjectBoardsReader(ECADIntegratorSettings settings) : BoardReader<ADDocument>(settings)
{
  public static List<BoardData<ADDocument>> Read(
    ADIntegratorSettings settings,
    Dictionary<string, ADDocument> projectItems)
  {
    return new ProjectBoardsReader((ECADIntegratorSettings) settings).GetBoards(projectItems);
  }

  protected override string ReadDesignation(string boardName, IValueBagContainer component)
  {
    string designationPropName = this.BoardDesignationPropName;
    return Convert.ToString(ParametersHelper.GetParameterValue(((ParametersContainer) component).Parameters, designationPropName) ?? throw new Exception($"В штампе файла {boardName} не найден параметр {designationPropName} для определения обозначения документа."));
  }

  protected override string ReadName(string boardName, IValueBagContainer component)
  {
    string boardNamePropName = this.BoardNamePropName;
    return Convert.ToString(ParametersHelper.GetParameterValue(((ParametersContainer) component).Parameters, boardNamePropName) ?? throw new Exception($"В штампе файла {boardName} не найден параметр {boardNamePropName} для определения наименования документа."));
  }

  protected override bool ReadIsMain(IValueBagContainer component) => false;

  protected override IValueBagContainer GetAsmComponent(ADDocument board) => board.Properties;

  protected override string ReadArticleKey(IValueBagContainer component)
  {
    return ((ParametersContainer) component).InternalId;
  }

  private string BoardDesignationPropName
  {
    get
    {
      return (string) (this.settings.DocumentAttributesTable.Find((Predicate<Tuple<StringKey, StringKey, bool>>) (x => x.Item1 == (StringKey) IDCache.Default.Designation.Text)) ?? throw new Exception("В настройках интегратора не указан параметр документа соотвестствующий атрибуту Обозначение.")).Item2;
    }
  }

  private string BoardNamePropName
  {
    get
    {
      return (string) (this.settings.DocumentAttributesTable.Find((Predicate<Tuple<StringKey, StringKey, bool>>) (x => x.Item1 == (StringKey) IDCache.Default.Name.Text)) ?? throw new Exception("В настройках интегратора не указан параметр документа соотвестствующий атрибуту Наименование.")).Item2;
    }
  }
}
