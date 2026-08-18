// Decompiled with JetBrains decompiler
// Type: Intermech.Project.UnknownResource
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Project;

[Serializable]
public class UnknownResource : Resource
{
  [CanBeNull]
  private ResourceCollection _availableResources;
  [CanBeNull]
  private ResourceCollection _candidateResources;

  public UnknownResource()
    : base((ISessionProvider) null, 0L, string.Empty, 0)
  {
  }

  public UnknownResource([CanBeNull] IEnumerable<Resource> candidateResources)
    : this()
  {
    if (candidateResources != null)
    {
      ResourceCollection resourceCollection = new ResourceCollection();
      foreach (Resource candidateResource in candidateResources)
        resourceCollection.Add(candidateResource);
      this.CandidateResources = resourceCollection;
    }
    else
      this.CandidateResources = (ResourceCollection) null;
  }

  public UnknownResource(
    [NotNull, ItemNotNull] IEnumerable<Resource> candidateResources,
    [NotNull, ItemNotNull] ResourceCollection availableResources)
    : this(candidateResources)
  {
    this.AvailableResources = availableResources;
  }

  [CanBeNull]
  public virtual ResourceCollection AvailableResources
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._availableResources;
    [MethodImpl(MethodImplOptions.AggressiveInlining)] set => this._availableResources = value;
  }

  [CanBeNull]
  public virtual ResourceCollection CandidateResources
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._candidateResources;
    set
    {
      if (value == this.CandidateResources)
        return;
      if (value != null)
      {
        if (value.Count < 2)
          throw new ArgumentException("Candidate resources must contain more at least two members, otherwise set them to null or use a standard resource instead of this unknown resource.", nameof (CandidateResources));
        if (value.OfType<UnknownResource>().Any<UnknownResource>())
          throw new ArgumentException("Unknown resources are not allowed to be candidate resources.", nameof (CandidateResources));
      }
      this._candidateResources = value;
      this.OnPropertyChanged(nameof (CandidateResources));
    }
  }

  public sealed override string Name
  {
    get
    {
      if (this.CandidateResources == null)
        return IMProject.Unknown;
      if (this.AvailableResources != null)
      {
        bool flag = true;
        string str = (string) null;
        foreach (Resource candidateResource in (System.Collections.ObjectModel.Collection<Resource>) this.CandidateResources)
        {
          string functions = candidateResource.Functions;
          if (str == null && !string.IsNullOrEmpty(functions))
            str = functions;
          if (functions == null || !string.Equals(functions, str, StringComparison.InvariantCultureIgnoreCase))
          {
            flag = false;
            break;
          }
        }
        if (flag && !this.AvailableResources.Any<Resource>((Func<Resource, bool>) (resource2 => resource2.Functions == str && !this.CandidateResources.Contains(resource2))))
          return $"{IMProject.Unknown} {IMProject.CandidatesPreSymbol}{str}{IMProject.CandidatesPostSymbol}";
      }
      ResourceCollection availableResources = this.AvailableResources;
      if ((availableResources != null ? (availableResources.All<Resource>((Func<Resource, bool>) (resource3 => this.CandidateResources.Contains(resource3))) ? 1 : 0) : 1) != 0)
        return IMProject.Unknown;
      return $"{IMProject.Unknown} {IMProject.CandidatesPreSymbol}{string.Join(IMProject.ListSeparatorSymbol + " ", this.CandidateResources.Select<Resource, string>((Func<Resource, string>) (resource4 => resource4.Name)).ToArray<string>())}{IMProject.CandidatesPostSymbol}";
    }
    set
    {
      throw new ArgumentException("Name cannot be set for an Unknown resource.", nameof (Name));
    }
  }
}
