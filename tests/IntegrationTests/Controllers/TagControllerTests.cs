using Application.Dtos.Tags;
using FluentAssertions;
using Infra.Persistence;
using IntegrationTests.Fixtures;
using Microsoft.AspNetCore.Mvc.Testing;
using Model.Tags;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using Testcontainers.PostgreSql;

namespace IntegrationTests.Controllers;

[CollectionDefinition(nameof(TestBillsCollection))]
public class TagControllerTests : IClassFixture<WebApplicationFactory<Program>>, 
                                    IClassFixture<PostgresTestContainerFixture>
{
    private const string ConnectionStringSettingNode = "ConnectionStrings:BillsDatabase";
    private readonly WebApplicationFactory<Program> _factory;
    private readonly PostgreSqlContainer _postSqlContainer;
    private readonly BillsContext _context;

    public TagControllerTests(WebApplicationFactory<Program> factory,
        PostgresTestContainerFixture postSqlTestcontainerFixture)
    {
        _factory = factory;
        _postSqlContainer = postSqlTestcontainerFixture.PostgreSqlContainer;
        _context = postSqlTestcontainerFixture.Context;
    }

    [Fact]
    public async Task GetOneTag_Returns200WithTag()
    {
        //Arrange
        var tagName = Guid.NewGuid().ToString();
        var tag = new Tag(tagName);

        _context.Tags.Add(tag);

        await _context.SaveChangesAsync();

        var expectedTagResponse = new TagResponse(tag.Name, tag.Code);

        var HttpClient = _factory
            .WithWebHostBuilder(builder =>
            {
                builder.UseSetting(ConnectionStringSettingNode, _postSqlContainer.GetConnectionString());
            })
            .CreateClient();

        //Act
        var httpResponse = await HttpClient.GetAsync($"/api/tags/{tag.Code}");

        //Assert
        httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        var tagResult = await GetResultFromContentAsync<TagResponse>(httpResponse);

        tagResult.Should().BeEquivalentTo(expectedTagResponse);
    }

    [Fact]
    public async Task GetTags_Returns200WithTags()
    {
        //Arrange
        var tagName = Guid.NewGuid().ToString();
        var tag = new Tag(tagName);

        _context.Tags.Add(tag);

        await _context.SaveChangesAsync();

        var expectedTagResponse = new TagResponse(tag.Name, tag.Code);

        var HttpClient = _factory
            .WithWebHostBuilder(builder =>
            {
                builder.UseSetting(ConnectionStringSettingNode, _postSqlContainer.GetConnectionString());
            })
            .CreateClient();

        //Act
        var httpResponse = await HttpClient.GetAsync($"/api/tags");

        //Assert
        httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        var tagResult = await GetResultFromContentAsync<List<TagResponse>>(httpResponse);

        tagResult.Should().HaveCount(1);
        tagResult[0].Should().BeEquivalentTo(expectedTagResponse);
    }    

    [Fact]
    public async Task CreateTag_ReturnsCreatedWithTag()
    {
        //Arrange
        var tagName = Guid.NewGuid().ToString();
        var createdTag = new CreateTagRequest(tagName);

        var HttpClient = _factory
            .WithWebHostBuilder(builder =>
            {
                builder.UseSetting(ConnectionStringSettingNode, _postSqlContainer.GetConnectionString());
            })
            .CreateClient();

        //Act
        var httpResponse = await HttpClient.PostAsJsonAsync("/api/tags", createdTag);

        //Assert
        httpResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        var createdTagResult = await GetResultFromContentAsync<CreatedTagResponse>(httpResponse);

        var tagHttpResponse = await HttpClient.GetAsync($"/api/tags/{createdTagResult!.Code}");
        var tagFromGet = await GetResultFromContentAsync<TagResponse>(tagHttpResponse);

        createdTagResult.Name.Should().Be(tagFromGet.Name);
        createdTagResult.Code.Should().Be(tagFromGet.Code);
    }

    [Fact]
    public async Task DeleteTag_ReturnsNoContentAndTagMustBeDeleted()
    {
        //Arrange
        var tagName = Guid.NewGuid().ToString();
        var tag = new Tag(tagName);

        _context.Tags.Add(tag);

        await _context.SaveChangesAsync();

        var expectedTagResponse = new TagResponse(tag.Name, tag.Code);

        var HttpClient = _factory
            .WithWebHostBuilder(builder =>
            {
                builder.UseSetting(ConnectionStringSettingNode, _postSqlContainer.GetConnectionString());
            })
            .CreateClient();

        //Act
        var httpResponse = await HttpClient.DeleteAsync($"/api/tags/{tag.Code}");

        //Assert
        httpResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
        
        var deletedTag = _context.Tags.FirstOrDefault(t => t.Code == tag.Code && t.Name == tag.Name);
        deletedTag.Should().BeNull();
    }

    [Fact]
    public async Task UpdateTag_ReturnsOkAndTagMustBeUpdated()
    {
        //Arrange
        var tagName = Guid.NewGuid().ToString();
        var tag = new Tag(tagName);

        _context.Tags.Add(tag);

        await _context.SaveChangesAsync();

        var request = new UpdateTagRequest(Guid.NewGuid().ToString());

        var HttpClient = _factory
            .WithWebHostBuilder(builder =>
            {
                builder.UseSetting(ConnectionStringSettingNode, _postSqlContainer.GetConnectionString());
            })
            .CreateClient();

        //Act
        var httpResponse = await HttpClient.PutAsJsonAsync($"/api/tags/{tag.Code}", request);

        //Assert
        httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        var responseTag = await GetResultFromContentAsync<UpdatedTagResponse>(httpResponse);

        var tagHttpResponse = await HttpClient.GetAsync($"/api/tags/{responseTag!.Code}");
        var tagFromGet = await GetResultFromContentAsync<TagResponse>(tagHttpResponse);

        tagFromGet.Code.Should().Be(responseTag.Code);
        tagFromGet.Name.Should().Be(responseTag.Name.ToUpper());

    }

    private static async Task<T> GetResultFromContentAsync<T>(HttpResponseMessage httpResponse)
    {
        var responseJson = await httpResponse.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(responseJson);
    }
}
