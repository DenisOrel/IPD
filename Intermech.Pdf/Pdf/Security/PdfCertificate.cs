// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Security.PdfCertificate
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Native;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;


namespace Syncfusion.Pdf.Security
{
    public class PdfCertificate
    {
      private const uint CERT_STORE_OPEN_EXISTING_FLAG = 16384 /*0x4000*/;
      private const uint CERT_STORE_READONLY_FLAG = 32768 /*0x8000*/;
      private const uint CERT_SYSTEM_STORE_CURRENT_USER = 65536 /*0x010000*/;
      private const uint CERT_SYSTEM_STORE_LOCAL_MACHINE = 131072 /*0x020000*/;
      private const uint CRYPT_USER_KEYSET = 4096 /*0x1000*/;
      private const uint ENCODING_TYPE = 65537 /*0x010001*/;
      private IntPtr m_certificate;
      private string m_issuerName;
      private byte[] m_serialNumber;
      private uint m_signatureLength;
      private CRYPT_SIGN_MESSAGE_PARA m_signParams;
      private string m_subjectName;
      private DateTime m_validFrom;
      private DateTime m_validTo;
      private int m_version;
      private X509Certificate2 m_x509Certificate;
      private const uint openflags = 114688 /*0x01C000*/;
      private const uint PKCS_7_ASN_ENCODING = 65536 /*0x010000*/;
      private const string STORE_PROVIDER = "System";
      private const string STORE_TYPE = "MY";
      private const string szOID_COMMON_NAME = "2.5.4.3";
      private const uint X509_ASN_ENCODING = 1;
      private const int X509_NAME = 7;

      internal PdfCertificate(IntPtr certificate)
      {
        if (certificate == IntPtr.Zero)
          throw new ArgumentNullException(nameof (certificate));
        this.Initialize(certificate);
      }

      public PdfCertificate(string pfxPath, string password)
      {
        if (pfxPath == null)
          throw new ArgumentNullException(nameof (pfxPath));
        if (password == null)
          throw new ArgumentNullException(nameof (password));
        this.Initialize(pfxPath, password);
        this.m_x509Certificate = new X509Certificate2(pfxPath, password);
      }

      private DateTime ConvertTime(Syncfusion.Pdf.Native.FILETIME filetime)
      {
        SYSTEMTIME lpSystemTime = new SYSTEMTIME();
        IntPtr num = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof (IntPtr)));
        Marshal.StructureToPtr<Syncfusion.Pdf.Native.FILETIME>(filetime, num, true);
        KernelApi.FileTimeToSystemTime(num, ref lpSystemTime);
        return new DateTime((int) lpSystemTime.wYear, (int) lpSystemTime.wMonth, (int) lpSystemTime.wDay, (int) lpSystemTime.wHour, (int) lpSystemTime.wMinute, (int) lpSystemTime.wSecond, (int) lpSystemTime.wMilliseconds);
      }

      private static string CryptDecodeObjectEx(CRYPTOAPI_BLOB blob)
      {
        IntPtr zero = IntPtr.Zero;
        int cbStructInfo = 0;
        CryptoApi.CryptDecodeObject(65537U /*0x010001*/, 7U, blob.pbData, blob.cbData, 0U, zero, ref cbStructInfo);
        IntPtr num = Marshal.AllocHGlobal(cbStructInfo);
        CryptoApi.CryptDecodeObject(65537U /*0x010001*/, 7U, blob.pbData, blob.cbData, 0U, num, ref cbStructInfo);
        CERT_NAME_INFO structure1 = (CERT_NAME_INFO) Marshal.PtrToStructure(num, typeof (CERT_NAME_INFO));
        string str1 = string.Empty;
        Marshal.FreeHGlobal(num);
        IntPtr ptr = structure1.rgRDN;
        CERT_RDN_ATTR certRdnAttr = new CERT_RDN_ATTR();
        for (int index = 0; "2.5.4.3" != str1 && structure1.cRDN != index; ++index)
        {
          CERT_RDN structure2 = (CERT_RDN) Marshal.PtrToStructure(ptr, typeof (CERT_RDN));
          certRdnAttr = (CERT_RDN_ATTR) Marshal.PtrToStructure(structure2.rgRDNAttr, typeof (CERT_RDN_ATTR));
          str1 = certRdnAttr.pszObjId;
          ptr = new IntPtr(ptr.ToInt32() + Marshal.SizeOf<CERT_RDN>(structure2));
        }
        byte[] numArray = new byte[certRdnAttr.Value.cbData];
        if (numArray.Length == 0)
          return (string) null;
        string str2 = (string) null;
        Marshal.Copy(certRdnAttr.Value.pbData, numArray, 0, numArray.Length);
        if (certRdnAttr.dwValueType == 4 || certRdnAttr.dwValueType == 5)
          return new string(Encoding.UTF8.GetChars(numArray));
        return certRdnAttr.dwValueType != 12 && certRdnAttr.dwValueType != 13 ? str2 : Encoding.Unicode.GetString(numArray, 0, numArray.Length);
      }

      private static bool Equals(byte[] arr1, byte[] arr2)
      {
        if (arr1 == null)
          throw new ArgumentNullException(nameof (arr1));
        if (arr2 == null)
          throw new ArgumentNullException(nameof (arr2));
        bool flag = arr1.Length == arr2.Length;
        if (flag)
        {
          for (int index = 0; index < arr1.Length; ++index)
          {
            if ((int) arr1[index] != (int) arr2[index])
              return false;
          }
        }
        return flag;
      }

      public static PdfCertificate FindByIssuer(StoreType type, string issuer)
      {
        if (issuer == null)
          throw new ArgumentNullException(nameof (issuer));
        IntPtr storeProvider = CryptoApi.CertOpenSystemStore(IntPtr.Zero, type.ToString());
        for (IntPtr index = CryptoApi.CertEnumCertificatesInStore(storeProvider, IntPtr.Zero); index != IntPtr.Zero; index = CryptoApi.CertEnumCertificatesInStore(storeProvider, index))
        {
          IntPtr num = CryptoApi.CertDuplicateCertificateContext(index);
          string str = PdfCertificate.CryptDecodeObjectEx(PdfCertificate.GetCertificateInfo(num).Issuer);
          if (issuer == str)
          {
            if (storeProvider != IntPtr.Zero)
              CryptoApi.CertCloseStore(storeProvider, 0);
            return new PdfCertificate(num);
          }
        }
        if (storeProvider != IntPtr.Zero)
          CryptoApi.CertCloseStore(storeProvider, 0);
        return (PdfCertificate) null;
      }

      public static PdfCertificate FindBySerialId(StoreType type, byte[] certId)
      {
        if (certId == null)
          throw new ArgumentNullException(nameof (certId));
        IntPtr storeProvider = CryptoApi.CertOpenSystemStore(IntPtr.Zero, type.ToString());
        for (IntPtr index = CryptoApi.CertEnumCertificatesInStore(storeProvider, IntPtr.Zero); index != IntPtr.Zero; index = CryptoApi.CertEnumCertificatesInStore(storeProvider, index))
        {
          IntPtr num = CryptoApi.CertDuplicateCertificateContext(index);
          CERT_INFO certificateInfo = PdfCertificate.GetCertificateInfo(num);
          byte[] numArray = new byte[certificateInfo.SerialNumber.cbData];
          Marshal.Copy(certificateInfo.SerialNumber.pbData, numArray, 0, numArray.Length);
          if (PdfCertificate.Equals(numArray, certId))
          {
            if (storeProvider != IntPtr.Zero)
              CryptoApi.CertCloseStore(storeProvider, 0);
            return new PdfCertificate(num);
          }
        }
        if (storeProvider != IntPtr.Zero)
          CryptoApi.CertCloseStore(storeProvider, 0);
        return (PdfCertificate) null;
      }

      public static PdfCertificate FindBySubject(StoreType type, string subject)
      {
        if (subject == null)
          throw new ArgumentNullException(nameof (subject));
        IntPtr storeProvider = CryptoApi.CertOpenSystemStore(IntPtr.Zero, type.ToString());
        for (IntPtr index = CryptoApi.CertEnumCertificatesInStore(storeProvider, IntPtr.Zero); index != IntPtr.Zero; index = CryptoApi.CertEnumCertificatesInStore(storeProvider, index))
        {
          IntPtr num = CryptoApi.CertDuplicateCertificateContext(index);
          string str = PdfCertificate.CryptDecodeObjectEx(PdfCertificate.GetCertificateInfo(num).Subject);
          if (subject == str)
          {
            if (storeProvider != IntPtr.Zero)
              CryptoApi.CertCloseStore(storeProvider, 0);
            return new PdfCertificate(num);
          }
          CryptoApi.CertFreeCertificateContext(num);
        }
        if (storeProvider != IntPtr.Zero)
          CryptoApi.CertCloseStore(storeProvider, 0);
        return (PdfCertificate) null;
      }

      private static CERT_INFO GetCertificateInfo(IntPtr hCertCtx)
      {
        return (CERT_INFO) Marshal.PtrToStructure(((CERT_CONTEXT) Marshal.PtrToStructure(hCertCtx, typeof (CERT_CONTEXT))).pCertInfo, typeof (CERT_INFO));
      }

      public static PdfCertificate[] GetCertificates()
      {
        List<PdfCertificate> certList = new List<PdfCertificate>();
        PdfCertificate.GetCertificatesByType(StoreType.CA, certList);
        PdfCertificate.GetCertificatesByType(StoreType.MY, certList);
        PdfCertificate.GetCertificatesByType(StoreType.ROOT, certList);
        PdfCertificate.GetCertificatesByType(StoreType.SPC, certList);
        int count = certList.Count;
        if (count == 0)
          return (PdfCertificate[]) null;
        PdfCertificate[] certificates = new PdfCertificate[count];
        for (int index = 0; index < count; ++index)
          certificates[index] = certList[index];
        return certificates;
      }

      private static void GetCertificatesByType(StoreType type, List<PdfCertificate> certList)
      {
        IntPtr storeProvider = CryptoApi.CertOpenSystemStore(IntPtr.Zero, type.ToString());
        for (IntPtr index = CryptoApi.CertEnumCertificatesInStore(storeProvider, IntPtr.Zero); index != IntPtr.Zero; index = CryptoApi.CertEnumCertificatesInStore(storeProvider, index))
        {
          PdfCertificate pdfCertificate = new PdfCertificate(CryptoApi.CertDuplicateCertificateContext(index));
          certList.Add(pdfCertificate);
        }
        if (!(storeProvider != IntPtr.Zero))
          return;
        CryptoApi.CertCloseStore(storeProvider, 0);
      }

      private static byte[] GetFileBytes(string filename)
      {
        if (!File.Exists(filename))
          return (byte[]) null;
        using (FileStream fileStream = File.OpenRead(filename))
        {
          int length = (int) fileStream.Length;
          byte[] buffer = new byte[length];
          fileStream.Seek(0L, SeekOrigin.Begin);
          fileStream.Read(buffer, 0, length);
          return buffer;
        }
      }

      internal uint GetSignatureLength()
      {
        if (this.m_signatureLength == 0U)
        {
          byte[] bytes = Encoding.UTF8.GetBytes(Environment.CurrentDirectory);
          int[] rgcbToBeSigned = new int[1]{ bytes.Length };
          IntPtr destination1 = Marshal.AllocCoTaskMem(bytes.Length);
          Marshal.Copy(bytes, 0, destination1, bytes.Length);
          if (!CryptoApi.CryptSignMessage(ref this.m_signParams, true, 1U, new IntPtr[1]
          {
            destination1
          }, rgcbToBeSigned, IntPtr.Zero, ref this.m_signatureLength))
          {
            uint lastError = KernelApi.GetLastError();
            IntPtr num = Marshal.AllocHGlobal(4);
            uint length = KernelApi.FormatMessage(FormatMessageFlags.AllocateBuffer | FormatMessageFlags.FromSystem, IntPtr.Zero, lastError, 0U, num, 4U, IntPtr.Zero);
            byte[] destination2 = new byte[4];
            Marshal.Copy(num, destination2, 0, 4);
            int int32 = BitConverter.ToInt32(destination2, 0);
            Marshal.FreeHGlobal(num);
            num = new IntPtr(int32);
            byte[] numArray = new byte[(int) length];
            Marshal.Copy(num, numArray, 0, (int) length);
            Marshal.FreeHGlobal(num);
            throw new Exception(Encoding.UTF8.GetString(numArray));
          }
        }
        return this.m_signatureLength;
      }

      internal byte[] GetSignatureValue(byte[][] dataBlocks)
      {
        if (dataBlocks == null)
          throw new ArgumentNullException(nameof (dataBlocks));
        uint signatureLength = this.GetSignatureLength();
        int length = dataBlocks.Length;
        byte[] destination1 = new byte[(int) signatureLength];
        IntPtr[] rgpbToBeSigned = new IntPtr[length];
        int[] rgcbToBeSigned = new int[length];
        for (int index = 0; index < length; ++index)
        {
          byte[] dataBlock = dataBlocks[index];
          IntPtr destination2 = Marshal.AllocCoTaskMem(dataBlock.Length);
          Marshal.Copy(dataBlock, 0, destination2, dataBlock.Length);
          rgpbToBeSigned[index] = destination2;
          rgcbToBeSigned[index] = dataBlock.Length;
        }
        IntPtr num = Marshal.AllocCoTaskMem((int) signatureLength);
        CryptoApi.CryptSignMessage(ref this.m_signParams, true, (uint) length, rgpbToBeSigned, rgcbToBeSigned, num, ref signatureLength);
        Marshal.Copy(num, destination1, 0, destination1.Length);
        return destination1;
      }

      private void Initialize(IntPtr certificate)
      {
        this.m_certificate = certificate;
        CERT_INFO structure = (CERT_INFO) Marshal.PtrToStructure(((CERT_CONTEXT) Marshal.PtrToStructure(certificate, typeof (CERT_CONTEXT))).pCertInfo, typeof (CERT_INFO));
        this.m_version = structure.dwVersion;
        this.m_serialNumber = new byte[structure.SerialNumber.cbData];
        Marshal.Copy(structure.SerialNumber.pbData, this.m_serialNumber, 0, this.m_serialNumber.Length);
        string pszObjId = structure.SignatureAlgorithm.pszObjId;
        this.m_issuerName = PdfCertificate.CryptDecodeObjectEx(structure.Issuer);
        this.m_subjectName = PdfCertificate.CryptDecodeObjectEx(structure.Subject);
        this.m_validFrom = this.ConvertTime(structure.NotBefore);
        this.m_validTo = this.ConvertTime(structure.NotAfter);
        this.m_signParams = new CRYPT_SIGN_MESSAGE_PARA();
        this.m_signParams.cbSize = (uint) Marshal.SizeOf(typeof (CRYPT_SIGN_MESSAGE_PARA));
        this.m_signParams.dwMsgEncodingType = 65537U /*0x010001*/;
        this.m_signParams.pSigningCert = this.m_certificate;
        this.m_signParams.HashAlgorithm.pszObjId = pszObjId;
        this.m_signParams.HashAlgorithm.Parameters.cbData = 0;
        this.m_signParams.pvHashAuxInfo = new IntPtr(0);
        this.m_signParams.cMsgCert = 1U;
        this.m_signParams.rgpMsgCert = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof (IntPtr)));
        Marshal.StructureToPtr<IntPtr>(this.m_certificate, this.m_signParams.rgpMsgCert, true);
        this.m_signParams.cMsgCrl = 0U;
        this.m_signParams.rgpMsgCrl = new IntPtr(0);
        this.m_signParams.cAuthAttr = 0U;
        this.m_signParams.rgAuthAttr = new IntPtr(0);
        this.m_signParams.cUnauthAttr = 0U;
        this.m_signParams.rgUnauthAttr = new IntPtr(0);
        this.m_signParams.dwFlags = 0U;
        this.m_signParams.dwInnerContentType = 0U;
      }

      private void Initialize(string pfxFileName, string password)
      {
        IntPtr zero = IntPtr.Zero;
        byte[] source = File.Exists(pfxFileName) ? PdfCertificate.GetFileBytes(pfxFileName) : throw new PdfException("File is not found");
        CRYPT_DATA_BLOB pPfx = new CRYPT_DATA_BLOB();
        pPfx.cbData = source.Length;
        pPfx.pbData = Marshal.AllocHGlobal(source.Length);
        Marshal.Copy(source, 0, pPfx.pbData, source.Length);
        IntPtr storeProvider = CryptoApi.PFXIsPFXBlob(ref pPfx) ? CryptoApi.PFXImportCertStore(ref pPfx, password, 4096U /*0x1000*/) : throw new ArgumentException("File has wrong format");
        if (storeProvider == IntPtr.Zero)
          throw new ArgumentException(new Win32Exception(Marshal.GetLastWin32Error()).Message);
        Marshal.FreeHGlobal(pPfx.pbData);
        for (IntPtr index = CryptoApi.CertEnumCertificatesInStore(storeProvider, IntPtr.Zero); index != IntPtr.Zero; index = CryptoApi.CertEnumCertificatesInStore(storeProvider, index))
          this.Initialize(CryptoApi.CertDuplicateCertificateContext(index));
        if (!(storeProvider != IntPtr.Zero))
          return;
        CryptoApi.CertCloseStore(storeProvider, 0);
      }

      public string IssuerName => this.m_issuerName;

      public byte[] SerialNumber => this.m_serialNumber;

      public string SubjectName => this.m_subjectName;

      internal IntPtr SysCertificate => this.m_certificate;

      public DateTime ValidFrom => this.m_validFrom;

      public DateTime ValidTo => this.m_validTo;

      public int Version => this.m_version;

      internal X509Certificate2 X509Certificate => this.m_x509Certificate;
    }
}
