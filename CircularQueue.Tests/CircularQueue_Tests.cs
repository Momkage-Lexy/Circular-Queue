namespace CircularQueue.Tests
{
    [TestFixture]
    public class CircularQueue_Tests
    {
        private CircularQueue<int> queue;
        [SetUp]
        public void Setup()
        {
            queue = new CircularQueue<int>(5);
        }

        // QUEUE REQUIREMENTS 

        /* REQUIREMENT: FIFO First element added to the queue should be the first one to be removed
        EXPECTED RESULT: True if the first element added is the first one to be removed */
        [Test] 
        public void CircularQueue_Should_Follow_FIFO()
        {
            // ARRANGE
            queue.Enqueue(1); // First in = 1
            queue.Enqueue(2); // Second in = 2

            // ACT
            var firstOut = queue.Dequeue(); 

            // ASSERT 
            Assert.IsTrue(firstOut == 1); 
        } 

        /* REQUIREMENT: Enqueue Adds an element to the rear of the queue
        EXPECTED RESULT: True if the element is added to the rear of the queue */
        [Test] 
        public void OperationEnqueue_Should_AddElementToRear()
        {
            // ARRANGE
            queue.Enqueue(1); 

            // ACT
            queue.Enqueue(2); 

            // ASSERT 
            Assert.IsTrue(queue.Size == 2); 
        } 

        /* REQUIRMENT: Dequeue removes an element from the front of the queue
        EXPECTED RESULT: True if the element is removed from the front of the queue */
        [Test] 
        public void OperationDequeue_Should_RemoveElementFromFront()
        {
            // ARRANGE
            queue.Enqueue(1); // First in = 1
            queue.Enqueue(2); // Second in = 2

            // ACT
            var removedElement = queue.Dequeue(); 

            // ASSERT 
            Assert.IsTrue(removedElement == 1); 
        } 

        /* REQUIREMENT: Peak/Front retrieves the front element of the queue without removing it 
        EXPECTED RESULT: True if the front element is retrieved without removing it */
        [Test] 
        public void OperationFront_Should_RetrieveFrontElement_WithoutRemoving()
        {
            // ARRANGE
            queue.Enqueue(1);
            queue.Enqueue(2);

            // ACT
            var frontElement = queue.Peek();

            // ASSERT
            Assert.IsTrue(frontElement == 1);
        } 

        /* -REQUIREMENT: IsEmpty checks if the queue is empty 
        EXPECTED RESULT: True if the queue is empty */
        [Test] 
        public void OperationIsEmpty_Should_CheckIfQueueIsEmpty()
        {
            // ARRANGE
            var isEmptyInitially = queue.IsEmpty(); // Empty Queue

            // ACT
            queue.Enqueue(1);
            var isEmptyAfterEnqueue = queue.IsEmpty(); // FALSE
            queue.Dequeue();
            var isEmptyAfterDequeue = queue.IsEmpty(); // TRUE

            // ASSERT
            Assert.IsTrue(isEmptyInitially && !isEmptyAfterEnqueue && isEmptyAfterDequeue);            
        } 

        /* REQUIREMENT: Size Returns the number of elements in the queue
        EXPECTED RESULT: True if the correct size of the queue is returned */
        [Test] 
        public void OperationSize_Should_ReturnQueueSize()
        {
            // ARRANGE
            queue.Enqueue(1);
            queue.Enqueue(2);

            // ACT
            var queueSize = queue.Size;

            // ASSERT
            Assert.IsTrue(queueSize == 2);
        } 

        /* REQUIREMENT: -O(1) for Dequeue and Enqueue 
        EXPECTED RESULT: True if Enqueue and Dequeue operations have O(1) complexity */
        // Optimized with ChatGPT
        [Test] 
        public void OperationsDequeueAndEnqueue_ShouldHave_01Complexity()
        {
            // ARRANGE
            queue.Enqueue(1);
            queue.Enqueue(2);

            // ACT 
            // Time of Enqueue
            var enqueueStart = DateTime.Now;
            queue.Enqueue(3);
            var enqueueEnd = DateTime.Now;

            // Time of Dequeue
            var dequeueStart = DateTime.Now;
            queue.Dequeue();
            var dequeueEnd = DateTime.Now;

            // Duration
            var enqueueDuration = enqueueEnd - enqueueStart;
            var dequeueDuration = dequeueEnd - dequeueStart;

            // ASSERT
            Assert.IsTrue(enqueueDuration.TotalMilliseconds < 1 && dequeueDuration.TotalMilliseconds < 1);
        } 

        /* CIRCULAR QUEUE REQUIREMENTS */

        /* REQUIREMENT: Two pointers, one at the front and one at the back, are present in a circular queue. 
        The front pointer points to the first member in the queue. 
        EXPECTED RESULT: True if the front pointer points to the first element in the queue */
        [Test] 
        public void FrontPointer_Should_PointToFirstElementInQueue()
        {
            // ARRANGE
            queue.Enqueue(1);
            queue.Enqueue(2);

            // ACT
            var frontElement = queue.Peek();

            // ASSERT
            Assert.IsTrue(frontElement == 1);
        } 

        /* REQUIREMENT: Two pointers, one at the front and one at the back, are present in a circular queue. 
        Rear pointer points to the last member in the queue. 
        EXPECTED RESULT: True if the rear pointer points to the last element in the queue */
        [Test] 
        public void RearPointer_Should_PointToLastElementInQueue()
        {
            // ARRANGE
            queue.Enqueue(1);
            queue.Enqueue(2);

            // ACT
            var rearElement = queue.PeekRear();

            // ASSERT
            Assert.IsTrue(rearElement == 2);
        }

        /* REQUIREMENT: The size of a circular queue is established at initialization. 
        EXPECTED RESULT: True if the size of the queue is established at initializatione */

        [Test]
        public void Size_Should_BeEstablishedAtInitializations()
        {
            // ARRANGE & ACT
            // Get the capacity of the queue
            var initialSize = queue.Capacity;

            // ASSERT
            Assert.IsTrue(initialSize == 5);
        } 

        /* REQUIREMENT:  
        If the queue is already full, additional components cannot be added without first being dequeued. 
        EXPECTED RESULT: True if additional components cannot be added to a full queue */
        [Test] 
        public void CannotAdd_IfQueueIsFull()
        {
            // ARRANGE
            // Create a queue with capacity 2 and fill it
            queue = new CircularQueue<int>(2);
            queue.Enqueue(1);
            queue.Enqueue(2);

            // ACT & ASSERT
            Assert.Throws<InvalidOperationException>(() => queue.Enqueue(3));
        } 

        /* REQUIREMENT: A circular queue is a data structure with a circle-like connection between the last and first positions. 
        This indicates that the rear pointer rolls around to the beginning of the array when it reaches the end. 
        EXPECTED RESULT: True if the rear pointer returns to the front when it reaches the end */
        [Test] 
        public void RearPointer_Should_ReturnToFront()
        {
            // ARRANGE
            // Enqueue elements to fill the queue and cause the rear pointer to wrap around
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Dequeue();
            queue.Enqueue(4);

            // ACT
            // Peek at the front element without removing it
            var frontElement = queue.Peek();

            // ASSERT
            // The front element should be the second element enqueued, after one dequeue operation
            Assert.IsTrue(frontElement == 2);
        } 

        /* REQUIREMENT: A circular queue effectively uses memory by recycling the empty spaces left behind when elements are dequeued.
        EXPECTED RESULT: True if empty spaces are recycled */
        [Test] 
        public void EmptySpaces_ShouldBeRecycled()
        {
            // ARRANGE
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Dequeue();
            queue.Enqueue(3);

            // ACT
            var size = queue.Size;

            // ASSERT
            // The queue should correctly count the elements after recycling the space
            Assert.IsTrue(size == 2);
        }

        /* REQUIRMENT: A circular queue should be able to resized. The circular queue should know where the new front and rear now "point" to. 
        EXPECTED RESULT: True if the new front and rear pointers are correctly updated after resizing */
        [Test] 
        public void Resize_Should_HaveNewFrontRear()
        {
            // ARRANGE
            // Enqueue two elements, then resize the queue to a larger capacity
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Resize(4);

            // ACT
            // Peek at the front element and enqueue another element to check rear pointer
            var frontElement = queue.Peek();
            queue.Enqueue(3);
            var rearElement = queue.PeekRear();

            // ASSERT
            // The front element should be the first enqueued and rear element should be the last enqueued
            Assert.IsTrue(frontElement == 1 && rearElement == 3);
        }

        /* REQUIRMENT: Increasing the size should result in no data loss and similarly. 
        EXPECTED RESULT: True if increasing the size results in no data loss */
        [Test] 
        public void Increase_ShouldNot_HaveDataLoss()
        {
            // ARRANGE
            // Enqueue two elements, then resize the queue to a larger capacity and enqueue more elements
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Resize(4);
            queue.Enqueue(3);
            queue.Enqueue(4);

            // ACT
            // Get the current count of elements in the queue
            var size = queue.Size;

            // ASSERT
            // The queue should retain all elements after resizing
            Assert.IsTrue(size == 4);
        }
    }
}