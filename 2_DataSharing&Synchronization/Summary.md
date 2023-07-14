# Data Sharing & Synchronization Content
[1.Critical Sections](#1-critical-sections)\
[2.Interlocked Operations](#2-interlocked-operations)\
[3.Spin Locking and Lock Recursion](#3-spin-locking-and-lock-recursion)\
[4.Mutex](#4-mutex)\
[5.Reader-Writer Locks](#5-reader-writer-locks)

## 1. Critical Sections
### a. Uses the *lock* keyword
### b. Typically locks on an existing object
* Best to make a new *object* to lock on
### c. A shorthand for Monitor.Enter()/Exit()
### d. Blocks until a lock is available
* Unless you use Monitor.TryEnter() with a timeout value.

## 2. Interlocked Operations
### a. Useful for atomically chaning low-level primitives
* *Interlock.Increment()/Decrement()*
* *Interlock.Add()*
* *Interlock.Exchange()/CompareExchange()*

## 3. Spin Locking and Lock Recursion
### a. A spin lock wastes CPU cycles without yielding
* Useful for brief pauses to prevent rescheduling
### b. *Enter()* to take, *Exit()* to release (if taken successfully)
### c. Lock Recursion = ability to enter a lock twice on the same thread
* **Not recommended** using SpinLock for lock recursion
### d. SpinLock doesn't support lock recursion
### e. Owner tracking helps keep a record of thread that acquired the lock
* Recursion with tracking = exception, without = **deadlock**

## 4. Mutex*
### a. A *WaitHandle*-derived synchronization primitive
### b. *WaitOne()* to acquire
* Possibly with a timeout
### c. *ReleaseMutex()* to release
### d. *Mutex.WaitAll()* to acquire several
### e. Global/named mutexes are **shared between processes**
* *Mutex.OpenExisting()* to acquire
* *mutex = new Mutex(false, <name> )*

## 5. Reader-Writer Locks
### a. A reader-writer lock can lock for reading or writing
* *(Enter/Exit) (Read/Write) Lock()*
### b. Supports lock recursion in ctor parameter
* **Not recommended**
### c. Supports upgradeability
* **Enter/ExitUpgradeableReadLock()*
* **Enter/ExitUpgradeableWriteLock()*

