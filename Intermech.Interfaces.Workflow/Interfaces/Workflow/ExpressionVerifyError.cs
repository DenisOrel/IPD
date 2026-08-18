// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Workflow.ExpressionVerifyError
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

#nullable disable
namespace Intermech.Interfaces.Workflow;

public class ExpressionVerifyError
{
  private string _errorText = string.Empty;

  public ExpressionVerifyError(string errorText) => this._errorText = errorText;

  public string ErrorText => this._errorText;
}
