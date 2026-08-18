// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.AVSConfig.AVSConfigTypeDescriptor
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces.Client;
using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.AVS.AVSConfig;

[Serializable]
public class AVSConfigTypeDescriptor(AvsConfig baseAvsConfig) : ClassWrapperForPropertyGrid((object) baseAvsConfig)
{
  protected override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
  {
    if (this._globalizedProps == null)
    {
      this._globalizedProps = new PropertyDescriptorCollection((PropertyDescriptor[]) null);
      PropertyDescriptorCollection propertyDescriptions;
      if ((propertyDescriptions = this.BaseClass is AvsConfig baseClass ? baseClass.PropertyDescriptions : (PropertyDescriptorCollection) null) != null)
      {
        foreach (PropertyDescriptor propertyDescriptor in propertyDescriptions)
          this._globalizedProps.Add(propertyDescriptor);
      }
    }
    return this._globalizedProps;
  }
}
