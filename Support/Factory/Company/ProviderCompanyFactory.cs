namespace Support.Factory.Company;

public class ProviderCompanyFactory : CompanyFactory
{
    public override ICompany CreateCompany()
    {
        return new ProviderCompany();
    }
}