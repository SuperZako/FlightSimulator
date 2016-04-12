using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    using System.Threading;

    //FPSを計測・計算するためのクラス
    //init()してから、処理の手前でisJustTime()→描画を終えた後にWaitAndCalc()を呼ぶ

class FPS
{
    //期待するFPS（1秒間に描画するフレーム数）
    public const double FIXED_FPS = 60.0f;	//60FPS
    public const double WAIT_TIME = 1000.0f / FIXED_FPS;
    //実際のFPS
    private double actualFPS = 0.0;
    //1フレームで使える持ち時間定数定義
    private const long PERIOD = (long)(1.0 / FIXED_FPS * 1000); // 単位: ms
    //FPSを計算する間隔定数定義（1s = 10^9ns）
    private const long MAX_STATS_INTERVAL = 1000L; // 単位: ms
    //wait用変数宣言
    private long beforeTime, afterTime, timeDiff, sleepTime;
    private long overSleepTime = 0L;
    private int noDelays = 0;
    // FPS計算用変数宣言
    private long calcInterval = 0L; // in ns
    private long prevCalcTime;
    //次のフレームまで幾つ待つか
    private double nextFrame;
    //フレーム数
    private long frameCount = 0;


    public FPS()
    {

    }
    public void init()
    {
        beforeTime = System.Environment.TickCount;
        nextFrame = prevCalcTime = beforeTime;
    }

    public bool isJustTime()
    {
        return (double)System.Environment.TickCount >= nextFrame;
    }

    //現在の実際のFPS値を返す
    public double getActualFPS()
    {
        return actualFPS;
    }

    public void WaitAndCalc()
    {
        wait();
        calc();
        calcNextTime();
    }

    private void wait()
    {
        //FPSの計算と調整
        afterTime = System.Environment.TickCount;
        timeDiff = afterTime - beforeTime;
        // 前回のフレームの休止時間誤差も引いておく
        sleepTime = (PERIOD - timeDiff) - overSleepTime;

        if (sleepTime > 0)
        {
            // 休止時間がとれる場合
            Thread.Sleep((int)(sleepTime / 1000000L)); // nano->ms
            // sleep()の誤差
            overSleepTime = (System.Environment.TickCount - afterTime) - sleepTime;
        }
        else
        {
            // 状態更新・レンダリングで時間を使い切ってしまい
            // 休止時間がとれない場合
            overSleepTime = 0L;
            // 休止なしが16回以上続いたら
            if (++noDelays >= 16)
            {
                Thread.Sleep(0); // 他のスレッドを強制実行
                noDelays = 0;
            }
        }
        beforeTime = System.Environment.TickCount;
    }

    private void calc()
    {
        frameCount++;
        calcInterval += FPS.PERIOD;

        // 1秒おきにFPSを再計算する
        if (calcInterval >= FPS.MAX_STATS_INTERVAL)
        {
            long timeNow = System.Environment.TickCount;
            // 実際の経過時間を測定
            long realElapsedTime = timeNow - prevCalcTime; // 単位: ms

            // 実際のFPSを算出
            actualFPS = ((double)frameCount / realElapsedTime) * 1000L;
            //次の計算のために
            frameCount = 0L;
            calcInterval = 0L;
            prevCalcTime = timeNow;
        }

    }

    private void calcNextTime()
    {
        nextFrame += FPS.WAIT_TIME;
    }

}