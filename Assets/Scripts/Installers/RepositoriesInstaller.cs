using InfoList.Data;
using InfoList.Domain;
using Zenject;

public class RepositoriesInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IDataItemsRepository>().To<DataItemsRepository>().AsSingle();
        Container.Bind<ICachedDataItemsRepository>().To<CachedDataItemsRepository>().AsSingle();
    }
}