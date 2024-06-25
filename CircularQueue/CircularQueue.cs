namespace CircularQueue
{
    public class CircularQueue<T>
    {
        // Initialization Optimized by ChatGPT
        private T[] _queue; // Array to hold the elements of the queue
        // Initialization Optimized by ChatGPT
        private int _front; // Index of the front element
        // Initialization Optimized by ChatGPT
        private int _rear; // Index of the rear element
        // Initialization Optimized by ChatGPT
        private int _count; // Number of elements in the queue

        // Constructor to initialize the circular queue with a specific size
        // Initialization Optimized by ChatGPT
        public CircularQueue(int size)
        {
            _queue = new T[size]; // Initialize the array with the given size
            _front = 0; // Set the front index to 0
            _rear = -1; // Set the rear index to -1 (indicating the queue is empty)
            _count = 0; // Initialize the count of elements to 0
        }

        /* ------------------ Size ------------------ */

        // Property to get the current number of elements in the queue
        public int Size => _count;

        /* ------------------ Capacity ------------------ */

        // Property to get the capacity of the queue (size of the array)
        public int Capacity => _queue.Length;

        /* ------------------ Enqueue ------------------ */

        // Method to add an element to the rear of the queue
        public void Enqueue(T item)
        {
            if (_count == Capacity)
            {
                // If the queue is full, throw an exception
                throw new InvalidOperationException("Queue is full");
            }

            // Calculate the new rear index using modulo to wrap around
            _rear = (_rear + 1) % Capacity;
            _queue[_rear] = item; // Add the element to the rear of the queue
            _count++; // Increment the count of elements
        }

        /* ------------------ Dequeue ------------------ */

        // Method to remove and return the front element from the queue
        public T Dequeue()
        {
            if (_count == 0)
            {
                // If the queue is empty, throw an exception
                throw new InvalidOperationException("Queue is empty");
            }

            var item = _queue[_front]; // Get the front element
            _queue[_front] = default(T); // Clear the front element (optional)
            _front = (_front + 1) % Capacity; // Calculate the new front index using modulo to wrap around
            _count--; // Decrement the count of elements
            return item; // Return the front element
        }

        /* ------------------ Peek ------------------ */

        // Method to return the front element without removing it
        public T Peek()
        {
            if (_count == 0)
            {
                // If the queue is empty, throw an exception
                throw new InvalidOperationException("Queue is empty");
            }

            return _queue[_front]; // Return the front element
        }

        /* ------------------ Peek Rear ------------------ */

        // Method to return the rear element without removing it
        public T PeekRear()
        {
            if (_count == 0)
            {
                // If the queue is empty, throw an exception
                throw new InvalidOperationException("Queue is empty");
            }

            return _queue[_rear]; // Return the rear element
        }

        /* ------------------ IsEmpty ------------------ */

        // Method to check if the queue is empty
        public bool IsEmpty()
        {
            return _count == 0; // Return true if the count of elements is 0
        }

        /* ------------------ Resize ------------------ */

        // Method to resize the queue to a new size
        public void Resize(int newSize)
        {
            if (newSize < _count)
            {
                // If the new size is less than the current number of elements, throw an exception
                throw new InvalidOperationException("New size must be greater than current number of elements.");
            }

            // Create a new array with the new size
            var newQueue = new T[newSize];
            for (int i = 0; i < _count; i++)
            {
                // Copy elements from the old array to the new array, handling wrap around
                newQueue[i] = _queue[(_front + i) % Capacity];
            }

            _queue = newQueue; // Replace the old array with the new array
            _front = 0; // Reset the front index to 0
            _rear = _count - 1; // Reset the rear index to the last element

        }
    }
}
