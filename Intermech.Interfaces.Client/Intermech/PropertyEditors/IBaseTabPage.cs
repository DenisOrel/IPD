// Decompiled with JetBrains decompiler
// Type: Intermech.PropertyEditors.IBaseTabPage
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Windows.Forms;

#nullable disable
namespace Intermech.PropertyEditors;

/// <summary>Интерфейс TabPage для страниц DatabaseConfigurator</summary>
public interface IBaseTabPage
{
  /// <summary>Выполнить док в панель</summary>
  /// <param name="panel"></param>
  void DockToPanel(Panel panel);

  /// <summary>Вернуть интерфейс формы в TabPage</summary>
  ITabPageForm TabPageProcessingForm { get; }
}
