using DogWalking.DL.Context;
using DogWalking.DL.Repositories;
using DogWalking.BL.Services;
using System;

namespace DogWalking.WinForms.Infrastructure
{
    public static class ServiceFactory
    {
        public static void UseClientService(Action<ClientService> action)
        {
            using (var ctx = new DogWalkingContext())
            {
                var repo = new ClientRepository(ctx);
                var service = new ClientService(repo);

                action(service);
            }
        }

        public static TResult UseClientService<TResult>(Func<ClientService, TResult> func)
        {
            using (var ctx = new DogWalkingContext())
            {
                var repo = new ClientRepository(ctx);
                var service = new ClientService(repo);

                return func(service);
            }
        }

        public static void UseAuthService(Action<AuthService> action)
        {
            using (var ctx = new DogWalkingContext())
            {
                var repo = new UserRepository(ctx);
                var service = new AuthService(repo);

                action(service);
            }
        }

        public static TResult UseAuthService<TResult>(Func<AuthService, TResult> func)
        {
            using (var ctx = new DogWalkingContext())
            {
                var repo = new UserRepository(ctx);
                var service = new AuthService(repo);

                return func(service);
            }
        }
    }
}
