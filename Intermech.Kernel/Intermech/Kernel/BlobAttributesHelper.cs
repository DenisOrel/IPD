// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.BlobAttributesHelper
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;


namespace Intermech.Kernel;

public class BlobAttributesHelper
{
  public static object[] GetBlobValues(IDBAttribute blobAttribute, GetBlobValueModes mode)
  {
    object[] blobValues = new object[blobAttribute.ValuesCount];
    for (int index = 0; index < blobAttribute.ValuesCount; ++index)
    {
      blobAttribute.Index = index;
      blobValues[index] = BlobAttributesHelper.GetBlobValue(blobAttribute, mode);
    }
    return blobValues;
  }

  public static object GetBlobValue(IDBAttribute blobAttribute, GetBlobValueModes mode)
  {
    IBlobReader blobReader = blobAttribute as IBlobReader;
    switch (mode)
    {
      case GetBlobValueModes.InfoOnly:
        return (object) blobReader.OpenBlob(-1);
      case GetBlobValueModes.BlobValue:
        return (object) new BlobValue(blobReader.OpenBlob(0), blobReader.ReadDataBlock());
      case GetBlobValueModes.UnpackedData:
        throw new NotImplementedException();
      default:
        throw new KernelException("Unknown GetBlobValueMode");
    }
  }

  public static void SetBlobValues(IDBAttribute blobAttribute, object[] vals, UserSession session)
  {
    IBlobWriter blobWriter = blobAttribute as IBlobWriter;
    bool flag = true;
    session.StartTransaction();
    try
    {
      for (int index = 0; index < vals.Length; ++index)
      {
        if (vals[index] is BlobValue val)
        {
          if (val.Index == -1)
          {
            if (index == blobAttribute.ValuesCount)
              blobAttribute.AddValue((object) null);
            else
              blobAttribute.Index = index;
          }
          else
          {
            blobAttribute.Index = val.Index;
            flag = false;
          }
          if (blobWriter.OpenBlob(val.Header, false))
            blobWriter.WriteDataBlock(val.Data);
        }
      }
      if (flag)
      {
        while (blobAttribute.ValuesCount > vals.Length)
        {
          blobAttribute.Index = blobAttribute.ValuesCount - 1;
          blobAttribute.DeleteValue();
        }
      }
      session.Commit();
    }
    catch
    {
      session.Rollback();
      throw;
    }
  }
}
