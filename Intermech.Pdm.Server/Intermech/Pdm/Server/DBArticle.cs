// Decompiled with JetBrains decompiler
// Type: Intermech.Pdm.Server.DBArticle
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Pdm;
using Intermech.Kernel;
using System.Data;

#nullable disable
namespace Intermech.Pdm.Server;

internal sealed class DBArticle(UserSession uSession, DataTable objectParams) : DBObject(uSession, objectParams), IDBArticle
{
  protected override void DoBeforeSetAdditionalAttributeValue(
    IDBAttribute attribute,
    object newValue)
  {
    if (ServerPDMPlugin.IsOrderPointMode && (attribute.AttributeID == ServerPDMPlugin.MaterialAttrID || attribute.AttributeID == ServerPDMPlugin.QualityControlAttrID))
    {
      IDBAttribute attributeById = this.GetAttributeByID(ServerPDMPlugin.OrderExistsAttrID);
      if (attributeById != null && attributeById.AsBoolean)
        throw new KernelException(this.NameInMessages + " выдана по заказу. Для изменения значений атрибутов 'Материал' и 'Контроль качества' обратитесь к администратору.");
    }
    base.DoBeforeSetAdditionalAttributeValue(attribute, newValue);
  }

  public bool KeepRelationWithSpecification { get; set; }
}
