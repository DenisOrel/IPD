// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.SameDocSection
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data.EntityDb;
using Intermech.Data.SectionEntities;
using Intermech.IO;
using System;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

internal sealed class SameDocSection
{
  public static readonly SectionPropertyReference ReferenceRef = new SectionPropertyReference(typeof (SameDocSection), nameof (Reference));
  public static readonly SectionPropertyReference IdentityValueRef = new SectionPropertyReference(typeof (SameDocSection), nameof (IdentityValue));
  private readonly SameDocReference reference;
  private readonly string masterFile;
  private readonly string identityValue;

  public SameDocSection(SameDocReference reference, string masterFile, string identityValue)
  {
    if (reference == null)
      throw new ArgumentNullException(nameof (reference));
    if (string.IsNullOrEmpty(masterFile))
      throw new ArgumentException();
    if (string.IsNullOrEmpty(identityValue))
      throw new ArgumentException();
    this.reference = reference;
    this.masterFile = masterFile;
    this.identityValue = identityValue;
  }

  [Indexable(IndexType.Auto, true)]
  public SameDocReference Reference => this.reference;

  public string MasterFile => this.masterFile;

  [Indexable(IndexType.Auto, true)]
  [Comparer(typeof (ServiceObjectAttribute.NewObject), new object[] {typeof (PathComparer)})]
  public string IdentityValue => this.identityValue;
}
