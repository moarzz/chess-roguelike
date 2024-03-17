using UnityEngine;
using Stockfish.NET;
using System.Diagnostics;
using System;
using System.IO;
using UnityEditor;
using System.Linq;
using System.Text;

public class TestDll : MonoBehaviour
{
    IStockfish stockfish;


    void Start() 
    {
        /*stockfish = new Stockfish.NET.Core.Stockfish(@"C:\Users\xavie\Downloads\stockfish-windows-x86-64-avx2\stockfish\stockfish-windows-x86-64-avx2");
        var bestMove = stockfish.GetBestMove();
        print(bestMove);*/
        //Process process = Process.Start(@"C:\Users\xavie\Downloads\stockfish-windows-x86-64-avx2\stockfish\stockfish-windows-x86-64-avx2");

        ProcessStartInfo StartInfo = new ProcessStartInfo("stockfish-windows-x86-64-avx2.exe"); //starts stockfish
        //StartInfo.CreateNoWindow = false;
        StartInfo.RedirectStandardInput = true;
        StartInfo.RedirectStandardOutput = true;
        StartInfo.UseShellExecute = false; //required to redirect

        Process process = Process.Start(StartInfo);
        //process.StartInfo = StartInfo;
        //process.Start();

        StreamReader SR = process.StandardOutput;
        StreamWriter SW = process.StandardInput;
        SW.WriteLine("@echo on");
        SW.WriteLine("I did a poopi");

        //StreamWriter sw = process.StandardInput;
        // StreamReader sr = process.StandardOutput;
        //sw.WriteLine("go");
        //sw.Close();
    }
}
