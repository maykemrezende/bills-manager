namespace IntegrationTests.Fixtures;

[CollectionDefinition(nameof(TestBillsCollection))]
public class TestBillsCollection : ICollectionFixture<PostgresTestContainerFixture>
{
}
