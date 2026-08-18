
// Type: Intermech.Client.Core.FormDesigner.NoneAction
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;


namespace Intermech.Client.Core.FormDesigner;

/// <summary>Нет действий.</summary>
internal class NoneAction : IFormDesignerActionHandler
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="button"></param>
  /// <param name="form"></param>
  /// <returns></returns>
  public bool ButtonEnabled(object button, object form) => false;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="button"></param>
  /// <param name="form"></param>
  public void ButtonPressed(object button, object form)
  {
  }
}
