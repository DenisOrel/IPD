
// Type: Intermech.Navigator.Controls.GetSupportedColumnsEventHandler
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;


namespace Intermech.Navigator.Controls;

/// <summary>
/// Делегат для получения коллекции поддерживаемых колонок для указанного контрола
/// </summary>
/// <returns>Контрол, которому срочно требуется коллекция поддерживаемых колонок</returns>
public delegate NodeColumnCollection GetSupportedColumnsEventHandler(object sender);
