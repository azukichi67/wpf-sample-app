using SampleApp.UI.Models;

namespace SampleApp.UI.Tests.Models;

public class CountModelTests
{
    [Fact]
    public void 初期値()
    {
        // Arrange, Act
        var model = new CountModel();

        // Assert
        Assert.Equal(0, model.Value);
    }

    public class Increment
    {

        [Fact]
        public void 正常()
        {
            // Arrange
            var model = new CountModel();

            // Act
            model.Increment();

            // Assert
            // Value が1になっていること
            Assert.Equal(1, model.Value);
        }
    }

    public class Decrement
    {
        [Fact]
        public void 正常()
        {
            // Arrange
            var model = new CountModel();
            model.Increment();
            model.Increment();

            // Act
            model.Decrement();

            // Assert
            // Value が1になっていること
            Assert.Equal(1, model.Value);
        }

        [Fact]
        public void Countがゼロ()
        {
            // Arrange
            var model = new CountModel();

            // Act
            model.Decrement();

            // Assert
            // Value が0のままであること
            Assert.Equal(0, model.Value);
        }
    }

    public class CanDecrement
    {

        [Fact]
        public void Countがゼロ()
        {
            // Arrange
            var model = new CountModel();

            // Act & Assert
            Assert.False(model.CanDecrement());
        }

        [Fact]
        public void Countがゼロより大きい()
        {
            // Arrange
            var model = new CountModel();
            model.Increment();

            // Act & Assert
            Assert.True(model.CanDecrement());
        }
    }
}
