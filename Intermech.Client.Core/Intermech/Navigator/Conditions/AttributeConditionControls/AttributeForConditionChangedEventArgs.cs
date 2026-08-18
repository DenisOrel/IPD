
// Type: Intermech.Navigator.Conditions.AttributeConditionControls.AttributeForConditionChangedEventArgs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Navigator.Conditions.AttributeConditionControls;

internal sealed class AttributeForConditionChangedEventArgs : EventArgs
{
  public object AttributeID { get; private set; }

  public string AttributeName { get; private set; }

  public AttributeForConditionChangedEventArgs()
    : this((object) null, string.Empty)
  {
  }

  public AttributeForConditionChangedEventArgs(object attributeID, string attributeName)
  {
    this.AttributeID = attributeID;
    this.AttributeName = attributeName;
  }
}
