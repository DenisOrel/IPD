
// Type: Intermech.Navigator.Conditions.ConditionEditorAttributeService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;


namespace Intermech.Navigator.Conditions;

/// <summary>Служба обработки условий для специальных атрибутов</summary>
internal sealed class ConditionEditorAttributeService : IConditionEditorAttributeService
{
  /// <summary>Кэш обработчиков</summary>
  private Dictionary<Guid, IConditionEditorAttribute> _handlers = new Dictionary<Guid, IConditionEditorAttribute>();

  public void Register(Guid attributeGuid, IConditionEditorAttribute handler)
  {
    if (this._handlers.ContainsKey(attributeGuid))
      throw new Exception(string.Format(LocalizationHolder.rm.GetString("Client.Core_1501"), (object) attributeGuid));
    this._handlers.Add(attributeGuid, handler);
  }

  public IConditionEditorAttribute GetHandler(Guid attributeGuid)
  {
    IConditionEditorAttribute conditionEditorAttribute;
    return this._handlers.TryGetValue(attributeGuid, out conditionEditorAttribute) ? conditionEditorAttribute : (IConditionEditorAttribute) null;
  }
}
