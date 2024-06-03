using CKB;
using static CKB.Utilities.CommonGameplayFacade;

public class SpeedItem : Item
{
    protected override void TakeItem(Group group)
    {
        group.StartDash();
        ItemSpawn.Spawn(0);
        Destroy(gameObject);
    }
}
