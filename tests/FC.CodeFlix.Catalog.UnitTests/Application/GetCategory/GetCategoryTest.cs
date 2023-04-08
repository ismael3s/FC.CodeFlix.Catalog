﻿using FC.CodeFlix.Catalog.Application.Exceptions;
using FluentAssertions;
using Moq;
using SUT = FC.CodeFlix.Catalog.Application.UseCases.Category.GetCategory;
namespace FC.CodeFlix.Catalog.UnitTests.Application.GetCategory;
[Collection(nameof(GetCategoryTestFixture))]
public class GetCategoryTest
{
    private readonly GetCategoryTestFixture _fixture;

    public GetCategoryTest(GetCategoryTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(ShouldReturnAnCategory_WhenIdIsFound))]
    [Trait("Application", "GetCategory - Use Cases")]
    public async Task ShouldReturnAnCategory_WhenIdIsFound()
    {
        var categoryMockRepository = _fixture.GetCategoryMockRepository();
        var sampleCategory = _fixture.GetCategory();

        categoryMockRepository.Setup(
            x => x.GetAsync(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()
            )
        ).ReturnsAsync(sampleCategory);

        var sut = new SUT.GetCategory(categoryMockRepository.Object);
        var input = new SUT.GetCategoryInput(sampleCategory.Id);

        var output = await sut.Handle(input, CancellationToken.None);

        categoryMockRepository.Verify(
            repository => repository.GetAsync(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()
            ),
            Times.Once
        );


        output.Should().NotBeNull();
        output!.Id.Should().Be(sampleCategory.Id);
        output!.Name.Should().Be(sampleCategory.Name);
    }

    [Fact(DisplayName = nameof(ShouldForwardRepositoryError_WhenCategoryIsNotfound))]
    [Trait("Application", "GetCategory - Use Cases")]
    public async Task ShouldForwardRepositoryError_WhenCategoryIsNotfound()
    {
        var categoryMockRepository = _fixture.GetCategoryMockRepository();
        var guid = Guid.NewGuid();

        categoryMockRepository.Setup(
            x => x.GetAsync(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()
            )
        ).ThrowsAsync(new NotFoundException($"Category '{guid}' not found"));

        var sut = new SUT.GetCategory(categoryMockRepository.Object);
        var input = new SUT.GetCategoryInput(guid);

        var action = async () => await sut.Handle(input, CancellationToken.None);


        await action.Should()
            .ThrowAsync<NotFoundException>()
            .WithMessage($"Category '{guid}' not found");

        categoryMockRepository.Verify(
            repository => repository.GetAsync(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()
            ),
            Times.Once
        );




    }
}
