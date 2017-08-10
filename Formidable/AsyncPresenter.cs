using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Formidable
{
    public class AsyncPresenter
    {
        private readonly TaskFactory _taskFactory;

        public AsyncPresenter()
        {
            this._taskFactory = new TaskFactory(TaskCreationOptions.PreferFairness, TaskContinuationOptions.None);
        }

        /// <summary>
        /// Runs Specified Operation and Subsequent Callback in a new Task
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public Task WithNewTask(Action operation, Action callback = default(Action), Action<Exception> failureCallback = default(Action<Exception>))
        {
            if (callback != default(Action))
            {
                return this._taskFactory.StartNew(operation).ContinueWith((previous) =>
                {
                    if (previous.Exception != null)
                    {
                        if (failureCallback != default(Action<Exception>))
                        {
                            failureCallback(previous.Exception);
                        }
                        else
                        {
                            throw previous.Exception;
                        }
                    }
                    else
                    {
                        callback();
                    }
                });
            }
            else
            {
                return this._taskFactory.StartNew(operation);
            }
        }

        /// <summary>
        /// Executes Operation after all provided Tasks are completed
        /// </summary>
        /// <param name="tasks"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        public Task ContinueAfter(Task[] tasks, Action<Task[]> operation, Action<Exception> failureCallback = default(Action<Exception>))
        {
            if (tasks != null && tasks.Any())
            {
                return this._taskFactory.ContinueWhenAll(tasks, (t) => {
                    bool _failure = false;
                    foreach (Task _task in t)
                    {
                        if (_task.Exception != null)
                        {
                            _failure = true;
                            if (failureCallback != default(Action<Exception>))
                            {
                                failureCallback(_task.Exception);
                            }
                            else
                            {
                                throw _task.Exception;
                            }
                            break;
                        }
                    }

                    if (!_failure)
                    {
                        operation(t);
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
        /// <param name="control"></param>
        /// <param name="uiOperation"></param>
        [DebuggerStepThrough]
        public void WithControl<T>(T control, Action<T> uiOperation, bool async = false) where T : Control
        {
            MethodInvoker _method = (MethodInvoker)(() =>
            {
                uiOperation(control);
            });

            if (control.InvokeRequired)
            {
                if (!async)
                {
                    control.Invoke(_method);
                }
                else
                {
                    control.BeginInvoke(_method);
                }
            }
            else
            {
                if (!control.IsDisposed)
                {
                    uiOperation(control);
                }
            }
        }
    }
}
