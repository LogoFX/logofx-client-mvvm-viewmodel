using System.Threading.Tasks;

namespace LogoFX.Client.Mvvm.ViewModel.Interfaces
{
    /// <summary>
    /// Represents an editable view model
    /// </summary>
    public interface IEditableViewModel
    {
        /// <summary>
        /// Returns <see cref="true"/> if the view model has changes, <see cref="false"/> otherwise. />
        /// </summary>
        bool IsDirty { get; }
        /// <summary>
        /// Returns <see cref="true"/> if the view model has errors, <see cref="false"/> otherwise. />
        /// </summary>
        bool HasErrors { get; }
        /// <summary>
        /// Gets or sets the value which enables/disables undo operations.
        /// </summary>
        bool CanUndo { get; set; }
        
        /// <summary>
        /// Reverts the last operation.
        /// </summary>
        void Undo();

        /// <summary>
        /// Saves the state asynchronously
        /// </summary>
        /// <returns></returns>
        Task<bool> SaveAsync();        
    }
}
