// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Editors.ObjectAndAttLinkAttDescriber
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.PropertyEditors;
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Editors;

internal class ObjectAndAttLinkAttDescriber : IAttributePropertyDescriber
{
  public TypeConverter GetPropDescriptorConverter(int attributeId) => (TypeConverter) null;

  public bool GetPropDescriptorReadonly(int attributeId, bool baseReadonly) => baseReadonly;

  public string GetPropDescriptorMask(int attributeId, string baseMask) => baseMask;

  public object GetAttributeValue(IElementInfo iElementInfo, int attributeId, object propertyValue)
  {
    return propertyValue is ObjectAndAttLinkAttDescriber.TypeAndAttribiteValue andAttribiteValue ? (object) andAttribiteValue.Value : (object) null;
  }

  public object GetPropDescriptorEditor(int attributeId)
  {
    return (object) new ObjectAndAttLinkAttDescriber.TypeAndAttribiteEditor();
  }

  public System.Type GetPropDescriptorType(int attributeId, FieldTypes baseType)
  {
    return typeof (ObjectAndAttLinkAttDescriber.TypeAndAttribiteValue);
  }

  public object GetPropDescriptorValue(
    IElementInfo iElementInfo,
    int attributeId,
    object actualValue)
  {
    return (object) new ObjectAndAttLinkAttDescriber.TypeAndAttribiteValue(actualValue);
  }

  public bool GetPropDescriptorReset(int attributeId, bool baseReset) => true;

  public TypeConverter GetConverter(int attributeId, object attributeProcessor)
  {
    return (TypeConverter) null;
  }

  internal class TypeAndAttribiteValue
  {
    private Guid _objectTypeGuid;
    private Guid _attTypeGuid;
    private int _objectTypeId;
    private int _attTypeId;
    private string _text;
    private const char SeparatorChar = ';';

    public string Value
    {
      get
      {
        return Guid.Empty.Equals(this._objectTypeGuid) ? (string) null : ObjectAndAttLinkAttDescriber.TypeAndAttribiteValue.FormatGuids(this._objectTypeGuid, this._attTypeGuid);
      }
    }

    internal TypeAndAttribiteValue(int typeId, int attId)
    {
      this.Clean();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IUserSession session = sessionKeeper.Session;
        if (typeId != -1)
          this._objectTypeGuid = session.GetObjectType(typeId).PropertiesStructure.ObjectTypeGuid;
        this._attTypeGuid = session.GetAttributeType(attId).PropertiesStructure.AttributeGuid;
        this._objectTypeId = typeId;
        this._attTypeId = attId;
      }
    }

    public TypeAndAttribiteValue(object value)
    {
      this.Clean();
      if (value == null || Convert.IsDBNull(value) || Convert.ToString(value).Length == 0)
        return;
      string[] strArray = Convert.ToString(value).Split(';');
      if (strArray.Length != 2)
        return;
      this._objectTypeGuid = new Guid(strArray[0]);
      this._attTypeGuid = new Guid(strArray[1]);
    }

    private void Clean()
    {
      this._text = (string) null;
      this._attTypeGuid = Guid.Empty;
      this._objectTypeGuid = Guid.Empty;
      this._attTypeId = -1;
      this._objectTypeId = -1;
    }

    public override string ToString()
    {
      if (this._text == null)
      {
        if (Guid.Empty.Equals(this._attTypeGuid))
        {
          this._text = string.Empty;
        }
        else
        {
          IClientMetadataCache service = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
          string objectTypeName = LocalizationHolder.rm.GetString("Imbase.Client_44");
          if (!this._objectTypeGuid.Equals(Guid.Empty))
          {
            IDBObjectTypeInfo objectType = service.GetObjectType(this._objectTypeGuid, true);
            objectTypeName = objectType.ObjectTypeName;
            this._objectTypeId = objectType.ObjectType;
          }
          IDBAttributeTypeInfo attributeType = service.GetAttributeType(this._attTypeGuid, true);
          this._text = $"<{objectTypeName}>.{attributeType.Name}";
          this._attTypeId = attributeType.AttributeID;
        }
      }
      return this._text;
    }

    internal static string FormatGuids(Guid guid, Guid guid2)
    {
      return $"{guid.ToString()}{(ValueType) ';'}{guid2.ToString()}";
    }

    internal int ObjectTypeId => this._objectTypeId;

    internal int AttTypeId => this._attTypeId;
  }

  internal class TypeAndAttribiteEditor : UITypeEditor
  {
    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
    {
      return UITypeEditorEditStyle.Modal;
    }

    public override object EditValue(
      ITypeDescriptorContext context,
      System.IServiceProvider sp,
      object value)
    {
      AdvSelectorForm advSelectorForm;
      if (value != null)
      {
        ObjectAndAttLinkAttDescriber.TypeAndAttribiteValue andAttribiteValue = value as ObjectAndAttLinkAttDescriber.TypeAndAttribiteValue;
        advSelectorForm = new AdvSelectorForm(AttributableElements.Object, -1, andAttribiteValue.ObjectTypeId, new int[1]
        {
          andAttribiteValue.AttTypeId
        });
      }
      else
        advSelectorForm = new AdvSelectorForm(AdvSelector.AttributableTypeWithAttributeType, AttributableElements.Object);
      return advSelectorForm.ShowDialog() == DialogResult.OK ? (object) new ObjectAndAttLinkAttDescriber.TypeAndAttribiteValue(advSelectorForm.ObjectType, advSelectorForm.AttributeTypes[0]) : value;
    }
  }
}
