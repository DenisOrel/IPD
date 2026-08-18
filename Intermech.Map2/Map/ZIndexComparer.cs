using System.Collections;


namespace Intermech.Map
{
    public class ZIndexComparer : IComparer
    {
      public int Compare(object x, object y)
      {
        MapObject mapObject1 = x as MapObject;
        MapObject mapObject2 = y as MapObject;
        if (mapObject1.ZIndex == mapObject2.ZIndex)
          return 0;
        return mapObject1.ZIndex >= mapObject2.ZIndex ? 1 : -1;
      }
    }
}
