
// Type: Intermech.Navigator.DBObjects.IVersionsDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Navigator.DBObjects;

internal interface IVersionsDescriptor
{
  long ObjectID { get; }

  long ID { get; }

  int ObjectTypeID { get; }

  VersionsWindowVisualModes VisualMode { get; }

  DateTime CurrentDate { get; set; }

  string ObjectCaption { get; }

  string Path { get; }

  NodeColumnCollection TreeColumns { get; }
}
