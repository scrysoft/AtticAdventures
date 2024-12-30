using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace RealSoftGames
{
    public class TaskManager
    {
        private readonly Queue<Func<Task>> taskQueue = new Queue<Func<Task>>();
        private bool isProcessing;

        public void Enqueue(Func<Task> taskGenerator)
        {
            taskQueue.Enqueue(taskGenerator);
            if (!isProcessing)
                ProcessTaskQueue();
        }

        public void Enqueue<TResult>(Func<Task<TResult>> taskGenerator, Action<TResult> onCompleted)
        {
            async Task WrappedTask()
            {
                TResult result = await taskGenerator();
                onCompleted?.Invoke(result);
            }

            taskQueue.Enqueue(WrappedTask);
            if (!isProcessing)
                ProcessTaskQueue();
        }

        public async void ProcessTaskQueue()
        {
            if (isProcessing)
                return;

            isProcessing = true;

            while (taskQueue.Count > 0)
            {
                var taskGenerator = taskQueue.Dequeue();
                try
                {
                    var task = taskGenerator.Invoke();
                    await task;
                }
                catch (Exception ex)
                {
                    // Handle or log exception
                    Debug.LogError($"Task failed: {ex.Message}");
                }
            }

            isProcessing = false;
        }
    }
}