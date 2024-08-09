namespace Support.Factory.Cylinder;

public class CylinderOperationCenterFactory : CylinderFactory
{
    public override ICylinder CreateCylinder()
    {
        return new Cylinder();
    }
}