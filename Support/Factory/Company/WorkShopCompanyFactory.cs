namespace Support.Factory.Company;

public class WorkShopCompanyFactory : CompanyFactory
{
    public override ICompany CreateCompany()
    {
        return new WorkShopCompany();
    }
}