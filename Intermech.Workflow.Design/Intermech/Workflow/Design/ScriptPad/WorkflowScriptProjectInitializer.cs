// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.ScriptPad.WorkflowScriptProjectInitializer
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Scripting.Services;

#nullable disable
namespace Intermech.Workflow.Design.ScriptPad;

/// <summary>
/// Класс инициализатора для проектов сценариев Workflow.
/// Реализация является thread safe.
/// </summary>
public sealed class WorkflowScriptProjectInitializer : DBScriptProjectInitializer
{
  private const string scriptCodeTemplate = "using System;\r\nusing Intermech.Interfaces;\r\nusing Intermech.Interfaces.Workflow;\r\n\r\npublic class Script\r\n{\r\n    public ICSharpScriptContext ScriptContext { get; set; }\r\n\r\n    public void Execute(IActivity activity)\r\n    {\r\n        //Вставьте ваш код здесь\r\n    }\r\n}\r\n";

  /// <summary>Создает объект.</summary>
  public WorkflowScriptProjectInitializer()
    : base("using System;\r\nusing Intermech.Interfaces;\r\nusing Intermech.Interfaces.Workflow;\r\n\r\npublic class Script\r\n{\r\n    public ICSharpScriptContext ScriptContext { get; set; }\r\n\r\n    public void Execute(IActivity activity)\r\n    {\r\n        //Вставьте ваш код здесь\r\n    }\r\n}\r\n")
  {
  }
}
