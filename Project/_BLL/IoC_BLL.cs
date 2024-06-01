using Ninject.Modules;

namespace Project_BLL
{
    public class IoC_BLL : NinjectModule
    {
        public override void Load()
        {
            Bind<ILogic>().To<BLL>();
        }
    }
}
