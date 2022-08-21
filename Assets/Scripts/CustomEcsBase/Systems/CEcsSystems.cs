using System;
using System.Collections.Generic;
using System.Reflection;
using CustomEcsBase.Systems.EcsSystem;
using CustomEcsBase.World;

namespace CustomEcsBase.Systems
{
    public class CEcsSystems
    {
        private readonly CEcsWorld world;

        private List<ICEcsSystem> allSystems = new List<ICEcsSystem>();
        private List<ICEcsRunSystem> runSystems = new List<ICEcsRunSystem>();

        private bool inited = false;
        private bool disposed = false;

        public CEcsSystems(CEcsWorld world)
        {
            this.world = world;
        }

        public void Init()
        {
            if (disposed) return;
            inited = false;

            for (int i = 0; i < allSystems.Count; i++)
            {
                if (allSystems[i] is ICEcsInitSystem initSystem)
                {
                    initSystem.Init();
                }
            }

            inited = true;
        }


        public void Run()
        {
            if (!inited) return;

            for (int i = 0; i < runSystems.Count; i++)
            {
                runSystems[i].Run();
            }
        }

        public void Add(ICEcsSystem system)
        {
            if (system == null)
            {
                throw new Exception("System is null.");
            }

            if (inited)
            {
                throw new Exception("Cant add system after initialization.");
            }

            if (disposed)
            {
                throw new Exception("Cant touch after destroy.");
            }

            if (!allSystems.Contains(system))
            {
                allSystems.Add(system);
            }

            if (system is ICEcsRunSystem runSystem)
            {
                if (!runSystems.Contains(runSystem))
                {
                    runSystems.Add(runSystem);
                }
            }

            InjectData(system);
        }

        public void Dispose()
        {
            if (!inited) return;

            for (int i = 0; i < allSystems.Count; i++)
            {
                if (allSystems[i] is ICEcsDisposeSystem disposeSystem)
                {
                    disposeSystem.Dispose();
                }
            }

            allSystems.Clear();
            disposed = true;
        }

        private void InjectData(ICEcsSystem system)
        {
            if (system == null) return;
            var worldType = world.GetType();
            var systemType = system.GetType();

            foreach (var fieldInfo in systemType.GetFields(BindingFlags.Public | BindingFlags.NonPublic |
                                                           BindingFlags.Instance))
            {
                if (fieldInfo.IsStatic) return;

                if (fieldInfo.FieldType.IsAssignableFrom(worldType))
                {
                    fieldInfo.SetValue(system, world);
                }
            }

            system.InjectionCompleted();
        }
    }
}