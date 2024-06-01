using Ninject.Modules;
using DAL;

namespace Project_DAL
{
    public class IoC_DAL : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>();
        }
    }
}
