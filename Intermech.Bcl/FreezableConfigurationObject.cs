
// Type: Intermech.FreezableConfigurationObject
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech
{
    public abstract class FreezableConfigurationObject : FreezableObject, ICloneable
    {
      object ICloneable.Clone() => this.DoClone();

      protected object DoClone()
      {
        FreezableConfigurationObject emptyObject = this.CreateEmptyObject();
        emptyObject.Assign(this);
        return (object) emptyObject;
      }

      protected virtual FreezableConfigurationObject CreateEmptyObject()
      {
        return (FreezableConfigurationObject) Activator.CreateInstance(this.GetType());
      }

      public abstract void Assign(FreezableConfigurationObject other);
    }
}
