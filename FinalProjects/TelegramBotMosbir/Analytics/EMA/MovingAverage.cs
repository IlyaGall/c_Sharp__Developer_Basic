
namespace Analytics.EMA
{
    static public class MovingAverage
    {
        /// <summary>
        /// Расчёт скользящей средней
        /// </summary>
        /// <param name="kvs">массив значений(цена акции за день)</param>
        /// <param name="period">период</param>
        /// <returns></returns>
        public static double[] EMA(double[] kvs, int period)
        {
            //https://programmersought.com/article/360411371762/

            double[] res = new double[kvs.Length];
            double up = kvs[0];
            res[0] = up; // первый день
            double w1 = (period - 1D) / (period + 1);
            double w2 = 2D / (period + 1);
            for (int i = 1; i < kvs.Length; i++)
            {
                up = (up * w1) + (kvs[i] * w2);
                res[i] = up;
            }
            return res;
        }
    }
}
