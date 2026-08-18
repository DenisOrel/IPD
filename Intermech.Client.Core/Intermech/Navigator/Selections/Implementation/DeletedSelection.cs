
// Type: Intermech.Navigator.Selections.Implementation.DeletedSelection
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Collections.Generic;


namespace Intermech.Navigator.Selections.Implementation;

internal class DeletedSelection
{
  public string SelectionName;
  public int Icon;
  public List<AttachedObject> ObjectTypes;
  public List<AttachedObject> ParentSelections;

  public DeletedSelection(string selectionName, int icon)
  {
    this.SelectionName = selectionName;
    this.Icon = icon;
    this.ObjectTypes = new List<AttachedObject>(1);
    this.ParentSelections = new List<AttachedObject>(1);
  }

  public void AddObjectType(string name, int icon)
  {
    this.ObjectTypes.Add(new AttachedObject(name, icon));
  }

  public void AddParentSelection(string name, int icon)
  {
    this.ParentSelections.Add(new AttachedObject(name, icon));
  }
}
