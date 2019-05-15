using System.Collections.ObjectModel;
using System.Threading;
using FluentAssertions;
using Xunit;

namespace LogoFX.Client.Mvvm.ViewModel.Tests.WrappingCollectionTests
{
    public class ConcurrencyTests
    {
        static ConcurrencyTests()
        {
            Dispatch.Current = new DefaultDispatch();
        }

        [Fact]
        public void Add_ModelIsAddedAndThenRemovedAndThenReAdded_ExceptionIsNotThrown()
        {
            var lastModel = new TestModel(3);
            var dataSource =
                new ObservableCollection<TestModel>(new[] {new TestModel(1), new TestModel(2)});

            var wc = new WrappingCollection(r => r.UseConcurrent()).WithSource(dataSource);
            dataSource.Add(lastModel);
            dataSource.Remove(lastModel);
            var exception = Record.Exception(() => dataSource.Add(lastModel));

            exception.Should().BeNull();
        }
    }
}
