// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.UsageData
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

#nullable disable
namespace Intermech.Project.Controls;

public class UsageData
{
  public readonly double _PossibleWork;
  public readonly double _Work;
  public readonly double _PeakLoad;

  public UsageData(double possibleWork, double work, double peakLoad)
  {
    this._PossibleWork = possibleWork;
    this._Work = work;
    this._PeakLoad = peakLoad;
  }

  public double Load => this._Work / this._PossibleWork;
}
