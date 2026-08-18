// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Expert.IExpertEditor
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

#nullable disable
namespace Intermech.Interfaces.Expert;

/// <summary>Интерфейс редактора объектов ЭС</summary>
public interface IExpertEditor
{
  /// <summary>Запустить редактор формул для редактирования условия</summary>
  /// <param name="cond">FormulaData, если условие существует, или null</param>
  /// <param name="title">Текст заголовка окна</param>
  /// <returns>true, если пользователь нажал OK</returns>
  bool EditCondition(ref object cond, string title);
}
