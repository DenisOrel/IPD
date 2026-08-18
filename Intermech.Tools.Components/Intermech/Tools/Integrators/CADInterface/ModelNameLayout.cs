// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.ModelNameLayout
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data;
using Intermech.Tools.Components.Properties;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

internal sealed class ModelNameLayout(StringKey nameKey) : BasicAttributeLayout(nameKey, (ICollection<StringKey>) new StringKey[3]
{
  nameKey,
  (StringKey) CADDocumentResources.EMB_BasicArticleInstanceAttribute,
  (StringKey) CADDocumentResources.EMB_IndependentDesignation
})
{
}
