// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Base.Hash
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;
using System;
using System.IO;
using System.Text;

#nullable disable
namespace Intermech.Hashes.Base;

internal abstract class Hash : IHash
{
  private int buffer_size;
  protected int block_size;
  protected int hash_size;
  protected static int BUFFER_SIZE = 65536 /*0x010000*/;
  private static string IndexOutOfRange = "Current Index Is Out Of Range";
  private static string InvalidBufferSize = "\"BufferSize\" Must Be Greater Than Zero";
  private static string UnAssignedStream = "Input Stream Is Unassigned";
  private static string FileNotExist = "Specified File Not Found";
  private static string CloneNotYetImplemented = "Clone Not Yet Implemented For \"{0}\"";

  public virtual string Name => this.GetType().Name;

  public virtual int BufferSize
  {
    get => this.buffer_size;
    set
    {
      this.buffer_size = value > 0 ? value : throw new ArgumentHashLibException(Hash.InvalidBufferSize);
    }
  }

  public virtual int BlockSize
  {
    get => this.block_size;
    set => this.block_size = value;
  }

  public virtual int HashSize
  {
    get => this.hash_size;
    set => this.hash_size = value;
  }

  public Hash(int a_hash_size, int a_block_size)
  {
    this.block_size = a_block_size;
    this.hash_size = a_hash_size;
    this.buffer_size = Hash.BUFFER_SIZE;
  }

  public abstract void Initialize();

  public virtual IHash Clone()
  {
    throw new NotImplementedHashLibException(string.Format(Hash.CloneNotYetImplemented, (object) this.Name));
  }

  public virtual IHashResult ComputeString(string a_data, Encoding encoding)
  {
    return this.ComputeBytes(Converters.ConvertStringToBytes(a_data, encoding));
  }

  public virtual IHashResult ComputeUntyped(IntPtr a_data, long a_length)
  {
    this.Initialize();
    this.TransformUntyped(a_data, a_length);
    return this.TransformFinal();
  }

  public virtual unsafe void TransformUntyped(IntPtr a_data, long a_length)
  {
    byte[] array = new byte[0];
    byte* src = (byte*) (void*) a_data;
    int num = (long) this.buffer_size > a_length ? Hash.BUFFER_SIZE : this.buffer_size;
    if ((IntPtr) src == IntPtr.Zero)
      return;
    Array.Resize<byte>(ref array, num);
    for (byte* numPtr = src + a_length; src < numPtr; src += num)
    {
      if (numPtr - src >= (long) num)
      {
        fixed (byte* dest = array)
          Intermech.Hashes.Utils.Utils.Memmove((IntPtr) (void*) dest, (IntPtr) (void*) src, num);
        this.TransformBytes(array);
      }
      else
      {
        Array.Resize<byte>(ref array, (int) (numPtr - src));
        fixed (byte* dest = array)
          Intermech.Hashes.Utils.Utils.Memmove((IntPtr) (void*) dest, (IntPtr) (void*) src, array.Length);
        this.TransformBytes(array);
        break;
      }
    }
  }

  public virtual IHashResult ComputeStream(Stream a_stream, long a_length = -1)
  {
    this.Initialize();
    this.TransformStream(a_stream, a_length);
    return this.TransformFinal();
  }

  public virtual IHashResult ComputeFile(string a_file_name, long a_from = 0, long a_length = -1)
  {
    this.Initialize();
    this.TransformFile(a_file_name, a_from, a_length);
    return this.TransformFinal();
  }

  public virtual IHashResult ComputeBytes(byte[] a_data)
  {
    this.Initialize();
    this.TransformBytes(a_data);
    return this.TransformFinal();
  }

  public virtual void TransformString(string a_data, Encoding encoding)
  {
    this.TransformBytes(Converters.ConvertStringToBytes(a_data, encoding));
  }

  public virtual void TransformBytes(byte[] a_data)
  {
    this.TransformBytes(a_data, 0, a_data != null ? a_data.Length : 0);
  }

  public virtual void TransformBytes(byte[] a_data, int a_index)
  {
    if (a_data.Empty())
      return;
    this.TransformBytes(a_data, a_index, a_data.Length - a_index);
  }

  public abstract void TransformBytes(byte[] a_data, int a_index, int a_length);

  public virtual void TransformStream(Stream a_stream, long a_length = -1)
  {
    long num1 = 0;
    ulong length1 = a_stream != null ? (ulong) a_stream.Length : 0UL;
    if (a_stream == null)
      throw new ArgumentNullHashLibException(Hash.UnAssignedStream);
    if (a_length > -1L && (ulong) (a_stream.Position + a_length) > length1)
      throw new IndexOutOfRangeHashLibException(Hash.IndexOutOfRange);
    if (a_stream.Position >= (long) (int) length1)
      return;
    int length2 = (int) length1 <= Hash.BUFFER_SIZE ? (a_length == -1L ? (int) length1 : (int) a_length) : (a_length != -1L ? (a_length > (long) Hash.BUFFER_SIZE ? Hash.BUFFER_SIZE : (int) a_length) : Hash.BUFFER_SIZE);
    byte[] array = new byte[length2];
    if (length2 == Hash.BUFFER_SIZE)
    {
      do
      {
        int num2 = a_stream.Read(array, 0, length2);
        if (num2 != Hash.BUFFER_SIZE)
        {
          Array.Resize<byte>(ref array, num2);
          this.TransformBytes(array, 0, num2);
          return;
        }
        if (num2 != 0)
        {
          num1 += (long) num2;
          this.TransformBytes(array, 0, num2);
        }
        else
          goto label_4;
      }
      while (a_length == -1L || a_length - num1 > (long) Hash.BUFFER_SIZE);
      goto label_11;
label_4:
      return;
label_11:
      ulong num3 = (ulong) (a_length - num1);
      Array.Resize<byte>(ref array, (int) num3);
      a_stream.Read(array, 0, (int) num3);
      this.TransformBytes(array, 0, (int) num3);
    }
    else
    {
      a_stream.Read(array, 0, length2);
      this.TransformBytes(array, 0, length2);
    }
  }

  public virtual void TransformFile(string a_file_name, long a_from = 0, long a_length = -1)
  {
    Stream a_stream = (Stream) File.OpenRead(a_file_name);
    if (!a_stream.CanRead)
      throw new ArgumentHashLibException(Hash.FileNotExist);
    a_stream.Position = 0L;
    this.TransformStream(a_stream, a_length);
    a_stream.Close();
  }

  public abstract IHashResult TransformFinal();
}
