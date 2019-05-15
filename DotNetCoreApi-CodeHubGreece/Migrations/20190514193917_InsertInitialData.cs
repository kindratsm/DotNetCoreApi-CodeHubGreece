using Microsoft.EntityFrameworkCore.Migrations;

namespace DotNetCoreApi_CodeHubGreece.Migrations
{
    public partial class InsertInitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO Products (Name) VALUES ('Axia');");
            migrationBuilder.Sql(@"INSERT INTO Customers (Name, Description, CountryId) SELECT 'Piraeus Bank', 'Universal Bank Piraeus Bank A.E. was formed in 1916 with the aim of supporting small and medium enterprises', Id From Countries Where Code = 'GR';");
            migrationBuilder.Sql(@"INSERT INTO Customers (Name, Description, CountryId) SELECT 'National Bank of Greece', 'National Bank of Greece is a global banking and financial services company that was founded in 1841', Id From Countries Where Code = 'GR';");
            migrationBuilder.Sql(@"INSERT INTO Customers (Name, Description, CountryId) SELECT 'Eurobank Ergasias SA', 'Founded in 1990, Eurobank Ergasias SA is the second largest bank in Greece', Id From Countries Where Code = 'GR';");
            migrationBuilder.Sql(@"INSERT INTO Customers (Name, Description, CountryId) SELECT 'Alpha Bank', 'Alpha Bank is the largest of the banks in Greece in terms of market capitalization', Id From Countries Where Code = 'GR';");
            migrationBuilder.Sql(@"INSERT INTO Customers (Name, Description, CountryId) SELECT 'Attica Bank', 'Founded in 1925 and formerly called Bank of Attica SA, Attica Bank Banking Company SA is the country’s fifth-largest bank', Id From Countries Where Code = 'GR';");
            migrationBuilder.Sql(@"INSERT INTO Customers (Name, Description, CountryId) SELECT 'HSBC Greece', 'International bank HSBC established a presence in Greece in 1981 and is now one of the major banks in Greece', Id From Countries Where Code = 'GR';");
            migrationBuilder.Sql(@"INSERT INTO Customers (Name, Description, CountryId) SELECT 'Citibank Greece', 'One of the largest card issuers in Greece is international bank, Citibank', Id From Countries Where Code = 'GR';");
            migrationBuilder.Sql(@"INSERT INTO Customers (Name, Description, CountryId) SELECT 'Aegean Baltic Bank', 'Specialist Aegean Baltic Bank (ABBank) provides corporate, investment, and wealth management products and services, focusing on the Greek and regional shipping communities', Id From Countries Where Code = 'GR';");
            migrationBuilder.Sql(@"INSERT INTO Customers (Name, Description, CountryId) SELECT 'Investment Bank of Greece', 'With headquarters in Athens, the Investment Bank of Greece SA is a subsidiary of Cyprus Popular Bank', Id From Countries Where Code = 'GR';");
            migrationBuilder.Sql(@"INSERT INTO Versions (ReleaseDate, ProductId, VersionNumber, Description) SELECT '2019-05-01', Id, '1.0.0', '1.0.0: Initial release' FROM Products WHERE Name = 'Axia';");
            migrationBuilder.Sql(@"INSERT INTO Versions (ReleaseDate, ProductId, VersionNumber, Description) SELECT '2019-05-01', Id, '1.1.0', '1.1.0: Bug fixes' FROM Products WHERE Name = 'Axia';");
            migrationBuilder.Sql(@"INSERT INTO Versions (ReleaseDate, ProductId, VersionNumber, Description) SELECT '2019-05-01', Id, '1.2.0', '1.2.0: Performance improvements' FROM Products WHERE Name = 'Axia';");
            migrationBuilder.Sql(@"INSERT INTO Versions (ReleaseDate, ProductId, VersionNumber, Description) SELECT '2019-05-01', Id, '1.3.0', '1.3.0: New module' FROM Products WHERE Name = 'Axia';");
            migrationBuilder.Sql(@"INSERT INTO Versions (ReleaseDate, ProductId, VersionNumber, Description) SELECT '2019-05-01', Id, '1.4.0', '1.4.0: Performance improvements' FROM Products WHERE Name = 'Axia';");
            migrationBuilder.Sql(@"INSERT INTO Versions (ReleaseDate, ProductId, VersionNumber, Description) SELECT '2019-05-01', Id, '1.5.0', '1.5.0: New features' FROM Products WHERE Name = 'Axia';");
            migrationBuilder.Sql(@"INSERT INTO Versions (ReleaseDate, ProductId, VersionNumber, Description) SELECT '2019-05-01', Id, '1.6.0', '1.6.0: Bug fixes' FROM Products WHERE Name = 'Axia';");
            migrationBuilder.Sql(@"INSERT INTO Versions (ReleaseDate, ProductId, VersionNumber, Description) SELECT '2019-05-01', Id, '1.7.0', '1.7.0: Performance improvements' FROM Products WHERE Name = 'Axia';");
            migrationBuilder.Sql(@"INSERT INTO Versions (ReleaseDate, ProductId, VersionNumber, Description) SELECT '2019-05-01', Id, '1.8.0', '1.8.0: New module' FROM Products WHERE Name = 'Axia';");
            migrationBuilder.Sql(@"INSERT INTO Versions (ReleaseDate, ProductId, VersionNumber, Description) SELECT '2019-05-01', Id, '1.9.0', '1.9.0: Bug fixes' FROM Products WHERE Name = 'Axia';");
            migrationBuilder.Sql(@"INSERT INTO Versions (ReleaseDate, ProductId, VersionNumber, Description) SELECT '2019-05-01', Id, '2.0.0', '2.0.0: New framework' FROM Products WHERE Name = 'Axia';");
            migrationBuilder.Sql(@"INSERT INTO VersionNoteTypes (Name) VALUES ('Module'), ('Bug fix'), ('New feature'), ('Performance improvement');");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DELETE FROM Products;");
            migrationBuilder.Sql(@"DELETE FROM Customers;");
            migrationBuilder.Sql(@"DELETE FROM Versions;");
            migrationBuilder.Sql(@"DELETE FROM VersionNoteTypes;");
        }
    }
}
