# LLM-Visitors
[UniTask] [Extension] Shortcut for 1-to-100 tickrate halls-visitors system (e.g subscriber-notifier system)

Simple example of our `Discord Overlay Service` being one of many `IVisitor` instances that is inside `MonoHall` ticking with custom tickrate:
```
using Cysharp.Threading.Tasks;
using LLM.Visitors.Base;
using LLM.Services.DiscordWrapper;

namespace LLM.Visitors
{
    public class DiscordVisitor : IVisitor
    {
        private DiscordService service;

        public DiscordVisitor(DiscordService service)
        {
            service.Initialize();
            service.LookupForDiscordClient();
        }

        public UniTask OnVisitorEnter() => service.EnableOverlay();
        public UniTask OnVisitorLeave() => service.DisposeOverlay();

        public void OnTick() => service.PerformOverlayTicks();
    }
}
```

Our `Service Collection` has initialized `MonoHall` instance:

```
private void InitializeHall(ServiceCollection services)
{
    hall.Enter(new DiscordVisitor(services.Get<DiscordService>()));
    hall.Enter(new FireabseVisitor(services.Get<Database>()));
    hall.Enter(new DevConsoleVisitor());
    hall.Enter(new LogTesterVisitor());

    DontDestroyOnLoad(hall);
}
```
