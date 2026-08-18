// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.NavigatorWindowCaptionsHelper
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Text;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Вспомогательный статический класс для работы с заголовками окон "Навигатора"
/// </summary>
public static class NavigatorWindowCaptionsHelper
{
  /// <summary>
  /// Обработчик события, собирающего информацию о заголовке окна "Навигатора"
  /// </summary>
  public static NavigatorWindowCaptionEventHandler OnGetNavigatorWindowCaption;

  /// <summary>Собрать информацию о заголовке окна "Навигатора"</summary>
  /// <param name="sender">Отправитель события</param>
  /// <param name="e">Аргументы события</param>
  public static void GetNavigatorWindowCaption(object sender, NavigatorWindowCaptionEventArgs e)
  {
    if (e == null || e.RootDescriptor == null || NavigatorWindowCaptionsHelper.OnGetNavigatorWindowCaption == null)
      return;
    Delegate[] invocationList = NavigatorWindowCaptionsHelper.OnGetNavigatorWindowCaption.GetInvocationList();
    StringBuilder stringBuilder1 = new StringBuilder(e.ExtraText);
    StringBuilder stringBuilder2 = new StringBuilder(e.TextHint);
    for (int index = 0; index < invocationList.Length; ++index)
    {
      NavigatorWindowCaptionEventArgs e1 = new NavigatorWindowCaptionEventArgs((object) e);
      e1.ExtraText = string.Empty;
      e1.TextHint = string.Empty;
      ((NavigatorWindowCaptionEventHandler) invocationList[index])(sender, e1);
      if (!string.IsNullOrEmpty(e1.ExtraText))
      {
        if (stringBuilder1.Length > 0)
          stringBuilder1.Append(" ");
        stringBuilder1.Append(e1.ExtraText);
      }
      if (!string.IsNullOrEmpty(e1.TextHint))
      {
        if (stringBuilder2.Length > 0)
          stringBuilder2.Append("\n");
        stringBuilder2.Append(e1.TextHint);
      }
    }
    e.ExtraText = stringBuilder1.ToString();
    e.TextHint = stringBuilder2.ToString();
  }
}
