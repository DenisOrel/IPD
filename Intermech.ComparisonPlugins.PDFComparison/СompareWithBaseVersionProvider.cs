using Intermech.Interfaces;


namespace Intermech.ComparisonPlugins.PDFComparison
{
    public class СompareWithBaseVersionProvider : ComparisonProvider
    {
      protected override void SetComparedVersions(long firstItem, long secondItem)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          long idByObjectId = sessionKeeper.Session.GetIDByObjectID(firstItem);
          long objectId = sessionKeeper.Session.GetObjectBaseVersionByID(idByObjectId, false).ObjectID;
          base.SetComparedVersions(firstItem, objectId);
        }
      }
    }
}
