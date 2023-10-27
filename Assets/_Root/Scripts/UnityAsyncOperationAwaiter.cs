using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

public static class ExtensionMethods
{
    public static TaskAwaiter GetAwaiter(this AsyncOperation asyncOperation)
    {
        var taskcompletionSource = new TaskCompletionSource<object>();
        asyncOperation.completed += obj => { taskcompletionSource.SetResult(null); };
        return ((Task)taskcompletionSource.Task).GetAwaiter();
    }
}
