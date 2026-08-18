
// Type: Intermech.Project.Controls.SavedObjectViewProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;


namespace Intermech.Project.Controls;

/// <summary>Провайдер вьюшек для нода дерева сохранённого ранее состава объекта
///   WARNING: Писалось для дерева состава сохранённого ранее объекта, однако вьюшки там кажется не потребуются, так что код пока что (?)
///   не нужен
/// </summary>
public class SavedObjectViewProvider : IViewsProvider
{
  /// <summary>Gets the views</summary>
  /// <param name="items"></param>
  /// <param name="provider"></param>
  /// <returns>The views</returns>
  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider provider) => new ViewsInfo();
}
