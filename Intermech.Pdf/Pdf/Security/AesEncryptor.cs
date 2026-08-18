// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Security.AesEncryptor
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;


namespace Syncfusion.Pdf.Security
{
    internal class AesEncryptor
    {
      private int c_blockSize = 16 /*0x10*/;
      private Aes m_aes;
      private byte[] m_buf = new byte[16 /*0x10*/];
      private byte[] m_cbcV = new byte[16 /*0x10*/];
      private bool m_isEncryption;
      private int m_ivOff;
      private byte[] m_nextBlockV = new byte[16 /*0x10*/];

      internal AesEncryptor(byte[] key, byte[] iv, bool isEncryption)
      {
        this.m_aes = key.Length != this.c_blockSize ? new Aes(Aes.KeySize.Bits256, key) : new Aes(Aes.KeySize.Bits128, key);
        Array.Copy((Array) iv, 0, (Array) this.m_buf, 0, iv.Length);
        Array.Copy((Array) iv, 0, (Array) this.m_cbcV, 0, iv.Length);
        if (isEncryption)
          this.m_ivOff = this.m_buf.Length;
        this.m_isEncryption = isEncryption;
      }

      private static int AddPadding(byte[] input, int inOff)
      {
        byte num = (byte) (input.Length - inOff);
        for (; inOff < input.Length; ++inOff)
          input[inOff] = num;
        return (int) num;
      }

      internal int CalculateOutputSize()
      {
        int ivOff = this.m_ivOff;
        int num = ivOff % this.m_buf.Length;
        if (num != 0)
          return ivOff - num + this.m_buf.Length;
        return this.m_isEncryption ? ivOff + this.m_buf.Length : ivOff;
      }

      private static int CheckPadding(byte[] input)
      {
        int num = (int) input[input.Length - 1] & (int) byte.MaxValue;
        for (int index = 1; index <= num; ++index)
        {
          if ((int) input[input.Length - index] != num)
            throw new ArgumentException("Error while decrypting padding block");
        }
        return num;
      }

      internal int Finalize(byte[] output)
      {
        int num = 0;
        int outOff = 0;
        if (this.m_isEncryption)
        {
          if (this.m_ivOff == this.c_blockSize)
          {
            num = this.ProcessBlock(this.m_buf, 0, output, outOff);
            this.m_ivOff = 0;
          }
          AesEncryptor.AddPadding(this.m_buf, this.m_ivOff);
          return num + this.ProcessBlock(this.m_buf, 0, output, outOff + num);
        }
        if (this.m_ivOff == this.c_blockSize)
        {
          num = this.ProcessBlock(this.m_buf, 0, output, 0);
          this.m_ivOff = 0;
        }
        return num - AesEncryptor.CheckPadding(output);
      }

      internal int GetBlockSize(int length)
      {
        int num1 = length + this.m_ivOff;
        int num2 = num1 % this.m_buf.Length;
        return num2 == 0 ? num1 - this.m_buf.Length : num1 - num2;
      }

      private int ProcessBlock(byte[] input, int inOff, byte[] outBytes, int outOff)
      {
        if (inOff + this.c_blockSize > input.Length)
          throw new ArgumentException("input buffer length is too short");
        if (this.m_isEncryption)
        {
          for (int index = 0; index < this.c_blockSize; ++index)
            this.m_cbcV[index] = (byte) ((uint) this.m_cbcV[index] ^ (uint) input[inOff + index]);
          int num = this.m_aes.Cipher(this.m_cbcV, outBytes, outOff);
          Array.Copy((Array) outBytes, outOff, (Array) this.m_cbcV, 0, this.m_cbcV.Length);
          return num;
        }
        Array.Copy((Array) input, inOff, (Array) this.m_nextBlockV, 0, this.c_blockSize);
        int num1 = this.m_aes.InvCipher(this.m_nextBlockV, outBytes, outOff);
        for (int index = 0; index < this.c_blockSize; ++index)
          outBytes[outOff + index] = (byte) ((uint) outBytes[outOff + index] ^ (uint) this.m_cbcV[index]);
        byte[] cbcV = this.m_cbcV;
        this.m_cbcV = this.m_nextBlockV;
        this.m_nextBlockV = cbcV;
        return num1;
      }

      internal void ProcessBytes(byte[] input, int inOff, int length, byte[] output, int outOff)
      {
        if (length < 0)
          throw new ArgumentException("input data length cannot be negative");
        int num1 = 0;
        int length1 = this.m_buf.Length - this.m_ivOff;
        if (length > length1)
        {
          Array.Copy((Array) input, inOff, (Array) this.m_buf, this.m_ivOff, length1);
          int num2 = num1 + this.ProcessBlock(this.m_buf, 0, output, outOff);
          this.m_ivOff = 0;
          length -= length1;
          inOff += length1;
          while (length > this.m_buf.Length)
          {
            num2 += this.ProcessBlock(input, inOff, output, outOff + num2);
            length -= this.c_blockSize;
            inOff += this.c_blockSize;
          }
        }
        Array.Copy((Array) input, inOff, (Array) this.m_buf, this.m_ivOff, length);
        this.m_ivOff += length;
      }
    }
}
