using _Project.Scripts.Configs;

namespace _Project.Scripts.Infrastructure.Tasks
{
    public class LoadConfigsTask : BaseTask
    {
        private readonly ConfigsController _configsController;

        public LoadConfigsTask(ConfigsController configsController)
        {
            _configsController = configsController;
        }

        public override ITask Do()
        {
            _progress = 0.5f;
            _configsController.OnLoadingCompleteEvent -= OnConfigsLoadingComplete;
            _configsController.OnLoadingCompleteEvent += OnConfigsLoadingComplete;
            _configsController.LoadAssets();

            return this;
        }

        public override void Abort()
        {
            _configsController.OnLoadingCompleteEvent -= OnConfigsLoadingComplete;
        }

        private void OnConfigsLoadingComplete()
        {
            _configsController.OnLoadingCompleteEvent -= OnConfigsLoadingComplete;
        
            Complete();
        }
    }
}