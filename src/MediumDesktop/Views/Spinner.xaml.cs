using MediumDesktop.Behaviors;

namespace MediumDesktop.Views
{
    /// <summary>
    /// Interaction logic for Spinner.xaml
    /// </summary>
    public partial class Spinner
    {
        private StoryboardActiveByIsVisibileBehaviour _storyboardBehaviour;

        public Spinner()
        {
            InitializeComponent();

            _storyboardBehaviour = new StoryboardActiveByIsVisibileBehaviour(this, "Spinner");
        }

        public void Dispose()
        {
            if (_storyboardBehaviour != null)
            {
                _storyboardBehaviour.Dispose();
                _storyboardBehaviour = null;
            }
        }

    }
}
