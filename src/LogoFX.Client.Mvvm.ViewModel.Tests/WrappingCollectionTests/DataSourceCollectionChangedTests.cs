using System.Collections.ObjectModel;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace LogoFX.Client.Mvvm.ViewModel.Tests.WrappingCollectionTests
{        
    public class DataSourceCollectionChangedTests : WrappingCollectionTestsBase
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void DataSourceCollectionChanged_ModelIsAdded_ViewModelIsAdded(bool isConcurrent)
        {
            var dataSource =
                new ObservableCollection<TestModel>(new[] {new TestModel(1), new TestModel(2), new TestModel(3)});

            var wrappingCollection = new WrappingCollection(false, isConcurrent) {FactoryMethod = o => new TestViewModel((TestModel)o)};
            wrappingCollection.AddSource(dataSource);
            dataSource.Add(new TestModel(4));

            var viewModels = wrappingCollection.OfType<TestViewModel>();
            var actualViewModel = viewModels.SingleOrDefault(t => t.Model.Id == 4);
            actualViewModel.Should().NotBeNull();            
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void DataSourceCollectionChanged_ModelIsRemoved_ViewModelIsRemoved(bool isConcurrent)
        {
            var dataSource =
                new ObservableCollection<TestModel>(new[] { new TestModel(1), new TestModel(2), new TestModel(3) });

            var wrappingCollection = new WrappingCollection(false, isConcurrent) { FactoryMethod = o => new TestViewModel((TestModel)o) };
            wrappingCollection.AddSource(dataSource);
            dataSource.Remove(dataSource.Last());

            var viewModels = wrappingCollection.OfType<TestViewModel>();
            var actualViewModel = viewModels.SingleOrDefault(t => t.Model.Id == 3);
            actualViewModel.Should().BeNull();           
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void DataSourceCollectionChanged_DataSourceIsCleared_ViewModelsAreCleared(bool isConcurrent)
        {
            var dataSource =
                new ObservableCollection<TestModel>(new[] { new TestModel(1), new TestModel(2), new TestModel(3) });

            var wrappingCollection = new WrappingCollection(false, isConcurrent) { FactoryMethod = o => new TestViewModel((TestModel)o) };
            wrappingCollection.AddSource(dataSource);
            dataSource.Clear();

            var viewModels = wrappingCollection.OfType<TestViewModel>();
            viewModels.Should().BeEmpty();            
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void DataSourcesCollectionChanged_DataSourceIsAdded_ViewModelsAreAdded(bool isConcurrent)
        {
            var originalDataSource =
                new ObservableCollection<TestModel>(new[] { new TestModel(1), new TestModel(2), new TestModel(3) });
            var anotherDataSource = new ObservableCollection<TestModel>(new[] {new TestModel(5), new TestModel(6)});

            var wrappingCollection = new WrappingCollection(false, isConcurrent) { FactoryMethod = o => new TestViewModel((TestModel)o) };
            wrappingCollection.AddSource(originalDataSource);
            wrappingCollection.AddSource(anotherDataSource);

            wrappingCollection.Should().ContainItemsAssignableTo<TestViewModel>();            
            var expectedModels = originalDataSource.Concat(anotherDataSource);
            wrappingCollection.OfType<TestViewModel>().Select(t => t.Model).Should().BeEquivalentTo(expectedModels);            
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void DataSourcesCollectionChanged_DataSourceIsRemoved_ViewModelsAreRemoved(bool isConcurrent)
        {
            var originalDataSource =
                new ObservableCollection<TestModel>(new[] { new TestModel(1), new TestModel(2), new TestModel(3) });            

            var wrappingCollection = new WrappingCollection(false, isConcurrent) { FactoryMethod = o => new TestViewModel((TestModel)o) };
            wrappingCollection.AddSource(originalDataSource);
            wrappingCollection.RemoveSource(originalDataSource);

            wrappingCollection.Should().BeEmpty();            
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void DataSourcesCollectionChanged_DataSourcesAreCleared_ViewModelsAreRemoved(bool isConcurrent)
        {
            var originalDataSource =
                new ObservableCollection<TestModel>(new[] { new TestModel(1), new TestModel(2), new TestModel(3) });

            var wrappingCollection = new WrappingCollection(false, isConcurrent) { FactoryMethod = o => new TestViewModel((TestModel)o) };
            wrappingCollection.AddSource(originalDataSource);
            wrappingCollection.ClearSources();

            wrappingCollection.Should().BeEmpty();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void DataSourcesCollectionChanged_DataSourceIsAddedThenAllModelsAreRemovedThenModelIsAdded_ViewModelIsAdded(bool isConcurrent)
        {
            var models = new[] { new TestModel(1), new TestModel(2), new TestModel(3)};
            var newModel = new TestModel(4);
            var originalDataSource =
                new ObservableCollection<TestModel>(models);            

            var wrappingCollection = new WrappingCollection(false, isConcurrent) { FactoryMethod = o => new TestViewModel((TestModel)o) };
            wrappingCollection.AddSource(originalDataSource);
            originalDataSource.Remove(models[0]);
            originalDataSource.Remove(models[1]);
            originalDataSource.Remove(models[2]);
            originalDataSource.Add(newModel);

            var expectedModels = new[] {newModel};
            wrappingCollection.OfType<TestViewModel>().Select(t => t.Model).Should().BeEquivalentTo(expectedModels);            
        }
    }
}
