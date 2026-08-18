
// Type: Intermech.Search.Diff.TypeDiffPropertyDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;
using System.Reflection;


namespace Intermech.Search.Diff;

public sealed class TypeDiffPropertyDescriptor : DiffPropertyDescriptorBase<PropertyDiff>
{
  public TypeDiffPropertyDescriptor(Type componentType, PropertyInfo propertyInfo)
    : base(componentType, TypeDiffPropertyDescriptor.GetDisplayName(propertyInfo), propertyInfo.GetType())
  {
    this.PropertyInfo = !(propertyInfo == (PropertyInfo) null) ? propertyInfo : throw new ArgumentNullException(nameof (propertyInfo));
  }

  public PropertyInfo PropertyInfo { get; private set; }

  public override PropertyDiff GetDiff(IDiffCollection<PropertyDiff> diffCollection)
  {
    if (diffCollection == null)
      throw new ArgumentNullException(nameof (diffCollection));
    if (!(diffCollection is PropertyDiffCollection))
      throw new ArgumentException();
    return ((PropertyDiffCollection) diffCollection)[this.PropertyInfo.Name];
  }

  private static string GetDisplayName(PropertyInfo propertyInfo)
  {
    return !(Attribute.GetCustomAttribute((MemberInfo) propertyInfo, typeof (DisplayNameAttribute)) is DisplayNameAttribute customAttribute) ? propertyInfo.Name : customAttribute.DisplayName;
  }
}
