
// Type: Intermech.Protection.KeyApplications
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Aladdin.HASP;
using System.Collections.Generic;


namespace Intermech.Protection
{
    internal class KeyApplications
    {
      protected static ApplicationEntry[] _apps = new ApplicationEntry[327]
      {
        new ApplicationEntry("Local key", 0),
        new ApplicationEntry("IPS Altium Designer Connector 3.0", 99),
        new ApplicationEntry("IPS Altium Designer Connector 4.0", 124),
        new ApplicationEntry("IPS Altium Designer Connector 5.0", 224 /*0xE0*/),
        new ApplicationEntry("IPS Altium Designer Connector v6", 273),
        new ApplicationEntry("IPS Altium Designer Connector v7", 334),
        new ApplicationEntry("IPS Application Server 2.0", 3),
        new ApplicationEntry("IPS Application Server 3.0", 100),
        new ApplicationEntry("IPS Application Server 4.0", 191),
        new ApplicationEntry("IPS Application Server 5.0", 225),
        new ApplicationEntry("IPS Application Server v6", 274),
        new ApplicationEntry("IPS Application Server v7", 335),
        new ApplicationEntry("IPS Archives 2.0", 12),
        new ApplicationEntry("IPS Archives 3.0", 101),
        new ApplicationEntry("IPS Archives 4.0", 192 /*0xC0*/),
        new ApplicationEntry("IPS Archives 5.0", 226),
        new ApplicationEntry("IPS Archives v6", 275),
        new ApplicationEntry("IPS Archives v7", 336),
        new ApplicationEntry("IPS Autocad Connector 2.0", 26),
        new ApplicationEntry("IPS Autocad Connector 3.0", 102),
        new ApplicationEntry("IPS Autocad Connector 4.0", 193),
        new ApplicationEntry("IPS Autocad Connector 5.0", 227),
        new ApplicationEntry("IPS Autocad Connector v6", 276),
        new ApplicationEntry("IPS AutoMatch 2.0", 19),
        new ApplicationEntry("IPS AutoMatch 3.0", 103),
        new ApplicationEntry("IPS AutoMatch 4.0", 221),
        new ApplicationEntry("IPS AutoMatch 5.0", 228),
        new ApplicationEntry("IPS AutoMatch v6", 277),
        new ApplicationEntry("IPS AutoMatch v7", 338),
        new ApplicationEntry("IPS AVS 2.0", 10),
        new ApplicationEntry("IPS AVS 3.0", 104),
        new ApplicationEntry("IPS AVS 4.0", 220),
        new ApplicationEntry("IPS AVS 5.0", 229),
        new ApplicationEntry("IPS AVS v6", 278),
        new ApplicationEntry("IPS AVS v7", 339),
        new ApplicationEntry("IPS DWG Connector v7", 337),
        new ApplicationEntry("IPS ECO 2.0", 16 /*0x10*/),
        new ApplicationEntry("IPS ECO 3.0", 105),
        new ApplicationEntry("IPS ECO 4.0", 222),
        new ApplicationEntry("IPS ECO 5.0", 230),
        new ApplicationEntry("IPS ECO v6", 279),
        new ApplicationEntry("IPS ECO v7", 340),
        new ApplicationEntry("IPS Expert 2.0", 9),
        new ApplicationEntry("IPS Expert 3.0", 106),
        new ApplicationEntry("IPS Expert 4.0", 194),
        new ApplicationEntry("IPS Expert 5.0", 231),
        new ApplicationEntry("IPS Expert v6", 280),
        new ApplicationEntry("IPS Expert v7", 342),
        new ApplicationEntry("IPS Expert Server 2.0", 29),
        new ApplicationEntry("IPS Expert Server 3.0", 107),
        new ApplicationEntry("IPS Expert Server 4.0", 195),
        new ApplicationEntry("IPS Expert Server 5.0", 232),
        new ApplicationEntry("IPS Expert Server v6", 281),
        new ApplicationEntry("IPS Expert Server v7", 341),
        new ApplicationEntry("IPS Imbase 2.0", 2),
        new ApplicationEntry("IPS Imbase 3.0", 108),
        new ApplicationEntry("IPS Imbase 4.0", 196),
        new ApplicationEntry("IPS Imbase 5.0", 233),
        new ApplicationEntry("IPS Imbase v6", 282),
        new ApplicationEntry("IPS Imbase v7", 343),
        new ApplicationEntry("IPS ImProject 2.0", 27),
        new ApplicationEntry("IPS ImProject 3.0", 109),
        new ApplicationEntry("IPS ImProject 4.0", 197),
        new ApplicationEntry("IPS ImProject 5.0", 234),
        new ApplicationEntry("IPS ImProject v6", 283),
        new ApplicationEntry("IPS ImProject v7", 344),
        new ApplicationEntry("IPS Inventor Connector 2.0", 21),
        new ApplicationEntry("IPS Inventor Connector 3.0", 110),
        new ApplicationEntry("IPS Inventor Connector 4.0", 198),
        new ApplicationEntry("IPS Inventor Connector 5.0", 235),
        new ApplicationEntry("IPS Inventor Connector v6", 284),
        new ApplicationEntry("IPS Inventor Connector v7", 345),
        new ApplicationEntry("IPS Mentor Graphics Connector 2.0", 40),
        new ApplicationEntry("IPS Mentor Graphics Connector 3.0", 111),
        new ApplicationEntry("IPS Mentor Graphics Connector 4.0", 199),
        new ApplicationEntry("IPS Mentor Graphics Connector 5.0", 236),
        new ApplicationEntry("IPS Mentor Graphics Connector v6", 285),
        new ApplicationEntry("IPS Mentor Graphics Connector v7", 346),
        new ApplicationEntry("IPS MRP 2.0", 28),
        new ApplicationEntry("IPS MRP 3.0", 112 /*0x70*/),
        new ApplicationEntry("IPS MRP 4.0", 200),
        new ApplicationEntry("IPS MRP 5.0", 237),
        new ApplicationEntry("IPS MRP v6", 286),
        new ApplicationEntry("IPS MRP v7", 347),
        new ApplicationEntry("IPS Navigator 2.0", 1),
        new ApplicationEntry("IPS Navigator 3.0", 113),
        new ApplicationEntry("IPS Navigator 4.0", 201),
        new ApplicationEntry("IPS Navigator 5.0", 238),
        new ApplicationEntry("IPS Navigator v6", 287),
        new ApplicationEntry("IPS Navigator v7", 348),
        new ApplicationEntry("IPS Office 2.0", 35),
        new ApplicationEntry("IPS Office 3.0", 114),
        new ApplicationEntry("IPS Office 4.0", 202),
        new ApplicationEntry("IPS Office 5.0", 239),
        new ApplicationEntry("IPS Office v6", 288),
        new ApplicationEntry("IPS Office v7", 349),
        new ApplicationEntry("IPS PDM 2.0", 15),
        new ApplicationEntry("IPS PDM 3.0", 115),
        new ApplicationEntry("IPS PDM 4.0", 203),
        new ApplicationEntry("IPS PDM 5.0", 240 /*0xF0*/),
        new ApplicationEntry("IPS PDM v6", 289),
        new ApplicationEntry("IPS PDM v7", 351),
        new ApplicationEntry("IPS PDM Server 2.0", 32 /*0x20*/),
        new ApplicationEntry("IPS PDM Server 3.0", 116),
        new ApplicationEntry("IPS PDM Server 4.0", 204),
        new ApplicationEntry("IPS PDM Server 5.0", 241),
        new ApplicationEntry("IPS PDM Server v6", 290),
        new ApplicationEntry("IPS PDM Server v7", 350),
        new ApplicationEntry("IPS Pro/E Connector 2.0", 24),
        new ApplicationEntry("IPS Pro/E Connector 3.0", 117),
        new ApplicationEntry("IPS Pro/E Connector 4.0", 205),
        new ApplicationEntry("IPS Pro/E Connector 5.0", 242),
        new ApplicationEntry("IPS Pro/E Connector v6", 291),
        new ApplicationEntry("IPS Pro/E Connector v7", 352),
        new ApplicationEntry("IPS Signs 2.0", 11),
        new ApplicationEntry("IPS Signs 3.0", 118),
        new ApplicationEntry("IPS Signs 4.0", 206),
        new ApplicationEntry("IPS Signs 5.0", 243),
        new ApplicationEntry("IPS Signs v6", 292),
        new ApplicationEntry("IPS Signs v7", 353),
        new ApplicationEntry("IPS SolidEdge Connector 2.0", 23),
        new ApplicationEntry("IPS SolidEdge Connector 3.0", 119),
        new ApplicationEntry("IPS SolidEdge Connector 4.0", 207),
        new ApplicationEntry("IPS SolidEdge Connector 5.0", 244),
        new ApplicationEntry("IPS SolidEdge Connector v6", 293),
        new ApplicationEntry("IPS SolidEdge Connector v7", 354),
        new ApplicationEntry("IPS SolidWorks Connector 2.0", 22),
        new ApplicationEntry("IPS SolidWorks Connector 3.0", 120),
        new ApplicationEntry("IPS SolidWorks Connector 4.0", 208 /*0xD0*/),
        new ApplicationEntry("IPS SolidWorks Connector 5.0", 245),
        new ApplicationEntry("IPS SolidWorks Connector v6", 294),
        new ApplicationEntry("IPS SolidWorks Connector v7", 355),
        new ApplicationEntry("IPS Statistics 5.0", 256 /*0x0100*/),
        new ApplicationEntry("IPS Statistics v6", 295),
        new ApplicationEntry("IPS Statistics v7", 356),
        new ApplicationEntry("IPS TechAcad 2.0", 37),
        new ApplicationEntry("IPS TechAcad 3.0", 121),
        new ApplicationEntry("IPS TechAcad 4.0", 209),
        new ApplicationEntry("IPS TechAcad 5.0", 246),
        new ApplicationEntry("IPS TechAcad v6", 296),
        new ApplicationEntry("IPS TechAcad v7", 357),
        new ApplicationEntry("IPS Techcard Server 2.0", 31 /*0x1F*/),
        new ApplicationEntry("IPS Techcard Server 3.0", 122),
        new ApplicationEntry("IPS Techcard Server 4.0", 210),
        new ApplicationEntry("IPS Techcard Server 5.0", 247),
        new ApplicationEntry("IPS Techcard Server v6", 297),
        new ApplicationEntry("IPS Techcard Server v7", 358),
        new ApplicationEntry("IPS TP Designer 2.0", 18),
        new ApplicationEntry("IPS TP Designer 3.0", 123),
        new ApplicationEntry("IPS TP Designer 4.0", 211),
        new ApplicationEntry("IPS TP Designer 5.0", 248),
        new ApplicationEntry("IPS TP Designer v6", 298),
        new ApplicationEntry("IPS TP Designer v7", 359),
        new ApplicationEntry("IPS UG Connector 2.0", 25),
        new ApplicationEntry("IPS UG Connector 3.0", 125),
        new ApplicationEntry("IPS UG Connector 4.0", 212),
        new ApplicationEntry("IPS UG Connector 5.0", 249),
        new ApplicationEntry("IPS UG Connector v6", 299),
        new ApplicationEntry("IPS UG Connector v7", 360),
        new ApplicationEntry("IPS WebInterface 2.0", 36),
        new ApplicationEntry("IPS WebInterface 3.0", 126),
        new ApplicationEntry("IPS WebInterface 4.0", 213),
        new ApplicationEntry("IPS WebInterface 5.0", 250),
        new ApplicationEntry("IPS WebInterface v6", 300),
        new ApplicationEntry("IPS WebInterface Client v7", 361),
        new ApplicationEntry("IPS WebInterface Server v7", 362),
        new ApplicationEntry("IPS WebPortal Client 2.0", 33),
        new ApplicationEntry("IPS WebPortal Client 3.0", (int) sbyte.MaxValue),
        new ApplicationEntry("IPS WebPortal Client 4.0", 214),
        new ApplicationEntry("IPS WebPortal Client 5.0", 251),
        new ApplicationEntry("IPS WebPortal Client v6", 301),
        new ApplicationEntry("IPS WebPortal Client v7", 363),
        new ApplicationEntry("IPS WebPortal Server 2.0", 34),
        new ApplicationEntry("IPS WebPortal Server 3.0", 128 /*0x80*/),
        new ApplicationEntry("IPS WebPortal Server 4.0", 215),
        new ApplicationEntry("IPS WebPortal Server 5.0", 252),
        new ApplicationEntry("IPS WebPortal Server v6", 302),
        new ApplicationEntry("IPS WebPortal Server v7", 364),
        new ApplicationEntry("IPS Workflow 2.0", 14),
        new ApplicationEntry("IPS Workflow 3.0", 129),
        new ApplicationEntry("IPS Workflow 4.0", 216),
        new ApplicationEntry("IPS Workflow 5.0", 253),
        new ApplicationEntry("IPS Workflow v6", 303),
        new ApplicationEntry("IPS Workflow v7", 366),
        new ApplicationEntry("IPS Workflow Server 2.0", 30),
        new ApplicationEntry("IPS Workflow Server 3.0", 130),
        new ApplicationEntry("IPS Workflow Server 4.0", 218),
        new ApplicationEntry("IPS Workflow Server 5.0", 254),
        new ApplicationEntry("IPS Workflow Server v6", 304),
        new ApplicationEntry("IPS Workflow Server v7", 365),
        new ApplicationEntry("IPS XML Client 4.0", 96 /*0x60*/),
        new ApplicationEntry("IPS XML Client 5.0", 98),
        new ApplicationEntry("IPS XML Client v6", 305),
        new ApplicationEntry("IPS XML Client v7", 367),
        new ApplicationEntry("IPS XML Server 4.0", 95),
        new ApplicationEntry("IPS XML Server 5.0", 97),
        new ApplicationEntry("IPS XML Server v6", 306),
        new ApplicationEntry("IPS XML Server v7", 368),
        new ApplicationEntry("IPS Компас Connector 2.0", 39),
        new ApplicationEntry("IPS Компас Connector 3.0", 131),
        new ApplicationEntry("IPS Компас Connector 4.0", 219),
        new ApplicationEntry("IPS Компас Connector 5.0", (int) byte.MaxValue),
        new ApplicationEntry("IPS Компас Connector v6", 307),
        new ApplicationEntry("IPS Компас Connector v7", 369),
        new ApplicationEntry("AVS 6", 137),
        new ApplicationEntry("Imbase 5", 138),
        new ApplicationEntry("IMH 1", 139),
        new ApplicationEntry("IMViewer Pro v2", 328),
        new ApplicationEntry("Cadmech nanoCAD v1.11", 326),
        new ApplicationEntry("Rotation nanoCAD v1.11", 327),
        new ApplicationEntry("Cadmech nanoCAD v2", 57),
        new ApplicationEntry("Rotation nanoCAD v2", 58),
        new ApplicationEntry("Cadmech-T 15", 259),
        new ApplicationEntry("Cadmech-T v16", 48 /*0x30*/),
        new ApplicationEntry("Cadmech-T v17", 314),
        new ApplicationEntry("Cadmech-T v18", 333),
        new ApplicationEntry("Cadmech 14", 141),
        new ApplicationEntry("Cadmech 15", 142),
        new ApplicationEntry("Cadmech v16", 47),
        new ApplicationEntry("Cadmech v17", 312),
        new ApplicationEntry("Cadmech v18", 331),
        new ApplicationEntry("Cadmech v19", 62),
        new ApplicationEntry("Cadmech BricsCAD 2", 143),
        new ApplicationEntry("Cadmech BricsCAD 3", 144 /*0x90*/),
        new ApplicationEntry("Cadmech BricsCAD 4", 257),
        new ApplicationEntry("Cadmech BricsCAD v6", 309),
        new ApplicationEntry("Cadmech BricsCAD v7", 315),
        new ApplicationEntry("Cadmech BricsCAD v8", 60),
        new ApplicationEntry("Cadmech Inventor 10", 145),
        new ApplicationEntry("Cadmech Inventor 11", 146),
        new ApplicationEntry("Cadmech Inventor v12", 50),
        new ApplicationEntry("Cadmech Inventor v13", 311),
        new ApplicationEntry("Cadmech Inventor v14", 330),
        new ApplicationEntry("Cadmech Inventor v15", 371),
        new ApplicationEntry("Cadmech Inventor v16", 59),
        new ApplicationEntry("Cadmech КОМПАС-3D v1", 64 /*0x40*/),
        new ApplicationEntry("Cadmech ProE 6", 147),
        new ApplicationEntry("Cadmech ProE 7", 148),
        new ApplicationEntry("Cadmech ProE v8", 329),
        new ApplicationEntry("Cadmech ProE v9", 376),
        new ApplicationEntry("Cadmech ProE v10", 56),
        new ApplicationEntry("Cadmech SE 3", 149),
        new ApplicationEntry("Cadmech SE 4", 150),
        new ApplicationEntry("Cadmech SE v6", 377),
        new ApplicationEntry("Cadmech SE v7", 378),
        new ApplicationEntry("Cadmech SE v8", 55),
        new ApplicationEntry("Cadmech SW 13", 151),
        new ApplicationEntry("Cadmech SW 14", 152),
        new ApplicationEntry("Cadmech SW v17", 308),
        new ApplicationEntry("Cadmech SW v18", 317),
        new ApplicationEntry("Cadmech SW v19", 370),
        new ApplicationEntry("Cadmech SW v20", 54),
        new ApplicationEntry("Cadmech UG 10", 153),
        new ApplicationEntry("Cadmech UG 11", 154),
        new ApplicationEntry("Cadmech UG 12", 41),
        new ApplicationEntry("Cadmech UG 13", 46),
        new ApplicationEntry("Cadmech UG v14", 51),
        new ApplicationEntry("Cadmech UG v15", 52),
        new ApplicationEntry("Cadmech UG v16", 53),
        new ApplicationEntry("IMForging", 185),
        new ApplicationEntry("ImProject 7", 135),
        new ApplicationEntry("ImProject 8", 134),
        new ApplicationEntry("IMShape 1", 167),
        new ApplicationEntry("LCAD 5", 163),
        new ApplicationEntry("LCAD 6", 164),
        new ApplicationEntry("LCAD BricsCAD 5", 165),
        new ApplicationEntry("LCAD BricsCAD 6", 166),
        new ApplicationEntry("Rotation 14", 155),
        new ApplicationEntry("Rotation 15", 156),
        new ApplicationEntry("Rotation v16", 49),
        new ApplicationEntry("Rotation v17", 313),
        new ApplicationEntry("Rotation v18", 332),
        new ApplicationEntry("Rotation v19", 63 /*0x3F*/),
        new ApplicationEntry("Spring  BricsCAD 2", 161),
        new ApplicationEntry("Rotation BricsCAD 3", 160 /*0xA0*/),
        new ApplicationEntry("Rotation BricsCAD 4", 258),
        new ApplicationEntry("Rotation BricsCAD v6", 310),
        new ApplicationEntry("Rotation BricsCAD v7", 316),
        new ApplicationEntry("Rotation BricsCAD v8", 61),
        new ApplicationEntry("Search 14", 132),
        new ApplicationEntry("Search SPDS", 136),
        new ApplicationEntry("Search-T 14", 133),
        new ApplicationEntry("ядро Search v16", 372),
        new ApplicationEntry("Search v16", 373),
        new ApplicationEntry("Search-T v16", 374),
        new ApplicationEntry("Search СПДС v16", 375),
        new ApplicationEntry("Show 5", 140),
        new ApplicationEntry("Spring 14", 157),
        new ApplicationEntry("Spring 15", 158),
        new ApplicationEntry("Spring  BricsCAD 3", 162),
        new ApplicationEntry("TechCard 10", 170),
        new ApplicationEntry("TechCard 9", 169),
        new ApplicationEntry("UGTrans", 187),
        new ApplicationEntry("АРМ Конструктора оснастки 10", 172),
        new ApplicationEntry("АРМ Конструктора оснастки 9", 171),
        new ApplicationEntry("АРМ Материального нормирования 10", 174),
        new ApplicationEntry("АРМ Материального нормирования 9", 173),
        new ApplicationEntry("АРМ Нормирования техпроцессов 10", 176 /*0xB0*/),
        new ApplicationEntry("АРМ Нормирования техпроцессов 9", 175),
        new ApplicationEntry("АРМ Перевода техпроцессов 10", 178),
        new ApplicationEntry("АРМ Перевода техпроцессов 9", 177),
        new ApplicationEntry("АРМ Расцех. маршрутов/Матер. нормирвоания 10", 182),
        new ApplicationEntry("АРМ Расцех. маршрутов/Матер. нормирвоания 9", 181),
        new ApplicationEntry("АРМ Расцеховочных маршрутов 10", 180),
        new ApplicationEntry("АРМ Расцеховочных маршрутов 9", 179),
        new ApplicationEntry("Модуль экспорта-импорта XML 10", 184),
        new ApplicationEntry("Модуль экспорта-импорта XML 9", 183),
        new ApplicationEntry("Трудоемкость проектирования", 38),
        new ApplicationEntry("Трудоемкость проектирования AutoCAD", 188),
        new ApplicationEntry("Трудоемкость проектирования Inventor", 189),
        new ApplicationEntry("Трудоемкость проектирования NX", 190),
        new ApplicationEntry("TechCard 11", 217),
        new ApplicationEntry("АРМ Конструктора оснастки 11", 260),
        new ApplicationEntry("АРМ Материального нормирования 11", 261),
        new ApplicationEntry("АРМ Нормирования техпроцессов 11", 262),
        new ApplicationEntry("АРМ Перевода техпроцессов 11", 263),
        new ApplicationEntry("АРМ Расцех. маршрутов/Матер. нормирования 11", 264),
        new ApplicationEntry("АРМ Расцеховочных маршрутов 11", 265),
        new ApplicationEntry("Модуль экспорта-импорта XML 11", 266),
        new ApplicationEntry("TechCard v12", 318),
        new ApplicationEntry("АРМ Конструктора оснастки v12", 319),
        new ApplicationEntry("АРМ Материального нормирования v12", 320),
        new ApplicationEntry("АРМ Нормирования техпроцессов v12", 321),
        new ApplicationEntry("АРМ Перевода техпроцессов v12", 322),
        new ApplicationEntry("АРМ Расцех. маршрутов/Матер. нормирования v12", 323),
        new ApplicationEntry("АРМ Расцеховочных маршрутов v12", 324),
        new ApplicationEntry("Модуль экспорта-импорта XML v12", 325)
      };

      internal static void Clear()
      {
        foreach (ApplicationEntry app in KeyApplications._apps)
          app.Checked = false;
      }

      internal static ApplicationEntry GetEntry(int appId)
      {
        foreach (ApplicationEntry app in KeyApplications._apps)
        {
          if (app.ApplicationId == appId)
            return app;
        }
        return (ApplicationEntry) null;
      }

      internal static IApplicationEntry[] Applications
      {
        get
        {
          int length = KeyApplications._apps.Length;
          List<IApplicationEntry> applicationEntryList = new List<IApplicationEntry>(length);
          for (int index = 0; index < length; ++index)
          {
            ApplicationEntry app = KeyApplications._apps[index];
            applicationEntryList.Add((IApplicationEntry) new KeyApplications.ShortAppEntry(app.ApplicationName, app.ApplicationId));
          }
          return applicationEntryList.ToArray();
        }
      }

      internal static int CheckedCount(ref int value)
      {
        value = 0;
        int num = 0;
        foreach (ApplicationEntry app in KeyApplications._apps)
        {
          if (app.Checked)
          {
            ++num;
            value += app.ApplicationId & 4095 /*0x0FFF*/;
          }
        }
        value &= (int) ushort.MaxValue;
        return num;
      }

      internal static bool Login(int appId)
      {
        bool flag = false;
        ApplicationEntry entry = KeyApplications.GetEntry(appId);
        if (entry != null)
        {
          Hasp hasp = new Hasp(HaspFeature.FromFeature(appId));
          KeyHelper.CheckStatus(hasp.Login(VendorCode.Code, KeyHelper.KeyScope));
          entry.Key = hasp;
          flag = true;
        }
        return flag;
      }

      internal static int Unchecked
      {
        get
        {
          int num = 0;
          foreach (ApplicationEntry app in KeyApplications._apps)
          {
            if (!app.Checked)
              ++num;
          }
          return num;
        }
      }

      private class ShortAppEntry : IApplicationEntry
      {
        private string _name;
        private int _id;

        public ShortAppEntry(string name, int appId)
        {
          this._name = name;
          this._id = appId;
        }

        public string Name => this._name;

        public int Id => this._id;

        public override string ToString() => $"{this._id.ToString("000")} : {this._name}";
      }
    }
}
