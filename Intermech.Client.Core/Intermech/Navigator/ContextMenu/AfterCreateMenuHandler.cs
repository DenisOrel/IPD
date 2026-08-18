
// Type: Intermech.Navigator.ContextMenu.AfterCreateMenuHandler
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;


namespace Intermech.Navigator.ContextMenu;

/// <summary>
/// Делегат метода, который вызывается после построения контекстного меню.
/// </summary>
/// <param name="contextMenu">Визуальный компонент, реализующий контекстное меню</param>
/// <param name="viewServices">Контейнер с дополнительными сервисами</param>
public delegate void AfterCreateMenuHandler(Component contextMenu, IServiceProvider viewServices);
