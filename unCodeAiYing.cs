using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using Common.Utilities.Algorithm;
using Model;
//using DotNet.Framework.Common.Algorithm;

namespace yzm
{
    class UnCodeAiYing : UnCodebase
    {
        private List<SampleModel> codes = new List<SampleModel>();
        readonly SecuriteSiteModel trackSite=null;
        public UnCodeAiYing(Bitmap pic, List<SampleModel> codes,SecuriteSiteModel site)
            : base(pic)
        {
            this.codes = codes;
            trackSite = site;
        }

        #region common
        /// <summary>
        /// 得到垂直的有效投影点数组
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        public List<int> GetProjection(Bitmap map)
        {
            Color piexl;
            int nearDots = 0; 
            //逐点判断
            //map = (Bitmap)map.Clone();
            List<int> projection = new List<int>();
            for (int w = 0; w < map.Width; w++)
            {
                projection.Add(0);
                for (int h = 0; h < map.Height; h++)
                {

                    piexl = map.GetPixel(w, h);
                    if (piexl.R < 128)
                    {
                        //认为点数为黑
                        projection[projection.Count - 1] += 1; 
                    }
                    
                }
            }
            return projection;
        } 

        /// <summary>
        /// 得到投影比较结果
        /// projection 为单字符
        /// </summary>
        /// <param name="projection"></param>
        /// <returns></returns>
        public string GetCompareData(List<int> projection)
        {

            //
            //List<int> source = new List<int>();
            SourceInfo targetSource = new SourceInfo();

            //最小欧几里得距离
            double distance = 0;

            return targetSource.StringName;
        }

        #endregion

        public string GetPicnumByBmpCode(Form1 fm)
        {
            GrayByPixels(); //灰度处理
            fm.SetGrayedImg(grayDealed);

            //       codebase.ThiningPic(128);        //细化
            ClearNoise1(128, 1);  //清除噪点
            fm.SetNoiseFixedImg(noiseDealed);

            // 得到垂直投影累加二维数组
            List<int> projection = GetProjection(noiseDealed);
             
            //自动分割数组判断出具体个数和数字内容
            string comp =  GetCompareData(projection);


            //GetPicValidByValue(128, trackSite.CodeNum); //得到有效空间

            //Bitmap[] pics = GetSplitPics(trackSite.CodeNum, 1);     //分割


            //if (pics.Length != trackSite.CodeNum)
            //{
            //    return ""; //分割错误
            //}
            //else  // 重新调整大小
            //{
            //    for (int i = 0; i < trackSite.CodeNum; i++)
            //    {
            //        pics[i] = GetPicValidByValue(pics[i], 128);
            //    }
            //}

            //LevenshteinDistance ld = new LevenshteinDistance();

            //string result = "";

            //{
            //    for (int i = 0; i < trackSite.CodeNum; i++)
            //    {
            //        string code = GetSingleBmpCode(pics[i], 128);   //得到代码串

            //        decimal max = 0.0M;
            //        string value = "";

            //        for (int m = 0; m < codes.Count; m++)
            //        {
            //            SampleModel c = codes[m];
            //            decimal parent = ld.LevenshteinDistancePercent(code, c.Code);
            //            if (parent > max)
            //            {
            //                max = parent;
            //                value = c.TrueValue;
            //            }
            //        }

            //        //如果没有对应的数字则加入？

            //        result = result + value;

            //    }
            //}
            return result;
        }

        public string getPicnum(CheckType ct,Form1 fm)
        {
            if (ct == CheckType.BmpCode) return GetPicnumByBmpCode(fm);
            else if (ct == CheckType.BP) return getPicnumByBP(fm);

            return "";
        }

        public string getPicnumByBP(Form1 fm)
        {
            string result = "";

            return result;
        }
    }


    public  class SourceInfo
    {
        public string StringName { get; set; }

        /// <summary>
        /// 垂直投影数据
        /// </summary>
        public List<int> StringData { get; set; }  

    }
}
