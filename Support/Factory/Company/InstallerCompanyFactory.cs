namespace Support.Factory.Company;

public class InstallerCompanyFactory : CompanyFactory
{
    public override ICompany CreateCompany()
    {
        return new InstallerCompany();
    }
}