// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.PDMSubstitutesEditorOptionsHolder
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>
/// Вспомогательный класс, который позволяет управлять редактором допустимых замен
/// </summary>
public class PDMSubstitutesEditorOptionsHolder
{
  /// <summary>Режимы работы редактора допустимых замен</summary>
  public PDMSubstitutesEditorMode Mode;
  /// <summary>Форма спецификации</summary>
  public AVSSpecificationForm Form;
  /// <summary>
  /// Список исполнений, с которыми редактор допустимых замен может выполнять действия
  /// </summary>
  public List<long> Articles;

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="mode">Режимы работы редактора допустимых замен</param>
  /// <param name="form">Форма спецификации</param>
  /// <param name="articles">Список исполнений, с которыми редактор допустимых замен может выполнять действия</param>
  public PDMSubstitutesEditorOptionsHolder(
    PDMSubstitutesEditorMode mode,
    AVSSpecificationForm form,
    List<long> articles)
  {
    this.Mode = mode;
    this.Form = form;
    this.Articles = articles;
  }
}
