namespace wpf_game_dev_cycle.ViewModel
{
    public class HomeViewModel : ObservableObject
    {
        private string _labelText;
        
        public string LabelText
        {
            get => _labelText;
            set
            {
                _labelText = value;
                OnPropertyChanged();
            }
        }

        public HomeViewModel()
        {
            LabelText = $"\nThe application was created with the aim of creating a unified environment for " +
                        $"monitoring\n the product development cycle in the Game development company.\n\nTo navigate, use the menu on the left:\n\n" +
                        $"    * Tables management - edit, analyze, manipulate tabular data to create filters.\n    * Filters - filters, actually.";
        }
    }
}