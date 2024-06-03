using CKB;
using static CKB.Utilities.CommonGameplayFacade;

public class MagnetItem : Item
{
    protected override void TakeItem(Group group)
    {
        if (group == GroupPlayer)
        {
            GroupPlayer.StartMagnet(2.0f);
        }

        ItemSpawn.Spawn(1);
        Destroy(gameObject);
    }
}
