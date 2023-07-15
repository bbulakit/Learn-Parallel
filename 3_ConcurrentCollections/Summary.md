# Concurrent Collection Summary
### Concurrent collections use *TryXxx()* methods
> Return a *bool* indicating success
### Optimized for multithreaded use
> Some ops (E.g., *Count*) can block and make collection slow
### *ConCurrentBag/Queue/Stack* all implement *IProducerConsumerCollection*
### A *BlockingCollection* is a warpper around one of the *IProducerConsumerCollection* classes
> Provides blocking and bounding capabilities
### Ordering
> Queue = *FIFO*
> Stack = *LIFO*
> Bag = *Unordered*
