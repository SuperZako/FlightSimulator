// --------------------------------------------------------------------------------------------------
// This file was automatically generated by J2CS Translator (http://j2cstranslator.sourceforge.net/). 
// Version 1.3.6.20110331_01     
// 11/05/19 19:45    
// ${CustomMessageForDisclaimer}                                                                             
// --------------------------------------------------------------------------------------------------
namespace FlightSimulator
{


    using Jp.Maker1.Vsys3.Tools;
    using System;
    using System.Drawing;
    using System.Collections;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class LocusViewer
    {
        internal Material planeM;
        internal Material planeM2;
        internal Light light;
        internal Light amblight;

        public Color infoCol;
        public Color dataCol;
        public Color cursolCol;
        public Color suisenCol;

        public Font stdFont;
        public Font smallFont;
        public Projector proj;
        private Clipper clip;
        private Scine sc;
        private OpticalAmbience oa;

        public readonly double viewAngle;
        internal Recorder recorder;
        internal Locus locus;
        internal LocusGrid grid;
        internal double dist;
        internal double cx;
        internal double cy;
        internal double cz;
        internal Matrix44 rmat;
        internal Matrix44 tmat;
        internal Matrix44 mat_bef;
        internal Matrix44 mat;

        internal double moveingTime;
        internal double rotationTime;

        internal int currentDataIndex;
        internal int fastMoveStep;

        public LocusViewer(double sX0, double sY0, double sX1, double sY1,
                AirPlane ap, Recorder rec)
        {
            planeM = new Material(new LightColor(0.0D, 0.0D, 0.6D),
                            new LightColor(0.5D, 0.5D, 0.5D), 10.0D, new LightColor(0.0D, 0.0D,
                                    0.0D));
            planeM2 = new Material(new LightColor(0.6D, 0.0D, 0.0D),
                    new LightColor(0.5D, 0.5D, 0.5D), 10.0D, new LightColor(0.0D, 0.0D,
                            0.0D));
            light = Jp.Maker1.Vsys3.Tools.Light.Parallel(new LightColor(255.0D, 255.0D, 255.0D),
                    new Vector3D(0.5D, -0.5D, 1.0D));
            amblight = Jp.Maker1.Vsys3.Tools.Light.Ambience(new LightColor(128.0D, 128.0D, 128.0D));
            infoCol = Color.FromArgb(64, 64, 64);
            dataCol = Color.Gray;
            cursolCol = Color.FromArgb(255, 0, 0);
            suisenCol = Color.Gray;
            stdFont = new Font("SansSelif", 12);
            smallFont = new Font("SansSelif", 11);
            sc = new Scine();
            oa = new OpticalAmbience();
            viewAngle = 25.0D;
            grid = new LocusGrid();
            rmat = new Matrix44();
            tmat = new Matrix44();
            mat_bef = new Matrix44();
            mat = new Matrix44();
            moveingTime = 5.0D;
            rotationTime = 7.0D;
            currentDataIndex = 0;
            proj = new Projector(sX1 - sX0 + 1.0D, sY1 - sY0 + 1.0D,MathTool.DegToRad(25.0D));
            clip = new Clipper(sX0, sY0, sX1, sY1);
            oa.AddLight(light);
            oa.AddLight(amblight);

            recorder = rec;

            locus = new Locus(ap, rec, planeM, planeM2);
            InitDispParam();

            fastMoveStep = (recorder.N_record() / 50);
            if (fastMoveStep <= 0)
                fastMoveStep = 1;
        }

        public void InitDispParam()
        {
            BoundingBox bbx = locus.locusBBox;
            RecordData data = recorder.GetData(currentDataIndex);

            dist = (bbx.DiagonalSize() / 2.0D / System.Math.Tan(MathTool.DegToRad(25.0D) / 2.0D));

            cx = ((bbx.GetMaxX() + bbx.GetMinX()) / 2.0D);
            cy = ((bbx.GetMaxY() + bbx.GetMinY()) / 2.0D);
            cz = ((bbx.GetMaxZ() + bbx.GetMinZ()) / 2.0D);

            mat_bef.SetTMat(-data.wpos.x, -data.wpos.y, -data.wpos.z);
            rmat.SetUMat();
            tmat.SetTMat(-cx + data.wpos.x, -cy + data.wpos.y,
                    -cz + data.wpos.z + dist);
        }

        public void UpdateDispParam(SimulatorInterface simif, double dt)
        {
            Matrix44 mtemp = new Matrix44();
            Matrix44 mat_aft = new Matrix44();

            BoundingBox bbx = locus.locusBBox;
            double v = bbx.DiagonalSize() / moveingTime;
            double w = 6.283185307179586D / rotationTime;

            double acc = 1.0D;
            if (simif.ctrl_sw == 1)
            {
                acc = 0.1D;
            }
            if (simif.locus_action_sw[0] == 1)
            {
                mtemp.SetRzMat(-w * dt * acc);
                rmat = rmat.MultMat(mtemp);
            }
            if (simif.locus_action_sw[1] == 1)
            {
                mtemp.SetRzMat(w * dt * acc);
                rmat = rmat.MultMat(mtemp);
            }
            if (simif.locus_action_sw[2] == 1)
            {
                mtemp.SetRxMat(w * dt * acc);
                rmat = rmat.MultMat(mtemp);
            }
            if (simif.locus_action_sw[3] == 1)
            {
                mtemp.SetRxMat(-w * dt * acc);
                rmat = rmat.MultMat(mtemp);
            }
            if (simif.locus_action_sw[4] == 1)
            {
                mtemp.SetRyMat(w * dt * acc);
                rmat = rmat.MultMat(mtemp);
            }
            if (simif.locus_action_sw[5] == 1)
            {
                mtemp.SetRyMat(-w * dt * acc);
                rmat = rmat.MultMat(mtemp);
            }
            if (simif.locus_action_sw[6] == 1)
            {
                dist -= v * dt * 3.0D * acc;
                mtemp.SetTMat(0.0D, 0.0D, -v * dt * 3.0D * acc);
                tmat = tmat.MultMat(mtemp);
            }
            if (simif.locus_action_sw[7] == 1)
            {
                dist += v * dt * 3.0D * acc;
                mtemp.SetTMat(0.0D, 0.0D, v * dt * 3.0D * acc);
                tmat = tmat.MultMat(mtemp);
            }
            if (simif.locus_action_sw[10] == 1)
            {
                mtemp.SetTMat(v * dt * acc, 0.0D, 0.0D);
                tmat = tmat.MultMat(mtemp);
            }
            if (simif.locus_action_sw[11] == 1)
            {
                mtemp.SetTMat(-v * dt * acc, 0.0D, 0.0D);
                tmat = tmat.MultMat(mtemp);
            }
            if (simif.locus_action_sw[8] == 1)
            {
                mtemp.SetTMat(0.0D, v * dt * acc, 0.0D);
                tmat = tmat.MultMat(mtemp);
            }
            if (simif.locus_action_sw[9] == 1)
            {
                mtemp.SetTMat(0.0D, -v * dt * acc, 0.0D);
                tmat = tmat.MultMat(mtemp);
            }

            if (simif.param_reset_sw == 1)
            {
                InitDispParam();
                simif.locusImageMake = 1;
                simif.param_reset_sw = 0;
            }

            int flag_move_data = 0;
            if (simif.data_foword_sw == 1)
            {
                flag_move_data = 1;
                if (simif.ctrl_sw == 1)
                    currentDataIndex += fastMoveStep;
                else
                {
                    currentDataIndex += 1;
                }
                if (currentDataIndex >= recorder.N_record())
                {
                    currentDataIndex = (recorder.N_record() - 1);
                }
            }
            if (simif.data_back_sw == 1)
            {
                flag_move_data = 1;
                if (simif.ctrl_sw == 1)
                    currentDataIndex -= fastMoveStep;
                else
                {
                    currentDataIndex -= 1;
                }
                if (currentDataIndex < 0)
                {
                    currentDataIndex = 0;
                }
            }
            if (flag_move_data == 1)
            {
                RecordData data = recorder.GetData(currentDataIndex);
                mtemp.SetMat(mat_bef);
                mat_bef.SetTMat(-data.wpos.x, -data.wpos.y, -data.wpos.z);
                mat_aft = mat_bef.InvMat().MultMat(mtemp).MultMat(rmat).MultMat(tmat);
                rmat = mat_aft.DtMat();
                tmat.SetTMat(mat_aft.element[3,0], mat_aft.element[3,1], mat_aft.element[3,2]);
            }
        }

        public void DrawLocus(Graphics g, int mode)
        {
            if (mode == 1)
            {
                // g.SetColor(Java.Awt.Color.white);
                //g.FillRect((int)clip.vp_xmin, (int)clip.vp_ymin, (int)(clip.vp_xmax - clip.vp_xmin + 1.0D), (int)(clip.vp_ymax - clip.vp_ymin + 1.0D));
            }

            mat = mat_bef.MultMat(rmat).MultMat(tmat);

            sc = new Scine();
            sc.optamb = oa;
            sc.Add((VsElement)grid.MakeGrid(locus.locusBBox, dist, 25.0D).Transform(mat));
            Scine view = sc.MakeView(clip, proj, false);
            view.Draw(g);

            if (mode == 1)
            {
                sc = new Scine();
                sc.optamb = oa;
                sc.Add((VsElement)locus.locus.Transform(mat));
                sc.Add((VsElement)locus.groundLocus.Transform(mat));
                sc.Add((VsElement)locus.ybaseLocus.Transform(mat));
                view = sc.MakeView(clip, proj, true);

                view.Draw(g, 1);

                if (recorder.N_record() > 0)
                {
                    //g.SetColor(infoCol);
                    //g.SetFont(stdFont);
                    //g.DrawString("REC. FROM " + Jp.Maker1.Sim.Tools.Tool.TimeStr1(recorder.GetData(0).t) + " TO " + Jp.Maker1.Sim.Tools.Tool.TimeStr1(recorder.GetData(recorder.N_record() - 1).t), 3, 13);
                }
            }
        }

        public void DrawData(Graphics g, SimulatorInterface simif)
        {
            if (recorder.N_record() == 0)
            {
                //g.SetColor(Java.Awt.Color.black);
                // g.SetFont(stdFont);
                // g.DrawString("No Data", 290, 180);
                return;
            }

            if (simif.record_output_sw == 1)
            {
                //  g.SetColor(Java.Awt.Color.red);
                //  g.SetFont(stdFont);
                //  g.DrawString("Writing Data", 270, 100);
                return;
            }

            RecordData data = recorder.GetData(currentDataIndex);

            VsPoint pos = new VsPoint(new Vector3D(data.wpos), Color.White);
            pos = (VsPoint)pos.Transform(mat);
            pos = (VsPoint)pos.Clip3DF(clip);
            pos = (VsPoint)pos.Project(proj);
            VsSegment seg = new VsSegment(new Vector3D(data.wpos), new Vector3D(
                    data.wpos), suisenCol);
            seg.seg.p1.y = grid.ys;
            seg = (VsSegment)seg.Transform(mat);
            seg = (VsSegment)seg.Clip3DF(clip);
            seg = (VsSegment)seg.Project(proj);
            seg.Draw(g);
            pos.Draw(g);
            if (pos.pos != null)
            {
                int px = (int)(pos.pos.x + 0.5D);
                int py = (int)(pos.pos.y + 0.5D);
                //g.SetColor(cursolCol);
                //g.DrawRect(px - 10, py - 10, 21, 21);
            }

            //g.SetColor(infoCol);
            //g.SetFont(stdFont);
            String str = "GRID SCALE:" + DispFormat.DoubleFormat(grid.grid_scale, 0) + "m";
            str = str + "   BASE ALT:" + DispFormat.DoubleFormat(LocusGrid.Y_base(locus.locusBBox), 0) + "m";
           // g.DrawString(str, 3, (int)(proj.screenYOffset + proj.screenYSize) - 4);

            str = "DATA No." + (currentDataIndex + 1) + " TIME:" + Jp.Maker1.Sim.Tools.Tool.TimeStr1(data.t);
           // g.DrawString(str, 300, 13);

           // g.SetFont(smallFont);

            int datax = 3;
            int datay = 30;
            int dy = 10;

            if (simif.dispDataSw == 1)
            {
                double h = data.wpos.y;
                datay += 2;
                //g.DrawString("XE [m] : " + Jp.Maker1.Util.DispFormat.DoubleFormat(data.wpos.x, 4, 1), datax, datay);
                datay += dy;
                //g.DrawString("YE [m] : " + Jp.Maker1.Util.DispFormat.DoubleFormat(h, 4, 1), datax, datay);
                datay += dy;
                //g.DrawString("ZE [m] : " + Jp.Maker1.Util.DispFormat.DoubleFormat(data.wpos.z, 4, 1), datax, datay);
                datay += dy;
                //g.DrawString("YE [ft]: " + Jp.Maker1.Util.DispFormat.DoubleFormat(Jp.Maker1.Sim.Tools.UnitConvert.M2ft(h), 4, 1), datax, datay);
                datay += dy;
                datay += 2;
                double v = data.vcDash.Length();
                //g.DrawString("VC [m/s]: " + Jp.Maker1.Util.DispFormat.DoubleFormat(v, 3, 1), datax, datay);
                datay += dy;
                double ias = Jp.Maker1.Sim.Tools.UnitConvert.Mps2kt(v) * Math.Sqrt(Isa.Density(h) / Isa.Density(0.0D));
                //g.DrawString("IAS [kt]: " + Jp.Maker1.Util.DispFormat.DoubleFormat(ias, 3, 1), datax, datay);
                datay += dy;
                double alpha = MathTool.RadToDeg(Jp.Maker1.Sim.Tools.Tool.CalcAlpha(data.vcDash));
                datay += 2;
                //g.DrawString("�� [deg]: " + Jp.Maker1.Util.DispFormat.DoubleFormat(alpha, 3, 1), datax, datay);
                datay += dy;
                double beta = MathTool.RadToDeg(Jp.Maker1.Sim.Tools.Tool.CalcBeta(data.vcDash));
                //g.DrawString("�� [deg]: " + Jp.Maker1.Util.DispFormat.DoubleFormat(beta, 3, 1), datax, datay);
                datay += dy;
                datay += 2;
              //  g.DrawString("�� [deg]: " + Jp.Maker1.Util.DispFormat.DoubleFormat(Jp.Maker1.Util.MathTool.RadToDeg(data.roll.GetValue()), 3, 1), datax, datay);
                datay += dy;
               // g.DrawString("�� [deg]: " + Jp.Maker1.Util.DispFormat.DoubleFormat(Jp.Maker1.Util.MathTool.RadToDeg(data.pitch.GetValue()), 3, 1), datax, datay);
                datay += dy;
               // g.DrawString("�� [deg]: " + Jp.Maker1.Util.DispFormat.DoubleFormat(Jp.Maker1.Util.MathTool.RadToDeg(data.yaw.GetValue()), 3, 1), datax, datay);
                datay += dy;
                datay += 2;
               // g.DrawString("P [deg/s]: " + Jp.Maker1.Util.DispFormat.DoubleFormat(Jp.Maker1.Util.MathTool.RadToDeg(data.omega.x), 3, 1), datax, datay);
                datay += dy;
               // g.DrawString("Q [deg/s]: " + Jp.Maker1.Util.DispFormat.DoubleFormat(Jp.Maker1.Util.MathTool.RadToDeg(data.omega.y), 3, 1), datax, datay);
                datay += dy;
               // g.DrawString("R [deg/s]: " + Jp.Maker1.Util.DispFormat.DoubleFormat(Jp.Maker1.Util.MathTool.RadToDeg(data.omega.z), 3, 1), datax, datay);
                datay += dy;
                datay += 2;
               // g.DrawString("dU/dt [m/s2]: " + Jp.Maker1.Util.DispFormat.DoubleFormat(data.d_vc.x, 2, 1), datax, datay);
                datay += dy;
                // g.DrawString("dV/dt [m/s2]: " + Jp.Maker1.Util.DispFormat.DoubleFormat(data.d_vc.y, 2, 1), datax, datay);
                datay += dy;
               // g.DrawString("dW/dt [m/s2]: " + Jp.Maker1.Util.DispFormat.DoubleFormat(data.d_vc.z, 2, 1), datax, datay);
                datay += dy;
                datay += 2;
                // g.DrawString("dP/dt [deg/s2]: " + Jp.Maker1.Util.DispFormat.DoubleFormat(Jp.Maker1.Util.MathTool.RadToDeg(data.d_omega.x), 3, 1), datax, datay);
                datay += dy;
                //g.DrawString("dQ/dt [deg/s2]: " + Jp.Maker1.Util.DispFormat.DoubleFormat(Jp.Maker1.Util.MathTool.RadToDeg(data.d_omega.y), 3, 1), datax, datay);
                datay += dy;
                //  g.DrawString("dR/dt [deg/s2]: " + Jp.Maker1.Util.DispFormat.DoubleFormat(Jp.Maker1.Util.MathTool.RadToDeg(data.d_omega.z), 3, 1), datax, datay);
                datay += dy;
                datay += 2;
                //g.DrawString("�� [deg]: " + Jp.Maker1.Util.DispFormat.DoubleFormat(Jp.Maker1.Util.MathTool.RadToDeg(data.theta), 3, 1), datax, datay);
                datay += dy;
                datay += 2;
                //g.DrawString("Fx/m [m/s2]: " + Jp.Maker1.Util.DispFormat.DoubleFormat(data.f_m.x, 3, 1), datax, datay);
                datay += dy;
                //g.DrawString("Fy/m [m/s2]: " + Jp.Maker1.Util.DispFormat.DoubleFormat(data.f_m.y, 3, 1), datax, datay);
                datay += dy;
                // g.DrawString("Fz/m [m/s2]: " + Jp.Maker1.Util.DispFormat.DoubleFormat(data.f_m.z, 3, 1), datax, datay);
                datay += dy;
                datay += 2;
                // g.DrawString("��e [deg] : " + Jp.Maker1.Util.DispFormat.DoubleFormat(Jp.Maker1.Util.MathTool.RadToDeg(data.delta_e), 3, 1), datax, datay);
                datay += dy;
                // g.DrawString("��aR [deg]: " + Jp.Maker1.Util.DispFormat.DoubleFormat(Jp.Maker1.Util.MathTool.RadToDeg(data.delta_a_r), 3, 1), datax, datay);
                datay += dy;
                // g.DrawString("��aL [deg]: " + Jp.Maker1.Util.DispFormat.DoubleFormat(Jp.Maker1.Util.MathTool.RadToDeg(data.delta_a_l), 3, 1), datax, datay);
                datay += dy;
                // g.DrawString("��r [deg]: " + Jp.Maker1.Util.DispFormat.DoubleFormat(Jp.Maker1.Util.MathTool.RadToDeg(data.delta_r), 3, 1), datax, datay);
                datay += dy;
                // g.DrawString("��f [deg]: " + Jp.Maker1.Util.DispFormat.DoubleFormat(Jp.Maker1.Util.MathTool.RadToDeg(data.delta_t_flap), 3, 1), datax, datay);
                datay += dy;
                // g.DrawString("L.GEAR: " + data.flag_gear, datax, datay);
                datay += dy;
                // g.DrawString("LAND: " + data.flag_land, datax, datay);
                datay += dy;
                datay += 2;
                // g.DrawString("Thrust/m [m/s2]: " + Jp.Maker1.Util.DispFormat.DoubleFormat(data.thrust_m, 3, 1), datax, datay);
                datay += dy;
                // g.DrawString("THR-POS [m/s2]: " + Jp.Maker1.Util.DispFormat.DoubleFormat(data.throttle_pos, 3, 2), datax, datay);
                datay += dy;
            }
        }
    }
}