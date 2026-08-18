// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Electrical.BoardReader`1
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.Electrical;

public abstract class BoardReader<TBoard>
{
  protected ECADIntegratorSettings settings;

  public BoardReader(ECADIntegratorSettings settings) => this.settings = settings;

  /// <summary>Чтение Обозначения для board.</summary>
  /// <param name="boardName">Имя board</param>
  /// <param name="component">Компонент, описывающий атрибуты схемы</param>
  protected abstract string ReadDesignation(string boardName, IValueBagContainer component);

  /// <summary>Чтение Наименования для board.</summary>
  /// <param name="boardName">Имя board</param>
  /// <param name="component">Компонент, описывающий атрибуты схемы</param>
  protected abstract string ReadName(string boardName, IValueBagContainer component);

  /// <summary>Чтение признака главной единицы проекта</summary>
  /// <param name="component">Компонент, описывающий атрибуты схемы</param>
  protected abstract bool ReadIsMain(IValueBagContainer component);

  /// <summary>Чтение уникального ключа главной единицы проекта</summary>
  /// <param name="component">Компонент, описывающий атрибуты схемы</param>
  protected abstract string ReadArticleKey(IValueBagContainer component);

  /// <summary>Компонент, описывающий единицу проекта</summary>
  /// <param name="board">Единица проекта</param>
  protected abstract IValueBagContainer GetAsmComponent(TBoard board);

  /// <summary>Метод формирует список единиц проекта</summary>
  /// <returns></returns>
  public List<BoardData<TBoard>> GetBoards(Dictionary<string, TBoard> projectItems)
  {
    bool flag = false;
    if (projectItems.Count <= 0)
      return (List<BoardData<TBoard>>) null;
    List<BoardData<TBoard>> boards = new List<BoardData<TBoard>>();
    foreach (KeyValuePair<string, TBoard> projectItem in projectItems)
    {
      IValueBagContainer asmComponent = this.GetAsmComponent(projectItem.Value);
      string designation = this.ReadDesignation(projectItem.Key, asmComponent);
      string name = this.ReadName(projectItem.Key, asmComponent);
      bool mainSchema = this.ReadIsMain(asmComponent);
      if (mainSchema)
        flag = !flag ? true : throw new Exception("В проекте присутствует несколько главных схем.");
      string articleKey = this.ReadArticleKey(asmComponent);
      BoardData<TBoard> boardData = new BoardData<TBoard>(projectItem.Value, this.GetAsmComponent(projectItem.Value), mainSchema, designation, name, articleKey);
      boards.Add(boardData);
    }
    return boards;
  }
}
