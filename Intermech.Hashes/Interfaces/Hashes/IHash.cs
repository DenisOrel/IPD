// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Hashes.IHash
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using System;
using System.IO;
using System.Text;

#nullable disable
namespace Intermech.Interfaces.Hashes;

public interface IHash
{
  string Name { get; }

  int BlockSize { get; }

  int HashSize { get; }

  int BufferSize { get; set; }

  IHash Clone();

  IHashResult ComputeString(string a_data, Encoding encoding);

  IHashResult ComputeBytes(byte[] a_data);

  IHashResult ComputeUntyped(IntPtr a_data, long a_length);

  IHashResult ComputeStream(Stream a_stream, long a_length = -1);

  IHashResult ComputeFile(string a_file_name, long a_from = 0, long a_length = -1);

  void Initialize();

  void TransformBytes(byte[] a_data, int a_index, int a_length);

  void TransformBytes(byte[] a_data, int a_index);

  void TransformBytes(byte[] a_data);

  void TransformUntyped(IntPtr a_data, long a_length);

  IHashResult TransformFinal();

  void TransformString(string a_data, Encoding encoding);

  void TransformStream(Stream a_stream, long a_length = -1);

  void TransformFile(string a_file_name, long a_from = 0, long a_length = -1);
}
