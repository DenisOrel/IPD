// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.NullDigest
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;
using System.IO;

#nullable disable
namespace Intermech.Hashes;

internal sealed class NullDigest : Hash, ITransformBlock
{
  private MemoryStream Out;
  private static readonly string HashSizeNotImplemented = "HashSize Not Implemented For \"{0}\"";
  private static readonly string BlockSizeNotImplemented = "BlockSize Not Implemented For \"{0}\"";

  public NullDigest()
    : base(-1, -1)
  {
    this.Out = new MemoryStream();
  }

  ~NullDigest()
  {
    this.Out.Flush();
    this.Out.Close();
  }

  public override int BlockSize
  {
    get
    {
      throw new NotImplementedHashLibException(string.Format(NullDigest.BlockSizeNotImplemented, (object) this.Name));
    }
  }

  public override int HashSize
  {
    get
    {
      throw new NotImplementedHashLibException(string.Format(NullDigest.HashSizeNotImplemented, (object) this.Name));
    }
  }

  public override IHash Clone()
  {
    NullDigest nullDigest = new NullDigest();
    byte[] array = this.Out.ToArray();
    nullDigest.Out.Write(array, 0, array.Length);
    nullDigest.Out.Position = this.Out.Position;
    nullDigest.BufferSize = this.BufferSize;
    return (IHash) nullDigest;
  }

  public override void Initialize()
  {
    this.Out.Flush();
    this.Out.SetLength(0L);
  }

  public override IHashResult TransformFinal()
  {
    int length = (int) this.Out.Length;
    byte[] numArray = new byte[length];
    try
    {
      this.Out.Position = 0L;
      if (numArray.Length != 0)
        this.Out.Read(numArray, 0, length);
    }
    finally
    {
      this.Initialize();
    }
    return (IHashResult) new HashResult(numArray);
  }

  public override void TransformBytes(byte[] a_data, int a_index, int a_length)
  {
    if (a_data.Empty())
      return;
    this.Out.Write(a_data, a_index, a_length);
  }
}
