using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class TestDll : MonoBehaviour
{
    [DllImport("MathLibraryTest")]
    public static extern void fibonacci_init(ulong a, ulong b);

    [DllImport("MathLibraryTest")]
    public static extern ulong fibonacci_current();

    [DllImport("MathLibraryTest")]
    public static extern bool fibonacci_next();
    [DllImport("MathLibraryTest")]
    public static extern ulong fibonacci_index();

    // Start is called before the first frame update
    void Start()
    {
        // Initialize a Fibonacci relation sequence.
        fibonacci_init(1, 1);
        // Write out the sequence values until overflow.
        do
        {
            Debug.Log(fibonacci_index() + " : " + fibonacci_current());
        } while (fibonacci_next());
        // Report count of values written before overflow.
        Debug.Log(fibonacci_index() + 1 +
            " Fibonacci sequence values fit in an unsigned 64-bit integer.");
    }
}
