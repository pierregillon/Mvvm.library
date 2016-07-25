namespace Mvvm.Example
{
    public class ViewModelLocator
    {
        public CreateUserViewModel CreateUserViewModel
        {
            get { return new CreateUserViewModel(); }
        }

        public MainViewModel MainViewModel
        {
            get { return new MainViewModel(); }
        }
    }
}
