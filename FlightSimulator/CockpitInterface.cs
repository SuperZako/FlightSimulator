

    using Jp.Maker1.Sim.Tools;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

public class CockpitInterface
{
    public CockpitInterface()
    {
        stick_mode = 0;
        stick_pos_x = 0.0D;
        stick_pos_y = 0.0D;
        rudder_pos = 0.0D;
        RUDDER_MOVE_SPEED = 1.0D;
        key_rudder_left = (Keys)37;
        key_rudder_right = (Keys)39;
        key_rudder_center = (Keys)40;
        rudder_sw = 0;
        frudder_pos = new LowPassFilter1(0.0D);
        throttle_pos = 0.0D;
        THROTTLE_MOVE_SPEED = 0.3D;
        key_throttle_up = (Keys)65;
        key_throttle_down = (Keys)90;
        key_throttle_full = (Keys)81;
        key_throttle_0p = (Keys)49;
        key_throttle_10p = (Keys)50;
        key_throttle_20p = (Keys)51;
        key_throttle_30p = (Keys)52;
        key_throttle_40p = (Keys)53;
        key_throttle_50p = (Keys)54;
        key_throttle_60p = (Keys)55;
        key_throttle_70p = (Keys)56;
        key_throttle_80p = (Keys)57;
        key_throttle_90p = (Keys)48;
        key_throttle_100p = (Keys)189;
        throttle_sw = 0;
        mixture_pos = 0.0D;
        MIXTURE_MOVE_SPEED = 0.5D;
        key_mixture_rich = (Keys)83;
        key_mixture_lean = (Keys)88;
        key_mixture_full_rich = (Keys)87;
        mixture_sw = 0;
        prop_pitch_pos = 0.0D;
        PROPPITCH_MOVE_SPEED = 0.5D;
        key_prop_pitch_low = (Keys)68;
        key_prop_pitch_high = (Keys)67;
        key_prop_pitch_max_low = (Keys)69;
        prop_pitch_sw = 0;
        flap_sw = 0;
        key_flap_down = (Keys)70;
        key_flap_up = (Keys)86;
        landing_gear_sw = -1;
        landing_gear_counter = 0.0D;
        key_landing_gear_sw = (Keys)71;
        BRAKE_UP_MOVE_SPEED = 0.5D;
        BRAKE_REL_MOVE_SPEED = 5.0D;
        brakeLeft_pos = 0.0D;
        brakeRight_pos = 0.0D;
        brakeLeft_sw = 0;
        brakeRight_sw = 0;
        key_brake_left = (Keys)191;
        key_brake_right = (Keys)226;
        key_front = (Keys)104;
        key_front_left = (Keys)103;
        key_left = (Keys)100;
        key_rear_left = (Keys)97;
        key_rear = (Keys)98;
        key_rear_right = (Keys)99;
        key_right = (Keys)102;
        key_front_right = (Keys)105;
        key_upper = (Keys)101;
        view_direction = 0;
        view_upper = 0;
        key_padLock = (Keys)96;
        key_padLock_next = (Keys)107;
        key_padLock_prev = (Keys)109;
        padLock_sw = 0;
    }

    public int stick_mode;
    public double stick_pos_x;
    public double stick_pos_y;

    public double rudder_pos;
    public readonly double RUDDER_MOVE_SPEED;
    public Keys key_rudder_left;
    public Keys key_rudder_right;
    public Keys key_rudder_center;
    public int rudder_sw;
    public LowPassFilter1 frudder_pos;

    public double throttle_pos;

    public readonly double THROTTLE_MOVE_SPEED;
    public Keys key_throttle_up;
    public Keys key_throttle_down;
    public Keys key_throttle_full;
    public Keys key_throttle_0p;
    public Keys key_throttle_10p;
    public Keys key_throttle_20p;
    public Keys key_throttle_30p;
    public Keys key_throttle_40p;
    public Keys key_throttle_50p;
    public Keys key_throttle_60p;
    public Keys key_throttle_70p;
    public Keys key_throttle_80p;
    public Keys key_throttle_90p;
    public Keys key_throttle_100p;
    public int throttle_sw;

    public double mixture_pos;
    public readonly double MIXTURE_MOVE_SPEED;
    public Keys key_mixture_rich;
    public Keys key_mixture_lean;
    public Keys key_mixture_full_rich;
    public int mixture_sw;

    public double prop_pitch_pos;
    public readonly double PROPPITCH_MOVE_SPEED;
    public Keys key_prop_pitch_low;
    public Keys key_prop_pitch_high;
    public Keys key_prop_pitch_max_low;
    public int prop_pitch_sw;

    public int flap_sw;
    public Keys key_flap_down;
    public Keys key_flap_up;

    public int landing_gear_sw;
    public double landing_gear_counter;
    public Keys key_landing_gear_sw;

    public readonly double BRAKE_UP_MOVE_SPEED;
    public readonly double BRAKE_REL_MOVE_SPEED;
    public double brakeLeft_pos;
    public double brakeRight_pos;
    public int brakeLeft_sw;
    public int brakeRight_sw;
    public Keys key_brake_left;
    public Keys key_brake_right;

    public Keys key_front;
    public Keys key_front_left;
    public Keys key_left;
    public Keys key_rear_left;
    public Keys key_rear;
    public Keys key_rear_right;
    public Keys key_right;
    public Keys key_front_right;
    public Keys key_upper;
    public const int VIEW_DIR_NONE = 0;
    public const int VIEW_DIR_F_ = 1;
    public const int VIEW_DIR_FL = 2;
    public const int VIEW_DIR__L = 3;
    public const int VIEW_DIR_RL = 4;
    public const int VIEW_DIR_R_ = 5;
    public const int VIEW_DIR_RR = 6;
    public const int VIEW_DIR__R = 7;
    public const int VIEW_DIR_FR = 8;
    public int view_direction;
    public int view_upper;

    public Keys key_padLock;
    public Keys key_padLock_next;
    public Keys key_padLock_prev;
    public int padLock_sw;

    public void Change_stick_mode()
    {
        if (stick_mode == 0)
            stick_mode = 1;
        else
            stick_mode = 0;
    }

    public void Set_stick_pos_x(double p)
    {
        stick_pos_x = p;
        if (stick_pos_x > 1.0D)
            stick_pos_x = 1.0D;
        if (stick_pos_x < -1.0D)
            stick_pos_x = -1.0D;
    }

    public void Set_stick_pos_y(double p)
    {
        stick_pos_y = p;
        if (stick_pos_y > 1.0D)
            stick_pos_y = 1.0D;
        if (stick_pos_y < -1.0D)
            stick_pos_y = -1.0D;
    }

    public void UpdatePos(double dt)
    {
        rudder_pos = UpdatePos(rudder_pos, rudder_sw, dt, -1.0D, 1.0D, 1.0D, 1.0D);
        frudder_pos.Update(rudder_pos, dt);

        throttle_pos = UpdatePos(throttle_pos, throttle_sw, dt, 0.0D, 1.0D, 0.3D, 0.3D);

        mixture_pos = UpdatePos(mixture_pos, mixture_sw, dt, 0.0D, 1.0D, 0.5D, 0.5D);

        prop_pitch_pos = UpdatePos(prop_pitch_pos, prop_pitch_sw, dt, 0.0D, 1.0D, 0.5D, 0.5D);

        brakeLeft_pos = UpdatePos(brakeLeft_pos, brakeLeft_sw, dt, 0.0D, 1.0D, 0.5D, 5.0D);
        brakeRight_pos = UpdatePos(brakeRight_pos, brakeRight_sw, dt, 0.0D, 1.0D, 0.5D, 5.0D);

        landing_gear_counter += dt;
    }

    public double UpdatePos(double pos, int sw, double dt, double pmin, double pmax, double upSpeed, double downSpeed)
    {
        double p = pos;
        if (sw == -1)
        {
            p -= downSpeed * dt;
            if (p < pmin)
                p = pmin;
        }
        if (sw == 1)
        {
            p += upSpeed * dt;
            if (p > pmax)
                p = pmax;
        }
        if (sw == 3)
        {
            if (p < 0.0D)
            {
                p += upSpeed * dt;
                if (p > 0.0D)
                    p = 0.0D;
            }
            if (p > 0.0D)
            {
                p -= upSpeed * dt;
                if (p < 0.0D)
                    p = 0.0D;
            }
        }
        return p;
    }

}