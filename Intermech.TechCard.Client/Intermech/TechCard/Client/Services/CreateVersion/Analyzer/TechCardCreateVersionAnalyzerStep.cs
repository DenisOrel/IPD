// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Services.CreateVersion.Analyzer.TechCardCreateVersionAnalyzerStep
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Diagnostics;

#nullable disable
namespace Intermech.TechCard.Client.Services.CreateVersion.Analyzer;

internal abstract class TechCardCreateVersionAnalyzerStep
{
  /// <summary>
  /// 
  /// </summary>
  protected TechCardCreateVersionAnalyzerStepData _stepData;

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  protected abstract bool DoExecute();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="stepData"></param>
  /// <returns></returns>
  public bool Execute([NotNull] TechCardCreateVersionAnalyzerStepData stepData)
  {
    this._stepData = stepData;
    return this.DoExecute();
  }
}
