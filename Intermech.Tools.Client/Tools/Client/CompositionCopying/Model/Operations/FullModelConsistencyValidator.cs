// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.Operations.FullModelConsistencyValidator
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model.Operations;

internal sealed class FullModelConsistencyValidator : IObjectValidator<DBObjectGraph>
{
  private IObjectValidator<DBObjectGraph>[] children;

  public FullModelConsistencyValidator()
  {
    this.children = new IObjectValidator<DBObjectGraph>[4]
    {
      (IObjectValidator<DBObjectGraph>) new DocumentsValidator(),
      (IObjectValidator<DBObjectGraph>) new CADModelDrawingsValidator(),
      (IObjectValidator<DBObjectGraph>) new ArticlesValidator(),
      (IObjectValidator<DBObjectGraph>) new ScannedVerticesValidator()
    };
  }

  public IEnumerable<OperationError> Validate(DBObjectGraph instance, ValidationContext context)
  {
    IObjectValidator<DBObjectGraph>[] objectValidatorArray = this.children;
    for (int index = 0; index < objectValidatorArray.Length; ++index)
    {
      foreach (OperationError operationError in objectValidatorArray[index].Validate(instance, context))
        yield return operationError;
    }
    objectValidatorArray = (IObjectValidator<DBObjectGraph>[]) null;
  }
}
