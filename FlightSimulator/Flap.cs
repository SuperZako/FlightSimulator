    using Jp.Maker1.Sim.Tools;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;

    // Referenced classes of package jp.maker1.fsim:
    //            WingPlane

public class Flap
{

    public const int PLANE_FLAP = 1;
    public const int SPLIT_FLAP = 2;
    public const int SLOTTED_FLAP = 3;
    public static readonly String[] flap_type_name = { "none", "plane_flap",
				"split_flap", "slotted_flap" };
    public const int FORWORD = 1;
    public const int BACK = 1;
    private static double[] a_a6_ar = 
    {
        1.9790000000000001D,
        2.2501000000000002D,
        2.5556999999999999D, 
        2.9310999999999998D, 
        3.2886000000000002D,
        3.7332000000000001D,
        4.1863999999999999D,
        4.6044D,
        5.1528D, 
        5.6052999999999997D,
        6.1759000000000004D,
        6.7933000000000003D,
        7.4019000000000004D,
        8.0365000000000002D,
        9.0326000000000004D,
        11.045199999999999D,
        11.9975D
    };

    private static double[] a_a6_a_a6 = 
    {
        0.56969999999999998D,
        0.62939999999999996D, 
        0.67879999999999996D, 
        0.73680000000000001D,
        0.78110000000000002D,
        0.83230000000000004D, 
        0.87829999999999997D,
        0.91239999999999999D,
        0.94989999999999997D,
        0.97719999999999996D,
        1.0038D,
        1.0277000000000001D,
        1.0464D, 
        1.0634999999999999D,
        1.0891D,
        1.1266D,
        1.1385000000000001D 
    };

    public static Function2D a_a6;
    private static double[] lamda1_c_cf = { 0, 0.017600000000000001D, 0.0385D,
				0.070900000000000005D, 0.10829999999999999D, 0.14560000000000001D,
				0.1744D, 0.21199999999999999D, 0.25340000000000001D,
				0.30990000000000001D, 0.37990000000000002D, 0.45150000000000001D,
				0.53410000000000002D, 0.6159D, 0.69389999999999996D,
				0.77449999999999997D, 0.85009999999999997D, 0.9244D, 1.0001D };
    private static double[] lamda1_lamda1 = { 0, 0.15290000000000001D,
				0.25069999999999998D, 0.34239999999999998D, 0.42659999999999998D,
				0.49170000000000003D, 0.53380000000000005D, 0.57969999999999999D,
				0.62680000000000002D, 0.67769999999999997D, 0.73550000000000004D,
				0.79010000000000002D, 0.84540000000000004D, 0.88890000000000002D,
				0.92369999999999997D, 0.9536D, 0.97609999999999997D,
				0.99239999999999995D, 1.0D };
    public static Function2D lamda1;
    private static double[] lamda2_planeFlap_delta = { 0, 11.7454D,
				16.046099999999999D, 20.9939D, 24.880400000000002D,
				29.730599999999999D, 32.954999999999998D, 38.659199999999998D,
				43.784300000000002D, 50.562199999999997D, 57.724800000000002D,
				63.014200000000002D, 68.633799999999994D, 74.335800000000006D,
				80.781099999999995D, 86.234399999999994D, 100.25190000000001D };
    private static double[] lamda2_planeFlap_lamda2 = { 0,
				0.50139999999999996D, 0.65339999999999998D, 0.81279999999999997D,
				0.92989999999999995D, 1.056D, 1.1299999999999999D,
				1.2431000000000001D, 1.3294999999999999D, 1.4303999999999999D,
				1.5176000000000001D, 1.5690999999999999D, 1.6125D, 1.6455D,
				1.6662999999999999D, 1.6707000000000001D, 1.6507000000000001D };
    public static Function2D lamda2_planeFlap;
    private static double[] lamda2_splitFlap_tc012_delta = { 0,
				7.4806999999999997D, 13.273300000000001D, 18.636800000000001D,
				26.052800000000001D, 31.015000000000001D, 38.870399999999997D,
				46.531100000000002D, 51.7393D, 59.0137D, 66.287199999999999D,
				72.540099999999995D, 77.250600000000006D, 81.960899999999995D,
				86.009900000000002D, 100.2214D };
    private static double[] lamda2_splitFlap_tc012_lamda2 = { 0,
				0.44230000000000003D, 0.76270000000000004D, 1.0164D,
				1.3059000000000001D, 1.4763999999999999D, 1.6983999999999999D,
				1.8726D, 1.9754D, 2.1009000000000002D, 2.1956000000000002D,
				2.2541000000000002D, 2.2871000000000001D, 2.3119000000000001D,
				2.3264999999999998D, 2.3311999999999999D };
    private static Function2D lamda2_splitFlap_tc012;
    private static double[] lamda2_splitFlap_tc021_delta = { 0,
				5.5004999999999997D, 9.8023000000000007D, 14.186D, 19.148D,
				25.404499999999999D, 30.4481D, 36.648499999999999D, 42.82D,
				48.7714D, 56.2102D, 62.559899999999999D, 70.080200000000005D,
				75.7821D, 81.070400000000006D, 86.854299999999995D, 100.3621D };
    private static double[] lamda2_splitFlap_tc021_lamda2 = { 0, 0.2606D,
				0.45579999999999998D, 0.62419999999999998D, 0.78859999999999997D,
				0.9768D, 1.1042000000000001D, 1.2337D, 1.3376999999999999D,
				1.4261999999999999D, 1.5188999999999999D, 1.5813999999999999D,
				1.6372D, 1.6660999999999999D, 1.6807000000000001D, 1.6871D, 1.6733D };
    private static Function2D lamda2_splitFlap_tc021;
    private static double[] lamda2_splitFlap_t_c = { 0.12D,
				0.20999999999999999D };
    private static Function2D[] lamda2_splitFlap_lamda2;
    public static Function3D lamda2_splitFlap;
    private static double[] lamda2_slottedFlap_tc012_delta = { 0,
				6.3601000000000001D, 11.0776D, 15.366099999999999D,
				19.750800000000002D, 24.285900000000002D, 28.668900000000001D,
				34.938400000000001D, 39.071800000000003D, 43.782800000000002D,
				48.658700000000003D, 52.872799999999998D, 54.525300000000001D,
				55.764600000000002D, 58.655799999999999D, 100.3002D };
    private static double[] lamda2_slottedFlap_tc012_lamda2 = { 0, 0.4219D,
				0.70940000000000003D, 0.92859999999999998D, 1.1319999999999999D,
				1.3097000000000001D, 1.4515D, 1.5982000000000001D,
				1.6805000000000001D, 1.732D, 1.7690999999999999D,
				1.7796000000000001D, 1.7796000000000001D, 1.7776000000000001D,
				1.7552000000000001D, 1.3711D };
    private static Function2D lamda2_slottedFlap_tc012;
    private static double[] lamda2_slottedFlap_tc021_delta = { 0,
				6.1479999999999997D, 11.030799999999999D, 18.132100000000001D,
				22.915600000000001D, 26.636199999999999D, 30.3565D,
				34.572299999999998D, 38.456899999999997D, 43.250300000000003D,
				47.4649D, 53.166600000000003D, 58.370699999999999D, 100.3438D };
    private static double[] lamda2_slottedFlap_tc021_lamda2 = { 0,
				0.41930000000000001D, 0.70889999999999997D, 1.0616000000000001D,
				1.2537D, 1.3462000000000001D, 1.4244000000000001D, 1.4964D,
				1.5417000000000001D, 1.585D, 1.6138999999999999D, 1.6388D,
				1.6540999999999999D, 1.6557999999999999D };
    private static Function2D lamda2_slottedFlap_tc021;
    private static double[] lamda2_slottedFlap_t_c = { 0.12D,
				0.20999999999999999D };
    private static Function2D[] lamda2_slottedFlap_lamda2;
    public static Function3D lamda2_slottedFlap;
    private static double[] mu1_tc012_c_cf = { 0.099099999999999994D,
				0.14410000000000001D, 0.18290000000000001D, 0.22650000000000001D,
				0.26179999999999998D, 0.29430000000000001D, 0.32779999999999998D,
				0.3584D, 0.38119999999999998D, 0.39960000000000001D };
    private static double[] mu1_tc012_mu1 = { -0.35339999999999999D,
				-0.32579999999999998D, -0.3039D, -0.2802D, -0.26319999999999999D,
				-0.24909999999999999D, -0.23780000000000001D,
				-0.22950000000000001D, -0.22459999999999999D, -0.22359999999999999D };
    private static Function2D mu1_tc012;
    private static double[] mu1_tc021_c_cf = { 0.099000000000000005D,
				0.15240000000000001D, 0.19220000000000001D, 0.23200000000000001D,
				0.27429999999999999D, 0.3175D, 0.36409999999999998D,
				0.38929999999999998D, 0.39950000000000002D };
    private static double[] mu1_tc021_mu1 = { -0.28199999999999997D,
				-0.26879999999999998D, -0.25950000000000001D,
				-0.25019999999999998D, -0.24179999999999999D,
				-0.23350000000000001D, -0.2261D, -0.22359999999999999D,
				-0.22359999999999999D };
    private static Function2D mu1_tc021;
    private static double[] mu1_tc030_c_cf = { 0.0998D, 0.14929999999999999D,
				0.19400000000000001D, 0.24460000000000001D, 0.28639999999999999D,
				0.33110000000000001D, 0.36699999999999999D, 0.39960000000000001D };
    private static double[] mu1_tc030_mu1 = { -0.25359999999999999D, -0.2462D,
				-0.2407D, -0.23519999999999999D, -0.23069999999999999D,
				-0.22670000000000001D, -0.22420000000000001D, -0.22259999999999999D };
    private static Function2D mu1_tc030;
    private static double[] mu1_t_c = { 0.12D, 0.20999999999999999D,
				0.29999999999999999D };
    private static Function2D[] mu1_mu1;
    public static Function3D mu1;
    private static double[] delta1_pFsF_tc012_c_cf = { 0,
				0.038800000000000001D, 0.082299999999999998D, 0.12429999999999999D,
				0.1895D, 0.2571D, 0.2858D, 0.31890000000000002D, 0.3584D, 0.3826D,
				0.39800000000000002D };
    private static double[] delta1_pFsF_tc012_delta1 = { 0, 0.1396D,
				0.33900000000000002D, 0.56540000000000001D, 0.94569999999999999D,
				1.4069D, 1.5791999999999999D, 1.7809999999999999D, 1.9943D,
				2.1124999999999998D, 2.1789000000000001D };
    private static Function2D delta1_pFsP_tc012;
    private static double[] delta1_pFsF_tc021_c_cf = { 0, 0.0562D, 0.1071D,
				0.1525D, 0.19850000000000001D, 0.25080000000000002D,
				0.30170000000000002D, 0.3367D, 0.36990000000000001D,
				0.38769999999999999D, 0.39850000000000002D };
    private static double[] delta1_pFsF_tc021_delta1 = { 0, 0.2147D,
				0.47310000000000002D, 0.72750000000000004D, 1.0032000000000001D,
				1.3363D, 1.6391D, 1.8183D, 1.9709000000000001D,
				2.0497000000000001D, 2.0916000000000001D };
    private static Function2D delta1_pFsP_tc021;
    private static double[] delta1_pFsF_tc030_c_cf = { 0, 0.0562D, 0.1071D,
				0.1525D, 0.19850000000000001D, 0.2601D, 0.30459999999999998D,
				0.3362D, 0.36730000000000002D, 0.39850000000000002D };
    private static double[] delta1_pFsF_tc030_delta1 = { 0, 0.2147D,
				0.47310000000000002D, 0.72750000000000004D, 1.0032000000000001D,
				1.4564999999999999D, 1.8010999999999999D, 2.0264000000000002D,
				2.238D, 2.4422999999999999D };
    private static Function2D delta1_pFsP_tc030;
    private static double[] delta1_pFsF_t_c = { 0.12D, 0.20999999999999999D,
				0.29999999999999999D };
    private static Function2D[] delta1_pFsF_delta1;
    public static Function3D delta1_pFsF;
    private static double[] delta1_slottedFlap_tc012030_c_cf = { 0, 0.0562D,
				0.1071D, 0.1525D, 0.19850000000000001D, 0.2601D,
				0.29659999999999997D, 0.31640000000000001D, 0.33019999999999999D,
				0.35099999999999998D, 0.37169999999999997D, 0.38450000000000001D,
				0.39100000000000001D };
    private static double[] delta1_slottedFlap_tc012030_delta1 = { 0, 0.2147D,
				0.47310000000000002D, 0.72750000000000004D, 1.0032000000000001D,
				1.4564999999999999D, 1.7506999999999999D, 1.9574D,
				2.1076000000000001D, 2.3881999999999999D, 2.6627000000000001D,
				2.8767999999999998D, 3.0019D };
    private static Function2D delta1_slottedFlap_tc012030;
    private static double[] delta1_slottedFlap_tc021_c_cf = { 0, 0.0562D,
				0.1071D, 0.1525D, 0.19850000000000001D, 0.2601D, 0.3044D, 0.3306D,
				0.35589999999999999D, 0.37409999999999999D, 0.39929999999999999D };
    private static double[] delta1_slottedFlap_tc021_delta1 = { 0, 0.2147D,
				0.47310000000000002D, 0.72750000000000004D, 1.0032000000000001D,
				1.4564999999999999D, 1.8111999999999999D, 2.0350999999999999D,
				2.2616000000000001D, 2.4331D, 2.6964999999999999D };
    private static Function2D delta1_slottedFlap_tc021;
    private static double[] delta1_slottedFlap_t_c = { 0.12D,
				0.20999999999999999D, 0.29999999999999999D };
    private static Function2D[] delta1_slottedFlap_delta1;
    public static Function3D delta1_slottedFlap;
    private static double[] delta2_planeFlap_delta = { 0, 5.7340999999999998D,
				9.7570999999999994D, 13.9015D, 18.167000000000002D,
				24.381699999999999D, 29.925599999999999D, 33.072600000000001D,
				37.031999999999996D, 49.053100000000001D, 54.229700000000001D,
				61.929600000000001D, 65.888400000000004D };
    private static double[] delta2_planeFlap_delta2 = { 0,
				0.0054999999999999997D, 0.011299999999999999D,
				0.018200000000000001D, 0.027D, 0.041500000000000002D,
				0.055300000000000002D, 0.064600000000000005D,
				0.075600000000000001D, 0.11360000000000001D, 0.13020000000000001D,
				0.1545D, 0.1668D };
    public static Function2D delta2_planeFlap;
    private static double[] delta2_splitFlap_tc012_delta = { 0,
				5.1994999999999996D, 9.8719000000000001D, 13.7311D,
				17.589500000000001D, 22.767700000000001D, 30.1784D,
				37.487000000000002D, 47.824300000000001D, 53.811399999999999D,
				58.988D, 68.820099999999996D, 73.997699999999995D,
				77.899500000000003D, 79.8292D, 83.893100000000004D,
				86.636499999999998D };
    private static double[] delta2_splitFlap_tc012_delta2 = { 0, 0.0066D,
				0.0149D, 0.0229D, 0.032800000000000003D, 0.046100000000000002D,
				0.067699999999999996D, 0.090399999999999994D, 0.1265D, 0.1489D,
				0.16550000000000001D, 0.1953D, 0.2099D, 0.21990000000000001D,
				0.22370000000000001D, 0.22900000000000001D, 0.23200000000000001D };
    private static Function2D delta2_splitFlap_tc012;
    private static double[] delta2_splitFlap_tc021_delta = { 0,
				7.3707000000000003D, 11.6378D, 15.8027D, 19.865200000000002D,
				24.029199999999999D, 27.075299999999999D, 31.6432D, 34.2239D,
				43.561700000000002D, 52.899000000000001D, 58.524500000000003D,
				63.295000000000002D, 68.573300000000003D, 74.809200000000004D,
				78.870800000000003D, 82.527799999999999D, 85.981899999999996D };
    private static double[] delta2_splitFlap_tc021_delta2 = { 0,
				0.0058999999999999999D, 0.0114D, 0.017999999999999999D,
				0.025999999999999999D, 0.034599999999999999D,
				0.042099999999999999D, 0.0562D, 0.065199999999999994D,
				0.096199999999999994D, 0.12809999999999999D, 0.1474D,
				0.16300000000000001D, 0.17960000000000001D, 0.19789999999999999D,
				0.20799999999999999D, 0.21379999999999999D, 0.21859999999999999D };
    private static Function2D delta2_splitFlap_tc021;
    private static double[] delta2_splitFlap_tc030_delta = { 0,
				4.1318999999999999D, 8.6034000000000006D, 13.6837D,
				19.982199999999999D, 26.4832D, 31.764199999999999D,
				38.002000000000002D, 43.1798D, 48.458300000000001D,
				56.215699999999998D, 62.509099999999997D, 71.993300000000005D,
				77.373800000000003D, 80.825999999999993D, 84.177400000000006D,
				86.513800000000003D };
    private static double[] delta2_splitFlap_tc030_delta2 = { 0,
				0.0011000000000000001D, 0.0044000000000000003D,
				0.0099000000000000008D, 0.019199999999999998D,
				0.030300000000000001D, 0.041300000000000003D,
				0.055800000000000002D, 0.069900000000000004D,
				0.086300000000000002D, 0.1116D, 0.1313D, 0.15989999999999999D,
				0.17519999999999999D, 0.18410000000000001D, 0.19109999999999999D,
				0.19489999999999999D };
    private static Function2D delta2_splitFlap_tc030;
    private static double[] delta2_splitFlap_t_c = { 0.12D,
				0.20999999999999999D, 0.29999999999999999D };
    private static Function2D[] delta2_splitFlap_delta2;
    public static Function3D delta2_splitFlap;
    private static double[] delta2_slottedFlap_tc012_delta = { 0,
				9.1372999999999998D, 14.7272D, 21.637499999999999D,
				28.241800000000001D, 33.625700000000002D, 38.704500000000003D,
				45.102699999999999D, 49.672800000000002D, 53.532400000000003D,
				59.423699999999997D, 60.744599999999998D };
    private static double[] delta2_slottedFlap_tc012_delta2 = { 0,
				0.00089999999999999998D, 0.0038999999999999998D,
				0.0094999999999999998D, 0.017000000000000001D,
				0.025600000000000001D, 0.0344D, 0.047699999999999999D,
				0.057299999999999997D, 0.064500000000000002D,
				0.074899999999999994D, 0.076399999999999996D };
    private static Function2D delta2_slottedFlap_tc012;
    private static double[] delta2_slottedFlap_tc021_delta = { 0, 10.9655D,
				17.266100000000002D, 23.159099999999999D, 30.474299999999999D,
				35.045999999999999D, 45.001899999999999D, 51.097000000000001D,
				56.684199999999997D, 59.934899999999999D };
    private static double[] delta2_slottedFlap_tc021_delta2 = { 0,
				0.0044999999999999997D, 0.0094999999999999998D,
				0.016299999999999999D, 0.0253D, 0.031600000000000003D,
				0.045900000000000003D, 0.055199999999999999D,
				0.063799999999999996D, 0.0688D };
    private static Function2D delta2_slottedFlap_tc021;
    private static double[] delta2_slottedFlap_tc030_delta = { 0, 10.9655D,
				17.266100000000002D, 23.159099999999999D, 29.4575D,
				35.145400000000002D, 43.675699999999999D, 49.463799999999999D,
				55.049500000000002D, 58.198099999999997D, 62.565600000000003D };
    private static double[] delta2_slottedFlap_tc030_delta2 = { 0,
				0.0044999999999999997D, 0.0094999999999999998D,
				0.016299999999999999D, 0.0258D, 0.036400000000000002D,
				0.055300000000000002D, 0.0688D, 0.080399999999999999D,
				0.086400000000000005D, 0.094500000000000001D };
    private static Function2D delta2_slottedFlap_tc030;
    private static double[] delta2_slottedFlap_t_c = { 0.12D,
				0.20999999999999999D, 0.29999999999999999D };
    private static Function2D[] delta2_slottedFlap_delta2;
    public static Function3D delta2_slottedFlap;
    private static double[] ka_cfc1_delta = { -0.067599999999999993D, 17.471D,
				24.3142D, 29.364000000000001D, 36.075200000000002D,
				42.561700000000002D, 49.074399999999997D, 58.322000000000003D,
				66.6965D, 74.145899999999997D, 81.058800000000005D,
				86.509600000000006D, 89.966300000000004D };
    private static double[] ka_cfc1_ka = { 1.0D, 1.0D, 0.99309999999999998D,
				0.98370000000000002D, 0.96579999999999999D, 0.94269999999999998D,
				0.91620000000000001D, 0.87260000000000004D, 0.82479999999999998D,
				0.77610000000000001D, 0.72919999999999996D, 0.68899999999999995D,
				0.66169999999999995D };
    private static Function2D ka_cfc1;
    private static double[] ka_cfc2_delta = { -0.067599999999999993D,
				12.989599999999999D, 18.3047D, 22.679200000000002D,
				27.386199999999999D, 31.1845D, 35.6706D, 41.274099999999997D,
				46.785200000000003D, 51.106000000000002D, 60.201099999999997D,
				64.421000000000006D, 69.075400000000002D, 76.399199999999993D,
				83.690700000000007D, 90.008600000000001D };
    private static double[] ka_cfc2_ka = { 1.0D, 1.0003D, 0.99529999999999996D,
				0.98460000000000003D, 0.96830000000000005D, 0.95189999999999997D,
				0.9284D, 0.89470000000000005D, 0.85589999999999999D,
				0.82250000000000001D, 0.74219999999999997D, 0.70240000000000002D,
				0.65190000000000003D, 0.56799999999999995D, 0.48199999999999998D,
				0.39879999999999999D };
    private static Function2D ka_cfc2;
    private static double[] ka_cfc3_delta = { -0.067599999999999993D,
				9.9928000000000008D, 14.5327D, 19.294699999999999D,
				24.389600000000002D, 28.820399999999999D, 33.482500000000002D,
				37.868000000000002D, 42.133800000000001D, 45.624299999999998D,
				50.343299999999999D, 56.881900000000002D, 61.268900000000002D,
				65.0929D, 69.249600000000001D, 73.914500000000004D,
				77.295500000000004D, 82.237899999999996D, 85.840900000000005D,
				90.054000000000002D };
    private static double[] ka_cfc3_ka = { 1.0D, 1.0004999999999999D,
				0.99690000000000001D, 0.98560000000000003D, 0.96560000000000001D,
				0.94220000000000004D, 0.9123D, 0.87880000000000003D,
				0.84260000000000002D, 0.80769999999999997D, 0.75800000000000001D,
				0.68469999999999998D, 0.63249999999999995D, 0.58199999999999996D,
				0.52510000000000001D, 0.45910000000000001D, 0.40999999999999998D,
				0.33579999999999999D, 0.27889999999999998D, 0.20910000000000001D };
    private static Function2D ka_cfc3;
    private static double[] ka_cfc4_delta = { -0.067599999999999993D,
				9.3210999999999995D, 14.138D, 19.509399999999999D,
				24.106200000000001D, 28.038699999999999D, 32.692100000000003D,
				37.863100000000003D, 42.073999999999998D, 46.008400000000002D,
				54.274999999999999D, 58.607300000000002D, 63.960799999999999D,
				67.785600000000002D, 72.252799999999993D, 75.634500000000003D,
				81.076700000000002D, 85.655500000000004D, 90.036100000000005D };
    private static double[] ka_cfc4_ka = { 1.0D, 1.0005999999999999D,
				0.99490000000000001D, 0.97850000000000004D, 0.95720000000000005D,
				0.93369999999999997D, 0.89670000000000005D, 0.84950000000000003D,
				0.80610000000000004D, 0.7591D, 0.65459999999999996D,
				0.59350000000000003D, 0.51280000000000003D, 0.45279999999999998D,
				0.37819999999999998D, 0.32050000000000001D, 0.22670000000000001D,
				0.1411D, 0.0533D };
    private static Function2D ka_cfc4;
    private static double[] ka_cfc = { 0.10000000000000001D,
				0.20000000000000001D, 0.29999999999999999D, 0.40000000000000002D };
    private static Function2D[] ka_ka;
    public static Function3D ka;

    public Flap()
    {
    }

    public static double Beta_f(int lr, int hv_arrangement, double lamda_h,
            double beta)
    {
        double beta_f;
        if (hv_arrangement == 0)
        {
            if (lr == 0)
                beta_f = beta - lamda_h;
            else
                beta_f = beta + lamda_h;
        }
        else
        {
            beta_f = beta - lamda_h;
        }
        return beta_f;
    }

    public static double Delta_dash(double delta, double beta_f)
    {
        return System.Math.Atan2(System.Math.Sin(delta) * System.Math.Cos(beta_f), System.Math.Cos(delta));
    }

    public static double A_a6_calc(double a0, double ar, double lamda)
    {
        double a = WingPlane.Calc_cla_subsonic(a0, 0.0D, ar, lamda);
        double a6 = WingPlane.Calc_cla_subsonic(a0, 0.0D, 6D, lamda);
        return a / a6;
    }

    public static double Lamda1_t_flap(double cf_c)
    {
        double theta = System.Math.Acos(-1D + 2D * cf_c);
        return 1.0D + (-theta + System.Math.Sin(theta)) / 3.1415926535897931D;
    }

    public static double Lamda1_l_flap(double cf_c)
    {
        double theta = System.Math.Acos(1.0D - 2D * cf_c);
        return (-theta + System.Math.Sin(theta)) / 3.1415926535897931D;
    }

    public static double Cmac_t_flap(double cf_c)
    {
        double theta = System.Math.Acos(-1D + 2D * cf_c);
        return (-System.Math.Sin(theta) * (1.0D - System.Math.Cos(theta))) / 2D;
    }

    public static double Cmac_l_flap(double cf_c)
    {
        double theta = System.Math.Acos(1.0D - 2D * cf_c);
        return (-System.Math.Sin(theta) * (1.0D - System.Math.Cos(theta))) / 2D;
    }

    public static double Lamda2(int flapType, double t_c, double delta_dash)
    {
        double sign_delta_dash;
        double abs_delta_dash;
        if (delta_dash > 0.0D)
        {
            sign_delta_dash = 1.0D;
            abs_delta_dash = delta_dash;
        }
        else
        {
            sign_delta_dash = -1D;
            abs_delta_dash = -delta_dash;
        }
        double ret = 0.0D;
        switch (flapType)
        {
            case 1: // '\001'
                ret = lamda2_planeFlap.F(MathTool.RadToDeg(abs_delta_dash));
                break;

            case 2: // '\002'
                ret = lamda2_splitFlap.F(MathTool.RadToDeg(abs_delta_dash), t_c);
                break;

            case 3: // '\003'
                ret = lamda2_slottedFlap.F(MathTool.RadToDeg(abs_delta_dash), t_c);
                break;
        }
        ret *= sign_delta_dash;
        return ret;
    }

    public static double Delta1(int flapType, double t_c, double cf_c)
    {
        double ret = 0.0D;
        switch (flapType)
        {
            case 1: // '\001'
            case 2: // '\002'
                ret = delta1_pFsF.F(cf_c, t_c);
                break;

            case 3: // '\003'
                ret = delta1_slottedFlap.F(cf_c, t_c);
                break;
        }
        return ret;
    }

    public static double Delta2(int flapType, double t_c, double delta_dash)
    {
        double abs_delta_dash;
        if (delta_dash > 0.0D)
            abs_delta_dash = delta_dash;
        else
            abs_delta_dash = -delta_dash;
        double ret = 0.0D;
        switch (flapType)
        {
            case 1: // '\001'
                ret = delta2_planeFlap.F(MathTool.RadToDeg(abs_delta_dash));
                break;

            case 2: // '\002'
                ret = delta2_splitFlap.F(MathTool.RadToDeg(abs_delta_dash), t_c);
                break;

            case 3: // '\003'
                ret = delta2_slottedFlap.F(MathTool.RadToDeg(abs_delta_dash), t_c);
                break;
        }
        return ret;
    }

    public static double K_cla(double delta, double cf_c)
    {
        delta = Math.Abs(MathTool.RadToDeg(delta));
        double ret = ka.F(delta, cf_c);
        return ret;
    }

    static Flap()
    {
        a_a6 = new Function2D(a_a6_ar, a_a6_a_a6);
        lamda1 = new Function2D(lamda1_c_cf, lamda1_lamda1);
        lamda2_planeFlap = new Function2D(lamda2_planeFlap_delta, lamda2_planeFlap_lamda2);
        lamda2_splitFlap_tc012 = new Function2D(lamda2_splitFlap_tc012_delta, lamda2_splitFlap_tc012_lamda2);
        lamda2_splitFlap_tc021 = new Function2D(lamda2_splitFlap_tc021_delta, lamda2_splitFlap_tc021_lamda2);
        lamda2_splitFlap_lamda2 = (new Function2D[] { lamda2_splitFlap_tc012, lamda2_splitFlap_tc021 });
        lamda2_splitFlap = new Function3D(lamda2_splitFlap_t_c, lamda2_splitFlap_lamda2);
        lamda2_slottedFlap_tc012 = new Function2D(lamda2_slottedFlap_tc012_delta, lamda2_slottedFlap_tc012_lamda2);
        lamda2_slottedFlap_tc021 = new Function2D(lamda2_slottedFlap_tc021_delta, lamda2_slottedFlap_tc021_lamda2);
        lamda2_slottedFlap_lamda2 = (new Function2D[] { lamda2_slottedFlap_tc012, lamda2_slottedFlap_tc021 });
        lamda2_slottedFlap = new Function3D(lamda2_slottedFlap_t_c, lamda2_slottedFlap_lamda2);
        mu1_tc012 = new Function2D(mu1_tc012_c_cf, mu1_tc012_mu1);
        mu1_tc021 = new Function2D(mu1_tc021_c_cf, mu1_tc021_mu1);
        mu1_tc030 = new Function2D(mu1_tc030_c_cf, mu1_tc030_mu1);
        mu1_mu1 = (new Function2D[] { mu1_tc012, mu1_tc021, mu1_tc030 });
        mu1 = new Function3D(mu1_t_c, mu1_mu1);
        delta1_pFsP_tc012 = new Function2D(delta1_pFsF_tc012_c_cf,
                delta1_pFsF_tc012_delta1);
        delta1_pFsP_tc021 = new Function2D(delta1_pFsF_tc021_c_cf,
                delta1_pFsF_tc021_delta1);
        delta1_pFsP_tc030 = new Function2D(delta1_pFsF_tc030_c_cf,
                delta1_pFsF_tc030_delta1);
        delta1_pFsF_delta1 = (new Function2D[] { delta1_pFsP_tc012,
						delta1_pFsP_tc021, delta1_pFsP_tc030 });
        delta1_pFsF = new Function3D(delta1_pFsF_t_c, delta1_pFsF_delta1);
        delta1_slottedFlap_tc012030 = new Function2D(
                delta1_slottedFlap_tc012030_c_cf,
                delta1_slottedFlap_tc012030_delta1);
        delta1_slottedFlap_tc021 = new Function2D(
                delta1_slottedFlap_tc021_c_cf, delta1_slottedFlap_tc021_delta1);
        delta1_slottedFlap_delta1 = (new Function2D[] {
						delta1_slottedFlap_tc012030, delta1_slottedFlap_tc021,
						delta1_slottedFlap_tc012030 });
        delta1_slottedFlap = new Function3D(delta1_slottedFlap_t_c,
                delta1_slottedFlap_delta1);
        delta2_planeFlap = new Function2D(delta2_planeFlap_delta,
                delta2_planeFlap_delta2);
        delta2_splitFlap_tc012 = new Function2D(delta2_splitFlap_tc012_delta,
                delta2_splitFlap_tc012_delta2);
        delta2_splitFlap_tc021 = new Function2D(delta2_splitFlap_tc021_delta,
                delta2_splitFlap_tc021_delta2);
        delta2_splitFlap_tc030 = new Function2D(delta2_splitFlap_tc030_delta,
                delta2_splitFlap_tc030_delta2);
        delta2_splitFlap_delta2 = (new Function2D[] { delta2_splitFlap_tc012,
						delta2_splitFlap_tc021, delta2_splitFlap_tc030 });
        delta2_splitFlap = new Function3D(delta2_splitFlap_t_c,
                delta2_splitFlap_delta2);
        delta2_slottedFlap_tc012 = new Function2D(
                delta2_slottedFlap_tc012_delta, delta2_slottedFlap_tc012_delta2);
        delta2_slottedFlap_tc021 = new Function2D(
                delta2_slottedFlap_tc021_delta, delta2_slottedFlap_tc021_delta2);
        delta2_slottedFlap_tc030 = new Function2D(
                delta2_slottedFlap_tc030_delta, delta2_slottedFlap_tc030_delta2);
        delta2_slottedFlap_delta2 = (new Function2D[] { delta2_slottedFlap_tc012, delta2_slottedFlap_tc021, delta2_slottedFlap_tc030 });
        delta2_slottedFlap = new Function3D(delta2_slottedFlap_t_c, delta2_slottedFlap_delta2);
        ka_cfc1 = new Function2D(ka_cfc1_delta, ka_cfc1_ka);
        ka_cfc2 = new Function2D(ka_cfc2_delta, ka_cfc2_ka);
        ka_cfc3 = new Function2D(ka_cfc3_delta, ka_cfc3_ka);
        ka_cfc4 = new Function2D(ka_cfc4_delta, ka_cfc4_ka);
        ka_ka = (new Function2D[] { ka_cfc1, ka_cfc2, ka_cfc3, ka_cfc4 });
        ka = new Function3D(ka_cfc, ka_ka);
    }
}