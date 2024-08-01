# LLM-Visitors
[UniTask] [Extension] Shortcut for 1-to-100 tickrate halls-visitors system (e.g subscriber-notifier system)

### Ok but how to actually use it?
> Required <a href="https://github.com/Cysharp/UniTask">UniTask</a> itself, so first-thing-first install a .unitypackage <a href="https://github.com/Cysharp/UniTask/releases">from this link</a>.
> Can be used for default .NET classes if you install UniTask as NuGet package. Also feel free to change whatever you want or collab.


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
> [!TIP]
> You can create new `IHall` inheritance that is NOT a MonoBehaviour (like `public class ClockService : IHall`) and you can start it once somewhere ticking with tickrate you want, its all up to you we just showing u a simplest way

```
[SerializeField] private MonoHall hall;
...

private void InitializeHall(ServiceCollection services)
{
    hall.Enter(new DiscordVisitor(services.Get<DiscordService>()));
    hall.Enter(new FireabseVisitor(services.Get<Database>()));
    hall.Enter(new DevConsoleVisitor());
    hall.Enter(new LogTesterVisitor());

    DontDestroyOnLoad(hall);
}
```

> [!TIP]
> Here is how we set it up, making ServiceCollection new additional Scene which holds our services and IHall component, so we can swap scenes and do whatever we want when those two will sit there and provide us a support when we need it.
> Can be done better by making it just plain C# classes ofc, yet its just an example.

![MonoHall Setup Picture](https://bunbun.cloud/assets/images/git/monoHall1.png)

![MonoHall Setup Picture](https://bunbun.cloud/assets/images/git/monoHall2.png)
