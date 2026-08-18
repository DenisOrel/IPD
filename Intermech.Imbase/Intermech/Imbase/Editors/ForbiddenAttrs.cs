// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Editors.ForbiddenAttrs
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.PropertyEditors;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Imbase.Editors;

internal class ForbiddenAttrs : ISelectorFilter
{
  internal List<int> _attrsIDs;

  internal ForbiddenAttrs(List<int> attrsIDs) => this._attrsIDs = attrsIDs;

  public bool IsInFilter(int category, object id) => this._attrsIDs.Contains(Convert.ToInt32(id));
}
