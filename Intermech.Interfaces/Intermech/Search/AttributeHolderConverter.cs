
// Type: Intermech.Search.AttributeHolderConverter
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Search.ComponentModel;
using Intermech.Search.Data.Repositories;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;


namespace Intermech.Search
{
    public class AttributeHolderConverter : TypeConverter
    {
      public override PropertyDescriptorCollection GetProperties(
        ITypeDescriptorContext context,
        object value,
        Attribute[] attributes)
      {
        if (value == null)
          throw new ArgumentNullException(nameof (value));
        return !(value is IAttributeHolder) ? this.CreatePropertyDescriptorCollection((IAttributeHolder) value) : throw new ArgumentException();
      }

      public override bool GetPropertiesSupported(ITypeDescriptorContext context) => true;

      private PropertyDescriptorCollection CreatePropertyDescriptorCollection(
        IAttributeHolder attributeHolder)
      {
        List<System.ComponentModel.PropertyDescriptor> propertyDescriptorList = new List<System.ComponentModel.PropertyDescriptor>();
        foreach (_Attribute attribute in (IEnumerable<_Attribute>) attributeHolder.Attributes)
        {
          IMSAttributeType attributeType = ServiceLocator.Get<IAttributeTypeRepository>().Find(attribute.TypeID);
          AttributeHolderConverter.AttributeHolderPropertyDescriptor propertyDescriptor = new AttributeHolderConverter.AttributeHolderPropertyDescriptor(attributeHolder.GetType(), attributeType);
          propertyDescriptorList.Add((System.ComponentModel.PropertyDescriptor) propertyDescriptor);
        }
        return new PropertyDescriptorCollection(propertyDescriptorList.ToArray());
      }

      public class AttributeHolderPropertyDescriptor : System.ComponentModel.PropertyDescriptor
      {
        private Type _componentType;
        private string _category;
        private TypeConverter _converter;
        private string _description;
        private string _displayName;
        private bool _isReadOnly;
        private Type _propertyType;

        public AttributeHolderPropertyDescriptor(Type componentType, IMSAttributeType attributeType)
          : base(attributeType.Name, new Attribute[0])
        {
          if (attributeType == null)
            throw new ArgumentNullException(nameof (attributeType));
          this._componentType = componentType;
          this.AttributeType = attributeType;
          this._category = this.GetCategry(attributeType);
          this._converter = this.CreateConverter(attributeType);
          this._description = this.GetDescription(attributeType);
          this._displayName = this.GetDisplayName(attributeType);
          this._propertyType = this.GetPropertyType(attributeType);
        }

        public IMSAttributeType AttributeType { get; private set; }

        public override string Category => this._category;

        public override TypeConverter Converter => this._converter;

        public override string Description => this._description;

        public override string DisplayName => this._displayName;

        public override object GetValue(object component)
        {
          if (component == null)
            throw new ArgumentNullException();
          if (!(component is IAttributeHolder))
            throw new ArgumentException();
          _Attribute attribute = ((IAttributeHolder) component).Attributes.GetAttribute(this.AttributeType.AttributeID);
          if (attribute != null)
            this._isReadOnly = attribute.IsReadOnly.HasValue && attribute.IsReadOnly.Value;
          return ((IAttributeHolder) component).Attributes.GetAttributeValue(this.AttributeType.AttributeID);
        }

        public override bool IsReadOnly => this._isReadOnly;

        public override Type PropertyType => this._propertyType;

        public override void SetValue(object component, object value)
        {
          if (component == null)
            throw new ArgumentNullException();
          if (!(component is IAttributeHolder))
            throw new ArgumentException();
          ((IAttributeHolder) component).Attributes.SetAttributeValue(this.AttributeType.AttributeID, value);
        }

        private string GetCategry(IMSAttributeType attributeType)
        {
          if (AttributeTypeHelper.IsSystemAttributeTypeID(attributeType.AttributeID))
            return "Системные атрибуты";
          if (attributeType.FieldType == FieldTypes.ftBlob || attributeType.FieldType == FieldTypes.ftFile || attributeType.FieldType == FieldTypes.ftShortBlob)
            return "Файлы";
          using (SessionKeeper sessionKeeper = new SessionKeeper())
            return MetaDataHelper.GetAttributeGroup(((IEnumerable<int>) sessionKeeper.Session.GetAttributeType(attributeType.AttributeID).GetGroupsList()).FirstOrDefault<int>())?.Name;
        }

        private TypeConverter CreateConverter(IMSAttributeType attributeType)
        {
          Type converterType = this.GetConverterType(attributeType);
          return converterType == (Type) null ? (TypeConverter) null : Activator.CreateInstance(converterType) as TypeConverter;
        }

        private Type GetConverterType(IMSAttributeType attributeType)
        {
          if (attributeType.MultiValueMode == MultiValueModes.MultiValues || attributeType.MultiValueMode == MultiValueModes.MultiValuesFromList)
            return typeof (ListConverter);
          if (!AttributeTypeHelper.IsSystemAttributeTypeID(attributeType.AttributeID))
            return this.GetTypeConverter(attributeType.FieldType);
          ObligatoryObjectAttributes attributeId = (ObligatoryObjectAttributes) attributeType.AttributeID;
          switch (attributeId)
          {
            case ObligatoryObjectAttributes.F_CREATOR_ID:
              return typeof (ObjectLinkConverter);
            case ObligatoryObjectAttributes.F_ELEMENT_STATUSES:
              return typeof (StatusesConverter);
            case ObligatoryObjectAttributes.F_BASE_VERSION:
              return typeof (BaseVersionConverter);
            case ObligatoryObjectAttributes.F_PROJECT_ID:
              return typeof (ObjectLinkConverter);
            case ObligatoryObjectAttributes.F_LEVEL_ID:
              return typeof (LifecycleLevelLinkConverter);
            case ObligatoryObjectAttributes.F_OWNER_ID:
              return typeof (ObjectLinkConverter);
            case ObligatoryObjectAttributes.F_OBJECT_TYPE:
              return typeof (ObjectTypeLinkConverter);
            case ObligatoryObjectAttributes.F_CHKOUT_BY:
              return typeof (ObjectLinkConverter);
            case ObligatoryObjectAttributes.F_LC_STEP:
              return typeof (LifecycleStepLinkConverter);
            default:
              return this.GetTypeConverter(ObligatoryObjectAttributesHelper.GetDataType(attributeId));
          }
        }

        private Type GetTypeConverter(FieldTypes dataType)
        {
          switch (dataType)
          {
            case FieldTypes.ftShortBlob:
              return typeof (BlobInfoConverter);
            case FieldTypes.ftFile:
              return typeof (BlobInfoConverter);
            case FieldTypes.ftObjectLink:
              return typeof (ObjectLinkConverter);
            case FieldTypes.ftPassword:
              return typeof (PasswordConverter);
            case FieldTypes.ftBlob:
              return typeof (BlobInfoConverter);
            case FieldTypes.ftBoolean:
              return typeof (YesNoBooleanConverter);
            case FieldTypes.ftMeasured:
              return typeof (MeasuredValueConverter);
            default:
              return (Type) null;
          }
        }

        private string GetDescription(IMSAttributeType attributeType) => attributeType.Note;

        private string GetDisplayName(IMSAttributeType attributeType) => attributeType.Name;

        private Type GetPropertyType(IMSAttributeType attributeType)
        {
          switch (attributeType.RealFieldType)
          {
            case FieldTypes.ftString:
              return typeof (string);
            case FieldTypes.ftInteger:
              return typeof (long);
            case FieldTypes.ftDouble:
              return typeof (double);
            case FieldTypes.ftDateTime:
              return typeof (DateTime);
            case FieldTypes.ftShortBlob:
              return typeof (BlobInfo);
            case FieldTypes.ftFile:
              return typeof (BlobInfo);
            case FieldTypes.ftObjectLink:
              return typeof (long);
            case FieldTypes.ftPassword:
              return typeof (string);
            case FieldTypes.ftMemo:
              return typeof (string);
            case FieldTypes.ftBlob:
              return typeof (BlobInfo);
            case FieldTypes.ftBoolean:
              return typeof (bool);
            case FieldTypes.ftMeasured:
              return typeof (MeasuredValue);
            case FieldTypes.ftAutoInc:
              return typeof (long);
            case FieldTypes.ftGuid:
              return typeof (Guid);
            default:
              return typeof (object);
          }
        }

        public override bool CanResetValue(object component) => false;

        public override Type ComponentType => this._componentType;

        public override void ResetValue(object component) => throw new NotImplementedException();

        public override bool ShouldSerializeValue(object component) => false;
      }
    }
}
