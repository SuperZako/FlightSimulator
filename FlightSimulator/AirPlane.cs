

    using Jp.Maker1.Sim.Tools;
    using Jp.Maker1.Vsys3.Tools;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;

public class AirPlane
{
    public const int EXIST = 1;
    public const int NONE = 0;
    public const int RIGHT = 0;
    public const int LEFT = 1;
    public const int CENTER = 2;
    public const int HORIZONTAL = 0;
    public const int VERTICAL = 1;
    public const int HORIZONTAL_RIGHT = 1;
    public const int HORIZONTAL_LEFT = 2;
    public const int VERTICAL_RIGHT = 3;
    public const int VERTICAL_LEFT = 4;
    public const int T_FLAP = 1;
    public const int L_FLAP = 1;
    public const int ALL_F = 2;
    public const int DIRECT_LINK = 0;
    public const int ACTUATOR_DRIVE = 1;
    public const int RECIPROCATING_ENGINE = 1;
    public const int TURBOJET_ENGINE = 2;
    public const int TURBOPROP_ENGINE = 3;
    public const int TURBOFUN_ENGINE = 4;
    public static readonly String[] lrName = { "R", "L", "C" };

    public static readonly Vector3D ZERO_VECTOR = new Vector3D(0.0D, 0.0D, 0.0D);
    public static readonly Vector3D[] ZERO_VECTOR_PAIR = { ZERO_VECTOR, ZERO_VECTOR };
    public static readonly double[] ONE_PAIR = { 1.0D, 1.0D };
    public const int max_n_Fuselage = 7;
    public const int max_n_Wing = 1;
    public const int max_n_Htail = 1;
    public const int max_n_Vtail = 1;
    public const int max_n_Canard = 1;
    public const int max_n_Fin = 5;
    public const int max_n_Aileron = 1;
    public const int max_n_Elevator = 1;
    public const int max_n_Rudder = 1;
    public const int max_n_T_Flap = 1;
    public const int max_n_L_Flap = 1;
    public const int max_n_Canard_Elevator = 1;
    public const int max_n_LandingGear = 3;
    public const int max_n_PowerPlant = 4;
    public PlaneGeneral plane;

    public int n_fuselage;
    public Fuselage[,] fuslage;

    public int n_wing;
    public Wing[] wing;

    public int n_htail;
    public Wing[] htail;
    public int n_vtail;
    public Wing[] vtail;
    public int n_canard;
    public Wing[] canard;
    public int n_fin;
    public Wing[] fin;

    public int n_aileron;
    public ControlPlane[,] aileron;
    public int n_elevator;
    public ControlPlane[,] elevator;
    public int n_rudder;
    public ControlPlane[,] rudder;
    public int n_l_flap;
    public ControlPlane[,] l_flap;
    public int n_t_flap;
    public ControlPlane[,] t_flap;
    public int n_canard_elevator;
    public ControlPlane[,] canard_elevator;

    public int n_powerPlant;
    public PowerPlant[] powerPlant;

    public int n_LandingGear;
    public LandingGear[,] landing_gear;

    public AirBrake airbrake;

    public Interference interference;

    public Pilot pilot;
    public PlaneMotion pMotion;
    public Inertia inp;
    public CockpitInterface cif;
    public Isa atmos;
    public Vector3D force;
    public Vector3D torque;
    public Vector3D a_force;
    public Vector3D a_torque;
    public Vector3D f_force;
    public Vector3D f_torque;
    public Vector3D n_force;
    public Vector3D n_torque;
    public Vector3D t_force;
    public Vector3D t_torque;

    public int flag_landing_gear;
    public int flag_land;
    public double max_k_stall_wing;
    public double max_k_stall_htail;
    public int crash_point;

    public AirPlane()
    {
    }

    public AirPlane(Uri codeBase, String path)
    {
        plane = new PlaneGeneral();
        n_fuselage = 0;
        fuslage = new Fuselage[7, 2];
        n_wing = 0;
        wing = new Wing[1];
        n_htail = 0;
        htail = new Wing[1];
        n_vtail = 0;
        vtail = new Wing[1];
        n_canard = 0;
        canard = new Wing[1];
        n_fin = 0;
        fin = new Wing[5];
        n_aileron = 0;
        aileron = new ControlPlane[1, 2];
        n_elevator = 0;
        elevator = new ControlPlane[1, 2];
        n_rudder = 0;
        rudder = new ControlPlane[1, 2];
        n_l_flap = 0;
        l_flap = new ControlPlane[1, 2];
        n_t_flap = 0;
        t_flap = new ControlPlane[1, 2];
        n_canard_elevator = 0;
        canard_elevator = new ControlPlane[1, 2];
        n_powerPlant = 0;
        powerPlant = new PowerPlant[4];
        n_LandingGear = 0;
        landing_gear = new LandingGear[3, 2];
        airbrake = new AirBrake();
        interference = new Interference();
        pilot = new Pilot();
        force = new Vector3D();
        torque = new Vector3D();
        a_force = new Vector3D();
        a_torque = new Vector3D();
        f_force = new Vector3D();
        f_torque = new Vector3D();
        n_force = new Vector3D();
        n_torque = new Vector3D();
        t_force = new Vector3D();
        t_torque = new Vector3D();
        flag_landing_gear = 1;
        flag_land = 1;
        crash_point = -1;
        MakeObject();

        AirPlaneFile apsf = new AirPlaneFile(this, codeBase, path);

        Init();
    }

    public AirPlane(String path)
    {
        plane = new PlaneGeneral();
        n_fuselage = 0;
        fuslage = new Fuselage[7, 2];
        n_wing = 0;
        wing = new Wing[1];
        n_htail = 0;
        htail = new Wing[1];
        n_vtail = 0;
        vtail = new Wing[1];
        n_canard = 0;
        canard = new Wing[1];
        n_fin = 0;
        fin = new Wing[5];
        n_aileron = 0;
        aileron = new ControlPlane[1, 2];
        n_elevator = 0;
        elevator = new ControlPlane[1, 2];
        n_rudder = 0;
        rudder = new ControlPlane[1, 2];
        n_l_flap = 0;
        l_flap = new ControlPlane[1, 2];
        n_t_flap = 0;
        t_flap = new ControlPlane[1, 2];
        n_canard_elevator = 0;
        canard_elevator = new ControlPlane[1, 2];
        n_powerPlant = 0;
        powerPlant = new PowerPlant[4];
        n_LandingGear = 0;
        landing_gear = new LandingGear[3, 2];
        airbrake = new AirBrake();
        interference = new Interference();
        pilot = new Pilot();
        force = new Vector3D();
        torque = new Vector3D();
        a_force = new Vector3D();
        a_torque = new Vector3D();
        f_force = new Vector3D();
        f_torque = new Vector3D();
        n_force = new Vector3D();
        n_torque = new Vector3D();
        t_force = new Vector3D();
        t_torque = new Vector3D();
        flag_landing_gear = 1;
        flag_land = 1;
        crash_point = -1;
        MakeObject();

        AirPlaneFile apsf = new AirPlaneFile(this, path);

        Init();
    }

    public void MakeObject()
    {
        int i;
        for (i = 0; i < 7; i++)
        {
            for (int lr = 0; lr <= 1; lr++)
                fuslage[i, lr] = new Fuselage();
        }
        for (i = 0; i < 1; i++)
        {
            wing[i] = new Wing();
        }
        for (i = 0; i < 1; i++)
        {
            htail[i] = new Wing();
        }
        for (i = 0; i < 1; i++)
        {
            vtail[i] = new Wing();
        }
        for (i = 0; i < 1; i++)
        {
            canard[i] = new Wing();
        }
        for (i = 0; i < 5; i++)
        {
            fin[i] = new Wing();
        }
        for (i = 0; i < 1; i++)
        {
            for (int lr_0 = 0; lr_0 <= 1; lr_0++)
                elevator[i, lr_0] = new ControlPlane();
        }
        for (i = 0; i < 1; i++)
        {
            for (int lr_1 = 0; lr_1 <= 1; lr_1++)
                aileron[i, lr_1] = new ControlPlane();
        }
        for (i = 0; i < 1; i++)
        {
            for (int lr_2 = 0; lr_2 <= 1; lr_2++)
                rudder[i, lr_2] = new ControlPlane();
        }
        for (i = 0; i < 1; i++)
        {
            for (int lr_3 = 0; lr_3 <= 1; lr_3++)
                t_flap[i, lr_3] = new ControlPlane();
        }
        for (i = 0; i < 1; i++)
        {
            for (int lr_4 = 0; lr_4 <= 1; lr_4++)
                l_flap[i, lr_4] = new ControlPlane();
        }
        for (i = 0; i < 1; i++)
        {
            for (int lr_5 = 0; lr_5 <= 1; lr_5++)
                canard_elevator[i, lr_5] = new ControlPlane();
        }
        for (i = 0; i < 3; i++)
            for (int lr_6 = 0; lr_6 <= 1; lr_6++)
                landing_gear[i, lr_6] = new LandingGear();
    }

    public void Init()
    {
        cif = new CockpitInterface();

        inp = new Inertia(plane.name);

        inp.SetBlock(0, new InertiaBlock("‹@‘Ì", plane.cg, plane.m0, plane.ixx_m0, plane.iyy_m0, plane.izz_m0, plane.ixy_m0, plane.iyz_m0, plane.izx_m0));

        pMotion = new PlaneMotion(inp, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D);
        atmos = new Isa(pMotion.wpos.y);
    }

    public void Set_pMotionLand()
    {
        Vector3D p0 = new Vector3D(landing_gear[1, 0].p0_base);
        p0.y = 0.0D;
        p0.z -= landing_gear[1, 0].stroke0;
        Vector3D p1 = new Vector3D(landing_gear[0, 0].p0_base);
        p1.y = 0.0D;
        p1.z -= landing_gear[0, 0].stroke0;
        Vector3D land = p1.Sub(p0).NmlVec();
        if (land.x < 0.0D)
        {
            land = land.SclProd(-1.0D);
        }

        Bearing br = new Bearing(land.R2l());
        double pitch = -br.pitch.GetValue();

        double h0 = p0.SclProd(-1.0D).Sub(land.SclProd(land.DotProd(p0.SclProd(-1.0D)))).Length();

        pMotion = new PlaneMotion(inp, pMotion.wpos.x, h0, pMotion.wpos.y, 0.0D, pitch, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D);

        int i;
        for (i = 0; i < n_LandingGear; i++)
        {
            for (int lr = 0; lr <= 1; lr++)
            {
                if (landing_gear[i, lr].flag == 1)
                {
                    landing_gear[i, lr].delta = 1.0D;
                }
            }
        }
        flag_landing_gear = 1;

        for (i = 0; i < n_powerPlant; i++)
        {
            if (powerPlant[i].engine_type == 1)
            {
                ((ReciprocatingEngine)powerPlant[i].engine).enginePower.output = 0.0D;
            }
        }

        for (i = 0; i < n_t_flap; i++)
        {
            t_flap[i, 0].delta = 0.0D;
            t_flap[i, 1].delta = t_flap[i, 0].delta;
        }

        cif = new CockpitInterface();
    }

    public void Update(double dt)
    {
        cif.UpdatePos(dt);

        pilot.Update(this, cif, dt);

        FlightModel(dt);

        crash_point = -1;
        if (flag_land == 0)
        {
            crash_point = Check_crash();
            if (crash_point > -1)
            {
                Set_pMotionLand();
                pMotion.wpos.x = (pMotion.wpos.z = 0.0D);
            }

        }

        pMotion.Calc_equation_of_motion(inp, force, torque, dt);

        atmos.Init(Isa.Giopotential_altitude(pMotion.wpos.y));
    }

    public static Vector3D Get_point(Vector3D right_point, int lr)
    {
        Vector3D ret = new Vector3D(right_point);

        if (lr == 1)
        {
            ret.y *= -1.0D;
        }
        if (lr == 2)
        {
            ret.y = 0.0D;
        }
        return ret;
    }

    public int Check_crash()
    {
        int ret = -1;

        int n = plane.reference_point.Length;
        for (int i = 0; i < n; i++)
        {
            Vector3D p = plane.reference_point[i].R2l().MultMat(
                    pMotion.owm);
            if (p.y >= 0.0D)
                continue;
            ret = i;
        }

        return ret;
    }

    public double Get_max_k_stall_wing()
    {
        double ret = 0.0D;

        int i;
        for (i = 0; i < n_wing; i++)
        {
            Wing wg = wing[i];
            for (int lr = 0; lr < wg.n_lr; lr++)
            {
                for (int j = 0; j < wg.n_wing_block; j++)
                {
                    WingPlane wp = wg.wp[j, lr];
                    if (wp.k_stall <= ret)
                        continue;
                    ret = wp.k_stall;
                }
            }
        }

        for (i = 0; i < n_aileron; i++)
        {
            for (int lr_0 = 0; lr_0 <= 1; lr_0++)
            {
                ControlPlane cp = aileron[i, lr_0];
                if (cp.type != 0)
                {
                    WingPlane wp_1 = cp.baseWingBlock;
                    if (wp_1.k_stall <= ret)
                        continue;
                    ret = wp_1.k_stall;
                }
            }
        }

        for (i = 0; i < n_t_flap; i++)
        {
            for (int lr_2 = 0; lr_2 <= 1; lr_2++)
            {
                ControlPlane cp_3 = t_flap[i, lr_2];
                if (cp_3.type != 0)
                {
                    WingPlane wp_4 = cp_3.baseWingBlock;
                    if (wp_4.k_stall <= ret)
                        continue;
                    ret = wp_4.k_stall;
                }
            }

        }

        for (i = 0; i < n_l_flap; i++)
        {
            for (int lr_5 = 0; lr_5 <= 1; lr_5++)
            {
                ControlPlane cp_6 = l_flap[i, lr_5];
                if (cp_6.type != 0)
                {
                    WingPlane wp_7 = cp_6.baseWingBlock;
                    if (wp_7.k_stall <= ret)
                        continue;
                    ret = wp_7.k_stall;
                }
            }
        }
        return ret;
    }

    public double Get_max_k_stall_htail()
    {
        double ret = 0.0D;

        int i;
        for (i = 0; i < n_htail; i++)
        {
            Wing wg = htail[i];
            for (int lr = 0; lr < wg.n_lr; lr++)
            {
                for (int j = 0; j < wg.n_wing_block; j++)
                {
                    WingPlane wp = wg.wp[j, lr];
                    if (wp.k_stall <= ret)
                        continue;
                    ret = wp.k_stall;
                }
            }
        }

        for (i = 0; i < n_elevator; i++)
        {
            for (int lr_0 = 0; lr_0 <= 1; lr_0++)
            {
                ControlPlane cp = elevator[i, lr_0];
                if (cp.type != 0)
                {
                    WingPlane wp_1 = cp.baseWingBlock;
                    if (wp_1.k_stall <= ret)
                        continue;
                    ret = wp_1.k_stall;
                }
            }
        }
        return ret;
    }

    public void FlightModel(double dt)
    {
        force.SetVec(0.0D, 0.0D, 0.0D);
        torque.SetVec(0.0D, 0.0D, 0.0D);

        interference.Calc_interference(this, dt);

        t_force.SetVec(0.0D, 0.0D, 0.0D);
        t_torque.SetVec(0.0D, 0.0D, 0.0D);

        a_force.SetVec(0.0D, 0.0D, 0.0D);
        a_torque.SetVec(0.0D, 0.0D, 0.0D);

        f_force.SetVec(0.0D, 0.0D, 0.0D);
        f_torque.SetVec(0.0D, 0.0D, 0.0D);

        n_force.SetVec(0.0D, 0.0D, 0.0D);
        n_torque.SetVec(0.0D, 0.0D, 0.0D);

        ControlLink(dt);

        int i;
        for (i = 0; i < n_powerPlant; i++)
        {
            powerPlant[i].Calc_dynamics(this, dt);
            t_force = t_force.Add(powerPlant[i].engine.GetForce());
            t_torque = t_torque.Add(powerPlant[i].engine.GetTorque());
        }

        for (i = 0; i < n_fuselage; i++)
        {
            for (int lr = 0; lr <= 1; lr++)
            {
                fuslage[i, lr].Calc_dynamics(lr, this, interference.fuselage_dv[i, lr]);
                a_force = a_force.Add(fuslage[i, lr].fv);
                a_torque = a_torque.Add(fuslage[i, lr].tv);
            }
        }

        for (i = 0; i < n_canard; i++)
        {
            canard[i].Calc_dynamics(this, ZERO_VECTOR_PAIR, ONE_PAIR, ONE_PAIR);
            a_force = a_force.Add(canard[i].fv);
            a_torque = a_torque.Add(canard[i].tv);
        }

        for (i = 0; i < n_canard_elevator; i++)
        {
            for (int lr_0 = 0; lr_0 <= 1; lr_0++)
            {
                canard_elevator[i, lr_0].Calc_dynamics(lr_0, this, ZERO_VECTOR, 1.0D, 1.0D);
                a_force = a_force.Add(canard_elevator[i, lr_0].d_fv);
                a_torque = a_torque.Add(canard_elevator[i, lr_0].d_tv);
            }
        }

        for (i = 0; i < n_wing; i++)
        {
            wing[i].Calc_dynamics(this, ZERO_VECTOR_PAIR, ONE_PAIR, ONE_PAIR);
            a_force = a_force.Add(wing[i].fv);
            a_torque = a_torque.Add(wing[i].tv);
        }

        for (i = 0; i < n_aileron; i++)
        {
            for (int lr_1 = 0; lr_1 <= 1; lr_1++)
            {
                aileron[i, lr_1].Calc_dynamics(lr_1, this, ZERO_VECTOR, 1.0D, 1.0D);
                a_force = a_force.Add(aileron[i, lr_1].d_fv);
                a_torque = a_torque.Add(aileron[i, lr_1].d_tv);
            }
        }

        for (i = 0; i < n_l_flap; i++)
        {
            for (int lr_2 = 0; lr_2 <= 1; lr_2++)
            {
                l_flap[i, lr_2].Calc_dynamics(lr_2, this, ZERO_VECTOR, 1.0D, 1.0D);
                a_force = a_force.Add(l_flap[i, lr_2].d_fv);
                a_torque = a_torque.Add(l_flap[i, lr_2].d_tv);
            }
        }

        for (i = 0; i < n_t_flap; i++)
        {
            for (int lr_3 = 0; lr_3 <= 1; lr_3++)
            {
                t_flap[i, lr_3].Calc_dynamics(lr_3, this, ZERO_VECTOR, 1.0D, 1.0D);
                a_force = a_force.Add(t_flap[i, lr_3].d_fv);
                a_torque = a_torque.Add(t_flap[i, lr_3].d_tv);
            }
        }

        for (i = 0; i < n_htail; i++)
        {
            htail[i].Calc_dynamics(this, interference.htail_dv,
                    interference.htail_k_q, ONE_PAIR);
            a_force = a_force.Add(htail[i].fv);
            a_torque = a_torque.Add(htail[i].tv);
        }

        for (i = 0; i < n_elevator; i++)
        {
            for (int lr_4 = 0; lr_4 <= 1; lr_4++)
            {
                elevator[i, lr_4].Calc_dynamics(lr_4, this, interference.htail_dv[lr_4], interference.htail_k_q[lr_4], 1.0D);
                a_force = a_force.Add(elevator[i, lr_4].d_fv);
                a_torque = a_torque.Add(elevator[i, lr_4].d_tv);
            }
        }

        for (i = 0; i < n_vtail; i++)
        {
            vtail[i].Calc_dynamics(this, interference.vtail_dv,
                    interference.vtail_k_q, interference.vtail_k_S);
            a_force = a_force.Add(vtail[i].fv);
            a_torque = a_torque.Add(vtail[i].tv);
        }

        for (i = 0; i < n_rudder; i++)
        {
            for (int lr_5 = 0; lr_5 <= 1; lr_5++)
            {
                rudder[i, lr_5].Calc_dynamics(lr_5, this, interference.vtail_dv[lr_5], interference.vtail_k_q[lr_5], interference.vtail_k_S[lr_5]);
                a_force = a_force.Add(rudder[i, lr_5].d_fv);
                a_torque = a_torque.Add(rudder[i, lr_5].d_tv);
            }
        }

        for (i = 0; i < n_fin; i++)
        {
            fin[i].Calc_dynamics(this, ZERO_VECTOR_PAIR, ONE_PAIR, ONE_PAIR);
            a_force = a_force.Add(fin[i].fv);
            a_torque = a_torque.Add(fin[i].tv);
        }

        max_k_stall_wing = Get_max_k_stall_wing();

        max_k_stall_htail = Get_max_k_stall_htail();

        flag_landing_gear = 1;
        flag_land = 1;
        for (i = 0; i < n_LandingGear; i++)
        {
            for (int lr_6 = 0; lr_6 <= 1; lr_6++)
            {
                if (landing_gear[i, lr_6].flag != 0)
                {
                    landing_gear[i, lr_6].Set_landling_gear_delta(lr_6,
                            cif, dt);
                    landing_gear[i, lr_6].Calc_dynamics(lr_6, this, dt);
                    n_force = n_force.Add(landing_gear[i, lr_6].n_fv);
                    n_torque = n_torque.Add(landing_gear[i, lr_6].n_tv);
                    f_force = f_force.Add(landing_gear[i, lr_6].f_fv);
                    f_torque = f_torque.Add(landing_gear[i, lr_6].f_tv);
                    a_force = a_force.Add(landing_gear[i, lr_6].a_fv);
                    a_torque = a_torque.Add(landing_gear[i, lr_6].a_tv);
                    if (landing_gear[i, lr_6].delta != 1.0D)
                    {
                        flag_landing_gear = 0;
                    }
                    if (landing_gear[i, lr_6].flag_land != 1)
                    {
                        flag_land = 0;
                    }
                }

            }

        }

        force = force.Add(t_force);
        torque = torque.Add(t_torque);

        force = force.Add(n_force);
        torque = torque.Add(n_torque);

        if (pMotion.vc.Length() >= 0.1D)
        {
            force = force.Add(a_force);
            torque = torque.Add(a_torque);
            force = force.Add(f_force);
            torque = torque.Add(f_torque);
        }
        else if (cif.throttle_pos > 0.0D)
        {
            force = force.Add(a_force);
            torque = torque.Add(a_torque);
        }
        else if (flag_land == 1)
        {
            pMotion.vc.SetVec(0.0D, 0.0D, 0.0D);
            pMotion.omega.SetVec(0.0D, 0.0D, 0.0D);
        }
    }

    public void ControlLink(double dt)
    {
        int i;
        for (i = 0; i < n_elevator; i++)
        {
            if (elevator[i, 0].type != 0)
            {
                ControlPlane ele = elevator[i, 0];

                ele.delta = ((1.0D - cif.stick_pos_y) / 2.0D * (ele.delta_max - ele.delta_min) + ele.delta_min);
            }
            if (elevator[i, 1].type != 0)
            {
                elevator[i, 1].delta = elevator[i, 0].delta;
            }
        }

        for (i = 0; i < n_canard_elevator; i++)
        {
            if (canard_elevator[i, 0].type != 0)
            {
                if (cif.stick_pos_y > 0.0D)
                    canard_elevator[i, 0].delta = (cif.stick_pos_y * canard_elevator[i, 0].delta_min);
                else
                {
                    canard_elevator[i, 0].delta = (-cif.stick_pos_y * canard_elevator[i, 0].delta_max);
                }
            }
            if (canard_elevator[i, 1].type != 0)
            {
                canard_elevator[i, 1].delta = canard_elevator[i, 0].delta;
            }
        }

        for (i = 0; i < n_aileron; i++)
        {
            if (aileron[i, 0].type != 0)
            {
                if (cif.stick_pos_x >= 0.0D)
                    aileron[i, 0].delta = (cif.stick_pos_x * aileron[i, 0].delta_min);
                else
                {
                    aileron[i, 0].delta = (-cif.stick_pos_x * aileron[i, 0].delta_max);
                }
            }
            if (aileron[i, 1].type != 0)
            {
                if (cif.stick_pos_x >= 0.0D)
                    aileron[i, 1].delta = (cif.stick_pos_x * aileron[i, 1].delta_max);
                else
                {
                    aileron[i, 1].delta = (-cif.stick_pos_x * aileron[i, 1].delta_min);
                }
            }
        }

        for (i = 0; i < n_rudder; i++)
        {
            if (rudder[i, 0].type != 0)
            {
                if (cif.rudder_pos > 0.0D)
                {
                    rudder[i, 0].delta = (-cif.frudder_pos.output * rudder[i, 0].delta_min);
                }
                else
                {
                    rudder[i, 0].delta = (cif.frudder_pos.output * rudder[i, 0].delta_max);
                }
            }
            if (rudder[i, 1].type != 0)
            {
                rudder[i, 1].delta = rudder[i, 1].delta;
            }
        }

        for (i = 0; i < n_LandingGear; i++)
        {
            if (landing_gear[i, 0].flag != 0)
            {
                landing_gear[i, 0].brake = cif.brakeRight_pos;
            }
            if (landing_gear[i, 1].flag != 0)
            {
                landing_gear[i, 1].brake = cif.brakeLeft_pos;
            }
        }

        for (i = 0; i < n_t_flap; i++)
        {
            t_flap[i, 0].Update(cif.flap_sw, dt);
            t_flap[i, 1].delta = t_flap[i, 0].delta;
        }

        for (i = 0; i < n_powerPlant; i++)
            powerPlant[i].throttle = (1.0D - System.Math.Cos(cif.throttle_pos * Math.PI * 0.5D));
    }
}