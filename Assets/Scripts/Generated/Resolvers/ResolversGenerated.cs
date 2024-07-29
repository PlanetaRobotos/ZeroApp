using System; using System.Collections.Generic;using MVVM;using UnityEngine; namespace MVVM.Generated{    
                public class Resolver__Project_Scripts_Infrastructure_ReactiveAnimations_TestVM: IResolver
                {
                        private Dictionary<string, Func<_Project.Scripts.Infrastructure.ReactiveAnimations.TestVM, IReactiveProperty>> map = new()
                        {
                            { "TestBool", o => o.TestBool },
                        };

                        public IReactiveProperty Map(UnityEngine.Object target, string name)
                        {
                            return map[name].Invoke(target as _Project.Scripts.Infrastructure.ReactiveAnimations.TestVM);
                        }
                }
                
        public static class BindersLoader
        {
            [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
            private static void InitResolvers()
            {
                Binders.AddResolvers(new()
                {
                    {typeof(_Project.Scripts.Infrastructure.ReactiveAnimations.TestVM),new Resolver__Project_Scripts_Infrastructure_ReactiveAnimations_TestVM()},    
                });
            }
        }}