# Task Programming Content
1.[Tasks](#Tasks)
2.[Cancellation)(#Cancellation)
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
