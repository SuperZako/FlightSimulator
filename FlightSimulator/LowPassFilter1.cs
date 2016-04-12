// --------------------------------------------------------------------------------------------------
// This file was automatically generated by J2CS Translator (http://j2cstranslator.sourceforge.net/). 
// Version 1.3.6.20110331_01     
// 11/05/19 19:45    
// ${CustomMessageForDisclaimer}                                                                             
// --------------------------------------------------------------------------------------------------
namespace Jp.Maker1.Sim.Tools
{

    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class LowPassFilter1
    {
        public double output;
        public double timeConstant;
        public bool isFirst;

        public LowPassFilter1(double timeConstantIn)
        {
            isFirst = true;
            SetTimeConstant(timeConstantIn);
        }

        public LowPassFilter1(double timeConstantIn, double initValue)
        {
            isFirst = true;
            SetTimeConstant(timeConstantIn);
            Update(initValue, 0.0D);
        }

        public void SetTimeConstant(double timeConstantIn)
        {
            timeConstant = timeConstantIn;
        }

        public void Update(double inputValue, double dt)
        {
            if (isFirst)
            {
                output = inputValue;
                isFirst = false;
            }
            else
            {
                double k;

                if (timeConstant > 0.0D)
                    k = 1.0D - Math.Exp(-1.0D / timeConstant * dt);
                else
                {
                    k = 1.0D;
                }
                output = (k * inputValue + (1.0D - k) * output);
            }
        }

        public double Value()
        {
            return output;
        }

        public static double TimeConstantFrom95pTime(double t95p)
        {
            return -t95p / Math.Log(0.05D);
        }
    }
}