
// Type: Intermech.Interfaces.WebPortal.SiteInfo
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.WebPortal
{
    /// <summary>Описывает узел информационной системы</summary>
    [Serializable]
    public class SiteInfo : IComparable, IComparable<SiteInfo>
    {
      /// <summary>Идентификатор узла для которого создана данная сессия</summary>
      public long ID;
      /// <summary>
      /// Глобальный дентификатор узла для которого создана данная сессия
      /// </summary>
      public Guid GUID;
      /// <summary>Код узла для которого создана данная сессия</summary>
      public char Code;
      /// <summary>Наименование</summary>
      public string Caption;
      public SystemTypes SystemType;

      public SiteInfo()
      {
      }

      public SiteInfo(long id, Guid guid, char code, string caption, SystemTypes systemType)
      {
        this.ID = id;
        this.GUID = guid;
        this.Code = code;
        this.Caption = caption;
        this.SystemType = systemType;
      }

      public override string ToString() => this.Caption;

      public int CompareTo(object obj) => this.CompareTo(obj as SiteInfo);

      public int CompareTo(SiteInfo other) => other == null ? 1 : this.Code.CompareTo(other.Code);
    }
}
