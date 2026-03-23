using SampleApp.UI.ViewModels;

namespace SampleApp.UI.Tests.ViewModels;

public class CouterMvvmViewModelTests
{
    [Fact]
    public void 初期値()
    {
        // Arrange, Act
        var vm = new CouterMvvmViewModel();

        // Assert
        Assert.Equal(0, vm.Count);
    }

    public class IncrementCommand
    {
        [Fact]
        public void 正常()
        {
            // Arrange
            var vm = new CouterMvvmViewModel();

            // Act
            vm.IncrementCommand.Execute(null);

            // Assert
            // Count が1になっていること
            Assert.Equal(1, vm.Count);
        }
    }

    public class DecrementCommand
    {
        [Fact]
        public void 正常()
        {
            // Arrange
            var vm = new CouterMvvmViewModel();
            vm.IncrementCommand.Execute(null);
            vm.IncrementCommand.Execute(null);

            // Act
            vm.DecrementCommand.Execute(null);

            // Assert
            // Count が1になっていること
            Assert.Equal(1, vm.Count);
        }

        [Fact]
        public void Countがゼロ()
        {
            // Arrange
            var vm = new CouterMvvmViewModel();

            // Act
            vm.DecrementCommand.Execute(null);

            // Assert
            // Count が0のままであること
            Assert.Equal(0, vm.Count);
        }
    }

    public class CanDecrementCommand
    {
        [Fact]
        public void Countがゼロ()
        {
            // Arrange
            var vm = new CouterMvvmViewModel();

            // Act & Assert
            Assert.False(vm.DecrementCommand.CanExecute(null));
        }

        [Fact]
        public void Countがゼロより大きい()
        {
            // Arrange
            var vm = new CouterMvvmViewModel();
            vm.IncrementCommand.Execute(null);

            // Act & Assert
            Assert.True(vm.DecrementCommand.CanExecute(null));
        }
    }
}
