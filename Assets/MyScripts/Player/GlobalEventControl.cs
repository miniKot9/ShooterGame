using System;

public class GlobalEventControl
{
    public static Action ShootAction;

    public static Action<float, float> HorizontalVerticalMove;
    public static Action<int> ItemDrop;

    public static int PlayerAmmo;
    public static Action UpdateInventory;

}
