
// Type: Intermech.Tools.Settings.PropertyEditors.IObjectTypeListAdapter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Tools.Settings.PropertyEditors;

public interface IObjectTypeListAdapter
{
  object Create(Guid objectTypeGuid, int objectTypeId, string objectTypeName);

  int GetObjectTypeId(object listItem);

  string GetObjectTypeName(object listItem);
}
