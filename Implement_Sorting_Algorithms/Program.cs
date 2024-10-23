using System.Diagnostics;

Stopwatch stopwatch = Stopwatch.StartNew();

// usage 100 thousand values
int values = 100000;
Console.WriteLine("Generate array of " + values.ToString("#,###") + " values");
stopwatch.Start();
int[] largeArr = GenerateRandomArray(values, 1, 1000);
stopwatch.Stop();
DisplayRuntime(stopwatch);
int[] tempArr;

// Write your function to test each algorithm here

//bubble sort is slow because each value has to be moved through every
//future value one at a time and is reapeated for each value in the array
tempArr = CopyBaseArr(largeArr);
stopwatch.Restart();
bubbleSort(tempArr, tempArr.Length);
Console.Write("\nAlgorithm: Bubble ");
DisplayRuntime(stopwatch);

//insertion sort is faster than bubble sort because each value in the array is only being moved 1 time,
//but still has to shift values over when something is inserted
tempArr = CopyBaseArr(largeArr);
stopwatch.Restart();
insertionSort(tempArr);
Console.Write("\nAlgorithm: Insertion ");
DisplayRuntime(stopwatch);

//merge sort divides the array into smaller arrays until they can be compared 1 by 1,
//but it does not have to rearange the values within the array
//because the array was partitioned into small arrays that are reorganized before reassembling
tempArr = CopyBaseArr(largeArr);
stopwatch.Restart();
mergeSort(tempArr, 0, tempArr.Length - 1);
Console.Write("\nAlgorithm: Merge ");
DisplayRuntime(stopwatch);

//quick sort works like the insertion sort by moving values to the side of a comparison value,
//but does it more efficiently because it is dividing the question on placement into smaller
//and smaller arrays similar to the way that merge sort gets its efficiency
tempArr = CopyBaseArr(largeArr);
stopwatch.Restart();
QuickSort(tempArr, 0, tempArr.Length - 1);
Console.Write("\nAlgorithm: Quick ");
DisplayRuntime(stopwatch);


// Write individual functions for each algorithm here (Bubble, Insertion, Merge, and Quick sort)
static void bubbleSort(int[] arr, int n)
{
    int i, j, temp;
    bool swapped;
    for (i = 0; i < n - 1; i++)
    {
        swapped = false;
        for (j = 0; j < n - i - 1; j++)
        {
            if (arr[j] > arr[j + 1])
            {

                // Swap arr[j] and arr[j+1]
                temp = arr[j];
                arr[j] = arr[j + 1];
                arr[j + 1] = temp;
                swapped = true;
            }
        }

        // If no two elements were
        // swapped by inner loop, then break
        if (swapped == false)
            break;
    }
}
void insertionSort(int[] arr)
{
    int n = arr.Length;
    for (int i = 1; i < n; ++i)
    {
        int key = arr[i];
        int j = i - 1;

        /* Move elements of arr[0..i-1], that are
           greater than key, to one position ahead
           of their current position */
        while (j >= 0 && arr[j] > key)
        {
            arr[j + 1] = arr[j];
            j = j - 1;
        }
        arr[j + 1] = key;
    }
}
static void merge(int[] arr, int l, int m, int r)
{
    // Find sizes of two
    // subarrays to be merged
    int n1 = m - l + 1;
    int n2 = r - m;

    // Create temp arrays
    int[] L = new int[n1];
    int[] R = new int[n2];
    int i, j;

    // Copy data to temp arrays
    for (i = 0; i < n1; ++i)
        L[i] = arr[l + i];
    for (j = 0; j < n2; ++j)
        R[j] = arr[m + 1 + j];

    // Merge the temp arrays

    // Initial indexes of first
    // and second subarrays
    i = 0;
    j = 0;

    // Initial index of merged
    // subarray array
    int k = l;
    while (i < n1 && j < n2)
    {
        if (L[i] <= R[j])
        {
            arr[k] = L[i];
            i++;
        }
        else
        {
            arr[k] = R[j];
            j++;
        }
        k++;
    }

    // Copy remaining elements
    // of L[] if any
    while (i < n1)
    {
        arr[k] = L[i];
        i++;
        k++;
    }

    // Copy remaining elements
    // of R[] if any
    while (j < n2)
    {
        arr[k] = R[j];
        j++;
        k++;
    }
}

// Main function that
// sorts arr[l..r] using
// merge()
static void mergeSort(int[] arr, int l, int r)
{
    if (l < r)
    {

        // Find the middle point
        int m = l + (r - l) / 2;

        // Sort first and second halves
        mergeSort(arr, l, m);
        mergeSort(arr, m + 1, r);

        // Merge the sorted halves
        merge(arr, l, m, r);
    }
}
static int Partition(int[] arr, int low, int high)
{

    // Choose the pivot
    int pivot = arr[high];

    // Index of smaller element and indicates 
    // the right position of pivot found so far
    int i = low - 1;

    // Traverse arr[low..high] and move all smaller
    // elements to the left side. Elements from low to 
    // i are smaller after every iteration
    for (int j = low; j <= high - 1; j++)
    {
        if (arr[j] < pivot)
        {
            i++;
            Swap(arr, i, j);
        }
    }

    // Move pivot after smaller elements and
    // return its position
    Swap(arr, i + 1, high);
    return i + 1;
}

// Swap function
static void Swap(int[] arr, int i, int j)
{
    int temp = arr[i];
    arr[i] = arr[j];
    arr[j] = temp;
}

// The QuickSort function implementation
static void QuickSort(int[] arr, int low, int high)
{
    if (low < high)
    {

        // pi is the partition return index of pivot
        int pi = Partition(arr, low, high);

        // Recursion calls for smaller elements
        // and greater or equals elements
        QuickSort(arr, low, pi - 1);
        QuickSort(arr, pi + 1, high);
    }
}


// function
static int[] GenerateRandomArray(int length, int minValue, int maxValue)
{
    Random rand = new Random();
    int[] array = new int[length];

    for (int i = 0; i < length; i++)
    {
        array[i] = rand.Next(minValue, maxValue); // Generates a random integer within the specified range
    }

    return array;
}

static void DisplayRuntime(Stopwatch stopwatch)
{
    TimeSpan ts = stopwatch.Elapsed;

    // Format and display the TimeSpan value.
    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
        ts.Hours, ts.Minutes, ts.Seconds,
        ts.Milliseconds / 10);
    Console.WriteLine("Time Taken: " + elapsedTime);
}

//I made this so that each sorting algorithm is sorting through the same array
static int[] CopyBaseArr(int[] Base)
{
    return (int[])Base.Clone();
}