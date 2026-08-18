// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.ExpressionInfo
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using Intermech.Interfaces;
using System;

#nullable disable
namespace Intermech.Workflow;

[Serializable]
public class ExpressionInfo
{
  private int _objectTypeForLink;
  private string _formulaForLink;
  private bool _elseLink;
  private Guid _linkGuid = Guid.Empty;
  private long _linkID = -1;
  private string _objectTypeName = string.Empty;
  private string _currentActivityTitle = "Любой тип объекта";

  public int ObjectTypeForLink
  {
    get => this._objectTypeForLink;
    set
    {
      this._objectTypeForLink = value;
      if (string.IsNullOrEmpty(this._objectTypeName))
        return;
      this._objectTypeName = string.Empty;
    }
  }

  public Guid LinkGuid
  {
    get => this._linkGuid;
    set => this._linkGuid = value;
  }

  public long LinkID
  {
    get => this._linkID;
    set => this._linkID = Math.Abs(value);
  }

  /// <summary>Формула для вычисления.</summary>
  public string FormulaForLink
  {
    get => this._formulaForLink;
    set => this._formulaForLink = value;
  }

  /// <summary>True - это ссылка "Иначе", False - Это обычная ссылка</summary>
  public bool ElseLink
  {
    get => this._elseLink;
    set
    {
      this._elseLink = value;
      if (this._elseLink)
      {
        this.FormulaForLink = "ИНАЧЕ";
      }
      else
      {
        if (!(this.FormulaForLink == "ИНАЧЕ"))
          return;
        this.FormulaForLink = string.Empty;
      }
    }
  }

  public string ObjectTypeName
  {
    get
    {
      if (string.IsNullOrEmpty(this._objectTypeName))
        this._objectTypeName = this.ObjectTypeForLink < 0 ? this._currentActivityTitle : MetaDataHelper.GetObjectTypeName(this.ObjectTypeForLink) ?? "";
      return this._objectTypeName;
    }
  }

  public ExpressionInfo(
    int objectTypeForLink,
    Guid linkGuid,
    long linkID,
    string formula,
    bool elseLink = false)
  {
    this._objectTypeForLink = objectTypeForLink;
    this._linkGuid = linkGuid;
    this._formulaForLink = formula;
    this._elseLink = elseLink;
    this._linkID = linkID;
  }

  /// <summary>
  /// стандартный конструктор без параметров нужен для правильной десериализации
  /// </summary>
  public ExpressionInfo()
  {
  }

  public override string ToString() => this.FormulaForLink;
}
