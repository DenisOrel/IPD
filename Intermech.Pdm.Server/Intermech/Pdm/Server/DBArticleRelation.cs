// Decompiled with JetBrains decompiler
// Type: Intermech.Pdm.Server.DBArticleRelation
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using Intermech.Interfaces;
using Intermech.Kernel;
using System.Data;

#nullable disable
namespace Intermech.Pdm.Server;

public class DBArticleRelation(UserSession uSession, DataTable relationsTable) : DBRelation(uSession, relationsTable)
{
  public override bool IsCheckParentReadOnly
  {
    get
    {
      IDBAttribute attributeById = this.GetAttributeByID(ServerPDMPlugin._contextCompositionAttrID);
      return (attributeById == null || attributeById.AsInteger == 1L || attributeById.AsInteger == 0L) && base.IsCheckParentReadOnly;
    }
  }
}
