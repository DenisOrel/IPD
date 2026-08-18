// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Reports.ReportsPaсkageException
// Assembly: Intermech.Interfaces.Reports, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3A40A7D8-A018-4590-B8F9-C63911182943
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Reports.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Reports.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Reports;

/// <summary>
/// 
/// </summary>
public class ReportsPaсkageException : Exception
{
  /// <summary>Constructor</summary>
  /// <param name="message"></param>
  public ReportsPaсkageException(string message)
    : base(message)
  {
  }

  /// <summary>Constructor</summary>
  /// <param name="message"></param>
  /// <param name="innerException"></param>
  public ReportsPaсkageException(string message, Exception innerException)
    : base(message, innerException)
  {
  }
}
