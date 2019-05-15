using System;

namespace LogoFX.Client.Mvvm.ViewModel
{
    public class SelectionSetupOptions : SetupOptions
    {
        private const SelectionMode DefaultSelectionMode = SelectionMode.ZeroOrMore;
        public SelectionMode SelectionMode { get; private set; } = DefaultSelectionMode;
        public Predicate<object> SelectionPredicate { get; private set; }       

        public SelectionSetupOptions UseSelectionMode(SelectionMode selectionMode)
        {
            SelectionMode = selectionMode;
            return this;
        }

        public SelectionSetupOptions UseSelectionPredicate(Predicate<object> selectionPredicate)
        {
            SelectionPredicate = selectionPredicate;
            return this;
        }

        public new SelectionSetupOptions UseBulk()
        {
            base.UseBulk();
            return this;
        }

        public new SelectionSetupOptions UseConcurrent()
        {
            base.UseConcurrent();
            return this;
        }
    }
}