using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class PBRT_r
    {
        string name;
        string pbrtname;
        string pbrtscene;
        int R_X;
        int R_Y;

        int rexstart;
        int x;
        int rexend;

        int reystart;
        int y;
        int reyend;

        public PBRT_r(string N)
        {
            
            pbrtname = N;
            name = new string(pbrtname.ToCharArray(), 0, pbrtname.IndexOf("."));
        }

        public void ReadResolution()
        {
            StreamReader st = new StreamReader(pbrtname, Encoding.Default);
            pbrtscene = st.ReadToEnd();

             rexstart = pbrtscene.IndexOf("xresolution");
            int x = pbrtscene.IndexOf("[", rexstart);
             rexend = pbrtscene.IndexOf("]", x);
            R_X = Convert.ToUInt16(new string(pbrtscene.ToCharArray(), x+1, rexend-x-1));

             reystart = pbrtscene.IndexOf("yresolution");
            int y = pbrtscene.IndexOf("[", reystart);
             reyend = pbrtscene.IndexOf("]", y);
            R_Y = Convert.ToUInt16(new string(pbrtscene.ToCharArray(), y + 1, reyend - y - 1));
            //string rexstr = "xresolution\" [" +  + "]";
            //string scene = new string(pbrtscene.ToCharArray(), 0, sppindex);
            //scene += sppstr;
            //scene = scene + new string(pbrtscene.ToCharArray(), sppend, pbrtscene.Length - sppend);
            //pbrtscene = scene;



            //int Lookat = pbrtscene.IndexOf("LookAt");
            //int i = pbrtscene.IndexOf(" ", Lookat + 6);
            //int j = 0, k;
            //while (j < 6)
            //{
            //    k = pbrtscene.IndexOf(" ", i + 1);
            //    if (k != i + 1)
            //    {
            //        j = j + 1;
            //    }
            //    i = k;
            //}
            //LookAt = Lookat + 6;
            //LookAtEnd = i;


        }


        

        public void makeScene()
        {
            int x = R_X;
            int y = R_Y ;

            for (int k = 0; k < 2; k++)
            {

                string Resolution = " ";
                x = x / 2;
                y = y / 2;
               Resolution = Resolution + "xresolution\" ["  +x.ToString()+"] \"integer yresolution\" ["+y.ToString() ;
                    
          //      LookAtstr += "\r";
               string scene = new string(pbrtscene.ToCharArray(), 0, rexstart);
                scene += Resolution;
                scene = scene + new string(pbrtscene.ToCharArray(), reyend, pbrtscene.Length - reyend);//坐标修改完成

          //      //name修改

                int nameindex = scene.IndexOf("Film");
                nameindex = scene.IndexOf("filename", nameindex);
                nameindex = scene.IndexOf("\"", nameindex + 1);
                nameindex = scene.IndexOf("\"", nameindex + 1);
                int namEnd = scene.IndexOf(".exr", nameindex);
                string namestr = name + "_" + Convert.ToUInt16(x)+"_"+Convert.ToUInt16(y);
                string newscene = new string(scene.ToCharArray(), 0, nameindex + 1);
                newscene += namestr;
              newscene = newscene + new string(scene.ToCharArray(), namEnd, scene.Length - namEnd);

                FileStream fs = new FileStream(namestr + ".pbrt", FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                //开始写入
                sw.Write(newscene);

              sw.Flush();

                sw.Close();
                fs.Close();
            }
        }


    }
}
