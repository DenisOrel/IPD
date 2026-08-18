// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.AttributesDescribers.ImbaseLinkAttributesDescriber
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Client.Core.PropertyEditors;
using Intermech.Imbase.AttributesDescribers.Editors;

#nullable disable
namespace Intermech.Imbase.AttributesDescribers;

internal class ImbaseLinkAttributesDescriber : AttributablePropertyDescriber
{
  public override object GetPropDescriptorEditor(int attributeId)
  {
    return (object) new ImbaseLinkAttributesEditor(attributeId);
  }
}
