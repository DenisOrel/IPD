// Decompiled with JetBrains decompiler
// Type: Intermech.Project.TaskFilter
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using Intermech.Project.Evaluator;
using Intermech.Project.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Project;

public class TaskFilter : IComparable
{
  [NotNull]
  [NotEmpty]
  private string _name = string.Empty;
  [NotNull]
  public readonly ExpressionList Expressions = new ExpressionList();
  public FilterFlags Flags;
  [CanBeNull]
  private string _sortValue;
  [CanBeNull]
  private static string _allTasksFilterName;
  /// <summary>Карандаш в виде строки (для экономии GDI ресурсов, объект вида карандаш создаётся по месту использования)</summary>
  /// <remarks>Строка генерируется с помощью GraphicFuncs.PenToString и преобразуется в карандаш с помощью GraphicFuncs.StringToPen(</remarks>
  /// &gt;
  [NotNull]
  public string PenStr = string.Empty;
  /// <summary>Кисть в виде строки (для экономии GDI ресурсов, объект вида кисть создаётся по месту использования)</summary>
  /// <remarks>Строка генерируется с помощью GraphicFuncs.BrushToString и преобразуется в кисть с помощью GraphicFuncs.StringToBrush(</remarks>
  /// &gt;
  [NotNull]
  public string BrushStr = string.Empty;
  [CanBeNull]
  public object Tag;

  [NotNull]
  [NotEmpty]
  public string Name
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._name;
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this._name = value;
      this._sortValue = this.AllTasks ? "!" : (string) null;
    }
  }

  public TaskFilter([NotNull, NotEmpty] string name, [NotNull, ItemNotNull] ExpressionList expressions)
  {
    this.Name = name;
    this.Expressions = expressions;
  }

  public TaskFilter([NotNull, NotEmpty] string name, [NotNull] Expression expression)
    : this(name, new ExpressionList((IEnumerable<Expression>) new Expression[1]
    {
      expression
    }))
  {
  }

  public TaskFilter([NotNull] string name) => this.Name = name;

  public TaskFilter([CanBeNull] TaskFilter proto)
  {
    if (proto == null)
      return;
    this.Assign(proto);
  }

  public TaskFilter()
    : this(string.Empty)
  {
  }

  public override string ToString()
  {
    string name = this.Name;
    if (this.RequiresInput)
      name += "...";
    return name;
  }

  public void Assign([NotNull] TaskFilter filter)
  {
    this.Name = filter.Name;
    this.Expressions.Clear();
    this.Expressions.AddRange(filter.Expressions.Select<Expression, Expression>((Func<Expression, Expression>) (e => new Expression(e))));
    this.Flags = filter.Flags;
    this.PenStr = filter.PenStr;
    this.BrushStr = filter.BrushStr;
  }

  public override bool Equals([CanBeNull] object obj)
  {
    if (obj == null)
      return false;
    if (this == obj)
      return true;
    return obj is TaskFilter taskFilter && string.Equals(this.Name, taskFilter.Name, StringComparison.Ordinal) && this.Flags == taskFilter.Flags && this.Expressions.Equals((object) taskFilter.Expressions) && string.Equals(this.PenStr, taskFilter.PenStr, StringComparison.Ordinal) && this.BrushStr.Equals(taskFilter.BrushStr) && base.Equals((object) taskFilter);
  }

  public override int GetHashCode()
  {
    return (this.Name, this.Flags, this.Expressions, this.PenStr, this.BrushStr).GetHashCode();
  }

  public bool HasFlag(FilterFlags flag) => (this.Flags & flag) == flag;

  public void SetFlag(FilterFlags flag, bool value)
  {
    if (value)
      this.Flags |= flag;
    else
      this.Flags &= ~flag;
  }

  public void Save(int index, [NotNull] XmlIni ini)
  {
    string Section1 = "f" + index.ToString();
    ini.WriteString(Section1, "Name", this.Name);
    ini.WriteInteger(Section1, "Flags", (long) this.Flags);
    ini.WriteInteger(Section1, "Count", (long) this.Expressions.Count);
    for (int index1 = 0; index1 < this.Expressions.Count; ++index1)
    {
      Expression expression = this.Expressions[index1];
      string Section2 = $"{Section1}/e{(object) (index1 + 1)}";
      ini.WriteString(Section2, "Prop", expression.Property?.Name ?? string.Empty);
      ini.WriteString(Section2, "Op", expression.Operation?.Symbol ?? string.Empty);
      ini.WriteString(Section2, "Group", Convert.ToInt32((object) expression.GroupOperation).ToString(), "0");
      object obj = (object) null;
      if (expression.Value != null)
      {
        obj = expression.Value;
        if (obj is PossibleValue possibleValue)
          obj = possibleValue.Value;
      }
      ini.WriteString(Section2, "Value", obj?.ToString() ?? string.Empty);
    }
    ini.WriteString(Section1, "Brush", this.BrushStr);
    ini.WriteString(Section1, "Pen", this.PenStr);
  }

  public void Load(int index, [NotNull] XmlIni ini)
  {
    string Section1 = "f" + index.ToString();
    this.Name = ini.ReadString(Section1, "Name", string.Empty);
    this.Flags = (FilterFlags) ini.ReadInteger(Section1, "Flags", 0L);
    long num = ini.ReadInteger(Section1, "Count", 0L);
    for (int index1 = 0; (long) index1 < num; ++index1)
    {
      string Section2 = $"{Section1}/e{(object) (index1 + 1)}";
      string property = ini.ReadString(Section2, "Prop", string.Empty);
      string str1 = ini.ReadString(Section2, "Op", string.Empty);
      string str2 = ini.ReadString(Section2, "Value", string.Empty);
      string operation = str1;
      string str3 = str2;
      Expression expression = new Expression(property, operation, (object) str3);
      expression.GroupOperation = (GroupOperation) ini.ReadInteger(Section2, "Group", (long) expression.GroupOperation);
      this.Expressions.Add(expression);
    }
    this.BrushStr = ini.ReadString(Section1, "Brush");
    this.PenStr = ini.ReadString(Section1, "Pen");
  }

  [NotNull]
  protected virtual string SortValue => this._sortValue ?? this.Name;

  public virtual int CompareTo([CanBeNull] object other)
  {
    if (other == null)
      return 1;
    if (this == other)
      return 0;
    return !(other is TaskFilter taskFilter) ? 1 : string.Compare(this.SortValue, taskFilter.SortValue, StringComparison.Ordinal);
  }

  [NotNull]
  public static string AllTasksFilterName
  {
    get
    {
      return TaskFilter._allTasksFilterName ?? (TaskFilter._allTasksFilterName = Resources.FilterAllTasks);
    }
  }

  /// <summary>
  /// Требуется запрашивать ввод значения пользователя, или нет
  /// Значение должно быть уточнено, если в поле "значение" находится строка вида: "Текст запроса:"?
  /// </summary>
  public bool RequiresInput
  {
    get => this.Expressions.Any<Expression>((Func<Expression, bool>) (e => e.RequiresInput));
  }

  public bool IsPaintFilter => !string.IsNullOrEmpty(this.BrushStr);

  public bool AllTasks => this._name == TaskFilter.AllTasksFilterName;
}
