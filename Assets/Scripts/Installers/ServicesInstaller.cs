using Zenject;

public class ServicesInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IDataServer>().To<DataServerMock>().AsSingle();
    }
}