
// Type: Intermech.Interfaces.DocumentTypeSettings
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;
using System.IO;


namespace Intermech.Interfaces
{
    [Serializable]
    public struct DocumentTypeSettings(
      string documentFileExt,
      string additionalDocumentFileExts,
      string documentTypeName,
      string documentTypeCode,
      string outputObjectTypes,
      bool documentNameInStamp,
      bool documentTypeCodeInDesignation)
    {
      /// <summary>Расширение имени файла для типа документа (.txt)</summary>
      public string DocumentFileExt = documentFileExt;
      /// <summary>
      /// Список дополнительных расширений файлов для типа документа (через , или ; - .doc,.exe;.log )
      /// Разворачивается в List(string) через SplitAdditionalFileExts
      /// </summary>
      public string AdditionalDocumentFileExts = additionalDocumentFileExts;
      /// <summary>
      /// Список GUIDов типов объектов, выпускаемых по документам данного типа, через ,
      /// </summary>
      public string OutputObjectTypes = outputObjectTypes;
      /// <summary>наименование типа документов (возможно наследованное)</summary>
      public string DocumentTypeName = documentTypeName;
      /// <summary>код типа документов (возможно наследованное)</summary>
      public string DocumentTypeCode = documentTypeCode;
      /// <summary>отрисовывать наименование типа в штампе чертежа</summary>
      public bool DocumentNameInStamp = documentNameInStamp;
      /// <summary>код типа документа входит в обозначение</summary>
      public bool DocumentTypeCodeInDesignation = documentTypeCodeInDesignation;
      private static readonly char[] listSeparators = new char[2]
      {
        ',',
        ';'
      };

      public DocumentTypeSettings Clone()
      {
        return new DocumentTypeSettings(this.DocumentFileExt, this.AdditionalDocumentFileExts, this.DocumentTypeName, this.DocumentTypeCode, this.OutputObjectTypes, this.DocumentNameInStamp, this.DocumentTypeCodeInDesignation);
      }

      public static DocumentTypeSettings CreateDefault()
      {
        return new DocumentTypeSettings("", "", "", "", "", true, false);
      }

      /// <summary>проверяет валидность расширения</summary>
      /// <param name="s"></param>
      /// <returns></returns>
      public static bool IsValidDocumentFileExt(string s)
      {
        if (s == string.Empty)
          return true;
        if (s[0] != '.' || s.IndexOf('.', 1) != -1)
          return false;
        char[] invalidFileNameChars = Path.GetInvalidFileNameChars();
        for (int index = 1; index < invalidFileNameChars.Length; ++index)
        {
          if (s.IndexOf(invalidFileNameChars[index]) != -1)
            return false;
        }
        return true;
      }

      public static List<string> SplitOutputObjectTypes(string s)
      {
        return new List<string>((IEnumerable<string>) s.Split(','));
      }

      /// <summary>разбивает строку на расширения</summary>
      /// <param name="value">Исходная строка, содержащая расширения файлов разделенные запятой или точкой с запятой</param>
      /// <returns>Список, содержащий расширения файлов</returns>
      public static List<string> SplitAdditionalFileExts(string value)
      {
        if (string.IsNullOrEmpty(value))
          return new List<string>(0);
        List<string> stringList = new List<string>((IEnumerable<string>) value.Split(DocumentTypeSettings.listSeparators, StringSplitOptions.RemoveEmptyEntries));
        for (int index = 0; index < stringList.Count; ++index)
          stringList[index] = stringList[index].Trim();
        stringList.RemoveAll(new Predicate<string>(string.IsNullOrEmpty));
        return stringList;
      }
    }
}
