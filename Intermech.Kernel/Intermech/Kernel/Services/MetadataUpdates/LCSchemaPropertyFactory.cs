// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.MetadataUpdates.LCSchemaPropertyFactory
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.LifeCycles;
using System.Xml;


namespace Intermech.Kernel.Services.MetadataUpdates;

internal sealed class LCSchemaPropertyFactory : OptionizedPropertyFactory<LCSchemaOptions>
{
  protected override IPropertyNode GetPropertyNode(
    IUserSession session,
    XmlNode node,
    string nodeID)
  {
    IPropertyNode propertyNode;
    switch (nodeID)
    {
      case "F_DRAW_DATA":
        propertyNode = (IPropertyNode) new SimpleFileNode(session, node, nodeID, this.Directory);
        break;
      case "F_SCHEMA_DATA":
        propertyNode = (IPropertyNode) new DataSetFileNode(session, node, nodeID, this.Directory);
        break;
      default:
        propertyNode = base.GetPropertyNode(session, node, nodeID);
        break;
    }
    return propertyNode;
  }

  public DBLCSchemaProperties Properties
  {
    get
    {
      return new DBLCSchemaProperties()
      {
        Options = this.GetOptions(LCSchemaOptions.None),
        Name = this.GetPropertyValue<string>("F_NAME", string.Empty),
        Note = this.GetPropertyValue<string>("F_NOTE", string.Empty),
        IsDefaultSchema = this.GetPropertyValue<bool>("F_DEFAULT", false),
        AreaID = this.GetPropertyValue<string>("F_AREA_ID", string.Empty)
      };
    }
  }
}
