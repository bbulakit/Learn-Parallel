# Task Programming Content
[1.Tasks](#1-tasks)\
[2.Cancellation](#2-cancellation)\
[3.Waiting for Time to Pass](#3-waiting-for-time-to-pass)\
[4.Waiting for Tasks](#4-waiting-for-tasks)\
[5.Exception Handling](#5-exception-handling)

## 1. Tasks
### a. Task is a unit of work that takes a function
* new Task(function), t.Start()
* Task.Factory.StartNew(function)

### b. Tasks can be passed an object
### c. Tasks can return values
* new Task<T>, task.Result

### d. Tasks can report their state
* task.IsCompleted, task.IsFaulted, etc.

## 2. Cancellation
### a. Cancellation of tasks is supported via
* CancellationTokenSource, which returns a
* CanncellationToken token = cts.Token

### b. The token is passed into the function
* E.g., Task.Factory.StartNew(..., token)

### c. To cancel, we call cts.Cancel()

### d. cancellation is cooperative
* Task can check token.IsCancellationRequested and 'soft fail' or
* Throw an exception via token. ThrowIfCancellationRequested()

## 3. Waiting for Time to Pass
### a. Thread.Sleep(msec)
### b. token.WaitHandle.WaitOne(msec)
* Returns a bool indicating whether cancellation was requested in the time period specified
### c. Thread.SpinWait()
* SpiNWait.SpinUntil(function)
* Spin waiting does not give up the thread's turn

## 4. Waiting for Tasks
### a. Waiting for single task
* task.Wait(optional timeout)
### b. Waiting for several tasks
* task.WaitAll(t, t2)
* task.WaitAny(t, t2)
### c. WaitAny/WaitAll will throw on cancellation

## 5. Exception Handling
### a. An unobserved task exception will not get handled
### b. task.Wait() or Task.WaitAny()/WaitAll() will catch an...
* AggregateException
* Use ae.InnerExceptions to iterate all exceptions caught
* Use ae.Handle(e => {...}) to selectively handle exceptions
** Return true if handled, false otherwise
### c. Note: there are ways of handling unobserved exceptions
* They are tricky and unreliable
