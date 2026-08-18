// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Data.Filters.IFilter
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Kernel.Search;
using System.Collections.Generic;


namespace Intermech.Search.Data.Filters;

public interface IFilter
{
  bool Apply(CompositionPart compositionPart);

  bool Apply(Applicability applicability);

  IEnumerable<CompositionPart> Apply(IEnumerable<CompositionPart> composition);

  IEnumerable<Applicability> Apply(IEnumerable<Applicability> applicabilities);

  List<ColumnDescriptor> Columns { get; }

  void Configure(FilterOptions options);
}
