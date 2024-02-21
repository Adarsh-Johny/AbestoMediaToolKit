using Abesto.MediaToolKit.Functions.Image;
using Amazon.S3.Model;
using FluentAssertions;

namespace Abesto.MediaToolKit.Tests
{
    public class ImageIdentifierTests
    {
        [Fact]
        public void FindImages_WithNullS3Objects_ReturnsEmptyList()
        {
            List<S3Object>? s3Objects = null;

            var result = s3Objects.FindImages();

            result.Should().BeEmpty();
        }

        [Fact]
        public void FindImages_WithEmptyS3Objects_ReturnsEmptyList()
        {
            List<S3Object> s3Objects = [];

            var result = s3Objects.FindImages();

            result.Should().BeEmpty();
        }

        [Fact]
        public void FindImages_WithImages_ReturnsFilteredImages()
        {
            var s3Objects = new List<S3Object>
        {
            new() { Key = "images/test1.jpg", Size = 1024 },
            new() { Key = "documents/test.docx", Size = 2048 },
            new() { Key = "images/test2.png", Size = 3072 },
            new() { Key = "images/test3.txt", Size = 4096 },
            new() { Key = "images/test5.png", Size = 0 }
        };

            var result = s3Objects.FindImages().ToList();

            result.Should().HaveCount(2);
            result.Should().Contain(s3Object => s3Object.Key == "images/test1.jpg");
            result.Should().Contain(s3Object => s3Object.Key == "images/test2.png");
        }
    }

}
