namespace LogoFX.Client.Mvvm.ViewModel
{
    public class SetupOptions
    {
        public bool IsBulk { get; protected set; }
        public bool IsConcurrent { get; protected set; }

        public SetupOptions UseBulk()
        {
            IsBulk = true;
            return this;
        }

        public SetupOptions UseConcurrent()
        {
            IsConcurrent = true;
            return this;
        }
    }
}
