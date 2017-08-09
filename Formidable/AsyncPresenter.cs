using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Formidable
{
    public class AsyncPresenter
    {
        private static object threadLocker = new object();
        private static AsyncPresenter instance = null;
        private TaskFactory taskFactory = new TaskFactory(TaskCreationOptions.PreferFairness, TaskContinuationOptions.None);

        /// <summary>
        /// Runs Specified Operation and Subsequent Callback in a new Task
        /// </summary>
        /// <param name="Operation"></param>
        /// <param name="Callback"></param>
        /// <returns></returns>
        public Task WithNewTask(Action Operation, Action Callback = default(Action), Action<Exception> FailureCallback = default(Action<Exception>))
        {
            if (Callback != default(Action))
            {
                return this.taskFactory.StartNew(Operation).ContinueWith((previous) =>
                {
                    if (previous.Exception != null)
                    {
                        if (FailureCallback != default(Action<Exception>))
                        {
                            FailureCallback(previous.Exception);
                        }
                        else
                        {
                            throw previous.Exception;
                        }
                    }
                    else
                    {
                        Callback();
                    }
                });
            }
            else
            {
                return this.taskFactory.StartNew(Operation);
            }
        }

        /// <summary>
        /// Executes Operation after all provided Tasks are completed
        /// </summary>
        /// <param name="Tasks"></param>
        /// <param name="Operation"></param>
        /// <returns></returns>
        public Task ContinueAfter(Task[] Tasks, Action<Task[]> Operation, Action<Exception> FailureCallback = default(Action<Exception>))
        {
            if (Tasks != null && Tasks.Count() > 0)
            {
                return this.taskFactory.ContinueWhenAll(Tasks, (t) => {
                    bool failure = false;
                    foreach (Task task in t)
                    {
                        if (task.Exception != null)
                        {
                            failure = true;
                            if (FailureCallback != default(Action<Exception>))
                            {
                                FailureCallback(task.Exception);
                            }
                            else
                            {
                                throw task.Exception;
                            }
                            break;
                        }
                    }

                    if (!failure)
                    {
                        Operation(t);
                    }

                });
            }
            else
            {
                throw new Exception("No Tasks were provided.");
            }
        }

        /// <summary>
        /// Executes UI Operation using the specified Control (cross-threads)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Control"></param>
        /// <param name="UIOperation"></param>
        [DebuggerStepThrough]
        public void WithControl<T>(T Control, Action<T> UIOperation, bool Async = false) where T : Control
        {
            MethodInvoker method = (MethodInvoker)(() =>
            {
                UIOperation(Control);
            });

            if (Control.InvokeRequired)
            {
                if (!Async)
                {
                    Control.Invoke(method);
                }
                else
                {
                    Control.BeginInvoke(method);
                }
            }
            else
            {
                if (!Control.IsDisposed)
                {
                    UIOperation(Control);
                }
            }
        }

        public static AsyncPresenter Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (threadLocker)
                    {
                        instance = new AsyncPresenter();
                    }
                }

                return instance;
            }
        }
    }
}
