using Unity.AspNet.Mvc;
using Unity.Lifetime;

namespace AuthorizeNetSample.Web.Ioc {
    public class PerRequestOrTransientLifeTimeManager : LifetimeManager {
        private readonly PerRequestLifetimeManager m_PerRequestLifetimeManager = new PerRequestLifetimeManager();
        private readonly TransientLifetimeManager m_TransientLifetimeManager = new TransientLifetimeManager();

        public override object GetValue() {
            return GetAppropriateLifetimeManager().GetValue();
        }
        public override void SetValue(object newValue) {
            GetAppropriateLifetimeManager().SetValue(newValue);
        }
        public override void RemoveValue() {
            GetAppropriateLifetimeManager().RemoveValue();
        }

        private LifetimeManager GetAppropriateLifetimeManager() {
            //PerRequestLifetimeManager can only be used in the context of an HTTP request
            if (System.Web.HttpContext.Current == null)
                return m_TransientLifetimeManager;

            return m_PerRequestLifetimeManager;
        }
    }
}