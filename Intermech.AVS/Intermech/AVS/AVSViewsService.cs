// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.AVSViewsService
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

#nullable disable
namespace Intermech.AVS;

/// <summary> Класс сервиса окна AVS-са для работы с вьюшками </summary>
internal class AVSViewsService : IAVSViewsService
{
  private AVSWindow _avsWindow;

  public AVSViewsService(AVSWindow avsWindow) => this._avsWindow = avsWindow;

  public AVSWindow AVSWindow
  {
    get => this._avsWindow;
    set => this._avsWindow = value;
  }
}
