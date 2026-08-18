
// Type: Intermech.Client.Core.CompositionView.BeforeAllCreations
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;


namespace Intermech.Client.Core.CompositionView;

/// <summary>
/// 
/// </summary>
/// <param name="sender"></param>
/// <param name="session"></param>
public delegate void BeforeAllCreations(object sender, IUserSession session);
