// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.TechCard.TechParams.TechCardParamsTechProc
// Assembly: Intermech.Interfaces.TechCard, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B0F892EF-B72A-4A7D-8F43-9EB461AAC859
// Assembly location: D:\IPS\Client\Intermech.Interfaces.TechCard.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.TechCard.xml

using Intermech.Interfaces.Configuration;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.TechCard.TechParams;

/// <summary>
/// Класс для хранения параметров TechCard для "Техпроцесса"
/// </summary>
[Serializable]
public class TechCardParamsTechProc : AppSettingsBase
{
  /// <summary>
  /// 
  /// </summary>
  protected bool _openWindowAfterCreate = true;

  /// <summary>Открывать объект в новом окне после создания</summary>
  public bool OpenWindowAfterCreate
  {
    [DebuggerStepThrough] get => this._openWindowAfterCreate;
    set => this._openWindowAfterCreate = value;
  }
}
