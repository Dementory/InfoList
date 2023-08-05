using InfoList.Presentation;
using Zenject;

public class InfoPageInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IInfoListViewModel>().To<InfoListViewModel>().AsSingle();
    }
}