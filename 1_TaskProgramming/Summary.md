# Task Programming Content
[1.Tasks](#Tasks)\
[2.Cancellation](#Cancellation)


## Tasks
### Task is a unit of work that takes a function
* new Task(function), t.Start()
* Task.Factory.StartNew(function)

### Tasks can be passed an object
### Tasks can return values
* new Task<T>, task.Result

### Tasks can report their state
* task.IsCompleted, task.IsFaulted, etc.

## Cancellation
### Cancellation of tasks is supported via
* CancellationTokenSource, which returns a
* CanncellationToken token = cts.Token

### The token is passed into the function
* E.g., Task.Factory.StartNew(..., token)

### To cancel, we call cts.Cancel()

### cancellation is cooperative
* Task can check token.IsCancellationRequested and 'soft fail' or
* Throw an exception via token. ThrowIfCancellationRequested()

## Waiting for Time to Pass
### Thread.Sleep(msec)
### token.WaitHandle.WaitOne(msec)
* Returns a bool indicating whether cancellation was requested in the time period specified
### Thread.SpinWait()
* SpiNWait.SpinUntil(function)
* Spin waiting does not give up the thread's turn

## Waiting for Tasks
### Waiting for single task
* task.Wait(optional timeout)
### Waiting for several tasks
* task.WaitAll(t, t2)
* task.WaitAny(t, t2)
### WaitAny/WaitAll will throw on cancellation

## ExceptionHandling
### An unobserved task exception will not get handled
### task.Wait() or Task.WaitAny()/WaitAll() will catch an...
* AggregateException
* Use ae.InnerExceptions to iterate all exceptions caught
* Use ae.Handle(e => {...}) to selectively handle exceptions
** Return true if handled, false otherwise
### Note: there are ways of handling unobserved exceptions
* They are tricky and unreliable
