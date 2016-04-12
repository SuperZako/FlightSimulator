using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;


namespace FlightSimulator
{
    class Player
    {
        //公開定数定義
        public const int SIZE = 20;			//自機サイズ
        //非公開定数定義
        private const int SPEED_SLOW = 2;	//低速移動速度
        private const int SPEED_NORMAL = 4;	//通常移動速度
        //公開変数
        public Point pos;	//自機座標
        //非公開変数
        private int speed;		//移動速度
        private bool isSlow;	//低速移動か否か？

        //コンストラクタ
        public Player(Point pos)
        {
            pos = pos;
            setSlowMode(true);
        }
        //低速移動に設定するかしないか
        public void setSlowMode(bool flag)
        {
            isSlow = flag;
            if (isSlow)
            {
                speed = SPEED_SLOW;
            }
            else
            {
                speed = SPEED_NORMAL;
            }
        }
        //低速移動モードかどうかの状態
        public bool isSlowMode()
        {
            return isSlow;
        }
        //自機の移動速度を返す
        public int getSpeed()
        {
            return speed;
        }
    }

}
