
// Type: Intermech.Navigator.Controls.ViewStateService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;


namespace Intermech.Navigator.Controls;

/// <summary>Состояние закладок</summary>
public class ViewStateService : IViewState
{
  /// <summary>Флажки состояния закладок</summary>
  private ViewStateFlags _flags;

  /// <summary>Создать экземпляр класса</summary>
  public ViewStateService()
    : this(ViewStateFlags.None)
  {
  }

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="flags">Флажки состояния закладок</param>
  public ViewStateService(ViewStateFlags flags) => this._flags = flags;

  public void SetViewStateFlags(ViewStateFlags viewStateFlags) => this._flags = viewStateFlags;

  /// <summary>Флажки состояния закладок</summary>
  ViewStateFlags IViewState.ViewState => this._flags;
}
