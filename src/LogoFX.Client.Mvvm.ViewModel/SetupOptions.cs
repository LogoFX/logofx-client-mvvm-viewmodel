namespace LogoFX.Client.Mvvm.ViewModel
{
    /// <summary>
    /// Represents setup options for <see cref="WrappingCollection"/>
    /// </summary>
    public class SetupOptions
    {
        public bool IsBulk { get; protected set; }
        public bool IsConcurrent { get; protected set; }

        /// <summary>
        /// Configures the setup to use bulk mode.
        /// </summary>
        /// <returns></returns>
        public SetupOptions UseBulk()
        {
            IsBulk = true;
            return this;
        }

        /// <summary>
        /// Configures the setup to use concurrent data structures.
        /// </summary>
        /// <returns></returns>
        public SetupOptions UseConcurrent()
        {
            IsConcurrent = true;
            return this;
        }
    }
}
