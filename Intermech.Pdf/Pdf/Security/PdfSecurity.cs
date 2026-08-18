// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Security.PdfSecurity
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;


namespace Syncfusion.Pdf.Security
{
    public class PdfSecurity
    {
      private PdfEncryptor m_encryptor = new PdfEncryptor();
      private string m_ownerPassword = string.Empty;
      private string m_userPassword = string.Empty;

      public PdfPermissionsFlags ResetPermissions(PdfPermissionsFlags flags)
      {
        this.Permissions &= ~flags;
        return this.Permissions;
      }

      public PdfPermissionsFlags SetPermissions(PdfPermissionsFlags flags)
      {
        this.Permissions |= flags;
        return this.Permissions;
      }

      public PdfEncryptionAlgorithm Algorithm
      {
        get => this.m_encryptor.EncryptionAlgorithm;
        set => this.m_encryptor.EncryptionAlgorithm = value;
      }

      internal bool Enabled
      {
        get => this.m_encryptor.Encrypt;
        set => this.m_encryptor.Encrypt = value;
      }

      internal PdfEncryptor Encryptor
      {
        get => this.m_encryptor;
        set => this.m_encryptor = value;
      }

      public PdfEncryptionKeySize KeySize
      {
        get => this.m_encryptor.CryptographicAlgorithm;
        set => this.m_encryptor.CryptographicAlgorithm = value;
      }

      public string OwnerPassword
      {
        get => this.m_encryptor.OwnerPassword;
        set
        {
          if (PdfDocument.ConformanceLevel == PdfConformanceLevel.Pdf_A1B)
            throw new Exception("Document encryption is not allowed with PDF/A1-B Conformance documents.");
          this.m_encryptor.OwnerPassword = value;
        }
      }

      public PdfPermissionsFlags Permissions
      {
        get => this.m_encryptor.Permissions;
        set
        {
          if (this.m_encryptor.Permissions == value)
            return;
          this.m_encryptor.Permissions = value;
        }
      }

      public string UserPassword
      {
        get => this.m_encryptor.UserPassword;
        set
        {
          if (PdfDocument.ConformanceLevel == PdfConformanceLevel.Pdf_A1B)
            throw new Exception("Document encryption is not allowed with PDF/A1-B Conformance documents.");
          this.m_encryptor.UserPassword = value;
        }
      }
    }
}
