using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Xamarin.Basics.Extensions.Tasks
{
    public static class WhenAllTaskExtensions
    {
        public static async Task<(T0, T1)> WhenAll<T0, T1>(Task<T0> task0, Task<T1> task1)
        {
            await Task.WhenAll(task0, task1).ConfigureAwait(false);
            return (task0.Result, task1.Result);
        }

        public static async Task<(T0, T1, T2)> WhenAll<T0, T1, T2>(Task<T0> task0, Task<T1> task1, Task<T2> task2)
        {
            await Task.WhenAll(task0, task1, task2).ConfigureAwait(false);
            return (task0.Result, task1.Result, task2.Result);
        }

        public static async Task<T0> WhenAll<T0>(Task<T0> task0, Task task1)
        {
            await Task.WhenAll(task0, task1).ConfigureAwait(false);
            return task0.Result;
        }

        public static async Task<(T0, T1)> WhenAll<T0, T1>(Task<T0> task0, Task<T1> task1, Task task2)
        {
            await Task.WhenAll(task0, task1, task2).ConfigureAwait(false);
            return (task0.Result, task1.Result);
        }

        public static TaskAwaiter<(T1, T2)> GetAwaiter<T1, T2>(this ValueTuple<Task<T1>, Task<T2>> tasks)
        {
            return WhenAll(tasks.Item1, tasks.Item2).GetAwaiter();
        }

        public static TaskAwaiter<(T1, T2, T3)> GetAwaiter<T1, T2, T3>(
            this ValueTuple<Task<T1>, Task<T2>, Task<T3>> tasks)
        {
            return WhenAll(tasks.Item1, tasks.Item2, tasks.Item3).GetAwaiter();
        }

        public static TaskAwaiter<T1> GetAwaiter<T1>(this ValueTuple<Task<T1>, Task> tasks)
        {
            return WhenAll(tasks.Item1, tasks.Item2).GetAwaiter();
        }

        public static TaskAwaiter<(T1, T2)> GetAwaiter<T1, T2>(this ValueTuple<Task<T1>, Task<T2>, Task> tasks)
        {
            return WhenAll(tasks.Item1, tasks.Item2, tasks.Item3).GetAwaiter();
        }
    }
}