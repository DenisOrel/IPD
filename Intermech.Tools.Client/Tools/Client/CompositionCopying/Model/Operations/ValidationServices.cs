// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.Operations.ValidationServices
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;
using System.Linq;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model.Operations;

internal static class ValidationServices
{
  public static void ValidateObject<T>(T instance, IObjectValidator<T> validator)
  {
    if (validator == null)
      throw new ArgumentNullException(nameof (validator));
    ValidationContext context = new ValidationContext((object) instance);
    OperationError operationError = validator.Validate(instance, context).FirstOrDefault<OperationError>();
    if (operationError != null)
      throw new InvalidOperationException(operationError.Message);
  }
}
