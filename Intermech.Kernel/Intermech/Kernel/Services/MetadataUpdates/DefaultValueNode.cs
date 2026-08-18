// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.MetadataUpdates.DefaultValueNode
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Globalization;
using System.Xml;


namespace Intermech.Kernel.Services.MetadataUpdates;

internal sealed class DefaultValueNode : XMLPropertyNode<object>
{
  private readonly FieldTypes _fieldType;

  public DefaultValueNode(IUserSession session, XmlNode node, FieldTypes fieldType)
    : base(session, node, "F_DEFAULT_VALUE", false)
  {
    this._fieldType = fieldType;
    this.ReadValue(session, node);
  }

  protected override object GetValue(IUserSession session, string nodeAttributeValue)
  {
    object obj = (object) null;
    if (nodeAttributeValue != string.Empty)
    {
      switch (this._fieldType)
      {
        case FieldTypes.ftDateTime:
          obj = nodeAttributeValue.Equals("NOW") || nodeAttributeValue.Equals(Consts.CurrentDateFunction) ? (object) Consts.CurrentDateFunction : (object) Convert.ToDateTime(nodeAttributeValue, (IFormatProvider) CultureInfo.InvariantCulture);
          break;
        case FieldTypes.ftObjectLink:
          if (GuidHelper.IsGuid(nodeAttributeValue))
          {
            QuickObjectInfo objectInfo = session.GetObjectInfo(new Guid(nodeAttributeValue));
            if (!objectInfo.Empty)
            {
              obj = (object) objectInfo.ObjectID;
              break;
            }
            break;
          }
          if (nodeAttributeValue.Equals("CURRENT"))
          {
            obj = (object) Consts.CurrentUserFunction;
            break;
          }
          break;
        case FieldTypes.ftMeasured:
          string[] strArray = nodeAttributeValue.Split(' ');
          if (strArray != null && strArray.Length == 2 && GuidHelper.IsGuid(strArray[1]))
          {
            double num = Convert.ToDouble(strArray[0], (IFormatProvider) CultureInfo.InvariantCulture);
            QuickObjectInfo objectInfo = session.GetObjectInfo(new Guid(strArray[1]));
            if (!objectInfo.Empty)
            {
              MeasureDescriptor descriptor = MeasureHelper.FindDescriptor(objectInfo.ObjectID);
              if (descriptor != null)
              {
                obj = (object) $"{Convert.ToString(num, (IFormatProvider) CultureInfo.InvariantCulture)} {descriptor.ShortName}";
                break;
              }
              break;
            }
            break;
          }
          break;
        default:
          obj = (object) nodeAttributeValue;
          break;
      }
    }
    return obj;
  }
}
