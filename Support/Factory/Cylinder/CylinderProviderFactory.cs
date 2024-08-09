namespace Support.Factory.Cylinder;

public class CylinderProviderFactory : CylinderFactory
{
    public override ICylinder CreateCylinder()
    {
        return new CylinderProvider();
    }
}