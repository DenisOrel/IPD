
// Type: Intermech.Search.Diff.PropertyDiffCollection
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;


namespace Intermech.Search.Diff;

[TypeConverter(typeof (PropertyDiffCollectionConverter))]
public sealed class PropertyDiffCollection : DiffCollectionBase<PropertyDiff>
{
  private Dictionary<string, PropertyDiff> _diffDictionary = new Dictionary<string, PropertyDiff>();

  public PropertyDiffCollection(object @object, object otherObject)
  {
    if (@object == null)
      throw new ArgumentNullException("@object");
    if (otherObject == null)
      throw new ArgumentNullException(nameof (otherObject));
    foreach (PropertyInfo property in @object.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
    {
      DiffOperand fistOperand = new DiffOperand(property.GetValue(@object, new object[0]));
      DiffOperand secondOperand = new DiffOperand(property.GetValue(otherObject, new object[0]));
      PropertyDiff propertyDiff = new PropertyDiff(property, fistOperand, secondOperand);
      this._diffDictionary.Add(property.Name, propertyDiff);
    }
  }

  public PropertyDiff this[string propertyName] => this._diffDictionary[propertyName];

  public override IEnumerator<PropertyDiff> GetEnumerator()
  {
    return (IEnumerator<PropertyDiff>) this._diffDictionary.Values.GetEnumerator();
  }
}
