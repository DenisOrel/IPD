// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.RuleConverter
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor;

internal class RuleConverter : TypeConverter
{
  private Dictionary<string, string> _rulesDictionary;

  public RuleConverter(Type type)
  {
  }

  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    if (string.IsNullOrEmpty(value as string))
      return (object) string.Empty;
    if (this._rulesDictionary == null)
      this._rulesDictionary = ConfigEditorHelper.GetHelper().VersionRulesDictionary;
    string str;
    return this._rulesDictionary != null && this._rulesDictionary.TryGetValue(value.ToString(), out str) ? (object) str : value;
  }
}
