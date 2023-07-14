# Task Programming Content
[1.Tasks](#Tasks)\
[2.Cancellation](#Cancellation)
[3.Waiting for Time to Pass](#waiting-for-time-to-pass)
[4.Waiting for Tasks](#waiting-for-tasks)
[5.Exception Handling](#exception-handling)

## 1. Tasks
### 1. Task is a unit of work that takes a function
* new Task(function), t.Start()
* Task.Factory.StartNew(function)

### 2. Tasks can be passed an object
### 3. Tasks can return values
* new Task<T>, task.Result

### 4. Tasks can report their state
* task.IsCompleted, task.IsFaulted, etc.

## 2. Cancellation
### 1. Cancellation of tasks is supported via
* CancellationTokenSource, which returns a
* CanncellationToken token = cts.Token

### 2. The token is passed into the function
* E.g., Task.Factory.StartNew(..., token)

### 3. To cancel, we call cts.Cancel()

### 4. cancellation is cooperative
* Task can check token.IsCancellationRequested and 'soft fail' or
* Throw an exception via token. ThrowIfCancellationRequested()

## 3. Waiting for Time to Pass
### 1. Thread.Sleep(msec)
### 2. token.WaitHandle.WaitOne(msec)
* Returns a bool indicating whether cancellation was requested in the time period specified
### 3. Thread.SpinWait()
* SpiNWait.SpinUntil(function)
* Spin waiting does not give up the thread's turn

## 4. Waiting for Tasks
### 1. Waiting for single task
* task.Wait(optional timeout)
### 2. Waiting for several tasks
* task.WaitAll(t, t2)
* task.WaitAny(t, t2)
### 3. WaitAny/WaitAll will throw on cancellation

## 5. Exception Handling
### 1. An unobserved task exception will not get handled
### 2. task.Wait() or Task.WaitAny()/WaitAll() will catch an...
* AggregateException
* Use ae.InnerExceptions to iterate all exceptions caught
* Use ae.Handle(e => {...}) to selectively handle exceptions
** Return true if handled, false otherwise
### 3. Note: there are ways of handling unobserved exceptions
* They are tricky and unreliable
