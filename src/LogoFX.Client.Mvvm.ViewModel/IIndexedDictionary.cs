using System.Collections.Generic;

namespace LogoFX.Client.Mvvm.ViewModel
{
    interface IIndexedDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {        
        void AddAt(int index, TKey key, TValue item);                
    }

    interface IIndexedDictionaryFactory
    {
        IIndexedDictionary<TKey, TValue> Create<TKey, TValue>();
    }

    class RegularIndexedDictionaryFactory : IIndexedDictionaryFactory
    {
        public IIndexedDictionary<TKey, TValue> Create<TKey, TValue>()
        {
            return new WrappingCollection.IndexedDictionary<TKey, TValue>();
        }
    }

    class ConcurrentIndexedDictionaryFactory : IIndexedDictionaryFactory
    {
        public IIndexedDictionary<TKey, TValue> Create<TKey, TValue>()
        {
            return new WrappingCollection.ConcurrentIndexedDictionary<TKey, TValue>();
        }
    }
}