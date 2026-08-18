
// Type: Intermech.Interfaces.SignConsts
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    public class SignConsts
    {
      /// <summary>Тип объекта "Подпись с криптозащитой"</summary>
      public static readonly Guid CryptoSignObjectTypeGuid = new Guid("cad00138-306c-11d8-b4e9-00304f19f545");
      /// <summary>Атрибут "Версия подписи"</summary>
      public static readonly Guid SignVersionAttrTypeGuid = new Guid("cad00145-306c-11d8-b4e9-00304f19f545");
      private static int signVersionAttrTypeID = -1;
      /// <summary>Атрибут "Должность"</summary>
      public static readonly Guid RankAttrTypeGuid = new Guid("cad00142-306c-11d8-b4e9-00304f19f545");
      /// <summary>Атрибут "Подписал"</summary>
      public static readonly Guid SignUpAttrTypeGuid = new Guid("cad00143-306c-11d8-b4e9-00304f19f545");
      /// <summary>Атрибут Дата подписания</summary>
      public static readonly Guid SignDateAttrTypeGuid = new Guid("cad014cb-306c-11d8-b4e9-00304f19f545");
      /// <summary>Атрибут "Электронная цифровая подпись"</summary>
      public static readonly Guid EDSAttrTypeGuid = new Guid("cad00146-306c-11d8-b4e9-00304f19f545");
      /// <summary>Атрибут "ЭЦП (данные для хэша)"</summary>
      public static readonly Guid SignDataSequenceAttrTypeGuid = new Guid("cadd968c-306c-11d8-b4e9-00304f19f545");
      /// <summary>Атрибут "Графа для подписи"</summary>
      public static readonly Guid GraphAttrTypeGuid = new Guid("cad00141-306c-11d8-b4e9-00304f19f545");
      public static readonly string SignFolderPostfix = ".SIGNS";
      public static readonly string SignFileExtension = ".sig";
      public static readonly string HashOrderFileExtension = ".lst";
      public const int ConvertSignGraphsMetadataDone = 1;
      public const int ConvertSignGraphsAllDone = 2;

      public static int SignVersionAttrTypeID
      {
        get
        {
          if (SignConsts.signVersionAttrTypeID == -1)
            SignConsts.signVersionAttrTypeID = MetaDataHelper.GetAttributeTypeID(SignConsts.SignVersionAttrTypeGuid);
          return SignConsts.signVersionAttrTypeID;
        }
      }

      /// <summary>Для разовой инициализации.</summary>
      /// <param name="session"></param>
      public static void Init(IUserSession session)
      {
        SignConsts.signVersionAttrTypeID = session.GetAttributeType(SignConsts.SignVersionAttrTypeGuid).AttributeID;
      }
    }
}
