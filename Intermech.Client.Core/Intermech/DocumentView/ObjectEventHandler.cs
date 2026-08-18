
// Type: Intermech.DocumentView.ObjectEventHandler
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.DocumentView;

/// <summary>
/// Represents methods that handle <see cref="T:Intermech.Map.MapObjectEventArgs" />.
/// </summary>
[Serializable]
public delegate void ObjectEventHandler(object sender, ObjectEventArgs e);
