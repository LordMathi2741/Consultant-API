namespace Support.Factory.Cylinder;

public class WorkShopCylinderFactory : CylinderFactory
{
    public override ICylinder CreateCylinder()
    {
        return new WorkShopCylinder();
    }
}