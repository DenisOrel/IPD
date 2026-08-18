
// Type: Intermech.PropertyEditors.PropertyFormsHolder
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections;


namespace Intermech.PropertyEditors;

/// <summary>Summary description for PropertyForms.</summary>
public class PropertyFormsHolder
{
  private static Hashtable propertyForms = new Hashtable();

  public static Intermech.PropertyEditors.PropertyForms PropertyForms(Guid instGuid)
  {
    return (Intermech.PropertyEditors.PropertyForms) PropertyFormsHolder.propertyForms[(object) instGuid];
  }

  public static void RegisterPropertyForms(Guid instGuid)
  {
    PropertyFormsHolder.propertyForms.Add((object) instGuid, (object) new Intermech.PropertyEditors.PropertyForms(instGuid));
  }

  public static void UnregisterPropertyForms(Guid instGuid)
  {
    Intermech.PropertyEditors.PropertyForms propertyForms = PropertyFormsHolder.PropertyForms(instGuid);
    PropertyFormsHolder.propertyForms.Remove((object) instGuid);
    propertyForms?.Dispose();
  }
}
