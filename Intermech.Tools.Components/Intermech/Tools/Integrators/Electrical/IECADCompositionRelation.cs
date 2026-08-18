// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Electrical.IECADCompositionRelation
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.Electrical;

public interface IECADCompositionRelation
{
  void Handle(List<Guid> posGuidsCache);
}
