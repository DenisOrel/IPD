// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Interfaces.Validate.ValidateDocumentHandler
// Assembly: Intermech.TechCard.Document.Interfaces, Version=7.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: D9DB0A36-F52B-4632-90E0-E8B14A322D86
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Interfaces.Document;

#nullable disable
namespace Intermech.TechCard.Document.Interfaces.Validate;

public class ValidateDocumentHandler
{
  private readonly ValidateDocumentMode _mode;
  private readonly bool _defaultValue;
  private readonly IValidateDocumentAction[] _actions;

  public ValidateDocumentHandler(
    ValidateDocumentMode mode,
    bool defaultValue,
    [NotNull] params IValidateDocumentAction[] actions)
  {
    this._mode = mode;
    this._defaultValue = defaultValue;
    this._actions = actions;
  }

  public bool Execute([NotNull] ImDocumentData document)
  {
    foreach (IValidateDocumentAction action in this._actions)
    {
      bool flag = action.Validate(document);
      switch (this._mode)
      {
        case ValidateDocumentMode.AnyAction:
          if (flag)
            return true;
          break;
        case ValidateDocumentMode.AllAction:
          if (!flag)
            return false;
          break;
      }
    }
    return this._defaultValue;
  }
}
