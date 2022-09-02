using System;
using System.Collections.Generic;
using System.Reflection;
using CustomEcsBase.Filter.Filters.ComponentsFilter;
using CustomEcsBase.Filter.Filters.DataFilter;
using CustomEcsBase.Systems.EcsSystem;
using CustomEcsBase.World;

namespace CustomEcsBase.Systems
{
    public class EcsSystems
    {
        private readonly EcsWorld world;

        private List<IEcsSystem> allSystems = new List<IEcsSystem>();
        private List<IEcsRunSystem> runSystems = new List<IEcsRunSystem>();
        private List<IEcsFixedRunSystem> fixedRunSystems = new List<IEcsFixedRunSystem>();

        private bool inited = false;
        private bool disposed = false;

        public EcsSystems(EcsWorld world)
        {
            this.world = world;
        }

        public void Init()
        {
            if (disposed) return;
            inited = false;

            for (int i = 0; i < allSystems.Count; i++)
            {
                if (allSystems[i] is IEcsInitSystem initSystem)
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

        public void FixedRun()
        {
            if (!inited) return;
            for (int i = 0; i < fixedRunSystems.Count; i++)
            {
                fixedRunSystems[i].FixedRun();
            }
        }

        public void Add(IEcsSystem system)
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

            if (system is IEcsRunSystem runSystem)
            {
                if (!runSystems.Contains(runSystem))
                {
                    runSystems.Add(runSystem);
                }
            }

            if (system is IEcsFixedRunSystem fixedRunSystem)
            {
                if (!fixedRunSystems.Contains(fixedRunSystem))
                {
                    fixedRunSystems.Add(fixedRunSystem);
                }
            }

            InjectData(system);
        }

        public void Dispose()
        {
            if (!inited) return;

            for (int i = 0; i < allSystems.Count; i++)
            {
                if (allSystems[i] is IEcsDisposeSystem disposeSystem)
                {
                    disposeSystem.Dispose();
                }
            }

            allSystems.Clear();
            runSystems.Clear();
            fixedRunSystems.Clear();

            disposed = true;
        }

        private void InjectData(IEcsSystem system)
        {
            if (system == null) return;
            var worldType = world.GetType();
            var systemType = system.GetType();

            var componentsFilters = typeof(EcsBaseComponentsFilter);
            var sharedDataFilters = typeof(EcsBaseSharedDataFilter);

            foreach (var fieldInfo in systemType.GetFields(BindingFlags.Public | BindingFlags.NonPublic |
                                                           BindingFlags.Instance))
            {
                if (fieldInfo.IsStatic) return;

                if (fieldInfo.FieldType.IsAssignableFrom(worldType))
                {
                    fieldInfo.SetValue(system, world);
                }

                if (fieldInfo.FieldType.IsSubclassOf(componentsFilters) ||
                    fieldInfo.FieldType.IsSubclassOf(sharedDataFilters))
                {
                    fieldInfo.SetValue(system, world.GetFilter(fieldInfo.FieldType));
                }
            }
        }
    }
}