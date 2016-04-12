 

 using System;
 using System.Drawing;
 using System.Collections;
 using System.ComponentModel;
 using System.Windows.Forms;
 using System.IO;
 using System.Runtime.CompilerServices;
 using System.Threading;

namespace FlightSimulator
{
    //[シリアライズ可能]
    public class FlightSimulator : Form
    {
        public FlightSimulator()
            : base()
        {
            //ダブルバッファリング設定
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);


            version = "Ver.1.1 2003.11.04";
            eh = new EventHandler();
            tp = new TimeParam();
            simIF = new SimulatorInterface();
            recorder = new Recorder();
            fps = new FPS();

        }

        internal readonly String version;
        // internal ThreadWrapper thread;
        internal Image doubleBuffer;
        internal Graphics myGC;
        internal Uri codeBase;
        internal Image LocusImage;
        internal Graphics locusGC;
        internal EventHandler eh;

        static internal double sX0 = 0.0D;
        static internal double sY0 = 0.0D;
        static internal double sX1 = 600.0D;
        static internal double sY1 = 400.0D;

        internal TimeParam tp;

        internal SimulatorInterface simIF;
        internal GraphicsGenerator gd;
        internal LocusViewer locusVierer;
        internal ConfigMenu configMenu;
        internal AirPlaneList apl;
        internal AirPlane ap;
        internal Recorder recorder;
        //FPS計測
        private FPS fps;

        public String GetAppletInfo()
        {
            return "";
        }

        ////public void Main(String[] args)
        ////{
        ////    FlightSimulator applet = new FlightSimulator();
        ////    // Frame frame = new Frame("Applet");

        ////    // frame.AddWindowListener(applet);
        ////    // frame.Add("Center", applet);
        ////    //  frame.SetSize((int)sX1, (int)sY1);
        ////    //  frame.Show();

        ////    applet.Init();
        ////    //applet.Start();
        ////}

        public void Init()
        {
            //base.Init();
            //AddKeyListener(this);
            //AddMouseListener(this);
            //AddMouseMotionListener(this);

            // codeBase = GetCodeBase();
            gd = new GraphicsGenerator(codeBase, sX0, sY0, sX1, sY1);

            //doubleBuffer = CreateImage((int)gd.proj.screenXSize, (int)gd.proj.screenYSize);
            //myGC = doubleBuffer.GetGraphics();
            //LocusImage = CreateImage((int)gd.proj.screenXSize, (int)gd.proj.screenYSize);
            //locusGC = LocusImage.GetGraphics();

            Jp.Maker1.Util.Gui.Caption.Initial(270, 180, GraphicsGenerator.CAPTION_COLOR, GraphicsGenerator.CAPTION_FONT);

            apl = new AirPlaneList(codeBase, "data/airplane_list.txt");
            apl.Print();

            ap = new AirPlane(codeBase, apl.GetPath(0));

            ap.Set_pMotionLand();

            configMenu = new ConfigMenu(sX0, sY0, sX1, sY1, apl);

            InitPadlockList();

            tp.SetTSTART();
        }

        //public virtual void KeyPressed(KeyEvent e)
        //{
        //    int keyCode = e.GetKeyCode();
        //    eh.KeyPressed(keyCode, ap, simIF, recorder,
        //            ap.pilot.pObjList);
        //}

        //キー入力押下処理
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            eh.KeyPressed(e.KeyCode, ap, simIF, recorder, ap.pilot.pObjList);
        }

        //public virtual void KeyReleased(KeyEvent e)
        //{
        //    int keyCode = e.GetKeyCode();
        //    eh.KeyReleased(keyCode, ap, simIF);
        //}

        //public virtual void KeyTyped(KeyEvent e)
        //{
        //}

        //public virtual void MouseClicked(MouseEvent e)
        //{
        //    int x = e.GetX();
        //    int y = e.GetY();
        //    eh.MouseClicked(x, y, ap, simIF, gd,
        //            configMenu);
        //}

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            int x = e.X;
            int y = e.Y;
            eh.MouseClicked(x, y, ap, simIF, gd, configMenu);
        }


        //public virtual void MouseMoved(MouseEvent e)
        //{
        //    int x = e.GetX();
        //    int y = e.GetY();
        //    eh.MouseMoved(x, y, ap, simIF, gd);
        //}

        protected override void OnMouseMove(MouseEventArgs e)
        {
            int x = e.X;
            int y = e.Y;
            eh.MouseMoved(x, y, ap, simIF, gd);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            myGC = e.Graphics;
            Update(e.Graphics);
        }

        //public virtual void Run()
        //{
        //    //while (thread == ILOG.J2CsMapping.Threading.ThreadWrapper.CurrentThread)
        //    {
        //        //Repaint();
        //        try
        //        {
        //            //Sleep(tp.ThreadPeriodMsec());
        //        }
        //        catch (ThreadInterruptedException localInterruptedException)
        //        {
        //        }
        //    }
        //}

        //public override void Start()
        //{
        //    if (thread == null)
        //    {
        //        thread = new ThreadWrapper(this);
        //        thread.Start();
        //    }
        //}

        //public override void Stop()
        //{
        //    thread = null;
        //}

        public void Update(Graphics g)
        {
            if (simIF.changePlane == 1)
            {
                //g.DrawString("Now Loading...", 200, 250);
                System.Console.Out.WriteLine("################# 飛行機変更 ############################################");
                System.Console.Out.WriteLine("飛行機番号=" + configMenu.selectedAirPlane);
                System.Console.Out.WriteLine("CODEBASE=" + codeBase);
                System.Console.Out.WriteLine("PATH=" + configMenu.apl.GetPath(configMenu.selectedAirPlane));
                simIF.changePlane = 0;
                ap = new AirPlane(codeBase, configMenu.apl.GetPath(configMenu.selectedAirPlane));
                ap.Set_pMotionLand();
                InitPadlockList();
            }

            if (simIF.changePSetting == 1)
            {
                configMenu.SetPSetting(tp);
                simIF.changePSetting = 0;
            }

            if (simIF.pause_sw == 0)
            {
                simIF.counter -= 1;
                if (simIF.counter <= 0)
                {
                    for (int i = 0; i < tp.t_disp; i++)
                    {
                        ap.Update(tp.dt);
                        if (ap.crash_point != -1)
                        {
                            Jp.Maker1.Util.Gui.Caption.SetCaption("POINT " + ap.crash_point + " TOUCH THE GROUND.", 4.0D);
                        }

                        tp.simT += tp.dt;
                        tp.rec_counter += 1;
                        if (tp.rec_counter >= tp.t_record)
                        {
                            recorder.Record(tp.simT, tp.dt, ap);
                            tp.rec_counter = 0;
                        }
                    }
                    if (simIF.slow_mode == 1)
                        simIF.counter = simIF.slow_count;
                    else
                    {
                        simIF.counter = 0;
                    }
                }

            }

            if (simIF.mode == 0)
            {
                gd.CreateImage(myGC, ap, simIF, tp.dt);
                //myGC.SetColor(Java.Awt.Color.black);
                myGC.DrawRectangle(Pens.Black, (int)sX0, (int)sY0, (int)(sX1 - sX0 - 1.0D), (int)(sY1 - sY0 - 1.0D));
            }
            else if (simIF.mode == 1)
            {
                if (simIF.locusMake == 1)
                {
                    //g.SetColor(Java.Awt.Color.white);
                    //g.FillRect((int)sX0, (int)sY0, (int)(sX1 - sX0 + 1.0D), (int)(sY1 - sY0 + 1.0D));
                    //g.SetColor(Java.Awt.Color.black);
                    //g.DrawString("Now Drawing...", 280, 150);
                    locusVierer = new LocusViewer(sX0, sY0, sX1, sY1, ap, recorder);
                    simIF.locusMake = 0;
                }
                locusVierer.UpdateDispParam(simIF, tp.ThreadPeriod());
                if (simIF.locusImageMake == 1)
                {
                    //g.SetColor(Java.Awt.Color.white);
                    //g.FillRect((int)sX0, (int)sY0, (int)(sX1 - sX0 + 1.0D), (int)(sY1 - sY0 + 1.0D));
                    //g.SetColor(Java.Awt.Color.black);
                    //g.DrawString("Now Drawing...", 280, 150);
                    locusVierer.DrawLocus(locusGC, 1);
                    // myGC.DrawImage(LocusImage, 0, 0, this);
                    simIF.locusImageMake = 0;
                }
                //myGC.DrawImage(LocusImage, 0, 0, this);
                if (simIF.Check_locus_action_sw() > 0)
                {
                    // myGC.DrawImage(LocusImage, 0, 0, this);
                    locusVierer.DrawLocus(myGC, 0);
                }
                locusVierer.DrawData(myGC, simIF);
                // myGC.SetColor(Java.Awt.Color.black);
                //myGC.DrawRect((int)sX0, (int)sY0, (int)(sX1 - sX0 - 1.0D), (int)(sY1 - sY0 - 1.0D));
            }
            else if (simIF.mode == 2)
            {
                configMenu.Draw(myGC, "Ver.1.1 2003.11.04");
            }

            tp.tsim_dt += tp.ThreadPeriod();
            long t_end = (DateTime.Now.Ticks / 10000);

            if (simIF.pause_sw == 0)
            {
                //myGC.SetColor(gd.informationColor);
                myGC.DrawString("ΔT:" + DispFormat.DoubleFormat(tp.dt * 1000.0D, 3, 0), gd.stdFont, gd.interfaceColorBrush, 2, 11);
                //myGC.DrawString("T:" + Jp.Maker1.Sim.Tools.Tool.TimeStr3(tp.simT), 2, 22);
                //myGC.DrawString("T-ΣΔT:" + Jp.Maker1.Util.DispFormat.DoubleFormat((t_end - tp.tStart) / 1000.0D - tp.tsim_dt, 3, 3), 2, 33);
                //myGC.DrawString("TGT.FR:" + Jp.Maker1.Util.DispFormat.DoubleFormat(1.0D / tp.ThreadPeriod(), 3, 1), 2, 44);
            }

            Jp.Maker1.Util.Gui.Caption.Draw(myGC);
            Jp.Maker1.Util.Gui.Caption.Update(tp.ThreadPeriod());

            //g.DrawImage(doubleBuffer, 0, 0, this);

            if (simIF.record_output_sw == 1)
            {
                simIF.record_output_sw = 0;
                recorder.Print("Ver.1.1 2003.11.04");
            }
        }

        //public virtual void WindowActivated(WindowEvent e)
        //{
        //}

        //public virtual void WindowClosed(WindowEvent e)
        //{
        //}

        //public virtual void WindowClosing(WindowEvent e)
        //{
        //    Stop();
        //    Environment.Exit(0);
        //}

        //public virtual void WindowDeactivated(WindowEvent e)
        //{
        //}

        //public virtual void WindowDeiconified(WindowEvent e)
        //{
        //}

        //public virtual void WindowIconified(WindowEvent e)
        //{
        //}

        //public virtual void WindowOpened(WindowEvent e)
        //{
        //}

        private void InitPadlockList()
        {
            ap.pilot.pObjList.AddObject("RUNWAY", new Vector3D(0.0D, 0.0D, 0.0D));
        }

        //ゲームループのスレッド
        public void Run()
        {
            Show();
            Init();
            fps.init();
            while (Created)
            {
                if (fps.isJustTime())
                {
                    //再描画
                    Invalidate();


                    fps.WaitAndCalc();
                }
                //    //他の処理を任せる
                Application.DoEvents();
            }
        }

        //プログラムエントリポイント
        [STAThread]
        public static void Main(String[] args)
        {
            FlightSimulator fsim = new FlightSimulator();
            Thread t = new Thread(new ThreadStart(fsim.Run));
            t.Start();
            t.Join();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FlightSimulator
            // 
            this.ClientSize = new System.Drawing.Size(473, 470);
            this.Name = "FlightSimulator";
            this.ResumeLayout(false);

        }
    }
}