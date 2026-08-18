
// Type: Intermech.Search.UI.PropertyGrid.PropertyTabCollection
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms.Design;


namespace Intermech.Search.UI.PropertyGrid;

public sealed class PropertyTabCollection : IEnumerable<PropertyTab>, IEnumerable
{
  private Dictionary<Type, PropertyTab> _dictionary = new Dictionary<Type, PropertyTab>();

  public PropertyTabCollection(SimplePropertyGrid propertyGrid)
  {
    this.PropertyGrid = propertyGrid != null ? propertyGrid : throw new ArgumentNullException(nameof (propertyGrid));
  }

  public event EventHandler TabTypeAdded;

  public SimplePropertyGrid PropertyGrid { get; private set; }

  public void AddTabType(Type propertyTabType)
  {
    this._dictionary[propertyTabType] = !(propertyTabType == (Type) null) ? Activator.CreateInstance(propertyTabType) as PropertyTab : throw new ArgumentNullException(nameof (propertyTabType));
    this.OnTabTypeAdded();
  }

  public PropertyTab this[Type propertyTabType]
  {
    get
    {
      if (propertyTabType == (Type) null)
        throw new ArgumentNullException(nameof (propertyTabType));
      PropertyTab propertyTab = (PropertyTab) null;
      this._dictionary.TryGetValue(propertyTabType, out propertyTab);
      return propertyTab;
    }
  }

  public IEnumerator<PropertyTab> GetEnumerator()
  {
    return (IEnumerator<PropertyTab>) this._dictionary.Values.GetEnumerator();
  }

  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

  private void OnTabTypeAdded()
  {
    EventHandler tabTypeAdded = this.TabTypeAdded;
    if (tabTypeAdded == null)
      return;
    tabTypeAdded((object) this, new EventArgs());
  }
}
