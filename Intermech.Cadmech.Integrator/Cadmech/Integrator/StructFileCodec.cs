// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.StructFileCodec
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using System;
using System.Data;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal class StructFileCodec
{
  private IServiceProvider owner;

  public StructFileCodec(IServiceProvider owner) => this.owner = owner;

  public StructFile Decode(
    CreateSpecJob job,
    DataTable structTable,
    FileContent fieldLayoutContent)
  {
    DecodeData decodeData = new DecodeData();
    decodeData.Job = job;
    decodeData.StructTable = structTable;
    decodeData.FieldLayoutFile = fieldLayoutContent;
    IDecodeAction[] decodeActionArray = new IDecodeAction[4]
    {
      (IDecodeAction) new ExtendStructTableAction(),
      (IDecodeAction) new UnpackOverridedFieldsAction(),
      (IDecodeAction) new ReadTableAction(this.owner),
      (IDecodeAction) new DecodeIndsAction()
    };
    foreach (IDecodeAction decodeAction in decodeActionArray)
      decodeAction.Run(decodeData);
    return decodeData.StructFile;
  }

  public DataTable Encode(BaseSpecJob job, StructFile structFile)
  {
    EncodeData encodeData = new EncodeData();
    encodeData.Job = job;
    encodeData.StructFile = structFile;
    IEncodeAction[] encodeActionArray = new IEncodeAction[2]
    {
      (IEncodeAction) new WriteTableAction(),
      (IEncodeAction) new PackOverridedFieldsAction()
    };
    foreach (IEncodeAction encodeAction in encodeActionArray)
      encodeAction.Run(encodeData);
    return encodeData.StructTable;
  }
}
