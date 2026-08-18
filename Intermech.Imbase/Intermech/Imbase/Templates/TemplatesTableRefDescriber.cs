// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Templates.TemplatesTableRefDescriber
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using Intermech.PropertyEditors;
using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.Imbase.Templates;

internal class TemplatesTableRefDescriber : IAttributePropertyDescriber
{
  public TypeConverter GetPropDescriptorConverter(int attributeId) => (TypeConverter) null;

  public bool GetPropDescriptorReadonly(int attributeId, bool baseReadonly) => baseReadonly;

  public string GetPropDescriptorMask(int attributeId, string baseMask) => baseMask;

  public object GetAttributeValue(IElementInfo iElementInfo, int attributeId, object propertyValue)
  {
    return propertyValue == null ? (object) null : (object) (propertyValue as TemplatesBody).Filter;
  }

  public object GetPropDescriptorEditor(int attributeId) => (object) new TemplatesTableRefsEditor();

  public Type GetPropDescriptorType(int attributeId, FieldTypes baseType) => typeof (TemplatesBody);

  public object GetPropDescriptorValue(
    IElementInfo iElementInfo,
    int attributeId,
    object actualValue)
  {
    long elementIdentifier = iElementInfo.ElementIdentifier;
    switch (elementIdentifier)
    {
      case -1:
      case 0:
        return (object) null;
      default:
        TemplatesBody propDescriptorValue = new TemplatesBody(string.Empty, UseTemplate.Ref);
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject dbObject1 = sessionKeeper.Session.GetObject(elementIdentifier);
          if (dbObject1 == null)
            return (object) null;
          IDBAttribute attributeById1 = dbObject1.GetAttributeByID(Intermech.Imbase.Consts.ImbaseTemplateAttID);
          if (attributeById1 != null)
            propDescriptorValue.Filter = attributeById1.AsString;
          IDBAttribute attributeById2 = dbObject1.GetAttributeByID(Intermech.Imbase.Consts.ImbaseTableRefAttID);
          if (attributeById2 == null)
            return (object) null;
          long asInteger1 = attributeById2.AsInteger;
          switch (asInteger1)
          {
            case -1:
            case 0:
              return (object) null;
            default:
              IDBObject dbObject2 = sessionKeeper.Session.GetObject(asInteger1);
              if (dbObject2 == null)
                return (object) null;
              IDBAttribute attributeById3 = dbObject2.GetAttributeByID(Intermech.Imbase.Consts.ImbaseTemplateRefAttID);
              if (attributeById3 == null)
                return (object) null;
              long asInteger2 = attributeById3.AsInteger;
              switch (asInteger2)
              {
                case -1:
                case 0:
                  return (object) null;
                default:
                  IDBObject dbObject3 = sessionKeeper.Session.GetObject(asInteger2);
                  if (dbObject3 == null)
                    return (object) null;
                  IDBAttribute attributeById4 = dbObject3.GetAttributeByID(Intermech.Imbase.Consts.ImbaseTemplateDataAttID);
                  if (attributeById4 == null || string.IsNullOrEmpty(attributeById4.AsString))
                    return (object) null;
                  propDescriptorValue.Body = attributeById4.AsString;
                  break;
              }
              break;
          }
        }
        return (object) propDescriptorValue;
    }
  }

  public bool GetPropDescriptorReset(int attributeId, bool baseReset) => true;

  public TypeConverter GetConverter(int attributeId, object attributeProcessor)
  {
    return (TypeConverter) null;
  }
}
