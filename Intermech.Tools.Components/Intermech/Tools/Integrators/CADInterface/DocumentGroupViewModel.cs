// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.DocumentGroupViewModel
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Реализует модель представления для типа DocumentGroup, обеспечивающий подсветку изменений в PropertyGrid.
/// </summary>
public sealed class DocumentGroupViewModel : ICloneable, IEquatable<DocumentGroupViewModel>
{
  private string name;
  private List<GlobalId<int>> documentTypes;

  public DocumentGroupViewModel()
  {
    this.name = string.Empty;
    this.documentTypes = new List<GlobalId<int>>(0);
  }

  public DocumentGroupViewModel(DocumentGroup group)
  {
    this.name = group != null ? group.Name : throw new ArgumentNullException(nameof (group));
    this.documentTypes = new List<GlobalId<int>>((IEnumerable<GlobalId<int>>) group.DocumentTypes);
  }

  public DocumentGroupViewModel(string name, List<GlobalId<int>> documentTypes)
  {
    if (name == null)
      throw new ArgumentNullException(nameof (name));
    if (documentTypes == null)
      throw new ArgumentNullException(nameof (documentTypes));
    this.name = name;
    this.documentTypes = documentTypes;
  }

  public bool IsUnnamed
  {
    [DebuggerStepThrough] get => this.Name == string.Empty;
  }

  public string Name
  {
    [DebuggerStepThrough] get => this.name;
  }

  public List<GlobalId<int>> DocumentTypes
  {
    [DebuggerStepThrough] get => this.documentTypes;
  }

  public DocumentGroupViewModel Clone()
  {
    return new DocumentGroupViewModel(this.Name, new List<GlobalId<int>>((IEnumerable<GlobalId<int>>) this.DocumentTypes));
  }

  object ICloneable.Clone() => (object) this.Clone();

  public override int GetHashCode() => this.DocumentTypes.Count ^ this.Name.GetHashCode();

  public override bool Equals(object obj)
  {
    return !(obj is DocumentGroupViewModel other) ? base.Equals(obj) : this.Equals(other);
  }

  public bool Equals(DocumentGroupViewModel other)
  {
    if (other == null || other.Name != this.Name || other.DocumentTypes.Count != this.DocumentTypes.Count)
      return false;
    for (int index = 0; index < other.DocumentTypes.Count; ++index)
    {
      if (!this.DocumentTypes.Contains(other.DocumentTypes[index]))
        return false;
    }
    return true;
  }

  public override string ToString() => LocalizationHolder.rm.GetString("Tools.Components_358");
}
