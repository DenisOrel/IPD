// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Expert.CompGenParms
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.Expert;

/// <summary>Параметры генерации комплекта документов</summary>
[Category("Expert System")]
[Serializable]
public class CompGenParms
{
  /// <summary>ИД задачи экспертной системы</summary>
  public int TaskId { get; set; }

  /// <summary>ИД шаблона комплекта документов</summary>
  public long CompScriptId { get; set; }

  /// <summary>ИД объекта</summary>
  public long ContextId { get; set; }

  /// <summary>ИД уже существующего комплекта документов (если есть)</summary>
  public long ComplectId { get; set; }

  /// <summary>
  /// true, если надо создать дополнительные комплекты, false - если основной комплект
  /// </summary>
  public bool DopComplects { get; set; }

  public CompGenParms(int taskId, long compScriptId, long contextId)
  {
    this.TaskId = taskId;
    this.CompScriptId = compScriptId;
    this.ContextId = contextId;
    this.ComplectId = 0L;
    this.DopComplects = false;
  }

  public CompGenParms(int taskId, long compScriptId, long contextId, long complectId)
  {
    this.TaskId = taskId;
    this.CompScriptId = compScriptId;
    this.ContextId = contextId;
    this.ComplectId = complectId;
    this.DopComplects = false;
  }

  public CompGenParms(
    int taskId,
    long compScriptId,
    long contextId,
    long complectId,
    bool dopComplects)
  {
    this.TaskId = taskId;
    this.CompScriptId = compScriptId;
    this.ContextId = contextId;
    this.ComplectId = complectId;
    this.DopComplects = dopComplects;
  }
}
