// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.AutoSelection.AutoSelectionLog.IAutoSelectionLogRec
// Assembly: Intermech.Interfaces.AutoSelection, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A8A58CF2-90E0-4922-B0EB-2EB55893A867
// Assembly location: D:\IPS\Client\Intermech.Interfaces.AutoSelection.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.AutoSelection.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.AutoSelection.AutoSelectionLog;

/// <summary>Interface for autoselection log record</summary>
public interface IAutoSelectionLogRec
{
  /// <summary>Owner record</summary>
  IAutoSelectionLogRec Owner { get; }

  /// <summary>Child record</summary>
  IList<IAutoSelectionLogRec> ChildsList { get; }

  /// <summary>Log data</summary>
  string Data { get; }
}
