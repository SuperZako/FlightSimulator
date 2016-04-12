    using Jp.Maker1.Io.Textfile;
    using Jp.Maker1.Sim.Tools;
    using Jp.Maker1.Vsys3.Tools;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;

public class AirPlaneFile : ReadTextFileOnServer
{
    internal Uri url;
    internal FileInfo file;

    public AirPlaneFile(AirPlane ap, Uri codeBase, String path)
    {
        url = null;
        file = null;
        //try
        //{
        //    url = new Uri(codeBase, path);
        //}
        //catch (Exception e)
        //{
        //    Console.Error.WriteLine(e.StackTrace);
        //}
        Open(path);
        ReadAirPlaneFile(ap, false);
        Close();
    }

    public AirPlaneFile(AirPlane ap, String path)
    {
        url = null;
        file = null;
        try
        {
            file = new FileInfo(path);
        }
        catch (Exception e)
        {
            Console.Error.WriteLine(e.StackTrace);
        }
        Open(file);
        ReadAirPlaneFile(ap, false);

        Close();
    }

    private void ReadAirPlaneFile(AirPlane ap, bool flagMessage)
    {
        System.Console.Out.WriteLine("機体全般 ************************************************");
        ReadPlaneGeneral(ap.plane, flagMessage);
        ap.plane.Print();

        System.Console.Out.WriteLine("エンジン ************************************************");
        ap.n_powerPlant = ReadPowerPlant(ap.powerPlant, flagMessage);
        int i;
        for (i = 0; i < ap.n_powerPlant; i++)
        {
            ap.powerPlant[i].Print();
        }

        System.Console.Out.WriteLine("胴体 ****************************************************");
        ap.n_fuselage = ReadIntValue("fuselage", flagMessage);
        for (i = 0; i < ap.n_fuselage; i++)
        {
            ReadFuselage(ap.fuslage[i, 0], ap.fuslage[i, 1], flagMessage);
            ap.fuslage[i, 0].Print();
            ap.fuslage[i, 1].Print();
        }

        System.Console.Out.WriteLine("主翼 ****************************************************");
        ap.n_wing = ReadIntValue("wing", flagMessage);
        for (i = 0; i < ap.n_wing; i++)
        {
            ReadWing(ap.wing[i], 0, flagMessage);
            ap.wing[i].Print();
        }

        System.Console.Out.WriteLine("水平尾翼 ************************************************");
        ap.n_htail = ReadIntValue("htail", flagMessage);
        for (i = 0; i < ap.n_htail; i++)
        {
            ReadWing(ap.htail[i], 0, flagMessage);
            ap.htail[i].Print();
        }

        System.Console.Out.WriteLine("カナード翼 ************************************************");
        ap.n_canard = ReadIntValue("canard", flagMessage);
        for (i = 0; i < ap.n_canard; i++)
        {
            ReadWing(ap.canard[i], 0, flagMessage);
            ap.canard[i].Print();
        }

        System.Console.Out
                .WriteLine("垂直尾翼 ************************************************");
        ap.n_vtail = ReadIntValue("vtail", flagMessage);
        for (i = 0; i < ap.n_vtail; i++)
        {
            ReadWing(ap.vtail[i], 1, flagMessage);
            ap.vtail[i].Print();
        }

        System.Console.Out.WriteLine("フィン ************************************************");
        ap.n_fin = ReadIntValue("fin", flagMessage);
        for (i = 0; i < ap.n_fin; i++)
        {
            ReadWing(ap.fin[i], 1, flagMessage);
            ap.fin[i].Print();
        }

        System.Console.Out.WriteLine("エレベータ **************************************************");
        ap.n_elevator = ReadIntValue("elevator", flagMessage);
        for (i = 0; i < ap.n_elevator; i++)
        {
            ReadControlPlane(ap.htail[0], ap.elevator[i, 0], ap.elevator[i, 1],
                    flagMessage);
            for (int lr = 0; lr <= 1; lr++)
            {
                System.Console.Out.WriteLine("-----------------------------------------------------");
                System.Console.Out.WriteLine(AirPlane.lrName[lr]);
                System.Console.Out.WriteLine("-----------------------------------------------------");
                ap.elevator[i, lr].Print();
            }
        }

        System.Console.Out.WriteLine("カナードエレベータ **************************************************");
        ap.n_canard_elevator = ReadIntValue("canaer_elevator", flagMessage);
        for (i = 0; i < ap.n_canard_elevator; i++)
        {
            ReadControlPlane(ap.canard[0], ap.canard_elevator[i, 0],
                    ap.canard_elevator[i, 1], flagMessage);
            for (int lr_0 = 0; lr_0 <= 1; lr_0++)
            {
                System.Console.Out.WriteLine("-----------------------------------------------------");
                System.Console.Out.WriteLine(AirPlane.lrName[lr_0]);
                System.Console.Out.WriteLine("-----------------------------------------------------");
                ap.canard_elevator[i, lr_0].Print();
            }
        }

        System.Console.Out.WriteLine("エルロン **************************************************");
        ap.n_aileron = ReadIntValue("aileron", flagMessage);
        for (i = 0; i < ap.n_aileron; i++)
        {
            ReadControlPlane(ap.wing[0], ap.aileron[i, 0], ap.aileron[i, 1], flagMessage);
            for (int lr_1 = 0; lr_1 <= 1; lr_1++)
            {
                System.Console.Out.WriteLine("-----------------------------------------------------");
                System.Console.Out.WriteLine(AirPlane.lrName[lr_1]);
                System.Console.Out.WriteLine("-----------------------------------------------------");
                ap.aileron[i, lr_1].Print();
            }
        }

        System.Console.Out.WriteLine("ラダー **************************************************");
        ap.n_rudder = ReadIntValue("rudder", flagMessage);
        for (i = 0; i < ap.n_rudder; i++)
        {
            ReadControlPlane(ap.vtail[0], ap.rudder[i, 0], ap.rudder[i, 1], flagMessage);
            for (int lr_2 = 0; lr_2 <= 1; lr_2++)
            {
                System.Console.Out.WriteLine("-----------------------------------------------------");
                System.Console.Out.WriteLine(AirPlane.lrName[lr_2]);
                System.Console.Out.WriteLine("-----------------------------------------------------");
                ap.rudder[i, lr_2].Print();
            }
        }

        System.Console.Out.WriteLine("後縁フラップ **************************************************");
        ap.n_t_flap = ReadIntValue("t_flap", flagMessage);
        for (i = 0; i < ap.n_t_flap; i++)
        {
            ReadControlPlane(ap.wing[0], ap.t_flap[i, 0], ap.t_flap[i, 1], flagMessage);
            for (int lr_3 = 0; lr_3 <= 1; lr_3++)
            {
                System.Console.Out.WriteLine("-----------------------------------------------------");
                System.Console.Out.WriteLine(AirPlane.lrName[lr_3]);
                System.Console.Out.WriteLine("-----------------------------------------------------");
                ap.t_flap[i, lr_3].Print();
            }
        }

        System.Console.Out.WriteLine("前縁フラップ **************************************************");
        ap.n_l_flap = ReadIntValue("l_flap", flagMessage);
        for (i = 0; i < ap.n_l_flap; i++)
        {
            ReadControlPlane(ap.wing[0], ap.l_flap[i, 0], ap.l_flap[i, 1],
                    flagMessage);
            for (int lr_4 = 0; lr_4 <= 1; lr_4++)
            {
                System.Console.Out.WriteLine("-----------------------------------------------------");
                System.Console.Out.WriteLine(AirPlane.lrName[lr_4]);
                System.Console.Out.WriteLine("-----------------------------------------------------");
                ap.l_flap[i, lr_4].Print();
            }
        }

        System.Console.Out.WriteLine("着陸装置 ************************************************");
        ap.n_LandingGear = ReadIntValue("landing_gear", flagMessage);
        for (i = 0; i < ap.n_LandingGear; i++)
        {
            ReadLamdingGear(ap.landing_gear[i, 0], ap.landing_gear[i, 1], flagMessage);
            ap.landing_gear[i, 0].Print();
            ap.landing_gear[i, 1].Print();
        }

        System.Console.Out.WriteLine("干渉 ************************************************");
        ReadInterference(ap.interference);
        ap.interference.Init(ap);
        ap.interference.Print();
    }

    private void ReadPlaneGeneral(PlaneGeneral pg, bool flagMessage)
    {
        pg.name = ReadString(" name ", flagMessage);
        pg.maker = ReadString(" maker ", flagMessage);

        pg.length = ReadDblValue("length", flagMessage);
        pg.width = ReadDblValue("width", flagMessage);
        pg.height = ReadDblValue("height", flagMessage);
        pg.x_offset = ReadDblValue("x_offset", flagMessage);

        pg.m = ReadDblValue("m", flagMessage);
        pg.m0 = ReadDblValue("m0", flagMessage);
        pg.cg = ReadVector("C.G.", flagMessage);
        pg.ixx_m0 = ReadDblValue("Ixx/m0", flagMessage);
        pg.iyy_m0 = ReadDblValue("Iyy/m0", flagMessage);
        pg.izz_m0 = ReadDblValue("Izz/m0", flagMessage);
        pg.ixy_m0 = ReadDblValue("Ixy/m0", flagMessage);
        pg.iyz_m0 = ReadDblValue("Iyz/m0", flagMessage);
        pg.izx_m0 = ReadDblValue("Izx/m0", flagMessage);

        pg.eyePoint = ReadVector("eye_point", flagMessage);

        int n = ReadIntValue("reference_point", flagMessage);
        pg.reference_point = new Vector3D[n];
        for (int i = 0; i < n; i++)
        {
            pg.reference_point[i] = ReadVector();
            ReadLine();
        }

        pg.Init();
    }

    public void ReadFuselage(Fuselage fsR, Fuselage fsL, bool flagMessage)
    {
        fsR.flag = 0;
        fsL.flag = 0;

        fsR.block_name = ReadString("blcok_name", flagMessage);
        int n = ReadIntValue("n", flagMessage);

        fsR.flag = 1;
        fsR.ac_base = ReadVector("Ac", flagMessage);
        fsR.lfus = ReadDblValue("length", flagMessage);
        fsR.vfus = ReadDblValue("Vfus", flagMessage);
        fsR.cd_s = ReadDblValue("CD*Spi(fus)", flagMessage);
        fsR.s_pi = ReadDblValue("Spi", flagMessage);
        fsR.Init();

        if (n > 1)
        {
            fsL.block_name = fsR.block_name;
            fsL.flag = fsR.flag;
            fsL.ac_base = fsR.ac_base;
            fsL.lfus = fsR.lfus;
            fsL.vfus = fsR.vfus;
            fsL.cd_s = fsR.cd_s;
            fsL.s_pi = fsR.s_pi;
            fsL.Init();
        }
    }

    private int ReadPowerPlant(PowerPlant[] pp, bool flagMessage)
    {
        int n = ReadIntValue("power_plant", flagMessage);
        for (int i = 0; i < n; i++)
        {
            pp[i] = new PowerPlant();
            pp[i].p = ReadVector("thrust_point", flagMessage);
            pp[i].v = ReadVector("thrust_vector", flagMessage);
            String str = ReadString("engine_type", flagMessage);
            if (str.Equals("RECIPROCATING_ENGINE"))
            {
                pp[i].engine_type = 1;
                ReadReciprocatingEngine(pp[i], flagMessage);
            }
            else if (str.Equals("TURBOJET_ENGINE"))
            {
                pp[i].engine_type = 0;
            }
            else if (str.Equals("TURBOPROP_ENGINE"))
            {
                pp[i].engine_type = 0;
            }
            else if (str.Equals("TURBOFUN_ENGINE"))
            {
                pp[i].engine_type = 0;
            }
            else
            {
                pp[i].engine_type = 0;
            }
            pp[i].Init();
        }

        return n;
    }

    public void ReadReciprocatingEngine(PowerPlant pp, bool flagMessage)
    {
        ReciprocatingEngine rp = new ReciprocatingEngine();
        pp.engine = rp;
        rp.pp = pp;

        rp.engineName = ReadString("engine_name", flagMessage);
        rp.p_r = ReadDblValue("p_takeoff", flagMessage);
        rp.p_ck = ReadDblValue("p_max_0", flagMessage);
        rp.p_k = ReadDblValue("p_max", flagMessage);
        rp.h_k = ReadDblValue("rated_altitude", flagMessage);
        rp.h_k2_shift = ReadDblValue("blower_shift_altitude", flagMessage);
        rp.p_k2 = ReadDblValue("p_max_2", flagMessage);
        rp.h_k2 = ReadDblValue("rated_altitude_2", flagMessage);

        rp.propellerName = ReadString("propepper_name", flagMessage);
        rp.diameter = ReadDblValue("diameter", flagMessage);
        rp.fm = ReadDblValue("fm", flagMessage);
        rp.epsilon = ReadDblValue("epsilon", flagMessage);

        rp.Init();
    }

    private void ReadWing(Wing wg, int hv_arrangement, bool flagMessage)
    {
        double[] b2f_beta = { -90.0D, -80.0D, -70.0D, -60.0D, -50.0D, -40.0D, -30.0D, -20.0D, -10.0D, 0, 10.0D, 20.0D, 30.0D, 40.0D, 50.0D, 60.0D, 70.0D, 80.0D, 90.0D };
        double[] b2f_b = new double[19];

        int i;
        for (i = 0; i < b2f_beta.Length; i++)
        {
            b2f_beta[i] = MathTool.DegToRad(b2f_beta[i]);
        }

        wg.flag = 1;
        wg.name = ReadString("name", flagMessage);
        wg.n_lr = ReadIntValue("n", flagMessage);

        wg.hv_arrangement = hv_arrangement;
        wg.s2 = ReadDblValue("S/n", flagMessage);
        Pass("b/n = ");
        for (i = 0; i < 19; i++)
        {
            b2f_b[i] = ReadDouble();
        }
        wg.b2_func = new Function2D(b2f_beta, b2f_b);
        wg.k_ar = ReadDblValue("k_Ar", flagMessage);

        wg.n_wing_block = ReadIntValue("wing_block", flagMessage);
        wg.wp = new WingPlane[wg.n_wing_block, 2];
        for (i = 0; i < wg.n_wing_block; i++)
        {
            wg.wp[i, 0] = new WingPlane();
            wg.wp[i, 1] = new WingPlane();
            WingPlane wpR = wg.wp[i, 0];
            WingPlane wpL = wg.wp[i, 1];
            ReadWingPlane(wg, wpR, wpL, flagMessage);
        }
    }

    private void ReadWingPlane(Wing wg, WingPlane wpR, WingPlane wpL,            bool flagMessage)
    {
        wpR.flag = 0;
        wpL.flag = 0;

        wpR.block_name = ReadString("block_name", flagMessage);
        if (wg.n_lr >= 1)
        {
            wpR.flag = 1;
            wpR.wing = wg;
            if (wg.hv_arrangement == 0)
                wpR.arrangement = 1;
            else
            {
                wpR.arrangement = 3;
            }
            wpR.s2 = ReadDblValue("S/n", flagMessage);
            wpR.ac_base = ReadVector("Ac", flagMessage);
            wpR.gc = ReadVector("Gc", flagMessage);
            wpR.gamma = MathTool.DegToRad(ReadDblValue("gamma", flagMessage));
            wpR.delta = MathTool.DegToRad(ReadDblValue("delta", flagMessage));
            wpR.lamda = MathTool.DegToRad(ReadDblValue("lamda", flagMessage));
            wpR.t_c = ReadDblValue("t/c", flagMessage);
            wpR.a0 = ReadDblValue("a0", flagMessage);
            wpR.alpha0 = MathTool.DegToRad(ReadDblValue("alpha0", flagMessage));
            wpR.clmax = ReadDblValue("CLmax", flagMessage);
            wpR.delta_clmax = ReadDblValue("delta_CLmax", flagMessage);
            wpR.delta_alphe_p = MathTool.DegToRad(ReadDblValue("delta_alpha_p", flagMessage));
            wpR.clmin = ReadDblValue("CLmin", flagMessage);
            wpR.delta_clmin = ReadDblValue("delta_CLmin", flagMessage);
            wpR.delta_alphe_m = MathTool.DegToRad(ReadDblValue("delta_alpha_m", flagMessage));
            wpR.alpha_i = MathTool.DegToRad(ReadDblValue("alpha_i", flagMessage));
            wpR.cdmin = ReadDblValue("CDmin", flagMessage);
            wpR.k_cd = ReadDblValue("k_cd", flagMessage);
            wpR.delta_cdmin = ReadDblValue("delta_CDmin", flagMessage);
            wpR.alpha_backet = MathTool.DegToRad(ReadDblValue("alpha_backet", flagMessage));
            wpR.cmac = ReadDblValue("Cmac", flagMessage);
            wpR.ew = ReadDblValue("ew", flagMessage);
            wpR.mc = ReadDblValue("Mc", flagMessage);
            wpR.Init();
        }
        if (wg.n_lr == 2)
        {
            wpL.flag = 1;
            wpL.wing = wg;
            wpL.block_name = wpR.block_name;
            if (wg.hv_arrangement == 0)
                wpL.arrangement = 2;
            else
            {
                wpL.arrangement = 4;
            }
            wpL.s2 = wpR.s2;
            wpL.ac_base = wpR.ac_base;
            wpL.gc = wpR.gc;
            wpL.gamma = wpR.gamma;
            wpL.delta = wpR.delta;
            wpL.lamda = wpR.lamda;
            wpL.t_c = wpR.t_c;

            wpL.a0 = wpR.a0;
            wpL.alpha0 = wpR.alpha0;
            wpL.clmax = wpR.clmax;
            wpL.delta_clmax = wpR.delta_clmax;
            wpL.delta_alphe_p = wpR.delta_alphe_p;
            wpL.clmin = wpR.clmin;
            wpL.delta_clmin = wpR.delta_clmin;
            wpL.delta_alphe_m = wpR.delta_alphe_m;
            wpL.alpha_i = wpR.alpha_i;
            wpL.cdmin = wpR.cdmin;
            wpL.k_cd = wpR.k_cd;
            wpL.delta_cdmin = wpR.delta_cdmin;
            wpL.alpha_backet = wpR.alpha_backet;
            wpL.cmac = wpR.cmac;
            wpL.ew = wpR.ew;
            wpL.mc = wpR.mc;
            wpL.Init();
        }
    }

    private void ReadControlPlane(Wing wg, ControlPlane cpR, ControlPlane cpL,
            bool flagMessage)
    {
        cpR.type = 0;
        cpL.type = 0;

        String typeStr = ReadString("type", flagMessage);
        if (typeStr.Equals("all_flying"))
            cpR.type = 2;
        else if (typeStr.Equals("tailing_edge"))
            cpR.type = 1;
        else if (typeStr.Equals("leading_edge"))
            cpR.type = 1;
        else
        {
            cpR.type = 0;
        }

        typeStr = ReadString("actuate_type", flagMessage);
        if (typeStr.Equals("actuator_drive"))
            cpR.actuate_type = 1;
        else
        {
            cpR.actuate_type = 0;
        }
        if (wg.n_lr == 2)
        {
            cpL.type = cpR.type;
            cpL.actuate_type = cpR.actuate_type;
        }

        ReadWingPlane(wg, cpR.baseWingBlock, cpL.baseWingBlock, flagMessage);

        cpR.delta_max = MathTool.DegToRad(ReadDblValue("delta_max", flagMessage));
        cpR.delta_min = MathTool.DegToRad(ReadDblValue("delta_min", flagMessage));
        cpR.lamda_h = MathTool.DegToRad(ReadDblValue("lamda_H", flagMessage));
        if (cpL.type != 0)
        {
            cpL.delta_max = cpR.delta_max;
            cpL.delta_min = cpR.delta_min;
            cpL.lamda_h = cpR.lamda_h;
        }

        if ((cpR.type == 1) || (cpR.type == 1))
        {
            ReadControlPlaneFlapType(cpR, cpL, flagMessage);
        }

        if (cpR.type == 2)
        {
            ReadControlPlaneAllFType(cpR, cpL, flagMessage);
        }

        if (cpR.actuate_type == 1)
        {
            cpR.t_move = ReadDblValue("t_moving", flagMessage);
            if ((cpL.baseWingBlock != null) && (cpL.baseWingBlock.flag == 1))
            {
                cpL.t_move = cpR.t_move;
            }
        }
        cpR.Init();
        cpL.Init();
    }

    private void ReadControlPlaneFlapType(ControlPlane cpR, ControlPlane cpL, bool flagMessage)
    {
        String flap_type_name = ReadString("flap", flagMessage);
        cpR.flap_type = 0;
        cpL.flap_type = 0;
        for (int i = 0; i < Flap.flap_type_name.Length; i++)
        {
            if (Flap.flap_type_name[i].Equals(flap_type_name))
            {
                cpR.flap_type = i;
            }
        }
        cpR.cf_c = ReadDblValue("cf/c", flagMessage);
        cpR.dCLmax = ReadDblValue("dCLmax", flagMessage);
        if (cpR.type != 0)
        {
            cpL.flap_type = cpR.flap_type;
            cpL.cf_c = cpR.cf_c;
            cpL.dCLmax = cpR.dCLmax;
        }
    }

    private void ReadControlPlaneAllFType(ControlPlane cpR, ControlPlane cpL, bool flagMessage)
    {
        cpR.gamma_h = MathTool.DegToRad(ReadDblValue("gamma_H", flagMessage));
        cpR.hc = ReadVector("HC", flagMessage);
        if (cpR.type != 0)
        {
            cpL.gamma_h = cpR.gamma_h;
            cpL.hc = cpR.hc;
        }
    }

    private void ReadLamdingGear(LandingGear lgR, LandingGear lgL, bool flagMessage)
    {
        lgR.flag = 0;
        lgL.flag = 0;

        lgR.block_name = ReadString("block_name", flagMessage);
        int n = ReadIntValue("n", flagMessage);
        lgR.flag = 1;
        lgR.p0_base = ReadVector("p0_base", flagMessage);
        lgR.stroke0 = ReadDblValue("stroke", flagMessage);
        lgR.w = ReadDblValue("w", flagMessage);
        double tire_r = ReadDblValue("r", flagMessage);
        double tb_max = ReadDblValue("Tb_max", flagMessage);
        lgR.tire = new Tire2(tire_r, tb_max);
        lgR.cd_s = ReadDblValue("CD*S", flagMessage);
        lgR.sa.p0 = ReadVector("pa0_base", flagMessage);
        lgR.sa.p1 = ReadVector("pa1_base", flagMessage);
        lgR.t_move = ReadDblValue("t_moving", flagMessage);
        lgR.t_lag_move[0] = ReadDblValue("t_lag_right", flagMessage);
        lgR.t_lag_move[1] = ReadDblValue("t_lag_left", flagMessage);
        lgR.Init();
        if (n > 1)
        {
            lgL.block_name = lgR.block_name;
            lgL.flag = 1;
            lgL.p0_base = lgR.p0_base;
            lgL.stroke0 = lgR.stroke0;
            lgL.w = lgR.w;
            lgL.tire = new Tire2(tire_r, tb_max);
            lgL.cd_s = lgR.cd_s;
            lgL.sa = lgR.sa;
            lgL.t_move = lgR.t_move;
            lgL.t_lag_move = lgR.t_lag_move;
            lgL.Init();
        }
    }

    private void ReadInterference(Interference itf)
    {
        double[] k_svt_alpha = { 0, 20.0D, 40.0D, 60.0D, 80.0D, 100.0D, 120.0D,
					140.0D, 160.0D, 180.0D, 200.0D, 220.0D, 240.0D, 260.0D, 280.0D,
					300.0D, 320.0D, 340.0D, 360.0D };
        double[] k_svt_wg_k_svt = new double[19];
        double[] k_svt_ht_k_svt = new double[19];
        int i;
        for (i = 0; i < k_svt_alpha.Length; i++)
        {
            k_svt_alpha[i] = MathTool.DegToRad(k_svt_alpha[i]);
        }

        Pass("k_SVtail_Wing_stall(alpha) = ");
        for (i = 0; i < 19; i++)
        {
            k_svt_wg_k_svt[i] = ReadDouble();
        }
        itf.k_SVtail_Wing_stall = new Function2D(k_svt_alpha, k_svt_wg_k_svt);
        Pass("k_SVtail_Htail_stall(alpha) = ");
        for (i = 0; i < 19; i++)
        {
            k_svt_ht_k_svt[i] = ReadDouble();
        }
        itf.k_SVtail_Htail_stall = new Function2D(k_svt_alpha, k_svt_ht_k_svt);
    }
}