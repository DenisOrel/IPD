// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.XmlAttributeComparer
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor;

internal class XmlAttributeComparer : IComparer<KeyValuePair<string, object>>
{
  public int Compare(KeyValuePair<string, object> attr1, KeyValuePair<string, object> attr2)
  {
    if (attr1.Key == "guid")
      return -1;
    if (attr2.Key == "guid")
      return 1;
    if (attr1.Key == "name")
      return -1;
    return attr1.Key == "name" ? 1 : string.Compare(attr1.Key, attr2.Key, StringComparison.CurrentCultureIgnoreCase);
  }
}
